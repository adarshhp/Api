using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YourProjectName.Data;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.


builder.Services.AddControllers();



//i edited here ADARSH

builder.Services.AddDbContext<LoginDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//until here


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Addding cors here
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Frontend origin
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
//until here


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}








//added this shit
app.UseCors("AllowSpecificOrigin");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
