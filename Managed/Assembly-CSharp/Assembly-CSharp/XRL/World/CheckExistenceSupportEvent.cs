using System;

namespace XRL.World
{
	// Token: 0x020006DA RID: 1754
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class CheckExistenceSupportEvent : PooledEvent<CheckExistenceSupportEvent>
	{
		// Token: 0x060049F2 RID: 18930 RVA: 0x00172EE3 File Offset: 0x001710E3
		public override int GetCascadeLevel()
		{
			return CheckExistenceSupportEvent.CascadeLevel;
		}

		// Token: 0x060049F3 RID: 18931 RVA: 0x00172EEA File Offset: 0x001710EA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049F4 RID: 18932 RVA: 0x00172EF3 File Offset: 0x001710F3
		public override void Reset()
		{
			base.Reset();
			this.SupportedBy = null;
			this.Object = null;
		}

		// Token: 0x060049F5 RID: 18933 RVA: 0x00172F09 File Offset: 0x00171109
		public static CheckExistenceSupportEvent FromPool(GameObject SupportedBy, GameObject Object)
		{
			CheckExistenceSupportEvent checkExistenceSupportEvent = PooledEvent<CheckExistenceSupportEvent>.FromPool();
			checkExistenceSupportEvent.SupportedBy = SupportedBy;
			checkExistenceSupportEvent.Object = Object;
			return checkExistenceSupportEvent;
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x00172F1E File Offset: 0x0017111E
		public static bool Check(GameObject SupportedBy, GameObject Object)
		{
			return GameObject.Validate(ref SupportedBy) && SupportedBy.WantEvent(PooledEvent<CheckExistenceSupportEvent>.ID, CheckExistenceSupportEvent.CascadeLevel) && !SupportedBy.HandleEvent(CheckExistenceSupportEvent.FromPool(SupportedBy, Object));
		}

		// Token: 0x04002385 RID: 9093
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002386 RID: 9094
		public GameObject SupportedBy;

		// Token: 0x04002387 RID: 9095
		public GameObject Object;
	}
}
