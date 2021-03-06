﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.I18nText;
using Toolbelt.Blazor.I18nText.Internals;

namespace Toolbelt.Blazor.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding I18n Text service.
    /// </summary>
    public static class I18nTextDependencyInjection
    {
        /// <summary>
        ///  Adds a I18n Text service to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection to add the service to.</param>
        public static IServiceCollection AddI18nText<TStartup>(this IServiceCollection services, Action<I18nTextOptions> configure = null) where TStartup : class
        {
            services.AddSingleton<BlazorPathInfoService>();
            services.AddScoped(serviceProvider =>
            {
                var options = new I18nTextOptions
                {
                    GetInitialLanguageAsync = I18nText.I18nText.GetInitialLanguageAsync,
                    PersistCurrentLanguageAsync = I18nText.I18nText.PersistCurrentLanguageAsync,
                };
                configure?.Invoke(options);
                return new I18nText.I18nText(typeof(TStartup), serviceProvider, options);
            });
            return services;
        }
    }
}
