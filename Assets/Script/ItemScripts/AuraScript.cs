using UnityEngine;
using System.Collections;

public class AuraScript : MonoBehaviour {

    public float speed = .5f;
    public Renderer rend;
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = (rend.material.mainTextureOffset + Vector2.up * Time.deltaTime*speed);
        offset.y = offset.y % 1f;
        rend.material.mainTextureOffset = offset;
	}
}
