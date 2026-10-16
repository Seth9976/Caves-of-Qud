using System;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a handle and a corresponding blob on a metadata heap that was reserved for future content update.</summary>
	/// <typeparam name="THandle" />
	// Token: 0x02000044 RID: 68
	public readonly struct ReservedBlob<THandle> where THandle : struct
	{
		/// <summary>Gets the reserved blob handle.</summary>
		/// <returns>The reserved bloc handle.</returns>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008CA1 File Offset: 0x00006EA1
		public THandle Handle { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008CA9 File Offset: 0x00006EA9
		public Blob Content { get; }

		// Token: 0x060002CA RID: 714 RVA: 0x00008CB1 File Offset: 0x00006EB1
		internal ReservedBlob(THandle handle, Blob content)
		{
			this.Handle = handle;
			this.Content = content;
		}

		/// <summary>Returns a <see cref="T:System.Reflection.Metadata.BlobWriter" /> to be used to update the content.</summary>
		/// <returns>A blob writer to be used to update the content.</returns>
		// Token: 0x060002CB RID: 715 RVA: 0x00008CC1 File Offset: 0x00006EC1
		public BlobWriter CreateWriter()
		{
			return new BlobWriter(this.Content);
		}
	}
}
