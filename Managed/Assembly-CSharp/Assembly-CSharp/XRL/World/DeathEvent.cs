using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006F4 RID: 1780
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 15)]
	public class DeathEvent : IDeathEvent
	{
		// Token: 0x06004A8B RID: 19083 RVA: 0x00174796 File Offset: 0x00172996
		public DeathEvent()
		{
			this.ID = DeathEvent.ID;
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x001747A9 File Offset: 0x001729A9
		public override int GetCascadeLevel()
		{
			return DeathEvent.CascadeLevel;
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x001747B0 File Offset: 0x001729B0
		public static int CountPool()
		{
			if (DeathEvent.Pool != null)
			{
				return DeathEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x001747C5 File Offset: 0x001729C5
		public static void ResetPool()
		{
			while (DeathEvent.PoolCounter > 0)
			{
				DeathEvent.Pool[--DeathEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x001747ED File Offset: 0x001729ED
		public static void ResetTo(ref DeathEvent E)
		{
			MinEvent.ResetTo<DeathEvent>(E, DeathEvent.Pool, ref DeathEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00174803 File Offset: 0x00172A03
		public static DeathEvent FromPool()
		{
			return MinEvent.FromPool<DeathEvent>(ref DeathEvent.Pool, ref DeathEvent.PoolCounter, 8192);
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00174819 File Offset: 0x00172A19
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x00174830 File Offset: 0x00172A30
		public static void Send(GameObject Dying, GameObject Killer, ref string Reason, ref string ThirdPersonReason, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null)
		{
			try
			{
				if (Killer != null)
				{
					DeathEvent deathEvent = DeathEvent.FromPool();
					deathEvent.Dying = Dying;
					deathEvent.Killer = Killer;
					deathEvent.Weapon = Weapon;
					deathEvent.Projectile = Projectile;
					deathEvent.Accidental = Accidental;
					deathEvent.AlwaysUsePopups = AlwaysUsePopups;
					deathEvent.KillerText = KillerText;
					deathEvent.Reason = Reason;
					deathEvent.ThirdPersonReason = ThirdPersonReason;
					Killer.HandleEvent(deathEvent);
					Reason = deathEvent.Reason;
					ThirdPersonReason = deathEvent.ThirdPersonReason;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Death MinEvent handling", ex);
			}
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x001748C4 File Offset: 0x00172AC4
		public static void Send(GameObject Dying, GameObject Killer, GameObject Weapon = null, GameObject Projectile = null, bool Accidental = false, bool AlwaysUsePopups = false, string KillerText = null, string Reason = null, string ThirdPersonReason = null)
		{
			DeathEvent.Send(Dying, Killer, ref Reason, ref ThirdPersonReason, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText);
		}

		// Token: 0x040023F2 RID: 9202
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(DeathEvent), null, new MinEvent.EventPoolCount(DeathEvent.CountPool), new MinEvent.EventPoolReset(DeathEvent.ResetPool));

		// Token: 0x040023F3 RID: 9203
		public new static readonly int CascadeLevel = 15;

		// Token: 0x040023F4 RID: 9204
		private static List<DeathEvent> Pool;

		// Token: 0x040023F5 RID: 9205
		private static int PoolCounter;
	}
}
