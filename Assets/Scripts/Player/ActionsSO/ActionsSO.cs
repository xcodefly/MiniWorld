using UnityEngine;

[System.Serializable]
public enum ActionType {none, ItemPick,ItemDrop,ItemHold, Cut, Water, Dig, PickAxe,Drop}

[System.Serializable]
public enum ActionButton {none, A, B, X, Y, LS, RS}

[CreateAssetMenu(menuName = "My Assets/ActionsSO")]
public class ActionsSO : ScriptableObject
{
    // Start is called before the first frame update
    public ActionType actionType;
    public Sprite sprite;
    public ActionButton actionButton;
}





