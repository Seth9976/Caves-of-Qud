using System;
using System.Collections.Generic;
using System.Text;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Tinkering
{
	// Token: 0x02000B37 RID: 2871
	[HasModSensitiveStaticCache]
	public class BitType
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060073EF RID: 29679 RVA: 0x002BB818 File Offset: 0x002B9A18
		public static List<BitType> BitTypes
		{
			get
			{
				BitType.CheckInit();
				return BitType._BitTypes;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060073F0 RID: 29680 RVA: 0x002BB824 File Offset: 0x002B9A24
		public static Dictionary<char, BitType> BitMap
		{
			get
			{
				BitType.CheckInit();
				return BitType._BitMap;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060073F1 RID: 29681 RVA: 0x002BB830 File Offset: 0x002B9A30
		public static Dictionary<int, List<BitType>> LevelMap
		{
			get
			{
				BitType.CheckInit();
				return BitType._LevelMap;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060073F2 RID: 29682 RVA: 0x002BB83C File Offset: 0x002B9A3C
		public static List<char> BitOrder
		{
			get
			{
				BitType.CheckInit();
				return BitType._BitOrder;
			}
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x002BB848 File Offset: 0x002B9A48
		public static int GetBitSortOrder(char bit)
		{
			int num = 0;
			if (BitType.BitSortOrder.TryGetValue(bit, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060073F4 RID: 29684 RVA: 0x002BB869 File Offset: 0x002B9A69
		public static Dictionary<char, int> BitSortOrder
		{
			get
			{
				BitType.CheckInit();
				return BitType._BitSortOrder;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060073F5 RID: 29685 RVA: 0x002BB875 File Offset: 0x002B9A75
		public static Dictionary<int, char> TierBits
		{
			get
			{
				BitType.CheckInit();
				return BitType._TierBits;
			}
		}

		// Token: 0x060073F6 RID: 29686 RVA: 0x002BB881 File Offset: 0x002B9A81
		public static void CheckInit()
		{
			if (BitType._BitTypes == null)
			{
				Loading.LoadTask("Initialize tinkering", new Action(BitType.Init), true);
			}
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x002BB8A1 File Offset: 0x002B9AA1
		public BitType(int Level, char Color, string Description)
		{
			this.Level = Level;
			this.Color = Color;
			this.Description = Description;
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x002BB8C0 File Offset: 0x002B9AC0
		public static string TranslateBit(char Bit)
		{
			if (Bit <= 'M')
			{
				if (Bit <= 'C')
				{
					if (Bit == '0')
					{
						return "A";
					}
					if (Bit == 'B')
					{
						return "C";
					}
					if (Bit == 'C')
					{
						return "D";
					}
				}
				else
				{
					if (Bit == 'G')
					{
						return "B";
					}
					if (Bit == 'K')
					{
						return "5";
					}
					if (Bit == 'M')
					{
						return "8";
					}
				}
			}
			else if (Bit <= 'Y')
			{
				if (Bit == 'R')
				{
					return "A";
				}
				if (Bit == 'W')
				{
					return "6";
				}
				if (Bit == 'Y')
				{
					return "7";
				}
			}
			else if (Bit <= 'c')
			{
				if (Bit == 'b')
				{
					return "3";
				}
				if (Bit == 'c')
				{
					return "4";
				}
			}
			else
			{
				if (Bit == 'g')
				{
					return "2";
				}
				if (Bit == 'r')
				{
					return "1";
				}
			}
			return "?";
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x002BB988 File Offset: 0x002B9B88
		public static char CharTranslateBit(char Bit)
		{
			if (Bit <= 'M')
			{
				if (Bit <= 'C')
				{
					if (Bit == '0')
					{
						return 'A';
					}
					if (Bit == 'B')
					{
						return 'C';
					}
					if (Bit == 'C')
					{
						return 'D';
					}
				}
				else
				{
					if (Bit == 'G')
					{
						return 'B';
					}
					if (Bit == 'K')
					{
						return '5';
					}
					if (Bit == 'M')
					{
						return '8';
					}
				}
			}
			else if (Bit <= 'Y')
			{
				if (Bit == 'R')
				{
					return 'A';
				}
				if (Bit == 'W')
				{
					return '6';
				}
				if (Bit == 'Y')
				{
					return '7';
				}
			}
			else if (Bit <= 'c')
			{
				if (Bit == 'b')
				{
					return '3';
				}
				if (Bit == 'c')
				{
					return '4';
				}
			}
			else
			{
				if (Bit == 'g')
				{
					return '2';
				}
				if (Bit == 'r')
				{
					return '1';
				}
			}
			return '?';
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x002BBA20 File Offset: 0x002B9C20
		public static int GetBitTier(char Bit)
		{
			if (Bit <= 'R')
			{
				if (Bit <= 'G')
				{
					if (Bit == 'B')
					{
						return 0;
					}
					if (Bit == 'C')
					{
						return 0;
					}
					if (Bit == 'G')
					{
						return 0;
					}
				}
				else
				{
					if (Bit == 'K')
					{
						return 5;
					}
					if (Bit == 'M')
					{
						return 8;
					}
					if (Bit == 'R')
					{
						return 0;
					}
				}
			}
			else if (Bit <= 'b')
			{
				if (Bit == 'W')
				{
					return 6;
				}
				if (Bit == 'Y')
				{
					return 7;
				}
				if (Bit == 'b')
				{
					return 3;
				}
			}
			else
			{
				if (Bit == 'c')
				{
					return 4;
				}
				if (Bit == 'g')
				{
					return 2;
				}
				if (Bit == 'r')
				{
					return 1;
				}
			}
			return 0;
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x002BBA9C File Offset: 0x002B9C9C
		public static string ReverseTranslateBit(char Bit)
		{
			switch (Bit)
			{
			case '1':
				return "r";
			case '2':
				return "g";
			case '3':
				return "b";
			case '4':
				return "c";
			case '5':
				return "K";
			case '6':
				return "W";
			case '7':
				return "Y";
			case '8':
				return "M";
			case 'A':
				return "R";
			case 'B':
				return "G";
			case 'C':
				return "B";
			case 'D':
				return "C";
			}
			return "?";
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x002BBB54 File Offset: 0x002B9D54
		public static char ReverseCharTranslateBit(char Bit)
		{
			switch (Bit)
			{
			case '1':
				return 'r';
			case '2':
				return 'g';
			case '3':
				return 'b';
			case '4':
				return 'c';
			case '5':
				return 'K';
			case '6':
				return 'W';
			case '7':
				return 'Y';
			case '8':
				return 'M';
			case 'A':
				return 'R';
			case 'B':
				return 'G';
			case 'C':
				return 'B';
			case 'D':
				return 'C';
			}
			return '?';
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x002BBBE2 File Offset: 0x002B9DE2
		public static void SortBits(char[] Bits)
		{
			Array.Sort<char>(Bits, (char a, char b) => BitType.GetBitSortOrder(a).CompareTo(BitType.GetBitSortOrder(b)));
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x002BBC09 File Offset: 0x002B9E09
		public static void SortBits(List<char> Bits)
		{
			Bits.Sort((char a, char b) => BitType.GetBitSortOrder(a).CompareTo(BitType.GetBitSortOrder(b)));
		}

		// Token: 0x060073FF RID: 29695 RVA: 0x002BBC30 File Offset: 0x002B9E30
		public static void SortBits(List<string> Bits)
		{
			Bits.Sort((string a, string b) => BitType.GetBitSortOrder(a[0]).CompareTo(BitType.GetBitSortOrder(b[0])));
		}

		// Token: 0x06007400 RID: 29696 RVA: 0x002BBC58 File Offset: 0x002B9E58
		public static string GetString(string Bits)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (Options.AlphanumericBits)
			{
				for (int i = 0; i < Bits.Length; i++)
				{
					char c = BitType.CharTranslateBit(Bits[i]);
					stringBuilder.Append("{{").Append(BitType.ReverseTranslateBit(c)).Append("|")
						.Append(c)
						.Append("}}");
				}
			}
			else
			{
				for (int j = 0; j < Bits.Length; j++)
				{
					char c2 = BitType.CharTranslateBit(Bits[j]);
					stringBuilder.Append("{{").Append(BitType.ReverseTranslateBit(c2)).Append("|")
						.Append('\a')
						.Append("}}");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x002BBD1B File Offset: 0x002B9F1B
		public static string GetString(char Bit)
		{
			return BitType.GetString(Bit.ToString());
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x002BBD2C File Offset: 0x002B9F2C
		public static string GetDisplayString(string Bits)
		{
			if (Bits.Length > 20)
			{
				char[] array = new char[Bits.Length];
				int i = 0;
				int length = Bits.Length;
				while (i < length)
				{
					array[i] = Bits[i];
					i++;
				}
				BitType.SortBits(array);
				bool alphanumericBits = Options.AlphanumericBits;
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				for (int j = 0; j < array.Length; j++)
				{
					char c = array[j];
					int num = 1;
					while (j < array.Length - 1 && array[j + 1] == c)
					{
						j++;
						num++;
					}
					c = BitType.CharTranslateBit(c);
					if (num <= 4)
					{
						stringBuilder.Append("{{").Append(BitType.ReverseTranslateBit(c)).Append('|');
						for (int k = 0; k < num; k++)
						{
							stringBuilder.Append(alphanumericBits ? c : '\a');
						}
						stringBuilder.Append("}}");
					}
					else
					{
						stringBuilder.Append("{{").Append(BitType.ReverseTranslateBit(c)).Append('|')
							.Append(alphanumericBits ? c : '\a')
							.Append("}}(")
							.Append(num)
							.Append(')');
					}
				}
				return stringBuilder.ToString();
			}
			return BitType.GetString(Bits);
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x002BBE70 File Offset: 0x002BA070
		public static string ToRealBits(string BitTemplate, string Blueprint)
		{
			BitType.CheckInit();
			if (Blueprint == null)
			{
				Blueprint = "dummyvalue";
			}
			List<string> list = new List<string>();
			Random random = null;
			for (int i = 0; i < BitTemplate.Length; i++)
			{
				if (BitType._BitMap.ContainsKey(BitTemplate[i]))
				{
					list.Add(BitTemplate[i].ToString());
				}
				else
				{
					int num = Convert.ToInt32(BitTemplate[i].ToString());
					int num2 = 1;
					bool flag = false;
					if (random == null)
					{
						random = Stat.GetSeededRandomGenerator(Blueprint);
					}
					if (random.Next(0, 101) <= 0)
					{
						flag = true;
					}
					while (num > 0 && flag)
					{
						num--;
						num2++;
						if (random.Next(0, 101) <= 90)
						{
							flag = false;
						}
					}
					for (int j = 0; j < num2; j++)
					{
						int count = BitType.LevelMap[num].Count;
						list.Add(BitType.LevelMap[num].GetRandomElement<BitType>(null).Color.ToString());
					}
				}
			}
			BitType.SortBits(list);
			StringBuilder stringBuilder = new StringBuilder();
			for (int k = 0; k < list.Count; k++)
			{
				stringBuilder.Append(list[k]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x002BBFB4 File Offset: 0x002BA1B4
		public static string GetRandomBits(int Num, int UpToLevel = 8)
		{
			if (Num < 1 || UpToLevel < 0)
			{
				return null;
			}
			List<char> list = new List<char>(BitType.BitTypes.Count);
			foreach (BitType bitType in BitType.BitTypes)
			{
				if (bitType.Level <= UpToLevel)
				{
					list.Add(bitType.Color);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			char[] array = new char[Num];
			for (int i = 0; i < Num; i++)
			{
				array[i] = list.GetRandomElement<char>(null);
			}
			BitType.SortBits(array);
			return new string(array);
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x002BC068 File Offset: 0x002BA268
		private static void AddType(BitType NewType, bool TierPriority = false, bool visible = true)
		{
			BitType._BitSortOrder.Add(NewType.Color, BitType._BitTypes.Count);
			if (visible)
			{
				BitType._BitTypes.Add(NewType);
			}
			if (visible)
			{
				BitType._BitOrder.Add(NewType.Color);
			}
			BitType._BitMap.Add(NewType.Color, NewType);
			List<BitType> list;
			if (BitType._LevelMap.TryGetValue(NewType.Level, out list))
			{
				list.Add(NewType);
			}
			else
			{
				BitType._LevelMap.Add(NewType.Level, new List<BitType> { NewType });
			}
			if (TierPriority || !BitType._TierBits.ContainsKey(NewType.Level))
			{
				BitType._TierBits[NewType.Level] = NewType.Color;
			}
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x002BC128 File Offset: 0x002BA328
		public static void Init()
		{
			int num = 12;
			BitType._BitTypes = new List<BitType>(num);
			BitType._LevelMap = new Dictionary<int, List<BitType>>(num);
			BitType._BitMap = new Dictionary<char, BitType>(num);
			BitType._BitOrder = new List<char>(num);
			BitType._BitSortOrder = new Dictionary<char, int>(num);
			BitType._TierBits = new Dictionary<int, char>(num);
			BitType.AddType(new BitType(0, 'R', "{{R|scrap power systems}}"), false, true);
			BitType.AddType(new BitType(0, 'G', "{{G|scrap crystal}}"), false, true);
			BitType.AddType(new BitType(0, 'B', "{{B|scrap metal}}"), true, true);
			BitType.AddType(new BitType(0, 'C', "{{C|scrap electronics}}"), false, true);
			BitType.AddType(new BitType(1, 'r', "{{r|phasic power systems}}"), false, true);
			BitType.AddType(new BitType(2, 'g', "{{g|flawless crystal}}"), false, true);
			BitType.AddType(new BitType(3, 'b', "{{b|pure alloy}}"), false, true);
			BitType.AddType(new BitType(4, 'c', "{{c|pristine electronics}}"), false, true);
			BitType.AddType(new BitType(5, 'K', "{{K|nanomaterials}}"), false, true);
			BitType.AddType(new BitType(6, 'W', "{{W|photonics}}"), false, true);
			BitType.AddType(new BitType(7, 'Y', "{{Y|AI microcontrollers}}"), false, true);
			BitType.AddType(new BitType(8, 'M', "{{M|metacrystal}}"), false, true);
		}

		// Token: 0x0400355D RID: 13661
		public int Level;

		// Token: 0x0400355E RID: 13662
		public char Color;

		// Token: 0x0400355F RID: 13663
		public string Description;

		// Token: 0x04003560 RID: 13664
		[ModSensitiveStaticCache(false)]
		private static List<BitType> _BitTypes;

		// Token: 0x04003561 RID: 13665
		[ModSensitiveStaticCache(false)]
		private static Dictionary<char, BitType> _BitMap;

		// Token: 0x04003562 RID: 13666
		[ModSensitiveStaticCache(false)]
		public static Dictionary<int, List<BitType>> _LevelMap;

		// Token: 0x04003563 RID: 13667
		[ModSensitiveStaticCache(false)]
		public static List<char> _BitOrder;

		// Token: 0x04003564 RID: 13668
		[ModSensitiveStaticCache(false)]
		public static Dictionary<char, int> _BitSortOrder;

		// Token: 0x04003565 RID: 13669
		[ModSensitiveStaticCache(false)]
		public static Dictionary<int, char> _TierBits;
	}
}
