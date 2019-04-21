using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortObject : MonoBehaviour
{
    public SkinnedMeshRenderer rend;
    public Animator anim;
    public GameObject marker;
    public float markerHeight;
    public float markerSize;
    public float moveSpeed = 1.2f;

    private int value;

    private Quaternion originalRot;
    private Vector3 target;
    private bool isMoving;
    private GameObject markerObj;

    private static float timeSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        Debug.Log("Start called");
        markerObj = Instantiate(marker, transform.position + (Vector3.up * markerHeight), Quaternion.identity, transform);
        markerObj.transform.localScale = Vector3.one * markerSize;
        markerObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * timeSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) <= 0.01f)
            {
                transform.rotation = originalRot;
                transform.position = target;
                anim.SetBool("isRunning", false);
                markerObj.SetActive(false);
                isMoving = false;
            }
        }
    }

    public void MoveTo(Vector3 pos)
    {
        originalRot = transform.rotation;
        transform.LookAt(pos);
        target = pos;
        anim.SetBool("isRunning", true);
        markerObj.SetActive(true);
        isMoving = true;
    }

    //TODO - this may not be the cleanest way to implement speed
    public static void SetTimeSpeed(float speed)
    {
        timeSpeed = speed;
    }

    public int Value
    {
        set { this.value = value; }
        get { return this.value; }
    }

    public bool IsMoving
    {
        get { return isMoving; }
    }

    public void SetColor(Color color)
    {
        rend.material.color = color;
    }
}
