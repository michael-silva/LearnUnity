using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private List<GameObject> players;
    [SerializeField] private int playerActiveIndex;

    public bool IsPlayerActive(GameObject player)
    {
        return player == GetActivePlayer();
    }

    public GameObject GetActivePlayer()
    {
        return players[playerActiveIndex];
    }

    public void SetActivePlayer(int index)
    {
        if (index < 0 || index >= players.Count)
        {
            Debug.LogError($"Index {index} is out of players list with {players.Count - 1}");
            return;
        }
        playerActiveIndex = index;
    }

    private void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!Input.GetKeyDown(KeyCode.Keypad0 + i)) continue;
            SetActivePlayer(i);
            break;
        }
    }
}
