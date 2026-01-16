using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI onScreenDebt;

    int debt = 372000;
    int emotionalSanity = 4;
    ItemCategories playerLike1;
    ItemCategories playerLike2;
    

    private void Awake()
    {
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
        UpdateDebt();

        int like1=Random.Range(0, 6);
        int like2=Random.Range(0, 6);
        while (like2 == like1)
        {
            like2=Random.Range(0, 6);
        }
        playerLike1 = (ItemCategories)like1;
        playerLike2 = (ItemCategories)like2;
    }
    void UpdateDebt()
    {
        onScreenDebt.text = debt.ToString();
    }

    

}


public enum ItemCategories
{
    Music=0,
    Food=1,
    Drinks=2,
    Alcohol=3,
    Style=4,
    Art=5,
    none=6
}
