using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001EC RID: 492
	internal abstract class PendingEdit
	{
		// Token: 0x06001421 RID: 5153
		internal abstract GeneratorDriverState Commit(GeneratorDriverState state);

		// Token: 0x06001422 RID: 5154
		internal abstract bool AcceptedBy(GeneratorInfo info);

		// Token: 0x06001423 RID: 5155
		internal abstract bool TryApply(GeneratorInfo info, GeneratorEditContext context);
	}
}
