using UnityEngine;

[CreateAssetMenu(fileName ="NewItem",menuName = "Items/Item")]
public class Items :ScriptableObject
{
   [SerializeField]ItemCategories category = ItemCategories.none;
   [SerializeField] int monetaryValue;
   [SerializeField] int emotionalValue;
    [SerializeField] Sprite itemSprite;
}


