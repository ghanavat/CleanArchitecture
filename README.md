# Clean Architecture template

A Clean Architecture approach to design better applications.

![Clean Architecture layers!](./CleanArchitecture.jpg "Clean Architecture")

## Overview
This is a sample application solution designed with Clean Architecture rules. In a nutshell, it's intended to be a correct/proper (I'll try!) reference for new projects :)

In Clean Architecture, the dependency rule says that projects must point inwards. All layers referenced inwards to the **Domain** layer.

## The image above
I decided to put this image to remind ourselves what Clean Architecture looks like in theory.
In the following sections, I explain each layer in my own language.

### Entities
This is the core of the architecture.
You use this layer to encapsulate all _business wide_ rules.
In this document and the template, I'm calling it **Core** layer.

Don't let the word *Entities* to confuse you. 
In a large enterprise solution, they are encapsulating business rules. 
At first glance, they may look like a model with some logics in them. 
However, for single applications, Entities are business objects, but still encapsulate business rules and are not likely to be changed often.

### Use Cases
This layer is an orchestrator. It's a wrapper around the **Entities** (Core) layer. 
We call it *UseCases* in this template too, but often it's called *Application* layer. 
It is a layer in which all application logics live.

Changes in this layer should not affect the *Entities*. 
Also, changes done in other layers should not affect the **UseCases** either.

### Interface Adapters (Controllers, Gateways, Presenters)
Think of this layer as your application entry point. 
API endpoints are defined in this layer, and it is also the layer in which the data structure mapping should be done.

We call this layer in this template, **Presentation**
(I have seen examples that others call this layer Web, but it's confusing to me!: D). 
We use this layer as an _entry point_;
to map data from the format that was used/needed for UserCases and Entities 
to the format they need to be for external agencies/parties such as Database or Web/UI.

### Frameworks and Drivers
This layer is the last one in Clean Architecture approach. It is the layer where all the software details should be living. The intention here is to provide an environment in which the *details* can cause little/no chaos.

The *details* bit mentioned in this layer can also be the implementation of your **Command** (mutations) *interfaces* that are defined in the **Entities** layer.

In this template, we named this layer **Infrastructure**.

> Note: According to Uncle Bob explanation, the Web/UI is also a detail and Clean Architecture rule says it should be kept on the outside. 

## Repository Structure
The structure of this project template is based on Clean Architecture approach. 
It is without doubt one of the most popular approaches to structure a software solution.

Below, you will find out, in outward order, the responsibility of each layer.
Note, they are layers not *.csproj* projects.

### Core (a.k.a Domain)
The **Core** or **Domain** layer is the heart of the Clean Architecture. It contains of all business logics, Entities and Aggregates.

An *Aggregate* is a group of Entities that should change together. So, Entities that are mutated together are placed under *Aggregates*.

>Note: This project is referenced by **Infrastructure** and **UseCases** projects only, and it does not reference any project from outer layers.
> The reason it is referenced by Infrastructure is because that the domain entities are populated and needed by Infrastructure.

### UseCases (a.k.a. Application)
The Application layer is our orchestrator layer for the Domain. It's the next layer sitting next to the Domain layer.
In other words, it is a wrapper around the Domain layer.

This is the layer in which a Command and Query (CQRS) approach can be used to handle the use cases.

You would also need to put all **Query** related *interfaces* in this layer.

This layer/project references **Core** project/layer.

### Infrastructure
This layer, as its names suggests, takes care of any external facing implementation/services, 
such as Database and Web/APIs. 
All ORM related implementations working with files and sending email, etc. are all part of this layer.

> Please note: This layer references the **Core/Domain** project, and in some cases it might need to reference **UseCases** project.

It is also important to note that the **Query** *interfaces* that you put in the **Core** project are implemented in this layer.

Originally, I configured this layer to use MongoDB EF Core. 
I had some challenges,
one of which was deserialisation between MongoDb '_id' with type ObjectId, to the domain entities identity properties.

The Core dependency rule here is that 
we must keep the Core layer protected from any non-domain dependencies or third parties.
So, I implemented a custom value converter, _CustomObjectIdConverter.cs_,
to take care of the type conversions and configured the DBContext class to use it.
However, later I found out this was a bit overkill.
The below code works just fine.

```csharp
HasBsonRepresentation(BsonType.ObjectId);
```

If you decide to not use MongoDb EF Core package, and instead you like to use MongoDb Driver, you must consider a different configuration using _BsonClassMap.TryRegisterClassMap_ for the ObjectId to be serialised/deserialised without having to decorate your domain entities.

```csharp
BsonClassMap.RegisterClassMap<SampleEntity>(map =>
    {
        map.MapIdMember(x => x.Id)
            .SetIdGenerator(StringObjectIdGenerator.Instance)
            .SetSerializer(new StringSerializer(BsonType.ObjectId));
    });
```

Currently, the infrastructure is based on SQL Db and EF Core.

### Presentation
The Presentation layer (can also be named Web) is the entry point into the project. 
It is also where the API project resides. 
According to Uncle Bob diagram and explanation, the data structure mapping is done in this layer.

## Shared
The **Shared** (also named *SharedKernel* by some other developers) project is not a layer in Clean Architecture. 
It is only a project in the solution to encapsulate 
and accommodate the shared stuff and non-domain related base classes.
