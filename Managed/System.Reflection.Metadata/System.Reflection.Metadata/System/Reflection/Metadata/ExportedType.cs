using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000084 RID: 132
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ExportedType
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
		internal ExportedType(MetadataReader reader, int rowId)
		{
			this.reader = reader;
			this.rowId = rowId;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000C506 File Offset: 0x0000A706
		private ExportedTypeHandle Handle
		{
			get
			{
				return ExportedTypeHandle.FromRowId(this.rowId);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000C513 File Offset: 0x0000A713
		public TypeAttributes Attributes
		{
			get
			{
				return this.reader.ExportedTypeTable.GetFlags(this.rowId);
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000C52C File Offset: 0x0000A72C
		public bool IsForwarder
		{
			get
			{
				return this.Attributes.IsForwarder() && this.Implementation.Kind == HandleKind.AssemblyReference;
			}
		}

		/// <summary>Gets the name of the target type, or <see langword="default" /> if the type is nested or defined in a root namespace.</summary>
		/// <returns>A <see cref="T:System.Reflection.Metadata.StringHandle" /> struct instance.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000C55A File Offset: 0x0000A75A
		public StringHandle Name
		{
			get
			{
				return this.reader.ExportedTypeTable.GetTypeName(this.rowId);
			}
		}

		/// <summary>Gets the full name of the namespace that contains the target type, or <see langword="default" /> if the type is nested or defined in a root namespace.</summary>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000C572 File Offset: 0x0000A772
		public StringHandle Namespace
		{
			get
			{
				return this.reader.ExportedTypeTable.GetTypeNamespaceString(this.rowId);
			}
		}

		/// <summary>Gets the definition handle of the namespace where the target type is defined, or <see langword="default" /> if the type is nested or defined in a root namespace.</summary>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000C58A File Offset: 0x0000A78A
		public NamespaceDefinitionHandle NamespaceDefinition
		{
			get
			{
				return this.reader.ExportedTypeTable.GetTypeNamespace(this.rowId);
			}
		}

		/// <summary>Gets a handle to resolve the implementation of the target type.</summary>
		/// <returns>
		///   <list type="bullet">
		///     <item>
		///       <description>
		///         <see cref="T:System.Reflection.Metadata.AssemblyFileHandle" /> representing another module in the assembly.</description>
		///     </item>
		///     <item>
		///       <description>
		///         <see cref="T:System.Reflection.Metadata.AssemblyReferenceHandle" /> representing another assembly if <see cref="P:System.Reflection.Metadata.ExportedType.IsForwarder" /> is <see langword="true" />.</description>
		///     </item>
		///     <item>
		///       <description>
		///         <see cref="T:System.Reflection.Metadata.ExportedTypeHandle" /> representing the declaring exported type in which this was is nested.</description>
		///     </item>
		///   </list>
		/// </returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000C5A2 File Offset: 0x0000A7A2
		public EntityHandle Implementation
		{
			get
			{
				return this.reader.ExportedTypeTable.GetImplementation(this.rowId);
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000C5BA File Offset: 0x0000A7BA
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this.reader, this.Handle);
		}

		// Token: 0x0400035A RID: 858
		internal readonly MetadataReader reader;

		// Token: 0x0400035B RID: 859
		internal readonly int rowId;
	}
}
