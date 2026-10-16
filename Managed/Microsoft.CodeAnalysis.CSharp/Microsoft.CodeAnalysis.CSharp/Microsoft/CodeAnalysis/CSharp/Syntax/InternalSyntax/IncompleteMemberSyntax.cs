using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200044E RID: 1102
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IncompleteMemberSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005D68 RID: 23912 RVA: 0x0016C1F0 File Offset: 0x0016A3F0
		internal IncompleteMemberSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
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
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0016C248 File Offset: 0x0016A448
		internal IncompleteMemberSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
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
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x0016C2A4 File Offset: 0x0016A4A4
		internal IncompleteMemberSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 3;
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
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x0016C2F5 File Offset: 0x0016A4F5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06005D6C RID: 23916 RVA: 0x0016C302 File Offset: 0x0016A502
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x0016C30F File Offset: 0x0016A50F
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x0016C318 File Offset: 0x0016A518
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
				greenNode = this.type;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x0016C357 File Offset: 0x0016A557
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IncompleteMemberSyntax(this, parent, position);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x0016C361 File Offset: 0x0016A561
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIncompleteMember(this);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x0016C36A File Offset: 0x0016A56A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIncompleteMember(this);
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x0016C374 File Offset: 0x0016A574
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public IncompleteMemberSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || type != this.Type)
			{
				IncompleteMemberSyntax incompleteMemberSyntax = SyntaxFactory.IncompleteMember(attributeLists, modifiers, type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					incompleteMemberSyntax = incompleteMemberSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					incompleteMemberSyntax = incompleteMemberSyntax.WithAnnotationsGreen(annotations);
				}
				return incompleteMemberSyntax;
			}
			return this;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x0016C3DE File Offset: 0x0016A5DE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IncompleteMemberSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x0016C404 File Offset: 0x0016A604
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IncompleteMemberSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x0016C42C File Offset: 0x0016A62C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal IncompleteMemberSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
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
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			if (typeSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeSyntax);
				this.type = typeSyntax;
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x0016C49E File Offset: 0x0016A69E
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x0016C4CB File Offset: 0x0016A6CB
		static IncompleteMemberSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IncompleteMemberSyntax), (ObjectReader r) => new IncompleteMemberSyntax(r));
		}

		// Token: 0x040018A3 RID: 6307
		internal readonly GreenNode attributeLists;

		// Token: 0x040018A4 RID: 6308
		internal readonly GreenNode modifiers;

		// Token: 0x040018A5 RID: 6309
		internal readonly TypeSyntax type;
	}
}
