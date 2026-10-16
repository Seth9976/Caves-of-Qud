using System;

namespace XRL.World.Parts
{
	// Token: 0x020012F4 RID: 4852
	public class NoBackdropBleedthrough : IPart
	{
		// Token: 0x0600BE6E RID: 48750 RVA: 0x0045BE94 File Offset: 0x0045A094
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x0600BE6F RID: 48751 RVA: 0x0045BE9C File Offset: 0x0045A09C
		public override bool FinalRender(RenderEvent E)
		{
			if (!string.IsNullOrEmpty(this.ZoneFilter))
			{
				GameObject parentObject = this.ParentObject;
				string text;
				if (parentObject == null)
				{
					text = null;
				}
				else
				{
					Zone currentZone = parentObject.CurrentZone;
					text = ((currentZone != null) ? currentZone.ZoneID : null);
				}
				if (!(text == this.ZoneFilter))
				{
					return false;
				}
			}
			E.WantsBackdrop = this.WantsBackdrop;
			E.BackdropBleedthrough = false;
			return false;
		}

		// Token: 0x04004CB1 RID: 19633
		public string WantsBackdrop = "backdropBlack";

		// Token: 0x04004CB2 RID: 19634
		public string ZoneFilter;
	}
}
