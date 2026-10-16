using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000059 RID: 89
	internal static class OperatorFacts
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00047E28 File Offset: 0x00046028
		public static bool DefinitelyHasNoUserDefinedOperators(TypeSymbol type)
		{
			TypeKind typeKind = type.TypeKind;
			if (typeKind != TypeKind.Class && typeKind != TypeKind.Interface && typeKind - TypeKind.Struct > 1)
			{
				return true;
			}
			switch (type.SpecialType)
			{
			case SpecialType.System_Object:
			case SpecialType.System_Enum:
			case SpecialType.System_MulticastDelegate:
			case SpecialType.System_Delegate:
			case SpecialType.System_ValueType:
			case SpecialType.System_Void:
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Decimal:
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			case SpecialType.System_String:
			case SpecialType.System_Array:
				break;
			case SpecialType.System_IntPtr:
				if (!type.IsNativeIntegerType)
				{
					return false;
				}
				break;
			case SpecialType.System_UIntPtr:
				if (!type.IsNativeIntegerType)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00047ECF File Offset: 0x000460CF
		public static string BinaryOperatorNameFromSyntaxKind(SyntaxKind kind)
		{
			return OperatorFacts.BinaryOperatorNameFromSyntaxKindIfAny(kind) ?? "op_Addition";
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00047EE0 File Offset: 0x000460E0
		internal static string BinaryOperatorNameFromSyntaxKindIfAny(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.LessThanToken)
			{
				switch (kind)
				{
				case SyntaxKind.PercentToken:
					return "op_Modulus";
				case SyntaxKind.CaretToken:
					return "op_ExclusiveOr";
				case SyntaxKind.AmpersandToken:
					return "op_BitwiseAnd";
				case SyntaxKind.AsteriskToken:
					return "op_Multiply";
				case SyntaxKind.OpenParenToken:
				case SyntaxKind.CloseParenToken:
					break;
				case SyntaxKind.MinusToken:
					return "op_Subtraction";
				case SyntaxKind.PlusToken:
					return "op_Addition";
				default:
					if (kind == SyntaxKind.BarToken)
					{
						return "op_BitwiseOr";
					}
					if (kind == SyntaxKind.LessThanToken)
					{
						return "op_LessThan";
					}
					break;
				}
			}
			else
			{
				if (kind == SyntaxKind.GreaterThanToken)
				{
					return "op_GreaterThan";
				}
				if (kind == SyntaxKind.SlashToken)
				{
					return "op_Division";
				}
				switch (kind)
				{
				case SyntaxKind.ExclamationEqualsToken:
					return "op_Inequality";
				case SyntaxKind.EqualsEqualsToken:
					return "op_Equality";
				case SyntaxKind.LessThanEqualsToken:
					return "op_LessThanOrEqual";
				case SyntaxKind.LessThanLessThanToken:
					return "op_LeftShift";
				case SyntaxKind.GreaterThanEqualsToken:
					return "op_GreaterThanOrEqual";
				case SyntaxKind.GreaterThanGreaterThanToken:
					return "op_RightShift";
				}
			}
			return null;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00047FD5 File Offset: 0x000461D5
		public static string UnaryOperatorNameFromSyntaxKind(SyntaxKind kind)
		{
			return OperatorFacts.UnaryOperatorNameFromSyntaxKindIfAny(kind) ?? "op_UnaryPlus";
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00047FE8 File Offset: 0x000461E8
		internal static string UnaryOperatorNameFromSyntaxKindIfAny(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.PlusToken)
			{
				if (kind <= SyntaxKind.ExclamationToken)
				{
					if (kind == SyntaxKind.TildeToken)
					{
						return "op_OnesComplement";
					}
					if (kind == SyntaxKind.ExclamationToken)
					{
						return "op_LogicalNot";
					}
				}
				else
				{
					if (kind == SyntaxKind.MinusToken)
					{
						return "op_UnaryNegation";
					}
					if (kind == SyntaxKind.PlusToken)
					{
						return "op_UnaryPlus";
					}
				}
			}
			else if (kind <= SyntaxKind.PlusPlusToken)
			{
				if (kind == SyntaxKind.MinusMinusToken)
				{
					return "op_Decrement";
				}
				if (kind == SyntaxKind.PlusPlusToken)
				{
					return "op_Increment";
				}
			}
			else
			{
				if (kind == SyntaxKind.TrueKeyword)
				{
					return "op_True";
				}
				if (kind == SyntaxKind.FalseKeyword)
				{
					return "op_False";
				}
			}
			return null;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00048086 File Offset: 0x00046286
		public static string OperatorNameFromDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.OperatorDeclarationSyntax declaration)
		{
			return OperatorFacts.OperatorNameFromDeclaration((Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.OperatorDeclarationSyntax)declaration.Green);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00048098 File Offset: 0x00046298
		public static string OperatorNameFromDeclaration(Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.OperatorDeclarationSyntax declaration)
		{
			SyntaxKind kind = declaration.OperatorToken.Kind;
			if (SyntaxFacts.IsBinaryExpressionOperatorToken(kind))
			{
				if (SyntaxFacts.IsPrefixUnaryExpressionOperatorToken(kind) && declaration.ParameterList.Parameters.Count == 1)
				{
					return OperatorFacts.UnaryOperatorNameFromSyntaxKind(kind);
				}
				return OperatorFacts.BinaryOperatorNameFromSyntaxKind(kind);
			}
			else
			{
				if (SyntaxFacts.IsUnaryOperatorDeclarationToken(kind))
				{
					return OperatorFacts.UnaryOperatorNameFromSyntaxKind(kind);
				}
				return "op_UnaryPlus";
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000480FC File Offset: 0x000462FC
		public static string UnaryOperatorNameFromOperatorKind(UnaryOperatorKind kind)
		{
			UnaryOperatorKind unaryOperatorKind = kind & UnaryOperatorKind.OpMask;
			if (unaryOperatorKind <= UnaryOperatorKind.UnaryPlus)
			{
				if (unaryOperatorKind <= UnaryOperatorKind.PostfixDecrement)
				{
					if (unaryOperatorKind != UnaryOperatorKind.PostfixIncrement)
					{
						if (unaryOperatorKind != UnaryOperatorKind.PostfixDecrement)
						{
							goto IL_00A8;
						}
						goto IL_0096;
					}
				}
				else if (unaryOperatorKind != UnaryOperatorKind.PrefixIncrement)
				{
					if (unaryOperatorKind == UnaryOperatorKind.PrefixDecrement)
					{
						goto IL_0096;
					}
					if (unaryOperatorKind != UnaryOperatorKind.UnaryPlus)
					{
						goto IL_00A8;
					}
					return "op_UnaryPlus";
				}
				return "op_Increment";
				IL_0096:
				return "op_Decrement";
			}
			if (unaryOperatorKind <= UnaryOperatorKind.LogicalNegation)
			{
				if (unaryOperatorKind == UnaryOperatorKind.UnaryMinus)
				{
					return "op_UnaryNegation";
				}
				if (unaryOperatorKind == UnaryOperatorKind.LogicalNegation)
				{
					return "op_LogicalNot";
				}
			}
			else
			{
				if (unaryOperatorKind == UnaryOperatorKind.BitwiseComplement)
				{
					return "op_OnesComplement";
				}
				if (unaryOperatorKind == UnaryOperatorKind.True)
				{
					return "op_True";
				}
				if (unaryOperatorKind == UnaryOperatorKind.False)
				{
					return "op_False";
				}
			}
			IL_00A8:
			throw ExceptionUtilities.UnexpectedValue(kind & UnaryOperatorKind.OpMask);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x000481C4 File Offset: 0x000463C4
		public static string BinaryOperatorNameFromOperatorKind(BinaryOperatorKind kind)
		{
			BinaryOperatorKind binaryOperatorKind = kind & BinaryOperatorKind.OpMask;
			if (binaryOperatorKind <= BinaryOperatorKind.Equal)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.Division)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.Addition)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Multiplication)
						{
							return "op_Multiply";
						}
						if (binaryOperatorKind == BinaryOperatorKind.Addition)
						{
							return "op_Addition";
						}
					}
					else
					{
						if (binaryOperatorKind == BinaryOperatorKind.Subtraction)
						{
							return "op_Subtraction";
						}
						if (binaryOperatorKind == BinaryOperatorKind.Division)
						{
							return "op_Division";
						}
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.LeftShift)
				{
					if (binaryOperatorKind == BinaryOperatorKind.Remainder)
					{
						return "op_Modulus";
					}
					if (binaryOperatorKind == BinaryOperatorKind.LeftShift)
					{
						return "op_LeftShift";
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.RightShift)
					{
						return "op_RightShift";
					}
					if (binaryOperatorKind == BinaryOperatorKind.Equal)
					{
						return "op_Equality";
					}
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.GreaterThanOrEqual)
			{
				if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
				{
					if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
					{
						return "op_Inequality";
					}
					if (binaryOperatorKind == BinaryOperatorKind.GreaterThan)
					{
						return "op_GreaterThan";
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.LessThan)
					{
						return "op_LessThan";
					}
					if (binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return "op_GreaterThanOrEqual";
					}
				}
			}
			else if (binaryOperatorKind <= BinaryOperatorKind.And)
			{
				if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
				{
					return "op_LessThanOrEqual";
				}
				if (binaryOperatorKind == BinaryOperatorKind.And)
				{
					return "op_BitwiseAnd";
				}
			}
			else
			{
				if (binaryOperatorKind == BinaryOperatorKind.Xor)
				{
					return "op_ExclusiveOr";
				}
				if (binaryOperatorKind == BinaryOperatorKind.Or)
				{
					return "op_BitwiseOr";
				}
			}
			throw ExceptionUtilities.UnexpectedValue(kind & BinaryOperatorKind.OpMask);
		}
	}
}
