using UnityEngine;

[CreateAssetMenu()]
public class CookingRecipeSO : ScriptableObject
{

    public KitchenObjectsSO input;
    public KitchenObjectsSO output;
    public float cookProgressMax;
    
}
