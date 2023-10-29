﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CCSequenceAction : SSAction, ISSActionCallback
{
	public List<SSAction> sequence;
	public int repeat = -1; //repeat forever
	public int start = 0;
    public static CCSequenceAction GetSSAction_3step(Vector3 start, Vector3 end, float height, float speed, GameObject obj)
    {
        CCMoveToAction A = CCMoveToAction.GetSSAction(new Vector3(start.x, height, start.z), speed);
        CCMoveToAction B = CCMoveToAction.GetSSAction(new Vector3(end.x, height, end.z), Vector3.Distance(start,end)*2);
        CCMoveToAction C = CCMoveToAction.GetSSAction(end, speed);
        CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction>();
		action.repeat = 1;
        action.sequence = new List<SSAction> { A, B, C };
        action.start = 0;
		return action;
    }
    public static CCSequenceAction GetSSAction(int repeat, int start , List<SSAction> sequence){
		CCSequenceAction action = ScriptableObject.CreateInstance<CCSequenceAction> ();
		action.repeat = repeat;
		action.sequence= sequence;
		action.start = start;
		return action;
	}
		
	// Update is called once per frame
	public override void Update ()
	{
		if (sequence.Count == 0) return;  
		if (start < sequence.Count) {
			sequence [start].Update ();
		}
	}

	public void SSActionEvent (SSAction source, SSActionEventType events = SSActionEventType.Competeted, int intParam = 0, string strParam = null, Object objectParam = null)
	{
		source.destory = false;
		this.start++;
		if (this.start >= sequence.Count) {
			this.start = 0;
			if (repeat > 0) repeat--;
			if (repeat == 0) { this.destory = true; this.callback.SSActionEvent (this); }
		}
	}

	// Use this for initialization
	public override void Start () {
		foreach (SSAction action in sequence) {
			action.gameobject = this.gameobject;
			action.transform = this.transform;
			action.callback = this;
			action.Start ();
		}
	}

	void OnDestory() {
		//TODO: something
	}
}

