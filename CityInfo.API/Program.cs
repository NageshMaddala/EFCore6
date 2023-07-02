var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Default configuration
//builder.Services.AddControllers();

// xml representation not supported
//builder.Services.AddControllers(options =>
//{
//    options.ReturnHttpNotAcceptable = true;
//});

// Support additional formats
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Generates specification
    app.UseSwagger();

    // Generates documentation
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
