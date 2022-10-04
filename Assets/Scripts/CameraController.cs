using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float offsetX;
    public float offsetY;
    public float limitX;
    public float limitX1;
    public float limitY;
    public float limitY1;
    public Transform boundsRight;
    public Transform boundsDown;
    public Transform boundsUp;
    public Transform boundsLeft;
    private float camOrthSize;
    private float camY, camX;
    private float cameraRatio;
    private Vector3 smoothPos;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        camOrthSize = cam.orthographicSize;
        cameraRatio = (boundsRight.position.x + camOrthSize) / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (player == null)
        //{
    if (player == null) {
            GameObject playerGameObject = GameObject.Find("Player");
            if (playerGameObject == null) return;
            else player = playerGameObject.transform;
    }
    //Camera cam = Camera.main;
    //float height = 2f * cam.orthographicSize;
    //float width = height * cam.aspect;
    //float inputX = Input.GetAxis("Horizontal");
    //if (player.position.x + offsetX + width/2 > boundsRight.position.x && inputX >0 )
    //{
    //}
    //else
    //{
    //    Vector3 pos = transform.position;
    //    pos.x = player.position.x + offsetX;
    //    pos.y = player.position.y + offsetY;
    //    transform.position = pos;
    //}

    camY = Mathf.Clamp(player.position.y, boundsDown.position.y + camOrthSize, boundsUp.position.y - camOrthSize);
        camX = Mathf.Clamp(player.position.x, boundsLeft.position.x + cameraRatio, boundsRight.position.x - cameraRatio);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), 0.5f);
        this.transform.position = smoothPos;
        //    }
        //}

        //if (player.position.y - transform.position.y < offsetY)
        //{
        //    pos.y = player.position.y - offsetY;
        //}


        //}
        //if (transform.position.y - player.position.y < offsetY)
        //{

        //pos.y = player.position.y;

        //}

        //}
    }

}

