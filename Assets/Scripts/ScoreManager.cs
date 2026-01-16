using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TextMeshProUGUI onScreenDebt;

    int debt = 372000;
    int emotionalSanity = 4;
    

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
    }
    void UpdateDebt()
    {
        onScreenDebt.text = debt.ToString();
    }
}


public enum ItemCategories
{
    Music,
    Food,
    Drinks,
    Alcohol,
    Style,
    Art,
    none
}
