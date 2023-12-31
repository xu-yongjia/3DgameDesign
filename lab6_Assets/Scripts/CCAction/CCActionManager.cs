﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {
	
	private FirstController sceneController;
	private CCMoveToAction moveToA , moveToB, moveToC, moveToD;

	protected new void Start() {
        sceneController = GetComponent<FirstController>();
    }

	// Update is called once per frame
	protected new void Update ()
	{
		base.Update ();
	}
		
	#region ISSActionCallback implementation
	public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)//动作完成时，将对象标记为可点击
	{//动作结束，设置对象可以点击
        source.gameobject.GetComponent<GameobjectInfo>().clickable = true;
    }
	#endregion
    public void Move_3step(GameObject obj,int a,int b,float height,float speed)//指定待移动对象、目标区域和索引、上升的高度、移动的速度
    {
        
        Vector3 start = obj.transform.position,end=calPosition(a,b);
        CCSequenceAction move = CCSequenceAction.GetSSAction_3step(start, end, height, speed, obj);
        this.RunAction(obj, move, this);
    }
    public void Move_to(GameObject obj,Vector3 target,float speed)//指定待移动对象、移动目标、速度
    {
        CCMoveToAction move = CCMoveToAction.GetSSAction(target, speed);
        this.RunAction(obj, move, this);
    }
    Vector3 calPosition(int a, int b)//计算人物的位置
    {
        Vector3 res = new Vector3();
        Transform t = this.gameObject.transform;
        switch (a)
        {
            case 1://左岸
                res = new Vector3(0, 2, -7 - 2 * b);
                break;
            case 2://右岸
                res = new Vector3(0, 2, 7 + 2 * b);
                break;
            case 3://船
                res = new Vector3(0, 1.25f, SSDirector.getInstance().currentSceneController.getBoatPosition() + b * 1.2f - 0.6f);
                break;
        }
        return res;
    }
}

