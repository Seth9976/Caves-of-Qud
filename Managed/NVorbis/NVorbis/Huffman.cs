using System;
using System.Collections.Generic;

namespace NVorbis
{
	// Token: 0x02000003 RID: 3
	internal static class Huffman
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002394 File Offset: 0x00000594
		internal static List<HuffmanListNode> BuildPrefixedLinkedList(int[] values, int[] lengthList, int[] codeList, out int tableBits, out HuffmanListNode firstOverflowNode)
		{
			HuffmanListNode[] array = new HuffmanListNode[lengthList.Length];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new HuffmanListNode
				{
					Value = values[i],
					Length = ((lengthList[i] <= 0) ? 99999 : lengthList[i]),
					Bits = codeList[i],
					Mask = (1 << lengthList[i]) - 1
				};
				if (lengthList[i] > 0 && num < lengthList[i])
				{
					num = lengthList[i];
				}
			}
			Array.Sort<HuffmanListNode>(array, new Comparison<HuffmanListNode>(Huffman.SortCallback));
			tableBits = ((num > 10) ? 10 : num);
			List<HuffmanListNode> list = new List<HuffmanListNode>(1 << tableBits);
			firstOverflowNode = null;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length >= 99999)
				{
					break;
				}
				if (firstOverflowNode == null)
				{
					int length = array[j].Length;
					if (length > tableBits)
					{
						firstOverflowNode = array[j];
					}
					else
					{
						int num2 = 1 << tableBits - length;
						HuffmanListNode huffmanListNode = array[j];
						for (int k = 0; k < num2; k++)
						{
							int num3 = (k << length) | huffmanListNode.Bits;
							while (list.Count <= num3)
							{
								list.Add(null);
							}
							list[num3] = huffmanListNode;
						}
					}
				}
				else
				{
					array[j - 1].Next = array[j];
				}
			}
			while (list.Count < 1 << tableBits)
			{
				list.Add(null);
			}
			return list;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002508 File Offset: 0x00000708
		private static int SortCallback(HuffmanListNode i1, HuffmanListNode i2)
		{
			int num = i1.Length - i2.Length;
			if (num == 0)
			{
				return i1.Bits - i2.Bits;
			}
			return num;
		}

		// Token: 0x0400000A RID: 10
		private const int MAX_TABLE_BITS = 10;
	}
}
