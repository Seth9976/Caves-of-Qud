using System;
using System.Linq;
using XRL.Language;
using XRL.UI;
using XRL.World.Skills;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x02001322 RID: 4898
	[Serializable]
	public abstract class BaseInitiatorySkill : BaseSkill
	{
		// Token: 0x0600BF8C RID: 49036 RVA: 0x00462DFC File Offset: 0x00460FFC
		public override bool ShowWaterRitualTagName(IBaseSkillEntry Entry)
		{
			return false;
		}

		// Token: 0x0600BF8D RID: 49037 RVA: 0x00462DFF File Offset: 0x00460FFF
		public override string GetWaterRitualText(IBaseSkillEntry Entry)
		{
			return "I seek =skill.name=.";
		}

		// Token: 0x0600BF8E RID: 49038 RVA: 0x00462E06 File Offset: 0x00461006
		public override int GetWaterRitualSkillPointCost(IBaseSkillEntry Entry)
		{
			return Entry.Cost;
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF8F RID: 49039 RVA: 0x00462E10 File Offset: 0x00461010
		public virtual string GetCompletedText(GameObject Actor, GameObject Object, SkillEntry Skill, string Context)
		{
			return Actor.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, Actor.IsPlayer(), true, null, true, null, false) + " completed " + Skill.Name + ".";
		}

		/// <remarks>Invoked on stateless instance.</remarks>
		// Token: 0x0600BF90 RID: 49040 RVA: 0x00462E64 File Offset: 0x00461064
		public virtual string GetExpendedText(GameObject Actor, GameObject Object, SkillEntry Skill, string Context)
		{
			if (Context.HasDelimitedSubstring(',', "TrainingBook", StringComparison.Ordinal))
			{
				return string.Concat(new string[]
				{
					Actor.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, Actor.IsPlayer(), true, null, true, null, false),
					" already gleaned as many insights into ",
					Skill.Name,
					" from ",
					Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					" as ",
					Actor.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false),
					" going to."
				});
			}
			return null;
		}

		// Token: 0x0600BF91 RID: 49041 RVA: 0x00462F48 File Offset: 0x00461148
		public override void ShowAddPopup(BeforeAddSkillEvent E)
		{
			if (E.Source != null && (E.IsWaterRitual || E.IsBook))
			{
				string text = (E.IsWaterRitual ? "lead" : "guide");
				IBaseSkillEntry entry = E.Entry;
				PowerEntry power = entry as PowerEntry;
				if (power != null && power.ParentSkill != null)
				{
					bool flag = power.ParentSkill.PowerList.Any<PowerEntry>((PowerEntry p) => p != power && !E.Actor.HasSkill(p.Class));
					E.Actor.ShowSuccess(string.Concat(new string[]
					{
						E.Source.Does(text, int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false),
						" you through a mysterious rite. Your journey upon ",
						power.ParentSkill.Name,
						flag ? " continues" : " has reached completion",
						power.Name.IsNullOrEmpty() ? "" : (" with initiation into " + power.Name),
						"."
					}), true);
					return;
				}
				if (E.Entry is SkillEntry)
				{
					GameObject actor = E.Actor;
					string[] array = new string[5];
					array[0] = E.Source.Does(text, int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false);
					array[1] = " you through a rite of ancient mystery, one not for profane eyes or ears. You have begun your journey upon ";
					array[2] = base.DisplayName;
					int num = 3;
					string text2;
					if (!E.Include.IsNullOrEmpty<IBaseSkillEntry>())
					{
						text2 = " with initiation into " + Grammar.MakeAndList(E.Include.Select<IBaseSkillEntry, string>((IBaseSkillEntry x) => x.Name).ToList<string>(), true);
					}
					else
					{
						text2 = "";
					}
					array[num] = text2;
					array[4] = ".";
					actor.ShowSuccess(string.Concat(array), true);
				}
			}
		}

		// Token: 0x0600BF92 RID: 49042 RVA: 0x0046319C File Offset: 0x0046139C
		public static IBaseSkillEntry GetSkillFor(GameObject Object, SkillEntry Skill)
		{
			if (!Object.HasPart(Skill.Class))
			{
				return Skill;
			}
			foreach (PowerEntry powerEntry in Skill.PowerList)
			{
				if (!Object.HasSkill(powerEntry.Class) && powerEntry.MeetsRequirements(Object, false))
				{
					return powerEntry;
				}
			}
			return null;
		}

		// Token: 0x0600BF93 RID: 49043 RVA: 0x00463218 File Offset: 0x00461418
		public static bool ShowCompletedPopup(GameObject Actor, GameObject Object, SkillEntry Skill, string Context)
		{
			BaseInitiatorySkill baseInitiatorySkill = Skill.Generic as BaseInitiatorySkill;
			if (baseInitiatorySkill != null)
			{
				string completedText = baseInitiatorySkill.GetCompletedText(Actor, Object, Skill, Context);
				if (!completedText.IsNullOrEmpty())
				{
					Popup.Show(completedText, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BF94 RID: 49044 RVA: 0x0046325C File Offset: 0x0046145C
		public static bool ShowExpendedPopup(GameObject Actor, GameObject Object, SkillEntry Skill, string Context)
		{
			BaseInitiatorySkill baseInitiatorySkill = Skill.Generic as BaseInitiatorySkill;
			if (baseInitiatorySkill != null)
			{
				string expendedText = baseInitiatorySkill.GetExpendedText(Actor, Object, Skill, Context);
				if (!expendedText.IsNullOrEmpty())
				{
					Popup.Show(expendedText, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BF95 RID: 49045 RVA: 0x0046329F File Offset: 0x0046149F
		public static string GetInitiatoryKey(GameObject Actor, IBaseSkillEntry Entry)
		{
			return Entry.Class + "_Initiated_By_" + Actor.ID;
		}
	}
}
