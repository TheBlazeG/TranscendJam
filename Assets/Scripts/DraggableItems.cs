
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggableItems : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float sensitivity;
    [SerializeField] GameObject storageArea;
    [SerializeField] LayerMask areasLayer;
    Collider2D results;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDrag()
    {
        spriteRenderer.color = Color.black;
        transform.position += new Vector3(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"),0)*Time.deltaTime*sensitivity;
        Cursor.visible = false;
    }

    private void OnMouseUp()
    {
        results = Physics2D.OverlapCircle(transform.position, 2, areasLayer);
        if (results!=null)
        {
            storageArea = results.gameObject;
        }

        spriteRenderer.color= Color.white;
        var v = Camera.main.WorldToScreenPoint(transform.position);
        Mouse.current.WarpCursorPosition(v);
        Cursor.visible = true;
        if(storageArea!=null)
        {
            if (storageArea.CompareTag("Keep"))
            {
                //codigo de preservar objeto
            }
            else if (storageArea.CompareTag("Sell"))
            {
                //codigo de vender objeto
            }
            gameObject.SetActive(false);
        }
    }



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Keep")||other.CompareTag("Sell"))
    //    {
    //        storageArea = other.gameObject;
    //    }
    //}
}
