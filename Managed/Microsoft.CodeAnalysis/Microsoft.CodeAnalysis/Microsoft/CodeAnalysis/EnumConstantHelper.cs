using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200018D RID: 397
	internal static class EnumConstantHelper
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x0002F828 File Offset: 0x0002DA28
		[NullableContext(1)]
		internal static EnumOverflowKind OffsetValue(ConstantValue constantValue, uint offset, out ConstantValue offsetValue)
		{
			offsetValue = ConstantValue.Bad;
			EnumOverflowKind enumOverflowKind;
			switch (constantValue.Discriminator)
			{
			case ConstantValueTypeDiscriminator.SByte:
			{
				long num = (long)constantValue.SByteValue;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(127L, num, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create((sbyte)(num + (long)((ulong)offset)));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.Byte:
			{
				ulong num2 = (ulong)constantValue.ByteValue;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(255UL, num2, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create((byte)(num2 + (ulong)offset));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.Int16:
			{
				long num3 = (long)constantValue.Int16Value;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(32767L, num3, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create((short)(num3 + (long)((ulong)offset)));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.UInt16:
			{
				ulong num4 = (ulong)constantValue.UInt16Value;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(65535UL, num4, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create((ushort)(num4 + (ulong)offset));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.Int32:
			{
				long num5 = (long)constantValue.Int32Value;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(2147483647L, num5, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create((int)(num5 + (long)((ulong)offset)));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.UInt32:
			{
				ulong num6 = (ulong)constantValue.UInt32Value;
				enumOverflowKind = EnumConstantHelper.CheckOverflow((ulong)(-1), num6, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create((uint)(num6 + (ulong)offset));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.Int64:
			{
				long int64Value = constantValue.Int64Value;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(long.MaxValue, int64Value, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create(int64Value + (long)((ulong)offset));
				}
				break;
			}
			case ConstantValueTypeDiscriminator.UInt64:
			{
				ulong uint64Value = constantValue.UInt64Value;
				enumOverflowKind = EnumConstantHelper.CheckOverflow(ulong.MaxValue, uint64Value, offset);
				if (enumOverflowKind == EnumOverflowKind.NoOverflow)
				{
					offsetValue = ConstantValue.Create(uint64Value + (ulong)offset);
				}
				break;
			}
			default:
				throw ExceptionUtilities.UnexpectedValue(constantValue.Discriminator);
			}
			return enumOverflowKind;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002F9DE File Offset: 0x0002DBDE
		private static EnumOverflowKind CheckOverflow(long maxOffset, long previous, uint offset)
		{
			return EnumConstantHelper.CheckOverflow((ulong)(maxOffset - previous), offset);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002F9E9 File Offset: 0x0002DBE9
		private static EnumOverflowKind CheckOverflow(ulong maxOffset, ulong previous, uint offset)
		{
			return EnumConstantHelper.CheckOverflow(maxOffset - previous, offset);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002F9F4 File Offset: 0x0002DBF4
		private static EnumOverflowKind CheckOverflow(ulong maxOffset, uint offset)
		{
			if ((ulong)offset <= maxOffset)
			{
				return EnumOverflowKind.NoOverflow;
			}
			if ((ulong)(offset - 1U) != maxOffset)
			{
				return EnumOverflowKind.OverflowIgnore;
			}
			return EnumOverflowKind.OverflowReport;
		}
	}
}
