namespace Unice.ViewHelpers.TestRunner
{
    /// <summary>
    /// Interface which is testable by the TestRunner component.
    /// </summary>
    public interface ITestable
    {
        /// <summary>
        /// Test is currently running.
        /// </summary>
        bool IsTestRunning { set; get; }
        /// <summary>
        /// Run test method.
        /// </summary>
        void RunTest();
    }
}
