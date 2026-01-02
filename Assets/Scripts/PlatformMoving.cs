using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.6f;
    float startPositionX;
    [SerializeField] private float moveRange = 2.0f;
    [SerializeField] bool isMovingRight = false;

    private void Awake()
    {
        startPositionX = this.transform.position.x;
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }



    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight==true)
        {
            if (this.transform.position.x < startPositionX + moveRange)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
                isMovingRight = false;
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - moveRange)
            {
                MoveLeft();
            }
            else
            {
                MoveRight();
                isMovingRight = true;
            }
        }
    }
}
