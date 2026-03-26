using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RecipeSo", menuName = "Scriptable Objects/RecipeSo")]
public class RecipeSO : ScriptableObject
{

    public List<KitchenObjectsSO> kitchenObjectSOList;
    public string recipeName;

}
