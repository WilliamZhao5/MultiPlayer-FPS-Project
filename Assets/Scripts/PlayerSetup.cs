using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof (Player))]
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
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
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

        GameManager.DisconnectPlayer(transform.name);
    }
}
