using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
            print("FUCK!");
        }
    }
}
