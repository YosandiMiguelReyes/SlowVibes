# ADR-005: Use Enums for Closed Domain Value Sets

## Context

Several domain concepts initially used integers or strings to represent a fixed set of values.

Examples included:

- Order status
- Order source
- Delivery type
- Payment status
- Payment method
- User role

Using arbitrary integers or strings makes the Domain less expressive and allows invalid values to be represented.

## Decision

Closed sets of Domain values will be represented using enums.

Examples include:

- OrderStatuses
- OrderSources
- DeliveryTypes
- PaymentStatus
- PaymentMethods
- Role
- NotificationStatus

Persisted/configurable lookup data such as InventoryMovementReason and InventoryMovementType will remain entities rather than enums because their values are intended to be configurable data.

## Rationale

Enums make the Domain language explicit and prevent arbitrary values from being assigned.

For example:

`OrderStatuses.Pending`

is more expressive than:

`OrderStatusId = 0`

This approach is appropriate when the set of values is controlled by the application and is not expected to be dynamically created by administrators.

## Consequences

Persistence is responsible for deciding how enum values are stored in SQL Server.

If a concept becomes dynamically configurable in the future, it can be modeled as a persisted entity instead of an enum.