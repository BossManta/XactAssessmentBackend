var policyName = "_myAllowSpecificOrigins"; //Cors

var builder = WebApplication.CreateBuilder(args);

//Adds service to do controller endpoint routing.
builder.Services.AddControllers();

//configure Swagger(A tool to test and show API endpoints)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Business Logic Injection
builder.Services.AddSingleton<IDebtorLogic, DebtorLogic>();
builder.Services.AddSingleton<IStockLogic, StockLogic>();
builder.Services.AddSingleton<IInvoiceLogic, InvoiceLogic>();


//Configure Cors
builder.Services.AddCors(options => {
    options.AddPolicy(policyName, builder => {
        builder.WithOrigins("*")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

//Allow for Swagger to be used in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
