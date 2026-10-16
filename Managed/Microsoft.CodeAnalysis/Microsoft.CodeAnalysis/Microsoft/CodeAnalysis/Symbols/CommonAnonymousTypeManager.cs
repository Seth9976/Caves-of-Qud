using System;

namespace Microsoft.CodeAnalysis.Symbols
{
	// Token: 0x020002B8 RID: 696
	internal abstract class CommonAnonymousTypeManager
	{
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x00056023 File Offset: 0x00054223
		internal bool AreTemplatesSealed
		{
			get
			{
				return this._templatesSealed == ThreeState.True;
			}
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0005602E File Offset: 0x0005422E
		protected void SealTemplates()
		{
			this._templatesSealed = ThreeState.True;
		}

		// Token: 0x04000D6F RID: 3439
		private ThreeState _templatesSealed = ThreeState.False;
	}
}
