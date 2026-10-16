using System;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x0200148F RID: 5263
	public class CyberneticsRemoveResult : CyberneticsScreen
	{
		// Token: 0x0600CEE7 RID: 52967 RVA: 0x004B5010 File Offset: 0x004B3210
		public CyberneticsRemoveResult(GameObject SelectedImplant)
		{
			this.MainText += "..................................................\n";
			this.MainText += "................................\n";
			this.MainText = this.MainText + "Uninstalling " + SelectedImplant.BaseKnownDisplayName + "...........\n";
			this.MainText += "Interfacing with nervous system...................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += "..................................................\n";
			this.MainText += ".....Complete!\n\n";
			this.MainText += "Congratulations! ";
			this.MainText += "Your cybernetic implant was successfully uninstalled.";
			this.Options.Add("Return To Main Menu");
		}

		// Token: 0x0600CEE8 RID: 52968 RVA: 0x004B5146 File Offset: 0x004B3346
		public override void Back()
		{
			base.Terminal.CheckSecurity(25, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0600CEE9 RID: 52969 RVA: 0x004B515C File Offset: 0x004B335C
		public override void Activate()
		{
			this.Back();
		}

		// Token: 0x0600CEEA RID: 52970 RVA: 0x004B5164 File Offset: 0x004B3364
		public override void TextComplete()
		{
			SoundManager.PlayUISound("sfx_cybernetic_implant_remove", 1f, false, false, SoundRequest.SoundEffectType.None);
			if (this.Destroyed)
			{
				SoundManager.PlayUISound("sfx_cybernetic_implant_destroyed", 1f, false, false, SoundRequest.SoundEffectType.None);
			}
		}

		// Token: 0x04005059 RID: 20569
		public bool Destroyed;
	}
}
