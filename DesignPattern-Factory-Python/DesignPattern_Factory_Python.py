from abc import ABC, abstractmethod
from typing import List

# Step Interface
class IWorkflowStep(ABC):
    """
    Interface for workflow steps that defines the contract for executing steps.
    """
    @abstractmethod
    def execute_step(self):
        """
        Execute the workflow step.
        """
        pass

# Concrete Steps for Order Processing
class ValidateOrder(IWorkflowStep):
    """
    Concrete step for validating orders.
    """
    def execute_step(self):
        """
        Executes the validation step of the order processing workflow.
        """
        print("Validating Order...")

class ProcessPayment(IWorkflowStep):
    """
    Concrete step for processing payments.
    """
    def execute_step(self):
        """
        Executes the payment processing step of the order processing workflow.
        """
        print("Processing Payment...")

class ShipOrder(IWorkflowStep):
    """
    Concrete step for shipping orders.
    """
    def execute_step(self):
        """
        Executes the shipping step of the order processing workflow.
        """
        print("Shipping Order...")

# Concrete Steps for Invoice Processing
class GenerateInvoice(IWorkflowStep):
    """
    Concrete step for generating invoices.
    """
    def execute_step(self):
        """
        Executes the invoice generation step of the invoice processing workflow.
        """
        print("Generating Invoice...")

class SendInvoice(IWorkflowStep):
    """
    Concrete step for sending invoices.
    """
    def execute_step(self):
        """
        Executes the step for sending invoices to customers.
        """
        print("Sending Invoice to Customer...")

# Custom Steps for Custom Workflow
class PrepareSpecialOrder(IWorkflowStep):
    """
    Custom step for preparing a special order.
    """
    def execute_step(self):
        """
        Executes the special order preparation step.
        """
        print("Preparing Special Order...")

class NotifyCustomer(IWorkflowStep):
    """
    Custom step for notifying customers.
    """
    def execute_step(self):
        """
        Executes the step for notifying customers about their orders.
        """
        print("Notifying Customer...")

# Abstract Factory Class
class WorkflowFactory(ABC):
    """
    Abstract factory class for creating workflow steps.
    """
    @abstractmethod
    def create_workflow_steps(self) -> List[IWorkflowStep]:
        """
        Creates a list of workflow steps.
        
        Returns:
            List[IWorkflowStep]: A list of instances implementing IWorkflowStep.
        """
        pass

# Concrete Factory for Order Processing Workflow
class OrderProcessingFactory(WorkflowFactory):
    """
    Concrete factory for creating order processing workflow steps.
    """
    def create_workflow_steps(self) -> List[IWorkflowStep]:
        """
        Creates the steps for the order processing workflow.
        
        Returns:
            List[IWorkflowStep]: A list of order processing steps.
        """
        return [
            ValidateOrder(),
            ProcessPayment(),
            ShipOrder()
        ]

# Concrete Factory for Invoice Processing Workflow
class InvoiceProcessingFactory(WorkflowFactory):
    """
    Concrete factory for creating invoice processing workflow steps.
    """
    def create_workflow_steps(self) -> List[IWorkflowStep]:
        """
        Creates the steps for the invoice processing workflow.
        
        Returns:
            List[IWorkflowStep]: A list of invoice processing steps.
        """
        return [
            GenerateInvoice(),
            SendInvoice()
        ]

# Concrete Factory for Custom Workflow
class CustomWorkflowFactory(WorkflowFactory):
    """
    Concrete factory for creating custom workflow steps.
    """
    def create_workflow_steps(self) -> List[IWorkflowStep]:
        """
        Creates the steps for the custom workflow.
        
        Returns:
            List[IWorkflowStep]: A list of custom workflow steps.
        """
        return [
            PrepareSpecialOrder(),
            NotifyCustomer()
        ]

# Workflow Processor
class WorkflowProcessor:
    """
    Class responsible for executing workflows using a specific workflow factory.
    """
    def __init__(self, workflow_factory: WorkflowFactory):
        """
        Initializes a new instance of the WorkflowProcessor class.

        Args:
            workflow_factory (WorkflowFactory): The factory that creates workflow steps.
        """
        self._workflow_factory = workflow_factory

    def execute_workflow(self):
        """
        Executes the workflow steps created by the factory.
        """
        steps = self._workflow_factory.create_workflow_steps()
        for step in steps:
            try:
                step.execute_step()
            except Exception as ex:
                print(f"Error executing step: {ex}")

# Client code
if __name__ == "__main__":
    # Execute Order Processing Workflow
    order_processor = WorkflowProcessor(OrderProcessingFactory())
    print("Executing Order Processing Workflow:")
    order_processor.execute_workflow()

    # Execute Invoice Processing Workflow
    invoice_processor = WorkflowProcessor(InvoiceProcessingFactory())
    print("\nExecuting Invoice Processing Workflow:")
    invoice_processor.execute_workflow()

    # Execute Custom Workflow
    custom_processor = WorkflowProcessor(CustomWorkflowFactory())
    print("\nExecuting Custom Workflow:")
    custom_processor.execute_workflow()
