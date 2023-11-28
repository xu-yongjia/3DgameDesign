using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    private Animator ani;
    public float fulltime;
    public float maxspeed;
    private float holdtime = 0;
    public GameObject prefab; // 预制对象的引用

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        if (Input.GetButton("Fire1"))
        {
            ani.SetBool("Fire", true);
        }
        else
        {
            ani.SetBool("Fire", false);
        }
        if(Input.GetButton("Fire2"))
        {
            ani.SetBool("Holding",true);
            if(holdtime < fulltime && ani.GetCurrentAnimatorStateInfo(0).IsName("hold"))
                holdtime += Time.deltaTime;
            ani.SetFloat("Blend", holdtime / fulltime);
        }
        else
        {
            ani.SetBool("Holding", false);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void EnterEmpty()
    {
        holdtime = 0;
        ani.SetFloat("Blend", 0);
    }
    public void Shoot(string name)
    {
        Debug.Log("Shoot");
        if (name == "shoot")
        {
            SSDirector.getInstance().currentSceneController.Shoot(ani.GetFloat("Blend") * maxspeed);
            /*float initialSpeed= ani.GetFloat("Blend")*maxspeed;
            // 从预制中实例化游戏对象
            GameObject Arrow = Instantiate(prefab);

            // 设置位置和朝向
            Arrow.transform.position = transform.position;
            Arrow.transform.rotation = transform.rotation;
            // 获取刚体组件
            Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

            // 计算初始速度向量
            Vector3 initialVelocity = transform.forward * initialSpeed;

            // 设置初始速度
            rigidbody.velocity = initialVelocity;
            */
        }
    }
}
