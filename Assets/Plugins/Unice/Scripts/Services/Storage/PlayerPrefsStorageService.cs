using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "player prefs storage service", menuName = "App/Services/PlayerPrefsStorage")]
public class PlayerPrefsStorageService : StorageService {
    /// <summary>
    /// Save string data.
    /// </summary>
    /// <param name="key">data key.</param>
    /// <param name="data">data</param>
    /// <returns>Did overwrite value</returns>
    public override async UniTask<bool> SaveAsync(string key, string data) {
        bool hasKey = PlayerPrefs.HasKey(key);
        PlayerPrefs.SetString(key, data);

        return await UniTask.FromResult(hasKey);
    }

    /// <summary>
    /// Load string data.
    /// </summary>
    /// <param name="key">storage key</param>
    /// <returns>Value from key. Empty string if value does not exist.</returns>
    public override async UniTask<string> LoadAsync(string key) {
        string data = PlayerPrefs.GetString(key);
        return await UniTask.FromResult(data);
    }

    /// <summary>
    /// Delete data.
    /// </summary>
    /// <param name="key">data key.</param>
    public override UniTask DeleteAsync(string key) {
        PlayerPrefs.DeleteKey(key);

        return UniTask.CompletedTask;
    }
}