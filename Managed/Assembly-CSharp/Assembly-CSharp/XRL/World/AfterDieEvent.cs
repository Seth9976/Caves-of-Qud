using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200065B RID: 1627
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterDieEvent : IDeathEvent
	{
		// Token: 0x06004656 RID: 18006 RVA: 0x00166439 File Offset: 0x00164639
		public AfterDieEvent()
		{
			this.ID = AfterDieEvent.ID;
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x0016644C File Offset: 0x0016464C
		public static int CountPool()
		{
			if (AfterDieEvent.Pool != null)
			{
				return AfterDieEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x00166461 File Offset: 0x00164661
		public static void ResetPool()
		{
			while (AfterDieEvent.PoolCounter > 0)
			{
				AfterDieEvent.Pool[--AfterDieEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x00166489 File Offset: 0x00164689
		public static void ResetTo(ref AfterDieEvent E)
		{
			MinEvent.ResetTo<AfterDieEvent>(E, AfterDieEvent.Pool, ref AfterDieEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0016649F File Offset: 0x0016469F
		public static AfterDieEvent FromPool()
		{
			return MinEvent.FromPool<AfterDieEvent>(ref AfterDieEvent.Pool, ref AfterDieEvent.PoolCounter, 8192);
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x001664B5 File Offset: 0x001646B5
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x001664CC File Offset: 0x001646CC
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.HasRegisteredEvent("AfterDie"))
				{
					Event @event = Event.New("AfterDie", 0, 0, 0);
					@event.SetParameter("Dying", Dying);
					@event.SetParameter("Killer", Killer);
					@event.SetParameter("Weapon", Weapon);
					@event.SetParameter("Projectile", Projectile);
					@event.SetParameter("KillerText", KillerText);
					@event.SetParameter("Reason", Reason);
					@event.SetParameter("ThirdPersonReason", ThirdPersonReason);
					@event.SetFlag("Accidental", Accidental);
					@event.SetFlag("AlwaysUsePopups", AlwaysUsePopups);
					flag = Dying.FireEvent(@event);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("AfterDie registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.WantEvent(AfterDieEvent.ID, MinEvent.CascadeLevel))
				{
					AfterDieEvent afterDieEvent = AfterDieEvent.FromPool();
					afterDieEvent.Dying = Dying;
					afterDieEvent.Killer = Killer;
					afterDieEvent.Weapon = Weapon;
					afterDieEvent.Projectile = Projectile;
					afterDieEvent.Accidental = Accidental;
					afterDieEvent.AlwaysUsePopups = AlwaysUsePopups;
					afterDieEvent.KillerText = KillerText;
					afterDieEvent.Reason = Reason;
					afterDieEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Dying.HandleEvent(afterDieEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("AfterDie MinEvent handling", ex2);
			}
		}

		// Token: 0x04002165 RID: 8549
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterDieEvent), null, new MinEvent.EventPoolCount(AfterDieEvent.CountPool), new MinEvent.EventPoolReset(AfterDieEvent.ResetPool));

		// Token: 0x04002166 RID: 8550
		private static List<AfterDieEvent> Pool;

		// Token: 0x04002167 RID: 8551
		private static int PoolCounter;
	}
}
