using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F6 RID: 502
	internal static class SpecialTypeExtensions
	{
		// Token: 0x0600143E RID: 5182 RVA: 0x0003F84F File Offset: 0x0003DA4F
		public static bool IsClrInteger(this SpecialType specialType)
		{
			return specialType - SpecialType.System_Boolean <= 9 || specialType - SpecialType.System_IntPtr <= 1;
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0003F862 File Offset: 0x0003DA62
		public static bool IsBlittable(this SpecialType specialType)
		{
			return specialType - SpecialType.System_Boolean <= 9 || specialType - SpecialType.System_Single <= 1;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0003F878 File Offset: 0x0003DA78
		public static bool IsValueType(this SpecialType specialType)
		{
			switch (specialType)
			{
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
			case SpecialType.System_IntPtr:
			case SpecialType.System_UIntPtr:
			case SpecialType.System_Nullable_T:
			case SpecialType.System_DateTime:
			case SpecialType.System_TypedReference:
			case SpecialType.System_ArgIterator:
			case SpecialType.System_RuntimeArgumentHandle:
			case SpecialType.System_RuntimeFieldHandle:
			case SpecialType.System_RuntimeMethodHandle:
			case SpecialType.System_RuntimeTypeHandle:
				return true;
			}
			return false;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0003F924 File Offset: 0x0003DB24
		public static int SizeInBytes(this SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_Boolean:
				return 1;
			case SpecialType.System_Char:
				return 2;
			case SpecialType.System_SByte:
				return 1;
			case SpecialType.System_Byte:
				return 1;
			case SpecialType.System_Int16:
				return 2;
			case SpecialType.System_UInt16:
				return 2;
			case SpecialType.System_Int32:
				return 4;
			case SpecialType.System_UInt32:
				return 4;
			case SpecialType.System_Int64:
				return 8;
			case SpecialType.System_UInt64:
				return 8;
			case SpecialType.System_Decimal:
				return 16;
			case SpecialType.System_Single:
				return 4;
			case SpecialType.System_Double:
				return 8;
			default:
				return 0;
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0003F98C File Offset: 0x0003DB8C
		public static bool IsPrimitiveRecursiveStruct(this SpecialType specialType)
		{
			switch (specialType)
			{
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
			case SpecialType.System_Single:
			case SpecialType.System_Double:
			case SpecialType.System_IntPtr:
			case SpecialType.System_UIntPtr:
				return true;
			}
			return false;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0003F9E6 File Offset: 0x0003DBE6
		public static bool IsValidEnumUnderlyingType(this SpecialType specialType)
		{
			return specialType - SpecialType.System_SByte <= 7;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0003F9F2 File Offset: 0x0003DBF2
		public static bool IsNumericType(this SpecialType specialType)
		{
			return specialType - SpecialType.System_SByte <= 10;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0003F9FF File Offset: 0x0003DBFF
		public static bool IsIntegralType(this SpecialType specialType)
		{
			return specialType - SpecialType.System_SByte <= 7;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0003FA0B File Offset: 0x0003DC0B
		public static bool IsUnsignedIntegralType(this SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_Byte:
			case SpecialType.System_UInt16:
			case SpecialType.System_UInt32:
			case SpecialType.System_UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0003FA37 File Offset: 0x0003DC37
		public static bool IsSignedIntegralType(this SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_SByte:
			case SpecialType.System_Int16:
			case SpecialType.System_Int32:
			case SpecialType.System_Int64:
				return true;
			}
			return false;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0003FA63 File Offset: 0x0003DC63
		public static int VBForToShiftBits(this SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_SByte:
				return 7;
			case SpecialType.System_Int16:
				return 15;
			case SpecialType.System_Int32:
				return 31;
			case SpecialType.System_Int64:
				return 63;
			}
			throw ExceptionUtilities.UnexpectedValue(specialType);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0003FAA4 File Offset: 0x0003DCA4
		[NullableContext(1)]
		public static SpecialType FromRuntimeTypeOfLiteralValue(object value)
		{
			if (value.GetType() == typeof(int))
			{
				return SpecialType.System_Int32;
			}
			if (value.GetType() == typeof(string))
			{
				return SpecialType.System_String;
			}
			if (value.GetType() == typeof(bool))
			{
				return SpecialType.System_Boolean;
			}
			if (value.GetType() == typeof(char))
			{
				return SpecialType.System_Char;
			}
			if (value.GetType() == typeof(long))
			{
				return SpecialType.System_Int64;
			}
			if (value.GetType() == typeof(double))
			{
				return SpecialType.System_Double;
			}
			if (value.GetType() == typeof(uint))
			{
				return SpecialType.System_UInt32;
			}
			if (value.GetType() == typeof(ulong))
			{
				return SpecialType.System_UInt64;
			}
			if (value.GetType() == typeof(float))
			{
				return SpecialType.System_Single;
			}
			if (value.GetType() == typeof(decimal))
			{
				return SpecialType.System_Decimal;
			}
			if (value.GetType() == typeof(short))
			{
				return SpecialType.System_Int16;
			}
			if (value.GetType() == typeof(ushort))
			{
				return SpecialType.System_UInt16;
			}
			if (value.GetType() == typeof(DateTime))
			{
				return SpecialType.System_DateTime;
			}
			if (value.GetType() == typeof(byte))
			{
				return SpecialType.System_Byte;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return SpecialType.System_SByte;
			}
			return SpecialType.None;
		}
	}
}
