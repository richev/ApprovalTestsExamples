using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.ContinuousIntegration;
using NUnit.Framework;

namespace Example.Services.Tests
{
    [UseReporter(typeof(NCrunchReporter))]
    [TestFixture]
    public class ObjectTests
    {
        private class Base
        {
            /// <summary>
            /// Should not be in Stringified string.
            /// </summary>
            public static string StaticProperty { get; set; }

            public string BaseClassProperty { get; set; }
        }

        private class Inherited: Base
        {
            public string InheritedClassProperty { get; set; }
        }

        [Test]
        public void ObjectTest()
        {
            var name = new Base
            {
                BaseClassProperty = "Foo"
            };

            Approvals.Verify(Stringify.Item(name));
        }

        [Test]
        public void InheritedObjectTest()
        {
            var inherited = new Inherited
            {
                BaseClassProperty = "Foo",
                InheritedClassProperty = "Bar"
            };

            Approvals.Verify(Stringify.Item(inherited));
        }
    }
}
