﻿namespace CleanArchitecture.Core.Interfaces;

/// <summary>
/// This is marker interface. You can use this to mark your root entities.
/// By marking your root entities, you can apply contraint to your Respository, so domain operations are done only on root entities.
/// </summary>
public interface IAggregateRoot { }