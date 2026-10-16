using System;

namespace XRL.World.Parts
{
	// Token: 0x0200102A RID: 4138
	[Serializable]
	public class FillPit : IPart
	{
		// Token: 0x0600A4D2 RID: 42194 RVA: 0x003C45C0 File Offset: 0x003C27C0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforePullDownEvent.ID;
		}

		// Token: 0x0600A4D3 RID: 42195 RVA: 0x003C45D8 File Offset: 0x003C27D8
		public override bool HandleEvent(BeforePullDownEvent E)
		{
			if (this.ReplaceWith.IsNullOrEmpty())
			{
				E.Pit.Obliterate(null, false, null);
			}
			else
			{
				E.Pit.ReplaceWith(this.ReplaceWith);
			}
			if (this.DestroySelf)
			{
				this.ParentObject.Destroy(null, false, false, null);
			}
			return false;
		}

		// Token: 0x0600A4D4 RID: 42196 RVA: 0x003C462E File Offset: 0x003C282E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400443C RID: 17468
		public bool DestroySelf;

		// Token: 0x0400443D RID: 17469
		public string ReplaceWith;
	}
}
