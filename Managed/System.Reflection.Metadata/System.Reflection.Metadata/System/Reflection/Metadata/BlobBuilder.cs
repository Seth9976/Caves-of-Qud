using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Metadata
{
	// Token: 0x02000042 RID: 66
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public class BlobBuilder
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00007D06 File Offset: 0x00005F06
		private BlobBuilder FirstChunk
		{
			get
			{
				return this._nextOrPrevious._nextOrPrevious;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00007D13 File Offset: 0x00005F13
		private bool IsHead
		{
			get
			{
				return (this._length & 2147483648U) == 0U;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00007D24 File Offset: 0x00005F24
		private int Length
		{
			get
			{
				return (int)(this._length & 2147483647U);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00007D32 File Offset: 0x00005F32
		private uint FrozenLength
		{
			get
			{
				return this._length | 2147483648U;
			}
		}

		/// <param name="capacity" />
		// Token: 0x06000280 RID: 640 RVA: 0x00007D40 File Offset: 0x00005F40
		public BlobBuilder(int capacity = 256)
		{
			if (capacity < 0)
			{
				Throw.ArgumentOutOfRange("capacity");
			}
			this._nextOrPrevious = this;
			this._buffer = new byte[Math.Max(16, capacity)];
		}

		/// <param name="minimalSize" />
		// Token: 0x06000281 RID: 641 RVA: 0x00007D70 File Offset: 0x00005F70
		protected virtual BlobBuilder AllocateChunk(int minimalSize)
		{
			return new BlobBuilder(Math.Max(this._buffer.Length, minimalSize));
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007D85 File Offset: 0x00005F85
		protected virtual void FreeChunk()
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007D88 File Offset: 0x00005F88
		public void Clear()
		{
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			BlobBuilder firstChunk = this.FirstChunk;
			if (firstChunk != this)
			{
				byte[] buffer = firstChunk._buffer;
				firstChunk._length = this.FrozenLength;
				firstChunk._buffer = this._buffer;
				this._buffer = buffer;
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				if (blobBuilder != this)
				{
					blobBuilder.ClearChunk();
					blobBuilder.FreeChunk();
				}
			}
			this.ClearChunk();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007E30 File Offset: 0x00006030
		protected void Free()
		{
			this.Clear();
			this.FreeChunk();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007E3E File Offset: 0x0000603E
		internal void ClearChunk()
		{
			this._length = 0U;
			this._previousLengthOrFrozenSuffixLengthDelta = 0;
			this._nextOrPrevious = this;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007E55 File Offset: 0x00006055
		private void CheckInvariants()
		{
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00007E57 File Offset: 0x00006057
		public int Count
		{
			get
			{
				return this._previousLengthOrFrozenSuffixLengthDelta + this.Length;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00007E66 File Offset: 0x00006066
		// (set) Token: 0x06000289 RID: 649 RVA: 0x00007E6E File Offset: 0x0000606E
		private int PreviousLength
		{
			get
			{
				return this._previousLengthOrFrozenSuffixLengthDelta;
			}
			set
			{
				this._previousLengthOrFrozenSuffixLengthDelta = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00007E77 File Offset: 0x00006077
		protected int FreeBytes
		{
			get
			{
				return this._buffer.Length - this.Length;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00007E88 File Offset: 0x00006088
		protected internal int ChunkCapacity
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00007E92 File Offset: 0x00006092
		internal BlobBuilder.Chunks GetChunks()
		{
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			return new BlobBuilder.Chunks(this);
		}

		/// <summary>Returns a sequence of all blobs that represent the content of the builder.</summary>
		/// <exception cref="T:System.InvalidOperationException">Content is not available; the builder has been linked with another one.</exception>
		/// <returns>A sequence of blobs.</returns>
		// Token: 0x0600028D RID: 653 RVA: 0x00007EA7 File Offset: 0x000060A7
		public BlobBuilder.Blobs GetBlobs()
		{
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			return new BlobBuilder.Blobs(this);
		}

		/// <summary>Compares the current content of this writer with another one.</summary>
		/// <param name="other">A <see cref="T:System.Reflection.Metadata.BlobBuilder" /> instance to compare with this one.</param>
		/// <exception cref="T:System.InvalidOperationException">Content is not available; the builder has been linked with another one.</exception>
		/// <returns>
		///   <see langword="true" /> if equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600028E RID: 654 RVA: 0x00007EBC File Offset: 0x000060BC
		public bool ContentEquals(BlobBuilder other)
		{
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			if (!other.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (this.Count != other.Count)
			{
				return false;
			}
			BlobBuilder.Chunks chunks = this.GetChunks();
			BlobBuilder.Chunks chunks2 = other.GetChunks();
			int num = 0;
			int num2 = 0;
			bool flag = chunks.MoveNext();
			bool flag2 = chunks2.MoveNext();
			while (flag && flag2)
			{
				BlobBuilder blobBuilder = chunks.Current;
				BlobBuilder blobBuilder2 = chunks2.Current;
				int num3 = Math.Min(blobBuilder.Length - num, blobBuilder2.Length - num2);
				if (!ByteSequenceComparer.Equals(blobBuilder._buffer, num, blobBuilder2._buffer, num2, num3))
				{
					return false;
				}
				num += num3;
				num2 += num3;
				if (num == blobBuilder.Length)
				{
					flag = chunks.MoveNext();
					num = 0;
				}
				if (num2 == blobBuilder2.Length)
				{
					flag2 = chunks2.MoveNext();
					num2 = 0;
				}
			}
			return flag == flag2;
		}

		/// <exception cref="T:System.InvalidOperationException">Content is not available; the builder has been linked with another one.</exception>
		// Token: 0x0600028F RID: 655 RVA: 0x00007FAE File Offset: 0x000061AE
		public byte[] ToArray()
		{
			return this.ToArray(0, this.Count);
		}

		/// <param name="start" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">The range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the buffer content.</exception>
		/// <exception cref="T:System.InvalidOperationException">Content is not available; the builder has been linked with another one.</exception>
		// Token: 0x06000290 RID: 656 RVA: 0x00007FC0 File Offset: 0x000061C0
		public byte[] ToArray(int start, int byteCount)
		{
			BlobUtilities.ValidateRange(this.Count, start, byteCount, "byteCount");
			byte[] array = new byte[byteCount];
			int num = 0;
			int num2 = start;
			int num3 = start + byteCount;
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				int num4 = num + blobBuilder.Length;
				if (num4 > num2)
				{
					int num5 = Math.Min(num3, num4) - num2;
					Array.Copy(blobBuilder._buffer, num2 - num, array, num2 - start, num5);
					num2 += num5;
					if (num2 == num3)
					{
						break;
					}
				}
				num = num4;
			}
			return array;
		}

		/// <exception cref="T:System.InvalidOperationException">Content is not available; the builder has been linked with another one.</exception>
		// Token: 0x06000291 RID: 657 RVA: 0x00008074 File Offset: 0x00006274
		[NullableContext(0)]
		public ImmutableArray<byte> ToImmutableArray()
		{
			return this.ToImmutableArray(0, this.Count);
		}

		/// <param name="start" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">The range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the buffer content.</exception>
		/// <exception cref="T:System.InvalidOperationException">Content is not available; the builder has been linked with another one.</exception>
		// Token: 0x06000292 RID: 658 RVA: 0x00008084 File Offset: 0x00006284
		[NullableContext(0)]
		public ImmutableArray<byte> ToImmutableArray(int start, int byteCount)
		{
			byte[] array = this.ToArray(start, byteCount);
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref array);
		}

		/// <param name="destination" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Content is not available, the builder has been linked with another one.</exception>
		// Token: 0x06000293 RID: 659 RVA: 0x000080A4 File Offset: 0x000062A4
		public void WriteContentTo(Stream destination)
		{
			if (destination == null)
			{
				Throw.ArgumentNull("destination");
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				destination.Write(blobBuilder._buffer, 0, blobBuilder.Length);
			}
		}

		/// <param name="destination" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is default (<see cref="T:System.Reflection.Metadata.BlobWriter" />).</exception>
		/// <exception cref="T:System.InvalidOperationException">Content is not available, the builder has been linked with another one.</exception>
		// Token: 0x06000294 RID: 660 RVA: 0x00008114 File Offset: 0x00006314
		public void WriteContentTo(ref BlobWriter destination)
		{
			if (destination.IsDefault)
			{
				Throw.ArgumentNull("destination");
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				destination.WriteBytes(blobBuilder._buffer, 0, blobBuilder.Length);
			}
		}

		/// <param name="destination" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destination" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Content is not available, the builder has been linked with another one.</exception>
		// Token: 0x06000295 RID: 661 RVA: 0x00008188 File Offset: 0x00006388
		public void WriteContentTo(BlobBuilder destination)
		{
			if (destination == null)
			{
				Throw.ArgumentNull("destination");
			}
			foreach (BlobBuilder blobBuilder in this.GetChunks())
			{
				destination.WriteBytes(blobBuilder._buffer, 0, blobBuilder.Length);
			}
		}

		/// <param name="prefix" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="prefix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x06000296 RID: 662 RVA: 0x000081F8 File Offset: 0x000063F8
		public void LinkPrefix(BlobBuilder prefix)
		{
			if (prefix == null)
			{
				Throw.ArgumentNull("prefix");
			}
			if (!prefix.IsHead || !this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (prefix.Count == 0)
			{
				return;
			}
			this.PreviousLength += prefix.Count;
			prefix._length = prefix.FrozenLength;
			BlobBuilder firstChunk = this.FirstChunk;
			BlobBuilder firstChunk2 = prefix.FirstChunk;
			BlobBuilder nextOrPrevious = this._nextOrPrevious;
			BlobBuilder nextOrPrevious2 = prefix._nextOrPrevious;
			this._nextOrPrevious = ((nextOrPrevious != this) ? nextOrPrevious : prefix);
			prefix._nextOrPrevious = ((firstChunk != this) ? firstChunk : ((firstChunk2 != prefix) ? firstChunk2 : prefix));
			if (nextOrPrevious != this)
			{
				nextOrPrevious._nextOrPrevious = ((firstChunk2 != prefix) ? firstChunk2 : prefix);
			}
			if (nextOrPrevious2 != prefix)
			{
				nextOrPrevious2._nextOrPrevious = prefix;
			}
			prefix.CheckInvariants();
			this.CheckInvariants();
		}

		/// <param name="suffix" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="suffix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x06000297 RID: 663 RVA: 0x000082B8 File Offset: 0x000064B8
		public void LinkSuffix(BlobBuilder suffix)
		{
			if (suffix == null)
			{
				throw new ArgumentNullException("suffix");
			}
			if (!this.IsHead || !suffix.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (suffix.Count == 0)
			{
				return;
			}
			bool flag = this.Count == 0;
			byte[] buffer = suffix._buffer;
			uint length = suffix._length;
			int previousLength = suffix.PreviousLength;
			int length2 = suffix.Length;
			suffix._buffer = this._buffer;
			suffix._length = this.FrozenLength;
			this._buffer = buffer;
			this._length = length;
			this.PreviousLength += suffix.Length + previousLength;
			suffix._previousLengthOrFrozenSuffixLengthDelta = previousLength + length2 - suffix.Length;
			if (!flag)
			{
				BlobBuilder firstChunk = this.FirstChunk;
				BlobBuilder firstChunk2 = suffix.FirstChunk;
				BlobBuilder nextOrPrevious = this._nextOrPrevious;
				BlobBuilder nextOrPrevious2 = suffix._nextOrPrevious;
				this._nextOrPrevious = nextOrPrevious2;
				suffix._nextOrPrevious = ((firstChunk2 != suffix) ? firstChunk2 : ((firstChunk != this) ? firstChunk : suffix));
				if (nextOrPrevious != this)
				{
					nextOrPrevious._nextOrPrevious = suffix;
				}
				if (nextOrPrevious2 != suffix)
				{
					nextOrPrevious2._nextOrPrevious = ((firstChunk != this) ? firstChunk : suffix);
				}
			}
			this.CheckInvariants();
			suffix.CheckInvariants();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000083D7 File Offset: 0x000065D7
		private void AddLength(int value)
		{
			this._length += (uint)value;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000083E8 File Offset: 0x000065E8
		[MethodImpl(8)]
		private void Expand(int newLength)
		{
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			BlobBuilder blobBuilder = this.AllocateChunk(Math.Max(newLength, 16));
			if (blobBuilder.ChunkCapacity < newLength)
			{
				throw new InvalidOperationException(SR.Format(SR.ReturnedBuilderSizeTooSmall, base.GetType(), "AllocateChunk"));
			}
			byte[] buffer = blobBuilder._buffer;
			if (this._length == 0U)
			{
				blobBuilder._buffer = this._buffer;
				this._buffer = buffer;
			}
			else
			{
				BlobBuilder nextOrPrevious = this._nextOrPrevious;
				BlobBuilder firstChunk = this.FirstChunk;
				if (nextOrPrevious == this)
				{
					this._nextOrPrevious = blobBuilder;
				}
				else
				{
					blobBuilder._nextOrPrevious = firstChunk;
					nextOrPrevious._nextOrPrevious = blobBuilder;
					this._nextOrPrevious = blobBuilder;
				}
				blobBuilder._buffer = this._buffer;
				blobBuilder._length = this.FrozenLength;
				blobBuilder._previousLengthOrFrozenSuffixLengthDelta = this.PreviousLength;
				this._buffer = buffer;
				this.PreviousLength += this.Length;
				this._length = 0U;
			}
			this.CheckInvariants();
		}

		/// <summary>Reserves a contiguous block of bytes.</summary>
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x0600029A RID: 666 RVA: 0x000084D4 File Offset: 0x000066D4
		public Blob ReserveBytes(int byteCount)
		{
			if (byteCount < 0)
			{
				Throw.ArgumentOutOfRange("byteCount");
			}
			int num = this.ReserveBytesImpl(byteCount);
			return new Blob(this._buffer, num, byteCount);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00008504 File Offset: 0x00006704
		private int ReserveBytesImpl(int byteCount)
		{
			uint num = this._length;
			if ((ulong)num > (ulong)((long)(this._buffer.Length - byteCount)))
			{
				this.Expand(byteCount);
				num = 0U;
			}
			this._length = num + (uint)byteCount;
			return (int)num;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000853A File Offset: 0x0000673A
		private int ReserveBytesPrimitive(int byteCount)
		{
			return this.ReserveBytesImpl(byteCount);
		}

		/// <summary>Writes a specified number of occurrences of a byte value to the builder.</summary>
		/// <param name="value" />
		/// <param name="byteCount">The number of occurences of <paramref name="value" /> to write.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable, it has been linked with another one.</exception>
		// Token: 0x0600029D RID: 669 RVA: 0x00008544 File Offset: 0x00006744
		public void WriteBytes(byte value, int byteCount)
		{
			if (byteCount < 0)
			{
				Throw.ArgumentOutOfRange("byteCount");
			}
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			int num = Math.Min(this.FreeBytes, byteCount);
			this._buffer.WriteBytes(this.Length, value, num);
			this.AddLength(num);
			int num2 = byteCount - num;
			if (num2 > 0)
			{
				this.Expand(num2);
				this._buffer.WriteBytes(0, value, num2);
				this.AddLength(num2);
			}
		}

		/// <summary>Writes a specified number of bytes from a buffer to the builder.</summary>
		/// <param name="buffer" />
		/// <param name="byteCount">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable, it has been linked with another one.</exception>
		// Token: 0x0600029E RID: 670 RVA: 0x000085B7 File Offset: 0x000067B7
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
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			this.WriteBytesUnchecked(buffer, byteCount);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000085EC File Offset: 0x000067EC
		private unsafe void WriteBytesUnchecked(byte* buffer, int byteCount)
		{
			int num = Math.Min(this.FreeBytes, byteCount);
			Marshal.Copy((IntPtr)((void*)buffer), this._buffer, this.Length, num);
			this.AddLength(num);
			int num2 = byteCount - num;
			if (num2 > 0)
			{
				this.Expand(num2);
				Marshal.Copy((IntPtr)((void*)(buffer + num)), this._buffer, 0, num2);
				this.AddLength(num2);
			}
		}

		/// <summary>Attempts to write a sequence of bytes to the builder. A return value indicates the number of bytes successfully written.</summary>
		/// <param name="source" />
		/// <param name="byteCount" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="byteCount" /> is negative.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		/// <returns>The number of bytes successfully written from <paramref name="source" />.</returns>
		// Token: 0x060002A0 RID: 672 RVA: 0x00008650 File Offset: 0x00006850
		public int TryWriteBytes(Stream source, int byteCount)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount");
			}
			if (byteCount == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = Math.Min(this.FreeBytes, byteCount);
			if (num2 > 0)
			{
				num = source.TryReadAll(this._buffer, this.Length, num2);
				this.AddLength(num);
				if (num != num2)
				{
					return num;
				}
			}
			int num3 = byteCount - num2;
			if (num3 > 0)
			{
				this.Expand(num3);
				num = source.TryReadAll(this._buffer, 0, num3);
				this.AddLength(num);
				num += num2;
			}
			return num;
		}

		/// <summary>Writes the contents of an immutable byte array to the builder.</summary>
		/// <param name="buffer">The array to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A1 RID: 673 RVA: 0x000086DD File Offset: 0x000068DD
		[NullableContext(0)]
		public void WriteBytes(ImmutableArray<byte> buffer)
		{
			this.WriteBytes(buffer, 0, buffer.IsDefault ? 0 : buffer.Length);
		}

		/// <summary>Writes a specified number of bytes starting at a specified index of an immutable array to the builder.</summary>
		/// <param name="buffer" />
		/// <param name="start" />
		/// <param name="byteCount">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of the <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A2 RID: 674 RVA: 0x000086FA File Offset: 0x000068FA
		[NullableContext(0)]
		public void WriteBytes(ImmutableArray<byte> buffer, int start, int byteCount)
		{
			this.WriteBytes(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(buffer), start, byteCount);
		}

		/// <summary>Writes the contents of a byte array to the builder.</summary>
		/// <param name="buffer">The byte array to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A3 RID: 675 RVA: 0x0000870A File Offset: 0x0000690A
		public void WriteBytes(byte[] buffer)
		{
			this.WriteBytes(buffer, 0, (buffer != null) ? buffer.Length : 0);
		}

		/// <summary>Writes a specified number of bytes starting at a specified index in a byte array to the builder.</summary>
		/// <param name="buffer" />
		/// <param name="start" />
		/// <param name="byteCount">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The range specified by <paramref name="start" /> and <paramref name="byteCount" /> falls outside of the bounds of <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A4 RID: 676 RVA: 0x00008720 File Offset: 0x00006920
		public unsafe void WriteBytes(byte[] buffer, int start, int byteCount)
		{
			if (buffer == null)
			{
				Throw.ArgumentNull("buffer");
			}
			BlobUtilities.ValidateRange(buffer.Length, start, byteCount, "byteCount");
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (buffer.Length == 0)
			{
				return;
			}
			fixed (byte* ptr = &buffer[0])
			{
				byte* ptr2 = ptr;
				this.WriteBytesUnchecked(ptr2 + start, byteCount);
			}
		}

		/// <param name="position" />
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A5 RID: 677 RVA: 0x00008773 File Offset: 0x00006973
		public void PadTo(int position)
		{
			this.WriteBytes(0, position - this.Count);
		}

		/// <param name="alignment" />
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A6 RID: 678 RVA: 0x00008784 File Offset: 0x00006984
		public void Align(int alignment)
		{
			int count = this.Count;
			this.WriteBytes(0, BitArithmetic.Align(count, alignment) - count);
		}

		/// <summary>Writes a <see cref="T:System.Boolean" /> value to the builder.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A7 RID: 679 RVA: 0x000087A8 File Offset: 0x000069A8
		public void WriteBoolean(bool value)
		{
			this.WriteByte(value ? 1 : 0);
		}

		/// <summary>Writes a <see cref="T:System.Byte" /> value to the builder.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002A8 RID: 680 RVA: 0x000087B8 File Offset: 0x000069B8
		public void WriteByte(byte value)
		{
			int num = this.ReserveBytesPrimitive(1);
			this._buffer.WriteByte(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002A9 RID: 681 RVA: 0x000087DA File Offset: 0x000069DA
		public void WriteSByte(sbyte value)
		{
			this.WriteByte((byte)value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002AA RID: 682 RVA: 0x000087E4 File Offset: 0x000069E4
		public void WriteDouble(double value)
		{
			int num = this.ReserveBytesPrimitive(8);
			this._buffer.WriteDouble(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002AB RID: 683 RVA: 0x00008808 File Offset: 0x00006A08
		public void WriteSingle(float value)
		{
			int num = this.ReserveBytesPrimitive(4);
			this._buffer.WriteSingle(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002AC RID: 684 RVA: 0x0000882A File Offset: 0x00006A2A
		public void WriteInt16(short value)
		{
			this.WriteUInt16((ushort)value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002AD RID: 685 RVA: 0x00008834 File Offset: 0x00006A34
		public void WriteUInt16(ushort value)
		{
			int num = this.ReserveBytesPrimitive(2);
			this._buffer.WriteUInt16(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002AE RID: 686 RVA: 0x00008856 File Offset: 0x00006A56
		public void WriteInt16BE(short value)
		{
			this.WriteUInt16BE((ushort)value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002AF RID: 687 RVA: 0x00008860 File Offset: 0x00006A60
		public void WriteUInt16BE(ushort value)
		{
			int num = this.ReserveBytesPrimitive(2);
			this._buffer.WriteUInt16BE(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B0 RID: 688 RVA: 0x00008882 File Offset: 0x00006A82
		public void WriteInt32BE(int value)
		{
			this.WriteUInt32BE((uint)value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B1 RID: 689 RVA: 0x0000888C File Offset: 0x00006A8C
		public void WriteUInt32BE(uint value)
		{
			int num = this.ReserveBytesPrimitive(4);
			this._buffer.WriteUInt32BE(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B2 RID: 690 RVA: 0x000088AE File Offset: 0x00006AAE
		public void WriteInt32(int value)
		{
			this.WriteUInt32((uint)value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B3 RID: 691 RVA: 0x000088B8 File Offset: 0x00006AB8
		public void WriteUInt32(uint value)
		{
			int num = this.ReserveBytesPrimitive(4);
			this._buffer.WriteUInt32(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B4 RID: 692 RVA: 0x000088DA File Offset: 0x00006ADA
		public void WriteInt64(long value)
		{
			this.WriteUInt64((ulong)value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B5 RID: 693 RVA: 0x000088E4 File Offset: 0x00006AE4
		public void WriteUInt64(ulong value)
		{
			int num = this.ReserveBytesPrimitive(8);
			this._buffer.WriteUInt64(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B6 RID: 694 RVA: 0x00008908 File Offset: 0x00006B08
		public void WriteDecimal(decimal value)
		{
			int num = this.ReserveBytesPrimitive(13);
			this._buffer.WriteDecimal(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000892C File Offset: 0x00006B2C
		public void WriteGuid(Guid value)
		{
			int num = this.ReserveBytesPrimitive(16);
			this._buffer.WriteGuid(num, value);
		}

		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B8 RID: 696 RVA: 0x0000894F File Offset: 0x00006B4F
		public void WriteDateTime(DateTime value)
		{
			this.WriteInt64(value.Ticks);
		}

		/// <summary>Writes a reference to a heap (heap offset) or a table (row number).</summary>
		/// <param name="reference">Heap offset or table row number.</param>
		/// <param name="isSmall">
		///   <see langword="true" /> to encode the reference as a 16-bit integer; <see langword="false" /> to encode it as a 32-bit integer.</param>
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000895E File Offset: 0x00006B5E
		public void WriteReference(int reference, bool isSmall)
		{
			if (isSmall)
			{
				this.WriteUInt16((ushort)reference);
				return;
			}
			this.WriteInt32(reference);
		}

		/// <summary>Writes a UTF16 (little-endian) encoded character array at the current position.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002BA RID: 698 RVA: 0x00008974 File Offset: 0x00006B74
		public unsafe void WriteUTF16(char[] value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (value.Length == 0)
			{
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				fixed (char* ptr = &value[0])
				{
					char* ptr2 = ptr;
					this.WriteBytesUnchecked((byte*)ptr2, value.Length * 2);
				}
				return;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(value);
			fixed (byte* ptr3 = &bytes[0])
			{
				byte* ptr4 = ptr3;
				this.WriteBytesUnchecked(ptr4, bytes.Length);
			}
		}

		/// <summary>Writes UTF16 (little-endian) encoded string at the current position.</summary>
		/// <param name="value" />
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002BB RID: 699 RVA: 0x000089EC File Offset: 0x00006BEC
		public unsafe void WriteUTF16(string value)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			if (BitConverter.IsLittleEndian)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.WriteBytesUnchecked((byte*)ptr, value.Length * 2);
				}
				return;
			}
			byte[] bytes = Encoding.Unicode.GetBytes(value);
			byte[] array;
			byte* ptr2;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			this.WriteBytesUnchecked(ptr2, bytes.Length);
			array = null;
		}

		/// <summary>Writes a string in SerString format (see ECMA-335-II 23.3 Custom attributes).</summary>
		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002BC RID: 700 RVA: 0x00008A70 File Offset: 0x00006C70
		[NullableContext(2)]
		public void WriteSerializedString(string value)
		{
			if (value == null)
			{
				this.WriteByte(byte.MaxValue);
				return;
			}
			this.WriteUTF8(value, 0, value.Length, true, true);
		}

		/// <summary>Writes a string in User String (#US) heap format (see ECMA-335-II 24.2.4 #US and #Blob heaps).</summary>
		/// <param name="value" />
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002BD RID: 701 RVA: 0x00008A91 File Offset: 0x00006C91
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
		/// <param name="value">Constant value.</param>
		/// <param name="allowUnpairedSurrogates">
		///   <see langword="true" /> to encode unpaired surrogates as specified; <see langword="false" /> to replace them with a U+FFFD character.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">Builder is not writable, it has been linked with another one.</exception>
		// Token: 0x060002BE RID: 702 RVA: 0x00008AC5 File Offset: 0x00006CC5
		public void WriteUTF8(string value, bool allowUnpairedSurrogates = true)
		{
			if (value == null)
			{
				Throw.ArgumentNull("value");
			}
			this.WriteUTF8(value, 0, value.Length, allowUnpairedSurrogates, false);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008AE4 File Offset: 0x00006CE4
		internal unsafe void WriteUTF8(string str, int start, int length, bool allowUnpairedSurrogates, bool prependSize)
		{
			if (!this.IsHead)
			{
				Throw.InvalidOperationBuilderAlreadyLinked();
			}
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + start;
				int num = this.FreeBytes - (prependSize ? 4 : 0);
				char* ptr3;
				int utf8ByteCount = BlobUtilities.GetUTF8ByteCount(ptr2, length, num, out ptr3);
				int num2 = (int)((long)(ptr3 - ptr2));
				int num3 = length - num2;
				int utf8ByteCount2 = BlobUtilities.GetUTF8ByteCount(ptr3, num3);
				if (prependSize)
				{
					this.WriteCompressedInteger(utf8ByteCount + utf8ByteCount2);
				}
				this._buffer.WriteUTF8(this.Length, ptr2, num2, utf8ByteCount, allowUnpairedSurrogates);
				this.AddLength(utf8ByteCount);
				if (utf8ByteCount2 > 0)
				{
					this.Expand(utf8ByteCount2);
					this._buffer.WriteUTF8(0, ptr3, num3, utf8ByteCount2, allowUnpairedSurrogates);
					this.AddLength(utf8ByteCount2);
				}
			}
		}

		/// <summary>Implements compressed signed integer encoding as defined by ECMA-335-II chapter 23.2: Blobs and signatures.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> can't be represented as a compressed signed integer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002C0 RID: 704 RVA: 0x00008BA5 File Offset: 0x00006DA5
		public void WriteCompressedSignedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedSignedInteger(this, value);
		}

		/// <summary>Implements compressed unsigned integer encoding as defined by ECMA-335-II chapter 23.2: Blobs and signatures.</summary>
		/// <param name="value">The value to write.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> can't be represented as a compressed unsigned integer.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002C1 RID: 705 RVA: 0x00008BAE File Offset: 0x00006DAE
		public void WriteCompressedInteger(int value)
		{
			BlobWriterImpl.WriteCompressedInteger(this, (uint)value);
		}

		/// <summary>Writes a constant value (see ECMA-335 Partition II section 22.9) at the current position.</summary>
		/// <param name="value">The constant value to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of a constant type.</exception>
		/// <exception cref="T:System.InvalidOperationException">The builder is not writable; it has been linked with another one.</exception>
		// Token: 0x060002C2 RID: 706 RVA: 0x00008BB7 File Offset: 0x00006DB7
		[NullableContext(2)]
		public void WriteConstant(object value)
		{
			BlobWriterImpl.WriteConstant(this, value);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008BC0 File Offset: 0x00006DC0
		internal string GetDebuggerDisplay()
		{
			if (!this.IsHead)
			{
				return "<" + BlobBuilder.Display(this._buffer, this.Length) + ">";
			}
			return string.Join("->", from chunk in this.GetChunks()
				select "[" + BlobBuilder.Display(chunk._buffer, chunk.Length) + "]");
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008C2F File Offset: 0x00006E2F
		private static string Display(byte[] bytes, int length)
		{
			if (length > 64)
			{
				return BitConverter.ToString(bytes, 0, 32) + "-...-" + BitConverter.ToString(bytes, length - 32, 32);
			}
			return BitConverter.ToString(bytes, 0, length);
		}

		// Token: 0x040001AB RID: 427
		internal const int DefaultChunkSize = 256;

		// Token: 0x040001AC RID: 428
		internal const int MinChunkSize = 16;

		// Token: 0x040001AD RID: 429
		private BlobBuilder _nextOrPrevious;

		// Token: 0x040001AE RID: 430
		private int _previousLengthOrFrozenSuffixLengthDelta;

		// Token: 0x040001AF RID: 431
		private byte[] _buffer;

		// Token: 0x040001B0 RID: 432
		private uint _length;

		// Token: 0x040001B1 RID: 433
		private const uint IsFrozenMask = 2147483648U;

		// Token: 0x02000194 RID: 404
		[NullableContext(0)]
		internal struct Chunks : IEnumerable<BlobBuilder>, IEnumerable, IEnumerator<BlobBuilder>, IEnumerator, IDisposable
		{
			// Token: 0x06000C35 RID: 3125 RVA: 0x00024F15 File Offset: 0x00023115
			[NullableContext(1)]
			internal Chunks(BlobBuilder builder)
			{
				this._head = builder;
				this._next = builder.FirstChunk;
				this._currentOpt = null;
			}

			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00024F31 File Offset: 0x00023131
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17000314 RID: 788
			// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00024F39 File Offset: 0x00023139
			[Nullable(1)]
			public BlobBuilder Current
			{
				[NullableContext(1)]
				get
				{
					return this._currentOpt;
				}
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x00024F44 File Offset: 0x00023144
			public bool MoveNext()
			{
				if (this._currentOpt == this._head)
				{
					return false;
				}
				if (this._currentOpt == this._head._nextOrPrevious)
				{
					this._currentOpt = this._head;
					return true;
				}
				this._currentOpt = this._next;
				this._next = this._next._nextOrPrevious;
				return true;
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x00024FA0 File Offset: 0x000231A0
			public void Reset()
			{
				this._currentOpt = null;
				this._next = this._head.FirstChunk;
			}

			// Token: 0x06000C3A RID: 3130 RVA: 0x00024FBA File Offset: 0x000231BA
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000C3B RID: 3131 RVA: 0x00024FBC File Offset: 0x000231BC
			public BlobBuilder.Chunks GetEnumerator()
			{
				return this;
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x00024FC4 File Offset: 0x000231C4
			IEnumerator<BlobBuilder> IEnumerable<BlobBuilder>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x00024FD1 File Offset: 0x000231D1
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040009CA RID: 2506
			private readonly BlobBuilder _head;

			// Token: 0x040009CB RID: 2507
			private BlobBuilder _next;

			// Token: 0x040009CC RID: 2508
			private BlobBuilder _currentOpt;
		}

		// Token: 0x02000195 RID: 405
		[NullableContext(0)]
		public struct Blobs : IEnumerable<Blob>, IEnumerable, IEnumerator<Blob>, IEnumerator, IDisposable
		{
			// Token: 0x06000C3E RID: 3134 RVA: 0x00024FDE File Offset: 0x000231DE
			[NullableContext(1)]
			internal Blobs(BlobBuilder builder)
			{
				this._chunks = new BlobBuilder.Chunks(builder);
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06000C3F RID: 3135 RVA: 0x00024FEC File Offset: 0x000231EC
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			/// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			// Token: 0x17000316 RID: 790
			// (get) Token: 0x06000C40 RID: 3136 RVA: 0x00024FFC File Offset: 0x000231FC
			public Blob Current
			{
				get
				{
					BlobBuilder blobBuilder = this._chunks.Current;
					if (blobBuilder != null)
					{
						return new Blob(blobBuilder._buffer, 0, blobBuilder.Length);
					}
					return default(Blob);
				}
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			// Token: 0x06000C41 RID: 3137 RVA: 0x00025034 File Offset: 0x00023234
			public bool MoveNext()
			{
				return this._chunks.MoveNext();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			// Token: 0x06000C42 RID: 3138 RVA: 0x00025041 File Offset: 0x00023241
			public void Reset()
			{
				this._chunks.Reset();
			}

			/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
			// Token: 0x06000C43 RID: 3139 RVA: 0x0002504E File Offset: 0x0002324E
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000C44 RID: 3140 RVA: 0x00025050 File Offset: 0x00023250
			public BlobBuilder.Blobs GetEnumerator()
			{
				return this;
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06000C45 RID: 3141 RVA: 0x00025058 File Offset: 0x00023258
			IEnumerator<Blob> IEnumerable<Blob>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
			// Token: 0x06000C46 RID: 3142 RVA: 0x00025065 File Offset: 0x00023265
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040009CD RID: 2509
			private BlobBuilder.Chunks _chunks;
		}
	}
}
