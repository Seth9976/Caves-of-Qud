using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000083 RID: 131
	public class TestNameGenerator
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x0000B4C8 File Offset: 0x000096C8
		public TestNameGenerator()
		{
			this._pattern = TestNameGenerator.DefaultTestNamePattern;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000B4DB File Offset: 0x000096DB
		public TestNameGenerator(string pattern)
		{
			this._pattern = pattern;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000B4EA File Offset: 0x000096EA
		public string GetDisplayName(TestMethod testMethod)
		{
			return this.GetDisplayName(testMethod, null);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public string GetDisplayName(TestMethod testMethod, object[] args)
		{
			if (this._fragments == null)
			{
				this._fragments = TestNameGenerator.BuildFragmentList(this._pattern);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TestNameGenerator.NameFragment nameFragment in this._fragments)
			{
				stringBuilder.Append(nameFragment.GetText(testMethod, args));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000B574 File Offset: 0x00009774
		private static List<TestNameGenerator.NameFragment> BuildFragmentList(string pattern)
		{
			List<TestNameGenerator.NameFragment> list = new List<TestNameGenerator.NameFragment>();
			int i = 0;
			while (i < pattern.Length)
			{
				int num = pattern.IndexOf('{', i);
				if (num < 0)
				{
					break;
				}
				int num2 = pattern.IndexOf('}', num);
				if (num2 >= 0)
				{
					if (num > i)
					{
						list.Add(new TestNameGenerator.FixedTextFragment(pattern.Substring(i, num - i)));
					}
					string text = pattern.Substring(num, num2 - num + 1);
					if (text == null)
					{
						goto IL_031C;
					}
					int length = text.Length;
					if (length != 3)
					{
						goto IL_031C;
					}
					char c = text[1];
					if (c <= 'a')
					{
						switch (c)
						{
						case '0':
							if (!(text == "{0}"))
							{
								goto IL_031C;
							}
							break;
						case '1':
							if (!(text == "{1}"))
							{
								goto IL_031C;
							}
							break;
						case '2':
							if (!(text == "{2}"))
							{
								goto IL_031C;
							}
							break;
						case '3':
							if (!(text == "{3}"))
							{
								goto IL_031C;
							}
							break;
						case '4':
							if (!(text == "{4}"))
							{
								goto IL_031C;
							}
							break;
						case '5':
							if (!(text == "{5}"))
							{
								goto IL_031C;
							}
							break;
						case '6':
							if (!(text == "{6}"))
							{
								goto IL_031C;
							}
							break;
						case '7':
							if (!(text == "{7}"))
							{
								goto IL_031C;
							}
							break;
						case '8':
							if (!(text == "{8}"))
							{
								goto IL_031C;
							}
							break;
						case '9':
							if (!(text == "{9}"))
							{
								goto IL_031C;
							}
							break;
						case ':':
						case ';':
						case '<':
						case '=':
						case '>':
						case '?':
						case '@':
						case 'A':
						case 'B':
							goto IL_031C;
						case 'C':
							if (!(text == "{C}"))
							{
								goto IL_031C;
							}
							list.Add(new TestNameGenerator.ClassFullNameFragment());
							goto IL_03A7;
						default:
							if (c != 'M')
							{
								if (c != 'a')
								{
									goto IL_031C;
								}
								if (!(text == "{a}"))
								{
									goto IL_031C;
								}
								list.Add(new TestNameGenerator.ArgListFragment(0));
								goto IL_03A7;
							}
							else
							{
								if (!(text == "{M}"))
								{
									goto IL_031C;
								}
								list.Add(new TestNameGenerator.MethodFullNameFragment());
								goto IL_03A7;
							}
							break;
						}
						int num3 = (int)(text[1] - '0');
						list.Add(new TestNameGenerator.ArgumentFragment(num3, 40));
					}
					else if (c <= 'i')
					{
						if (c != 'c')
						{
							if (c != 'i')
							{
								goto IL_031C;
							}
							if (!(text == "{i}"))
							{
								goto IL_031C;
							}
							list.Add(new TestNameGenerator.TestIDFragment());
						}
						else
						{
							if (!(text == "{c}"))
							{
								goto IL_031C;
							}
							list.Add(new TestNameGenerator.ClassNameFragment());
						}
					}
					else if (c != 'm')
					{
						if (c != 'n')
						{
							goto IL_031C;
						}
						if (!(text == "{n}"))
						{
							goto IL_031C;
						}
						list.Add(new TestNameGenerator.NamespaceFragment());
					}
					else
					{
						if (!(text == "{m}"))
						{
							goto IL_031C;
						}
						list.Add(new TestNameGenerator.MethodNameFragment());
					}
					IL_03A7:
					i = num2 + 1;
					continue;
					IL_031C:
					char c2 = text[1];
					if (text.Length >= 5 && text[2] == ':' && (c2 == 'a' || char.IsDigit(c2)))
					{
						int num4;
						try
						{
							num4 = int.Parse(text.Substring(3, text.Length - 4));
						}
						catch
						{
							num4 = -1;
						}
						if (num4 > 0)
						{
							if (c2 == 'a')
							{
								list.Add(new TestNameGenerator.ArgListFragment(num4));
								goto IL_03A7;
							}
							list.Add(new TestNameGenerator.ArgumentFragment((int)(c2 - '0'), num4));
							goto IL_03A7;
						}
					}
					list.Add(new TestNameGenerator.FixedTextFragment(text));
					goto IL_03A7;
				}
				break;
			}
			if (i < pattern.Length)
			{
				list.Add(new TestNameGenerator.FixedTextFragment(pattern.Substring(i)));
			}
			return list;
		}

		// Token: 0x040000F7 RID: 247
		public static string DefaultTestNamePattern = "{m}{a}";

		// Token: 0x040000F8 RID: 248
		private string _pattern;

		// Token: 0x040000F9 RID: 249
		private List<TestNameGenerator.NameFragment> _fragments;

		// Token: 0x0200017D RID: 381
		private abstract class NameFragment
		{
			// Token: 0x06000AA7 RID: 2727 RVA: 0x00019ACE File Offset: 0x00017CCE
			public virtual string GetText(TestMethod testMethod, object[] args)
			{
				return this.GetText(testMethod.Method.MethodInfo, args);
			}

			// Token: 0x06000AA8 RID: 2728
			public abstract string GetText(MethodInfo method, object[] args);

			// Token: 0x06000AA9 RID: 2729 RVA: 0x00019AE4 File Offset: 0x00017CE4
			protected static void AppendGenericTypeNames(StringBuilder sb, MethodInfo method)
			{
				sb.Append("<");
				int num = 0;
				foreach (Type type in method.GetGenericArguments())
				{
					if (num++ > 0)
					{
						sb.Append(",");
					}
					sb.Append(type.Name);
				}
				sb.Append(">");
			}

			// Token: 0x06000AAA RID: 2730 RVA: 0x00019B48 File Offset: 0x00017D48
			protected static string GetDisplayString(object arg, int stringMax)
			{
				string text = ((arg == null) ? "null" : Convert.ToString(arg, CultureInfo.InvariantCulture));
				if (arg is double)
				{
					double num = (double)arg;
					if (double.IsNaN(num))
					{
						text = "double.NaN";
					}
					else if (double.IsPositiveInfinity(num))
					{
						text = "double.PositiveInfinity";
					}
					else if (double.IsNegativeInfinity(num))
					{
						text = "double.NegativeInfinity";
					}
					else if (num == 1.7976931348623157E+308)
					{
						text = "double.MaxValue";
					}
					else if (num == -1.7976931348623157E+308)
					{
						text = "double.MinValue";
					}
					else
					{
						if (text.IndexOf('.') == -1)
						{
							text += ".0";
						}
						text += "d";
					}
				}
				else if (arg is float)
				{
					float num2 = (float)arg;
					if (float.IsNaN(num2))
					{
						text = "float.NaN";
					}
					else if (float.IsPositiveInfinity(num2))
					{
						text = "float.PositiveInfinity";
					}
					else if (float.IsNegativeInfinity(num2))
					{
						text = "float.NegativeInfinity";
					}
					else if (num2 == 3.4028235E+38f)
					{
						text = "float.MaxValue";
					}
					else if (num2 == -3.4028235E+38f)
					{
						text = "float.MinValue";
					}
					else
					{
						if (text.IndexOf('.') == -1)
						{
							text += ".0";
						}
						text += "f";
					}
				}
				else if (arg is decimal)
				{
					decimal num3 = (decimal)arg;
					if (num3 == -79228162514264337593543950335m)
					{
						text = "decimal.MinValue";
					}
					else if (num3 == 79228162514264337593543950335m)
					{
						text = "decimal.MaxValue";
					}
					else
					{
						text += "m";
					}
				}
				else if (arg is long)
				{
					if (arg.Equals(-9223372036854775808L))
					{
						text = "long.MinValue";
					}
					else if (arg.Equals(9223372036854775807L))
					{
						text = "long.MaxValue";
					}
					else
					{
						text += "L";
					}
				}
				else if (arg is ulong)
				{
					ulong num4 = (ulong)arg;
					if (num4 == 0UL)
					{
						text = "ulong.MinValue";
					}
					else if (num4 == 18446744073709551615UL)
					{
						text = "ulong.MaxValue";
					}
					else
					{
						text += "UL";
					}
				}
				else if (arg is string)
				{
					string text2 = (string)arg;
					bool flag = stringMax > 0 && text2.Length > stringMax;
					int num5 = (flag ? (stringMax - "...".Length) : 0);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("\"");
					foreach (char c in text2)
					{
						stringBuilder.Append(TestNameGenerator.NameFragment.EscapeCharInString(c));
						if (flag && stringBuilder.Length > num5)
						{
							stringBuilder.Append("...");
							break;
						}
					}
					stringBuilder.Append("\"");
					text = stringBuilder.ToString();
				}
				else if (arg is char)
				{
					text = "'" + TestNameGenerator.NameFragment.EscapeSingleChar((char)arg) + "'";
				}
				else if (arg is int)
				{
					if (arg.Equals(2147483647))
					{
						text = "int.MaxValue";
					}
					else if (arg.Equals(-2147483648))
					{
						text = "int.MinValue";
					}
				}
				else if (arg is uint)
				{
					if (arg.Equals(4294967295U))
					{
						text = "uint.MaxValue";
					}
					else if (arg.Equals(0U))
					{
						text = "uint.MinValue";
					}
				}
				else if (arg is short)
				{
					if (arg.Equals(32767))
					{
						text = "short.MaxValue";
					}
					else if (arg.Equals(-32768))
					{
						text = "short.MinValue";
					}
				}
				else if (arg is ushort)
				{
					if (arg.Equals(65535))
					{
						text = "ushort.MaxValue";
					}
					else if (arg.Equals(0))
					{
						text = "ushort.MinValue";
					}
				}
				else if (arg is byte)
				{
					if (arg.Equals(255))
					{
						text = "byte.MaxValue";
					}
					else if (arg.Equals(0))
					{
						text = "byte.MinValue";
					}
				}
				else if (arg is sbyte)
				{
					if (arg.Equals(127))
					{
						text = "sbyte.MaxValue";
					}
					else if (arg.Equals(-128))
					{
						text = "sbyte.MinValue";
					}
				}
				return text;
			}

			// Token: 0x06000AAB RID: 2731 RVA: 0x00019FED File Offset: 0x000181ED
			private static string EscapeSingleChar(char c)
			{
				if (c == '\'')
				{
					return "\\'";
				}
				return TestNameGenerator.NameFragment.EscapeControlChar(c);
			}

			// Token: 0x06000AAC RID: 2732 RVA: 0x0001A000 File Offset: 0x00018200
			private static string EscapeCharInString(char c)
			{
				if (c == '"')
				{
					return "\\\"";
				}
				return TestNameGenerator.NameFragment.EscapeControlChar(c);
			}

			// Token: 0x06000AAD RID: 2733 RVA: 0x0001A014 File Offset: 0x00018214
			private static string EscapeControlChar(char c)
			{
				if (c <= '\\')
				{
					switch (c)
					{
					case '\0':
						return "\\0";
					case '\u0001':
					case '\u0002':
					case '\u0003':
					case '\u0004':
					case '\u0005':
					case '\u0006':
						break;
					case '\a':
						return "\\a";
					case '\b':
						return "\\b";
					case '\t':
						return "\\t";
					case '\n':
						return "\\n";
					case '\v':
						return "\\v";
					case '\f':
						return "\\f";
					case '\r':
						return "\\r";
					default:
						if (c == '\\')
						{
							return "\\\\";
						}
						break;
					}
				}
				else if (c == '\u0085' || c == '\u2028' || c == '\u2029')
				{
					return string.Format("\\x{0:X4}", (int)c);
				}
				return c.ToString();
			}

			// Token: 0x04000276 RID: 630
			private const string THREE_DOTS = "...";
		}

		// Token: 0x0200017E RID: 382
		private class TestIDFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000AAF RID: 2735 RVA: 0x0001A0D3 File Offset: 0x000182D3
			public override string GetText(MethodInfo method, object[] args)
			{
				return "{i}";
			}

			// Token: 0x06000AB0 RID: 2736 RVA: 0x0001A0DA File Offset: 0x000182DA
			public override string GetText(TestMethod testMethod, object[] args)
			{
				return testMethod.Id;
			}
		}

		// Token: 0x0200017F RID: 383
		private class FixedTextFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000AB2 RID: 2738 RVA: 0x0001A0EA File Offset: 0x000182EA
			public FixedTextFragment(string text)
			{
				this._text = text;
			}

			// Token: 0x06000AB3 RID: 2739 RVA: 0x0001A0F9 File Offset: 0x000182F9
			public override string GetText(MethodInfo method, object[] args)
			{
				return this._text;
			}

			// Token: 0x04000277 RID: 631
			private string _text;
		}

		// Token: 0x02000180 RID: 384
		private class MethodNameFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000AB4 RID: 2740 RVA: 0x0001A104 File Offset: 0x00018304
			public override string GetText(MethodInfo method, object[] args)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(method.Name);
				if (method.IsGenericMethod)
				{
					TestNameGenerator.NameFragment.AppendGenericTypeNames(stringBuilder, method);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000181 RID: 385
		private class NamespaceFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000AB6 RID: 2742 RVA: 0x0001A139 File Offset: 0x00018339
			public override string GetText(MethodInfo method, object[] args)
			{
				return method.DeclaringType.Namespace;
			}
		}

		// Token: 0x02000182 RID: 386
		private class MethodFullNameFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000AB8 RID: 2744 RVA: 0x0001A148 File Offset: 0x00018348
			public override string GetText(MethodInfo method, object[] args)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(method.DeclaringType.FullName);
				stringBuilder.Append('.');
				stringBuilder.Append(method.Name);
				if (method.IsGenericMethod)
				{
					TestNameGenerator.NameFragment.AppendGenericTypeNames(stringBuilder, method);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x02000183 RID: 387
		private class ClassNameFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000ABA RID: 2746 RVA: 0x0001A198 File Offset: 0x00018398
			public override string GetText(MethodInfo method, object[] args)
			{
				return method.DeclaringType.Name;
			}
		}

		// Token: 0x02000184 RID: 388
		private class ClassFullNameFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000ABC RID: 2748 RVA: 0x0001A1A5 File Offset: 0x000183A5
			public override string GetText(MethodInfo method, object[] args)
			{
				return method.DeclaringType.FullName;
			}
		}

		// Token: 0x02000185 RID: 389
		private class ArgListFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000ABE RID: 2750 RVA: 0x0001A1B2 File Offset: 0x000183B2
			public ArgListFragment(int maxStringLength)
			{
				this._maxStringLength = maxStringLength;
			}

			// Token: 0x06000ABF RID: 2751 RVA: 0x0001A1C4 File Offset: 0x000183C4
			public override string GetText(MethodInfo method, object[] arglist)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (arglist != null)
				{
					stringBuilder.Append('(');
					for (int i = 0; i < arglist.Length; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(TestNameGenerator.NameFragment.GetDisplayString(arglist[i], this._maxStringLength));
					}
					stringBuilder.Append(')');
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04000278 RID: 632
			private int _maxStringLength;
		}

		// Token: 0x02000186 RID: 390
		private class ArgumentFragment : TestNameGenerator.NameFragment
		{
			// Token: 0x06000AC0 RID: 2752 RVA: 0x0001A225 File Offset: 0x00018425
			public ArgumentFragment(int index, int maxStringLength)
			{
				this._index = index;
				this._maxStringLength = maxStringLength;
			}

			// Token: 0x06000AC1 RID: 2753 RVA: 0x0001A23B File Offset: 0x0001843B
			public override string GetText(MethodInfo method, object[] args)
			{
				if (this._index >= args.Length)
				{
					return string.Empty;
				}
				return TestNameGenerator.NameFragment.GetDisplayString(args[this._index], this._maxStringLength);
			}

			// Token: 0x04000279 RID: 633
			private int _index;

			// Token: 0x0400027A RID: 634
			private int _maxStringLength;
		}
	}
}
