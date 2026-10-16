using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003EC RID: 1004
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class VariableDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005732 RID: 22322 RVA: 0x0015751D File Offset: 0x0015571D
		internal VariableDeclarationSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode variables, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (variables != null)
			{
				base.AdjustFlagsAndWidth(variables);
				this.variables = variables;
			}
		}

		// Token: 0x06005733 RID: 22323 RVA: 0x00157550 File Offset: 0x00155750
		internal VariableDeclarationSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode variables, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (variables != null)
			{
				base.AdjustFlagsAndWidth(variables);
				this.variables = variables;
			}
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x00157587 File Offset: 0x00155787
		internal VariableDeclarationSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode variables)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (variables != null)
			{
				base.AdjustFlagsAndWidth(variables);
				this.variables = variables;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x001575B6 File Offset: 0x001557B6
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06005736 RID: 22326 RVA: 0x001575BE File Offset: 0x001557BE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<VariableDeclaratorSyntax> Variables
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<VariableDeclaratorSyntax>(new SyntaxList<CSharpSyntaxNode>(this.variables));
			}
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x001575D8 File Offset: 0x001557D8
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
					greenNode = this.variables;
				}
			}
			else
			{
				greenNode = this.type;
			}
			return greenNode;
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x00157603 File Offset: 0x00155803
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new VariableDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005739 RID: 22329 RVA: 0x0015760D File Offset: 0x0015580D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitVariableDeclaration(this);
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x00157616 File Offset: 0x00155816
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitVariableDeclaration(this);
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x00157620 File Offset: 0x00155820
		public VariableDeclarationSyntax Update(TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
		{
			if (type == this.Type)
			{
				SeparatedSyntaxList<VariableDeclaratorSyntax> separatedSyntaxList = this.Variables;
				if (!((in variables) != (in separatedSyntaxList)))
				{
					return this;
				}
			}
			VariableDeclarationSyntax variableDeclarationSyntax = SyntaxFactory.VariableDeclaration(type, variables);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				variableDeclarationSyntax = variableDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				variableDeclarationSyntax = variableDeclarationSyntax.WithAnnotationsGreen(annotations);
			}
			return variableDeclarationSyntax;
		}

		// Token: 0x0600573C RID: 22332 RVA: 0x0015767F File Offset: 0x0015587F
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new VariableDeclarationSyntax(base.Kind, this.type, this.variables, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x0015769F File Offset: 0x0015589F
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new VariableDeclarationSyntax(base.Kind, this.type, this.variables, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x001576C0 File Offset: 0x001558C0
		internal VariableDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.variables = greenNode;
			}
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x00157712 File Offset: 0x00155912
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
			writer.WriteValue(this.variables);
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x00157733 File Offset: 0x00155933
		static VariableDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(VariableDeclarationSyntax), (ObjectReader r) => new VariableDeclarationSyntax(r));
		}

		// Token: 0x04001707 RID: 5895
		internal readonly TypeSyntax type;

		// Token: 0x04001708 RID: 5896
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode variables;
	}
}
