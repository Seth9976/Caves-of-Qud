using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x02001492 RID: 5266
	public class GritGateTerminalScreen : TerminalScreen
	{
		// Token: 0x0600CF10 RID: 53008 RVA: 0x004B635E File Offset: 0x004B455E
		protected override void ClearOptions()
		{
			base.ClearOptions();
			this.optionActions.Clear();
		}

		// Token: 0x0600CF11 RID: 53009 RVA: 0x004B6374 File Offset: 0x004B4574
		public override void BeforeRender(ScreenBuffer buffer, ref string footerText)
		{
			if (this.generator == null)
			{
				GameObject gameObject = base.Terminal.Terminal.CurrentCell.ParentZone.FindObject((GameObject o) => o.Blueprint == "GritGateFusionPowerStation" && o.Physics.CurrentCell.Y >= 10);
				if (gameObject != null)
				{
					this.generator = gameObject.GetPart<ElectricalPowerTransmission>();
				}
			}
			if (this.generator == null)
			{
				this.available = 0;
				footerText = "[{{R|!!! ERROR: POWER SYSTEMS HAVE FAILED !!!}}]";
			}
			else
			{
				int num = 4000;
				int totalDraw = this.generator.GetTotalDraw();
				this.available = num - totalDraw;
				if (this.available < 0)
				{
					footerText = "[{{W|!!! WARNING: INSUFFICIENT POWER !!!}}]";
				}
				else
				{
					footerText = string.Format(" Available power: {0} amps ", (float)this.available * 0.1f);
				}
				GritGateAmperageImposter.display = " Available power: " + ((float)this.available * 0.1f).ToString() + " amps ";
			}
			if (buffer != null)
			{
				int num2 = 3;
				buffer.Goto(num2 + 3, 24);
				buffer.Write(footerText, true, false, false, null, -1);
			}
		}

		// Token: 0x04005069 RID: 20585
		public List<Action> optionActions = new List<Action>();

		// Token: 0x0400506A RID: 20586
		private ElectricalPowerTransmission generator;

		// Token: 0x0400506B RID: 20587
		public int available;
	}
}
