using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI;

namespace XRL.World.Tinkering
{
	// Token: 0x02000B36 RID: 2870
	public class BitCost : Dictionary<char, int>
	{
		// Token: 0x060073E3 RID: 29667 RVA: 0x002BB482 File Offset: 0x002B9682
		public BitCost()
			: base(BitType.BitTypes.Count)
		{
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x002BB494 File Offset: 0x002B9694
		public BitCost(int Size)
			: base(Size)
		{
		}

		// Token: 0x060073E5 RID: 29669 RVA: 0x002BB49D File Offset: 0x002B969D
		public BitCost(string Bits)
			: this(Math.Min(BitType.BitTypes.Count, Bits.Length))
		{
			this.Import(Bits);
		}

		// Token: 0x060073E6 RID: 29670 RVA: 0x002BB4C4 File Offset: 0x002B96C4
		public void Import(string Bits)
		{
			int i = 0;
			int length = Bits.Length;
			while (i < length)
			{
				this.Increment(Bits[i]);
				i++;
			}
		}

		// Token: 0x060073E7 RID: 29671 RVA: 0x002BB4F4 File Offset: 0x002B96F4
		public void CopyTo(BitCost Other)
		{
			Other.Clear();
			foreach (KeyValuePair<char, int> keyValuePair in this)
			{
				Other[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x002BB558 File Offset: 0x002B9758
		public void AddTo(BitCost Other)
		{
			foreach (KeyValuePair<char, int> keyValuePair in this)
			{
				int i = 0;
				int value = keyValuePair.Value;
				while (i < value)
				{
					Other.Increment(keyValuePair.Key);
					i++;
				}
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x002BB5C4 File Offset: 0x002B97C4
		public int Total
		{
			get
			{
				int num = 0;
				foreach (int num2 in base.Values)
				{
					num += num2;
				}
				return num;
			}
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x002BB618 File Offset: 0x002B9818
		public string ToBits()
		{
			BitCost.SB.Clear();
			int i = 0;
			int count = BitType.BitOrder.Count;
			while (i < count)
			{
				char c = BitType.BitOrder[i];
				int num;
				if (base.TryGetValue(c, out num))
				{
					for (int j = 0; j < num; j++)
					{
						BitCost.SB.Append(c);
					}
				}
				i++;
			}
			return BitCost.SB.ToString();
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x002BB684 File Offset: 0x002B9884
		public override string ToString()
		{
			BitCost.SB.Clear();
			this.ToStringBuilder(BitCost.SB);
			return BitCost.SB.ToString();
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x002BB6A8 File Offset: 0x002B98A8
		public void ToStringBuilder(StringBuilder SB)
		{
			if (Options.AlphanumericBits)
			{
				int i = 0;
				int count = BitType.BitOrder.Count;
				while (i < count)
				{
					char c = BitType.BitOrder[i];
					int num;
					if (base.TryGetValue(c, out num))
					{
						char c2 = BitType.CharTranslateBit(c);
						SB.Append("{{").Append(c).Append('|');
						for (int j = 0; j < num; j++)
						{
							SB.Append(c2);
						}
						SB.Append("}}");
					}
					i++;
				}
				return;
			}
			int k = 0;
			int count2 = BitType.BitOrder.Count;
			while (k < count2)
			{
				char c3 = BitType.BitOrder[k];
				int num2;
				if (base.TryGetValue(c3, out num2))
				{
					BitType.CharTranslateBit(c3);
					SB.Append("{{").Append(c3).Append('|');
					for (int l = 0; l < num2; l++)
					{
						SB.Append('\a');
					}
					SB.Append("}}");
				}
				k++;
			}
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x002BB7B0 File Offset: 0x002B99B0
		public int GetHighestTier()
		{
			int num = 0;
			foreach (char c in base.Keys)
			{
				int bitTier = BitType.GetBitTier(c);
				if (bitTier > num)
				{
					num = bitTier;
				}
			}
			return num;
		}

		// Token: 0x0400355C RID: 13660
		[NonSerialized]
		private static StringBuilder SB = new StringBuilder();
	}
}
