using System;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000358 RID: 856
	internal readonly struct BlendedNode
	{
		// Token: 0x06004829 RID: 18473 RVA: 0x0011B4DA File Offset: 0x001196DA
		internal BlendedNode(CSharpSyntaxNode node, SyntaxToken token, Blender blender)
		{
			this.Node = node;
			this.Token = token;
			this.Blender = blender;
		}

		// Token: 0x0400152F RID: 5423
		internal readonly CSharpSyntaxNode Node;

		// Token: 0x04001530 RID: 5424
		internal readonly SyntaxToken Token;

		// Token: 0x04001531 RID: 5425
		internal readonly Blender Blender;
	}
}
