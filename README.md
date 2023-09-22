# ProductManagerAPI Project

## Overview
`ProductManagerAPI` is a RESTful API developed to facilitate CRUD operations pertaining to products. The API efficiently handles tasks such as listing, accessing, adding, updating, and deleting products.

## Features
### 1. CRUD Operations
- **CRUD Operations**: Complete CRUD (Create, Read, Update, Delete) operations for products.
- **JSON Data Source**: Uses a JSON file as its data store, making it lightweight and easy to deploy.
- **Service Layer**: Implements a service layer for business logic, ensuring that the API controllers remain clean and focused on request/response handling.
- **Repository Pattern**: Utilizes the repository pattern for data access, providing flexibility to switch data sources in the future if needed.
- **Search Capability**: Provides an endpoint for searching products based on criteria like name, order, and pagination.
- **Partial Updates**: Implements the HTTP PATCH method to allow for partial updates of product records.

### 3. Error Handling
`ProductManagerAPI` ensures robust error feedback through the integration of the `ErrorHandlingMiddleware`. This middleware adeptly intercepts unhandled exceptions during request processing. Depending on the exception type, the system decides upon an apt HTTP status code, be it 400 Bad Request or 500 Internal Server Error, and subsequently furnishes the client with a systematically structured JSON response detailing the status code and a pertinent error message.

### 4. Swagger Integration
The API supports integration with Swagger, paving the way for streamlined API documentation and testing.

## Technical Details

### Dependencies

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
![md](https://github.com/CambelFatih/patikarestful/assets/79880394/84a337f1-ee5f-401c-a8c4-10a39b724adb)
- `GET /api/products`: Retrieve all products.
- `GET /api/products/{id}`: Retrieve a single product by ID.
- `POST /api/products`: Create a new product.
- `PUT /api/products/{id}`: Update an existing product.
- `DELETE /api/products/{id}`: Delete a product.
- `GET /api/products/search`: Search for products based on certain criteria.
- `PATCH /api/products/{id}`: Partially update a product.

#### Postmen requests that update only the Name field with Patch
![patch](https://github.com/CambelFatih/patikarestful/assets/79880394/5896a0dc-81a4-4c65-8a4b-bb79d529e987)
![md2](https://github.com/CambelFatih/patikarestful/assets/79880394/66cbc5ff-b649-4921-abe0-a32b6cc6d2bd)

## Endpoint

`GET /api/products/search`

## Parameters

- **name** _(optional)_: Filters the products by their name. Example: `?name=Shirt`
  
- **orderBy** _(optional, default: 'Id')_: Specifies the property to order by. Valid options include `Id`, `Name`, `Price`, etc. Example: `?orderBy=Price`
  
- **ascending** _(optional, default: true)_: If set to `true`, products will be ordered in ascending order based on the `orderBy` property. If `false`, they will be ordered in descending order. Example: `?ascending=false`
  
- **page** _(optional, default: 1)_: The page number to return. Use this for pagination. Example: `?page=2`
  
- **pageSize** _(optional, default: 10)_: The number of products to return per page. Example: `?pageSize=5`

## Usage in Swagger UI

1. Navigate to the Swagger UI for MyWebApi.
2. Under the "Products" section, find the "GET /api/products/search" endpoint.
3. Click on the endpoint to expand it.
4. You'll see input fields for each parameter. Enter the desired values:
    - For `name`, you can input a substring or full product name.
    - Use the dropdown for `orderBy` to select the desired ordering property.
    - Toggle the checkbox for `ascending` to switch between ascending and descending order.
    - Input the desired page number in `page`.
    - Input the desired number of products per page in `pageSize`.
5. Click "Execute" to run the request and view the results below.

#### Sort in reverse order by id, pagination is page 1, with 4 elements on each page.
![search1](https://github.com/CambelFatih/patikarestful/assets/79880394/94983a22-12e2-4057-9eef-6d62295abd01)
![search2](https://github.com/CambelFatih/patikarestful/assets/79880394/5bea18ea-22f1-4684-a3aa-e43954ad9b5a)
