using UnityEngine;
using UnityEngine.UI;

namespace ViewSystem
{
    public class MainMenuView : View
    {
        [SerializeField] private Button clientButton;
        [SerializeField] private Button hostButton;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            clientButton.onClick.AddListener(() =>
            {
                ConnectionManager.Connect(ConnectionType.Client);
            });
            
            hostButton.onClick.AddListener(() =>
            {
                ConnectionManager.Connect(ConnectionType.Host);
            });
        }
    }

}
