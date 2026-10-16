using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200042C RID: 1068
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BaseListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005B68 RID: 23400 RVA: 0x00165A36 File Offset: 0x00163C36
		internal BaseListSyntax(SyntaxKind kind, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode types, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			if (types != null)
			{
				base.AdjustFlagsAndWidth(types);
				this.types = types;
			}
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x00165A69 File Offset: 0x00163C69
		internal BaseListSyntax(SyntaxKind kind, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode types, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			if (types != null)
			{
				base.AdjustFlagsAndWidth(types);
				this.types = types;
			}
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x00165AA0 File Offset: 0x00163CA0
		internal BaseListSyntax(SyntaxKind kind, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode types)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
			if (types != null)
			{
				base.AdjustFlagsAndWidth(types);
				this.types = types;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06005B6B RID: 23403 RVA: 0x00165ACF File Offset: 0x00163CCF
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06005B6C RID: 23404 RVA: 0x00165AD7 File Offset: 0x00163CD7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<BaseTypeSyntax> Types
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<BaseTypeSyntax>(new SyntaxList<CSharpSyntaxNode>(this.types));
			}
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x00165AF0 File Offset: 0x00163CF0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.types;
				}
			}
			else
			{
				greenNode = this.colonToken;
			}
			return greenNode;
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x00165B1B File Offset: 0x00163D1B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BaseListSyntax(this, parent, position);
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x00165B25 File Offset: 0x00163D25
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBaseList(this);
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x00165B2E File Offset: 0x00163D2E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBaseList(this);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x00165B38 File Offset: 0x00163D38
		public BaseListSyntax Update(SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<BaseTypeSyntax> types)
		{
			if (colonToken == this.ColonToken)
			{
				SeparatedSyntaxList<BaseTypeSyntax> separatedSyntaxList = this.Types;
				if (!((in types) != (in separatedSyntaxList)))
				{
					return this;
				}
			}
			BaseListSyntax baseListSyntax = SyntaxFactory.BaseList(colonToken, types);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				baseListSyntax = baseListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				baseListSyntax = baseListSyntax.WithAnnotationsGreen(annotations);
			}
			return baseListSyntax;
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x00165B97 File Offset: 0x00163D97
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BaseListSyntax(base.Kind, this.colonToken, this.types, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x00165BB7 File Offset: 0x00163DB7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BaseListSyntax(base.Kind, this.colonToken, this.types, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x00165BD8 File Offset: 0x00163DD8
		internal BaseListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.colonToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.types = greenNode;
			}
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x00165C2A File Offset: 0x00163E2A
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.colonToken);
			writer.WriteValue(this.types);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00165C4B File Offset: 0x00163E4B
		static BaseListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BaseListSyntax), (ObjectReader r) => new BaseListSyntax(r));
		}

		// Token: 0x04001825 RID: 6181
		internal readonly SyntaxToken colonToken;

		// Token: 0x04001826 RID: 6182
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode types;
	}
}
