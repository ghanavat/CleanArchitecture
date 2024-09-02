using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace SampleApi.CustomOpenApiProcessors;

/// <summary>
/// Custom operation processor
/// </summary>
public class CustomOperationProcessor : IOperationProcessor
{
    /// <summary>
    /// Processes customisation configuration for Operation context
    /// </summary>
    /// <param name="context">The context that this processor is written for and using to customise its properties.</param>
    /// <returns></returns>
    public bool Process(OperationProcessorContext context)
    {
        context.OperationDescription.Operation.OperationId = context.MethodInfo.Name;
        return true;
    }
}
