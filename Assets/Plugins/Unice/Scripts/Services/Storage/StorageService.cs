using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class StorageService : ScriptableObject {
    /// <summary>
    /// Save string data.
    /// </summary>
    /// <param name="key">data key.</param>
    /// <param name="data">data</param>
    /// <returns>Did overwrite value</returns>
    public abstract UniTask<bool> SaveAsync(string key, string data);

    /// <summary>
    /// Load string data.
    /// </summary>
    public abstract UniTask<string> LoadAsync(string key);

    /// <summary>
    /// Delete data.
    /// </summary>
    /// <param name="key">data key.</param>
    public abstract UniTask DeleteAsync(string key);
}