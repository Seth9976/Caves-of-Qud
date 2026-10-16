using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x0200008A RID: 138
	[NullableContext(1)]
	internal interface IMetadataNamedArgument : IMetadataExpression
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000372 RID: 882
		string ArgumentName { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000373 RID: 883
		IMetadataExpression ArgumentValue { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000374 RID: 884
		bool IsField { get; }
	}
}
