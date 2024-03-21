using OSK.Serialization.Binary.Sharp.UnitTests.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSK.Serialization.Binary.Sharp.UnitTests
{
    public class BinarySerializerTests : SerializerTests
    {
        #region Constructors

        public BinarySerializerTests()
            : base(new BinarySharpSerializer())
        {
        }

        #endregion

        #region SerializerTests Overrides

        protected override async Task<DeserializationTestParameters> GetDeserializationTestParametersAsync()
        {
            var expectedValue = new TestMessage()
            {
                Name = "HelloWorld",
                Data = new List<TestData>()
                {
                    new TestData()
                    {
                        Index = 1,
                        Data = new TestClass()
                        {
                            A = 123,
                            B = "ABC",
                            C = new TestClass() {
                                A = 456,
                                B = "DEF",
                                C = null
                            }
                        }
                    }
                }
            };

            return new DeserializationTestParameters()
            {
                ExpectedResult = expectedValue,
                Data = await Serializer.SerializeAsync(expectedValue)
            };
        }

        #endregion
    }
}
