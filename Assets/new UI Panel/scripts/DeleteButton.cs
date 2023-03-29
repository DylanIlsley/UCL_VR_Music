using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public NetworkHandler networkHandler;
    public GameObject objectToSpawn;
    public Vector3 spawnPosition; 
    public Vector3 spawnRotation; 
    public Vector3 spawnScale;

    private GameObject ConfirmationPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        // Spawn Confirmation Panel if not spawned already
        if (ConfirmationPanel == null) {
            ConfirmationPanel = Instantiate(objectToSpawn, spawnPosition, Quaternion.Euler(spawnRotation));
            ConfirmationPanel.transform.localScale = spawnScale;
        } else {
            // do nothing
        }
    }

}
