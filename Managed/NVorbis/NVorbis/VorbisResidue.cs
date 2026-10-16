using System;
using System.Linq;

namespace NVorbis
{
	// Token: 0x02000027 RID: 39
	internal abstract class VorbisResidue
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00008980 File Offset: 0x00006B80
		internal static VorbisResidue Init(VorbisStreamDecoder vorbis, DataPacket packet)
		{
			int num = (int)packet.ReadBits(16);
			VorbisResidue vorbisResidue = null;
			switch (num)
			{
			case 0:
				vorbisResidue = new VorbisResidue.Residue0(vorbis);
				break;
			case 1:
				vorbisResidue = new VorbisResidue.Residue1(vorbis);
				break;
			case 2:
				vorbisResidue = new VorbisResidue.Residue2(vorbis);
				break;
			}
			if (vorbisResidue == null)
			{
				throw new InvalidDataException();
			}
			vorbisResidue.Init(packet);
			return vorbisResidue;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000089DC File Offset: 0x00006BDC
		private static int icount(int v)
		{
			int num = 0;
			while (v != 0)
			{
				num += v & 1;
				v >>= 1;
			}
			return num;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000089FC File Offset: 0x00006BFC
		protected VorbisResidue(VorbisStreamDecoder vorbis)
		{
			this._vorbis = vorbis;
			this._residue = new float[this._vorbis._channels][];
			for (int i = 0; i < this._vorbis._channels; i++)
			{
				this._residue[i] = new float[this._vorbis.Block1Size];
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00008A5C File Offset: 0x00006C5C
		protected float[][] GetResidueBuffer(int channels)
		{
			float[][] array = this._residue;
			if (channels < this._vorbis._channels)
			{
				array = new float[channels][];
				Array.Copy(this._residue, array, channels);
			}
			for (int i = 0; i < channels; i++)
			{
				Array.Clear(array[i], 0, array[i].Length);
			}
			return array;
		}

		// Token: 0x06000148 RID: 328
		internal abstract float[][] Decode(DataPacket packet, bool[] doNotDecode, int channels, int blockSize);

		// Token: 0x06000149 RID: 329
		protected abstract void Init(DataPacket packet);

		// Token: 0x040000C2 RID: 194
		private VorbisStreamDecoder _vorbis;

		// Token: 0x040000C3 RID: 195
		private float[][] _residue;

		// Token: 0x02000028 RID: 40
		private class Residue0 : VorbisResidue
		{
			// Token: 0x0600014A RID: 330 RVA: 0x00008AAD File Offset: 0x00006CAD
			internal Residue0(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x0600014B RID: 331 RVA: 0x00008AB8 File Offset: 0x00006CB8
			protected override void Init(DataPacket packet)
			{
				this._begin = (int)packet.ReadBits(24);
				this._end = (int)packet.ReadBits(24);
				this._partitionSize = (int)packet.ReadBits(24) + 1;
				this._classifications = (int)packet.ReadBits(6) + 1;
				this._classBook = this._vorbis.Books[(int)packet.ReadBits(8)];
				this._cascade = new int[this._classifications];
				int num = 0;
				for (int i = 0; i < this._classifications; i++)
				{
					int num2 = (int)packet.ReadBits(3);
					if (packet.ReadBit())
					{
						this._cascade[i] = ((int)packet.ReadBits(5) << 3) | num2;
					}
					else
					{
						this._cascade[i] = num2;
					}
					num += VorbisResidue.icount(this._cascade[i]);
				}
				int[] array = new int[num];
				for (int j = 0; j < num; j++)
				{
					array[j] = (int)packet.ReadBits(8);
					if (this._vorbis.Books[array[j]].MapType == 0)
					{
						throw new InvalidDataException();
					}
				}
				int entries = this._classBook.Entries;
				int k = this._classBook.Dimensions;
				int num3 = 1;
				while (k > 0)
				{
					num3 *= this._classifications;
					if (num3 > entries)
					{
						throw new InvalidDataException();
					}
					k--;
				}
				k = this._classBook.Dimensions;
				this._books = new VorbisCodebook[this._classifications][];
				num = 0;
				int num4 = 0;
				for (int l = 0; l < this._classifications; l++)
				{
					int num5 = Utils.ilog(this._cascade[l]);
					this._books[l] = new VorbisCodebook[num5];
					if (num5 > 0)
					{
						num4 = Math.Max(num4, num5);
						for (int m = 0; m < num5; m++)
						{
							if ((this._cascade[l] & (1 << m)) > 0)
							{
								this._books[l][m] = this._vorbis.Books[array[num++]];
							}
						}
					}
				}
				this._maxStages = num4;
				this._decodeMap = new int[num3][];
				for (int n = 0; n < num3; n++)
				{
					int num6 = n;
					int num7 = num3 / this._classifications;
					this._decodeMap[n] = new int[this._classBook.Dimensions];
					for (int num8 = 0; num8 < this._classBook.Dimensions; num8++)
					{
						int num9 = num6 / num7;
						num6 -= num9 * num7;
						num7 /= this._classifications;
						this._decodeMap[n][num8] = num9;
					}
				}
				this._entryCache = new int[this._partitionSize];
				this._partWordCache = new int[this._vorbis._channels][][];
				int num10 = ((this._end - this._begin) / this._partitionSize + this._classBook.Dimensions - 1) / this._classBook.Dimensions;
				for (int num11 = 0; num11 < this._vorbis._channels; num11++)
				{
					this._partWordCache[num11] = new int[num10][];
				}
			}

			// Token: 0x0600014C RID: 332 RVA: 0x00008DC8 File Offset: 0x00006FC8
			internal override float[][] Decode(DataPacket packet, bool[] doNotDecode, int channels, int blockSize)
			{
				float[][] residueBuffer = base.GetResidueBuffer(doNotDecode.Length);
				int num = ((this._end < blockSize / 2) ? this._end : (blockSize / 2));
				int num2 = num - this._begin;
				if (num2 > 0 && Enumerable.Contains<bool>(doNotDecode, false))
				{
					int num3 = num2 / this._partitionSize;
					int num4 = (num3 + this._classBook.Dimensions - 1) / this._classBook.Dimensions;
					for (int i = 0; i < channels; i++)
					{
						Array.Clear(this._partWordCache[i], 0, num4);
					}
					for (int j = 0; j < this._maxStages; j++)
					{
						int k = 0;
						int num5 = 0;
						while (k < num3)
						{
							if (j == 0)
							{
								for (int l = 0; l < channels; l++)
								{
									int num6 = this._classBook.DecodeScalar(packet);
									if (num6 < 0 || num6 >= this._decodeMap.Length)
									{
										k = num3;
										j = this._maxStages;
										break;
									}
									this._partWordCache[l][num5] = this._decodeMap[num6];
								}
							}
							int num7 = 0;
							while (k < num3 && num7 < this._classBook.Dimensions)
							{
								int num8 = this._begin + k * this._partitionSize;
								for (int m = 0; m < channels; m++)
								{
									int num9 = this._partWordCache[m][num5][num7];
									if ((this._cascade[num9] & (1 << j)) != 0)
									{
										VorbisCodebook vorbisCodebook = this._books[num9][j];
										if (vorbisCodebook != null && this.WriteVectors(vorbisCodebook, packet, residueBuffer, m, num8, this._partitionSize))
										{
											k = num3;
											j = this._maxStages;
											break;
										}
									}
								}
								num7++;
								k++;
							}
							num5++;
						}
					}
				}
				return residueBuffer;
			}

			// Token: 0x0600014D RID: 333 RVA: 0x00008F90 File Offset: 0x00007190
			protected virtual bool WriteVectors(VorbisCodebook codebook, DataPacket packet, float[][] residue, int channel, int offset, int partitionSize)
			{
				float[] array = residue[channel];
				int num = partitionSize / codebook.Dimensions;
				for (int i = 0; i < num; i++)
				{
					if ((this._entryCache[i] = codebook.DecodeScalar(packet)) == -1)
					{
						return true;
					}
				}
				for (int j = 0; j < codebook.Dimensions; j++)
				{
					int k = 0;
					while (k < num)
					{
						array[offset] += codebook[this._entryCache[k], j];
						k++;
						offset++;
					}
				}
				return false;
			}

			// Token: 0x040000C4 RID: 196
			private int _begin;

			// Token: 0x040000C5 RID: 197
			private int _end;

			// Token: 0x040000C6 RID: 198
			private int _partitionSize;

			// Token: 0x040000C7 RID: 199
			private int _classifications;

			// Token: 0x040000C8 RID: 200
			private int _maxStages;

			// Token: 0x040000C9 RID: 201
			private VorbisCodebook[][] _books;

			// Token: 0x040000CA RID: 202
			private VorbisCodebook _classBook;

			// Token: 0x040000CB RID: 203
			private int[] _cascade;

			// Token: 0x040000CC RID: 204
			private int[] _entryCache;

			// Token: 0x040000CD RID: 205
			private int[][] _decodeMap;

			// Token: 0x040000CE RID: 206
			private int[][][] _partWordCache;
		}

		// Token: 0x02000029 RID: 41
		private class Residue1 : VorbisResidue.Residue0
		{
			// Token: 0x0600014E RID: 334 RVA: 0x0000901D File Offset: 0x0000721D
			internal Residue1(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x0600014F RID: 335 RVA: 0x00009028 File Offset: 0x00007228
			protected override bool WriteVectors(VorbisCodebook codebook, DataPacket packet, float[][] residue, int channel, int offset, int partitionSize)
			{
				float[] array = residue[channel];
				int i = 0;
				while (i < partitionSize)
				{
					int num = codebook.DecodeScalar(packet);
					if (num == -1)
					{
						return true;
					}
					for (int j = 0; j < codebook.Dimensions; j++)
					{
						array[offset + i] += codebook[num, j];
						i++;
					}
				}
				return false;
			}
		}

		// Token: 0x0200002A RID: 42
		private class Residue2 : VorbisResidue.Residue0
		{
			// Token: 0x06000150 RID: 336 RVA: 0x00009085 File Offset: 0x00007285
			internal Residue2(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x06000151 RID: 337 RVA: 0x0000908E File Offset: 0x0000728E
			internal override float[][] Decode(DataPacket packet, bool[] doNotDecode, int channels, int blockSize)
			{
				this._channels = channels;
				return base.Decode(packet, doNotDecode, 1, blockSize * channels);
			}

			// Token: 0x06000152 RID: 338 RVA: 0x000090A4 File Offset: 0x000072A4
			protected override bool WriteVectors(VorbisCodebook codebook, DataPacket packet, float[][] residue, int channel, int offset, int partitionSize)
			{
				int num = 0;
				offset /= this._channels;
				int i = 0;
				while (i < partitionSize)
				{
					int num2 = codebook.DecodeScalar(packet);
					if (num2 == -1)
					{
						return true;
					}
					int j = 0;
					while (j < codebook.Dimensions)
					{
						residue[num][offset] += codebook[num2, j];
						if (++num == this._channels)
						{
							num = 0;
							offset++;
						}
						j++;
						i++;
					}
				}
				return false;
			}

			// Token: 0x040000CF RID: 207
			private int _channels;
		}
	}
}
