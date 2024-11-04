// Step Interface
class IWorkflowStep {
    // Executes a specific step in the workflow.
    executeStep() {
        throw new Error("Method 'executeStep()' must be implemented.");
    }
}

// Concrete Steps for Order Processing
class ValidateOrder extends IWorkflowStep {
    // Validates the order in the workflow.
    executeStep() {
        console.log("Validating Order...");
    }
}

class ProcessPayment extends IWorkflowStep {
    // Processes payment for the order.
    executeStep() {
        console.log("Processing Payment...");
    }
}

class ShipOrder extends IWorkflowStep {
    // Ships the order to the customer.
    executeStep() {
        console.log("Shipping Order...");
    }
}

// Concrete Steps for Invoice Processing
class GenerateInvoice extends IWorkflowStep {
    // Generates an invoice for the order.
    executeStep() {
        console.log("Generating Invoice...");
    }
}

class SendInvoice extends IWorkflowStep {
    // Sends the invoice to the customer.
    executeStep() {
        console.log("Sending Invoice to Customer...");
    }
}

// Custom Steps for Custom Workflow
class PrepareSpecialOrder extends IWorkflowStep {
    // Prepares a special order according to specific requirements.
    executeStep() {
        console.log("Preparing Special Order...");
    }
}

class NotifyCustomer extends IWorkflowStep {
    // Notifies the customer about the order status.
    executeStep() {
        console.log("Notifying Customer...");
    }
}

// Abstract Factory Class for creating workflow steps
class WorkflowFactory {
    // Creates and returns an array of workflow steps.
    createWorkflowSteps() {
        throw new Error("Method 'createWorkflowSteps()' must be implemented.");
    }
}

// Concrete Factory for Order Processing Workflow
class OrderProcessingFactory extends WorkflowFactory {
    // Creates the steps necessary for processing an order.
    createWorkflowSteps() {
        return [
            new ValidateOrder(),
            new ProcessPayment(),
            new ShipOrder()
        ];
    }
}

// Concrete Factory for Invoice Processing Workflow
class InvoiceProcessingFactory extends WorkflowFactory {
    // Creates the steps necessary for processing an invoice.
    createWorkflowSteps() {
        return [
            new GenerateInvoice(),
            new SendInvoice()
        ];
    }
}

// Concrete Factory for Custom Workflow
class CustomWorkflowFactory extends WorkflowFactory {
    // Creates custom steps for specific workflows.
    createWorkflowSteps() {
        return [
            new PrepareSpecialOrder(),
            new NotifyCustomer()
        ];
    }
}

// Workflow Processor for executing workflows
class WorkflowProcessor {
    constructor(factory) {
        // Initializes the WorkflowProcessor with a specific factory.
        this.workflowFactory = factory;
    }

    // Executes the steps defined in the workflow factory.
    executeWorkflow() {
        const steps = this.workflowFactory.createWorkflowSteps(); // Get the steps from the factory
        for (const step of steps) {
            try {
                step.executeStep(); // Execute each step
            } catch (error) {
                console.error(`Error executing step: ${error.message}`); // Handle any exceptions
            }
        }
    }
}

// Client code for executing workflows
function main() {
    // Execute Order Processing Workflow
    const orderProcessor = new WorkflowProcessor(new OrderProcessingFactory());
    console.log("Executing Order Processing Workflow:");
    orderProcessor.executeWorkflow();

    // Execute Invoice Processing Workflow
    const invoiceProcessor = new WorkflowProcessor(new InvoiceProcessingFactory());
    console.log("\nExecuting Invoice Processing Workflow:");
    invoiceProcessor.executeWorkflow();

    // Execute Custom Workflow
    const customProcessor = new WorkflowProcessor(new CustomWorkflowFactory());
    console.log("\nExecuting Custom Workflow:");
    customProcessor.executeWorkflow();
}

// Run the main function
main();
