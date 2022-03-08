var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BookStore");
builder.Services.AddDbContext<BookStoreContext>(o => o.UseSqlServer(connString));

builder.Services.AddIdentityCore<ApiUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();

builder.Services.AddAutoMapper(typeof(MapConfig));

builder.Services.AddScoped<IAuthorRepository,AuthorRepository>();   
builder.Services.AddScoped<IBookRepository,BookRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    #region Implementing "Authorize"
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id  = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    #endregion
});

builder.Host.UseSerilog((hostBuilderContext, loggerConfig) =>
                loggerConfig.WriteTo.Console().ReadFrom.Configuration(hostBuilderContext.Configuration));

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", b => b.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader());
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],

            ValidateLifetime = true,

            ValidateIssuerSigningKey =true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
