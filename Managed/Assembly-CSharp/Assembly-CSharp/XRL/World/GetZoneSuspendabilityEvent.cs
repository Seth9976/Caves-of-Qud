using System;

namespace XRL.World
{
	// Token: 0x020007AD RID: 1965
	[GameEvent(Cascade = 0, Cache = MinEvent.Cache.Singleton)]
	public class GetZoneSuspendabilityEvent : IZoneEvent
	{
		// Token: 0x06004FEF RID: 20463 RVA: 0x00185948 File Offset: 0x00183B48
		public GetZoneSuspendabilityEvent()
		{
			this.ID = GetZoneSuspendabilityEvent.ID;
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x00185962 File Offset: 0x00183B62
		public override int GetCascadeLevel()
		{
			return GetZoneSuspendabilityEvent.CascadeLevel;
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x00185969 File Offset: 0x00183B69
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0018597D File Offset: 0x00183B7D
		public override void Reset()
		{
			base.Reset();
			this.Suspendability = Suspendability.Active;
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x0018598C File Offset: 0x00183B8C
		public static Suspendability GetFor(Zone Zone, Suspendability Suspendability = Suspendability.Suspendable)
		{
			GetZoneSuspendabilityEvent.Instance.Zone = Zone;
			GetZoneSuspendabilityEvent.Instance.Suspendability = Suspendability;
			The.Game.HandleEvent(GetZoneSuspendabilityEvent.Instance);
			Zone.HandleEvent(GetZoneSuspendabilityEvent.Instance);
			Suspendability suspendability = GetZoneSuspendabilityEvent.Instance.Suspendability;
			GetZoneSuspendabilityEvent.Instance.Reset();
			return suspendability;
		}

		// Token: 0x04002792 RID: 10130
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetZoneSuspendabilityEvent), null, null, null);

		// Token: 0x04002793 RID: 10131
		public new static readonly int CascadeLevel = 0;

		// Token: 0x04002794 RID: 10132
		public static readonly GetZoneSuspendabilityEvent Instance = new GetZoneSuspendabilityEvent();

		// Token: 0x04002795 RID: 10133
		public Suspendability Suspendability = Suspendability.Suspendable;
	}
}
