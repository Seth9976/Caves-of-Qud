using System;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001A4 RID: 420
	public class AiffFileWriter : Stream
	{
		// Token: 0x0600085E RID: 2142 RVA: 0x00017F68 File Offset: 0x00016168
		public static void CreateAiffFile(string filename, WaveStream sourceProvider)
		{
			using (AiffFileWriter aiffFileWriter = new AiffFileWriter(filename, sourceProvider.WaveFormat))
			{
				byte[] array = new byte[16384];
				while (sourceProvider.Position < sourceProvider.Length)
				{
					int num = Math.Min((int)(sourceProvider.Length - sourceProvider.Position), array.Length);
					int num2 = sourceProvider.Read(array, 0, num);
					if (num2 == 0)
					{
						break;
					}
					aiffFileWriter.Write(array, 0, num2);
				}
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x00017FE8 File Offset: 0x000161E8
		public AiffFileWriter(Stream outStream, WaveFormat format)
		{
			this.outStream = outStream;
			this.format = format;
			this.writer = new BinaryWriter(outStream, Encoding.UTF8);
			this.writer.Write(Encoding.UTF8.GetBytes("FORM"));
			this.writer.Write(0);
			this.writer.Write(Encoding.UTF8.GetBytes("AIFF"));
			this.CreateCommChunk();
			this.WriteSsndChunkHeader();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00018079 File Offset: 0x00016279
		public AiffFileWriter(string filename, WaveFormat format)
			: this(new FileStream(filename, 2, 2, 1), format)
		{
			this.filename = filename;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00018094 File Offset: 0x00016294
		private void WriteSsndChunkHeader()
		{
			this.writer.Write(Encoding.UTF8.GetBytes("SSND"));
			this.dataSizePos = this.outStream.Position;
			this.writer.Write(0);
			this.writer.Write(0);
			this.writer.Write(this.SwapEndian(this.format.BlockAlign));
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00018100 File Offset: 0x00016300
		private byte[] SwapEndian(short n)
		{
			return new byte[]
			{
				(byte)(n >> 8),
				(byte)(n & 255)
			};
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00018128 File Offset: 0x00016328
		private byte[] SwapEndian(int n)
		{
			return new byte[]
			{
				(byte)((n >> 24) & 255),
				(byte)((n >> 16) & 255),
				(byte)((n >> 8) & 255),
				(byte)(n & 255)
			};
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00018174 File Offset: 0x00016374
		private void CreateCommChunk()
		{
			this.writer.Write(Encoding.UTF8.GetBytes("COMM"));
			this.writer.Write(this.SwapEndian(18));
			this.writer.Write(this.SwapEndian((short)this.format.Channels));
			this.commSampleCountPos = this.outStream.Position;
			this.writer.Write(0);
			this.writer.Write(this.SwapEndian((short)this.format.BitsPerSample));
			this.writer.Write(IEEE.ConvertToIeeeExtended((double)this.format.SampleRate));
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00018221 File Offset: 0x00016421
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00018229 File Offset: 0x00016429
		public override long Length
		{
			get
			{
				return (long)this.dataChunkSize;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00018232 File Offset: 0x00016432
		public WaveFormat WaveFormat
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001823A File Offset: 0x0001643A
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0001823D File Offset: 0x0001643D
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00018240 File Offset: 0x00016440
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00018243 File Offset: 0x00016443
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new InvalidOperationException("Cannot read from an AiffFileWriter");
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001824F File Offset: 0x0001644F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new InvalidOperationException("Cannot seek within an AiffFileWriter");
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0001825B File Offset: 0x0001645B
		public override void SetLength(long value)
		{
			throw new InvalidOperationException("Cannot set length of an AiffFileWriter");
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x00018267 File Offset: 0x00016467
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x00018270 File Offset: 0x00016470
		public override long Position
		{
			get
			{
				return (long)this.dataChunkSize;
			}
			set
			{
				throw new InvalidOperationException("Repositioning an AiffFileWriter is not supported");
			}
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001827C File Offset: 0x0001647C
		public override void Write(byte[] data, int offset, int count)
		{
			byte[] array = new byte[data.Length];
			int num = this.format.BitsPerSample / 8;
			for (int i = 0; i < data.Length; i++)
			{
				int num2 = (int)Math.Floor((double)i / (double)num) * num + (num - i % num - 1);
				array[i] = data[num2];
			}
			this.outStream.Write(array, offset, count);
			this.dataChunkSize += count;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000182E8 File Offset: 0x000164E8
		public void WriteSample(float sample)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				this.writer.Write(this.SwapEndian((short)(32767f * sample)));
				this.dataChunkSize += 2;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				byte[] bytes = BitConverter.GetBytes((int)(2.1474836E+09f * sample));
				this.value24[2] = bytes[1];
				this.value24[1] = bytes[2];
				this.value24[0] = bytes[3];
				this.writer.Write(this.value24);
				this.dataChunkSize += 3;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				this.writer.Write(this.SwapEndian(65535 * (int)sample));
				this.dataChunkSize += 4;
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM or IEEE float audio data supported");
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000183E0 File Offset: 0x000165E0
		public void WriteSamples(float[] samples, int offset, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.WriteSample(samples[offset + i]);
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00018404 File Offset: 0x00016604
		public void WriteSamples(short[] samples, int offset, int count)
		{
			if (this.WaveFormat.BitsPerSample == 16)
			{
				for (int i = 0; i < count; i++)
				{
					this.writer.Write(this.SwapEndian(samples[i + offset]));
				}
				this.dataChunkSize += count * 2;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 24)
			{
				for (int j = 0; j < count; j++)
				{
					byte[] bytes = BitConverter.GetBytes(65535 * (int)samples[j + offset]);
					this.value24[2] = bytes[1];
					this.value24[1] = bytes[2];
					this.value24[0] = bytes[3];
					this.writer.Write(this.value24);
				}
				this.dataChunkSize += count * 3;
				return;
			}
			if (this.WaveFormat.BitsPerSample == 32 && this.WaveFormat.Encoding == WaveFormatEncoding.Extensible)
			{
				for (int k = 0; k < count; k++)
				{
					this.writer.Write(this.SwapEndian(65535 * (int)samples[k + offset]));
				}
				this.dataChunkSize += count * 4;
				return;
			}
			throw new InvalidOperationException("Only 16, 24 or 32 bit PCM audio data supported");
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00018526 File Offset: 0x00016726
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00018534 File Offset: 0x00016734
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

		// Token: 0x06000876 RID: 2166 RVA: 0x00018580 File Offset: 0x00016780
		protected virtual void UpdateHeader(BinaryWriter writer)
		{
			this.Flush();
			writer.Seek(4, 0);
			writer.Write(this.SwapEndian((int)(this.outStream.Length - 8L)));
			this.UpdateCommChunk(writer);
			this.UpdateSsndChunk(writer);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000185BA File Offset: 0x000167BA
		private void UpdateCommChunk(BinaryWriter writer)
		{
			writer.Seek((int)this.commSampleCountPos, 0);
			writer.Write(this.SwapEndian(this.dataChunkSize * 8 / this.format.BitsPerSample / this.format.Channels));
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000185F7 File Offset: 0x000167F7
		private void UpdateSsndChunk(BinaryWriter writer)
		{
			writer.Seek((int)this.dataSizePos, 0);
			writer.Write(this.SwapEndian(this.dataChunkSize));
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001861C File Offset: 0x0001681C
		~AiffFileWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x04000A52 RID: 2642
		private Stream outStream;

		// Token: 0x04000A53 RID: 2643
		private BinaryWriter writer;

		// Token: 0x04000A54 RID: 2644
		private long dataSizePos;

		// Token: 0x04000A55 RID: 2645
		private long commSampleCountPos;

		// Token: 0x04000A56 RID: 2646
		private int dataChunkSize = 8;

		// Token: 0x04000A57 RID: 2647
		private WaveFormat format;

		// Token: 0x04000A58 RID: 2648
		private string filename;

		// Token: 0x04000A59 RID: 2649
		private byte[] value24 = new byte[3];
	}
}
