using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
	public Vector3 target;
	public float speed;

	public static CCMoveToAction GetSSAction(Vector3 target, float speed){//创建动作实例
		CCMoveToAction action = ScriptableObject.CreateInstance<CCMoveToAction> ();
		action.target = target;
		action.speed = speed;
		return action;
	}

	public override void Update ()
	{//每次Update，按指定速度向指定位置移动
		this.transform.position = Vector3.MoveTowards (this.transform.position, target, speed * Time.deltaTime);
		if (this.transform.position == target) {
			//waiting for destroy
			this.destory = true;  
			this.callback.SSActionEvent (this);
		}
	}

	public override void Start () {
	}
}

