#######################
restaurantsdailymenus API
support:
authentication
 Bearer token
version
is connected to mongodb


https://localhost:7220/swagger/v1/swagger.json
https://localhost:7220/swagger
########################
with swagger.json the obtained from link from https://localhost:7220/swagger/v1/swagger.json we can generate client code in various programming languages using tools like Swagger Codegen or OpenAPI Generator.
This client code can then be used to interact with the restaurantsdailymenus API programmatically.
I execute the following command to generate a C# client using OpenAPI Generator:
nswag openapi2csclient  /input:"swagger.json" /output:"ApiClient.cs"  /namespace:RestaurantsDailyMenus.Api   /GenerateClientInterfaces:true

This will generate from the api information:
AuthClient
RestaurantsClient
DailyMenusClient
All DTOs (Restaurant, DailyMenu, LoginDto, etc.)