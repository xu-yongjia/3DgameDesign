using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuskArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter dusk");
        SSDirector.getInstance().currentSceneController.PlayerCollisionEnter(other, this.gameObject, 3);
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("exit dusk");
        SSDirector.getInstance().currentSceneController.PlayerCollisionEnter(other, this.gameObject, 4);
    }
}
