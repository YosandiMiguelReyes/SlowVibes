## Context

The project initially considered placing the `Result<T>` pattern inside the Domain layer.

After reviewing the responsibilities of each layer, it became clear that the Domain should model business concepts and business rules rather than communication mechanisms between layers.

---

## Decision

`Result<T>` will belong to the Application layer instead of the Domain.

The Domain will remain independent of response models and application flow concerns.

---

## Rationale

The Domain should describe business behavior, not how operations communicate success or failure.

`Result<T>` is an application concern because it represents the outcome of a use case rather than a business concept.

Keeping it outside the Domain preserves separation of concerns and respects the Dependency Rule.