using System.Collections.Generic;

namespace OSK.Serialization.Binary.Sharp.UnitTests.Helpers
{
    public class TestMessage
    {
        public string Name { get; set; }

        public IEnumerable<TestData> Data { get; set; }
    }
}
