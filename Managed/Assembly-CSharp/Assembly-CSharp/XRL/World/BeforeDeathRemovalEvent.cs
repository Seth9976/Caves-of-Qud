using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200068D RID: 1677
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeDeathRemovalEvent : IDeathEvent
	{
		// Token: 0x060047CA RID: 18378 RVA: 0x0016A742 File Offset: 0x00168942
		public BeforeDeathRemovalEvent()
		{
			this.ID = BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0016A755 File Offset: 0x00168955
		public static int CountPool()
		{
			if (BeforeDeathRemovalEvent.Pool != null)
			{
				return BeforeDeathRemovalEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x0016A76A File Offset: 0x0016896A
		public static void ResetPool()
		{
			while (BeforeDeathRemovalEvent.PoolCounter > 0)
			{
				BeforeDeathRemovalEvent.Pool[--BeforeDeathRemovalEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x0016A792 File Offset: 0x00168992
		public static void ResetTo(ref BeforeDeathRemovalEvent E)
		{
			MinEvent.ResetTo<BeforeDeathRemovalEvent>(E, BeforeDeathRemovalEvent.Pool, ref BeforeDeathRemovalEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x0016A7A8 File Offset: 0x001689A8
		public static BeforeDeathRemovalEvent FromPool()
		{
			return MinEvent.FromPool<BeforeDeathRemovalEvent>(ref BeforeDeathRemovalEvent.Pool, ref BeforeDeathRemovalEvent.PoolCounter, 8192);
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0016A7BE File Offset: 0x001689BE
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0016A7D4 File Offset: 0x001689D4
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.HasRegisteredEvent("BeforeDeathRemoval"))
				{
					Event @event = Event.New("BeforeDeathRemoval", 0, 0, 0);
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
				MetricsManager.LogError("BeforeDeathRemoval registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.WantEvent(BeforeDeathRemovalEvent.ID, MinEvent.CascadeLevel))
				{
					BeforeDeathRemovalEvent beforeDeathRemovalEvent = BeforeDeathRemovalEvent.FromPool();
					beforeDeathRemovalEvent.Dying = Dying;
					beforeDeathRemovalEvent.Killer = Killer;
					beforeDeathRemovalEvent.Weapon = Weapon;
					beforeDeathRemovalEvent.Projectile = Projectile;
					beforeDeathRemovalEvent.Accidental = Accidental;
					beforeDeathRemovalEvent.AlwaysUsePopups = AlwaysUsePopups;
					beforeDeathRemovalEvent.KillerText = KillerText;
					beforeDeathRemovalEvent.Reason = Reason;
					beforeDeathRemovalEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Dying.HandleEvent(beforeDeathRemovalEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("BeforeDeathRemoval MinEvent handling", ex2);
			}
		}

		// Token: 0x04002238 RID: 8760
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeDeathRemovalEvent), null, new MinEvent.EventPoolCount(BeforeDeathRemovalEvent.CountPool), new MinEvent.EventPoolReset(BeforeDeathRemovalEvent.ResetPool));

		// Token: 0x04002239 RID: 8761
		private static List<BeforeDeathRemovalEvent> Pool;

		// Token: 0x0400223A RID: 8762
		private static int PoolCounter;
	}
}
