using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006AE RID: 1710
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 15)]
	public class BlasphemyPerformedEvent : MinEvent
	{
		// Token: 0x060048BB RID: 18619 RVA: 0x0016D347 File Offset: 0x0016B547
		public BlasphemyPerformedEvent()
		{
			this.ID = BlasphemyPerformedEvent.ID;
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0016D35A File Offset: 0x0016B55A
		public override int GetCascadeLevel()
		{
			return BlasphemyPerformedEvent.CascadeLevel;
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0016D361 File Offset: 0x0016B561
		public static int CountPool()
		{
			if (BlasphemyPerformedEvent.Pool != null)
			{
				return BlasphemyPerformedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0016D376 File Offset: 0x0016B576
		public static void ResetPool()
		{
			while (BlasphemyPerformedEvent.PoolCounter > 0)
			{
				BlasphemyPerformedEvent.Pool[--BlasphemyPerformedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0016D39E File Offset: 0x0016B59E
		public static void ResetTo(ref BlasphemyPerformedEvent E)
		{
			MinEvent.ResetTo<BlasphemyPerformedEvent>(E, BlasphemyPerformedEvent.Pool, ref BlasphemyPerformedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0016D3B4 File Offset: 0x0016B5B4
		public static BlasphemyPerformedEvent FromPool()
		{
			return MinEvent.FromPool<BlasphemyPerformedEvent>(ref BlasphemyPerformedEvent.Pool, ref BlasphemyPerformedEvent.PoolCounter, 8192);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0016D3CA File Offset: 0x0016B5CA
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0016D3D3 File Offset: 0x0016B5D3
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Being = null;
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0016D3F0 File Offset: 0x0016B5F0
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
						The.Game.PlayerReputation.BlasphemyPerformed(Being);
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
			bool flag = Zone != null && Zone.WantEvent(BlasphemyPerformedEvent.ID, BlasphemyPerformedEvent.CascadeLevel);
			bool flag2 = (!flag || Actor.GetCurrentZone() != Zone) && Actor.WantEvent(BlasphemyPerformedEvent.ID, BlasphemyPerformedEvent.CascadeLevel);
			bool flag3 = GameObject.Validate(ref Object) && (!flag || Object.GetCurrentZone() != Zone) && Object.WantEvent(BlasphemyPerformedEvent.ID, BlasphemyPerformedEvent.CascadeLevel);
			if (flag || flag2 || flag3)
			{
				BlasphemyPerformedEvent blasphemyPerformedEvent = BlasphemyPerformedEvent.FromPool();
				blasphemyPerformedEvent.Actor = Actor;
				blasphemyPerformedEvent.Object = Object;
				blasphemyPerformedEvent.Being = Being;
				if (flag)
				{
					Zone.HandleEvent(blasphemyPerformedEvent);
				}
				if (flag2)
				{
					Actor.HandleEvent(blasphemyPerformedEvent);
				}
				if (flag3)
				{
					Object.HandleEvent(blasphemyPerformedEvent);
				}
			}
		}

		// Token: 0x040022DC RID: 8924
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BlasphemyPerformedEvent), null, new MinEvent.EventPoolCount(BlasphemyPerformedEvent.CountPool), new MinEvent.EventPoolReset(BlasphemyPerformedEvent.ResetPool));

		// Token: 0x040022DD RID: 8925
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040022DE RID: 8926
		private static List<BlasphemyPerformedEvent> Pool;

		// Token: 0x040022DF RID: 8927
		private static int PoolCounter;

		// Token: 0x040022E0 RID: 8928
		public GameObject Actor;

		// Token: 0x040022E1 RID: 8929
		public GameObject Object;

		// Token: 0x040022E2 RID: 8930
		public Worshippable Being;
	}
}
