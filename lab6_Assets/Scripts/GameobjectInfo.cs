using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectInfo : MonoBehaviour
{
    //����λ����Ϣ������+�±꣩
    public int Area { set; get; }
    public int Index { set; get; }
    //�Ƿ���Ե��
    public bool clickable { set; get; } = true;
    public void UpdatePosition(int a, int b,bool movenow)//��ʼ��ʱ����movenowΪtrue�������ƶ���ָ��λ�á���Ϸ����ʱ����Ϊfalse���ɶ���������ִ�ж���
    {
        Area = a;
        Index = b;
        if (movenow)
        {
            Transform t = this.gameObject.transform;
            t.position = calPosition(a, b);
        }
    }
    Vector3 calPosition(int a, int b)//���������λ��
    {
        Vector3 res = new Vector3();
        Transform t = this.gameObject.transform;
        switch (a)
        {
            case 1://��
                res = new Vector3(0, 2, -7 - 2 * b);
                break;
            case 2://�Ұ�
                res = new Vector3(0, 2, 7 + 2 * b);
                break;
            case 3://��
                res = new Vector3(0, 1.25f, SSDirector.getInstance().currentSceneController.getBoatPosition() + b * 1.2f - 0.6f);
                break;
        }
        return res;
    }
}