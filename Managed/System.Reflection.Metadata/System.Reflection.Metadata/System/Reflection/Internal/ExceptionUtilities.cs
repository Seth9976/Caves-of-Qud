using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x0200016E RID: 366
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ExceptionUtilities
	{
		// Token: 0x06000B5B RID: 2907 RVA: 0x0002299C File Offset: 0x00020B9C
		internal static Exception UnexpectedValue(object value)
		{
			if (value != null && value.GetType().FullName != null)
			{
				return new InvalidOperationException(SR.Format(SR.UnexpectedValue, value, value.GetType().FullName));
			}
			return new InvalidOperationException(SR.Format(SR.UnexpectedValueUnknownType, value));
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000229DA File Offset: 0x00020BDA
		internal static Exception Unreachable
		{
			get
			{
				return new InvalidOperationException(SR.UnreachableLocation);
			}
		}
	}
}
