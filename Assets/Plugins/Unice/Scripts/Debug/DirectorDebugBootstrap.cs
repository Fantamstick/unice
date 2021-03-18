using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Debug bootstrap for Directors.
/// </summary>
public abstract class DirectorDebugBootstrap : MonoBehaviour {
    void Start() {
        if (SceneService.IsDebugMode) {
            // run parent bootstrap script.
            OnStart();
        } else {
            // scene is not in debug mode (a real game is being played).
            Destroy(gameObject);
        }
    }
    
    protected abstract UniTaskVoid OnStart();
}