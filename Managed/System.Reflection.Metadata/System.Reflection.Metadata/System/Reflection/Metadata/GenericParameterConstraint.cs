using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000087 RID: 135
	public readonly struct GenericParameterConstraint
	{
		// Token: 0x06000549 RID: 1353 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		[NullableContext(1)]
		internal GenericParameterConstraint(MetadataReader reader, GenericParameterConstraintHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000C902 File Offset: 0x0000AB02
		private GenericParameterConstraintHandle Handle
		{
			get
			{
				return GenericParameterConstraintHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets the constrained <see cref="T:System.Reflection.Metadata.GenericParameterHandle" />.</summary>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000C90F File Offset: 0x0000AB0F
		public GenericParameterHandle Parameter
		{
			get
			{
				return this._reader.GenericParamConstraintTable.GetOwner(this.Handle);
			}
		}

		/// <summary>Gets a handle (<see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />) 
		///       specifying from which type this generic parameter is constrained to derive,
		///       or which interface this generic parameter is constrained to implement.</summary>
		/// <returns>An <see cref="T:System.Reflection.Metadata.EntityHandle" /> instance.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000C927 File Offset: 0x0000AB27
		public EntityHandle Type
		{
			get
			{
				return this._reader.GenericParamConstraintTable.GetConstraint(this.Handle);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000C93F File Offset: 0x0000AB3F
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000360 RID: 864
		private readonly MetadataReader _reader;

		// Token: 0x04000361 RID: 865
		private readonly int _rowId;
	}
}
