using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;

    CharacterController cont;

	// Use this for initialization
	void Start () {
        cont = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        Vector3 dir = Vector3.forward * ver + Vector3.right * hor;
        cont.Move(dir * speed * Time.deltaTime);
    }
}
