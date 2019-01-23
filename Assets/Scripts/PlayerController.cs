using UnityEngine;
using System;
using Chronos;
using System.Collections;

public class PlayerController : BaseBehaviour
{
    public enum MoveDirection
    {
        forward,
        back,
        left,
        right,
        center
    };

    KeyCode LeftArrow;
    KeyCode RightArrow;
    KeyCode UpArrow;
    KeyCode DownArrow;

    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("PlayerController");
                go.AddComponent<PlayerController>();
            }

            return _instance;
        }
    }

    public int startX;
    public int startZ;
    public int currentX;
    public int currentZ;
    public int targetX;
    public int targetZ;
    public int distanceToMove;

    public float normalSpeed = 5.0f;
    public float slowSpeed = 2.0f;
    public float fastSpeed = 10.0f;
    public float currentSpeed;

    public bool movable = true;
    public bool isMoving = false;
    public bool isSlowed = false;
    public bool isPlaying = false;

    public GameController gameController;

    private Rigidbody rb;

    public Transform PlayerObject;

    public DrawLevel currentLevel;

    public string[,] currentLevelInstance;


    public int CurrentPosX { get; set; }
    public int CurrentPosZ { get; set; }
    public int CurrentPosY { get; set; }
    public int StartPosX { get; set; }
    public int StartPosZ { get; set; }

    private bool keyPressed;
    public MoveDirection moveDirection = MoveDirection.center;

    public Clock clock;

    void Awake()
    {
        _instance = this;


        currentLevel = FindObjectOfType<DrawLevel>();

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentLevelInstance = GameController.Instance.levelInstance;

        StartPosX = Convert.ToInt32(GameController.Instance.extrasList[0, 1]);
        StartPosZ = Convert.ToInt32(GameController.Instance.extrasList[0, 2]);
        CurrentPosX = StartPosX;
        CurrentPosZ = StartPosZ;
        moveDirection = MoveDirection.center;

        clock = time.clock;
        currentSpeed = normalSpeed;
    }


    private MoveDirection GetInput()
    {

        var h = Input.GetAxisRaw("Horizontal");
        var z = Input.GetAxisRaw("Vertical");
        
        //Debug.Log("xKey = " + h.ToString());
        //Debug.Log("zKey = " + z.ToString());

        if (movable == false || (h == 0 && z == 0))
        {
            return moveDirection = MoveDirection.center;
        }

        if (h == 1 && z == 0)
        {
            return moveDirection = MoveDirection.right;
        }
        else if (h == -1 && z == 0)
        {
            return moveDirection = MoveDirection.left;
        }
        else if (z == 1 && h == 0)
        {
            return moveDirection = MoveDirection.forward;
        }
        else if (z == -1 && h == 0)
        {
            return moveDirection = MoveDirection.back;
        }
        else
        {
            return moveDirection = MoveDirection.center;
        }
    }

    private void FixedUpdate()
    {
        if (movable == true)
        {
            GetInput();
            // moveDirection = MoveDirection.forward;
            if (moveDirection != MoveDirection.center)
            {
                // StartCheck(MoveDirection direction, int targetZ, int targetX, int CurrentPosX, int CurrentPosZ )
                distanceToMove = CheckAhead(moveDirection);

                Vector3 currentPos = this.transform.position;

                if (distanceToMove > 0)
                {
                    movable = false;

                    if (isSlowed == true)
                    {
                        currentSpeed = slowSpeed;
                    }

                    //for ( int i = 1; i <= distanceToMove; i++ )
                    //{
                    if (moveDirection == MoveDirection.forward)
                    {
                        StartCoroutine(MoveOverSpeed(this.gameObject, currentPos + new Vector3(0, 0, distanceToMove),
                            currentSpeed));
                    }
                    else if (moveDirection == MoveDirection.back)
                    {
                        StartCoroutine(MoveOverSpeed(this.gameObject, currentPos - new Vector3(0, 0, distanceToMove),
                            currentSpeed));
                    }
                    else if (moveDirection == MoveDirection.left)
                    {
                        //clock.localTimeScale = 5;
                        StartCoroutine(MoveOverSpeed(this.gameObject, currentPos - new Vector3(distanceToMove, 0, 0),
                            currentSpeed));
                    }
                    else if (moveDirection == MoveDirection.right)
                    {
                        //clock.localTimeScale = 1;
                        StartCoroutine(MoveOverSpeed(this.gameObject, currentPos + new Vector3(distanceToMove, 0, 0),
                            currentSpeed));
                    }

                    //}
                }
            }
        }
    }

    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position =
                Vector3.MoveTowards(objectToMove.transform.position, end, speed * time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        movable = true;
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
    }

    private int CheckAhead(MoveDirection direction)
    {
        var distance = 0;
        var collision = false;

        switch (direction)
        {
            case MoveDirection.forward:

                targetZ = CurrentPosZ + 1;
                targetX = CurrentPosX;

                do
                {
                    if (currentLevelInstance[targetX, targetZ] == "0")
                    {
                        distance++;
                        targetZ++;
                        // Debug.Log(currentLevelInstance[targetX, targetZ]);
                    }
                    else
                    {
                        collision = true;
                        Debug.Log("Collision: " + currentLevelInstance[targetX, targetZ]);
                        print("Distance = " + distance);
                        break;
                    }
                } while (!collision);

                CurrentPosX = targetX;
                CurrentPosZ = targetZ - 1;

                return distance;


            case MoveDirection.back:

                targetZ = CurrentPosZ - 1;
                targetX = CurrentPosX;

                do
                {
                    if (currentLevelInstance[targetX, targetZ] == "0")
                    {
                        distance++;
                        targetZ--;
                    }
                    else
                    {
                        collision = true;
                        Debug.Log("Collision: " + currentLevelInstance[targetX, targetZ]);
                        print("Distance = " + distance);
                        break;
                    }
                } while (!collision);

                CurrentPosX = targetX;
                CurrentPosZ = targetZ + 1;

                return distance;

            case MoveDirection.right:

                targetX = CurrentPosX + 1;
                targetZ = CurrentPosZ;

                do
                {
                    if (currentLevelInstance[targetX, targetZ] == "0")
                    {
                        distance++;
                        targetX++;
                    }
                    else
                    {
                        collision = true;
                        Debug.Log("Collision: " + currentLevelInstance[targetX, targetZ]);
                        print("Distance = " + distance);
                        break;
                    }
                } while (!collision);

                CurrentPosX = targetX - 1;
                CurrentPosZ = targetZ;

                return distance;

            case MoveDirection.left:

                targetX = CurrentPosX - 1;
                targetZ = CurrentPosZ;

                do
                {
                    if (currentLevelInstance[targetX, targetZ] == "0")
                    {
                        distance++;
                        targetX--;
                    }
                    else
                    {
                        collision = true;
                        Debug.Log("Collision: " + currentLevelInstance[targetX, targetZ]);
                        print("Distance = " + distance);
                        break;
                    }
                } while (!collision);

                CurrentPosX = targetX + 1;
                CurrentPosZ = targetZ;

                return distance;
        }

        return 0;
    }

    private void CheckTarget(int xPos, int zPos)
    {
    }

    private void Move(Vector3 dir)
    {
        //var target = transform.position + dir;
        //var beyondBoundaries = BeyondBounds(target);
        //if ( beyondBoundaries )
        //{
        //	return;
        //}

        //movable = false;

        //StartCoroutine(MoveWithSpeed(target));
    }

    private IEnumerator MoveWithSpeed(Vector3 position)
    {
        var startPos = transform.position;
        var distance = Vector3.Magnitude(startPos - position);
        var startTime = Time.time;
        var distanceCovered = 0.0f;
        var t = 0.0f;

        while (t < 1.0f && distance > 0.0f)
        {
            distanceCovered = (Time.time - startTime) * normalSpeed;
            t = distanceCovered / distance;
            transform.position = Vector3.Lerp(startPos, position, t);
            yield return null;
        }

        transform.position = position;
        movable = true;
    }

    //private bool BeyondBounds( Vector3 position )
    //{
    //	var local = _grid.WorldToGrid(position);
    //	var from = _renderer.From;
    //	var to = _renderer.To;

    //	var x = local.x;
    //	var z = local.z;

    //	if ( x > to.x || z > to.z || x < from.x || z < from.z )
    //	{
    //		return true;
    //	}
    //	return false;
    //}

    //private Vector3 DirectionToVector( MoveDirection dir )
    //{

    //	var forward = _grid.Forward;
    //	var right = _grid.Right;
    //	switch ( dir )
    //	{
    //		case MoveDirection.forward: return forward;
    //		case MoveDirection.back: return -forward;
    //		case MoveDirection.right: return right;
    //		case MoveDirection.left: return -right;
    //		default: return Vector3.zero;
    //	}
    //}

    //private MoveDirection GetInput()
    //{

    //	var h = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
    //	var z = Mathf.RoundToInt(Input.GetAxis("Vertical"));

    //	Debug.Log("xKey = " + h.ToString());
    //	Debug.Log("zKey = " + z.ToString());

    //	if ( !isMoving || ( h == 0 && z == 0 ) )
    //	{
    //		return MoveDirection.center;
    //	}


    //	if ( h == 1 )
    //	{
    //		return moveDirection = MoveDirection.right;
    //	}
    //	else if ( h == -1 )
    //	{
    //		return moveDirection = MoveDirection.left;
    //	}
    //	else if ( z == 1 )
    //	{
    //		return moveDirection = MoveDirection.forward;
    //	}
    //	else if ( z == -1 )
    //	{
    //		return moveDirection = MoveDirection.back;
    //	}

    //	return moveDirection = MoveDirection.center;
    //}

    //if ((xKeyPressed < 0 && zKeyPressed == 0 ) )
    //{
    //	print("Pressed left");
    //	moveDirection = MoveDirection.left;
    //}
    //else if (xKeyPressed > 0 && zKeyPressed == 0)
    //{
    //	print("Pressed right");
    //	moveDirection = MoveDirection.right;
    //}
    //else if (zKeyPressed < 0 && xKeyPressed == 0)
    //{
    //	print("Pressed down");
    //	moveDirection = MoveDirection.down;
    //}		
    //else if (zKeyPressed > 0 && zKeyPressed == 0 )
    //{
    //	print("Pressed up");
    //	moveDirection = MoveDirection.up;
    //}


    //}
} // end of class