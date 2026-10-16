using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000507 RID: 1287
	internal static class NullableContextExtensions
	{
		// Token: 0x06006C86 RID: 27782 RVA: 0x00190958 File Offset: 0x0018EB58
		internal static bool TryGetByte(this NullableContextKind kind, out byte? value)
		{
			switch (kind)
			{
			case NullableContextKind.Unknown:
				value = default(byte?);
				return false;
			case NullableContextKind.None:
				value = default(byte?);
				return true;
			case NullableContextKind.Oblivious:
				value = new byte?(0);
				return true;
			case NullableContextKind.NotAnnotated:
				value = new byte?(1);
				return true;
			case NullableContextKind.Annotated:
				value = new byte?(2);
				return true;
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x001909C8 File Offset: 0x0018EBC8
		internal static NullableContextKind ToNullableContextFlags(this byte? value)
		{
			if (value == null)
			{
				return NullableContextKind.None;
			}
			switch (value.GetValueOrDefault())
			{
			case 0:
				return NullableContextKind.Oblivious;
			case 1:
				return NullableContextKind.NotAnnotated;
			case 2:
				return NullableContextKind.Annotated;
			default:
				throw ExceptionUtilities.UnexpectedValue(value);
			}
		}
	}
}
