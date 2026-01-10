using UnityEngine;

public class LockLever : MonoBehaviour
{
    public int leverID;
    private Animator anim;
    private bool isPlayerInRange = false; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Lis przy dźwigni. Możesz nacisnąć E.");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ActivateLever();
        }
    }

    void ActivateLever()
    {
        if (anim != null)
        {
            anim.SetTrigger("Pull");
        }

        CoreLockManager.instance.UseLever(leverID);
        Debug.Log("Dźwignia " + leverID + " zadziałała błyskawicznie!");
    }
}