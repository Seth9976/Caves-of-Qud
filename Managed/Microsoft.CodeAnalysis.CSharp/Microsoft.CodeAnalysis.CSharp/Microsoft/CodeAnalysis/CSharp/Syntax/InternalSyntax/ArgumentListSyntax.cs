using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003AA RID: 938
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArgumentListSyntax : BaseArgumentListSyntax
	{
		// Token: 0x06005367 RID: 21351 RVA: 0x0014CF30 File Offset: 0x0014B130
		internal ArgumentListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0014CF80 File Offset: 0x0014B180
		internal ArgumentListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0014CFD2 File Offset: 0x0014B1D2
		internal ArgumentListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x0014D011 File Offset: 0x0014B211
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x0600536B RID: 21355 RVA: 0x0014D019 File Offset: 0x0014B219
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ArgumentSyntax>(new SyntaxList<CSharpSyntaxNode>(this.arguments));
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x0014D030 File Offset: 0x0014B230
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0014D038 File Offset: 0x0014B238
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
				greenNode = this.arguments;
				break;
			case 2:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0014D077 File Offset: 0x0014B277
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ArgumentListSyntax(this, parent, position);
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0014D081 File Offset: 0x0014B281
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArgumentList(this);
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0014D08A File Offset: 0x0014B28A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArgumentList(this);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0014D094 File Offset: 0x0014B294
		public ArgumentListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<ArgumentSyntax> separatedSyntaxList = this.Arguments;
				if (!((in arguments) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			ArgumentListSyntax argumentListSyntax = SyntaxFactory.ArgumentList(openParenToken, arguments, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				argumentListSyntax = argumentListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				argumentListSyntax = argumentListSyntax.WithAnnotationsGreen(annotations);
			}
			return argumentListSyntax;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0014D0FD File Offset: 0x0014B2FD
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ArgumentListSyntax(base.Kind, this.openParenToken, this.arguments, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0014D123 File Offset: 0x0014B323
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ArgumentListSyntax(base.Kind, this.openParenToken, this.arguments, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0014D14C File Offset: 0x0014B34C
		internal ArgumentListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.arguments = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0014D1B8 File Offset: 0x0014B3B8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.arguments);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0014D1E5 File Offset: 0x0014B3E5
		static ArgumentListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ArgumentListSyntax), (ObjectReader r) => new ArgumentListSyntax(r));
		}

		// Token: 0x0400164F RID: 5711
		internal readonly SyntaxToken openParenToken;

		// Token: 0x04001650 RID: 5712
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode arguments;

		// Token: 0x04001651 RID: 5713
		internal readonly SyntaxToken closeParenToken;
	}
}
