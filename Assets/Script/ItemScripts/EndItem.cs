using UnityEngine;
using System.Collections;

public class EndItem : MonoBehaviour
{
    public int playerId = 0;
	[HideInInspector] public LevelController controller;

    bool isPlayer = false;
    void OnTriggerEnter(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (p == null || p.id != playerId) return;
        isPlayer = true;

		controller.PlayerOnEnd(playerId, isPlayer);
	}

    void OnTriggerExit(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (p == null || p.id != playerId) return;
        isPlayer = false;

		controller.PlayerOnEnd(playerId, isPlayer);
	}
}
