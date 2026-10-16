using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200015C RID: 348
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class ConstantValue : IEquatable<ConstantValue>
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000DE2 RID: 3554
		public abstract ConstantValueTypeDiscriminator Discriminator { get; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000DE3 RID: 3555
		internal abstract SpecialType SpecialType { get; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00027C4C File Offset: 0x00025E4C
		[Nullable(2)]
		public virtual string StringValue
		{
			[NullableContext(2)]
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00027C53 File Offset: 0x00025E53
		[Nullable(2)]
		internal virtual Rope RopeValue
		{
			[NullableContext(2)]
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00027C5A File Offset: 0x00025E5A
		public virtual bool BooleanValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00027C61 File Offset: 0x00025E61
		public virtual sbyte SByteValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00027C68 File Offset: 0x00025E68
		public virtual byte ByteValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00027C6F File Offset: 0x00025E6F
		public virtual short Int16Value
		{
			get
			{
				return (short)this.SByteValue;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00027C77 File Offset: 0x00025E77
		public virtual ushort UInt16Value
		{
			get
			{
				return (ushort)this.ByteValue;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00027C7F File Offset: 0x00025E7F
		public virtual int Int32Value
		{
			get
			{
				return (int)this.Int16Value;
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00027C87 File Offset: 0x00025E87
		public virtual uint UInt32Value
		{
			get
			{
				return (uint)this.UInt16Value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00027C8F File Offset: 0x00025E8F
		public virtual long Int64Value
		{
			get
			{
				return (long)this.Int32Value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00027C98 File Offset: 0x00025E98
		public virtual ulong UInt64Value
		{
			get
			{
				return (ulong)this.UInt32Value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00027CA1 File Offset: 0x00025EA1
		public virtual char CharValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00027CA8 File Offset: 0x00025EA8
		public virtual decimal DecimalValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x00027CAF File Offset: 0x00025EAF
		public virtual DateTime DateTimeValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00027CB6 File Offset: 0x00025EB6
		public virtual double DoubleValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00027CBD File Offset: 0x00025EBD
		public virtual float SingleValue
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00027CC4 File Offset: 0x00025EC4
		public virtual bool IsDefaultValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00027CC7 File Offset: 0x00025EC7
		public static ConstantValue Bad
		{
			get
			{
				return ConstantValue.ConstantValueBad.Instance;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00027CCE File Offset: 0x00025ECE
		public static ConstantValue Null
		{
			get
			{
				return ConstantValue.ConstantValueNull.Instance;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00027CD5 File Offset: 0x00025ED5
		public static ConstantValue Nothing
		{
			get
			{
				return ConstantValue.Null;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00027CDC File Offset: 0x00025EDC
		public static ConstantValue Unset
		{
			get
			{
				return ConstantValue.ConstantValueNull.Uninitialized;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00027CE3 File Offset: 0x00025EE3
		public static ConstantValue True
		{
			get
			{
				return ConstantValue.ConstantValueOne.Boolean;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00027CEA File Offset: 0x00025EEA
		public static ConstantValue False
		{
			get
			{
				return ConstantValue.ConstantValueDefault.Boolean;
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00027CF1 File Offset: 0x00025EF1
		public static ConstantValue Create([Nullable(2)] string value)
		{
			if (value == null)
			{
				return ConstantValue.Null;
			}
			return new ConstantValue.ConstantValueString(value);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00027D02 File Offset: 0x00025F02
		internal static ConstantValue CreateFromRope(Rope value)
		{
			return new ConstantValue.ConstantValueString(value);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00027D0A File Offset: 0x00025F0A
		public static ConstantValue Create(char value)
		{
			if (value == '\0')
			{
				return ConstantValue.ConstantValueDefault.Char;
			}
			return new ConstantValue.ConstantValueI16(value);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00027D1B File Offset: 0x00025F1B
		public static ConstantValue Create(sbyte value)
		{
			if (value == 0)
			{
				return ConstantValue.ConstantValueDefault.SByte;
			}
			if (value == 1)
			{
				return ConstantValue.ConstantValueOne.SByte;
			}
			return new ConstantValue.ConstantValueI8(value);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00027D36 File Offset: 0x00025F36
		public static ConstantValue Create(byte value)
		{
			if (value == 0)
			{
				return ConstantValue.ConstantValueDefault.Byte;
			}
			if (value == 1)
			{
				return ConstantValue.ConstantValueOne.Byte;
			}
			return new ConstantValue.ConstantValueI8(value);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00027D51 File Offset: 0x00025F51
		public static ConstantValue Create(short value)
		{
			if (value == 0)
			{
				return ConstantValue.ConstantValueDefault.Int16;
			}
			if (value == 1)
			{
				return ConstantValue.ConstantValueOne.Int16;
			}
			return new ConstantValue.ConstantValueI16(value);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00027D6C File Offset: 0x00025F6C
		public static ConstantValue Create(ushort value)
		{
			if (value == 0)
			{
				return ConstantValue.ConstantValueDefault.UInt16;
			}
			if (value == 1)
			{
				return ConstantValue.ConstantValueOne.UInt16;
			}
			return new ConstantValue.ConstantValueI16(value);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00027D87 File Offset: 0x00025F87
		public static ConstantValue Create(int value)
		{
			if (value == 0)
			{
				return ConstantValue.ConstantValueDefault.Int32;
			}
			if (value == 1)
			{
				return ConstantValue.ConstantValueOne.Int32;
			}
			return new ConstantValue.ConstantValueI32(value);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00027DA2 File Offset: 0x00025FA2
		public static ConstantValue Create(uint value)
		{
			if (value == 0U)
			{
				return ConstantValue.ConstantValueDefault.UInt32;
			}
			if (value == 1U)
			{
				return ConstantValue.ConstantValueOne.UInt32;
			}
			return new ConstantValue.ConstantValueI32(value);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00027DBD File Offset: 0x00025FBD
		public static ConstantValue Create(long value)
		{
			if (value == 0L)
			{
				return ConstantValue.ConstantValueDefault.Int64;
			}
			if (value == 1L)
			{
				return ConstantValue.ConstantValueOne.Int64;
			}
			return new ConstantValue.ConstantValueI64(value);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00027DD9 File Offset: 0x00025FD9
		public static ConstantValue Create(ulong value)
		{
			if (value == 0UL)
			{
				return ConstantValue.ConstantValueDefault.UInt64;
			}
			if (value == 1UL)
			{
				return ConstantValue.ConstantValueOne.UInt64;
			}
			return new ConstantValue.ConstantValueI64(value);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00027DF5 File Offset: 0x00025FF5
		public static ConstantValue CreateNativeInt(int value)
		{
			if (value == 0)
			{
				return ConstantValue.ConstantValueDefault.NInt;
			}
			if (value == 1)
			{
				return ConstantValue.ConstantValueOne.NInt;
			}
			return new ConstantValue.ConstantValueNativeInt(value);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00027E10 File Offset: 0x00026010
		public static ConstantValue CreateNativeUInt(uint value)
		{
			if (value == 0U)
			{
				return ConstantValue.ConstantValueDefault.NUInt;
			}
			if (value == 1U)
			{
				return ConstantValue.ConstantValueOne.NUInt;
			}
			return new ConstantValue.ConstantValueNativeInt(value);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00027E2B File Offset: 0x0002602B
		public static ConstantValue Create(bool value)
		{
			if (value)
			{
				return ConstantValue.ConstantValueOne.Boolean;
			}
			return ConstantValue.ConstantValueDefault.Boolean;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00027E3B File Offset: 0x0002603B
		public static ConstantValue Create(float value)
		{
			if (BitConverter.DoubleToInt64Bits((double)value) == 0L)
			{
				return ConstantValue.ConstantValueDefault.Single;
			}
			if (value == 1f)
			{
				return ConstantValue.ConstantValueOne.Single;
			}
			return new ConstantValue.ConstantValueSingle((double)value);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00027E61 File Offset: 0x00026061
		public static ConstantValue CreateSingle(double value)
		{
			if (BitConverter.DoubleToInt64Bits(value) == 0L)
			{
				return ConstantValue.ConstantValueDefault.Single;
			}
			if (value == 1.0)
			{
				return ConstantValue.ConstantValueOne.Single;
			}
			return new ConstantValue.ConstantValueSingle(value);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00027E89 File Offset: 0x00026089
		public static ConstantValue Create(double value)
		{
			if (BitConverter.DoubleToInt64Bits(value) == 0L)
			{
				return ConstantValue.ConstantValueDefault.Double;
			}
			if (value == 1.0)
			{
				return ConstantValue.ConstantValueOne.Double;
			}
			return new ConstantValue.ConstantValueDouble(value);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00027EB1 File Offset: 0x000260B1
		public static ConstantValue Create(decimal value)
		{
			if (decimal.GetBits(value)[3] == 0)
			{
				if (value == 0m)
				{
					return ConstantValue.ConstantValueDefault.Decimal;
				}
				if (value == 1m)
				{
					return ConstantValue.ConstantValueOne.Decimal;
				}
			}
			return new ConstantValue.ConstantValueDecimal(value);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00027EEC File Offset: 0x000260EC
		public static ConstantValue Create(DateTime value)
		{
			if (value == default(DateTime))
			{
				return ConstantValue.ConstantValueDefault.DateTime;
			}
			return new ConstantValue.ConstantValueDateTime(value);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00027F18 File Offset: 0x00026118
		public static ConstantValue Create(object value, SpecialType st)
		{
			ConstantValueTypeDiscriminator discriminator = ConstantValue.GetDiscriminator(st);
			return ConstantValue.Create(value, discriminator);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00027F34 File Offset: 0x00026134
		public static ConstantValue CreateSizeOf(SpecialType st)
		{
			int num = st.SizeInBytes();
			if (num != 0)
			{
				return ConstantValue.Create(num);
			}
			return null;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00027F54 File Offset: 0x00026154
		public static ConstantValue Create(object value, ConstantValueTypeDiscriminator discriminator)
		{
			switch (discriminator)
			{
			case ConstantValueTypeDiscriminator.Nothing:
				return ConstantValue.Null;
			case ConstantValueTypeDiscriminator.SByte:
				return ConstantValue.Create((sbyte)value);
			case ConstantValueTypeDiscriminator.Byte:
				return ConstantValue.Create((byte)value);
			case ConstantValueTypeDiscriminator.Int16:
				return ConstantValue.Create((short)value);
			case ConstantValueTypeDiscriminator.UInt16:
				return ConstantValue.Create((ushort)value);
			case ConstantValueTypeDiscriminator.Int32:
				return ConstantValue.Create((int)value);
			case ConstantValueTypeDiscriminator.UInt32:
				return ConstantValue.Create((uint)value);
			case ConstantValueTypeDiscriminator.Int64:
				return ConstantValue.Create((long)value);
			case ConstantValueTypeDiscriminator.UInt64:
				return ConstantValue.Create((ulong)value);
			case ConstantValueTypeDiscriminator.NInt:
				return ConstantValue.CreateNativeInt((int)value);
			case ConstantValueTypeDiscriminator.NUInt:
				return ConstantValue.CreateNativeUInt((uint)value);
			case ConstantValueTypeDiscriminator.Char:
				return ConstantValue.Create((char)value);
			case ConstantValueTypeDiscriminator.Boolean:
				return ConstantValue.Create((bool)value);
			case ConstantValueTypeDiscriminator.Single:
				if (!(value is double))
				{
					return ConstantValue.Create((float)value);
				}
				return ConstantValue.CreateSingle((double)value);
			case ConstantValueTypeDiscriminator.Double:
				return ConstantValue.Create((double)value);
			case ConstantValueTypeDiscriminator.String:
				return ConstantValue.Create((string)value);
			case ConstantValueTypeDiscriminator.Decimal:
				return ConstantValue.Create((decimal)value);
			case ConstantValueTypeDiscriminator.DateTime:
				return ConstantValue.Create((DateTime)value);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000280A3 File Offset: 0x000262A3
		public static ConstantValue Default(SpecialType st)
		{
			return ConstantValue.Default(ConstantValue.GetDiscriminator(st));
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000280B0 File Offset: 0x000262B0
		public static ConstantValue Default(ConstantValueTypeDiscriminator discriminator)
		{
			switch (discriminator)
			{
			case ConstantValueTypeDiscriminator.Nothing:
			case ConstantValueTypeDiscriminator.String:
				return ConstantValue.Null;
			case ConstantValueTypeDiscriminator.Bad:
				return ConstantValue.Bad;
			case ConstantValueTypeDiscriminator.SByte:
				return ConstantValue.ConstantValueDefault.SByte;
			case ConstantValueTypeDiscriminator.Byte:
				return ConstantValue.ConstantValueDefault.Byte;
			case ConstantValueTypeDiscriminator.Int16:
				return ConstantValue.ConstantValueDefault.Int16;
			case ConstantValueTypeDiscriminator.UInt16:
				return ConstantValue.ConstantValueDefault.UInt16;
			case ConstantValueTypeDiscriminator.Int32:
				return ConstantValue.ConstantValueDefault.Int32;
			case ConstantValueTypeDiscriminator.UInt32:
				return ConstantValue.ConstantValueDefault.UInt32;
			case ConstantValueTypeDiscriminator.Int64:
				return ConstantValue.ConstantValueDefault.Int64;
			case ConstantValueTypeDiscriminator.UInt64:
				return ConstantValue.ConstantValueDefault.UInt64;
			case ConstantValueTypeDiscriminator.NInt:
				return ConstantValue.ConstantValueDefault.NInt;
			case ConstantValueTypeDiscriminator.NUInt:
				return ConstantValue.ConstantValueDefault.NUInt;
			case ConstantValueTypeDiscriminator.Char:
				return ConstantValue.ConstantValueDefault.Char;
			case ConstantValueTypeDiscriminator.Boolean:
				return ConstantValue.ConstantValueDefault.Boolean;
			case ConstantValueTypeDiscriminator.Single:
				return ConstantValue.ConstantValueDefault.Single;
			case ConstantValueTypeDiscriminator.Double:
				return ConstantValue.ConstantValueDefault.Double;
			case ConstantValueTypeDiscriminator.Decimal:
				return ConstantValue.ConstantValueDefault.Decimal;
			case ConstantValueTypeDiscriminator.DateTime:
				return ConstantValue.ConstantValueDefault.DateTime;
			default:
				throw ExceptionUtilities.UnexpectedValue(discriminator);
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00028188 File Offset: 0x00026388
		internal static ConstantValueTypeDiscriminator GetDiscriminator(SpecialType st)
		{
			switch (st)
			{
			case SpecialType.System_Boolean:
				return ConstantValueTypeDiscriminator.Boolean;
			case SpecialType.System_Char:
				return ConstantValueTypeDiscriminator.Char;
			case SpecialType.System_SByte:
				return ConstantValueTypeDiscriminator.SByte;
			case SpecialType.System_Byte:
				return ConstantValueTypeDiscriminator.Byte;
			case SpecialType.System_Int16:
				return ConstantValueTypeDiscriminator.Int16;
			case SpecialType.System_UInt16:
				return ConstantValueTypeDiscriminator.UInt16;
			case SpecialType.System_Int32:
				return ConstantValueTypeDiscriminator.Int32;
			case SpecialType.System_UInt32:
				return ConstantValueTypeDiscriminator.UInt32;
			case SpecialType.System_Int64:
				return ConstantValueTypeDiscriminator.Int64;
			case SpecialType.System_UInt64:
				return ConstantValueTypeDiscriminator.UInt64;
			case SpecialType.System_Decimal:
				return ConstantValueTypeDiscriminator.Decimal;
			case SpecialType.System_Single:
				return ConstantValueTypeDiscriminator.Single;
			case SpecialType.System_Double:
				return ConstantValueTypeDiscriminator.Double;
			case SpecialType.System_String:
				return ConstantValueTypeDiscriminator.String;
			case SpecialType.System_IntPtr:
				return ConstantValueTypeDiscriminator.NInt;
			case SpecialType.System_UIntPtr:
				return ConstantValueTypeDiscriminator.NUInt;
			case SpecialType.System_DateTime:
				return ConstantValueTypeDiscriminator.DateTime;
			}
			return ConstantValueTypeDiscriminator.Bad;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00028238 File Offset: 0x00026438
		private static SpecialType GetSpecialType(ConstantValueTypeDiscriminator discriminator)
		{
			switch (discriminator)
			{
			case ConstantValueTypeDiscriminator.SByte:
				return SpecialType.System_SByte;
			case ConstantValueTypeDiscriminator.Byte:
				return SpecialType.System_Byte;
			case ConstantValueTypeDiscriminator.Int16:
				return SpecialType.System_Int16;
			case ConstantValueTypeDiscriminator.UInt16:
				return SpecialType.System_UInt16;
			case ConstantValueTypeDiscriminator.Int32:
				return SpecialType.System_Int32;
			case ConstantValueTypeDiscriminator.UInt32:
				return SpecialType.System_UInt32;
			case ConstantValueTypeDiscriminator.Int64:
				return SpecialType.System_Int64;
			case ConstantValueTypeDiscriminator.UInt64:
				return SpecialType.System_UInt64;
			case ConstantValueTypeDiscriminator.NInt:
				return SpecialType.System_IntPtr;
			case ConstantValueTypeDiscriminator.NUInt:
				return SpecialType.System_UIntPtr;
			case ConstantValueTypeDiscriminator.Char:
				return SpecialType.System_Char;
			case ConstantValueTypeDiscriminator.Boolean:
				return SpecialType.System_Boolean;
			case ConstantValueTypeDiscriminator.Single:
				return SpecialType.System_Single;
			case ConstantValueTypeDiscriminator.Double:
				return SpecialType.System_Double;
			case ConstantValueTypeDiscriminator.String:
				return SpecialType.System_String;
			case ConstantValueTypeDiscriminator.Decimal:
				return SpecialType.System_Decimal;
			case ConstantValueTypeDiscriminator.DateTime:
				return SpecialType.System_DateTime;
			default:
				return SpecialType.None;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x000282C8 File Offset: 0x000264C8
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				switch (this.Discriminator)
				{
				case ConstantValueTypeDiscriminator.Nothing:
					return null;
				case ConstantValueTypeDiscriminator.Bad:
					return null;
				case ConstantValueTypeDiscriminator.SByte:
					return Boxes.Box(this.SByteValue);
				case ConstantValueTypeDiscriminator.Byte:
					return Boxes.Box(this.ByteValue);
				case ConstantValueTypeDiscriminator.Int16:
					return Boxes.Box(this.Int16Value);
				case ConstantValueTypeDiscriminator.UInt16:
					return Boxes.Box(this.UInt16Value);
				case ConstantValueTypeDiscriminator.Int32:
					return Boxes.Box(this.Int32Value);
				case ConstantValueTypeDiscriminator.UInt32:
					return Boxes.Box(this.UInt32Value);
				case ConstantValueTypeDiscriminator.Int64:
					return Boxes.Box(this.Int64Value);
				case ConstantValueTypeDiscriminator.UInt64:
					return Boxes.Box(this.UInt64Value);
				case ConstantValueTypeDiscriminator.NInt:
					return Boxes.Box(this.Int32Value);
				case ConstantValueTypeDiscriminator.NUInt:
					return Boxes.Box(this.UInt32Value);
				case ConstantValueTypeDiscriminator.Char:
					return Boxes.Box(this.CharValue);
				case ConstantValueTypeDiscriminator.Boolean:
					return Boxes.Box(this.BooleanValue);
				case ConstantValueTypeDiscriminator.Single:
					return Boxes.Box(this.SingleValue);
				case ConstantValueTypeDiscriminator.Double:
					return Boxes.Box(this.DoubleValue);
				case ConstantValueTypeDiscriminator.String:
					return this.StringValue;
				case ConstantValueTypeDiscriminator.Decimal:
					return Boxes.Box(this.DecimalValue);
				case ConstantValueTypeDiscriminator.DateTime:
					return this.DateTimeValue;
				default:
					throw ExceptionUtilities.UnexpectedValue(this.Discriminator);
				}
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0002840E File Offset: 0x0002660E
		public static bool IsIntegralType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator - ConstantValueTypeDiscriminator.SByte <= 9;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0002841A File Offset: 0x0002661A
		public bool IsIntegral
		{
			get
			{
				return ConstantValue.IsIntegralType(this.Discriminator);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00028428 File Offset: 0x00026628
		public bool IsNegativeNumeric
		{
			get
			{
				ConstantValueTypeDiscriminator discriminator = this.Discriminator;
				switch (discriminator)
				{
				case ConstantValueTypeDiscriminator.SByte:
					return this.SByteValue < 0;
				case ConstantValueTypeDiscriminator.Byte:
				case ConstantValueTypeDiscriminator.UInt16:
				case ConstantValueTypeDiscriminator.UInt32:
				case ConstantValueTypeDiscriminator.UInt64:
					break;
				case ConstantValueTypeDiscriminator.Int16:
					return this.Int16Value < 0;
				case ConstantValueTypeDiscriminator.Int32:
				case ConstantValueTypeDiscriminator.NInt:
					return this.Int32Value < 0;
				case ConstantValueTypeDiscriminator.Int64:
					return this.Int64Value < 0L;
				default:
					switch (discriminator)
					{
					case ConstantValueTypeDiscriminator.Single:
						return this.SingleValue < 0f;
					case ConstantValueTypeDiscriminator.Double:
						return this.DoubleValue < 0.0;
					case ConstantValueTypeDiscriminator.Decimal:
						return this.DecimalValue < 0m;
					}
					break;
				}
				return false;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000284E0 File Offset: 0x000266E0
		public bool IsNumeric
		{
			get
			{
				switch (this.Discriminator)
				{
				case ConstantValueTypeDiscriminator.SByte:
				case ConstantValueTypeDiscriminator.Byte:
				case ConstantValueTypeDiscriminator.Int16:
				case ConstantValueTypeDiscriminator.UInt16:
				case ConstantValueTypeDiscriminator.Int32:
				case ConstantValueTypeDiscriminator.UInt32:
				case ConstantValueTypeDiscriminator.Int64:
				case ConstantValueTypeDiscriminator.UInt64:
				case ConstantValueTypeDiscriminator.NInt:
				case ConstantValueTypeDiscriminator.NUInt:
				case ConstantValueTypeDiscriminator.Single:
				case ConstantValueTypeDiscriminator.Double:
				case ConstantValueTypeDiscriminator.Decimal:
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00028541 File Offset: 0x00026741
		public static bool IsUnsignedIntegralType(ConstantValueTypeDiscriminator discriminator)
		{
			switch (discriminator)
			{
			case ConstantValueTypeDiscriminator.Byte:
			case ConstantValueTypeDiscriminator.UInt16:
			case ConstantValueTypeDiscriminator.UInt32:
			case ConstantValueTypeDiscriminator.UInt64:
			case ConstantValueTypeDiscriminator.NUInt:
				return true;
			}
			return false;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00028574 File Offset: 0x00026774
		public bool IsUnsigned
		{
			get
			{
				return ConstantValue.IsUnsignedIntegralType(this.Discriminator);
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00028581 File Offset: 0x00026781
		public static bool IsBooleanType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator == ConstantValueTypeDiscriminator.Boolean;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00028588 File Offset: 0x00026788
		public bool IsBoolean
		{
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.Boolean;
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00028594 File Offset: 0x00026794
		public static bool IsCharType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator == ConstantValueTypeDiscriminator.Char;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002859B File Offset: 0x0002679B
		public bool IsChar
		{
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.Char;
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000285A7 File Offset: 0x000267A7
		public static bool IsStringType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator == ConstantValueTypeDiscriminator.String;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x000285AE File Offset: 0x000267AE
		[MemberNotNullWhen(true, "StringValue")]
		public bool IsString
		{
			[MemberNotNullWhen(true, "StringValue")]
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.String;
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000285BA File Offset: 0x000267BA
		public static bool IsDecimalType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator == ConstantValueTypeDiscriminator.Decimal;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000285C1 File Offset: 0x000267C1
		public bool IsDecimal
		{
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.Decimal;
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000285CD File Offset: 0x000267CD
		public static bool IsDateTimeType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator == ConstantValueTypeDiscriminator.DateTime;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000285D4 File Offset: 0x000267D4
		public bool IsDateTime
		{
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.DateTime;
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000285E0 File Offset: 0x000267E0
		public static bool IsFloatingType(ConstantValueTypeDiscriminator discriminator)
		{
			return discriminator == ConstantValueTypeDiscriminator.Double || discriminator == ConstantValueTypeDiscriminator.Single;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000285EE File Offset: 0x000267EE
		public bool IsFloating
		{
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.Double || this.Discriminator == ConstantValueTypeDiscriminator.Single;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00028606 File Offset: 0x00026806
		public bool IsBad
		{
			get
			{
				return this.Discriminator == ConstantValueTypeDiscriminator.Bad;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00028611 File Offset: 0x00026811
		public bool IsNull
		{
			get
			{
				return this == ConstantValue.Null;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0002861B File Offset: 0x0002681B
		public bool IsNothing
		{
			get
			{
				return this == ConstantValue.Nothing;
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00028628 File Offset: 0x00026828
		public void Serialize(BlobBuilder writer)
		{
			switch (this.Discriminator)
			{
			case ConstantValueTypeDiscriminator.SByte:
				writer.WriteSByte(this.SByteValue);
				return;
			case ConstantValueTypeDiscriminator.Byte:
				writer.WriteByte(this.ByteValue);
				return;
			case ConstantValueTypeDiscriminator.Int16:
			case ConstantValueTypeDiscriminator.Char:
				writer.WriteInt16(this.Int16Value);
				return;
			case ConstantValueTypeDiscriminator.UInt16:
				writer.WriteUInt16(this.UInt16Value);
				return;
			case ConstantValueTypeDiscriminator.Int32:
				writer.WriteInt32(this.Int32Value);
				return;
			case ConstantValueTypeDiscriminator.UInt32:
				writer.WriteUInt32(this.UInt32Value);
				return;
			case ConstantValueTypeDiscriminator.Int64:
				writer.WriteInt64(this.Int64Value);
				return;
			case ConstantValueTypeDiscriminator.UInt64:
				writer.WriteUInt64(this.UInt64Value);
				return;
			case ConstantValueTypeDiscriminator.Boolean:
				writer.WriteBoolean(this.BooleanValue);
				return;
			case ConstantValueTypeDiscriminator.Single:
				writer.WriteSingle(this.SingleValue);
				return;
			case ConstantValueTypeDiscriminator.Double:
				writer.WriteDouble(this.DoubleValue);
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(this.Discriminator);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00028720 File Offset: 0x00026920
		public override string ToString()
		{
			string valueToDisplay = this.GetValueToDisplay();
			return string.Format("{0}({1}: {2})", base.GetType().Name, valueToDisplay, this.Discriminator);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00028755 File Offset: 0x00026955
		[NullableContext(2)]
		internal virtual string GetValueToDisplay()
		{
			object value = this.Value;
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00028768 File Offset: 0x00026968
		[NullableContext(2)]
		public virtual bool Equals(ConstantValue other)
		{
			return other == this || (other != null && this.Discriminator == other.Discriminator);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00028783 File Offset: 0x00026983
		[NullableContext(2)]
		public static bool operator ==(ConstantValue left, ConstantValue right)
		{
			return right == left || (left != null && left.Equals(right));
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00028797 File Offset: 0x00026997
		[NullableContext(2)]
		public static bool operator !=(ConstantValue left, ConstantValue right)
		{
			return !(left == right);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x000287A4 File Offset: 0x000269A4
		public override int GetHashCode()
		{
			return this.Discriminator.GetHashCode();
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x000287C5 File Offset: 0x000269C5
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ConstantValue);
		}

		// Token: 0x040003E4 RID: 996
		public const ConstantValue NotAvailable = null;

		// Token: 0x040003E5 RID: 997
		private static readonly double _s_IEEE_canonical_NaN = BitConverter.Int64BitsToDouble(-2251799813685248L);

		// Token: 0x02000589 RID: 1417
		[Nullable(0)]
		private sealed class ConstantValueBad : ConstantValue
		{
			// Token: 0x060034E9 RID: 13545 RVA: 0x00095750 File Offset: 0x00093950
			private ConstantValueBad()
			{
			}

			// Token: 0x17000EFD RID: 3837
			// (get) Token: 0x060034EA RID: 13546 RVA: 0x00095758 File Offset: 0x00093958
			public override ConstantValueTypeDiscriminator Discriminator
			{
				get
				{
					return ConstantValueTypeDiscriminator.Bad;
				}
			}

			// Token: 0x17000EFE RID: 3838
			// (get) Token: 0x060034EB RID: 13547 RVA: 0x0009575B File Offset: 0x0009395B
			internal override SpecialType SpecialType
			{
				get
				{
					return SpecialType.None;
				}
			}

			// Token: 0x060034EC RID: 13548 RVA: 0x0009575E File Offset: 0x0009395E
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return this == other;
			}

			// Token: 0x060034ED RID: 13549 RVA: 0x00095764 File Offset: 0x00093964
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this);
			}

			// Token: 0x060034EE RID: 13550 RVA: 0x0009576C File Offset: 0x0009396C
			internal override string GetValueToDisplay()
			{
				return "bad";
			}

			// Token: 0x040015F0 RID: 5616
			public static readonly ConstantValue.ConstantValueBad Instance = new ConstantValue.ConstantValueBad();
		}

		// Token: 0x0200058A RID: 1418
		[NullableContext(2)]
		[Nullable(0)]
		private sealed class ConstantValueNull : ConstantValue
		{
			// Token: 0x060034F0 RID: 13552 RVA: 0x0009577F File Offset: 0x0009397F
			private ConstantValueNull()
			{
			}

			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x060034F1 RID: 13553 RVA: 0x00095787 File Offset: 0x00093987
			public override ConstantValueTypeDiscriminator Discriminator
			{
				get
				{
					return ConstantValueTypeDiscriminator.Nothing;
				}
			}

			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x060034F2 RID: 13554 RVA: 0x0009578A File Offset: 0x0009398A
			internal override SpecialType SpecialType
			{
				get
				{
					return SpecialType.None;
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x060034F3 RID: 13555 RVA: 0x0009578D File Offset: 0x0009398D
			public override string StringValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000F02 RID: 3842
			// (get) Token: 0x060034F4 RID: 13556 RVA: 0x00095790 File Offset: 0x00093990
			internal override Rope RopeValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060034F5 RID: 13557 RVA: 0x00095793 File Offset: 0x00093993
			public override bool Equals(ConstantValue other)
			{
				return this == other;
			}

			// Token: 0x060034F6 RID: 13558 RVA: 0x00095799 File Offset: 0x00093999
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this);
			}

			// Token: 0x17000F03 RID: 3843
			// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000957A1 File Offset: 0x000939A1
			public override bool IsDefaultValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060034F8 RID: 13560 RVA: 0x000957A4 File Offset: 0x000939A4
			[NullableContext(1)]
			internal override string GetValueToDisplay()
			{
				if (this != ConstantValue.ConstantValueNull.Uninitialized)
				{
					return "null";
				}
				return "unset";
			}

			// Token: 0x040015F1 RID: 5617
			[Nullable(1)]
			public static readonly ConstantValue.ConstantValueNull Instance = new ConstantValue.ConstantValueNull();

			// Token: 0x040015F2 RID: 5618
			[Nullable(1)]
			public static readonly ConstantValue.ConstantValueNull Uninitialized = new ConstantValue.ConstantValueNull();
		}

		// Token: 0x0200058B RID: 1419
		[Nullable(0)]
		private sealed class ConstantValueString : ConstantValue
		{
			// Token: 0x060034FA RID: 13562 RVA: 0x000957CF File Offset: 0x000939CF
			public ConstantValueString(string value)
			{
				this._value = Rope.ForString(value);
			}

			// Token: 0x060034FB RID: 13563 RVA: 0x000957E3 File Offset: 0x000939E3
			public ConstantValueString(Rope value)
			{
				this._value = value;
			}

			// Token: 0x17000F04 RID: 3844
			// (get) Token: 0x060034FC RID: 13564 RVA: 0x000957F2 File Offset: 0x000939F2
			public override ConstantValueTypeDiscriminator Discriminator
			{
				get
				{
					return ConstantValueTypeDiscriminator.String;
				}
			}

			// Token: 0x17000F05 RID: 3845
			// (get) Token: 0x060034FD RID: 13565 RVA: 0x000957F6 File Offset: 0x000939F6
			internal override SpecialType SpecialType
			{
				get
				{
					return SpecialType.System_String;
				}
			}

			// Token: 0x17000F06 RID: 3846
			// (get) Token: 0x060034FE RID: 13566 RVA: 0x000957FC File Offset: 0x000939FC
			public override string StringValue
			{
				get
				{
					string text = null;
					WeakReference<string> constantValueReference = this._constantValueReference;
					if (constantValueReference == null || !constantValueReference.TryGetTarget(ref text))
					{
						text = this._value.ToString();
						this._constantValueReference = new WeakReference<string>(text);
					}
					return text;
				}
			}

			// Token: 0x17000F07 RID: 3847
			// (get) Token: 0x060034FF RID: 13567 RVA: 0x0009583D File Offset: 0x00093A3D
			internal override Rope RopeValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06003500 RID: 13568 RVA: 0x00095845 File Offset: 0x00093A45
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003501 RID: 13569 RVA: 0x0009585D File Offset: 0x00093A5D
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value.Equals(other.RopeValue);
			}

			// Token: 0x06003502 RID: 13570 RVA: 0x0009587B File Offset: 0x00093A7B
			internal override string GetValueToDisplay()
			{
				if (this._value != null)
				{
					return string.Format("\"{0}\"", this._value);
				}
				return "null";
			}

			// Token: 0x040015F3 RID: 5619
			private readonly Rope _value;

			// Token: 0x040015F4 RID: 5620
			[Nullable(new byte[] { 2, 1 })]
			private WeakReference<string> _constantValueReference;
		}

		// Token: 0x0200058C RID: 1420
		[NullableContext(0)]
		private sealed class ConstantValueDecimal : ConstantValue
		{
			// Token: 0x06003503 RID: 13571 RVA: 0x0009589B File Offset: 0x00093A9B
			public ConstantValueDecimal(decimal value)
			{
				this._value = value;
			}

			// Token: 0x17000F08 RID: 3848
			// (get) Token: 0x06003504 RID: 13572 RVA: 0x000958AA File Offset: 0x00093AAA
			public override ConstantValueTypeDiscriminator Discriminator
			{
				get
				{
					return ConstantValueTypeDiscriminator.Decimal;
				}
			}

			// Token: 0x17000F09 RID: 3849
			// (get) Token: 0x06003505 RID: 13573 RVA: 0x000958AE File Offset: 0x00093AAE
			internal override SpecialType SpecialType
			{
				get
				{
					return SpecialType.System_Decimal;
				}
			}

			// Token: 0x17000F0A RID: 3850
			// (get) Token: 0x06003506 RID: 13574 RVA: 0x000958B2 File Offset: 0x00093AB2
			public override decimal DecimalValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06003507 RID: 13575 RVA: 0x000958BC File Offset: 0x00093ABC
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003508 RID: 13576 RVA: 0x000958E2 File Offset: 0x00093AE2
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.DecimalValue;
			}

			// Token: 0x040015F5 RID: 5621
			private readonly decimal _value;
		}

		// Token: 0x0200058D RID: 1421
		[NullableContext(0)]
		private sealed class ConstantValueDateTime : ConstantValue
		{
			// Token: 0x06003509 RID: 13577 RVA: 0x00095900 File Offset: 0x00093B00
			public ConstantValueDateTime(DateTime value)
			{
				this._value = value;
			}

			// Token: 0x17000F0B RID: 3851
			// (get) Token: 0x0600350A RID: 13578 RVA: 0x0009590F File Offset: 0x00093B0F
			public override ConstantValueTypeDiscriminator Discriminator
			{
				get
				{
					return ConstantValueTypeDiscriminator.DateTime;
				}
			}

			// Token: 0x17000F0C RID: 3852
			// (get) Token: 0x0600350B RID: 13579 RVA: 0x00095913 File Offset: 0x00093B13
			internal override SpecialType SpecialType
			{
				get
				{
					return SpecialType.System_DateTime;
				}
			}

			// Token: 0x17000F0D RID: 3853
			// (get) Token: 0x0600350C RID: 13580 RVA: 0x00095917 File Offset: 0x00093B17
			public override DateTime DateTimeValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x0600350D RID: 13581 RVA: 0x00095920 File Offset: 0x00093B20
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x0600350E RID: 13582 RVA: 0x00095946 File Offset: 0x00093B46
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.DateTimeValue;
			}

			// Token: 0x040015F6 RID: 5622
			private readonly DateTime _value;
		}

		// Token: 0x0200058E RID: 1422
		[NullableContext(0)]
		private abstract class ConstantValueDiscriminated : ConstantValue
		{
			// Token: 0x0600350F RID: 13583 RVA: 0x00095964 File Offset: 0x00093B64
			public ConstantValueDiscriminated(ConstantValueTypeDiscriminator discriminator)
			{
				this._discriminator = discriminator;
			}

			// Token: 0x17000F0E RID: 3854
			// (get) Token: 0x06003510 RID: 13584 RVA: 0x00095973 File Offset: 0x00093B73
			public override ConstantValueTypeDiscriminator Discriminator
			{
				get
				{
					return this._discriminator;
				}
			}

			// Token: 0x17000F0F RID: 3855
			// (get) Token: 0x06003511 RID: 13585 RVA: 0x0009597B File Offset: 0x00093B7B
			internal override SpecialType SpecialType
			{
				get
				{
					return ConstantValue.GetSpecialType(this._discriminator);
				}
			}

			// Token: 0x040015F7 RID: 5623
			private readonly ConstantValueTypeDiscriminator _discriminator;
		}

		// Token: 0x0200058F RID: 1423
		[Nullable(0)]
		private class ConstantValueDefault : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003512 RID: 13586 RVA: 0x00095988 File Offset: 0x00093B88
			protected ConstantValueDefault(ConstantValueTypeDiscriminator discriminator)
				: base(discriminator)
			{
			}

			// Token: 0x17000F10 RID: 3856
			// (get) Token: 0x06003513 RID: 13587 RVA: 0x00095991 File Offset: 0x00093B91
			public override byte ByteValue
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000F11 RID: 3857
			// (get) Token: 0x06003514 RID: 13588 RVA: 0x00095994 File Offset: 0x00093B94
			public override sbyte SByteValue
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000F12 RID: 3858
			// (get) Token: 0x06003515 RID: 13589 RVA: 0x00095997 File Offset: 0x00093B97
			public override bool BooleanValue
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F13 RID: 3859
			// (get) Token: 0x06003516 RID: 13590 RVA: 0x0009599A File Offset: 0x00093B9A
			public override double DoubleValue
			{
				get
				{
					return 0.0;
				}
			}

			// Token: 0x17000F14 RID: 3860
			// (get) Token: 0x06003517 RID: 13591 RVA: 0x000959A5 File Offset: 0x00093BA5
			public override float SingleValue
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x17000F15 RID: 3861
			// (get) Token: 0x06003518 RID: 13592 RVA: 0x000959AC File Offset: 0x00093BAC
			public override decimal DecimalValue
			{
				get
				{
					return 0m;
				}
			}

			// Token: 0x17000F16 RID: 3862
			// (get) Token: 0x06003519 RID: 13593 RVA: 0x000959B3 File Offset: 0x00093BB3
			public override char CharValue
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x17000F17 RID: 3863
			// (get) Token: 0x0600351A RID: 13594 RVA: 0x000959B8 File Offset: 0x00093BB8
			public override DateTime DateTimeValue
			{
				get
				{
					return default(DateTime);
				}
			}

			// Token: 0x0600351B RID: 13595 RVA: 0x000959CE File Offset: 0x00093BCE
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return this == other;
			}

			// Token: 0x0600351C RID: 13596 RVA: 0x000959D4 File Offset: 0x00093BD4
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this);
			}

			// Token: 0x17000F18 RID: 3864
			// (get) Token: 0x0600351D RID: 13597 RVA: 0x000959DC File Offset: 0x00093BDC
			public override bool IsDefaultValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x040015F8 RID: 5624
			public static readonly ConstantValue.ConstantValueDefault SByte = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.SByte);

			// Token: 0x040015F9 RID: 5625
			public static readonly ConstantValue.ConstantValueDefault Byte = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.Byte);

			// Token: 0x040015FA RID: 5626
			public static readonly ConstantValue.ConstantValueDefault Int16 = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.Int16);

			// Token: 0x040015FB RID: 5627
			public static readonly ConstantValue.ConstantValueDefault UInt16 = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.UInt16);

			// Token: 0x040015FC RID: 5628
			public static readonly ConstantValue.ConstantValueDefault Int32 = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.Int32);

			// Token: 0x040015FD RID: 5629
			public static readonly ConstantValue.ConstantValueDefault UInt32 = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.UInt32);

			// Token: 0x040015FE RID: 5630
			public static readonly ConstantValue.ConstantValueDefault Int64 = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.Int64);

			// Token: 0x040015FF RID: 5631
			public static readonly ConstantValue.ConstantValueDefault UInt64 = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.UInt64);

			// Token: 0x04001600 RID: 5632
			public static readonly ConstantValue.ConstantValueDefault NInt = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.NInt);

			// Token: 0x04001601 RID: 5633
			public static readonly ConstantValue.ConstantValueDefault NUInt = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.NUInt);

			// Token: 0x04001602 RID: 5634
			public static readonly ConstantValue.ConstantValueDefault Char = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.Char);

			// Token: 0x04001603 RID: 5635
			public static readonly ConstantValue.ConstantValueDefault Single = new ConstantValue.ConstantValueSingleZero();

			// Token: 0x04001604 RID: 5636
			public static readonly ConstantValue.ConstantValueDefault Double = new ConstantValue.ConstantValueDoubleZero();

			// Token: 0x04001605 RID: 5637
			public static readonly ConstantValue.ConstantValueDefault Decimal = new ConstantValue.ConstantValueDecimalZero();

			// Token: 0x04001606 RID: 5638
			public static readonly ConstantValue.ConstantValueDefault DateTime = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.DateTime);

			// Token: 0x04001607 RID: 5639
			public static readonly ConstantValue.ConstantValueDefault Boolean = new ConstantValue.ConstantValueDefault(ConstantValueTypeDiscriminator.Boolean);
		}

		// Token: 0x02000590 RID: 1424
		[NullableContext(0)]
		private sealed class ConstantValueDecimalZero : ConstantValue.ConstantValueDefault
		{
			// Token: 0x0600351F RID: 13599 RVA: 0x00095AA0 File Offset: 0x00093CA0
			internal ConstantValueDecimalZero()
				: base(ConstantValueTypeDiscriminator.Decimal)
			{
			}

			// Token: 0x06003520 RID: 13600 RVA: 0x00095AAA File Offset: 0x00093CAA
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return other == this || (other != null && this.Discriminator == other.Discriminator && other.DecimalValue == 0m);
			}
		}

		// Token: 0x02000591 RID: 1425
		[NullableContext(0)]
		private sealed class ConstantValueDoubleZero : ConstantValue.ConstantValueDefault
		{
			// Token: 0x06003521 RID: 13601 RVA: 0x00095AD7 File Offset: 0x00093CD7
			internal ConstantValueDoubleZero()
				: base(ConstantValueTypeDiscriminator.Double)
			{
			}

			// Token: 0x06003522 RID: 13602 RVA: 0x00095AE1 File Offset: 0x00093CE1
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return other == this || (other != null && this.Discriminator == other.Discriminator && other.DoubleValue == 0.0);
			}
		}

		// Token: 0x02000592 RID: 1426
		[NullableContext(0)]
		private sealed class ConstantValueSingleZero : ConstantValue.ConstantValueDefault
		{
			// Token: 0x06003523 RID: 13603 RVA: 0x00095B0F File Offset: 0x00093D0F
			internal ConstantValueSingleZero()
				: base(ConstantValueTypeDiscriminator.Single)
			{
			}

			// Token: 0x06003524 RID: 13604 RVA: 0x00095B19 File Offset: 0x00093D19
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return other == this || (other != null && this.Discriminator == other.Discriminator && other.SingleValue == 0f);
			}
		}

		// Token: 0x02000593 RID: 1427
		[Nullable(0)]
		private class ConstantValueOne : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003525 RID: 13605 RVA: 0x00095B43 File Offset: 0x00093D43
			protected ConstantValueOne(ConstantValueTypeDiscriminator discriminator)
				: base(discriminator)
			{
			}

			// Token: 0x17000F19 RID: 3865
			// (get) Token: 0x06003526 RID: 13606 RVA: 0x00095B4C File Offset: 0x00093D4C
			public override byte ByteValue
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17000F1A RID: 3866
			// (get) Token: 0x06003527 RID: 13607 RVA: 0x00095B4F File Offset: 0x00093D4F
			public override sbyte SByteValue
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17000F1B RID: 3867
			// (get) Token: 0x06003528 RID: 13608 RVA: 0x00095B52 File Offset: 0x00093D52
			public override bool BooleanValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F1C RID: 3868
			// (get) Token: 0x06003529 RID: 13609 RVA: 0x00095B55 File Offset: 0x00093D55
			public override double DoubleValue
			{
				get
				{
					return 1.0;
				}
			}

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x0600352A RID: 13610 RVA: 0x00095B60 File Offset: 0x00093D60
			public override float SingleValue
			{
				get
				{
					return 1f;
				}
			}

			// Token: 0x17000F1E RID: 3870
			// (get) Token: 0x0600352B RID: 13611 RVA: 0x00095B67 File Offset: 0x00093D67
			public override decimal DecimalValue
			{
				get
				{
					return 1m;
				}
			}

			// Token: 0x0600352C RID: 13612 RVA: 0x00095B6E File Offset: 0x00093D6E
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return this == other;
			}

			// Token: 0x0600352D RID: 13613 RVA: 0x00095B74 File Offset: 0x00093D74
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this);
			}

			// Token: 0x04001608 RID: 5640
			public static readonly ConstantValue.ConstantValueOne SByte = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.SByte);

			// Token: 0x04001609 RID: 5641
			public static readonly ConstantValue.ConstantValueOne Byte = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Byte);

			// Token: 0x0400160A RID: 5642
			public static readonly ConstantValue.ConstantValueOne Int16 = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Int16);

			// Token: 0x0400160B RID: 5643
			public static readonly ConstantValue.ConstantValueOne UInt16 = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.UInt16);

			// Token: 0x0400160C RID: 5644
			public static readonly ConstantValue.ConstantValueOne Int32 = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Int32);

			// Token: 0x0400160D RID: 5645
			public static readonly ConstantValue.ConstantValueOne UInt32 = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.UInt32);

			// Token: 0x0400160E RID: 5646
			public static readonly ConstantValue.ConstantValueOne Int64 = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Int64);

			// Token: 0x0400160F RID: 5647
			public static readonly ConstantValue.ConstantValueOne UInt64 = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.UInt64);

			// Token: 0x04001610 RID: 5648
			public static readonly ConstantValue.ConstantValueOne NInt = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.NInt);

			// Token: 0x04001611 RID: 5649
			public static readonly ConstantValue.ConstantValueOne NUInt = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.NUInt);

			// Token: 0x04001612 RID: 5650
			public static readonly ConstantValue.ConstantValueOne Single = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Single);

			// Token: 0x04001613 RID: 5651
			public static readonly ConstantValue.ConstantValueOne Double = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Double);

			// Token: 0x04001614 RID: 5652
			public static readonly ConstantValue.ConstantValueOne Decimal = new ConstantValue.ConstantValueDecimalOne();

			// Token: 0x04001615 RID: 5653
			public static readonly ConstantValue.ConstantValueOne Boolean = new ConstantValue.ConstantValueOne(ConstantValueTypeDiscriminator.Boolean);
		}

		// Token: 0x02000594 RID: 1428
		[NullableContext(0)]
		private sealed class ConstantValueDecimalOne : ConstantValue.ConstantValueOne
		{
			// Token: 0x0600352F RID: 13615 RVA: 0x00095C28 File Offset: 0x00093E28
			internal ConstantValueDecimalOne()
				: base(ConstantValueTypeDiscriminator.Decimal)
			{
			}

			// Token: 0x06003530 RID: 13616 RVA: 0x00095C32 File Offset: 0x00093E32
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return other == this || (other != null && this.Discriminator == other.Discriminator && other.DecimalValue == 1m);
			}
		}

		// Token: 0x02000595 RID: 1429
		[NullableContext(0)]
		private sealed class ConstantValueI8 : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003531 RID: 13617 RVA: 0x00095C5F File Offset: 0x00093E5F
			public ConstantValueI8(sbyte value)
				: base(ConstantValueTypeDiscriminator.SByte)
			{
				this._value = (byte)value;
			}

			// Token: 0x06003532 RID: 13618 RVA: 0x00095C70 File Offset: 0x00093E70
			public ConstantValueI8(byte value)
				: base(ConstantValueTypeDiscriminator.Byte)
			{
				this._value = value;
			}

			// Token: 0x17000F1F RID: 3871
			// (get) Token: 0x06003533 RID: 13619 RVA: 0x00095C80 File Offset: 0x00093E80
			public override byte ByteValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000F20 RID: 3872
			// (get) Token: 0x06003534 RID: 13620 RVA: 0x00095C88 File Offset: 0x00093E88
			public override sbyte SByteValue
			{
				get
				{
					return (sbyte)this._value;
				}
			}

			// Token: 0x06003535 RID: 13621 RVA: 0x00095C94 File Offset: 0x00093E94
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003536 RID: 13622 RVA: 0x00095CBA File Offset: 0x00093EBA
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.ByteValue;
			}

			// Token: 0x04001616 RID: 5654
			private readonly byte _value;
		}

		// Token: 0x02000596 RID: 1430
		[NullableContext(0)]
		private sealed class ConstantValueI16 : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003537 RID: 13623 RVA: 0x00095CD5 File Offset: 0x00093ED5
			public ConstantValueI16(short value)
				: base(ConstantValueTypeDiscriminator.Int16)
			{
				this._value = value;
			}

			// Token: 0x06003538 RID: 13624 RVA: 0x00095CE5 File Offset: 0x00093EE5
			public ConstantValueI16(ushort value)
				: base(ConstantValueTypeDiscriminator.UInt16)
			{
				this._value = (short)value;
			}

			// Token: 0x06003539 RID: 13625 RVA: 0x00095CF6 File Offset: 0x00093EF6
			public ConstantValueI16(char value)
				: base(ConstantValueTypeDiscriminator.Char)
			{
				this._value = (short)value;
			}

			// Token: 0x17000F21 RID: 3873
			// (get) Token: 0x0600353A RID: 13626 RVA: 0x00095D08 File Offset: 0x00093F08
			public override short Int16Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000F22 RID: 3874
			// (get) Token: 0x0600353B RID: 13627 RVA: 0x00095D10 File Offset: 0x00093F10
			public override ushort UInt16Value
			{
				get
				{
					return (ushort)this._value;
				}
			}

			// Token: 0x17000F23 RID: 3875
			// (get) Token: 0x0600353C RID: 13628 RVA: 0x00095D19 File Offset: 0x00093F19
			public override char CharValue
			{
				get
				{
					return (char)this._value;
				}
			}

			// Token: 0x0600353D RID: 13629 RVA: 0x00095D24 File Offset: 0x00093F24
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x0600353E RID: 13630 RVA: 0x00095D4A File Offset: 0x00093F4A
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.Int16Value;
			}

			// Token: 0x04001617 RID: 5655
			private readonly short _value;
		}

		// Token: 0x02000597 RID: 1431
		[NullableContext(0)]
		private sealed class ConstantValueI32 : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x0600353F RID: 13631 RVA: 0x00095D65 File Offset: 0x00093F65
			public ConstantValueI32(int value)
				: base(ConstantValueTypeDiscriminator.Int32)
			{
				this._value = value;
			}

			// Token: 0x06003540 RID: 13632 RVA: 0x00095D75 File Offset: 0x00093F75
			public ConstantValueI32(uint value)
				: base(ConstantValueTypeDiscriminator.UInt32)
			{
				this._value = (int)value;
			}

			// Token: 0x17000F24 RID: 3876
			// (get) Token: 0x06003541 RID: 13633 RVA: 0x00095D85 File Offset: 0x00093F85
			public override int Int32Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000F25 RID: 3877
			// (get) Token: 0x06003542 RID: 13634 RVA: 0x00095D8D File Offset: 0x00093F8D
			public override uint UInt32Value
			{
				get
				{
					return (uint)this._value;
				}
			}

			// Token: 0x06003543 RID: 13635 RVA: 0x00095D98 File Offset: 0x00093F98
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003544 RID: 13636 RVA: 0x00095DBE File Offset: 0x00093FBE
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.Int32Value;
			}

			// Token: 0x04001618 RID: 5656
			private readonly int _value;
		}

		// Token: 0x02000598 RID: 1432
		[NullableContext(0)]
		private sealed class ConstantValueI64 : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003545 RID: 13637 RVA: 0x00095DD9 File Offset: 0x00093FD9
			public ConstantValueI64(long value)
				: base(ConstantValueTypeDiscriminator.Int64)
			{
				this._value = value;
			}

			// Token: 0x06003546 RID: 13638 RVA: 0x00095DE9 File Offset: 0x00093FE9
			public ConstantValueI64(ulong value)
				: base(ConstantValueTypeDiscriminator.UInt64)
			{
				this._value = (long)value;
			}

			// Token: 0x17000F26 RID: 3878
			// (get) Token: 0x06003547 RID: 13639 RVA: 0x00095DFA File Offset: 0x00093FFA
			public override long Int64Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000F27 RID: 3879
			// (get) Token: 0x06003548 RID: 13640 RVA: 0x00095E02 File Offset: 0x00094002
			public override ulong UInt64Value
			{
				get
				{
					return (ulong)this._value;
				}
			}

			// Token: 0x06003549 RID: 13641 RVA: 0x00095E0C File Offset: 0x0009400C
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x0600354A RID: 13642 RVA: 0x00095E32 File Offset: 0x00094032
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.Int64Value;
			}

			// Token: 0x04001619 RID: 5657
			private readonly long _value;
		}

		// Token: 0x02000599 RID: 1433
		[NullableContext(0)]
		private sealed class ConstantValueNativeInt : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x0600354B RID: 13643 RVA: 0x00095E4D File Offset: 0x0009404D
			public ConstantValueNativeInt(int value)
				: base(ConstantValueTypeDiscriminator.NInt)
			{
				this._value = value;
			}

			// Token: 0x0600354C RID: 13644 RVA: 0x00095E5E File Offset: 0x0009405E
			public ConstantValueNativeInt(uint value)
				: base(ConstantValueTypeDiscriminator.NUInt)
			{
				this._value = (int)value;
			}

			// Token: 0x17000F28 RID: 3880
			// (get) Token: 0x0600354D RID: 13645 RVA: 0x00095E6F File Offset: 0x0009406F
			public override int Int32Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000F29 RID: 3881
			// (get) Token: 0x0600354E RID: 13646 RVA: 0x00095E77 File Offset: 0x00094077
			public override uint UInt32Value
			{
				get
				{
					return (uint)this._value;
				}
			}

			// Token: 0x0600354F RID: 13647 RVA: 0x00095E80 File Offset: 0x00094080
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003550 RID: 13648 RVA: 0x00095EA6 File Offset: 0x000940A6
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value == other.Int32Value;
			}

			// Token: 0x0400161A RID: 5658
			private readonly int _value;
		}

		// Token: 0x0200059A RID: 1434
		[NullableContext(0)]
		private sealed class ConstantValueDouble : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003551 RID: 13649 RVA: 0x00095EC1 File Offset: 0x000940C1
			public ConstantValueDouble(double value)
				: base(ConstantValueTypeDiscriminator.Double)
			{
				if (double.IsNaN(value))
				{
					value = ConstantValue._s_IEEE_canonical_NaN;
				}
				this._value = value;
			}

			// Token: 0x17000F2A RID: 3882
			// (get) Token: 0x06003552 RID: 13650 RVA: 0x00095EE1 File Offset: 0x000940E1
			public override double DoubleValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06003553 RID: 13651 RVA: 0x00095EEC File Offset: 0x000940EC
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003554 RID: 13652 RVA: 0x00095F14 File Offset: 0x00094114
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value.Equals(other.DoubleValue);
			}

			// Token: 0x0400161B RID: 5659
			private readonly double _value;
		}

		// Token: 0x0200059B RID: 1435
		[NullableContext(0)]
		private sealed class ConstantValueSingle : ConstantValue.ConstantValueDiscriminated
		{
			// Token: 0x06003555 RID: 13653 RVA: 0x00095F40 File Offset: 0x00094140
			public ConstantValueSingle(double value)
				: base(ConstantValueTypeDiscriminator.Single)
			{
				if (double.IsNaN(value))
				{
					value = ConstantValue._s_IEEE_canonical_NaN;
				}
				this._value = value;
			}

			// Token: 0x17000F2B RID: 3883
			// (get) Token: 0x06003556 RID: 13654 RVA: 0x00095F60 File Offset: 0x00094160
			public override double DoubleValue
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x17000F2C RID: 3884
			// (get) Token: 0x06003557 RID: 13655 RVA: 0x00095F68 File Offset: 0x00094168
			public override float SingleValue
			{
				get
				{
					return (float)this._value;
				}
			}

			// Token: 0x06003558 RID: 13656 RVA: 0x00095F74 File Offset: 0x00094174
			public override int GetHashCode()
			{
				return Hash.Combine(base.GetHashCode(), this._value.GetHashCode());
			}

			// Token: 0x06003559 RID: 13657 RVA: 0x00095F9C File Offset: 0x0009419C
			[NullableContext(2)]
			public override bool Equals(ConstantValue other)
			{
				return base.Equals(other) && this._value.Equals(other.DoubleValue);
			}

			// Token: 0x0400161C RID: 5660
			private readonly double _value;
		}
	}
}
