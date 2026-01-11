using UnityEngine;

public class BlokadaManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isOpening = false;
    [SerializeField] private Vector3 moveOffset = new Vector3(0, 0.5f, 0);
    [SerializeField] private float speed = 0.03f;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position + moveOffset;
    }

    public void Otworz()
    {
        isOpening = true;
    }

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isOpening = false;
            }
        }
    }
}
