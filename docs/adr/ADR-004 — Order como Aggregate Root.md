# ADR-004: Model Order as an Aggregate Root

## Context

An Order contains one or more OrderItems and is responsible for financial totals and order lifecycle.

Initially, OrderItems could be created independently and required an OrderId during construction. This made the Domain depend unnecessarily on a database-generated identifier.

The design was reconsidered using aggregate boundaries.

## Decision

`Order` will be modeled as the Aggregate Root for OrderItems.

OrderItems will be created without requiring an OrderId in their Domain factory.

The Order aggregate will control:

- Adding items
- Removing items
- Changing item quantities
- Recalculating order totals
- Completing an order
- Cancelling an order

OrderItems will remain responsible for their own invariants and profit calculation.

The `OrderId` property may still exist on OrderItem for Persistence/EF Core relationship mapping, but it is not required during Domain construction.

## Rationale

The Order owns the collection of OrderItems and is responsible for maintaining consistency across the aggregate.

This prevents external code from modifying the OrderItems collection directly and keeps business rules related to the order inside the aggregate boundary.

It also prevents the Domain creation process from depending on a database-generated OrderId.

## Consequences

Order becomes the entry point for modifying its OrderItems.

Persistence may still map the OrderId foreign key.

Application services will orchestrate use cases without directly manipulating the OrderItems collection.