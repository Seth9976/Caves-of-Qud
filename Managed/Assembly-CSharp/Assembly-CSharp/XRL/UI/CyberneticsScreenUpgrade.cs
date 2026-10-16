using System;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x0200148D RID: 5261
	public class CyberneticsScreenUpgrade : CyberneticsScreen
	{
		// Token: 0x0600CEDE RID: 52958 RVA: 0x004B4918 File Offset: 0x004B2B18
		public int GetUpgradeCost()
		{
			int num = base.Terminal.Licenses - base.Terminal.FreeLicenses;
			if (num < 8)
			{
				return 1;
			}
			if (num < 16)
			{
				return 2;
			}
			if (num < 24)
			{
				return 3;
			}
			return 4;
		}

		// Token: 0x0600CEDF RID: 52959 RVA: 0x004B4954 File Offset: 0x004B2B54
		protected override void OnUpdate()
		{
			this.ClearOptions();
			this.MainText = "Become a finer Aristocrat. Upgrade your license tier with cybernetics credits.\n\n{{C|1}} credit for license tiers 1-8\n{{C|2}} credits for license tiers 9-16\n{{C|3}} credits for license tiers 17-24\n{{C|4}} credits for license tiers 25+\n";
			if (base.Terminal.FreeLicenses > 0)
			{
				this.MainText = this.MainText + "\nRemember, Aristocrat, your base license tier is {{C|" + (base.Terminal.Licenses - base.Terminal.FreeLicenses).ToString() + "}}.";
			}
			int upgradeCost = this.GetUpgradeCost();
			string text = string.Concat(new string[]
			{
				"Upgrade Your License [{{C|",
				this.GetUpgradeCost().ToString(),
				"}} ",
				(upgradeCost == 1) ? "credit" : "credits",
				"]"
			});
			if (base.Terminal.Credits < this.GetUpgradeCost())
			{
				text += " {{R|insufficent credits}}";
			}
			this.Options.Add(text);
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEE0 RID: 52960 RVA: 0x004B4A44 File Offset: 0x004B2C44
		public override void Back()
		{
			base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0600CEE1 RID: 52961 RVA: 0x004B4A5C File Offset: 0x004B2C5C
		public override void Activate()
		{
			if (base.Terminal.Selected != 0)
			{
				base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
				return;
			}
			if (base.Terminal.Credits < this.GetUpgradeCost())
			{
				base.Terminal.CurrentScreen = new CyberneticsScreenSimpleText("{{R|Insufficient credits to upgrade}}", new CyberneticsScreenUpgrade(), 0);
				return;
			}
			int num = this.GetUpgradeCost();
			int num2 = 0;
			int count = base.Terminal.Wedges.Count;
			while (num2 < count && num > 0)
			{
				CyberneticsCreditWedge cyberneticsCreditWedge = base.Terminal.Wedges[num2];
				if (cyberneticsCreditWedge.ParentObject != null && cyberneticsCreditWedge.ParentObject.IsValid())
				{
					int count2 = cyberneticsCreditWedge.ParentObject.Count;
					int credits = cyberneticsCreditWedge.Credits;
					int num3 = credits * count2;
					if (num3 > num)
					{
						int num4 = 0;
						int num5 = 0;
						while (num >= credits && num4 < count2)
						{
							cyberneticsCreditWedge.ParentObject.Destroy(null, false, false, null);
							num -= credits;
							num4++;
							if (++num5 >= 10000)
							{
								throw new Exception("infinite loop in license upgrade wedge use");
							}
						}
						if (num > 0 && num4 < count2)
						{
							cyberneticsCreditWedge.ParentObject.SplitStack(1, null, false);
							cyberneticsCreditWedge.Credits -= num;
							cyberneticsCreditWedge.ParentObject.CheckStack();
							num = 0;
						}
					}
					else
					{
						num -= num3;
						cyberneticsCreditWedge.ParentObject.Obliterate(null, false, null);
					}
				}
				num2++;
			}
			SoundManager.PlayUISound("sfx_cybernetic_terminal_license_upgrade", 1f, false, false, SoundRequest.SoundEffectType.None);
			base.Terminal.Subject.ModIntProperty("CyberneticsLicenses", 1, false);
			base.Terminal.CurrentScreen = new CyberneticsScreenSimpleText("You are Becoming, Aristocrat.", new CyberneticsScreenUpgrade(), 5);
		}
	}
}
