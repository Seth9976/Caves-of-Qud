using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000644 RID: 1604
	internal struct NudgeJobData
	{
		// Token: 0x04001632 RID: 5682
		public IntPtr headSrc;

		// Token: 0x04001633 RID: 5683
		public IntPtr headDst;

		// Token: 0x04001634 RID: 5684
		public int headCount;

		// Token: 0x04001635 RID: 5685
		public IntPtr tailSrc;

		// Token: 0x04001636 RID: 5686
		public IntPtr tailDst;

		// Token: 0x04001637 RID: 5687
		public int tailCount;

		// Token: 0x04001638 RID: 5688
		public Matrix4x4 transform;

		// Token: 0x04001639 RID: 5689
		public int keepZ;
	}
}
