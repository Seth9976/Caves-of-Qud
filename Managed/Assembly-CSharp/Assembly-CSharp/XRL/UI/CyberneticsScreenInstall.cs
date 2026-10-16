using System;
using System.Collections.Generic;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x02001489 RID: 5257
	public class CyberneticsScreenInstall : CyberneticsScreen
	{
		// Token: 0x0600CECC RID: 52940 RVA: 0x004B40C4 File Offset: 0x004B22C4
		protected override void OnUpdate()
		{
			this.MainText = "You are becoming, aristocrat. Choose an implant to install.";
			CyberneticsScreenInstall.Implants.Clear();
			this.ClearOptions();
			if (base.Terminal.Implants.Count == 0)
			{
				this.MainText += "\n\n{{R|<No implants available>}}";
			}
			foreach (GameObject gameObject in base.Terminal.Implants)
			{
				if (!CyberneticsScreenInstall.Implants.Contains(gameObject))
				{
					CyberneticsScreenInstall.Implants.Add(gameObject);
					string text = gameObject.BaseDisplayName;
					if (gameObject.HasTag("CyberneticsOneOnly") && base.Terminal.Subject.HasInstalledCybernetics(gameObject.Blueprint, null))
					{
						text += " {{R|[already installed]}}";
					}
					else
					{
						int cost = gameObject.GetPart<CyberneticsBaseItem>().Cost;
						text = text + " " + ("[" + cost.Things("license point", null) + "]").Color((cost > base.LicensesRemaining) ? "R" : "C");
					}
					this.Options.Add(text);
				}
			}
			this.Options.Add("Return to main menu");
		}

		// Token: 0x0600CECD RID: 52941 RVA: 0x004B4220 File Offset: 0x004B2420
		public override void Back()
		{
			base.Terminal.CurrentScreen = new CyberneticsScreenMainMenu();
		}

		// Token: 0x0600CECE RID: 52942 RVA: 0x004B4234 File Offset: 0x004B2434
		public override void Activate()
		{
			if (base.Terminal.Selected >= CyberneticsScreenInstall.Implants.Count)
			{
				base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
				return;
			}
			GameObject gameObject = CyberneticsScreenInstall.Implants[base.Terminal.Selected];
			if (gameObject.HasTag("CyberneticsOneOnly") && base.Terminal.Subject.HasInstalledCybernetics(gameObject.Blueprint, null))
			{
				CyberneticsTerminal terminal = base.Terminal;
				string text = "Cybernetics already present:\n  -";
				GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(gameObject.Blueprint);
				terminal.CurrentScreen = new CyberneticsScreenSimpleText(text + (((blueprintIfExists != null) ? blueprintIfExists.CachedDisplayNameStripped : null) ?? gameObject.BaseDisplayName) + "\n\nPlease uninstall existing implant.", new CyberneticsScreenInstall(), 1);
				return;
			}
			if (gameObject.GetPart<CyberneticsBaseItem>().Cost > base.LicensesRemaining)
			{
				base.Terminal.CurrentScreen = new CyberneticsScreenSimpleText("Insufficent license points to install:\n  -" + gameObject.BaseDisplayName + "\n\nPlease uninstall an implant or upgrade your license.", new CyberneticsScreenInstall(), 1);
				return;
			}
			if (gameObject.IsBroken() || gameObject.IsRusted() || gameObject.IsEMPed() || gameObject.IsTemporary)
			{
				base.Terminal.CurrentScreen = new CyberneticsScreenSimpleText("Error: Condition inadequate for installation\n  -" + gameObject.BaseDisplayName + "\n\nPlease supply a replacement.", new CyberneticsScreenInstall(), 1);
				return;
			}
			base.Terminal.CurrentScreen = new CyberneticsScreenInstallLocation(gameObject);
		}

		// Token: 0x0400504D RID: 20557
		private static List<GameObject> Implants = new List<GameObject>();
	}
}
