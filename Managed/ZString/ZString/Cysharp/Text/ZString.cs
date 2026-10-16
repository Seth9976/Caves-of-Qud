using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Cysharp.Text
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	public static class ZString
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0001AE14 File Offset: 0x00019014
		public unsafe static string Concat<[Nullable(2)] T1>(T1 arg1)
		{
			if (!(typeof(T1) == typeof(string)))
			{
				string text;
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
				{
					if (typeof(T1) == typeof(string))
					{
						if (arg1 != null)
						{
							utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
						}
					}
					else if (typeof(T1) == typeof(int))
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
					}
					else
					{
						utf16ValueStringBuilder.Append<T1>(arg1);
					}
					text = utf16ValueStringBuilder.ToString();
				}
				return text;
			}
			if (arg1 == null)
			{
				return string.Empty;
			}
			return Unsafe.As<string>(arg1);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0001AEF0 File Offset: 0x000190F0
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2>(T1 arg1, T2 arg2)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001B000 File Offset: 0x00019200
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(T1 arg1, T2 arg2, T3 arg3)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001B184 File Offset: 0x00019384
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001B370 File Offset: 0x00019570
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001B5C4 File Offset: 0x000197C4
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001B884 File Offset: 0x00019A84
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001BBAC File Offset: 0x00019DAC
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001BF40 File Offset: 0x0001A140
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001C33C File Offset: 0x0001A53C
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				if (typeof(T11) == typeof(string))
				{
					if (arg11 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T11, string>(ref arg11));
					}
				}
				else if (typeof(T11) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T11, int>(ref arg11));
				}
				else
				{
					utf16ValueStringBuilder.Append<T11>(arg11);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				if (typeof(T11) == typeof(string))
				{
					if (arg11 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T11, string>(ref arg11));
					}
				}
				else if (typeof(T11) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T11, int>(ref arg11));
				}
				else
				{
					utf16ValueStringBuilder.Append<T11>(arg11);
				}
				if (typeof(T12) == typeof(string))
				{
					if (arg12 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T12, string>(ref arg12));
					}
				}
				else if (typeof(T12) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T12, int>(ref arg12));
				}
				else
				{
					utf16ValueStringBuilder.Append<T12>(arg12);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				if (typeof(T11) == typeof(string))
				{
					if (arg11 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T11, string>(ref arg11));
					}
				}
				else if (typeof(T11) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T11, int>(ref arg11));
				}
				else
				{
					utf16ValueStringBuilder.Append<T11>(arg11);
				}
				if (typeof(T12) == typeof(string))
				{
					if (arg12 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T12, string>(ref arg12));
					}
				}
				else if (typeof(T12) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T12, int>(ref arg12));
				}
				else
				{
					utf16ValueStringBuilder.Append<T12>(arg12);
				}
				if (typeof(T13) == typeof(string))
				{
					if (arg13 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T13, string>(ref arg13));
					}
				}
				else if (typeof(T13) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T13, int>(ref arg13));
				}
				else
				{
					utf16ValueStringBuilder.Append<T13>(arg13);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001D754 File Offset: 0x0001B954
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				if (typeof(T11) == typeof(string))
				{
					if (arg11 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T11, string>(ref arg11));
					}
				}
				else if (typeof(T11) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T11, int>(ref arg11));
				}
				else
				{
					utf16ValueStringBuilder.Append<T11>(arg11);
				}
				if (typeof(T12) == typeof(string))
				{
					if (arg12 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T12, string>(ref arg12));
					}
				}
				else if (typeof(T12) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T12, int>(ref arg12));
				}
				else
				{
					utf16ValueStringBuilder.Append<T12>(arg12);
				}
				if (typeof(T13) == typeof(string))
				{
					if (arg13 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T13, string>(ref arg13));
					}
				}
				else if (typeof(T13) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T13, int>(ref arg13));
				}
				else
				{
					utf16ValueStringBuilder.Append<T13>(arg13);
				}
				if (typeof(T14) == typeof(string))
				{
					if (arg14 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T14, string>(ref arg14));
					}
				}
				else if (typeof(T14) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T14, int>(ref arg14));
				}
				else
				{
					utf16ValueStringBuilder.Append<T14>(arg14);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001DD64 File Offset: 0x0001BF64
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				if (typeof(T11) == typeof(string))
				{
					if (arg11 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T11, string>(ref arg11));
					}
				}
				else if (typeof(T11) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T11, int>(ref arg11));
				}
				else
				{
					utf16ValueStringBuilder.Append<T11>(arg11);
				}
				if (typeof(T12) == typeof(string))
				{
					if (arg12 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T12, string>(ref arg12));
					}
				}
				else if (typeof(T12) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T12, int>(ref arg12));
				}
				else
				{
					utf16ValueStringBuilder.Append<T12>(arg12);
				}
				if (typeof(T13) == typeof(string))
				{
					if (arg13 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T13, string>(ref arg13));
					}
				}
				else if (typeof(T13) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T13, int>(ref arg13));
				}
				else
				{
					utf16ValueStringBuilder.Append<T13>(arg13);
				}
				if (typeof(T14) == typeof(string))
				{
					if (arg14 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T14, string>(ref arg14));
					}
				}
				else if (typeof(T14) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T14, int>(ref arg14));
				}
				else
				{
					utf16ValueStringBuilder.Append<T14>(arg14);
				}
				if (typeof(T15) == typeof(string))
				{
					if (arg15 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T15, string>(ref arg15));
					}
				}
				else if (typeof(T15) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T15, int>(ref arg15));
				}
				else
				{
					utf16ValueStringBuilder.Append<T15>(arg15);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001E3DC File Offset: 0x0001C5DC
		public unsafe static string Concat<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15, [Nullable(2)] T16>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				if (typeof(T1) == typeof(string))
				{
					if (arg1 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T1, string>(ref arg1));
					}
				}
				else if (typeof(T1) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T1, int>(ref arg1));
				}
				else
				{
					utf16ValueStringBuilder.Append<T1>(arg1);
				}
				if (typeof(T2) == typeof(string))
				{
					if (arg2 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T2, string>(ref arg2));
					}
				}
				else if (typeof(T2) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T2, int>(ref arg2));
				}
				else
				{
					utf16ValueStringBuilder.Append<T2>(arg2);
				}
				if (typeof(T3) == typeof(string))
				{
					if (arg3 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T3, string>(ref arg3));
					}
				}
				else if (typeof(T3) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T3, int>(ref arg3));
				}
				else
				{
					utf16ValueStringBuilder.Append<T3>(arg3);
				}
				if (typeof(T4) == typeof(string))
				{
					if (arg4 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T4, string>(ref arg4));
					}
				}
				else if (typeof(T4) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T4, int>(ref arg4));
				}
				else
				{
					utf16ValueStringBuilder.Append<T4>(arg4);
				}
				if (typeof(T5) == typeof(string))
				{
					if (arg5 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T5, string>(ref arg5));
					}
				}
				else if (typeof(T5) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T5, int>(ref arg5));
				}
				else
				{
					utf16ValueStringBuilder.Append<T5>(arg5);
				}
				if (typeof(T6) == typeof(string))
				{
					if (arg6 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T6, string>(ref arg6));
					}
				}
				else if (typeof(T6) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T6, int>(ref arg6));
				}
				else
				{
					utf16ValueStringBuilder.Append<T6>(arg6);
				}
				if (typeof(T7) == typeof(string))
				{
					if (arg7 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T7, string>(ref arg7));
					}
				}
				else if (typeof(T7) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T7, int>(ref arg7));
				}
				else
				{
					utf16ValueStringBuilder.Append<T7>(arg7);
				}
				if (typeof(T8) == typeof(string))
				{
					if (arg8 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T8, string>(ref arg8));
					}
				}
				else if (typeof(T8) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T8, int>(ref arg8));
				}
				else
				{
					utf16ValueStringBuilder.Append<T8>(arg8);
				}
				if (typeof(T9) == typeof(string))
				{
					if (arg9 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T9, string>(ref arg9));
					}
				}
				else if (typeof(T9) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T9, int>(ref arg9));
				}
				else
				{
					utf16ValueStringBuilder.Append<T9>(arg9);
				}
				if (typeof(T10) == typeof(string))
				{
					if (arg10 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T10, string>(ref arg10));
					}
				}
				else if (typeof(T10) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T10, int>(ref arg10));
				}
				else
				{
					utf16ValueStringBuilder.Append<T10>(arg10);
				}
				if (typeof(T11) == typeof(string))
				{
					if (arg11 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T11, string>(ref arg11));
					}
				}
				else if (typeof(T11) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T11, int>(ref arg11));
				}
				else
				{
					utf16ValueStringBuilder.Append<T11>(arg11);
				}
				if (typeof(T12) == typeof(string))
				{
					if (arg12 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T12, string>(ref arg12));
					}
				}
				else if (typeof(T12) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T12, int>(ref arg12));
				}
				else
				{
					utf16ValueStringBuilder.Append<T12>(arg12);
				}
				if (typeof(T13) == typeof(string))
				{
					if (arg13 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T13, string>(ref arg13));
					}
				}
				else if (typeof(T13) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T13, int>(ref arg13));
				}
				else
				{
					utf16ValueStringBuilder.Append<T13>(arg13);
				}
				if (typeof(T14) == typeof(string))
				{
					if (arg14 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T14, string>(ref arg14));
					}
				}
				else if (typeof(T14) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T14, int>(ref arg14));
				}
				else
				{
					utf16ValueStringBuilder.Append<T14>(arg14);
				}
				if (typeof(T15) == typeof(string))
				{
					if (arg15 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T15, string>(ref arg15));
					}
				}
				else if (typeof(T15) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T15, int>(ref arg15));
				}
				else
				{
					utf16ValueStringBuilder.Append<T15>(arg15);
				}
				if (typeof(T16) == typeof(string))
				{
					if (arg16 != null)
					{
						utf16ValueStringBuilder.Append(*Unsafe.As<T16, string>(ref arg16));
					}
				}
				else if (typeof(T16) == typeof(int))
				{
					utf16ValueStringBuilder.Append(*Unsafe.As<T16, int>(ref arg16));
				}
				else
				{
					utf16ValueStringBuilder.Append<T16>(arg16);
				}
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1>(string format, T1 arg1)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1>(format, arg1);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001EB08 File Offset: 0x0001CD08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1>(format, arg1);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001EB50 File Offset: 0x0001CD50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2>(string format, T1 arg1, T2 arg2)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2>(format, arg1, arg2);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001EB98 File Offset: 0x0001CD98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2>(format, arg1, arg2);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001EBE0 File Offset: 0x0001CDE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(string format, T1 arg1, T2 arg2, T3 arg3)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3>(format, arg1, arg2, arg3);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3>(format, arg1, arg2, arg3);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001EC78 File Offset: 0x0001CE78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4>(format, arg1, arg2, arg3, arg4);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001ED10 File Offset: 0x0001CF10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001ED60 File Offset: 0x0001CF60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5>(format, arg1, arg2, arg3, arg4, arg5);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001EE00 File Offset: 0x0001D000
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6>(format, arg1, arg2, arg3, arg4, arg5, arg6);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001EE50 File Offset: 0x0001D050
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001EF4C File Offset: 0x0001D14C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001EFA0 File Offset: 0x0001D1A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001F050 File Offset: 0x0001D250
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001F100 File Offset: 0x0001D300
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001F15C File Offset: 0x0001D35C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001F1B8 File Offset: 0x0001D3B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001F214 File Offset: 0x0001D414
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001F270 File Offset: 0x0001D470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001F2D0 File Offset: 0x0001D4D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001F330 File Offset: 0x0001D530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001F390 File Offset: 0x0001D590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001F454 File Offset: 0x0001D654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15, [Nullable(2)] T16>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001F51C File Offset: 0x0001D71C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15, [Nullable(2)] T16>([Nullable(0)] ReadOnlySpan<char> format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, arg16);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001F580 File Offset: 0x0001D780
		public static Utf16PreparedFormat<T1> PrepareUtf16<[Nullable(2)] T1>(string format)
		{
			return new Utf16PreparedFormat<T1>(format);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001F588 File Offset: 0x0001D788
		public static Utf8PreparedFormat<T1> PrepareUtf8<[Nullable(2)] T1>(string format)
		{
			return new Utf8PreparedFormat<T1>(format);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001F590 File Offset: 0x0001D790
		public static Utf16PreparedFormat<T1, T2> PrepareUtf16<[Nullable(2)] T1, [Nullable(2)] T2>(string format)
		{
			return new Utf16PreparedFormat<T1, T2>(format);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001F598 File Offset: 0x0001D798
		public static Utf8PreparedFormat<T1, T2> PrepareUtf8<[Nullable(2)] T1, [Nullable(2)] T2>(string format)
		{
			return new Utf8PreparedFormat<T1, T2>(format);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001F5A0 File Offset: 0x0001D7A0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3> PrepareUtf16<T1, T2, T3>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3>(format);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001F5A8 File Offset: 0x0001D7A8
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3> PrepareUtf8<T1, T2, T3>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3>(format);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4> PrepareUtf16<T1, T2, T3, T4>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4>(format);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4> PrepareUtf8<T1, T2, T3, T4>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4>(format);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5> PrepareUtf16<T1, T2, T3, T4, T5>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5>(format);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0001F5C8 File Offset: 0x0001D7C8
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5> PrepareUtf8<T1, T2, T3, T4, T5>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5>(format);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6> PrepareUtf16<T1, T2, T3, T4, T5, T6>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6>(format);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001F5D8 File Offset: 0x0001D7D8
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6> PrepareUtf8<T1, T2, T3, T4, T5, T6>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6>(format);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7>(format);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001F5E8 File Offset: 0x0001D7E8
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7>(format);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8>(format);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8>(format);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001F600 File Offset: 0x0001D800
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001F608 File Offset: 0x0001D808
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9>(format);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001F610 File Offset: 0x0001D810
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001F618 File Offset: 0x0001D818
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001F620 File Offset: 0x0001D820
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001F628 File Offset: 0x0001D828
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001F630 File Offset: 0x0001D830
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001F638 File Offset: 0x0001D838
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0001F640 File Offset: 0x0001D840
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001F648 File Offset: 0x0001D848
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001F650 File Offset: 0x0001D850
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001F658 File Offset: 0x0001D858
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001F660 File Offset: 0x0001D860
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001F668 File Offset: 0x0001D868
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001F670 File Offset: 0x0001D870
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> PrepareUtf16<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([Nullable(1)] string format)
		{
			return new Utf16PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001F678 File Offset: 0x0001D878
		[NullableContext(2)]
		[return: Nullable(1)]
		public static Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> PrepareUtf8<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>([Nullable(1)] string format)
		{
			return new Utf8PreparedFormat<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(format);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001F680 File Offset: 0x0001D880
		public static void Utf8Format<[Nullable(2)] T1>(IBufferWriter<byte> bufferWriter, string format, T1 arg1)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						if (parseResult.Index == 0)
						{
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
						}
						else
						{
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0001F860 File Offset: 0x0001DA60
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						int index = parseResult.Index;
						if (index != 0)
						{
							if (index != 1)
							{
								ExceptionUtil.ThrowFormatException();
								ExceptionUtil.ThrowFormatException();
							}
							else
							{
								Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							}
						}
						else
						{
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0001FA64 File Offset: 0x0001DC64
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0001FC90 File Offset: 0x0001DE90
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00020144 File Offset: 0x0001E344
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000203D0 File Offset: 0x0001E5D0
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00020678 File Offset: 0x0001E878
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00020940 File Offset: 0x0001EB40
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00020C28 File Offset: 0x0001EE28
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00020F30 File Offset: 0x0001F130
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T11>(ref bufferWriter, arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00021254 File Offset: 0x0001F454
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T11>(ref bufferWriter, arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T12>(ref bufferWriter, arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00021598 File Offset: 0x0001F798
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T11>(ref bufferWriter, arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T12>(ref bufferWriter, arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T13>(ref bufferWriter, arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000218FC File Offset: 0x0001FAFC
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T11>(ref bufferWriter, arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T12>(ref bufferWriter, arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T13>(ref bufferWriter, arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						case 13:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T14>(ref bufferWriter, arg14, parseResult.Alignment, standardFormat, "arg14");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00021C80 File Offset: 0x0001FE80
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T11>(ref bufferWriter, arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T12>(ref bufferWriter, arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T13>(ref bufferWriter, arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						case 13:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T14>(ref bufferWriter, arg14, parseResult.Alignment, standardFormat, "arg14");
							break;
						case 14:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T15>(ref bufferWriter, arg15, parseResult.Alignment, standardFormat, "arg15");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00022020 File Offset: 0x00020220
		public static void Utf8Format<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15, [Nullable(2)] T16>(IBufferWriter<byte> bufferWriter, string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
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
						Span<byte> span = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num2));
						int bytes = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num2), span);
						bufferWriter.Advance(bytes);
						i++;
						num = i;
					}
					else
					{
						int num3 = i - num;
						Span<byte> span2 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num3));
						int bytes2 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num3), span2);
						bufferWriter.Advance(bytes2);
						FormatParser.ParseResult parseResult = FormatParser.Parse(format, i);
						num = parseResult.LastIndex;
						i = parseResult.LastIndex - 1;
						StandardFormat standardFormat = StandardFormat.Parse(parseResult.FormatString);
						switch (parseResult.Index)
						{
						case 0:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T1>(ref bufferWriter, arg1, parseResult.Alignment, standardFormat, "arg1");
							break;
						case 1:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T2>(ref bufferWriter, arg2, parseResult.Alignment, standardFormat, "arg2");
							break;
						case 2:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T3>(ref bufferWriter, arg3, parseResult.Alignment, standardFormat, "arg3");
							break;
						case 3:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T4>(ref bufferWriter, arg4, parseResult.Alignment, standardFormat, "arg4");
							break;
						case 4:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T5>(ref bufferWriter, arg5, parseResult.Alignment, standardFormat, "arg5");
							break;
						case 5:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T6>(ref bufferWriter, arg6, parseResult.Alignment, standardFormat, "arg6");
							break;
						case 6:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T7>(ref bufferWriter, arg7, parseResult.Alignment, standardFormat, "arg7");
							break;
						case 7:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T8>(ref bufferWriter, arg8, parseResult.Alignment, standardFormat, "arg8");
							break;
						case 8:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T9>(ref bufferWriter, arg9, parseResult.Alignment, standardFormat, "arg9");
							break;
						case 9:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T10>(ref bufferWriter, arg10, parseResult.Alignment, standardFormat, "arg10");
							break;
						case 10:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T11>(ref bufferWriter, arg11, parseResult.Alignment, standardFormat, "arg11");
							break;
						case 11:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T12>(ref bufferWriter, arg12, parseResult.Alignment, standardFormat, "arg12");
							break;
						case 12:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T13>(ref bufferWriter, arg13, parseResult.Alignment, standardFormat, "arg13");
							break;
						case 13:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T14>(ref bufferWriter, arg14, parseResult.Alignment, standardFormat, "arg14");
							break;
						case 14:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T15>(ref bufferWriter, arg15, parseResult.Alignment, standardFormat, "arg15");
							break;
						case 15:
							Utf8FormatHelper.FormatTo<IBufferWriter<byte>, T16>(ref bufferWriter, arg16, parseResult.Alignment, standardFormat, "arg16");
							break;
						default:
							ExceptionUtil.ThrowFormatException();
							ExceptionUtil.ThrowFormatException();
							break;
						}
					}
				}
				else if (c == '}')
				{
					if (i + 1 < format.Length && format[i + 1] == '}')
					{
						int num4 = i - num;
						Span<byte> span3 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num4));
						int bytes3 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num4), span3);
						bufferWriter.Advance(bytes3);
						i++;
						num = i;
					}
					else
					{
						ExceptionUtil.ThrowFormatException();
					}
				}
			}
			int num5 = format.Length - num;
			if (num5 > 0)
			{
				Span<byte> span4 = bufferWriter.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(num5));
				int bytes4 = ZString.UTF8NoBom.GetBytes(format.AsSpan(num, num5), span4);
				bufferWriter.Advance(bytes4);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000223E0 File Offset: 0x000205E0
		[NullableContext(0)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AppendChars<TBufferWriter>([Nullable(1)] ref TBufferWriter sb, ReadOnlySpan<char> chars) where TBufferWriter : IBufferWriter<byte>
		{
			Span<byte> span = sb.GetSpan(ZString.UTF8NoBom.GetMaxByteCount(chars.Length));
			sb.Advance(ZString.UTF8NoBom.GetBytes(chars, span));
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00022423 File Offset: 0x00020623
		public static Utf16ValueStringBuilder CreateStringBuilder()
		{
			return new Utf16ValueStringBuilder(false);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0002242B File Offset: 0x0002062B
		public static Utf8ValueStringBuilder CreateUtf8StringBuilder()
		{
			return new Utf8ValueStringBuilder(false);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00022433 File Offset: 0x00020633
		public static Utf16ValueStringBuilder CreateStringBuilder(bool notNested)
		{
			return new Utf16ValueStringBuilder(notNested);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0002243B File Offset: 0x0002063B
		public static Utf8ValueStringBuilder CreateUtf8StringBuilder(bool notNested)
		{
			return new Utf8ValueStringBuilder(notNested);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00022444 File Offset: 0x00020644
		public unsafe static string Join<[Nullable(2)] T>(char separator, params T[] values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values.AsSpan<T>());
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00022474 File Offset: 0x00020674
		public unsafe static string Join<[Nullable(2)] T>(char separator, List<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0002249C File Offset: 0x0002069C
		public unsafe static string Join<[Nullable(2)] T>(char separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000224C4 File Offset: 0x000206C4
		public unsafe static string Join<[Nullable(2)] T>(char separator, IEnumerable<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000224EC File Offset: 0x000206EC
		public unsafe static string Join<[Nullable(2)] T>(char separator, ICollection<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values.AsEnumerable<T>());
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00022518 File Offset: 0x00020718
		public unsafe static string Join<[Nullable(2)] T>(char separator, IList<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00022540 File Offset: 0x00020740
		public unsafe static string Join<[Nullable(2)] T>(char separator, IReadOnlyList<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00022568 File Offset: 0x00020768
		public unsafe static string Join<[Nullable(2)] T>(char separator, IReadOnlyCollection<T> values)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)2];
			*intPtr = (short)separator;
			return ZString.JoinInternal<T>(new Span<char>(intPtr, 1), values.AsEnumerable<T>());
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00022592 File Offset: 0x00020792
		public static string Join<[Nullable(2)] T>(string separator, params T[] values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values.AsSpan<T>());
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000225AA File Offset: 0x000207AA
		public static string Join<[Nullable(2)] T>(string separator, List<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000225B8 File Offset: 0x000207B8
		public static string Join<[Nullable(2)] T>(string separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000225C6 File Offset: 0x000207C6
		public static string Join<[Nullable(2)] T>(string separator, ICollection<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values.AsEnumerable<T>());
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000225D9 File Offset: 0x000207D9
		public static string Join<[Nullable(2)] T>(string separator, IList<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000225E7 File Offset: 0x000207E7
		public static string Join<[Nullable(2)] T>(string separator, IReadOnlyList<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000225F5 File Offset: 0x000207F5
		public static string Join<[Nullable(2)] T>(string separator, IReadOnlyCollection<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values.AsEnumerable<T>());
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00022608 File Offset: 0x00020808
		public static string Join<[Nullable(2)] T>(string separator, IEnumerable<T> values)
		{
			return ZString.JoinInternal<T>(separator.AsSpan(), values);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00022618 File Offset: 0x00020818
		public static string Concat<[Nullable(2)] T>(params T[] values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values.AsSpan<T>());
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00022640 File Offset: 0x00020840
		public static string Concat<[Nullable(2)] T>(List<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0002265C File Offset: 0x0002085C
		public static string Concat<[Nullable(2)] T>([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00022678 File Offset: 0x00020878
		public static string Concat<[Nullable(2)] T>(ICollection<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values.AsEnumerable<T>());
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0002269C File Offset: 0x0002089C
		public static string Concat<[Nullable(2)] T>(IList<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000226B8 File Offset: 0x000208B8
		public static string Concat<[Nullable(2)] T>(IReadOnlyList<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000226D4 File Offset: 0x000208D4
		public static string Concat<[Nullable(2)] T>(IReadOnlyCollection<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values.AsEnumerable<T>());
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000226F8 File Offset: 0x000208F8
		public static string Concat<[Nullable(2)] T>(IEnumerable<T> values)
		{
			return ZString.JoinInternal<T>(default(ReadOnlySpan<char>), values);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00022714 File Offset: 0x00020914
		private static string JoinInternal<[Nullable(2)] T>([Nullable(0)] ReadOnlySpan<char> separator, IList<T> values)
		{
			IReadOnlyList<T> readOnlyList = values as IReadOnlyList<T>;
			readOnlyList = readOnlyList ?? new ReadOnlyListAdaptor<T>(values);
			return ZString.JoinInternal<T>(separator, readOnlyList);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00022740 File Offset: 0x00020940
		private static string JoinInternal<[Nullable(2)] T>([Nullable(0)] ReadOnlySpan<char> separator, IReadOnlyList<T> values)
		{
			if (values.Count == 0)
			{
				return string.Empty;
			}
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendJoinInternal<T>(separator, values);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00022798 File Offset: 0x00020998
		[NullableContext(0)]
		[return: Nullable(1)]
		private unsafe static string JoinInternal<[Nullable(2)] T>(ReadOnlySpan<char> separator, [Nullable(new byte[] { 0, 1 })] ReadOnlySpan<T> values)
		{
			if (values.Length == 0)
			{
				return string.Empty;
			}
			if (typeof(T) == typeof(string) && values.Length == 1)
			{
				return Unsafe.As<string>(*values[0]);
			}
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendJoinInternal<T>(separator, values);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0002282C File Offset: 0x00020A2C
		private static string JoinInternal<[Nullable(2)] T>([Nullable(0)] ReadOnlySpan<char> separator, IEnumerable<T> values)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendJoinInternal<T>(separator, values);
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x040000D0 RID: 208
		private static Encoding UTF8NoBom = new UTF8Encoding(false);
	}
}
