using UnityEngine;
using System.Collections;

public class KeyItem : MonoBehaviour {

    public float distPlayer = 1f;
    Transform myPlayer;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (myPlayer == null) return;
        Vector3 nextPos = (myPlayer.position - transform.position);
        nextPos.y = 0;
        nextPos = transform.position + nextPos.normalized * (nextPos.magnitude - distPlayer);
        transform.position = Vector3.Lerp(transform.position, nextPos, .7f);
	}

    void OnTriggerEnter(Collider c)
    {
        PlayerController p = c.GetComponent<PlayerController>();
        if (myPlayer != null || p == null) return;
        myPlayer = p.AddKey(this);
    }

    public void setNewFollow(Transform newFollow)
    {
        myPlayer = newFollow;
    }
}
