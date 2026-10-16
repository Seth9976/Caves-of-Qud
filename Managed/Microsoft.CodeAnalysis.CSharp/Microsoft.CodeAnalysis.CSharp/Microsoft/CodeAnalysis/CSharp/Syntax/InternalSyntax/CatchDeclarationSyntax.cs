using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000413 RID: 1043
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class CatchDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x060059B9 RID: 22969 RVA: 0x0015F86C File Offset: 0x0015DA6C
		internal CatchDeclarationSyntax(SyntaxKind kind, SyntaxToken openParenToken, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (identifier != null)
			{
				base.AdjustFlagsAndWidth(identifier);
				this.identifier = identifier;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x0015F8CC File Offset: 0x0015DACC
		internal CatchDeclarationSyntax(SyntaxKind kind, SyntaxToken openParenToken, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (identifier != null)
			{
				base.AdjustFlagsAndWidth(identifier);
				this.identifier = identifier;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x0015F930 File Offset: 0x0015DB30
		internal CatchDeclarationSyntax(SyntaxKind kind, SyntaxToken openParenToken, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (identifier != null)
			{
				base.AdjustFlagsAndWidth(identifier);
				this.identifier = identifier;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x0015F98B File Offset: 0x0015DB8B
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060059BD RID: 22973 RVA: 0x0015F993 File Offset: 0x0015DB93
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060059BE RID: 22974 RVA: 0x0015F99B File Offset: 0x0015DB9B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x0015F9A3 File Offset: 0x0015DBA3
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x0015F9AC File Offset: 0x0015DBAC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openParenToken;
				break;
			case 1:
				greenNode = this.type;
				break;
			case 2:
				greenNode = this.identifier;
				break;
			case 3:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x0015F9F8 File Offset: 0x0015DBF8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new CatchDeclarationSyntax(this, parent, position);
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x0015FA02 File Offset: 0x0015DC02
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCatchDeclaration(this);
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x0015FA0B File Offset: 0x0015DC0B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCatchDeclaration(this);
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x0015FA14 File Offset: 0x0015DC14
		public CatchDeclarationSyntax Update(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken closeParenToken)
		{
			if (openParenToken != this.OpenParenToken || type != this.Type || identifier != this.Identifier || closeParenToken != this.CloseParenToken)
			{
				CatchDeclarationSyntax catchDeclarationSyntax = SyntaxFactory.CatchDeclaration(openParenToken, type, identifier, closeParenToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					catchDeclarationSyntax = catchDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					catchDeclarationSyntax = catchDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return catchDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x0015FA80 File Offset: 0x0015DC80
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new CatchDeclarationSyntax(base.Kind, this.openParenToken, this.type, this.identifier, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x0015FAAC File Offset: 0x0015DCAC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new CatchDeclarationSyntax(base.Kind, this.openParenToken, this.type, this.identifier, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x0015FAD8 File Offset: 0x0015DCD8
		internal CatchDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.identifier = syntaxToken2;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.closeParenToken = syntaxToken3;
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x0015FB5E File Offset: 0x0015DD5E
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.type);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x0015FB97 File Offset: 0x0015DD97
		static CatchDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(CatchDeclarationSyntax), (ObjectReader r) => new CatchDeclarationSyntax(r));
		}

		// Token: 0x040017A8 RID: 6056
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040017A9 RID: 6057
		internal readonly TypeSyntax type;

		// Token: 0x040017AA RID: 6058
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken identifier;

		// Token: 0x040017AB RID: 6059
		internal readonly SyntaxToken closeParenToken;
	}
}
