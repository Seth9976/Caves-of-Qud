using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002F5 RID: 757
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class SingleVariableDesignationSyntax : VariableDesignationSyntax
	{
		// Token: 0x06004227 RID: 16935 RVA: 0x0011092E File Offset: 0x0010EB2E
		internal SingleVariableDesignationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x00110939 File Offset: 0x0010EB39
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((SingleVariableDesignationSyntax)base.Green).identifier, base.Position, 0);
			}
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x00110958 File Offset: 0x0010EB58
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x0011095B File Offset: 0x0010EB5B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x0011095E File Offset: 0x0010EB5E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSingleVariableDesignation(this);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x00110967 File Offset: 0x0010EB67
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSingleVariableDesignation(this);
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x00110970 File Offset: 0x0010EB70
		public SingleVariableDesignationSyntax Update(SyntaxToken identifier)
		{
			if (!(identifier != this.Identifier))
			{
				return this;
			}
			SingleVariableDesignationSyntax singleVariableDesignationSyntax = SyntaxFactory.SingleVariableDesignation(identifier);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return singleVariableDesignationSyntax;
			}
			return singleVariableDesignationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x001109AB File Offset: 0x0010EBAB
		public SingleVariableDesignationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(identifier);
		}
	}
}
