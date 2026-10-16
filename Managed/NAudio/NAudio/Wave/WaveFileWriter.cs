using System;
using System.IO;
using System.Text;
using NAudio.Wave.SampleProviders;

namespace NAudio.Wave
{
	// Token: 0x020001A8 RID: 424
	public class WaveFileWriter : Stream
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x00018EE8 File Offset: 0x000170E8
		public static void CreateWaveFile16(string filename, ISampleProvider sourceProvider)
		{
			WaveFileWriter.CreateWaveFile(filename, new SampleToWaveProvider16(sourceProvider));
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00018EF8 File Offset: 0x000170F8
		public static void CreateWaveFile(string filename, IWaveProvider sourceProvider)
		{
			using (WaveFileWriter waveFileWriter = new WaveFileWriter(filename, sourceProvider.WaveFormat))
			{
				long num = 0L;
				byte[] array = new byte[sourceProvider.WaveFormat.AverageBytesPerSecond * 4];
				for (;;)
				{
					int num2 = sourceProvider.Read(array, 0, array.Length);
					if (num2 == 0)
					{
						break;
					}
					num += (long)num2;
					waveFileWriter.Write(array, 0, num2);
				}
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00018F64 File Offset: 0x00017164
		public WaveFileWriter(Stream outStream, WaveFormat format)
		{
			this.outStream = outStream;
			this.format = format;
			this.writer = new BinaryWriter(outStream, Encoding.UTF8);
			this.writer.Write(Encoding.UTF8.GetBytes("RIFF"));
			this.writer.Write(0);
			this.writer.Write(Encoding.UTF8.GetBytes("WAVE"));
			this.writer.Write(Encoding.UTF8.GetBytes("fmt "));
			format.Serialize(this.writer);
			this.CreateFactChunk();
			this.WriteDataChunkHeader();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00019014 File Offset: 0x00017214
		public WaveFileWriter(string filename, WaveFormat format)
			: this(new FileStream(filename, 2, 2, 1), format)
		{
			this.filename = filename;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001902D File Offset: 0x0001722D
		private void WriteDataChunkHeader()
		{
			this.writer.Write(Encoding.UTF8.GetBytes("data"));
			this.dataSizePos = this.outStream.Position;
			this.writer.Write(0);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00019068 File Offset: 0x00017268
		private void CreateFactChunk()
		{
			if (this.HasFactChunk())
			{
				this.writer.Write(Encoding.UTF8.GetBytes("fact"));
				this.writer.Write(4);
				this.factSampleCountPos = this.outStream.Position;
				this.writer.Write(0);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000190C0 File Offset: 0x000172C0
		private bool HasFactChunk()
		{
			return this.format.Encoding != WaveFormatEncoding.Pcm && this.format.BitsPerSample != 0;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000190E3 File Offset: 0x000172E3
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x000190EB File Offset: 0x000172EB
		public override long Length
		{
			get
			{
				return this.dataChunkSize;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000190F3 File Offset: 0x000172F3
		public WaveFormat WaveFormat
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000190FB File Offset: 0x000172FB
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000190FE File Offset: 0x000172FE
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00019101 File Offset: 0x00017301
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00019104 File Offset: 0x00017304
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Cannot read from a WaveFileWriter");
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00019110 File Offset: 0x00017310
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Cannot seek within a WaveFileWriter");
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001911C File Offset: 0x0001731C
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("Cannot set length of a WaveFileWriter");
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00019128 File Offset: 0x00017328
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00019130 File Offset: 0x00017330
		public override long Position
		{
			get
			{
				return this.dataChunkSize;
			}
			set
			{
				throw new InvalidOperationException("Repositioning a WaveFileWriter is not supported");
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001913C File Offset: 0x0001733C
		[Obsolete("Use Write instead")]
		public void WriteData(byte[] data, int offset, int count)
		{
			this.Write(data, offset, count);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00019148 File Offset: 0x00017348
		public override void Write(byte[] data, int offset, int count)
		{
			if (this.outStream.Length + (long)count > (long)((ulong)(-1)))
			{
				throw new ArgumentException("WAV file too large", "count");
			}
			this.outStream.Write(data, offset, count);
			this.dataChunkSize += (long)count;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00019194 File Offset: 0x00017394
		public void WriteSample(float sample)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				this.writer.Write((short)(32767f * sample));
				this.dataChunkSize += 2L;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				byte[] bytes = BitConverter.GetBytes((int)(2.1474836E+09f * sample));
				this.value24[0] = bytes[1];
				this.value24[1] = bytes[2];
				this.value24[2] = bytes[3];
				this.writer.Write(this.value24);
				this.dataChunkSize += 3L;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				this.writer.Write(65535 * (int)sample);
				this.dataChunkSize += 4L;
				return;
			}
			if (this.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
			{
				this.writer.Write(sample);
				this.dataChunkSize += 4L;
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000192AC File Offset: 0x000174AC
		public void WriteSamples(float[] samples, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteSample(samples[offset + i]);
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000192D0 File Offset: 0x000174D0
		[Obsolete("Use WriteSamples instead")]
		public void WriteData(short[] samples, int offset, int count)
		{
			this.WriteSamples(samples, offset, count);
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000192DC File Offset: 0x000174DC
		public void WriteSamples(short[] samples, int offset, int count)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				for (int i = 0; i < count; i++)
				{
					this.writer.Write(samples[i + offset]);
				}
				this.dataChunkSize += (long)(count * 2);
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				for (int j = 0; j < count; j++)
				{
					byte[] bytes = BitConverter.GetBytes(65535 * (int)samples[j + offset]);
					this.value24[0] = bytes[1];
					this.value24[1] = bytes[2];
					this.value24[2] = bytes[3];
					this.writer.Write(this.value24);
				}
				this.dataChunkSize += (long)(count * 3);
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				for (int k = 0; k < count; k++)
				{
					this.writer.Write(65535 * (int)samples[k + offset]);
				}
				this.dataChunkSize += (long)(count * 4);
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
			{
				for (int l = 0; l < count; l++)
				{
					this.writer.Write((float)samples[l + offset] / 32768f);
				}
				this.dataChunkSize += (long)(count * 4);
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0001944C File Offset: 0x0001764C
		public override void Flush()
		{
			long position = this.writer.BaseStream.Position;
			this.UpdateHeader(this.writer);
			this.writer.BaseStream.Position = position;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00019488 File Offset: 0x00017688
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.outStream != null)
			{
				try
				{
					this.UpdateHeader(this.writer);
				}
				finally
				{
					this.outStream.Close();
					this.outStream = null;
				}
			}
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000194D4 File Offset: 0x000176D4
		protected virtual void UpdateHeader(BinaryWriter writer)
		{
			writer.Flush();
			this.UpdateRiffChunk(writer);
			this.UpdateFactChunk(writer);
			this.UpdateDataChunk(writer);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000194F1 File Offset: 0x000176F1
		private void UpdateDataChunk(BinaryWriter writer)
		{
			writer.Seek((int)this.dataSizePos, 0);
			writer.Write((uint)this.dataChunkSize);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0001950F File Offset: 0x0001770F
		private void UpdateRiffChunk(BinaryWriter writer)
		{
			writer.Seek(4, 0);
			writer.Write((uint)(this.outStream.Length - 8L));
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00019530 File Offset: 0x00017730
		private void UpdateFactChunk(BinaryWriter writer)
		{
			if (this.HasFactChunk())
			{
				int num = this.format.BitsPerSample * this.format.Channels;
				if (num != 0)
				{
					writer.Seek((int)this.factSampleCountPos, 0);
					writer.Write((int)(this.dataChunkSize * 8L / (long)num));
				}
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00019584 File Offset: 0x00017784
		~WaveFileWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x04000A6D RID: 2669
		private Stream outStream;

		// Token: 0x04000A6E RID: 2670
		private readonly BinaryWriter writer;

		// Token: 0x04000A6F RID: 2671
		private long dataSizePos;

		// Token: 0x04000A70 RID: 2672
		private long factSampleCountPos;

		// Token: 0x04000A71 RID: 2673
		private long dataChunkSize;

		// Token: 0x04000A72 RID: 2674
		private readonly WaveFormat format;

		// Token: 0x04000A73 RID: 2675
		private readonly string filename;

		// Token: 0x04000A74 RID: 2676
		private readonly byte[] value24 = new byte[3];
	}
}
