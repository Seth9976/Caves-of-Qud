using System;
using fsm;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class AniState : State
{
	// Token: 0x1700012E RID: 302
	// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000398B4 File Offset: 0x00037AB4
	public float normalizedTime
	{
		get
		{
			AnimationState animationState = this.anim[this.curAniName];
			if (!(animationState != null))
			{
				return 0f;
			}
			return animationState.normalizedTime;
		}
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x000398E8 File Offset: 0x00037AE8
	public AniState(string _name, Animation _anim, State _parent = null)
		: base(_name, _parent)
	{
		this.anim = _anim;
		if (this.anim == null)
		{
			Debug.LogError("");
		}
		this.onFadeIn = (Action<Transition>)Delegate.Combine(this.onFadeIn, new Action<Transition>(this.Play));
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x00039940 File Offset: 0x00037B40
	public AniTransition to(AniState _targetState, float _duration)
	{
		AniTransition aniTransition = new AniTransition
		{
			source = this,
			target = _targetState,
			duration = _duration
		};
		this.transitionList.Add(aniTransition);
		return aniTransition;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00039975 File Offset: 0x00037B75
	public virtual void Play(Transition transition)
	{
		this.DoPlay(transition, this.name);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00039984 File Offset: 0x00037B84
	protected void DoPlay(Transition transition, string animName)
	{
		if (string.IsNullOrEmpty(animName))
		{
			this.anim.Stop();
			this.curAniName = null;
			return;
		}
		if (transition == null || ((TimerTransition)transition).duration == 0f)
		{
			this.anim.Play(animName);
		}
		else
		{
			this.anim.CrossFade(animName, ((TimerTransition)transition).duration);
		}
		this.curAniName = animName;
	}

	// Token: 0x04000618 RID: 1560
	public Animation anim;

	// Token: 0x04000619 RID: 1561
	public string curAniName;
}
