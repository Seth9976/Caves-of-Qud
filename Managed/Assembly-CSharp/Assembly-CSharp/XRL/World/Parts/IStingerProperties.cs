using System;
using System.Text;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200119C RID: 4508
	[Serializable]
	public abstract class IStingerProperties : IPart
	{
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600B34F RID: 45903 RVA: 0x004173F1 File Offset: 0x004155F1
		public virtual string SaveVs
		{
			get
			{
				return "Stinger Injected Poison";
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600B350 RID: 45904 RVA: 0x004173F8 File Offset: 0x004155F8
		public virtual string SaveAttribute
		{
			get
			{
				return "Toughness";
			}
		}

		// Token: 0x0600B351 RID: 45905 RVA: 0x004173FF File Offset: 0x004155FF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600B352 RID: 45906 RVA: 0x00417418 File Offset: 0x00415618
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary && E.Weapon == this.ParentObject)
			{
				E.SetFinalizedChance((E.Properties.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) || E.Properties.HasDelimitedSubstring(',', "Lunging", StringComparison.Ordinal) || E.Properties.HasDelimitedSubstring(',', "Flurrying", StringComparison.Ordinal)) ? 100 : 20);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B353 RID: 45907 RVA: 0x0041749C File Offset: 0x0041569C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B354 RID: 45908 RVA: 0x0041749F File Offset: 0x0041569F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
		}

		// Token: 0x0600B355 RID: 45909 RVA: 0x004174AC File Offset: 0x004156AC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				string stringParameter = E.GetStringParameter("Properties", "");
				if (this.Stinger != null || gameObjectParameter.TryGetPart<Stinger>(out this.Stinger))
				{
					int num = 20;
					if (!this.ParentObject.IsEquippedOrDefaultOfPrimary(gameObjectParameter) || stringParameter.HasDelimitedSubstring(',', "Stinging", StringComparison.Ordinal) || stringParameter.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) || stringParameter.HasDelimitedSubstring(',', "Lunging", StringComparison.Ordinal))
					{
						num = 100;
					}
					if (num.in100())
					{
						int level = this.Stinger.Level;
						this.ApplyPoison(this.Stinger, level, gameObjectParameter, gameObjectParameter2);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B356 RID: 45910
		public abstract Effect CreateEffect(GameObject Attacker, GameObject Defender, int Level);

		// Token: 0x0600B357 RID: 45911 RVA: 0x00417580 File Offset: 0x00415780
		public virtual void FailureMessage(GameObject Attacker, GameObject Defender, Effect Effect)
		{
			if (Attacker.IsPlayer() || Defender.IsPlayer())
			{
				IComponent<GameObject>.XDidY(Defender, "resist", "the effects of " + Attacker.poss("venom", true), "!", null, null, Defender, null, false, false, null, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x0600B358 RID: 45912 RVA: 0x004175D1 File Offset: 0x004157D1
		public virtual int GetCooldown(int Level)
		{
			return 25;
		}

		// Token: 0x0600B359 RID: 45913 RVA: 0x004175D8 File Offset: 0x004157D8
		public virtual string GetDamage(int Level)
		{
			string text;
			if (Level < 10)
			{
				if (Level >= 4)
				{
					if (Level >= 7)
					{
						text = "1d10";
					}
					else
					{
						text = "1d8";
					}
				}
				else
				{
					text = "1d6";
				}
			}
			else if (Level < 16)
			{
				if (Level >= 13)
				{
					text = "2d6";
				}
				else
				{
					text = "1d12";
				}
			}
			else if (Level >= 19)
			{
				text = "2d8";
			}
			else
			{
				text = "2d6+1";
			}
			return text;
		}

		// Token: 0x0600B35A RID: 45914 RVA: 0x0041763E File Offset: 0x0041583E
		public virtual int GetPenetration(int Level)
		{
			if (Level >= 2)
			{
				return Math.Min(9, (Level - 2) / 3 + 4);
			}
			return 3;
		}

		// Token: 0x0600B35B RID: 45915 RVA: 0x00417654 File Offset: 0x00415854
		public virtual string GetDuration(int Level)
		{
			return "";
		}

		// Token: 0x0600B35C RID: 45916 RVA: 0x0041765B File Offset: 0x0041585B
		public virtual string GetDescription()
		{
			return "You bear a tail with a stinger that delivers " + this.GetAdjective() + " venom to your enemies.";
		}

		// Token: 0x0600B35D RID: 45917 RVA: 0x00417672 File Offset: 0x00415872
		public virtual string GetAdjective()
		{
			return "poisonous";
		}

		// Token: 0x0600B35E RID: 45918 RVA: 0x00417679 File Offset: 0x00415879
		public virtual void SuccessMessage(GameObject Attacker, GameObject Defender, Effect Effect)
		{
		}

		// Token: 0x0600B35F RID: 45919 RVA: 0x0041767B File Offset: 0x0041587B
		public virtual void AppendLevelText(StringBuilder SB, int Level)
		{
		}

		// Token: 0x0600B360 RID: 45920 RVA: 0x0041767D File Offset: 0x0041587D
		public virtual void VisualEffect(GameObject Attacker, GameObject Defender)
		{
			Defender.Splatter("&g.");
		}

		// Token: 0x0600B361 RID: 45921 RVA: 0x0041768C File Offset: 0x0041588C
		public virtual void ApplyPoison(Stinger Mutation, int Level, GameObject Attacker, GameObject Defender)
		{
			if (Defender.HasStat(this.SaveAttribute) && Defender.FireEvent("CanApplyPoison") && CanApplyEffectEvent.Check(Defender, "Poison", 0))
			{
				Effect effect = this.CreateEffect(Attacker, Defender, Level);
				if (Defender.FireEvent("ApplyPoison"))
				{
					string saveAttribute = this.SaveAttribute;
					int save = Mutation.GetSave(Level);
					string saveVs = this.SaveVs;
					if (!Defender.MakeSave(saveAttribute, save, Attacker, null, saveVs, false, false, false, false, this.ParentObject) && ApplyEffectEvent.Check(Defender, "Poison", effect, this.ParentObject) && Defender.ApplyEffect(effect, this.ParentObject))
					{
						this.SuccessMessage(Attacker, Defender, effect);
						goto IL_00B0;
					}
				}
				this.FailureMessage(Attacker, Defender, effect);
			}
			IL_00B0:
			this.VisualEffect(Attacker, Defender);
		}

		// Token: 0x04004852 RID: 18514
		public const string BASE_SAVE_VS = "Stinger Injected Poison";

		// Token: 0x04004853 RID: 18515
		public const string BASE_SAVE_ATTR = "Toughness";

		// Token: 0x04004854 RID: 18516
		[NonSerialized]
		private Stinger Stinger;
	}
}
