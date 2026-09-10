# Kestrel

**Execution-first workflow & bug-collaboration tool for QA and engineering teams.**

Kestrel cuts down the turnaround time between a bug being found and a bug being fixed - by making written structure, not scattered docs or verbal back-and-forth, the backbone of how work gets reported, tracked and resolved.

---

## Why did I build this?

Working at Actriv, I watched a teammate lean on Google Translate for 90% of our conversations just to get past a communication barrier , Kestrel grew out of that: a tool where clear written structure, not verbal back-and-forth, drives how work gets reported and resolved.

---

## The problem

Bugs reported by QA often get lost in documents, threads, or verbal handoffs — and can take weeks to reach the engineers who need to fix them. Kestrel exists to close that gap: automating the busywork around reporting so teams can focus on execution instead of chasing context.

---

## Features

- **AI-powered bug report generation** — turn a plain description into a structured, ready-to-triage bug report
- **AI task extraction** — surface and remind engineers about high-priority bugs automatically
- **Default reporting structure** — consistent report format generated for you, no more ad-hoc docs
- **Collaboration tools** — comments and feedback directly on reports/documents
- **Focus features** — cut noise, keep attention on what's actionable
- **Async team coordination** — built for teams that aren't always talking in real time

---

## Tech stack

| Layer | Tech |
|---|---|
| Backend | C# / .NET, EF Core |
| Database | MSSQL |
| Caching / Real-time | Redis |
| Desktop client | Avalonia |
| Web dashboard | React |

The desktop and web clients both consume the same backend API.

---

## Status

🚧 Actively in development - backend is being rewritten in C#, with a full MSSQL schema and EF Core models scaffolded.

---

## Roadmap

- [ ] Finish EF Core backend rewrite
- [ ] Ship AI-assisted bug report generation
- [ ] Build out Avalonia desktop client
- [ ] Build out React web dashboard
- [ ] Async coordination features

---

## License

_TBD_
