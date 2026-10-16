using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000819 RID: 2073
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class OnDeathRemovalEvent : IDeathEvent
	{
		// Token: 0x060052A6 RID: 21158 RVA: 0x0018D69B File Offset: 0x0018B89B
		public OnDeathRemovalEvent()
		{
			this.ID = OnDeathRemovalEvent.ID;
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0018D6AE File Offset: 0x0018B8AE
		public static int CountPool()
		{
			if (OnDeathRemovalEvent.Pool != null)
			{
				return OnDeathRemovalEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0018D6C3 File Offset: 0x0018B8C3
		public static void ResetPool()
		{
			while (OnDeathRemovalEvent.PoolCounter > 0)
			{
				OnDeathRemovalEvent.Pool[--OnDeathRemovalEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0018D6EB File Offset: 0x0018B8EB
		public static void ResetTo(ref OnDeathRemovalEvent E)
		{
			MinEvent.ResetTo<OnDeathRemovalEvent>(E, OnDeathRemovalEvent.Pool, ref OnDeathRemovalEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0018D701 File Offset: 0x0018B901
		public static OnDeathRemovalEvent FromPool()
		{
			return MinEvent.FromPool<OnDeathRemovalEvent>(ref OnDeathRemovalEvent.Pool, ref OnDeathRemovalEvent.PoolCounter, 8192);
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0018D717 File Offset: 0x0018B917
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0018D72C File Offset: 0x0018B92C
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			bool flag = true;
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.HasRegisteredEvent("OnDeathRemoval"))
				{
					Event @event = Event.New("OnDeathRemoval", 0, 0, 0);
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
				MetricsManager.LogError("OnDeathRemoval registered event handling", ex);
			}
			try
			{
				if (flag && GameObject.Validate(ref Dying) && Dying.WantEvent(OnDeathRemovalEvent.ID, MinEvent.CascadeLevel))
				{
					OnDeathRemovalEvent onDeathRemovalEvent = OnDeathRemovalEvent.FromPool();
					onDeathRemovalEvent.Dying = Dying;
					onDeathRemovalEvent.Killer = Killer;
					onDeathRemovalEvent.Weapon = Weapon;
					onDeathRemovalEvent.Projectile = Projectile;
					onDeathRemovalEvent.Accidental = Accidental;
					onDeathRemovalEvent.AlwaysUsePopups = AlwaysUsePopups;
					onDeathRemovalEvent.KillerText = KillerText;
					onDeathRemovalEvent.Reason = Reason;
					onDeathRemovalEvent.ThirdPersonReason = ThirdPersonReason;
					flag = Dying.HandleEvent(onDeathRemovalEvent);
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogError("OnDeathRemoval MinEvent handling", ex2);
			}
		}

		// Token: 0x04002989 RID: 10633
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(OnDeathRemovalEvent), null, new MinEvent.EventPoolCount(OnDeathRemovalEvent.CountPool), new MinEvent.EventPoolReset(OnDeathRemovalEvent.ResetPool));

		// Token: 0x0400298A RID: 10634
		private static List<OnDeathRemovalEvent> Pool;

		// Token: 0x0400298B RID: 10635
		private static int PoolCounter;
	}
}
