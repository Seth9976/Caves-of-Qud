using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x02000075 RID: 117
	public class OffsetSampleProvider : ISampleProvider
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00008170 File Offset: 0x00006370
		private int TimeSpanToSamples(TimeSpan time)
		{
			return (int)(time.TotalSeconds * (double)this.WaveFormat.SampleRate) * this.WaveFormat.Channels;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000081A0 File Offset: 0x000063A0
		private TimeSpan SamplesToTimeSpan(int samples)
		{
			return TimeSpan.FromSeconds((double)(samples / this.WaveFormat.Channels) / (double)this.WaveFormat.SampleRate);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000270 RID: 624 RVA: 0x000081C2 File Offset: 0x000063C2
		// (set) Token: 0x06000271 RID: 625 RVA: 0x000081CA File Offset: 0x000063CA
		public int DelayBySamples
		{
			get
			{
				return this.delayBySamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set DelayBySamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("DelayBySamples must be a multiple of WaveFormat.Channels");
				}
				this.delayBySamples = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00008200 File Offset: 0x00006400
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000820E File Offset: 0x0000640E
		public TimeSpan DelayBy
		{
			get
			{
				return this.SamplesToTimeSpan(this.delayBySamples);
			}
			set
			{
				this.delayBySamples = this.TimeSpanToSamples(value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000821D File Offset: 0x0000641D
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00008225 File Offset: 0x00006425
		public int SkipOverSamples
		{
			get
			{
				return this.skipOverSamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set SkipOverSamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("SkipOverSamples must be a multiple of WaveFormat.Channels");
				}
				this.skipOverSamples = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000825B File Offset: 0x0000645B
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00008269 File Offset: 0x00006469
		public TimeSpan SkipOver
		{
			get
			{
				return this.SamplesToTimeSpan(this.skipOverSamples);
			}
			set
			{
				this.skipOverSamples = this.TimeSpanToSamples(value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00008278 File Offset: 0x00006478
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00008280 File Offset: 0x00006480
		public int TakeSamples
		{
			get
			{
				return this.takeSamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set TakeSamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("TakeSamples must be a multiple of WaveFormat.Channels");
				}
				this.takeSamples = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000082B6 File Offset: 0x000064B6
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000082C4 File Offset: 0x000064C4
		public TimeSpan Take
		{
			get
			{
				return this.SamplesToTimeSpan(this.takeSamples);
			}
			set
			{
				this.takeSamples = this.TimeSpanToSamples(value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000082D3 File Offset: 0x000064D3
		// (set) Token: 0x0600027D RID: 637 RVA: 0x000082DB File Offset: 0x000064DB
		public int LeadOutSamples
		{
			get
			{
				return this.leadOutSamples;
			}
			set
			{
				if (this.phase != 0)
				{
					throw new InvalidOperationException("Can't set LeadOutSamples after calling Read");
				}
				if (value % this.WaveFormat.Channels != 0)
				{
					throw new ArgumentException("LeadOutSamples must be a multiple of WaveFormat.Channels");
				}
				this.leadOutSamples = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008311 File Offset: 0x00006511
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000831F File Offset: 0x0000651F
		public TimeSpan LeadOut
		{
			get
			{
				return this.SamplesToTimeSpan(this.leadOutSamples);
			}
			set
			{
				this.leadOutSamples = this.TimeSpanToSamples(value);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000832E File Offset: 0x0000652E
		public OffsetSampleProvider(ISampleProvider sourceProvider)
		{
			this.sourceProvider = sourceProvider;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000833D File Offset: 0x0000653D
		public WaveFormat WaveFormat
		{
			get
			{
				return this.sourceProvider.WaveFormat;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000834C File Offset: 0x0000654C
		public int Read(float[] buffer, int offset, int count)
		{
			int num = 0;
			if (this.phase == 0)
			{
				this.phase++;
			}
			if (this.phase == 1)
			{
				int num2 = Math.Min(count, this.DelayBySamples - this.phasePos);
				for (int i = 0; i < num2; i++)
				{
					buffer[offset + i] = 0f;
				}
				this.phasePos += num2;
				num += num2;
				if (this.phasePos >= this.DelayBySamples)
				{
					this.phase++;
					this.phasePos = 0;
				}
			}
			if (this.phase == 2)
			{
				if (this.SkipOverSamples > 0)
				{
					float[] array = new float[this.WaveFormat.SampleRate * this.WaveFormat.Channels];
					int num4;
					for (int j = 0; j < this.SkipOverSamples; j += num4)
					{
						int num3 = Math.Min(this.SkipOverSamples - j, array.Length);
						num4 = this.sourceProvider.Read(array, 0, num3);
						if (num4 == 0)
						{
							break;
						}
					}
				}
				this.phase++;
				this.phasePos = 0;
			}
			if (this.phase == 3)
			{
				int num5 = count - num;
				if (this.TakeSamples != 0)
				{
					num5 = Math.Min(num5, this.TakeSamples - this.phasePos);
				}
				int num6 = this.sourceProvider.Read(buffer, offset + num, num5);
				this.phasePos += num6;
				num += num6;
				if (num6 < num5)
				{
					this.phase++;
					this.phasePos = 0;
				}
			}
			if (this.phase == 4)
			{
				int num7 = Math.Min(count - num, this.LeadOutSamples - this.phasePos);
				for (int k = 0; k < num7; k++)
				{
					buffer[offset + num + k] = 0f;
				}
				this.phasePos += num7;
				num += num7;
				if (this.phasePos >= this.LeadOutSamples)
				{
					this.phase++;
					this.phasePos = 0;
				}
			}
			return num;
		}

		// Token: 0x04000387 RID: 903
		private readonly ISampleProvider sourceProvider;

		// Token: 0x04000388 RID: 904
		private int phase;

		// Token: 0x04000389 RID: 905
		private int phasePos;

		// Token: 0x0400038A RID: 906
		private int delayBySamples;

		// Token: 0x0400038B RID: 907
		private int skipOverSamples;

		// Token: 0x0400038C RID: 908
		private int takeSamples;

		// Token: 0x0400038D RID: 909
		private int leadOutSamples;
	}
}
