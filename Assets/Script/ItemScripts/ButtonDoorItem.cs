using UnityEngine;
using System.Collections;

public class ButtonDoorItem : MonoBehaviour {

    public bool opened = false;
    public Animator anim;
    public Collider col;
    public Renderer[] rends;
    int numPlayers = 0;
	[HideInInspector] public int group;


    public void SetDoorOpened(bool isOpen)
    {
        if (isOpen != opened)
        {
            anim.SetInteger("DoorState", (isOpen) ? Random.Range(1, 3) : 0);
            col.isTrigger = isOpen;
        }
        opened = isOpen;
    }

    public void SetColor(Color c)
    {
        for (int i = 0; i < rends.Length; ++i) rends[i].material.color = c;
    }
}
