using System.Runtime.InteropServices;
using CleanArchitecture.Core.ActionOptions;
using Ghanavats.Domain.Primitives;

namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// Domain Factory interface. This is implemented in the Infrastructure layer.
/// </summary>
/// <remarks>
/// Inject this interface in the client code to access its member.
/// </remarks>
/// <typeparam name="TRequest">A mediatr (in this solution) command as IRequest/ICommand, although it does not have to be.</typeparam>
/// <typeparam name="TResponse">An aggregate root object.</typeparam>
public interface IDomainFactory<in TRequest, out TResponse>
    where TRequest : class
    where TResponse : EntityBase, IAggregateRoot
{
    /// <summary>
    /// Create domain entity object
    /// </summary>
    /// <param name="request">The request that is used to create an entity object with</param>
    /// <param name="action">Optional action to enforce further behaviour or options</param>
    /// <returns>An instance of <typeparamref name="TResponse"/></returns>
    TResponse? CreateEntityObject(TRequest request, [Optional] Action<DomainFactoryOption> action);
}

/* README
 Creation of an object can be a major operation in itself, 
 but complex assembly operations do not fit the responsibility of the created objects.
 Combining such responsibilities can produce ungainly designs that are hard to understand. 
 Making the client direct construction muddies the design of the client, 
 breaches encapsulation of the assembled object or AGGREGATE, 
 and overly couples the client to the implementation of the created object.
 
 Complex object creation is a responsibility of the domain layer, 
 yet that task does not belong to the object that express the model. 
 There are some cases in which an object creation and assembly corresponds to a milestone significant in the domain, 
 such as 'open a bank account'. 
 But object creation and assembly usually have no meaning in the domain; they are a necessity of the implementation. 
 To solve this problem, we have to add constructs to the domain design that are not ENTITIES, VALUE OBJECTS, 
 or SERVICES. 
 It is important to make the point clear: 
 We are adding elements to the design that do not correspond to anything in the model, 
 but they are nonetheless part of the domain layer's responsibility.
 
 A program element whose responsibility is the creation of other objects is called a FACTORY.
 Just as the interface of an object should encapsulate its implementation, 
 thus allowing a client to use the object's behaviour without knowing how it works, 
 a FACTORY encapsulates the knowledge needed to create a complex object or AGGREGATE. 
 It provides an interface that reflects the goals of the client and an abstract view of the created object.
 
 Shift the responsibility for creating instances of complex objects and AGGREGATES to a separate object, 
 which may itself have no responsibility in the domain model but is still part of the domain design. 
 Provide an interface that encapsulates all complex assembly, 
 and that does not require the client to reference the concrete classes of the objects being instantiated. 
 Create entire AGGREGATES as a piece, enforcing their invariants.
 
 Basic requirements for a good factory:
 
 1. Each creation method is atomic and enforces all invariants of the created object or AGGREGATE. 
 A FACTORY should only be able to produce an object in a consistent state. 
 For an ENTITY, this means the creation of the entire AGGREGATE, 
 with all invariants satisfied, but probably with optional elements still to be added. 
 For an immutable VALUE OBJECT, this means that all attributes are initialised to their final state. 
 If the interface makes it possible to request an object that can't be created correctly, 
 then an exception should be raised, 
 or some other mechanism should be invoked that will ensure that no improper return value is possible.
 
 2. The FACTORY should be abstracted to the type desired, rather than the concrete class(es) created.
 */
