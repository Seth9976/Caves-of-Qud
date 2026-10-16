using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents a block of memory in the Portable Executable (PE) image.</summary>
	// Token: 0x02000037 RID: 55
	public readonly struct PEMemoryBlock
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00005E89 File Offset: 0x00004089
		[NullableContext(1)]
		internal PEMemoryBlock(AbstractMemoryBlock block, int offset = 0)
		{
			this._block = block;
			this._offset = offset;
		}

		/// <summary>Gets a pointer to the first byte of the block.</summary>
		/// <returns>A pointer to the first byte of the block.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005E99 File Offset: 0x00004099
		public unsafe byte* Pointer
		{
			get
			{
				if (this._block == null)
				{
					return null;
				}
				return this._block.Pointer + this._offset;
			}
		}

		/// <summary>Gets the length of the block.</summary>
		/// <returns>The length of the block.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00005EB8 File Offset: 0x000040B8
		public int Length
		{
			get
			{
				AbstractMemoryBlock block = this._block;
				return (((block != null) ? new int?(block.Size) : default(int?)) - this._offset).GetValueOrDefault();
			}
		}

		/// <summary>Creates a <see cref="T:System.Reflection.Metadata.BlobReader" /> for a blob spanning the entire block.</summary>
		/// <returns>A reader for a blob spanning the entire block.</returns>
		// Token: 0x060001EA RID: 490 RVA: 0x00005F16 File Offset: 0x00004116
		public BlobReader GetReader()
		{
			return new BlobReader(this.Pointer, this.Length);
		}

		/// <summary>Creates a <see cref="T:System.Reflection.Metadata.BlobReader" /> for a blob spanning a part of the block.</summary>
		/// <param name="start">The starting position in the block.</param>
		/// <param name="length">The number of bytes in the portion of the block.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified range is not contained within the block.</exception>
		/// <returns>A reader for a blob spanning a portion of the block.</returns>
		// Token: 0x060001EB RID: 491 RVA: 0x00005F29 File Offset: 0x00004129
		public BlobReader GetReader(int start, int length)
		{
			BlobUtilities.ValidateRange(this.Length, start, length, "length");
			return new BlobReader(this.Pointer + start, length);
		}

		/// <summary>Reads the contents of the entire block into an array.</summary>
		/// <returns>An immutable byte array.</returns>
		// Token: 0x060001EC RID: 492 RVA: 0x00005F4B File Offset: 0x0000414B
		public ImmutableArray<byte> GetContent()
		{
			AbstractMemoryBlock block = this._block;
			if (block == null)
			{
				return ImmutableArray<byte>.Empty;
			}
			return block.GetContentUnchecked(this._offset, this.Length);
		}

		/// <summary>Reads the contents of a part of the block into an array.</summary>
		/// <param name="start">The starting position in the block.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified range is not contained within the block.</exception>
		/// <returns>An immutable array of bytes.</returns>
		// Token: 0x060001ED RID: 493 RVA: 0x00005F6E File Offset: 0x0000416E
		public ImmutableArray<byte> GetContent(int start, int length)
		{
			BlobUtilities.ValidateRange(this.Length, start, length, "length");
			AbstractMemoryBlock block = this._block;
			if (block == null)
			{
				return ImmutableArray<byte>.Empty;
			}
			return block.GetContentUnchecked(this._offset + start, length);
		}

		// Token: 0x0400017B RID: 379
		private readonly AbstractMemoryBlock _block;

		// Token: 0x0400017C RID: 380
		private readonly int _offset;
	}
}
