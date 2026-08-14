using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Services;
using Microsoft.EntityFrameworkCore;
using WebApi.Exceptions;

var builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReservationDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "ReservationDb" ) ) );

builder.Services.AddScoped<IPropertyRepository, EFPropertyRepository>();
builder.Services.AddScoped<IRoomTypeRepository, EFRoomTypeRepository>();
builder.Services.AddScoped<IReservationRepository, EFReservationRepository>();

builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();