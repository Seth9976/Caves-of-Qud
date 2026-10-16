using System;

namespace NAudio.Wave.SampleProviders
{
	// Token: 0x0200007C RID: 124
	public class SignalGenerator : ISampleProvider
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000896F File Offset: 0x00006B6F
		public SignalGenerator()
			: this(44100, 2)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00008980 File Offset: 0x00006B80
		public SignalGenerator(int sampleRate, int channel)
		{
			this.phi = 0.0;
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channel);
			this.Type = SignalGeneratorType.Sin;
			this.Frequency = 440.0;
			this.Gain = 1.0;
			this.PhaseReverse = new bool[channel];
			this.SweepLengthSecs = 2.0;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008A06 File Offset: 0x00006C06
		public WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008A0E File Offset: 0x00006C0E
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00008A16 File Offset: 0x00006C16
		public double Frequency { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008A1F File Offset: 0x00006C1F
		public double FrequencyLog
		{
			get
			{
				return Math.Log(this.Frequency);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008A2C File Offset: 0x00006C2C
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00008A34 File Offset: 0x00006C34
		public double FrequencyEnd { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00008A3D File Offset: 0x00006C3D
		public double FrequencyEndLog
		{
			get
			{
				return Math.Log(this.FrequencyEnd);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00008A4A File Offset: 0x00006C4A
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00008A52 File Offset: 0x00006C52
		public double Gain { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008A5B File Offset: 0x00006C5B
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00008A63 File Offset: 0x00006C63
		public bool[] PhaseReverse { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008A6C File Offset: 0x00006C6C
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00008A74 File Offset: 0x00006C74
		public SignalGeneratorType Type { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008A7D File Offset: 0x00006C7D
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00008A85 File Offset: 0x00006C85
		public double SweepLengthSecs { get; set; }

		// Token: 0x060002A7 RID: 679 RVA: 0x00008A90 File Offset: 0x00006C90
		public int Read(float[] buffer, int offset, int count)
		{
			int num = offset;
			for (int i = 0; i < count / this.waveFormat.Channels; i++)
			{
				double num4;
				switch (this.Type)
				{
				case SignalGeneratorType.Pink:
				{
					double num2 = this.NextRandomTwo();
					this.pinkNoiseBuffer[0] = 0.99886 * this.pinkNoiseBuffer[0] + num2 * 0.0555179;
					this.pinkNoiseBuffer[1] = 0.99332 * this.pinkNoiseBuffer[1] + num2 * 0.0750759;
					this.pinkNoiseBuffer[2] = 0.969 * this.pinkNoiseBuffer[2] + num2 * 0.153852;
					this.pinkNoiseBuffer[3] = 0.8665 * this.pinkNoiseBuffer[3] + num2 * 0.3104856;
					this.pinkNoiseBuffer[4] = 0.55 * this.pinkNoiseBuffer[4] + num2 * 0.5329522;
					this.pinkNoiseBuffer[5] = -0.7616 * this.pinkNoiseBuffer[5] - num2 * 0.016898;
					double num3 = this.pinkNoiseBuffer[0] + this.pinkNoiseBuffer[1] + this.pinkNoiseBuffer[2] + this.pinkNoiseBuffer[3] + this.pinkNoiseBuffer[4] + this.pinkNoiseBuffer[5] + this.pinkNoiseBuffer[6] + num2 * 0.5362;
					this.pinkNoiseBuffer[6] = num2 * 0.115926;
					num4 = this.Gain * (num3 / 5.0);
					break;
				}
				case SignalGeneratorType.White:
					num4 = this.Gain * this.NextRandomTwo();
					break;
				case SignalGeneratorType.Sweep:
				{
					double num5 = Math.Exp(this.FrequencyLog + (double)this.nSample * (this.FrequencyEndLog - this.FrequencyLog) / (this.SweepLengthSecs * (double)this.waveFormat.SampleRate));
					double num6 = 6.283185307179586 * num5 / (double)this.waveFormat.SampleRate;
					this.phi += num6;
					num4 = this.Gain * Math.Sin(this.phi);
					this.nSample++;
					if ((double)this.nSample > this.SweepLengthSecs * (double)this.waveFormat.SampleRate)
					{
						this.nSample = 0;
						this.phi = 0.0;
					}
					break;
				}
				case SignalGeneratorType.Sin:
				{
					double num6 = 6.283185307179586 * this.Frequency / (double)this.waveFormat.SampleRate;
					num4 = this.Gain * Math.Sin((double)this.nSample * num6);
					this.nSample++;
					break;
				}
				case SignalGeneratorType.Square:
				{
					double num6 = 2.0 * this.Frequency / (double)this.waveFormat.SampleRate;
					double num7 = (double)this.nSample * num6 % 2.0 - 1.0;
					num4 = ((num7 > 0.0) ? this.Gain : (-this.Gain));
					this.nSample++;
					break;
				}
				case SignalGeneratorType.Triangle:
				{
					double num6 = 2.0 * this.Frequency / (double)this.waveFormat.SampleRate;
					double num7 = (double)this.nSample * num6 % 2.0;
					num4 = 2.0 * num7;
					if (num4 > 1.0)
					{
						num4 = 2.0 - num4;
					}
					if (num4 < -1.0)
					{
						num4 = -2.0 - num4;
					}
					num4 *= this.Gain;
					this.nSample++;
					break;
				}
				case SignalGeneratorType.SawTooth:
				{
					double num6 = 2.0 * this.Frequency / (double)this.waveFormat.SampleRate;
					double num7 = (double)this.nSample * num6 % 2.0 - 1.0;
					num4 = this.Gain * num7;
					this.nSample++;
					break;
				}
				default:
					num4 = 0.0;
					break;
				}
				for (int j = 0; j < this.waveFormat.Channels; j++)
				{
					if (this.PhaseReverse[j])
					{
						buffer[num++] = (float)(-(float)num4);
					}
					else
					{
						buffer[num++] = (float)num4;
					}
				}
			}
			return count;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00008EFD File Offset: 0x000070FD
		private double NextRandomTwo()
		{
			return 2.0 * this.random.NextDouble() - 1.0;
		}

		// Token: 0x04000399 RID: 921
		private const double TwoPi = 6.283185307179586;

		// Token: 0x0400039A RID: 922
		private readonly WaveFormat waveFormat;

		// Token: 0x0400039B RID: 923
		private readonly Random random = new Random();

		// Token: 0x0400039C RID: 924
		private readonly double[] pinkNoiseBuffer = new double[7];

		// Token: 0x0400039D RID: 925
		private int nSample;

		// Token: 0x0400039E RID: 926
		private double phi;
	}
}
