using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Scene
{
    public class Loader : MonoBehaviour
    {
        private static UnityScene targetScene;
        private static bool isNetworked;
        
        public static void Load(UnityScene targetScene, bool isNetworked = false)
        {
            Loader.targetScene = targetScene;
            Loader.isNetworked = isNetworked;
            SceneManager.LoadScene(UnityScene.Loading.ToString());
        }
        
        public static void LoaderCallback()
        {
            if (isNetworked)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(UnityScene.Game.ToString(), LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene(targetScene.ToString());
            }
        }
    }
}