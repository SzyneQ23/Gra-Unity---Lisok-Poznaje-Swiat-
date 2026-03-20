using Unity.VisualScripting;
using UnityEngine;

public class SzkieletTrigger : MonoBehaviour
{

    public SzkieletController szkielet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            szkielet.WakeUp();
            Debug.Log("Pojawil sie szkielet!");
            gameObject.SetActive(false);
        }
    }

}
