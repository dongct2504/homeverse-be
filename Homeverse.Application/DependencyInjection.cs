﻿using Homeverse.Application.Mappings;
using Homeverse.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Homeverse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));

        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IContactService, ContactService>();
        services.AddScoped<IEnumService, EnumService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
}
