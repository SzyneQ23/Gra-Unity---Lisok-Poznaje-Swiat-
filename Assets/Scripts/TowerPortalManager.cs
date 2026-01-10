using UnityEngine;

public class TowerPortalManager : MonoBehaviour
{
    public static TowerPortalManager instance;

    [Header("Twoja Sekwencja (ID kolorów)")]
    public int[] correctSequence = { 0, 1, 1, 2, 2, 3 };
    private int currentFloor = 0;

    [Header("Punkty Startowe")]
    public Transform groundFloorStart; 
    public Transform[] floorSpawnPoints; 

    private void Awake() => instance = this;

    public void PortalEntered(int id, GameObject player)
    {
        if (id == correctSequence[currentFloor])
        {
            currentFloor++;

            if (currentFloor < floorSpawnPoints.Length)
            {
                player.transform.position = floorSpawnPoints[currentFloor].position;
                Debug.Log("Dobry wybór! Piętro: " + currentFloor);
            }
            else
            {
                Debug.Log("SZCZYT OSIĄGNIĘTY!");
            }
        }
        else
        {
            currentFloor = 0;
            player.transform.position = groundFloorStart.position;
            Debug.Log("Błąd! Spadek na sam dół.");
        }
    }
}