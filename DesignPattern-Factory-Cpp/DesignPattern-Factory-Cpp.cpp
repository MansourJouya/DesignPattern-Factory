#include <iostream>
#include <memory>
#include <vector>

// Step Interface
class IWorkflowStep {
public:
    // Executes a specific step in the workflow.
    virtual void ExecuteStep() = 0;
    // Default destructor for proper cleanup of derived classes.
    virtual ~IWorkflowStep() = default;
};

// Concrete Steps for Order Processing
class ValidateOrder : public IWorkflowStep {
public:
    // Validates the order in the workflow.
    void ExecuteStep() override {
        std::cout << "Validating Order..." << std::endl;
    }
};

class ProcessPayment : public IWorkflowStep {
public:
    // Processes payment for the order.
    void ExecuteStep() override {
        std::cout << "Processing Payment..." << std::endl;
    }
};

class ShipOrder : public IWorkflowStep {
public:
    // Ships the order to the customer.
    void ExecuteStep() override {
        std::cout << "Shipping Order..." << std::endl;
    }
};

// Concrete Steps for Invoice Processing
class GenerateInvoice : public IWorkflowStep {
public:
    // Generates an invoice for the order.
    void ExecuteStep() override {
        std::cout << "Generating Invoice..." << std::endl;
    }
};

class SendInvoice : public IWorkflowStep {
public:
    // Sends the invoice to the customer.
    void ExecuteStep() override {
        std::cout << "Sending Invoice to Customer..." << std::endl;
    }
};

// Custom Steps for Custom Workflow
class PrepareSpecialOrder : public IWorkflowStep {
public:
    // Prepares a special order according to specific requirements.
    void ExecuteStep() override {
        std::cout << "Preparing Special Order..." << std::endl;
    }
};

class NotifyCustomer : public IWorkflowStep {
public:
    // Notifies the customer about the order status.
    void ExecuteStep() override {
        std::cout << "Notifying Customer..." << std::endl;
    }
};

// Abstract Factory Class for creating workflow steps
class WorkflowFactory {
public:
    // Creates and returns a list of workflow steps.
    virtual std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() = 0;
    // Default destructor for proper cleanup of derived classes.
    virtual ~WorkflowFactory() = default;
};

// Concrete Factory for Order Processing Workflow
class OrderProcessingFactory : public WorkflowFactory {
public:
    // Creates the steps necessary for processing an order.
    std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() override {
        return {
            std::make_shared<ValidateOrder>(),
            std::make_shared<ProcessPayment>(),
            std::make_shared<ShipOrder>()
        };
    }
};

// Concrete Factory for Invoice Processing Workflow
class InvoiceProcessingFactory : public WorkflowFactory {
public:
    // Creates the steps necessary for processing an invoice.
    std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() override {
        return {
            std::make_shared<GenerateInvoice>(),
            std::make_shared<SendInvoice>()
        };
    }
};

// Concrete Factory for Custom Workflow
class CustomWorkflowFactory : public WorkflowFactory {
public:
    // Creates custom steps for specific workflows.
    std::vector<std::shared_ptr<IWorkflowStep>> CreateWorkflowSteps() override {
        return {
            std::make_shared<PrepareSpecialOrder>(),
            std::make_shared<NotifyCustomer>()
        };
    }
};

// Workflow Processor for executing workflows
class WorkflowProcessor {
private:
    std::shared_ptr<WorkflowFactory> workflowFactory; // Holds the factory to create workflow steps

public:
    // Initializes the WorkflowProcessor with a specific factory.
    WorkflowProcessor(std::shared_ptr<WorkflowFactory> factory) : workflowFactory(std::move(factory)) {}

    // Executes the steps defined in the workflow factory.
    void ExecuteWorkflow() {
        auto steps = workflowFactory->CreateWorkflowSteps(); // Get the steps from the factory
        for (const auto& step : steps) {
            try {
                step->ExecuteStep(); // Execute each step
            }
            catch (const std::exception& ex) {
                std::cout << "Error executing step: " << ex.what() << std::endl; // Handle any exceptions
            }
        }
    }
};

// Client code for executing workflows
int main() {
    // Execute Order Processing Workflow
    WorkflowProcessor orderProcessor(std::make_shared<OrderProcessingFactory>());
    std::cout << "Executing Order Processing Workflow:" << std::endl;
    orderProcessor.ExecuteWorkflow();

    // Execute Invoice Processing Workflow
    WorkflowProcessor invoiceProcessor(std::make_shared<InvoiceProcessingFactory>());
    std::cout << "\nExecuting Invoice Processing Workflow:" << std::endl;
    invoiceProcessor.ExecuteWorkflow();

    // Execute Custom Workflow
    WorkflowProcessor customProcessor(std::make_shared<CustomWorkflowFactory>());
    std::cout << "\nExecuting Custom Workflow:" << std::endl;
    customProcessor.ExecuteWorkflow();

    return 0; // Return success code
}
