using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200042B RID: 1067
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EnumMemberDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005B57 RID: 23383 RVA: 0x001656B8 File Offset: 0x001638B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EnumMemberDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, EqualsValueClauseSyntax equalsValue, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (equalsValue != null)
			{
				base.AdjustFlagsAndWidth(equalsValue);
				this.equalsValue = equalsValue;
			}
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x00165720 File Offset: 0x00163920
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EnumMemberDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, EqualsValueClauseSyntax equalsValue, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (equalsValue != null)
			{
				base.AdjustFlagsAndWidth(equalsValue);
				this.equalsValue = equalsValue;
			}
		}

		// Token: 0x06005B59 RID: 23385 RVA: 0x0016578C File Offset: 0x0016398C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EnumMemberDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (equalsValue != null)
			{
				base.AdjustFlagsAndWidth(equalsValue);
				this.equalsValue = equalsValue;
			}
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x001657ED File Offset: 0x001639ED
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06005B5B RID: 23387 RVA: 0x001657FA File Offset: 0x001639FA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x00165807 File Offset: 0x00163A07
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06005B5D RID: 23389 RVA: 0x0016580F File Offset: 0x00163A0F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public EqualsValueClauseSyntax EqualsValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.equalsValue;
			}
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x00165818 File Offset: 0x00163A18
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
				greenNode = this.identifier;
				break;
			case 3:
				greenNode = this.equalsValue;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x00165864 File Offset: 0x00163A64
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EnumMemberDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x0016586E File Offset: 0x00163A6E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEnumMemberDeclaration(this);
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x00165877 File Offset: 0x00163A77
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEnumMemberDeclaration(this);
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00165880 File Offset: 0x00163A80
		public EnumMemberDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken identifier, EqualsValueClauseSyntax equalsValue)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || identifier != this.Identifier || equalsValue != this.EqualsValue)
			{
				EnumMemberDeclarationSyntax enumMemberDeclarationSyntax = SyntaxFactory.EnumMemberDeclaration(attributeLists, modifiers, identifier, equalsValue);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					enumMemberDeclarationSyntax = enumMemberDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					enumMemberDeclarationSyntax = enumMemberDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return enumMemberDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005B63 RID: 23395 RVA: 0x001658F6 File Offset: 0x00163AF6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EnumMemberDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.identifier, this.equalsValue, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B64 RID: 23396 RVA: 0x00165922 File Offset: 0x00163B22
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EnumMemberDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.identifier, this.equalsValue, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x00165950 File Offset: 0x00163B50
		internal EnumMemberDeclarationSyntax(ObjectReader reader)
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
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			EqualsValueClauseSyntax equalsValueClauseSyntax = (EqualsValueClauseSyntax)reader.ReadValue();
			if (equalsValueClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(equalsValueClauseSyntax);
				this.equalsValue = equalsValueClauseSyntax;
			}
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x001659DC File Offset: 0x00163BDC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.equalsValue);
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x00165A15 File Offset: 0x00163C15
		static EnumMemberDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EnumMemberDeclarationSyntax), (ObjectReader r) => new EnumMemberDeclarationSyntax(r));
		}

		// Token: 0x04001821 RID: 6177
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001822 RID: 6178
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001823 RID: 6179
		internal readonly SyntaxToken identifier;

		// Token: 0x04001824 RID: 6180
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly EqualsValueClauseSyntax equalsValue;
	}
}
