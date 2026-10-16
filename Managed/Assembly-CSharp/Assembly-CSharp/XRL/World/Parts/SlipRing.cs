using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, save bonus and activation chance are
	/// increased by a percentage equal to ((power load - 100) / 10),
	/// i.e. 30% for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001098 RID: 4248
	public class SlipRing : IPoweredPart
	{
		// Token: 0x0600A910 RID: 43280 RVA: 0x003DD265 File Offset: 0x003DB465
		public SlipRing()
		{
			this.IsEMPSensitive = false;
			this.IsPowerLoadSensitive = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A911 RID: 43281 RVA: 0x003DD291 File Offset: 0x003DB491
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A912 RID: 43282 RVA: 0x003DD294 File Offset: 0x003DB494
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ChargeUse > 0 && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A913 RID: 43283 RVA: 0x003DD2D8 File Offset: 0x003DB4D8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600A914 RID: 43284 RVA: 0x003DD306 File Offset: 0x003DB506
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "DefenderBeforeHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A915 RID: 43285 RVA: 0x003DD320 File Offset: 0x003DB520
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "DefenderBeforeHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A916 RID: 43286 RVA: 0x003DD33C File Offset: 0x003DB53C
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Grab", E))
			{
				int num = this.MyPowerLoadLevel();
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3))
				{
					E.Roll += this.GetSaveBonus(num);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A917 RID: 43287 RVA: 0x003DD3A0 File Offset: 0x003DB5A0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int num = this.MyPowerLoadLevel();
			SavingThrows.AppendSaveBonusDescription(E.Postfix, this.GetSaveBonus(num), "Grab", false, true, true);
			E.Postfix.AppendRules(this.GetActivationChance(num).Signed(false) + "% chance to slip away from natural melee attacks");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A918 RID: 43288 RVA: 0x003DD3F8 File Offset: 0x003DB5F8
		public int GetSaveBonus(int PowerLoad = -2147483648)
		{
			int num = this.MyPowerLoadBonus(PowerLoad, 100, 10);
			if (num == 0)
			{
				return this.SaveBonus;
			}
			return this.SaveBonus * (100 + num) / 100;
		}

		// Token: 0x0600A919 RID: 43289 RVA: 0x003DD42C File Offset: 0x003DB62C
		public int GetActivationChance(int PowerLoad = -2147483648)
		{
			int num = this.MyPowerLoadBonus(PowerLoad, 100, 10);
			if (num == 0)
			{
				return this.ActivationChance;
			}
			return this.ActivationChance * (100 + num) / 100;
		}

		// Token: 0x0600A91A RID: 43290 RVA: 0x003DD460 File Offset: 0x003DB660
		public bool IsActiveFor(GameObject Weapon)
		{
			if (Weapon == null || !Weapon.IsNatural())
			{
				return false;
			}
			int num = this.MyPowerLoadLevel();
			if (this.GetActivationChance(num).in100())
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				int num2 = 1;
				int? num3 = new int?(num);
				return base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num2, null, false, 0L, num3);
			}
			return false;
		}

		// Token: 0x0600A91B RID: 43291 RVA: 0x003DD4B8 File Offset: 0x003DB6B8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DefenderBeforeHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				if (this.IsActiveFor(gameObjectParameter))
				{
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter3 = E.GetGameObjectParameter("Defender");
					if (gameObjectParameter2 != null && gameObjectParameter3 != null)
					{
						IComponent<GameObject>.XDidYToZ(gameObjectParameter3, "slip", "away from", gameObjectParameter2, gameObjectParameter.ShortDisplayName, "!", null, null, gameObjectParameter3, null, false, false, false, false, true, null, null, null, false, false, false, false, false, null);
						E.SetParameter("NoMissMessage", 1);
					}
					return false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040045CF RID: 17871
		public const string SAVE_BONUS_VS = "Grab";

		// Token: 0x040045D0 RID: 17872
		public int SaveBonus = 15;

		// Token: 0x040045D1 RID: 17873
		public int ActivationChance = 5;
	}
}
