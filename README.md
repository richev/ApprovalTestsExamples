# Approval Tests Examples
Some example usages of [Approval Tests](https://github.com/approvals/ApprovalTests.Net). Assert on all the things!

Currently [using](https://github.com/richev/ApprovalTestsExamples/blob/master/ApprovalTests.Tests/Properties/AssemblyInfo.cs#L38)
Approval Tests' [`WinMergeReporter`](http://blog.approvaltests.com/2011/12/using-reporters-in-approval-tests.html)
to launch WinMerge to show the results of any failing tests. This gives a super productive way (on a developer's machine) to understand what
has broken a test.

String representations of objects being tested are created using a [`Stringify`](https://github.com/richev/ApprovalTestsExamples/blob/master/ApprovalTests.Tests/Stringify.cs)
class that recursively loops over their properties
([example](https://github.com/richev/ApprovalTestsExamples/blob/master/ApprovalTests.Tests/UserServiceTests.GetUsers_returns_expected_users.approved.txt)).

## Tips
By default, *approved* files exist in the same folder as their corresponding test. If added to the project,
things can get a bit untidy. Visual Studio supports "nesting" of items. Use this
[File Nesting](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.FileNesting) add-in to make
this nice and easy.