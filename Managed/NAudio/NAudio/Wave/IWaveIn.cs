using System;

namespace NAudio.Wave
{
	// Token: 0x02000081 RID: 129
	public interface IWaveIn : IDisposable
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B0 RID: 688
		// (set) Token: 0x060002B1 RID: 689
		WaveFormat WaveFormat { get; set; }

		// Token: 0x060002B2 RID: 690
		void StartRecording();

		// Token: 0x060002B3 RID: 691
		void StopRecording();

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002B4 RID: 692
		// (remove) Token: 0x060002B5 RID: 693
		event EventHandler<WaveInEventArgs> DataAvailable;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002B6 RID: 694
		// (remove) Token: 0x060002B7 RID: 695
		event EventHandler<StoppedEventArgs> RecordingStopped;
	}
}
