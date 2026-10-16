using System;

namespace XRL.World
{
	// Token: 0x020006AC RID: 1708
	[GameEvent(Cascade = 143, Cache = MinEvent.Cache.Singleton)]
	public class BeginTakeActionEvent : SingletonEvent<BeginTakeActionEvent>
	{
		// Token: 0x060048AE RID: 18606 RVA: 0x0016D11C File Offset: 0x0016B31C
		public override int GetCascadeLevel()
		{
			return BeginTakeActionEvent.CascadeLevel;
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0016D123 File Offset: 0x0016B323
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0016D12C File Offset: 0x0016B32C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Traveling = false;
			this.TravelMessagesSuppressed = false;
			this.PreventAction = false;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0016D150 File Offset: 0x0016B350
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && Object.HasRegisteredEvent(BeginTakeActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(BeginTakeActionEvent.registeredInstance);
			}
			if (flag)
			{
				SingletonEvent<BeginTakeActionEvent>.Instance.Object = Object;
				SingletonEvent<BeginTakeActionEvent>.Instance.Traveling = false;
				SingletonEvent<BeginTakeActionEvent>.Instance.TravelMessagesSuppressed = false;
				SingletonEvent<BeginTakeActionEvent>.Instance.PreventAction = false;
				flag = Object.HandleEvent(SingletonEvent<BeginTakeActionEvent>.Instance) && !SingletonEvent<BeginTakeActionEvent>.Instance.PreventAction;
			}
			return flag;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0016D1D0 File Offset: 0x0016B3D0
		public static bool Check(GameObject Object, bool Traveling, ref bool TravelMessagesSuppressed)
		{
			bool flag = true;
			if (flag && Object.HasRegisteredEvent(BeginTakeActionEvent.registeredInstance.ID))
			{
				flag = Object.FireEvent(BeginTakeActionEvent.registeredInstance);
			}
			if (flag)
			{
				SingletonEvent<BeginTakeActionEvent>.Instance.Object = Object;
				SingletonEvent<BeginTakeActionEvent>.Instance.Traveling = Traveling;
				SingletonEvent<BeginTakeActionEvent>.Instance.TravelMessagesSuppressed = TravelMessagesSuppressed;
				SingletonEvent<BeginTakeActionEvent>.Instance.PreventAction = false;
				flag = Object.HandleEvent(SingletonEvent<BeginTakeActionEvent>.Instance) && !SingletonEvent<BeginTakeActionEvent>.Instance.PreventAction;
				TravelMessagesSuppressed = SingletonEvent<BeginTakeActionEvent>.Instance.TravelMessagesSuppressed;
			}
			return flag;
		}

		// Token: 0x040022D3 RID: 8915
		public new static readonly int CascadeLevel = 143;

		// Token: 0x040022D4 RID: 8916
		public GameObject Object;

		// Token: 0x040022D5 RID: 8917
		public bool Traveling;

		// Token: 0x040022D6 RID: 8918
		public bool TravelMessagesSuppressed;

		// Token: 0x040022D7 RID: 8919
		public bool PreventAction;

		// Token: 0x040022D8 RID: 8920
		public static ImmutableEvent registeredInstance = new ImmutableEvent("BeginTakeAction");
	}
}
