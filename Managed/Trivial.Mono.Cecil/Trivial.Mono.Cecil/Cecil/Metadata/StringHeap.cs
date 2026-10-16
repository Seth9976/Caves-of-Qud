using System;
using System.Collections.Generic;
using System.Text;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000EC RID: 236
	internal class StringHeap : Heap
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0001FCE7 File Offset: 0x0001DEE7
		public StringHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		public string Read(uint index)
		{
			if (index == 0U)
			{
				return string.Empty;
			}
			string text;
			if (this.strings.TryGetValue(index, ref text))
			{
				return text;
			}
			if ((ulong)index > (ulong)((long)(this.data.Length - 1)))
			{
				return string.Empty;
			}
			text = this.ReadStringAt(index);
			if (text.Length != 0)
			{
				this.strings.Add(index, text);
			}
			return text;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001FD58 File Offset: 0x0001DF58
		protected virtual string ReadStringAt(uint index)
		{
			int num = 0;
			int num2 = (int)index;
			while (this.data[num2] != 0)
			{
				num++;
				num2++;
			}
			return Encoding.UTF8.GetString(this.data, (int)index, num);
		}

		// Token: 0x040003D5 RID: 981
		private readonly Dictionary<uint, string> strings = new Dictionary<uint, string>();
	}
}
