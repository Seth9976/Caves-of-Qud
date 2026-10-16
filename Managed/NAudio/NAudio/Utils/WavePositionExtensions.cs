using System;
using NAudio.Wave;

namespace NAudio.Utils
{
	// Token: 0x0200006A RID: 106
	public static class WavePositionExtensions
	{
		// Token: 0x0600024A RID: 586 RVA: 0x00007734 File Offset: 0x00005934
		public static TimeSpan GetPositionTimeSpan(this IWavePosition @this)
		{
			long num = @this.GetPosition() / (long)(@this.OutputWaveFormat.Channels * @this.OutputWaveFormat.BitsPerSample / 8);
			return TimeSpan.FromMilliseconds((double)num * 1000.0 / (double)@this.OutputWaveFormat.SampleRate);
		}
	}
}
