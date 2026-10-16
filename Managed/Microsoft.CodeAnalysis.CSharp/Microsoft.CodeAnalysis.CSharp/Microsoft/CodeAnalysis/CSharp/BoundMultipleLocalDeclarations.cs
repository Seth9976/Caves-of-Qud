using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200018F RID: 399
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundMultipleLocalDeclarations : BoundMultipleLocalDeclarationsBase
	{
		// Token: 0x06001E92 RID: 7826 RVA: 0x000A0F5B File Offset: 0x0009F15B
		public BoundMultipleLocalDeclarations(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundLocalDeclaration> localDeclarations, bool hasErrors = false)
			: base(BoundKind.MultipleLocalDeclarations, syntax, localDeclarations, hasErrors || localDeclarations.HasErrors<BoundLocalDeclaration>())
		{
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000A0F73 File Offset: 0x0009F173
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitMultipleLocalDeclarations(this);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000A0F7C File Offset: 0x0009F17C
		public BoundMultipleLocalDeclarations Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundLocalDeclaration> localDeclarations)
		{
			if (localDeclarations != base.LocalDeclarations)
			{
				BoundMultipleLocalDeclarations boundMultipleLocalDeclarations = new BoundMultipleLocalDeclarations(this.Syntax, localDeclarations, base.HasErrors);
				boundMultipleLocalDeclarations.CopyAttributes(this);
				return boundMultipleLocalDeclarations;
			}
			return this;
		}
	}
}
