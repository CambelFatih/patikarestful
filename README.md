# ProductManagerAPI Project

## Overview
`ProductManagerAPI` is a RESTful API developed to facilitate CRUD operations pertaining to products. The API efficiently handles tasks such as listing, accessing, adding, updating, and deleting products.

## Features

### 1. CRUD Operations
- **List All Products**: Fetches a comprehensive list of all products.
- **Get a Specific Product**: Retrieves detailed information about a product identified by its ID.
- **Create a Product**: Incorporates a new product entry into the system.
- **Update a Product**: Modifies the details of an existing product.
- **Delete a Product**: Removes a product from the system based on its ID.

### 2. Product Filtering
Provides users with the functionality to display products based on specific filtering criteria, primarily the product name.

### 3. Error Handling
`ProductManagerAPI` ensures robust error feedback through the integration of the `ErrorHandlingMiddleware`. This middleware adeptly intercepts unhandled exceptions during request processing. Depending on the exception type, the system decides upon an apt HTTP status code, be it 400 Bad Request or 500 Internal Server Error, and subsequently furnishes the client with a systematically structured JSON response detailing the status code and a pertinent error message.

### 4. Swagger Integration
The API supports integration with Swagger, paving the way for streamlined API documentation and testing.

## Technical Details

## Dependencies

This project is built with `.NET 7.0` and utilizes the following NuGet packages:

- **Microsoft.AspNetCore.JsonPatch**: This library provides support for JSON Patch, a format for describing changes to a JSON document. We're using version `7.0.11`.
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson**: A compatibility pack to use `Newtonsoft.Json` as the JSON serializer for ASP.NET Core. Current version is `7.0.*`, indicating it uses the latest patch version of the `7.0` series.
- **Microsoft.AspNetCore.OpenApi**: Enables OpenAPI features for the ASP.NET Core application, using version `7.0.9`.
- **Newtonsoft.Json**: A popular high-performance JSON framework for .NET, used at version `13.0.3`.
- **Swashbuckle.AspNetCore**: Integrates Swagger tools including the UI into an ASP.NET Core application. The version in use is `6.5.0`.

Make sure to keep these packages updated for the latest features and bug fixes.


### Middleware

#### ErrorHandlingMiddleware
Central to `ProductManagerAPI's` resilience is its `ErrorHandlingMiddleware`, a custom middleware dedicated to capturing and effectively processing unhandled exceptions. The workflow entails:
1. **Exception Catching**: Captures any unhandled exceptions arising in the middleware pipeline.
2. **Determining HTTP Status Code**: Matches the exception type to a corresponding HTTP status code. For instance, an `ArgumentException` would yield a 400 Bad Request.
3. **Response Structuring**: Utilizes the `ApiResponse` model to mold the error response, ensuring standardized feedback that incorporates the HTTP status code and an elucidative error message.
4. **Error Response Dispatch**: The crafted error details undergo JSON serialization before being dispatched to the client.

### Routes
- `GET /api/products`: Displays all products.
- `GET /api/products/{id}`: Extracts information about a product using its ID.
- `POST /api/products`: Introduces a new product.
- `PUT /api/products/{id}`: Updates details of an already listed product.
- `DELETE /api/products/{id}`: Erases a specific product based on its ID.
- `GET /api/products/filter?name={name}`: Enables product filtration based on name.

