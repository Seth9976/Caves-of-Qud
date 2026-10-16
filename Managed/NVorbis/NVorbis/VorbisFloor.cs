using System;
using System.Collections.Generic;
using System.Linq;

namespace NVorbis
{
	// Token: 0x0200001B RID: 27
	internal abstract class VorbisFloor
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00006AD0 File Offset: 0x00004CD0
		internal static VorbisFloor Init(VorbisStreamDecoder vorbis, DataPacket packet)
		{
			int num = (int)packet.ReadBits(16);
			VorbisFloor vorbisFloor = null;
			switch (num)
			{
			case 0:
				vorbisFloor = new VorbisFloor.Floor0(vorbis);
				break;
			case 1:
				vorbisFloor = new VorbisFloor.Floor1(vorbis);
				break;
			}
			if (vorbisFloor == null)
			{
				throw new InvalidDataException();
			}
			vorbisFloor.Init(packet);
			return vorbisFloor;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006B1C File Offset: 0x00004D1C
		protected VorbisFloor(VorbisStreamDecoder vorbis)
		{
			this._vorbis = vorbis;
		}

		// Token: 0x060000FA RID: 250
		protected abstract void Init(DataPacket packet);

		// Token: 0x060000FB RID: 251
		internal abstract VorbisFloor.PacketData UnpackPacket(DataPacket packet, int blockSize, int channel);

		// Token: 0x060000FC RID: 252
		internal abstract void Apply(VorbisFloor.PacketData packetData, float[] residue);

		// Token: 0x04000084 RID: 132
		private VorbisStreamDecoder _vorbis;

		// Token: 0x0200001C RID: 28
		internal abstract class PacketData
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000FD RID: 253
			protected abstract bool HasEnergy { get; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000FE RID: 254 RVA: 0x00006B2B File Offset: 0x00004D2B
			// (set) Token: 0x060000FF RID: 255 RVA: 0x00006B33 File Offset: 0x00004D33
			internal bool ForceEnergy { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000100 RID: 256 RVA: 0x00006B3C File Offset: 0x00004D3C
			// (set) Token: 0x06000101 RID: 257 RVA: 0x00006B44 File Offset: 0x00004D44
			internal bool ForceNoEnergy { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000102 RID: 258 RVA: 0x00006B4D File Offset: 0x00004D4D
			internal bool ExecuteChannel
			{
				get
				{
					return (this.ForceEnergy | this.HasEnergy) & !this.ForceNoEnergy;
				}
			}

			// Token: 0x04000085 RID: 133
			internal int BlockSize;
		}

		// Token: 0x0200001D RID: 29
		private class Floor0 : VorbisFloor
		{
			// Token: 0x06000104 RID: 260 RVA: 0x00006B6E File Offset: 0x00004D6E
			internal Floor0(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x06000105 RID: 261 RVA: 0x00006B78 File Offset: 0x00004D78
			protected override void Init(DataPacket packet)
			{
				this._order = (int)packet.ReadBits(8);
				this._rate = (int)packet.ReadBits(16);
				this._bark_map_size = (int)packet.ReadBits(16);
				this._ampBits = (int)packet.ReadBits(6);
				this._ampOfs = (int)packet.ReadBits(8);
				this._books = new VorbisCodebook[(int)packet.ReadBits(4) + 1];
				if (this._order < 1 || this._rate < 1 || this._bark_map_size < 1 || this._books.Length == 0)
				{
					throw new InvalidDataException();
				}
				this._ampDiv = (1 << this._ampBits) - 1;
				for (int i = 0; i < this._books.Length; i++)
				{
					int num = (int)packet.ReadBits(8);
					if (num < 0 || num >= this._vorbis.Books.Length)
					{
						throw new InvalidDataException();
					}
					VorbisCodebook vorbisCodebook = this._vorbis.Books[num];
					if (vorbisCodebook.MapType == 0 || vorbisCodebook.Dimensions < 1)
					{
						throw new InvalidDataException();
					}
					this._books[i] = vorbisCodebook;
				}
				this._bookBits = Utils.ilog(this._books.Length);
				this._barkMaps = new Dictionary<int, int[]>();
				this._barkMaps[this._vorbis.Block0Size] = this.SynthesizeBarkCurve(this._vorbis.Block0Size / 2);
				this._barkMaps[this._vorbis.Block1Size] = this.SynthesizeBarkCurve(this._vorbis.Block1Size / 2);
				this._wMap = new Dictionary<int, float[]>();
				this._wMap[this._vorbis.Block0Size] = this.SynthesizeWDelMap(this._vorbis.Block0Size / 2);
				this._wMap[this._vorbis.Block1Size] = this.SynthesizeWDelMap(this._vorbis.Block1Size / 2);
				this._reusablePacketData = new VorbisFloor.Floor0.PacketData0[this._vorbis._channels];
				for (int j = 0; j < this._reusablePacketData.Length; j++)
				{
					this._reusablePacketData[j] = new VorbisFloor.Floor0.PacketData0
					{
						Coeff = new float[this._order + 1]
					};
				}
			}

			// Token: 0x06000106 RID: 262 RVA: 0x00006DA0 File Offset: 0x00004FA0
			private int[] SynthesizeBarkCurve(int n)
			{
				float num = (float)this._bark_map_size / VorbisFloor.Floor0.toBARK((double)(this._rate / 2));
				int[] array = new int[n + 1];
				for (int i = 0; i < n - 1; i++)
				{
					array[i] = Math.Min(this._bark_map_size - 1, (int)Math.Floor((double)(VorbisFloor.Floor0.toBARK((double)((float)this._rate / 2f / (float)n * (float)i)) * num)));
				}
				array[n] = -1;
				return array;
			}

			// Token: 0x06000107 RID: 263 RVA: 0x00006E14 File Offset: 0x00005014
			private static float toBARK(double lsp)
			{
				return (float)(13.1 * Math.Atan(0.00074 * lsp) + 2.24 * Math.Atan(1.85E-08 * lsp * lsp) + 0.0001 * lsp);
			}

			// Token: 0x06000108 RID: 264 RVA: 0x00006E68 File Offset: 0x00005068
			private float[] SynthesizeWDelMap(int n)
			{
				float num = (float)(3.141592653589793 / (double)this._bark_map_size);
				float[] array = new float[n];
				for (int i = 0; i < n; i++)
				{
					array[i] = 2f * (float)Math.Cos((double)(num * (float)i));
				}
				return array;
			}

			// Token: 0x06000109 RID: 265 RVA: 0x00006EB0 File Offset: 0x000050B0
			internal override VorbisFloor.PacketData UnpackPacket(DataPacket packet, int blockSize, int channel)
			{
				VorbisFloor.Floor0.PacketData0 packetData = this._reusablePacketData[channel];
				packetData.BlockSize = blockSize;
				packetData.ForceEnergy = false;
				packetData.ForceNoEnergy = false;
				packetData.Amp = packet.ReadBits(this._ampBits);
				if (packetData.Amp > 0f)
				{
					Array.Clear(packetData.Coeff, 0, packetData.Coeff.Length);
					packetData.Amp = packetData.Amp / (float)this._ampDiv * (float)this._ampOfs;
					uint num = (uint)packet.ReadBits(this._bookBits);
					if ((ulong)num >= (ulong)((long)this._books.Length))
					{
						packetData.Amp = 0f;
						return packetData;
					}
					VorbisCodebook vorbisCodebook = this._books[(int)((UIntPtr)num)];
					int i = 0;
					while (i < this._order)
					{
						int num2 = vorbisCodebook.DecodeScalar(packet);
						if (num2 == -1)
						{
							packetData.Amp = 0f;
							return packetData;
						}
						int num3 = 0;
						while (i < this._order && num3 < vorbisCodebook.Dimensions)
						{
							packetData.Coeff[i] = vorbisCodebook[num2, num3];
							num3++;
							i++;
						}
					}
					float num4 = 0f;
					int j = 0;
					while (j < this._order)
					{
						int num5 = 0;
						while (j < this._order && num5 < vorbisCodebook.Dimensions)
						{
							packetData.Coeff[j] += num4;
							j++;
							num5++;
						}
						num4 = packetData.Coeff[j - 1];
					}
				}
				return packetData;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x00007024 File Offset: 0x00005224
			internal override void Apply(VorbisFloor.PacketData packetData, float[] residue)
			{
				VorbisFloor.Floor0.PacketData0 packetData2 = packetData as VorbisFloor.Floor0.PacketData0;
				if (packetData2 == null)
				{
					throw new ArgumentException("Incorrect packet data!");
				}
				int num = packetData2.BlockSize / 2;
				if (packetData2.Amp > 0f)
				{
					int[] array = this._barkMaps[packetData2.BlockSize];
					float[] array2 = this._wMap[packetData2.BlockSize];
					int i;
					for (i = 0; i < this._order; i++)
					{
						packetData2.Coeff[i] = 2f * (float)Math.Cos((double)packetData2.Coeff[i]);
					}
					i = 0;
					while (i < num)
					{
						int num2 = array[i];
						float num3 = 0.5f;
						float num4 = 0.5f;
						float num5 = array2[num2];
						int j;
						for (j = 1; j < this._order; j += 2)
						{
							num4 *= num5 - packetData2.Coeff[j - 1];
							num3 *= num5 - packetData2.Coeff[j];
						}
						if (j == this._order)
						{
							num4 *= num5 - packetData2.Coeff[j - 1];
							num3 *= num3 * (4f - num5 * num5);
							num4 *= num4;
						}
						else
						{
							num3 *= num3 * (2f - num5);
							num4 *= num4 * (2f + num5);
						}
						num4 = packetData2.Amp / (float)Math.Sqrt((double)(num3 + num4)) - (float)this._ampOfs;
						num4 = (float)Math.Exp((double)(num4 * 0.11512925f));
						residue[i] *= num4;
						while (array[++i] == num2)
						{
							residue[i] *= num4;
						}
					}
					return;
				}
				Array.Clear(residue, 0, num);
			}

			// Token: 0x04000088 RID: 136
			private int _order;

			// Token: 0x04000089 RID: 137
			private int _rate;

			// Token: 0x0400008A RID: 138
			private int _bark_map_size;

			// Token: 0x0400008B RID: 139
			private int _ampBits;

			// Token: 0x0400008C RID: 140
			private int _ampOfs;

			// Token: 0x0400008D RID: 141
			private int _ampDiv;

			// Token: 0x0400008E RID: 142
			private VorbisCodebook[] _books;

			// Token: 0x0400008F RID: 143
			private int _bookBits;

			// Token: 0x04000090 RID: 144
			private Dictionary<int, float[]> _wMap;

			// Token: 0x04000091 RID: 145
			private Dictionary<int, int[]> _barkMaps;

			// Token: 0x04000092 RID: 146
			private VorbisFloor.Floor0.PacketData0[] _reusablePacketData;

			// Token: 0x0200001E RID: 30
			private class PacketData0 : VorbisFloor.PacketData
			{
				// Token: 0x17000043 RID: 67
				// (get) Token: 0x0600010B RID: 267 RVA: 0x000071E8 File Offset: 0x000053E8
				protected override bool HasEnergy
				{
					get
					{
						return this.Amp > 0f;
					}
				}

				// Token: 0x04000093 RID: 147
				internal float[] Coeff;

				// Token: 0x04000094 RID: 148
				internal float Amp;
			}
		}

		// Token: 0x0200001F RID: 31
		private class Floor1 : VorbisFloor
		{
			// Token: 0x0600010D RID: 269 RVA: 0x000071FF File Offset: 0x000053FF
			internal Floor1(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x0600010E RID: 270 RVA: 0x00007224 File Offset: 0x00005424
			protected override void Init(DataPacket packet)
			{
				this._partitionClass = new int[(int)packet.ReadBits(5)];
				for (int i = 0; i < this._partitionClass.Length; i++)
				{
					this._partitionClass[i] = (int)packet.ReadBits(4);
				}
				int num = Enumerable.Max(this._partitionClass);
				this._classDimensions = new int[num + 1];
				this._classSubclasses = new int[num + 1];
				this._classMasterbooks = new VorbisCodebook[num + 1];
				this._classMasterBookIndex = new int[num + 1];
				this._subclassBooks = new VorbisCodebook[num + 1][];
				this._subclassBookIndex = new int[num + 1][];
				for (int j = 0; j <= num; j++)
				{
					this._classDimensions[j] = (int)packet.ReadBits(3) + 1;
					this._classSubclasses[j] = (int)packet.ReadBits(2);
					if (this._classSubclasses[j] > 0)
					{
						this._classMasterBookIndex[j] = (int)packet.ReadBits(8);
						this._classMasterbooks[j] = this._vorbis.Books[this._classMasterBookIndex[j]];
					}
					this._subclassBooks[j] = new VorbisCodebook[1 << this._classSubclasses[j]];
					this._subclassBookIndex[j] = new int[this._subclassBooks[j].Length];
					for (int k = 0; k < this._subclassBooks[j].Length; k++)
					{
						int num2 = (int)packet.ReadBits(8) - 1;
						if (num2 >= 0)
						{
							this._subclassBooks[j][k] = this._vorbis.Books[num2];
						}
						this._subclassBookIndex[j][k] = num2;
					}
				}
				this._multiplier = (int)packet.ReadBits(2);
				this._range = VorbisFloor.Floor1._rangeLookup[this._multiplier];
				this._yBits = VorbisFloor.Floor1._yBitsLookup[this._multiplier];
				this._multiplier++;
				int num3 = (int)packet.ReadBits(4);
				List<int> list = new List<int>();
				list.Add(0);
				list.Add(1 << num3);
				for (int l = 0; l < this._partitionClass.Length; l++)
				{
					int num4 = this._partitionClass[l];
					for (int m = 0; m < this._classDimensions[num4]; m++)
					{
						list.Add((int)packet.ReadBits(num3));
					}
				}
				this._xList = list.ToArray();
				this._lNeigh = new int[list.Count];
				this._hNeigh = new int[list.Count];
				this._sortIdx = new int[list.Count];
				this._sortIdx[0] = 0;
				this._sortIdx[1] = 1;
				for (int n = 2; n < this._lNeigh.Length; n++)
				{
					this._lNeigh[n] = 0;
					this._hNeigh[n] = 1;
					this._sortIdx[n] = n;
					for (int num5 = 2; num5 < n; num5++)
					{
						int num6 = this._xList[num5];
						if (num6 < this._xList[n])
						{
							if (num6 > this._xList[this._lNeigh[n]])
							{
								this._lNeigh[n] = num5;
							}
						}
						else if (num6 < this._xList[this._hNeigh[n]])
						{
							this._hNeigh[n] = num5;
						}
					}
				}
				for (int num7 = 0; num7 < this._sortIdx.Length - 1; num7++)
				{
					for (int num8 = num7 + 1; num8 < this._sortIdx.Length; num8++)
					{
						if (this._xList[num7] == this._xList[num8])
						{
							throw new InvalidDataException();
						}
						if (this._xList[this._sortIdx[num7]] > this._xList[this._sortIdx[num8]])
						{
							int num9 = this._sortIdx[num7];
							this._sortIdx[num7] = this._sortIdx[num8];
							this._sortIdx[num8] = num9;
						}
					}
				}
				this._reusablePacketData = new VorbisFloor.Floor1.PacketData1[this._vorbis._channels];
				for (int num10 = 0; num10 < this._reusablePacketData.Length; num10++)
				{
					this._reusablePacketData[num10] = new VorbisFloor.Floor1.PacketData1();
				}
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00007630 File Offset: 0x00005830
			internal override VorbisFloor.PacketData UnpackPacket(DataPacket packet, int blockSize, int channel)
			{
				VorbisFloor.Floor1.PacketData1 packetData = this._reusablePacketData[channel];
				packetData.BlockSize = blockSize;
				packetData.ForceEnergy = false;
				packetData.ForceNoEnergy = false;
				packetData.PostCount = 0;
				Array.Clear(packetData.Posts, 0, 64);
				if (packet.ReadBit())
				{
					int num = 2;
					packetData.Posts[0] = (int)packet.ReadBits(this._yBits);
					packetData.Posts[1] = (int)packet.ReadBits(this._yBits);
					for (int i = 0; i < this._partitionClass.Length; i++)
					{
						int num2 = this._partitionClass[i];
						int num3 = this._classDimensions[num2];
						int num4 = this._classSubclasses[num2];
						int num5 = (1 << num4) - 1;
						uint num6 = 0U;
						if (num4 > 0 && (num6 = (uint)this._classMasterbooks[num2].DecodeScalar(packet)) == 4294967295U)
						{
							num = 0;
							break;
						}
						for (int j = 0; j < num3; j++)
						{
							checked
							{
								VorbisCodebook vorbisCodebook = this._subclassBooks[num2][(int)((IntPtr)(unchecked((ulong)num6 & (ulong)((long)num5))))];
								num6 >>= num4;
								if (vorbisCodebook != null && (packetData.Posts[num] = vorbisCodebook.DecodeScalar(packet)) == -1)
								{
									num = 0;
									i = this._partitionClass.Length;
									break;
								}
							}
							num++;
						}
					}
					packetData.PostCount = num;
				}
				return packetData;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x0000776C File Offset: 0x0000596C
			internal override void Apply(VorbisFloor.PacketData packetData, float[] residue)
			{
				VorbisFloor.Floor1.PacketData1 packetData2 = packetData as VorbisFloor.Floor1.PacketData1;
				if (packetData2 == null)
				{
					throw new ArgumentException("Incorrect packet data!", "packetData");
				}
				int num = packetData2.BlockSize / 2;
				if (packetData2.PostCount > 0)
				{
					bool[] array = this.UnwrapPosts(packetData2);
					int num2 = 0;
					int num3 = packetData2.Posts[0] * this._multiplier;
					for (int i = 1; i < packetData2.PostCount; i++)
					{
						int num4 = this._sortIdx[i];
						if (array[num4])
						{
							int num5 = this._xList[num4];
							int num6 = packetData2.Posts[num4] * this._multiplier;
							if (num2 < num)
							{
								this.RenderLineMulti(num2, num3, Math.Min(num5, num), num6, residue);
							}
							num2 = num5;
							num3 = num6;
						}
						if (num2 >= num)
						{
							break;
						}
					}
					if (num2 < num)
					{
						this.RenderLineMulti(num2, num3, num, num3, residue);
						return;
					}
				}
				else
				{
					Array.Clear(residue, 0, num);
				}
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00007844 File Offset: 0x00005A44
			private bool[] UnwrapPosts(VorbisFloor.Floor1.PacketData1 data)
			{
				Array.Clear(this._stepFlags, 2, 62);
				this._stepFlags[0] = true;
				this._stepFlags[1] = true;
				Array.Clear(this._finalY, 2, 62);
				this._finalY[0] = data.Posts[0];
				this._finalY[1] = data.Posts[1];
				for (int i = 2; i < data.PostCount; i++)
				{
					int num = this._lNeigh[i];
					int num2 = this._hNeigh[i];
					int num3 = this.RenderPoint(this._xList[num], this._finalY[num], this._xList[num2], this._finalY[num2], this._xList[i]);
					int num4 = data.Posts[i];
					int num5 = this._range - num3;
					int num6 = num3;
					int num7;
					if (num5 < num6)
					{
						num7 = num5 * 2;
					}
					else
					{
						num7 = num6 * 2;
					}
					if (num4 != 0)
					{
						this._stepFlags[num] = true;
						this._stepFlags[num2] = true;
						this._stepFlags[i] = true;
						if (num4 >= num7)
						{
							if (num5 > num6)
							{
								this._finalY[i] = num4 - num6 + num3;
							}
							else
							{
								this._finalY[i] = num3 - num4 + num5 - 1;
							}
						}
						else if (num4 % 2 == 1)
						{
							this._finalY[i] = num3 - (num4 + 1) / 2;
						}
						else
						{
							this._finalY[i] = num3 + num4 / 2;
						}
					}
					else
					{
						this._stepFlags[i] = false;
						this._finalY[i] = num3;
					}
				}
				for (int j = 0; j < data.PostCount; j++)
				{
					data.Posts[j] = this._finalY[j];
				}
				return this._stepFlags;
			}

			// Token: 0x06000112 RID: 274 RVA: 0x000079DC File Offset: 0x00005BDC
			private int RenderPoint(int x0, int y0, int x1, int y1, int X)
			{
				int num = y1 - y0;
				int num2 = x1 - x0;
				int num3 = Math.Abs(num);
				int num4 = num3 * (X - x0);
				int num5 = num4 / num2;
				if (num < 0)
				{
					return y0 - num5;
				}
				return y0 + num5;
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00007A14 File Offset: 0x00005C14
			private void RenderLineMulti(int x0, int y0, int x1, int y1, float[] v)
			{
				int num = y1 - y0;
				int num2 = x1 - x0;
				int num3 = Math.Abs(num);
				int num4 = 1 - ((num >> 31) & 1) * 2;
				int num5 = num / num2;
				int num6 = x0;
				int num7 = y0;
				int num8 = -num2;
				v[x0] *= VorbisFloor.Floor1.inverse_dB_table[y0];
				num3 -= Math.Abs(num5) * num2;
				while (++num6 < x1)
				{
					num7 += num5;
					num8 += num3;
					if (num8 >= 0)
					{
						num8 -= num2;
						num7 += num4;
					}
					v[num6] *= VorbisFloor.Floor1.inverse_dB_table[num7];
				}
			}

			// Token: 0x04000095 RID: 149
			private int[] _partitionClass;

			// Token: 0x04000096 RID: 150
			private int[] _classDimensions;

			// Token: 0x04000097 RID: 151
			private int[] _classSubclasses;

			// Token: 0x04000098 RID: 152
			private int[] _xList;

			// Token: 0x04000099 RID: 153
			private int[] _classMasterBookIndex;

			// Token: 0x0400009A RID: 154
			private int[] _hNeigh;

			// Token: 0x0400009B RID: 155
			private int[] _lNeigh;

			// Token: 0x0400009C RID: 156
			private int[] _sortIdx;

			// Token: 0x0400009D RID: 157
			private int _multiplier;

			// Token: 0x0400009E RID: 158
			private int _range;

			// Token: 0x0400009F RID: 159
			private int _yBits;

			// Token: 0x040000A0 RID: 160
			private VorbisCodebook[] _classMasterbooks;

			// Token: 0x040000A1 RID: 161
			private VorbisCodebook[][] _subclassBooks;

			// Token: 0x040000A2 RID: 162
			private int[][] _subclassBookIndex;

			// Token: 0x040000A3 RID: 163
			private static int[] _rangeLookup = new int[] { 256, 128, 86, 64 };

			// Token: 0x040000A4 RID: 164
			private static int[] _yBitsLookup = new int[] { 8, 7, 7, 6 };

			// Token: 0x040000A5 RID: 165
			private VorbisFloor.Floor1.PacketData1[] _reusablePacketData;

			// Token: 0x040000A6 RID: 166
			private bool[] _stepFlags = new bool[64];

			// Token: 0x040000A7 RID: 167
			private int[] _finalY = new int[64];

			// Token: 0x040000A8 RID: 168
			private static readonly float[] inverse_dB_table = new float[]
			{
				1.0649863E-07f, 1.1341951E-07f, 1.2079015E-07f, 1.2863978E-07f, 1.369995E-07f, 1.459025E-07f, 1.5538409E-07f, 1.6548181E-07f, 1.7623574E-07f, 1.8768856E-07f,
				1.998856E-07f, 2.128753E-07f, 2.2670913E-07f, 2.4144197E-07f, 2.5713223E-07f, 2.7384212E-07f, 2.9163792E-07f, 3.1059022E-07f, 3.307741E-07f, 3.5226967E-07f,
				3.7516213E-07f, 3.995423E-07f, 4.255068E-07f, 4.5315863E-07f, 4.8260745E-07f, 5.1397E-07f, 5.4737063E-07f, 5.829419E-07f, 6.208247E-07f, 6.611694E-07f,
				7.041359E-07f, 7.4989464E-07f, 7.98627E-07f, 8.505263E-07f, 9.057983E-07f, 9.646621E-07f, 1.0273513E-06f, 1.0941144E-06f, 1.1652161E-06f, 1.2409384E-06f,
				1.3215816E-06f, 1.4074654E-06f, 1.4989305E-06f, 1.5963394E-06f, 1.7000785E-06f, 1.8105592E-06f, 1.9282195E-06f, 2.053526E-06f, 2.1869757E-06f, 2.3290977E-06f,
				2.4804558E-06f, 2.6416496E-06f, 2.813319E-06f, 2.9961443E-06f, 3.1908505E-06f, 3.39821E-06f, 3.619045E-06f, 3.8542307E-06f, 4.1047006E-06f, 4.371447E-06f,
				4.6555283E-06f, 4.958071E-06f, 5.280274E-06f, 5.623416E-06f, 5.988857E-06f, 6.3780467E-06f, 6.7925284E-06f, 7.2339453E-06f, 7.704048E-06f, 8.2047E-06f,
				8.737888E-06f, 9.305725E-06f, 9.910464E-06f, 1.0554501E-05f, 1.1240392E-05f, 1.1970856E-05f, 1.2748789E-05f, 1.3577278E-05f, 1.4459606E-05f, 1.5399271E-05f,
				1.6400005E-05f, 1.7465769E-05f, 1.8600793E-05f, 1.9809577E-05f, 2.1096914E-05f, 2.2467912E-05f, 2.3928002E-05f, 2.5482977E-05f, 2.7139005E-05f, 2.890265E-05f,
				3.078091E-05f, 3.2781227E-05f, 3.4911533E-05f, 3.718028E-05f, 3.9596467E-05f, 4.2169668E-05f, 4.491009E-05f, 4.7828602E-05f, 5.0936775E-05f, 5.424693E-05f,
				5.7772202E-05f, 6.152657E-05f, 6.552491E-05f, 6.9783084E-05f, 7.4317984E-05f, 7.914758E-05f, 8.429104E-05f, 8.976875E-05f, 9.560242E-05f, 0.00010181521f,
				0.00010843174f, 0.00011547824f, 0.00012298267f, 0.00013097477f, 0.00013948625f, 0.00014855085f, 0.00015820454f, 0.00016848555f, 0.00017943469f, 0.00019109536f,
				0.00020351382f, 0.0002167393f, 0.00023082423f, 0.00024582449f, 0.00026179955f, 0.00027881275f, 0.00029693157f, 0.00031622787f, 0.00033677815f, 0.00035866388f,
				0.00038197188f, 0.00040679457f, 0.00043323037f, 0.0004613841f, 0.0004913675f, 0.00052329927f, 0.0005573062f, 0.0005935231f, 0.0006320936f, 0.0006731706f,
				0.000716917f, 0.0007635063f, 0.00081312325f, 0.00086596457f, 0.00092223985f, 0.0009821722f, 0.0010459992f, 0.0011139743f, 0.0011863665f, 0.0012634633f,
				0.0013455702f, 0.0014330129f, 0.0015261382f, 0.0016253153f, 0.0017309374f, 0.0018434235f, 0.0019632196f, 0.0020908006f, 0.0022266726f, 0.0023713743f,
				0.0025254795f, 0.0026895993f, 0.0028643848f, 0.0030505287f, 0.003248769f, 0.0034598925f, 0.0036847359f, 0.0039241905f, 0.0041792067f, 0.004450795f,
				0.004740033f, 0.005048067f, 0.0053761187f, 0.005725489f, 0.0060975635f, 0.0064938175f, 0.0069158226f, 0.0073652514f, 0.007843887f, 0.008353627f,
				0.008896492f, 0.009474637f, 0.010090352f, 0.01074608f, 0.011444421f, 0.012188144f, 0.012980198f, 0.013823725f, 0.014722068f, 0.015678791f,
				0.016697686f, 0.017782796f, 0.018938422f, 0.020169148f, 0.021479854f, 0.022875736f, 0.02436233f, 0.025945531f, 0.027631618f, 0.029427277f,
				0.031339627f, 0.03337625f, 0.035545226f, 0.037855156f, 0.0403152f, 0.042935107f, 0.045725275f, 0.048696756f, 0.05186135f, 0.05523159f,
				0.05882085f, 0.062643364f, 0.06671428f, 0.07104975f, 0.075666964f, 0.08058423f, 0.08582105f, 0.09139818f, 0.097337745f, 0.1036633f,
				0.11039993f, 0.11757434f, 0.12521498f, 0.13335215f, 0.14201812f, 0.15124726f, 0.16107617f, 0.1715438f, 0.18269168f, 0.19456401f,
				0.20720787f, 0.22067343f, 0.23501402f, 0.25028655f, 0.26655158f, 0.28387362f, 0.3023213f, 0.32196787f, 0.34289113f, 0.36517414f,
				0.3889052f, 0.41417846f, 0.44109413f, 0.4697589f, 0.50028646f, 0.53279793f, 0.5674221f, 0.6042964f, 0.64356697f, 0.6853896f,
				0.72993004f, 0.777365f, 0.8278826f, 0.88168305f, 0.9389798f, 1f
			};

			// Token: 0x02000020 RID: 32
			private class PacketData1 : VorbisFloor.PacketData
			{
				// Token: 0x17000044 RID: 68
				// (get) Token: 0x06000115 RID: 277 RVA: 0x00007F33 File Offset: 0x00006133
				protected override bool HasEnergy
				{
					get
					{
						return this.PostCount > 0;
					}
				}

				// Token: 0x040000A9 RID: 169
				public int[] Posts = new int[64];

				// Token: 0x040000AA RID: 170
				public int PostCount;
			}
		}
	}
}
