using UnityEngine;

namespace Scene
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            Loader.Load(UnityScene.Menu);
        }
    }

}
