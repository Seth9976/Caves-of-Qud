using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000381 RID: 897
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PointerTypeSyntax : TypeSyntax
	{
		// Token: 0x06005116 RID: 20758 RVA: 0x001471EE File Offset: 0x001453EE
		internal PointerTypeSyntax(SyntaxKind kind, TypeSyntax elementType, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			base.AdjustFlagsAndWidth(asteriskToken);
			this.asteriskToken = asteriskToken;
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x0014721E File Offset: 0x0014541E
		internal PointerTypeSyntax(SyntaxKind kind, TypeSyntax elementType, SyntaxToken asteriskToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			base.AdjustFlagsAndWidth(asteriskToken);
			this.asteriskToken = asteriskToken;
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x00147252 File Offset: 0x00145452
		internal PointerTypeSyntax(SyntaxKind kind, TypeSyntax elementType, SyntaxToken asteriskToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			base.AdjustFlagsAndWidth(asteriskToken);
			this.asteriskToken = asteriskToken;
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x0014727E File Offset: 0x0014547E
		public TypeSyntax ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x0600511A RID: 20762 RVA: 0x00147286 File Offset: 0x00145486
		public SyntaxToken AsteriskToken
		{
			get
			{
				return this.asteriskToken;
			}
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00147290 File Offset: 0x00145490
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.asteriskToken;
				}
			}
			else
			{
				greenNode = this.elementType;
			}
			return greenNode;
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x001472BB File Offset: 0x001454BB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PointerTypeSyntax(this, parent, position);
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x001472C5 File Offset: 0x001454C5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPointerType(this);
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x001472CE File Offset: 0x001454CE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPointerType(this);
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x001472D8 File Offset: 0x001454D8
		public PointerTypeSyntax Update(TypeSyntax elementType, SyntaxToken asteriskToken)
		{
			if (elementType != this.ElementType || asteriskToken != this.AsteriskToken)
			{
				PointerTypeSyntax pointerTypeSyntax = SyntaxFactory.PointerType(elementType, asteriskToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					pointerTypeSyntax = pointerTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					pointerTypeSyntax = pointerTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return pointerTypeSyntax;
			}
			return this;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0014732E File Offset: 0x0014552E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PointerTypeSyntax(base.Kind, this.elementType, this.asteriskToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0014734E File Offset: 0x0014554E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PointerTypeSyntax(base.Kind, this.elementType, this.asteriskToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00147370 File Offset: 0x00145570
		internal PointerTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.elementType = typeSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.asteriskToken = syntaxToken;
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x001473BF File Offset: 0x001455BF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.elementType);
			writer.WriteValue(this.asteriskToken);
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x001473E0 File Offset: 0x001455E0
		static PointerTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PointerTypeSyntax), (ObjectReader r) => new PointerTypeSyntax(r));
		}

		// Token: 0x040015EC RID: 5612
		internal readonly TypeSyntax elementType;

		// Token: 0x040015ED RID: 5613
		internal readonly SyntaxToken asteriskToken;
	}
}
