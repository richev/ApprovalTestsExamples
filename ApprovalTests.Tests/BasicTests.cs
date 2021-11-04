using ApprovalTests;
using ApprovalTests.Reporters;
using Example.Services.Models;
using NUnit.Framework;
using System.Collections.Generic;
using ApprovalTests.Reporters.ContinuousIntegration;

namespace Example.Services.Tests
{
    [UseReporter(typeof(NCrunchReporter))]
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void NullTest()
        {
            object name = null;

            Approvals.Verify(Stringify.Item(name));
        }

        [Test]
        public void EmptyArrayTest()
        {
            var array = new int[0];

            Approvals.Verify(Stringify.Items(array));
        }

        [Test]
        public void ArrayTest()
        {
            var array = new [] { 1, 2, 4, 10, 42 };

            Approvals.Verify(Stringify.Items(array));
        }

        [Test]
        public void SimpleDictionaryTest()
        {
            var dictionary = new Dictionary<string, int>
            {
                { "Foo", 11 },
                { "Bar", 42 }
            };

            Approvals.Verify(Stringify.Items(dictionary));
        }

        [Test]
        public void EmptyDictionaryTest()
        {
            var dictionary = new Dictionary<string, int>();

            Approvals.Verify(Stringify.Items(dictionary));
        }

        [Test]
        public void ComplexDictionaryTest()
        {
            var dictionary = new Dictionary<string, NameModel>
            {
                { "Foo", new NameModel { First = "Jack", Last = "Black" } },
                { "Bar", new NameModel { First = "Jill", Last = "Blue" } },
            };

            Approvals.Verify(Stringify.Items(dictionary));
        }
    }
}
