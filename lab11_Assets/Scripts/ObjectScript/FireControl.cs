using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    private Animator ani;
    public float fulltime;
    public float maxspeed;
    private float holdtime = 0;
    public GameObject prefab; // Ԥ�ƶ��������

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
            // ��Ԥ����ʵ������Ϸ����
            GameObject Arrow = Instantiate(prefab);

            // ����λ�úͳ���
            Arrow.transform.position = transform.position;
            Arrow.transform.rotation = transform.rotation;
            // ��ȡ�������
            Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

            // �����ʼ�ٶ�����
            Vector3 initialVelocity = transform.forward * initialSpeed;

            // ���ó�ʼ�ٶ�
            rigidbody.velocity = initialVelocity;
            */
        }
    }
}
