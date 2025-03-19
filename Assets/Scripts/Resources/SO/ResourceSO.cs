using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/ResourceSO")]
public class ResourceSO : ScriptableObject
{
    // Start is called before the first frame update
    public ResourceType resName;
    public int stackSize;
    public Sprite icon;
}

[System.Serializable]
public enum ResourceType { none, wood, water, dirt, seed, clay}
