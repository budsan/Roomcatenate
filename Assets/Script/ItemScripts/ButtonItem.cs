using UnityEngine;
using System.Collections;

public class ButtonItem : MonoBehaviour
{
	public Renderer toTint;
	public Color baseColor;
	public bool pressed;
	public Animator anim;

    int numPlayers = 0;

	public float Fx(float comp, float foo)
	{
		if (foo < 0)
		{
			return comp * (1.0f + foo);
		}
		else if (foo > 0)
		{
			return (1.0f - comp) * foo + comp;
		}

		return comp;
	}

	public void Update()
	{
		float foo;
		if (pressed)
			foo = (Mathf.Sin(Time.time) * 0.05f) + 0.3f;
		else
			foo = Mathf.Sin(Time.time) * 0.1f;

		Color newColor = new Color(Fx(baseColor.r, foo), Fx(baseColor.g, foo), Fx(baseColor.b, foo));
		toTint.material.color = newColor;

        anim.SetBool("Pressed", pressed);
    }


    void OnTriggerEnter(Collider c)
    {
        if (c.tag != "Player") return;
        ++numPlayers;
        pressed = numPlayers != 0;
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag != "Player") return;
        --numPlayers;
        pressed = numPlayers != 0;
    }
}
