using System;

namespace XRL.World.Parts
{
	// Token: 0x0200122B RID: 4651
	[Serializable]
	public class PreservableItem : IPart
	{
		// Token: 0x0600B8C4 RID: 47300 RVA: 0x00436358 File Offset: 0x00434558
		public override bool SameAs(IPart p)
		{
			PreservableItem preservableItem = p as PreservableItem;
			return !(preservableItem.Result != this.Result) && preservableItem.Number == this.Number && base.SameAs(p);
		}

		// Token: 0x0600B8C5 RID: 47301 RVA: 0x00436398 File Offset: 0x00434598
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004A62 RID: 19042
		public string Result;

		// Token: 0x04004A63 RID: 19043
		public int Number;
	}
}
