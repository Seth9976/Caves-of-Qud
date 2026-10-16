using System;
using UnityEngine;

namespace fsm
{
	// Token: 0x02000353 RID: 851
	public class TimerTransition : Transition
	{
		// Token: 0x06001F9D RID: 8093 RVA: 0x00099C34 File Offset: 0x00097E34
		public TimerTransition()
		{
			this.onStart = (Action)Delegate.Combine(this.onStart, new Action(delegate
			{
				this.timer = 0f;
			}));
			this.onTransition = delegate
			{
				this.timer += Time.deltaTime;
				if (this.onTick != null)
				{
					this.onTick(this.timer / this.duration);
				}
				return this.timer >= this.duration;
			};
		}

		// Token: 0x0400104D RID: 4173
		public float duration = 1f;

		// Token: 0x0400104E RID: 4174
		public Action<float> onTick;

		// Token: 0x0400104F RID: 4175
		private float timer;
	}
}
