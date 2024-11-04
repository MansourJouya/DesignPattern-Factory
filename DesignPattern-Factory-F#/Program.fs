open System
open System.Collections.Generic

// IWorkflowStep interface: Defines the contract for each step in the workflow
type IWorkflowStep =
    abstract member ExecuteStep: unit -> unit

// ValidateOrder class: Validates an order as part of the workflow
type ValidateOrder() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Validating Order...")

// ProcessPayment class: Processes payment in the workflow
type ProcessPayment() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Processing Payment...")

// ShipOrder class: Ships the order as part of the workflow
type ShipOrder() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Shipping Order...")

// GenerateInvoice class: Generates an invoice in the workflow
type GenerateInvoice() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Generating Invoice...")

// SendInvoice class: Sends the invoice to the customer
type SendInvoice() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Sending Invoice to Customer...")

// PrepareSpecialOrder class: Prepares a special order as part of the custom workflow
type PrepareSpecialOrder() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Preparing Special Order...")

// NotifyCustomer class: Notifies the customer as part of the custom workflow
type NotifyCustomer() =
    interface IWorkflowStep with
        member this.ExecuteStep() =
            Console.WriteLine("Notifying Customer...")

// Abstract WorkflowFactory class: Abstract factory for creating workflow steps
[<AbstractClass>]
type WorkflowFactory() =
    abstract member CreateWorkflowSteps: unit -> List<IWorkflowStep>

// OrderProcessingFactory class: Creates order processing workflow steps
type OrderProcessingFactory() =
    inherit WorkflowFactory()
    override this.CreateWorkflowSteps() : List<IWorkflowStep> =
        List<IWorkflowStep>([
            ValidateOrder() :> IWorkflowStep
            ProcessPayment() :> IWorkflowStep
            ShipOrder() :> IWorkflowStep 
        ]) // Use the List constructor to create a List<IWorkflowStep>

// InvoiceProcessingFactory class: Creates invoice processing workflow steps
type InvoiceProcessingFactory() =
    inherit WorkflowFactory()
    override this.CreateWorkflowSteps() : List<IWorkflowStep> =
        List<IWorkflowStep>([
            GenerateInvoice() :> IWorkflowStep
            SendInvoice() :> IWorkflowStep 
        ]) // Use the List constructor to create a List<IWorkflowStep>

// CustomWorkflowFactory class: Creates custom workflow steps
type CustomWorkflowFactory() =
    inherit WorkflowFactory()
    override this.CreateWorkflowSteps() : List<IWorkflowStep> =
        List<IWorkflowStep>([
            PrepareSpecialOrder() :> IWorkflowStep
            NotifyCustomer() :> IWorkflowStep 
        ]) // Use the List constructor to create a List<IWorkflowStep>

// WorkflowProcessor class: Executes the workflow using the provided factory
type WorkflowProcessor(factory: WorkflowFactory) =
    member this.ExecuteWorkflow() =
        let steps = factory.CreateWorkflowSteps()
        for step in steps do
            try
                step.ExecuteStep() // Execute each workflow step
            with ex ->
                Console.WriteLine($"Error executing step: {ex.Message}") // Handle any exceptions

// Entry point of the application
[<EntryPoint>]
let main argv =
    // Execute Order Processing Workflow
    let orderProcessor = WorkflowProcessor(OrderProcessingFactory())
    Console.WriteLine("Executing Order Processing Workflow:")
    orderProcessor.ExecuteWorkflow()

    // Execute Invoice Processing Workflow
    let invoiceProcessor = WorkflowProcessor(InvoiceProcessingFactory())
    Console.WriteLine("\nExecuting Invoice Processing Workflow:")
    invoiceProcessor.ExecuteWorkflow()

    // Execute Custom Workflow
    let customProcessor = WorkflowProcessor(CustomWorkflowFactory())
    Console.WriteLine("\nExecuting Custom Workflow:")
    customProcessor.ExecuteWorkflow()

    0 // Return an integer exit code
