using System;

namespace fsm
{
	// Token: 0x0200034C RID: 844
	[Serializable]
	public class UITransition : TimerTransition
	{
		// Token: 0x06001F77 RID: 8055 RVA: 0x00099154 File Offset: 0x00097354
		public UITransition()
		{
			this.onStart = (Action)Delegate.Combine(this.onStart, new Action(delegate
			{
				exUIMng.inst.enabled = false;
				this.from.StartFadeOut();
				this.to.StartFadeIn();
			}));
			this.onEnd = (Action)Delegate.Combine(this.onEnd, new Action(delegate
			{
				exUIMng.inst.enabled = true;
				this.from.FinishFadeOut();
				this.to.FinishFadeIn();
			}));
			this.onTick = (Action<float>)Delegate.Combine(this.onTick, new Action<float>(delegate(float _ratio)
			{
				this.from.FadeOut(_ratio);
				this.to.FadeIn(_ratio);
			}));
		}

		// Token: 0x04001028 RID: 4136
		public exUIPanel from;

		// Token: 0x04001029 RID: 4137
		public exUIPanel to;
	}
}
