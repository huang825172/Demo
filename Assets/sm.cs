using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sm : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject ball;
    public GameObject space;

    private Animator animator;
    private SpriteRenderer SpriteRenderer;
    private Rigidbody2D rigid;

    private Vector2 initPos;
    private Quaternion initRot;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
        this.rigid = GetComponent<Rigidbody2D>();
        this.initPos = transform.position;
        this.initRot = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
        if (transform.position.y < -10)
        {
            this.space.SetActive(true);
        }
        if (this.space.activeSelf && Input.GetKey(KeyCode.Space))
        {
            this.rigid.isKinematic = true;
            this.rigid.velocity = Vector2.zero;
            this.rigid.angularVelocity = 0;
            this.transform.SetPositionAndRotation(this.initPos, this.initRot);
            this.rigid.isKinematic = false;
            this.space.SetActive(false);
        }
        else if (!this.space.activeSelf && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            this.animator.SetBool("isWalking", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.SpriteRenderer.flipX = true;
                transform.Translate(Vector2.right * -0.01f);
            }
            else
            {
                this.SpriteRenderer.flipX = false;
                transform.Translate(Vector2.right * 0.01f);
            }
        } else this.animator.SetBool("isWalking", false);
        if (Mathf.Abs(transform.position.x - ball.transform.position.x) < 0.3) dialogue.SetActive(true);
        else dialogue.SetActive(false);
    }

    void Update()
    {
        SyncCam();
    }

    private void SyncCam()
    {
        Camera.main.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            Camera.main.transform.position.z);
    }
}
