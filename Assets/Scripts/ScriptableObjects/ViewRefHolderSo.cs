using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using ViewSystem;

[CreateAssetMenu()]
public class ViewRefHolderSo : ScriptableObject
{
    public List<View> viewList;
    public View defaultView;
}
