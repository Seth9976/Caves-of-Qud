using System;
using System.Text;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the percentage of extra damage done to
	/// diggable targets (base value of which is 100 / HitsRequired, so
	/// 25% by default), will increased by ((power load - 100) / 30), i.e.
	/// 10% for the standard overload power load of 400, and the effective
	/// PenetrationBonus will be increased by (power load - 100)%, i.e.
	/// 30% for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x0200101F RID: 4127
	[Serializable]
	public class Drill : IPoweredPart
	{
		// Token: 0x0600A458 RID: 42072 RVA: 0x003C1821 File Offset: 0x003BFA21
		public Drill()
		{
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A459 RID: 42073 RVA: 0x003C1848 File Offset: 0x003BFA48
		public override bool SameAs(IPart p)
		{
			Drill drill = p as Drill;
			return drill.PenetrationBonus == this.PenetrationBonus && drill.HitsRequired == this.HitsRequired && base.SameAs(p);
		}

		// Token: 0x0600A45A RID: 42074 RVA: 0x003C1883 File Offset: 0x003BFA83
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<PathAsBurrowerEvent>.ID;
		}

		// Token: 0x0600A45B RID: 42075 RVA: 0x003C18A4 File Offset: 0x003BFAA4
		public override bool HandleEvent(PathAsBurrowerEvent E)
		{
			return !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && base.HandleEvent(E);
		}

		// Token: 0x0600A45C RID: 42076 RVA: 0x003C18E4 File Offset: 0x003BFAE4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int effectivePenetrationBonus = this.GetEffectivePenetrationBonus();
			if (effectivePenetrationBonus != 0)
			{
				E.Postfix.AppendRules(((this.ChargeUse > 0) ? "When powered, " : "") + effectivePenetrationBonus.Signed(false) + " penetration vs. walls.", (this.ChargeUse > 0) ? new Action<StringBuilder>(base.AddStatusSummary) : null);
			}
			int wallHitsRequired = this.GetWallHitsRequired();
			if (wallHitsRequired > 0)
			{
				E.Postfix.AppendRules(((this.ChargeUse > 0) ? "When powered, destroys " : "Destroys ") + " walls after " + wallHitsRequired.Things("penetrating hit", null) + ".", (this.ChargeUse > 0) ? new Action<StringBuilder>(base.AddStatusSummary) : null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A45D RID: 42077 RVA: 0x003C19AB File Offset: 0x003BFBAB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A45E RID: 42078 RVA: 0x003C19AE File Offset: 0x003BFBAE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			Registrar.Register("GetWeaponHitDice");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A45F RID: 42079 RVA: 0x003C19D0 File Offset: 0x003BFBD0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GetWeaponHitDice")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && gameObjectParameter.IsDiggable() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					E.SetParameter("PenetrationBonus", E.GetIntParameter("PenetrationBonus", 0) + this.GetEffectivePenetrationBonus());
				}
			}
			else if (E.ID == "WeaponDealDamage")
			{
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter2 != null && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					if (!this.Sound.IsNullOrEmpty())
					{
						gameObjectParameter2.PlayWorldSound(this.Sound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					if (gameObjectParameter2.IsDiggable())
					{
						double wallBonusPercentage = this.GetWallBonusPercentage();
						if (wallBonusPercentage > 0.0)
						{
							Damage damage = E.GetParameter("Damage") as Damage;
							int num = (int)Math.Floor((double)gameObjectParameter2.baseHitpoints * wallBonusPercentage / 100.0);
							if (num >= damage.Amount)
							{
								damage.Amount = num;
								CombatJuice.playPrefabAnimation(gameObjectParameter2, "Impacts/ImpactVFXDig", null, null, null, false);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A460 RID: 42080 RVA: 0x003C1B58 File Offset: 0x003BFD58
		public double GetWallBonusPercentage()
		{
			double num = 100.0 / (double)this.HitsRequired + (double)this.MyPowerLoadBonus(int.MinValue, 100, 30);
			if (this.ParentObject.IsGiganticEquipment)
			{
				num *= 2.0;
			}
			return num;
		}

		// Token: 0x0600A461 RID: 42081 RVA: 0x003C1BA2 File Offset: 0x003BFDA2
		public int GetWallHitsRequired()
		{
			return Drill.GetWallHitsRequired(this.GetWallBonusPercentage());
		}

		// Token: 0x0600A462 RID: 42082 RVA: 0x003C1BAF File Offset: 0x003BFDAF
		public static int GetWallHitsRequired(double Percentage)
		{
			if (Percentage <= 0.0)
			{
				return 0;
			}
			return Math.Max((int)Math.Ceiling(100.0 / Percentage), 1);
		}

		// Token: 0x0600A463 RID: 42083 RVA: 0x003C1BD6 File Offset: 0x003BFDD6
		public static int GetWallHitsRequired(int Percentage)
		{
			return Drill.GetWallHitsRequired((double)((float)Percentage));
		}

		// Token: 0x0600A464 RID: 42084 RVA: 0x003C1BE0 File Offset: 0x003BFDE0
		public int GetEffectivePenetrationBonus()
		{
			int num = this.PenetrationBonus;
			int num2 = this.MyPowerLoadBonus(int.MinValue, 100, 10);
			if (num2 != 0)
			{
				num = (int)Math.Round((double)num * (100.0 + (double)num2) / 100.0);
			}
			return num;
		}

		// Token: 0x04004422 RID: 17442
		public int PenetrationBonus = 24;

		// Token: 0x04004423 RID: 17443
		public int HitsRequired = 4;

		// Token: 0x04004424 RID: 17444
		public string Sound;
	}
}
