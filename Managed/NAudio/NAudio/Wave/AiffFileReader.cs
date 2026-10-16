using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001DB RID: 475
	public class AiffFileReader : WaveStream
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E0C1 File Offset: 0x0001C2C1
		public AiffFileReader(string aiffFile)
			: this(File.OpenRead(aiffFile))
		{
			this.ownInput = true;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E0D8 File Offset: 0x0001C2D8
		public AiffFileReader(Stream inputStream)
		{
			this.waveStream = inputStream;
			AiffFileReader.ReadAiffHeader(this.waveStream, out this.waveFormat, out this.dataPosition, out this.dataChunkLength, this.chunks);
			this.Position = 0L;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001E134 File Offset: 0x0001C334
		public static void ReadAiffHeader(Stream stream, out WaveFormat format, out long dataChunkPosition, out int dataChunkLength, List<AiffFileReader.AiffChunk> chunks)
		{
			dataChunkPosition = -1L;
			format = null;
			BinaryReader binaryReader = new BinaryReader(stream);
			if (AiffFileReader.ReadChunkName(binaryReader) != "FORM")
			{
				throw new FormatException("Not an AIFF file - no FORM header.");
			}
			AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
			string text = AiffFileReader.ReadChunkName(binaryReader);
			if (text != "AIFC" && text != "AIFF")
			{
				throw new FormatException("Not an AIFF file - no AIFF/AIFC header.");
			}
			dataChunkLength = 0;
			while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
			{
				AiffFileReader.AiffChunk aiffChunk = AiffFileReader.ReadChunkHeader(binaryReader);
				if (aiffChunk.ChunkName == "COMM")
				{
					short num = AiffFileReader.ConvertShort(binaryReader.ReadBytes(2));
					AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
					short num2 = AiffFileReader.ConvertShort(binaryReader.ReadBytes(2));
					double num3 = IEEE.ConvertFromIeeeExtended(binaryReader.ReadBytes(10));
					format = new WaveFormat((int)num3, (int)num2, (int)num);
					if (aiffChunk.ChunkLength > 18U && text == "AIFC")
					{
						string text2 = new string(binaryReader.ReadChars(4)).ToLower();
						if (text2 != "none")
						{
							throw new FormatException("Compressed AIFC is not supported.");
						}
						binaryReader.ReadBytes((int)(aiffChunk.ChunkLength - 22U));
					}
					else
					{
						binaryReader.ReadBytes((int)(aiffChunk.ChunkLength - 18U));
					}
				}
				else if (aiffChunk.ChunkName == "SSND")
				{
					uint num4 = AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
					AiffFileReader.ConvertInt(binaryReader.ReadBytes(4));
					dataChunkPosition = (long)((ulong)(aiffChunk.ChunkStart + 16U + num4));
					dataChunkLength = (int)(aiffChunk.ChunkLength - 8U);
					binaryReader.ReadBytes((int)(aiffChunk.ChunkLength - 8U));
				}
				else
				{
					if (chunks != null)
					{
						chunks.Add(aiffChunk);
					}
					binaryReader.ReadBytes((int)aiffChunk.ChunkLength);
				}
				if (aiffChunk.ChunkName == "\0\0\0\0")
				{
					break;
				}
			}
			if (format == null)
			{
				throw new FormatException("Invalid AIFF file - No COMM chunk found.");
			}
			if (dataChunkPosition == -1L)
			{
				throw new FormatException("Invalid AIFF file - No SSND chunk found.");
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001E33E File Offset: 0x0001C53E
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001E36C File Offset: 0x0001C56C
		public override WaveFormat WaveFormat
		{
			get
			{
				return this.waveFormat;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001E374 File Offset: 0x0001C574
		public override long Length
		{
			get
			{
				return (long)this.dataChunkLength;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0001E380 File Offset: 0x0001C580
		public long SampleCount
		{
			get
			{
				if (this.waveFormat.Encoding == WaveFormatEncoding.Pcm || this.waveFormat.Encoding == WaveFormatEncoding.Extensible || this.waveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
				{
					return (long)(this.dataChunkLength / this.BlockAlign);
				}
				throw new FormatException("Sample count is calculated only for the standard encodings");
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0001E3D4 File Offset: 0x0001C5D4
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x0001E3E8 File Offset: 0x0001C5E8
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

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E450 File Offset: 0x0001C650
		public override int Read(byte[] array, int offset, int count)
		{
			if (count % this.waveFormat.BlockAlign != 0)
			{
				throw new ArgumentException(string.Format("Must read complete blocks: requested {0}, block align is {1}", count, this.WaveFormat.BlockAlign));
			}
			int num3;
			lock (this.lockObject)
			{
				if (this.Position + (long)count > (long)this.dataChunkLength)
				{
					count = this.dataChunkLength - (int)this.Position;
				}
				byte[] array2 = new byte[count];
				int num = this.waveStream.Read(array2, offset, count);
				int num2 = this.WaveFormat.BitsPerSample / 8;
				for (int i = 0; i < num; i += num2)
				{
					if (this.WaveFormat.BitsPerSample == 8)
					{
						array[i] = array2[i];
					}
					else if (this.WaveFormat.BitsPerSample == 16)
					{
						array[i] = array2[i + 1];
						array[i + 1] = array2[i];
					}
					else if (this.WaveFormat.BitsPerSample == 24)
					{
						array[i] = array2[i + 2];
						array[i + 1] = array2[i + 1];
						array[i + 2] = array2[i];
					}
					else
					{
						if (this.WaveFormat.BitsPerSample != 32)
						{
							throw new FormatException("Unsupported PCM format.");
						}
						array[i] = array2[i + 3];
						array[i + 1] = array2[i + 2];
						array[i + 2] = array2[i + 1];
						array[i + 3] = array2[i];
					}
				}
				num3 = num;
			}
			return num3;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001E5C8 File Offset: 0x0001C7C8
		private static uint ConvertInt(byte[] buffer)
		{
			if (buffer.Length != 4)
			{
				throw new Exception("Incorrect length for long.");
			}
			return (uint)(((int)buffer[0] << 24) | ((int)buffer[1] << 16) | ((int)buffer[2] << 8) | (int)buffer[3]);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E5F2 File Offset: 0x0001C7F2
		private static short ConvertShort(byte[] buffer)
		{
			if (buffer.Length != 2)
			{
				throw new Exception("Incorrect length for int.");
			}
			return (short)(((int)buffer[0] << 8) | (int)buffer[1]);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E610 File Offset: 0x0001C810
		private static AiffFileReader.AiffChunk ReadChunkHeader(BinaryReader br)
		{
			AiffFileReader.AiffChunk aiffChunk = new AiffFileReader.AiffChunk((uint)br.BaseStream.Position, AiffFileReader.ReadChunkName(br), AiffFileReader.ConvertInt(br.ReadBytes(4)));
			return aiffChunk;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001E643 File Offset: 0x0001C843
		private static string ReadChunkName(BinaryReader br)
		{
			return new string(br.ReadChars(4));
		}

		// Token: 0x04000B52 RID: 2898
		private readonly WaveFormat waveFormat;

		// Token: 0x04000B53 RID: 2899
		private readonly bool ownInput;

		// Token: 0x04000B54 RID: 2900
		private readonly long dataPosition;

		// Token: 0x04000B55 RID: 2901
		private readonly int dataChunkLength;

		// Token: 0x04000B56 RID: 2902
		private readonly List<AiffFileReader.AiffChunk> chunks = new List<AiffFileReader.AiffChunk>();

		// Token: 0x04000B57 RID: 2903
		private Stream waveStream;

		// Token: 0x04000B58 RID: 2904
		private readonly object lockObject = new object();

		// Token: 0x020001DC RID: 476
		public struct AiffChunk
		{
			// Token: 0x06000A64 RID: 2660 RVA: 0x0001E651 File Offset: 0x0001C851
			public AiffChunk(uint start, string name, uint length)
			{
				this.ChunkStart = start;
				this.ChunkName = name;
				this.ChunkLength = length + ((length % 2U == 1U) ? 1U : 0U);
			}

			// Token: 0x04000B59 RID: 2905
			public string ChunkName;

			// Token: 0x04000B5A RID: 2906
			public uint ChunkLength;

			// Token: 0x04000B5B RID: 2907
			public uint ChunkStart;
		}
	}
}
