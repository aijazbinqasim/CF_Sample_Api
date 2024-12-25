var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API",
        Version = "v1",
        Description = "Showing how you can build minimal api with .net"
    });
});

builder.Services.AddDbContext<ApplicationContext>(config => config.UseSqlServer(CipherHelper.Decrypt(builder.Configuration.GetConnectionString("DbConnection")!)));
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddTransient<ApiKeyValidationConfig>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.UseApiKeyMiddleware();
app.MapGroup("/api/v1/")
    .WithTags("Author endpoints")
    .MapEndPoint();

app.Run();
