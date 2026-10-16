using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200013C RID: 316
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CommonSyntaxAndDeclarationManager
	{
		// Token: 0x06000C6F RID: 3183 RVA: 0x00026308 File Offset: 0x00024508
		public CommonSyntaxAndDeclarationManager([Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxTree> externalSyntaxTrees, string scriptClassName, SourceReferenceResolver resolver, CommonMessageProvider messageProvider, bool isSubmission)
		{
			this.ExternalSyntaxTrees = externalSyntaxTrees;
			this.ScriptClassName = scriptClassName ?? "";
			this.Resolver = resolver;
			this.MessageProvider = messageProvider;
			this.IsSubmission = isSubmission;
		}

		// Token: 0x04000369 RID: 873
		[Nullable(new byte[] { 0, 1 })]
		internal readonly ImmutableArray<SyntaxTree> ExternalSyntaxTrees;

		// Token: 0x0400036A RID: 874
		internal readonly string ScriptClassName;

		// Token: 0x0400036B RID: 875
		internal readonly SourceReferenceResolver Resolver;

		// Token: 0x0400036C RID: 876
		internal readonly CommonMessageProvider MessageProvider;

		// Token: 0x0400036D RID: 877
		internal readonly bool IsSubmission;
	}
}
