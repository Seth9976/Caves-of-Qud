using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000075 RID: 117
	public class Randomizer : Random
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00009736 File Offset: 0x00007936
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000973D File Offset: 0x0000793D
		public static int InitialSeed
		{
			get
			{
				return Randomizer._initialSeed;
			}
			set
			{
				Randomizer._initialSeed = value;
				Randomizer._seedGenerator = new Random(Randomizer._initialSeed);
			}
		} = new Random().Next();

		// Token: 0x06000417 RID: 1047 RVA: 0x00009754 File Offset: 0x00007954
		public static Randomizer GetRandomizer(MemberInfo member)
		{
			if (Randomizer.Randomizers.ContainsKey(member))
			{
				return Randomizer.Randomizers[member];
			}
			Randomizer randomizer = Randomizer.CreateRandomizer();
			Randomizer.Randomizers[member] = randomizer;
			return randomizer;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000978D File Offset: 0x0000798D
		public static Randomizer GetRandomizer(ParameterInfo parameter)
		{
			return Randomizer.GetRandomizer(parameter.Member);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000979A File Offset: 0x0000799A
		public static Randomizer CreateRandomizer()
		{
			return new Randomizer(Randomizer._seedGenerator.Next());
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000097AB File Offset: 0x000079AB
		public Randomizer()
		{
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000097B3 File Offset: 0x000079B3
		public Randomizer(int seed)
			: base(seed)
		{
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000097BC File Offset: 0x000079BC
		[CLSCompliant(false)]
		public uint NextUInt()
		{
			return this.NextUInt(0U, uint.MaxValue);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000097C6 File Offset: 0x000079C6
		[CLSCompliant(false)]
		public uint NextUInt(uint max)
		{
			return this.NextUInt(0U, max);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000097D0 File Offset: 0x000079D0
		[CLSCompliant(false)]
		public uint NextUInt(uint min, uint max)
		{
			Guard.ArgumentInRange(max >= min, "Maximum value must be greater than or equal to minimum.", "max");
			if (min == max)
			{
				return min;
			}
			uint num = max - min;
			uint num2 = uint.MaxValue - uint.MaxValue % num;
			uint num3;
			do
			{
				num3 = this.RawUInt();
			}
			while (num3 > num2);
			return num3 % num + min;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00009813 File Offset: 0x00007A13
		public short NextShort()
		{
			return this.NextShort(0, short.MaxValue);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00009821 File Offset: 0x00007A21
		public short NextShort(short max)
		{
			return this.NextShort(0, max);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000982B File Offset: 0x00007A2B
		public short NextShort(short min, short max)
		{
			return (short)this.Next((int)min, (int)max);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00009836 File Offset: 0x00007A36
		[CLSCompliant(false)]
		public ushort NextUShort()
		{
			return this.NextUShort(0, ushort.MaxValue);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00009844 File Offset: 0x00007A44
		[CLSCompliant(false)]
		public ushort NextUShort(ushort max)
		{
			return this.NextUShort(0, max);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000984E File Offset: 0x00007A4E
		[CLSCompliant(false)]
		public ushort NextUShort(ushort min, ushort max)
		{
			return (ushort)this.Next((int)min, (int)max);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00009859 File Offset: 0x00007A59
		public long NextLong()
		{
			return this.NextLong(0L, long.MaxValue);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000986C File Offset: 0x00007A6C
		public long NextLong(long max)
		{
			return this.NextLong(0L, max);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00009878 File Offset: 0x00007A78
		public long NextLong(long min, long max)
		{
			Guard.ArgumentInRange(max >= min, "Maximum value must be greater than or equal to minimum.", "max");
			if (min == max)
			{
				return min;
			}
			ulong num = (ulong)(max - min);
			ulong num2 = ulong.MaxValue - ulong.MaxValue % num;
			ulong num3;
			do
			{
				num3 = this.RawULong();
			}
			while (num3 > num2);
			return (long)(num3 % num + (ulong)min);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000098BD File Offset: 0x00007ABD
		[CLSCompliant(false)]
		public ulong NextULong()
		{
			return this.NextULong(0UL, ulong.MaxValue);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000098C9 File Offset: 0x00007AC9
		[CLSCompliant(false)]
		public ulong NextULong(ulong max)
		{
			return this.NextULong(0UL, max);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000098D4 File Offset: 0x00007AD4
		[CLSCompliant(false)]
		public ulong NextULong(ulong min, ulong max)
		{
			Guard.ArgumentInRange(max >= min, "Maximum value must be greater than or equal to minimum.", "max");
			ulong num = max - min;
			if (num == 0UL)
			{
				return min;
			}
			ulong num2 = ulong.MaxValue - ulong.MaxValue % num;
			ulong num3;
			do
			{
				num3 = this.RawULong();
			}
			while (num3 > num2);
			return num3 % num + min;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00009918 File Offset: 0x00007B18
		public byte NextByte()
		{
			return this.NextByte(0, byte.MaxValue);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00009926 File Offset: 0x00007B26
		public byte NextByte(byte max)
		{
			return this.NextByte(0, max);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00009930 File Offset: 0x00007B30
		public byte NextByte(byte min, byte max)
		{
			return (byte)this.Next((int)min, (int)max);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000993B File Offset: 0x00007B3B
		[CLSCompliant(false)]
		public sbyte NextSByte()
		{
			return this.NextSByte(0, sbyte.MaxValue);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00009946 File Offset: 0x00007B46
		[CLSCompliant(false)]
		public sbyte NextSByte(sbyte max)
		{
			return this.NextSByte(0, max);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00009950 File Offset: 0x00007B50
		[CLSCompliant(false)]
		public sbyte NextSByte(sbyte min, sbyte max)
		{
			return (sbyte)this.Next((int)min, (int)max);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000995B File Offset: 0x00007B5B
		public bool NextBool()
		{
			return this.NextDouble() < 0.5;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000996E File Offset: 0x00007B6E
		public bool NextBool(double probability)
		{
			Guard.ArgumentInRange(probability >= 0.0 && probability <= 1.0, "Probability must be from 0.0 to 1.0", "probability");
			return this.NextDouble() < probability;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000099A6 File Offset: 0x00007BA6
		public double NextDouble(double max)
		{
			return this.NextDouble() * max;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000099B0 File Offset: 0x00007BB0
		public double NextDouble(double min, double max)
		{
			Guard.ArgumentInRange(max >= min, "Maximum value must be greater than or equal to minimum.", "max");
			if (max == min)
			{
				return min;
			}
			double num = max - min;
			return this.NextDouble() * num + min;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000099E7 File Offset: 0x00007BE7
		public float NextFloat()
		{
			return (float)this.NextDouble();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000099F0 File Offset: 0x00007BF0
		public float NextFloat(float max)
		{
			return (float)this.NextDouble((double)max);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000099FB File Offset: 0x00007BFB
		public float NextFloat(float min, float max)
		{
			return (float)this.NextDouble((double)min, (double)max);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00009A08 File Offset: 0x00007C08
		public object NextEnum(Type type)
		{
			Array enumValues = TypeHelper.GetEnumValues(type);
			return enumValues.GetValue(this.Next(0, enumValues.Length));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00009A2F File Offset: 0x00007C2F
		public T NextEnum<T>()
		{
			return (T)((object)this.NextEnum(typeof(T)));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00009A48 File Offset: 0x00007C48
		public string GetString(int outputLength, string allowedChars)
		{
			StringBuilder stringBuilder = new StringBuilder(outputLength);
			for (int i = 0; i < outputLength; i++)
			{
				stringBuilder.Append(allowedChars[this.Next(0, allowedChars.Length)]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00009A88 File Offset: 0x00007C88
		public string GetString(int outputLength)
		{
			return this.GetString(outputLength, "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_");
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00009A96 File Offset: 0x00007C96
		public string GetString()
		{
			return this.GetString(25, "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_");
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public decimal NextDecimal()
		{
			int num = this.Next(0, int.MaxValue);
			int num2 = this.Next(0, int.MaxValue);
			int num3 = this.Next(0, int.MaxValue);
			return new decimal(num, num2, num3, false, 0);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public decimal NextDecimal(decimal max)
		{
			return this.NextDecimal() % max;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public decimal NextDecimal(decimal min, decimal max)
		{
			Guard.ArgumentInRange(max >= min, "Maximum value must be greater than or equal to minimum.", "max");
			Guard.ArgumentValid(max < 0m == min < 0m || min + decimal.MaxValue >= max, "Range too great for decimal data, use double range", "max");
			if (min == max)
			{
				return min;
			}
			decimal num = max - min;
			decimal num2 = decimal.MaxValue - decimal.MaxValue % num;
			decimal num3;
			do
			{
				num3 = this.NextDecimal();
			}
			while (num3 > num2);
			return num3 % num + min;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00009BA8 File Offset: 0x00007DA8
		private uint RawUInt()
		{
			byte[] array = new byte[4];
			this.NextBytes(array);
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00009BCC File Offset: 0x00007DCC
		private uint RawUShort()
		{
			byte[] array = new byte[4];
			this.NextBytes(array);
			return BitConverter.ToUInt32(array, 0);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00009BF0 File Offset: 0x00007DF0
		private ulong RawULong()
		{
			byte[] array = new byte[8];
			this.NextBytes(array);
			return BitConverter.ToUInt64(array, 0);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00009C14 File Offset: 0x00007E14
		private long RawLong()
		{
			byte[] array = new byte[8];
			this.NextBytes(array);
			return BitConverter.ToInt64(array, 0);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00009C38 File Offset: 0x00007E38
		private decimal RawDecimal()
		{
			int num = this.Next(0, int.MaxValue);
			int num2 = this.Next(0, int.MaxValue);
			int num3 = this.Next(0, int.MaxValue);
			bool flag = this.NextBool();
			byte b = this.NextByte(29);
			return new decimal(num, num2, num3, flag, b);
		}

		// Token: 0x040000B0 RID: 176
		private static Random _seedGenerator;

		// Token: 0x040000B1 RID: 177
		private static int _initialSeed;

		// Token: 0x040000B2 RID: 178
		private static Dictionary<MemberInfo, Randomizer> Randomizers = new Dictionary<MemberInfo, Randomizer>();

		// Token: 0x040000B3 RID: 179
		public const string DefaultStringChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789_";

		// Token: 0x040000B4 RID: 180
		private const int DefaultStringLength = 25;
	}
}
