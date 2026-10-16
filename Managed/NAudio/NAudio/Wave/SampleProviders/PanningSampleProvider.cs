using System;
using NAudio.Utils;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200018F RID: 399
	public class PanningSampleProvider : ISampleProvider
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x0001715C File Offset: 0x0001535C
		public PanningSampleProvider(ISampleProvider source)
		{
			if (source.WaveFormat.Channels != 1)
			{
				throw new ArgumentException("Source sample provider must be mono");
			}
			this.source = source;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(source.WaveFormat.SampleRate, 2);
			this.panStrategy = new SinPanStrategy();
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000171B1 File Offset: 0x000153B1
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x000171B9 File Offset: 0x000153B9
		public float Pan
		{
			get
			{
				return this.pan;
			}
			set
			{
				if (value < -1f || value > 1f)
				{
					throw new ArgumentOutOfRangeException("value", "Pan must be in the range -1 to 1");
				}
				this.pan = value;
				this.UpdateMultipliers();
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000171E8 File Offset: 0x000153E8
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x000171F0 File Offset: 0x000153F0
		public IPanStrategy PanStrategy
		{
			get
			{
				return this.panStrategy;
			}
			set
			{
				this.panStrategy = value;
				this.UpdateMultipliers();
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00017200 File Offset: 0x00015400
		private void UpdateMultipliers()
		{
			StereoSamplePair multipliers = this.panStrategy.GetMultipliers(this.Pan);
			this.leftMultiplier = multipliers.Left;
			this.rightMultiplier = multipliers.Right;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00017239 File Offset: 0x00015439
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00017244 File Offset: 0x00015444
		public int Read(float[] buffer, int offset, int count)
		{
			int num = count / 2;
			this.sourceBuffer = BufferHelpers.Ensure(this.sourceBuffer, num);
			int num2 = this.source.Read(this.sourceBuffer, 0, num);
			int num3 = offset;
			for (int i = 0; i < num2; i++)
			{
				buffer[num3++] = this.leftMultiplier * this.sourceBuffer[i];
				buffer[num3++] = this.rightMultiplier * this.sourceBuffer[i];
			}
			return num2 * 2;
		}

		// Token: 0x0400097F RID: 2431
		private readonly ISampleProvider source;

		// Token: 0x04000980 RID: 2432
		private float pan;

		// Token: 0x04000981 RID: 2433
		private float leftMultiplier;

		// Token: 0x04000982 RID: 2434
		private float rightMultiplier;

		// Token: 0x04000983 RID: 2435
		private readonly WaveFormat waveFormat;

		// Token: 0x04000984 RID: 2436
		private float[] sourceBuffer;

		// Token: 0x04000985 RID: 2437
		private IPanStrategy panStrategy;
	}
}
