using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class StorageService : ScriptableObject {
    /// <summary>
    /// Save string data.
    /// </summary>
    /// <param name="key">data key.</param>
    /// <param name="data">data</param>
    /// <returns>Was save successful?</returns>
    public abstract UniTask<bool> SaveAsync(string key, string data);

    /// <summary>
    /// Does storage contain data for key?
    /// </summary>
    /// <param name="key"></param>
    public abstract UniTask<bool> HasValue(string key);

    /// <summary>
    /// Load string data.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Data: loaded data if exists. Error: error string if error occurs</returns>
    public abstract UniTask<(string data, string error)> LoadAsync(string key);

    /// <summary>
    /// Delete data.
    /// </summary>
    /// <param name="key">data key.</param>
    public abstract UniTask DeleteAsync(string key);
}