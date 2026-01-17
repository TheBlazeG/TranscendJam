using UnityEngine;

[CreateAssetMenu(fileName ="NewItem",menuName = "Items/Item")]
public class Items :ScriptableObject
{
    //scriptable object con valores y datos necesarios para los items
   public ItemCategories category = ItemCategories.none;
    public int monetaryValue;
    public int emotionalValue;
     public Sprite itemSprite;
}


