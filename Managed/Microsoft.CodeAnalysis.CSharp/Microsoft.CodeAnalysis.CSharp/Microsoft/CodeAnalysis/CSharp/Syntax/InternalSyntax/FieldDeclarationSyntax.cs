using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000437 RID: 1079
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FieldDeclarationSyntax : BaseFieldDeclarationSyntax
	{
		// Token: 0x06005BEC RID: 23532 RVA: 0x00166B88 File Offset: 0x00164D88
		internal FieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005BED RID: 23533 RVA: 0x00166BEC File Offset: 0x00164DEC
		internal FieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x00166C54 File Offset: 0x00164E54
		internal FieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(declaration);
			this.declaration = declaration;
			base.AdjustFlagsAndWidth(semicolonToken);
			this.semicolonToken = semicolonToken;
		}

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06005BEF RID: 23535 RVA: 0x00166CB1 File Offset: 0x00164EB1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06005BF0 RID: 23536 RVA: 0x00166CBE File Offset: 0x00164EBE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x00166CCB File Offset: 0x00164ECB
		public override VariableDeclarationSyntax Declaration
		{
			get
			{
				return this.declaration;
			}
		}

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06005BF2 RID: 23538 RVA: 0x00166CD3 File Offset: 0x00164ED3
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x00166CDC File Offset: 0x00164EDC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.modifiers;
				break;
			case 2:
				greenNode = this.declaration;
				break;
			case 3:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x00166D28 File Offset: 0x00164F28
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FieldDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x00166D32 File Offset: 0x00164F32
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFieldDeclaration(this);
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x00166D3B File Offset: 0x00164F3B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFieldDeclaration(this);
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x00166D44 File Offset: 0x00164F44
		public FieldDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || declaration != this.Declaration || semicolonToken != this.SemicolonToken)
			{
				FieldDeclarationSyntax fieldDeclarationSyntax = SyntaxFactory.FieldDeclaration(attributeLists, modifiers, declaration, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					fieldDeclarationSyntax = fieldDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					fieldDeclarationSyntax = fieldDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return fieldDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x00166DBA File Offset: 0x00164FBA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FieldDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.declaration, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x00166DE6 File Offset: 0x00164FE6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FieldDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.declaration, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005BFA RID: 23546 RVA: 0x00166E14 File Offset: 0x00165014
		internal FieldDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(variableDeclarationSyntax);
			this.declaration = variableDeclarationSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.semicolonToken = syntaxToken;
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x00166E9D File Offset: 0x0016509D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.declaration);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x00166ED6 File Offset: 0x001650D6
		static FieldDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FieldDeclarationSyntax), (ObjectReader r) => new FieldDeclarationSyntax(r));
		}

		// Token: 0x04001835 RID: 6197
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001836 RID: 6198
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001837 RID: 6199
		internal readonly VariableDeclarationSyntax declaration;

		// Token: 0x04001838 RID: 6200
		internal readonly SyntaxToken semicolonToken;
	}
}
