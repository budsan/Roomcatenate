using UnityEngine;
using System.Collections;

public class BasicDoorItem : MonoBehaviour {

    public bool opened = false;
    public Animator anim;

    int numPlayers = 0;


    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<PlayerController>() == null) return;
        ++numPlayers;
        bool newOpen = numPlayers != 0;
        if (newOpen != opened) anim.SetInteger("DoorState", Random.Range(1, 3));
        opened = newOpen;
    }

    void OnTriggerExit(Collider c)
    {
        if (c.GetComponent<PlayerController>() == null) return;
        --numPlayers;
        bool newOpen = numPlayers != 0;
        if (newOpen != opened) anim.SetInteger("DoorState", 0);
        opened = newOpen;
    }
}
