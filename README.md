# MyWebApi Project

## Overview
`MyWebApi` is a RESTful API designed to manage CRUD operations related to products. This API supports primary functions such as listing products, accessing a specific product, adding a new product, updating a product, and deleting a product.

## Features

### 1. CRUD Operations
- **List All Products**: Lists all products.
- **Get a Specific Product**: Retrieves a product with a specific ID.
- **Create a Product**: Adds a new product.
- **Update a Product**: Updates an existing product.
- **Delete a Product**: Deletes an existing product.

### 2. Product Filtering
Users can list specific products by filtering according to the product name.

### 3. Error Handling
To ensure robustness and clarity for API users, the `MyWebApi` project employs the `ErrorHandlingMiddleware` for systematic error handling. This middleware captures unhandled exceptions as they propagate through the application pipeline. Based on the nature of the exception, an appropriate HTTP status code, ranging from 400 Bad Request to 500 Internal Server Error, is determined. The client then receives a structured JSON response, comprised of this status code and a descriptive error message. This design promotes consistent, informative feedback across all endpoints, regardless of the encountered issue.

### 4. Swagger Integration
Integration with Swagger for API documentation and testing.

## Technical Details

### Dependencies
- **MyWebApi.Repositories**: Used to manage database operations related to products.
- **MyWebApi.Models**: Defines model structures that the API will use.
- **Newtonsoft.Json**: Used for JSON operations.
- **Swashbuckle.AspNetCore**: Provides Swagger integration for API documentation.

## Error Handling in MyWebApi Project

In the `MyWebApi` project, error handling is a critical aspect to ensure that our API users receive meaningful feedback when unexpected scenarios occur. By implementing custom middleware to catch these exceptions, the system guarantees a consistent and descriptive response format for any errors.

### Middleware

#### ErrorHandlingMiddleware
The `ErrorHandlingMiddleware` is a custom middleware dedicated to capturing and processing unhandled exceptions. Here's how it works:

1. **Exception Catching**: As the request travels through the middleware pipeline, any unhandled exceptions are caught by `ErrorHandlingMiddleware`.
   
2. **Determining HTTP Status Code**: The type of exception thrown determines the HTTP status code of the response. For instance, an `ArgumentException` translates to a 400 Bad Request, whereas a generic unhandled exception would result in a 500 Internal Server Error.
   
3. **Formatting the Response**: The error response is structured using the `ApiResponse` model. This ensures that the API's error responses are consistent, containing an HTTP status code and an error message extracted from the exception.
   
4. **Returning the Error Response**: The error details are serialized to JSON and sent back to the client.

This middleware ensures that our API is resilient and communicates effectively with clients, even when things go wrong.


### Routes
- `GET /api/products`: Lists all products.
- `GET /api/products/{id}`: Retrieves a product with a specific ID.
- `POST /api/products`: Adds a new product.
- `PUT /api/products/{id}`: Updates an existing product.
- `DELETE /api/products/{id}`: Deletes an existing product.
- `GET /api/products/filter?name={name}`: Filters products by name.
