# CMPG-323-Project-2---37579193-

# Project Overview
- The EcoPower Logistics CRUD RESTful API project, created in Visual Studio 2022 using C#, 
  showcases an effective API solution for managing logistics data. 
  This system is intended to simplify the administration of the supply, storage, and transportation of solar energy system components. 
  The project, which is based on REST's guiding principles, incorporates Swagger for interactive API description. 
  For essential CRUD activities (Create, Read, Update, Delete) on resources connected to logistics, the API provides a set of endpoints.
  
# Attributes
- Real-time tracking: Through the use of the API, distinct tracking IDs are created for each order, 
  allowing consumers to get updates in real-time on the progress and location of their shipments as well as other 
  information like customer data and the items they have purchased.

- The Swagger dynamic tool, which creates interactive API documentation automatically, and the API are smoothly integrated. 
  This functionality facilitates effective testing and makes stakeholder contact with the endpoints simpler.
  
- Integration of an Azure database: improves data storage, retrieval, and overall performance by utilizing Azure for database hosting. 
  For effective data administration, the API interfaces with the Azure-hosted database without any issues.

# How to use

# Essential 
- Ensure you have Visual Studio 2022 installed with a compatible .NET environment (Version6.0).
- Have an active Azure subscription for hosting the database.

# Installation
- Clone the repository.
- Open the folder and copy the controllers,models, project.cs and obj to your visual studios file
- Build the solution to restore NuGet packages and compile the project.

# Configure 
- Include Azure database connection strings when configuring the database connection information in the appsettings.json file.
- Set up authentication and permission in accordance with your company's needs. Such as your username and password

# Purpose
- Use Visual Studio to launch the application 
- An easy-to-use interface is provided by this interactive documentation so that users may explore and test the API endpoints.
- Use the different endpoints to carry out the required CRUD actions on the logistical data.

# Documentation

# For Products Controller:
- Endpoint: 'GET /api/products'
- Description: Retrieve a list of all products available in the inventory.

# Get Product by ID
- Endpoint: GET /api/products/{id}
- Description: Retrieve details of a specific product based on its ID.

# Get Products for Order
- Endpoint: GET /api/products/Order/{orderId}
- Description: Retrieve a list of products associated with a specific order.

# Create Product
- Endpoint: POST /api/products
- Description: Create a new product entry in the inventory.

# Update Product
- Endpoint: PUT /api/products/{id}
- Description: Update the details of an existing product.

# Delete Product
- Endpoint: DELETE /api/products/{id}
- Description: Delete a product entry from the inventory.


#For Orders Controller

# Get All Orders
- Endpoint: GET /api/orders
- Description: Retrieve a list of all orders.

# Get Order by ID
- Endpoint: GET /api/orders/{id}
- Description: Retrieve details of a specific order based on its ID.

# Get Orders by Customer
- Endpoint: GET /api/orders/ByCustomer/{customerId}
- Description: Retrieve a list of orders associated with a specific customer.

# Create Order
- Endpoint: POST /api/orders
- Description: Place a new order for solar energy system components.

# Update Order
- Endpoint: PUT /api/orders/{id}
- Description: Update the details of an existing order.

# Delete Order
- Endpoint: DELETE /api/orders/{id}
- Description: Delete an order from the system.


# Customers Controller

# Get All Customers
- Endpoint: GET /api/customers
- Description: Retrieve a list of all customers.

# Get Customer by ID
- Endpoint: GET /api/customers/{id}
- Description: Retrieve details of a specific customer based on their ID.

# Create Customer
- Endpoint: POST /api/customers
- Description: Create a new customer entry in the database.

# Update Customer
- Endpoint: PUT /api/customers/{id}
- Description: Update the details of an existing customer.

# Delete Customer
- Endpoint: DELETE /api/customers/{id}
- Description: Delete a customer from the database.