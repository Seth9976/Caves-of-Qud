using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A0 RID: 160
	public readonly struct InterfaceImplementation
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0000D2C1 File Offset: 0x0000B4C1
		[NullableContext(1)]
		internal InterfaceImplementation(MetadataReader reader, InterfaceImplementationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000D2D7 File Offset: 0x0000B4D7
		private InterfaceImplementationHandle Handle
		{
			get
			{
				return InterfaceImplementationHandle.FromRowId(this._rowId);
			}
		}

		/// <summary>Gets the interface that is implemented (<see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" />, or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />).</summary>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000D2E4 File Offset: 0x0000B4E4
		public EntityHandle Interface
		{
			get
			{
				return this._reader.InterfaceImplTable.GetInterface(this._rowId);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040003B9 RID: 953
		private readonly MetadataReader _reader;

		// Token: 0x040003BA RID: 954
		private readonly int _rowId;
	}
}
