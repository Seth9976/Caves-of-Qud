using System;
using System.Collections.Generic;
using System.Linq;

namespace NVorbis
{
	// Token: 0x0200001A RID: 26
	internal class VorbisCodebook
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00006510 File Offset: 0x00004710
		internal static VorbisCodebook Init(VorbisStreamDecoder vorbis, DataPacket packet, int number)
		{
			VorbisCodebook vorbisCodebook = new VorbisCodebook();
			vorbisCodebook.BookNum = number;
			vorbisCodebook.Init(packet);
			return vorbisCodebook;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006532 File Offset: 0x00004732
		private VorbisCodebook()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000653C File Offset: 0x0000473C
		internal void Init(DataPacket packet)
		{
			ulong num = packet.ReadBits(24);
			if (num != 5653314UL)
			{
				throw new InvalidDataException();
			}
			this.Dimensions = (int)packet.ReadBits(16);
			this.Entries = (int)packet.ReadBits(24);
			this.Lengths = new int[this.Entries];
			this.InitTree(packet);
			this.InitLookupTable(packet);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000065A0 File Offset: 0x000047A0
		private void InitTree(DataPacket packet)
		{
			int num = 0;
			bool flag;
			if (packet.ReadBit())
			{
				int num2 = (int)packet.ReadBits(5) + 1;
				int i = 0;
				while (i < this.Entries)
				{
					int num3 = (int)packet.ReadBits(Utils.ilog(this.Entries - i));
					while (--num3 >= 0)
					{
						this.Lengths[i++] = num2;
					}
					num2++;
				}
				num = 0;
				flag = false;
			}
			else
			{
				flag = packet.ReadBit();
				for (int j = 0; j < this.Entries; j++)
				{
					if (!flag || packet.ReadBit())
					{
						this.Lengths[j] = (int)packet.ReadBits(5) + 1;
						num++;
					}
					else
					{
						this.Lengths[j] = -1;
					}
				}
			}
			this.MaxBits = Enumerable.Max(this.Lengths);
			int[] array = null;
			if (flag && num >= this.Entries >> 2)
			{
				array = new int[this.Entries];
				Array.Copy(this.Lengths, array, this.Entries);
				flag = false;
			}
			int num4;
			if (flag)
			{
				num4 = num;
			}
			else
			{
				num4 = 0;
			}
			int num5 = num4;
			int[] array2 = null;
			int[] array3 = null;
			if (!flag)
			{
				array3 = new int[this.Entries];
			}
			else if (num5 != 0)
			{
				array = new int[num5];
				array3 = new int[num5];
				array2 = new int[num5];
			}
			if (!this.ComputeCodewords(flag, num5, array3, array, this.Lengths, this.Entries, array2))
			{
				throw new InvalidDataException();
			}
			this.PrefixList = Huffman.BuildPrefixedLinkedList(array2 ?? Enumerable.ToArray<int>(Enumerable.Range(0, array3.Length)), array ?? this.Lengths, array3, out this.PrefixBitLength, out this.PrefixOverflowTree);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000673C File Offset: 0x0000493C
		private bool ComputeCodewords(bool sparse, int sortedEntries, int[] codewords, int[] codewordLengths, int[] len, int n, int[] values)
		{
			int num = 0;
			uint[] array = new uint[32];
			int num2 = 0;
			while (num2 < n && len[num2] <= 0)
			{
				num2++;
			}
			if (num2 == n)
			{
				return true;
			}
			this.AddEntry(sparse, codewords, codewordLengths, 0U, num2, num++, len[num2], values);
			for (int i = 1; i <= len[num2]; i++)
			{
				array[i] = 1U << 32 - i;
			}
			for (int i = num2 + 1; i < n; i++)
			{
				int num3 = len[i];
				if (num3 > 0)
				{
					while (num3 > 0 && array[num3] == 0U)
					{
						num3--;
					}
					if (num3 == 0)
					{
						return false;
					}
					uint num4 = array[num3];
					array[num3] = 0U;
					this.AddEntry(sparse, codewords, codewordLengths, Utils.BitReverse(num4), i, num++, len[i], values);
					if (num3 != len[i])
					{
						for (int j = len[i]; j > num3; j--)
						{
							array[j] = num4 + (1U << 32 - j);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000682C File Offset: 0x00004A2C
		private void AddEntry(bool sparse, int[] codewords, int[] codewordLengths, uint huffCode, int symbol, int count, int len, int[] values)
		{
			if (sparse)
			{
				codewords[count] = (int)huffCode;
				codewordLengths[count] = len;
				values[count] = symbol;
				return;
			}
			codewords[symbol] = (int)huffCode;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000684C File Offset: 0x00004A4C
		private void InitLookupTable(DataPacket packet)
		{
			this.MapType = (int)packet.ReadBits(4);
			if (this.MapType == 0)
			{
				return;
			}
			float num = Utils.ConvertFromVorbisFloat32(packet.ReadUInt32());
			float num2 = Utils.ConvertFromVorbisFloat32(packet.ReadUInt32());
			int num3 = (int)packet.ReadBits(4) + 1;
			bool flag = packet.ReadBit();
			int num4 = this.Entries * this.Dimensions;
			float[] array = new float[num4];
			if (this.MapType == 1)
			{
				num4 = this.lookup1_values();
			}
			uint[] array2 = new uint[num4];
			for (int i = 0; i < num4; i++)
			{
				array2[i] = (uint)packet.ReadBits(num3);
			}
			if (this.MapType == 1)
			{
				for (int j = 0; j < this.Entries; j++)
				{
					double num5 = 0.0;
					int num6 = 1;
					for (int k = 0; k < this.Dimensions; k++)
					{
						int num7 = j / num6 % num4;
						double num8 = (double)(array2[num7] * num2 + num) + num5;
						array[j * this.Dimensions + k] = (float)num8;
						if (flag)
						{
							num5 = num8;
						}
						num6 *= num4;
					}
				}
			}
			else
			{
				for (int l = 0; l < this.Entries; l++)
				{
					double num9 = 0.0;
					int num10 = l * this.Dimensions;
					for (int m = 0; m < this.Dimensions; m++)
					{
						double num11 = (double)(array2[num10] * num2 + num) + num9;
						array[l * this.Dimensions + m] = (float)num11;
						if (flag)
						{
							num9 = num11;
						}
						num10++;
					}
				}
			}
			this.LookupTable = array;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000069E0 File Offset: 0x00004BE0
		private int lookup1_values()
		{
			int num = (int)Math.Floor(Math.Exp(Math.Log((double)this.Entries) / (double)this.Dimensions));
			if (Math.Floor(Math.Pow((double)(num + 1), (double)this.Dimensions)) <= (double)this.Entries)
			{
				num++;
			}
			return num;
		}

		// Token: 0x1700003E RID: 62
		internal float this[int entry, int dim]
		{
			get
			{
				return this.LookupTable[entry * this.Dimensions + dim];
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006A44 File Offset: 0x00004C44
		internal int DecodeScalar(DataPacket packet)
		{
			int num2;
			int num = (int)packet.TryPeekBits(this.PrefixBitLength, out num2);
			if (num2 == 0)
			{
				return -1;
			}
			HuffmanListNode huffmanListNode = this.PrefixList[num];
			if (huffmanListNode != null)
			{
				packet.SkipBits(huffmanListNode.Length);
				return huffmanListNode.Value;
			}
			num = (int)packet.TryPeekBits(this.MaxBits, out num2);
			huffmanListNode = this.PrefixOverflowTree;
			while (huffmanListNode.Bits != (num & huffmanListNode.Mask))
			{
				if ((huffmanListNode = huffmanListNode.Next) == null)
				{
					return -1;
				}
			}
			packet.SkipBits(huffmanListNode.Length);
			return huffmanListNode.Value;
		}

		// Token: 0x0400007A RID: 122
		internal int BookNum;

		// Token: 0x0400007B RID: 123
		internal int Dimensions;

		// Token: 0x0400007C RID: 124
		internal int Entries;

		// Token: 0x0400007D RID: 125
		private int[] Lengths;

		// Token: 0x0400007E RID: 126
		private float[] LookupTable;

		// Token: 0x0400007F RID: 127
		internal int MapType;

		// Token: 0x04000080 RID: 128
		private HuffmanListNode PrefixOverflowTree;

		// Token: 0x04000081 RID: 129
		private List<HuffmanListNode> PrefixList;

		// Token: 0x04000082 RID: 130
		private int PrefixBitLength;

		// Token: 0x04000083 RID: 131
		private int MaxBits;
	}
}
