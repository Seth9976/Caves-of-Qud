using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001234 RID: 4660
	[Serializable]
	public class BitLocker : IPart
	{
		// Token: 0x0600B91B RID: 47387 RVA: 0x00439103 File Offset: 0x00437303
		public override IPart DeepCopy(GameObject Parent)
		{
			BitLocker bitLocker = base.DeepCopy(Parent) as BitLocker;
			bitLocker.BitStorage = new Dictionary<char, int>(this.BitStorage);
			return bitLocker;
		}

		// Token: 0x0600B91C RID: 47388 RVA: 0x00439124 File Offset: 0x00437324
		public int GetTotalBitCount()
		{
			int num = 0;
			foreach (int num2 in this.BitStorage.Values)
			{
				num += num2;
			}
			return num;
		}

		// Token: 0x0600B91D RID: 47389 RVA: 0x0043917C File Offset: 0x0043737C
		public string GetBitsString()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			char[] array = this.BitStorage.Keys.ToArray<char>();
			BitType.SortBits(array);
			foreach (char c in array)
			{
				if (BitType.BitSortOrder.ContainsKey(c) && this.BitStorage[c] > 0)
				{
					stringBuilder.Append(BitType.GetString(c)).Append(" x{{C|").Append(this.BitStorage[c])
						.Append("}} - ")
						.Append(BitType.BitMap[c].Description)
						.Append("\n");
				}
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString();
			}
			return "no bits";
		}

		// Token: 0x0600B91E RID: 47390 RVA: 0x0043923C File Offset: 0x0043743C
		public string GetBitsSummary()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			char[] array = this.BitStorage.Keys.ToArray<char>();
			BitType.SortBits(array);
			foreach (char c in array)
			{
				if (BitType.BitSortOrder.ContainsKey(c) && this.BitStorage[c] > 0)
				{
					stringBuilder.Append(BitType.TranslateBit(c)).Append(this.BitStorage[c]);
				}
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString();
			}
			return "-";
		}

		// Token: 0x0600B91F RID: 47391 RVA: 0x004392C8 File Offset: 0x004374C8
		public void AddAllBits(int num)
		{
			foreach (char c in BitType.BitSortOrder.Keys)
			{
				if (!this.BitStorage.ContainsKey(c))
				{
					this.BitStorage.Add(c, num);
				}
				else
				{
					Dictionary<char, int> bitStorage = this.BitStorage;
					char c2 = c;
					bitStorage[c2] += num;
				}
			}
		}

		// Token: 0x0600B920 RID: 47392 RVA: 0x00439350 File Offset: 0x00437550
		public void AddBits(string Bits)
		{
			for (int i = 0; i < Bits.Length; i++)
			{
				if (!BitType.BitSortOrder.ContainsKey(Bits[i]))
				{
					MetricsManager.LogWarning(string.Format("Couldn't find bit type {0}", Bits[i]));
				}
				else
				{
					if (!this.BitStorage.ContainsKey(Bits[i]))
					{
						this.BitStorage.Add(Bits[i], 0);
					}
					Dictionary<char, int> bitStorage = this.BitStorage;
					char c = Bits[i];
					int num = bitStorage[c];
					bitStorage[c] = num + 1;
				}
			}
		}

		// Token: 0x0600B921 RID: 47393 RVA: 0x004393E8 File Offset: 0x004375E8
		public bool UseBits(string Bits)
		{
			if (!this.HasBits(Bits))
			{
				return false;
			}
			for (int i = 0; i < Bits.Length; i++)
			{
				Dictionary<char, int> bitStorage = this.BitStorage;
				char c = Bits[i];
				int num = bitStorage[c];
				bitStorage[c] = num - 1;
			}
			return true;
		}

		// Token: 0x0600B922 RID: 47394 RVA: 0x00439434 File Offset: 0x00437634
		public bool UseBits(Dictionary<char, int> Bits)
		{
			if (!this.HasBits(Bits))
			{
				return false;
			}
			foreach (KeyValuePair<char, int> keyValuePair in Bits)
			{
				if (this.BitStorage.ContainsKey(keyValuePair.Key))
				{
					Dictionary<char, int> bitStorage = this.BitStorage;
					char key = keyValuePair.Key;
					bitStorage[key] -= keyValuePair.Value;
				}
			}
			return true;
		}

		// Token: 0x0600B923 RID: 47395 RVA: 0x004394C0 File Offset: 0x004376C0
		public bool UseBits(char Bit, int Number)
		{
			if (this.GetBitCount(Bit) < Number)
			{
				return false;
			}
			Dictionary<char, int> bitStorage = this.BitStorage;
			bitStorage[Bit] -= Number;
			return true;
		}

		// Token: 0x0600B924 RID: 47396 RVA: 0x004394F3 File Offset: 0x004376F3
		public bool HasBits(string Bits)
		{
			return this.HasBits(new BitCost(Bits));
		}

		// Token: 0x0600B925 RID: 47397 RVA: 0x00439504 File Offset: 0x00437704
		public bool HasBits(Dictionary<char, int> Bits)
		{
			foreach (KeyValuePair<char, int> keyValuePair in Bits)
			{
				if (BitType.BitSortOrder.ContainsKey(keyValuePair.Key))
				{
					int num;
					if (!this.BitStorage.TryGetValue(keyValuePair.Key, out num))
					{
						return false;
					}
					if (num < keyValuePair.Value)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600B926 RID: 47398 RVA: 0x0043958C File Offset: 0x0043778C
		public int GetBitCount(char Bit)
		{
			int num;
			this.BitStorage.TryGetValue(Bit, out num);
			return num;
		}

		// Token: 0x0600B927 RID: 47399 RVA: 0x004395AC File Offset: 0x004377AC
		public static bool HasBits(GameObject who, string Bits)
		{
			BitLocker part = who.GetPart<BitLocker>();
			return part != null && part.HasBits(Bits);
		}

		// Token: 0x0600B928 RID: 47400 RVA: 0x004395CC File Offset: 0x004377CC
		public static bool HasBits(GameObject who, Dictionary<char, int> Bits)
		{
			BitLocker part = who.GetPart<BitLocker>();
			return part != null && part.HasBits(Bits);
		}

		// Token: 0x0600B929 RID: 47401 RVA: 0x004395EC File Offset: 0x004377EC
		public static int GetBitCount(GameObject who, char Bit)
		{
			BitLocker part = who.GetPart<BitLocker>();
			if (part == null)
			{
				return 0;
			}
			return part.GetBitCount(Bit);
		}

		// Token: 0x0600B92A RID: 47402 RVA: 0x0043960C File Offset: 0x0043780C
		public static bool UseBits(GameObject who, string Bits)
		{
			BitLocker part = who.GetPart<BitLocker>();
			return part != null && part.UseBits(Bits);
		}

		// Token: 0x0600B92B RID: 47403 RVA: 0x0043962C File Offset: 0x0043782C
		public static bool UseBits(GameObject who, Dictionary<char, int> Bits)
		{
			BitLocker part = who.GetPart<BitLocker>();
			return part != null && part.UseBits(Bits);
		}

		// Token: 0x0600B92C RID: 47404 RVA: 0x0043964C File Offset: 0x0043784C
		public static bool UseBits(GameObject who, char Bit, int Number)
		{
			BitLocker part = who.GetPart<BitLocker>();
			return part != null && part.UseBits(Bit, Number);
		}

		// Token: 0x04004A7D RID: 19069
		public Dictionary<char, int> BitStorage = new Dictionary<char, int>();
	}
}
