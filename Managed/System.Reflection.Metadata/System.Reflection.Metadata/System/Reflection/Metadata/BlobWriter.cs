using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	public struct BlobWriter
	{
		/// <param name="size" />
		// Token: 0x0600023D RID: 573 RVA: 0x000070D2 File Offset: 0x000052D2
		public BlobWriter(int size)
		{
			this = new BlobWriter(new byte[size]);
		}

		/// <param name="buffer" />
		// Token: 0x0600023E RID: 574 RVA: 0x000070E0 File Offset: 0x000052E0
		public BlobWriter(byte[] buffer)
		{
			this = new BlobWriter(buffer, 0, buffer.Length);
		}

		/// <param name="blob" />
		// Token: 0x0600023F RID: 575 RVA: 0x000070ED File Offset: 0x000052ED
		public BlobWriter(Blob blob)
		{
			this = new BlobWriter(blob.Buffer, blob.Start, blob.Length);
		}

		/// <param name="buffer" />
		/// <param name="start" />
		/// <param name="count" />
		// Token: 0x06000240 RID: 576 RVA: 0x00007108 File Offset: 0x00005308
		public BlobWriter(byte[] buffer, int start, int count)
		{
			this._buffer = buffer;
			this._start = start;
			this._position = start;
			this._end = start + count;
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007128 File Offset: 0x00005328
		internal bool IsDefault
		{
			get
			{
				return this._buffer == null;
			}
		}

		/// <summary>Compares the current content of this writer with another one.</summary>
		/// <param name="other" />
		// Token: 0x06000242 RID: 578 RVA: 0x00007133 File Offset: 0x00005333
		public bool ContentEquals(BlobWriter other)
		{
			return this.Length == other.Length && ByteSequenceComparer.Equals(this._buffer, this._start, other._buffer, other._start, this.Length);
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007169 File Offset: 0x00005369
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007178 File Offset: 0x00005378
		public int Offset
		{
			get
			{
				return this._position - this._start;
			}
			set
			{
				if (value < 0 || this._start > this._end - value)
				{
					Throw.ValueArgumentOutOfRange();
				}
				this._position = this._start + value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000071A1 File Offset: 0x000053A1
		public int Length
		{
			get
			{
				return this._end - this._start;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000071B0 File Offset: 0x000053B0
		public int RemainingBytes
		{
			get
			{
				return this._end - this._position;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000071BF File Offset: 0x000053BF
		public Blob Blob
		{
			get
			{
				return new Blob(this._buffer, this._start, this.Length);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000071D8 File Offset: 0x000053D8
		public byte[] ToArray()
		{
			return this.ToArray(0, this.Offset);
		}

		/// <param name="start" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">The range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the buffer content.</exception>
		// Token: 0x06000249 RID: 585 RVA: 0x000071E8 File Offset: 0x000053E8
		public byte[] ToArray(int start, int byteCount)
		{
			BlobUtilities.ValidateRange(this.Length, start, byteCount, "byteCount");
			byte[] array = new byte[byteCount];
			Array.Copy(this._buffer, this._start + start, array, 0, byteCount);
			return array;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007225 File Offset: 0x00005425
		[NullableContext(0)]
		public ImmutableArray<byte> ToImmutableArray()
		{
			return this.ToImmutableArray(0, this.Offset);
		}

		/// <param name="start" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">Range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the buffer content.</exception>
		// Token: 0x0600024B RID: 587 RVA: 0x00007234 File Offset: 0x00005434
		[NullableContext(0)]
		public ImmutableArray<byte> ToImmutableArray(int start, int byteCount)
		{
			byte[] array = this.ToArray(start, byteCount);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00007254 File Offset: 0x00005454
		private int Advance(int value)
		{
			int position = this._position;
			if (position > this._end - value)
			{
				Throw.OutOfBounds();
			}
			this._position = position + value;
			return position;
		}

		/// <param name="value" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		// Token: 0x0600024D RID: 589 RVA: 0x00007284 File Offset: 0x00005484
		public unsafe void WriteBytes(byte value, int byteCount)
		{
			if (byteCount < 0)
			{
				Throw.ArgumentOutOfRange("byteCount");
			}
			int num = this.Advance(byteCount);
			byte[] array;
			byte* ptr;
			if ((array = this._buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + num;
			for (int i = 0; i < byteCount; i++)
			{
				ptr2[i] = value;
			}
			array = null;
		}

		/// <param name="buffer" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		// Token: 0x0600024E RID: 590 RVA: 0x000072E0 File Offset: 0x000054E0
		[NullableContext(0)]
		public unsafe void WriteBytes(byte* buffer, int byteCount)
		{
			if (buffer == null)
			{
				Throw.ArgumentNull("buffer");
			}
			if (byteCount < 0)
			{
				Throw.ArgumentOutOfRange("byteCount");
			}
			this.WriteBytesUnchecked(buffer, byteCount);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00007308 File Offset: 0x00005508
		private unsafe void WriteBytesUnchecked(byte* buffer, int byteCount)
		{
			int num = this.Advance(byteCount);
			Marshal.Copy((IntPtr)((void*)buffer), this._buffer, num, byteCount);
		}

		/// <param name="source" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000250 RID: 592 RVA: 0x00007330 File Offset: 0x00005530
		public void WriteBytes(BlobBuilder source)
		{
			if (source == null)
			{
				Throw.ArgumentNull("source");
			}
			source.WriteContentTo(ref this);
		}

		/// <param name="source" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		// Token: 0x06000251 RID: 593 RVA: 0x00007348 File Offset: 0x00005548
		public int WriteBytes(Stream source, int byteCount)
		{
			if (source == null)
			{
				Throw.ArgumentNull("source");
			}
			if (byteCount < 0)
			{
				Throw.ArgumentOutOfRange("byteCount");
			}
			int num = this.Advance(byteCount);
			int num2 = source.TryReadAll(this._buffer, num, byteCount);
			this._position = num + num2;
			return num2;
		}

		/// <param name="buffer" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x06000252 RID: 594 RVA: 0x00007391 File Offset: 0x00005591
		[NullableContext(0)]
		public void WriteBytes(ImmutableArray<byte> buffer)
		{
			this.WriteBytes(buffer, 0, buffer.IsDefault ? 0 : buffer.Length);
		}

		/// <param name="buffer" />
		/// <param name="start" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the <paramref name="buffer" />.</exception>
		// Token: 0x06000253 RID: 595 RVA: 0x000073AE File Offset: 0x000055AE
		[NullableContext(0)]
		public void WriteBytes(ImmutableArray<byte> buffer, int start, int byteCount)
		{
			this.WriteBytes(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(buffer), start, byteCount);
		}

		/// <param name="buffer" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		// Token: 0x06000254 RID: 596 RVA: 0x000073BE File Offset: 0x000055BE
		public void WriteBytes(byte[] buffer)
		{
			this.WriteBytes(buffer, 0, (buffer != null) ? buffer.Length : 0);
		}

		/// <param name="buffer" />
		/// <param name="start" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the <paramref name="buffer" />.</exception>
		// Token: 0x06000255 RID: 597 RVA: 0x000073D4 File Offset: 0x000055D4
		public unsafe void WriteBytes(byte[] buffer, int start, int byteCount)
		{
			if (buffer == null)
			{
				Throw.ArgumentNull("buffer");
			}
			BlobUtilities.ValidateRange(buffer.Length, start, byteCount, "byteCount");
			if (buffer.Length == 0)
			{
				return;
			}
			fixed (byte* ptr = &buffer[0])
			{
				byte* ptr2 = ptr;
				this.WriteBytes(ptr2 + start, byteCount);
			}
		}

		/// <param name="offset" />
		// Token: 0x06000256 RID: 598 RVA: 0x0000741A File Offset: 0x0000561A
		public void PadTo(int offset)
		{
			this.WriteBytes(0, offset - this.Offset);
		}

		/// <param name="alignment" />
		// Token: 0x06000257 RID: 599 RVA: 0x0000742C File Offset: 0x0000562C
		public void Align(int alignment)
		{
			int offset = this.Offset;
			this.WriteBytes(0, BitArithmetic.Align(offset, alignment) - offset);
		}

		/// <param name="value" />
		// Token: 0x06000258 RID: 600 RVA: 0x00007450 File Offset: 0x00005650
		public void WriteBoolean(bool value)
		{
			this.WriteByte(value ? 1 : 0);
		}

		/// <param name="value" />
		// Token: 0x06000259 RID: 601 RVA: 0x00007460 File Offset: 0x00005660
		public void WriteByte(byte value)
		{
			int num = this.Advance(1);
			this._buffer[num] = value;
		}

		/// <param name="value" />
		// Token: 0x0600025A RID: 602 RVA: 0x0000747E File Offset: 0x0000567E
		public void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		/// <param name="value" />
		// Token: 0x0600025B RID: 603 RVA: 0x00007488 File Offset: 0x00005688
		public void WriteDouble(double value)
		{
			int num = this.Advance(8);
			this._buffer.WriteDouble(num, value);
		}

		/// <param name="value" />
		// Token: 0x0600025C RID: 604 RVA: 0x000074AC File Offset: 0x000056AC
		public void WriteSingle(float value)
		{
			int num = this.Advance(4);
			this._buffer.WriteSingle(num, value);
		}

		/// <param name="value" />
		// Token: 0x0600025D RID: 605 RVA: 0x000074CE File Offset: 0x000056CE
		public void WriteInt16(short value)
		{
			this.WriteUInt16((ushort)value);
		}

		/// <param name="value" />
		// Token: 0x0600025E RID: 606 RVA: 0x000074D8 File Offset: 0x000056D8
		public void WriteUInt16(ushort value)
		{
			int num = this.Advance(2);
			this._buffer.WriteUInt16(num, value);
		}

		/// <param name="value" />
		// Token: 0x0600025F RID: 607 RVA: 0x000074FA File Offset: 0x000056FA
		public void WriteInt16BE(short value)
		{
			this.WriteUInt16BE((ushort)value);
		}

		/// <param name="value" />
		// Token: 0x06000260 RID: 608 RVA: 0x00007504 File Offset: 0x00005704
		public void WriteUInt16BE(ushort value)
		{
			int num = this.Advance(2);
			this._buffer.WriteUInt16BE(num, value);
		}

		/// <param name="value" />
		// Token: 0x06000261 RID: 609 RVA: 0x00007526 File Offset: 0x00005726
		public void WriteInt32BE(int value)
		{
			this.WriteUInt32BE((uint)value);
		}

		/// <param name="value" />
		// Token: 0x06000262 RID: 610 RVA: 0x00007530 File Offset: 0x00005730
		public void WriteUInt32BE(uint value)
		{
			int num = this.Advance(4);
			this._buffer.WriteUInt32BE(num, value);
		}

		/// <param name="value" />
		// Token: 0x06000263 RID: 611 RVA: 0x00007552 File Offset: 0x00005752
		public void WriteInt32(int value)
		{
			this.WriteUInt32((uint)value);
		}

		/// <param name="value" />
		// Token: 0x06000264 RID: 612 RVA: 0x0000755C File Offset: 0x0000575C
		public void WriteUInt32(uint value)
		{
			int num = this.Advance(4);
			this._buffer.WriteUInt32(num, value);
		}

		/// <param name="value" />
		// Token: 0x06000265 RID: 613 RVA: 0x0000757E File Offset: 0x0000577E
		public void WriteInt64(long value)
		{
			this.WriteUInt64((ulong)value);
		}

		/// <param name="value" />
		// Token: 0x06000266 RID: 614 RVA: 0x00007588 File Offset: 0x00005788
		public void WriteUInt64(ulong value)
		{
			int num = this.Advance(8);
			this._buffer.WriteUInt64(num, value);
		}

		/// <param name="value" />
		// Token: 0x06000267 RID: 615 RVA: 0x000075AC File Offset: 0x000057AC
		public void WriteDecimal(decimal value)
		{
			int num = this.Advance(13);
			this._buffer.WriteDecimal(num, value);
		}

		/// <param name="value" />
		// Token: 0x06000268 RID: 616 RVA: 0x000075D0 File Offset: 0x000057D0
		public void WriteGuid(Guid value)
		{
			int num = this.Advance(16);
			this._buffer.WriteGuid(num, value);
		}

		/// <param name="value" />
		// Token: 0x06000269 RID: 617 RVA: 0x000075F3 File Offset: 0x000057F3
		public void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.Ticks);
		}

		/// <summary>Writes a reference to a heap (heap offset) or a table (row number).</summary>
		/// <param name="reference">Heap offset or table row number.</param>
		/// <param name="isSmall">
		///   <see langword="true" /> to encode the reference as 16-bit integer, <see langword="false" /> to encode as 32-bit integer.</param>
		// Token: 0x0600026A RID: 618 RVA: 0x00007602 File Offset: 0x00005802
		public void WriteReference(int reference, bool isSmall)
		{
			if (isSmall)
			{
				this.WriteUInt16((ushort)reference);
				return;
			}
			this.WriteInt32(reference);
		}

		/// <summary>Writes a UTF16 (little-endian) encoded string at the current position.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600026B RID: 619 RVA: 0x00007618 File Offset: 0x00005818
		public unsafe void WriteUTF16(char[] value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			if (value.Length == 0)
			{
				return;
			}
			fixed (char* ptr = &value[0])
			{
				char* ptr2 = ptr;
				this.WriteBytesUnchecked((byte*)ptr2, value.Length * 2);
			}
		}

		/// <summary>Writes a UTF16 (little-endian) encoded string at the current position.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600026C RID: 620 RVA: 0x00007654 File Offset: 0x00005854
		public unsafe void WriteUTF16(string value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.WriteBytesUnchecked((byte*)ptr, value.Length * 2);
			}
		}

		/// <summary>Writes a string in SerString format (see ECMA-335-II 23.3 Custom attributes).</summary>
		/// <param name="str" />
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x0600026D RID: 621 RVA: 0x0000768F File Offset: 0x0000588F
		[NullableContext(2)]
		public void WriteSerializedString(string str)
		{
			if (str == null)
			{
				this.WriteByte(byte.MaxValue);
				return;
			}
			this.WriteUTF8(str, 0, str.Length, true, true);
		}

		/// <summary>Writes a string in User String (#US) heap format (see ECMA-335-II 24.2.4 #US and #Blob heaps).</summary>
		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x0600026E RID: 622 RVA: 0x000076B0 File Offset: 0x000058B0
		public void WriteUserString(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteCompressedInteger(BlobUtilities.GetUserStringByteLength(value.Length));
			this.WriteUTF16(value);
			this.WriteByte(BlobUtilities.GetUserStringTrailingByte(value));
		}

		/// <summary>Writes a UTF8 encoded string at the current position.</summary>
		/// <param name="value" />
		/// <param name="allowUnpairedSurrogates" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600026F RID: 623 RVA: 0x000076E4 File Offset: 0x000058E4
		public void WriteUTF8(string value, bool allowUnpairedSurrogates)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			this.WriteUTF8(value, 0, value.Length, allowUnpairedSurrogates, false);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007704 File Offset: 0x00005904
		private unsafe void WriteUTF8(string str, int start, int length, bool allowUnpairedSurrogates, bool prependSize)
		{
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + start;
				int utf8ByteCount = BlobUtilities.GetUTF8ByteCount(ptr2, length);
				if (prependSize)
				{
					this.WriteCompressedInteger(utf8ByteCount);
				}
				int num = this.Advance(utf8ByteCount);
				this._buffer.WriteUTF8(num, ptr2, length, utf8ByteCount, allowUnpairedSurrogates);
			}
		}

		/// <summary>Implements compressed signed integer encoding as defined by ECMA-335-II chapter 23.2: Blobs and signatures.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> can't be represented as a compressed signed integer.</exception>
		// Token: 0x06000271 RID: 625 RVA: 0x00007758 File Offset: 0x00005958
		public void WriteCompressedSignedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedSignedInteger(ref this, value);
		}

		/// <summary>Implements compressed unsigned integer encoding as defined by ECMA-335-II chapter 23.2: Blobs and signatures.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> can't be represented as a compressed unsigned integer.</exception>
		// Token: 0x06000272 RID: 626 RVA: 0x00007761 File Offset: 0x00005961
		public void WriteCompressedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedInteger(ref this, (uint)value);
		}

		/// <summary>Writes a constant value (see ECMA-335 Partition II section 22.9) at the current position.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of a constant type.</exception>
		// Token: 0x06000273 RID: 627 RVA: 0x0000776A File Offset: 0x0000596A
		[NullableContext(2)]
		public void WriteConstant(object value)
		{
			BlobWriterImpl.WriteConstant(ref this, value);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007773 File Offset: 0x00005973
		public void Clear()
		{
			this._position = this._start;
		}

		// Token: 0x040001A2 RID: 418
		private readonly byte[] _buffer;

		// Token: 0x040001A3 RID: 419
		private readonly int _start;

		// Token: 0x040001A4 RID: 420
		private readonly int _end;

		// Token: 0x040001A5 RID: 421
		private int _position;
	}
}
