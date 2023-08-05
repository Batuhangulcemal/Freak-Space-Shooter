using System.Collections.Generic;
using UnityEngine;

namespace ViewSystem
{
    [RequireComponent(typeof(ViewRefHolder))]
    public class ViewManager : MonoBehaviour
    {
        private static ViewManager instance;
        
        private View activeView;
        private ViewRefHolder RefHolder => GetViewSystemRefHolder();
        private ViewRefHolder refHolder;
        
        private List<View> ViewList => RefHolder.ViewList;
        private View DefaultView => RefHolder.DefaultView;
        private Transform ViewTransform => RefHolder.viewTransform;
        
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Initialize();
        }
        
        public void Initialize()
        {
            if(DefaultView != null)
            {
                OpenView(DefaultView);
            }
        }
        
        public static void Show<TView>(object args = null) where TView : View
        {
            foreach (View view in instance.ViewList)
            {
                if (view is TView)
                {
                    if (instance.activeView == null)
                    {
                        instance.OpenView(view, args);
                    }
                    else
                    {
                        if (instance.activeView is not TView)
                        {
                            instance.CloseView(instance.activeView);
                            instance.OpenView(view, args);
                        }
                    }
                }
            }
        }
        
        private void OpenView(View view, object args = null)
        {
            activeView = Instantiate(view, ViewTransform) as View;
            activeView.PassArgs(args);
        }
        
        private void CloseView(View view)
        {
            Destroy(view.gameObject);
        }
        
        
        
        
        private ViewRefHolder GetViewSystemRefHolder()
        {
            if(refHolder == null)
            {
                refHolder = GetComponent<ViewRefHolder>();
            }

            return refHolder;
        }

    }

}
