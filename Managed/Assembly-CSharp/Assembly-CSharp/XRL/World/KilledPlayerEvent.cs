using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007FB RID: 2043
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class KilledPlayerEvent : IDeathEvent
	{
		// Token: 0x060051CE RID: 20942 RVA: 0x0018ACFF File Offset: 0x00188EFF
		public KilledPlayerEvent()
		{
			this.ID = KilledPlayerEvent.ID;
		}

		// Token: 0x060051CF RID: 20943 RVA: 0x0018AD12 File Offset: 0x00188F12
		public static int CountPool()
		{
			if (KilledPlayerEvent.Pool != null)
			{
				return KilledPlayerEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060051D0 RID: 20944 RVA: 0x0018AD27 File Offset: 0x00188F27
		public static void ResetPool()
		{
			while (KilledPlayerEvent.PoolCounter > 0)
			{
				KilledPlayerEvent.Pool[--KilledPlayerEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060051D1 RID: 20945 RVA: 0x0018AD4F File Offset: 0x00188F4F
		public static void ResetTo(ref KilledPlayerEvent E)
		{
			MinEvent.ResetTo<KilledPlayerEvent>(E, KilledPlayerEvent.Pool, ref KilledPlayerEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060051D2 RID: 20946 RVA: 0x0018AD65 File Offset: 0x00188F65
		public static KilledPlayerEvent FromPool()
		{
			return MinEvent.FromPool<KilledPlayerEvent>(ref KilledPlayerEvent.Pool, ref KilledPlayerEvent.PoolCounter, 8192);
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0018AD7B File Offset: 0x00188F7B
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060051D4 RID: 20948 RVA: 0x0018AD90 File Offset: 0x00188F90
		public static void Send(GameObject Dying, GameObject Killer, ref string Reason, ref string ThirdPersonReason, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Killer) && Dying.HasRegisteredEvent("KilledPlayer"))
				{
					Event @event = Event.New("KilledPlayer", 0, 0, 0);
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
				MetricsManager.LogError("KilledPlayer registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Killer) && Dying.WantEvent(KilledPlayerEvent.ID, MinEvent.CascadeLevel))
				{
					KilledPlayerEvent killedPlayerEvent = KilledPlayerEvent.FromPool();
					killedPlayerEvent.Dying = Dying;
					killedPlayerEvent.Killer = Killer;
					killedPlayerEvent.Weapon = Weapon;
					killedPlayerEvent.Projectile = Projectile;
					killedPlayerEvent.Accidental = Accidental;
					killedPlayerEvent.AlwaysUsePopups = AlwaysUsePopups;
					killedPlayerEvent.KillerText = KillerText;
					killedPlayerEvent.Reason = Reason;
					killedPlayerEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Killer.HandleEvent(killedPlayerEvent);
					Reason = killedPlayerEvent.Reason;
					ThirdPersonReason = killedPlayerEvent.ThirdPersonReason;
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("KilledPlayer MinEvent handling", ex2);
			}
		}

		// Token: 0x060051D5 RID: 20949 RVA: 0x0018AF30 File Offset: 0x00189130
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			KilledPlayerEvent.Send(Dying, Killer, ref Reason, ref ThirdPersonReason, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText);
		}

		// Token: 0x04002913 RID: 10515
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(KilledPlayerEvent), null, new MinEvent.EventPoolCount(KilledPlayerEvent.CountPool), new MinEvent.EventPoolReset(KilledPlayerEvent.ResetPool));

		// Token: 0x04002914 RID: 10516
		private static List<KilledPlayerEvent> Pool;

		// Token: 0x04002915 RID: 10517
		private static int PoolCounter;
	}
}
