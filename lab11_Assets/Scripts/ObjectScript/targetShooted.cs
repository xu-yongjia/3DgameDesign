using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetShooted : MonoBehaviour
{
    public int targetValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("enter dusk");
        SSDirector.getInstance().currentSceneController.ArrowCollisionEnter(collision, this.gameObject, targetValue);
    }
}
