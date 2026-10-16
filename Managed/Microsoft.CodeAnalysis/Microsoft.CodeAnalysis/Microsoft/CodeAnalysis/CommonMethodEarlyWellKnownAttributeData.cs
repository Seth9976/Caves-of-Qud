using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200021D RID: 541
	[NullableContext(2)]
	[Nullable(0)]
	internal class CommonMethodEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x00043EC9 File Offset: 0x000420C9
		[NullableContext(1)]
		public void AddConditionalSymbol(string name)
		{
			this._lazyConditionalSymbols = this._lazyConditionalSymbols.Add(name);
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00043EDD File Offset: 0x000420DD
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ConditionalSymbols
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyConditionalSymbols;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00043EE5 File Offset: 0x000420E5
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00043EFC File Offset: 0x000420FC
		public ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				if (!this._obsoleteAttributeData.IsUninitialized)
				{
					return this._obsoleteAttributeData;
				}
				return null;
			}
			set
			{
				this._obsoleteAttributeData = value;
			}
		}

		// Token: 0x040008BC RID: 2236
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> _lazyConditionalSymbols = ImmutableArray<string>.Empty;

		// Token: 0x040008BD RID: 2237
		[Nullable(1)]
		private ObsoleteAttributeData _obsoleteAttributeData = ObsoleteAttributeData.Uninitialized;
	}
}
