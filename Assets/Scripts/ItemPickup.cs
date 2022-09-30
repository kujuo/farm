//using System.Collections.Generic;
//using UnityEngine;

//public class ItemPickup : MonoBehaviour
//{
//    GameObject pickedItem;

//    HashSet<string> tags = new HashSet<string>();
//    private PlayerController playerController;
//    public Request bk;

//    void Start()
//    {
//        tags.Add("Apple");
//        tags.Add("Pear");
//        tags.Add("Strawberry");
//        tags.Add("Grape");
//        playerController = GetComponent<PlayerController>();
//    }

//    private void Update()
//    {
//        if(Input.GetKey(KeyCode.Space) && pickedItem != null)
//        {
//            pickedItem.transform.SetParent(null);
//            if (playerController.playerDir.x > 0)
//            {
//                pickedItem.transform.position = transform.position + Vector3.right * 1.5f;
//            }
//            else if (playerController.playerDir.x < 0)
//            {
//                pickedItem.transform.position = transform.position + Vector3.left * 1.5f;
//            }
//            if (playerController.playerDir.y > 0)
//            {
//                pickedItem.transform.position = transform.position + Vector3.up * 1.5f;
//            }
//            else if (playerController.playerDir.y < 0)
//            {
//                pickedItem.transform.position = transform.position + Vector3.down * 1.5f;
//            }
//            pickedItem = null;
//        }
//    }

//    void OnTriggerEnter2D(Collider2D other)
//    {

//        if (pickedItem == null && tags.Contains(other.tag) && other.transform.parent != bk.transform)
//        {
//            other.transform.SetParent(transform);
//            other.transform.position = transform.position + Vector3.right * 0.5f;
//            pickedItem = other.gameObject;

//        }

//        else if (other.CompareTag("Basket") && pickedItem != null)
//        {
//            Request basket = other.GetComponent<Request>();
//            if (basket != null)
//            {
//                if (basket.request.CompareTag(pickedItem.tag))
//                {
//                    basket.count += 1;
//                    Destroy(pickedItem);
//                    Destroy(basket.request);
//                    //basket.request = null;
//                }

//            }
//        }
//    }
//}
