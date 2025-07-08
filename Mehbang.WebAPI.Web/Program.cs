using Mehbang.WebAPI.Application.Services.Students;
using Mehbang.WebAPI.Core.Services.Contract;
using Mehbang.WebAPI.Data.Repositories.Contract;
using Mehbang.WebAPI.Data.Repositories.Implementation;
using Mehbang.WebAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region Inject Services

builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IStudentService, StudentService>();

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlite("Data Source=students.db"));

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
