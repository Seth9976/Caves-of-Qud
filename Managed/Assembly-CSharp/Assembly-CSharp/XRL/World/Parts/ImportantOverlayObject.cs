using System;

namespace XRL.World.Parts
{
	// Token: 0x02001300 RID: 4864
	[Serializable]
	public class ImportantOverlayObject : IPart
	{
		// Token: 0x0600BEA8 RID: 48808 RVA: 0x0045D04C File Offset: 0x0045B24C
		public override void Attach()
		{
			this.ParentObject.Flags |= 256;
		}
	}
}
