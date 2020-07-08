using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MatchSettings matchSettings;

    //if there are more than one GameManager, give an error
    //if not, this GameManager is the instance in the scene
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more than one GameManager in the scene.");
        } else
        {
            instance = this;
        }
    }

    #region Player Tracking

    private const string PLAYER_PREFIX = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string netID, Player player)
    {
        string PlayerID = PLAYER_PREFIX + netID;
        players.Add(PlayerID, player);
        player.transform.name = PlayerID;
    }

    public static void DisconnectPlayer(string PlayID)
    {
        players.Remove(PlayID);
    }

    public static Player getPlayer (string playerID)
    {
        return players[playerID];
    }

    /*
    //used to show the Dictionary players to the screen.
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string playerID in players.Keys)
        {
            GUILayout.Label(playerID + " - " + players[playerID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    */

    #endregion
}
