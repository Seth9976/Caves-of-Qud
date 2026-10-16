using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200025E RID: 606
	[NullableContext(1)]
	[Nullable(0)]
	internal static class RefKindExtensions
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x00045BCF File Offset: 0x00043DCF
		internal static string ToParameterDisplayString(this RefKind kind)
		{
			switch (kind)
			{
			case RefKind.Ref:
				return "ref";
			case RefKind.Out:
				return "out";
			case RefKind.In:
				return "in";
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00045C04 File Offset: 0x00043E04
		internal static string ToArgumentDisplayString(this RefKind kind)
		{
			switch (kind)
			{
			case RefKind.Ref:
				return "ref";
			case RefKind.Out:
				return "out";
			case RefKind.In:
				return "in";
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x00045C39 File Offset: 0x00043E39
		internal static string ToParameterPrefix(this RefKind kind)
		{
			switch (kind)
			{
			case RefKind.None:
				return string.Empty;
			case RefKind.Ref:
				return "ref ";
			case RefKind.Out:
				return "out ";
			case RefKind.In:
				return "in ";
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x04000953 RID: 2387
		internal const RefKind StrictIn = (RefKind)4;
	}
}
