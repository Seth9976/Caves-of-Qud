using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000086 RID: 134
	public readonly struct GenericParameter
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x0000C839 File Offset: 0x0000AA39
		[NullableContext(1)]
		internal GenericParameter(MetadataReader reader, GenericParameterHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000C84F File Offset: 0x0000AA4F
		private GenericParameterHandle Handle
		{
			get
			{
				return GenericParameterHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" /> or <see cref="T:System.Reflection.Metadata.MethodDefinitionHandle" /> that represents the parent of this generic parameter.</summary>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000C85C File Offset: 0x0000AA5C
		public EntityHandle Parent
		{
			get
			{
				return this._reader.GenericParamTable.GetOwner(this.Handle);
			}
		}

		/// <summary>Gets the attributes specifying variance and constraints.</summary>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000C874 File Offset: 0x0000AA74
		public GenericParameterAttributes Attributes
		{
			get
			{
				return this._reader.GenericParamTable.GetFlags(this.Handle);
			}
		}

		/// <summary>Gets the zero-based index of the parameter within the declaring generic type or method declaration.</summary>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public int Index
		{
			get
			{
				return (int)this._reader.GenericParamTable.GetNumber(this.Handle);
			}
		}

		/// <summary>Gets the name of the generic parameter.</summary>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
		public StringHandle Name
		{
			get
			{
				return this._reader.GenericParamTable.GetName(this.Handle);
			}
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000C8BC File Offset: 0x0000AABC
		public GenericParameterConstraintHandleCollection GetConstraints()
		{
			return this._reader.GenericParamConstraintTable.FindConstraintsForGenericParam(this.Handle);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0400035E RID: 862
		private readonly MetadataReader _reader;

		// Token: 0x0400035F RID: 863
		private readonly int _rowId;
	}
}
