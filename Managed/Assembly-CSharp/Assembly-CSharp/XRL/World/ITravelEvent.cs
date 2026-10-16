using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007DD RID: 2013
	[GameEvent(Cascade = 17, Base = true)]
	public abstract class ITravelEvent : MinEvent
	{
		// Token: 0x06005113 RID: 20755 RVA: 0x001888AB File Offset: 0x00186AAB
		public override int GetCascadeLevel()
		{
			return ITravelEvent.CascadeLevel;
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x001888B2 File Offset: 0x00186AB2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x001888BB File Offset: 0x00186ABB
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.TravelClass = null;
			this.PercentageBonus = 0;
			this.ApplicationTracking = null;
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x001888E0 File Offset: 0x00186AE0
		public int GetApplied(string Application)
		{
			int num;
			if (this.ApplicationTracking != null && !string.IsNullOrEmpty(Application) && this.ApplicationTracking.TryGetValue(Application, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x00188910 File Offset: 0x00186B10
		public void SetApplied(string Application, int Value)
		{
			if (!string.IsNullOrEmpty(Application))
			{
				if (this.ApplicationTracking == null)
				{
					this.ApplicationTracking = new Dictionary<string, int>();
				}
				this.ApplicationTracking[Application] = Value;
			}
		}

		// Token: 0x04002888 RID: 10376
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002889 RID: 10377
		public GameObject Actor;

		// Token: 0x0400288A RID: 10378
		public string TravelClass;

		// Token: 0x0400288B RID: 10379
		public int PercentageBonus;

		// Token: 0x0400288C RID: 10380
		public Dictionary<string, int> ApplicationTracking;
	}
}
