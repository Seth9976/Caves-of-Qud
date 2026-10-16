using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000690 RID: 1680
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class BeforeDieEvent : IDeathEvent
	{
		// Token: 0x060047DF RID: 18399 RVA: 0x0016AC3F File Offset: 0x00168E3F
		public BeforeDieEvent()
		{
			this.ID = BeforeDieEvent.ID;
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x0016AC52 File Offset: 0x00168E52
		public static int CountPool()
		{
			if (BeforeDieEvent.Pool != null)
			{
				return BeforeDieEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0016AC67 File Offset: 0x00168E67
		public static void ResetPool()
		{
			while (BeforeDieEvent.PoolCounter > 0)
			{
				BeforeDieEvent.Pool[--BeforeDieEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0016AC8F File Offset: 0x00168E8F
		public static void ResetTo(ref BeforeDieEvent E)
		{
			MinEvent.ResetTo<BeforeDieEvent>(E, BeforeDieEvent.Pool, ref BeforeDieEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x0016ACA5 File Offset: 0x00168EA5
		public static BeforeDieEvent FromPool()
		{
			return MinEvent.FromPool<BeforeDieEvent>(ref BeforeDieEvent.Pool, ref BeforeDieEvent.PoolCounter, 8192);
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0016ACBB File Offset: 0x00168EBB
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0016ACD0 File Offset: 0x00168ED0
		public static bool Check(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.HasRegisteredEvent("BeforeDie"))
				{
					Event @event = Event.New("BeforeDie", 0, 0, 0);
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
				MetricsManager.LogError("BeforeDie registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.WantEvent(BeforeDieEvent.ID, MinEvent.CascadeLevel))
				{
					BeforeDieEvent beforeDieEvent = BeforeDieEvent.FromPool();
					beforeDieEvent.Dying = Dying;
					beforeDieEvent.Killer = Killer;
					beforeDieEvent.Weapon = Weapon;
					beforeDieEvent.Projectile = Projectile;
					beforeDieEvent.Accidental = Accidental;
					beforeDieEvent.AlwaysUsePopups = AlwaysUsePopups;
					beforeDieEvent.KillerText = KillerText;
					beforeDieEvent.Reason = Reason;
					beforeDieEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Dying.HandleEvent(beforeDieEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("BeforeDie MinEvent handling", ex2);
			}
			return flag;
		}

		// Token: 0x04002242 RID: 8770
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(BeforeDieEvent), null, new MinEvent.EventPoolCount(BeforeDieEvent.CountPool), new MinEvent.EventPoolReset(BeforeDieEvent.ResetPool));

		// Token: 0x04002243 RID: 8771
		private static List<BeforeDieEvent> Pool;

		// Token: 0x04002244 RID: 8772
		private static int PoolCounter;
	}
}
