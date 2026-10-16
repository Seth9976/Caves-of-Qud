using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AD RID: 173
	public readonly struct MethodImport
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x00011291 File Offset: 0x0000F491
		internal MethodImport(MethodImportAttributes attributes, StringHandle name, ModuleReferenceHandle module)
		{
			this._attributes = attributes;
			this._name = name;
			this._module = module;
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public MethodImportAttributes Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public StringHandle Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public ModuleReferenceHandle Module
		{
			get
			{
				return this._module;
			}
		}

		// Token: 0x04000431 RID: 1073
		private readonly MethodImportAttributes _attributes;

		// Token: 0x04000432 RID: 1074
		private readonly StringHandle _name;

		// Token: 0x04000433 RID: 1075
		private readonly ModuleReferenceHandle _module;
	}
}
