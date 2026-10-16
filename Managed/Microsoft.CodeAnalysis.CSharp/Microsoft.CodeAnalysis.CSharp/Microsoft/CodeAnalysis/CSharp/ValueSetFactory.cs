using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200023D RID: 573
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class ValueSetFactory
	{
		// Token: 0x06002F16 RID: 12054 RVA: 0x000F1D98 File Offset: 0x000EFF98
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IValueSetFactory ForSpecialType(SpecialType specialType, bool isNative = false)
		{
			switch (specialType)
			{
			case SpecialType.System_Boolean:
				return ValueSetFactory.ForBool;
			case SpecialType.System_Char:
				return ValueSetFactory.ForChar;
			case SpecialType.System_SByte:
				return ValueSetFactory.ForSByte;
			case SpecialType.System_Byte:
				return ValueSetFactory.ForByte;
			case SpecialType.System_Int16:
				return ValueSetFactory.ForShort;
			case SpecialType.System_UInt16:
				return ValueSetFactory.ForUShort;
			case SpecialType.System_Int32:
				return ValueSetFactory.ForInt;
			case SpecialType.System_UInt32:
				return ValueSetFactory.ForUInt;
			case SpecialType.System_Int64:
				return ValueSetFactory.ForLong;
			case SpecialType.System_UInt64:
				return ValueSetFactory.ForULong;
			case SpecialType.System_Decimal:
				return ValueSetFactory.ForDecimal;
			case SpecialType.System_Single:
				return ValueSetFactory.ForFloat;
			case SpecialType.System_Double:
				return ValueSetFactory.ForDouble;
			case SpecialType.System_String:
				return ValueSetFactory.ForString;
			case SpecialType.System_IntPtr:
				if (isNative)
				{
					return ValueSetFactory.ForNint;
				}
				break;
			case SpecialType.System_UIntPtr:
				if (isNative)
				{
					return ValueSetFactory.ForNuint;
				}
				break;
			}
			return null;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000F1E58 File Offset: 0x000F0058
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static IValueSetFactory ForType(TypeSymbol type)
		{
			type = type.EnumUnderlyingTypeOrSelf();
			return ValueSetFactory.ForSpecialType(type.SpecialType, type.IsNativeIntegerType);
		}

		// Token: 0x0400137D RID: 4989
		internal static readonly IValueSetFactory<byte> ForByte = ValueSetFactory.NumericValueSetFactory<byte, ValueSetFactory.ByteTC>.Instance;

		// Token: 0x0400137E RID: 4990
		internal static readonly IValueSetFactory<sbyte> ForSByte = ValueSetFactory.NumericValueSetFactory<sbyte, ValueSetFactory.SByteTC>.Instance;

		// Token: 0x0400137F RID: 4991
		internal static readonly IValueSetFactory<char> ForChar = ValueSetFactory.NumericValueSetFactory<char, ValueSetFactory.CharTC>.Instance;

		// Token: 0x04001380 RID: 4992
		internal static readonly IValueSetFactory<short> ForShort = ValueSetFactory.NumericValueSetFactory<short, ValueSetFactory.ShortTC>.Instance;

		// Token: 0x04001381 RID: 4993
		internal static readonly IValueSetFactory<ushort> ForUShort = ValueSetFactory.NumericValueSetFactory<ushort, ValueSetFactory.UShortTC>.Instance;

		// Token: 0x04001382 RID: 4994
		internal static readonly IValueSetFactory<int> ForInt = ValueSetFactory.NumericValueSetFactory<int, ValueSetFactory.IntTC>.Instance;

		// Token: 0x04001383 RID: 4995
		internal static readonly IValueSetFactory<uint> ForUInt = ValueSetFactory.NumericValueSetFactory<uint, ValueSetFactory.UIntTC>.Instance;

		// Token: 0x04001384 RID: 4996
		internal static readonly IValueSetFactory<long> ForLong = ValueSetFactory.NumericValueSetFactory<long, ValueSetFactory.LongTC>.Instance;

		// Token: 0x04001385 RID: 4997
		internal static readonly IValueSetFactory<ulong> ForULong = ValueSetFactory.NumericValueSetFactory<ulong, ValueSetFactory.ULongTC>.Instance;

		// Token: 0x04001386 RID: 4998
		internal static readonly IValueSetFactory<bool> ForBool = ValueSetFactory.BoolValueSetFactory.Instance;

		// Token: 0x04001387 RID: 4999
		internal static readonly IValueSetFactory<float> ForFloat = ValueSetFactory.FloatingValueSetFactory<float, ValueSetFactory.SingleTC>.Instance;

		// Token: 0x04001388 RID: 5000
		internal static readonly IValueSetFactory<double> ForDouble = ValueSetFactory.FloatingValueSetFactory<double, ValueSetFactory.DoubleTC>.Instance;

		// Token: 0x04001389 RID: 5001
		internal static readonly IValueSetFactory<string> ForString = ValueSetFactory.EnumeratedValueSetFactory<string, ValueSetFactory.StringTC>.Instance;

		// Token: 0x0400138A RID: 5002
		internal static readonly IValueSetFactory<decimal> ForDecimal = ValueSetFactory.DecimalValueSetFactory.Instance;

		// Token: 0x0400138B RID: 5003
		internal static readonly IValueSetFactory<int> ForNint = ValueSetFactory.NintValueSetFactory.Instance;

		// Token: 0x0400138C RID: 5004
		internal static readonly IValueSetFactory<uint> ForNuint = ValueSetFactory.NuintValueSetFactory.Instance;

		// Token: 0x02000764 RID: 1892
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class BoolValueSet : IValueSet<bool>, IValueSet
		{
			// Token: 0x06008722 RID: 34594 RVA: 0x001E9B34 File Offset: 0x001E7D34
			private BoolValueSet(bool hasFalse, bool hasTrue)
			{
				this._hasFalse = hasFalse;
				this._hasTrue = hasTrue;
			}

			// Token: 0x06008723 RID: 34595 RVA: 0x001E9B59 File Offset: 0x001E7D59
			public static ValueSetFactory.BoolValueSet Create(bool hasFalse, bool hasTrue)
			{
				if (!hasFalse)
				{
					if (hasTrue)
					{
						return ValueSetFactory.BoolValueSet.OnlyTrue;
					}
					return ValueSetFactory.BoolValueSet.None;
				}
				else
				{
					if (hasTrue)
					{
						return ValueSetFactory.BoolValueSet.AllValues;
					}
					return ValueSetFactory.BoolValueSet.OnlyFalse;
				}
			}

			// Token: 0x170026EE RID: 9966
			// (get) Token: 0x06008724 RID: 34596 RVA: 0x001E9B7F File Offset: 0x001E7D7F
			bool IValueSet.IsEmpty
			{
				get
				{
					return !this._hasFalse && !this._hasTrue;
				}
			}

			// Token: 0x170026EF RID: 9967
			// (get) Token: 0x06008725 RID: 34597 RVA: 0x001E9B94 File Offset: 0x001E7D94
			ConstantValue IValueSet.Sample
			{
				get
				{
					bool flag;
					if (!this._hasTrue)
					{
						if (!this._hasFalse)
						{
							throw new ArgumentException();
						}
						flag = false;
					}
					else
					{
						flag = true;
					}
					return ConstantValue.Create(flag);
				}
			}

			// Token: 0x06008726 RID: 34598 RVA: 0x001E9BB5 File Offset: 0x001E7DB5
			public bool Any(BinaryOperatorKind relation, bool value)
			{
				if (relation != BinaryOperatorKind.Equal)
				{
					return true;
				}
				if (value)
				{
					return this._hasTrue;
				}
				return this._hasFalse;
			}

			// Token: 0x06008727 RID: 34599 RVA: 0x001E9BD1 File Offset: 0x001E7DD1
			bool IValueSet.Any(BinaryOperatorKind relation, ConstantValue value)
			{
				return value.IsBad || this.Any(relation, value.BooleanValue);
			}

			// Token: 0x06008728 RID: 34600 RVA: 0x001E9BEA File Offset: 0x001E7DEA
			public bool All(BinaryOperatorKind relation, bool value)
			{
				if (relation != BinaryOperatorKind.Equal)
				{
					return true;
				}
				if (value)
				{
					return !this._hasFalse;
				}
				return !this._hasTrue;
			}

			// Token: 0x06008729 RID: 34601 RVA: 0x001E9C0C File Offset: 0x001E7E0C
			bool IValueSet.All(BinaryOperatorKind relation, ConstantValue value)
			{
				return !value.IsBad && this.All(relation, value.BooleanValue);
			}

			// Token: 0x0600872A RID: 34602 RVA: 0x001E9C25 File Offset: 0x001E7E25
			public IValueSet<bool> Complement()
			{
				return ValueSetFactory.BoolValueSet.Create(!this._hasFalse, !this._hasTrue);
			}

			// Token: 0x0600872B RID: 34603 RVA: 0x001E9C3E File Offset: 0x001E7E3E
			IValueSet IValueSet.Complement()
			{
				return this.Complement();
			}

			// Token: 0x0600872C RID: 34604 RVA: 0x001E9C48 File Offset: 0x001E7E48
			public IValueSet<bool> Intersect(IValueSet<bool> other)
			{
				if (this == other)
				{
					return this;
				}
				ValueSetFactory.BoolValueSet boolValueSet = (ValueSetFactory.BoolValueSet)other;
				return ValueSetFactory.BoolValueSet.Create(this._hasFalse & boolValueSet._hasFalse, this._hasTrue & boolValueSet._hasTrue);
			}

			// Token: 0x0600872D RID: 34605 RVA: 0x001E9C81 File Offset: 0x001E7E81
			public IValueSet Intersect(IValueSet other)
			{
				return this.Intersect((IValueSet<bool>)other);
			}

			// Token: 0x0600872E RID: 34606 RVA: 0x001E9C90 File Offset: 0x001E7E90
			public IValueSet<bool> Union(IValueSet<bool> other)
			{
				if (this == other)
				{
					return this;
				}
				ValueSetFactory.BoolValueSet boolValueSet = (ValueSetFactory.BoolValueSet)other;
				return ValueSetFactory.BoolValueSet.Create(this._hasFalse | boolValueSet._hasFalse, this._hasTrue | boolValueSet._hasTrue);
			}

			// Token: 0x0600872F RID: 34607 RVA: 0x001E9CC9 File Offset: 0x001E7EC9
			IValueSet IValueSet.Union(IValueSet other)
			{
				return this.Union((IValueSet<bool>)other);
			}

			// Token: 0x06008730 RID: 34608 RVA: 0x001E9CD7 File Offset: 0x001E7ED7
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this == obj;
			}

			// Token: 0x06008731 RID: 34609 RVA: 0x001E9CDD File Offset: 0x001E7EDD
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this);
			}

			// Token: 0x06008732 RID: 34610 RVA: 0x001E9CE8 File Offset: 0x001E7EE8
			public override string ToString()
			{
				bool hasFalse = this._hasFalse;
				bool hasTrue = this._hasTrue;
				string text;
				if (!hasFalse)
				{
					if (hasTrue)
					{
						text = "{true}";
					}
					else
					{
						text = "{}";
					}
				}
				else if (hasTrue)
				{
					text = "{false,true}";
				}
				else
				{
					text = "{false}";
				}
				return text;
			}

			// Token: 0x04002389 RID: 9097
			private readonly bool _hasFalse;

			// Token: 0x0400238A RID: 9098
			private readonly bool _hasTrue;

			// Token: 0x0400238B RID: 9099
			internal static readonly ValueSetFactory.BoolValueSet AllValues = new ValueSetFactory.BoolValueSet(true, true);

			// Token: 0x0400238C RID: 9100
			internal static readonly ValueSetFactory.BoolValueSet None = new ValueSetFactory.BoolValueSet(false, false);

			// Token: 0x0400238D RID: 9101
			internal static readonly ValueSetFactory.BoolValueSet OnlyTrue = new ValueSetFactory.BoolValueSet(false, true);

			// Token: 0x0400238E RID: 9102
			internal static readonly ValueSetFactory.BoolValueSet OnlyFalse = new ValueSetFactory.BoolValueSet(true, false);
		}

		// Token: 0x02000765 RID: 1893
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class BoolValueSetFactory : IValueSetFactory<bool>, IValueSetFactory
		{
			// Token: 0x06008734 RID: 34612 RVA: 0x001E9D5F File Offset: 0x001E7F5F
			private BoolValueSetFactory()
			{
			}

			// Token: 0x170026F0 RID: 9968
			// (get) Token: 0x06008735 RID: 34613 RVA: 0x001E9D67 File Offset: 0x001E7F67
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.BoolValueSet.AllValues;
				}
			}

			// Token: 0x170026F1 RID: 9969
			// (get) Token: 0x06008736 RID: 34614 RVA: 0x001E9D6E File Offset: 0x001E7F6E
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.BoolValueSet.None;
				}
			}

			// Token: 0x06008737 RID: 34615 RVA: 0x001E9D75 File Offset: 0x001E7F75
			public IValueSet<bool> Related(BinaryOperatorKind relation, bool value)
			{
				if (relation != BinaryOperatorKind.Equal)
				{
					return ValueSetFactory.BoolValueSet.AllValues;
				}
				if (value)
				{
					return ValueSetFactory.BoolValueSet.OnlyTrue;
				}
				return ValueSetFactory.BoolValueSet.OnlyFalse;
			}

			// Token: 0x06008738 RID: 34616 RVA: 0x001E9D94 File Offset: 0x001E7F94
			IValueSet IValueSetFactory.Random(int expectedSize, Random random)
			{
				ValueSetFactory.BoolValueSet boolValueSet;
				switch (random.Next(4))
				{
				case 0:
					boolValueSet = ValueSetFactory.BoolValueSet.None;
					break;
				case 1:
					boolValueSet = ValueSetFactory.BoolValueSet.OnlyFalse;
					break;
				case 2:
					boolValueSet = ValueSetFactory.BoolValueSet.OnlyTrue;
					break;
				case 3:
					boolValueSet = ValueSetFactory.BoolValueSet.AllValues;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue("random");
				}
				return boolValueSet;
			}

			// Token: 0x06008739 RID: 34617 RVA: 0x001E9DED File Offset: 0x001E7FED
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				return ConstantValue.Create(random.NextDouble() < 0.5);
			}

			// Token: 0x0600873A RID: 34618 RVA: 0x001E9E08 File Offset: 0x001E8008
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					return this.Related(relation, value.BooleanValue);
				}
				return ValueSetFactory.BoolValueSet.AllValues;
			}

			// Token: 0x0600873B RID: 34619 RVA: 0x001E9E32 File Offset: 0x001E8032
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				return left.IsBad || right.IsBad || left.BooleanValue == right.BooleanValue;
			}

			// Token: 0x0400238F RID: 9103
			public static readonly ValueSetFactory.BoolValueSetFactory Instance = new ValueSetFactory.BoolValueSetFactory();
		}

		// Token: 0x02000766 RID: 1894
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct ByteTC : ValueSetFactory.INumericTC<byte>
		{
			// Token: 0x170026F2 RID: 9970
			// (get) Token: 0x0600873D RID: 34621 RVA: 0x001E9E60 File Offset: 0x001E8060
			byte ValueSetFactory.INumericTC<byte>.MinValue
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170026F3 RID: 9971
			// (get) Token: 0x0600873E RID: 34622 RVA: 0x001E9E63 File Offset: 0x001E8063
			byte ValueSetFactory.INumericTC<byte>.MaxValue
			{
				get
				{
					return byte.MaxValue;
				}
			}

			// Token: 0x170026F4 RID: 9972
			// (get) Token: 0x0600873F RID: 34623 RVA: 0x001E9E6A File Offset: 0x001E806A
			byte ValueSetFactory.INumericTC<byte>.Zero
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06008740 RID: 34624 RVA: 0x001E9E70 File Offset: 0x001E8070
			bool ValueSetFactory.INumericTC<byte>.Related(BinaryOperatorKind relation, byte left, byte right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x06008741 RID: 34625 RVA: 0x001E9EDA File Offset: 0x001E80DA
			byte ValueSetFactory.INumericTC<byte>.Next(byte value)
			{
				return value + 1;
			}

			// Token: 0x06008742 RID: 34626 RVA: 0x001E9EE0 File Offset: 0x001E80E0
			byte ValueSetFactory.INumericTC<byte>.Prev(byte value)
			{
				return value - 1;
			}

			// Token: 0x06008743 RID: 34627 RVA: 0x001E9EE6 File Offset: 0x001E80E6
			byte ValueSetFactory.INumericTC<byte>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.ByteValue;
				}
				return 0;
			}

			// Token: 0x06008744 RID: 34628 RVA: 0x001E9EF8 File Offset: 0x001E80F8
			ConstantValue ValueSetFactory.INumericTC<byte>.ToConstantValue(byte value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x06008745 RID: 34629 RVA: 0x001E9F00 File Offset: 0x001E8100
			string ValueSetFactory.INumericTC<byte>.ToString(byte value)
			{
				return value.ToString();
			}

			// Token: 0x06008746 RID: 34630 RVA: 0x001E9F09 File Offset: 0x001E8109
			byte ValueSetFactory.INumericTC<byte>.Random(Random random)
			{
				return (byte)random.Next(0, 256);
			}
		}

		// Token: 0x02000767 RID: 1895
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct CharTC : ValueSetFactory.INumericTC<char>
		{
			// Token: 0x170026F5 RID: 9973
			// (get) Token: 0x06008747 RID: 34631 RVA: 0x001E9F18 File Offset: 0x001E8118
			char ValueSetFactory.INumericTC<char>.MinValue
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x170026F6 RID: 9974
			// (get) Token: 0x06008748 RID: 34632 RVA: 0x001E9F1B File Offset: 0x001E811B
			char ValueSetFactory.INumericTC<char>.MaxValue
			{
				get
				{
					return char.MaxValue;
				}
			}

			// Token: 0x170026F7 RID: 9975
			// (get) Token: 0x06008749 RID: 34633 RVA: 0x001E9F22 File Offset: 0x001E8122
			char ValueSetFactory.INumericTC<char>.Zero
			{
				get
				{
					return '\0';
				}
			}

			// Token: 0x0600874A RID: 34634 RVA: 0x001E9F28 File Offset: 0x001E8128
			bool ValueSetFactory.INumericTC<char>.Related(BinaryOperatorKind relation, char left, char right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x0600874B RID: 34635 RVA: 0x001E9F92 File Offset: 0x001E8192
			char ValueSetFactory.INumericTC<char>.Next(char value)
			{
				return value + '\u0001';
			}

			// Token: 0x0600874C RID: 34636 RVA: 0x001E9F98 File Offset: 0x001E8198
			char ValueSetFactory.INumericTC<char>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.CharValue;
				}
				return '\0';
			}

			// Token: 0x0600874D RID: 34637 RVA: 0x001E9FAA File Offset: 0x001E81AA
			string ValueSetFactory.INumericTC<char>.ToString(char c)
			{
				return ObjectDisplay.FormatPrimitive(c, ObjectDisplayOptions.UseQuotes | ObjectDisplayOptions.EscapeNonPrintableCharacters);
			}

			// Token: 0x0600874E RID: 34638 RVA: 0x001E9FB9 File Offset: 0x001E81B9
			char ValueSetFactory.INumericTC<char>.Prev(char value)
			{
				return value - '\u0001';
			}

			// Token: 0x0600874F RID: 34639 RVA: 0x001E9FBF File Offset: 0x001E81BF
			char ValueSetFactory.INumericTC<char>.Random(Random random)
			{
				return (char)random.Next(0, 65536);
			}

			// Token: 0x06008750 RID: 34640 RVA: 0x001E9FCE File Offset: 0x001E81CE
			ConstantValue ValueSetFactory.INumericTC<char>.ToConstantValue(char value)
			{
				return ConstantValue.Create(value);
			}
		}

		// Token: 0x02000768 RID: 1896
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct DecimalTC : ValueSetFactory.INumericTC<decimal>
		{
			// Token: 0x170026F8 RID: 9976
			// (get) Token: 0x06008751 RID: 34641 RVA: 0x001E9FD6 File Offset: 0x001E81D6
			decimal ValueSetFactory.INumericTC<decimal>.MinValue
			{
				get
				{
					return -79228162514264337593543950335m;
				}
			}

			// Token: 0x170026F9 RID: 9977
			// (get) Token: 0x06008752 RID: 34642 RVA: 0x001E9FE2 File Offset: 0x001E81E2
			decimal ValueSetFactory.INumericTC<decimal>.MaxValue
			{
				get
				{
					return 79228162514264337593543950335m;
				}
			}

			// Token: 0x170026FA RID: 9978
			// (get) Token: 0x06008753 RID: 34643 RVA: 0x001E9FEE File Offset: 0x001E81EE
			decimal ValueSetFactory.INumericTC<decimal>.Zero
			{
				get
				{
					return 0m;
				}
			}

			// Token: 0x06008754 RID: 34644 RVA: 0x001E9FF5 File Offset: 0x001E81F5
			public decimal FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.DecimalValue;
				}
				return 0m;
			}

			// Token: 0x06008755 RID: 34645 RVA: 0x001EA00B File Offset: 0x001E820B
			public ConstantValue ToConstantValue(decimal value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x06008756 RID: 34646 RVA: 0x001EA014 File Offset: 0x001E8214
			public decimal Next(decimal value)
			{
				if (value == 0m)
				{
					return ValueSetFactory.DecimalTC.epsilon;
				}
				uint num;
				uint num2;
				uint num3;
				bool flag;
				byte b;
				ValueSetFactory.DecimalTC.DecimalRep.FromValue(value).Deconstruct(out num, out num2, out num3, out flag, out b);
				uint num4 = num;
				uint num5 = num2;
				uint num6 = num3;
				bool flag2 = flag;
				byte b2 = b;
				if (flag2)
				{
					if (value == -ValueSetFactory.DecimalTC.epsilon)
					{
						return ValueSetFactory.DecimalTC.normalZero;
					}
					if (num4 != 0U)
					{
						return new ValueSetFactory.DecimalTC.DecimalRep(num4 - 1U, num5, num6, flag2, b2).Value;
					}
					if (num5 != 0U)
					{
						return new ValueSetFactory.DecimalTC.DecimalRep(uint.MaxValue, num5 - 1U, num6, flag2, b2).Value;
					}
					return new ValueSetFactory.DecimalTC.DecimalRep(uint.MaxValue, uint.MaxValue, num6 - 1U, flag2, b2).Value;
				}
				else
				{
					if (num4 != 4294967295U)
					{
						return new ValueSetFactory.DecimalTC.DecimalRep(num4 + 1U, num5, num6, flag2, b2).Value;
					}
					if (num5 != 4294967295U)
					{
						return new ValueSetFactory.DecimalTC.DecimalRep(0U, num5 + 1U, num6, flag2, b2).Value;
					}
					if (num6 != 4294967295U)
					{
						return new ValueSetFactory.DecimalTC.DecimalRep(0U, 0U, num6 + 1U, flag2, b2).Value;
					}
					num4 = 2576980377U;
					num5 = 2576980377U;
					num6 = 429496729U;
					b2 -= 1;
					return new ValueSetFactory.DecimalTC.DecimalRep(num4 + 1U, num5, num6, flag2, b2).Value;
				}
			}

			// Token: 0x06008757 RID: 34647 RVA: 0x001EA144 File Offset: 0x001E8344
			bool ValueSetFactory.INumericTC<decimal>.Related(BinaryOperatorKind relation, decimal left, decimal right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x06008758 RID: 34648 RVA: 0x001EA1B7 File Offset: 0x001E83B7
			string ValueSetFactory.INumericTC<decimal>.ToString(decimal value)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0:G}", new object[] { value }));
			}

			// Token: 0x06008759 RID: 34649 RVA: 0x001EA1D7 File Offset: 0x001E83D7
			decimal ValueSetFactory.INumericTC<decimal>.Prev(decimal value)
			{
				return -this.Next(-value);
			}

			// Token: 0x0600875A RID: 34650 RVA: 0x001EA1EC File Offset: 0x001E83EC
			public decimal Random(Random random)
			{
				ValueSetFactory.INumericTC<uint> numericTC = default(ValueSetFactory.UIntTC);
				return new ValueSetFactory.DecimalTC.DecimalRep(numericTC.Random(random), numericTC.Random(random), numericTC.Random(random), random.NextDouble() < 0.5, (byte)random.Next(0, 29)).Normalize().Value;
			}

			// Token: 0x0600875B RID: 34651 RVA: 0x001EA250 File Offset: 0x001E8450
			public static decimal Normalize(decimal value)
			{
				return ValueSetFactory.DecimalTC.DecimalRep.FromValue(value).Normalize().Value;
			}

			// Token: 0x04002390 RID: 9104
			private const uint transitionLow = 2576980377U;

			// Token: 0x04002391 RID: 9105
			private const uint transitionMid = 2576980377U;

			// Token: 0x04002392 RID: 9106
			private const uint transitionHigh = 429496729U;

			// Token: 0x04002393 RID: 9107
			private const byte maxScale = 28;

			// Token: 0x04002394 RID: 9108
			private static readonly decimal normalZero = 0.0000000000000000000000000000m;

			// Token: 0x04002395 RID: 9109
			private static readonly decimal epsilon = 0.0000000000000000000000000001m;

			// Token: 0x020009ED RID: 2541
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			private struct DecimalRep
			{
				// Token: 0x060093A3 RID: 37795 RVA: 0x002032E0 File Offset: 0x002014E0
				public DecimalRep(uint low, uint mid, uint high, bool isNegative, byte scale)
				{
					if (scale > 28)
					{
						throw new ArgumentException("scale");
					}
					this.low = low;
					this.mid = mid;
					this.high = high;
					this.isNegative = isNegative;
					this.scale = scale;
				}

				// Token: 0x1700293E RID: 10558
				// (get) Token: 0x060093A4 RID: 37796 RVA: 0x00203318 File Offset: 0x00201518
				public decimal Value
				{
					get
					{
						return new decimal((int)this.low, (int)this.mid, (int)this.high, this.isNegative, this.scale);
					}
				}

				// Token: 0x060093A5 RID: 37797 RVA: 0x00203340 File Offset: 0x00201540
				public ValueSetFactory.DecimalTC.DecimalRep Normalize()
				{
					if (this.scale == 28)
					{
						return this;
					}
					ValueSetFactory.DecimalTC.DecimalRep decimalRep = this;
					uint num;
					uint num2;
					uint num3;
					bool flag;
					byte b;
					decimalRep.Deconstruct(out num, out num2, out num3, out flag, out b);
					uint num4 = num;
					uint num5 = num2;
					uint num6 = num3;
					bool flag2 = flag;
					byte b2 = b;
					while (b2 < 28 && (ulong)num6 * 10UL <= (ulong)(-1))
					{
						long num7 = (long)(10UL * (ulong)num6);
						long num8 = (long)(10UL * (ulong)num5);
						long num9 = (long)(10UL * (ulong)num4);
						num8 += num9 >> 32;
						num9 &= (long)((ulong)(-1));
						num7 += num8 >> 32;
						num8 &= (long)((ulong)(-1));
						if (num7 > (long)((ulong)(-1)))
						{
							break;
						}
						num4 = (uint)num9;
						num5 = (uint)num8;
						num6 = (uint)num7;
						b2 += 1;
					}
					return new ValueSetFactory.DecimalTC.DecimalRep(num4, num5, num6, flag2, b2);
				}

				// Token: 0x060093A6 RID: 37798 RVA: 0x002033F8 File Offset: 0x002015F8
				public static ValueSetFactory.DecimalTC.DecimalRep FromValue(decimal value)
				{
					bool flag;
					byte b;
					uint num;
					uint num2;
					uint num3;
					value.GetBits(out flag, out b, out num, out num2, out num3);
					return new ValueSetFactory.DecimalTC.DecimalRep(num, num2, num3, flag, b);
				}

				// Token: 0x060093A7 RID: 37799 RVA: 0x00203420 File Offset: 0x00201620
				public void Deconstruct(out uint low, out uint mid, out uint high, out bool isNegative, out byte scale)
				{
					uint num = this.low;
					uint num2 = this.mid;
					uint num3 = this.high;
					bool flag = this.isNegative;
					byte b = this.scale;
					low = num;
					mid = num2;
					high = num3;
					isNegative = flag;
					scale = b;
				}

				// Token: 0x060093A8 RID: 37800 RVA: 0x00203464 File Offset: 0x00201664
				[global::System.Runtime.CompilerServices.NullableContext(1)]
				public override string ToString()
				{
					return string.Format("Decimal({0}, 0x{1:08X} 0x{2:08X} 0x{3:08X} *10^-{4})", new object[]
					{
						this.isNegative ? "-" : "+",
						this.high,
						this.mid,
						this.low,
						this.scale
					});
				}

				// Token: 0x04002A22 RID: 10786
				public readonly uint low;

				// Token: 0x04002A23 RID: 10787
				public readonly uint mid;

				// Token: 0x04002A24 RID: 10788
				public readonly uint high;

				// Token: 0x04002A25 RID: 10789
				public readonly bool isNegative;

				// Token: 0x04002A26 RID: 10790
				public readonly byte scale;
			}
		}

		// Token: 0x02000769 RID: 1897
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class DecimalValueSetFactory : IValueSetFactory<decimal>, IValueSetFactory
		{
			// Token: 0x170026FB RID: 9979
			// (get) Token: 0x0600875D RID: 34653 RVA: 0x001EA295 File Offset: 0x001E8495
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.NumericValueSet<decimal, ValueSetFactory.DecimalTC>.AllValues;
				}
			}

			// Token: 0x170026FC RID: 9980
			// (get) Token: 0x0600875E RID: 34654 RVA: 0x001EA29C File Offset: 0x001E849C
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.NumericValueSet<decimal, ValueSetFactory.DecimalTC>.NoValues;
				}
			}

			// Token: 0x0600875F RID: 34655 RVA: 0x001EA2A3 File Offset: 0x001E84A3
			public IValueSet<decimal> Related(BinaryOperatorKind relation, decimal value)
			{
				return this._underlying.Related(relation, ValueSetFactory.DecimalTC.Normalize(value));
			}

			// Token: 0x06008760 RID: 34656 RVA: 0x001EA2B7 File Offset: 0x001E84B7
			IValueSet IValueSetFactory.Random(int expectedSize, Random random)
			{
				return this._underlying.Random(expectedSize, random);
			}

			// Token: 0x06008761 RID: 34657 RVA: 0x001EA2C8 File Offset: 0x001E84C8
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				return ConstantValue.Create(default(ValueSetFactory.DecimalTC).Random(random));
			}

			// Token: 0x06008762 RID: 34658 RVA: 0x001EA2EC File Offset: 0x001E84EC
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					return this.Related(relation, default(ValueSetFactory.DecimalTC).FromConstantValue(value));
				}
				return ValueSetFactory.NumericValueSet<decimal, ValueSetFactory.DecimalTC>.AllValues;
			}

			// Token: 0x06008763 RID: 34659 RVA: 0x001EA31F File Offset: 0x001E851F
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				return this._underlying.Related(relation, left, right);
			}

			// Token: 0x04002396 RID: 9110
			public static readonly ValueSetFactory.DecimalValueSetFactory Instance = new ValueSetFactory.DecimalValueSetFactory();

			// Token: 0x04002397 RID: 9111
			private readonly IValueSetFactory<decimal> _underlying = ValueSetFactory.NumericValueSetFactory<decimal, ValueSetFactory.DecimalTC>.Instance;
		}

		// Token: 0x0200076A RID: 1898
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct DoubleTC : ValueSetFactory.FloatingTC<double>, ValueSetFactory.INumericTC<double>
		{
			// Token: 0x170026FD RID: 9981
			// (get) Token: 0x06008766 RID: 34662 RVA: 0x001EA34E File Offset: 0x001E854E
			double ValueSetFactory.INumericTC<double>.MinValue
			{
				get
				{
					return double.NegativeInfinity;
				}
			}

			// Token: 0x170026FE RID: 9982
			// (get) Token: 0x06008767 RID: 34663 RVA: 0x001EA359 File Offset: 0x001E8559
			double ValueSetFactory.INumericTC<double>.MaxValue
			{
				get
				{
					return double.PositiveInfinity;
				}
			}

			// Token: 0x170026FF RID: 9983
			// (get) Token: 0x06008768 RID: 34664 RVA: 0x001EA364 File Offset: 0x001E8564
			double ValueSetFactory.FloatingTC<double>.NaN
			{
				get
				{
					return double.NaN;
				}
			}

			// Token: 0x17002700 RID: 9984
			// (get) Token: 0x06008769 RID: 34665 RVA: 0x001EA36F File Offset: 0x001E856F
			double ValueSetFactory.INumericTC<double>.Zero
			{
				get
				{
					return 0.0;
				}
			}

			// Token: 0x0600876A RID: 34666 RVA: 0x001EA37C File Offset: 0x001E857C
			public double Next(double value)
			{
				if (value == 0.0)
				{
					return double.Epsilon;
				}
				if (value < 0.0)
				{
					if (value == -5E-324)
					{
						return 0.0;
					}
					if (value == double.NegativeInfinity)
					{
						return double.MinValue;
					}
					return -ValueSetFactory.DoubleTC.ULongAsDouble(ValueSetFactory.DoubleTC.DoubleAsULong(-value) - 1UL);
				}
				else
				{
					if (value == 1.7976931348623157E+308)
					{
						return double.PositiveInfinity;
					}
					return ValueSetFactory.DoubleTC.ULongAsDouble(ValueSetFactory.DoubleTC.DoubleAsULong(value) + 1UL);
				}
			}

			// Token: 0x0600876B RID: 34667 RVA: 0x001EA40C File Offset: 0x001E860C
			private static ulong DoubleAsULong(double d)
			{
				if (d == 0.0)
				{
					return 0UL;
				}
				return (ulong)BitConverter.DoubleToInt64Bits(d);
			}

			// Token: 0x0600876C RID: 34668 RVA: 0x001EA423 File Offset: 0x001E8623
			private static double ULongAsDouble(ulong l)
			{
				return BitConverter.Int64BitsToDouble((long)l);
			}

			// Token: 0x0600876D RID: 34669 RVA: 0x001EA42C File Offset: 0x001E862C
			bool ValueSetFactory.INumericTC<double>.Related(BinaryOperatorKind relation, double left, double right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right || (double.IsNaN(left) && double.IsNaN(right));
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x0600876E RID: 34670 RVA: 0x001EA4A8 File Offset: 0x001E86A8
			double ValueSetFactory.INumericTC<double>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.DoubleValue;
				}
				return 0.0;
			}

			// Token: 0x0600876F RID: 34671 RVA: 0x001EA4C2 File Offset: 0x001E86C2
			ConstantValue ValueSetFactory.INumericTC<double>.ToConstantValue(double value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x06008770 RID: 34672 RVA: 0x001EA4CC File Offset: 0x001E86CC
			string ValueSetFactory.INumericTC<double>.ToString(double value)
			{
				if (double.IsNaN(value))
				{
					return "NaN";
				}
				if (value == double.NegativeInfinity)
				{
					return "-Inf";
				}
				if (value != double.PositiveInfinity)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0:G17}", new object[] { value }));
				}
				return "Inf";
			}

			// Token: 0x06008771 RID: 34673 RVA: 0x001EA529 File Offset: 0x001E8729
			double ValueSetFactory.INumericTC<double>.Prev(double value)
			{
				return -this.Next(-value);
			}

			// Token: 0x06008772 RID: 34674 RVA: 0x001EA534 File Offset: 0x001E8734
			double ValueSetFactory.INumericTC<double>.Random(Random random)
			{
				return random.NextDouble() * 100.0 - 50.0;
			}
		}

		// Token: 0x0200076B RID: 1899
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class EnumeratedValueSet<T, [global::System.Runtime.CompilerServices.Nullable(0)] TTC> : IValueSet<T>, IValueSet where TTC : struct, ValueSetFactory.IEquatableValueTC<T>
		{
			// Token: 0x06008773 RID: 34675 RVA: 0x001EA550 File Offset: 0x001E8750
			private EnumeratedValueSet(bool included, ImmutableHashSet<T> membersIncludedOrExcluded)
			{
				this._included = included;
				this._membersIncludedOrExcluded = membersIncludedOrExcluded;
			}

			// Token: 0x06008774 RID: 34676 RVA: 0x001EA575 File Offset: 0x001E8775
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			internal static ValueSetFactory.EnumeratedValueSet<T, TTC> Including(T value)
			{
				return new ValueSetFactory.EnumeratedValueSet<T, TTC>(true, ImmutableHashSet<T>.Empty.Add(value));
			}

			// Token: 0x17002701 RID: 9985
			// (get) Token: 0x06008775 RID: 34677 RVA: 0x001EA588 File Offset: 0x001E8788
			public bool IsEmpty
			{
				get
				{
					return this._included && this._membersIncludedOrExcluded.IsEmpty;
				}
			}

			// Token: 0x17002702 RID: 9986
			// (get) Token: 0x06008776 RID: 34678 RVA: 0x001EA5A0 File Offset: 0x001E87A0
			ConstantValue IValueSet.Sample
			{
				get
				{
					if (this.IsEmpty)
					{
						throw new ArgumentException();
					}
					TTC ttc = default(TTC);
					if (this._included)
					{
						return ttc.ToConstantValue(Enumerable.First<T>(Enumerable.OrderBy<T, T>(this._membersIncludedOrExcluded, (T k) => k)));
					}
					if (typeof(T) == typeof(string))
					{
						if (this.Any(BinaryOperatorKind.Equal, (T)((object)"")))
						{
							return ttc.ToConstantValue((T)((object)""));
						}
						for (char c = 'A'; c <= 'z'; c += '\u0001')
						{
							if (this.Any(BinaryOperatorKind.Equal, (T)((object)c.ToString())))
							{
								return ttc.ToConstantValue((T)((object)c.ToString()));
							}
						}
					}
					foreach (T t in ttc.RandomValues(this._membersIncludedOrExcluded.Count + 1, new Random(0), this._membersIncludedOrExcluded.Count + 1))
					{
						if (this.Any(BinaryOperatorKind.Equal, t))
						{
							return ttc.ToConstantValue(t);
						}
					}
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x06008777 RID: 34679 RVA: 0x001EA6FD File Offset: 0x001E88FD
			public bool Any(BinaryOperatorKind relation, T value)
			{
				return relation != BinaryOperatorKind.Equal || this._included == this._membersIncludedOrExcluded.Contains(value);
			}

			// Token: 0x06008778 RID: 34680 RVA: 0x001EA720 File Offset: 0x001E8920
			bool IValueSet.Any(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TTC ttc = default(TTC);
					return this.Any(relation, ttc.FromConstantValue(value));
				}
				return true;
			}

			// Token: 0x06008779 RID: 34681 RVA: 0x001EA754 File Offset: 0x001E8954
			public bool All(BinaryOperatorKind relation, T value)
			{
				if (relation != BinaryOperatorKind.Equal)
				{
					return false;
				}
				if (!this._included)
				{
					return false;
				}
				int count = this._membersIncludedOrExcluded.Count;
				return count == 0 || (count == 1 && this._membersIncludedOrExcluded.Contains(value));
			}

			// Token: 0x0600877A RID: 34682 RVA: 0x001EA79C File Offset: 0x001E899C
			bool IValueSet.All(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TTC ttc = default(TTC);
					return this.All(relation, ttc.FromConstantValue(value));
				}
				return false;
			}

			// Token: 0x0600877B RID: 34683 RVA: 0x001EA7CF File Offset: 0x001E89CF
			public IValueSet<T> Complement()
			{
				return new ValueSetFactory.EnumeratedValueSet<T, TTC>(!this._included, this._membersIncludedOrExcluded);
			}

			// Token: 0x0600877C RID: 34684 RVA: 0x001EA7E5 File Offset: 0x001E89E5
			IValueSet IValueSet.Complement()
			{
				return this.Complement();
			}

			// Token: 0x0600877D RID: 34685 RVA: 0x001EA7F0 File Offset: 0x001E89F0
			public IValueSet<T> Intersect(IValueSet<T> o)
			{
				if (this == o)
				{
					return this;
				}
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet = (ValueSetFactory.EnumeratedValueSet<T, TTC>)o;
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet2;
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet3;
				if (this._membersIncludedOrExcluded.Count <= enumeratedValueSet._membersIncludedOrExcluded.Count)
				{
					enumeratedValueSet2 = enumeratedValueSet;
					enumeratedValueSet3 = this;
				}
				else
				{
					ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet4 = enumeratedValueSet;
					enumeratedValueSet2 = this;
					enumeratedValueSet3 = enumeratedValueSet4;
				}
				bool included = enumeratedValueSet2._included;
				bool included2 = enumeratedValueSet3._included;
				if (included)
				{
					if (!included2)
					{
						return new ValueSetFactory.EnumeratedValueSet<T, TTC>(true, enumeratedValueSet2._membersIncludedOrExcluded.Except(enumeratedValueSet3._membersIncludedOrExcluded));
					}
					return new ValueSetFactory.EnumeratedValueSet<T, TTC>(true, enumeratedValueSet2._membersIncludedOrExcluded.Intersect(enumeratedValueSet3._membersIncludedOrExcluded));
				}
				else
				{
					if (included2)
					{
						return new ValueSetFactory.EnumeratedValueSet<T, TTC>(true, enumeratedValueSet3._membersIncludedOrExcluded.Except(enumeratedValueSet2._membersIncludedOrExcluded));
					}
					return new ValueSetFactory.EnumeratedValueSet<T, TTC>(false, enumeratedValueSet2._membersIncludedOrExcluded.Union(enumeratedValueSet3._membersIncludedOrExcluded));
				}
			}

			// Token: 0x0600877E RID: 34686 RVA: 0x001EA8AB File Offset: 0x001E8AAB
			IValueSet IValueSet.Intersect(IValueSet other)
			{
				return this.Intersect((IValueSet<T>)other);
			}

			// Token: 0x0600877F RID: 34687 RVA: 0x001EA8BC File Offset: 0x001E8ABC
			public IValueSet<T> Union(IValueSet<T> o)
			{
				if (this == o)
				{
					return this;
				}
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet = (ValueSetFactory.EnumeratedValueSet<T, TTC>)o;
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet2;
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet3;
				if (this._membersIncludedOrExcluded.Count <= enumeratedValueSet._membersIncludedOrExcluded.Count)
				{
					enumeratedValueSet2 = enumeratedValueSet;
					enumeratedValueSet3 = this;
				}
				else
				{
					ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet4 = enumeratedValueSet;
					enumeratedValueSet2 = this;
					enumeratedValueSet3 = enumeratedValueSet4;
				}
				bool included = enumeratedValueSet2._included;
				bool included2 = enumeratedValueSet3._included;
				if (!included)
				{
					if (included2)
					{
						return new ValueSetFactory.EnumeratedValueSet<T, TTC>(false, enumeratedValueSet2._membersIncludedOrExcluded.Except(enumeratedValueSet3._membersIncludedOrExcluded));
					}
					return new ValueSetFactory.EnumeratedValueSet<T, TTC>(false, enumeratedValueSet2._membersIncludedOrExcluded.Intersect(enumeratedValueSet3._membersIncludedOrExcluded));
				}
				else
				{
					if (!included2)
					{
						return new ValueSetFactory.EnumeratedValueSet<T, TTC>(false, enumeratedValueSet3._membersIncludedOrExcluded.Except(enumeratedValueSet2._membersIncludedOrExcluded));
					}
					return new ValueSetFactory.EnumeratedValueSet<T, TTC>(true, enumeratedValueSet2._membersIncludedOrExcluded.Union(enumeratedValueSet3._membersIncludedOrExcluded));
				}
			}

			// Token: 0x06008780 RID: 34688 RVA: 0x001EA977 File Offset: 0x001E8B77
			IValueSet IValueSet.Union(IValueSet other)
			{
				return this.Union((IValueSet<T>)other);
			}

			// Token: 0x06008781 RID: 34689 RVA: 0x001EA988 File Offset: 0x001E8B88
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				ValueSetFactory.EnumeratedValueSet<T, TTC> enumeratedValueSet = obj as ValueSetFactory.EnumeratedValueSet<T, TTC>;
				return enumeratedValueSet != null && this._included == enumeratedValueSet._included && this._membersIncludedOrExcluded.SetEquals(enumeratedValueSet._membersIncludedOrExcluded);
			}

			// Token: 0x06008782 RID: 34690 RVA: 0x001EA9C0 File Offset: 0x001E8BC0
			public override int GetHashCode()
			{
				return Hash.Combine(this._included.GetHashCode(), this._membersIncludedOrExcluded.GetHashCode());
			}

			// Token: 0x06008783 RID: 34691 RVA: 0x001EA9EC File Offset: 0x001E8BEC
			public override string ToString()
			{
				return (this._included ? "" : "~") + "{" + string.Join(",", Enumerable.Select<T, string>(this._membersIncludedOrExcluded, (T o) => o.ToString())) + "}";
			}

			// Token: 0x04002398 RID: 9112
			private readonly bool _included;

			// Token: 0x04002399 RID: 9113
			private readonly ImmutableHashSet<T> _membersIncludedOrExcluded;

			// Token: 0x0400239A RID: 9114
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.EnumeratedValueSet<T, TTC> AllValues = new ValueSetFactory.EnumeratedValueSet<T, TTC>(false, ImmutableHashSet<T>.Empty);

			// Token: 0x0400239B RID: 9115
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.EnumeratedValueSet<T, TTC> NoValues = new ValueSetFactory.EnumeratedValueSet<T, TTC>(true, ImmutableHashSet<T>.Empty);
		}

		// Token: 0x0200076C RID: 1900
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class EnumeratedValueSetFactory<T, [global::System.Runtime.CompilerServices.Nullable(0)] TTC> : IValueSetFactory<T>, IValueSetFactory where TTC : struct, ValueSetFactory.IEquatableValueTC<T>
		{
			// Token: 0x17002703 RID: 9987
			// (get) Token: 0x06008785 RID: 34693 RVA: 0x001EAA72 File Offset: 0x001E8C72
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.EnumeratedValueSet<T, TTC>.AllValues;
				}
			}

			// Token: 0x17002704 RID: 9988
			// (get) Token: 0x06008786 RID: 34694 RVA: 0x001EAA79 File Offset: 0x001E8C79
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.EnumeratedValueSet<T, TTC>.NoValues;
				}
			}

			// Token: 0x06008787 RID: 34695 RVA: 0x001EAA80 File Offset: 0x001E8C80
			private EnumeratedValueSetFactory()
			{
			}

			// Token: 0x06008788 RID: 34696 RVA: 0x001EAA88 File Offset: 0x001E8C88
			public IValueSet<T> Related(BinaryOperatorKind relation, T value)
			{
				if (relation == BinaryOperatorKind.Equal)
				{
					return ValueSetFactory.EnumeratedValueSet<T, TTC>.Including(value);
				}
				return ValueSetFactory.EnumeratedValueSet<T, TTC>.AllValues;
			}

			// Token: 0x06008789 RID: 34697 RVA: 0x001EAAA0 File Offset: 0x001E8CA0
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad && !value.IsNull)
				{
					TTC ttc = default(TTC);
					return this.Related(relation, ttc.FromConstantValue(value));
				}
				return ValueSetFactory.EnumeratedValueSet<T, TTC>.AllValues;
			}

			// Token: 0x0600878A RID: 34698 RVA: 0x001EAAE4 File Offset: 0x001E8CE4
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				TTC ttc = default(TTC);
				T t = ttc.FromConstantValue(left);
				return t.Equals(ttc.FromConstantValue(right));
			}

			// Token: 0x0600878B RID: 34699 RVA: 0x001EAB28 File Offset: 0x001E8D28
			public IValueSet Random(int expectedSize, Random random)
			{
				TTC ttc = default(TTC);
				T[] array = ttc.RandomValues(expectedSize, random, expectedSize * 2);
				IValueSet<T> valueSet = ValueSetFactory.EnumeratedValueSet<T, TTC>.NoValues;
				foreach (T t in array)
				{
					valueSet = valueSet.Union(this.Related(BinaryOperatorKind.Equal, t));
				}
				return valueSet;
			}

			// Token: 0x0600878C RID: 34700 RVA: 0x001EAB84 File Offset: 0x001E8D84
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				TTC ttc = default(TTC);
				return ttc.ToConstantValue(ttc.RandomValues(1, random, 100)[0]);
			}

			// Token: 0x0400239C RID: 9116
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.EnumeratedValueSetFactory<T, TTC> Instance = new ValueSetFactory.EnumeratedValueSetFactory<T, TTC>();
		}

		// Token: 0x0200076D RID: 1901
		private interface FloatingTC<[global::System.Runtime.CompilerServices.Nullable(2)] T> : ValueSetFactory.INumericTC<T>
		{
			// Token: 0x17002705 RID: 9989
			// (get) Token: 0x0600878E RID: 34702
			T NaN { get; }
		}

		// Token: 0x0200076E RID: 1902
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class FloatingValueSet<[global::System.Runtime.CompilerServices.Nullable(2)] TFloating, [global::System.Runtime.CompilerServices.Nullable(0)] TFloatingTC> : IValueSet<TFloating>, IValueSet where TFloatingTC : struct, ValueSetFactory.FloatingTC<TFloating>
		{
			// Token: 0x0600878F RID: 34703 RVA: 0x001EABCC File Offset: 0x001E8DCC
			private FloatingValueSet(IValueSet<TFloating> numbers, bool hasNaN)
			{
				this._numbers = numbers;
				this._hasNaN = hasNaN;
			}

			// Token: 0x06008790 RID: 34704 RVA: 0x001EABF4 File Offset: 0x001E8DF4
			internal static IValueSet<TFloating> Random(int expectedSize, Random random)
			{
				bool flag = random.NextDouble() < 0.5;
				if (flag)
				{
					expectedSize--;
				}
				if (expectedSize < 1)
				{
					expectedSize = 2;
				}
				return new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>((IValueSet<TFloating>)ValueSetFactory.NumericValueSetFactory<TFloating, TFloatingTC>.Instance.Random(expectedSize, random), flag);
			}

			// Token: 0x17002706 RID: 9990
			// (get) Token: 0x06008791 RID: 34705 RVA: 0x001EAC39 File Offset: 0x001E8E39
			public bool IsEmpty
			{
				get
				{
					return !this._hasNaN && this._numbers.IsEmpty;
				}
			}

			// Token: 0x17002707 RID: 9991
			// (get) Token: 0x06008792 RID: 34706 RVA: 0x001EAC50 File Offset: 0x001E8E50
			ConstantValue IValueSet.Sample
			{
				get
				{
					if (this.IsEmpty)
					{
						throw new ArgumentException();
					}
					if (!this._numbers.IsEmpty)
					{
						return this._numbers.Sample;
					}
					TFloatingTC tfloatingTC = default(TFloatingTC);
					return tfloatingTC.ToConstantValue(tfloatingTC.NaN);
				}
			}

			// Token: 0x06008793 RID: 34707 RVA: 0x001EACA8 File Offset: 0x001E8EA8
			public static IValueSet<TFloating> Related(BinaryOperatorKind relation, TFloating value)
			{
				TFloatingTC tfloatingTC = default(TFloatingTC);
				if (tfloatingTC.Related(BinaryOperatorKind.Equal, tfloatingTC.NaN, value))
				{
					if (relation <= BinaryOperatorKind.GreaterThan)
					{
						if (relation != BinaryOperatorKind.Equal)
						{
							if (relation != BinaryOperatorKind.GreaterThan)
							{
								goto IL_006E;
							}
							goto IL_0068;
						}
					}
					else
					{
						if (relation == BinaryOperatorKind.LessThan)
						{
							goto IL_0068;
						}
						if (relation != BinaryOperatorKind.GreaterThanOrEqual && relation != BinaryOperatorKind.LessThanOrEqual)
						{
							goto IL_006E;
						}
					}
					return new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(ValueSetFactory.NumericValueSet<TFloating, TFloatingTC>.NoValues, true);
					IL_0068:
					return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.NoValues;
					IL_006E:
					throw ExceptionUtilities.UnexpectedValue(relation);
				}
				return new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(ValueSetFactory.NumericValueSetFactory<TFloating, TFloatingTC>.Instance.Related(relation, value), false);
			}

			// Token: 0x06008794 RID: 34708 RVA: 0x001EAD44 File Offset: 0x001E8F44
			public IValueSet<TFloating> Intersect(IValueSet<TFloating> o)
			{
				if (this == o)
				{
					return this;
				}
				ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC> floatingValueSet = (ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>)o;
				return new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(this._numbers.Intersect(floatingValueSet._numbers), this._hasNaN & floatingValueSet._hasNaN);
			}

			// Token: 0x06008795 RID: 34709 RVA: 0x001EAD81 File Offset: 0x001E8F81
			IValueSet IValueSet.Intersect(IValueSet other)
			{
				return this.Intersect((IValueSet<TFloating>)other);
			}

			// Token: 0x06008796 RID: 34710 RVA: 0x001EAD90 File Offset: 0x001E8F90
			public IValueSet<TFloating> Union(IValueSet<TFloating> o)
			{
				if (this == o)
				{
					return this;
				}
				ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC> floatingValueSet = (ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>)o;
				return new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(this._numbers.Union(floatingValueSet._numbers), this._hasNaN | floatingValueSet._hasNaN);
			}

			// Token: 0x06008797 RID: 34711 RVA: 0x001EADCD File Offset: 0x001E8FCD
			IValueSet IValueSet.Union(IValueSet other)
			{
				return this.Union((IValueSet<TFloating>)other);
			}

			// Token: 0x06008798 RID: 34712 RVA: 0x001EADDB File Offset: 0x001E8FDB
			public IValueSet<TFloating> Complement()
			{
				return new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(this._numbers.Complement(), !this._hasNaN);
			}

			// Token: 0x06008799 RID: 34713 RVA: 0x001EADF6 File Offset: 0x001E8FF6
			IValueSet IValueSet.Complement()
			{
				return this.Complement();
			}

			// Token: 0x0600879A RID: 34714 RVA: 0x001EAE00 File Offset: 0x001E9000
			bool IValueSet.Any(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TFloatingTC tfloatingTC = default(TFloatingTC);
					return this.Any(relation, tfloatingTC.FromConstantValue(value));
				}
				return true;
			}

			// Token: 0x0600879B RID: 34715 RVA: 0x001EAE34 File Offset: 0x001E9034
			public bool Any(BinaryOperatorKind relation, TFloating value)
			{
				TFloatingTC tfloatingTC = default(TFloatingTC);
				return (this._hasNaN && tfloatingTC.Related(relation, tfloatingTC.NaN, value)) || this._numbers.Any(relation, value);
			}

			// Token: 0x0600879C RID: 34716 RVA: 0x001EAE80 File Offset: 0x001E9080
			bool IValueSet.All(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TFloatingTC tfloatingTC = default(TFloatingTC);
					return this.All(relation, tfloatingTC.FromConstantValue(value));
				}
				return false;
			}

			// Token: 0x0600879D RID: 34717 RVA: 0x001EAEB4 File Offset: 0x001E90B4
			public bool All(BinaryOperatorKind relation, TFloating value)
			{
				TFloatingTC tfloatingTC = default(TFloatingTC);
				return (!this._hasNaN || tfloatingTC.Related(relation, tfloatingTC.NaN, value)) && this._numbers.All(relation, value);
			}

			// Token: 0x0600879E RID: 34718 RVA: 0x001EAEFE File Offset: 0x001E90FE
			public override int GetHashCode()
			{
				return this._numbers.GetHashCode();
			}

			// Token: 0x0600879F RID: 34719 RVA: 0x001EAF0C File Offset: 0x001E910C
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				if (this != obj)
				{
					ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC> floatingValueSet = obj as ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>;
					return floatingValueSet != null && this._hasNaN == floatingValueSet._hasNaN && this._numbers.Equals(floatingValueSet._numbers);
				}
				return true;
			}

			// Token: 0x060087A0 RID: 34720 RVA: 0x001EAF4C File Offset: 0x001E914C
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this._hasNaN)
				{
					stringBuilder.Append("NaN");
				}
				string text = this._numbers.ToString();
				if (stringBuilder.Length > 1 && text.Length > 1)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(text);
				return stringBuilder.ToString();
			}

			// Token: 0x0400239D RID: 9117
			private readonly IValueSet<TFloating> _numbers;

			// Token: 0x0400239E RID: 9118
			private readonly bool _hasNaN;

			// Token: 0x0400239F RID: 9119
			internal static readonly IValueSet<TFloating> AllValues = new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(ValueSetFactory.NumericValueSet<TFloating, TFloatingTC>.AllValues, true);

			// Token: 0x040023A0 RID: 9120
			internal static readonly IValueSet<TFloating> NoValues = new ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>(ValueSetFactory.NumericValueSet<TFloating, TFloatingTC>.NoValues, false);
		}

		// Token: 0x0200076F RID: 1903
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class FloatingValueSetFactory<[global::System.Runtime.CompilerServices.Nullable(2)] TFloating, [global::System.Runtime.CompilerServices.Nullable(0)] TFloatingTC> : IValueSetFactory<TFloating>, IValueSetFactory where TFloatingTC : struct, ValueSetFactory.FloatingTC<TFloating>
		{
			// Token: 0x060087A2 RID: 34722 RVA: 0x001EAFCD File Offset: 0x001E91CD
			private FloatingValueSetFactory()
			{
			}

			// Token: 0x17002708 RID: 9992
			// (get) Token: 0x060087A3 RID: 34723 RVA: 0x001EAFD5 File Offset: 0x001E91D5
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.AllValues;
				}
			}

			// Token: 0x17002709 RID: 9993
			// (get) Token: 0x060087A4 RID: 34724 RVA: 0x001EAFDC File Offset: 0x001E91DC
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.NoValues;
				}
			}

			// Token: 0x060087A5 RID: 34725 RVA: 0x001EAFE3 File Offset: 0x001E91E3
			public IValueSet<TFloating> Related(BinaryOperatorKind relation, TFloating value)
			{
				return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.Related(relation, value);
			}

			// Token: 0x060087A6 RID: 34726 RVA: 0x001EAFEC File Offset: 0x001E91EC
			IValueSet IValueSetFactory.Random(int expectedSize, Random random)
			{
				return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.Random(expectedSize, random);
			}

			// Token: 0x060087A7 RID: 34727 RVA: 0x001EAFF8 File Offset: 0x001E91F8
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				TFloatingTC tfloatingTC = default(TFloatingTC);
				return tfloatingTC.ToConstantValue(tfloatingTC.Random(random));
			}

			// Token: 0x060087A8 RID: 34728 RVA: 0x001EB028 File Offset: 0x001E9228
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TFloatingTC tfloatingTC = default(TFloatingTC);
					return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.Related(relation, tfloatingTC.FromConstantValue(value));
				}
				return ValueSetFactory.FloatingValueSet<TFloating, TFloatingTC>.AllValues;
			}

			// Token: 0x060087A9 RID: 34729 RVA: 0x001EB060 File Offset: 0x001E9260
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				TFloatingTC tfloatingTC = default(TFloatingTC);
				return tfloatingTC.Related(relation, tfloatingTC.FromConstantValue(left), tfloatingTC.FromConstantValue(right));
			}

			// Token: 0x040023A1 RID: 9121
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.FloatingValueSetFactory<TFloating, TFloatingTC> Instance = new ValueSetFactory.FloatingValueSetFactory<TFloating, TFloatingTC>();
		}

		// Token: 0x02000770 RID: 1904
		private interface IEquatableValueTC<T>
		{
			// Token: 0x060087AB RID: 34731
			T FromConstantValue(ConstantValue constantValue);

			// Token: 0x060087AC RID: 34732
			ConstantValue ToConstantValue(T value);

			// Token: 0x060087AD RID: 34733
			T[] RandomValues(int count, Random random, int scope = 0);
		}

		// Token: 0x02000771 RID: 1905
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct IntTC : ValueSetFactory.INumericTC<int>
		{
			// Token: 0x1700270A RID: 9994
			// (get) Token: 0x060087AE RID: 34734 RVA: 0x001EB0AB File Offset: 0x001E92AB
			int ValueSetFactory.INumericTC<int>.MinValue
			{
				get
				{
					return int.MinValue;
				}
			}

			// Token: 0x1700270B RID: 9995
			// (get) Token: 0x060087AF RID: 34735 RVA: 0x001EB0B2 File Offset: 0x001E92B2
			int ValueSetFactory.INumericTC<int>.MaxValue
			{
				get
				{
					return int.MaxValue;
				}
			}

			// Token: 0x1700270C RID: 9996
			// (get) Token: 0x060087B0 RID: 34736 RVA: 0x001EB0B9 File Offset: 0x001E92B9
			int ValueSetFactory.INumericTC<int>.Zero
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060087B1 RID: 34737 RVA: 0x001EB0BC File Offset: 0x001E92BC
			public bool Related(BinaryOperatorKind relation, int left, int right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x060087B2 RID: 34738 RVA: 0x001EB126 File Offset: 0x001E9326
			int ValueSetFactory.INumericTC<int>.Next(int value)
			{
				return value + 1;
			}

			// Token: 0x060087B3 RID: 34739 RVA: 0x001EB12B File Offset: 0x001E932B
			int ValueSetFactory.INumericTC<int>.Prev(int value)
			{
				return value - 1;
			}

			// Token: 0x060087B4 RID: 34740 RVA: 0x001EB130 File Offset: 0x001E9330
			public int FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.Int32Value;
				}
				return 0;
			}

			// Token: 0x060087B5 RID: 34741 RVA: 0x001EB142 File Offset: 0x001E9342
			public ConstantValue ToConstantValue(int value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x060087B6 RID: 34742 RVA: 0x001EB14A File Offset: 0x001E934A
			string ValueSetFactory.INumericTC<int>.ToString(int value)
			{
				return value.ToString();
			}

			// Token: 0x060087B7 RID: 34743 RVA: 0x001EB153 File Offset: 0x001E9353
			public int Random(Random random)
			{
				return (random.Next() << 10) ^ random.Next();
			}
		}

		// Token: 0x02000772 RID: 1906
		private interface INumericTC<[global::System.Runtime.CompilerServices.Nullable(2)] T>
		{
			// Token: 0x060087B8 RID: 34744
			T FromConstantValue(ConstantValue constantValue);

			// Token: 0x060087B9 RID: 34745
			ConstantValue ToConstantValue(T value);

			// Token: 0x060087BA RID: 34746
			bool Related(BinaryOperatorKind relation, T left, T right);

			// Token: 0x1700270D RID: 9997
			// (get) Token: 0x060087BB RID: 34747
			T MinValue { get; }

			// Token: 0x1700270E RID: 9998
			// (get) Token: 0x060087BC RID: 34748
			T MaxValue { get; }

			// Token: 0x060087BD RID: 34749
			T Next(T value);

			// Token: 0x060087BE RID: 34750
			T Prev(T value);

			// Token: 0x060087BF RID: 34751
			T Random(Random random);

			// Token: 0x1700270F RID: 9999
			// (get) Token: 0x060087C0 RID: 34752
			T Zero { get; }

			// Token: 0x060087C1 RID: 34753
			string ToString(T value);
		}

		// Token: 0x02000773 RID: 1907
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct LongTC : ValueSetFactory.INumericTC<long>
		{
			// Token: 0x17002710 RID: 10000
			// (get) Token: 0x060087C2 RID: 34754 RVA: 0x001EB165 File Offset: 0x001E9365
			long ValueSetFactory.INumericTC<long>.MinValue
			{
				get
				{
					return long.MinValue;
				}
			}

			// Token: 0x17002711 RID: 10001
			// (get) Token: 0x060087C3 RID: 34755 RVA: 0x001EB170 File Offset: 0x001E9370
			long ValueSetFactory.INumericTC<long>.MaxValue
			{
				get
				{
					return long.MaxValue;
				}
			}

			// Token: 0x17002712 RID: 10002
			// (get) Token: 0x060087C4 RID: 34756 RVA: 0x001EB17B File Offset: 0x001E937B
			long ValueSetFactory.INumericTC<long>.Zero
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x060087C5 RID: 34757 RVA: 0x001EB180 File Offset: 0x001E9380
			bool ValueSetFactory.INumericTC<long>.Related(BinaryOperatorKind relation, long left, long right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x060087C6 RID: 34758 RVA: 0x001EB1EA File Offset: 0x001E93EA
			long ValueSetFactory.INumericTC<long>.Next(long value)
			{
				return value + 1L;
			}

			// Token: 0x060087C7 RID: 34759 RVA: 0x001EB1F0 File Offset: 0x001E93F0
			long ValueSetFactory.INumericTC<long>.Prev(long value)
			{
				return value - 1L;
			}

			// Token: 0x060087C8 RID: 34760 RVA: 0x001EB1F6 File Offset: 0x001E93F6
			long ValueSetFactory.INumericTC<long>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.Int64Value;
				}
				return 0L;
			}

			// Token: 0x060087C9 RID: 34761 RVA: 0x001EB209 File Offset: 0x001E9409
			ConstantValue ValueSetFactory.INumericTC<long>.ToConstantValue(long value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x060087CA RID: 34762 RVA: 0x001EB211 File Offset: 0x001E9411
			string ValueSetFactory.INumericTC<long>.ToString(long value)
			{
				return value.ToString();
			}

			// Token: 0x060087CB RID: 34763 RVA: 0x001EB21A File Offset: 0x001E941A
			long ValueSetFactory.INumericTC<long>.Random(Random random)
			{
				return ((long)random.Next() << 35) ^ ((long)random.Next() << 10) ^ (long)random.Next();
			}
		}

		// Token: 0x02000774 RID: 1908
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NintValueSet : IValueSet<int>, IValueSet
		{
			// Token: 0x060087CC RID: 34764 RVA: 0x001EB239 File Offset: 0x001E9439
			internal NintValueSet(bool hasSmall, IValueSet<int> values, bool hasLarge)
			{
				this._hasSmall = hasSmall;
				this._values = values;
				this._hasLarge = hasLarge;
			}

			// Token: 0x17002713 RID: 10003
			// (get) Token: 0x060087CD RID: 34765 RVA: 0x001EB256 File Offset: 0x001E9456
			public bool IsEmpty
			{
				get
				{
					return !this._hasSmall && !this._hasLarge && this._values.IsEmpty;
				}
			}

			// Token: 0x17002714 RID: 10004
			// (get) Token: 0x060087CE RID: 34766 RVA: 0x001EB275 File Offset: 0x001E9475
			[global::System.Runtime.CompilerServices.Nullable(2)]
			ConstantValue IValueSet.Sample
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						throw new ArgumentException();
					}
					if (!this._values.IsEmpty)
					{
						return this._values.Sample;
					}
					return null;
				}
			}

			// Token: 0x060087CF RID: 34767 RVA: 0x001EB2A0 File Offset: 0x001E94A0
			public bool All(BinaryOperatorKind relation, int value)
			{
				bool flag = this._hasLarge;
				if (flag)
				{
					bool flag2 = relation == BinaryOperatorKind.LessThan || relation == BinaryOperatorKind.LessThanOrEqual;
					flag = flag2;
				}
				if (flag)
				{
					return false;
				}
				flag = this._hasSmall;
				if (flag)
				{
					bool flag2 = relation == BinaryOperatorKind.GreaterThan || relation == BinaryOperatorKind.GreaterThanOrEqual;
					flag = flag2;
				}
				return !flag && this._values.All(relation, value);
			}

			// Token: 0x060087D0 RID: 34768 RVA: 0x001EB314 File Offset: 0x001E9514
			bool IValueSet.All(BinaryOperatorKind relation, ConstantValue value)
			{
				return value.IsBad || this.All(relation, value.Int32Value);
			}

			// Token: 0x060087D1 RID: 34769 RVA: 0x001EB330 File Offset: 0x001E9530
			public bool Any(BinaryOperatorKind relation, int value)
			{
				bool flag = this._hasSmall;
				if (flag)
				{
					bool flag2 = relation == BinaryOperatorKind.LessThan || relation == BinaryOperatorKind.LessThanOrEqual;
					flag = flag2;
				}
				if (flag)
				{
					return true;
				}
				flag = this._hasLarge;
				if (flag)
				{
					bool flag2 = relation == BinaryOperatorKind.GreaterThan || relation == BinaryOperatorKind.GreaterThanOrEqual;
					flag = flag2;
				}
				return flag || this._values.Any(relation, value);
			}

			// Token: 0x060087D2 RID: 34770 RVA: 0x001EB3A4 File Offset: 0x001E95A4
			bool IValueSet.Any(BinaryOperatorKind relation, ConstantValue value)
			{
				return value.IsBad || this.Any(relation, value.Int32Value);
			}

			// Token: 0x060087D3 RID: 34771 RVA: 0x001EB3BD File Offset: 0x001E95BD
			public IValueSet<int> Complement()
			{
				return new ValueSetFactory.NintValueSet(!this._hasSmall, this._values.Complement(), !this._hasLarge);
			}

			// Token: 0x060087D4 RID: 34772 RVA: 0x001EB3E1 File Offset: 0x001E95E1
			IValueSet IValueSet.Complement()
			{
				return this.Complement();
			}

			// Token: 0x060087D5 RID: 34773 RVA: 0x001EB3EC File Offset: 0x001E95EC
			public IValueSet<int> Intersect(IValueSet<int> o)
			{
				ValueSetFactory.NintValueSet nintValueSet = (ValueSetFactory.NintValueSet)o;
				return new ValueSetFactory.NintValueSet(this._hasSmall && nintValueSet._hasSmall, this._values.Intersect(nintValueSet._values), this._hasLarge && nintValueSet._hasLarge);
			}

			// Token: 0x060087D6 RID: 34774 RVA: 0x001EB438 File Offset: 0x001E9638
			IValueSet IValueSet.Intersect(IValueSet other)
			{
				return this.Intersect((ValueSetFactory.NintValueSet)other);
			}

			// Token: 0x060087D7 RID: 34775 RVA: 0x001EB448 File Offset: 0x001E9648
			public IValueSet<int> Union(IValueSet<int> o)
			{
				ValueSetFactory.NintValueSet nintValueSet = (ValueSetFactory.NintValueSet)o;
				return new ValueSetFactory.NintValueSet(this._hasSmall || nintValueSet._hasSmall, this._values.Union(nintValueSet._values), this._hasLarge || nintValueSet._hasLarge);
			}

			// Token: 0x060087D8 RID: 34776 RVA: 0x001EB494 File Offset: 0x001E9694
			IValueSet IValueSet.Union(IValueSet other)
			{
				return this.Union((ValueSetFactory.NintValueSet)other);
			}

			// Token: 0x060087D9 RID: 34777 RVA: 0x001EB4A4 File Offset: 0x001E96A4
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				ValueSetFactory.NintValueSet nintValueSet = obj as ValueSetFactory.NintValueSet;
				return nintValueSet != null && this._hasSmall == nintValueSet._hasSmall && this._hasLarge == nintValueSet._hasLarge && this._values.Equals(nintValueSet._values);
			}

			// Token: 0x060087DA RID: 34778 RVA: 0x001EB4EC File Offset: 0x001E96EC
			public override int GetHashCode()
			{
				return Hash.Combine(this._hasSmall.GetHashCode(), Hash.Combine(this._hasLarge.GetHashCode(), this._values.GetHashCode()));
			}

			// Token: 0x060087DB RID: 34779 RVA: 0x001EB52C File Offset: 0x001E972C
			public override string ToString()
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				StringBuilder builder = instance.Builder;
				if (this._hasSmall)
				{
					builder.Append("Small");
				}
				if (this._hasSmall && !this._values.IsEmpty)
				{
					builder.Append(",");
				}
				builder.Append(this._values.ToString());
				if (this._hasLarge && builder.Length > 0)
				{
					builder.Append(",");
				}
				if (this._hasLarge)
				{
					builder.Append("Large");
				}
				return instance.ToStringAndFree();
			}

			// Token: 0x040023A2 RID: 9122
			public static readonly ValueSetFactory.NintValueSet AllValues = new ValueSetFactory.NintValueSet(true, ValueSetFactory.NumericValueSet<int, ValueSetFactory.IntTC>.AllValues, true);

			// Token: 0x040023A3 RID: 9123
			public static readonly ValueSetFactory.NintValueSet NoValues = new ValueSetFactory.NintValueSet(false, ValueSetFactory.NumericValueSet<int, ValueSetFactory.IntTC>.NoValues, false);

			// Token: 0x040023A4 RID: 9124
			private readonly IValueSet<int> _values;

			// Token: 0x040023A5 RID: 9125
			private readonly bool _hasSmall;

			// Token: 0x040023A6 RID: 9126
			private readonly bool _hasLarge;
		}

		// Token: 0x02000775 RID: 1909
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NintValueSetFactory : IValueSetFactory<int>, IValueSetFactory
		{
			// Token: 0x060087DD RID: 34781 RVA: 0x001EB5E6 File Offset: 0x001E97E6
			private NintValueSetFactory()
			{
			}

			// Token: 0x17002715 RID: 10005
			// (get) Token: 0x060087DE RID: 34782 RVA: 0x001EB5EE File Offset: 0x001E97EE
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.NintValueSet.AllValues;
				}
			}

			// Token: 0x17002716 RID: 10006
			// (get) Token: 0x060087DF RID: 34783 RVA: 0x001EB5F5 File Offset: 0x001E97F5
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.NintValueSet.NoValues;
				}
			}

			// Token: 0x060087E0 RID: 34784 RVA: 0x001EB5FC File Offset: 0x001E97FC
			public IValueSet<int> Related(BinaryOperatorKind relation, int value)
			{
				bool flag = relation == BinaryOperatorKind.LessThan || relation == BinaryOperatorKind.LessThanOrEqual;
				bool flag2 = flag;
				IValueSet<int> valueSet = ValueSetFactory.NumericValueSetFactory<int, ValueSetFactory.IntTC>.Instance.Related(relation, value);
				bool flag3 = relation == BinaryOperatorKind.GreaterThan || relation == BinaryOperatorKind.GreaterThanOrEqual;
				return new ValueSetFactory.NintValueSet(flag2, valueSet, flag3);
			}

			// Token: 0x060087E1 RID: 34785 RVA: 0x001EB658 File Offset: 0x001E9858
			IValueSet IValueSetFactory.Random(int expectedSize, Random random)
			{
				return new ValueSetFactory.NintValueSet(random.NextDouble() < 0.25, (IValueSet<int>)ValueSetFactory.NumericValueSetFactory<int, ValueSetFactory.IntTC>.Instance.Random(expectedSize, random), random.NextDouble() < 0.25);
			}

			// Token: 0x060087E2 RID: 34786 RVA: 0x001EB694 File Offset: 0x001E9894
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				return ConstantValue.CreateNativeInt(default(ValueSetFactory.IntTC).Random(random));
			}

			// Token: 0x060087E3 RID: 34787 RVA: 0x001EB6B8 File Offset: 0x001E98B8
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					return this.Related(relation, default(ValueSetFactory.IntTC).FromConstantValue(value));
				}
				return ValueSetFactory.NintValueSet.AllValues;
			}

			// Token: 0x060087E4 RID: 34788 RVA: 0x001EB6EC File Offset: 0x001E98EC
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				ValueSetFactory.IntTC intTC = default(ValueSetFactory.IntTC);
				return intTC.Related(relation, intTC.FromConstantValue(left), intTC.FromConstantValue(right));
			}

			// Token: 0x040023A7 RID: 9127
			public static readonly ValueSetFactory.NintValueSetFactory Instance = new ValueSetFactory.NintValueSetFactory();
		}

		// Token: 0x02000776 RID: 1910
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NuintValueSet : IValueSet<uint>, IValueSet
		{
			// Token: 0x060087E6 RID: 34790 RVA: 0x001EB725 File Offset: 0x001E9925
			internal NuintValueSet(IValueSet<uint> values, bool hasLarge)
			{
				this._values = values;
				this._hasLarge = hasLarge;
			}

			// Token: 0x17002717 RID: 10007
			// (get) Token: 0x060087E7 RID: 34791 RVA: 0x001EB73B File Offset: 0x001E993B
			public bool IsEmpty
			{
				get
				{
					return !this._hasLarge && this._values.IsEmpty;
				}
			}

			// Token: 0x17002718 RID: 10008
			// (get) Token: 0x060087E8 RID: 34792 RVA: 0x001EB752 File Offset: 0x001E9952
			[global::System.Runtime.CompilerServices.Nullable(2)]
			ConstantValue IValueSet.Sample
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get
				{
					if (this.IsEmpty)
					{
						throw new ArgumentException();
					}
					if (!this._values.IsEmpty)
					{
						return this._values.Sample;
					}
					return null;
				}
			}

			// Token: 0x060087E9 RID: 34793 RVA: 0x001EB77C File Offset: 0x001E997C
			public bool All(BinaryOperatorKind relation, uint value)
			{
				bool flag = this._hasLarge;
				if (flag)
				{
					bool flag2 = relation == BinaryOperatorKind.LessThan || relation == BinaryOperatorKind.LessThanOrEqual;
					flag = flag2;
				}
				return !flag && this._values.All(relation, value);
			}

			// Token: 0x060087EA RID: 34794 RVA: 0x001EB7C3 File Offset: 0x001E99C3
			bool IValueSet.All(BinaryOperatorKind relation, ConstantValue value)
			{
				return value.IsBad || this.All(relation, value.UInt32Value);
			}

			// Token: 0x060087EB RID: 34795 RVA: 0x001EB7DC File Offset: 0x001E99DC
			public bool Any(BinaryOperatorKind relation, uint value)
			{
				bool flag = this._hasLarge;
				if (flag)
				{
					bool flag2 = relation == BinaryOperatorKind.GreaterThan || relation == BinaryOperatorKind.GreaterThanOrEqual;
					flag = flag2;
				}
				return flag || this._values.Any(relation, value);
			}

			// Token: 0x060087EC RID: 34796 RVA: 0x001EB823 File Offset: 0x001E9A23
			bool IValueSet.Any(BinaryOperatorKind relation, ConstantValue value)
			{
				return value.IsBad || this.Any(relation, value.UInt32Value);
			}

			// Token: 0x060087ED RID: 34797 RVA: 0x001EB83C File Offset: 0x001E9A3C
			public IValueSet<uint> Complement()
			{
				return new ValueSetFactory.NuintValueSet(this._values.Complement(), !this._hasLarge);
			}

			// Token: 0x060087EE RID: 34798 RVA: 0x001EB857 File Offset: 0x001E9A57
			IValueSet IValueSet.Complement()
			{
				return this.Complement();
			}

			// Token: 0x060087EF RID: 34799 RVA: 0x001EB860 File Offset: 0x001E9A60
			public IValueSet<uint> Intersect(IValueSet<uint> o)
			{
				ValueSetFactory.NuintValueSet nuintValueSet = (ValueSetFactory.NuintValueSet)o;
				return new ValueSetFactory.NuintValueSet(this._values.Intersect(nuintValueSet._values), this._hasLarge && nuintValueSet._hasLarge);
			}

			// Token: 0x060087F0 RID: 34800 RVA: 0x001EB89B File Offset: 0x001E9A9B
			IValueSet IValueSet.Intersect(IValueSet other)
			{
				return this.Intersect((ValueSetFactory.NuintValueSet)other);
			}

			// Token: 0x060087F1 RID: 34801 RVA: 0x001EB8AC File Offset: 0x001E9AAC
			public IValueSet<uint> Union(IValueSet<uint> o)
			{
				ValueSetFactory.NuintValueSet nuintValueSet = (ValueSetFactory.NuintValueSet)o;
				return new ValueSetFactory.NuintValueSet(this._values.Union(nuintValueSet._values), this._hasLarge || nuintValueSet._hasLarge);
			}

			// Token: 0x060087F2 RID: 34802 RVA: 0x001EB8E7 File Offset: 0x001E9AE7
			IValueSet IValueSet.Union(IValueSet other)
			{
				return this.Union((ValueSetFactory.NuintValueSet)other);
			}

			// Token: 0x060087F3 RID: 34803 RVA: 0x001EB8F8 File Offset: 0x001E9AF8
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				ValueSetFactory.NuintValueSet nuintValueSet = obj as ValueSetFactory.NuintValueSet;
				return nuintValueSet != null && this._hasLarge == nuintValueSet._hasLarge && this._values.Equals(nuintValueSet._values);
			}

			// Token: 0x060087F4 RID: 34804 RVA: 0x001EB930 File Offset: 0x001E9B30
			public override int GetHashCode()
			{
				return Hash.Combine(this._hasLarge.GetHashCode(), this._values.GetHashCode());
			}

			// Token: 0x060087F5 RID: 34805 RVA: 0x001EB95C File Offset: 0x001E9B5C
			public override string ToString()
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				StringBuilder builder = instance.Builder;
				builder.Append(this._values.ToString());
				if (this._hasLarge && builder.Length > 0)
				{
					builder.Append(",");
				}
				if (this._hasLarge)
				{
					builder.Append("Large");
				}
				return instance.ToStringAndFree();
			}

			// Token: 0x040023A8 RID: 9128
			public static readonly ValueSetFactory.NuintValueSet AllValues = new ValueSetFactory.NuintValueSet(ValueSetFactory.NumericValueSet<uint, ValueSetFactory.UIntTC>.AllValues, true);

			// Token: 0x040023A9 RID: 9129
			public static readonly ValueSetFactory.NuintValueSet NoValues = new ValueSetFactory.NuintValueSet(ValueSetFactory.NumericValueSet<uint, ValueSetFactory.UIntTC>.NoValues, false);

			// Token: 0x040023AA RID: 9130
			private readonly IValueSet<uint> _values;

			// Token: 0x040023AB RID: 9131
			private readonly bool _hasLarge;
		}

		// Token: 0x02000777 RID: 1911
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NuintValueSetFactory : IValueSetFactory<uint>, IValueSetFactory
		{
			// Token: 0x060087F7 RID: 34807 RVA: 0x001EB9DF File Offset: 0x001E9BDF
			private NuintValueSetFactory()
			{
			}

			// Token: 0x17002719 RID: 10009
			// (get) Token: 0x060087F8 RID: 34808 RVA: 0x001EB9E7 File Offset: 0x001E9BE7
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.NuintValueSet.AllValues;
				}
			}

			// Token: 0x1700271A RID: 10010
			// (get) Token: 0x060087F9 RID: 34809 RVA: 0x001EB9EE File Offset: 0x001E9BEE
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.NuintValueSet.NoValues;
				}
			}

			// Token: 0x060087FA RID: 34810 RVA: 0x001EB9F8 File Offset: 0x001E9BF8
			public IValueSet<uint> Related(BinaryOperatorKind relation, uint value)
			{
				IValueSet<uint> valueSet = ValueSetFactory.NumericValueSetFactory<uint, ValueSetFactory.UIntTC>.Instance.Related(relation, value);
				bool flag = relation == BinaryOperatorKind.GreaterThan || relation == BinaryOperatorKind.GreaterThanOrEqual;
				return new ValueSetFactory.NuintValueSet(valueSet, flag);
			}

			// Token: 0x060087FB RID: 34811 RVA: 0x001EBA35 File Offset: 0x001E9C35
			IValueSet IValueSetFactory.Random(int expectedSize, Random random)
			{
				return new ValueSetFactory.NuintValueSet((IValueSet<uint>)ValueSetFactory.NumericValueSetFactory<uint, ValueSetFactory.UIntTC>.Instance.Random(expectedSize, random), random.NextDouble() < 0.25);
			}

			// Token: 0x060087FC RID: 34812 RVA: 0x001EBA60 File Offset: 0x001E9C60
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				return ConstantValue.CreateNativeUInt(default(ValueSetFactory.UIntTC).Random(random));
			}

			// Token: 0x060087FD RID: 34813 RVA: 0x001EBA84 File Offset: 0x001E9C84
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					return this.Related(relation, default(ValueSetFactory.UIntTC).FromConstantValue(value));
				}
				return ValueSetFactory.NuintValueSet.AllValues;
			}

			// Token: 0x060087FE RID: 34814 RVA: 0x001EBAB8 File Offset: 0x001E9CB8
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				ValueSetFactory.UIntTC uintTC = default(ValueSetFactory.UIntTC);
				return uintTC.Related(relation, uintTC.FromConstantValue(left), uintTC.FromConstantValue(right));
			}

			// Token: 0x040023AC RID: 9132
			public static readonly ValueSetFactory.NuintValueSetFactory Instance = new ValueSetFactory.NuintValueSetFactory();
		}

		// Token: 0x02000778 RID: 1912
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NumericValueSet<[global::System.Runtime.CompilerServices.Nullable(2)] T, [global::System.Runtime.CompilerServices.Nullable(0)] TTC> : IValueSet<T>, IValueSet where TTC : struct, ValueSetFactory.INumericTC<T>
		{
			// Token: 0x06008800 RID: 34816 RVA: 0x001EBAF1 File Offset: 0x001E9CF1
			internal NumericValueSet(T first, T last)
				: this(ImmutableArray.Create<ValueTuple<T, T>>(new ValueTuple<T, T>(first, last)))
			{
			}

			// Token: 0x06008801 RID: 34817 RVA: 0x001EBB05 File Offset: 0x001E9D05
			internal NumericValueSet([TupleElementNames(new string[] { "first", "last" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<ValueTuple<T, T>> intervals)
			{
				this._intervals = intervals;
			}

			// Token: 0x1700271B RID: 10011
			// (get) Token: 0x06008802 RID: 34818 RVA: 0x001EBB14 File Offset: 0x001E9D14
			public bool IsEmpty
			{
				get
				{
					return this._intervals.Length == 0;
				}
			}

			// Token: 0x1700271C RID: 10012
			// (get) Token: 0x06008803 RID: 34819 RVA: 0x001EBB34 File Offset: 0x001E9D34
			ConstantValue IValueSet.Sample
			{
				get
				{
					if (this.IsEmpty)
					{
						throw new ArgumentException();
					}
					TTC ttc = default(TTC);
					IValueSet<T> valueSet = ValueSetFactory.NumericValueSetFactory<T, TTC>.Instance.Related(BinaryOperatorKind.GreaterThanOrEqual, ttc.Zero);
					ValueSetFactory.NumericValueSet<T, TTC> numericValueSet = (ValueSetFactory.NumericValueSet<T, TTC>)this.Intersect(valueSet);
					if (!numericValueSet.IsEmpty)
					{
						return ttc.ToConstantValue(numericValueSet._intervals[0].Item1);
					}
					return ttc.ToConstantValue(this._intervals[this._intervals.Length - 1].Item2);
				}
			}

			// Token: 0x06008804 RID: 34820 RVA: 0x001EBBDC File Offset: 0x001E9DDC
			public bool Any(BinaryOperatorKind relation, T value)
			{
				ValueSetFactory.NumericValueSet<T, TTC>.<>c__DisplayClass9_0 CS$<>8__locals1;
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.tc = default(TTC);
				if (relation > BinaryOperatorKind.GreaterThan)
				{
					if (relation != BinaryOperatorKind.LessThan)
					{
						if (relation == BinaryOperatorKind.GreaterThanOrEqual)
						{
							goto IL_008C;
						}
						if (relation != BinaryOperatorKind.LessThanOrEqual)
						{
							goto IL_00F2;
						}
					}
					return this._intervals.Length > 0 && CS$<>8__locals1.tc.Related(relation, this._intervals[0].Item1, value);
				}
				if (relation == BinaryOperatorKind.Equal)
				{
					return this.<Any>g__anyIntervalContains|9_0(0, this._intervals.Length - 1, value, ref CS$<>8__locals1);
				}
				if (relation != BinaryOperatorKind.GreaterThan)
				{
					goto IL_00F2;
				}
				IL_008C:
				return this._intervals.Length > 0 && CS$<>8__locals1.tc.Related(relation, this._intervals[this._intervals.Length - 1].Item2, value);
				IL_00F2:
				throw ExceptionUtilities.UnexpectedValue(relation);
			}

			// Token: 0x06008805 RID: 34821 RVA: 0x001EBCE8 File Offset: 0x001E9EE8
			bool IValueSet.Any(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TTC ttc = default(TTC);
					return this.Any(relation, ttc.FromConstantValue(value));
				}
				return true;
			}

			// Token: 0x06008806 RID: 34822 RVA: 0x001EBD1C File Offset: 0x001E9F1C
			public bool All(BinaryOperatorKind relation, T value)
			{
				if (this._intervals.Length == 0)
				{
					return true;
				}
				TTC ttc = default(TTC);
				if (relation > BinaryOperatorKind.GreaterThan)
				{
					if (relation != BinaryOperatorKind.LessThan)
					{
						if (relation == BinaryOperatorKind.GreaterThanOrEqual)
						{
							goto IL_0085;
						}
						if (relation != BinaryOperatorKind.LessThanOrEqual)
						{
							goto IL_010D;
						}
					}
					return ttc.Related(relation, this._intervals[this._intervals.Length - 1].Item2, value);
				}
				if (relation == BinaryOperatorKind.Equal)
				{
					return this._intervals.Length == 1 && ttc.Related(BinaryOperatorKind.Equal, this._intervals[0].Item1, value) && ttc.Related(BinaryOperatorKind.Equal, this._intervals[0].Item2, value);
				}
				if (relation != BinaryOperatorKind.GreaterThan)
				{
					goto IL_010D;
				}
				IL_0085:
				return ttc.Related(relation, this._intervals[0].Item1, value);
				IL_010D:
				throw ExceptionUtilities.UnexpectedValue(relation);
			}

			// Token: 0x06008807 RID: 34823 RVA: 0x001EBE44 File Offset: 0x001EA044
			bool IValueSet.All(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TTC ttc = default(TTC);
					return this.All(relation, ttc.FromConstantValue(value));
				}
				return false;
			}

			// Token: 0x06008808 RID: 34824 RVA: 0x001EBE78 File Offset: 0x001EA078
			public IValueSet<T> Complement()
			{
				if (this._intervals.Length == 0)
				{
					return ValueSetFactory.NumericValueSet<T, TTC>.AllValues;
				}
				TTC ttc = default(TTC);
				ArrayBuilder<ValueTuple<T, T>> instance = ArrayBuilder<ValueTuple<T, T>>.GetInstance();
				if (ttc.Related(BinaryOperatorKind.LessThan, ttc.MinValue, this._intervals[0].Item1))
				{
					instance.Add(new ValueTuple<T, T>(ttc.MinValue, ttc.Prev(this._intervals[0].Item1)));
				}
				int num = this._intervals.Length - 1;
				for (int i = 0; i < num; i++)
				{
					instance.Add(new ValueTuple<T, T>(ttc.Next(this._intervals[i].Item2), ttc.Prev(this._intervals[i + 1].Item1)));
				}
				if (ttc.Related(BinaryOperatorKind.LessThan, this._intervals[num].Item2, ttc.MaxValue))
				{
					instance.Add(new ValueTuple<T, T>(ttc.Next(this._intervals[num].Item2), ttc.MaxValue));
				}
				return new ValueSetFactory.NumericValueSet<T, TTC>(instance.ToImmutableAndFree());
			}

			// Token: 0x06008809 RID: 34825 RVA: 0x001EC002 File Offset: 0x001EA202
			IValueSet IValueSet.Complement()
			{
				return this.Complement();
			}

			// Token: 0x0600880A RID: 34826 RVA: 0x001EC00C File Offset: 0x001EA20C
			public IValueSet<T> Intersect(IValueSet<T> o)
			{
				ValueSetFactory.NumericValueSet<T, TTC> numericValueSet = (ValueSetFactory.NumericValueSet<T, TTC>)o;
				TTC ttc = default(TTC);
				ArrayBuilder<ValueTuple<T, T>> instance = ArrayBuilder<ValueTuple<T, T>>.GetInstance();
				ImmutableArray<ValueTuple<T, T>> intervals = this._intervals;
				ImmutableArray<ValueTuple<T, T>> intervals2 = numericValueSet._intervals;
				int num = 0;
				int num2 = 0;
				while (num < intervals.Length && num2 < intervals2.Length)
				{
					ValueTuple<T, T> valueTuple = intervals[num];
					ValueTuple<T, T> valueTuple2 = intervals2[num2];
					if (ttc.Related(BinaryOperatorKind.LessThan, valueTuple.Item2, valueTuple2.Item1))
					{
						num++;
					}
					else if (ttc.Related(BinaryOperatorKind.LessThan, valueTuple2.Item2, valueTuple.Item1))
					{
						num2++;
					}
					else
					{
						ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, ValueSetFactory.NumericValueSet<T, TTC>.Max(valueTuple.Item1, valueTuple2.Item1), ValueSetFactory.NumericValueSet<T, TTC>.Min(valueTuple.Item2, valueTuple2.Item2));
						if (ttc.Related(BinaryOperatorKind.LessThan, valueTuple.Item2, valueTuple2.Item2))
						{
							num++;
						}
						else if (ttc.Related(BinaryOperatorKind.LessThan, valueTuple2.Item2, valueTuple.Item2))
						{
							num2++;
						}
						else
						{
							num++;
							num2++;
						}
					}
				}
				return new ValueSetFactory.NumericValueSet<T, TTC>(instance.ToImmutableAndFree());
			}

			// Token: 0x0600880B RID: 34827 RVA: 0x001EC168 File Offset: 0x001EA368
			private static void Add([TupleElementNames(new string[] { "first", "last" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] ArrayBuilder<ValueTuple<T, T>> builder, T first, T last)
			{
				TTC ttc = default(TTC);
				if (builder.Count > 0 && (ttc.Related(BinaryOperatorKind.Equal, ttc.MinValue, first) || ttc.Related(BinaryOperatorKind.GreaterThanOrEqual, builder.Last().Item2, ttc.Prev(first))))
				{
					ValueTuple<T, T> valueTuple = builder.Pop<ValueTuple<T, T>>();
					valueTuple.Item2 = ValueSetFactory.NumericValueSet<T, TTC>.Max(last, valueTuple.Item2);
					builder.Push(valueTuple);
					return;
				}
				builder.Add(new ValueTuple<T, T>(first, last));
			}

			// Token: 0x0600880C RID: 34828 RVA: 0x001EC204 File Offset: 0x001EA404
			private static T Min(T a, T b)
			{
				TTC ttc = default(TTC);
				if (!ttc.Related(BinaryOperatorKind.LessThan, a, b))
				{
					return b;
				}
				return a;
			}

			// Token: 0x0600880D RID: 34829 RVA: 0x001EC234 File Offset: 0x001EA434
			private static T Max(T a, T b)
			{
				TTC ttc = default(TTC);
				if (!ttc.Related(BinaryOperatorKind.LessThan, a, b))
				{
					return a;
				}
				return b;
			}

			// Token: 0x0600880E RID: 34830 RVA: 0x001EC262 File Offset: 0x001EA462
			IValueSet IValueSet.Intersect(IValueSet other)
			{
				return this.Intersect((IValueSet<T>)other);
			}

			// Token: 0x0600880F RID: 34831 RVA: 0x001EC270 File Offset: 0x001EA470
			public IValueSet<T> Union(IValueSet<T> o)
			{
				ValueSetFactory.NumericValueSet<T, TTC> numericValueSet = (ValueSetFactory.NumericValueSet<T, TTC>)o;
				TTC ttc = default(TTC);
				ArrayBuilder<ValueTuple<T, T>> instance = ArrayBuilder<ValueTuple<T, T>>.GetInstance();
				ImmutableArray<ValueTuple<T, T>> intervals = this._intervals;
				ImmutableArray<ValueTuple<T, T>> intervals2 = numericValueSet._intervals;
				int i = 0;
				int j = 0;
				while (i < intervals.Length)
				{
					if (j >= intervals2.Length)
					{
						break;
					}
					ValueTuple<T, T> valueTuple = intervals[i];
					ValueTuple<T, T> valueTuple2 = intervals2[j];
					if (ttc.Related(BinaryOperatorKind.LessThan, valueTuple.Item2, valueTuple2.Item1))
					{
						ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, valueTuple.Item1, valueTuple.Item2);
						i++;
					}
					else if (ttc.Related(BinaryOperatorKind.LessThan, valueTuple2.Item2, valueTuple.Item1))
					{
						ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, valueTuple2.Item1, valueTuple2.Item2);
						j++;
					}
					else
					{
						ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, ValueSetFactory.NumericValueSet<T, TTC>.Min(valueTuple.Item1, valueTuple2.Item1), ValueSetFactory.NumericValueSet<T, TTC>.Max(valueTuple.Item2, valueTuple2.Item2));
						i++;
						j++;
					}
				}
				while (i < intervals.Length)
				{
					ValueTuple<T, T> valueTuple3 = intervals[i];
					ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, valueTuple3.Item1, valueTuple3.Item2);
					i++;
				}
				while (j < intervals2.Length)
				{
					ValueTuple<T, T> valueTuple4 = intervals2[j];
					ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, valueTuple4.Item1, valueTuple4.Item2);
					j++;
				}
				return new ValueSetFactory.NumericValueSet<T, TTC>(instance.ToImmutableAndFree());
			}

			// Token: 0x06008810 RID: 34832 RVA: 0x001EC3FB File Offset: 0x001EA5FB
			IValueSet IValueSet.Union(IValueSet other)
			{
				return this.Union((IValueSet<T>)other);
			}

			// Token: 0x06008811 RID: 34833 RVA: 0x001EC40C File Offset: 0x001EA60C
			internal static IValueSet<T> Random(int expectedSize, Random random)
			{
				TTC ttc = default(TTC);
				T[] array = new T[expectedSize * 2];
				int i = 0;
				int num = expectedSize * 2;
				while (i < num)
				{
					array[i] = ttc.Random(random);
					i++;
				}
				Array.Sort<T>(array);
				ArrayBuilder<ValueTuple<T, T>> instance = ArrayBuilder<ValueTuple<T, T>>.GetInstance();
				int j = 0;
				int num2 = array.Length;
				while (j < num2)
				{
					T t = array[j];
					T t2 = array[j + 1];
					ValueSetFactory.NumericValueSet<T, TTC>.Add(instance, t, t2);
					j += 2;
				}
				return new ValueSetFactory.NumericValueSet<T, TTC>(instance.ToImmutableAndFree());
			}

			// Token: 0x06008812 RID: 34834 RVA: 0x001EC4A0 File Offset: 0x001EA6A0
			public override string ToString()
			{
				TTC tc = default(TTC);
				return string.Join(",", this._intervals.Select<ValueTuple<T, T>, string>(([TupleElementNames(new string[] { "first", "last" })] ValueTuple<T, T> p) => string.Concat(new string[]
				{
					"[",
					tc.ToString(p.Item1),
					"..",
					tc.ToString(p.Item2),
					"]"
				})));
			}

			// Token: 0x06008813 RID: 34835 RVA: 0x001EC4E0 File Offset: 0x001EA6E0
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				ValueSetFactory.NumericValueSet<T, TTC> numericValueSet = obj as ValueSetFactory.NumericValueSet<T, TTC>;
				return numericValueSet != null && this._intervals.SequenceEqual<ValueTuple<T, T>, ValueTuple<T, T>>(numericValueSet._intervals, null);
			}

			// Token: 0x06008814 RID: 34836 RVA: 0x001EC50C File Offset: 0x001EA70C
			public override int GetHashCode()
			{
				return Hash.Combine(Hash.CombineValues<ValueTuple<T, T>>(this._intervals, int.MaxValue), this._intervals.Length);
			}

			// Token: 0x06008815 RID: 34837 RVA: 0x001EC53C File Offset: 0x001EA73C
			// Note: this type is marked as 'beforefieldinit'.
			static NumericValueSet()
			{
				TTC ttc = default(TTC);
				T minValue = ttc.MinValue;
				ttc = default(TTC);
				ValueSetFactory.NumericValueSet<T, TTC>.AllValues = new ValueSetFactory.NumericValueSet<T, TTC>(minValue, ttc.MaxValue);
				ValueSetFactory.NumericValueSet<T, TTC>.NoValues = new ValueSetFactory.NumericValueSet<T, TTC>(ImmutableArray<ValueTuple<T, T>>.Empty);
			}

			// Token: 0x06008816 RID: 34838 RVA: 0x001EC58C File Offset: 0x001EA78C
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			[CompilerGenerated]
			private bool <Any>g__anyIntervalContains|9_0(int firstIntervalIndex, int lastIntervalIndex, [global::System.Runtime.CompilerServices.Nullable(1)] T value, ref ValueSetFactory.NumericValueSet<T, TTC>.<>c__DisplayClass9_0 A_4)
			{
				while (lastIntervalIndex >= firstIntervalIndex)
				{
					if (lastIntervalIndex == firstIntervalIndex)
					{
						return A_4.tc.Related(BinaryOperatorKind.GreaterThanOrEqual, value, this._intervals[lastIntervalIndex].Item1) && A_4.tc.Related(BinaryOperatorKind.LessThanOrEqual, value, this._intervals[lastIntervalIndex].Item2);
					}
					int num = firstIntervalIndex + (lastIntervalIndex - firstIntervalIndex) / 2;
					if (A_4.tc.Related(BinaryOperatorKind.LessThanOrEqual, value, this._intervals[num].Item2))
					{
						lastIntervalIndex = num;
					}
					else
					{
						firstIntervalIndex = num + 1;
					}
				}
				return false;
			}

			// Token: 0x040023AD RID: 9133
			[TupleElementNames(new string[] { "first", "last" })]
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1, 1 })]
			private readonly ImmutableArray<ValueTuple<T, T>> _intervals;

			// Token: 0x040023AE RID: 9134
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.NumericValueSet<T, TTC> AllValues;

			// Token: 0x040023AF RID: 9135
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.NumericValueSet<T, TTC> NoValues;
		}

		// Token: 0x02000779 RID: 1913
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class NumericValueSetFactory<[global::System.Runtime.CompilerServices.Nullable(2)] T, [global::System.Runtime.CompilerServices.Nullable(0)] TTC> : IValueSetFactory<T>, IValueSetFactory where TTC : struct, ValueSetFactory.INumericTC<T>
		{
			// Token: 0x1700271D RID: 10013
			// (get) Token: 0x06008817 RID: 34839 RVA: 0x001EC647 File Offset: 0x001EA847
			IValueSet IValueSetFactory.AllValues
			{
				get
				{
					return ValueSetFactory.NumericValueSet<T, TTC>.AllValues;
				}
			}

			// Token: 0x1700271E RID: 10014
			// (get) Token: 0x06008818 RID: 34840 RVA: 0x001EC64E File Offset: 0x001EA84E
			IValueSet IValueSetFactory.NoValues
			{
				get
				{
					return ValueSetFactory.NumericValueSet<T, TTC>.NoValues;
				}
			}

			// Token: 0x06008819 RID: 34841 RVA: 0x001EC655 File Offset: 0x001EA855
			private NumericValueSetFactory()
			{
			}

			// Token: 0x0600881A RID: 34842 RVA: 0x001EC660 File Offset: 0x001EA860
			public IValueSet<T> Related(BinaryOperatorKind relation, T value)
			{
				TTC ttc = default(TTC);
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return new ValueSetFactory.NumericValueSet<T, TTC>(value, value);
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						if (ttc.Related(BinaryOperatorKind.GreaterThanOrEqual, value, ttc.MaxValue))
						{
							return ValueSetFactory.NumericValueSet<T, TTC>.NoValues;
						}
						return new ValueSetFactory.NumericValueSet<T, TTC>(ttc.Next(value), ttc.MaxValue);
					}
				}
				else if (relation != BinaryOperatorKind.LessThan)
				{
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return new ValueSetFactory.NumericValueSet<T, TTC>(value, ttc.MaxValue);
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return new ValueSetFactory.NumericValueSet<T, TTC>(ttc.MinValue, value);
					}
				}
				else
				{
					if (ttc.Related(BinaryOperatorKind.LessThanOrEqual, value, ttc.MinValue))
					{
						return ValueSetFactory.NumericValueSet<T, TTC>.NoValues;
					}
					return new ValueSetFactory.NumericValueSet<T, TTC>(ttc.MinValue, ttc.Prev(value));
				}
				throw ExceptionUtilities.UnexpectedValue(relation);
			}

			// Token: 0x0600881B RID: 34843 RVA: 0x001EC788 File Offset: 0x001EA988
			IValueSet IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue value)
			{
				if (!value.IsBad)
				{
					TTC ttc = default(TTC);
					return this.Related(relation, ttc.FromConstantValue(value));
				}
				return ValueSetFactory.NumericValueSet<T, TTC>.AllValues;
			}

			// Token: 0x0600881C RID: 34844 RVA: 0x001EC7C1 File Offset: 0x001EA9C1
			public IValueSet Random(int expectedSize, Random random)
			{
				return ValueSetFactory.NumericValueSet<T, TTC>.Random(expectedSize, random);
			}

			// Token: 0x0600881D RID: 34845 RVA: 0x001EC7CC File Offset: 0x001EA9CC
			ConstantValue IValueSetFactory.RandomValue(Random random)
			{
				TTC ttc = default(TTC);
				return ttc.ToConstantValue(ttc.Random(random));
			}

			// Token: 0x0600881E RID: 34846 RVA: 0x001EC7FC File Offset: 0x001EA9FC
			bool IValueSetFactory.Related(BinaryOperatorKind relation, ConstantValue left, ConstantValue right)
			{
				TTC ttc = default(TTC);
				return ttc.Related(relation, ttc.FromConstantValue(left), ttc.FromConstantValue(right));
			}

			// Token: 0x040023B0 RID: 9136
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 1, 0 })]
			public static readonly ValueSetFactory.NumericValueSetFactory<T, TTC> Instance = new ValueSetFactory.NumericValueSetFactory<T, TTC>();
		}

		// Token: 0x0200077A RID: 1914
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct SByteTC : ValueSetFactory.INumericTC<sbyte>
		{
			// Token: 0x1700271F RID: 10015
			// (get) Token: 0x06008820 RID: 34848 RVA: 0x001EC847 File Offset: 0x001EAA47
			sbyte ValueSetFactory.INumericTC<sbyte>.MinValue
			{
				get
				{
					return sbyte.MinValue;
				}
			}

			// Token: 0x17002720 RID: 10016
			// (get) Token: 0x06008821 RID: 34849 RVA: 0x001EC84B File Offset: 0x001EAA4B
			sbyte ValueSetFactory.INumericTC<sbyte>.MaxValue
			{
				get
				{
					return sbyte.MaxValue;
				}
			}

			// Token: 0x17002721 RID: 10017
			// (get) Token: 0x06008822 RID: 34850 RVA: 0x001EC84F File Offset: 0x001EAA4F
			sbyte ValueSetFactory.INumericTC<sbyte>.Zero
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06008823 RID: 34851 RVA: 0x001EC854 File Offset: 0x001EAA54
			bool ValueSetFactory.INumericTC<sbyte>.Related(BinaryOperatorKind relation, sbyte left, sbyte right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x06008824 RID: 34852 RVA: 0x001EC8BE File Offset: 0x001EAABE
			sbyte ValueSetFactory.INumericTC<sbyte>.Next(sbyte value)
			{
				return value + 1;
			}

			// Token: 0x06008825 RID: 34853 RVA: 0x001EC8C4 File Offset: 0x001EAAC4
			sbyte ValueSetFactory.INumericTC<sbyte>.Prev(sbyte value)
			{
				return value - 1;
			}

			// Token: 0x06008826 RID: 34854 RVA: 0x001EC8CA File Offset: 0x001EAACA
			sbyte ValueSetFactory.INumericTC<sbyte>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.SByteValue;
				}
				return 0;
			}

			// Token: 0x06008827 RID: 34855 RVA: 0x001EC8DC File Offset: 0x001EAADC
			public ConstantValue ToConstantValue(sbyte value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x06008828 RID: 34856 RVA: 0x001EC8E4 File Offset: 0x001EAAE4
			string ValueSetFactory.INumericTC<sbyte>.ToString(sbyte value)
			{
				return value.ToString();
			}

			// Token: 0x06008829 RID: 34857 RVA: 0x001EC8ED File Offset: 0x001EAAED
			sbyte ValueSetFactory.INumericTC<sbyte>.Random(Random random)
			{
				return (sbyte)random.Next();
			}
		}

		// Token: 0x0200077B RID: 1915
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct ShortTC : ValueSetFactory.INumericTC<short>
		{
			// Token: 0x17002722 RID: 10018
			// (get) Token: 0x0600882A RID: 34858 RVA: 0x001EC8F6 File Offset: 0x001EAAF6
			short ValueSetFactory.INumericTC<short>.MinValue
			{
				get
				{
					return short.MinValue;
				}
			}

			// Token: 0x17002723 RID: 10019
			// (get) Token: 0x0600882B RID: 34859 RVA: 0x001EC8FD File Offset: 0x001EAAFD
			short ValueSetFactory.INumericTC<short>.MaxValue
			{
				get
				{
					return short.MaxValue;
				}
			}

			// Token: 0x17002724 RID: 10020
			// (get) Token: 0x0600882C RID: 34860 RVA: 0x001EC904 File Offset: 0x001EAB04
			short ValueSetFactory.INumericTC<short>.Zero
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600882D RID: 34861 RVA: 0x001EC908 File Offset: 0x001EAB08
			bool ValueSetFactory.INumericTC<short>.Related(BinaryOperatorKind relation, short left, short right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x0600882E RID: 34862 RVA: 0x001EC972 File Offset: 0x001EAB72
			short ValueSetFactory.INumericTC<short>.Next(short value)
			{
				return value + 1;
			}

			// Token: 0x0600882F RID: 34863 RVA: 0x001EC978 File Offset: 0x001EAB78
			short ValueSetFactory.INumericTC<short>.Prev(short value)
			{
				return value - 1;
			}

			// Token: 0x06008830 RID: 34864 RVA: 0x001EC97E File Offset: 0x001EAB7E
			short ValueSetFactory.INumericTC<short>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.Int16Value;
				}
				return 0;
			}

			// Token: 0x06008831 RID: 34865 RVA: 0x001EC990 File Offset: 0x001EAB90
			ConstantValue ValueSetFactory.INumericTC<short>.ToConstantValue(short value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x06008832 RID: 34866 RVA: 0x001EC998 File Offset: 0x001EAB98
			string ValueSetFactory.INumericTC<short>.ToString(short value)
			{
				return value.ToString();
			}

			// Token: 0x06008833 RID: 34867 RVA: 0x001EC9A1 File Offset: 0x001EABA1
			short ValueSetFactory.INumericTC<short>.Random(Random random)
			{
				return (short)random.Next();
			}
		}

		// Token: 0x0200077C RID: 1916
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct SingleTC : ValueSetFactory.FloatingTC<float>, ValueSetFactory.INumericTC<float>
		{
			// Token: 0x17002725 RID: 10021
			// (get) Token: 0x06008834 RID: 34868 RVA: 0x001EC9AA File Offset: 0x001EABAA
			float ValueSetFactory.INumericTC<float>.MinValue
			{
				get
				{
					return float.NegativeInfinity;
				}
			}

			// Token: 0x17002726 RID: 10022
			// (get) Token: 0x06008835 RID: 34869 RVA: 0x001EC9B1 File Offset: 0x001EABB1
			float ValueSetFactory.INumericTC<float>.MaxValue
			{
				get
				{
					return float.PositiveInfinity;
				}
			}

			// Token: 0x17002727 RID: 10023
			// (get) Token: 0x06008836 RID: 34870 RVA: 0x001EC9B8 File Offset: 0x001EABB8
			float ValueSetFactory.FloatingTC<float>.NaN
			{
				get
				{
					return float.NaN;
				}
			}

			// Token: 0x17002728 RID: 10024
			// (get) Token: 0x06008837 RID: 34871 RVA: 0x001EC9BF File Offset: 0x001EABBF
			float ValueSetFactory.INumericTC<float>.Zero
			{
				get
				{
					return 0f;
				}
			}

			// Token: 0x06008838 RID: 34872 RVA: 0x001EC9C8 File Offset: 0x001EABC8
			public float Next(float value)
			{
				if (value == 0f)
				{
					return float.Epsilon;
				}
				if (value < 0f)
				{
					if (value == -1E-45f)
					{
						return 0f;
					}
					if (value == float.NegativeInfinity)
					{
						return float.MinValue;
					}
					return -ValueSetFactory.SingleTC.UintAsFloat(ValueSetFactory.SingleTC.FloatAsUint(-value) - 1U);
				}
				else
				{
					if (value == 3.4028235E+38f)
					{
						return float.PositiveInfinity;
					}
					return ValueSetFactory.SingleTC.UintAsFloat(ValueSetFactory.SingleTC.FloatAsUint(value) + 1U);
				}
			}

			// Token: 0x06008839 RID: 34873 RVA: 0x001ECA34 File Offset: 0x001EAC34
			private unsafe static uint FloatAsUint(float d)
			{
				if (d == 0f)
				{
					return 0U;
				}
				uint* ptr = (uint*)(&d);
				return *ptr;
			}

			// Token: 0x0600883A RID: 34874 RVA: 0x001ECA54 File Offset: 0x001EAC54
			private unsafe static float UintAsFloat(uint l)
			{
				float* ptr = (float*)(&l);
				return *ptr;
			}

			// Token: 0x0600883B RID: 34875 RVA: 0x001ECA68 File Offset: 0x001EAC68
			bool ValueSetFactory.INumericTC<float>.Related(BinaryOperatorKind relation, float left, float right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right || (float.IsNaN(left) && float.IsNaN(right));
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x0600883C RID: 34876 RVA: 0x001ECAE4 File Offset: 0x001EACE4
			float ValueSetFactory.INumericTC<float>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.SingleValue;
				}
				return 0f;
			}

			// Token: 0x0600883D RID: 34877 RVA: 0x001ECAFA File Offset: 0x001EACFA
			ConstantValue ValueSetFactory.INumericTC<float>.ToConstantValue(float value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x0600883E RID: 34878 RVA: 0x001ECB04 File Offset: 0x001EAD04
			string ValueSetFactory.INumericTC<float>.ToString(float value)
			{
				if (float.IsNaN(value))
				{
					return "NaN";
				}
				if (value == float.NegativeInfinity)
				{
					return "-Inf";
				}
				if (value != float.PositiveInfinity)
				{
					return FormattableString.Invariant(FormattableStringFactory.Create("{0:G9}", new object[] { value }));
				}
				return "Inf";
			}

			// Token: 0x0600883F RID: 34879 RVA: 0x001ECB59 File Offset: 0x001EAD59
			float ValueSetFactory.INumericTC<float>.Prev(float value)
			{
				return -this.Next(-value);
			}

			// Token: 0x06008840 RID: 34880 RVA: 0x001ECB64 File Offset: 0x001EAD64
			float ValueSetFactory.INumericTC<float>.Random(Random random)
			{
				return (float)(random.NextDouble() * 100.0 - 50.0);
			}
		}

		// Token: 0x0200077D RID: 1917
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct StringTC : ValueSetFactory.IEquatableValueTC<string>
		{
			// Token: 0x06008841 RID: 34881 RVA: 0x001ECB81 File Offset: 0x001EAD81
			string ValueSetFactory.IEquatableValueTC<string>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.StringValue;
				}
				return string.Empty;
			}

			// Token: 0x06008842 RID: 34882 RVA: 0x001ECB98 File Offset: 0x001EAD98
			string[] ValueSetFactory.IEquatableValueTC<string>.RandomValues(int count, Random random, int scope)
			{
				string[] array = new string[count];
				int num = 0;
				for (int i = 0; i < scope; i++)
				{
					int num2 = count - num;
					int num3 = scope - i;
					if (random.NextDouble() * (double)num3 < (double)num2)
					{
						array[num++] = i.ToString();
					}
				}
				return array;
			}

			// Token: 0x06008843 RID: 34883 RVA: 0x001ECBE0 File Offset: 0x001EADE0
			ConstantValue ValueSetFactory.IEquatableValueTC<string>.ToConstantValue(string value)
			{
				return ConstantValue.Create(value);
			}
		}

		// Token: 0x0200077E RID: 1918
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct UIntTC : ValueSetFactory.INumericTC<uint>
		{
			// Token: 0x17002729 RID: 10025
			// (get) Token: 0x06008844 RID: 34884 RVA: 0x001ECBE8 File Offset: 0x001EADE8
			uint ValueSetFactory.INumericTC<uint>.MinValue
			{
				get
				{
					return 0U;
				}
			}

			// Token: 0x1700272A RID: 10026
			// (get) Token: 0x06008845 RID: 34885 RVA: 0x001ECBEB File Offset: 0x001EADEB
			uint ValueSetFactory.INumericTC<uint>.MaxValue
			{
				get
				{
					return uint.MaxValue;
				}
			}

			// Token: 0x1700272B RID: 10027
			// (get) Token: 0x06008846 RID: 34886 RVA: 0x001ECBEE File Offset: 0x001EADEE
			uint ValueSetFactory.INumericTC<uint>.Zero
			{
				get
				{
					return 0U;
				}
			}

			// Token: 0x06008847 RID: 34887 RVA: 0x001ECBF4 File Offset: 0x001EADF4
			public bool Related(BinaryOperatorKind relation, uint left, uint right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x06008848 RID: 34888 RVA: 0x001ECC5E File Offset: 0x001EAE5E
			uint ValueSetFactory.INumericTC<uint>.Next(uint value)
			{
				return value + 1U;
			}

			// Token: 0x06008849 RID: 34889 RVA: 0x001ECC63 File Offset: 0x001EAE63
			public uint FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.UInt32Value;
				}
				return 0U;
			}

			// Token: 0x0600884A RID: 34890 RVA: 0x001ECC75 File Offset: 0x001EAE75
			public ConstantValue ToConstantValue(uint value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x0600884B RID: 34891 RVA: 0x001ECC7D File Offset: 0x001EAE7D
			string ValueSetFactory.INumericTC<uint>.ToString(uint value)
			{
				return value.ToString();
			}

			// Token: 0x0600884C RID: 34892 RVA: 0x001ECC86 File Offset: 0x001EAE86
			uint ValueSetFactory.INumericTC<uint>.Prev(uint value)
			{
				return value - 1U;
			}

			// Token: 0x0600884D RID: 34893 RVA: 0x001ECC8B File Offset: 0x001EAE8B
			public uint Random(Random random)
			{
				return (uint)((random.Next() << 10) ^ random.Next());
			}
		}

		// Token: 0x0200077F RID: 1919
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct ULongTC : ValueSetFactory.INumericTC<ulong>
		{
			// Token: 0x1700272C RID: 10028
			// (get) Token: 0x0600884E RID: 34894 RVA: 0x001ECC9D File Offset: 0x001EAE9D
			ulong ValueSetFactory.INumericTC<ulong>.MinValue
			{
				get
				{
					return 0UL;
				}
			}

			// Token: 0x1700272D RID: 10029
			// (get) Token: 0x0600884F RID: 34895 RVA: 0x001ECCA1 File Offset: 0x001EAEA1
			ulong ValueSetFactory.INumericTC<ulong>.MaxValue
			{
				get
				{
					return ulong.MaxValue;
				}
			}

			// Token: 0x1700272E RID: 10030
			// (get) Token: 0x06008850 RID: 34896 RVA: 0x001ECCA5 File Offset: 0x001EAEA5
			ulong ValueSetFactory.INumericTC<ulong>.Zero
			{
				get
				{
					return 0UL;
				}
			}

			// Token: 0x06008851 RID: 34897 RVA: 0x001ECCAC File Offset: 0x001EAEAC
			bool ValueSetFactory.INumericTC<ulong>.Related(BinaryOperatorKind relation, ulong left, ulong right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x06008852 RID: 34898 RVA: 0x001ECD16 File Offset: 0x001EAF16
			ulong ValueSetFactory.INumericTC<ulong>.Next(ulong value)
			{
				return value + 1UL;
			}

			// Token: 0x06008853 RID: 34899 RVA: 0x001ECD1C File Offset: 0x001EAF1C
			ulong ValueSetFactory.INumericTC<ulong>.Prev(ulong value)
			{
				return value - 1UL;
			}

			// Token: 0x06008854 RID: 34900 RVA: 0x001ECD22 File Offset: 0x001EAF22
			ulong ValueSetFactory.INumericTC<ulong>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.UInt64Value;
				}
				return 0UL;
			}

			// Token: 0x06008855 RID: 34901 RVA: 0x001ECD35 File Offset: 0x001EAF35
			ConstantValue ValueSetFactory.INumericTC<ulong>.ToConstantValue(ulong value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x06008856 RID: 34902 RVA: 0x001ECD3D File Offset: 0x001EAF3D
			string ValueSetFactory.INumericTC<ulong>.ToString(ulong value)
			{
				return value.ToString();
			}

			// Token: 0x06008857 RID: 34903 RVA: 0x001ECD46 File Offset: 0x001EAF46
			ulong ValueSetFactory.INumericTC<ulong>.Random(Random random)
			{
				return (ulong)(((long)random.Next() << 35) ^ ((long)random.Next() << 10) ^ (long)random.Next());
			}
		}

		// Token: 0x02000780 RID: 1920
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private struct UShortTC : ValueSetFactory.INumericTC<ushort>
		{
			// Token: 0x1700272F RID: 10031
			// (get) Token: 0x06008858 RID: 34904 RVA: 0x001ECD65 File Offset: 0x001EAF65
			ushort ValueSetFactory.INumericTC<ushort>.MinValue
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17002730 RID: 10032
			// (get) Token: 0x06008859 RID: 34905 RVA: 0x001ECD68 File Offset: 0x001EAF68
			ushort ValueSetFactory.INumericTC<ushort>.MaxValue
			{
				get
				{
					return ushort.MaxValue;
				}
			}

			// Token: 0x17002731 RID: 10033
			// (get) Token: 0x0600885A RID: 34906 RVA: 0x001ECD6F File Offset: 0x001EAF6F
			ushort ValueSetFactory.INumericTC<ushort>.Zero
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600885B RID: 34907 RVA: 0x001ECD74 File Offset: 0x001EAF74
			bool ValueSetFactory.INumericTC<ushort>.Related(BinaryOperatorKind relation, ushort left, ushort right)
			{
				if (relation <= BinaryOperatorKind.GreaterThan)
				{
					if (relation == BinaryOperatorKind.Equal)
					{
						return left == right;
					}
					if (relation == BinaryOperatorKind.GreaterThan)
					{
						return left > right;
					}
				}
				else
				{
					if (relation == BinaryOperatorKind.LessThan)
					{
						return left < right;
					}
					if (relation == BinaryOperatorKind.GreaterThanOrEqual)
					{
						return left >= right;
					}
					if (relation == BinaryOperatorKind.LessThanOrEqual)
					{
						return left <= right;
					}
				}
				throw new ArgumentException("relation");
			}

			// Token: 0x0600885C RID: 34908 RVA: 0x001ECDDE File Offset: 0x001EAFDE
			ushort ValueSetFactory.INumericTC<ushort>.Next(ushort value)
			{
				return value + 1;
			}

			// Token: 0x0600885D RID: 34909 RVA: 0x001ECDE4 File Offset: 0x001EAFE4
			ushort ValueSetFactory.INumericTC<ushort>.FromConstantValue(ConstantValue constantValue)
			{
				if (!constantValue.IsBad)
				{
					return constantValue.UInt16Value;
				}
				return 0;
			}

			// Token: 0x0600885E RID: 34910 RVA: 0x001ECDF6 File Offset: 0x001EAFF6
			ConstantValue ValueSetFactory.INumericTC<ushort>.ToConstantValue(ushort value)
			{
				return ConstantValue.Create(value);
			}

			// Token: 0x0600885F RID: 34911 RVA: 0x001ECDFE File Offset: 0x001EAFFE
			string ValueSetFactory.INumericTC<ushort>.ToString(ushort value)
			{
				return value.ToString();
			}

			// Token: 0x06008860 RID: 34912 RVA: 0x001ECE07 File Offset: 0x001EB007
			ushort ValueSetFactory.INumericTC<ushort>.Prev(ushort value)
			{
				return value - 1;
			}

			// Token: 0x06008861 RID: 34913 RVA: 0x001ECE0D File Offset: 0x001EB00D
			ushort ValueSetFactory.INumericTC<ushort>.Random(Random random)
			{
				return (ushort)random.Next();
			}
		}
	}
}
