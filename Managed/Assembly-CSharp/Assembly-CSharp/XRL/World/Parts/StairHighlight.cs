using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200128C RID: 4748
	[Serializable]
	public class StairHighlight : IPart
	{
		// Token: 0x0600BBDA RID: 48090 RVA: 0x00449890 File Offset: 0x00447A90
		public override bool FinalRender(RenderEvent E)
		{
			if (this.bEnabled && Options.HighlightStairs && this.ParentObject.CurrentCell != null && this.ParentObject.CurrentCell.IsExplored() && !this.ParentObject.CurrentCell.HasObjectWithPropertyOrTag("SuspendedPlatform"))
			{
				E.CustomDraw = true;
				E.ColorString = (Options.UseTiles ? "&y^M" : "&Y^M");
				E.DetailColor = "Y";
			}
			return true;
		}

		// Token: 0x04004B4A RID: 19274
		public bool bEnabled = true;
	}
}
