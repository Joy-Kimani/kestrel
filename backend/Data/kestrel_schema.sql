/* =========================================================
   KESTREL — Database Schema (MSSQL / T-SQL)
   Grouped by domain. Designed to be scaffolded via EF Core
   Code-First or reverse-engineered with `dotnet ef dbcontext scaffold`.
   ========================================================= */

/* ---------------------------------------------------------
   1. CORE — Users, Teams, Projects, Membership
   --------------------------------------------------------- */

CREATE TABLE Users (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Email           NVARCHAR(256)   NOT NULL UNIQUE,
    DisplayName     NVARCHAR(128)   NOT NULL,
    PasswordHash    NVARCHAR(256)   NOT NULL,
    AvatarUrl       NVARCHAR(512)   NULL,
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    LastActiveAt    DATETIME2       NULL,
    IsActive        BIT             NOT NULL DEFAULT 1
);

CREATE TABLE Teams (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name            NVARCHAR(128)   NOT NULL,
    OwnerId         UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE TeamMembers (
    TeamId          UNIQUEIDENTIFIER NOT NULL REFERENCES Teams(Id),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    RoleInTeam      NVARCHAR(32)    NOT NULL DEFAULT 'Member', -- Member, Lead, Admin
    JoinedAt        DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    PRIMARY KEY (TeamId, UserId)
);

CREATE TABLE Projects (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TeamId          UNIQUEIDENTIFIER NOT NULL REFERENCES Teams(Id),
    Name            NVARCHAR(128)   NOT NULL,
    Description     NVARCHAR(1024)  NULL,
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE UserSettings (
    UserId              UNIQUEIDENTIFIER PRIMARY KEY REFERENCES Users(Id),
    Theme               NVARCHAR(32)   NOT NULL DEFAULT 'System',
    NotificationLevel   NVARCHAR(32)   NOT NULL DEFAULT 'All', -- All, HighPriorityOnly, Mute
    ApiKeysEncrypted    NVARCHAR(MAX)  NULL,  -- encrypted JSON blob for integrations
    UpdatedAt           DATETIME2      NOT NULL DEFAULT SYSUTCDATETIME()
);


/* ---------------------------------------------------------
   2. BUG WORKFLOW — the execution-first core
   --------------------------------------------------------- */

CREATE TABLE BugReports (
    Id                  UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProjectId           UNIQUEIDENTIFIER NOT NULL REFERENCES Projects(Id),
    ReporterId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    AssigneeId          UNIQUEIDENTIFIER NULL REFERENCES Users(Id),
    Title               NVARCHAR(256)   NOT NULL,
    RawDescription      NVARCHAR(MAX)   NULL,   -- plain text QA typed in
    StructuredReport    NVARCHAR(MAX)   NULL,   -- AI-generated structured version (JSON or Markdown)
    Status              NVARCHAR(32)    NOT NULL DEFAULT 'New', -- New, Triaged, InProgress, Fixed, Verified, Closed
    Priority            NVARCHAR(16)    NOT NULL DEFAULT 'Medium', -- Low, Medium, High, Critical
    Severity            NVARCHAR(16)    NULL,
    CreatedAt           DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt           DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    ResolvedAt          DATETIME2       NULL
);

CREATE INDEX IX_BugReports_Project_Status ON BugReports(ProjectId, Status);
CREATE INDEX IX_BugReports_Assignee ON BugReports(AssigneeId);

CREATE TABLE BugAttachments (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BugReportId     UNIQUEIDENTIFIER NOT NULL REFERENCES BugReports(Id),
    FileUrl         NVARCHAR(512)   NOT NULL,
    FileType        NVARCHAR(32)    NOT NULL, -- Screenshot, Recording, Log, Other
    UploadedById    UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE BugStatusHistory (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BugReportId     UNIQUEIDENTIFIER NOT NULL REFERENCES BugReports(Id),
    OldStatus       NVARCHAR(32)    NULL,
    NewStatus       NVARCHAR(32)    NOT NULL,
    ChangedById     UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    ChangedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Tags (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProjectId       UNIQUEIDENTIFIER NOT NULL REFERENCES Projects(Id),
    Name            NVARCHAR(64)    NOT NULL,
    Color           NVARCHAR(16)    NULL
);

CREATE TABLE BugReportTags (
    BugReportId     UNIQUEIDENTIFIER NOT NULL REFERENCES BugReports(Id),
    TagId           UNIQUEIDENTIFIER NOT NULL REFERENCES Tags(Id),
    PRIMARY KEY (BugReportId, TagId)
);


/* ---------------------------------------------------------
   3. AI FEATURES — extraction, reminders
   --------------------------------------------------------- */

CREATE TABLE AiTaskExtractions (
    Id                  UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    BugReportId         UNIQUEIDENTIFIER NOT NULL REFERENCES BugReports(Id),
    RawInput            NVARCHAR(MAX)   NOT NULL,
    ExtractedJson       NVARCHAR(MAX)   NOT NULL,  -- structured output: steps, expected/actual, env, etc.
    ModelUsed           NVARCHAR(64)    NOT NULL,
    CreatedAt           DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE AiReminders (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    BugReportId     UNIQUEIDENTIFIER NULL REFERENCES BugReports(Id),
    ReminderText    NVARCHAR(512)   NOT NULL,
    TriggerAt       DATETIME2       NOT NULL,
    IsSent          BIT             NOT NULL DEFAULT 0,
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE INDEX IX_AiReminders_TriggerAt ON AiReminders(TriggerAt) WHERE IsSent = 0;


/* ---------------------------------------------------------
   4. COLLABORATION — comments/feedback on documents & bugs
   --------------------------------------------------------- */

-- "Documents" covers specs, design docs, or any written artifact
-- that isn't a bug report but still needs comment threads.
CREATE TABLE Documents (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProjectId       UNIQUEIDENTIFIER NOT NULL REFERENCES Projects(Id),
    Title           NVARCHAR(256)   NOT NULL,
    Content         NVARCHAR(MAX)   NULL,
    CreatedById     UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

-- Polymorphic-ish comment target via two nullable FKs + a CHECK
-- (simpler in MSSQL than a generic polymorphic association table).
CREATE TABLE Comments (
    Id                  UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    DocumentId          UNIQUEIDENTIFIER NULL REFERENCES Documents(Id),
    BugReportId         UNIQUEIDENTIFIER NULL REFERENCES BugReports(Id),
    ParentCommentId     UNIQUEIDENTIFIER NULL REFERENCES Comments(Id), -- threaded replies
    AuthorId            UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    Content              NVARCHAR(2000)  NOT NULL,
    IsResolved          BIT             NOT NULL DEFAULT 0,
    CreatedAt           DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt           DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT CK_Comments_OneTarget CHECK (
        (CASE WHEN DocumentId IS NOT NULL THEN 1 ELSE 0 END) +
        (CASE WHEN BugReportId IS NOT NULL THEN 1 ELSE 0 END) = 1
    )
);

CREATE TABLE CommentReactions (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CommentId       UNIQUEIDENTIFIER NOT NULL REFERENCES Comments(Id),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    ReactionType    NVARCHAR(16)    NOT NULL, -- ThumbsUp, Heart, Eyes, etc.
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_CommentReaction UNIQUE (CommentId, UserId, ReactionType)
);


/* ---------------------------------------------------------
   5. FOCUS FEATURES — deep-work session tracking
   --------------------------------------------------------- */

CREATE TABLE FocusSessions (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    BugReportId     UNIQUEIDENTIFIER NULL REFERENCES BugReports(Id), -- optional: session tied to a specific bug
    StartedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME(),
    EndedAt         DATETIME2       NULL,
    DurationMinutes AS DATEDIFF(MINUTE, StartedAt, EndedAt) PERSISTED,
    Notes           NVARCHAR(512)   NULL
);

CREATE INDEX IX_FocusSessions_User ON FocusSessions(UserId, StartedAt);


/* ---------------------------------------------------------
   6. ASYNC TEAM COORDINATION — status updates, notifications
   --------------------------------------------------------- */

-- Async "standup" style updates, not tied to a single bug.
CREATE TABLE StatusUpdates (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    TeamId          UNIQUEIDENTIFIER NOT NULL REFERENCES Teams(Id),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    Content         NVARCHAR(1024)  NOT NULL,
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Notifications (
    Id              UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserId          UNIQUEIDENTIFIER NOT NULL REFERENCES Users(Id),
    Type            NVARCHAR(32)    NOT NULL, -- BugAssigned, BugEscalated, CommentReply, ReminderDue, StatusUpdatePosted
    PayloadJson     NVARCHAR(MAX)   NULL,     -- flexible payload (bug id, comment id, etc.)
    IsRead          BIT             NOT NULL DEFAULT 0,
    CreatedAt       DATETIME2       NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE INDEX IX_Notifications_User_Unread ON Notifications(UserId) WHERE IsRead = 0;
