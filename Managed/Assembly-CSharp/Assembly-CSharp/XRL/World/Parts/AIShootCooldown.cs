using System;

namespace XRL.World.Parts
{
	// Token: 0x020010FE RID: 4350
	[Serializable]
	public class AIShootCooldown : AIBehaviorPart
	{
		// Token: 0x0600AC1C RID: 44060 RVA: 0x003F15CF File Offset: 0x003EF7CF
		public override bool SameAs(IPart p)
		{
			return !((p as AIShootCooldown).Cooldown != this.Cooldown) && base.SameAs(p);
		}

		// Token: 0x0600AC1D RID: 44061 RVA: 0x003F15F2 File Offset: 0x003EF7F2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIAfterMissileEvent>.ID || ID == PooledEvent<AIWantUseWeaponEvent>.ID;
		}

		// Token: 0x0600AC1E RID: 44062 RVA: 0x003F1610 File Offset: 0x003EF810
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return !this.CooldownActive() && base.HandleEvent(E);
		}

		// Token: 0x0600AC1F RID: 44063 RVA: 0x003F1623 File Offset: 0x003EF823
		public override bool HandleEvent(AIAfterMissileEvent E)
		{
			this.LastFireTimeTick = The.Game.TimeTicks;
			this.CurrentCooldown = Math.Max(this.CurrentCooldown, this.Cooldown.RollCached());
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC20 RID: 44064 RVA: 0x003F1658 File Offset: 0x003EF858
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC21 RID: 44065 RVA: 0x003F165B File Offset: 0x003EF85B
		public bool CooldownActive()
		{
			return this.LastFireTimeTick != The.Game.TimeTicks && this.LastFireTimeTick + (long)this.CurrentCooldown > The.Game.TimeTicks;
		}

		// Token: 0x0400473F RID: 18239
		public string Cooldown = "1d6";

		// Token: 0x04004740 RID: 18240
		public int CurrentCooldown;

		// Token: 0x04004741 RID: 18241
		public long LastFireTimeTick;
	}
}
