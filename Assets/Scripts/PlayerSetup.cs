using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayer = "RemotePlayer";

    private Camera sceneCamera;
    private string playerID;

    //Disable the Player Motor and Controller if the Player is not the local player
    //if the Player is the local player, then remove the Scnene Camera
    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRomoteLayer();

        } else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);

            }
        }

        RegisterPlayer();
    }

    //register the Player so that each player has a unique ID and name
    void RegisterPlayer()
    {
        playerID = "Player " + GetComponent<NetworkIdentity>().netId;
        transform.name = playerID;
    }

    //assign remote layer to the non-local Player
    void AssignRomoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayer);
    }

    //disable certain components in the Player
    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);

        }
    }
}
