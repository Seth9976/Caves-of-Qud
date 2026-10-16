using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200004C RID: 76
	internal static class ConversionKindExtensions
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x00041BA2 File Offset: 0x0003FDA2
		public static bool IsDynamic(this ConversionKind conversionKind)
		{
			return conversionKind == ConversionKind.ImplicitDynamic || conversionKind == ConversionKind.ExplicitDynamic;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00041BB0 File Offset: 0x0003FDB0
		public static bool IsImplicitConversion(this ConversionKind conversionKind)
		{
			switch (conversionKind)
			{
			case ConversionKind.UnsetConversionKind:
			case ConversionKind.NoConversion:
				return false;
			case ConversionKind.Identity:
			case ConversionKind.ImplicitNumeric:
			case ConversionKind.ImplicitEnumeration:
			case ConversionKind.ImplicitThrow:
			case ConversionKind.ImplicitTupleLiteral:
			case ConversionKind.ImplicitTuple:
			case ConversionKind.ImplicitNullable:
			case ConversionKind.NullLiteral:
			case ConversionKind.ImplicitReference:
			case ConversionKind.Boxing:
			case ConversionKind.ImplicitPointerToVoid:
			case ConversionKind.ImplicitNullToPointer:
			case ConversionKind.ImplicitPointer:
			case ConversionKind.ImplicitDynamic:
			case ConversionKind.ImplicitConstant:
			case ConversionKind.ImplicitUserDefined:
			case ConversionKind.AnonymousFunction:
			case ConversionKind.MethodGroup:
			case ConversionKind.InterpolatedString:
			case ConversionKind.SwitchExpression:
			case ConversionKind.ConditionalExpression:
			case ConversionKind.Deconstruction:
			case ConversionKind.StackAllocToPointerType:
			case ConversionKind.StackAllocToSpanType:
			case ConversionKind.DefaultLiteral:
			case ConversionKind.ObjectCreation:
				return true;
			case ConversionKind.ExplicitTupleLiteral:
			case ConversionKind.ExplicitTuple:
			case ConversionKind.ExplicitDynamic:
			case ConversionKind.ExplicitNumeric:
			case ConversionKind.ExplicitEnumeration:
			case ConversionKind.ExplicitNullable:
			case ConversionKind.ExplicitReference:
			case ConversionKind.Unboxing:
			case ConversionKind.ExplicitUserDefined:
			case ConversionKind.ExplicitPointerToPointer:
			case ConversionKind.ExplicitIntegerToPointer:
			case ConversionKind.ExplicitPointerToInteger:
			case ConversionKind.IntPtr:
				return false;
			}
			throw ExceptionUtilities.UnexpectedValue(conversionKind);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00041C7E File Offset: 0x0003FE7E
		public static bool IsUserDefinedConversion(this ConversionKind conversionKind)
		{
			return conversionKind == ConversionKind.ImplicitUserDefined || conversionKind == ConversionKind.ExplicitUserDefined;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00041C8D File Offset: 0x0003FE8D
		public static bool IsPointerConversion(this ConversionKind kind)
		{
			return kind - ConversionKind.ImplicitPointerToVoid <= 2 || kind - ConversionKind.ExplicitPointerToPointer <= 2;
		}
	}
}
