using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductApi.Configration;
using ProductApi.Model;
using ProductApi.Servies.SCategores;
using ProductApi.Servies.SProduct;
using ProductApi.Servies.SUser;
using System.Text;

// إنشاء Builder المسؤول عن إعداد المشروع وقراءة الإعدادات من appsettings.json
var builder = WebApplication.CreateBuilder(args);

// ========================== FluentValidation ==========================

// تفعيل التحقق التلقائي من صحة البيانات القادمة من المستخدم.
// عند إرسال DTO إلى Controller سيتم تشغيل Validators تلقائياً.
builder.Services.AddFluentValidationAutoValidation();

// البحث عن جميع Validators الموجودة في المشروع وإضافتها إلى DI Container.
// FluentValidations هو أي Validator موجود داخل المشروع.
builder.Services.AddValidatorsFromAssemblyContaining<FluentValidations>();

// ========================== Swagger ==========================

// إضافة Swagger لاختبار الـ API من المتصفح.
builder.Services.AddSwaggerGen(s =>
{
// تعريف نوع المصادقة الذي سيستخدمه Swagger.
// هنا نستخدم JWT Bearer Token.
s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
// اسم Header الذي سيتم إرسال التوكن من خلاله.
Name = "Authorization",

```
    // نوع المصادقة HTTP Authentication.
    Type = SecuritySchemeType.Http,

    // نوع المخطط المستخدم.
    Scheme = "Bearer",

    // شكل التوكن JWT.
    BearerFormat = "JWT",

    // مكان إرسال التوكن داخل Header.
    In = ParameterLocation.Header,

    // شرح للمستخدم داخل Swagger.
    Description = "Enter JWT like: Bearer {token}"
});

// إجبار Swagger على إرسال التوكن مع جميع الطلبات المحمية.
s.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                // الرجوع إلى Security Definition السابق.
                Type = ReferenceType.SecurityScheme,

                // اسم الـ Security Definition.
                Id = "Bearer"
            }
        },
        new string[] {}
    }
});
```

});

// ========================== Database ==========================

// تسجيل DbContext داخل Dependency Injection.
// UseSqlServer يعني استخدام SQL Server كقاعدة بيانات.
builder.Services.AddDbContext<AppDbContexests>(
option => option.UseSqlServer(
builder.Configuration.GetConnectionString("FC")));

// FC هو اسم Connection String داخل appsettings.json

// ========================== Controllers ==========================

// تفعيل Controllers داخل المشروع.
builder.Services.AddControllers();

// ========================== Dependency Injection ==========================

// عند طلب IRUser سيتم إنشاء RUser تلقائياً.
builder.Services.AddScoped<IRUser, RUser>();

// Repository الخاص بالأقسام.
builder.Services.AddScoped<IRCategores, RCategores>();

// Repository الخاص بالمنتجات.
builder.Services.AddScoped<IRProduct, RProduct>();

// Service الخاص بالمنتجات.
builder.Services.AddScoped<ISProduct, SProduct>();

// Service الخاص بالأقسام.
builder.Services.AddScoped<ISCategores, SCategores>();

// Service الخاص بالمستخدمين.
builder.Services.AddScoped<ISUser, SUsers>();

// ========================== AutoMapper ==========================

// تسجيل AutoMapper لتحويل DTO ↔ Entity.
builder.Services.AddAutoMapper(typeof(Program));

// ========================== JWT Settings ==========================

// قراءة قسم JWT من appsettings.json.
var settingJwt = builder.Configuration.GetSection("JWT");

// تحويل المفتاح السري إلى Byte Array لاستخدامه في تشفير التوكن.
var Key = Encoding.UTF8.GetBytes(settingJwt["Key"]!);

// ربط بيانات JWT الموجودة في appsettings مع كلاس JWT.
builder.Services.Configure<JWT>(settingJwt);

// ========================== Authentication ==========================

// تفعيل JWT Authentication.
builder.Services.AddAuthentication(options =>
{
// النظام الافتراضي للمصادقة.
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

```
// النظام المستخدم عند وجود [Authorize].
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
```

})
.AddJwtBearer(options =>
{
// إعدادات التحقق من التوكن.
options.TokenValidationParameters = new TokenValidationParameters
{
// التحقق من أن Issuer صحيح.
ValidateIssuer = true,

```
    // التحقق من أن Audience صحيح.
    ValidateAudience = true,

    // التحقق من صلاحية التوكن وعدم انتهاء مدته.
    ValidateLifetime = true,

    // التحقق من صحة التوقيع.
    ValidateIssuerSigningKey = true,

    // المفتاح المستخدم للتحقق من صحة التوقيع.
    IssuerSigningKey = new SymmetricSecurityKey(Key),

    // الجهة المصدرة للتوكن.
    ValidIssuer = settingJwt["Issuer"],

    // الجهة المستقبلة للتوكن.
    ValidAudience = settingJwt["Audience"],

    // منع السماح الافتراضي بعد انتهاء التوكن.
    ClockSkew = TimeSpan.Zero
};
```

});

// ========================== Build ==========================

// إنشاء التطبيق بعد الانتهاء من تسجيل الخدمات.
var app = builder.Build();

// ========================== Middleware Pipeline ==========================

// تشغيل Swagger فقط أثناء التطوير.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();

```
// واجهة Swagger UI.
app.UseSwaggerUI();
```

}

// تحويل جميع الطلبات إلى HTTPS.
app.UseHttpsRedirection();

// قراءة JWT Token والتحقق منه.
app.UseAuthentication();

// تطبيق [Authorize] والتحقق من الصلاحيات.
app.UseAuthorization();

// ربط Controllers مع Routes.
app.MapControllers();

// تشغيل التطبيق.
app.Run();
