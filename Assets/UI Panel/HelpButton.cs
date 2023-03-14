using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    // assign in inspector
    public GameObject objectToSpawn; 
    public Vector3 spawnPosition; 
    public Vector3 spawnRotation; 
    public Vector3 spawnScale; 

    private GameObject helpmenu;

    public void OnButtonClick()
    {
        if (helpmenu == null) {
            helpmenu = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(spawnRotation));
            helpmenu.transform.localScale = spawnScale;
        }
        else 
        {
            Destroy(helpmenu);
        }
    }
}

