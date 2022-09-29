using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEditor.VersionControl;
using UnityEngine;

public class Request : MonoBehaviour
{
    public GameObject request;
    public int count;
    private bool full;
    public GameObject[] requests;
    // Start is called before the first frame update
    void Start()
    {
        if (!request)
        {
            GameObject newReq = requests[Random.Range(0, requests.Length)];
                //Random.Range(0, requests.Length)];
            Instantiate(newReq);
            GameObject.Find(newReq.name + "(Clone)").transform.parent = transform;
            newReq.transform.position = transform.position + new Vector3(0, 1, 0);
            request = GameObject.Find(newReq.name + "(Clone)");
            
        }
        if (full)
        {
            count = 0;
        }
    }

    private void Update()
    {
        if (!request)
        {
            GameObject newReq = requests[Random.Range(0, requests.Length)];
            Instantiate(newReq);
            GameObject.Find(newReq.name + "(Clone)").transform.parent = transform;
            newReq.transform.position = transform.position + new Vector3(0, 1, 0);
            request = GameObject.Find(newReq.name + "(Clone)");
        }
        if (count == 5)
        {
            full = true;
        }  
    }

}
