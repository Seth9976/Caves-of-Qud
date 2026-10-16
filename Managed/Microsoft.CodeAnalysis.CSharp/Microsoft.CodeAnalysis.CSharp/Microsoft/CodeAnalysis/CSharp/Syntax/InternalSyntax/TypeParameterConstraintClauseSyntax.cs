using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000430 RID: 1072
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeParameterConstraintClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005B98 RID: 23448 RVA: 0x00166020 File Offset: 0x00164220
		internal TypeParameterConstraintClauseSyntax(SyntaxKind kind, SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode constraints, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(whereKeyword);
			this.whereKeyword = whereKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			if (constraints != null)
			{
				base.AdjustFlagsAndWidth(constraints);
				this.constraints = constraints;
			}
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x00166080 File Offset: 0x00164280
		internal TypeParameterConstraintClauseSyntax(SyntaxKind kind, SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode constraints, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(whereKeyword);
			this.whereKeyword = whereKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			if (constraints != null)
			{
				base.AdjustFlagsAndWidth(constraints);
				this.constraints = constraints;
			}
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x001660E4 File Offset: 0x001642E4
		internal TypeParameterConstraintClauseSyntax(SyntaxKind kind, SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode constraints)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(whereKeyword);
			this.whereKeyword = whereKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			if (constraints != null)
			{
				base.AdjustFlagsAndWidth(constraints);
				this.constraints = constraints;
			}
		}

		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06005B9B RID: 23451 RVA: 0x0016613F File Offset: 0x0016433F
		public SyntaxToken WhereKeyword
		{
			get
			{
				return this.whereKeyword;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x00166147 File Offset: 0x00164347
		public IdentifierNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x0016614F File Offset: 0x0016434F
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06005B9E RID: 23454 RVA: 0x00166157 File Offset: 0x00164357
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TypeParameterConstraintSyntax> Constraints
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<TypeParameterConstraintSyntax>(new SyntaxList<CSharpSyntaxNode>(this.constraints));
			}
		}

		// Token: 0x06005B9F RID: 23455 RVA: 0x00166170 File Offset: 0x00164370
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.whereKeyword;
				break;
			case 1:
				greenNode = this.name;
				break;
			case 2:
				greenNode = this.colonToken;
				break;
			case 3:
				greenNode = this.constraints;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x001661BC File Offset: 0x001643BC
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeParameterConstraintClauseSyntax(this, parent, position);
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x001661C6 File Offset: 0x001643C6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeParameterConstraintClause(this);
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x001661CF File Offset: 0x001643CF
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameterConstraintClause(this);
		}

		// Token: 0x06005BA3 RID: 23459 RVA: 0x001661D8 File Offset: 0x001643D8
		public TypeParameterConstraintClauseSyntax Update(SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
		{
			if (whereKeyword == this.WhereKeyword && name == this.Name && colonToken == this.ColonToken)
			{
				SeparatedSyntaxList<TypeParameterConstraintSyntax> separatedSyntaxList = this.Constraints;
				if (!((in constraints) != (in separatedSyntaxList)))
				{
					return this;
				}
			}
			TypeParameterConstraintClauseSyntax typeParameterConstraintClauseSyntax = SyntaxFactory.TypeParameterConstraintClause(whereKeyword, name, colonToken, constraints);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				typeParameterConstraintClauseSyntax = typeParameterConstraintClauseSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				typeParameterConstraintClauseSyntax = typeParameterConstraintClauseSyntax.WithAnnotationsGreen(annotations);
			}
			return typeParameterConstraintClauseSyntax;
		}

		// Token: 0x06005BA4 RID: 23460 RVA: 0x0016624C File Offset: 0x0016444C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeParameterConstraintClauseSyntax(base.Kind, this.whereKeyword, this.name, this.colonToken, this.constraints, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005BA5 RID: 23461 RVA: 0x00166278 File Offset: 0x00164478
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeParameterConstraintClauseSyntax(base.Kind, this.whereKeyword, this.name, this.colonToken, this.constraints, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005BA6 RID: 23462 RVA: 0x001662A4 File Offset: 0x001644A4
		internal TypeParameterConstraintClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.whereKeyword = syntaxToken;
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(identifierNameSyntax);
			this.name = identifierNameSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.colonToken = syntaxToken2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.constraints = greenNode;
			}
		}

		// Token: 0x06005BA7 RID: 23463 RVA: 0x0016632A File Offset: 0x0016452A
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.whereKeyword);
			writer.WriteValue(this.name);
			writer.WriteValue(this.colonToken);
			writer.WriteValue(this.constraints);
		}

		// Token: 0x06005BA8 RID: 23464 RVA: 0x00166363 File Offset: 0x00164563
		static TypeParameterConstraintClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeParameterConstraintClauseSyntax), (ObjectReader r) => new TypeParameterConstraintClauseSyntax(r));
		}

		// Token: 0x0400182A RID: 6186
		internal readonly SyntaxToken whereKeyword;

		// Token: 0x0400182B RID: 6187
		internal readonly IdentifierNameSyntax name;

		// Token: 0x0400182C RID: 6188
		internal readonly SyntaxToken colonToken;

		// Token: 0x0400182D RID: 6189
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode constraints;
	}
}
