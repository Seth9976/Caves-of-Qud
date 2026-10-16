using System;
using System.Collections;
using System.Globalization;
using System.Text;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200011A RID: 282
	internal static class MsgUtils
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000143CF File Offset: 0x000125CF
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x000143D6 File Offset: 0x000125D6
		public static ValueFormatter DefaultValueFormatter { get; set; } = (object val) => string.Format(MsgUtils.Fmt_Default, val);

		// Token: 0x060008DC RID: 2268 RVA: 0x000143E0 File Offset: 0x000125E0
		static MsgUtils()
		{
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is ValueType))
				{
					return next(val);
				}
				return string.Format(MsgUtils.Fmt_ValueType, val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is DateTime))
				{
					return next(val);
				}
				return MsgUtils.FormatDateTime((DateTime)val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is DateTimeOffset))
				{
					return next(val);
				}
				return MsgUtils.FormatDateTimeOffset((DateTimeOffset)val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is decimal))
				{
					return next(val);
				}
				return MsgUtils.FormatDecimal((decimal)val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is float))
				{
					return next(val);
				}
				return MsgUtils.FormatFloat((float)val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is double))
				{
					return next(val);
				}
				return MsgUtils.FormatDouble((double)val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is char))
				{
					return next(val);
				}
				return string.Format(MsgUtils.Fmt_Char, val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is IEnumerable))
				{
					return next(val);
				}
				return MsgUtils.FormatCollection((IEnumerable)val, 0L, 10);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is string))
				{
					return next(val);
				}
				return MsgUtils.FormatString((string)val);
			});
			MsgUtils.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!val.GetType().IsArray)
				{
					return next(val);
				}
				return MsgUtils.FormatArray((Array)val);
			});
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001452E File Offset: 0x0001272E
		public static void AddFormatter(ValueFormatterFactory formatterFactory)
		{
			MsgUtils.DefaultValueFormatter = formatterFactory(MsgUtils.DefaultValueFormatter);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00014540 File Offset: 0x00012740
		public static string FormatValue(object val)
		{
			if (val == null)
			{
				return MsgUtils.Fmt_Null;
			}
			TestExecutionContext currentContext = TestExecutionContext.CurrentContext;
			if (currentContext != null)
			{
				return currentContext.CurrentValueFormatter(val);
			}
			return MsgUtils.DefaultValueFormatter(val);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00014578 File Offset: 0x00012778
		public static string FormatCollection(IEnumerable collection, long start, int max)
		{
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in collection)
			{
				if ((long)num2++ >= start)
				{
					if (++num > max)
					{
						break;
					}
					stringBuilder.Append((num == 1) ? "< " : ", ");
					stringBuilder.Append(MsgUtils.FormatValue(obj));
				}
			}
			if (num == 0)
			{
				return MsgUtils.Fmt_EmptyCollection;
			}
			if (num > max)
			{
				stringBuilder.Append("...");
			}
			stringBuilder.Append(" >");
			return stringBuilder.ToString();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00014634 File Offset: 0x00012834
		private static string FormatArray(Array array)
		{
			if (array.Length == 0)
			{
				return MsgUtils.Fmt_EmptyCollection;
			}
			int rank = array.Rank;
			int[] array2 = new int[rank];
			int num = 1;
			int num2 = rank;
			while (--num2 >= 0)
			{
				num = (array2[num2] = num * array.GetLength(num2));
			}
			int num3 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in array)
			{
				if (num3 > 0)
				{
					stringBuilder.Append(", ");
				}
				bool flag = false;
				for (int i = 0; i < rank; i++)
				{
					flag = flag || num3 % array2[i] == 0;
					if (flag)
					{
						stringBuilder.Append("< ");
					}
				}
				stringBuilder.Append(MsgUtils.FormatValue(obj));
				num3++;
				bool flag2 = false;
				for (int j = 0; j < rank; j++)
				{
					flag2 = flag2 || num3 % array2[j] == 0;
					if (flag2)
					{
						stringBuilder.Append(" >");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00014764 File Offset: 0x00012964
		private static string FormatString(string s)
		{
			if (!(s == string.Empty))
			{
				return string.Format(MsgUtils.Fmt_String, s);
			}
			return MsgUtils.Fmt_EmptyString;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00014784 File Offset: 0x00012984
		private static string FormatDouble(double d)
		{
			if (double.IsNaN(d) || double.IsInfinity(d))
			{
				return d.ToString();
			}
			string text = d.ToString("G17", CultureInfo.InvariantCulture);
			if (text.IndexOf('.') > 0)
			{
				return text + "d";
			}
			return text + ".0d";
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000147E0 File Offset: 0x000129E0
		private static string FormatFloat(float f)
		{
			if (float.IsNaN(f) || float.IsInfinity(f))
			{
				return f.ToString();
			}
			string text = f.ToString("G9", CultureInfo.InvariantCulture);
			if (text.IndexOf('.') > 0)
			{
				return text + "f";
			}
			return text + ".0f";
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00014839 File Offset: 0x00012A39
		private static string FormatDecimal(decimal d)
		{
			return d.ToString("G29", CultureInfo.InvariantCulture) + "m";
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00014856 File Offset: 0x00012A56
		private static string FormatDateTime(DateTime dt)
		{
			return dt.ToString(MsgUtils.Fmt_DateTime, CultureInfo.InvariantCulture);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00014869 File Offset: 0x00012A69
		private static string FormatDateTimeOffset(DateTimeOffset dto)
		{
			return dto.ToString(MsgUtils.Fmt_DateTimeOffset, CultureInfo.InvariantCulture);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001487C File Offset: 0x00012A7C
		public static string GetTypeRepresentation(object obj)
		{
			Array array = obj as Array;
			if (array == null)
			{
				return string.Format("<{0}>", obj.GetType());
			}
			StringBuilder stringBuilder = new StringBuilder();
			Type type = array.GetType();
			int num = 0;
			while (type.IsArray)
			{
				type = type.GetElementType();
				num++;
			}
			stringBuilder.Append(type.ToString());
			stringBuilder.Append('[');
			for (int i = 0; i < array.Rank; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(array.GetLength(i));
			}
			stringBuilder.Append(']');
			while (--num > 0)
			{
				stringBuilder.Append("[]");
			}
			return string.Format("<{0}>", stringBuilder.ToString());
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00014940 File Offset: 0x00012B40
		public static string EscapeControlChars(string s)
		{
			if (s != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = s;
				int i = 0;
				while (i < text.Length)
				{
					char c = text[i];
					if (c <= '\\')
					{
						switch (c)
						{
						case '\0':
							stringBuilder.Append("\\0");
							break;
						case '\u0001':
						case '\u0002':
						case '\u0003':
						case '\u0004':
						case '\u0005':
						case '\u0006':
							goto IL_012D;
						case '\a':
							stringBuilder.Append("\\a");
							break;
						case '\b':
							stringBuilder.Append("\\b");
							break;
						case '\t':
							stringBuilder.Append("\\t");
							break;
						case '\n':
							stringBuilder.Append("\\n");
							break;
						case '\v':
							stringBuilder.Append("\\v");
							break;
						case '\f':
							stringBuilder.Append("\\f");
							break;
						case '\r':
							stringBuilder.Append("\\r");
							break;
						default:
							if (c != '\\')
							{
								goto IL_012D;
							}
							stringBuilder.Append("\\\\");
							break;
						}
					}
					else
					{
						if (c != '\u0085' && c != '\u2028' && c != '\u2029')
						{
							goto IL_012D;
						}
						stringBuilder.Append(string.Format("\\x{0:X4}", (int)c));
					}
					IL_0135:
					i++;
					continue;
					IL_012D:
					stringBuilder.Append(c);
					goto IL_0135;
				}
				s = stringBuilder.ToString();
			}
			return s;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00014A9C File Offset: 0x00012C9C
		public static string EscapeNullCharacters(string s)
		{
			if (s != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (char c in s)
				{
					if (c == '\0')
					{
						stringBuilder.Append("\\0");
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				s = stringBuilder.ToString();
			}
			return s;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00014AF0 File Offset: 0x00012CF0
		public static string GetArrayIndicesAsString(int[] indices)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			for (int i = 0; i < indices.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(indices[i].ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00014B4C File Offset: 0x00012D4C
		public static int[] GetArrayIndicesFromCollectionIndex(IEnumerable collection, long index)
		{
			Array array = collection as Array;
			int num = ((array == null) ? 1 : array.Rank);
			int[] array2 = new int[num];
			int num2 = num;
			while (--num2 > 0)
			{
				int length = array.GetLength(num2);
				array2[num2] = (int)index % length;
				index /= (long)length;
			}
			array2[0] = (int)index;
			return array2;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00014B9C File Offset: 0x00012D9C
		public static string ClipString(string s, int maxStringLength, int clipStart)
		{
			int num = maxStringLength;
			StringBuilder stringBuilder = new StringBuilder();
			if (clipStart > 0)
			{
				num -= "...".Length;
				stringBuilder.Append("...");
			}
			if (s.Length - clipStart > num)
			{
				num -= "...".Length;
				stringBuilder.Append(s.Substring(clipStart, num));
				stringBuilder.Append("...");
			}
			else if (clipStart > 0)
			{
				stringBuilder.Append(s.Substring(clipStart));
			}
			else
			{
				stringBuilder.Append(s);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00014C28 File Offset: 0x00012E28
		public static void ClipExpectedAndActual(ref string expected, ref string actual, int maxDisplayLength, int mismatch)
		{
			int num = Math.Max(expected.Length, actual.Length);
			if (num <= maxDisplayLength)
			{
				return;
			}
			int num2 = maxDisplayLength - "...".Length;
			int num3 = num - num2;
			if (num3 > mismatch)
			{
				num3 = Math.Max(0, mismatch - num2 / 2);
			}
			expected = MsgUtils.ClipString(expected, maxDisplayLength, num3);
			actual = MsgUtils.ClipString(actual, maxDisplayLength, num3);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00014C88 File Offset: 0x00012E88
		public static int FindMismatchPosition(string expected, string actual, int istart, bool ignoreCase)
		{
			int num = Math.Min(expected.Length, actual.Length);
			string text = (ignoreCase ? expected.ToLower() : expected);
			string text2 = (ignoreCase ? actual.ToLower() : actual);
			for (int i = istart; i < num; i++)
			{
				if (text[i] != text2[i])
				{
					return i;
				}
			}
			if (expected.Length != actual.Length)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x040001C7 RID: 455
		private const string ELLIPSIS = "...";

		// Token: 0x040001C8 RID: 456
		private static readonly string Fmt_Null = "null";

		// Token: 0x040001C9 RID: 457
		private static readonly string Fmt_EmptyString = "<string.Empty>";

		// Token: 0x040001CA RID: 458
		private static readonly string Fmt_EmptyCollection = "<empty>";

		// Token: 0x040001CB RID: 459
		private static readonly string Fmt_String = "\"{0}\"";

		// Token: 0x040001CC RID: 460
		private static readonly string Fmt_Char = "'{0}'";

		// Token: 0x040001CD RID: 461
		private static readonly string Fmt_DateTime = "yyyy-MM-dd HH:mm:ss.fff";

		// Token: 0x040001CE RID: 462
		private static readonly string Fmt_DateTimeOffset = "yyyy-MM-dd HH:mm:ss.fffzzz";

		// Token: 0x040001CF RID: 463
		private static readonly string Fmt_ValueType = "{0}";

		// Token: 0x040001D0 RID: 464
		private static readonly string Fmt_Default = "<{0}>";
	}
}
