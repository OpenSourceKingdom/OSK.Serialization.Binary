using OSK.Serialization.Abstractions.Binary;
using OSK.Serialization.Binary.Sharp.Internal;
using Polenter.Serialization;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OSK.Serialization.Binary.Sharp
{
    public class BinarySharpSerializer : IBinarySerializer
    {
        #region Variables

        public static BinarySharpSerializationOptions DefaultOptions { get; private set; } = CreateDefaultOptions();

        private readonly SharpSerializer _serializer;

        #endregion

        #region Constructors

        public BinarySharpSerializer()
            : this(DefaultOptions) { }

        public BinarySharpSerializer(BinarySharpSerializationOptions options)
            : this(BinaryUtilsHelper.CreateSerializer(options))
        {
        }

        public BinarySharpSerializer(SharpSerializer serializer)
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        #endregion

        #region IBinarySerializer

        public ValueTask<object> DeserializeAsync(byte[] data, Type type, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream(data);
            var obj = _serializer.Deserialize(memoryStream);
            return new ValueTask<object>(obj);
        }

        public ValueTask<byte[]> SerializeAsync(object data, CancellationToken cancellationToken = default)
        {
            using var memoryStream = new MemoryStream();
            _serializer.Serialize(data, memoryStream);

            return new ValueTask<byte[]>(memoryStream.ToArray());
        }

        #endregion

        #region Helpers 

        private static BinarySharpSerializationOptions CreateDefaultOptions()
        {
            return new BinarySharpSerializationOptions()
            {
                Encoding = Encoding.UTF8
            };
        }

        #endregion
    }
}
