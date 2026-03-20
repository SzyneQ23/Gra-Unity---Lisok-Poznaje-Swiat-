using UnityEngine;
using UnityEngine.UIElements;

public class FIreballController : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    private Vector3 targetPos;
    private GameObject player;
    private Vector3 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        direction = (player.transform.position - transform.position).normalized;
        Destroy(gameObject, 4.0f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Gracz trafiony fireballem!");
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
