using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OSK.Serialization.Abstractions.Binary;
using OSK.Serialization.Binary.Sharp.Internal;
using System;
using System.Text;

namespace OSK.Serialization.Binary.Sharp
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBinarySharpSerialization(this IServiceCollection services)
            => services.AddBinarySharpSerialization(o =>
            {
                o.Encoding = Encoding.UTF8;
            });

        public static IServiceCollection AddBinarySharpSerialization(this IServiceCollection services,
            Action<BinarySharpSerializationOptions> options)
        {
            services.Configure(options);
            services.TryAddTransient(provider =>
            {
                var sharpSerializerOptions = provider.GetRequiredService<IOptions<BinarySharpSerializationOptions>>();
                return BinaryUtilsHelper.CreateSerializer(sharpSerializerOptions.Value);
            });
            services.TryAddTransient<IBinarySerializer, BinarySharpSerializer>();

            return services;
        }
    }
}
