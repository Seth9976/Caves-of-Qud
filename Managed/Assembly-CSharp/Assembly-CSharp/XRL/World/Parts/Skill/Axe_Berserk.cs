using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200131C RID: 4892
	[Serializable]
	public class Axe_Berserk : BaseSkill
	{
		// Token: 0x0600BF44 RID: 48964 RVA: 0x00460EDE File Offset: 0x0045F0DE
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", this.GetDuration(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Axe_Berserk.COOLDOWN, 0);
		}

		// Token: 0x0600BF45 RID: 48965 RVA: 0x00460F0C File Offset: 0x0045F10C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<CanRefreshAbilityEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BF46 RID: 48966 RVA: 0x00460F3A File Offset: 0x0045F13A
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF47 RID: 48967 RVA: 0x00460F60 File Offset: 0x0045F160
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.IsPrimaryAxeEquipped() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && base.ConTarget(E.Target) >= 1f)
			{
				E.Add("CommandAxeBerserk", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF48 RID: 48968 RVA: 0x00460FBC File Offset: 0x0045F1BC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF49 RID: 48969 RVA: 0x00460FDF File Offset: 0x0045F1DF
		public override bool HandleEvent(CanRefreshAbilityEvent E)
		{
			return (!(E.Ability.ID == this.ActivatedAbilityID) || !this.ParentObject.HasEffect(typeof(Berserk))) && base.HandleEvent(E);
		}

		// Token: 0x0600BF4A RID: 48970 RVA: 0x00461019 File Offset: 0x0045F219
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandAxeBerserk");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BF4B RID: 48971 RVA: 0x0046102E File Offset: 0x0045F22E
		public bool IsPrimaryAxeEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("Axe");
		}

		// Token: 0x0600BF4C RID: 48972 RVA: 0x00461040 File Offset: 0x0045F240
		public GameObject GetPrimaryAxe()
		{
			return this.ParentObject.GetPrimaryWeaponOfType("Axe");
		}

		// Token: 0x0600BF4D RID: 48973 RVA: 0x00461054 File Offset: 0x0045F254
		public int GetDuration()
		{
			int num = Axe_Berserk.DURATION;
			if (this.ParentObject.HasIntProperty("ImprovedBerserk"))
			{
				num += num * this.ParentObject.GetIntProperty("ImprovedBerserk", 0);
			}
			return num;
		}

		// Token: 0x0600BF4E RID: 48974 RVA: 0x00461090 File Offset: 0x0045F290
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandAxeBerserk")
			{
				if (!this.IsPrimaryAxeEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have an axe equipped in your primary hand to go berserk.", true, true, true);
					}
					return false;
				}
				Axe_Dismember part = this.ParentObject.GetPart<Axe_Dismember>();
				if (part != null && part.IsMyActivatedAbilityCoolingDown(part.ActivatedAbilityID, null))
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You can't go berserk until Dismember is off cooldown.", true, true, true);
					}
					return false;
				}
				IComponent<GameObject>.XDidY(this.ParentObject, "work", this.ParentObject.itself + " into a blood frenzy", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.ParticleText("&R!!!", false);
				this.ParentObject.ApplyEffect(new Berserk(this.GetDuration()), null);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Axe_Berserk.COOLDOWN, null, null, false);
				if (part != null)
				{
					part.CooldownMyActivatedAbility(part.ActivatedAbilityID, Axe_Dismember.Cooldown, null, null, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BF4F RID: 48975 RVA: 0x004611AC File Offset: 0x0045F3AC
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Berserk!", "CommandAxeBerserk", "Stances", null, "\u0001", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600BF50 RID: 48976 RVA: 0x004611EE File Offset: 0x0045F3EE
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D12 RID: 19730
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D13 RID: 19731
		public static readonly int COOLDOWN = 100;

		// Token: 0x04004D14 RID: 19732
		public static readonly int DURATION = 5;
	}
}
