using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	internal static class BlobWriterImpl
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00007781 File Offset: 0x00005981
		internal static int GetCompressedIntegerSize(int value)
		{
			if (value <= 127)
			{
				return 1;
			}
			if (value <= 16383)
			{
				return 2;
			}
			return 4;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007798 File Offset: 0x00005998
		internal static void WriteCompressedInteger(ref BlobWriter writer, uint value)
		{
			if (value <= 127U)
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (value <= 16383U)
			{
				writer.WriteUInt16BE((ushort)(32768U | value));
				return;
			}
			if (value <= 536870911U)
			{
				writer.WriteUInt32BE(3221225472U | value);
				return;
			}
			Throw.ValueArgumentOutOfRange();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000077E8 File Offset: 0x000059E8
		internal static void WriteCompressedInteger(BlobBuilder writer, uint value)
		{
			if (value <= 127U)
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (value <= 16383U)
			{
				writer.WriteUInt16BE((ushort)(32768U | value));
				return;
			}
			if (value <= 536870911U)
			{
				writer.WriteUInt32BE(3221225472U | value);
				return;
			}
			Throw.ValueArgumentOutOfRange();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007838 File Offset: 0x00005A38
		internal static void WriteCompressedSignedInteger(ref BlobWriter writer, int value)
		{
			int num = value >> 31;
			if ((value & -64) == (num & -64))
			{
				int num2 = ((value & 63) << 1) | (num & 1);
				writer.WriteByte((byte)num2);
				return;
			}
			if ((value & -8192) == (num & -8192))
			{
				int num3 = ((value & 8191) << 1) | (num & 1);
				writer.WriteUInt16BE((ushort)(32768 | num3));
				return;
			}
			if ((value & -268435456) == (num & -268435456))
			{
				int num4 = ((value & 268435455) << 1) | (num & 1);
				writer.WriteUInt32BE((uint)(-1073741824 | num4));
				return;
			}
			Throw.ValueArgumentOutOfRange();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000078C8 File Offset: 0x00005AC8
		internal static void WriteCompressedSignedInteger(BlobBuilder writer, int value)
		{
			int num = value >> 31;
			if ((value & -64) == (num & -64))
			{
				int num2 = ((value & 63) << 1) | (num & 1);
				writer.WriteByte((byte)num2);
				return;
			}
			if ((value & -8192) == (num & -8192))
			{
				int num3 = ((value & 8191) << 1) | (num & 1);
				writer.WriteUInt16BE((ushort)(32768 | num3));
				return;
			}
			if ((value & -268435456) == (num & -268435456))
			{
				int num4 = ((value & 268435455) << 1) | (num & 1);
				writer.WriteUInt32BE((uint)(-1073741824 | num4));
				return;
			}
			Throw.ValueArgumentOutOfRange();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007958 File Offset: 0x00005B58
		[NullableContext(2)]
		internal static void WriteConstant(ref BlobWriter writer, object value)
		{
			if (value == null)
			{
				writer.WriteUInt32(0U);
				return;
			}
			Type type = value.GetType();
			if (IntrospectionExtensions.GetTypeInfo(type).IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(bool))
			{
				writer.WriteBoolean((bool)value);
				return;
			}
			if (type == typeof(int))
			{
				writer.WriteInt32((int)value);
				return;
			}
			if (type == typeof(string))
			{
				writer.WriteUTF16((string)value);
				return;
			}
			if (type == typeof(byte))
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (type == typeof(char))
			{
				writer.WriteUInt16((ushort)((char)value));
				return;
			}
			if (type == typeof(double))
			{
				writer.WriteDouble((double)value);
				return;
			}
			if (type == typeof(short))
			{
				writer.WriteInt16((short)value);
				return;
			}
			if (type == typeof(long))
			{
				writer.WriteInt64((long)value);
				return;
			}
			if (type == typeof(sbyte))
			{
				writer.WriteSByte((sbyte)value);
				return;
			}
			if (type == typeof(float))
			{
				writer.WriteSingle((float)value);
				return;
			}
			if (type == typeof(ushort))
			{
				writer.WriteUInt16((ushort)value);
				return;
			}
			if (type == typeof(uint))
			{
				writer.WriteUInt32((uint)value);
				return;
			}
			if (type == typeof(ulong))
			{
				writer.WriteUInt64((ulong)value);
				return;
			}
			throw new ArgumentException(SR.Format(SR.InvalidConstantValueOfType, type));
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007B30 File Offset: 0x00005D30
		internal static void WriteConstant(BlobBuilder writer, [Nullable(2)] object value)
		{
			if (value == null)
			{
				writer.WriteUInt32(0U);
				return;
			}
			Type type = value.GetType();
			if (IntrospectionExtensions.GetTypeInfo(type).IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(bool))
			{
				writer.WriteBoolean((bool)value);
				return;
			}
			if (type == typeof(int))
			{
				writer.WriteInt32((int)value);
				return;
			}
			if (type == typeof(string))
			{
				writer.WriteUTF16((string)value);
				return;
			}
			if (type == typeof(byte))
			{
				writer.WriteByte((byte)value);
				return;
			}
			if (type == typeof(char))
			{
				writer.WriteUInt16((ushort)((char)value));
				return;
			}
			if (type == typeof(double))
			{
				writer.WriteDouble((double)value);
				return;
			}
			if (type == typeof(short))
			{
				writer.WriteInt16((short)value);
				return;
			}
			if (type == typeof(long))
			{
				writer.WriteInt64((long)value);
				return;
			}
			if (type == typeof(sbyte))
			{
				writer.WriteSByte((sbyte)value);
				return;
			}
			if (type == typeof(float))
			{
				writer.WriteSingle((float)value);
				return;
			}
			if (type == typeof(ushort))
			{
				writer.WriteUInt16((ushort)value);
				return;
			}
			if (type == typeof(uint))
			{
				writer.WriteUInt32((uint)value);
				return;
			}
			if (type == typeof(ulong))
			{
				writer.WriteUInt64((ulong)value);
				return;
			}
			throw new ArgumentException(SR.Format(SR.InvalidConstantValueOfType, type));
		}

		// Token: 0x040001A6 RID: 422
		internal const int SingleByteCompressedIntegerMaxValue = 127;

		// Token: 0x040001A7 RID: 423
		internal const int TwoByteCompressedIntegerMaxValue = 16383;

		// Token: 0x040001A8 RID: 424
		internal const int MaxCompressedIntegerValue = 536870911;

		// Token: 0x040001A9 RID: 425
		internal const int MinSignedCompressedIntegerValue = -268435456;

		// Token: 0x040001AA RID: 426
		internal const int MaxSignedCompressedIntegerValue = 268435455;
	}
}
