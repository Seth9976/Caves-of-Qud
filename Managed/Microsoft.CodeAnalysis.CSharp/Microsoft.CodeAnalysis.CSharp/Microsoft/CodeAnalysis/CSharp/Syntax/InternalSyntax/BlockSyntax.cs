using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E9 RID: 1001
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BlockSyntax : StatementSyntax
	{
		// Token: 0x060056F7 RID: 22263 RVA: 0x001565D4 File Offset: 0x001547D4
		internal BlockSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode statements, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (statements != null)
			{
				base.AdjustFlagsAndWidth(statements);
				this.statements = statements;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x00156638 File Offset: 0x00154838
		internal BlockSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode statements, SyntaxToken closeBraceToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (statements != null)
			{
				base.AdjustFlagsAndWidth(statements);
				this.statements = statements;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x001566A0 File Offset: 0x001548A0
		internal BlockSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode statements, SyntaxToken closeBraceToken)
			: base(kind)
		{
			base.SlotCount = 4;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			base.AdjustFlagsAndWidth(openBraceToken);
			this.openBraceToken = openBraceToken;
			if (statements != null)
			{
				base.AdjustFlagsAndWidth(statements);
				this.statements = statements;
			}
			base.AdjustFlagsAndWidth(closeBraceToken);
			this.closeBraceToken = closeBraceToken;
		}

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x001566FE File Offset: 0x001548FE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x0015670B File Offset: 0x0015490B
		public SyntaxToken OpenBraceToken
		{
			get
			{
				return this.openBraceToken;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x00156713 File Offset: 0x00154913
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<StatementSyntax> Statements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<StatementSyntax>(this.statements);
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060056FD RID: 22269 RVA: 0x00156720 File Offset: 0x00154920
		public SyntaxToken CloseBraceToken
		{
			get
			{
				return this.closeBraceToken;
			}
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x00156728 File Offset: 0x00154928
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
				greenNode = this.openBraceToken;
				break;
			case 2:
				greenNode = this.statements;
				break;
			case 3:
				greenNode = this.closeBraceToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x00156774 File Offset: 0x00154974
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BlockSyntax(this, parent, position);
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0015677E File Offset: 0x0015497E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBlock(this);
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x00156787 File Offset: 0x00154987
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBlock(this);
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x00156790 File Offset: 0x00154990
		public BlockSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			if (attributeLists != this.AttributeLists || openBraceToken != this.OpenBraceToken || statements != this.Statements || closeBraceToken != this.CloseBraceToken)
			{
				BlockSyntax blockSyntax = SyntaxFactory.Block(attributeLists, openBraceToken, statements, closeBraceToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					blockSyntax = blockSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					blockSyntax = blockSyntax.WithAnnotationsGreen(annotations);
				}
				return blockSyntax;
			}
			return this;
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x00156806 File Offset: 0x00154A06
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BlockSyntax(base.Kind, this.attributeLists, this.openBraceToken, this.statements, this.closeBraceToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x00156832 File Offset: 0x00154A32
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BlockSyntax(base.Kind, this.attributeLists, this.openBraceToken, this.statements, this.closeBraceToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00156860 File Offset: 0x00154A60
		internal BlockSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBraceToken = syntaxToken;
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.statements = greenNode2;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBraceToken = syntaxToken2;
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x001568E9 File Offset: 0x00154AE9
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.openBraceToken);
			writer.WriteValue(this.statements);
			writer.WriteValue(this.closeBraceToken);
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x00156922 File Offset: 0x00154B22
		static BlockSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BlockSyntax), (ObjectReader r) => new BlockSyntax(r));
		}

		// Token: 0x040016F3 RID: 5875
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040016F4 RID: 5876
		internal readonly SyntaxToken openBraceToken;

		// Token: 0x040016F5 RID: 5877
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode statements;

		// Token: 0x040016F6 RID: 5878
		internal readonly SyntaxToken closeBraceToken;
	}
}
