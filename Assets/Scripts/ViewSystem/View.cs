using UnityEngine;

namespace ViewSystem
{
    public class View : MonoBehaviour
    {
        public bool IsInitialized { get; private set; }

        public virtual void Initialize()
        {
            IsInitialized = true;
        }
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show(object args = null)
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        public virtual void PassArgs(object args = null)
        {

        }
    }

}
