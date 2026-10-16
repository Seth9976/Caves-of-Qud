using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E7 RID: 999
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class GlobalStatementSyntax : MemberDeclarationSyntax
	{
		// Token: 0x060056E3 RID: 22243 RVA: 0x001562C8 File Offset: 0x001544C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal GlobalStatementSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x0015631C File Offset: 0x0015451C
		internal GlobalStatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, StatementSyntax statement, SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x00156374 File Offset: 0x00154574
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal GlobalStatementSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] StatementSyntax statement)
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
			base.AdjustFlagsAndWidth(statement);
			this.statement = statement;
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x001563C1 File Offset: 0x001545C1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x001563CE File Offset: 0x001545CE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x001563DB File Offset: 0x001545DB
		public StatementSyntax Statement
		{
			get
			{
				return this.statement;
			}
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x001563E4 File Offset: 0x001545E4
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
				greenNode = this.statement;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x00156423 File Offset: 0x00154623
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new GlobalStatementSyntax(this, parent, position);
		}

		// Token: 0x060056EB RID: 22251 RVA: 0x0015642D File Offset: 0x0015462D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGlobalStatement(this);
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x00156436 File Offset: 0x00154636
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGlobalStatement(this);
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x00156440 File Offset: 0x00154640
		public GlobalStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, StatementSyntax statement)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || statement != this.Statement)
			{
				GlobalStatementSyntax globalStatementSyntax = SyntaxFactory.GlobalStatement(attributeLists, modifiers, statement);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					globalStatementSyntax = globalStatementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					globalStatementSyntax = globalStatementSyntax.WithAnnotationsGreen(annotations);
				}
				return globalStatementSyntax;
			}
			return this;
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x001564AA File Offset: 0x001546AA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new GlobalStatementSyntax(base.Kind, this.attributeLists, this.modifiers, this.statement, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x001564D0 File Offset: 0x001546D0
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new GlobalStatementSyntax(base.Kind, this.attributeLists, this.modifiers, this.statement, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x001564F8 File Offset: 0x001546F8
		internal GlobalStatementSyntax(ObjectReader reader)
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
			StatementSyntax statementSyntax = (StatementSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(statementSyntax);
			this.statement = statementSyntax;
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x00156567 File Offset: 0x00154767
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.statement);
		}

		// Token: 0x060056F2 RID: 22258 RVA: 0x00156594 File Offset: 0x00154794
		static GlobalStatementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(GlobalStatementSyntax), (ObjectReader r) => new GlobalStatementSyntax(r));
		}

		// Token: 0x040016F0 RID: 5872
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040016F1 RID: 5873
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x040016F2 RID: 5874
		internal readonly StatementSyntax statement;
	}
}
