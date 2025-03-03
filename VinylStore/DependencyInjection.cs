﻿using FluentValidation;
using VinylStore.Validators;
using VinylStoreBL;

namespace VinylStore
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterDataLayer();
            services.RegisterBusinessLayer();
            services.AddValidatorsFromAssemblyContaining<AddVinylRequestValidator>();
        }
    }
}
