using Polenter.Serialization;
namespace OSK.Serialization.Binary.Sharp.Internal
{
    internal static class BinaryUtilsHelper
    {
        public static SharpSerializer CreateSerializer(BinarySharpSerializationOptions options)
        {
            return new SharpSerializer(new SharpSerializerBinarySettings()
            {
                Encoding = options.Encoding
            });
        }
    }
}
