using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000365 RID: 869
	internal class Expression
	{
		// Token: 0x060024F9 RID: 9465 RVA: 0x000732CC File Offset: 0x000714CC
		[NullableContext(1)]
		public static ConstantValue SynthesizeNumeric(ITypeSymbol type, int value)
		{
			switch (type.SpecialType)
			{
			case SpecialType.System_Object:
				return ConstantValue.Create(1, ConstantValueTypeDiscriminator.Int32);
			case SpecialType.System_Boolean:
				return ConstantValue.Create(value != 0);
			case SpecialType.System_Char:
				return ConstantValue.Create((char)value);
			case SpecialType.System_SByte:
				return ConstantValue.Create((sbyte)value);
			case SpecialType.System_Byte:
				return ConstantValue.Create((byte)value);
			case SpecialType.System_Int16:
				return ConstantValue.Create((short)value);
			case SpecialType.System_UInt16:
				return ConstantValue.Create((ushort)value);
			case SpecialType.System_Int32:
				return ConstantValue.Create(value);
			case SpecialType.System_UInt32:
				return ConstantValue.Create((uint)value);
			case SpecialType.System_Int64:
				return ConstantValue.Create((long)value);
			case SpecialType.System_UInt64:
				return ConstantValue.Create((ulong)((long)value));
			case SpecialType.System_Single:
				return ConstantValue.Create((float)value);
			case SpecialType.System_Double:
				return ConstantValue.Create((double)value);
			}
			if (type.TypeKind == TypeKind.Enum)
			{
				return Expression.SynthesizeNumeric(((INamedTypeSymbol)type).EnumUnderlyingType, value);
			}
			return ConstantValue.Bad;
		}
	}
}
