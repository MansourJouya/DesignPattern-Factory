Imports System
Imports System.Collections.Generic

' IWorkflowStep Interface: Defines the contract for each step in the workflow
Public Interface IWorkflowStep
    Sub ExecuteStep()
End Interface

' ValidateOrder Class: Validates an order as part of the workflow
Public Class ValidateOrder
    Implements IWorkflowStep

    ' Executes the validation step of the workflow
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Validating Order...")
    End Sub
End Class

' ProcessPayment Class: Processes payment in the workflow
Public Class ProcessPayment
    Implements IWorkflowStep

    ' Executes the payment processing step
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Processing Payment...")
    End Sub
End Class

' ShipOrder Class: Ships the order as part of the workflow
Public Class ShipOrder
    Implements IWorkflowStep

    ' Executes the shipping step of the workflow
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Shipping Order...")
    End Sub
End Class

' GenerateInvoice Class: Generates an invoice in the workflow
Public Class GenerateInvoice
    Implements IWorkflowStep

    ' Executes the invoice generation step
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Generating Invoice...")
    End Sub
End Class

' SendInvoice Class: Sends the invoice to the customer
Public Class SendInvoice
    Implements IWorkflowStep

    ' Executes the step for sending the invoice
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Sending Invoice to Customer...")
    End Sub
End Class

' PrepareSpecialOrder Class: Prepares a special order as part of the custom workflow
Public Class PrepareSpecialOrder
    Implements IWorkflowStep

    ' Executes the step for preparing a special order
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Preparing Special Order...")
    End Sub
End Class

' NotifyCustomer Class: Notifies the customer as part of the custom workflow
Public Class NotifyCustomer
    Implements IWorkflowStep

    ' Executes the step for notifying customers
    Public Sub ExecuteStep() Implements IWorkflowStep.ExecuteStep
        Console.WriteLine("Notifying Customer...")
    End Sub
End Class

' WorkflowFactory Class: Abstract factory for creating workflow steps
Public MustInherit Class WorkflowFactory
    ' Creates a list of workflow steps
    Public MustOverride Function CreateWorkflowSteps() As List(Of IWorkflowStep)
End Class

' OrderProcessingFactory Class: Creates order processing workflow steps
Public Class OrderProcessingFactory
    Inherits WorkflowFactory

    ' Creates the steps for the order processing workflow
    Public Overrides Function CreateWorkflowSteps() As List(Of IWorkflowStep)
        Return New List(Of IWorkflowStep) From {
            New ValidateOrder(),
            New ProcessPayment(),
            New ShipOrder()
        }
    End Function
End Class

' InvoiceProcessingFactory Class: Creates invoice processing workflow steps
Public Class InvoiceProcessingFactory
    Inherits WorkflowFactory

    ' Creates the steps for the invoice processing workflow
    Public Overrides Function CreateWorkflowSteps() As List(Of IWorkflowStep)
        Return New List(Of IWorkflowStep) From {
            New GenerateInvoice(),
            New SendInvoice()
        }
    End Function
End Class

' CustomWorkflowFactory Class: Creates custom workflow steps
Public Class CustomWorkflowFactory
    Inherits WorkflowFactory

    ' Creates the steps for the custom workflow
    Public Overrides Function CreateWorkflowSteps() As List(Of IWorkflowStep)
        Return New List(Of IWorkflowStep) From {
            New PrepareSpecialOrder(),
            New NotifyCustomer()
        }
    End Function
End Class

' WorkflowProcessor Class: Executes the workflow using the provided factory
Public Class WorkflowProcessor
    Private ReadOnly workflowFactory As WorkflowFactory

    ' Initializes the WorkflowProcessor with a specific factory
    Public Sub New(factory As WorkflowFactory)
        workflowFactory = factory
    End Sub

    ' Executes the workflow steps created by the factory
    Public Sub ExecuteWorkflow()
        ' Get the steps from the factory
        Dim steps As List(Of IWorkflowStep) = workflowFactory.CreateWorkflowSteps()

        ' Loop through each workflow step
        For Each workflowStepObj As Object In steps
            Dim workflowStep As IWorkflowStep = CType(workflowStepObj, IWorkflowStep) ' Cast to IWorkflowStep
            Try
                ' Execute each workflow step
                workflowStep.ExecuteStep()
            Catch ex As Exception
                ' Handle any exceptions
                Console.WriteLine($"Error executing step: {ex.Message}")
            End Try
        Next
    End Sub
End Class

' Program Class: Entry point for the application
Public Class Program
    ' Main method: The entry point of the application
    Public Shared Sub Main()
        ' Execute Order Processing Workflow
        Dim orderProcessor As New WorkflowProcessor(New OrderProcessingFactory())
        Console.WriteLine("Executing Order Processing Workflow:")
        orderProcessor.ExecuteWorkflow()

        ' Execute Invoice Processing Workflow
        Dim invoiceProcessor As New WorkflowProcessor(New InvoiceProcessingFactory())
        Console.WriteLine(vbLf & "Executing Invoice Processing Workflow:")
        invoiceProcessor.ExecuteWorkflow()

        ' Execute Custom Workflow
        Dim customProcessor As New WorkflowProcessor(New CustomWorkflowFactory())
        Console.WriteLine(vbLf & "Executing Custom Workflow:")
        customProcessor.ExecuteWorkflow()
    End Sub
End Class
