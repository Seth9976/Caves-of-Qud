using System;
using System.Collections.Generic;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Skills;

namespace XRL.World.Units
{
	// Token: 0x020009B5 RID: 2485
	[Serializable]
	public class GameObjectSkillUnit : GameObjectUnit
	{
		// Token: 0x06006B05 RID: 27397 RVA: 0x002274F8 File Offset: 0x002256F8
		public override void Apply(GameObject Object)
		{
			Skills skills = Object.RequirePart<Skills>(false);
			SkillEntry skillEntry = null;
			if (!this.Skill.IsNullOrEmpty() && SkillFactory.Factory.SkillByClass.TryGetValue(this.Skill, out skillEntry))
			{
				skills.AddSkill(this.Skill);
			}
			if (!this.Power.IsNullOrEmpty())
			{
				if (((skillEntry != null) ? skillEntry.Powers : null) != null && (this.Power == "*" || this.Power == "All"))
				{
					using (Dictionary<string, PowerEntry>.Enumerator enumerator = skillEntry.Powers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, PowerEntry> keyValuePair = enumerator.Current;
							skills.AddSkill(keyValuePair.Value.Class);
						}
						return;
					}
				}
				if (SkillFactory.Factory.PowersByClass.ContainsKey(this.Power))
				{
					skills.AddSkill(this.Power);
				}
			}
		}

		// Token: 0x06006B06 RID: 27398 RVA: 0x002275F8 File Offset: 0x002257F8
		public override void Remove(GameObject Object)
		{
			Skills skills = Object.RequirePart<Skills>(false);
			SkillEntry skillEntry = null;
			if (!this.Skill.IsNullOrEmpty() && SkillFactory.Factory.SkillByClass.TryGetValue(this.Skill, out skillEntry))
			{
				skills.RemoveSkill(Object.GetPart(this.Skill) as BaseSkill);
			}
			if (!this.Power.IsNullOrEmpty())
			{
				if (((skillEntry != null) ? skillEntry.Powers : null) != null && (this.Power == "*" || this.Power == "All"))
				{
					using (Dictionary<string, PowerEntry>.Enumerator enumerator = skillEntry.Powers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, PowerEntry> keyValuePair = enumerator.Current;
							skills.RemoveSkill(Object.GetPart(keyValuePair.Key) as BaseSkill);
						}
						return;
					}
				}
				if (SkillFactory.Factory.PowersByClass.ContainsKey(this.Power))
				{
					skills.RemoveSkill(Object.GetPart(this.Power) as BaseSkill);
				}
			}
		}

		// Token: 0x06006B07 RID: 27399 RVA: 0x00227714 File Offset: 0x00225914
		public override void Reset()
		{
			base.Reset();
			this.Skill = null;
			this.Power = null;
		}

		// Token: 0x06006B08 RID: 27400 RVA: 0x0022772C File Offset: 0x0022592C
		public override string GetDescription(bool Inscription = false)
		{
			SkillEntry skillEntry;
			if (!this.Skill.IsNullOrEmpty() && SkillFactory.Factory.SkillByClass.TryGetValue(this.Skill, out skillEntry))
			{
				if (skillEntry.Powers != null && (this.Power == "*" || this.Power == "All"))
				{
					return "Has every " + skillEntry.Name + " skill";
				}
				return "Has the " + skillEntry.Name + " skill";
			}
			else
			{
				PowerEntry powerEntry;
				if (!this.Power.IsNullOrEmpty() && SkillFactory.Factory.PowersByClass.TryGetValue(this.Power, out powerEntry))
				{
					return "Has the " + powerEntry.Name + " skill";
				}
				return "Has the " + (this.Skill.IsNullOrEmpty() ? this.Power : this.Skill) + " skill";
			}
		}

		// Token: 0x04003102 RID: 12546
		public string Skill;

		// Token: 0x04003103 RID: 12547
		public string Power;
	}
}
