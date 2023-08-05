using System.Collections.Generic;
using UnityEngine;

namespace ViewSystem
{
    public class ViewRefHolder : MonoBehaviour
    {
        public List<View> ViewList => viewRefHolderSo.viewList;
        public View DefaultView => viewRefHolderSo.defaultView;
        
        [SerializeField] private ViewRefHolderSo viewRefHolderSo;
        [SerializeField] public Transform viewTransform;
    }
}