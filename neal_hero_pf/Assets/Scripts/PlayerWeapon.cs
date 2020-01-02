using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.forward * 20f, ForceMode.Impulse);
        rb.velocity = transform.forward * 20f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Name : " + other.transform.name);
        if(other.transform.CompareTag("Wall") || other.transform.CompareTag("Monster"))
        {
            Debug.Log("Name :" + other.transform.name);
            rb.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Name : " + collision.transform.name);
        if (collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Monster"))
        {
            Debug.Log("Name : " + collision.transform.name);
            rb.velocity = Vector3.zero;
            Destroy(this.gameObject);
        }

    }
}
