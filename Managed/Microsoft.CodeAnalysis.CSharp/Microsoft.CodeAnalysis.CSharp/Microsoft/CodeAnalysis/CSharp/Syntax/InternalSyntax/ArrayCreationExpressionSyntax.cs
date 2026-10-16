using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003BD RID: 957
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArrayCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600547F RID: 21631 RVA: 0x00150044 File Offset: 0x0014E244
		internal ArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x00150094 File Offset: 0x0014E294
		internal ArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x001500E7 File Offset: 0x0014E2E7
		internal ArrayCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06005482 RID: 21634 RVA: 0x00150127 File Offset: 0x0014E327
		public SyntaxToken NewKeyword
		{
			get
			{
				return this.newKeyword;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x0015012F File Offset: 0x0014E32F
		public ArrayTypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06005484 RID: 21636 RVA: 0x00150137 File Offset: 0x0014E337
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x00150140 File Offset: 0x0014E340
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.newKeyword;
				break;
			case 1:
				greenNode = this.type;
				break;
			case 2:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0015017F File Offset: 0x0014E37F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ArrayCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00150189 File Offset: 0x0014E389
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrayCreationExpression(this);
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00150192 File Offset: 0x0014E392
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrayCreationExpression(this);
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0015019C File Offset: 0x0014E39C
		public ArrayCreationExpressionSyntax Update(SyntaxToken newKeyword, ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
		{
			if (newKeyword != this.NewKeyword || type != this.Type || initializer != this.Initializer)
			{
				ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = SyntaxFactory.ArrayCreationExpression(newKeyword, type, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					arrayCreationExpressionSyntax = arrayCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					arrayCreationExpressionSyntax = arrayCreationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return arrayCreationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x001501FC File Offset: 0x0014E3FC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ArrayCreationExpressionSyntax(base.Kind, this.newKeyword, this.type, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x00150222 File Offset: 0x0014E422
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ArrayCreationExpressionSyntax(base.Kind, this.newKeyword, this.type, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x00150248 File Offset: 0x0014E448
		internal ArrayCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.newKeyword = syntaxToken;
			ArrayTypeSyntax arrayTypeSyntax = (ArrayTypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(arrayTypeSyntax);
			this.type = arrayTypeSyntax;
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			if (initializerExpressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(initializerExpressionSyntax);
				this.initializer = initializerExpressionSyntax;
			}
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x001502B4 File Offset: 0x0014E4B4
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.newKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x001502E1 File Offset: 0x0014E4E1
		static ArrayCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ArrayCreationExpressionSyntax), (ObjectReader r) => new ArrayCreationExpressionSyntax(r));
		}

		// Token: 0x04001686 RID: 5766
		internal readonly SyntaxToken newKeyword;

		// Token: 0x04001687 RID: 5767
		internal readonly ArrayTypeSyntax type;

		// Token: 0x04001688 RID: 5768
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly InitializerExpressionSyntax initializer;
	}
}
