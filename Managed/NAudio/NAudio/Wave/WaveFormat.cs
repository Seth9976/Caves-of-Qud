using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x02000196 RID: 406
	[StructLayout(0, Pack = 2)]
	public class WaveFormat
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x0001743F File Offset: 0x0001563F
		public WaveFormat()
			: this(44100, 16, 2)
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001744F File Offset: 0x0001564F
		public WaveFormat(int sampleRate, int channels)
			: this(sampleRate, 16, channels)
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001745C File Offset: 0x0001565C
		public int ConvertLatencyToByteSize(int milliseconds)
		{
			int num = (int)((double)this.AverageBytesPerSecond / 1000.0 * (double)milliseconds);
			if (num % this.BlockAlign != 0)
			{
				num = num + this.BlockAlign - num % this.BlockAlign;
			}
			return num;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001749C File Offset: 0x0001569C
		public static WaveFormat CreateCustomFormat(WaveFormatEncoding tag, int sampleRate, int channels, int averageBytesPerSecond, int blockAlign, int bitsPerSample)
		{
			return new WaveFormat
			{
				waveFormatTag = tag,
				channels = (short)channels,
				sampleRate = sampleRate,
				averageBytesPerSecond = averageBytesPerSecond,
				blockAlign = (short)blockAlign,
				bitsPerSample = (short)bitsPerSample,
				extraSize = 0
			};
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x000174E6 File Offset: 0x000156E6
		public static WaveFormat CreateALawFormat(int sampleRate, int channels)
		{
			return WaveFormat.CreateCustomFormat(WaveFormatEncoding.ALaw, sampleRate, channels, sampleRate * channels, channels, 8);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000174F5 File Offset: 0x000156F5
		public static WaveFormat CreateMuLawFormat(int sampleRate, int channels)
		{
			return WaveFormat.CreateCustomFormat(WaveFormatEncoding.MuLaw, sampleRate, channels, sampleRate * channels, channels, 8);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00017504 File Offset: 0x00015704
		public WaveFormat(int rate, int bits, int channels)
		{
			if (channels < 1)
			{
				throw new ArgumentOutOfRangeException("channels", "Channels must be 1 or greater");
			}
			this.waveFormatTag = WaveFormatEncoding.Pcm;
			this.channels = (short)channels;
			this.sampleRate = rate;
			this.bitsPerSample = (short)bits;
			this.extraSize = 0;
			this.blockAlign = (short)(channels * (bits / 8));
			this.averageBytesPerSecond = this.sampleRate * (int)this.blockAlign;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00017570 File Offset: 0x00015770
		public static WaveFormat CreateIeeeFloatWaveFormat(int sampleRate, int channels)
		{
			WaveFormat waveFormat = new WaveFormat();
			waveFormat.waveFormatTag = WaveFormatEncoding.IeeeFloat;
			waveFormat.channels = (short)channels;
			waveFormat.bitsPerSample = 32;
			waveFormat.sampleRate = sampleRate;
			waveFormat.blockAlign = (short)(4 * channels);
			waveFormat.averageBytesPerSecond = sampleRate * (int)waveFormat.blockAlign;
			waveFormat.extraSize = 0;
			return waveFormat;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000175C4 File Offset: 0x000157C4
		public static WaveFormat MarshalFromPtr(IntPtr pointer)
		{
			WaveFormat waveFormat = (WaveFormat)Marshal.PtrToStructure(pointer, typeof(WaveFormat));
			WaveFormatEncoding encoding = waveFormat.Encoding;
			switch (encoding)
			{
			case WaveFormatEncoding.Pcm:
				waveFormat.extraSize = 0;
				break;
			case WaveFormatEncoding.Adpcm:
				waveFormat = (AdpcmWaveFormat)Marshal.PtrToStructure(pointer, typeof(AdpcmWaveFormat));
				break;
			default:
				if (encoding != WaveFormatEncoding.Gsm610)
				{
					if (encoding != WaveFormatEncoding.Extensible)
					{
						if (waveFormat.ExtraSize > 0)
						{
							waveFormat = (WaveFormatExtraData)Marshal.PtrToStructure(pointer, typeof(WaveFormatExtraData));
						}
					}
					else
					{
						waveFormat = (WaveFormatExtensible)Marshal.PtrToStructure(pointer, typeof(WaveFormatExtensible));
					}
				}
				else
				{
					waveFormat = (Gsm610WaveFormat)Marshal.PtrToStructure(pointer, typeof(Gsm610WaveFormat));
				}
				break;
			}
			return waveFormat;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00017680 File Offset: 0x00015880
		public static IntPtr MarshalToPtr(WaveFormat format)
		{
			int num = Marshal.SizeOf(format);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(format, intPtr, false);
			return intPtr;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x000176A4 File Offset: 0x000158A4
		public static WaveFormat FromFormatChunk(BinaryReader br, int formatChunkLength)
		{
			WaveFormatExtraData waveFormatExtraData = new WaveFormatExtraData();
			waveFormatExtraData.ReadWaveFormat(br, formatChunkLength);
			waveFormatExtraData.ReadExtraData(br);
			return waveFormatExtraData;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000176C8 File Offset: 0x000158C8
		private void ReadWaveFormat(BinaryReader br, int formatChunkLength)
		{
			if (formatChunkLength < 16)
			{
				throw new InvalidDataException("Invalid WaveFormat Structure");
			}
			this.waveFormatTag = (WaveFormatEncoding)br.ReadUInt16();
			this.channels = br.ReadInt16();
			this.sampleRate = br.ReadInt32();
			this.averageBytesPerSecond = br.ReadInt32();
			this.blockAlign = br.ReadInt16();
			this.bitsPerSample = br.ReadInt16();
			if (formatChunkLength > 16)
			{
				this.extraSize = br.ReadInt16();
				if ((int)this.extraSize != formatChunkLength - 18)
				{
					this.extraSize = (short)(formatChunkLength - 18);
				}
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00017758 File Offset: 0x00015958
		public WaveFormat(BinaryReader br)
		{
			int num = br.ReadInt32();
			this.ReadWaveFormat(br, num);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001777C File Offset: 0x0001597C
		public override string ToString()
		{
			WaveFormatEncoding waveFormatEncoding = this.waveFormatTag;
			if (waveFormatEncoding == WaveFormatEncoding.Pcm || waveFormatEncoding == WaveFormatEncoding.Extensible)
			{
				return string.Format("{0} bit PCM: {1}kHz {2} channels", this.bitsPerSample, this.sampleRate / 1000, this.channels);
			}
			return this.waveFormatTag.ToString();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000177E0 File Offset: 0x000159E0
		public override bool Equals(object obj)
		{
			WaveFormat waveFormat = obj as WaveFormat;
			return waveFormat != null && (this.waveFormatTag == waveFormat.waveFormatTag && this.channels == waveFormat.channels && this.sampleRate == waveFormat.sampleRate && this.averageBytesPerSecond == waveFormat.averageBytesPerSecond && this.blockAlign == waveFormat.blockAlign) && this.bitsPerSample == waveFormat.bitsPerSample;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001784F File Offset: 0x00015A4F
		public override int GetHashCode()
		{
			return (int)(this.waveFormatTag ^ (WaveFormatEncoding)this.channels) ^ this.sampleRate ^ this.averageBytesPerSecond ^ (int)this.blockAlign ^ (int)this.bitsPerSample;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0001787A File Offset: 0x00015A7A
		public WaveFormatEncoding Encoding
		{
			get
			{
				return this.waveFormatTag;
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00017884 File Offset: 0x00015A84
		public virtual void Serialize(BinaryWriter writer)
		{
			writer.Write((int)(18 + this.extraSize));
			writer.Write((short)this.Encoding);
			writer.Write((short)this.Channels);
			writer.Write(this.SampleRate);
			writer.Write(this.AverageBytesPerSecond);
			writer.Write((short)this.BlockAlign);
			writer.Write((short)this.BitsPerSample);
			writer.Write(this.extraSize);
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x000178F8 File Offset: 0x00015AF8
		public int Channels
		{
			get
			{
				return (int)this.channels;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00017900 File Offset: 0x00015B00
		public int SampleRate
		{
			get
			{
				return this.sampleRate;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00017908 File Offset: 0x00015B08
		public int AverageBytesPerSecond
		{
			get
			{
				return this.averageBytesPerSecond;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00017910 File Offset: 0x00015B10
		public virtual int BlockAlign
		{
			get
			{
				return (int)this.blockAlign;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00017918 File Offset: 0x00015B18
		public int BitsPerSample
		{
			get
			{
				return (int)this.bitsPerSample;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00017920 File Offset: 0x00015B20
		public int ExtraSize
		{
			get
			{
				return (int)this.extraSize;
			}
		}

		// Token: 0x04000989 RID: 2441
		protected WaveFormatEncoding waveFormatTag;

		// Token: 0x0400098A RID: 2442
		protected short channels;

		// Token: 0x0400098B RID: 2443
		protected int sampleRate;

		// Token: 0x0400098C RID: 2444
		protected int averageBytesPerSecond;

		// Token: 0x0400098D RID: 2445
		protected short blockAlign;

		// Token: 0x0400098E RID: 2446
		protected short bitsPerSample;

		// Token: 0x0400098F RID: 2447
		protected short extraSize;
	}
}
