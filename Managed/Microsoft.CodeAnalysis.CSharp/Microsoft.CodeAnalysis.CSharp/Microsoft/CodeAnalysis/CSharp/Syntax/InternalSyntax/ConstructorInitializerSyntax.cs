using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200043F RID: 1087
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConstructorInitializerSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005C7E RID: 23678 RVA: 0x00168FA5 File Offset: 0x001671A5
		internal ConstructorInitializerSyntax(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(thisOrBaseKeyword);
			this.thisOrBaseKeyword = thisOrBaseKeyword;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x00168FE8 File Offset: 0x001671E8
		internal ConstructorInitializerSyntax(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(thisOrBaseKeyword);
			this.thisOrBaseKeyword = thisOrBaseKeyword;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x00169037 File Offset: 0x00167237
		internal ConstructorInitializerSyntax(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			base.AdjustFlagsAndWidth(thisOrBaseKeyword);
			this.thisOrBaseKeyword = thisOrBaseKeyword;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x00169073 File Offset: 0x00167273
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x0016907B File Offset: 0x0016727B
		public SyntaxToken ThisOrBaseKeyword
		{
			get
			{
				return this.thisOrBaseKeyword;
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06005C83 RID: 23683 RVA: 0x00169083 File Offset: 0x00167283
		public ArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x06005C84 RID: 23684 RVA: 0x0016908C File Offset: 0x0016728C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.colonToken;
				break;
			case 1:
				greenNode = this.thisOrBaseKeyword;
				break;
			case 2:
				greenNode = this.argumentList;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x001690CB File Offset: 0x001672CB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConstructorInitializerSyntax(this, parent, position);
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x001690D5 File Offset: 0x001672D5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstructorInitializer(this);
		}

		// Token: 0x06005C87 RID: 23687 RVA: 0x001690DE File Offset: 0x001672DE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstructorInitializer(this);
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x001690E8 File Offset: 0x001672E8
		public ConstructorInitializerSyntax Update(SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
		{
			if (colonToken != this.ColonToken || thisOrBaseKeyword != this.ThisOrBaseKeyword || argumentList != this.ArgumentList)
			{
				ConstructorInitializerSyntax constructorInitializerSyntax = SyntaxFactory.ConstructorInitializer(base.Kind, colonToken, thisOrBaseKeyword, argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					constructorInitializerSyntax = constructorInitializerSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					constructorInitializerSyntax = constructorInitializerSyntax.WithAnnotationsGreen(annotations);
				}
				return constructorInitializerSyntax;
			}
			return this;
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x0016914E File Offset: 0x0016734E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConstructorInitializerSyntax(base.Kind, this.colonToken, this.thisOrBaseKeyword, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x00169174 File Offset: 0x00167374
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConstructorInitializerSyntax(base.Kind, this.colonToken, this.thisOrBaseKeyword, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x0016919C File Offset: 0x0016739C
		internal ConstructorInitializerSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.colonToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.thisOrBaseKeyword = syntaxToken2;
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(argumentListSyntax);
			this.argumentList = argumentListSyntax;
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x00169205 File Offset: 0x00167405
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.colonToken);
			writer.WriteValue(this.thisOrBaseKeyword);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x00169232 File Offset: 0x00167432
		static ConstructorInitializerSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConstructorInitializerSyntax), (ObjectReader r) => new ConstructorInitializerSyntax(r));
		}

		// Token: 0x04001865 RID: 6245
		internal readonly SyntaxToken colonToken;

		// Token: 0x04001866 RID: 6246
		internal readonly SyntaxToken thisOrBaseKeyword;

		// Token: 0x04001867 RID: 6247
		internal readonly ArgumentListSyntax argumentList;
	}
}
