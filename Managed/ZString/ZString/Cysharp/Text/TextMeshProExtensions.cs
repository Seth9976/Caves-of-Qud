using System;
using System.Runtime.CompilerServices;
using TMPro;

namespace Cysharp.Text
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	public static class TextMeshProExtensions
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0001A680 File Offset: 0x00018880
		public static void SetText(this TMP_Text text, Utf16ValueStringBuilder stringBuilder)
		{
			ArraySegment<char> arraySegment = stringBuilder.AsArraySegment();
			text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001A6B0 File Offset: 0x000188B0
		public static void SetText<[Nullable(2)] T>(this TMP_Text text, T arg0)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.Append<T>(arg0);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001A710 File Offset: 0x00018910
		public static void SetTextFormat<[Nullable(2)] T0>(this TMP_Text text, string format, T0 arg0)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0>(format, arg0);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001A774 File Offset: 0x00018974
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1>(this TMP_Text text, string format, T0 arg0, T1 arg1)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1>(format, arg0, arg1);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001A7D8 File Offset: 0x000189D8
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2>(format, arg0, arg1, arg2);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001A83C File Offset: 0x00018A3C
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3>(format, arg0, arg1, arg2, arg3);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4>(format, arg0, arg1, arg2, arg3, arg4);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001A90C File Offset: 0x00018B0C
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5>(format, arg0, arg1, arg2, arg3, arg4, arg5);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001A978 File Offset: 0x00018B78
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001A9E4 File Offset: 0x00018BE4
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001AA54 File Offset: 0x00018C54
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001AB38 File Offset: 0x00018D38
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001ABAC File Offset: 0x00018DAC
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001AC24 File Offset: 0x00018E24
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001AC9C File Offset: 0x00018E9C
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001AD18 File Offset: 0x00018F18
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001AD94 File Offset: 0x00018F94
		public static void SetTextFormat<[Nullable(2)] T0, [Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8, [Nullable(2)] T9, [Nullable(2)] T10, [Nullable(2)] T11, [Nullable(2)] T12, [Nullable(2)] T13, [Nullable(2)] T14, [Nullable(2)] T15>(this TMP_Text text, string format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
		{
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = new Utf16ValueStringBuilder(true))
			{
				utf16ValueStringBuilder.AppendFormat<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(format, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
				ArraySegment<char> arraySegment = utf16ValueStringBuilder.AsArraySegment();
				text.SetCharArray(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
			}
		}
	}
}
