# RecipeFinderAPI
My first REST API created in ASP.Net 6.0 
<ul>
<li>This API is used to store cooking recipes and all needed ingridients and manage them.</li>
<li>Each recipe has list of recipeIngridients store in separate table (1 to many relation).</li>
<li>Each recipeIngridient has name, description and foreign keys for ingridient and unit name store in separate table (1 to many relation).</li>
<li>Endpoint /api/recipe/searchResult?mode=number return a search result (on POST action) based on number given as query parameter
<ul>
<li>number = 0 (default) - return all recipes that have all ingridients from given list (recipe may have some other ingridients)</li>
<li>number = 1 - return all recipes that have ingridients only from given list (not necessary all of them)</li>
</ul>
</li>
<li>API has authentication and authorization which allows to register and log in users and use API endpoints only by authenticated users.</li>
<li>Authentication and authorization mechanisms are implemented using AspNetCore.Identity and JWT tokens.</li>
<li>It has OpenAPI specification, which was made using Swagger. </li>
<li>You need to run API locally (for example via Visual Studio), then Swagger UI should open in browser. API used local database named (localdb)\localRecipeFinderDB. Fell free to change connection string if needed. It is hardcoded in Entities/RecipesDBContext.cs</li>
</ul>

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


