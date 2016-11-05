using UnityEngine;
using System.Collections;

public class EndItem : MonoBehaviour {

    int playerId = 0;
    bool isPlayer = false;
    void OnTriggerEnter(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (p == null || p.id != playerId) return;
        isPlayer = true;
    }

    void OnTriggerExit(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (p == null || p.id != playerId) return;
        isPlayer = false;
    }
}
