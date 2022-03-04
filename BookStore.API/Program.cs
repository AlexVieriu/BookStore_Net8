var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BookStore");
builder.Services.AddDbContext<BookStoreContext>(o => o.UseSqlServer(connString));

builder.Services.AddIdentityCore<ApiUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((hostBuilderContext, loggerConfig) =>
                loggerConfig.WriteTo.Console().ReadFrom.Configuration(hostBuilderContext.Configuration));

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", b => b.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader());
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
