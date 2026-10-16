using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E6 RID: 486
	public interface ISourceGenerator
	{
		// Token: 0x06001411 RID: 5137
		void Initialize(GeneratorInitializationContext context);

		// Token: 0x06001412 RID: 5138
		void Execute(GeneratorExecutionContext context);
	}
}
