using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>A <see cref="T:System.Reflection.Metadata.BlobHandle" /> representing a blob on #Blob heap in Portable PDB structured as Document Name.</summary>
	// Token: 0x020000B6 RID: 182
	public readonly struct DocumentNameBlobHandle : IEquatable<DocumentNameBlobHandle>
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x00011686 File Offset: 0x0000F886
		private DocumentNameBlobHandle(int heapOffset)
		{
			this._heapOffset = heapOffset;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001168F File Offset: 0x0000F88F
		internal static DocumentNameBlobHandle FromOffset(int heapOffset)
		{
			return new DocumentNameBlobHandle(heapOffset);
		}

		/// <param name="handle" />
		// Token: 0x060006ED RID: 1773 RVA: 0x00011697 File Offset: 0x0000F897
		public static implicit operator BlobHandle(DocumentNameBlobHandle handle)
		{
			return BlobHandle.FromOffset(handle._heapOffset);
		}

		/// <param name="handle" />
		// Token: 0x060006EE RID: 1774 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public static explicit operator DocumentNameBlobHandle(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				Throw.InvalidCast();
			}
			return DocumentNameBlobHandle.FromOffset(handle.GetHeapOffset());
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000116C0 File Offset: 0x0000F8C0
		public bool IsNil
		{
			get
			{
				return this._heapOffset == 0;
			}
		}

		/// <param name="obj" />
		// Token: 0x060006F0 RID: 1776 RVA: 0x000116CB File Offset: 0x0000F8CB
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is DocumentNameBlobHandle && this.Equals((DocumentNameBlobHandle)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060006F1 RID: 1777 RVA: 0x000116E3 File Offset: 0x0000F8E3
		public bool Equals(DocumentNameBlobHandle other)
		{
			return this._heapOffset == other._heapOffset;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000116F3 File Offset: 0x0000F8F3
		public override int GetHashCode()
		{
			return this._heapOffset;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060006F3 RID: 1779 RVA: 0x000116FB File Offset: 0x0000F8FB
		public static bool operator ==(DocumentNameBlobHandle left, DocumentNameBlobHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060006F4 RID: 1780 RVA: 0x00011705 File Offset: 0x0000F905
		public static bool operator !=(DocumentNameBlobHandle left, DocumentNameBlobHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000443 RID: 1091
		private readonly int _heapOffset;
	}
}
