using System;
using UniRx.Async;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unice.Models
{
    /// <summary>
    /// Resource Reference filepath.
    /// </summary>
    /// <typeparam name="T">Type of file that is referenced.</typeparam>
    [Serializable]
    public struct ResourceReference<T> where T : Object
    {
        public string ResourcePath;

        /// <summary>
        /// Load file in Resources folder.
        /// </summary>
        /// <returns>Loaded file</returns>
        public T Resolve()
        {
            return Resources.Load<T>(ResourcePath);
        }

        /// <summary>
        /// Load file in Resources folder asynchronously.
        /// </summary>
        /// <returns>Loaded file</returns>
        public async UniTask<T> ResolveAsync()
        {
            return await Resources.LoadAsync<T>(ResourcePath) as T;
        }
        
        bool IsFileExists(string resourcePath, ref string errorMessage)
        {
            Object file = Resources.Load(resourcePath);

            if (file == null)
            {
                errorMessage = "File does not exist in Resources path!";
                return false;
            }
            
            if (!(file is T))
            {
                errorMessage = "File exists, but not the specified file type!";
                return false;
            }

            return true;
        }
    }
}