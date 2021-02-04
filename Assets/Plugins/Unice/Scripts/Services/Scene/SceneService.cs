using System;
using Unice.Util;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneService {
    /// <summary>
    /// Debug mode:
    /// Set to false when NOT running test scenes.
    /// </summary>
    public static bool IsDebugMode { get; set; } = true;

    /// <summary>
    /// Load scene asynchronously. 
    /// </summary>
    /// <param name="sceneName">Name of scene to load.</param>
    /// <param name="canActivate">Callback to know when to activate scene when given a load percentage.</param>
    /// <param name="cleanupAction">Cleanup previous scene</param>
    public static async UniTask LoadAsync(string sceneName, Func<float, bool> canActivate = null, Func<UniTask> cleanupAction = null) {
        var prevSceneName = SceneManager.GetActiveScene().name;
        if (prevSceneName == sceneName) {
            Debug.Log($"{sceneName} already loaded.");
            return;
        }
        
        if (canActivate == null) {
            // load scene as is by default.
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        } else {
            var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loadOperation.allowSceneActivation = false;
        
            while (loadOperation.progress < 0.85f || !canActivate(loadOperation.progress)) {
                // wait until both scene is mostly loaded and can activate.
                await Delay.NextFrame();
            }
        
            loadOperation.allowSceneActivation = true;

            while (!loadOperation.isDone) {
                // wait until scene is entirely loaded.
                await Delay.NextFrame();
            } 
        }

        if (cleanupAction != null) {
            await UniTask.Create(cleanupAction);
        }
       
        await SceneManager.UnloadSceneAsync(prevSceneName);
    }
}