using UnityEngine;
using UnityEngine.UI;

public class ItemCatButtonManager : MonoBehaviour
{
    public void changeButtonColor()
    {
        gameObject.transform.GetComponent<Image>().color = Color.blue; 
    }
}
