using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI onScreenDebt;
    [SerializeField] TextMeshProUGUI onScreenItemCategory;
    [SerializeField] public Image[] onScreenSanity;
    Items currentItem;
    DraggableItems items;
    int currentItemIndex=0;
    int totalValueOfItems;
    int debt = 372000;
    public int interestPerSecond = 0;
    int emotionalSanity = 4;
    ItemCategories playerLike1;
    ItemCategories playerLike2;
    

    private void Awake()
    {
        //singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            {
                Destroy(this);
            }
        }
        currentItem = items.itemsToDecide[currentItemIndex];
        items.UpdateSprite(currentItemIndex);
        UpdateItemCategory();
        UpdateDebt();
        UpdateEmotionalSanity();
        UpdateValue();

        //gustos del personaje asignados de manera aleatoria
        int like1=Random.Range(0, 7);
        int like2=Random.Range(0, 7);
        while (like2 == like1)
        {
            like2=Random.Range(0, 6);
        }
        playerLike1 = (ItemCategories)like1;
        playerLike2 = (ItemCategories)like2;
    }

    private void Update()
    {
        AddDebt();
        if (debt>totalValueOfItems)
        {
            
        }
    }

    private void UpdateItemCategory()
    {
        onScreenItemCategory.text=currentItem.category.ToString();
    }

    //función para actualizar el valor emocional visible en UI
    private void UpdateEmotionalSanity()
    {
        for (int i = 0; i < onScreenSanity.Length; i++)
        {
            if (i < emotionalSanity)
            {
            onScreenSanity[i].gameObject.SetActive(true);
            }
            else
            {
            onScreenSanity[i].gameObject.SetActive(false);
            }
        }
    }
    //función para actualizar el valor monetario disponible visible en UI
    private void UpdateValue()
    {
        totalValueOfItems-=currentItem.monetaryValue;
        Debug.Log("no has agregado el codigo para objetos");
    }
    //función para actualizar el monto de deuda visible en UI
    void UpdateDebt()
    {
        onScreenDebt.text = debt.ToString();
    }

    //función para vender item; drena valor emocional a la sanidad, resta monto disponible y resta dinero a la deuda
    public void SellItem()
    {
        int emotionalBonus;
        if (currentItem.category==playerLike1||currentItem.category == playerLike2)
        {
             emotionalBonus = 1;
        }
        else
        {
             emotionalBonus = 0;
        }
        emotionalSanity -=(currentItem.emotionalValue+emotionalBonus);
        debt-=currentItem.monetaryValue;
        UpdateDebt();
        UpdateValue();
        UpdateEmotionalSanity();
        if (items.itemsToDecide.Count>0)
        {
            items.itemsToDecide.Remove(currentItem);
            currentItemIndex = Random.Range(0,items.itemsToDecide.Count);
        currentItem=items.itemsToDecide[currentItemIndex];
            UpdateItemCategory();
            items.UpdateSprite(currentItemIndex);
        }
        else
        {
            //lose code
        }
    }

    //función para guardar item; drena monto disponible y suma sanidad
    public void KeepItem()
    {
        int emotionalBonus;
        if (currentItem.category == playerLike1 || currentItem.category == playerLike2)
        {
            emotionalBonus = 1;
        }
        else
        {
            emotionalBonus = 0;
        }
        emotionalSanity +=(currentItem.emotionalValue+emotionalBonus);
        UpdateValue();
        UpdateEmotionalSanity();
        if (items.itemsToDecide.Count > 0)
        {
            items.itemsToDecide.Remove(currentItem);
            currentItemIndex = Random.Range(0, items.itemsToDecide.Count);
            currentItem = items.itemsToDecide[currentItemIndex];
            UpdateItemCategory();
            items.UpdateSprite(currentItemIndex);
        }
        else 
        {
        //lose code
        }
    }
    
    public void AddDebt()
    {
        debt += interestPerSecond;
    }

}

//enum para categorizar los items y designar gustos del jugador
public enum ItemCategories
{
    Music=0,
    Food=1,
    Drinks=2,
    Alcohol=3,
    Style=4,
    Art=5,
    Games=6,
    none=7
}
