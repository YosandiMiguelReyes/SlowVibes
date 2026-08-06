## Context

The project contains multiple repositories that share the same CRUD operations.

Two approaches were considered:

1. Create a dedicated repository interface and implementation for every entity.
2. Use a generic `IBaseRepository<TEntity>` with a reusable `BaseRepository<TEntity>` implementation.

Although business-specific repositories provide a more expressive API, they also introduce duplicated CRUD code for entities that only require basic persistence operations.

---

## Decision

The project will use a Generic Base Repository.

Business-specific repositories will inherit from the generic implementation and expose additional methods only when required by the business.

---

## Rationale

The primary goal of SlowVibes is to learn Clean Architecture and modern .NET development while building a production-quality portfolio project.

Using a Generic Repository reduces duplicated CRUD code and allows development time to be focused on business logic, Entity Framework Core, and application architecture.

This decision was made consciously after evaluating the trade-offs.

## References

- Clean Architecture – Robert C. Martin
- Domain-Driven Design Distilled – Vaughn Vernon

---

## Learning Notes

This decision was made after evaluating both approaches rather than following a tutorial or template. The trade-off was considered acceptable for the educational goals and scope of this project.