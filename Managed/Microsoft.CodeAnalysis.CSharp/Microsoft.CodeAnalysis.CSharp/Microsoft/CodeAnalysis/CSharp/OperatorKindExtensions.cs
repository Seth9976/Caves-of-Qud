using System;
using System.Collections.Immutable;
using System.Linq.Expressions;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200005C RID: 92
	internal static class OperatorKindExtensions
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x00048342 File Offset: 0x00046542
		public static int OperatorIndex(this UnaryOperatorKind kind)
		{
			return (kind.Operator() >> 8) - UnaryOperatorKind._Object;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0004834F File Offset: 0x0004654F
		public static UnaryOperatorKind Operator(this UnaryOperatorKind kind)
		{
			return kind & UnaryOperatorKind.OpMask;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00048358 File Offset: 0x00046558
		public static UnaryOperatorKind Unlifted(this UnaryOperatorKind kind)
		{
			return kind & ~UnaryOperatorKind.Lifted;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00048361 File Offset: 0x00046561
		public static bool IsLifted(this UnaryOperatorKind kind)
		{
			return (kind & UnaryOperatorKind.Lifted) > UnaryOperatorKind.Error;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0004836D File Offset: 0x0004656D
		public static bool IsChecked(this UnaryOperatorKind kind)
		{
			return (kind & UnaryOperatorKind.Checked) > UnaryOperatorKind.Error;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00048379 File Offset: 0x00046579
		public static bool IsUserDefined(this UnaryOperatorKind kind)
		{
			return (kind & UnaryOperatorKind.TypeMask) == UnaryOperatorKind.UserDefined;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00048386 File Offset: 0x00046586
		public static UnaryOperatorKind OverflowChecks(this UnaryOperatorKind kind)
		{
			return kind & UnaryOperatorKind.Checked;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00048390 File Offset: 0x00046590
		public static UnaryOperatorKind WithOverflowChecksIfApplicable(this UnaryOperatorKind kind, bool enabled)
		{
			if (!enabled)
			{
				return kind & ~UnaryOperatorKind.Checked;
			}
			if (kind.IsDynamic())
			{
				return kind | UnaryOperatorKind.Checked;
			}
			if (kind.IsIntegral())
			{
				UnaryOperatorKind unaryOperatorKind = kind.Operator();
				if (unaryOperatorKind <= UnaryOperatorKind.PostfixDecrement)
				{
					if (unaryOperatorKind != UnaryOperatorKind.PostfixIncrement && unaryOperatorKind != UnaryOperatorKind.PostfixDecrement)
					{
						return kind;
					}
				}
				else if (unaryOperatorKind != UnaryOperatorKind.PrefixIncrement && unaryOperatorKind != UnaryOperatorKind.PrefixDecrement && unaryOperatorKind != UnaryOperatorKind.UnaryMinus)
				{
					return kind;
				}
				return kind | UnaryOperatorKind.Checked;
			}
			return kind;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00048402 File Offset: 0x00046602
		public static UnaryOperatorKind OperandTypes(this UnaryOperatorKind kind)
		{
			return kind & UnaryOperatorKind.TypeMask;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0004840B File Offset: 0x0004660B
		public static bool IsDynamic(this UnaryOperatorKind kind)
		{
			return kind.OperandTypes() == UnaryOperatorKind.Dynamic;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00048418 File Offset: 0x00046618
		public static bool IsIntegral(this UnaryOperatorKind kind)
		{
			switch (kind.OperandTypes())
			{
			case UnaryOperatorKind.SByte:
			case UnaryOperatorKind.Byte:
			case UnaryOperatorKind.Short:
			case UnaryOperatorKind.UShort:
			case UnaryOperatorKind.Int:
			case UnaryOperatorKind.UInt:
			case UnaryOperatorKind.Long:
			case UnaryOperatorKind.ULong:
			case UnaryOperatorKind.NInt:
			case UnaryOperatorKind.NUInt:
			case UnaryOperatorKind.Char:
			case UnaryOperatorKind.Enum:
			case UnaryOperatorKind.Pointer:
				return true;
			}
			return false;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00048499 File Offset: 0x00046699
		public static UnaryOperatorKind WithType(this UnaryOperatorKind kind, UnaryOperatorKind type)
		{
			return kind | type;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0004849E File Offset: 0x0004669E
		public static int OperatorIndex(this BinaryOperatorKind kind)
		{
			return (kind.Operator() >> 8) - BinaryOperatorKind.Object;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000484AB File Offset: 0x000466AB
		public static BinaryOperatorKind Operator(this BinaryOperatorKind kind)
		{
			return kind & BinaryOperatorKind.OpMask;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000484B4 File Offset: 0x000466B4
		public static BinaryOperatorKind Unlifted(this BinaryOperatorKind kind)
		{
			return kind & ~BinaryOperatorKind.Lifted;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000484BD File Offset: 0x000466BD
		public static BinaryOperatorKind OperatorWithLogical(this BinaryOperatorKind kind)
		{
			return kind & (BinaryOperatorKind.OpMask | BinaryOperatorKind.Logical);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x000484C6 File Offset: 0x000466C6
		public static BinaryOperatorKind WithType(this BinaryOperatorKind kind, SpecialType type)
		{
			switch (type)
			{
			case SpecialType.System_Int32:
				return kind | BinaryOperatorKind.Int;
			case SpecialType.System_UInt32:
				return kind | BinaryOperatorKind.UInt;
			case SpecialType.System_Int64:
				return kind | BinaryOperatorKind.Long;
			case SpecialType.System_UInt64:
				return kind | BinaryOperatorKind.ULong;
			default:
				throw ExceptionUtilities.UnexpectedValue(type);
			}
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000484FE File Offset: 0x000466FE
		public static UnaryOperatorKind WithType(this UnaryOperatorKind kind, SpecialType type)
		{
			switch (type)
			{
			case SpecialType.System_Int32:
				return kind | UnaryOperatorKind.Int;
			case SpecialType.System_UInt32:
				return kind | UnaryOperatorKind.UInt;
			case SpecialType.System_Int64:
				return kind | UnaryOperatorKind.Long;
			case SpecialType.System_UInt64:
				return kind | UnaryOperatorKind.ULong;
			default:
				throw ExceptionUtilities.UnexpectedValue(type);
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00048536 File Offset: 0x00046736
		public static BinaryOperatorKind WithType(this BinaryOperatorKind kind, BinaryOperatorKind type)
		{
			return kind | type;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0004853B File Offset: 0x0004673B
		public static bool IsLifted(this BinaryOperatorKind kind)
		{
			return (kind & BinaryOperatorKind.Lifted) > BinaryOperatorKind.Error;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00048547 File Offset: 0x00046747
		public static bool IsDynamic(this BinaryOperatorKind kind)
		{
			return kind.OperandTypes() == BinaryOperatorKind.Dynamic;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00048554 File Offset: 0x00046754
		public static bool IsComparison(this BinaryOperatorKind kind)
		{
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
			{
				if (binaryOperatorKind != BinaryOperatorKind.Equal && binaryOperatorKind != BinaryOperatorKind.NotEqual && binaryOperatorKind != BinaryOperatorKind.GreaterThan)
				{
					return false;
				}
			}
			else if (binaryOperatorKind != BinaryOperatorKind.LessThan && binaryOperatorKind != BinaryOperatorKind.GreaterThanOrEqual && binaryOperatorKind != BinaryOperatorKind.LessThanOrEqual)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x000485A5 File Offset: 0x000467A5
		public static bool IsChecked(this BinaryOperatorKind kind)
		{
			return (kind & BinaryOperatorKind.Checked) > BinaryOperatorKind.Error;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x000485B4 File Offset: 0x000467B4
		public static bool EmitsAsCheckedInstruction(this BinaryOperatorKind kind)
		{
			if (!kind.IsChecked())
			{
				return false;
			}
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			return binaryOperatorKind == BinaryOperatorKind.Multiplication || binaryOperatorKind == BinaryOperatorKind.Addition || binaryOperatorKind == BinaryOperatorKind.Subtraction;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000485F0 File Offset: 0x000467F0
		public static BinaryOperatorKind WithOverflowChecksIfApplicable(this BinaryOperatorKind kind, bool enabled)
		{
			if (!enabled)
			{
				return kind & ~BinaryOperatorKind.Checked;
			}
			if (kind.IsDynamic())
			{
				return kind | BinaryOperatorKind.Checked;
			}
			if (kind.IsIntegral())
			{
				BinaryOperatorKind binaryOperatorKind = kind.Operator();
				if (binaryOperatorKind <= BinaryOperatorKind.Addition)
				{
					if (binaryOperatorKind != BinaryOperatorKind.Multiplication && binaryOperatorKind != BinaryOperatorKind.Addition)
					{
						return kind;
					}
				}
				else if (binaryOperatorKind != BinaryOperatorKind.Subtraction && binaryOperatorKind != BinaryOperatorKind.Division)
				{
					return kind;
				}
				return kind | BinaryOperatorKind.Checked;
			}
			return kind;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0004865C File Offset: 0x0004685C
		public static bool IsEnum(this BinaryOperatorKind kind)
		{
			BinaryOperatorKind binaryOperatorKind = kind.OperandTypes();
			return binaryOperatorKind - BinaryOperatorKind.Enum <= 2;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0004867A File Offset: 0x0004687A
		public static bool IsEnum(this UnaryOperatorKind kind)
		{
			return kind.OperandTypes() == UnaryOperatorKind.Enum;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00048688 File Offset: 0x00046888
		public static bool IsIntegral(this BinaryOperatorKind kind)
		{
			switch (kind.OperandTypes())
			{
			case BinaryOperatorKind.Int:
			case BinaryOperatorKind.UInt:
			case BinaryOperatorKind.Long:
			case BinaryOperatorKind.ULong:
			case BinaryOperatorKind.NInt:
			case BinaryOperatorKind.NUInt:
			case BinaryOperatorKind.Char:
			case BinaryOperatorKind.Enum:
			case BinaryOperatorKind.EnumAndUnderlying:
			case BinaryOperatorKind.UnderlyingAndEnum:
			case BinaryOperatorKind.Pointer:
			case BinaryOperatorKind.PointerAndInt:
			case BinaryOperatorKind.PointerAndUInt:
			case BinaryOperatorKind.PointerAndLong:
			case BinaryOperatorKind.PointerAndULong:
			case BinaryOperatorKind.IntAndPointer:
			case BinaryOperatorKind.UIntAndPointer:
			case BinaryOperatorKind.LongAndPointer:
			case BinaryOperatorKind.ULongAndPointer:
				return true;
			}
			return false;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00048731 File Offset: 0x00046931
		public static bool IsLogical(this BinaryOperatorKind kind)
		{
			return (kind & BinaryOperatorKind.Logical) > BinaryOperatorKind.Error;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0004873D File Offset: 0x0004693D
		public static BinaryOperatorKind OperandTypes(this BinaryOperatorKind kind)
		{
			return kind & BinaryOperatorKind.TypeMask;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00048746 File Offset: 0x00046946
		public static bool IsUserDefined(this BinaryOperatorKind kind)
		{
			return (kind & BinaryOperatorKind.TypeMask) == BinaryOperatorKind.UserDefined;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00048754 File Offset: 0x00046954
		public static bool IsShift(this BinaryOperatorKind kind)
		{
			BinaryOperatorKind binaryOperatorKind = kind.Operator();
			return binaryOperatorKind == BinaryOperatorKind.LeftShift || binaryOperatorKind == BinaryOperatorKind.RightShift;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0004877C File Offset: 0x0004697C
		public static ExpressionType ToExpressionType(this BinaryOperatorKind kind, bool isCompoundAssignment)
		{
			if (isCompoundAssignment)
			{
				BinaryOperatorKind binaryOperatorKind = kind.Operator();
				if (binaryOperatorKind <= BinaryOperatorKind.Remainder)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.Addition)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Multiplication)
						{
							return 69;
						}
						if (binaryOperatorKind == BinaryOperatorKind.Addition)
						{
							return 63;
						}
					}
					else
					{
						if (binaryOperatorKind == BinaryOperatorKind.Subtraction)
						{
							return 73;
						}
						if (binaryOperatorKind == BinaryOperatorKind.Division)
						{
							return 65;
						}
						if (binaryOperatorKind == BinaryOperatorKind.Remainder)
						{
							return 68;
						}
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.RightShift)
				{
					if (binaryOperatorKind == BinaryOperatorKind.LeftShift)
					{
						return 67;
					}
					if (binaryOperatorKind == BinaryOperatorKind.RightShift)
					{
						return 72;
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.And)
					{
						return 64;
					}
					if (binaryOperatorKind == BinaryOperatorKind.Xor)
					{
						return 66;
					}
					if (binaryOperatorKind == BinaryOperatorKind.Or)
					{
						return 70;
					}
				}
			}
			else
			{
				BinaryOperatorKind binaryOperatorKind = kind.Operator();
				if (binaryOperatorKind <= BinaryOperatorKind.Equal)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.Division)
					{
						if (binaryOperatorKind <= BinaryOperatorKind.Addition)
						{
							if (binaryOperatorKind == BinaryOperatorKind.Multiplication)
							{
								return 26;
							}
							if (binaryOperatorKind == BinaryOperatorKind.Addition)
							{
								return 0;
							}
						}
						else
						{
							if (binaryOperatorKind == BinaryOperatorKind.Subtraction)
							{
								return 42;
							}
							if (binaryOperatorKind == BinaryOperatorKind.Division)
							{
								return 12;
							}
						}
					}
					else if (binaryOperatorKind <= BinaryOperatorKind.LeftShift)
					{
						if (binaryOperatorKind == BinaryOperatorKind.Remainder)
						{
							return 25;
						}
						if (binaryOperatorKind == BinaryOperatorKind.LeftShift)
						{
							return 19;
						}
					}
					else
					{
						if (binaryOperatorKind == BinaryOperatorKind.RightShift)
						{
							return 41;
						}
						if (binaryOperatorKind == BinaryOperatorKind.Equal)
						{
							return 13;
						}
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.GreaterThanOrEqual)
				{
					if (binaryOperatorKind <= BinaryOperatorKind.GreaterThan)
					{
						if (binaryOperatorKind == BinaryOperatorKind.NotEqual)
						{
							return 35;
						}
						if (binaryOperatorKind == BinaryOperatorKind.GreaterThan)
						{
							return 15;
						}
					}
					else
					{
						if (binaryOperatorKind == BinaryOperatorKind.LessThan)
						{
							return 20;
						}
						if (binaryOperatorKind == BinaryOperatorKind.GreaterThanOrEqual)
						{
							return 16;
						}
					}
				}
				else if (binaryOperatorKind <= BinaryOperatorKind.And)
				{
					if (binaryOperatorKind == BinaryOperatorKind.LessThanOrEqual)
					{
						return 21;
					}
					if (binaryOperatorKind == BinaryOperatorKind.And)
					{
						return 2;
					}
				}
				else
				{
					if (binaryOperatorKind == BinaryOperatorKind.Xor)
					{
						return 14;
					}
					if (binaryOperatorKind == BinaryOperatorKind.Or)
					{
						return 36;
					}
				}
			}
			throw ExceptionUtilities.UnexpectedValue(kind.Operator());
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0004895C File Offset: 0x00046B5C
		public static ExpressionType ToExpressionType(this UnaryOperatorKind kind)
		{
			UnaryOperatorKind unaryOperatorKind = kind.Operator();
			if (unaryOperatorKind <= UnaryOperatorKind.UnaryPlus)
			{
				if (unaryOperatorKind <= UnaryOperatorKind.PostfixDecrement)
				{
					if (unaryOperatorKind != UnaryOperatorKind.PostfixIncrement)
					{
						if (unaryOperatorKind != UnaryOperatorKind.PostfixDecrement)
						{
							goto IL_008F;
						}
						return 49;
					}
				}
				else if (unaryOperatorKind != UnaryOperatorKind.PrefixIncrement)
				{
					if (unaryOperatorKind == UnaryOperatorKind.PrefixDecrement)
					{
						return 49;
					}
					if (unaryOperatorKind != UnaryOperatorKind.UnaryPlus)
					{
						goto IL_008F;
					}
					return 29;
				}
				return 54;
			}
			if (unaryOperatorKind <= UnaryOperatorKind.LogicalNegation)
			{
				if (unaryOperatorKind == UnaryOperatorKind.UnaryMinus)
				{
					return 28;
				}
				if (unaryOperatorKind == UnaryOperatorKind.LogicalNegation)
				{
					return 34;
				}
			}
			else
			{
				if (unaryOperatorKind == UnaryOperatorKind.BitwiseComplement)
				{
					return 82;
				}
				if (unaryOperatorKind == UnaryOperatorKind.True)
				{
					return 83;
				}
				if (unaryOperatorKind == UnaryOperatorKind.False)
				{
					return 84;
				}
			}
			IL_008F:
			throw ExceptionUtilities.UnexpectedValue(kind.Operator());
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00048A08 File Offset: 0x00046C08
		public static RefKind RefKinds(this ImmutableArray<RefKind> ArgumentRefKinds, int index)
		{
			if (!ArgumentRefKinds.IsDefault && index < ArgumentRefKinds.Length)
			{
				return ArgumentRefKinds[index];
			}
			return RefKind.None;
		}
	}
}
