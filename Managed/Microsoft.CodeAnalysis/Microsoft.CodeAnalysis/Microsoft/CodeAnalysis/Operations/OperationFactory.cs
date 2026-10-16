using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000468 RID: 1128
	internal static class OperationFactory
	{
		// Token: 0x06002C3A RID: 11322 RVA: 0x0007D20E File Offset: 0x0007B40E
		[NullableContext(1)]
		public static IInvalidOperation CreateInvalidOperation(SemanticModel semanticModel, SyntaxNode syntax, [Nullable(new byte[] { 0, 1 })] ImmutableArray<IOperation> children, bool isImplicit)
		{
			return new InvalidOperation(children, semanticModel, syntax, null, null, isImplicit);
		}

		// Token: 0x0400118D RID: 4493
		[Nullable(1)]
		public static readonly IConvertibleConversion IdentityConversion = new OperationFactory.IdentityConvertibleConversion();

		// Token: 0x02000728 RID: 1832
		private class IdentityConvertibleConversion : IConvertibleConversion
		{
			// Token: 0x06003BA6 RID: 15270 RVA: 0x000AB663 File Offset: 0x000A9863
			public CommonConversion ToCommonConversion()
			{
				return new CommonConversion(true, true, false, false, true, false, null);
			}
		}
	}
}
