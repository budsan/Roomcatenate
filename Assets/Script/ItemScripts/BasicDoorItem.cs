using UnityEngine;
using System.Collections;

public class BasicDoorItem : MonoBehaviour {

    public bool opened = false;
    public Animator anim;
    public GameObject keyLock;
    public Animator keyAnim;

    bool hasKey = false;
    public bool HasKey
    {
        get
        {
            return hasKey;
        }
    }

    int numPlayers = 0;

    public void SetKey()
    {
        hasKey = true;
        keyLock.SetActive(true);
    }

    void OnTriggerEnter(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();

        if (p == null) return;
        ++numPlayers;
        if(hasKey)
        {
            if (p.RemoveKey())
            {
                hasKey = false;
                keyAnim.SetTrigger("Open");
                Destroy(keyLock.GetComponent<Collider>());
                Destroy(keyLock, 1f);
            }
            else return;
        }
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
