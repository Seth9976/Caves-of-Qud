using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F23 RID: 3875
	[Serializable]
	public class CyberneticsCommunicationsInterlock : IPart
	{
		// Token: 0x06009A88 RID: 39560 RVA: 0x00381183 File Offset: 0x0037F383
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A89 RID: 39561 RVA: 0x00381186 File Offset: 0x0037F386
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetRebukeLevelEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009A8A RID: 39562 RVA: 0x003811B4 File Offset: 0x0037F3B4
		public override bool HandleEvent(GetRebukeLevelEvent E)
		{
			E.Level += GetAvailableComputePowerEvent.AdjustUp(E.Actor, this.Bonus);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A8B RID: 39563 RVA: 0x003811DC File Offset: 0x0037F3DC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ParentObject.Implantee != null)
			{
				GenotypeEntry genotypeEntry = this.ParentObject.Implantee.genotypeEntry;
				bool flag;
				if (genotypeEntry == null)
				{
					flag = true;
				}
				else
				{
					List<string> skills = genotypeEntry.Skills;
					bool? flag2 = ((skills != null) ? new bool?(skills.Contains("Persuasion_RebukeRobot")) : null);
					bool flag3 = true;
					flag = !((flag2.GetValueOrDefault() == flag3) & (flag2 != null));
				}
				if (flag)
				{
					E.Postfix.AppendRules("For mutants, the first communications interlock allows the use of Rebuke Robot at a level of ability comparable to that of a True Kin with no communications interlock.");
				}
			}
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A8C RID: 39564 RVA: 0x00381273 File Offset: 0x0037F473
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (!E.Implantee.HasPart("Persuasion_RebukeRobot"))
			{
				E.Implantee.AddSkill("Persuasion_RebukeRobot", null, null);
				this.SkillApplied = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A8D RID: 39565 RVA: 0x003812A8 File Offset: 0x0037F4A8
		public override bool HandleEvent(UnimplantedEvent E)
		{
			if (this.SkillApplied)
			{
				GameObject implantee = E.Implantee;
				if (implantee != null)
				{
					implantee.RemoveSkill("Persuasion_RebukeRobot");
				}
				this.SkillApplied = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A8E RID: 39566 RVA: 0x003812D6 File Offset: 0x0037F4D6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004044 RID: 16452
		public const string SKILL = "Persuasion_RebukeRobot";

		// Token: 0x04004045 RID: 16453
		public const int DEFAULT_BONUS = 5;

		// Token: 0x04004046 RID: 16454
		public int Bonus = 5;

		// Token: 0x04004047 RID: 16455
		public bool SkillApplied;
	}
}
