using System;
using System.Collections.Generic;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x0200148A RID: 5258
	public class CyberneticsScreenInstallLocation : CyberneticsScreen
	{
		// Token: 0x0600CED1 RID: 52945 RVA: 0x004B43A7 File Offset: 0x004B25A7
		public CyberneticsScreenInstallLocation(GameObject Implant)
		{
			this.Implant = Implant;
		}

		// Token: 0x0600CED2 RID: 52946 RVA: 0x004B43B8 File Offset: 0x004B25B8
		protected override void OnUpdate()
		{
			CyberneticsScreenInstallLocation.Slots.Clear();
			this.ClearOptions();
			this.MainText = "Please choose a target body part.";
			CyberneticsBaseItem part = this.Implant.GetPart<CyberneticsBaseItem>();
			Body body = base.Terminal.Subject.Body;
			List<string> list = part.Slots.CachedCommaExpansion();
			int num = 1;
			foreach (BodyPart bodyPart in body.GetParts())
			{
				if (list.Contains(bodyPart.Type) && bodyPart.CanReceiveCyberneticImplant())
				{
					if (bodyPart.Cybernetics != null)
					{
						if (!bodyPart.Cybernetics.HasTagOrProperty("CyberneticsNoRemove"))
						{
							string text = "";
							if (bodyPart.Cybernetics.HasTag("CyberneticsDestroyOnRemoval"))
							{
								text = "{{R|[destroyed on uninstall]}}";
							}
							CyberneticsScreenInstallLocation.Slots.Add(bodyPart);
							this.Options.Add(string.Concat(new string[]
							{
								bodyPart.Name,
								" [will replace ",
								bodyPart.Cybernetics.BaseKnownDisplayName,
								"]",
								text
							}));
						}
					}
					else
					{
						CyberneticsScreenInstallLocation.Slots.Add(bodyPart);
						this.Options.Add(bodyPart.Name);
					}
					num++;
				}
			}
			this.Options.Add("<cancel operation, return to main menu>");
		}

		// Token: 0x0600CED3 RID: 52947 RVA: 0x004B4534 File Offset: 0x004B2734
		public override void Back()
		{
			base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0600CED4 RID: 52948 RVA: 0x004B454C File Offset: 0x004B274C
		public override void Activate()
		{
			if (base.Terminal.Selected < CyberneticsScreenInstallLocation.Slots.Count)
			{
				base.Terminal.CurrentScreen = new CyberneticsInstallResult(this.Implant, CyberneticsScreenInstallLocation.Slots, base.Terminal.Selected);
				return;
			}
			this.Back();
		}

		// Token: 0x0400504E RID: 20558
		private GameObject Implant;

		// Token: 0x0400504F RID: 20559
		private static List<BodyPart> Slots = new List<BodyPart>();
	}
}
