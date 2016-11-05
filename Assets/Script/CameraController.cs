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
	public float up = 1;

	public Vector3 forward;
	public Vector3 right;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (target != null)
		{
			transform.position = target.position + Vector3.up * up;
			transform.localRotation = Quaternion.identity;
			transform.Rotate(Vector3.up, angle, Space.Self);

			forward = transform.forward;
			right = transform.right;

			transform.Rotate(Vector3.right, angleUp, Space.Self);
			transform.Translate(Vector3.back * dist, Space.Self);

			angle += GestureZone.Instance.angleIncr;
		}

		GestureZone.Instance.angleIncr = 0;
	}
}
