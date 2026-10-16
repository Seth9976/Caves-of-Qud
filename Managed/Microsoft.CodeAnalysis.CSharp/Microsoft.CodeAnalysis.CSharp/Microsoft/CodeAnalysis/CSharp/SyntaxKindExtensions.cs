using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000235 RID: 565
	internal static class SyntaxKindExtensions
	{
		// Token: 0x06002EEB RID: 12011 RVA: 0x000F14C8 File Offset: 0x000EF6C8
		internal static SpecialType GetSpecialType(this SyntaxKind kind)
		{
			switch (kind)
			{
			case SyntaxKind.BoolKeyword:
				return SpecialType.System_Boolean;
			case SyntaxKind.ByteKeyword:
				return SpecialType.System_Byte;
			case SyntaxKind.SByteKeyword:
				return SpecialType.System_SByte;
			case SyntaxKind.ShortKeyword:
				return SpecialType.System_Int16;
			case SyntaxKind.UShortKeyword:
				return SpecialType.System_UInt16;
			case SyntaxKind.IntKeyword:
				return SpecialType.System_Int32;
			case SyntaxKind.UIntKeyword:
				return SpecialType.System_UInt32;
			case SyntaxKind.LongKeyword:
				return SpecialType.System_Int64;
			case SyntaxKind.ULongKeyword:
				return SpecialType.System_UInt64;
			case SyntaxKind.DoubleKeyword:
				return SpecialType.System_Double;
			case SyntaxKind.FloatKeyword:
				return SpecialType.System_Single;
			case SyntaxKind.DecimalKeyword:
				return SpecialType.System_Decimal;
			case SyntaxKind.StringKeyword:
				return SpecialType.System_String;
			case SyntaxKind.CharKeyword:
				return SpecialType.System_Char;
			case SyntaxKind.VoidKeyword:
				return SpecialType.System_Void;
			case SyntaxKind.ObjectKeyword:
				return SpecialType.System_Object;
			default:
				throw ExceptionUtilities.UnexpectedValue(kind);
			}
		}
	}
}
