using Microsoft.AspNetCore.Authentication.Cookies;
using SiakWebApps.DataAccess;
using SiakWebApps.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// 添加Cookie认证
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
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

// 创建初始用户
using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
    await userService.CreateInitialUserAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 添加认证中间件
app.UseAuthorization();

// Health check endpoint for Docker
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();