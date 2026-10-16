using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Roslyn.Utilities
{
	// Token: 0x02000041 RID: 65
	internal static class EnumUtilities
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005650 File Offset: 0x00003850
		[NullableContext(1)]
		internal static ulong ConvertEnumUnderlyingTypeToUInt64(object value, SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_SByte:
				return (ulong)((long)((sbyte)value));
			case SpecialType.System_Byte:
				return (ulong)((byte)value);
			case SpecialType.System_Int16:
				return (ulong)((long)((short)value));
			case SpecialType.System_UInt16:
				return (ulong)((ushort)value);
			case SpecialType.System_Int32:
				return (ulong)((long)((int)value));
			case SpecialType.System_UInt32:
				return (ulong)((uint)value);
			case SpecialType.System_Int64:
				return (ulong)((long)value);
			case SpecialType.System_UInt64:
				return (ulong)value;
			default:
				throw new InvalidOperationException(string.Format("{0} is not a valid underlying type for an enum", specialType));
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000056DB File Offset: 0x000038DB
		[return: Nullable(new byte[] { 1, 0 })]
		internal static T[] GetValues<T>() where T : struct
		{
			return (T[])Enum.GetValues(typeof(T));
		}
	}
}
