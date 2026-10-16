using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C9 RID: 1225
	internal sealed class ModuleWellKnownAttributeData : CommonModuleWellKnownAttributeData, ISkipLocalsInitAttributeTarget
	{
		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06006870 RID: 26736 RVA: 0x00188293 File Offset: 0x00186493
		// (set) Token: 0x06006871 RID: 26737 RVA: 0x0018829B File Offset: 0x0018649B
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

		// Token: 0x040019E3 RID: 6627
		private bool _hasSkipLocalsInitAttribute;
	}
}
