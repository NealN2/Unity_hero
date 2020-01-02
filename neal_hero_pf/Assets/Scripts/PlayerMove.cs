using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //singleton
    public static PlayerMove Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMove>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerMove");
                    instance = instanceContainer.AddComponent<PlayerMove>();
                }
            }
            return instance;
        }
    }
    private static PlayerMove instance;


    public float moveSpeed;

    Rigidbody rb;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        /*
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rb.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
        */

        if (JoyStick.Instance.joyVec.x != 0 || JoyStick.Instance.joyVec.y != 0)
        {
            anim.SetBool("Walk", true);

            rb.velocity = new Vector3(JoyStick.Instance.joyVec.x * moveSpeed, rb.velocity.y, JoyStick.Instance.joyVec.y * moveSpeed);
            
            rb.rotation = Quaternion.LookRotation(new Vector3(JoyStick.Instance.joyVec.x, 0, JoyStick.Instance.joyVec.y));

        }

        else
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("Walk", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("NextStage"))
        {
            Debug.Log("다음 STAGE에 입장합니다");
            StageManager.Instance.EnterNextStage();
        }
    }
}
