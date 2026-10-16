using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001154 RID: 4436
	[Serializable]
	public class ModNav : IModification
	{
		// Token: 0x0600B036 RID: 45110 RVA: 0x0040E36F File Offset: 0x0040C56F
		public ModNav()
		{
		}

		// Token: 0x0600B037 RID: 45111 RVA: 0x0040E377 File Offset: 0x0040C577
		public ModNav(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B038 RID: 45112 RVA: 0x0040E380 File Offset: 0x0040C580
		public override void ApplyModification(GameObject Object)
		{
			NavigationBonus navigationBonus = Object.GetPart<NavigationBonus>();
			int num = 20 + this.Tier * 3;
			int num2 = 15 + this.Tier * 2;
			int num3 = 15 + this.Tier * 2;
			if (navigationBonus == null)
			{
				navigationBonus = new NavigationBonus();
				navigationBonus.PercentBonus = num.ToString();
				navigationBonus.EncounterPercentBonus = num2.ToString();
				navigationBonus.SpeedPercentBonus = num3.ToString();
				navigationBonus.ChargeUse = 1;
				navigationBonus.ComputePowerFactor = 1f;
				navigationBonus.SingleApplicationKey = "NavMapApplied";
				navigationBonus.ShowInShortDescription = true;
				Object.AddPart<NavigationBonus>(navigationBonus, true, false);
			}
			else
			{
				if (string.IsNullOrEmpty(navigationBonus.PercentBonus))
				{
					navigationBonus.PercentBonus = num.ToString();
				}
				else if (Stat.RollMin(navigationBonus.PercentBonus) == Stat.RollMax(navigationBonus.PercentBonus))
				{
					navigationBonus.PercentBonus = (Stat.Roll(navigationBonus.PercentBonus, null) + num).ToString();
				}
				else
				{
					NavigationBonus navigationBonus2 = navigationBonus;
					navigationBonus2.PercentBonus += num.Signed(false);
				}
				if (string.IsNullOrEmpty(navigationBonus.EncounterPercentBonus))
				{
					navigationBonus.EncounterPercentBonus = num2.ToString();
				}
				else if (Stat.RollMin(navigationBonus.EncounterPercentBonus) == Stat.RollMax(navigationBonus.EncounterPercentBonus))
				{
					navigationBonus.EncounterPercentBonus = (Stat.Roll(navigationBonus.EncounterPercentBonus, null) + num2).ToString();
				}
				else
				{
					NavigationBonus navigationBonus3 = navigationBonus;
					navigationBonus3.EncounterPercentBonus += num2.Signed(false);
				}
				if (string.IsNullOrEmpty(navigationBonus.SpeedPercentBonus))
				{
					navigationBonus.SpeedPercentBonus = num3.ToString();
				}
				else if (Stat.RollMin(navigationBonus.SpeedPercentBonus) == Stat.RollMax(navigationBonus.SpeedPercentBonus))
				{
					navigationBonus.SpeedPercentBonus = (Stat.Roll(navigationBonus.SpeedPercentBonus, null) + num3).ToString();
				}
				else
				{
					NavigationBonus navigationBonus4 = navigationBonus;
					navigationBonus4.SpeedPercentBonus += num3.Signed(false);
				}
				if (navigationBonus.ComputePowerFactor < 1f)
				{
					navigationBonus.ComputePowerFactor = 1f;
				}
				if (string.IsNullOrEmpty(navigationBonus.SingleApplicationKey))
				{
					navigationBonus.SingleApplicationKey = "NavMapApplied";
				}
				navigationBonus.ChargeUse++;
			}
			navigationBonus.IsEMPSensitive = true;
			navigationBonus.IsBootSensitive = true;
			int num4 = Math.Max((this.Tier >= 5) ? (5 - this.Tier / 2) : this.Tier, 2);
			BootSequence part = Object.GetPart<BootSequence>();
			if (part == null)
			{
				Object.AddPart<BootSequence>(new BootSequence
				{
					BootTime = num4,
					ReadoutInName = true,
					ReadoutInDescription = true
				}, true, false);
			}
			else
			{
				if (part.BootTime < num4)
				{
					part.BootTime = num4;
				}
				part.ReadoutInName = true;
				part.ReadoutInDescription = true;
			}
			Object.RequirePart<EnergyCellSocket>(false);
			base.IncreaseDifficultyAndComplexity(1, 2, null);
		}

		// Token: 0x0600B039 RID: 45113 RVA: 0x0040E639 File Offset: 0x0040C839
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B03A RID: 45114 RVA: 0x0040E65F File Offset: 0x0040C85F
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("{{r|nav}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B03B RID: 45115 RVA: 0x0040E689 File Offset: 0x0040C889
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Base.Compound(this.GetPhysicalDescription());
			E.Postfix.AppendRules(ModNav.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B03C RID: 45116 RVA: 0x0040E6BB File Offset: 0x0040C8BB
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 10);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B03D RID: 45117 RVA: 0x0040E6DF File Offset: 0x0040C8DF
		public string GetPhysicalDescription()
		{
			return "";
		}

		// Token: 0x0600B03E RID: 45118 RVA: 0x0040E6E6 File Offset: 0x0040C8E6
		public static string GetDescription(int Tier)
		{
			return "Nav: When powered and booted up, this item enhances navigation.";
		}
	}
}
