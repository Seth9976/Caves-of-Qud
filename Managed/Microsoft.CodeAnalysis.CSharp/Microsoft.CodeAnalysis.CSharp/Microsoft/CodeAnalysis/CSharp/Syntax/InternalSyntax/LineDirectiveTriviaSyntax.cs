using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000479 RID: 1145
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LineDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005FD7 RID: 24535 RVA: 0x00172B4C File Offset: 0x00170D4C
		internal LineDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(lineKeyword);
			this.lineKeyword = lineKeyword;
			base.AdjustFlagsAndWidth(line);
			this.line = line;
			if (file != null)
			{
				base.AdjustFlagsAndWidth(file);
				this.file = file;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FD8 RID: 24536 RVA: 0x00172BC4 File Offset: 0x00170DC4
		internal LineDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(lineKeyword);
			this.lineKeyword = lineKeyword;
			base.AdjustFlagsAndWidth(line);
			this.line = line;
			if (file != null)
			{
				base.AdjustFlagsAndWidth(file);
				this.file = file;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06005FD9 RID: 24537 RVA: 0x00172C40 File Offset: 0x00170E40
		internal LineDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(lineKeyword);
			this.lineKeyword = lineKeyword;
			base.AdjustFlagsAndWidth(line);
			this.line = line;
			if (file != null)
			{
				base.AdjustFlagsAndWidth(file);
				this.file = file;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700163C RID: 5692
		// (get) Token: 0x06005FDA RID: 24538 RVA: 0x00172CB3 File Offset: 0x00170EB3
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700163D RID: 5693
		// (get) Token: 0x06005FDB RID: 24539 RVA: 0x00172CBB File Offset: 0x00170EBB
		public SyntaxToken LineKeyword
		{
			get
			{
				return this.lineKeyword;
			}
		}

		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06005FDC RID: 24540 RVA: 0x00172CC3 File Offset: 0x00170EC3
		public SyntaxToken Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x00172CCB File Offset: 0x00170ECB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken File
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.file;
			}
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06005FDE RID: 24542 RVA: 0x00172CD3 File Offset: 0x00170ED3
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06005FDF RID: 24543 RVA: 0x00172CDB File Offset: 0x00170EDB
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x00172CE4 File Offset: 0x00170EE4
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
				syntaxToken = this.lineKeyword;
				break;
			case 2:
				syntaxToken = this.line;
				break;
			case 3:
				syntaxToken = this.file;
				break;
			case 4:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x00172D3D File Offset: 0x00170F3D
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new LineDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x00172D47 File Offset: 0x00170F47
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLineDirectiveTrivia(this);
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x00172D50 File Offset: 0x00170F50
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLineDirectiveTrivia(this);
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x00172D5C File Offset: 0x00170F5C
		public LineDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || lineKeyword != this.LineKeyword || line != this.Line || file != this.File || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				LineDirectiveTriviaSyntax lineDirectiveTriviaSyntax = SyntaxFactory.LineDirectiveTrivia(hashToken, lineKeyword, line, file, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					lineDirectiveTriviaSyntax = lineDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					lineDirectiveTriviaSyntax = lineDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return lineDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x00172DD8 File Offset: 0x00170FD8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new LineDirectiveTriviaSyntax(base.Kind, this.hashToken, this.lineKeyword, this.line, this.file, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x00172E1C File Offset: 0x0017101C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new LineDirectiveTriviaSyntax(base.Kind, this.hashToken, this.lineKeyword, this.line, this.file, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x00172E60 File Offset: 0x00171060
		internal LineDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.lineKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.line = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken4 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken4);
				this.file = syntaxToken4;
			}
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.endOfDirectiveToken = syntaxToken5;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x00172F10 File Offset: 0x00171110
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.lineKeyword);
			writer.WriteValue(this.line);
			writer.WriteValue(this.file);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x00172F6C File Offset: 0x0017116C
		static LineDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(LineDirectiveTriviaSyntax), (ObjectReader r) => new LineDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001921 RID: 6433
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001922 RID: 6434
		internal readonly SyntaxToken lineKeyword;

		// Token: 0x04001923 RID: 6435
		internal readonly SyntaxToken line;

		// Token: 0x04001924 RID: 6436
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken file;

		// Token: 0x04001925 RID: 6437
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001926 RID: 6438
		internal readonly bool isActive;
	}
}
