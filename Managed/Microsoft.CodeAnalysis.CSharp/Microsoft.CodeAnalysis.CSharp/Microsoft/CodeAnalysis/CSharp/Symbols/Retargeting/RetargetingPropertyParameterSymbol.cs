using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005DC RID: 1500
	internal sealed class RetargetingPropertyParameterSymbol : RetargetingParameterSymbol
	{
		// Token: 0x06007D72 RID: 32114 RVA: 0x001C311A File Offset: 0x001C131A
		public RetargetingPropertyParameterSymbol(RetargetingPropertySymbol retargetingProperty, ParameterSymbol underlyingParameter)
			: base(underlyingParameter)
		{
			this._retargetingProperty = retargetingProperty;
		}

		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x06007D73 RID: 32115 RVA: 0x001C312A File Offset: 0x001C132A
		protected override RetargetingModuleSymbol RetargetingModule
		{
			get
			{
				return this._retargetingProperty.RetargetingModule;
			}
		}

		// Token: 0x04001D88 RID: 7560
		private readonly RetargetingPropertySymbol _retargetingProperty;
	}
}
