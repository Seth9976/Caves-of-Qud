using System;

namespace XRL.World.Parts
{
	// Token: 0x020012F3 RID: 4851
	[Serializable]
	public class WantsBackdrop : IPart
	{
		// Token: 0x0600BE6A RID: 48746 RVA: 0x0045BDFC File Offset: 0x00459FFC
		public override bool SameAs(IPart p)
		{
			return !((p as WantsBackdrop).Backdrop != this.Backdrop) && base.SameAs(p);
		}

		// Token: 0x0600BE6B RID: 48747 RVA: 0x0045BE1F File Offset: 0x0045A01F
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x0600BE6C RID: 48748 RVA: 0x0045BE28 File Offset: 0x0045A028
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
					goto IL_0050;
				}
			}
			E.WantsBackdrop = this.Backdrop;
			E.BackdropBleedthrough = this.BackdropBleedthrough;
			IL_0050:
			return base.FinalRender(E);
		}

		// Token: 0x04004CAE RID: 19630
		public string ZoneFilter;

		// Token: 0x04004CAF RID: 19631
		public string Backdrop;

		// Token: 0x04004CB0 RID: 19632
		public bool BackdropBleedthrough;
	}
}
