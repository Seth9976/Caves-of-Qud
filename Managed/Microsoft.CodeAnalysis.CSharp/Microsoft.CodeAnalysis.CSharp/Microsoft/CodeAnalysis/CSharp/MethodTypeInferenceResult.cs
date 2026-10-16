using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000069 RID: 105
	internal struct MethodTypeInferenceResult
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00049A70 File Offset: 0x00047C70
		public MethodTypeInferenceResult(bool success, ImmutableArray<TypeWithAnnotations> inferredTypeArguments)
		{
			this.Success = success;
			this.InferredTypeArguments = inferredTypeArguments;
		}

		// Token: 0x04000447 RID: 1095
		public readonly ImmutableArray<TypeWithAnnotations> InferredTypeArguments;

		// Token: 0x04000448 RID: 1096
		public readonly bool Success;
	}
}
