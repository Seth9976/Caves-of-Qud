using System;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000F3 RID: 243
	internal sealed class UserStringHeapBuffer : StringHeapBuffer
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002040C File Offset: 0x0001E60C
		public override uint GetStringIndex(string @string)
		{
			uint position;
			if (this.strings.TryGetValue(@string, ref position))
			{
				return position;
			}
			position = (uint)this.position;
			this.WriteString(@string);
			this.strings.Add(@string, position);
			return position;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00020448 File Offset: 0x0001E648
		protected override void WriteString(string @string)
		{
			base.WriteCompressedUInt32((uint)(@string.Length * 2 + 1));
			byte b = 0;
			for (int i = 0; i < @string.Length; i++)
			{
				char c = @string.get_Chars(i);
				base.WriteUInt16((ushort)c);
				if (b != 1 && (c < ' ' || c > '~') && (c > '~' || (c >= '\u0001' && c <= '\b') || (c >= '\u000e' && c <= '\u001f') || c == '\'' || c == '-'))
				{
					b = 1;
				}
			}
			base.WriteByte(b);
		}
	}
}
