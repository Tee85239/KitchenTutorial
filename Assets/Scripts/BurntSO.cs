using UnityEngine;

[CreateAssetMenu(fileName = "BurntSO", menuName = "Scriptable Objects/BurntSO")]
public class BurntSO : ScriptableObject
{
    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float burnTime;
    
}
