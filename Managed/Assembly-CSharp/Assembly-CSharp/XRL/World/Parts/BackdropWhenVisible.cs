using System;

namespace XRL.World.Parts
{
	// Token: 0x020012D1 RID: 4817
	[Serializable]
	public class BackdropWhenVisible : IPart
	{
		// Token: 0x0600BDD2 RID: 48594 RVA: 0x004595F1 File Offset: 0x004577F1
		public override bool SameAs(IPart p)
		{
			return !((p as WantsBackdrop).Backdrop != this.Backdrop) && base.SameAs(p);
		}

		// Token: 0x0600BDD3 RID: 48595 RVA: 0x00459614 File Offset: 0x00457814
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x0600BDD4 RID: 48596 RVA: 0x0045961C File Offset: 0x0045781C
		public override bool FinalRender(RenderEvent E)
		{
			if (this.ParentObject.IsVisible())
			{
				E.WantsBackdrop = this.Backdrop;
				E.BackdropBleedthrough = this.BackdropBleedthrough;
			}
			return base.FinalRender(E);
		}

		// Token: 0x04004C77 RID: 19575
		public string Backdrop;

		// Token: 0x04004C78 RID: 19576
		public bool BackdropBleedthrough;
	}
}
