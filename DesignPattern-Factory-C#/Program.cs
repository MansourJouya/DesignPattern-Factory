using System;
using System.Collections.Generic;

/// <summary>
/// Interface for workflow steps that defines the contract for executing steps.
/// </summary>
public interface IWorkflowStep
{
    void ExecuteStep();
}

/// <summary>
/// Concrete step for validating orders.
/// </summary>
public class ValidateOrder : IWorkflowStep
{
    /// <summary>
    /// Executes the validation step of the order processing workflow.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Validating Order...");
}

/// <summary>
/// Concrete step for processing payments.
/// </summary>
public class ProcessPayment : IWorkflowStep
{
    /// <summary>
    /// Executes the payment processing step of the order processing workflow.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Processing Payment...");
}

/// <summary>
/// Concrete step for shipping orders.
/// </summary>
public class ShipOrder : IWorkflowStep
{
    /// <summary>
    /// Executes the shipping step of the order processing workflow.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Shipping Order...");
}

/// <summary>
/// Concrete step for generating invoices.
/// </summary>
public class GenerateInvoice : IWorkflowStep
{
    /// <summary>
    /// Executes the invoice generation step of the invoice processing workflow.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Generating Invoice...");
}

/// <summary>
/// Concrete step for sending invoices.
/// </summary>
public class SendInvoice : IWorkflowStep
{
    /// <summary>
    /// Executes the step for sending invoices to customers.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Sending Invoice to Customer...");
}

/// <summary>
/// Custom step for preparing a special order.
/// </summary>
public class PrepareSpecialOrder : IWorkflowStep
{
    /// <summary>
    /// Executes the special order preparation step.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Preparing Special Order...");
}

/// <summary>
/// Custom step for notifying customers.
/// </summary>
public class NotifyCustomer : IWorkflowStep
{
    /// <summary>
    /// Executes the step for notifying customers about their orders.
    /// </summary>
    public void ExecuteStep() => Console.WriteLine("Notifying Customer...");
}

/// <summary>
/// Abstract factory class for creating workflow steps.
/// </summary>
public abstract class WorkflowFactory
{
    /// <summary>
    /// Creates a list of workflow steps.
    /// </summary>
    /// <returns>A list of IWorkflowStep instances.</returns>
    public abstract List<IWorkflowStep> CreateWorkflowSteps();
}

/// <summary>
/// Concrete factory for creating order processing workflow steps.
/// </summary>
public class OrderProcessingFactory : WorkflowFactory
{
    /// <summary>
    /// Creates the steps for the order processing workflow.
    /// </summary>
    /// <returns>A list of order processing steps.</returns>
    public override List<IWorkflowStep> CreateWorkflowSteps() => new List<IWorkflowStep>
    {
        new ValidateOrder(),
        new ProcessPayment(),
        new ShipOrder()
    };
}

/// <summary>
/// Concrete factory for creating invoice processing workflow steps.
/// </summary>
public class InvoiceProcessingFactory : WorkflowFactory
{
    /// <summary>
    /// Creates the steps for the invoice processing workflow.
    /// </summary>
    /// <returns>A list of invoice processing steps.</returns>
    public override List<IWorkflowStep> CreateWorkflowSteps() => new List<IWorkflowStep>
    {
        new GenerateInvoice(),
        new SendInvoice()
    };
}

/// <summary>
/// Concrete factory for creating custom workflow steps.
/// </summary>
public class CustomWorkflowFactory : WorkflowFactory
{
    /// <summary>
    /// Creates the steps for the custom workflow.
    /// </summary>
    /// <returns>A list of custom workflow steps.</returns>
    public override List<IWorkflowStep> CreateWorkflowSteps() => new List<IWorkflowStep>
    {
        new PrepareSpecialOrder(),
        new NotifyCustomer()
    };
}

/// <summary>
/// Class responsible for executing workflows using a specific workflow factory.
/// </summary>
public class WorkflowProcessor
{
    private readonly WorkflowFactory _workflowFactory;

    /// <summary>
    /// Initializes a new instance of the WorkflowProcessor class.
    /// </summary>
    /// <param name="workflowFactory">The factory that creates workflow steps.</param>
    public WorkflowProcessor(WorkflowFactory workflowFactory)
    {
        _workflowFactory = workflowFactory;
    }

    /// <summary>
    /// Executes the workflow steps created by the factory.
    /// </summary>
    public void ExecuteWorkflow()
    {
        var steps = _workflowFactory.CreateWorkflowSteps();
        foreach (var step in steps)
        {
            try
            {
                step.ExecuteStep();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing step: {ex.Message}");
            }
        }
    }
}

/// <summary>
/// Client code for executing workflows.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point of the program.
    /// </summary>
    public static void Main()
    {
        // Execute Order Processing Workflow
        WorkflowProcessor orderProcessor = new WorkflowProcessor(new OrderProcessingFactory());
        Console.WriteLine("Executing Order Processing Workflow:");
        orderProcessor.ExecuteWorkflow();

        // Execute Invoice Processing Workflow
        WorkflowProcessor invoiceProcessor = new WorkflowProcessor(new InvoiceProcessingFactory());
        Console.WriteLine("\nExecuting Invoice Processing Workflow:");
        invoiceProcessor.ExecuteWorkflow();

        // Example of using a new custom workflow
        WorkflowProcessor customProcessor = new WorkflowProcessor(new CustomWorkflowFactory());
        Console.WriteLine("\nExecuting Custom Workflow:");
        customProcessor.ExecuteWorkflow();
    }
}
