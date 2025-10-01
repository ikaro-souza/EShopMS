# EShopMS

## Tech and techniques used:

- [Postgres | Marten](https://martendb.io/introduction.html): Transforms Postgres into transactional document database
- [Vertical Slice Architecture](https://www.milanjovanovic.tech/blog/vertical-slice-architecture)
- [Carter](https://github.com/CarterCommunity/Carter): Minimal API setup
- [Mapster](https://github.com/MapsterMapper/Mapster): Object mapper
- [FluentValidation](https://docs.fluentvalidation.net/en/latest): Object validation
- [MediatR](https://mediatr.io)

## Key Concepts and Patterns

### [CQRS Pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)

### [Mediator Pattern](https://refactoring.guru/design-patterns/mediator)

The Mediator Pattern decouples components by providing a common interface they can communicate through, instead of
directly communicating with one another.
This pattern exemplifies the Dependency Inversion principle, because components depend on the mediator interface (an
abstraction) rather than directly on each other, applying DIP even between peers.

### [Dual write Problem](https://www.confluent.io/blog/dual-write-problem)

- Description: The Dual Write Problem is an intrinsic problem that distributed system (such as microservices) face.
  It happens when two systems have to update data based on an event, but one of them fail, leading to an inconsistent
  state.
- Solution: [Transactional Outbox Pattern](https://microservices.io/patterns/data/transactional-outbox.html)
  This patterns aims to solve the Dual Write problem by keeping a sort of ephemeral ledger in the service that emits
  the event in its database as a table, saving it as part of the transaction that updated the domain entitie(s). Then
  another
  process in the service is responsible for taking the stored messages and deleting them as they're sent to the message
  broker.

### [Saga Pattern](https://microservices.io/patterns/data/saga.html):

The saga pattern aims to provide data consistency across multiple services, by "chaining" a sequence of local
transactions.
In this pattern, a service updates its own database and sends a message to a message broker, another service consumes
this message and updates its own database, and so on and so forth, thus creating a chain of events.
It's essential to implement a rollback mechanism so that in case a service fails or rejects the event for some reason,
the previous services are able to revert the changes as well.

### Domain and Integration Events:

Although on the surface they are the same, a "message" that something happened, their context and implementation details
differ slightly.

- Domain events exist in the context of a domain/microservices, usually are sent after a change in
  an [aggregate](https://martinfowler.com/bliki/DDD_Aggregate.html)
  and handled by the same service that generated the event.
- Integration Events span the context of multiple services/domains, they're meant to communicate changes in the
  state of one service
  to all the others that need to know about it via a message broker like RabbitMQ.

### Gateway Routing Pattern:

In this pattern, the backend only exposes a single API, no matter how many services it has.
This is useful to simplify the integration between clients and the multiple services and helps decouple them even
further, for example, you can rewrite the entire auth service and as long as the API stays the same, nothing will change
for the client.

### API Gateway Pattern

The API Gateway is the single API that clients use to communicate with different services.
With an API Gateway you can aggregate calls to multiple services into a single endpoint, allowing complex operations to
handled while keeping the communication with client as simple as sending a single HTTP request and waiting for a
response.
The API Gateway is also responsible for cross-cutting concerns such as SSL termination, Caching, Rate Limiting, etc.

### Backend for Frontend (BFF)

In the context of API Gateways, this pattern aims to separate a single API Gateway into multiple smaller ones.
The reasons for this segregation can be summarized into two:

- To provide better resilience: A single API Gateway is a point of failure that effectively takes down the entire
  backend.
- Not to have a God Class: By having a single API Gateway, specially if it aggregates service calls, it can
  fall into the same problem of God service classes in traditional Layer Architecture APIs.

Another benefit of creating multiple gateways, is that each one can service a specific client better, by focusing
on the needs of that client. That can also be the reason for splitting gateways, which will come with the benefits
listed above.