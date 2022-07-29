using System;
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
    public override UniTask<bool> SaveAsync(string key, string data) {
        string error = null;
        
        try {
            // may fail if device has no more space.
            PlayerPrefs.SetString(key, data);
        } catch (Exception e) {
            error = e.Message;
        }

        bool hasError = !string.IsNullOrEmpty(error);
        
        if (hasError) {
            Debug.LogError($"Error saving key: {key}. {error}");
        }

        return UniTask.FromResult(!hasError);
    }

    /// <summary>
    /// Does storage contain data for key?
    /// </summary>
    /// <param name="key"></param>
    public override UniTask<bool> HasValue(string key) {
        return UniTask.FromResult(PlayerPrefs.HasKey(key));
    }

    /// <summary>
    /// Load string data.
    /// </summary>
    /// <param name="key">storage key</param>
    /// <returns>Value from key. Empty string if value does not exist.</returns>
    public override UniTask<(string data, string error)> LoadAsync(string key) {
        string error = String.Empty;
        
        try {
            // may fail if file is not accessible.
            string data = PlayerPrefs.GetString(key);
            return UniTask.FromResult((data, error));
        } catch (Exception e) {
            error = e.Message;
        }

        Debug.LogError($"Error saving key: {key}. {error}");
        
        return UniTask.FromResult((string.Empty, error));
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