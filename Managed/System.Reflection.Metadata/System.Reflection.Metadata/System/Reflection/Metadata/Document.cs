using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>The source document in the debug metadata.</summary>
	// Token: 0x020000B5 RID: 181
	public readonly struct Document
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00011603 File Offset: 0x0000F803
		[NullableContext(1)]
		internal Document(MetadataReader reader, DocumentHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00011619 File Offset: 0x0000F819
		private DocumentHandle Handle
		{
			get
			{
				return DocumentHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets the document name blob.</summary>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00011626 File Offset: 0x0000F826
		public DocumentNameBlobHandle Name
		{
			get
			{
				return this._reader.DocumentTable.GetName(this.Handle);
			}
		}

		/// <summary>Gets the source code language (C#, VB, F#, etc.).</summary>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001163E File Offset: 0x0000F83E
		public GuidHandle Language
		{
			get
			{
				return this._reader.DocumentTable.GetLanguage(this.Handle);
			}
		}

		/// <summary>Gets the hash algorithm used to calculate the <see cref="P:System.Reflection.Metadata.Document.Hash" /> (SHA1, SHA256, etc.).</summary>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00011656 File Offset: 0x0000F856
		public GuidHandle HashAlgorithm
		{
			get
			{
				return this._reader.DocumentTable.GetHashAlgorithm(this.Handle);
			}
		}

		/// <summary>Gets the document content hash.</summary>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001166E File Offset: 0x0000F86E
		public BlobHandle Hash
		{
			get
			{
				return this._reader.DocumentTable.GetHash(this.Handle);
			}
		}

		// Token: 0x04000441 RID: 1089
		private readonly MetadataReader _reader;

		// Token: 0x04000442 RID: 1090
		private readonly int _rowId;
	}
}
