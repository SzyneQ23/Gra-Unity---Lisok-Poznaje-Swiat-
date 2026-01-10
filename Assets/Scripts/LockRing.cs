using UnityEngine;

public class LockRing : MonoBehaviour
{
    private float targetRotation;
    public float rotationSpeed = 10f;
    public float TargetRotation => targetRotation; 

    void Start()
    {
        targetRotation = Random.Range(0, 4) * 90f;
        transform.eulerAngles = new Vector3(0, 0, targetRotation);
    }

    void Update()
    {
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetRotation, rotationSpeed * Time.deltaTime * 50f);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Rotate()
    {
        targetRotation += 90f;
    }

    public bool IsDoneRotating()
    {
        return Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetRotation)) < 0.5f;
    }
    public bool IsAligned()
    {
        float angle = transform.eulerAngles.z % 360;
        return Mathf.Abs(angle) < 1f; 
    }
}