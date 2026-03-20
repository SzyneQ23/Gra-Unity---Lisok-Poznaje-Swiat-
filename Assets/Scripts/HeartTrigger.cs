using UnityEngine;

public class HeartTrigger : MonoBehaviour
{
    public AudioClip dzwiekZebrania;

    private void OnDisable()
    {
        AudioSource.PlayClipAtPoint(dzwiekZebrania, Camera.main.transform.position);
    }
}
