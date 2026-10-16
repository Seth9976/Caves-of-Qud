using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200011F RID: 287
	public class Numerics
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00014E4C File Offset: 0x0001304C
		public static bool IsNumericType(object obj)
		{
			return Numerics.IsFloatingPointNumeric(obj) || Numerics.IsFixedPointNumeric(obj);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00014E5E File Offset: 0x0001305E
		public static bool IsFloatingPointNumeric(object obj)
		{
			if (obj != null)
			{
				if (obj is double)
				{
					return true;
				}
				if (obj is float)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00014E78 File Offset: 0x00013078
		public static bool IsFixedPointNumeric(object obj)
		{
			if (obj != null)
			{
				if (obj is byte)
				{
					return true;
				}
				if (obj is sbyte)
				{
					return true;
				}
				if (obj is decimal)
				{
					return true;
				}
				if (obj is int)
				{
					return true;
				}
				if (obj is uint)
				{
					return true;
				}
				if (obj is long)
				{
					return true;
				}
				if (obj is ulong)
				{
					return true;
				}
				if (obj is short)
				{
					return true;
				}
				if (obj is ushort)
				{
					return true;
				}
				if (obj is char)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00014EF0 File Offset: 0x000130F0
		public static bool AreEqual(object expected, object actual, ref Tolerance tolerance)
		{
			if (expected is double || actual is double)
			{
				return Numerics.AreEqual(Convert.ToDouble(expected), Convert.ToDouble(actual), ref tolerance);
			}
			if (expected is float || actual is float)
			{
				return Numerics.AreEqual(Convert.ToSingle(expected), Convert.ToSingle(actual), ref tolerance);
			}
			if (tolerance.Mode == ToleranceMode.Ulps)
			{
				throw new InvalidOperationException("Ulps may only be specified for floating point arguments");
			}
			if (expected is decimal || actual is decimal)
			{
				return Numerics.AreEqual(Convert.ToDecimal(expected), Convert.ToDecimal(actual), tolerance);
			}
			if (expected is ulong || actual is ulong)
			{
				return Numerics.AreEqual(Convert.ToUInt64(expected), Convert.ToUInt64(actual), tolerance);
			}
			if (expected is long || actual is long)
			{
				return Numerics.AreEqual(Convert.ToInt64(expected), Convert.ToInt64(actual), tolerance);
			}
			if (expected is uint || actual is uint)
			{
				return Numerics.AreEqual(Convert.ToUInt32(expected), Convert.ToUInt32(actual), tolerance);
			}
			return Numerics.AreEqual(Convert.ToInt32(expected), Convert.ToInt32(actual), tolerance);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00014FFC File Offset: 0x000131FC
		private static bool AreEqual(double expected, double actual, ref Tolerance tolerance)
		{
			if (double.IsNaN(expected) && double.IsNaN(actual))
			{
				return true;
			}
			if (double.IsInfinity(expected) || double.IsNaN(expected) || double.IsNaN(actual))
			{
				return expected.Equals(actual);
			}
			if (tolerance.IsUnsetOrDefault && GlobalSettings.DefaultFloatingPointTolerance > 0.0)
			{
				tolerance = new Tolerance(GlobalSettings.DefaultFloatingPointTolerance);
			}
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
				return Math.Abs(expected - actual) <= Convert.ToDouble(tolerance.Value);
			case ToleranceMode.Percent:
				if (expected == 0.0)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((expected - actual) / expected) <= Convert.ToDouble(tolerance.Value) / 100.0;
			case ToleranceMode.Ulps:
				return FloatingPointNumerics.AreAlmostEqualUlps(expected, actual, Convert.ToInt64(tolerance.Value));
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00015108 File Offset: 0x00013308
		private static bool AreEqual(float expected, float actual, ref Tolerance tolerance)
		{
			if (float.IsNaN(expected) && float.IsNaN(actual))
			{
				return true;
			}
			if (float.IsInfinity(expected) || float.IsNaN(expected) || float.IsNaN(actual))
			{
				return expected.Equals(actual);
			}
			if (tolerance.IsUnsetOrDefault && GlobalSettings.DefaultFloatingPointTolerance > 0.0)
			{
				tolerance = new Tolerance(GlobalSettings.DefaultFloatingPointTolerance);
			}
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
				return (double)Math.Abs(expected - actual) <= Convert.ToDouble(tolerance.Value);
			case ToleranceMode.Percent:
				if (expected == 0f)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((expected - actual) / expected) <= Convert.ToSingle(tolerance.Value) / 100f;
			case ToleranceMode.Ulps:
				return FloatingPointNumerics.AreAlmostEqualUlps(expected, actual, Convert.ToInt32(tolerance.Value));
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001520C File Offset: 0x0001340C
		private static bool AreEqual(decimal expected, decimal actual, Tolerance tolerance)
		{
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
			{
				decimal num = Convert.ToDecimal(tolerance.Value);
				if (num > 0m)
				{
					return Math.Abs(expected - actual) <= num;
				}
				return expected.Equals(actual);
			}
			case ToleranceMode.Percent:
				if (expected == 0m)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((double)(expected - actual) / (double)expected) <= Convert.ToDouble(tolerance.Value) / 100.0;
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000152D0 File Offset: 0x000134D0
		private static bool AreEqual(ulong expected, ulong actual, Tolerance tolerance)
		{
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
			{
				ulong num = Convert.ToUInt64(tolerance.Value);
				if (num > 0UL)
				{
					return ((expected >= actual) ? (expected - actual) : (actual - expected)) <= num;
				}
				return expected.Equals(actual);
			}
			case ToleranceMode.Percent:
				if (expected == 0UL)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((Math.Max(expected, actual) - Math.Min(expected, actual)) / expected) <= Convert.ToDouble(tolerance.Value) / 100.0;
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00015380 File Offset: 0x00013580
		private static bool AreEqual(long expected, long actual, Tolerance tolerance)
		{
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
			{
				long num = Convert.ToInt64(tolerance.Value);
				if (num > 0L)
				{
					return Math.Abs(expected - actual) <= num;
				}
				return expected.Equals(actual);
			}
			case ToleranceMode.Percent:
				if (expected == 0L)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((double)(expected - actual) / (double)expected) <= Convert.ToDouble(tolerance.Value) / 100.0;
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001541C File Offset: 0x0001361C
		private static bool AreEqual(uint expected, uint actual, Tolerance tolerance)
		{
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
			{
				uint num = Convert.ToUInt32(tolerance.Value);
				if (num > 0U)
				{
					return ((expected >= actual) ? (expected - actual) : (actual - expected)) <= num;
				}
				return expected.Equals(actual);
			}
			case ToleranceMode.Percent:
				if (expected == 0U)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((Math.Max(expected, actual) - Math.Min(expected, actual)) / expected) <= Convert.ToDouble(tolerance.Value) / 100.0;
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000154CC File Offset: 0x000136CC
		private static bool AreEqual(int expected, int actual, Tolerance tolerance)
		{
			switch (tolerance.Mode)
			{
			case ToleranceMode.Unset:
				return expected.Equals(actual);
			case ToleranceMode.Linear:
			{
				int num = Convert.ToInt32(tolerance.Value);
				if (num > 0)
				{
					return Math.Abs(expected - actual) <= num;
				}
				return expected.Equals(actual);
			}
			case ToleranceMode.Percent:
				if (expected == 0)
				{
					return expected.Equals(actual);
				}
				return Math.Abs((double)(expected - actual) / (double)expected) <= Convert.ToDouble(tolerance.Value) / 100.0;
			default:
				throw new ArgumentException("Unknown tolerance mode specified", "mode");
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00015568 File Offset: 0x00013768
		public static int Compare(object expected, object actual)
		{
			if (!Numerics.IsNumericType(expected) || !Numerics.IsNumericType(actual))
			{
				throw new ArgumentException("Both arguments must be numeric");
			}
			if (Numerics.IsFloatingPointNumeric(expected) || Numerics.IsFloatingPointNumeric(actual))
			{
				return Convert.ToDouble(expected).CompareTo(Convert.ToDouble(actual));
			}
			if (expected is decimal || actual is decimal)
			{
				return Convert.ToDecimal(expected).CompareTo(Convert.ToDecimal(actual));
			}
			if (expected is ulong || actual is ulong)
			{
				return Convert.ToUInt64(expected).CompareTo(Convert.ToUInt64(actual));
			}
			if (expected is long || actual is long)
			{
				return Convert.ToInt64(expected).CompareTo(Convert.ToInt64(actual));
			}
			if (expected is uint || actual is uint)
			{
				return Convert.ToUInt32(expected).CompareTo(Convert.ToUInt32(actual));
			}
			return Convert.ToInt32(expected).CompareTo(Convert.ToInt32(actual));
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00002050 File Offset: 0x00000250
		private Numerics()
		{
		}
	}
}
