using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Item.itemType headerType;
    private CanvasRenderer canvasRenderer;

    private void Awake()
    {
        canvasRenderer = this.GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManagerNew.Instance.filterType == headerType)
        {
            canvasRenderer.SetAlpha(0.5f);
        }
        else
        {
            canvasRenderer.SetAlpha(1f);
        }
    }
}