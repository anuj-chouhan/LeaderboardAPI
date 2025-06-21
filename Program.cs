using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerUI; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Swagger + XML comments
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Include XML documentation comments (for Swagger UI summaries/examples)
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    options.IncludeXmlComments(xmlPath);
});

// Configure Swagger UI options (e.g., allowed methods)
builder.Services.Configure<SwaggerUIOptions>(options =>
{
    options.SupportedSubmitMethods(new[]
    {
        SubmitMethod.Get,
        SubmitMethod.Post,
        SubmitMethod.Put,
        SubmitMethod.Patch,
        SubmitMethod.Delete
    });
});


var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Launches UI at /swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
