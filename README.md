# EShopMS

Tech and techniques used:
- [Postgres | Marten](https://martendb.io/introduction.html): Transforms Postgres into transactional document database
- [Vertical Slice Architecture](https://www.milanjovanovic.tech/blog/vertical-slice-architecture)
- [CQRS Pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Mediator Pattern](https://refactoring.guru/design-patterns/mediator)
    - [MediatR](https://mediatr.io)
- [Carter](https://github.com/CarterCommunity/Carter): Minimal API setup
- [Mapster](https://github.com/MapsterMapper/Mapster): Object mapper
- [FluentValidation](https://docs.fluentvalidation.net/en/latest): Object validation



## MediatR and the Mediator Pattern
The Mediator Pattern decouples components by providing a common interface they can communicate through, instead of directly communicating with one another.
This pattern exemplifies the Dependency Inversion principle, because components depend on the mediator interface (an abstraction) rather than directly on each other, applying DIP even between peers.

