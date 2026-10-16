using System;

namespace XRL.World
{
	// Token: 0x020007AC RID: 1964
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Singleton)]
	public class GetZoneFreezabilityEvent : IZoneEvent
	{
		// Token: 0x06004FE9 RID: 20457 RVA: 0x00185888 File Offset: 0x00183A88
		public GetZoneFreezabilityEvent()
		{
			this.ID = GetZoneFreezabilityEvent.ID;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x001858A2 File Offset: 0x00183AA2
		public override int GetCascadeLevel()
		{
			return GetZoneFreezabilityEvent.CascadeLevel;
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x001858A9 File Offset: 0x00183AA9
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x001858BD File Offset: 0x00183ABD
		public override void Reset()
		{
			base.Reset();
			this.Freezability = Freezability.TooRecentlyActive;
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x001858CC File Offset: 0x00183ACC
		public static Freezability GetFor(Zone Zone, Freezability Freezability = Freezability.Freezable)
		{
			GetZoneFreezabilityEvent.Instance.Zone = Zone;
			GetZoneFreezabilityEvent.Instance.Freezability = Freezability;
			The.Game.HandleEvent(GetZoneFreezabilityEvent.Instance);
			Zone.HandleEvent(GetZoneFreezabilityEvent.Instance);
			Freezability freezability = GetZoneFreezabilityEvent.Instance.Freezability;
			GetZoneFreezabilityEvent.Instance.Reset();
			return freezability;
		}

		// Token: 0x0400278E RID: 10126
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetZoneFreezabilityEvent), null, null, null);

		// Token: 0x0400278F RID: 10127
		public new static readonly int CascadeLevel = 0;

		// Token: 0x04002790 RID: 10128
		public static readonly GetZoneFreezabilityEvent Instance = new GetZoneFreezabilityEvent();

		// Token: 0x04002791 RID: 10129
		public Freezability Freezability = Freezability.Freezable;
	}
}
