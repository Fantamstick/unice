using UnityEngine;

namespace Unice.ViewHelpers.TestRunner
{
    /// <summary>
    /// Initializes testable components.
    /// </summary>
    public class TestRunner : MonoBehaviour
    {
        public GameObject[] TestableGameObjects = null;
        
        void Start()
        {
            foreach (GameObject go in TestableGameObjects)
            {
                foreach (MonoBehaviour component in go.GetComponents<MonoBehaviour>())
                {
                    if (component is ITestable testableC)
                    {
                        Debug.Log($"Initiating {go.name}");
                        testableC.IsTestRunning = true;
                        testableC.RunTest();
                    }
                }
            }
        }
    }
}
