using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI onScreenDebt;
    [SerializeField] TextMeshProUGUI onScreenItemCategory;
    [SerializeField] TextMeshProUGUI onScreenItemSanityCost;
    [SerializeField] TextMeshProUGUI onScreenItemValue;
    [SerializeField] public Image[] onScreenSanity;
    Items currentItem;
    [SerializeField] DraggableItems items;
    int currentItemIndex=0;
    int totalValueOfItems;
    public float debt = 37200;
    public int interestPerSecond = 0;
    int emotionalSanity = 4;
    ItemCategories playerLike1 = new ItemCategories();
    ItemCategories playerLike2 = new ItemCategories();
    [SerializeField] TextMeshProUGUI onScreenTrait1;
    [SerializeField] TextMeshProUGUI onScreenTrait2;



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
        
    }
    private void Start()
    {
        currentItem = items.itemsToDecide[currentItemIndex];
        items.UpdateSprite(currentItemIndex);
        UpdateItemCategoryCostAndValue();
        UpdateDebt();
        UpdateEmotionalSanity();
        UpdateValue();
        foreach (var item in items.itemsToDecide)
        {
            totalValueOfItems += item.monetaryValue;
        }
        //gustos del personaje asignados de manera aleatoria
        int like1 = Random.Range(0, 7);
        int like2 = Random.Range(0, 7);
        while (like2 == like1)
        {
            like2 = Random.Range(0, 6);
        }
        playerLike1 = (ItemCategories)like1;
        playerLike2 = (ItemCategories)like2;
        
        SetupPersonalityTraits();
    }

    private void SetupPersonalityTraits()
    {
        string trait1=playerLike1.ToString();
        string trait2=playerLike2.ToString();
        onScreenTrait1.text = trait1;
        onScreenTrait2.text = trait2;
    }

    private void Update()
    {
        AddDebt();
        if (debt>totalValueOfItems)
        {
            LoseGame();
        }
        if (debt<=0)
        {
            WinGame();
        }

        
    }

    private void UpdateItemCategoryCostAndValue()
    {
        onScreenItemCategory.text=currentItem.category.ToString();
        onScreenItemSanityCost.text =currentItem.emotionalValue.ToString();
        onScreenItemValue.text =currentItem.monetaryValue.ToString();
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
        
    }
    //función para actualizar el monto de deuda visible en UI
    void UpdateDebt()
    {
        onScreenDebt.text = ((int)debt).ToString();
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
        if (emotionalBonus + currentItem.emotionalValue>emotionalSanity)
            return;
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
            UpdateItemCategoryCostAndValue();
            items.UpdateSprite(currentItemIndex);
        }
        else
        {
            LoseGame();
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
        emotionalSanity= Mathf.Clamp(emotionalSanity,0,5);
        UpdateValue();
        UpdateEmotionalSanity();
        if (items.itemsToDecide.Count > 0)
        {
            items.itemsToDecide.Remove(currentItem);
            currentItemIndex = Random.Range(0, items.itemsToDecide.Count);
            currentItem = items.itemsToDecide[currentItemIndex];
            UpdateItemCategoryCostAndValue();
            items.UpdateSprite(currentItemIndex);

        }
        else 
        {
            LoseGame();
        }
    }
    
    public void AddDebt()
    {
        debt += interestPerSecond*Time.deltaTime;
        UpdateDebt();
    }

    void LoseGame()
    {
        Debug.Log("Lose");
        SceneManager.LoadScene("You lose");
    } 

    void WinGame()
    {
        Debug.Log("win");
        SceneManager.LoadScene("You win");

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
