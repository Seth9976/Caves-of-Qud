using System;
using System.Buffers;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using XRL.UI;

namespace XRL.World.Text
{
	// Token: 0x0200145F RID: 5215
	public class TextBuilder : IList<char>, ICollection<char>, IEnumerable<char>, IEnumerable, IReadOnlyList<char>, IReadOnlyCollection<char>, IReadOnlySpanConvertible<char>, IEquatable<string>, IDisposable
	{
		// Token: 0x0600CC5E RID: 52318 RVA: 0x004A3C98 File Offset: 0x004A1E98
		public void SetToTMP(TextMeshProUGUI tmp)
		{
			tmp.SetCharArray(this.Buffer, 0, this.Length);
		}

		// Token: 0x0600CC5F RID: 52319 RVA: 0x004A3CAD File Offset: 0x004A1EAD
		public TextBuilder AppendHex2(int Value)
		{
			return this.Append(TextBuilder.hexVals[(Value >> 4) & 15]).Append(TextBuilder.hexVals[Value & 15]);
		}

		// Token: 0x0600CC60 RID: 52320 RVA: 0x004A3CD0 File Offset: 0x004A1ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(char Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC61 RID: 52321 RVA: 0x004A3CDF File Offset: 0x004A1EDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(char Value, int Count)
		{
			return this.Insert(this.Length, Value, Count);
		}

		// Token: 0x0600CC62 RID: 52322 RVA: 0x004A3CEF File Offset: 0x004A1EEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(char? Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC63 RID: 52323 RVA: 0x004A3CFE File Offset: 0x004A1EFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(char? Value, int Count)
		{
			return this.Insert(this.Length, Value, Count);
		}

		// Token: 0x0600CC64 RID: 52324 RVA: 0x004A3D0E File Offset: 0x004A1F0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(bool Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC65 RID: 52325 RVA: 0x004A3D1D File Offset: 0x004A1F1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(ReadOnlySpan<char> Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC66 RID: 52326 RVA: 0x004A3D2C File Offset: 0x004A1F2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(ReadOnlySpan<char> Value, int Start, int Length)
		{
			return this.Insert(this.Length, Value, Start, Length);
		}

		// Token: 0x0600CC67 RID: 52327 RVA: 0x004A3D3D File Offset: 0x004A1F3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder AppendUpper(ReadOnlySpan<char> Value)
		{
			return this.InsertUpper(this.Length, Value);
		}

		// Token: 0x0600CC68 RID: 52328 RVA: 0x004A3D4C File Offset: 0x004A1F4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder AppendLower(ReadOnlySpan<char> Value)
		{
			return this.InsertLower(this.Length, Value);
		}

		// Token: 0x0600CC69 RID: 52329 RVA: 0x004A3D5B File Offset: 0x004A1F5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(string Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC6A RID: 52330 RVA: 0x004A3D6A File Offset: 0x004A1F6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(string Value, int Start)
		{
			return this.Insert(this.Length, Value, Start);
		}

		// Token: 0x0600CC6B RID: 52331 RVA: 0x004A3D7A File Offset: 0x004A1F7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(string Value, int Start, int Length)
		{
			return this.Insert(this.Length, Value, Start, Length);
		}

		// Token: 0x0600CC6C RID: 52332 RVA: 0x004A3D8B File Offset: 0x004A1F8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(StringBuilder Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC6D RID: 52333 RVA: 0x004A3D9A File Offset: 0x004A1F9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(IReadOnlySpanConvertible<char> Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC6E RID: 52334 RVA: 0x004A3DA9 File Offset: 0x004A1FA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(int Value)
		{
			return this.Insert(this.Length, Value, false);
		}

		// Token: 0x0600CC6F RID: 52335 RVA: 0x004A3DB9 File Offset: 0x004A1FB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(long Value)
		{
			return this.Insert(this.Length, Value, false);
		}

		// Token: 0x0600CC70 RID: 52336 RVA: 0x004A3DC9 File Offset: 0x004A1FC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(uint Value)
		{
			return this.Insert(this.Length, Value, false);
		}

		// Token: 0x0600CC71 RID: 52337 RVA: 0x004A3DD9 File Offset: 0x004A1FD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(ulong Value)
		{
			return this.Insert(this.Length, Value, false);
		}

		// Token: 0x0600CC72 RID: 52338 RVA: 0x004A3DE9 File Offset: 0x004A1FE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(float Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC73 RID: 52339 RVA: 0x004A3DF8 File Offset: 0x004A1FF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(double Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC74 RID: 52340 RVA: 0x004A3E07 File Offset: 0x004A2007
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(Guid Value)
		{
			return this.Insert(this.Length, Value);
		}

		// Token: 0x0600CC75 RID: 52341 RVA: 0x004A3E16 File Offset: 0x004A2016
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(int Value, bool Signed)
		{
			return this.Insert(this.Length, Value, Signed);
		}

		// Token: 0x0600CC76 RID: 52342 RVA: 0x004A3E26 File Offset: 0x004A2026
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(long Value, bool Signed)
		{
			return this.Insert(this.Length, Value, Signed);
		}

		// Token: 0x0600CC77 RID: 52343 RVA: 0x004A3E36 File Offset: 0x004A2036
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(uint Value, bool Signed)
		{
			return this.Insert(this.Length, Value, Signed);
		}

		// Token: 0x0600CC78 RID: 52344 RVA: 0x004A3E46 File Offset: 0x004A2046
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(ulong Value, bool Signed)
		{
			return this.Insert(this.Length, Value, Signed);
		}

		// Token: 0x0600CC79 RID: 52345 RVA: 0x004A3E56 File Offset: 0x004A2056
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(int Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			return this.Insert(this.Length, Value, Format, Provider);
		}

		// Token: 0x0600CC7A RID: 52346 RVA: 0x004A3E67 File Offset: 0x004A2067
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(long Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			return this.Insert(this.Length, Value, Format, Provider);
		}

		// Token: 0x0600CC7B RID: 52347 RVA: 0x004A3E78 File Offset: 0x004A2078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(uint Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			return this.Insert(this.Length, Value, Format, Provider);
		}

		// Token: 0x0600CC7C RID: 52348 RVA: 0x004A3E89 File Offset: 0x004A2089
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(ulong Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			return this.Insert(this.Length, Value, Format, Provider);
		}

		// Token: 0x0600CC7D RID: 52349 RVA: 0x004A3E9A File Offset: 0x004A209A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(float Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			return this.Insert(this.Length, Value, Format, Provider);
		}

		// Token: 0x0600CC7E RID: 52350 RVA: 0x004A3EAB File Offset: 0x004A20AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(double Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			return this.Insert(this.Length, Value, Format, Provider);
		}

		// Token: 0x0600CC7F RID: 52351 RVA: 0x004A3EBC File Offset: 0x004A20BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Append(Guid Value, ReadOnlySpan<char> Format)
		{
			return this.Insert(this.Length, Value, Format);
		}

		/// <remarks>
		/// This will call ToString and thus allocate a discarded string.
		/// If possible implement a dedicated method for the type.
		/// </remarks>
		// Token: 0x0600CC80 RID: 52352 RVA: 0x004A3ECC File Offset: 0x004A20CC
		public TextBuilder Append(object Value)
		{
			return this.Append(Value.ToString());
		}

		// Token: 0x0600CC81 RID: 52353 RVA: 0x004A3EDA File Offset: 0x004A20DA
		public TextBuilder AppendByType(object Value)
		{
			return this.InsertByType(this.Length, Value);
		}

		// Token: 0x0600CC82 RID: 52354 RVA: 0x004A3EE9 File Offset: 0x004A20E9
		public TextBuilder AppendLine()
		{
			return this.Append('\n');
		}

		// Token: 0x0600CC83 RID: 52355 RVA: 0x004A3EF3 File Offset: 0x004A20F3
		public TextBuilder AppendLine(char Value)
		{
			return this.Append(Value).Append('\n');
		}

		// Token: 0x0600CC84 RID: 52356 RVA: 0x004A3F03 File Offset: 0x004A2103
		public TextBuilder AppendLine(string Value)
		{
			return this.Append(Value).Append('\n');
		}

		// Token: 0x0600CC85 RID: 52357 RVA: 0x004A3F13 File Offset: 0x004A2113
		public TextBuilder AppendLine(ReadOnlySpan<char> Value)
		{
			return this.Append(Value).Append('\n');
		}

		// Token: 0x0600CC86 RID: 52358 RVA: 0x004A3F24 File Offset: 0x004A2124
		public TextBuilder AppendMask(int Mask, int Length = 32)
		{
			for (int i = Length - 1; i >= 0; i--)
			{
				this.Append(((Mask & (1 << i)) != 0) ? '1' : '0');
			}
			return this;
		}

		// Token: 0x0600CC87 RID: 52359 RVA: 0x004A3F58 File Offset: 0x004A2158
		public TextBuilder AppendMask(int Mask, int Start, int Length)
		{
			for (int i = Start + Length - 1; i >= Start; i--)
			{
				this.Append(((Mask & (1 << i)) != 0) ? '1' : '0');
			}
			return this;
		}

		// Token: 0x0600CC88 RID: 52360 RVA: 0x004A3F90 File Offset: 0x004A2190
		public TextBuilder AppendRange(IReadOnlyList<string> Range, string Separator)
		{
			int i = 0;
			int count = Range.Count;
			while (i < count)
			{
				if (i != 0)
				{
					this.Append(Separator);
				}
				this.Append(Range[i]);
				i++;
			}
			return this;
		}

		// Token: 0x0600CC89 RID: 52361 RVA: 0x004A3FCC File Offset: 0x004A21CC
		public TextBuilder AppendRange(IEnumerable<string> Range, string Separator)
		{
			bool flag = true;
			foreach (string text in Range)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					this.Append(Separator);
				}
				this.Append(text);
			}
			return this;
		}

		// Token: 0x0600CC8A RID: 52362 RVA: 0x004A4028 File Offset: 0x004A2228
		public TextBuilder AppendPrefixed(IReadOnlyList<string> Range, string Prefix)
		{
			int i = 0;
			int count = Range.Count;
			while (i < count)
			{
				this.Append(Prefix);
				this.Append(Range[i]);
				i++;
			}
			return this;
		}

		// Token: 0x0600CC8B RID: 52363 RVA: 0x004A4060 File Offset: 0x004A2260
		public TextBuilder AppendPrefixed(IEnumerable<string> Range, string Prefix)
		{
			foreach (string text in Range)
			{
				this.Append(Prefix);
				this.Append(text);
			}
			return this;
		}

		// Token: 0x0600CC8C RID: 52364 RVA: 0x004A40B4 File Offset: 0x004A22B4
		public TextBuilder Compound(string Value)
		{
			if (this.Length > 0)
			{
				this.Append(' ');
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC8D RID: 52365 RVA: 0x004A40D1 File Offset: 0x004A22D1
		public TextBuilder Compound(string Value, string With)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC8E RID: 52366 RVA: 0x004A40ED File Offset: 0x004A22ED
		public TextBuilder Compound(string Value, char With)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC8F RID: 52367 RVA: 0x004A4109 File Offset: 0x004A2309
		public TextBuilder Compound(char Value)
		{
			if (this.Length > 0)
			{
				this.Append(' ');
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC90 RID: 52368 RVA: 0x004A4126 File Offset: 0x004A2326
		public TextBuilder Compound(char Value, string With)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC91 RID: 52369 RVA: 0x004A4142 File Offset: 0x004A2342
		public TextBuilder Compound(char Value, char With)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC92 RID: 52370 RVA: 0x004A415E File Offset: 0x004A235E
		public TextBuilder Compound(int Value, bool Signed = false)
		{
			if (this.Length > 0)
			{
				this.Append(' ');
			}
			this.Append(Value, Signed);
			return this;
		}

		// Token: 0x0600CC93 RID: 52371 RVA: 0x004A417C File Offset: 0x004A237C
		public TextBuilder Compound(int Value, char With, bool Signed = false)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value, Signed);
			return this;
		}

		// Token: 0x0600CC94 RID: 52372 RVA: 0x004A4199 File Offset: 0x004A2399
		public TextBuilder Compound(int Value, string With, bool Signed = false)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value, Signed);
			return this;
		}

		// Token: 0x0600CC95 RID: 52373 RVA: 0x004A41B6 File Offset: 0x004A23B6
		public TextBuilder Compound(IReadOnlySpanConvertible<char> Value)
		{
			if (this.Length > 0)
			{
				this.Append(' ');
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC96 RID: 52374 RVA: 0x004A41D3 File Offset: 0x004A23D3
		public TextBuilder Compound(IReadOnlySpanConvertible<char> Value, char With)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x0600CC97 RID: 52375 RVA: 0x004A41EF File Offset: 0x004A23EF
		public TextBuilder Compound(IReadOnlySpanConvertible<char> Value, string With)
		{
			if (this.Length > 0)
			{
				this.Append(With);
			}
			this.Append(Value);
			return this;
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600CC98 RID: 52376 RVA: 0x004A420B File Offset: 0x004A240B
		bool ICollection<char>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600CC99 RID: 52377 RVA: 0x004A420E File Offset: 0x004A240E
		int ICollection<char>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600CC9A RID: 52378 RVA: 0x004A4216 File Offset: 0x004A2416
		int IReadOnlyCollection<char>.Count
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x0600CC9B RID: 52379 RVA: 0x004A421E File Offset: 0x004A241E
		void ICollection<char>.Add(char Value)
		{
			this.Append(Value);
		}

		// Token: 0x0600CC9C RID: 52380 RVA: 0x004A4228 File Offset: 0x004A2428
		void IList<char>.Insert(int Index, char Value)
		{
			this.Insert(Index, Value);
		}

		// Token: 0x0600CC9D RID: 52381 RVA: 0x004A4234 File Offset: 0x004A2434
		bool ICollection<char>.Remove(char Value)
		{
			int num = this.IndexOf(Value);
			if (num != -1)
			{
				this.Remove(num, 1);
				return true;
			}
			return false;
		}

		// Token: 0x0600CC9E RID: 52382 RVA: 0x004A4259 File Offset: 0x004A2459
		void IList<char>.RemoveAt(int Index)
		{
			this.Remove(Index, 1);
		}

		// Token: 0x0600CC9F RID: 52383 RVA: 0x004A4264 File Offset: 0x004A2464
		void ICollection<char>.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600CCA0 RID: 52384 RVA: 0x004A426D File Offset: 0x004A246D
		ReadOnlySpan<char> IReadOnlySpanConvertible<char>.AsSpan()
		{
			return this.AsSpan();
		}

		// Token: 0x0600CCA1 RID: 52385 RVA: 0x004A427A File Offset: 0x004A247A
		ReadOnlySpan<char> IReadOnlySpanConvertible<char>.AsSpan(int Start)
		{
			return this.AsSpan(Start);
		}

		// Token: 0x0600CCA2 RID: 52386 RVA: 0x004A4288 File Offset: 0x004A2488
		ReadOnlySpan<char> IReadOnlySpanConvertible<char>.AsSpan(int Start, int Length)
		{
			return this.AsSpan(Start, Length);
		}

		// Token: 0x0600CCA3 RID: 52387 RVA: 0x004A4298 File Offset: 0x004A2498
		public TextBuilder Insert(int Index, char Value)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] array = this.Buffer;
			if (length == array.Length)
			{
				this.Grow(1);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + 1, length - Index);
			}
			array[Index] = Value;
			this.Length = length + 1;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCA4 RID: 52388 RVA: 0x004A4304 File Offset: 0x004A2504
		public TextBuilder Insert(int Index, char Value, int Count)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			if (Count < 0)
			{
				throw new ArgumentOutOfRangeException("Count");
			}
			char[] array = this.Buffer;
			if (length + Count > array.Length)
			{
				this.Grow(Count);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + Count, length - Index);
			}
			for (int i = 0; i < Count; i++)
			{
				array[Index + i] = Value;
			}
			this.Length = length + Count;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCA5 RID: 52389 RVA: 0x004A438F File Offset: 0x004A258F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, char? Value)
		{
			if (Value == null)
			{
				return this;
			}
			return this.Insert(Index, Value.Value);
		}

		// Token: 0x0600CCA6 RID: 52390 RVA: 0x004A43AA File Offset: 0x004A25AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, char? Value, int Count)
		{
			if (Value == null)
			{
				return this;
			}
			return this.Insert(Index, Value.Value, Count);
		}

		// Token: 0x0600CCA7 RID: 52391 RVA: 0x004A43C6 File Offset: 0x004A25C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, bool Value)
		{
			return this.Insert(Index, Value ? "true" : "false");
		}

		// Token: 0x0600CCA8 RID: 52392 RVA: 0x004A43E0 File Offset: 0x004A25E0
		public TextBuilder Insert(int Index, ReadOnlySpan<char> Value)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] array = this.Buffer;
			int length2 = Value.Length;
			if (length + length2 > array.Length)
			{
				this.Grow(length2);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + length2, length - Index);
			}
			Value.CopyTo(array.AsSpan<char>(Index));
			this.Length = length + length2;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCA9 RID: 52393 RVA: 0x004A4460 File Offset: 0x004A2660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, string Value)
		{
			string text;
			if ((text = Value) == null)
			{
				text = (Value = "");
			}
			return this.Insert(Index, text, 0, Value.Length);
		}

		// Token: 0x0600CCAA RID: 52394 RVA: 0x004A447D File Offset: 0x004A267D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, string Value, int Start)
		{
			string text;
			if ((text = Value) == null)
			{
				text = (Value = "");
			}
			return this.Insert(Index, text, Start, Value.Length - Start);
		}

		// Token: 0x0600CCAB RID: 52395 RVA: 0x004A449C File Offset: 0x004A269C
		public TextBuilder Insert(int Index, string Value, int Start, int Length)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			if (Length == 0)
			{
				return this;
			}
			char[] array = this.Buffer;
			if (length + Length > array.Length)
			{
				this.Grow(Length);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + Length, length - Index);
			}
			Value.CopyTo(Start, array, Index, Length);
			this.Length = length + Length;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCAC RID: 52396 RVA: 0x004A451C File Offset: 0x004A271C
		public TextBuilder Insert(int Index, ReadOnlySpan<char> Value, int Start, int Length)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			if (Length == 0)
			{
				return this;
			}
			char[] array = this.Buffer;
			if (length + Length > array.Length)
			{
				this.Grow(Length);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + Length, length - Index);
			}
			Value.Slice(Start, Length).CopyTo(array.AsSpan<char>(Index, Length));
			this.Length = length + Length;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCAD RID: 52397 RVA: 0x004A45AC File Offset: 0x004A27AC
		public TextBuilder Insert(int Index, StringBuilder Value)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] array = this.Buffer;
			int length2 = Value.Length;
			if (length + length2 > array.Length)
			{
				this.Grow(length2);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + length2, length - Index);
			}
			Value.CopyTo(0, array, Index, length2);
			this.Length = length + length2;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCAE RID: 52398 RVA: 0x004A4627 File Offset: 0x004A2827
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, IReadOnlySpanConvertible<char> Value)
		{
			return this.Insert(Index, Value.AsSpan());
		}

		// Token: 0x0600CCAF RID: 52399 RVA: 0x004A4636 File Offset: 0x004A2836
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, int Value)
		{
			return this.Insert(Index, Value, false);
		}

		// Token: 0x0600CCB0 RID: 52400 RVA: 0x004A4641 File Offset: 0x004A2841
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, long Value)
		{
			return this.Insert(Index, Value, false);
		}

		// Token: 0x0600CCB1 RID: 52401 RVA: 0x004A464C File Offset: 0x004A284C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, uint Value)
		{
			return this.InsertInternal(Index, (ulong)Value, false, false);
		}

		// Token: 0x0600CCB2 RID: 52402 RVA: 0x004A4659 File Offset: 0x004A2859
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, ulong Value)
		{
			return this.InsertInternal(Index, Value, false, false);
		}

		// Token: 0x0600CCB3 RID: 52403 RVA: 0x004A4665 File Offset: 0x004A2865
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, float Value)
		{
			return this.Insert(Index, Value, ReadOnlySpan<char>.Empty, null);
		}

		// Token: 0x0600CCB4 RID: 52404 RVA: 0x004A4675 File Offset: 0x004A2875
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, double Value)
		{
			return this.Insert(Index, Value, ReadOnlySpan<char>.Empty, null);
		}

		// Token: 0x0600CCB5 RID: 52405 RVA: 0x004A4685 File Offset: 0x004A2885
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, Guid Value)
		{
			return this.Insert(Index, Value, ReadOnlySpan<char>.Empty);
		}

		// Token: 0x0600CCB6 RID: 52406 RVA: 0x004A4694 File Offset: 0x004A2894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, int Value, bool Signed)
		{
			if (Value == -2147483648)
			{
				return this.InsertInternal(Index, (ulong)int.MinValue, true, Signed);
			}
			return this.InsertInternal(Index, (ulong)((long)((Value + (Value >> 31)) ^ (Value >> 31))), Value < 0, Signed);
		}

		// Token: 0x0600CCB7 RID: 52407 RVA: 0x004A46C6 File Offset: 0x004A28C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, long Value, bool Signed)
		{
			if (Value == -9223372036854775808L)
			{
				return this.InsertInternal(Index, 9223372036854775808UL, true, Signed);
			}
			return this.InsertInternal(Index, (ulong)((Value + (Value >> 63)) ^ (Value >> 63)), Value < 0L, Signed);
		}

		// Token: 0x0600CCB8 RID: 52408 RVA: 0x004A46FF File Offset: 0x004A28FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, uint Value, bool Signed)
		{
			return this.InsertInternal(Index, (ulong)Value, false, Signed);
		}

		// Token: 0x0600CCB9 RID: 52409 RVA: 0x004A470C File Offset: 0x004A290C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TextBuilder Insert(int Index, ulong Value, bool Signed)
		{
			return this.InsertInternal(Index, Value, false, Signed);
		}

		// Token: 0x0600CCBA RID: 52410 RVA: 0x004A4718 File Offset: 0x004A2918
		public TextBuilder Insert(int Index, int Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			Span<char> span = this.ScratchBuffer.AsSpan<char>();
			int num;
			if (!Value.TryFormat(span, out num, Format, Provider))
			{
				this.ThrowTryFormatException(Value.ToString(), Format);
			}
			return this.Insert(Index, span.Slice(0, num));
		}

		// Token: 0x0600CCBB RID: 52411 RVA: 0x004A4764 File Offset: 0x004A2964
		public TextBuilder Insert(int Index, long Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			Span<char> span = this.ScratchBuffer.AsSpan<char>();
			int num;
			if (!Value.TryFormat(span, out num, Format, Provider))
			{
				this.ThrowTryFormatException(Value.ToString(), Format);
			}
			return this.Insert(Index, span.Slice(0, num));
		}

		// Token: 0x0600CCBC RID: 52412 RVA: 0x004A47B0 File Offset: 0x004A29B0
		public TextBuilder Insert(int Index, uint Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			Span<char> span = this.ScratchBuffer.AsSpan<char>();
			int num;
			if (!Value.TryFormat(span, out num, Format, Provider))
			{
				this.ThrowTryFormatException(Value.ToString(), Format);
			}
			return this.Insert(Index, span.Slice(0, num));
		}

		// Token: 0x0600CCBD RID: 52413 RVA: 0x004A47FC File Offset: 0x004A29FC
		public TextBuilder Insert(int Index, ulong Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			Span<char> span = this.ScratchBuffer.AsSpan<char>();
			int num;
			if (!Value.TryFormat(span, out num, Format, Provider))
			{
				this.ThrowTryFormatException(Value.ToString(), Format);
			}
			return this.Insert(Index, span.Slice(0, num));
		}

		// Token: 0x0600CCBE RID: 52414 RVA: 0x004A4848 File Offset: 0x004A2A48
		public TextBuilder Insert(int Index, float Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			Span<char> span = this.ScratchBuffer.AsSpan<char>();
			int num;
			if (!Value.TryFormat(span, out num, Format, Provider))
			{
				this.ThrowTryFormatException(Value.ToString(CultureInfo.InvariantCulture), Format);
			}
			return this.Insert(Index, span.Slice(0, num));
		}

		// Token: 0x0600CCBF RID: 52415 RVA: 0x004A4898 File Offset: 0x004A2A98
		public TextBuilder Insert(int Index, double Value, ReadOnlySpan<char> Format, IFormatProvider Provider = null)
		{
			Span<char> span = this.ScratchBuffer.AsSpan<char>();
			int num;
			if (!Value.TryFormat(span, out num, Format, Provider))
			{
				this.ThrowTryFormatException(Value.ToString(CultureInfo.InvariantCulture), Format);
			}
			return this.Insert(Index, span.Slice(0, num));
		}

		// Token: 0x0600CCC0 RID: 52416 RVA: 0x004A48E8 File Offset: 0x004A2AE8
		public TextBuilder Insert(int Index, Guid Value, ReadOnlySpan<char> Format)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] array = this.Buffer;
			int num = 68;
			if (length + num > array.Length)
			{
				this.Grow(num);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + num, length - Index);
			}
			Span<char> span = array.AsSpan<char>(Index, num);
			int num2;
			if (!Value.TryFormat(span, out num2, Format))
			{
				Array.Copy(array, Index + num, array, Index, length - Index);
				this.ThrowTryFormatException(Value.ToString(), Format);
			}
			if (num2 < num)
			{
				Array.Copy(array, Index + num, array, Index + num2, length - Index);
			}
			this.Length = length + num2;
			this.Version++;
			return this;
		}

		/// <inheritdoc cref="M:XRL.World.Text.TextBuilder.Append(System.Object)" path="remarks" />
		// Token: 0x0600CCC1 RID: 52417 RVA: 0x004A49A4 File Offset: 0x004A2BA4
		public TextBuilder Insert(int Index, object Value)
		{
			return this.Insert(Index, Value.ToString());
		}

		// Token: 0x0600CCC2 RID: 52418 RVA: 0x004A49B4 File Offset: 0x004A2BB4
		public TextBuilder InsertByType(int Index, object Value)
		{
			string text = Value as string;
			if (text != null)
			{
				return this.Insert(Index, text);
			}
			if (Value is char)
			{
				char c = (char)Value;
				return this.Insert(Index, c);
			}
			if (Value is int)
			{
				int num = (int)Value;
				return this.Insert(Index, num);
			}
			if (Value is long)
			{
				long num2 = (long)Value;
				return this.Insert(Index, num2);
			}
			if (Value is uint)
			{
				uint num3 = (uint)Value;
				return this.Insert(Index, num3);
			}
			if (Value is ulong)
			{
				ulong num4 = (ulong)Value;
				return this.Insert(Index, num4);
			}
			if (Value is float)
			{
				float num5 = (float)Value;
				return this.Insert(Index, num5);
			}
			if (Value is double)
			{
				double num6 = (double)Value;
				return this.Insert(Index, num6);
			}
			return this.Insert(Index, Value);
		}

		// Token: 0x0600CCC3 RID: 52419 RVA: 0x004A4A9C File Offset: 0x004A2C9C
		public unsafe TextBuilder InsertUpper(int Index, ReadOnlySpan<char> Value)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] array = this.Buffer;
			int length2 = Value.Length;
			if (length + length2 > array.Length)
			{
				this.Grow(length2);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + length2, length - Index);
			}
			for (int i = 0; i < length2; i++)
			{
				array[Index++] = char.ToUpperInvariant((char)(*Value[i]));
			}
			this.Length = length + length2;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCC4 RID: 52420 RVA: 0x004A4B30 File Offset: 0x004A2D30
		public unsafe TextBuilder InsertLower(int Index, ReadOnlySpan<char> Value)
		{
			int length = this.Length;
			if (Index > length)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] array = this.Buffer;
			int length2 = Value.Length;
			if (length + length2 > array.Length)
			{
				this.Grow(length2);
				array = this.Buffer;
			}
			if (Index < length)
			{
				Array.Copy(array, Index, array, Index + length2, length - Index);
			}
			for (int i = 0; i < length2; i++)
			{
				array[Index++] = char.ToLowerInvariant((char)(*Value[i]));
			}
			this.Length = length + length2;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCC5 RID: 52421 RVA: 0x004A4BC4 File Offset: 0x004A2DC4
		protected TextBuilder InsertInternal(int Index, ulong Value, bool Negative = false, bool Signed = false)
		{
			int num = this.Length;
			if (Index > num)
			{
				throw new ArgumentOutOfRangeException("Index");
			}
			char[] scratchBuffer = this.ScratchBuffer;
			int i = 0;
			do
			{
				ulong num2 = Value % 10UL;
				Value /= 10UL;
				scratchBuffer[i++] = (char)(48UL + num2);
			}
			while (Value > 0UL);
			if (Negative)
			{
				scratchBuffer[i++] = '-';
			}
			else if (Signed)
			{
				scratchBuffer[i++] = '+';
			}
			char[] array = this.Buffer;
			if (num + i > array.Length)
			{
				this.Grow(i);
				array = this.Buffer;
			}
			if (Index < num)
			{
				Array.Copy(array, Index, array, Index + i, num - Index);
			}
			num += i;
			while (i > 0)
			{
				array[Index++] = scratchBuffer[--i];
			}
			this.Version++;
			this.Length = num;
			return this;
		}

		// Token: 0x0600CCC6 RID: 52422 RVA: 0x004A4C8C File Offset: 0x004A2E8C
		protected void ThrowTryFormatException(string Value, ReadOnlySpan<char> Format)
		{
			throw new ArgumentException(string.Concat(new string[]
			{
				"Unable to format '",
				Value,
				"' to '",
				Format.ToString(),
				"'"
			}), "Format");
		}

		// Token: 0x0600CCC7 RID: 52423 RVA: 0x004A4CDC File Offset: 0x004A2EDC
		public static TextBuilder Get(int Capacity = 64)
		{
			TextBuilder.Entry entry;
			if (!TextBuilder.Bag.TryTake(out entry))
			{
				entry = new TextBuilder.Entry();
			}
			entry.Buffer = ArrayPool<char>.Shared.Rent(Capacity);
			if (Options.TextBuilderDebug)
			{
				TextBuilder.Traces[entry] = new StackTrace(1, true);
			}
			return entry;
		}

		// Token: 0x0600CCC8 RID: 52424 RVA: 0x004A4D28 File Offset: 0x004A2F28
		public static TextBuilder Get(ReadOnlySpan<char> Text)
		{
			return TextBuilder.Get(Text.Length).Append(Text);
		}

		// Token: 0x0600CCC9 RID: 52425 RVA: 0x004A4D3C File Offset: 0x004A2F3C
		public static TextBuilder Get(string Text)
		{
			if (Text.IsNullOrEmpty())
			{
				return TextBuilder.Get(64);
			}
			return TextBuilder.Get(Text.Length).Append(Text);
		}

		// Token: 0x0600CCCA RID: 52426 RVA: 0x004A4D5F File Offset: 0x004A2F5F
		public static TextBuilder Get(StringBuilder Text)
		{
			if (Text.IsNullOrEmpty())
			{
				return TextBuilder.Get(64);
			}
			return TextBuilder.Get(Text.Length).Append(Text);
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600CCCB RID: 52427 RVA: 0x004A4D82 File Offset: 0x004A2F82
		public int Capacity
		{
			get
			{
				return this.Buffer.Length;
			}
		}

		/// <summary>If false an error will be logged when the object is finalized.</summary>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600CCCC RID: 52428 RVA: 0x004A4D8C File Offset: 0x004A2F8C
		protected virtual bool Finalizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BDE RID: 3038
		public char this[int Index]
		{
			get
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Buffer[Index];
			}
			set
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Buffer[Index] = value;
				this.Version++;
			}
		}

		// Token: 0x17000BDF RID: 3039
		public char this[Index Index]
		{
			get
			{
				return this.Buffer[Index.GetOffset(this.Length)];
			}
			set
			{
				this.Buffer[Index.GetOffset(this.Length)] = value;
				this.Version++;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		public Span<char> this[Range Range]
		{
			get
			{
				return this.AsSpan(Range.Start.GetOffset(this.Length), Range.End.GetOffset(this.Length));
			}
		}

		// Token: 0x0600CCD2 RID: 52434 RVA: 0x004A4E49 File Offset: 0x004A3049
		protected TextBuilder(int ScratchCapacity = 32)
		{
			this.ScratchBuffer = new char[ScratchCapacity];
		}

		// Token: 0x0600CCD3 RID: 52435 RVA: 0x004A4E5D File Offset: 0x004A305D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AsSpan()
		{
			return new Span<char>(this.Buffer, 0, this.Length);
		}

		// Token: 0x0600CCD4 RID: 52436 RVA: 0x004A4E71 File Offset: 0x004A3071
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AsSpan(int Start, int Length)
		{
			if (Start + Length > this.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new Span<char>(this.Buffer, Start, Length);
		}

		// Token: 0x0600CCD5 RID: 52437 RVA: 0x004A4E91 File Offset: 0x004A3091
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AsSpan(int Start)
		{
			return new Span<char>(this.Buffer, Start, this.Length - Start);
		}

		// Token: 0x0600CCD6 RID: 52438 RVA: 0x004A4EA7 File Offset: 0x004A30A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<char> AsReadOnlySpan()
		{
			return new ReadOnlySpan<char>(this.Buffer, 0, this.Length);
		}

		// Token: 0x0600CCD7 RID: 52439 RVA: 0x004A4EBB File Offset: 0x004A30BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<char> AsReadOnlySpan(int Start, int Length)
		{
			if (Start + Length > this.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			return new ReadOnlySpan<char>(this.Buffer, Start, Length);
		}

		// Token: 0x0600CCD8 RID: 52440 RVA: 0x004A4EDB File Offset: 0x004A30DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ReadOnlySpan<char> AsReadOnlySpan(int Start)
		{
			return new ReadOnlySpan<char>(this.Buffer, Start, this.Length - Start);
		}

		// Token: 0x0600CCD9 RID: 52441 RVA: 0x004A4EF1 File Offset: 0x004A30F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public char[] GetArray()
		{
			return this.Buffer;
		}

		// Token: 0x0600CCDA RID: 52442 RVA: 0x004A4EF9 File Offset: 0x004A30F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public char[] GetArray(int Capacity)
		{
			if (Capacity > this.Buffer.Length)
			{
				this.Grow(Capacity - this.Buffer.Length);
			}
			return this.Buffer;
		}

		// Token: 0x0600CCDB RID: 52443 RVA: 0x004A4F1C File Offset: 0x004A311C
		protected void Grow(int Incoming)
		{
			int num = this.Buffer.Length;
			int num2 = Math.Max(num + Incoming, num * 2);
			char[] array = ArrayPool<char>.Shared.Rent(num2);
			Array.Copy(this.Buffer, 0, array, 0, this.Length);
			ArrayPool<char>.Shared.Return(this.Buffer, true);
			this.Buffer = array;
		}

		// Token: 0x0600CCDC RID: 52444 RVA: 0x004A4F76 File Offset: 0x004A3176
		public bool Contains(char Value)
		{
			return this.IndexOf(Value) != -1;
		}

		// Token: 0x0600CCDD RID: 52445 RVA: 0x004A4F85 File Offset: 0x004A3185
		public bool Contains(string Value, StringComparison Comparison = StringComparison.Ordinal)
		{
			return this.IndexOf(Value, Comparison) != -1;
		}

		// Token: 0x0600CCDE RID: 52446 RVA: 0x004A4F95 File Offset: 0x004A3195
		public bool Contains(ReadOnlySpan<char> Value, StringComparison Comparison = StringComparison.Ordinal)
		{
			return this.IndexOf(Value, Comparison) != -1;
		}

		// Token: 0x0600CCDF RID: 52447 RVA: 0x004A4FA5 File Offset: 0x004A31A5
		public bool StartsWith(char Value)
		{
			return this.Length != 0 && this.Buffer[0] == Value;
		}

		// Token: 0x0600CCE0 RID: 52448 RVA: 0x004A4FBC File Offset: 0x004A31BC
		public bool StartsWith(ReadOnlySpan<char> Value)
		{
			return this.Buffer.AsSpan<char>(0, this.Length).StartsWith<char>(Value);
		}

		// Token: 0x0600CCE1 RID: 52449 RVA: 0x004A4FD6 File Offset: 0x004A31D6
		public bool StartsWith(ReadOnlySpan<char> Value, StringComparison Comparison)
		{
			return this.Buffer.AsSpan<char>(0, this.Length).StartsWith(Value, Comparison);
		}

		// Token: 0x0600CCE2 RID: 52450 RVA: 0x004A4FF6 File Offset: 0x004A31F6
		public bool EndsWith(char Value)
		{
			return this.Length != 0 && this.Buffer[this.Length - 1] == Value;
		}

		// Token: 0x0600CCE3 RID: 52451 RVA: 0x004A5014 File Offset: 0x004A3214
		public bool EndsWith(ReadOnlySpan<char> Value)
		{
			return this.Buffer.AsSpan<char>(0, this.Length).EndsWith<char>(Value);
		}

		// Token: 0x0600CCE4 RID: 52452 RVA: 0x004A502E File Offset: 0x004A322E
		public bool EndsWith(ReadOnlySpan<char> Value, StringComparison Comparison)
		{
			return this.Buffer.AsSpan<char>(0, this.Length).EndsWith(Value, Comparison);
		}

		// Token: 0x0600CCE5 RID: 52453 RVA: 0x004A504E File Offset: 0x004A324E
		public void CopyTo(char[] Destination, int DestinationIndex)
		{
			Array.Copy(this.Buffer, 0, Destination, DestinationIndex, this.Length);
		}

		// Token: 0x0600CCE6 RID: 52454 RVA: 0x004A5064 File Offset: 0x004A3264
		public void CopyTo(int SourceIndex, char[] Destination, int DestinationIndex, int Length)
		{
			Array.Copy(this.Buffer, SourceIndex, Destination, DestinationIndex, Length);
		}

		// Token: 0x0600CCE7 RID: 52455 RVA: 0x004A5076 File Offset: 0x004A3276
		public int IndexOf(char Value)
		{
			return Array.IndexOf<char>(this.Buffer, Value, 0, this.Length);
		}

		// Token: 0x0600CCE8 RID: 52456 RVA: 0x004A508B File Offset: 0x004A328B
		public int IndexOf(char Value, int Start)
		{
			return Array.IndexOf<char>(this.Buffer, Value, Start, this.Length - Start);
		}

		// Token: 0x0600CCE9 RID: 52457 RVA: 0x004A50A2 File Offset: 0x004A32A2
		public int IndexOf(char Value, int Start, int Length)
		{
			return Array.IndexOf<char>(this.Buffer, Value, Start, Length);
		}

		// Token: 0x0600CCEA RID: 52458 RVA: 0x004A50B2 File Offset: 0x004A32B2
		public int IndexOf(string Value, StringComparison Comparison = StringComparison.Ordinal)
		{
			return this.AsSpan().IndexOf(Value, Comparison);
		}

		// Token: 0x0600CCEB RID: 52459 RVA: 0x004A50CB File Offset: 0x004A32CB
		public int IndexOf(ReadOnlySpan<char> Value, StringComparison Comparison = StringComparison.Ordinal)
		{
			return this.AsSpan().IndexOf(Value, Comparison);
		}

		// Token: 0x0600CCEC RID: 52460 RVA: 0x004A50DF File Offset: 0x004A32DF
		public int LastIndexOf(char Value)
		{
			return Array.LastIndexOf<char>(this.Buffer, Value, 0, this.Length);
		}

		// Token: 0x0600CCED RID: 52461 RVA: 0x004A50F4 File Offset: 0x004A32F4
		public int LastIndexOf(char Value, int Start)
		{
			return Array.LastIndexOf<char>(this.Buffer, Value, Start, this.Length - Start);
		}

		// Token: 0x0600CCEE RID: 52462 RVA: 0x004A510B File Offset: 0x004A330B
		public int LastIndexOf(char Value, int Start, int Length)
		{
			return Array.LastIndexOf<char>(this.Buffer, Value, Start, Length);
		}

		// Token: 0x0600CCEF RID: 52463 RVA: 0x004A511C File Offset: 0x004A331C
		public TextBuilder Remove(int Start, int Length)
		{
			int length = this.Length;
			if (Start < 0)
			{
				throw new ArgumentOutOfRangeException("Start");
			}
			if (Length < 0 || Length + Start > length)
			{
				throw new ArgumentOutOfRangeException("Length");
			}
			if (Length == 0)
			{
				return this;
			}
			if (Length == length && Start == 0)
			{
				this.Length = 0;
				return this;
			}
			Array.Copy(this.Buffer, Start + Length, this.Buffer, Start, length - Start - Length);
			this.Length = length - Length;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCF0 RID: 52464 RVA: 0x004A519B File Offset: 0x004A339B
		public TextBuilder Replace(ReadOnlySpan<char> Old, ReadOnlySpan<char> New)
		{
			return this.Replace(Old, New, 0, this.Length);
		}

		// Token: 0x0600CCF1 RID: 52465 RVA: 0x004A51AC File Offset: 0x004A33AC
		public TextBuilder Replace(ReadOnlySpan<char> Old, ReadOnlySpan<char> New, int Start, int Length)
		{
			int length = this.Length;
			int length2 = Old.Length;
			int length3 = New.Length;
			if (length2 == 0)
			{
				throw new ArgumentException("Old span is empty", "Old");
			}
			if (Start > length)
			{
				throw new ArgumentOutOfRangeException("Start");
			}
			if (Length < 0 || Start > length - Length)
			{
				throw new ArgumentOutOfRangeException("Length");
			}
			for (;;)
			{
				int num = this.Buffer.AsSpan<char>(Start, Length).IndexOf(Old, StringComparison.Ordinal);
				if (num == -1)
				{
					break;
				}
				Start += num;
				this.Remove(Start, length2);
				this.Insert(Start, New);
				Start += length3;
				Length -= num + length3 - (length3 - length2);
			}
			return this;
		}

		// Token: 0x0600CCF2 RID: 52466 RVA: 0x004A5254 File Offset: 0x004A3454
		public TextBuilder TrimStart()
		{
			char[] buffer = this.Buffer;
			int num = 0;
			int num2 = this.Length - 1;
			if (num2 == -1)
			{
				return this;
			}
			while (char.IsWhiteSpace(buffer[num]))
			{
				num++;
			}
			int num3 = num2 - num + 1;
			Array.Copy(buffer, num, buffer, 0, num3);
			this.Length = num3;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCF3 RID: 52467 RVA: 0x004A52B0 File Offset: 0x004A34B0
		public TextBuilder TrimStart(char Value)
		{
			char[] buffer = this.Buffer;
			int num = 0;
			int num2 = this.Length - 1;
			if (num2 == -1)
			{
				return this;
			}
			while (buffer[num] == Value)
			{
				num++;
			}
			int num3 = num2 - num + 1;
			Array.Copy(buffer, num, buffer, 0, num3);
			this.Length = num3;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCF4 RID: 52468 RVA: 0x004A5308 File Offset: 0x004A3508
		public TextBuilder TrimEnd()
		{
			char[] buffer = this.Buffer;
			int num = this.Length - 1;
			if (num == -1)
			{
				return this;
			}
			while (char.IsWhiteSpace(buffer[num]))
			{
				num--;
			}
			this.Length = num + 1;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCF5 RID: 52469 RVA: 0x004A5354 File Offset: 0x004A3554
		public TextBuilder TrimEnd(char Value)
		{
			char[] buffer = this.Buffer;
			int num = this.Length - 1;
			if (num == -1)
			{
				return this;
			}
			while (buffer[num] == Value)
			{
				num--;
			}
			this.Length = num + 1;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCF6 RID: 52470 RVA: 0x004A539C File Offset: 0x004A359C
		public TextBuilder Trim()
		{
			char[] buffer = this.Buffer;
			int num = 0;
			int num2 = this.Length - 1;
			if (num2 == -1)
			{
				return this;
			}
			while (char.IsWhiteSpace(buffer[num]))
			{
				num++;
			}
			while (num2 > num && char.IsWhiteSpace(buffer[num2]))
			{
				num2--;
			}
			int num3 = num2 - num + 1;
			Array.Copy(buffer, num, buffer, 0, num3);
			this.Length = num3;
			this.Version++;
			return this;
		}

		/// <summary>Trims both ends of whitespace and removes sequences of whitespace after each new line.</summary>
		// Token: 0x0600CCF7 RID: 52471 RVA: 0x004A540C File Offset: 0x004A360C
		public TextBuilder Unindent(bool Normalize = true)
		{
			this.Trim();
			char[] buffer = this.Buffer;
			int i = 0;
			int num = this.Length;
			int num2 = num;
			while (i < num)
			{
				char c = buffer[i];
				if (c == '\n')
				{
					if (i - num2 > 1)
					{
						goto IL_003D;
					}
					num2 = i;
				}
				else if (num2 < num && !char.IsWhiteSpace(c))
				{
					goto IL_003D;
				}
				IL_0058:
				i++;
				continue;
				IL_003D:
				this.Remove(num2 + 1, i - num2 - 1);
				num -= i - num2 - 1;
				i = num2;
				num2 = num;
				goto IL_0058;
			}
			if (Normalize)
			{
				this.Normalize();
			}
			this.Version++;
			return this;
		}

		/// <summary>Remove carriage returns.</summary>
		// Token: 0x0600CCF8 RID: 52472 RVA: 0x004A5494 File Offset: 0x004A3694
		public TextBuilder Normalize()
		{
			char[] buffer = this.Buffer;
			int num = this.Length;
			for (int i = num - 1; i >= 0; i--)
			{
				if (buffer[i] == '\r')
				{
					Array.Copy(buffer, i + 1, buffer, i, num - i - 1);
					num--;
				}
			}
			this.Length = num;
			this.Version++;
			return this;
		}

		// Token: 0x0600CCF9 RID: 52473 RVA: 0x004A54EE File Offset: 0x004A36EE
		public bool Equals(string Other)
		{
			return Other != null && this.Equals(Other.AsSpan(), StringComparison.Ordinal);
		}

		// Token: 0x0600CCFA RID: 52474 RVA: 0x004A5504 File Offset: 0x004A3704
		public bool Equals(ReadOnlySpan<char> Other, StringComparison Comparison = StringComparison.Ordinal)
		{
			int length = this.Length;
			return Other.Length == length && Other.Equals(this.Buffer.AsSpan<char>(0, length), Comparison);
		}

		// Token: 0x0600CCFB RID: 52475 RVA: 0x004A553D File Offset: 0x004A373D
		public TextBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		// Token: 0x0600CCFC RID: 52476 RVA: 0x004A5547 File Offset: 0x004A3747
		public TextBuilder Rebase(ReadOnlySpan<char> Value)
		{
			this.Length = 0;
			return this.Append(Value);
		}

		// Token: 0x0600CCFD RID: 52477 RVA: 0x004A5557 File Offset: 0x004A3757
		public override string ToString()
		{
			if (this.Length != 0)
			{
				return new string(this.Buffer, 0, this.Length);
			}
			return string.Empty;
		}

		// Token: 0x0600CCFE RID: 52478 RVA: 0x004A5579 File Offset: 0x004A3779
		public string ToString(int Start, int Length)
		{
			return new string(this.Buffer, Start, Length);
		}

		// Token: 0x0600CCFF RID: 52479 RVA: 0x004A5588 File Offset: 0x004A3788
		public virtual void Dispose(bool Destroy)
		{
			if (this.Buffer == null)
			{
				return;
			}
			ArrayPool<char>.Shared.Return(this.Buffer, true);
			this.Buffer = null;
			this.Length = 0;
			this.Version = 0;
		}

		// Token: 0x0600CD00 RID: 52480 RVA: 0x004A55B9 File Offset: 0x004A37B9
		public virtual void Dispose()
		{
			this.Dispose(false);
		}

		// Token: 0x0600CD01 RID: 52481 RVA: 0x004A55C2 File Offset: 0x004A37C2
		public TextBuilder.Enumerator GetEnumerator()
		{
			return new TextBuilder.Enumerator(this);
		}

		// Token: 0x0600CD02 RID: 52482 RVA: 0x004A55CA File Offset: 0x004A37CA
		IEnumerator<char> IEnumerable<char>.GetEnumerator()
		{
			return new TextBuilder.Enumerator(this);
		}

		// Token: 0x0600CD03 RID: 52483 RVA: 0x004A55D7 File Offset: 0x004A37D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new TextBuilder.Enumerator(this);
		}

		// Token: 0x0600CD04 RID: 52484 RVA: 0x004A55E4 File Offset: 0x004A37E4
		protected override void Finalize()
		{
			try
			{
				if (!this.Finalizable)
				{
					if (GameManager.Playing)
					{
						if (Options.TextBuilderDebug)
						{
							StackTrace stackTrace;
							if (TextBuilder.Traces.TryRemove(this, out stackTrace))
							{
								string name = base.GetType().GetName(true);
								string text = " leaked from ";
								StackTrace stackTrace2 = stackTrace;
								MetricsManager.LogError(name + text + ((stackTrace2 != null) ? stackTrace2.ToString() : null));
							}
							else
							{
								MetricsManager.LogError(base.GetType().GetName(true) + " leaked and was not disposed, no trace was associated with this instance.");
							}
						}
						else
						{
							MetricsManager.LogError(base.GetType().GetName(true) + " leaked and was not disposed, enable Text Builder leak analyzation in Options->Mods to get stack traces.");
						}
					}
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04004F3C RID: 20284
		private static char[] hexVals = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'a', 'b', 'c', 'd', 'e', 'f'
		};

		// Token: 0x04004F3D RID: 20285
		private const ulong Int32AbsoluteMinimum = 2147483648UL;

		// Token: 0x04004F3E RID: 20286
		private const ulong Int64AbsoluteMinimum = 9223372036854775808UL;

		// Token: 0x04004F3F RID: 20287
		private const int Int32Shift = 31;

		// Token: 0x04004F40 RID: 20288
		private const int Int64Shift = 63;

		// Token: 0x04004F41 RID: 20289
		private static readonly ConcurrentBag<TextBuilder.Entry> Bag = new ConcurrentBag<TextBuilder.Entry>();

		// Token: 0x04004F42 RID: 20290
		protected static readonly ConcurrentDictionary<TextBuilder, StackTrace> Traces = new ConcurrentDictionary<TextBuilder, StackTrace>();

		// Token: 0x04004F43 RID: 20291
		protected char[] Buffer;

		// Token: 0x04004F44 RID: 20292
		protected char[] ScratchBuffer;

		// Token: 0x04004F45 RID: 20293
		public int Length;

		// Token: 0x04004F46 RID: 20294
		public int Version;

		// Token: 0x02001D61 RID: 7521
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal static class Pool
		{
			/// <summary>Warm up the thread statics of concurrent pool from permanent threads.</summary>
			// Token: 0x0600F40D RID: 62477 RVA: 0x00551558 File Offset: 0x0054F758
			public static void Populate(int Amount = 8)
			{
				for (int i = 0; i < Amount; i++)
				{
					TextBuilder.Bag.Add(new TextBuilder.Entry());
				}
			}
		}

		// Token: 0x02001D62 RID: 7522
		private sealed class Entry : TextBuilder
		{
			// Token: 0x17000FDA RID: 4058
			// (get) Token: 0x0600F40E RID: 62478 RVA: 0x00551580 File Offset: 0x0054F780
			protected override bool Finalizable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600F40F RID: 62479 RVA: 0x00551583 File Offset: 0x0054F783
			public override void Dispose(bool Destroy)
			{
				base.Dispose(Destroy);
				if (!Destroy)
				{
					TextBuilder.Bag.Add(this);
				}
			}

			// Token: 0x0600F410 RID: 62480 RVA: 0x0055159A File Offset: 0x0054F79A
			public Entry()
				: base(32)
			{
			}
		}

		// Token: 0x02001D63 RID: 7523
		[Serializable]
		public struct Enumerator : IEnumerator<char>, IEnumerator, IDisposable
		{
			// Token: 0x17000FDB RID: 4059
			// (get) Token: 0x0600F411 RID: 62481 RVA: 0x005515A4 File Offset: 0x0054F7A4
			public char Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x17000FDC RID: 4060
			// (get) Token: 0x0600F412 RID: 62482 RVA: 0x005515AC File Offset: 0x0054F7AC
			object IEnumerator.Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x0600F413 RID: 62483 RVA: 0x005515B9 File Offset: 0x0054F7B9
			public Enumerator(TextBuilder Builder)
			{
				this.Builder = Builder;
				this.Index = 0;
				this.Version = Builder.Version;
				this.Item = '\0';
			}

			// Token: 0x0600F414 RID: 62484 RVA: 0x005515DC File Offset: 0x0054F7DC
			public bool MoveNext()
			{
				TextBuilder builder = this.Builder;
				if (builder.Version != this.Version)
				{
					throw new InvalidOperationException();
				}
				int index = this.Index;
				if (index >= builder.Length)
				{
					this.Item = '\0';
					return false;
				}
				this.Item = builder.Buffer[index];
				this.Index = index + 1;
				return true;
			}

			// Token: 0x0600F415 RID: 62485 RVA: 0x00551635 File Offset: 0x0054F835
			public void Reset()
			{
				if (this.Builder.Version != this.Version)
				{
					throw new InvalidOperationException();
				}
				this.Index = 0;
				this.Item = '\0';
			}

			// Token: 0x0600F416 RID: 62486 RVA: 0x0055165E File Offset: 0x0054F85E
			public void Dispose()
			{
			}

			// Token: 0x0400729B RID: 29339
			private TextBuilder Builder;

			// Token: 0x0400729C RID: 29340
			private int Index;

			// Token: 0x0400729D RID: 29341
			private int Version;

			// Token: 0x0400729E RID: 29342
			private char Item;
		}
	}
}
