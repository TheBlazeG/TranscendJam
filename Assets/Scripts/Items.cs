using UnityEngine;

[CreateAssetMenu(fileName ="NewItem",menuName = "Items/Item")]
public class Items :ScriptableObject
{
   public ItemCategories category = ItemCategories.none;
    public int monetaryValue;
    public int emotionalValue;
     public Sprite itemSprite;
}


