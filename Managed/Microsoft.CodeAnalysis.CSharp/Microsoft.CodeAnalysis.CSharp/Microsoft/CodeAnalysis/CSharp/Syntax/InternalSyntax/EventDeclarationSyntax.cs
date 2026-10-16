using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000444 RID: 1092
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class EventDeclarationSyntax : BasePropertyDeclarationSyntax
	{
		// Token: 0x06005CCE RID: 23758 RVA: 0x0016A0F0 File Offset: 0x001682F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EventDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken eventKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(eventKeyword);
			this.eventKeyword = eventKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (accessorList != null)
			{
				base.AdjustFlagsAndWidth(accessorList);
				this.accessorList = accessorList;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x0016A1A0 File Offset: 0x001683A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EventDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken eventKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(eventKeyword);
			this.eventKeyword = eventKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (accessorList != null)
			{
				base.AdjustFlagsAndWidth(accessorList);
				this.accessorList = accessorList;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005CD0 RID: 23760 RVA: 0x0016A254 File Offset: 0x00168454
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal EventDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken eventKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(eventKeyword);
			this.eventKeyword = eventKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (accessorList != null)
			{
				base.AdjustFlagsAndWidth(accessorList);
				this.accessorList = accessorList;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x0016A2FD File Offset: 0x001684FD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x0016A30A File Offset: 0x0016850A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06005CD3 RID: 23763 RVA: 0x0016A317 File Offset: 0x00168517
		public SyntaxToken EventKeyword
		{
			get
			{
				return this.eventKeyword;
			}
		}

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x0016A31F File Offset: 0x0016851F
		public override TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06005CD5 RID: 23765 RVA: 0x0016A327 File Offset: 0x00168527
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.explicitInterfaceSpecifier;
			}
		}

		// Token: 0x1700158F RID: 5519
		// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x0016A32F File Offset: 0x0016852F
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06005CD7 RID: 23767 RVA: 0x0016A337 File Offset: 0x00168537
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override AccessorListSyntax AccessorList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.accessorList;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x0016A33F File Offset: 0x0016853F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x0016A348 File Offset: 0x00168548
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
				greenNode = this.eventKeyword;
				break;
			case 3:
				greenNode = this.type;
				break;
			case 4:
				greenNode = this.explicitInterfaceSpecifier;
				break;
			case 5:
				greenNode = this.identifier;
				break;
			case 6:
				greenNode = this.accessorList;
				break;
			case 7:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x0016A3C8 File Offset: 0x001685C8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new EventDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x0016A3D2 File Offset: 0x001685D2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitEventDeclaration(this);
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x0016A3DB File Offset: 0x001685DB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitEventDeclaration(this);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x0016A3E4 File Offset: 0x001685E4
		public EventDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken eventKeyword, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || eventKeyword != this.EventKeyword || type != this.Type || explicitInterfaceSpecifier != this.ExplicitInterfaceSpecifier || identifier != this.Identifier || accessorList != this.AccessorList || semicolonToken != this.SemicolonToken)
			{
				EventDeclarationSyntax eventDeclarationSyntax = SyntaxFactory.EventDeclaration(attributeLists, modifiers, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					eventDeclarationSyntax = eventDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					eventDeclarationSyntax = eventDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return eventDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x0016A48C File Offset: 0x0016868C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new EventDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.eventKeyword, this.type, this.explicitInterfaceSpecifier, this.identifier, this.accessorList, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x0016A4DC File Offset: 0x001686DC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new EventDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.eventKeyword, this.type, this.explicitInterfaceSpecifier, this.identifier, this.accessorList, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x0016A52C File Offset: 0x0016872C
		internal EventDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 8;
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
			this.eventKeyword = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = (ExplicitInterfaceSpecifierSyntax)reader.ReadValue();
			if (explicitInterfaceSpecifierSyntax != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifierSyntax);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifierSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
			AccessorListSyntax accessorListSyntax = (AccessorListSyntax)reader.ReadValue();
			if (accessorListSyntax != null)
			{
				base.AdjustFlagsAndWidth(accessorListSyntax);
				this.accessorList = accessorListSyntax;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken3 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken3);
				this.semicolonToken = syntaxToken3;
			}
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x0016A638 File Offset: 0x00168838
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.eventKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.explicitInterfaceSpecifier);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.accessorList);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x0016A6AC File Offset: 0x001688AC
		static EventDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(EventDeclarationSyntax), (ObjectReader r) => new EventDeclarationSyntax(r));
		}

		// Token: 0x0400187B RID: 6267
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400187C RID: 6268
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x0400187D RID: 6269
		internal readonly SyntaxToken eventKeyword;

		// Token: 0x0400187E RID: 6270
		internal readonly TypeSyntax type;

		// Token: 0x0400187F RID: 6271
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x04001880 RID: 6272
		internal readonly SyntaxToken identifier;

		// Token: 0x04001881 RID: 6273
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly AccessorListSyntax accessorList;

		// Token: 0x04001882 RID: 6274
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
