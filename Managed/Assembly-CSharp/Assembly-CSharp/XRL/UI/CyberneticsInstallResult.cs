using System;
using System.Collections.Generic;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x0200148E RID: 5262
	public class CyberneticsInstallResult : CyberneticsScreen
	{
		// Token: 0x0600CEE3 RID: 52963 RVA: 0x004B4C18 File Offset: 0x004B2E18
		public CyberneticsInstallResult(GameObject SelectedImplant, List<BodyPart> Slots, int Slot)
		{
			this.Slot = Slot;
			this.implant = SelectedImplant;
			this.Slots = Slots;
			this.MainText += "..................................................\n";
			this.MainText += "................................\n";
			this.MainText = this.MainText + "Installing " + SelectedImplant.BaseDisplayName + ".............\n";
			this.MainText += "Interfacing with nervous system...................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += ".....Complete!\n\n";
			this.MainText += "Congratulations! Your cybernetic implant was successfully installed.\n";
			this.MainText += "You are becoming.";
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEE4 RID: 52964 RVA: 0x004B4D64 File Offset: 0x004B2F64
		public override void TextComplete()
		{
			if (!this.installed)
			{
				this.installed = true;
				GameObject cybernetics = this.Slots[this.Slot].Cybernetics;
				if (this.implant.HasTag("CyberneticsUsesEqSlot") && this.Slots[this.Slot].Equipped != null && !this.Slots[this.Slot].TryUnequip(false, false, false, false))
				{
					base.Terminal.CurrentScreen = new CyberneticsScreenSimpleText("!Error: cannot unequip that limb", this, 0);
					return;
				}
				if (GameObject.Validate(ref cybernetics))
				{
					if (cybernetics.HasTag("CyberneticsDestroyOnRemoval"))
					{
						cybernetics.Unimplant(false, null);
						cybernetics.Destroy(null, false, false, null);
					}
					else
					{
						cybernetics.Unimplant(true, null);
					}
				}
				this.implant.RemoveFromContext(null);
				this.implant.MakeUnderstood(false);
				this.Slots[this.Slot].Implant(this.implant, false, false);
				GameObject gameObject = this.implant;
				int? num;
				if (gameObject == null)
				{
					num = null;
				}
				else
				{
					CyberneticsBaseItem part = gameObject.GetPart<CyberneticsBaseItem>();
					num = ((part != null) ? new int?(part.Cost) : null);
				}
				int? num2 = num;
				this.licensePointsInstalled = num2.GetValueOrDefault();
				Body parentBody = this.Slots[this.Slot].ParentBody;
				if (parentBody.ParentObject.IsPlayer())
				{
					SoundManager.PlayUISound("sfx_cybernetic_implant_install", 1f, false, false, SoundRequest.SoundEffectType.None);
					Achievement.INSTALL_IMPLANT.Unlock();
					bool flag = true;
					foreach (string text in new List<string> { "Head", "Face", "Body", "Hands", "Feet", "Arm", "Back" })
					{
						List<BodyPart> part2 = parentBody.GetPart(text);
						if (part2.IsNullOrEmpty<BodyPart>())
						{
							flag = false;
							break;
						}
						foreach (BodyPart bodyPart in part2)
						{
							if (bodyPart.Cybernetics == null && bodyPart.CanReceiveCyberneticImplant())
							{
								flag = false;
								goto IL_0250;
							}
						}
					}
					IL_0250:
					if (flag)
					{
						Achievement.INSTALL_IMPLANT_EVERY_SLOT.Unlock();
					}
				}
			}
		}

		// Token: 0x0600CEE5 RID: 52965 RVA: 0x004B4FEC File Offset: 0x004B31EC
		public override void Back()
		{
			base.Terminal.CheckSecurity(25, new CyberneticsScreenMainMenu(), this.licensePointsInstalled);
		}

		// Token: 0x0600CEE6 RID: 52966 RVA: 0x004B5007 File Offset: 0x004B3207
		public override void Activate()
		{
			this.Back();
		}

		// Token: 0x04005054 RID: 20564
		public GameObject implant;

		// Token: 0x04005055 RID: 20565
		public List<BodyPart> Slots;

		// Token: 0x04005056 RID: 20566
		public int Slot;

		// Token: 0x04005057 RID: 20567
		public int licensePointsInstalled;

		// Token: 0x04005058 RID: 20568
		public bool installed;
	}
}
