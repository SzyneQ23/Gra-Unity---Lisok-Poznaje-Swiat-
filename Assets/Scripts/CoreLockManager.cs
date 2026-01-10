using UnityEngine;

public class CoreLockManager : MonoBehaviour
{
    public static CoreLockManager instance;
    public LockRing[] rings;
    public GameObject artifactBarrier;
    
    private bool hasWon = false; 

    private void Awake() => instance = this;

    public void UseLever(int id)
    {
        if (hasWon) return; 

        switch (id)
        {
            case 0: rings[0].Rotate(); break;
            case 1: rings[0].Rotate(); rings[1].Rotate(); break;
            case 2: rings[1].Rotate(); rings[2].Rotate(); break;
            case 3: rings[2].Rotate(); rings[3].Rotate(); break;
        }
    }

    void Update()
    {
        if (!hasWon)
        {
            CheckForWinCondition();
        }
    }

    void CheckForWinCondition()
    {
        foreach (LockRing r in rings)
        {
            float currentAngle = r.transform.eulerAngles.z % 360;
            if (currentAngle < 0) currentAngle += 360;

            bool isAtZero = Mathf.Abs(currentAngle) < 1f || Mathf.Abs(currentAngle - 360) < 1f;

            bool isStatic = r.IsDoneRotating();

            if (!isAtZero || !isStatic) return;
        }

        hasWon = true;
        ExecuteWin();
    }

    void ExecuteWin()
    {
        Debug.Log("ZAGADKA ROZWIĄZANA!");
        
        if (artifactBarrier != null) artifactBarrier.SetActive(false);

        foreach (LockRing ring in rings)
        {
            ring.gameObject.SetActive(false);
        }
    }
}