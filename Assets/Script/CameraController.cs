using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform target = null;
	public Transform Target
	{
		set
		{
			target = value;
		}
	}

	public float angle = 0;
	public float angleUp = 45;
	public float dist = 25;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (target != null)
		{
			transform.position = target.position;
			transform.localRotation = Quaternion.identity;
			transform.Rotate(Vector3.up, angle, Space.Self);
			transform.Rotate(Vector3.right, angleUp, Space.Self);
			transform.Translate(Vector3.back * dist, Space.Self);
		}
	}
}
