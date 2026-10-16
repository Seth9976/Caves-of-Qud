using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003AC RID: 940
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArgumentSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005387 RID: 21383 RVA: 0x0014D4E0 File Offset: 0x0014B6E0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ArgumentSyntax(SyntaxKind kind, NameColonSyntax nameColon, SyntaxToken refKindKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			if (refKindKeyword != null)
			{
				base.AdjustFlagsAndWidth(refKindKeyword);
				this.refKindKeyword = refKindKeyword;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x0014D534 File Offset: 0x0014B734
		internal ArgumentSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, ExpressionSyntax expression, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			if (refKindKeyword != null)
			{
				base.AdjustFlagsAndWidth(refKindKeyword);
				this.refKindKeyword = refKindKeyword;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x0014D58C File Offset: 0x0014B78C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ArgumentSyntax(SyntaxKind kind, NameColonSyntax nameColon, SyntaxToken refKindKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] ExpressionSyntax expression)
			: base(kind)
		{
			base.SlotCount = 3;
			if (nameColon != null)
			{
				base.AdjustFlagsAndWidth(nameColon);
				this.nameColon = nameColon;
			}
			if (refKindKeyword != null)
			{
				base.AdjustFlagsAndWidth(refKindKeyword);
				this.refKindKeyword = refKindKeyword;
			}
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x0600538A RID: 21386 RVA: 0x0014D5D9 File Offset: 0x0014B7D9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public NameColonSyntax NameColon
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.nameColon;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x0014D5E1 File Offset: 0x0014B7E1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken RefKindKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.refKindKeyword;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x0600538C RID: 21388 RVA: 0x0014D5E9 File Offset: 0x0014B7E9
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0014D5F4 File Offset: 0x0014B7F4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.nameColon;
				break;
			case 1:
				greenNode = this.refKindKeyword;
				break;
			case 2:
				greenNode = this.expression;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x0014D633 File Offset: 0x0014B833
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ArgumentSyntax(this, parent, position);
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x0014D63D File Offset: 0x0014B83D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArgument(this);
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x0014D646 File Offset: 0x0014B846
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArgument(this);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x0014D650 File Offset: 0x0014B850
		public ArgumentSyntax Update(NameColonSyntax nameColon, SyntaxToken refKindKeyword, ExpressionSyntax expression)
		{
			if (nameColon != this.NameColon || refKindKeyword != this.RefKindKeyword || expression != this.Expression)
			{
				ArgumentSyntax argumentSyntax = SyntaxFactory.Argument(nameColon, refKindKeyword, expression);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					argumentSyntax = argumentSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					argumentSyntax = argumentSyntax.WithAnnotationsGreen(annotations);
				}
				return argumentSyntax;
			}
			return this;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x0014D6B0 File Offset: 0x0014B8B0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ArgumentSyntax(base.Kind, this.nameColon, this.refKindKeyword, this.expression, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x0014D6D6 File Offset: 0x0014B8D6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ArgumentSyntax(base.Kind, this.nameColon, this.refKindKeyword, this.expression, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x0014D6FC File Offset: 0x0014B8FC
		internal ArgumentSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			NameColonSyntax nameColonSyntax = (NameColonSyntax)reader.ReadValue();
			if (nameColonSyntax != null)
			{
				base.AdjustFlagsAndWidth(nameColonSyntax);
				this.nameColon = nameColonSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.refKindKeyword = syntaxToken;
			}
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0014D76B File Offset: 0x0014B96B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.nameColon);
			writer.WriteValue(this.refKindKeyword);
			writer.WriteValue(this.expression);
		}

		// Token: 0x06005396 RID: 21398 RVA: 0x0014D798 File Offset: 0x0014B998
		static ArgumentSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ArgumentSyntax), (ObjectReader r) => new ArgumentSyntax(r));
		}

		// Token: 0x04001655 RID: 5717
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly NameColonSyntax nameColon;

		// Token: 0x04001656 RID: 5718
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken refKindKeyword;

		// Token: 0x04001657 RID: 5719
		internal readonly ExpressionSyntax expression;
	}
}
