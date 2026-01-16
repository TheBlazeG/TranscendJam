
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableItems : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float sensitivity;
    [SerializeField] GameObject storageArea;
    [SerializeField] LayerMask areasLayer;
    public List<Items> itemsToDecide;
    Collider2D results;
    private void Start()
    {
        //obtener componente de spriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDrag()
    {
        //cambia el color del sprite mas grisáceo para dar feedback y mueve el objeto según el mouse delta mientras hace el mouse invisible
        spriteRenderer.color = Color.lightGray;
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"),0)*Time.deltaTime*sensitivity;
        Cursor.visible = false;
    }

    private void OnMouseUp()
    {
        //revisa si soltaste el objeto en alguna zona de guardado, sea venta o guardar
        results = Physics2D.OverlapCircle(transform.position, 2, areasLayer);
        if (results!=null)
        {
            storageArea = results.gameObject;
        }


        //regresa el color del objeto al color original, y teletransporta el mouse al objeto antes de hacerlo visible
        spriteRenderer.color= Color.white;
        var v = Camera.main.WorldToScreenPoint(transform.position);
        Mouse.current.WarpCursorPosition(v);
        Cursor.visible = true;
        if(storageArea!=null)
        {
            if (storageArea.CompareTag("Keep"))
            {
                //código para llamar la funcion de keep
                Debug.Log("kept");
                ScoreManager.instance.KeepItem();
            }
            else if (storageArea.CompareTag("Sell"))
            {
                //código para llamar la función de sell
                Debug.Log("sold");
                ScoreManager.instance.SellItem();
            }
            storageArea = null;
            transform.position = Vector2.zero;

        }
    }

    public void UpdateSprite(int index)
    {
        spriteRenderer.sprite= itemsToDecide[index].itemSprite;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Keep")||other.CompareTag("Sell"))
    //    {
    //        storageArea = other.gameObject;
    //    }
    //}
}
