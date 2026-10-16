using System;
using NAudio.Wave.Asio;

namespace NAudio.Wave
{
	// Token: 0x020001A5 RID: 421
	public class AsioAudioAvailableEventArgs : EventArgs
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x0001864C File Offset: 0x0001684C
		public AsioAudioAvailableEventArgs(IntPtr[] inputBuffers, IntPtr[] outputBuffers, int samplesPerBuffer, AsioSampleType asioSampleType)
		{
			this.InputBuffers = inputBuffers;
			this.OutputBuffers = outputBuffers;
			this.SamplesPerBuffer = samplesPerBuffer;
			this.AsioSampleType = asioSampleType;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00018671 File Offset: 0x00016871
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00018679 File Offset: 0x00016879
		public IntPtr[] InputBuffers { get; private set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00018682 File Offset: 0x00016882
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0001868A File Offset: 0x0001688A
		public IntPtr[] OutputBuffers { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x00018693 File Offset: 0x00016893
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001869B File Offset: 0x0001689B
		public bool WrittenToOutputBuffers { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000186A4 File Offset: 0x000168A4
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x000186AC File Offset: 0x000168AC
		public int SamplesPerBuffer { get; private set; }

		// Token: 0x06000883 RID: 2179 RVA: 0x000186B8 File Offset: 0x000168B8
		public unsafe int GetAsInterleavedSamples(float[] samples)
		{
			int num = this.InputBuffers.Length;
			if (samples.Length < this.SamplesPerBuffer * num)
			{
				throw new ArgumentException("Buffer not big enough");
			}
			int num2 = 0;
			if (this.AsioSampleType == AsioSampleType.Int32LSB)
			{
				for (int i = 0; i < this.SamplesPerBuffer; i++)
				{
					for (int j = 0; j < num; j++)
					{
						samples[num2++] = (float)(*(int*)((byte*)(void*)this.InputBuffers[j] + (IntPtr)i * 4)) / 2.1474836E+09f;
					}
				}
			}
			else if (this.AsioSampleType == AsioSampleType.Int16LSB)
			{
				for (int k = 0; k < this.SamplesPerBuffer; k++)
				{
					for (int l = 0; l < num; l++)
					{
						samples[num2++] = (float)(*(short*)((byte*)(void*)this.InputBuffers[l] + (IntPtr)k * 2)) / 32767f;
					}
				}
			}
			else if (this.AsioSampleType == AsioSampleType.Int24LSB)
			{
				for (int m = 0; m < this.SamplesPerBuffer; m++)
				{
					for (int n = 0; n < num; n++)
					{
						byte* ptr = (byte*)(void*)this.InputBuffers[n] + (IntPtr)m * 3;
						int num3 = (int)(*ptr) | ((int)ptr[1] << 8) | ((int)((sbyte)ptr[2]) << 16);
						samples[num2++] = (float)num3 / 8388608f;
					}
				}
			}
			else
			{
				if (this.AsioSampleType != AsioSampleType.Float32LSB)
				{
					throw new NotImplementedException(string.Format("ASIO Sample Type {0} not supported", this.AsioSampleType));
				}
				for (int num4 = 0; num4 < this.SamplesPerBuffer; num4++)
				{
					for (int num5 = 0; num5 < num; num5++)
					{
						samples[num2++] = *(float*)((byte*)(void*)this.InputBuffers[num5] + (IntPtr)num4 * 4);
					}
				}
			}
			return this.SamplesPerBuffer * num;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00018890 File Offset: 0x00016A90
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x00018898 File Offset: 0x00016A98
		public AsioSampleType AsioSampleType { get; private set; }

		// Token: 0x06000886 RID: 2182 RVA: 0x000188A4 File Offset: 0x00016AA4
		[Obsolete("Better performance if you use the overload that takes an array, and reuse the same one")]
		public float[] GetAsInterleavedSamples()
		{
			int num = this.InputBuffers.Length;
			float[] array = new float[this.SamplesPerBuffer * num];
			this.GetAsInterleavedSamples(array);
			return array;
		}
	}
}
