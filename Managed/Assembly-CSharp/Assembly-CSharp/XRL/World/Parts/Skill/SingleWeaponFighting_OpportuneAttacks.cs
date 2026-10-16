using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001393 RID: 5011
	[Serializable]
	public class SingleWeaponFighting_OpportuneAttacks : ISingleWeaponFightingSkill
	{
		// Token: 0x0600C216 RID: 49686 RVA: 0x0046E735 File Offset: 0x0046C935
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAttackerMeleePenetrationEvent.ID;
		}

		// Token: 0x0600C217 RID: 49687 RVA: 0x0046E74B File Offset: 0x0046C94B
		public override bool HandleEvent(GetAttackerMeleePenetrationEvent E)
		{
			if (this.AbilityEntry.ToggleState && E.Critical)
			{
				this.PopulateSkills(E.Weapon);
				if (this.PopulateRefreshable(E.Attacker))
				{
					this.Refresh(2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C218 RID: 49688 RVA: 0x0046E78C File Offset: 0x0046C98C
		public void PopulateSkills(GameObject Weapon)
		{
			if (this.Skills == null)
			{
				this.Skills = new List<string>(2);
			}
			this.Skills.Clear();
			string text = ((Weapon != null) ? Weapon.GetWeaponSkill() : null);
			if (!text.IsNullOrEmpty())
			{
				this.Skills.Add(text);
			}
			this.Skills.Add("Shield");
		}

		// Token: 0x0600C219 RID: 49689 RVA: 0x0046E7EC File Offset: 0x0046C9EC
		public bool PopulateRefreshable(GameObject Actor)
		{
			Dictionary<Guid, ActivatedAbilityEntry> dictionary;
			if (Actor == null)
			{
				dictionary = null;
			}
			else
			{
				ActivatedAbilities activatedAbilities = Actor.ActivatedAbilities;
				dictionary = ((activatedAbilities != null) ? activatedAbilities.AbilityByGuid : null);
			}
			Dictionary<Guid, ActivatedAbilityEntry> dictionary2 = dictionary;
			if (dictionary2.IsNullOrEmpty<KeyValuePair<Guid, ActivatedAbilityEntry>>())
			{
				return false;
			}
			if (this.Abilities == null)
			{
				this.Abilities = new List<ActivatedAbilityEntry>();
			}
			this.Abilities.Clear();
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in dictionary2)
			{
				if (this.IsRefreshable(keyValuePair.Value))
				{
					this.Abilities.Add(keyValuePair.Value);
				}
			}
			return this.Abilities.Count > 0;
		}

		// Token: 0x0600C21A RID: 49690 RVA: 0x0046E8A4 File Offset: 0x0046CAA4
		public bool IsRefreshable(ActivatedAbilityEntry Entry)
		{
			string text;
			return Entry.Cooldown > 0 && Entry.TryGetTag("Skill", out text) && this.Skills.Contains(text) && CanRefreshAbilityEvent.Check(this.ParentObject, Entry);
		}

		// Token: 0x0600C21B RID: 49691 RVA: 0x0046E8E8 File Offset: 0x0046CAE8
		public void Refresh(int Amount)
		{
			this.Skills.Clear();
			for (int i = 0; i < Amount; i++)
			{
				ActivatedAbilityEntry activatedAbilityEntry = this.Abilities.RemoveRandomElement<ActivatedAbilityEntry>(null);
				if (activatedAbilityEntry == null)
				{
					break;
				}
				activatedAbilityEntry.Refresh(this);
				if (!this.Skills.Contains(activatedAbilityEntry.DisplayName))
				{
					this.Skills.Add(activatedAbilityEntry.DisplayName);
				}
			}
			int count = this.Skills.Count;
			if (count > 0)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				for (int j = 0; j < count; j++)
				{
					this.Skills[j] = stringBuilder.Append("{{W|").Append(this.Skills[j]).Append("}}")
						.ToString();
					stringBuilder.Clear();
				}
				stringBuilder.Append("advantage of ").Append(this.ParentObject.its).Append(" opponent's reaction to ")
					.Append(this.ParentObject.its)
					.Append(" attack! ")
					.Append(Grammar.MakeAndList(this.Skills, true))
					.Append((count == 1) ? " is " : " are ")
					.Append("refreshed");
				base.DidX("take", stringBuilder.ToString(), null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x04004D91 RID: 19857
		public const int ABL_REFRESH = 2;

		// Token: 0x04004D92 RID: 19858
		[NonSerialized]
		private List<string> Skills;

		// Token: 0x04004D93 RID: 19859
		[NonSerialized]
		private List<ActivatedAbilityEntry> Abilities;
	}
}
