using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C9 RID: 457
	internal static class RealParser
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x0003A200 File Offset: 0x00038400
		[NullableContext(1)]
		public static bool TryParseDouble(string s, out double d)
		{
			RealParser.DecimalFloatingPointString decimalFloatingPointString = RealParser.DecimalFloatingPointString.FromSource(s);
			RealParser.DoubleFloatingPointType instance = RealParser.DoubleFloatingPointType.Instance;
			ulong num2;
			int num = (int)RealParser.ConvertDecimalToFloatingPointBits(decimalFloatingPointString, instance, out num2);
			d = BitConverter.Int64BitsToDouble((long)num2);
			return num != 3;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0003A230 File Offset: 0x00038430
		[NullableContext(1)]
		public static bool TryParseFloat(string s, out float f)
		{
			RealParser.DecimalFloatingPointString decimalFloatingPointString = RealParser.DecimalFloatingPointString.FromSource(s);
			RealParser.FloatFloatingPointType instance = RealParser.FloatFloatingPointType.Instance;
			ulong num2;
			int num = (int)RealParser.ConvertDecimalToFloatingPointBits(decimalFloatingPointString, instance, out num2);
			f = RealParser.Int32BitsToFloat((uint)num2);
			return num != 3;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0003A260 File Offset: 0x00038460
		[NullableContext(1)]
		private static RealParser.Status ConvertDecimalToFloatingPointBits(RealParser.DecimalFloatingPointString data, RealParser.FloatingPointType type, out ulong result)
		{
			if (data.Mantissa.Length == 0)
			{
				result = type.Zero;
				return RealParser.Status.NoDigits;
			}
			uint num = (uint)(type.NormalMantissaBits + 1);
			int num2 = Math.Max(0, data.Exponent);
			uint num3 = Math.Min((uint)num2, data.MantissaCount);
			uint num4 = (uint)(num2 - (int)num3);
			uint num5 = 0U;
			uint num6 = num3;
			uint num7 = num6;
			uint mantissaCount = data.MantissaCount;
			uint num8 = mantissaCount - num7;
			BigInteger bigInteger = RealParser.AccumulateDecimalDigitsIntoBigInteger(data, num5, num6);
			if (num4 > 0U)
			{
				if ((ulong)num4 > (ulong)((long)type.OverflowDecimalExponent))
				{
					result = type.Infinity;
					return RealParser.Status.Overflow;
				}
				RealParser.MultiplyByPowerOfTen(ref bigInteger, num4);
			}
			byte[] array;
			uint num9 = RealParser.CountSignificantBits(bigInteger, out array);
			if (num9 >= num || num8 == 0U)
			{
				return RealParser.ConvertBigIntegerToFloatingPointBits(array, num9, num8 > 0U, type, out result);
			}
			uint num10 = ((data.Exponent < 0) ? (num8 + (uint)(-(uint)data.Exponent)) : num8);
			if (num9 == 0U && (ulong)num10 - (ulong)((long)data.MantissaCount) > (ulong)((long)type.OverflowDecimalExponent))
			{
				result = type.Zero;
				return RealParser.Status.Underflow;
			}
			BigInteger bigInteger2 = RealParser.AccumulateDecimalDigitsIntoBigInteger(data, num7, mantissaCount);
			BigInteger bigInteger3 = RealParser.s_bigOne;
			RealParser.MultiplyByPowerOfTen(ref bigInteger3, num10);
			uint num11 = RealParser.CountSignificantBits(bigInteger2);
			uint num12 = RealParser.CountSignificantBits(bigInteger3);
			uint num13 = ((num12 > num11) ? (num12 - num11) : 0U);
			if (num13 > 0U)
			{
				RealParser.ShiftLeft(ref bigInteger2, num13);
			}
			uint num14 = num - num9;
			uint num15 = num14;
			if (num9 > 0U)
			{
				if (num13 > num15)
				{
					return RealParser.ConvertBigIntegerToFloatingPointBits(array, num9, num8 > 0U, type, out result);
				}
				num15 -= num13;
			}
			uint num16 = ((bigInteger2 < bigInteger3) ? (num13 + 1U) : num13);
			RealParser.ShiftLeft(ref bigInteger2, num15);
			BigInteger bigInteger4;
			ulong num17 = (ulong)BigInteger.DivRem(bigInteger2, bigInteger3, ref bigInteger4);
			bool flag = bigInteger4.IsZero;
			uint num18 = RealParser.CountSignificantBits(num17);
			if (num18 > num14)
			{
				int num19 = (int)(num18 - num14);
				flag = flag && (num17 & ((1UL << num19) - 1UL)) == 0UL;
				num17 >>= num19;
			}
			ulong num20 = ((ulong)bigInteger << (int)num14) + num17;
			int num21 = (int)((num9 > 0U) ? (num9 - 2U) : (-num16 - 1U));
			return type.AssembleFloatingPointValue(num20, num21, flag, out result);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0003A470 File Offset: 0x00038670
		[NullableContext(1)]
		private static RealParser.Status ConvertBigIntegerToFloatingPointBits(byte[] integerValueAsBytes, uint integerBitsOfPrecision, bool hasNonzeroFractionalPart, RealParser.FloatingPointType type, out ulong result)
		{
			int denormalMantissaBits = (int)type.DenormalMantissaBits;
			bool flag = !hasNonzeroFractionalPart;
			int num = (int)((integerBitsOfPrecision - 1U) / 8U);
			int num2 = Math.Max(0, num - 8 + 1);
			int num3 = denormalMantissaBits + num2 * 8;
			ulong num4 = 0UL;
			for (int i = num; i >= num2; i--)
			{
				num4 <<= 8;
				num4 |= (ulong)integerValueAsBytes[i];
			}
			int num5 = num2 - 1;
			while (flag && num5 >= 0)
			{
				if (integerValueAsBytes[num5] != 0)
				{
					flag = false;
				}
				num5--;
			}
			return type.AssembleFloatingPointValue(num4, num3, flag, out result);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0003A4EA File Offset: 0x000386EA
		private static BigInteger AccumulateDecimalDigitsIntoBigInteger(RealParser.DecimalFloatingPointString data, uint integer_first_index, uint integer_last_index)
		{
			if (integer_first_index == integer_last_index)
			{
				return RealParser.s_bigZero;
			}
			return BigInteger.Parse(data.Mantissa.Substring((int)integer_first_index, (int)(integer_last_index - integer_first_index)));
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0003A50C File Offset: 0x0003870C
		private static uint CountSignificantBits(ulong data)
		{
			uint num = 0U;
			while (data != 0UL)
			{
				data >>= 1;
				num += 1U;
			}
			return num;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0003A52C File Offset: 0x0003872C
		private static uint CountSignificantBits(byte data)
		{
			uint num = 0U;
			while (data != 0)
			{
				data = (byte)(data >> 1);
				num += 1U;
			}
			return num;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0003A54C File Offset: 0x0003874C
		[NullableContext(1)]
		private static uint CountSignificantBits(BigInteger data, out byte[] dataBytes)
		{
			if (data.IsZero)
			{
				dataBytes = new byte[1];
				return 0U;
			}
			dataBytes = data.ToByteArray();
			for (int i = dataBytes.Length - 1; i >= 0; i--)
			{
				byte b = dataBytes[i];
				if (b != 0)
				{
					return (uint)(8 * i + (int)RealParser.CountSignificantBits(b));
				}
			}
			return 0U;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0003A59C File Offset: 0x0003879C
		private static uint CountSignificantBits(BigInteger data)
		{
			byte[] array;
			return RealParser.CountSignificantBits(data, out array);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0003A5B4 File Offset: 0x000387B4
		private static ulong RightShiftWithRounding(ulong value, int shift, bool hasZeroTail)
		{
			if (shift >= 64)
			{
				return 0UL;
			}
			ulong num = (1UL << shift - 1) - 1UL;
			ulong num2 = 1UL << shift - 1;
			ulong num3 = 1UL << shift;
			bool flag = (value & num3) > 0UL;
			bool flag2 = (value & num2) > 0UL;
			bool flag3 = !hasZeroTail || (value & num) > 0UL;
			return (value >> shift) + (RealParser.ShouldRoundUp(flag, flag2, flag3) ? 1UL : 0UL);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0003A621 File Offset: 0x00038821
		private static bool ShouldRoundUp(bool lsbBit, bool roundBit, bool hasTailBits)
		{
			return roundBit && (hasTailBits || lsbBit);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0003A62C File Offset: 0x0003882C
		private static void ShiftLeft(ref BigInteger number, uint shift)
		{
			BigInteger bigInteger = BigInteger.Pow(RealParser.s_bigTwo, (int)shift);
			number *= bigInteger;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0003A658 File Offset: 0x00038858
		private static void MultiplyByPowerOfTen(ref BigInteger number, uint power)
		{
			BigInteger bigInteger = BigInteger.Pow(RealParser.s_bigTen, (int)power);
			number *= bigInteger;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0003A684 File Offset: 0x00038884
		private static uint FloatToInt32Bits(float f)
		{
			return new RealParser.FloatUnion
			{
				FloatData = f
			}.IntData;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003A6A8 File Offset: 0x000388A8
		private static float Int32BitsToFloat(uint i)
		{
			return new RealParser.FloatUnion
			{
				IntData = i
			}.FloatData;
		}

		// Token: 0x040005AF RID: 1455
		private static readonly BigInteger s_bigZero = BigInteger.Zero;

		// Token: 0x040005B0 RID: 1456
		private static readonly BigInteger s_bigOne = BigInteger.One;

		// Token: 0x040005B1 RID: 1457
		private static readonly BigInteger s_bigTwo = new BigInteger(2);

		// Token: 0x040005B2 RID: 1458
		private static readonly BigInteger s_bigTen = new BigInteger(10);

		// Token: 0x020005CD RID: 1485
		private abstract class FloatingPointType
		{
			// Token: 0x17000F55 RID: 3925
			// (get) Token: 0x06003669 RID: 13929
			public abstract ushort DenormalMantissaBits { get; }

			// Token: 0x17000F56 RID: 3926
			// (get) Token: 0x0600366A RID: 13930 RVA: 0x00099436 File Offset: 0x00097636
			public ushort NormalMantissaBits
			{
				get
				{
					return this.DenormalMantissaBits + 1;
				}
			}

			// Token: 0x17000F57 RID: 3927
			// (get) Token: 0x0600366B RID: 13931
			public abstract ushort ExponentBits { get; }

			// Token: 0x17000F58 RID: 3928
			// (get) Token: 0x0600366C RID: 13932 RVA: 0x00099441 File Offset: 0x00097641
			public int MinBinaryExponent
			{
				get
				{
					return 1 - this.MaxBinaryExponent;
				}
			}

			// Token: 0x17000F59 RID: 3929
			// (get) Token: 0x0600366D RID: 13933
			public abstract int MaxBinaryExponent { get; }

			// Token: 0x17000F5A RID: 3930
			// (get) Token: 0x0600366E RID: 13934 RVA: 0x0009944B File Offset: 0x0009764B
			public int OverflowDecimalExponent
			{
				get
				{
					return (this.MaxBinaryExponent + (int)(2 * this.NormalMantissaBits)) / 3;
				}
			}

			// Token: 0x17000F5B RID: 3931
			// (get) Token: 0x0600366F RID: 13935
			public abstract int ExponentBias { get; }

			// Token: 0x17000F5C RID: 3932
			// (get) Token: 0x06003670 RID: 13936 RVA: 0x0009945E File Offset: 0x0009765E
			public ulong DenormalMantissaMask
			{
				get
				{
					return (1UL << (int)this.DenormalMantissaBits) - 1UL;
				}
			}

			// Token: 0x17000F5D RID: 3933
			// (get) Token: 0x06003671 RID: 13937 RVA: 0x0009946F File Offset: 0x0009766F
			public ulong NormalMantissaMask
			{
				get
				{
					return (1UL << (int)this.NormalMantissaBits) - 1UL;
				}
			}

			// Token: 0x17000F5E RID: 3934
			// (get) Token: 0x06003672 RID: 13938
			public abstract ulong Zero { get; }

			// Token: 0x17000F5F RID: 3935
			// (get) Token: 0x06003673 RID: 13939
			public abstract ulong Infinity { get; }

			// Token: 0x06003674 RID: 13940 RVA: 0x00099480 File Offset: 0x00097680
			public RealParser.Status AssembleFloatingPointValue(ulong initialMantissa, int initialExponent, bool hasZeroTail, out ulong result)
			{
				uint num = RealParser.CountSignificantBits(initialMantissa);
				int num2 = (int)((uint)this.NormalMantissaBits - num);
				int num3 = initialExponent - num2;
				ulong num4 = initialMantissa;
				int num5 = num3;
				if (num3 > this.MaxBinaryExponent)
				{
					result = this.Infinity;
					return RealParser.Status.Overflow;
				}
				if (num3 < this.MinBinaryExponent)
				{
					int num6 = num2 + num3 + this.ExponentBias - 1;
					num5 = -this.ExponentBias;
					if (num6 < 0)
					{
						num4 = RealParser.RightShiftWithRounding(num4, -num6, hasZeroTail);
						if (num4 == 0UL)
						{
							result = this.Zero;
							return RealParser.Status.Underflow;
						}
						if (num4 > this.DenormalMantissaMask)
						{
							num5 = initialExponent - (num6 + 1) - num2;
						}
					}
					else
					{
						num4 <<= num6;
					}
				}
				else if (num2 < 0)
				{
					num4 = RealParser.RightShiftWithRounding(num4, -num2, hasZeroTail);
					if (num4 > this.NormalMantissaMask)
					{
						num4 >>= 1;
						num5++;
						if (num5 > this.MaxBinaryExponent)
						{
							result = this.Infinity;
							return RealParser.Status.Overflow;
						}
					}
				}
				else if (num2 > 0)
				{
					num4 <<= num2;
				}
				num4 &= this.DenormalMantissaMask;
				ulong num7 = (ulong)((ulong)((long)(num5 + this.ExponentBias)) << (int)this.DenormalMantissaBits);
				result = num7 | num4;
				return RealParser.Status.OK;
			}
		}

		// Token: 0x020005CE RID: 1486
		private sealed class FloatFloatingPointType : RealParser.FloatingPointType
		{
			// Token: 0x06003676 RID: 13942 RVA: 0x00099587 File Offset: 0x00097787
			private FloatFloatingPointType()
			{
			}

			// Token: 0x17000F60 RID: 3936
			// (get) Token: 0x06003677 RID: 13943 RVA: 0x0009958F File Offset: 0x0009778F
			public override ushort DenormalMantissaBits
			{
				get
				{
					return 23;
				}
			}

			// Token: 0x17000F61 RID: 3937
			// (get) Token: 0x06003678 RID: 13944 RVA: 0x00099593 File Offset: 0x00097793
			public override ushort ExponentBits
			{
				get
				{
					return 8;
				}
			}

			// Token: 0x17000F62 RID: 3938
			// (get) Token: 0x06003679 RID: 13945 RVA: 0x00099596 File Offset: 0x00097796
			public override int MaxBinaryExponent
			{
				get
				{
					return 127;
				}
			}

			// Token: 0x17000F63 RID: 3939
			// (get) Token: 0x0600367A RID: 13946 RVA: 0x0009959A File Offset: 0x0009779A
			public override int ExponentBias
			{
				get
				{
					return 127;
				}
			}

			// Token: 0x17000F64 RID: 3940
			// (get) Token: 0x0600367B RID: 13947 RVA: 0x0009959E File Offset: 0x0009779E
			public override ulong Zero
			{
				get
				{
					return (ulong)RealParser.FloatToInt32Bits(0f);
				}
			}

			// Token: 0x17000F65 RID: 3941
			// (get) Token: 0x0600367C RID: 13948 RVA: 0x000995AB File Offset: 0x000977AB
			public override ulong Infinity
			{
				get
				{
					return (ulong)RealParser.FloatToInt32Bits(float.PositiveInfinity);
				}
			}

			// Token: 0x040016AA RID: 5802
			[Nullable(1)]
			public static RealParser.FloatFloatingPointType Instance = new RealParser.FloatFloatingPointType();
		}

		// Token: 0x020005CF RID: 1487
		private sealed class DoubleFloatingPointType : RealParser.FloatingPointType
		{
			// Token: 0x0600367E RID: 13950 RVA: 0x000995C4 File Offset: 0x000977C4
			private DoubleFloatingPointType()
			{
			}

			// Token: 0x17000F66 RID: 3942
			// (get) Token: 0x0600367F RID: 13951 RVA: 0x000995CC File Offset: 0x000977CC
			public override ushort DenormalMantissaBits
			{
				get
				{
					return 52;
				}
			}

			// Token: 0x17000F67 RID: 3943
			// (get) Token: 0x06003680 RID: 13952 RVA: 0x000995D0 File Offset: 0x000977D0
			public override ushort ExponentBits
			{
				get
				{
					return 11;
				}
			}

			// Token: 0x17000F68 RID: 3944
			// (get) Token: 0x06003681 RID: 13953 RVA: 0x000995D4 File Offset: 0x000977D4
			public override int MaxBinaryExponent
			{
				get
				{
					return 1023;
				}
			}

			// Token: 0x17000F69 RID: 3945
			// (get) Token: 0x06003682 RID: 13954 RVA: 0x000995DB File Offset: 0x000977DB
			public override int ExponentBias
			{
				get
				{
					return 1023;
				}
			}

			// Token: 0x17000F6A RID: 3946
			// (get) Token: 0x06003683 RID: 13955 RVA: 0x000995E2 File Offset: 0x000977E2
			public override ulong Zero
			{
				get
				{
					return (ulong)BitConverter.DoubleToInt64Bits(0.0);
				}
			}

			// Token: 0x17000F6B RID: 3947
			// (get) Token: 0x06003684 RID: 13956 RVA: 0x000995F2 File Offset: 0x000977F2
			public override ulong Infinity
			{
				get
				{
					return (ulong)BitConverter.DoubleToInt64Bits(double.PositiveInfinity);
				}
			}

			// Token: 0x040016AB RID: 5803
			[Nullable(1)]
			public static RealParser.DoubleFloatingPointType Instance = new RealParser.DoubleFloatingPointType();
		}

		// Token: 0x020005D0 RID: 1488
		[NullableContext(1)]
		[Nullable(0)]
		[DebuggerDisplay("0.{Mantissa}e{Exponent}")]
		private struct DecimalFloatingPointString
		{
			// Token: 0x17000F6C RID: 3948
			// (get) Token: 0x06003686 RID: 13958 RVA: 0x0009960E File Offset: 0x0009780E
			public uint MantissaCount
			{
				get
				{
					return (uint)this.Mantissa.Length;
				}
			}

			// Token: 0x06003687 RID: 13959 RVA: 0x0009961C File Offset: 0x0009781C
			public static RealParser.DecimalFloatingPointString FromSource(string source)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				int num2 = 0;
				while (num2 < source.Length && source.get_Chars(num2) == '0')
				{
					num2++;
				}
				int num3 = 0;
				while (num2 < source.Length && source.get_Chars(num2) >= '0' && source.get_Chars(num2) <= '9')
				{
					if (source.get_Chars(num2) == '0')
					{
						num3++;
					}
					else
					{
						stringBuilder.Append('0', num3);
						num3 = 0;
						stringBuilder.Append(source.get_Chars(num2));
					}
					num++;
					num2++;
				}
				if (num2 < source.Length && source.get_Chars(num2) == '.')
				{
					num2++;
					while (num2 < source.Length && source.get_Chars(num2) >= '0' && source.get_Chars(num2) <= '9')
					{
						if (source.get_Chars(num2) == '0')
						{
							num3++;
						}
						else
						{
							stringBuilder.Append('0', num3);
							num3 = 0;
							stringBuilder.Append(source.get_Chars(num2));
						}
						num2++;
					}
				}
				RealParser.DecimalFloatingPointString decimalFloatingPointString = default(RealParser.DecimalFloatingPointString);
				decimalFloatingPointString.Mantissa = stringBuilder.ToString();
				if (num2 < source.Length && (source.get_Chars(num2) == 'e' || source.get_Chars(num2) == 'E'))
				{
					char c = '\0';
					num2++;
					if (num2 < source.Length && (source.get_Chars(num2) == '-' || source.get_Chars(num2) == '+'))
					{
						c = source.get_Chars(num2);
						num2++;
					}
					int num4 = num2;
					int num5 = num2;
					while (num2 < source.Length && source.get_Chars(num2) >= '0' && source.get_Chars(num2) <= '9')
					{
						num2 = (num5 = num2 + 1);
					}
					int num6 = 0;
					if (int.TryParse(source.Substring(num4, num5 - num4), ref num6) && num6 <= 1073741824)
					{
						if (c == '-')
						{
							num -= num6;
						}
						else
						{
							num += num6;
						}
					}
					else
					{
						num = ((c == '-') ? (-1073741824) : 1073741824);
					}
				}
				decimalFloatingPointString.Exponent = num;
				return decimalFloatingPointString;
			}

			// Token: 0x040016AC RID: 5804
			public int Exponent;

			// Token: 0x040016AD RID: 5805
			public string Mantissa;
		}

		// Token: 0x020005D1 RID: 1489
		private enum Status
		{
			// Token: 0x040016AF RID: 5807
			OK,
			// Token: 0x040016B0 RID: 5808
			NoDigits,
			// Token: 0x040016B1 RID: 5809
			Underflow,
			// Token: 0x040016B2 RID: 5810
			Overflow
		}

		// Token: 0x020005D2 RID: 1490
		[StructLayout(2)]
		private struct FloatUnion
		{
			// Token: 0x040016B3 RID: 5811
			[FieldOffset(0)]
			public uint IntData;

			// Token: 0x040016B4 RID: 5812
			[FieldOffset(0)]
			public float FloatData;
		}
	}
}
