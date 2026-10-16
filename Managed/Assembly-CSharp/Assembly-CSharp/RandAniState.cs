using System;
using fsm;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class RandAniState : AniState
{
	// Token: 0x060008F6 RID: 2294 RVA: 0x000399EE File Offset: 0x00037BEE
	public RandAniState(string _name, Animation _anim, string _animList, State _parent = null)
		: base(_name, _anim, _parent)
	{
		this.animList = _animList.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00039A14 File Offset: 0x00037C14
	public override void Play(Transition transition)
	{
		string text = this.animList[global::UnityEngine.Random.Range(0, this.animList.Length)];
		base.DoPlay(transition, text);
	}

	// Token: 0x0400061A RID: 1562
	public string[] animList;
}
