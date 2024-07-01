using ModeloApi.Api.Middlewares;
using ModeloApi.Infra.IoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddInfraestructureJwt(builder.Configuration);
builder.Services.AddInfraestructureSwagger();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
});

builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin"));
    opt.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("UserOnly", policy => policy.RequireRole("User", "Admin", "SuperAdmin"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
