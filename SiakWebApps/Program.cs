using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using SiakWebApps.DataAccess;
using SiakWebApps.Services;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models; // Diperlukan untuk OpenApiInfo

// --- HASH GENERATOR ---
// 1. Ubah "password123" di bawah ini ke kata sandi yang Anda inginkan.
// 2. Jalankan aplikasi. Hash akan tercetak di konsol.
// 3. Salin hash tersebut dan update database Anda.
// 4. Hapus kode ini setelah selesai.
// --- END OF HASH GENERATOR ---

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Tambahkan Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "SiakApps API", 
        Version = "v1",
        Description = "API documentation for SiakApps education management system"
    });

    // Konfigurasi Bearer token untuk Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo("/app/data-protection-keys"));

// AddingCookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
        
        // Allow HTTP in development environment
        if (builder.Environment.IsDevelopment())
        {
            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        }
    });

// Configure session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Register data access and services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(sp => new UserRepository(connectionString));
builder.Services.AddSingleton(sp => new StudentRepository(connectionString));
builder.Services.AddSingleton(sp => new TeacherRepository(connectionString));
builder.Services.AddSingleton(sp => new SubjectRepository(connectionString));
builder.Services.AddSingleton(sp => new ClassRepository(connectionString));
builder.Services.AddSingleton(sp => new AcademicYearRepository(connectionString));
builder.Services.AddSingleton(sp => new SemesterRepository(connectionString));
builder.Services.AddSingleton(sp => new ExamRepository(connectionString));
builder.Services.AddSingleton(sp => new StudentGradeRepository(connectionString));
builder.Services.AddSingleton(sp => new MasterBudgetCategoryRepository(connectionString));
builder.Services.AddSingleton(sp => new MasterGenderRepository(connectionString));
builder.Services.AddSingleton(sp => new MasterProvinceRepository(connectionString));
builder.Services.AddSingleton(sp => new MasterCityRepository(connectionString));
builder.Services.AddSingleton(sp => new MasterDistrictRepository(connectionString));
builder.Services.AddSingleton(sp => new MasterSubdistrictRepository(connectionString));
builder.Services.AddSingleton(sp => new StudentClassRepository(connectionString));
builder.Services.AddSingleton(sp => new ParentRepository(connectionString));
builder.Services.AddSingleton(sp => new StudentRegistrationRepository(connectionString));
builder.Services.AddSingleton(sp => new ExpenseRepository(connectionString));
builder.Services.AddSingleton(sp => new RoleRepository(connectionString));
builder.Services.AddSingleton(sp => new RoomRepository(connectionString));
builder.Services.AddSingleton(sp => new StudentScholarshipRepository(connectionString));
builder.Services.AddSingleton(sp => new StudentParentRepository(connectionString));
builder.Services.AddSingleton(sp => new SchoolYearRepository(connectionString));
builder.Services.AddSingleton(sp => new PaymentTransactionRepository(connectionString));
builder.Services.AddSingleton(sp => new UserRoleRepository(connectionString));
builder.Services.AddSingleton(sp => new MenuAppRepository(connectionString));
builder.Services.AddSingleton(sp => new RoleMenuRepository(connectionString));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<TeacherService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<AcademicYearService>();
builder.Services.AddScoped<SemesterService>();
builder.Services.AddScoped<ExamService>();
builder.Services.AddScoped<StudentGradeService>();
builder.Services.AddScoped<MasterBudgetCategoryService>();
builder.Services.AddScoped<MasterGenderService>();
builder.Services.AddScoped<MasterProvinceService>();
builder.Services.AddScoped<MasterCityService>();
builder.Services.AddScoped<MasterDistrictService>();
builder.Services.AddScoped<MasterSubdistrictService>();
builder.Services.AddScoped<StudentClassService>();
builder.Services.AddScoped<ParentService>();
builder.Services.AddScoped<StudentRegistrationService>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<StudentScholarshipService>();
builder.Services.AddScoped<StudentParentService>();
builder.Services.AddScoped<SchoolYearService>();
builder.Services.AddScoped<PaymentTransactionService>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<MenuAppService>();
builder.Services.AddScoped<RoleMenuService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Create initial user
using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
    await userService.CreateInitialUserAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Konfigurasi Swagger hanya di environment Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SiakApps API V1");
        c.RoutePrefix = "api-docs"; // Akses via /api-docs
    });
}

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Health check endpoint for Docker
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();