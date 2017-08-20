using ApprovalTests;
using ApprovalTests.Reporters;
using Example.Services.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Example.Services.Tests
{
    [UseReporter(typeof(NCrunchReporter))]
    [TestFixture]
    public class BasicTests
    {
        [Test]
        public void ObjectTest()
        {
            var name = new NameModel { First = "Jack", Last = "Black" };

            Approvals.Verify(Stringify.Item(name));
        }

        [Test]
        public void ListTest()
        {
            var list = new [] { 1, 2, 4, 10, 42 };

            Approvals.Verify(Stringify.Items(list));
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
