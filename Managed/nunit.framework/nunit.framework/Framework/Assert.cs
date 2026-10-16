using System;
using System.Collections;
using System.ComponentModel;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200000B RID: 11
	public class Assert
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000025DC File Offset: 0x000007DC
		public static void Greater(int arg1, int arg2, string message, params object[] args)
		{
			Assert.That<int>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025F1 File Offset: 0x000007F1
		public static void Greater(int arg1, int arg2)
		{
			Assert.That<int>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002606 File Offset: 0x00000806
		[CLSCompliant(false)]
		public static void Greater(uint arg1, uint arg2, string message, params object[] args)
		{
			Assert.That<uint>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000261B File Offset: 0x0000081B
		[CLSCompliant(false)]
		public static void Greater(uint arg1, uint arg2)
		{
			Assert.That<uint>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002630 File Offset: 0x00000830
		public static void Greater(long arg1, long arg2, string message, params object[] args)
		{
			Assert.That<long>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002645 File Offset: 0x00000845
		public static void Greater(long arg1, long arg2)
		{
			Assert.That<long>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000265A File Offset: 0x0000085A
		[CLSCompliant(false)]
		public static void Greater(ulong arg1, ulong arg2, string message, params object[] args)
		{
			Assert.That<ulong>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000266F File Offset: 0x0000086F
		[CLSCompliant(false)]
		public static void Greater(ulong arg1, ulong arg2)
		{
			Assert.That<ulong>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002684 File Offset: 0x00000884
		public static void Greater(decimal arg1, decimal arg2, string message, params object[] args)
		{
			Assert.That<decimal>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002699 File Offset: 0x00000899
		public static void Greater(decimal arg1, decimal arg2)
		{
			Assert.That<decimal>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026AE File Offset: 0x000008AE
		public static void Greater(double arg1, double arg2, string message, params object[] args)
		{
			Assert.That<double>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026C3 File Offset: 0x000008C3
		public static void Greater(double arg1, double arg2)
		{
			Assert.That<double>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026D8 File Offset: 0x000008D8
		public static void Greater(float arg1, float arg2, string message, params object[] args)
		{
			Assert.That<float>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026ED File Offset: 0x000008ED
		public static void Greater(float arg1, float arg2)
		{
			Assert.That<float>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002702 File Offset: 0x00000902
		public static void Greater(IComparable arg1, IComparable arg2, string message, params object[] args)
		{
			Assert.That<IComparable>(arg1, Is.GreaterThan(arg2), message, args);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002712 File Offset: 0x00000912
		public static void Greater(IComparable arg1, IComparable arg2)
		{
			Assert.That<IComparable>(arg1, Is.GreaterThan(arg2), null, null);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002722 File Offset: 0x00000922
		public static void Less(int arg1, int arg2, string message, params object[] args)
		{
			Assert.That<int>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002737 File Offset: 0x00000937
		public static void Less(int arg1, int arg2)
		{
			Assert.That<int>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000274C File Offset: 0x0000094C
		[CLSCompliant(false)]
		public static void Less(uint arg1, uint arg2, string message, params object[] args)
		{
			Assert.That<uint>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002761 File Offset: 0x00000961
		[CLSCompliant(false)]
		public static void Less(uint arg1, uint arg2)
		{
			Assert.That<uint>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002776 File Offset: 0x00000976
		public static void Less(long arg1, long arg2, string message, params object[] args)
		{
			Assert.That<long>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000278B File Offset: 0x0000098B
		public static void Less(long arg1, long arg2)
		{
			Assert.That<long>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027A0 File Offset: 0x000009A0
		[CLSCompliant(false)]
		public static void Less(ulong arg1, ulong arg2, string message, params object[] args)
		{
			Assert.That<ulong>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027B5 File Offset: 0x000009B5
		[CLSCompliant(false)]
		public static void Less(ulong arg1, ulong arg2)
		{
			Assert.That<ulong>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027CA File Offset: 0x000009CA
		public static void Less(decimal arg1, decimal arg2, string message, params object[] args)
		{
			Assert.That<decimal>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027DF File Offset: 0x000009DF
		public static void Less(decimal arg1, decimal arg2)
		{
			Assert.That<decimal>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027F4 File Offset: 0x000009F4
		public static void Less(double arg1, double arg2, string message, params object[] args)
		{
			Assert.That<double>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002809 File Offset: 0x00000A09
		public static void Less(double arg1, double arg2)
		{
			Assert.That<double>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000281E File Offset: 0x00000A1E
		public static void Less(float arg1, float arg2, string message, params object[] args)
		{
			Assert.That<float>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002833 File Offset: 0x00000A33
		public static void Less(float arg1, float arg2)
		{
			Assert.That<float>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002848 File Offset: 0x00000A48
		public static void Less(IComparable arg1, IComparable arg2, string message, params object[] args)
		{
			Assert.That<IComparable>(arg1, Is.LessThan(arg2), message, args);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002858 File Offset: 0x00000A58
		public static void Less(IComparable arg1, IComparable arg2)
		{
			Assert.That<IComparable>(arg1, Is.LessThan(arg2), null, null);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002868 File Offset: 0x00000A68
		public static void GreaterOrEqual(int arg1, int arg2, string message, params object[] args)
		{
			Assert.That<int>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000287D File Offset: 0x00000A7D
		public static void GreaterOrEqual(int arg1, int arg2)
		{
			Assert.That<int>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002892 File Offset: 0x00000A92
		[CLSCompliant(false)]
		public static void GreaterOrEqual(uint arg1, uint arg2, string message, params object[] args)
		{
			Assert.That<uint>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028A7 File Offset: 0x00000AA7
		[CLSCompliant(false)]
		public static void GreaterOrEqual(uint arg1, uint arg2)
		{
			Assert.That<uint>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028BC File Offset: 0x00000ABC
		public static void GreaterOrEqual(long arg1, long arg2, string message, params object[] args)
		{
			Assert.That<long>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028D1 File Offset: 0x00000AD1
		public static void GreaterOrEqual(long arg1, long arg2)
		{
			Assert.That<long>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028E6 File Offset: 0x00000AE6
		[CLSCompliant(false)]
		public static void GreaterOrEqual(ulong arg1, ulong arg2, string message, params object[] args)
		{
			Assert.That<ulong>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028FB File Offset: 0x00000AFB
		[CLSCompliant(false)]
		public static void GreaterOrEqual(ulong arg1, ulong arg2)
		{
			Assert.That<ulong>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002910 File Offset: 0x00000B10
		public static void GreaterOrEqual(decimal arg1, decimal arg2, string message, params object[] args)
		{
			Assert.That<decimal>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002925 File Offset: 0x00000B25
		public static void GreaterOrEqual(decimal arg1, decimal arg2)
		{
			Assert.That<decimal>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000293A File Offset: 0x00000B3A
		public static void GreaterOrEqual(double arg1, double arg2, string message, params object[] args)
		{
			Assert.That<double>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000294F File Offset: 0x00000B4F
		public static void GreaterOrEqual(double arg1, double arg2)
		{
			Assert.That<double>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002964 File Offset: 0x00000B64
		public static void GreaterOrEqual(float arg1, float arg2, string message, params object[] args)
		{
			Assert.That<float>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002979 File Offset: 0x00000B79
		public static void GreaterOrEqual(float arg1, float arg2)
		{
			Assert.That<float>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000298E File Offset: 0x00000B8E
		public static void GreaterOrEqual(IComparable arg1, IComparable arg2, string message, params object[] args)
		{
			Assert.That<IComparable>(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000299E File Offset: 0x00000B9E
		public static void GreaterOrEqual(IComparable arg1, IComparable arg2)
		{
			Assert.That<IComparable>(arg1, Is.GreaterThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029AE File Offset: 0x00000BAE
		public static void LessOrEqual(int arg1, int arg2, string message, params object[] args)
		{
			Assert.That<int>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029C3 File Offset: 0x00000BC3
		public static void LessOrEqual(int arg1, int arg2)
		{
			Assert.That<int>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029D8 File Offset: 0x00000BD8
		[CLSCompliant(false)]
		public static void LessOrEqual(uint arg1, uint arg2, string message, params object[] args)
		{
			Assert.That<uint>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029ED File Offset: 0x00000BED
		[CLSCompliant(false)]
		public static void LessOrEqual(uint arg1, uint arg2)
		{
			Assert.That<uint>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A02 File Offset: 0x00000C02
		public static void LessOrEqual(long arg1, long arg2, string message, params object[] args)
		{
			Assert.That<long>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A17 File Offset: 0x00000C17
		public static void LessOrEqual(long arg1, long arg2)
		{
			Assert.That<long>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A2C File Offset: 0x00000C2C
		[CLSCompliant(false)]
		public static void LessOrEqual(ulong arg1, ulong arg2, string message, params object[] args)
		{
			Assert.That<ulong>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A41 File Offset: 0x00000C41
		[CLSCompliant(false)]
		public static void LessOrEqual(ulong arg1, ulong arg2)
		{
			Assert.That<ulong>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A56 File Offset: 0x00000C56
		public static void LessOrEqual(decimal arg1, decimal arg2, string message, params object[] args)
		{
			Assert.That<decimal>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A6B File Offset: 0x00000C6B
		public static void LessOrEqual(decimal arg1, decimal arg2)
		{
			Assert.That<decimal>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A80 File Offset: 0x00000C80
		public static void LessOrEqual(double arg1, double arg2, string message, params object[] args)
		{
			Assert.That<double>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A95 File Offset: 0x00000C95
		public static void LessOrEqual(double arg1, double arg2)
		{
			Assert.That<double>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AAA File Offset: 0x00000CAA
		public static void LessOrEqual(float arg1, float arg2, string message, params object[] args)
		{
			Assert.That<float>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002ABF File Offset: 0x00000CBF
		public static void LessOrEqual(float arg1, float arg2)
		{
			Assert.That<float>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public static void LessOrEqual(IComparable arg1, IComparable arg2, string message, params object[] args)
		{
			Assert.That<IComparable>(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002AE4 File Offset: 0x00000CE4
		public static void LessOrEqual(IComparable arg1, IComparable arg2)
		{
			Assert.That<IComparable>(arg1, Is.LessThanOrEqualTo(arg2), null, null);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static void True(bool? condition, string message, params object[] args)
		{
			Assert.That<bool?>(condition, Is.True, message, args);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B03 File Offset: 0x00000D03
		public static void True(bool condition, string message, params object[] args)
		{
			Assert.That<bool>(condition, Is.True, message, args);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B12 File Offset: 0x00000D12
		public static void True(bool? condition)
		{
			Assert.That<bool?>(condition, Is.True, null, null);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B21 File Offset: 0x00000D21
		public static void True(bool condition)
		{
			Assert.That<bool>(condition, Is.True, null, null);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static void IsTrue(bool? condition, string message, params object[] args)
		{
			Assert.That<bool?>(condition, Is.True, message, args);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B03 File Offset: 0x00000D03
		public static void IsTrue(bool condition, string message, params object[] args)
		{
			Assert.That<bool>(condition, Is.True, message, args);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B12 File Offset: 0x00000D12
		public static void IsTrue(bool? condition)
		{
			Assert.That<bool?>(condition, Is.True, null, null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B21 File Offset: 0x00000D21
		public static void IsTrue(bool condition)
		{
			Assert.That<bool>(condition, Is.True, null, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B30 File Offset: 0x00000D30
		public static void False(bool? condition, string message, params object[] args)
		{
			Assert.That<bool?>(condition, Is.False, message, args);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B3F File Offset: 0x00000D3F
		public static void False(bool condition, string message, params object[] args)
		{
			Assert.That<bool>(condition, Is.False, message, args);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B4E File Offset: 0x00000D4E
		public static void False(bool? condition)
		{
			Assert.That<bool?>(condition, Is.False, null, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B5D File Offset: 0x00000D5D
		public static void False(bool condition)
		{
			Assert.That<bool>(condition, Is.False, null, null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B30 File Offset: 0x00000D30
		public static void IsFalse(bool? condition, string message, params object[] args)
		{
			Assert.That<bool?>(condition, Is.False, message, args);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B3F File Offset: 0x00000D3F
		public static void IsFalse(bool condition, string message, params object[] args)
		{
			Assert.That<bool>(condition, Is.False, message, args);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B4E File Offset: 0x00000D4E
		public static void IsFalse(bool? condition)
		{
			Assert.That<bool?>(condition, Is.False, null, null);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B5D File Offset: 0x00000D5D
		public static void IsFalse(bool condition)
		{
			Assert.That<bool>(condition, Is.False, null, null);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B6C File Offset: 0x00000D6C
		public static void NotNull(object anObject, string message, params object[] args)
		{
			Assert.That<object>(anObject, Is.Not.Null, message, args);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B80 File Offset: 0x00000D80
		public static void NotNull(object anObject)
		{
			Assert.That<object>(anObject, Is.Not.Null, null, null);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B6C File Offset: 0x00000D6C
		public static void IsNotNull(object anObject, string message, params object[] args)
		{
			Assert.That<object>(anObject, Is.Not.Null, message, args);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002B80 File Offset: 0x00000D80
		public static void IsNotNull(object anObject)
		{
			Assert.That<object>(anObject, Is.Not.Null, null, null);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B94 File Offset: 0x00000D94
		public static void Null(object anObject, string message, params object[] args)
		{
			Assert.That<object>(anObject, Is.Null, message, args);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public static void Null(object anObject)
		{
			Assert.That<object>(anObject, Is.Null, null, null);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B94 File Offset: 0x00000D94
		public static void IsNull(object anObject, string message, params object[] args)
		{
			Assert.That<object>(anObject, Is.Null, message, args);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BA3 File Offset: 0x00000DA3
		public static void IsNull(object anObject)
		{
			Assert.That<object>(anObject, Is.Null, null, null);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002BB2 File Offset: 0x00000DB2
		public static void IsNaN(double aDouble, string message, params object[] args)
		{
			Assert.That<double>(aDouble, Is.NaN, message, args);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BC1 File Offset: 0x00000DC1
		public static void IsNaN(double aDouble)
		{
			Assert.That<double>(aDouble, Is.NaN, null, null);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public static void IsNaN(double? aDouble, string message, params object[] args)
		{
			Assert.That<double?>(aDouble, Is.NaN, message, args);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002BDF File Offset: 0x00000DDF
		public static void IsNaN(double? aDouble)
		{
			Assert.That<double?>(aDouble, Is.NaN, null, null);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002BEE File Offset: 0x00000DEE
		public static void IsEmpty(string aString, string message, params object[] args)
		{
			Assert.That<string>(aString, new EmptyStringConstraint(), message, args);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002BFD File Offset: 0x00000DFD
		public static void IsEmpty(string aString)
		{
			Assert.That<string>(aString, new EmptyStringConstraint(), null, null);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static void IsEmpty(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, new EmptyCollectionConstraint(), message, args);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002C1B File Offset: 0x00000E1B
		public static void IsEmpty(IEnumerable collection)
		{
			Assert.That<IEnumerable>(collection, new EmptyCollectionConstraint(), null, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C2A File Offset: 0x00000E2A
		public static void IsNotEmpty(string aString, string message, params object[] args)
		{
			Assert.That<string>(aString, Is.Not.Empty, message, args);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002C3E File Offset: 0x00000E3E
		public static void IsNotEmpty(string aString)
		{
			Assert.That<string>(aString, Is.Not.Empty, null, null);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002C52 File Offset: 0x00000E52
		public static void IsNotEmpty(IEnumerable collection, string message, params object[] args)
		{
			Assert.That<IEnumerable>(collection, Is.Not.Empty, message, args);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002C66 File Offset: 0x00000E66
		public static void IsNotEmpty(IEnumerable collection)
		{
			Assert.That<IEnumerable>(collection, Is.Not.Empty, null, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002C7A File Offset: 0x00000E7A
		public static void Zero(int actual)
		{
			Assert.That<int>(actual, Is.Zero);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002C87 File Offset: 0x00000E87
		public static void Zero(int actual, string message, params object[] args)
		{
			Assert.That<int>(actual, Is.Zero, message, args);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002C96 File Offset: 0x00000E96
		[CLSCompliant(false)]
		public static void Zero(uint actual)
		{
			Assert.That<uint>(actual, Is.Zero);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002CA3 File Offset: 0x00000EA3
		[CLSCompliant(false)]
		public static void Zero(uint actual, string message, params object[] args)
		{
			Assert.That<uint>(actual, Is.Zero, message, args);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002CB2 File Offset: 0x00000EB2
		public static void Zero(long actual)
		{
			Assert.That<long>(actual, Is.Zero);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002CBF File Offset: 0x00000EBF
		public static void Zero(long actual, string message, params object[] args)
		{
			Assert.That<long>(actual, Is.Zero, message, args);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002CCE File Offset: 0x00000ECE
		[CLSCompliant(false)]
		public static void Zero(ulong actual)
		{
			Assert.That<ulong>(actual, Is.Zero);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002CDB File Offset: 0x00000EDB
		[CLSCompliant(false)]
		public static void Zero(ulong actual, string message, params object[] args)
		{
			Assert.That<ulong>(actual, Is.Zero, message, args);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002CEA File Offset: 0x00000EEA
		public static void Zero(decimal actual)
		{
			Assert.That<decimal>(actual, Is.Zero);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public static void Zero(decimal actual, string message, params object[] args)
		{
			Assert.That<decimal>(actual, Is.Zero, message, args);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002D06 File Offset: 0x00000F06
		public static void Zero(double actual)
		{
			Assert.That<double>(actual, Is.Zero);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002D13 File Offset: 0x00000F13
		public static void Zero(double actual, string message, params object[] args)
		{
			Assert.That<double>(actual, Is.Zero, message, args);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002D22 File Offset: 0x00000F22
		public static void Zero(float actual)
		{
			Assert.That<float>(actual, Is.Zero);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002D2F File Offset: 0x00000F2F
		public static void Zero(float actual, string message, params object[] args)
		{
			Assert.That<float>(actual, Is.Zero, message, args);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002D3E File Offset: 0x00000F3E
		public static void NotZero(int actual)
		{
			Assert.That<int>(actual, Is.Not.Zero);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002D50 File Offset: 0x00000F50
		public static void NotZero(int actual, string message, params object[] args)
		{
			Assert.That<int>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002D64 File Offset: 0x00000F64
		[CLSCompliant(false)]
		public static void NotZero(uint actual)
		{
			Assert.That<uint>(actual, Is.Not.Zero);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002D76 File Offset: 0x00000F76
		[CLSCompliant(false)]
		public static void NotZero(uint actual, string message, params object[] args)
		{
			Assert.That<uint>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002D8A File Offset: 0x00000F8A
		public static void NotZero(long actual)
		{
			Assert.That<long>(actual, Is.Not.Zero);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002D9C File Offset: 0x00000F9C
		public static void NotZero(long actual, string message, params object[] args)
		{
			Assert.That<long>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002DB0 File Offset: 0x00000FB0
		[CLSCompliant(false)]
		public static void NotZero(ulong actual)
		{
			Assert.That<ulong>(actual, Is.Not.Zero);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002DC2 File Offset: 0x00000FC2
		[CLSCompliant(false)]
		public static void NotZero(ulong actual, string message, params object[] args)
		{
			Assert.That<ulong>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002DD6 File Offset: 0x00000FD6
		public static void NotZero(decimal actual)
		{
			Assert.That<decimal>(actual, Is.Not.Zero);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public static void NotZero(decimal actual, string message, params object[] args)
		{
			Assert.That<decimal>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002DFC File Offset: 0x00000FFC
		public static void NotZero(double actual)
		{
			Assert.That<double>(actual, Is.Not.Zero);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002E0E File Offset: 0x0000100E
		public static void NotZero(double actual, string message, params object[] args)
		{
			Assert.That<double>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002E22 File Offset: 0x00001022
		public static void NotZero(float actual)
		{
			Assert.That<float>(actual, Is.Not.Zero);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002E34 File Offset: 0x00001034
		public static void NotZero(float actual, string message, params object[] args)
		{
			Assert.That<float>(actual, Is.Not.Zero, message, args);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002E48 File Offset: 0x00001048
		public static void Positive(int actual)
		{
			Assert.That<int>(actual, Is.Positive);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002E55 File Offset: 0x00001055
		public static void Positive(int actual, string message, params object[] args)
		{
			Assert.That<int>(actual, Is.Positive, message, args);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002E64 File Offset: 0x00001064
		[CLSCompliant(false)]
		public static void Positive(uint actual)
		{
			Assert.That<uint>(actual, Is.Positive);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002E71 File Offset: 0x00001071
		[CLSCompliant(false)]
		public static void Positive(uint actual, string message, params object[] args)
		{
			Assert.That<uint>(actual, Is.Positive, message, args);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002E80 File Offset: 0x00001080
		public static void Positive(long actual)
		{
			Assert.That<long>(actual, Is.Positive);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002E8D File Offset: 0x0000108D
		public static void Positive(long actual, string message, params object[] args)
		{
			Assert.That<long>(actual, Is.Positive, message, args);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002E9C File Offset: 0x0000109C
		[CLSCompliant(false)]
		public static void Positive(ulong actual)
		{
			Assert.That<ulong>(actual, Is.Positive);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002EA9 File Offset: 0x000010A9
		[CLSCompliant(false)]
		public static void Positive(ulong actual, string message, params object[] args)
		{
			Assert.That<ulong>(actual, Is.Positive, message, args);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002EB8 File Offset: 0x000010B8
		public static void Positive(decimal actual)
		{
			Assert.That<decimal>(actual, Is.Positive);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002EC5 File Offset: 0x000010C5
		public static void Positive(decimal actual, string message, params object[] args)
		{
			Assert.That<decimal>(actual, Is.Positive, message, args);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002ED4 File Offset: 0x000010D4
		public static void Positive(double actual)
		{
			Assert.That<double>(actual, Is.Positive);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002EE1 File Offset: 0x000010E1
		public static void Positive(double actual, string message, params object[] args)
		{
			Assert.That<double>(actual, Is.Positive, message, args);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static void Positive(float actual)
		{
			Assert.That<float>(actual, Is.Positive);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002EFD File Offset: 0x000010FD
		public static void Positive(float actual, string message, params object[] args)
		{
			Assert.That<float>(actual, Is.Positive, message, args);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002F0C File Offset: 0x0000110C
		public static void Negative(int actual)
		{
			Assert.That<int>(actual, Is.Negative);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002F19 File Offset: 0x00001119
		public static void Negative(int actual, string message, params object[] args)
		{
			Assert.That<int>(actual, Is.Negative, message, args);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002F28 File Offset: 0x00001128
		[CLSCompliant(false)]
		public static void Negative(uint actual)
		{
			Assert.That<uint>(actual, Is.Negative);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002F35 File Offset: 0x00001135
		[CLSCompliant(false)]
		public static void Negative(uint actual, string message, params object[] args)
		{
			Assert.That<uint>(actual, Is.Negative, message, args);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002F44 File Offset: 0x00001144
		public static void Negative(long actual)
		{
			Assert.That<long>(actual, Is.Negative);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002F51 File Offset: 0x00001151
		public static void Negative(long actual, string message, params object[] args)
		{
			Assert.That<long>(actual, Is.Negative, message, args);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002F60 File Offset: 0x00001160
		[CLSCompliant(false)]
		public static void Negative(ulong actual)
		{
			Assert.That<ulong>(actual, Is.Negative);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002F6D File Offset: 0x0000116D
		[CLSCompliant(false)]
		public static void Negative(ulong actual, string message, params object[] args)
		{
			Assert.That<ulong>(actual, Is.Negative, message, args);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002F7C File Offset: 0x0000117C
		public static void Negative(decimal actual)
		{
			Assert.That<decimal>(actual, Is.Negative);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002F89 File Offset: 0x00001189
		public static void Negative(decimal actual, string message, params object[] args)
		{
			Assert.That<decimal>(actual, Is.Negative, message, args);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002F98 File Offset: 0x00001198
		public static void Negative(double actual)
		{
			Assert.That<double>(actual, Is.Negative);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002FA5 File Offset: 0x000011A5
		public static void Negative(double actual, string message, params object[] args)
		{
			Assert.That<double>(actual, Is.Negative, message, args);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00002FB4 File Offset: 0x000011B4
		public static void Negative(float actual)
		{
			Assert.That<float>(actual, Is.Negative);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002FC1 File Offset: 0x000011C1
		public static void Negative(float actual, string message, params object[] args)
		{
			Assert.That<float>(actual, Is.Negative, message, args);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002050 File Offset: 0x00000250
		protected Assert()
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002FD0 File Offset: 0x000011D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object a, object b)
		{
			throw new InvalidOperationException("Assert.Equals should not be used for Assertions");
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002FDC File Offset: 0x000011DC
		public new static void ReferenceEquals(object a, object b)
		{
			throw new InvalidOperationException("Assert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002FE8 File Offset: 0x000011E8
		public static void Pass(string message, params object[] args)
		{
			if (message == null)
			{
				message = string.Empty;
			}
			else if (args != null && args.Length != 0)
			{
				message = string.Format(message, args);
			}
			throw new SuccessException(message);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000300C File Offset: 0x0000120C
		public static void Pass(string message)
		{
			Assert.Pass(message, null);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003015 File Offset: 0x00001215
		public static void Pass()
		{
			Assert.Pass(string.Empty, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003022 File Offset: 0x00001222
		public static void Fail(string message, params object[] args)
		{
			if (message == null)
			{
				message = string.Empty;
			}
			else if (args != null && args.Length != 0)
			{
				message = string.Format(message, args);
			}
			throw new AssertionException(message);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003046 File Offset: 0x00001246
		public static void Fail(string message)
		{
			Assert.Fail(message, null);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000304F File Offset: 0x0000124F
		public static void Fail()
		{
			Assert.Fail(string.Empty, null);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000305C File Offset: 0x0000125C
		public static void Ignore(string message, params object[] args)
		{
			if (message == null)
			{
				message = string.Empty;
			}
			else if (args != null && args.Length != 0)
			{
				message = string.Format(message, args);
			}
			throw new IgnoreException(message);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003080 File Offset: 0x00001280
		public static void Ignore(string message)
		{
			Assert.Ignore(message, null);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003089 File Offset: 0x00001289
		public static void Ignore()
		{
			Assert.Ignore(string.Empty, null);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003096 File Offset: 0x00001296
		public static void Inconclusive(string message, params object[] args)
		{
			if (message == null)
			{
				message = string.Empty;
			}
			else if (args != null && args.Length != 0)
			{
				message = string.Format(message, args);
			}
			throw new InconclusiveException(message);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000030BA File Offset: 0x000012BA
		public static void Inconclusive(string message)
		{
			Assert.Inconclusive(message, null);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000030C3 File Offset: 0x000012C3
		public static void Inconclusive()
		{
			Assert.Inconclusive(string.Empty, null);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000030D0 File Offset: 0x000012D0
		public static void Contains(object expected, ICollection actual, string message, params object[] args)
		{
			Assert.That<ICollection>(actual, new CollectionContainsConstraint(expected), message, args);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000030E0 File Offset: 0x000012E0
		public static void Contains(object expected, ICollection actual)
		{
			Assert.That<ICollection>(actual, new CollectionContainsConstraint(expected), null, null);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000030F0 File Offset: 0x000012F0
		public static void AreEqual(double expected, double actual, double delta, string message, params object[] args)
		{
			Assert.AssertDoublesAreEqual(expected, actual, delta, message, args);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000030FD File Offset: 0x000012FD
		public static void AreEqual(double expected, double actual, double delta)
		{
			Assert.AssertDoublesAreEqual(expected, actual, delta, null, null);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003109 File Offset: 0x00001309
		public static void AreEqual(double expected, double? actual, double delta, string message, params object[] args)
		{
			Assert.AssertDoublesAreEqual(expected, actual.Value, delta, message, args);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000311D File Offset: 0x0000131D
		public static void AreEqual(double expected, double? actual, double delta)
		{
			Assert.AssertDoublesAreEqual(expected, actual.Value, delta, null, null);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003130 File Offset: 0x00001330
		public static void AreEqual(object expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.EqualTo(expected), message, args);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003140 File Offset: 0x00001340
		public static void AreEqual(object expected, object actual)
		{
			Assert.That<object>(actual, Is.EqualTo(expected), null, null);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003150 File Offset: 0x00001350
		public static void AreNotEqual(object expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.Not.EqualTo(expected), message, args);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003165 File Offset: 0x00001365
		public static void AreNotEqual(object expected, object actual)
		{
			Assert.That<object>(actual, Is.Not.EqualTo(expected), null, null);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000317A File Offset: 0x0000137A
		public static void AreSame(object expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.SameAs(expected), message, args);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000318A File Offset: 0x0000138A
		public static void AreSame(object expected, object actual)
		{
			Assert.That<object>(actual, Is.SameAs(expected), null, null);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000319A File Offset: 0x0000139A
		public static void AreNotSame(object expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.Not.SameAs(expected), message, args);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000031AF File Offset: 0x000013AF
		public static void AreNotSame(object expected, object actual)
		{
			Assert.That<object>(actual, Is.Not.SameAs(expected), null, null);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000031C4 File Offset: 0x000013C4
		protected static void AssertDoublesAreEqual(double expected, double actual, double delta, string message, object[] args)
		{
			if (double.IsNaN(expected) || double.IsInfinity(expected))
			{
				Assert.That<double>(actual, Is.EqualTo(expected), message, args);
				return;
			}
			Assert.That<double>(actual, Is.EqualTo(expected).Within(delta), message, args);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003215 File Offset: 0x00001415
		private static void IncrementAssertCount()
		{
			TestExecutionContext.CurrentContext.IncrementAssertCount();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003224 File Offset: 0x00001424
		public static Exception ThrowsAsync(IResolveConstraint expression, AsyncTestDelegate code, string message, params object[] args)
		{
			Exception ex = null;
			using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(code))
			{
				try
				{
					Task task = code();
					asyncInvocationRegion.WaitForPendingOperationsToComplete(task);
				}
				catch (Exception ex)
				{
				}
			}
			Assert.That<Exception>(ex, expression, message, args);
			return ex;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003280 File Offset: 0x00001480
		public static Exception ThrowsAsync(IResolveConstraint expression, AsyncTestDelegate code)
		{
			return Assert.ThrowsAsync(expression, code, string.Empty, null);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000328F File Offset: 0x0000148F
		public static Exception ThrowsAsync(Type expectedExceptionType, AsyncTestDelegate code, string message, params object[] args)
		{
			return Assert.ThrowsAsync(new ExceptionTypeConstraint(expectedExceptionType), code, message, args);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000329F File Offset: 0x0000149F
		public static Exception ThrowsAsync(Type expectedExceptionType, AsyncTestDelegate code)
		{
			return Assert.ThrowsAsync(new ExceptionTypeConstraint(expectedExceptionType), code, string.Empty, null);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000032B3 File Offset: 0x000014B3
		public static TActual ThrowsAsync<TActual>(AsyncTestDelegate code, string message, params object[] args) where TActual : Exception
		{
			return (TActual)((object)Assert.ThrowsAsync(typeof(TActual), code, message, args));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000032CC File Offset: 0x000014CC
		public static TActual ThrowsAsync<TActual>(AsyncTestDelegate code) where TActual : Exception
		{
			return Assert.ThrowsAsync<TActual>(code, string.Empty, null);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000032DA File Offset: 0x000014DA
		public static Exception CatchAsync(AsyncTestDelegate code, string message, params object[] args)
		{
			return Assert.ThrowsAsync(new InstanceOfTypeConstraint(typeof(Exception)), code, message, args);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000032F3 File Offset: 0x000014F3
		public static Exception CatchAsync(AsyncTestDelegate code)
		{
			return Assert.ThrowsAsync(new InstanceOfTypeConstraint(typeof(Exception)), code);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000330A File Offset: 0x0000150A
		public static Exception CatchAsync(Type expectedExceptionType, AsyncTestDelegate code, string message, params object[] args)
		{
			return Assert.ThrowsAsync(new InstanceOfTypeConstraint(expectedExceptionType), code, message, args);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000331A File Offset: 0x0000151A
		public static Exception CatchAsync(Type expectedExceptionType, AsyncTestDelegate code)
		{
			return Assert.ThrowsAsync(new InstanceOfTypeConstraint(expectedExceptionType), code);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003328 File Offset: 0x00001528
		public static TActual CatchAsync<TActual>(AsyncTestDelegate code, string message, params object[] args) where TActual : Exception
		{
			return (TActual)((object)Assert.ThrowsAsync(new InstanceOfTypeConstraint(typeof(TActual)), code, message, args));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003346 File Offset: 0x00001546
		public static TActual CatchAsync<TActual>(AsyncTestDelegate code) where TActual : Exception
		{
			return (TActual)((object)Assert.ThrowsAsync(new InstanceOfTypeConstraint(typeof(TActual)), code));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003362 File Offset: 0x00001562
		public static void DoesNotThrowAsync(AsyncTestDelegate code, string message, params object[] args)
		{
			Assert.That<AsyncTestDelegate>(code, new ThrowsNothingConstraint(), message, args);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003371 File Offset: 0x00001571
		public static void DoesNotThrowAsync(AsyncTestDelegate code)
		{
			Assert.DoesNotThrowAsync(code, string.Empty, null);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003380 File Offset: 0x00001580
		public static Exception Throws(IResolveConstraint expression, TestDelegate code, string message, params object[] args)
		{
			Exception ex = null;
			if (AsyncInvocationRegion.IsAsyncOperation(code))
			{
				using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(code))
				{
					code();
					try
					{
						asyncInvocationRegion.WaitForPendingOperationsToComplete(null);
						goto IL_003A;
					}
					catch (Exception ex)
					{
						goto IL_003A;
					}
				}
			}
			try
			{
				code();
			}
			catch (Exception ex)
			{
			}
			IL_003A:
			Assert.That<Exception>(ex, expression, message, args);
			return ex;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000033FC File Offset: 0x000015FC
		public static Exception Throws(IResolveConstraint expression, TestDelegate code)
		{
			return Assert.Throws(expression, code, string.Empty, null);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000340B File Offset: 0x0000160B
		public static Exception Throws(Type expectedExceptionType, TestDelegate code, string message, params object[] args)
		{
			return Assert.Throws(new ExceptionTypeConstraint(expectedExceptionType), code, message, args);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000341B File Offset: 0x0000161B
		public static Exception Throws(Type expectedExceptionType, TestDelegate code)
		{
			return Assert.Throws(new ExceptionTypeConstraint(expectedExceptionType), code, string.Empty, null);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000342F File Offset: 0x0000162F
		public static TActual Throws<TActual>(TestDelegate code, string message, params object[] args) where TActual : Exception
		{
			return (TActual)((object)Assert.Throws(typeof(TActual), code, message, args));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003448 File Offset: 0x00001648
		public static TActual Throws<TActual>(TestDelegate code) where TActual : Exception
		{
			return Assert.Throws<TActual>(code, string.Empty, null);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003456 File Offset: 0x00001656
		public static Exception Catch(TestDelegate code, string message, params object[] args)
		{
			return Assert.Throws(new InstanceOfTypeConstraint(typeof(Exception)), code, message, args);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000346F File Offset: 0x0000166F
		public static Exception Catch(TestDelegate code)
		{
			return Assert.Throws(new InstanceOfTypeConstraint(typeof(Exception)), code);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003486 File Offset: 0x00001686
		public static Exception Catch(Type expectedExceptionType, TestDelegate code, string message, params object[] args)
		{
			return Assert.Throws(new InstanceOfTypeConstraint(expectedExceptionType), code, message, args);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003496 File Offset: 0x00001696
		public static Exception Catch(Type expectedExceptionType, TestDelegate code)
		{
			return Assert.Throws(new InstanceOfTypeConstraint(expectedExceptionType), code);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000034A4 File Offset: 0x000016A4
		public static TActual Catch<TActual>(TestDelegate code, string message, params object[] args) where TActual : Exception
		{
			return (TActual)((object)Assert.Throws(new InstanceOfTypeConstraint(typeof(TActual)), code, message, args));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000034C2 File Offset: 0x000016C2
		public static TActual Catch<TActual>(TestDelegate code) where TActual : Exception
		{
			return (TActual)((object)Assert.Throws(new InstanceOfTypeConstraint(typeof(TActual)), code));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000034DE File Offset: 0x000016DE
		public static void DoesNotThrow(TestDelegate code, string message, params object[] args)
		{
			Assert.That(code, new ThrowsNothingConstraint(), message, args);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000034ED File Offset: 0x000016ED
		public static void DoesNotThrow(TestDelegate code)
		{
			Assert.DoesNotThrow(code, string.Empty, null);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002B03 File Offset: 0x00000D03
		public static void That(bool condition, string message, params object[] args)
		{
			Assert.That<bool>(condition, Is.True, message, args);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002B21 File Offset: 0x00000D21
		public static void That(bool condition)
		{
			Assert.That<bool>(condition, Is.True, null, null);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000034FB File Offset: 0x000016FB
		public static void That(bool condition, Func<string> getExceptionMessage)
		{
			Assert.That<bool>(condition, Is.True, getExceptionMessage);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003509 File Offset: 0x00001709
		public static void That(Func<bool> condition, string message, params object[] args)
		{
			Assert.That<bool>(condition(), Is.True, message, args);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000351D File Offset: 0x0000171D
		public static void That(Func<bool> condition)
		{
			Assert.That<bool>(condition(), Is.True, null, null);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003531 File Offset: 0x00001731
		public static void That(Func<bool> condition, Func<string> getExceptionMessage)
		{
			Assert.That<bool>(condition(), Is.True, getExceptionMessage);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003544 File Offset: 0x00001744
		public static void That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr)
		{
			Assert.That<TActual>(del, expr.Resolve(), null, null);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003554 File Offset: 0x00001754
		public static void That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr, string message, params object[] args)
		{
			IConstraint constraint = expr.Resolve();
			Assert.IncrementAssertCount();
			ConstraintResult constraintResult = constraint.ApplyTo<TActual>(del);
			if (!constraintResult.IsSuccess)
			{
				MessageWriter messageWriter = new TextMessageWriter(message, args);
				constraintResult.WriteMessageTo(messageWriter);
				throw new AssertionException(messageWriter.ToString());
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003596 File Offset: 0x00001796
		public static void That<TActual>(ActualValueDelegate<TActual> del, IResolveConstraint expr, Func<string> getExceptionMessage)
		{
			IConstraint constraint = expr.Resolve();
			Assert.IncrementAssertCount();
			if (!constraint.ApplyTo<TActual>(del).IsSuccess)
			{
				throw new AssertionException(getExceptionMessage());
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000035BC File Offset: 0x000017BC
		public static void That(TestDelegate code, IResolveConstraint constraint)
		{
			Assert.That(code, constraint, null, null);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000035C7 File Offset: 0x000017C7
		public static void That(TestDelegate code, IResolveConstraint constraint, string message, params object[] args)
		{
			Assert.That<object>(code, constraint, message, args);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000035D2 File Offset: 0x000017D2
		public static void That(TestDelegate code, IResolveConstraint constraint, Func<string> getExceptionMessage)
		{
			Assert.That<object>(code, constraint, getExceptionMessage);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000035DC File Offset: 0x000017DC
		public static void That<TActual>(TActual actual, IResolveConstraint expression)
		{
			Assert.That<TActual>(actual, expression, null, null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000035E8 File Offset: 0x000017E8
		public static void That<TActual>(TActual actual, IResolveConstraint expression, string message, params object[] args)
		{
			IConstraint constraint = expression.Resolve();
			Assert.IncrementAssertCount();
			ConstraintResult constraintResult = constraint.ApplyTo(actual);
			if (!constraintResult.IsSuccess)
			{
				MessageWriter messageWriter = new TextMessageWriter(message, args);
				constraintResult.WriteMessageTo(messageWriter);
				throw new AssertionException(messageWriter.ToString());
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000362F File Offset: 0x0000182F
		public static void That<TActual>(TActual actual, IResolveConstraint expression, Func<string> getExceptionMessage)
		{
			IConstraint constraint = expression.Resolve();
			Assert.IncrementAssertCount();
			if (!constraint.ApplyTo(actual).IsSuccess)
			{
				throw new AssertionException(getExceptionMessage());
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000365A File Offset: 0x0000185A
		public static void ByVal(object actual, IResolveConstraint expression)
		{
			Assert.That<object>(actual, expression, null, null);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000035C7 File Offset: 0x000017C7
		public static void ByVal(object actual, IResolveConstraint expression, string message, params object[] args)
		{
			Assert.That<object>(actual, expression, message, args);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003665 File Offset: 0x00001865
		public static void IsAssignableFrom(Type expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.AssignableFrom(expected), message, args);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003675 File Offset: 0x00001875
		public static void IsAssignableFrom(Type expected, object actual)
		{
			Assert.That<object>(actual, Is.AssignableFrom(expected), null, null);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003685 File Offset: 0x00001885
		public static void IsAssignableFrom<TExpected>(object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.AssignableFrom(typeof(TExpected)), message, args);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000369E File Offset: 0x0000189E
		public static void IsAssignableFrom<TExpected>(object actual)
		{
			Assert.That<object>(actual, Is.AssignableFrom(typeof(TExpected)), null, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000036B7 File Offset: 0x000018B7
		public static void IsNotAssignableFrom(Type expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.Not.AssignableFrom(expected), message, args);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000036CC File Offset: 0x000018CC
		public static void IsNotAssignableFrom(Type expected, object actual)
		{
			Assert.That<object>(actual, Is.Not.AssignableFrom(expected), null, null);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000036E1 File Offset: 0x000018E1
		public static void IsNotAssignableFrom<TExpected>(object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.Not.AssignableFrom(typeof(TExpected)), message, args);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000036FF File Offset: 0x000018FF
		public static void IsNotAssignableFrom<TExpected>(object actual)
		{
			Assert.That<object>(actual, Is.Not.AssignableFrom(typeof(TExpected)), null, null);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000371D File Offset: 0x0000191D
		public static void IsInstanceOf(Type expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.InstanceOf(expected), message, args);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000372D File Offset: 0x0000192D
		public static void IsInstanceOf(Type expected, object actual)
		{
			Assert.That<object>(actual, Is.InstanceOf(expected), null, null);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000373D File Offset: 0x0000193D
		public static void IsInstanceOf<TExpected>(object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.InstanceOf(typeof(TExpected)), message, args);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003756 File Offset: 0x00001956
		public static void IsInstanceOf<TExpected>(object actual)
		{
			Assert.That<object>(actual, Is.InstanceOf(typeof(TExpected)), null, null);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000376F File Offset: 0x0000196F
		public static void IsNotInstanceOf(Type expected, object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.Not.InstanceOf(expected), message, args);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003784 File Offset: 0x00001984
		public static void IsNotInstanceOf(Type expected, object actual)
		{
			Assert.That<object>(actual, Is.Not.InstanceOf(expected), null, null);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00003799 File Offset: 0x00001999
		public static void IsNotInstanceOf<TExpected>(object actual, string message, params object[] args)
		{
			Assert.That<object>(actual, Is.Not.InstanceOf(typeof(TExpected)), message, args);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000037B7 File Offset: 0x000019B7
		public static void IsNotInstanceOf<TExpected>(object actual)
		{
			Assert.That<object>(actual, Is.Not.InstanceOf(typeof(TExpected)), null, null);
		}
	}
}
