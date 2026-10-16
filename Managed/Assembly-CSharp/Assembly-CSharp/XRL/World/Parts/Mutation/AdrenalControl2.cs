using System;
using XRL.Core;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200142C RID: 5164
	[Serializable]
	public class AdrenalControl2 : BaseMutation
	{
		// Token: 0x0600C93A RID: 51514 RVA: 0x00494AB4 File Offset: 0x00492CB4
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Duration", this.GetQuicknessDuration(Level), false, 0);
			stats.Set("QuicknessBonus", this.GetQuicknessBonus(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("MutationBonus", this.GetMutationBonus(Level), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C93B RID: 51515 RVA: 0x00494B3C File Offset: 0x00492D3C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C93C RID: 51516 RVA: 0x00494B5A File Offset: 0x00492D5A
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ReleaseAdrenalineAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C93D RID: 51517 RVA: 0x00494B7E File Offset: 0x00492D7E
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 4 && base.IsMyActivatedAbilityAIUsable(this.ReleaseAdrenalineAbilityID, null))
			{
				E.Add("CommandReleaseAdrenaline", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C93E RID: 51518 RVA: 0x00494BB0 File Offset: 0x00492DB0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandReleaseAdrenaline");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C93F RID: 51519 RVA: 0x00494BD0 File Offset: 0x00492DD0
		public override string GetDescription()
		{
			return "You regulate your body's release of adrenaline.";
		}

		// Token: 0x0600C940 RID: 51520 RVA: 0x00494BD7 File Offset: 0x00492DD7
		public int GetQuicknessBonus(int Level)
		{
			return 9 + Level;
		}

		// Token: 0x0600C941 RID: 51521 RVA: 0x00494BDD File Offset: 0x00492DDD
		public int GetMutationBonus(int Level)
		{
			return Level / 3 + 1;
		}

		// Token: 0x0600C942 RID: 51522 RVA: 0x00494BE4 File Offset: 0x00492DE4
		public int GetQuicknessDuration(int Level)
		{
			return 20;
		}

		// Token: 0x0600C943 RID: 51523 RVA: 0x00494BE8 File Offset: 0x00492DE8
		public int GetCooldown(int Level)
		{
			return 200;
		}

		// Token: 0x0600C944 RID: 51524 RVA: 0x00494BF0 File Offset: 0x00492DF0
		public override string GetLevelText(int Level)
		{
			string text = "";
			text = text + "You can increase your body's adrenaline flow for " + this.GetQuicknessDuration(Level).ToString() + " rounds.\n";
			text = string.Concat(new string[]
			{
				text,
				"While it's flowing, you gain +{{C|",
				this.GetQuicknessBonus(Level).ToString(),
				"}} quickness and other physical mutations gain +{{C|",
				this.GetMutationBonus(Level).ToString(),
				"}} rank.\n"
			});
			return text + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C945 RID: 51525 RVA: 0x00494C90 File Offset: 0x00492E90
		public void SyncBonus()
		{
			int num = this.GetQuicknessBonus(base.Level);
			if (this.Duration <= 0)
			{
				num = 0;
			}
			if (this.ParentObject.HasStat("Speed") && num != this.CurrentBonus)
			{
				this.ParentObject.GetStat("Speed").Bonus -= this.CurrentBonus;
				this.CurrentBonus = num;
				this.ParentObject.GetStat("Speed").Bonus += this.CurrentBonus;
			}
			int num2 = this.GetMutationBonus(base.Level);
			if (this.Duration <= 0)
			{
				num2 = 0;
			}
			if (num2 != this.CurrentMLBonus)
			{
				this.ParentObject.ModIntProperty("AdrenalLevelModifier", -this.CurrentMLBonus, false);
				this.CurrentMLBonus = num2;
				this.ParentObject.ModIntProperty("AdrenalLevelModifier", this.CurrentMLBonus, false);
				SyncMutationLevelsEvent.Send(this.ParentObject);
			}
			if (num2 == 0 || num == 0)
			{
				this.ParentObject.RemoveEffect(typeof(AdrenalControl2Boosted));
				return;
			}
			AdrenalControl2Boosted adrenalControl2Boosted = this.ParentObject.GetEffect<AdrenalControl2Boosted>();
			if (adrenalControl2Boosted == null)
			{
				adrenalControl2Boosted = new AdrenalControl2Boosted(this);
				this.ParentObject.ApplyEffect(adrenalControl2Boosted, null);
			}
			adrenalControl2Boosted.AppliedMutationBonus = num2;
			adrenalControl2Boosted.AppliedSpeedBonus = num;
			adrenalControl2Boosted.Duration = this.Duration;
		}

		// Token: 0x0600C946 RID: 51526 RVA: 0x00494DDC File Offset: 0x00492FDC
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = 60 - this.Duration / 2;
				int num2 = XRLCore.CurrentFrame % num;
				if (num2 > 21 && num2 < 31)
				{
					E.Tile = null;
					E.RenderString = "\u0003";
					E.ColorString = "&r";
				}
			}
			return true;
		}

		// Token: 0x0600C947 RID: 51527 RVA: 0x00494E30 File Offset: 0x00493030
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Duration > 0)
				{
					this.Duration--;
					if (this.Duration == 0 && this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("Your adrenaline subsides.", null, true);
					}
				}
				this.SyncBonus();
			}
			else if (E.ID == "CommandReleaseAdrenaline")
			{
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("{{G|Your adrenaline starts to flow.}}", null, true);
				}
				this.Duration = this.GetQuicknessDuration(base.Level);
				base.CooldownMyActivatedAbility(this.ReleaseAdrenalineAbilityID, this.GetCooldown(base.Level), null, null, false);
				this.SyncBonus();
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C948 RID: 51528 RVA: 0x00494F30 File Offset: 0x00493130
		public override bool ChangeLevel(int NewLevel)
		{
			this.SyncBonus();
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C949 RID: 51529 RVA: 0x00494F40 File Offset: 0x00493140
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ReleaseAdrenalineAbilityID = base.AddMyActivatedAbility("Release Adrenaline", "CommandReleaseAdrenaline", "Physical Mutations", null, "å", null, false, false, false, false, false, false, false, false, true, true, true, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C94A RID: 51530 RVA: 0x00494F89 File Offset: 0x00493189
		public override bool Unmutate(GameObject GO)
		{
			this.Duration = 0;
			this.SyncBonus();
			base.RemoveMyActivatedAbility(ref this.ReleaseAdrenalineAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EAD RID: 20141
		public int CurrentBonus;

		// Token: 0x04004EAE RID: 20142
		public Guid ReleaseAdrenalineAbilityID = Guid.Empty;

		// Token: 0x04004EAF RID: 20143
		public int Duration;

		// Token: 0x04004EB0 RID: 20144
		public int CurrentMLBonus;
	}
}
