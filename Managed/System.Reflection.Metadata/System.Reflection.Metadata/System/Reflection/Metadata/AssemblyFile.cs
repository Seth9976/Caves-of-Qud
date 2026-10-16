using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000072 RID: 114
	public readonly struct AssemblyFile
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000B5CA File Offset: 0x000097CA
		[NullableContext(1)]
		internal AssemblyFile(MetadataReader reader, AssemblyFileHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000B5E0 File Offset: 0x000097E0
		private AssemblyFileHandle Handle
		{
			get
			{
				return AssemblyFileHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets a value that indicates whether the file contains metadata.</summary>
		/// <returns>
		///   <see langword="true" /> if the file contains metadata, <see langword="false" /> otherwise.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000B5ED File Offset: 0x000097ED
		public bool ContainsMetadata
		{
			get
			{
				return this._reader.FileTable.GetFlags(this.Handle) == 0U;
			}
		}

		/// <summary>Gets the file name, including its extension.</summary>
		/// <returns>A <see cref="T:System.Reflection.Metadata.StringHandle" /> instance representing the file name with its extension.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000B608 File Offset: 0x00009808
		public StringHandle Name
		{
			get
			{
				return this._reader.FileTable.GetName(this.Handle);
			}
		}

		/// <summary>Gets the hash value of the file content calculated using <see cref="P:System.Reflection.Metadata.AssemblyDefinition.HashAlgorithm" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.Metadata.BlobHandle" /> instance representing the hash value of the file content.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000B620 File Offset: 0x00009820
		public BlobHandle HashValue
		{
			get
			{
				return this._reader.FileTable.GetHashValue(this.Handle);
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000B638 File Offset: 0x00009838
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0400030D RID: 781
		private readonly MetadataReader _reader;

		// Token: 0x0400030E RID: 782
		private readonly int _rowId;
	}
}
