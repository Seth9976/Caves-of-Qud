using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A1 RID: 161
	public readonly struct ManifestResource
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x0000D314 File Offset: 0x0000B514
		[NullableContext(1)]
		internal ManifestResource(MetadataReader reader, ManifestResourceHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000D32A File Offset: 0x0000B52A
		private ManifestResourceHandle Handle
		{
			get
			{
				return ManifestResourceHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets the byte offset within the referenced file at which this resource record begins.</summary>
		/// <returns>The byte offset within the referenced file at which this resource record begins.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000D337 File Offset: 0x0000B537
		public long Offset
		{
			get
			{
				return (long)((ulong)this._reader.ManifestResourceTable.GetOffset(this.Handle));
			}
		}

		/// <summary>Gets the manifest resource attributes.</summary>
		/// <returns>A bitwise combination of the enumeration values that specify the manifest resource attributes.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000D350 File Offset: 0x0000B550
		public ManifestResourceAttributes Attributes
		{
			get
			{
				return this._reader.ManifestResourceTable.GetFlags(this.Handle);
			}
		}

		/// <summary>Gets the resource name.</summary>
		/// <returns>The resource name.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000D368 File Offset: 0x0000B568
		public StringHandle Name
		{
			get
			{
				return this._reader.ManifestResourceTable.GetName(this.Handle);
			}
		}

		/// <summary>Gets the implementation entity handle.</summary>
		/// <returns>An EntityHandle instance. If the <see cref="P:System.Reflection.Metadata.EntityHandle.IsNil" /> property is <see langword="true" />, the returned handle will have default values.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000D380 File Offset: 0x0000B580
		public EntityHandle Implementation
		{
			get
			{
				return this._reader.ManifestResourceTable.GetImplementation(this.Handle);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000D398 File Offset: 0x0000B598
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040003BB RID: 955
		private readonly MetadataReader _reader;

		// Token: 0x040003BC RID: 956
		private readonly int _rowId;
	}
}
