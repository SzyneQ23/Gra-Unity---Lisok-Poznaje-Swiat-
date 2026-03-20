using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;
    public GameObject platformPrefab;
    public int availablePlatforms = 4;
    
    private List<GameObject> placedPlatforms = new List<GameObject>();
    private List<GameObject> allPickups = new List<GameObject>(); 

    public TMP_Text availablePlatformsText;

    private void Awake() 
    { 
        instance = this; 

        GameObject[] pickups = GameObject.FindGameObjectsWithTag("PlatformPickup");
        allPickups.AddRange(pickups);
        availablePlatformsText.text=availablePlatforms.ToString();
    }

    public void ResetPlatforms()
    {
        foreach (GameObject p in placedPlatforms)
        {
            if (p != null) Destroy(p);
        }
        placedPlatforms.Clear();

        foreach (GameObject pickup in allPickups)
        {
            pickup.SetActive(true);
        }

        availablePlatforms = 4;
        availablePlatformsText.text=availablePlatforms.ToString();
        Debug.Log("Reset zakończony: Platformy 4, paczki odświeżone.");
    }

    public void AddPlatforms(int amount)
    {
        availablePlatforms += amount;
        availablePlatformsText.text=availablePlatforms.ToString();
    }

    public void PlacePlatform(Vector3 position)
    {
        if (availablePlatforms > 0)
        {
            GameObject newP = Instantiate(platformPrefab, position, Quaternion.identity);
            placedPlatforms.Add(newP);
            availablePlatforms--;
            availablePlatformsText.text=availablePlatforms.ToString();
        }
    }
}