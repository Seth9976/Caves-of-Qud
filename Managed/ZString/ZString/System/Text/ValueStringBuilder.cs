using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200000F RID: 15
	internal ref struct ValueStringBuilder
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00007FCA File Offset: 0x000061CA
		public ValueStringBuilder(Span<char> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._chars = initialBuffer;
			this._pos = 0;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00007FE1 File Offset: 0x000061E1
		public ValueStringBuilder(int initialCapacity)
		{
			this._arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialCapacity);
			this._chars = this._arrayToReturnToPool;
			this._pos = 0;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000800C File Offset: 0x0000620C
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00008014 File Offset: 0x00006214
		public int Length
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000801D File Offset: 0x0000621D
		public int Capacity
		{
			get
			{
				return this._chars.Length;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000802A File Offset: 0x0000622A
		public void EnsureCapacity(int capacity)
		{
			if (capacity > this._chars.Length)
			{
				this.Grow(capacity - this._pos);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00008048 File Offset: 0x00006248
		public ref char GetPinnableReference()
		{
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008055 File Offset: 0x00006255
		public unsafe ref char GetPinnableReference(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return MemoryMarshal.GetReference<char>(this._chars);
		}

		// Token: 0x17000004 RID: 4
		public ref char this[int index]
		{
			get
			{
				return this._chars[index];
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008094 File Offset: 0x00006294
		[NullableContext(1)]
		public override string ToString()
		{
			string text = this._chars.Slice(0, this._pos).ToString();
			this.Dispose();
			return text;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000080C7 File Offset: 0x000062C7
		public Span<char> RawChars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000080CF File Offset: 0x000062CF
		public unsafe ReadOnlySpan<char> AsSpan(bool terminate)
		{
			if (terminate)
			{
				this.EnsureCapacity(this.Length + 1);
				*this._chars[this.Length] = '\0';
			}
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000810C File Offset: 0x0000630C
		public ReadOnlySpan<char> AsSpan()
		{
			return this._chars.Slice(0, this._pos);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00008125 File Offset: 0x00006325
		public ReadOnlySpan<char> AsSpan(int start)
		{
			return this._chars.Slice(start, this._pos - start);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00008140 File Offset: 0x00006340
		public ReadOnlySpan<char> AsSpan(int start, int length)
		{
			return this._chars.Slice(start, length);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008154 File Offset: 0x00006354
		public bool TryCopyTo(Span<char> destination, out int charsWritten)
		{
			if (this._chars.Slice(0, this._pos).TryCopyTo(destination))
			{
				charsWritten = this._pos;
				this.Dispose();
				return true;
			}
			charsWritten = 0;
			this.Dispose();
			return false;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008198 File Offset: 0x00006398
		public void Insert(int index, char value, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + count));
			this._chars.Slice(index, count).Fill(value);
			this._pos += count;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008214 File Offset: 0x00006414
		[NullableContext(1)]
		public void Insert(int index, string s)
		{
			if (s == null)
			{
				return;
			}
			int length = s.Length;
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			int num = this._pos - index;
			this._chars.Slice(index, num).CopyTo(this._chars.Slice(index + length));
			s.AsSpan().CopyTo(this._chars.Slice(index));
			this._pos += length;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000829C File Offset: 0x0000649C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(char c)
		{
			int pos = this._pos;
			if (pos < this._chars.Length)
			{
				*this._chars[pos] = c;
				this._pos = pos + 1;
				return;
			}
			this.GrowAndAppend(c);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000082E0 File Offset: 0x000064E0
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void Append(string s)
		{
			if (s == null)
			{
				return;
			}
			int pos = this._pos;
			if (s.Length == 1 && pos < this._chars.Length)
			{
				*this._chars[pos] = s[0];
				this._pos = pos + 1;
				return;
			}
			this.AppendSlow(s);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008334 File Offset: 0x00006534
		[NullableContext(1)]
		private void AppendSlow(string s)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - s.Length)
			{
				this.Grow(s.Length);
			}
			s.AsSpan().CopyTo(this._chars.Slice(pos));
			this._pos += s.Length;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00008398 File Offset: 0x00006598
		public unsafe void Append(char c, int count)
		{
			if (this._pos > this._chars.Length - count)
			{
				this.Grow(count);
			}
			Span<char> span = this._chars.Slice(this._pos, count);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = c;
			}
			this._pos += count;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008400 File Offset: 0x00006600
		public unsafe void Append(char* value, int length)
		{
			if (this._pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			Span<char> span = this._chars.Slice(this._pos, length);
			for (int i = 0; i < span.Length; i++)
			{
				*span[i] = *(value++);
			}
			this._pos += length;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000846C File Offset: 0x0000666C
		public void Append(ReadOnlySpan<char> value)
		{
			if (this._pos > this._chars.Length - value.Length)
			{
				this.Grow(value.Length);
			}
			value.CopyTo(this._chars.Slice(this._pos));
			this._pos += value.Length;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000084D0 File Offset: 0x000066D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Span<char> AppendSpan(int length)
		{
			int pos = this._pos;
			if (pos > this._chars.Length - length)
			{
				this.Grow(length);
			}
			this._pos = pos + length;
			return this._chars.Slice(pos, length);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00008511 File Offset: 0x00006711
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void GrowAndAppend(char c)
		{
			this.Grow(1);
			this.Append(c);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00008524 File Offset: 0x00006724
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void Grow(int additionalCapacityBeyondPos)
		{
			char[] array = ArrayPool<char>.Shared.Rent(Math.Max(this._pos + additionalCapacityBeyondPos, this._chars.Length * 2));
			this._chars.Slice(0, this._pos).CopyTo(array);
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this._chars = (this._arrayToReturnToPool = array);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000085A4 File Offset: 0x000067A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			char[] arrayToReturnToPool = this._arrayToReturnToPool;
			this = default(ValueStringBuilder);
			if (arrayToReturnToPool != null)
			{
				ArrayPool<char>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04000031 RID: 49
		[Nullable(2)]
		private char[] _arrayToReturnToPool;

		// Token: 0x04000032 RID: 50
		private Span<char> _chars;

		// Token: 0x04000033 RID: 51
		private int _pos;
	}
}
