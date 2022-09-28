using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject healthCanvas;

    public void HealthbarCreation()
    {
        var canvas = Instantiate(healthCanvas, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        canvas.transform.SetParent(gameObject.transform, true);
    }

    public abstract void Attack();

    public abstract void OnHit(float damage);

    public abstract void ItemDrop();

    //public abstract float touchDamage(); 

}
