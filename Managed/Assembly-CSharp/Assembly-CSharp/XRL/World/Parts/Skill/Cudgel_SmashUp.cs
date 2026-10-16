using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001336 RID: 4918
	[Serializable]
	public class Cudgel_SmashUp : BaseSkill
	{
		// Token: 0x0600C026 RID: 49190 RVA: 0x00466F4F File Offset: 0x0046514F
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", Cudgel_SmashUp.DURATION, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Cudgel_SmashUp.COOLDOWN, 0);
		}

		// Token: 0x0600C027 RID: 49191 RVA: 0x00466F7C File Offset: 0x0046517C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CanRefreshAbilityEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C028 RID: 49192 RVA: 0x00466FA2 File Offset: 0x004651A2
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C029 RID: 49193 RVA: 0x00466FC8 File Offset: 0x004651C8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && GameObject.Validate(E.Target) && this.IsPrimaryCudgelEquipped() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandSmashUp", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C02A RID: 49194 RVA: 0x0046701A File Offset: 0x0046521A
		public override bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return (!(E.Ability.ID == this.ActivatedAbilityID) || !this.ParentObject.HasEffect(typeof(Cudgel_SmashingUp))) && base.HandleEvent(E);
		}

		// Token: 0x0600C02B RID: 49195 RVA: 0x00467054 File Offset: 0x00465254
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("CommandSmashUp");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C02C RID: 49196 RVA: 0x00467074 File Offset: 0x00465274
		public bool IsPrimaryCudgelEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("Cudgel");
		}

		// Token: 0x0600C02D RID: 49197 RVA: 0x00467086 File Offset: 0x00465286
		public GameObject GetPrimaryCudgel()
		{
			return this.ParentObject.GetPrimaryWeaponOfType("Cudgel");
		}

		// Token: 0x0600C02E RID: 49198 RVA: 0x00467098 File Offset: 0x00465298
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSmashUp")
			{
				if (!this.IsPrimaryCudgelEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have a cudgel equipped in your primary hand to demolish things.", true, true, true);
					}
					return false;
				}
				Cudgel_Slam part = this.ParentObject.GetPart<Cudgel_Slam>();
				if (part != null && part.IsMyActivatedAbilityCoolingDown(part.ActivatedAbilityID, null))
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You can't Demolish until Slam is off cooldown.", true, true, true);
					}
					return false;
				}
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("{{G|You prepare " + this.ParentObject.itself + " for demolition.}}", null, true);
					IComponent<GameObject>.ThePlayer.ParticleText("&R!!!", false);
				}
				int num = Cudgel_SmashUp.DURATION;
				if (this.ParentObject.HasIntProperty("ImprovedSmashUp"))
				{
					num += num * this.ParentObject.GetIntProperty("ImprovedSmashUp", 0);
				}
				this.ParentObject.ApplyEffect(new Cudgel_SmashingUp(num), null);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Cudgel_SmashUp.COOLDOWN, null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C02F RID: 49199 RVA: 0x004671B4 File Offset: 0x004653B4
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Demolish", "CommandSmashUp", "Stances", null, "\u001e", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C030 RID: 49200 RVA: 0x004671F6 File Offset: 0x004653F6
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D44 RID: 19780
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D45 RID: 19781
		public static readonly int COOLDOWN = 100;

		// Token: 0x04004D46 RID: 19782
		public static readonly int DURATION = 5;
	}
}
