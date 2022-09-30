using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public float walkDuration = 1f;
    public float idleDuration = 1f;
    public float attackDuration = 0.5f;
    public Sprite[] downIdle;
    public Sprite[] upIdle;
    public Sprite[] rightIdle;
    public Sprite[] leftIdle;

    public Sprite[] downMove;
    public Sprite[] upMove;
    public Sprite[] rightMove;
    public Sprite[] leftMove;

    //public Sprite[] upAttack;
    //public Sprite[] downAttack;
    //public Sprite[] rightAttack;
    //public Sprite[] leftAttack;

    public Sprite[] frames;
    
    public bool moving = true;
    
    public Vector2 direction;

    private SpriteRenderer sr;

    //private Vector2 rightVector = new Vector2(1, 0);
    //private Vector2 leftVector = new Vector2(-1, 0);
    //private Vector2 upVector = new Vector2(0, 1);
    //private Vector2 downVector = new Vector2(0, -1);

    // Start is called before the first frame update
    void Start()
    {
        frames = downIdle;
        sr = GetComponent<SpriteRenderer>();
    }

    // right, left, up, down
    public Sprite[] GetFramesFromDirection(Sprite[][] frames)
    {
        // right
        if (direction.Equals(Vector2.right))
        {
            return frames[0];
        }
        // left
        else if (direction.Equals(Vector2.left))
        {
            //playerDir = new Vector2(-1, 0);
            return frames[1];
        }
        // up 
        else if (direction.Equals(Vector2.up))
        {
            //playerDir = new Vector2(0, 1);
            return frames[2];
        }
        // down
        else
        {
            //playerDir = new Vector2(0, -1);
            return frames[3];
        }
    }

    // Update is called once per frame
    IEnumerator MoveAnimation()
    {
        Sprite[][] walkFrames = { rightMove, leftMove, upMove, downMove };
        frames = GetFramesFromDirection(walkFrames);
        float frameDuration = walkDuration;
        int i;
        i = 0;
        if (!moving) // idle animations
        {
            frameDuration = idleDuration;
            if (direction.x > 0) frames = rightIdle; // idle right
            else if (direction.x < 0) frames = leftIdle;
            else if (direction.y > 0) frames = upIdle;
            else if (direction.y < 0) frames = downIdle;
        }
        while (true)
        {
            sr.sprite = frames[i];
            i++;
            if (i >= frames.Length)
            {
                i = 0;
            }
            yield return new WaitForSeconds(frameDuration);
        }
    }

    public Vector2 getDirection(Vector2 direction)
    {
        direction.Normalize();
        float right = Vector2.Distance(direction, Vector2.right);
        float left = Vector2.Distance(direction, Vector2.left);
        float up = Vector2.Distance(direction, Vector2.up);
        float down = Vector2.Distance(direction, Vector2.down);

        Dictionary<float, Vector2> directions = new Dictionary<float, Vector2>(){
            {right, Vector2.right},
            //{left, Vector2.left},
            //{up, Vector2.up},
            //{down, Vector2.down},
        };

        if (!directions.ContainsKey(left))
        {
            directions.Add(left, Vector2.left);
        }
        if (!directions.ContainsKey(up))
        {
            directions.Add(up, Vector2.up);
        }
        if (!directions.ContainsKey(down))
        {
            directions.Add(down, Vector2.down);
        }
        float closest = Mathf.Min(right, left, up, down);
        return directions[closest];
    }

    //private IEnumerator Attack(bool canMove)
    //{

    //    //enabled = false;
    //    StopCoroutine("MoveAnimation");

    //    Sprite[][] frameSet = { rightAttack, leftAttack, upAttack, downAttack };
    //    Sprite[] attackFrames = GetFramesFromDirection(frameSet);

    //    foreach (Sprite frame in attackFrames)
    //    {
    //        sr.sprite = frame;
    //        yield return new WaitForSeconds(attackDuration);
    //    }
    //    //enabled = true;
    //    StartCoroutine("MoveAnimation");
    //}


}
