using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004F RID: 79
	public readonly struct BlobContentId : IEquatable<BlobContentId>
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000923F File Offset: 0x0000743F
		public Guid Guid { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00009247 File Offset: 0x00007447
		public uint Stamp { get; }

		/// <param name="guid" />
		/// <param name="stamp" />
		// Token: 0x060002F2 RID: 754 RVA: 0x0000924F File Offset: 0x0000744F
		public BlobContentId(Guid guid, uint stamp)
		{
			this.Guid = guid;
			this.Stamp = stamp;
		}

		/// <param name="id" />
		// Token: 0x060002F3 RID: 755 RVA: 0x0000925F File Offset: 0x0000745F
		public BlobContentId(ImmutableArray<byte> id)
		{
			this = new BlobContentId(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(id));
		}

		/// <param name="id" />
		// Token: 0x060002F4 RID: 756 RVA: 0x00009270 File Offset: 0x00007470
		[NullableContext(1)]
		public unsafe BlobContentId(byte[] id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length != 20)
			{
				throw new ArgumentException(SR.Format(SR.UnexpectedArrayLength, 20), "id");
			}
			fixed (byte* ptr = &id[0])
			{
				byte* ptr2 = ptr;
				BlobReader blobReader = new BlobReader(ptr2, id.Length);
				this.Guid = blobReader.ReadGuid();
				this.Stamp = blobReader.ReadUInt32();
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000092E4 File Offset: 0x000074E4
		public bool IsDefault
		{
			get
			{
				return this.Guid == default(Guid) && this.Stamp == 0U;
			}
		}

		/// <param name="hashCode" />
		// Token: 0x060002F6 RID: 758 RVA: 0x00009312 File Offset: 0x00007512
		public static BlobContentId FromHash(ImmutableArray<byte> hashCode)
		{
			return BlobContentId.FromHash(ImmutableByteArrayInterop.DangerousGetUnderlyingArray(hashCode));
		}

		/// <param name="hashCode" />
		// Token: 0x060002F7 RID: 759 RVA: 0x00009320 File Offset: 0x00007520
		[NullableContext(1)]
		public unsafe static BlobContentId FromHash(byte[] hashCode)
		{
			if (hashCode == null)
			{
				throw new ArgumentNullException("hashCode");
			}
			if (hashCode.Length < 20)
			{
				throw new ArgumentException(SR.Format(SR.HashTooShort, 20), "hashCode");
			}
			Guid guid = default(Guid);
			byte* ptr = (byte*)(&guid);
			for (int i = 0; i < 16; i++)
			{
				ptr[i] = hashCode[i];
			}
			ptr[7] = (ptr[7] & 15) | 64;
			ptr[8] = (ptr[8] & 63) | 128;
			uint num = (uint)(int.MinValue | (((int)hashCode[19] << 24) | ((int)hashCode[18] << 16) | ((int)hashCode[17] << 8) | (int)hashCode[16]));
			return new BlobContentId(guid, num);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000093C8 File Offset: 0x000075C8
		[NullableContext(1)]
		public static Func<IEnumerable<Blob>, BlobContentId> GetTimeBasedProvider()
		{
			uint timestamp = (uint)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
			return (IEnumerable<Blob> content) => new BlobContentId(Guid.NewGuid(), timestamp);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060002F9 RID: 761 RVA: 0x0000940C File Offset: 0x0000760C
		public bool Equals(BlobContentId other)
		{
			return this.Guid == other.Guid && this.Stamp == other.Stamp;
		}

		/// <param name="obj" />
		// Token: 0x060002FA RID: 762 RVA: 0x00009434 File Offset: 0x00007634
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is BlobContentId)
			{
				BlobContentId blobContentId = (BlobContentId)obj;
				return this.Equals(blobContentId);
			}
			return false;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000945C File Offset: 0x0000765C
		public override int GetHashCode()
		{
			return Hash.Combine(this.Stamp, this.Guid.GetHashCode());
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060002FC RID: 764 RVA: 0x00009488 File Offset: 0x00007688
		public static bool operator ==(BlobContentId left, BlobContentId right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060002FD RID: 765 RVA: 0x00009492 File Offset: 0x00007692
		public static bool operator !=(BlobContentId left, BlobContentId right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040002A7 RID: 679
		private const int Size = 20;
	}
}
