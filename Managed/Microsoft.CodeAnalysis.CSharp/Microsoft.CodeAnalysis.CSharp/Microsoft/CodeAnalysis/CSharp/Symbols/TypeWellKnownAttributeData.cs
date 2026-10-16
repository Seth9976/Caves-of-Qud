using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004CF RID: 1231
	internal sealed class TypeWellKnownAttributeData : CommonTypeWellKnownAttributeData, ISkipLocalsInitAttributeTarget
	{
		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x060068A3 RID: 26787 RVA: 0x001884EF File Offset: 0x001866EF
		// (set) Token: 0x060068A4 RID: 26788 RVA: 0x001884F7 File Offset: 0x001866F7
		public NamedTypeSymbol ComImportCoClass
		{
			get
			{
				return this._comImportCoClass;
			}
			set
			{
				this._comImportCoClass = value;
			}
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x060068A5 RID: 26789 RVA: 0x00188500 File Offset: 0x00186700
		// (set) Token: 0x060068A6 RID: 26790 RVA: 0x00188508 File Offset: 0x00186708
		public bool HasSkipLocalsInitAttribute
		{
			get
			{
				return this._hasSkipLocalsInitAttribute;
			}
			set
			{
				this._hasSkipLocalsInitAttribute = value;
			}
		}

		// Token: 0x040019F9 RID: 6649
		private NamedTypeSymbol _comImportCoClass;

		// Token: 0x040019FA RID: 6650
		private bool _hasSkipLocalsInitAttribute;
	}
}
