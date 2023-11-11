using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishCllision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.GetComponent<Rigidbody>().position = new Vector3(100000, 0, 0);

        SSDirector.getInstance().currentSceneController.RemoveCollision(this.gameObject);
    }
}
