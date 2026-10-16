using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200069C RID: 1692
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class BeforePullDownEvent : MinEvent
	{
		// Token: 0x0600483B RID: 18491 RVA: 0x0016BC08 File Offset: 0x00169E08
		public BeforePullDownEvent()
		{
			this.ID = BeforePullDownEvent.ID;
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0016BC1B File Offset: 0x00169E1B
		public override int GetCascadeLevel()
		{
			return BeforePullDownEvent.CascadeLevel;
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x0016BC22 File Offset: 0x00169E22
		public static int CountPool()
		{
			if (BeforePullDownEvent.Pool != null)
			{
				return BeforePullDownEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x0016BC37 File Offset: 0x00169E37
		public static void ResetPool()
		{
			while (BeforePullDownEvent.PoolCounter > 0)
			{
				BeforePullDownEvent.Pool[--BeforePullDownEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x0016BC5F File Offset: 0x00169E5F
		public static void ResetTo(ref BeforePullDownEvent E)
		{
			MinEvent.ResetTo<BeforePullDownEvent>(E, BeforePullDownEvent.Pool, ref BeforePullDownEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x0016BC75 File Offset: 0x00169E75
		public static BeforePullDownEvent FromPool()
		{
			return MinEvent.FromPool<BeforePullDownEvent>(ref BeforePullDownEvent.Pool, ref BeforePullDownEvent.PoolCounter, 8192);
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x0016BC8B File Offset: 0x00169E8B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0016BC94 File Offset: 0x00169E94
		public override void Reset()
		{
			base.Reset();
			this.Pit = null;
			this.Object = null;
			this.DestinationCell = null;
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0016BCB4 File Offset: 0x00169EB4
		public static bool Check(GameObject Pit, GameObject Object, ref Cell DestinationCell)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BeforePullDown"))
			{
				Event @event = Event.New("BeforePullDown", 0, 0, 0);
				@event.SetParameter("Pit", Pit);
				@event.SetParameter("Object", Object);
				@event.SetParameter("DestinationCell", DestinationCell);
				flag = Object.FireEvent(@event);
				DestinationCell = @event.GetParameter<Cell>("DestinationCell");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(BeforePullDownEvent.ID, BeforePullDownEvent.CascadeLevel))
			{
				BeforePullDownEvent beforePullDownEvent = BeforePullDownEvent.FromPool();
				beforePullDownEvent.Pit = Pit;
				beforePullDownEvent.Object = Object;
				beforePullDownEvent.DestinationCell = DestinationCell;
				flag = Object.HandleEvent(beforePullDownEvent);
				DestinationCell = beforePullDownEvent.DestinationCell;
			}
			return flag;
		}

		// Token: 0x04002280 RID: 8832
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforePullDownEvent), null, new MinEvent.EventPoolCount(BeforePullDownEvent.CountPool), new MinEvent.EventPoolReset(BeforePullDownEvent.ResetPool));

		// Token: 0x04002281 RID: 8833
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002282 RID: 8834
		private static List<BeforePullDownEvent> Pool;

		// Token: 0x04002283 RID: 8835
		private static int PoolCounter;

		// Token: 0x04002284 RID: 8836
		public GameObject Pit;

		// Token: 0x04002285 RID: 8837
		public GameObject Object;

		// Token: 0x04002286 RID: 8838
		public Cell DestinationCell;
	}
}
