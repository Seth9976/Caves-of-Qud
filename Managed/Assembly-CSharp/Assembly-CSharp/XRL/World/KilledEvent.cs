using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007FA RID: 2042
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class KilledEvent : IDeathEvent
	{
		// Token: 0x060051C5 RID: 20933 RVA: 0x0018AA80 File Offset: 0x00188C80
		public KilledEvent()
		{
			this.ID = KilledEvent.ID;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0018AA93 File Offset: 0x00188C93
		public static int CountPool()
		{
			if (KilledEvent.Pool != null)
			{
				return KilledEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060051C7 RID: 20935 RVA: 0x0018AAA8 File Offset: 0x00188CA8
		public static void ResetPool()
		{
			while (KilledEvent.PoolCounter > 0)
			{
				KilledEvent.Pool[--KilledEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060051C8 RID: 20936 RVA: 0x0018AAD0 File Offset: 0x00188CD0
		public static void ResetTo(ref KilledEvent E)
		{
			MinEvent.ResetTo<KilledEvent>(E, KilledEvent.Pool, ref KilledEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060051C9 RID: 20937 RVA: 0x0018AAE6 File Offset: 0x00188CE6
		public static KilledEvent FromPool()
		{
			return MinEvent.FromPool<KilledEvent>(ref KilledEvent.Pool, ref KilledEvent.PoolCounter, 8192);
		}

		// Token: 0x060051CA RID: 20938 RVA: 0x0018AAFC File Offset: 0x00188CFC
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060051CB RID: 20939 RVA: 0x0018AB10 File Offset: 0x00188D10
		public static void Send(GameObject Dying, GameObject Killer, ref string Reason, ref string ThirdPersonReason, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Killer) && Killer.HasRegisteredEvent("Killed"))
				{
					Event @event = Event.New("Killed", 0, 0, 0);
					@event.SetParameter("Object", Dying);
					@event.SetParameter("Killer", Killer);
					@event.SetParameter("Weapon", Weapon);
					@event.SetParameter("Projectile", Projectile);
					@event.SetParameter("KillerText", KillerText);
					@event.SetParameter("Reason", Reason);
					@event.SetParameter("ThirdPersonReason", ThirdPersonReason);
					@event.SetFlag("Accidental", Accidental);
					@event.SetFlag("AlwaysUsePopups", AlwaysUsePopups);
					flag = Killer.FireEvent(@event);
					Reason = @event.GetStringParameter("Reason", null);
					ThirdPersonReason = @event.GetStringParameter("ThirdPersonReason", null);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Killed registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Killer) && Killer.WantEvent(KilledEvent.ID, MinEvent.CascadeLevel))
				{
					KilledEvent killedEvent = KilledEvent.FromPool();
					killedEvent.Dying = Dying;
					killedEvent.Killer = Killer;
					killedEvent.Weapon = Weapon;
					killedEvent.Projectile = Projectile;
					killedEvent.Accidental = Accidental;
					killedEvent.AlwaysUsePopups = AlwaysUsePopups;
					killedEvent.KillerText = KillerText;
					killedEvent.Reason = Reason;
					killedEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Killer.HandleEvent(killedEvent);
					Reason = killedEvent.Reason;
					ThirdPersonReason = killedEvent.ThirdPersonReason;
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("Killed MinEvent handling", ex2);
			}
		}

		// Token: 0x060051CC RID: 20940 RVA: 0x0018ACB0 File Offset: 0x00188EB0
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			KilledEvent.Send(Dying, Killer, ref Reason, ref ThirdPersonReason, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText);
		}

		// Token: 0x04002910 RID: 10512
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(KilledEvent), null, new MinEvent.EventPoolCount(KilledEvent.CountPool), new MinEvent.EventPoolReset(KilledEvent.ResetPool));

		// Token: 0x04002911 RID: 10513
		private static List<KilledEvent> Pool;

		// Token: 0x04002912 RID: 10514
		private static int PoolCounter;
	}
}
