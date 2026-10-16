using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200087C RID: 2172
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 15)]
	public class WorshipPerformedEvent : MinEvent
	{
		// Token: 0x06005634 RID: 22068 RVA: 0x0019A8E7 File Offset: 0x00198AE7
		public WorshipPerformedEvent()
		{
			this.ID = WorshipPerformedEvent.ID;
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0019A8FA File Offset: 0x00198AFA
		public override int GetCascadeLevel()
		{
			return WorshipPerformedEvent.CascadeLevel;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0019A901 File Offset: 0x00198B01
		public static int CountPool()
		{
			if (WorshipPerformedEvent.Pool != null)
			{
				return WorshipPerformedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0019A916 File Offset: 0x00198B16
		public static void ResetPool()
		{
			while (WorshipPerformedEvent.PoolCounter > 0)
			{
				WorshipPerformedEvent.Pool[--WorshipPerformedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x0019A93E File Offset: 0x00198B3E
		public static void ResetTo(ref WorshipPerformedEvent E)
		{
			MinEvent.ResetTo<WorshipPerformedEvent>(E, WorshipPerformedEvent.Pool, ref WorshipPerformedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x0019A954 File Offset: 0x00198B54
		public static WorshipPerformedEvent FromPool()
		{
			return MinEvent.FromPool<WorshipPerformedEvent>(ref WorshipPerformedEvent.Pool, ref WorshipPerformedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x0019A96A File Offset: 0x00198B6A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x0019A973 File Offset: 0x00198B73
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Being = null;
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x0019A990 File Offset: 0x00198B90
		public static void Send(GameObject Actor, GameObject Object, Worshippable Being = null, Zone Zone = null)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return;
			}
			if (Being == null)
			{
				Being = Factions.FindWorshippable(Object);
			}
			if (Being != null)
			{
				if (Actor.IsPlayer())
				{
					if (GlobalConfig.GetBoolSetting("WorshipReputation", false))
					{
						The.Game.PlayerReputation.WorshipPerformed(Being);
					}
				}
				else if (Actor.IsCreature)
				{
					Actor.RequirePart<SacralTracking>(false);
				}
			}
			if (Zone == null)
			{
				Zone = Actor.CurrentZone;
			}
			bool flag = Zone != null && Zone.WantEvent(WorshipPerformedEvent.ID, WorshipPerformedEvent.CascadeLevel);
			bool flag2 = (!flag || Actor.GetCurrentZone() != Zone) && Actor.WantEvent(WorshipPerformedEvent.ID, WorshipPerformedEvent.CascadeLevel);
			bool flag3 = GameObject.Validate(ref Object) && (!flag || Object.GetCurrentZone() != Zone) && Object.WantEvent(WorshipPerformedEvent.ID, WorshipPerformedEvent.CascadeLevel);
			if (flag || flag2 || flag3)
			{
				WorshipPerformedEvent worshipPerformedEvent = WorshipPerformedEvent.FromPool();
				worshipPerformedEvent.Actor = Actor;
				worshipPerformedEvent.Object = Object;
				worshipPerformedEvent.Being = Being;
				if (flag)
				{
					Zone.HandleEvent(worshipPerformedEvent);
				}
				if (flag2)
				{
					Actor.HandleEvent(worshipPerformedEvent);
				}
				if (flag3)
				{
					Object.HandleEvent(worshipPerformedEvent);
				}
			}
		}

		// Token: 0x04002B6E RID: 11118
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(WorshipPerformedEvent), null, new MinEvent.EventPoolCount(WorshipPerformedEvent.CountPool), new MinEvent.EventPoolReset(WorshipPerformedEvent.ResetPool));

		// Token: 0x04002B6F RID: 11119
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002B70 RID: 11120
		private static List<WorshipPerformedEvent> Pool;

		// Token: 0x04002B71 RID: 11121
		private static int PoolCounter;

		// Token: 0x04002B72 RID: 11122
		public GameObject Actor;

		// Token: 0x04002B73 RID: 11123
		public GameObject Object;

		// Token: 0x04002B74 RID: 11124
		public Worshippable Being;
	}
}
