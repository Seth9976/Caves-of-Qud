using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E9 RID: 489
	public interface ISyntaxContextReceiver
	{
		// Token: 0x06001418 RID: 5144
		void OnVisitSyntaxNode(GeneratorSyntaxContext context);
	}
}
