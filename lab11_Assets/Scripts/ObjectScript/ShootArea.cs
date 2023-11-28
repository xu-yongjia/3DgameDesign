using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArea : MonoBehaviour
{
    public int areavalue;
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
        SSDirector.getInstance().currentSceneController.PlayerCollisionEnter(other,this.gameObject,1, areavalue);
    }
    public void OnTriggerExit(Collider other)
    {
        SSDirector.getInstance().currentSceneController.PlayerCollisionEnter(other,this.gameObject,2);
    }
}
