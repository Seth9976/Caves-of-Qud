using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x020001C5 RID: 453
	public class StreamVolumeEventArgs : EventArgs
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0001C536 File Offset: 0x0001A736
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x0001C53E File Offset: 0x0001A73E
		public float[] MaxSampleValues { get; set; }
	}
}
