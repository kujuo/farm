using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCatButtonManager : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.transform.GetComponent<Image>().color = Color.blue; 
    }
}
