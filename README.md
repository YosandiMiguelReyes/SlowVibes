Temp notes 
ADR-001: The project uses a generic IBaseRepository to reduce duplicated CRUD code and keep the learning focus on Clean Architecture, EF Core, and application design. We recognize that some teams prefer business-specific repository interfaces only, but for this project's goals, the trade-off is acceptable.

----------------------------
ADR 002
Remove Result<T> from the Domain layer.

Context

The Domain layer represents business concepts and business rules. Its responsibility is to model the problem domain without depending on application concerns or infrastructure.

Result<T> was initially placed in the Domain layer.

Decision

Result<T> will not belong to the Domain.

It will live in the Application layer (or a shared Application abstractions project if we ever split it further).

Reasoning

The Domain should answer questions like:

Is this Product valid?
Can this Order be completed?
Can this Discount be applied?

It should not decide how an operation communicates success or failure.

Result<T> is a communication mechanism between the Application layer and its consumers (API, MVC, gRPC, etc.), not a business concept.
