using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000251 RID: 593
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CrefParameterSyntax : CSharpSyntaxNode
	{
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x060038A8 RID: 14504 RVA: 0x000FB42A File Offset: 0x000F962A
		[EditorBrowsable(1)]
		public SyntaxToken RefOrOutKeyword
		{
			get
			{
				return this.RefKindKeyword;
			}
		}

		// Token: 0x060038A9 RID: 14505 RVA: 0x000FB432 File Offset: 0x000F9632
		[EditorBrowsable(1)]
		public CrefParameterSyntax WithRefOrOutKeyword(SyntaxToken refOrOutKeyword)
		{
			return this.Update(refOrOutKeyword, this.Type);
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000FB441 File Offset: 0x000F9641
		internal CrefParameterSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x000FB44C File Offset: 0x000F964C
		public SyntaxToken RefKindKeyword
		{
			get
			{
				SyntaxToken refKindKeyword = ((CrefParameterSyntax)base.Green).refKindKeyword;
				if (refKindKeyword == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, refKindKeyword, base.Position, 0);
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000FB485 File Offset: 0x000F9685
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000FB494 File Offset: 0x000F9694
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<TypeSyntax>(ref this.type, 1);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000FB4A9 File Offset: 0x000F96A9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000FB4B7 File Offset: 0x000F96B7
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCrefParameter(this);
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000FB4C0 File Offset: 0x000F96C0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCrefParameter(this);
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000FB4CC File Offset: 0x000F96CC
		public CrefParameterSyntax Update(SyntaxToken refKindKeyword, TypeSyntax type)
		{
			if (!(refKindKeyword != this.RefKindKeyword) && type == this.Type)
			{
				return this;
			}
			CrefParameterSyntax crefParameterSyntax = SyntaxFactory.CrefParameter(refKindKeyword, type);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return crefParameterSyntax;
			}
			return crefParameterSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000FB511 File Offset: 0x000F9711
		public CrefParameterSyntax WithRefKindKeyword(SyntaxToken refKindKeyword)
		{
			return this.Update(refKindKeyword, this.Type);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000FB520 File Offset: 0x000F9720
		public CrefParameterSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.RefKindKeyword, type);
		}

		// Token: 0x040013A8 RID: 5032
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
