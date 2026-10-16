using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	public struct Utf16ValueStringBuilder : IDisposable, IBufferWriter<char>, IResettableBufferWriter<char>
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000F794 File Offset: 0x0000D994
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, params T[] values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values.AsSpan<T>());
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000F7C8 File Offset: 0x0000D9C8
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, List<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		[NullableContext(2)]
		public unsafe void AppendJoin<T>(char separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000F818 File Offset: 0x0000DA18
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IEnumerable<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F840 File Offset: 0x0000DA40
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, ICollection<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values.AsEnumerable<T>());
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000F870 File Offset: 0x0000DA70
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IList<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F898 File Offset: 0x0000DA98
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IReadOnlyList<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		public unsafe void AppendJoin<[Nullable(2)] T>(char separator, IReadOnlyCollection<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			ReadOnlySpan<char> readOnlySpan = new Span<char>(intPtr, 1);
			this.AppendJoinInternal<T>(readOnlySpan, values.AsEnumerable<T>());
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000F8ED File Offset: 0x0000DAED
		public void AppendJoin<[Nullable(2)] T>(string separator, params T[] values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values.AsSpan<T>());
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F906 File Offset: 0x0000DB06
		public void AppendJoin<[Nullable(2)] T>(string separator, List<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F915 File Offset: 0x0000DB15
		public void AppendJoin<[Nullable(2)] T>(string separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F924 File Offset: 0x0000DB24
		public void AppendJoin<[Nullable(2)] T>(string separator, IEnumerable<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F933 File Offset: 0x0000DB33
		public void AppendJoin<[Nullable(2)] T>(string separator, ICollection<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values.AsEnumerable<T>());
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F947 File Offset: 0x0000DB47
		public void AppendJoin<[Nullable(2)] T>(string separator, IList<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F956 File Offset: 0x0000DB56
		public void AppendJoin<[Nullable(2)] T>(string separator, IReadOnlyList<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F965 File Offset: 0x0000DB65
		public void AppendJoin<[Nullable(2)] T>(string separator, IReadOnlyCollection<T> values)
		{
			this.AppendJoinInternal<T>(separator.AsSpan(), values.AsEnumerable<T>());
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F97C File Offset: 0x0000DB7C
		[NullableContext(0)]
		internal void AppendJoinInternal<[Nullable(2)] T>(ReadOnlySpan<char> separator, [Nullable(1)] IList<T> values)
		{
			IReadOnlyList<T> readOnlyList = values as IReadOnlyList<T>;
			readOnlyList = readOnlyList ?? new ReadOnlyListAdaptor<T>(values);
			this.AppendJoinInternal<T>(separator, readOnlyList);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F9AC File Offset: 0x0000DBAC
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

		// Token: 0x060001AB RID: 427 RVA: 0x0000FA20 File Offset: 0x0000DC20
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

		// Token: 0x060001AC RID: 428 RVA: 0x0000FA98 File Offset: 0x0000DC98
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

		// Token: 0x060001AD RID: 429 RVA: 0x0000FB28 File Offset: 0x0000DD28
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						if (parseResult.Index == 0)
						{
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
						}
						else
						{
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000FC54 File Offset: 0x0000DE54
		[NullableContext(0)]
		public unsafe void AppendFormat<[Nullable(2)] T1>(ReadOnlySpan<char> format, [Nullable(1)] T1 arg1)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						if (parseResult.Index == 0)
						{
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
						}
						else
						{
							Utf16ValueStringBuilder.ThrowFormatException();
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000FD98 File Offset: 0x0000DF98
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						int num4 = parseResult.Index;
						if (num4 != 0)
						{
							if (num4 != 1)
							{
								Utf16ValueStringBuilder.ThrowFormatException();
							}
							else
							{
								this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							}
						}
						else
						{
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num6 = format.Length - num;
			if (num6 > 0)
			{
				this.Append(format, num, num6);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						int num4 = parseResult.Index;
						if (num4 != 0)
						{
							if (num4 != 1)
							{
								Utf16ValueStringBuilder.ThrowFormatException();
							}
							else
							{
								this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							}
						}
						else
						{
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num5 = i - num;
						this.Append(format.Slice(num, num5));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num6 = format.Length - num;
			if (num6 > 0)
			{
				this.Append(format.Slice(num, num6));
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00010054 File Offset: 0x0000E254
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000101CC File Offset: 0x0000E3CC
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00010358 File Offset: 0x0000E558
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000104F0 File Offset: 0x0000E6F0
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000106A0 File Offset: 0x0000E8A0
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00010860 File Offset: 0x0000EA60
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00010A34 File Offset: 0x0000EC34
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00010C14 File Offset: 0x0000EE14
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00010E08 File Offset: 0x0000F008
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001100C File Offset: 0x0000F20C
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00011224 File Offset: 0x0000F424
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00011448 File Offset: 0x0000F648
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00011680 File Offset: 0x0000F880
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00011B24 File Offset: 0x0000FD24
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00011D8C File Offset: 0x0000FF8C
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00012008 File Offset: 0x00010208
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00012294 File Offset: 0x00010494
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00012534 File Offset: 0x00010734
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000127E0 File Offset: 0x000109E0
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00012AA0 File Offset: 0x00010CA0
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00012D70 File Offset: 0x00010F70
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00013054 File Offset: 0x00011254
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, parseResult.FormatString, "arg14");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00013344 File Offset: 0x00011544
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, parseResult.FormatString, "arg14");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00013648 File Offset: 0x00011848
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, parseResult.FormatString, "arg14");
							break;
						case 14:
							this.AppendFormatInternal<T15>(arg15, parseResult.Alignment, parseResult.FormatString, "arg15");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001395C File Offset: 0x00011B5C
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, parseResult.FormatString, "arg14");
							break;
						case 14:
							this.AppendFormatInternal<T15>(arg15, parseResult.Alignment, parseResult.FormatString, "arg15");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00013C84 File Offset: 0x00011E84
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
						this.Append(format, num, num3);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, parseResult.FormatString, "arg14");
							break;
						case 14:
							this.AppendFormatInternal<T15>(arg15, parseResult.Alignment, parseResult.FormatString, "arg15");
							break;
						case 15:
							this.AppendFormatInternal<T16>(arg16, parseResult.Alignment, parseResult.FormatString, "arg16");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
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
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format, num, num5);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00013FB8 File Offset: 0x000121B8
		public unsafe void AppendFormat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15, [Nullable(2)] T16>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			int num = 0;
			for (int i = 0; i < format.Length; i++)
			{
				char c = (char)(*format[i]);
				if (c == '{')
				{
					if (i == format.Length - 1)
					{
						throw new FormatException("invalid format");
					}
					if (i != format.Length && *format[i + 1] == 123)
					{
						int num2 = i - num;
						this.Append(format.Slice(num, num2));
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						this.Append(format.Slice(num, num3));
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						switch (parseResult.Index)
						{
						case 0:
							this.AppendFormatInternal<T1>(arg1, parseResult.Alignment, parseResult.FormatString, "arg1");
							break;
						case 1:
							this.AppendFormatInternal<T2>(arg2, parseResult.Alignment, parseResult.FormatString, "arg2");
							break;
						case 2:
							this.AppendFormatInternal<T3>(arg3, parseResult.Alignment, parseResult.FormatString, "arg3");
							break;
						case 3:
							this.AppendFormatInternal<T4>(arg4, parseResult.Alignment, parseResult.FormatString, "arg4");
							break;
						case 4:
							this.AppendFormatInternal<T5>(arg5, parseResult.Alignment, parseResult.FormatString, "arg5");
							break;
						case 5:
							this.AppendFormatInternal<T6>(arg6, parseResult.Alignment, parseResult.FormatString, "arg6");
							break;
						case 6:
							this.AppendFormatInternal<T7>(arg7, parseResult.Alignment, parseResult.FormatString, "arg7");
							break;
						case 7:
							this.AppendFormatInternal<T8>(arg8, parseResult.Alignment, parseResult.FormatString, "arg8");
							break;
						case 8:
							this.AppendFormatInternal<T9>(arg9, parseResult.Alignment, parseResult.FormatString, "arg9");
							break;
						case 9:
							this.AppendFormatInternal<T10>(arg10, parseResult.Alignment, parseResult.FormatString, "arg10");
							break;
						case 10:
							this.AppendFormatInternal<T11>(arg11, parseResult.Alignment, parseResult.FormatString, "arg11");
							break;
						case 11:
							this.AppendFormatInternal<T12>(arg12, parseResult.Alignment, parseResult.FormatString, "arg12");
							break;
						case 12:
							this.AppendFormatInternal<T13>(arg13, parseResult.Alignment, parseResult.FormatString, "arg13");
							break;
						case 13:
							this.AppendFormatInternal<T14>(arg14, parseResult.Alignment, parseResult.FormatString, "arg14");
							break;
						case 14:
							this.AppendFormatInternal<T15>(arg15, parseResult.Alignment, parseResult.FormatString, "arg15");
							break;
						case 15:
							this.AppendFormatInternal<T16>(arg16, parseResult.Alignment, parseResult.FormatString, "arg16");
							break;
						default:
							Utf16ValueStringBuilder.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && *format[i + 1] == 125)
					{
						int num4 = i - num;
						this.Append(format.Slice(num, num4));
						i++;
						num = i;
					}
					else
					{
						Utf16ValueStringBuilder.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				this.Append(format.Slice(num, num5));
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00014300 File Offset: 0x00012500
		[return: Nullable(2)]
		private static object CreateFormatter(Type type)
		{
			if (type == typeof(sbyte))
			{
				return new Utf16ValueStringBuilder.TryFormat<sbyte>(delegate(sbyte x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteInt64(dest, out written, (long)x);
				});
			}
			if (type == typeof(short))
			{
				return new Utf16ValueStringBuilder.TryFormat<short>(delegate(short x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteInt64(dest, out written, (long)x);
				});
			}
			if (type == typeof(int))
			{
				return new Utf16ValueStringBuilder.TryFormat<int>(delegate(int x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteInt64(dest, out written, (long)x);
				});
			}
			if (type == typeof(long))
			{
				return new Utf16ValueStringBuilder.TryFormat<long>(delegate(long x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteInt64(dest, out written, x);
				});
			}
			if (type == typeof(byte))
			{
				return new Utf16ValueStringBuilder.TryFormat<byte>(delegate(byte x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteUInt64(dest, out written, (ulong)x);
				});
			}
			if (type == typeof(ushort))
			{
				return new Utf16ValueStringBuilder.TryFormat<ushort>(delegate(ushort x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteUInt64(dest, out written, (ulong)x);
				});
			}
			if (type == typeof(uint))
			{
				return new Utf16ValueStringBuilder.TryFormat<uint>(delegate(uint x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteUInt64(dest, out written, (ulong)x);
				});
			}
			if (type == typeof(ulong))
			{
				return new Utf16ValueStringBuilder.TryFormat<ulong>(delegate(ulong x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (format.Length != 0)
					{
						return x.TryFormat(dest, out written, format, null);
					}
					return FastNumberWriter.TryWriteUInt64(dest, out written, x);
				});
			}
			if (type == typeof(float))
			{
				return new Utf16ValueStringBuilder.TryFormat<float>(delegate(float x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(double))
			{
				return new Utf16ValueStringBuilder.TryFormat<double>(delegate(double x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(TimeSpan))
			{
				return new Utf16ValueStringBuilder.TryFormat<TimeSpan>(delegate(TimeSpan x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(DateTime))
			{
				return new Utf16ValueStringBuilder.TryFormat<DateTime>(delegate(DateTime x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(DateTimeOffset))
			{
				return new Utf16ValueStringBuilder.TryFormat<DateTimeOffset>(delegate(DateTimeOffset x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(decimal))
			{
				return new Utf16ValueStringBuilder.TryFormat<decimal>(delegate(decimal x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(Guid))
			{
				return new Utf16ValueStringBuilder.TryFormat<Guid>(delegate(Guid x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					return x.TryFormat(dest, out written, format);
				});
			}
			if (type == typeof(byte?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<byte>();
			}
			if (type == typeof(DateTime?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<DateTime>();
			}
			if (type == typeof(DateTimeOffset?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<DateTimeOffset>();
			}
			if (type == typeof(decimal?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<decimal>();
			}
			if (type == typeof(double?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<double>();
			}
			if (type == typeof(short?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<short>();
			}
			if (type == typeof(int?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<int>();
			}
			if (type == typeof(long?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<long>();
			}
			if (type == typeof(sbyte?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<sbyte>();
			}
			if (type == typeof(float?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<float>();
			}
			if (type == typeof(TimeSpan?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<TimeSpan>();
			}
			if (type == typeof(ushort?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<ushort>();
			}
			if (type == typeof(uint?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<uint>();
			}
			if (type == typeof(ulong?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<ulong>();
			}
			if (type == typeof(Guid?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<Guid>();
			}
			if (type == typeof(bool?))
			{
				return Utf16ValueStringBuilder.CreateNullableFormatter<bool>();
			}
			if (type == typeof(IntPtr))
			{
				return new Utf16ValueStringBuilder.TryFormat<IntPtr>(delegate(IntPtr x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (IntPtr.Size != 4)
					{
						return x.ToInt64().TryFormat(dest, out written, format, null);
					}
					return x.ToInt32().TryFormat(dest, out written, format, null);
				});
			}
			if (type == typeof(UIntPtr))
			{
				return new Utf16ValueStringBuilder.TryFormat<UIntPtr>(delegate(UIntPtr x, Span<char> dest, out int written, ReadOnlySpan<char> format)
				{
					if (UIntPtr.Size != 4)
					{
						return x.ToUInt64().TryFormat(dest, out written, format, null);
					}
					return x.ToUInt32().TryFormat(dest, out written, format, null);
				});
			}
			return null;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000147E0 File Offset: 0x000129E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(byte value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001485C File Offset: 0x00012A5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(byte value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000148CF File Offset: 0x00012ACF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(byte value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000148DE File Offset: 0x00012ADE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(byte value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000148F0 File Offset: 0x00012AF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTime value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001496C File Offset: 0x00012B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTime value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000149DF File Offset: 0x00012BDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTime value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000149EE File Offset: 0x00012BEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTime value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00014A00 File Offset: 0x00012C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTimeOffset value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00014A7C File Offset: 0x00012C7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(DateTimeOffset value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00014AEF File Offset: 0x00012CEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTimeOffset value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00014AFE File Offset: 0x00012CFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(DateTimeOffset value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00014B10 File Offset: 0x00012D10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(decimal value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00014B8C File Offset: 0x00012D8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(decimal value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00014BFF File Offset: 0x00012DFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(decimal value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00014C0E File Offset: 0x00012E0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(decimal value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00014C20 File Offset: 0x00012E20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(double value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00014C9C File Offset: 0x00012E9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(double value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00014D0F File Offset: 0x00012F0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(double value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00014D1E File Offset: 0x00012F1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(double value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00014D30 File Offset: 0x00012F30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(short value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00014DAC File Offset: 0x00012FAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(short value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00014E1F File Offset: 0x0001301F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(short value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00014E2E File Offset: 0x0001302E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(short value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00014E40 File Offset: 0x00013040
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(int value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00014EBC File Offset: 0x000130BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(int value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00014F2F File Offset: 0x0001312F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(int value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00014F3E File Offset: 0x0001313E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(int value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00014F50 File Offset: 0x00013150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(long value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00014FCC File Offset: 0x000131CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(long value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001503F File Offset: 0x0001323F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(long value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0001504E File Offset: 0x0001324E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(long value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00015060 File Offset: 0x00013260
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(sbyte value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000150DC File Offset: 0x000132DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(sbyte value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0001514F File Offset: 0x0001334F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(sbyte value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001515E File Offset: 0x0001335E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(sbyte value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00015170 File Offset: 0x00013370
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(float value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000151EC File Offset: 0x000133EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(float value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0001525F File Offset: 0x0001345F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(float value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0001526E File Offset: 0x0001346E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(float value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00015280 File Offset: 0x00013480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(TimeSpan value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000152FC File Offset: 0x000134FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(TimeSpan value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0001536F File Offset: 0x0001356F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(TimeSpan value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0001537E File Offset: 0x0001357E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(TimeSpan value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00015390 File Offset: 0x00013590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ushort value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0001540C File Offset: 0x0001360C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ushort value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0001547F File Offset: 0x0001367F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ushort value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0001548E File Offset: 0x0001368E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ushort value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000154A0 File Offset: 0x000136A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(uint value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0001551C File Offset: 0x0001371C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(uint value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0001558F File Offset: 0x0001378F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(uint value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001559E File Offset: 0x0001379E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(uint value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000155B0 File Offset: 0x000137B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ulong value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0001562C File Offset: 0x0001382C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ulong value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan(), null))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001569F File Offset: 0x0001389F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ulong value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000156AE File Offset: 0x000138AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ulong value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000156C0 File Offset: 0x000138C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(Guid value)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>)))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00015738 File Offset: 0x00013938
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(Guid value, string format)
		{
			int num;
			if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan()))
			{
				this.Grow(num);
				if (!value.TryFormat(this.buffer.AsSpan<char>(this.index), out num, format.AsSpan()))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000157A9 File Offset: 0x000139A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(Guid value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000157B8 File Offset: 0x000139B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(Guid value, string format)
		{
			this.Append(value, format);
			this.AppendLine();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000157C8 File Offset: 0x000139C8
		static Utf16ValueStringBuilder()
		{
			char[] array = Environment.NewLine.ToCharArray();
			if (array.Length == 1)
			{
				Utf16ValueStringBuilder.newLine1 = array[0];
				Utf16ValueStringBuilder.crlf = false;
				return;
			}
			Utf16ValueStringBuilder.newLine1 = array[0];
			Utf16ValueStringBuilder.newLine2 = array[1];
			Utf16ValueStringBuilder.crlf = true;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0001580B File Offset: 0x00013A0B
		public int Length
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00015813 File Offset: 0x00013A13
		[NullableContext(0)]
		public ReadOnlySpan<char> AsSpan()
		{
			return this.buffer.AsSpan<char>(0, this.index);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001582C File Offset: 0x00013A2C
		[NullableContext(0)]
		public ReadOnlyMemory<char> AsMemory()
		{
			return this.buffer.AsMemory<char>(0, this.index);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00015845 File Offset: 0x00013A45
		[NullableContext(0)]
		public ArraySegment<char> AsArraySegment()
		{
			return new ArraySegment<char>(this.buffer, 0, this.index);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0001585C File Offset: 0x00013A5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Utf16ValueStringBuilder(bool disposeImmediately)
		{
			if (disposeImmediately && Utf16ValueStringBuilder.scratchBufferUsed)
			{
				Utf16ValueStringBuilder.ThrowNestedException();
			}
			char[] array;
			if (disposeImmediately)
			{
				array = Utf16ValueStringBuilder.scratchBuffer;
				if (array == null)
				{
					array = (Utf16ValueStringBuilder.scratchBuffer = new char[31111]);
				}
				Utf16ValueStringBuilder.scratchBufferUsed = true;
			}
			else
			{
				array = ArrayPool<char>.Shared.Rent(32768);
			}
			this.buffer = array;
			this.index = 0;
			this.disposeImmediately = disposeImmediately;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000158C4 File Offset: 0x00013AC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (this.buffer != null)
			{
				if (this.buffer.Length != 31111)
				{
					ArrayPool<char>.Shared.Return(this.buffer, false);
				}
				this.buffer = null;
				this.index = 0;
				if (this.disposeImmediately)
				{
					Utf16ValueStringBuilder.scratchBufferUsed = false;
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00015915 File Offset: 0x00013B15
		public void Clear()
		{
			this.index = 0;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0001591E File Offset: 0x00013B1E
		public void TryGrow(int sizeHint)
		{
			if (this.buffer.Length < this.index + sizeHint)
			{
				this.Grow(sizeHint);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001593C File Offset: 0x00013B3C
		public void Grow(int sizeHint)
		{
			int num = this.buffer.Length * 2;
			if (sizeHint != 0)
			{
				num = Math.Max(num, this.index + sizeHint);
			}
			char[] array = ArrayPool<char>.Shared.Rent(num);
			this.buffer.CopyTo(array, 0);
			if (this.buffer.Length != 31111)
			{
				ArrayPool<char>.Shared.Return(this.buffer, false);
			}
			this.buffer = array;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000159A8 File Offset: 0x00013BA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine()
		{
			if (Utf16ValueStringBuilder.crlf)
			{
				if (this.buffer.Length - this.index < 2)
				{
					this.Grow(2);
				}
				this.buffer[this.index] = Utf16ValueStringBuilder.newLine1;
				this.buffer[this.index + 1] = Utf16ValueStringBuilder.newLine2;
				this.index += 2;
				return;
			}
			if (this.buffer.Length - this.index < 1)
			{
				this.Grow(1);
			}
			this.buffer[this.index] = Utf16ValueStringBuilder.newLine1;
			this.index++;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00015A44 File Offset: 0x00013C44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(char value)
		{
			if (this.buffer.Length - this.index < 1)
			{
				this.Grow(1);
			}
			char[] array = this.buffer;
			int num = this.index;
			this.index = num + 1;
			array[num] = value;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00015A84 File Offset: 0x00013C84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount");
			}
			this.GetSpan(repeatCount).Fill(value);
			this.Advance(repeatCount);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00015AB7 File Offset: 0x00013CB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(char value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00015AC6 File Offset: 0x00013CC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(string value)
		{
			this.Append(value.AsSpan());
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00015AD4 File Offset: 0x00013CD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(string value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00015AE3 File Offset: 0x00013CE3
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

		// Token: 0x0600021B RID: 539 RVA: 0x00015B08 File Offset: 0x00013D08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(char[] value, int startIndex, int charCount)
		{
			if (this.buffer.Length - this.index < charCount)
			{
				this.Grow(charCount);
			}
			Array.Copy(value, startIndex, this.buffer, this.index, charCount);
			this.index += charCount;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00015B48 File Offset: 0x00013D48
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Append(ReadOnlySpan<char> value)
		{
			if (this.buffer.Length - this.index < value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this.buffer.AsSpan<char>(this.index));
			this.index += value.Length;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00015BA6 File Offset: 0x00013DA6
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AppendLine(ReadOnlySpan<char> value)
		{
			this.Append(value);
			this.AppendLine();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00015BB8 File Offset: 0x00013DB8
		public void Append<[Nullable(2)] T>(T value)
		{
			int num;
			if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(value, this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>)))
			{
				this.Grow(num);
				if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(value, this.buffer.AsSpan<char>(this.index), out num, default(ReadOnlySpan<char>)))
				{
					this.ThrowArgumentException("value");
				}
			}
			this.index += num;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00015C37 File Offset: 0x00013E37
		public void AppendLine<[Nullable(2)] T>(T value)
		{
			this.Append<T>(value);
			this.AppendLine();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00015C46 File Offset: 0x00013E46
		public void Insert(int index, string value, int count)
		{
			this.Insert(index, value.AsSpan(), count);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00015C56 File Offset: 0x00013E56
		public void Insert(int index, string value)
		{
			this.Insert(index, value.AsSpan(), 1);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00015C68 File Offset: 0x00013E68
		[NullableContext(0)]
		public void Insert(int index, ReadOnlySpan<char> value, int count)
		{
			if (count < 0)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("count");
			}
			int length = this.Length;
			if (index > length)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("index");
			}
			if (value.Length == 0 || count == 0)
			{
				return;
			}
			int num = index + value.Length * count;
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(32768, num));
			this.buffer.AsSpan<char>(0, index).CopyTo(array);
			int num2 = index;
			for (int i = 0; i < count; i++)
			{
				value.CopyTo(array.AsSpan<char>(num2));
				num2 += value.Length;
			}
			int num3 = this.index - index;
			this.buffer.AsSpan<char>(index, num3).CopyTo(array.AsSpan<char>(num2));
			if (this.buffer.Length != 31111 && this.buffer != null)
			{
				ArrayPool<char>.Shared.Return(this.buffer, false);
			}
			this.buffer = array;
			this.index = num2 + num3;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00015D6E File Offset: 0x00013F6E
		public void Replace(char oldChar, char newChar)
		{
			this.Replace(oldChar, newChar, 0, this.Length);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00015D80 File Offset: 0x00013F80
		public void Replace(char oldChar, char newChar, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex > length - count)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("count");
			}
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (this.buffer[i] == oldChar)
				{
					this.buffer[i] = newChar;
				}
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00015DDD File Offset: 0x00013FDD
		public void Replace(string oldValue, string newValue)
		{
			this.Replace(oldValue, newValue, 0, this.Length);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00015DEE File Offset: 0x00013FEE
		[NullableContext(0)]
		public void Replace(ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue)
		{
			this.Replace(oldValue, newValue, 0, this.Length);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00015DFF File Offset: 0x00013FFF
		public void Replace(string oldValue, string newValue, int startIndex, int count)
		{
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			this.Replace(oldValue.AsSpan(), newValue.AsSpan(), startIndex, count);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00015E24 File Offset: 0x00014024
		[NullableContext(0)]
		public void Replace(ReadOnlySpan<char> oldValue, ReadOnlySpan<char> newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("startIndex");
			}
			if (count < 0 || startIndex > length - count)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("count");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException("oldValue.Length is 0", "oldValue");
			}
			ReadOnlySpan<char> readOnlySpan = this.AsSpan();
			int num = startIndex + count;
			int num2 = 0;
			for (int i = startIndex; i < num; i += oldValue.Length)
			{
				int num3 = readOnlySpan.Slice(i, num - i).IndexOf(oldValue, StringComparison.Ordinal);
				if (num3 == -1)
				{
					break;
				}
				i += num3;
				num2++;
			}
			if (num2 == 0)
			{
				return;
			}
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(32768, this.Length + (newValue.Length - oldValue.Length) * num2));
			this.buffer.AsSpan<char>(0, startIndex).CopyTo(array);
			int num4 = startIndex;
			for (int j = startIndex; j < num; j += oldValue.Length)
			{
				int num5 = readOnlySpan.Slice(j, num - j).IndexOf(oldValue, StringComparison.Ordinal);
				if (num5 == -1)
				{
					ReadOnlySpan<char> readOnlySpan2 = readOnlySpan.Slice(j);
					readOnlySpan2.CopyTo(array.AsSpan<char>(num4));
					num4 += readOnlySpan2.Length;
					break;
				}
				readOnlySpan.Slice(j, num5).CopyTo(array.AsSpan<char>(num4));
				newValue.CopyTo(array.AsSpan<char>(num4 + num5));
				num4 += num5 + newValue.Length;
				j += num5;
			}
			if (this.buffer.Length != 31111)
			{
				ArrayPool<char>.Shared.Return(this.buffer, false);
			}
			this.buffer = array;
			this.index = num4;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00015FE8 File Offset: 0x000141E8
		public void ReplaceAt(char newChar, int replaceIndex)
		{
			int length = this.Length;
			if (replaceIndex > length)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("replaceIndex");
			}
			this.buffer[replaceIndex] = newChar;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00016014 File Offset: 0x00014214
		public void Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("length");
			}
			if (startIndex < 0)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("startIndex");
			}
			if (length > this.Length - startIndex)
			{
				Utf16ValueStringBuilder.ExceptionUtil.ThrowArgumentOutOfRangeException("length");
			}
			if (this.Length == length && startIndex == 0)
			{
				this.index = 0;
				return;
			}
			if (length == 0)
			{
				return;
			}
			int num = startIndex + length;
			this.buffer.AsSpan<char>(num, this.Length - num).CopyTo(this.buffer.AsSpan<char>(startIndex));
			this.index -= length;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000160A4 File Offset: 0x000142A4
		[NullableContext(0)]
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (destination.Length < this.index)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = this.index;
			this.buffer.AsSpan<char>(0, this.index).CopyTo(destination);
			return true;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000160E9 File Offset: 0x000142E9
		public override string ToString()
		{
			if (this.index == 0)
			{
				return string.Empty;
			}
			return new string(this.buffer, 0, this.index);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0001610B File Offset: 0x0001430B
		[NullableContext(0)]
		public Memory<char> GetMemory(int sizeHint)
		{
			if (this.buffer.Length - this.index < sizeHint)
			{
				this.Grow(sizeHint);
			}
			return this.buffer.AsMemory<char>(this.index);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00016137 File Offset: 0x00014337
		[NullableContext(0)]
		public Span<char> GetSpan(int sizeHint)
		{
			if (this.buffer.Length - this.index < sizeHint)
			{
				this.Grow(sizeHint);
			}
			return this.buffer.AsSpan<char>(this.index);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00016163 File Offset: 0x00014363
		public void Advance(int count)
		{
			this.index += count;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00016173 File Offset: 0x00014373
		void IResettableBufferWriter<char>.Reset()
		{
			this.index = 0;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001617C File Offset: 0x0001437C
		private void ThrowArgumentException(string paramName)
		{
			throw new ArgumentException("Can't format argument.", paramName);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00016189 File Offset: 0x00014389
		private static void ThrowFormatException()
		{
			throw new FormatException("Index (zero based) must be greater than or equal to zero and less than the size of the argument list.");
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00016198 File Offset: 0x00014398
		private unsafe void AppendFormatInternal<[Nullable(2)] T>(T arg, int width, [Nullable(0)] ReadOnlySpan<char> format, string argName)
		{
			if (width <= 0)
			{
				width *= -1;
				int num;
				if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, this.buffer.AsSpan<char>(this.index), out num, format))
				{
					this.Grow(num);
					if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, this.buffer.AsSpan<char>(this.index), out num, format))
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
				Span<char> span;
				int num5;
				checked
				{
					span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num4) * 2], num4);
					if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num5, format))
					{
						num4 = unchecked(span.Length * 2);
						span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num4) * 2], num4);
						if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num5, format))
						{
							this.ThrowArgumentException(argName);
						}
					}
				}
				int num6 = width - num5;
				if (num6 > 0)
				{
					this.Append(' ', num6);
				}
				this.Append(span.Slice(0, num5));
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00016313 File Offset: 0x00014513
		private static void ThrowNestedException()
		{
			throw new NestedStringBuilderCreationException("Utf16ValueStringBuilder", "");
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00016324 File Offset: 0x00014524
		public static void RegisterTryFormat<[Nullable(2)] T>(Utf16ValueStringBuilder.TryFormat<T> formatMethod)
		{
			Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate = formatMethod;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001632C File Offset: 0x0001452C
		[NullableContext(0)]
		[return: Nullable(new byte[] { 1, 0 })]
		private static Utf16ValueStringBuilder.TryFormat<T?> CreateNullableFormatter<T>() where T : struct
		{
			return delegate(T? x, Span<char> dest, out int written, ReadOnlySpan<char> format)
			{
				if (x == null)
				{
					written = 0;
					return true;
				}
				return Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(x.Value, dest, out written, format);
			};
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001634D File Offset: 0x0001454D
		[NullableContext(0)]
		public static void EnableNullableFormat<T>() where T : struct
		{
			Utf16ValueStringBuilder.RegisterTryFormat<T?>(Utf16ValueStringBuilder.CreateNullableFormatter<T>());
		}

		// Token: 0x040000BB RID: 187
		private const int ThreadStaticBufferSize = 31111;

		// Token: 0x040000BC RID: 188
		private const int DefaultBufferSize = 32768;

		// Token: 0x040000BD RID: 189
		private static char newLine1;

		// Token: 0x040000BE RID: 190
		private static char newLine2;

		// Token: 0x040000BF RID: 191
		private static bool crlf;

		// Token: 0x040000C0 RID: 192
		[Nullable(2)]
		[ThreadStatic]
		private static char[] scratchBuffer;

		// Token: 0x040000C1 RID: 193
		[ThreadStatic]
		internal static bool scratchBufferUsed;

		// Token: 0x040000C2 RID: 194
		[Nullable(2)]
		private char[] buffer;

		// Token: 0x040000C3 RID: 195
		private int index;

		// Token: 0x040000C4 RID: 196
		private bool disposeImmediately;

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x060003B4 RID: 948
		[NullableContext(0)]
		public delegate bool TryFormat<[Nullable(2)] T>([Nullable(1)] T value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format);

		// Token: 0x02000052 RID: 82
		[NullableContext(0)]
		private static class ExceptionUtil
		{
			// Token: 0x060003B7 RID: 951 RVA: 0x0002452D File Offset: 0x0002272D
			[NullableContext(1)]
			public static void ThrowArgumentOutOfRangeException(string paramName)
			{
				throw new ArgumentOutOfRangeException(paramName);
			}
		}

		// Token: 0x02000053 RID: 83
		[NullableContext(0)]
		public static class FormatterCache<[Nullable(2)] T>
		{
			// Token: 0x060003B8 RID: 952 RVA: 0x00024538 File Offset: 0x00022738
			static FormatterCache()
			{
				Utf16ValueStringBuilder.TryFormat<T> tryFormat = (Utf16ValueStringBuilder.TryFormat<T>)Utf16ValueStringBuilder.CreateFormatter(typeof(T));
				if (tryFormat == null)
				{
					if (typeof(T).IsEnum)
					{
						tryFormat = new Utf16ValueStringBuilder.TryFormat<T>(EnumUtil<T>.TryFormatUtf16);
					}
					else if (typeof(T) == typeof(string))
					{
						tryFormat = new Utf16ValueStringBuilder.TryFormat<T>(Utf16ValueStringBuilder.FormatterCache<T>.TryFormatString);
					}
					else
					{
						tryFormat = new Utf16ValueStringBuilder.TryFormat<T>(Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDefault);
					}
				}
				Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate = tryFormat;
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x000245BC File Offset: 0x000227BC
			private static bool TryFormatString([Nullable(1)] T value, Span<char> dest, out int written, ReadOnlySpan<char> format)
			{
				string text = value as string;
				if (text == null)
				{
					written = 0;
					return true;
				}
				written = text.Length;
				return text.AsSpan().TryCopyTo(dest);
			}

			// Token: 0x060003BA RID: 954 RVA: 0x000245F4 File Offset: 0x000227F4
			private static bool TryFormatDefault([Nullable(1)] T value, Span<char> dest, out int written, ReadOnlySpan<char> format)
			{
				if (value == null)
				{
					written = 0;
					return true;
				}
				IFormattable formattable = value as IFormattable;
				string text = ((formattable != null && format.Length != 0) ? formattable.ToString(format.ToString(), null) : value.ToString());
				written = text.Length;
				return text.AsSpan().TryCopyTo(dest);
			}

			// Token: 0x0400012E RID: 302
			[Nullable(1)]
			public static Utf16ValueStringBuilder.TryFormat<T> TryFormatDelegate;
		}
	}
}
