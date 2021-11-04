using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.ContinuousIntegration;
using NUnit.Framework;

namespace Example.Services.Tests
{
    [UseReporter(typeof(NCrunchReporter))]
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _userService;

        [SetUp]
        public void SetUp()
        {
            _userService = new UserService();
        }

        [Test]
        public void GetUsers_returns_expected_users()
        {
            var users = _userService.GetUsers();

            Approvals.Verify(Stringify.Items(users));
        }
    }
}
