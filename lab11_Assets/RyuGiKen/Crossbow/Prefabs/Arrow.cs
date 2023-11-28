using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.transform.parent = collision.gameObject.transform;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        // 禁用刚体组件
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }
        Destroy(gameObject, lifetime);
    }
}
