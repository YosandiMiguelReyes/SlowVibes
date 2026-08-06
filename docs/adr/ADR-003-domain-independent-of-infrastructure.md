## Context

Clean Architecture places the Domain at the center of the application.

A decision was required regarding whether the Domain should reference technologies such as Entity Framework Core, ASP.NET Core, databases, or other infrastructure concerns.

---

## Decision

The Domain layer will remain completely independent of infrastructure and external technologies.

Dependencies must always point toward the Domain.

---

## Rationale

Business rules should remain valid regardless of the persistence mechanism, presentation layer, or framework being used.

Keeping the Domain independent allows infrastructure to evolve without affecting business logic.
