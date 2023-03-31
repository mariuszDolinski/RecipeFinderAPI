# RecipeFinderAPI
My first REST API created in ASP.Net 6.0 
This API is used to store cooking recipes and all needed ingridients and manage them.
Each recipe has list of recipeIngridients store in separate table (1 to many relation).
Each recipeIngridient has name, description and foreign keys for ingridient and unit name store in separate table (1 to many relation)
Endpoint /api/recipe/searchResult provides a search method (on POST action) which return all recipes wich have all given ingridients names (in body request).
API has authentication and authorization which allows only authenticated users use API endpoints.
Authentication and authorization mechanisms are implemented using AspNetCore.Identity and JWT tokens.
It has OpenAPI specification, which was made using Swagger. 
You need to run API locally (for example via Visual Studio), then Swagger UI should open in browser.

<h1>Technology and package used:</h1>
<ul>
<li>Microsoft Visual Studio 2022</li>
<li>ASP.NET Core Web Api</li>
<li>.Net 6.0</li>
<li>Entity Framework 7.0.3</li>
<li>FluentValidation 11.2.2</li>
<li>AspNetCore.Authentication.JwtBearer 6.0.15</li>
<li>AspNetCore.Identity 2.2.0</li>
</ul>


