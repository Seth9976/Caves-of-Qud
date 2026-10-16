using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006FE RID: 1790
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EarlyBeforeDeathRemovalEvent : IDeathEvent
	{
		// Token: 0x06004AD9 RID: 19161 RVA: 0x001754CD File Offset: 0x001736CD
		public EarlyBeforeDeathRemovalEvent()
		{
			this.ID = EarlyBeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x001754E0 File Offset: 0x001736E0
		public static int CountPool()
		{
			if (EarlyBeforeDeathRemovalEvent.Pool != null)
			{
				return EarlyBeforeDeathRemovalEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x001754F5 File Offset: 0x001736F5
		public static void ResetPool()
		{
			while (EarlyBeforeDeathRemovalEvent.PoolCounter > 0)
			{
				EarlyBeforeDeathRemovalEvent.Pool[--EarlyBeforeDeathRemovalEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0017551D File Offset: 0x0017371D
		public static void ResetTo(ref EarlyBeforeDeathRemovalEvent E)
		{
			MinEvent.ResetTo<EarlyBeforeDeathRemovalEvent>(E, EarlyBeforeDeathRemovalEvent.Pool, ref EarlyBeforeDeathRemovalEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00175533 File Offset: 0x00173733
		public static EarlyBeforeDeathRemovalEvent FromPool()
		{
			return MinEvent.FromPool<EarlyBeforeDeathRemovalEvent>(ref EarlyBeforeDeathRemovalEvent.Pool, ref EarlyBeforeDeathRemovalEvent.PoolCounter, 8192);
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00175549 File Offset: 0x00173749
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00175560 File Offset: 0x00173760
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.HasRegisteredEvent("EarlyBeforeDeathRemoval"))
				{
					Event @event = Event.New("EarlyBeforeDeathRemoval", 0, 0, 0);
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
				MetricsManager.LogError("EarlyBeforeDeathRemoval registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.WantEvent(EarlyBeforeDeathRemovalEvent.ID, MinEvent.CascadeLevel))
				{
					EarlyBeforeDeathRemovalEvent earlyBeforeDeathRemovalEvent = EarlyBeforeDeathRemovalEvent.FromPool();
					earlyBeforeDeathRemovalEvent.Dying = Dying;
					earlyBeforeDeathRemovalEvent.Killer = Killer;
					earlyBeforeDeathRemovalEvent.Weapon = Weapon;
					earlyBeforeDeathRemovalEvent.Projectile = Projectile;
					earlyBeforeDeathRemovalEvent.Accidental = Accidental;
					earlyBeforeDeathRemovalEvent.AlwaysUsePopups = AlwaysUsePopups;
					earlyBeforeDeathRemovalEvent.KillerText = KillerText;
					earlyBeforeDeathRemovalEvent.Reason = Reason;
					earlyBeforeDeathRemovalEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Dying.HandleEvent(earlyBeforeDeathRemovalEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("EarlyBeforeDeathRemoval MinEvent handling", ex2);
			}
		}

		// Token: 0x04002427 RID: 9255
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EarlyBeforeDeathRemovalEvent), null, new MinEvent.EventPoolCount(EarlyBeforeDeathRemovalEvent.CountPool), new MinEvent.EventPoolReset(EarlyBeforeDeathRemovalEvent.ResetPool));

		// Token: 0x04002428 RID: 9256
		private static List<EarlyBeforeDeathRemovalEvent> Pool;

		// Token: 0x04002429 RID: 9257
		private static int PoolCounter;
	}
}
