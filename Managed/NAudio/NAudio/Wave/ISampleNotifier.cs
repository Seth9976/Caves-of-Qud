using System;

namespace NAudio.Wave
{
	// Token: 0x020001C6 RID: 454
	public interface ISampleNotifier
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060009CC RID: 2508
		// (remove) Token: 0x060009CD RID: 2509
		event EventHandler<SampleEventArgs> Sample;
	}
}
