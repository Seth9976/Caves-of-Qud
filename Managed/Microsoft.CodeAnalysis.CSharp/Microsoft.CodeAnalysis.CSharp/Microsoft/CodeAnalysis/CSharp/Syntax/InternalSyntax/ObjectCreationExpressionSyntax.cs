using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B9 RID: 953
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ObjectCreationExpressionSyntax : BaseObjectCreationExpressionSyntax
	{
		// Token: 0x0600543E RID: 21566 RVA: 0x0014F4C0 File Offset: 0x0014D6C0
		internal ObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (argumentList != null)
			{
				base.AdjustFlagsAndWidth(argumentList);
				this.argumentList = argumentList;
			}
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x0014F524 File Offset: 0x0014D724
		internal ObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (argumentList != null)
			{
				base.AdjustFlagsAndWidth(argumentList);
				this.argumentList = argumentList;
			}
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x0014F58C File Offset: 0x0014D78C
		internal ObjectCreationExpressionSyntax(SyntaxKind kind, SyntaxToken newKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(newKeyword);
			this.newKeyword = newKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (argumentList != null)
			{
				base.AdjustFlagsAndWidth(argumentList);
				this.argumentList = argumentList;
			}
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x0014F5EB File Offset: 0x0014D7EB
		public override SyntaxToken NewKeyword
		{
			get
			{
				return this.newKeyword;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06005442 RID: 21570 RVA: 0x0014F5F3 File Offset: 0x0014D7F3
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06005443 RID: 21571 RVA: 0x0014F5FB File Offset: 0x0014D7FB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArgumentListSyntax ArgumentList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06005444 RID: 21572 RVA: 0x0014F603 File Offset: 0x0014D803
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override InitializerExpressionSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x0014F60C File Offset: 0x0014D80C
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
				greenNode = this.argumentList;
				break;
			case 3:
				greenNode = this.initializer;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x0014F658 File Offset: 0x0014D858
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ObjectCreationExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x0014F662 File Offset: 0x0014D862
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitObjectCreationExpression(this);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x0014F66B File Offset: 0x0014D86B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitObjectCreationExpression(this);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x0014F674 File Offset: 0x0014D874
		public ObjectCreationExpressionSyntax Update(SyntaxToken newKeyword, TypeSyntax type, ArgumentListSyntax argumentList, InitializerExpressionSyntax initializer)
		{
			if (newKeyword != this.NewKeyword || type != this.Type || argumentList != this.ArgumentList || initializer != this.Initializer)
			{
				ObjectCreationExpressionSyntax objectCreationExpressionSyntax = SyntaxFactory.ObjectCreationExpression(newKeyword, type, argumentList, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					objectCreationExpressionSyntax = objectCreationExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					objectCreationExpressionSyntax = objectCreationExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return objectCreationExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x0014F6E0 File Offset: 0x0014D8E0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ObjectCreationExpressionSyntax(base.Kind, this.newKeyword, this.type, this.argumentList, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x0014F70C File Offset: 0x0014D90C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ObjectCreationExpressionSyntax(base.Kind, this.newKeyword, this.type, this.argumentList, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x0014F738 File Offset: 0x0014D938
		internal ObjectCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.newKeyword = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)reader.ReadValue();
			if (argumentListSyntax != null)
			{
				base.AdjustFlagsAndWidth(argumentListSyntax);
				this.argumentList = argumentListSyntax;
			}
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)reader.ReadValue();
			if (initializerExpressionSyntax != null)
			{
				base.AdjustFlagsAndWidth(initializerExpressionSyntax);
				this.initializer = initializerExpressionSyntax;
			}
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0014F7C1 File Offset: 0x0014D9C1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.newKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.argumentList);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x0014F7FA File Offset: 0x0014D9FA
		static ObjectCreationExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ObjectCreationExpressionSyntax), (ObjectReader r) => new ObjectCreationExpressionSyntax(r));
		}

		// Token: 0x04001679 RID: 5753
		internal readonly SyntaxToken newKeyword;

		// Token: 0x0400167A RID: 5754
		internal readonly TypeSyntax type;

		// Token: 0x0400167B RID: 5755
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ArgumentListSyntax argumentList;

		// Token: 0x0400167C RID: 5756
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly InitializerExpressionSyntax initializer;
	}
}
