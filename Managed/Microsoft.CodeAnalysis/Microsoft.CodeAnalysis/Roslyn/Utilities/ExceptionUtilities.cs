using System;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ExceptionUtilities
	{
		// Token: 0x06000155 RID: 341 RVA: 0x000056F1 File Offset: 0x000038F1
		internal static Exception UnexpectedValue([Nullable(2)] object o)
		{
			return new InvalidOperationException(string.Format("Unexpected value '{0}' of type '{1}'", o, (o != null) ? o.GetType().FullName : "<unknown>"));
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005718 File Offset: 0x00003918
		internal static Exception Unreachable
		{
			get
			{
				return new InvalidOperationException("This program location is thought to be unreachable.");
			}
		}
	}
}
