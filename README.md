This is only functional concept and missing many aspects

I am trying to maximize my time on the business logic

- Needs authentication and authorisation
- Needs async
- Needs permanent storage 
- Needs Moc testing instead
- Needs logging
- Needs separation of contracts
- possible Thread issues and multiple requests, needs request id
- unit tests is only focused on business logic (TDD)
- needs exception handling

Providers

- Needs editing schedule
- Providers need to be registered with provider id
- assumption here is that an hour has 4 slots and they start at 0 min
- we only accept full hour start and end
- remove duplicates
- use of strings in url as provider indicator is bad and will fail for some names. Need ids instead.

Clients

- need a separate time slots object from calender entry objects
- doesnt account for duplicates












