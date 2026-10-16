using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200005C RID: 92
	[StructLayout(0)]
	public class MFT_REGISTER_TYPE_INFO
	{
		// Token: 0x04000302 RID: 770
		public Guid guidMajorType;

		// Token: 0x04000303 RID: 771
		public Guid guidSubtype;
	}
}
