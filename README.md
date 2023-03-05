# event-source


Goals:

-The event store and the query store will be implemented in-memory, assuming they could be replaced with a data store in the future
-I decided against using a message queue like Rabbit MQ or Kafka to keep the architecture simple. Instead I implemented an in-memory observer pattern
-The architecture is built independent of UI decisions or a REST endpoint. These could easily be added in the future