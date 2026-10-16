using System;
using XRL.World.Conversations;
using XRL.World.Conversations.Parts;
using XRL.World.Skills;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001323 RID: 4899
	[Serializable]
	public class BaseSkill : IPart
	{
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600BF97 RID: 49047 RVA: 0x004632C0 File Offset: 0x004614C0
		// (set) Token: 0x0600BF98 RID: 49048 RVA: 0x00463304 File Offset: 0x00461504
		public string DisplayName
		{
			get
			{
				IBaseSkillEntry entry = this.Entry;
				string text = ((entry != null) ? entry.Name : null);
				if (text != null)
				{
					return text;
				}
				text = base.Name;
				int num = text.LastIndexOf('_');
				if (num != -1)
				{
					text = text.Substring(num + 1);
				}
				return text;
			}
			set
			{
				if (value == this.DisplayName)
				{
					MetricsManager.LogCallingModError("You do not need to set the display name of the skill " + this.DisplayName + ", please remove the attempt to set it");
					return;
				}
				MetricsManager.LogCallingModError(string.Concat(new string[] { "You cannot set the display name of the skill ", this.DisplayName, " to ", value, ", please remove the attempt to set it" }));
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600BF99 RID: 49049 RVA: 0x00463370 File Offset: 0x00461570
		public IBaseSkillEntry Entry
		{
			get
			{
				IBaseSkillEntry baseSkillEntry;
				if ((baseSkillEntry = this._Entry) == null)
				{
					baseSkillEntry = (this._Entry = SkillFactory.Factory.GetFirstEntry(base.Name, null));
				}
				return baseSkillEntry;
			}
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF9A RID: 49050 RVA: 0x004633A1 File Offset: 0x004615A1
		public virtual string GetDescription(IBaseSkillEntry Entry)
		{
			return Entry.Description;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF9B RID: 49051 RVA: 0x004633A9 File Offset: 0x004615A9
		public virtual bool MeetsRequirements(GameObject Object, bool ShowPopup)
		{
			return true;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF9C RID: 49052 RVA: 0x004633AC File Offset: 0x004615AC
		public virtual bool IsValidForElement(IConversationElement Element, IConversationPart Part, string Context)
		{
			WaterRitualLearnSkill waterRitualLearnSkill = Part as WaterRitualLearnSkill;
			if (waterRitualLearnSkill != null)
			{
				waterRitualLearnSkill.Text = this.GetWaterRitualText(waterRitualLearnSkill.Entry);
				waterRitualLearnSkill.TagName = this.ShowWaterRitualTagName(waterRitualLearnSkill.Entry);
				waterRitualLearnSkill.Reputation = this.GetWaterRitualReputationCost(waterRitualLearnSkill.Entry);
				waterRitualLearnSkill.Points = this.GetWaterRitualSkillPointCost(waterRitualLearnSkill.Entry);
			}
			return true;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF9D RID: 49053 RVA: 0x0046340C File Offset: 0x0046160C
		public virtual bool ShowWaterRitualTagName(IBaseSkillEntry Entry)
		{
			return true;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF9E RID: 49054 RVA: 0x0046340F File Offset: 0x0046160F
		public virtual string GetWaterRitualText(IBaseSkillEntry Entry)
		{
			return null;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF9F RID: 49055 RVA: 0x00463412 File Offset: 0x00461612
		public virtual int GetWaterRitualReputationCost(IBaseSkillEntry Entry)
		{
			return Entry.Cost;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BFA0 RID: 49056 RVA: 0x0046341A File Offset: 0x0046161A
		public virtual int GetWaterRitualSkillPointCost(IBaseSkillEntry Entry)
		{
			return 0;
		}

		// Token: 0x0600BFA1 RID: 49057 RVA: 0x0046341D File Offset: 0x0046161D
		public virtual bool BeforeAddSkill(BeforeAddSkillEvent E)
		{
			if (!E.IsInclusion)
			{
				this.ShowAddPopup(E);
			}
			return true;
		}

		// Token: 0x0600BFA2 RID: 49058 RVA: 0x00463430 File Offset: 0x00461630
		public virtual void ShowAddPopup(BeforeAddSkillEvent E)
		{
			if (E.IsWaterRitual)
			{
				E.Actor.ShowSuccess(E.Source.Does("teach", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " you {{W|" + this.DisplayName + "}}!", true);
				return;
			}
			if (E.IsBook)
			{
				E.Actor.ShowSuccess("You learn {{W|" + this.DisplayName + "}}!", true);
			}
		}

		// Token: 0x0600BFA3 RID: 49059 RVA: 0x004634C3 File Offset: 0x004616C3
		public virtual bool AddSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600BFA4 RID: 49060 RVA: 0x004634C6 File Offset: 0x004616C6
		public virtual bool RemoveSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600BFA5 RID: 49061 RVA: 0x004634CC File Offset: 0x004616CC
		public virtual void UseEnergy(int Amount)
		{
			this.ParentObject.UseEnergy(Amount, "Skill", null, null, false);
		}

		// Token: 0x0600BFA6 RID: 49062 RVA: 0x004634F8 File Offset: 0x004616F8
		public virtual void UseEnergy(int Amount, string Type)
		{
			this.ParentObject.UseEnergy(Amount, Type, null, null, false);
		}

		// Token: 0x0600BFA7 RID: 49063 RVA: 0x0046351E File Offset: 0x0046171E
		public virtual string GetWeaponCriticalDescription()
		{
			return null;
		}

		// Token: 0x0600BFA8 RID: 49064 RVA: 0x00463521 File Offset: 0x00461721
		public virtual int GetWeaponCriticalModifier(GameObject Attacker, GameObject Defender, GameObject Weapon)
		{
			return 0;
		}

		// Token: 0x0600BFA9 RID: 49065 RVA: 0x00463524 File Offset: 0x00461724
		public virtual void WeaponMadeCriticalHit(GameObject Attacker, GameObject Defender, GameObject Weapon, string Properties)
		{
		}

		// Token: 0x04004D25 RID: 19749
		public const int ELEMENT_WEIGHT_HIGH = 3;

		// Token: 0x04004D26 RID: 19750
		public const int ELEMENT_WEIGHT_MEDIUM = 2;

		// Token: 0x04004D27 RID: 19751
		public const int ELEMENT_WEIGHT_LOW = 1;

		// Token: 0x04004D28 RID: 19752
		[NonSerialized]
		private IBaseSkillEntry _Entry;
	}
}
