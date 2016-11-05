using UnityEngine;
using System.Collections;

public class LeverColliderItem : MonoBehaviour {
    public bool isRight;
    public Animator anim;

    void OnTriggerEnter(Collider c)
    {
        if (c.GetComponent<PlayerController>() == null) return;
        anim.SetBool("Right", isRight);
    }
}
