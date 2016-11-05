using UnityEngine;
using System.Collections;

public class PipeItem : MonoBehaviour {

    public Transform myPipe;
    public Transform otherPipe;
    public Transform otherPipeExit;

    void OnTriggerEnter(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (p == null) return;
        Vector3 upPos = myPipe.position + Vector3.up * .75f;
        Vector3 otherUpPos = otherPipe.position + Vector3.up*.75f;
        Vector3 endPos = otherPipeExit.position;
        p.StartPipeAnimation(upPos, otherUpPos, endPos);

    }

    void OnTriggerExit(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (p == null) return;
        p.OutTeleport();
    }
}
