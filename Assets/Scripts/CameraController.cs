using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform player;
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
        instance = this;
        cam = Camera.main;
        camOrthSize = cam.orthographicSize;
        if (boundsRight != null) cameraRatio = (boundsRight.position.x + camOrthSize) / 2.0f;
    }

    public void Clear()
    {
        boundsDown = null;
        boundsUp = null;
        boundsLeft = null;
        boundsRight = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            GameObject playerGameObject = GameObject.Find("Player");
            if (playerGameObject == null) return;
            player = playerGameObject.transform;
        }
        if (boundsRight == null)
        {
            GameObject right = GameObject.Find("BoundsRight");
            if (right == null) return;
            boundsRight = right.transform;
            cameraRatio = (boundsRight.position.x + camOrthSize) / 2.0f;
        }
        if (boundsLeft == null)
        {
            GameObject left = GameObject.Find("BoundsLeft");
            if (left == null) return;
            boundsLeft = left.transform;
        }
        if (boundsUp == null)
        {
            GameObject up = GameObject.Find("BoundsUp");
            if (up == null) return;
            boundsUp = up.transform;
        }
        if (boundsDown == null)
        {
            GameObject down = GameObject.Find("BoundsDown");
            if (down == null) return;
            boundsDown = down.transform;
        }

        camY = Mathf.Clamp(player.position.y, boundsDown.position.y + camOrthSize, boundsUp.position.y - camOrthSize);
        camX = Mathf.Clamp(player.position.x, boundsLeft.position.x + cameraRatio, boundsRight.position.x - cameraRatio);
        smoothPos = Vector3.Lerp(this.transform.position, new Vector3(camX, camY, this.transform.position.z), 0.5f);
        this.transform.position = smoothPos;
    }
}

