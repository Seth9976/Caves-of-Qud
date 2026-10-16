using System;
using System.Collections.Generic;
using XRL.World;
using XRL.World.Anatomy;

namespace XRL.UI
{
	// Token: 0x0200148C RID: 5260
	public class CyberneticsScreenRemove : CyberneticsScreen
	{
		// Token: 0x0600CED9 RID: 52953 RVA: 0x004B45FC File Offset: 0x004B27FC
		protected override void OnUpdate()
		{
			this.ClearOptions();
			this.Cybernetics.Clear();
			this.BodyParts.Clear();
			foreach (BodyPart bodyPart in base.Terminal.Subject.Body.GetBody().GetParts())
			{
				if (bodyPart.Cybernetics != null && !this.Cybernetics.Contains(bodyPart.Cybernetics))
				{
					string text = "";
					if (bodyPart.Cybernetics.HasTagOrProperty("CyberneticsNoRemove"))
					{
						text += " {{R|[cannot be uninstalled]}}";
					}
					if (bodyPart.Cybernetics.HasTag("CyberneticsDestroyOnRemoval"))
					{
						text += " {{R|[destroyed on uninstall]}}";
					}
					this.Options.Add(string.Concat(new string[]
					{
						bodyPart.Cybernetics.BaseKnownDisplayName,
						" [",
						this.GetBodyPartNameForTerminal(bodyPart),
						"]",
						text
					}));
					this.BodyParts.Add(bodyPart);
					this.Cybernetics.Add(bodyPart.Cybernetics);
				}
			}
			this.MainText = "You are given to whimsy, Aristocrat. Choose an implant to uninstall.";
			if (this.Options.Count == 0)
			{
				this.MainText += "\n\n{{R|<no implants installed>}}";
			}
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEDA RID: 52954 RVA: 0x004B4780 File Offset: 0x004B2980
		private string GetBodyPartNameForTerminal(BodyPart Part)
		{
			return Part.Name.Replace("Worn on ", "");
		}

		// Token: 0x0600CEDB RID: 52955 RVA: 0x004B4797 File Offset: 0x004B2997
		public override void Back()
		{
			base.Terminal.CurrentScreen = new CyberneticsScreenMainMenu();
		}

		// Token: 0x0600CEDC RID: 52956 RVA: 0x004B47AC File Offset: 0x004B29AC
		public override void Activate()
		{
			if (base.Terminal.Selected >= this.Cybernetics.Count)
			{
				base.Terminal.CheckSecurity(1, new CyberneticsScreenMainMenu(), 1);
				return;
			}
			if (this.Cybernetics[base.Terminal.Selected].HasTagOrProperty("CyberneticsNoRemove"))
			{
				base.Terminal.CurrentScreen = new CyberneticsScreenSimpleText("Whimsy must yield to necessity, Aristocrat.", new CyberneticsScreenRemove(), 1);
				return;
			}
			if (this.Cybernetics[base.Terminal.Selected].HasTag("CyberneticsDestroyOnRemoval"))
			{
				this.Cybernetics[base.Terminal.Selected].Unimplant(false, null);
				this.Cybernetics[base.Terminal.Selected].Destroy(null, false, false, null);
				CyberneticsRemoveResult cyberneticsRemoveResult = new CyberneticsRemoveResult(this.Cybernetics[base.Terminal.Selected]);
				cyberneticsRemoveResult.Destroyed = true;
				base.Terminal.CurrentScreen = cyberneticsRemoveResult;
				return;
			}
			this.Cybernetics[base.Terminal.Selected].Unimplant(true, null);
			base.Terminal.CurrentScreen = new CyberneticsRemoveResult(this.Cybernetics[base.Terminal.Selected]);
		}

		// Token: 0x04005052 RID: 20562
		private List<GameObject> Cybernetics = new List<GameObject>();

		// Token: 0x04005053 RID: 20563
		private List<BodyPart> BodyParts = new List<BodyPart>();
	}
}
