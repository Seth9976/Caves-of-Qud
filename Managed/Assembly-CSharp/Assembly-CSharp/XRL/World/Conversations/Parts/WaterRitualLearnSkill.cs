using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.UI;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C28 RID: 3112
	public class WaterRitualLearnSkill : IWaterRitualPart
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06007C75 RID: 31861 RVA: 0x002EBFF7 File Offset: 0x002EA1F7
		public string InitiatoryKey
		{
			get
			{
				return BaseInitiatorySkill.GetInitiatoryKey(The.Speaker, this.Entry);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06007C76 RID: 31862 RVA: 0x002EC009 File Offset: 0x002EA209
		public IBaseSkillEntry InitiatorySkill
		{
			get
			{
				return BaseInitiatorySkill.GetSkillFor(The.Player, this.ParentSkill);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06007C77 RID: 31863 RVA: 0x002EC01B File Offset: 0x002EA21B
		public override bool Available
		{
			get
			{
				return The.Player.Stat("SP", 0) >= this.Points;
			}
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x002EC038 File Offset: 0x002EA238
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnteredElementEvent.ID || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x002EC060 File Offset: 0x002EA260
		public override void Awake()
		{
			if (The.Speaker.HasProperty("WaterRitualNoSellSkill"))
			{
				return;
			}
			string text;
			if ((text = The.Speaker.GetStringProperty("WaterRitual_Skill", null)) == null)
			{
				text = The.Speaker.GetxTag("WaterRitual", "SellSkill", null) ?? (WaterRitual.Alternative ? WaterRitual.RecordFaction.WaterRitualAltSkill : WaterRitual.RecordFaction.WaterRitualSkill);
			}
			string text2 = text;
			if (text2.IsNullOrEmpty())
			{
				return;
			}
			if (SkillFactory.Factory.TryGetFirstEntry(text2, out this.Entry))
			{
				SkillEntry skillEntry = this.Entry as SkillEntry;
				if (skillEntry != null)
				{
					this.ParentSkill = skillEntry;
					if (this.Entry.Cost == 0)
					{
						this.Entry = skillEntry.PowerList.FirstOrDefault<PowerEntry>((PowerEntry x) => x.Cost > 0) ?? this.Entry;
					}
				}
				else
				{
					PowerEntry powerEntry = this.Entry as PowerEntry;
					if (powerEntry != null)
					{
						this.ParentSkill = powerEntry.ParentSkill;
						if (this.ParentSkill != null && (this.Entry.Cost == 0 || this.ParentSkill.Initiatory))
						{
							this.Entry = this.ParentSkill;
						}
					}
				}
			}
			if (this.Entry == null)
			{
				return;
			}
			this.TagName = true;
			this.DisplayName = this.Entry.Name;
			this.Reputation = this.Entry.Cost;
			this.Points = 0;
			if (!this.Entry.Generic.IsValidForElement(this.ParentElement, this, this.Context))
			{
				return;
			}
			if (this.ParentSkill != null && this.ParentSkill.Initiatory)
			{
				if (The.Player.GetIntProperty(this.InitiatoryKey, 0) > 0 || this.InitiatorySkill == null)
				{
					return;
				}
			}
			else if (The.Player.HasSkill(this.Entry.Class))
			{
				return;
			}
			this.Reputation = GetWaterRitualCostEvent.GetFor(The.Player, The.Speaker, "Skill", this.Reputation);
			this.Disambiguate = this.ShouldDisambiguatePower();
			this.Visible = true;
		}

		// Token: 0x06007C7A RID: 31866 RVA: 0x002EC26C File Offset: 0x002EA46C
		public void Unlock()
		{
			if (!base.UseReputation("WaterRitualUse"))
			{
				return;
			}
			string text = this.Entry.Class;
			if (this.ParentSkill != null && this.ParentSkill.Initiatory)
			{
				IBaseSkillEntry initiatorySkill = this.InitiatorySkill;
				text = ((initiatorySkill != null) ? initiatorySkill.Class : null);
				if (text.IsNullOrEmpty())
				{
					BaseInitiatorySkill.ShowCompletedPopup(The.Player, The.Speaker, this.ParentSkill, this.Context);
					return;
				}
				The.Player.SetIntProperty(this.InitiatoryKey, The.Player.Stat("Level", 0), false);
			}
			The.Player.AddSkill(text, The.Speaker, this.Context);
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x002EC319 File Offset: 0x002EA519
		public bool ShouldDisambiguatePower()
		{
			return !(this.Entry is SkillEntry) && this.ParentSkill != null && SkillFactory.Factory.PowersByClass.Count<KeyValuePair<string, PowerEntry>>((KeyValuePair<string, PowerEntry> Pair) => Pair.Value.Name == this.Entry.Name && Pair.Value.Class != this.Entry.Class) >= 1;
		}

		// Token: 0x06007C7C RID: 31868 RVA: 0x002EC358 File Offset: 0x002EA558
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (The.Player.Stat("SP", 0) < this.Points)
			{
				Popup.ShowFail("You don't have enough skill points.", true, true, true);
				return false;
			}
			this.Unlock();
			if (this.Points > 0)
			{
				The.Player.GetStat("SP").Penalty += this.Points;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C7D RID: 31869 RVA: 0x002EC3C4 File Offset: 0x002EA5C4
		public override bool HandleEvent(PrepareTextEvent E)
		{
			if (!this.Text.IsNullOrEmpty())
			{
				E.Text.Clear().Append(this.Text);
			}
			E.Text.Replace("=skill.name=", this.DisplayName);
			return base.HandleEvent(E);
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x002EC414 File Offset: 0x002EA614
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("{{").Append(this.Lowlight).Append("|[");
			if (this.TagName)
			{
				stringBuilder.Append("learn {{W|").Append(this.DisplayName);
				if (this.Disambiguate)
				{
					stringBuilder.Compound('(').Append(this.ParentSkill.Name).Append(')');
				}
				stringBuilder.Append("}}: ");
			}
			stringBuilder.Append("{{").Append(this.Numeric).Append("|")
				.Append(base.GetReputationCost("WaterRitualUse"))
				.Append("}} reputation");
			if (this.Points > 0)
			{
				stringBuilder.Append(", {{C|-").Append(this.Points).Append("}} SP");
			}
			E.Tag = stringBuilder.Append("]}}").ToString();
			return false;
		}

		// Token: 0x0400388C RID: 14476
		public string DisplayName;

		// Token: 0x0400388D RID: 14477
		public string Text;

		// Token: 0x0400388E RID: 14478
		public string Context = "WaterRitual,LearnSkill";

		// Token: 0x0400388F RID: 14479
		public SkillEntry ParentSkill;

		// Token: 0x04003890 RID: 14480
		public IBaseSkillEntry Entry;

		// Token: 0x04003891 RID: 14481
		public int Points;

		// Token: 0x04003892 RID: 14482
		public bool Disambiguate;

		// Token: 0x04003893 RID: 14483
		public bool TagName = true;
	}
}
