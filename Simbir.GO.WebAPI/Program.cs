using Microsoft.AspNetCore.Mvc.Infrastructure;
using Simbir.GO.Application;
using Simbir.GO.Infrastructure;
using Simbir.GO.WebAPI;
using Simbir.GO.WebAPI.Common.Errors;
using Simbir.GO.WebAPI.Common.Mappings;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddMappings();
    builder.Services.AddWebApi();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}