using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cysharp.Text
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	public struct Utf8ValueStringBuilder : IDisposable, IBufferWriter<byte>, IResettableBufferWriter<byte>
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0001635C File Offset: 0x0001455C
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, params T[] values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values.AsSpan<T>());
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00016390 File Offset: 0x00014590
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, List<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000163B8 File Offset: 0x000145B8
		[NullableContext(2)]
		public unsafe void AppendJoin<T>(char separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000163E0 File Offset: 0x000145E0
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IEnumerable<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00016408 File Offset: 0x00014608
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, ICollection<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values.AsEnumerable<T>());
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00016438 File Offset: 0x00014638
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IList<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00016460 File Offset: 0x00014660
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IReadOnlyList<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00016488 File Offset: 0x00014688
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IReadOnlyCollection<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values.AsEnumerable<T>());
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000164B5 File Offset: 0x000146B5
		public void AppendJoin<[Nullable(2)] T>(string separator, params T[] values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values.AsSpan<T>());
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000164CE File Offset: 0x000146CE
		public void AppendJoin<[Nullable(2)] T>(string separator, List<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000164DD File Offset: 0x000146DD
		public void AppendJoin<[Nullable(2)] T>(string separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000164EC File Offset: 0x000146EC
		public void AppendJoin<[Nullable(2)] T>(string separator, IEnumerable<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000164FB File Offset: 0x000146FB
		public void AppendJoin<[Nullable(2)] T>(string separator, ICollection<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values.AsEnumerable<T>());
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0001650F File Offset: 0x0001470F
		public void AppendJoin<[Nullable(2)] T>(string separator, IList<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0001651E File Offset: 0x0001471E
		public void AppendJoin<[Nullable(2)] T>(string separator, IReadOnlyList<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0001652D File Offset: 0x0001472D
		public void AppendJoin<[Nullable(2)] T>(string separator, IReadOnlyCollection<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values.AsEnumerable<T>());
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00016544 File Offset: 0x00014744
		[NullableContext(0)]
		internal void AppendJoinInternal<[Nullable(2)] T>(ReadOnlySpan<char> separator, [Nullable(1)] IList<T> values)
		{
			IReadOnlyList<T> readOnlyList = values as IReadOnlyList<T>;
			readOnlyList = readOnlyList ?? new ReadOnlyListAdaptor<T>(values);
			this.AppendJoinInternal<T>(separator, readOnlyList);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00016574 File Offset: 0x00014774
		[NullableContext(0)]
		internal void AppendJoinInternal<[Nullable(2)] T>(ReadOnlySpan<char> separator, [Nullable(1)] IReadOnlyList<T> values)
		{
			int count = values.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					this.Append(separator);
				}
				T t = values[i];
				if (typeof(T) == typeof(string))
				{
					string text = Unsafe.As<string>(t);
					if (!string.IsNullOrEmpty(text))
					{
						this.Append(text);
					}
				}
				else
				{
					this.Append<T>(t);
				}
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000165E8 File Offset: 0x000147E8
		[NullableContext(0)]
		internal unsafe void AppendJoinInternal<[Nullable(2)] T>(ReadOnlySpan<char> separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (i != 0)
				{
					this.Append(separator);
				}
				T t = *values[i];
				if (typeof(T) == typeof(string))
				{
					string text = Unsafe.As<string>(t);
					if (!string.IsNullOrEmpty(text))
					{
						this.Append(text);
					}
				}
				else
				{
					this.Append<T>(t);
				}
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00016660 File Offset: 0x00014860
		[NullableContext(0)]
		internal void AppendJoinInternal<[Nullable(2)] T>(ReadOnlySpan<char> separator, [Nullable(1)] IEnumerable<T> values)
		{
			bool flag = true;
			foreach (T t in values)
			{
				if (!flag)
				{
					this.Append(separator);
				}
				else
				{
					flag = false;
				}
				if (typeof(T) == typeof(string))
				{
					string text = Unsafe.As<string>(t);
					if (!string.IsNullOrEmpty(text))
					{
						this.Append(text);
					}
				}
				else
				{
					this.Append<T>(t);
				}
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000166F0 File Offset: 0x000148F0
		public void AppendFormat<[Nullable(2)] T1>(string format, T1 arg1)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						if (parseResult.Index == 0)
						{
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
						}
						else
						{
							this.ThrowFormatException();
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0001682C File Offset: 0x00014A2C
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2>(string format, T1 arg1, T2 arg2)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						int num4 = parseResult.Index;
						if (num4 != 0)
						{
							if (num4 != 1)
							{
								this.ThrowFormatException();
							}
							else
							{
								this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							}
						}
						else
						{
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num5 = i - num;
						this.Append(format, num, num5);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num6 = format.Length - num;
			if (num6 > 0)
			{
				this.Append(format, num, num6);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001698C File Offset: 0x00014B8C
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(string format, T1 arg1, T2 arg2, T3 arg3)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00016B0C File Offset: 0x00014D0C
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00016CA8 File Offset: 0x00014EA8
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00016E64 File Offset: 0x00015064
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00017040 File Offset: 0x00015240
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0001723C File Offset: 0x0001543C
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00017454 File Offset: 0x00015654
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0001768C File Offset: 0x0001588C
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000178E0 File Offset: 0x00015AE0
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00017B54 File Offset: 0x00015D54
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00017DE4 File Offset: 0x00015FE4
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00018094 File Offset: 0x00016294
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, standardFormat, "arg14");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00018360 File Offset: 0x00016560
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, standardFormat, "arg14");
							break;
						case 14:
							this.AppendFormatInternal<T15>(arg15, parseResult.Alignment, standardFormat, "arg15");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001864C File Offset: 0x0001684C
		public void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15, [Nullable(2)] T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = format[i];
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && format[i + 1] == '{')
					{
						int num2 = i - num;
						this.Append(format, num, num2);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.AsSpan(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, standardFormat, "arg14");
							break;
						case 14:
							this.AppendFormatInternal<T15>(arg15, parseResult.Alignment, standardFormat, "arg15");
							break;
						case 15:
							this.AppendFormatInternal<T16>(arg16, parseResult.Alignment, standardFormat, "arg16");
							break;
						default:
							this.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						this.Append(format, num, num4);
						i++;
						num = i;
					}
					else
					{
						this.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00018954 File Offset: 0x00016B54
		[return: Nullable(2)]
		private static object CreateFormatter(Type type)
		{
			if (type == typeof(byte))
			{
				return new Utf8ValueStringBuilder.TryFormat<byte>(delegate(byte x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(DateTime))
			{
				return new Utf8ValueStringBuilder.TryFormat<DateTime>(delegate(DateTime x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(DateTimeOffset))
			{
				return new Utf8ValueStringBuilder.TryFormat<DateTimeOffset>(delegate(DateTimeOffset x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(decimal))
			{
				return new Utf8ValueStringBuilder.TryFormat<decimal>(delegate(decimal x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(double))
			{
				return new Utf8ValueStringBuilder.TryFormat<double>(delegate(double x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(short))
			{
				return new Utf8ValueStringBuilder.TryFormat<short>(delegate(short x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(int))
			{
				return new Utf8ValueStringBuilder.TryFormat<int>(delegate(int x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(long))
			{
				return new Utf8ValueStringBuilder.TryFormat<long>(delegate(long x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(sbyte))
			{
				return new Utf8ValueStringBuilder.TryFormat<sbyte>(delegate(sbyte x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(float))
			{
				return new Utf8ValueStringBuilder.TryFormat<float>(delegate(float x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(TimeSpan))
			{
				return new Utf8ValueStringBuilder.TryFormat<TimeSpan>(delegate(TimeSpan x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(ushort))
			{
				return new Utf8ValueStringBuilder.TryFormat<ushort>(delegate(ushort x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(uint))
			{
				return new Utf8ValueStringBuilder.TryFormat<uint>(delegate(uint x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(ulong))
			{
				return new Utf8ValueStringBuilder.TryFormat<ulong>(delegate(ulong x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(Guid))
			{
				return new Utf8ValueStringBuilder.TryFormat<Guid>(delegate(Guid x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(bool))
			{
				return new Utf8ValueStringBuilder.TryFormat<bool>(delegate(bool x, Span<byte> dest, out int written, StandardFormat format)
				{
					return Utf8Formatter.TryFormat(x, dest, out written, format);
				});
			}
			if (type == typeof(byte?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<byte>();
			}
			if (type == typeof(DateTime?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<DateTime>();
			}
			if (type == typeof(DateTimeOffset?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<DateTimeOffset>();
			}
			if (type == typeof(decimal?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<decimal>();
			}
			if (type == typeof(double?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<double>();
			}
			if (type == typeof(short?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<short>();
			}
			if (type == typeof(int?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<int>();
			}
			if (type == typeof(long?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<long>();
			}
			if (type == typeof(sbyte?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<sbyte>();
			}
			if (type == typeof(float?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<float>();
			}
			if (type == typeof(TimeSpan?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<TimeSpan>();
			}
			if (type == typeof(ushort?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<ushort>();
			}
			if (type == typeof(uint?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<uint>();
			}
			if (type == typeof(ulong?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<ulong>();
			}
			if (type == typeof(Guid?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<Guid>();
			}
			if (type == typeof(bool?))
			{
				return Utf8ValueStringBuilder.CreateNullableFormatter<bool>();
			}
			if (type == typeof(IntPtr))
			{
				return new Utf8ValueStringBuilder.TryFormat<IntPtr>(delegate(IntPtr x, Span<byte> dest, out int written, StandardFormat format)
				{
					if (IntPtr.Size != 4)
					{
						return Utf8Formatter.TryFormat(x.ToInt64(), dest, out written, format);
					}
					return Utf8Formatter.TryFormat(x.ToInt32(), dest, out written, format);
				});
			}
			if (type == typeof(UIntPtr))
			{
				return new Utf8ValueStringBuilder.TryFormat<UIntPtr>(delegate(UIntPtr x, Span<byte> dest, out int written, StandardFormat format)
				{
					if (UIntPtr.Size != 4)
					{
						return Utf8Formatter.TryFormat(x.ToUInt64(), dest, out written, format);
					}
					return Utf8Formatter.TryFormat(x.ToUInt32(), dest, out written, format);
				});
			}
			return null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00018E68 File Offset: 0x00017068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(byte value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00018EE0 File Offset: 0x000170E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(byte value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00018F45 File Offset: 0x00017145
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(byte value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00018F54 File Offset: 0x00017154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(byte value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00018F64 File Offset: 0x00017164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTime value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00018FDC File Offset: 0x000171DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTime value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00019041 File Offset: 0x00017241
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTime value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00019050 File Offset: 0x00017250
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTime value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00019060 File Offset: 0x00017260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTimeOffset value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000190D8 File Offset: 0x000172D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTimeOffset value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001913D File Offset: 0x0001733D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTimeOffset value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001914C File Offset: 0x0001734C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTimeOffset value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0001915C File Offset: 0x0001735C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(decimal value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000191D4 File Offset: 0x000173D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(decimal value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00019239 File Offset: 0x00017439
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(decimal value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00019248 File Offset: 0x00017448
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(decimal value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00019258 File Offset: 0x00017458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(double value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000192D0 File Offset: 0x000174D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(double value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00019335 File Offset: 0x00017535
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(double value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00019344 File Offset: 0x00017544
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(double value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00019354 File Offset: 0x00017554
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(short value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000193CC File Offset: 0x000175CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(short value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00019431 File Offset: 0x00017631
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(short value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00019440 File Offset: 0x00017640
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(short value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00019450 File Offset: 0x00017650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(int value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000194C8 File Offset: 0x000176C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(int value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0001952D File Offset: 0x0001772D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(int value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0001953C File Offset: 0x0001773C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(int value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0001954C File Offset: 0x0001774C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(long value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000195C4 File Offset: 0x000177C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(long value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00019629 File Offset: 0x00017829
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(long value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00019638 File Offset: 0x00017838
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(long value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00019648 File Offset: 0x00017848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(sbyte value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000196C0 File Offset: 0x000178C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(sbyte value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00019725 File Offset: 0x00017925
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(sbyte value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00019734 File Offset: 0x00017934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(sbyte value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00019744 File Offset: 0x00017944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(float value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000197BC File Offset: 0x000179BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(float value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00019821 File Offset: 0x00017A21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(float value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00019830 File Offset: 0x00017A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(float value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00019840 File Offset: 0x00017A40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(TimeSpan value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000198B8 File Offset: 0x00017AB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(TimeSpan value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0001991D File Offset: 0x00017B1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(TimeSpan value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001992C File Offset: 0x00017B2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(TimeSpan value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0001993C File Offset: 0x00017B3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ushort value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000199B4 File Offset: 0x00017BB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ushort value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00019A19 File Offset: 0x00017C19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ushort value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00019A28 File Offset: 0x00017C28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ushort value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00019A38 File Offset: 0x00017C38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(uint value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00019AB0 File Offset: 0x00017CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(uint value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00019B15 File Offset: 0x00017D15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(uint value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00019B24 File Offset: 0x00017D24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(uint value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00019B34 File Offset: 0x00017D34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ulong value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00019BAC File Offset: 0x00017DAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ulong value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00019C11 File Offset: 0x00017E11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ulong value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00019C20 File Offset: 0x00017E20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ulong value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00019C30 File Offset: 0x00017E30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(Guid value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00019CA8 File Offset: 0x00017EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(Guid value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00019D0D File Offset: 0x00017F0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(Guid value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00019D1C File Offset: 0x00017F1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(Guid value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00019D2C File Offset: 0x00017F2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(bool value)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00019DA4 File Offset: 0x00017FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(bool value, StandardFormat format)
		{
			int num;
			if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
			{
				this.Grow(num);
				if (!Utf8Formatter.TryFormat(value, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00019E09 File Offset: 0x00018009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(bool value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00019E18 File Offset: 0x00018018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(bool value, StandardFormat format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00019E28 File Offset: 0x00018028
		static Utf8ValueStringBuilder()
		{
			byte[] bytes = Utf8ValueStringBuilder.UTF8NoBom.GetBytes(Environment.NewLine);
			if (bytes.Length == 1)
			{
				Utf8ValueStringBuilder.newLine1 = bytes[0];
				Utf8ValueStringBuilder.crlf = false;
				return;
			}
			Utf8ValueStringBuilder.newLine1 = bytes[0];
			Utf8ValueStringBuilder.newLine2 = bytes[1];
			Utf8ValueStringBuilder.crlf = true;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00019E7B File Offset: 0x0001807B
		public int Length
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00019E83 File Offset: 0x00018083
		[NullableContext(0)]
		public ReadOnlySpan<byte> AsSpan()
		{
			return this.buffer.AsSpan<byte>(0, this.index);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00019E9C File Offset: 0x0001809C
		[NullableContext(0)]
		public ReadOnlyMemory<byte> AsMemory()
		{
			return this.buffer.AsMemory<byte>(0, this.index);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00019EB5 File Offset: 0x000180B5
		[NullableContext(0)]
		public ArraySegment<byte> AsArraySegment()
		{
			return new ArraySegment<byte>(this.buffer, 0, this.index);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00019ECC File Offset: 0x000180CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Utf8ValueStringBuilder(bool disposeImmediately)
		{
			if (disposeImmediately && Utf8ValueStringBuilder.scratchBufferUsed)
			{
				Utf8ValueStringBuilder.ThrowNestedException();
			}
			byte[] array;
			if (disposeImmediately)
			{
				array = Utf8ValueStringBuilder.scratchBuffer;
				if (array == null)
				{
					array = (Utf8ValueStringBuilder.scratchBuffer = new byte[64444]);
				}
				Utf8ValueStringBuilder.scratchBufferUsed = true;
			}
			else
			{
				array = ArrayPool<byte>.Shared.Rent(65536);
			}
			this.buffer = array;
			this.index = 0;
			this.disposeImmediately = disposeImmediately;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00019F34 File Offset: 0x00018134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this.buffer != null)
			{
				if (this.buffer.Length != 64444)
				{
					ArrayPool<byte>.Shared.Return(this.buffer, false);
				}
				this.buffer = null;
				this.index = 0;
				if (this.disposeImmediately)
				{
					Utf8ValueStringBuilder.scratchBufferUsed = false;
				}
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00019F85 File Offset: 0x00018185
		public void Clear()
		{
			this.index = 0;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00019F8E File Offset: 0x0001818E
		public void TryGrow(int sizeHint)
		{
			if (this.buffer.Length < this.index + sizeHint)
			{
				this.Grow(sizeHint);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00019FAC File Offset: 0x000181AC
		public void Grow(int sizeHint)
		{
			int num = this.buffer.Length * 2;
			if (sizeHint != 0)
			{
				num = Math.Max(num, this.index + sizeHint);
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(num);
			this.buffer.CopyTo(array, 0);
			if (this.buffer.Length != 64444)
			{
				ArrayPool<byte>.Shared.Return(this.buffer, false);
			}
			this.buffer = array;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001A018 File Offset: 0x00018218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine()
		{
			if (Utf8ValueStringBuilder.crlf)
			{
				if (this.buffer.Length - this.index < 2)
				{
					this.Grow(2);
				}
				this.buffer[this.index] = Utf8ValueStringBuilder.newLine1;
				this.buffer[this.index + 1] = Utf8ValueStringBuilder.newLine2;
				this.index += 2;
				return;
			}
			if (this.buffer.Length - this.index < 1)
			{
				this.Grow(1);
			}
			this.buffer[this.index] = Utf8ValueStringBuilder.newLine1;
			this.index++;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001A0B4 File Offset: 0x000182B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char value)
		{
			int maxByteCount = Utf8ValueStringBuilder.UTF8NoBom.GetMaxByteCount(1);
			if (this.buffer.Length - this.index < maxByteCount)
			{
				this.Grow(maxByteCount);
			}
			fixed (byte* ptr = &this.buffer[this.index])
			{
				byte* ptr2 = ptr;
				this.index += Utf8ValueStringBuilder.UTF8NoBom.GetBytes(&value, 1, ptr2, maxByteCount);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001A11C File Offset: 0x0001831C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount");
			}
			if (value <= '\u007f')
			{
				this.GetSpan(repeatCount).Fill((byte)value);
				this.Advance(repeatCount);
				return;
			}
			int maxByteCount = Utf8ValueStringBuilder.UTF8NoBom.GetMaxByteCount(1);
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)maxByteCount], maxByteCount);
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)value;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			int bytes = Utf8ValueStringBuilder.UTF8NoBom.GetBytes(readOnlySpan, span);
			this.TryGrow(bytes * repeatCount);
			for (int i = 0; i < repeatCount; i++)
			{
				span.CopyTo(this.GetSpan(bytes));
				this.Advance(bytes);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001A1BE File Offset: 0x000183BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(char value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001A1CD File Offset: 0x000183CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(string value, int startIndex, int count)
		{
			if (value != null)
			{
				this.Append(value.AsSpan(startIndex, count));
				return;
			}
			if (startIndex == 0 && count == 0)
			{
				return;
			}
			throw new ArgumentNullException("value");
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0001A1F2 File Offset: 0x000183F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(string value)
		{
			this.Append(value.AsSpan());
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0001A200 File Offset: 0x00018400
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(string value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001A210 File Offset: 0x00018410
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ReadOnlySpan<char> value)
		{
			int maxByteCount = Utf8ValueStringBuilder.UTF8NoBom.GetMaxByteCount(value.Length);
			if (this.buffer.Length - this.index < maxByteCount)
			{
				this.Grow(maxByteCount);
			}
			this.index += Utf8ValueStringBuilder.UTF8NoBom.GetBytes(value, this.buffer.AsSpan<byte>(this.index));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0001A271 File Offset: 0x00018471
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ReadOnlySpan<char> value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0001A280 File Offset: 0x00018480
		[NullableContext(0)]
		public void AppendLiteral(ReadOnlySpan<byte> value)
		{
			if (this.buffer.Length - this.index < value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this.buffer.AsSpan<byte>(this.index));
			this.index += value.Length;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001A2E0 File Offset: 0x000184E0
		public void Append<[Nullable(2)] T>(T value)
		{
			int num;
			if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
			{
				this.Grow(num);
				if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(value, this.buffer.AsSpan<byte>(this.index), out num, default(StandardFormat)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001A35F File Offset: 0x0001855F
		public void AppendLine<[Nullable(2)] T>(T value)
		{
			this.Append<T>(value);
			this.AppendLine();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0001A370 File Offset: 0x00018570
		public void CopyTo(IBufferWriter<byte> bufferWriter)
		{
			Span<byte> span = bufferWriter.GetSpan(this.index);
			int num;
			this.TryCopyTo(span, out num);
			bufferWriter.Advance(num);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001A39C File Offset: 0x0001859C
		[NullableContext(0)]
		public bool TryCopyTo(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length < this.index)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = this.index;
			this.buffer.AsSpan<byte>(0, this.index).CopyTo(destination);
			return true;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001A3E1 File Offset: 0x000185E1
		public Task WriteToAsync(Stream stream)
		{
			return stream.WriteAsync(this.buffer, 0, this.index);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0001A3F6 File Offset: 0x000185F6
		public Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
		{
			return stream.WriteAsync(this.buffer, 0, this.index, cancellationToken);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001A40C File Offset: 0x0001860C
		public override string ToString()
		{
			if (this.index == 0)
			{
				return string.Empty;
			}
			return Utf8ValueStringBuilder.UTF8NoBom.GetString(this.buffer, 0, this.index);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001A433 File Offset: 0x00018633
		[NullableContext(0)]
		public Memory<byte> GetMemory(int sizeHint)
		{
			if (this.buffer.Length - this.index < sizeHint)
			{
				this.Grow(sizeHint);
			}
			return this.buffer.AsMemory<byte>(this.index);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0001A45F File Offset: 0x0001865F
		[NullableContext(0)]
		public Span<byte> GetSpan(int sizeHint)
		{
			if (this.buffer.Length - this.index < sizeHint)
			{
				this.Grow(sizeHint);
			}
			return this.buffer.AsSpan<byte>(this.index);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001A48B File Offset: 0x0001868B
		public void Advance(int count)
		{
			this.index += count;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001A49B File Offset: 0x0001869B
		void IResettableBufferWriter<byte>.Reset()
		{
			this.index = 0;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001A4A4 File Offset: 0x000186A4
		private void ThrowArgumentException(string paramName)
		{
			throw new ArgumentException("Can't format argument.", paramName);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001A4B1 File Offset: 0x000186B1
		private void ThrowFormatException()
		{
			throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001A4BD File Offset: 0x000186BD
		private static void ThrowNestedException()
		{
			throw new NestedStringBuilderCreationException("Utf8ValueStringBuilder", "");
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001A4D0 File Offset: 0x000186D0
		private unsafe void AppendFormatInternal<[Nullable(2)] T>(T arg, int width, StandardFormat format, string argName)
		{
			if (width <= 0)
			{
				width *= -1;
				int num;
				if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, this.buffer.AsSpan<byte>(this.index), out num, format))
				{
					this.Grow(num);
					if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, this.buffer.AsSpan<byte>(this.index), out num, format))
					{
						this.ThrowArgumentException(argName);
					}
				}
				this.index += num;
				int num2 = width - num;
				if (width > 0 && num2 > 0)
				{
					this.Append(' ', num2);
					return;
				}
			}
			else
			{
				if (typeof(T) == typeof(string))
				{
					string text = Unsafe.As<string>(arg);
					int num3 = width - text.Length;
					if (num3 > 0)
					{
						this.Append(' ', num3);
					}
					this.Append(text);
					return;
				}
				int num4 = (typeof(T).IsValueType ? (Unsafe.SizeOf<T>() * 8) : 1024);
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)num4], num4);
				int num5;
				if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num5, format))
				{
					num4 = span.Length * 2;
					span = new Span<byte>(stackalloc byte[(UIntPtr)num4], num4);
					if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num5, format))
					{
						this.ThrowArgumentException(argName);
					}
				}
				int num6 = width - num5;
				if (num6 > 0)
				{
					this.Append(' ', num6);
				}
				span.CopyTo(this.GetSpan(num5));
				this.Advance(num5);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001A649 File Offset: 0x00018849
		public static void RegisterTryFormat<[Nullable(2)] T>(Utf8ValueStringBuilder.TryFormat<T> formatMethod)
		{
			Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate = formatMethod;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001A651 File Offset: 0x00018851
		[NullableContext(0)]
		[return: Nullable(new byte[] { 1, 0 })]
		private static Utf8ValueStringBuilder.TryFormat<T?> CreateNullableFormatter<T>() where T : struct
		{
			return delegate(T? x, Span<byte> destination, out int written, StandardFormat format)
			{
				if (x == null)
				{
					written = 0;
					return true;
				}
				return Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(x.Value, destination, out written, format);
			};
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001A672 File Offset: 0x00018872
		[NullableContext(0)]
		public static void EnableNullableFormat<T>() where T : struct
		{
			Utf8ValueStringBuilder.RegisterTryFormat<T?>(Utf8ValueStringBuilder.CreateNullableFormatter<T>());
		}

		// Token: 0x040000C5 RID: 197
		private const int ThreadStaticBufferSize = 64444;

		// Token: 0x040000C6 RID: 198
		private const int DefaultBufferSize = 65536;

		// Token: 0x040000C7 RID: 199
		private static Encoding UTF8NoBom = new UTF8Encoding(false);

		// Token: 0x040000C8 RID: 200
		private static byte newLine1;

		// Token: 0x040000C9 RID: 201
		private static byte newLine2;

		// Token: 0x040000CA RID: 202
		private static bool crlf;

		// Token: 0x040000CB RID: 203
		[Nullable(2)]
		[ThreadStatic]
		private static byte[] scratchBuffer;

		// Token: 0x040000CC RID: 204
		[ThreadStatic]
		internal static bool scratchBufferUsed;

		// Token: 0x040000CD RID: 205
		[Nullable(2)]
		private byte[] buffer;

		// Token: 0x040000CE RID: 206
		private int index;

		// Token: 0x040000CF RID: 207
		private bool disposeImmediately;

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x060003D2 RID: 978
		[NullableContext(0)]
		public delegate bool TryFormat<[Nullable(2)] T>([Nullable(1)] T value, Span<byte> destination, out int written, StandardFormat format);

		// Token: 0x02000057 RID: 87
		[NullableContext(0)]
		public static class FormatterCache<[Nullable(2)] T>
		{
			// Token: 0x060003D5 RID: 981 RVA: 0x00024894 File Offset: 0x00022A94
			static FormatterCache()
			{
				Utf8ValueStringBuilder.TryFormat<T> tryFormat = (Utf8ValueStringBuilder.TryFormat<T>)Utf8ValueStringBuilder.CreateFormatter(typeof(T));
				if (tryFormat == null)
				{
					if (typeof(T).IsEnum)
					{
						tryFormat = new Utf8ValueStringBuilder.TryFormat<T>(EnumUtil<T>.TryFormatUtf8);
					}
					else
					{
						tryFormat = new Utf8ValueStringBuilder.TryFormat<T>(Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDefault);
					}
				}
				Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate = tryFormat;
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x000248EC File Offset: 0x00022AEC
			private static bool TryFormatDefault([Nullable(1)] T value, Span<byte> dest, out int written, StandardFormat format)
			{
				if (value == null)
				{
					written = 0;
					return true;
				}
				string text;
				if (!(typeof(T) == typeof(string)))
				{
					IFormattable formattable = value as IFormattable;
					text = ((formattable != null && format != default(StandardFormat)) ? formattable.ToString(format.ToString(), null) : value.ToString());
				}
				else
				{
					text = Unsafe.As<string>(value);
				}
				string text2 = text;
				written = Utf8ValueStringBuilder.UTF8NoBom.GetMaxByteCount(text2.Length);
				if (dest.Length < written)
				{
					return false;
				}
				written = Utf8ValueStringBuilder.UTF8NoBom.GetBytes(text2.AsSpan(), dest);
				return true;
			}

			// Token: 0x04000143 RID: 323
			[Nullable(1)]
			public static Utf8ValueStringBuilder.TryFormat<T> TryFormatDelegate;
		}
	}
}
