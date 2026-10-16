using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.Cecil.Metadata
{
	// Token: 0x020002AC RID: 684
	internal class StringHeap : Heap
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x000374A7 File Offset: 0x000356A7
		public StringHeap(byte[] data)
			: base(data)
		{
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000374BC File Offset: 0x000356BC
		public string Read(uint index)
		{
			if (index == 0U)
			{
				return string.Empty;
			}
			string text;
			if (this.strings.TryGetValue(index, out text))
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

		// Token: 0x06001129 RID: 4393 RVA: 0x00037518 File Offset: 0x00035718
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

		// Token: 0x040006A7 RID: 1703
		private readonly Dictionary<uint, string> strings = new Dictionary<uint, string>();
	}
}
