using System;
using System.Collections.Generic;
using System.IO;
using NAudio.FileFormats.Wav;

namespace NAudio.Wave
{
	// Token: 0x020001E1 RID: 481
	public class WaveFileReader : WaveStream
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0001F08C File Offset: 0x0001D28C
		public WaveFileReader(string waveFile)
			: this(File.OpenRead(waveFile), true)
		{
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001F09B File Offset: 0x0001D29B
		public WaveFileReader(Stream inputStream)
			: this(inputStream, false)
		{
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		private WaveFileReader(Stream inputStream, bool ownInput)
		{
			this.waveStream = inputStream;
			WaveFileChunkReader waveFileChunkReader = new WaveFileChunkReader();
			try
			{
				waveFileChunkReader.ReadWaveHeader(inputStream);
				this.waveFormat = waveFileChunkReader.WaveFormat;
				this.dataPosition = waveFileChunkReader.DataChunkPosition;
				this.dataChunkLength = waveFileChunkReader.DataChunkLength;
				this.chunks = waveFileChunkReader.RiffChunks;
			}
			catch
			{
				if (ownInput)
				{
					inputStream.Dispose();
				}
				throw;
			}
			this.Position = 0L;
			this.ownInput = ownInput;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001F144 File Offset: 0x0001D344
		public List<RiffChunk> ExtraChunks
		{
			get
			{
				return this.chunks;
			}
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001F14C File Offset: 0x0001D34C
		public byte[] GetChunkData(RiffChunk chunk)
		{
			long position = this.waveStream.Position;
			this.waveStream.Position = chunk.StreamPosition;
			byte[] array = new byte[chunk.Length];
			this.waveStream.Read(array, 0, array.Length);
			this.waveStream.Position = position;
			return array;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.waveStream != null)
			{
				if (this.ownInput)
				{
					this.waveStream.Close();
				}
				this.waveStream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001F1CE File Offset: 0x0001D3CE
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001F1D6 File Offset: 0x0001D3D6
		public override long Length
		{
			get
			{
				return this.dataChunkLength;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		public long SampleCount
		{
			get
			{
				if (this.waveFormat.Encoding == WaveFormatEncoding.Pcm || this.waveFormat.Encoding == WaveFormatEncoding.Extensible || this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
				{
					return this.dataChunkLength / (long)this.BlockAlign;
				}
				throw new InvalidOperationException("Sample count is calculated only for the standard encodings");
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0001F234 File Offset: 0x0001D434
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0001F248 File Offset: 0x0001D448
		public override long Position
		{
			get
			{
				return this.waveStream.Position - this.dataPosition;
			}
			set
			{
				lock (this.lockObject)
				{
					value = Math.Min(value, this.Length);
					value -= value % (long)this.waveFormat.BlockAlign;
					this.waveStream.Position = value + this.dataPosition;
				}
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		public override int Read(byte[] array, int offset, int count)
		{
			if (count % this.waveFormat.BlockAlign != 0)
			{
				throw new ArgumentException(string.Format("Must read complete blocks: requested {0}, block align is {1}", count, this.WaveFormat.BlockAlign));
			}
			int num;
			lock (this.lockObject)
			{
				if (this.Position + (long)count > this.dataChunkLength)
				{
					count = (int)(this.dataChunkLength - this.Position);
				}
				num = this.waveStream.Read(array, offset, count);
			}
			return num;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001F34C File Offset: 0x0001D54C
		public float[] ReadNextSampleFrame()
		{
			WaveFormatEncoding encoding = this.waveFormat.Encoding;
			switch (encoding)
			{
			case WaveFormatEncoding.Pcm:
			case WaveFormatEncoding.IeeeFloat:
				goto IL_0036;
			case WaveFormatEncoding.Adpcm:
				break;
			default:
				if (encoding == WaveFormatEncoding.Extensible)
				{
					goto IL_0036;
				}
				break;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
			IL_0036:
			float[] array = new float[this.waveFormat.Channels];
			int num = this.waveFormat.Channels * (this.waveFormat.BitsPerSample / 8);
			byte[] array2 = new byte[num];
			int num2 = this.Read(array2, 0, num);
			if (num2 == 0)
			{
				return null;
			}
			if (num2 < num)
			{
				throw new InvalidDataException("Unexpected end of file");
			}
			int num3 = 0;
			for (int i = 0; i < this.waveFormat.Channels; i++)
			{
				if (this.waveFormat.BitsPerSample == 16)
				{
					array[i] = (float)BitConverter.ToInt16(array2, num3) / 32768f;
					num3 += 2;
				}
				else if (this.waveFormat.BitsPerSample == 24)
				{
					array[i] = (float)(((int)((sbyte)array2[num3 + 2]) << 16) | ((int)array2[num3 + 1] << 8) | (int)array2[num3]) / 8388608f;
					num3 += 3;
				}
				else if (this.waveFormat.BitsPerSample == 32 && this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
				{
					array[i] = BitConverter.ToSingle(array2, num3);
					num3 += 4;
				}
				else
				{
					if (this.waveFormat.BitsPerSample != 32)
					{
						throw new InvalidOperationException("Unsupported bit depth");
					}
					array[i] = (float)BitConverter.ToInt32(array2, num3) / 2.1474836E+09f;
					num3 += 4;
				}
			}
			return array;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		[Obsolete("Use ReadNextSampleFrame instead (this version does not support stereo properly)")]
		public bool TryReadFloat(out float sampleValue)
		{
			float[] array = this.ReadNextSampleFrame();
			sampleValue = ((array != null) ? array[0] : 0f);
			return array != null;
		}

		// Token: 0x04000B6C RID: 2924
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B6D RID: 2925
		private readonly bool ownInput;

		// Token: 0x04000B6E RID: 2926
		private readonly long dataPosition;

		// Token: 0x04000B6F RID: 2927
		private readonly long dataChunkLength;

		// Token: 0x04000B70 RID: 2928
		private readonly List<RiffChunk> chunks = new List<RiffChunk>();

		// Token: 0x04000B71 RID: 2929
		private readonly object lockObject = new object();

		// Token: 0x04000B72 RID: 2930
		private Stream waveStream;
	}
}
