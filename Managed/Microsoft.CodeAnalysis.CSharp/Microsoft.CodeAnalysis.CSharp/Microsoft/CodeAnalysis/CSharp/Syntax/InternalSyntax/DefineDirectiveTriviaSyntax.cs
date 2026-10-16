using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000477 RID: 1143
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DefineDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005FB3 RID: 24499 RVA: 0x00172424 File Offset: 0x00170624
		internal DefineDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(defineKeyword);
			this.defineKeyword = defineKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x00172488 File Offset: 0x00170688
		internal DefineDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(defineKeyword);
			this.defineKeyword = defineKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x001724F0 File Offset: 0x001706F0
		internal DefineDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(defineKeyword);
			this.defineKeyword = defineKeyword;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06005FB6 RID: 24502 RVA: 0x0017254F File Offset: 0x0017074F
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06005FB7 RID: 24503 RVA: 0x00172557 File Offset: 0x00170757
		public SyntaxToken DefineKeyword
		{
			get
			{
				return this.defineKeyword;
			}
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06005FB8 RID: 24504 RVA: 0x0017255F File Offset: 0x0017075F
		public SyntaxToken Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06005FB9 RID: 24505 RVA: 0x00172567 File Offset: 0x00170767
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06005FBA RID: 24506 RVA: 0x0017256F File Offset: 0x0017076F
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x00172578 File Offset: 0x00170778
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.hashToken;
				break;
			case 1:
				syntaxToken = this.defineKeyword;
				break;
			case 2:
				syntaxToken = this.name;
				break;
			case 3:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x001725C4 File Offset: 0x001707C4
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DefineDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x001725CE File Offset: 0x001707CE
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDefineDirectiveTrivia(this);
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x001725D7 File Offset: 0x001707D7
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDefineDirectiveTrivia(this);
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x001725E0 File Offset: 0x001707E0
		public DefineDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || defineKeyword != this.DefineKeyword || name != this.Name || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				DefineDirectiveTriviaSyntax defineDirectiveTriviaSyntax = SyntaxFactory.DefineDirectiveTrivia(hashToken, defineKeyword, name, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					defineDirectiveTriviaSyntax = defineDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					defineDirectiveTriviaSyntax = defineDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return defineDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x0017264E File Offset: 0x0017084E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DefineDirectiveTriviaSyntax(base.Kind, this.hashToken, this.defineKeyword, this.name, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x00172680 File Offset: 0x00170880
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DefineDirectiveTriviaSyntax(base.Kind, this.hashToken, this.defineKeyword, this.name, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x001726B4 File Offset: 0x001708B4
		internal DefineDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.defineKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.name = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken4);
			this.endOfDirectiveToken = syntaxToken4;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x00172744 File Offset: 0x00170944
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.defineKeyword);
			writer.WriteValue(this.name);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x00172794 File Offset: 0x00170994
		static DefineDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DefineDirectiveTriviaSyntax), (ObjectReader r) => new DefineDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001917 RID: 6423
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001918 RID: 6424
		internal readonly SyntaxToken defineKeyword;

		// Token: 0x04001919 RID: 6425
		internal readonly SyntaxToken name;

		// Token: 0x0400191A RID: 6426
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x0400191B RID: 6427
		internal readonly bool isActive;
	}
}
