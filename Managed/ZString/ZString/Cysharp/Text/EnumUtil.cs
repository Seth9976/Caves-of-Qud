using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cysharp.Text
{
	// Token: 0x02000012 RID: 18
	internal static class EnumUtil<[Nullable(2)] T>
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000892C File Offset: 0x00006B2C
		static EnumUtil()
		{
			string[] array = Enum.GetNames(typeof(T));
			Array values = Enum.GetValues(typeof(T));
			EnumUtil<T>.names = new Dictionary<T, string>(array.Length);
			EnumUtil<T>.utf8names = new Dictionary<T, byte[]>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				T t = (T)((object)values.GetValue(i));
				if (EnumUtil<T>.names.ContainsKey(t))
				{
					EnumUtil<T>.names[t] = "$";
					EnumUtil<T>.utf8names[t] = Array.Empty<byte>();
				}
				else
				{
					EnumUtil<T>.names.Add(t, array[i]);
					EnumUtil<T>.utf8names.Add(t, Encoding.UTF8.GetBytes(array[i]));
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000089E4 File Offset: 0x00006BE4
		public static bool TryFormatUtf16([Nullable(1)] T value, Span<char> dest, out int written, ReadOnlySpan<char> _)
		{
			string text;
			if (!EnumUtil<T>.names.TryGetValue(value, out text) || text == "$")
			{
				text = value.ToString();
			}
			written = text.Length;
			return text.AsSpan().TryCopyTo(dest);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00008A34 File Offset: 0x00006C34
		public static bool TryFormatUtf8([Nullable(1)] T value, Span<byte> dest, out int written, StandardFormat _)
		{
			byte[] bytes;
			if (!EnumUtil<T>.utf8names.TryGetValue(value, out bytes) || bytes.Length == 0)
			{
				bytes = Encoding.UTF8.GetBytes(value.ToString());
			}
			written = bytes.Length;
			return bytes.AsSpan<byte>().TryCopyTo(dest);
		}

		// Token: 0x04000034 RID: 52
		[Nullable(1)]
		private const string InvalidName = "$";

		// Token: 0x04000035 RID: 53
		[Nullable(1)]
		private static readonly Dictionary<T, string> names;

		// Token: 0x04000036 RID: 54
		[Nullable(1)]
		private static readonly Dictionary<T, byte[]> utf8names;
	}
}
