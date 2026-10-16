using System;
using System.Collections.Generic;
using System.Text;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000ED RID: 237
	internal class StringHeapBuffer : HeapBuffer
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001F952 File Offset: 0x0001DB52
		public sealed override bool IsEmpty
		{
			get
			{
				return this.length <= 1;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001FD8F File Offset: 0x0001DF8F
		public StringHeapBuffer()
			: base(1)
		{
			base.WriteByte(0);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		public virtual uint GetStringIndex(string @string)
		{
			uint num;
			if (this.strings.TryGetValue(@string, ref num))
			{
				return num;
			}
			num = (uint)(this.strings.Count + 1);
			this.strings.Add(@string, num);
			return num;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		public uint[] WriteStrings()
		{
			List<KeyValuePair<string, uint>> list = StringHeapBuffer.SortStrings(this.strings);
			this.strings = null;
			uint[] array = new uint[list.Count + 1];
			array[0] = 0U;
			string text = string.Empty;
			foreach (KeyValuePair<string, uint> keyValuePair in list)
			{
				string key = keyValuePair.Key;
				uint value = keyValuePair.Value;
				int position = this.position;
				if (text.EndsWith(key, 4) && !StringHeapBuffer.IsLowSurrogateChar((int)keyValuePair.Key.get_Chars(0)))
				{
					array[(int)value] = (uint)(position - (Encoding.UTF8.GetByteCount(keyValuePair.Key) + 1));
				}
				else
				{
					array[(int)value] = (uint)position;
					this.WriteString(key);
				}
				text = keyValuePair.Key;
			}
			return array;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001FECC File Offset: 0x0001E0CC
		private static List<KeyValuePair<string, uint>> SortStrings(Dictionary<string, uint> strings)
		{
			List<KeyValuePair<string, uint>> list = new List<KeyValuePair<string, uint>>(strings);
			list.Sort(new StringHeapBuffer.SuffixSort());
			return list;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0001FEEC File Offset: 0x0001E0EC
		private static bool IsLowSurrogateChar(int c)
		{
			return c - 56320 <= 1023;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001FEFF File Offset: 0x0001E0FF
		protected virtual void WriteString(string @string)
		{
			base.WriteBytes(Encoding.UTF8.GetBytes(@string));
			base.WriteByte(0);
		}

		// Token: 0x040003D6 RID: 982
		protected Dictionary<string, uint> strings = new Dictionary<string, uint>(StringComparer.Ordinal);

		// Token: 0x0200014F RID: 335
		private class SuffixSort : IComparer<KeyValuePair<string, uint>>
		{
			// Token: 0x06000BE9 RID: 3049 RVA: 0x000265FC File Offset: 0x000247FC
			public int Compare(KeyValuePair<string, uint> xPair, KeyValuePair<string, uint> yPair)
			{
				string key = xPair.Key;
				string key2 = yPair.Key;
				int num = key.Length - 1;
				int num2 = key2.Length - 1;
				while (num >= 0 && num2 >= 0)
				{
					if (key.get_Chars(num) < key2.get_Chars(num2))
					{
						return -1;
					}
					if (key.get_Chars(num) > key2.get_Chars(num2))
					{
						return 1;
					}
					num--;
					num2--;
				}
				return key2.Length.CompareTo(key.Length);
			}
		}
	}
}
