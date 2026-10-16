using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003ED RID: 1005
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class VariableDeclaratorSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005741 RID: 22337 RVA: 0x00157754 File Offset: 0x00155954
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal VariableDeclaratorSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, BracketedArgumentListSyntax argumentList, EqualsValueClauseSyntax initializer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
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

		// Token: 0x06005742 RID: 22338 RVA: 0x001577A8 File Offset: 0x001559A8
		internal VariableDeclaratorSyntax(SyntaxKind kind, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BracketedArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax initializer, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
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

		// Token: 0x06005743 RID: 22339 RVA: 0x00157800 File Offset: 0x00155A00
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal VariableDeclaratorSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, BracketedArgumentListSyntax argumentList, EqualsValueClauseSyntax initializer)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
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

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x0015784E File Offset: 0x00155A4E
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06005745 RID: 22341 RVA: 0x00157856 File Offset: 0x00155A56
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BracketedArgumentListSyntax ArgumentList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06005746 RID: 22342 RVA: 0x0015785E File Offset: 0x00155A5E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public EqualsValueClauseSyntax Initializer
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x00157868 File Offset: 0x00155A68
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.identifier;
				break;
			case 1:
				greenNode = this.argumentList;
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

		// Token: 0x06005748 RID: 22344 RVA: 0x001578A7 File Offset: 0x00155AA7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new VariableDeclaratorSyntax(this, parent, position);
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x001578B1 File Offset: 0x00155AB1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitVariableDeclarator(this);
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x001578BA File Offset: 0x00155ABA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitVariableDeclarator(this);
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x001578C4 File Offset: 0x00155AC4
		public VariableDeclaratorSyntax Update(SyntaxToken identifier, BracketedArgumentListSyntax argumentList, EqualsValueClauseSyntax initializer)
		{
			if (identifier != this.Identifier || argumentList != this.ArgumentList || initializer != this.Initializer)
			{
				VariableDeclaratorSyntax variableDeclaratorSyntax = SyntaxFactory.VariableDeclarator(identifier, argumentList, initializer);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					variableDeclaratorSyntax = variableDeclaratorSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					variableDeclaratorSyntax = variableDeclaratorSyntax.WithAnnotationsGreen(annotations);
				}
				return variableDeclaratorSyntax;
			}
			return this;
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x00157924 File Offset: 0x00155B24
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new VariableDeclaratorSyntax(base.Kind, this.identifier, this.argumentList, this.initializer, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x0015794A File Offset: 0x00155B4A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new VariableDeclaratorSyntax(base.Kind, this.identifier, this.argumentList, this.initializer, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x00157970 File Offset: 0x00155B70
		internal VariableDeclaratorSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)reader.ReadValue();
			if (bracketedArgumentListSyntax != null)
			{
				base.AdjustFlagsAndWidth(bracketedArgumentListSyntax);
				this.argumentList = bracketedArgumentListSyntax;
			}
			EqualsValueClauseSyntax equalsValueClauseSyntax = (EqualsValueClauseSyntax)reader.ReadValue();
			if (equalsValueClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(equalsValueClauseSyntax);
				this.initializer = equalsValueClauseSyntax;
			}
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x001579DF File Offset: 0x00155BDF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.argumentList);
			writer.WriteValue(this.initializer);
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x00157A0C File Offset: 0x00155C0C
		static VariableDeclaratorSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(VariableDeclaratorSyntax), (ObjectReader r) => new VariableDeclaratorSyntax(r));
		}

		// Token: 0x04001709 RID: 5897
		internal readonly SyntaxToken identifier;

		// Token: 0x0400170A RID: 5898
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BracketedArgumentListSyntax argumentList;

		// Token: 0x0400170B RID: 5899
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly EqualsValueClauseSyntax initializer;
	}
}
