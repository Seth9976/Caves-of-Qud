using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005DB RID: 1499
	internal sealed class RetargetingMethodParameterSymbol : RetargetingParameterSymbol
	{
		// Token: 0x06007D70 RID: 32112 RVA: 0x001C30FD File Offset: 0x001C12FD
		public RetargetingMethodParameterSymbol(RetargetingMethodSymbol retargetingMethod, ParameterSymbol underlyingParameter)
			: base(underlyingParameter)
		{
			this._retargetingMethod = retargetingMethod;
		}

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x06007D71 RID: 32113 RVA: 0x001C310D File Offset: 0x001C130D
		protected override RetargetingModuleSymbol RetargetingModule
		{
			get
			{
				return this._retargetingMethod.RetargetingModule;
			}
		}

		// Token: 0x04001D87 RID: 7559
		private readonly RetargetingMethodSymbol _retargetingMethod;
	}
}
