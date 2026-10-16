using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000013 RID: 19
	internal static class ExceptionUtil
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00008A80 File Offset: 0x00006C80
		[NullableContext(1)]
		internal static void ThrowArgumentException(string paramName)
		{
			throw new ArgumentException("Can't format argument.", paramName);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008A8D File Offset: 0x00006C8D
		internal static void ThrowFormatException()
		{
			throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008A99 File Offset: 0x00006C99
		internal static void ThrowFormatError()
		{
			throw new FormatException("Input string was not in a correct format.");
		}
	}
}
