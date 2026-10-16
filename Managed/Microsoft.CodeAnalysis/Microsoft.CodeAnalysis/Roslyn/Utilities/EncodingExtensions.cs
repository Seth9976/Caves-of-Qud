using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslyn.Utilities
{
	// Token: 0x0200003C RID: 60
	[NullableContext(1)]
	[Nullable(0)]
	internal static class EncodingExtensions
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00004CC4 File Offset: 0x00002EC4
		internal static int GetMaxCharCountOrThrowIfHuge(this Encoding encoding, Stream stream)
		{
			long length = stream.Length;
			int num;
			if (encoding.TryGetMaxCharCount(length, out num))
			{
				return num;
			}
			throw new IOException(CodeAnalysisResources.StreamIsTooLong);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004CF0 File Offset: 0x00002EF0
		internal static bool TryGetMaxCharCount(this Encoding encoding, long length, out int maxCharCount)
		{
			maxCharCount = 0;
			if (length <= 2147483647L)
			{
				try
				{
					maxCharCount = encoding.GetMaxCharCount((int)length);
					return true;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				return false;
			}
			return false;
		}
	}
}
