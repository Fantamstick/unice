using System;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "persistent storage service", menuName = "App/Services/Encrypted PersistentStorage")]
public class EncryptedPersistentStorageService : StorageService {
    [SerializeField] string encryptionPassword = "";
    
    /// <summary>
    /// Save string data.
    /// </summary>
    /// <param name="key">data key.</param>
    /// <param name="data">data</param>
    /// <returns>Is save successful?</returns>
    public override UniTask<bool> SaveAsync(string key, string data) {
        string saveError = null;

        try {
            string encryptedData = Cryptography.Encrypt(data, encryptionPassword);
            byte[] byteData = Encoding.UTF8.GetBytes(encryptedData);
            
            // write the new content
            string path = GetFullPath(key);
            File.WriteAllBytes(path, byteData);
#if DEV_TEST_FILEACCESS_ERROR
            if (UnityEngine.Random.Range(0, 10) < 2) {
                // 20% chance of triggering failed save
                throw new Exception("Failed data load exception");
            }
#endif
            return UniTask.FromResult(true);
        } catch (Exception e) {
            saveError = e.Message;
        }

        Debug.LogError($"Error saving key: {key}. {saveError}");
        
        return UniTask.FromResult(false);
    }
    
    /// <summary>
    /// Does storage contain data for key?
    /// </summary>
    /// <param name="key"></param>
    public override UniTask<bool> HasValue(string key) {
        string path = GetFullPath(key);

        return UniTask.FromResult(File.Exists(path));
    }
    
    /// <summary>
    /// Load string data.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Data: loaded data if exists. Error: error string if error occurs</returns>
    public override UniTask<(string data, string error)> LoadAsync(string key) {
        string error = string.Empty;
 
        try {
            string path = GetFullPath(key);
            
            if (!File.Exists(path)) {
                return UniTask.FromResult((string.Empty, error));
            }
            
            // load in the save data as byte array
            byte[] jsonDataAsBytes = File.ReadAllBytes(path);
            string encryptedData = Encoding.UTF8.GetString(jsonDataAsBytes);

            // convert to string.
            string result = Cryptography.Decrypt(encryptedData, encryptionPassword);
            
            return UniTask.FromResult((result, error));
        } catch (Exception e) {
            error = e.Message;
        }

        Debug.LogError($"Error loading key: {key}. {error}");

        return UniTask.FromResult((string.Empty, error));
    }
    
    /// <summary>
    /// Delete data.
    /// </summary>
    /// <param name="key">data key.</param>
    public override UniTask DeleteAsync(string key) {
        string deleteError = "";

        try {
            string fullPath = GetFullPath(key);
            
            if (File.Exists(fullPath)) {
                File.Delete(fullPath);
            }
        } catch (Exception e) {
            deleteError = e.Message;
        }

        if (!string.IsNullOrEmpty(deleteError)) {
            Debug.LogError($"Error deleting key: {key}. {deleteError}");
        }
        
        return UniTask.CompletedTask;
    }
    
    /// <summary>
    /// Create file path for where a file is stored on the specific platform given a folder name and file name
    /// </summary>
    /// <param name="FolderName"></param>
    /// <param name="FileName"></param>
    /// <returns></returns>
    static string GetFullPath(string FileName) {
        string directoryName = Path.Combine(Application.persistentDataPath, "data");
        
        // create the file in the path if it doesn't exist
        if (!Directory.Exists(directoryName)) {
            Directory.CreateDirectory(directoryName);
        }

        return Path.Combine(directoryName, FileName + ".txt");
    }
}