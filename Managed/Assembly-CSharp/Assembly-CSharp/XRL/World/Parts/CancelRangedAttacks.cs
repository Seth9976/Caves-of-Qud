using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the chance to activate will, if less than 100,
	/// be increased by a relative percentage of ((power load - 100) / 10),
	/// i.e. 30% for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001006 RID: 4102
	[Serializable]
	public class CancelRangedAttacks : IPoweredPart
	{
		// Token: 0x0600A37A RID: 41850 RVA: 0x003BBE01 File Offset: 0x003BA001
		public CancelRangedAttacks()
		{
			this.IsPowerLoadSensitive = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A37B RID: 41851 RVA: 0x003BBE2C File Offset: 0x003BA02C
		public override bool SameAs(IPart p)
		{
			CancelRangedAttacks cancelRangedAttacks = p as CancelRangedAttacks;
			return cancelRangedAttacks.Chance == this.Chance && !(cancelRangedAttacks.Message != this.Message) && !(cancelRangedAttacks.Sound != this.Sound) && cancelRangedAttacks.UseChargeOnAttempt == this.UseChargeOnAttempt && cancelRangedAttacks.UseChargeOnSuccess == this.UseChargeOnSuccess && cancelRangedAttacks.DestroyProjectiles == this.DestroyProjectiles && cancelRangedAttacks.ShowInShortDescription == this.ShowInShortDescription && cancelRangedAttacks.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600A37C RID: 41852 RVA: 0x003BBED1 File Offset: 0x003BA0D1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<DefenderMissileHitEvent>.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription);
		}

		// Token: 0x0600A37D RID: 41853 RVA: 0x003BBEF8 File Offset: 0x003BA0F8
		public override bool HandleEvent(DefenderMissileHitEvent E)
		{
			if (base.IsReady(this.UseChargeOnAttempt, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.GetEffectiveChance().in100())
			{
				if (this.UseChargeOnSuccess)
				{
					base.ConsumeCharge(null, null);
				}
				if (!this.Message.IsNullOrEmpty())
				{
					string text = GameText.VariableReplace(this.Message, this.ParentObject, E.Projectile, false);
					if (!text.IsNullOrEmpty())
					{
						base.EmitMessage(text, ' ', false, false, false, null, null);
					}
				}
				base.PlayWorldSound(this.Sound, 1f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				E.Done = true;
				if (this.DestroyProjectiles)
				{
					GameObject projectile = E.Projectile;
					if (projectile != null)
					{
						projectile.Obliterate(null, false, null);
					}
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A37E RID: 41854 RVA: 0x003BBFFC File Offset: 0x003BA1FC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				string text = ((this.IsPowerSwitchSensitive && this.ParentObject.HasPart<PowerSwitch>()) ? "When activated, " : "");
				if (this.Chance > 0)
				{
					E.Postfix.AppendRules(text + this.GetEffectiveChance().ToString() + "% chance of stopping ranged attacks.", base.GetEventSensitiveAddStatusSummary(E));
				}
				if (this.ComputePowerFactor > 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
				}
				else if (this.ComputePowerFactor < 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice decreases this item's effectiveness.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A37F RID: 41855 RVA: 0x003BC0B0 File Offset: 0x003BA2B0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A380 RID: 41856 RVA: 0x003BC0B4 File Offset: 0x003BA2B4
		public int GetEffectiveChance()
		{
			int num = this.Chance;
			if (num < 100 && this.IsPowerLoadSensitive)
			{
				num = num * (100 + this.MyPowerLoadBonus(int.MinValue, 100, 10)) / 100;
			}
			return GetAvailableComputePowerEvent.AdjustUp(this, num, this.ComputePowerFactor);
		}

		// Token: 0x040043B7 RID: 17335
		public int Chance;

		// Token: 0x040043B8 RID: 17336
		public string Message;

		// Token: 0x040043B9 RID: 17337
		public string Sound;

		// Token: 0x040043BA RID: 17338
		public bool UseChargeOnAttempt = true;

		// Token: 0x040043BB RID: 17339
		public bool UseChargeOnSuccess;

		// Token: 0x040043BC RID: 17340
		public bool DestroyProjectiles = true;

		// Token: 0x040043BD RID: 17341
		public bool ShowInShortDescription = true;

		// Token: 0x040043BE RID: 17342
		public float ComputePowerFactor;
	}
}
