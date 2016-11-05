using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    public float speed = 10f;

    public int id = 0;

    CharacterController cont;

    List<KeyItem> keys;

	// Use this for initialization
	void Start () {
        cont = GetComponent<CharacterController>();
        keys = new List<KeyItem>();
	}
	
	// Update is called once per frame
	void Update () {

        ModelAnimations();

        if (animating) return;
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        Vector3 dir = Vector3.forward * ver + Vector3.right * hor;
        cont.Move(dir * speed * Time.deltaTime);
    }

    public Transform AddKey(KeyItem key)
    {
        keys.Add(key);
        if (keys.Count == 1) return transform;
        return keys[keys.Count - 2].transform;
    }

    public bool RemoveKey()
    {
        if (keys.Count <= 0) return false;
        Destroy(keys[0].gameObject);
        keys.RemoveAt(0);
        if (keys.Count > 0) keys[0].setNewFollow(transform);
        return true;
    }


    public enum ModelState
    {
        Basic,
        ToPipe,
        DownPipe,
        UpPipe,
        ToBasic
    }

    public Transform modelAnim;
    ModelState state = ModelState.Basic;
    bool teleported = false;
    bool animating = false;
    Vector3 nextPos;
    Vector3 halfPos;
    Vector3 initPos;
    Vector3 initHalfPos;
    Vector3 otherPipe;
    Vector3 otherEnd;


    float animCounter = 0;

    void ModelAnimations()
    {
        if (!animating) return;
        animCounter += Time.deltaTime*4;
        switch (state)
        {
            case ModelState.ToPipe:
                if(animCounter >= 1)
                {
                    animCounter = 0;
                    modelAnim.position = nextPos;
                    state = ModelState.DownPipe;
                    initPos = nextPos;
                    nextPos = initPos - Vector3.up * 1.5f;
                } else
                {
                    halfPos = Vector3.Lerp(initHalfPos, nextPos, animCounter);
                    modelAnim.position = Vector3.Lerp(initPos, halfPos, animCounter);
                }
                break;
            case ModelState.DownPipe:
                if (animCounter >= 1)
                {
                    transform.position = otherEnd;
                    animCounter = 0;
                    modelAnim.position = nextPos;
                    state = ModelState.UpPipe;
                    initPos = otherPipe - Vector3.up * 1.5f;
                    nextPos = otherPipe;
                }
                else
                {
                    modelAnim.position = Vector3.Lerp(initPos, nextPos, animCounter);
                }
                break;
            case ModelState.UpPipe:
                if (animCounter >= 1)
                {
                    animCounter = 0;
                    modelAnim.position = nextPos;
                    state = ModelState.ToBasic;
                    initPos = nextPos;
                    nextPos = otherEnd;
                    initHalfPos = initPos+(nextPos - initPos + Vector3.up*10) * .5f;
                }
                else
                {
                    modelAnim.position = Vector3.Lerp(initPos, nextPos, animCounter);
                }
                break;
            case ModelState.ToBasic:
                if (animCounter >= 1)
                {
                    modelAnim.localPosition = Vector3.zero;
                    state = ModelState.Basic;
                    animating = false;
                }
                else
                {
                    halfPos = Vector3.Lerp(initHalfPos, nextPos, animCounter);
                    modelAnim.position = Vector3.Lerp(initPos, halfPos, animCounter);
                }
                break;
            default:
                break;
        }
    }

    public void StartPipeAnimation(Vector3 pipePos, Vector3 otherPipePos, Vector3 otherEndPos)
    {
        if (teleported || animating) return;
        teleported = true;
        animating = true;
        animCounter = 0;
        state = ModelState.ToPipe;

        otherPipe = otherPipePos;
        otherEnd = otherEndPos;
        initPos = modelAnim.position;
        nextPos = pipePos;
        initHalfPos = initPos+(pipePos - initPos + Vector3.up*10) * .5f;
    }

    public void OutTeleport()
    {
        if (animating) return;
        teleported = false;
    }
}
