var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));
builder.Services.AddAutoMapper(typeof(ApiSampleMarker));
builder.Services.AddControllers()
.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<CustomerModel>();
    options.ImplicitlyValidateRootCollectionElements = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();
    dbContext.Customers.AddRange(GetCustomers());
    await dbContext.SaveChangesAsync();
}

app.Run();



static List<Customer> GetCustomers()
{
    var customer1 = new Customer("Firstname1", "LastName1");
    var customer2 = new Customer("Firstname2", "LastName2");
    var customer3 = new Customer("Firstname3", "LastName3");

    customer1.AddAddress(new("Street1"));
    customer1.AddAddress(new("Street2"));
    customer1.AddAddress(new("Street3"));

    return new()
    {
        customer1,
        customer2,
        customer3
    };
}
