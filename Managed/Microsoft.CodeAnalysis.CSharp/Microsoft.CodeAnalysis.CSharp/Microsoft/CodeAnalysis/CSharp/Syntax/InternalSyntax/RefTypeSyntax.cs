using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200038B RID: 907
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RefTypeSyntax : TypeSyntax
	{
		// Token: 0x060051AF RID: 20911 RVA: 0x00148928 File Offset: 0x00146B28
		internal RefTypeSyntax(SyntaxKind kind, SyntaxToken refKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken readOnlyKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(refKeyword);
			this.refKeyword = refKeyword;
			if (readOnlyKeyword != null)
			{
				base.AdjustFlagsAndWidth(readOnlyKeyword);
				this.readOnlyKeyword = readOnlyKeyword;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x060051B0 RID: 20912 RVA: 0x00148978 File Offset: 0x00146B78
		internal RefTypeSyntax(SyntaxKind kind, SyntaxToken refKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken readOnlyKeyword, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(refKeyword);
			this.refKeyword = refKeyword;
			if (readOnlyKeyword != null)
			{
				base.AdjustFlagsAndWidth(readOnlyKeyword);
				this.readOnlyKeyword = readOnlyKeyword;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x001489CA File Offset: 0x00146BCA
		internal RefTypeSyntax(SyntaxKind kind, SyntaxToken refKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken readOnlyKeyword, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(refKeyword);
			this.refKeyword = refKeyword;
			if (readOnlyKeyword != null)
			{
				base.AdjustFlagsAndWidth(readOnlyKeyword);
				this.readOnlyKeyword = readOnlyKeyword;
			}
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x00148A09 File Offset: 0x00146C09
		public SyntaxToken RefKeyword
		{
			get
			{
				return this.refKeyword;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x060051B3 RID: 20915 RVA: 0x00148A11 File Offset: 0x00146C11
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken ReadOnlyKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.readOnlyKeyword;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x060051B4 RID: 20916 RVA: 0x00148A19 File Offset: 0x00146C19
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x060051B5 RID: 20917 RVA: 0x00148A24 File Offset: 0x00146C24
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.refKeyword;
				break;
			case 1:
				greenNode = this.readOnlyKeyword;
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

		// Token: 0x060051B6 RID: 20918 RVA: 0x00148A63 File Offset: 0x00146C63
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new RefTypeSyntax(this, parent, position);
		}

		// Token: 0x060051B7 RID: 20919 RVA: 0x00148A6D File Offset: 0x00146C6D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitRefType(this);
		}

		// Token: 0x060051B8 RID: 20920 RVA: 0x00148A76 File Offset: 0x00146C76
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitRefType(this);
		}

		// Token: 0x060051B9 RID: 20921 RVA: 0x00148A80 File Offset: 0x00146C80
		public RefTypeSyntax Update(SyntaxToken refKeyword, SyntaxToken readOnlyKeyword, TypeSyntax type)
		{
			if (refKeyword != this.RefKeyword || readOnlyKeyword != this.ReadOnlyKeyword || type != this.Type)
			{
				RefTypeSyntax refTypeSyntax = SyntaxFactory.RefType(refKeyword, readOnlyKeyword, type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					refTypeSyntax = refTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					refTypeSyntax = refTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return refTypeSyntax;
			}
			return this;
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x00148AE0 File Offset: 0x00146CE0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new RefTypeSyntax(base.Kind, this.refKeyword, this.readOnlyKeyword, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x00148B06 File Offset: 0x00146D06
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new RefTypeSyntax(base.Kind, this.refKeyword, this.readOnlyKeyword, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060051BC RID: 20924 RVA: 0x00148B2C File Offset: 0x00146D2C
		internal RefTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.refKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.readOnlyKeyword = syntaxToken2;
			}
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x00148B98 File Offset: 0x00146D98
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.refKeyword);
			writer.WriteValue(this.readOnlyKeyword);
			writer.WriteValue(this.type);
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x00148BC5 File Offset: 0x00146DC5
		static RefTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(RefTypeSyntax), (ObjectReader r) => new RefTypeSyntax(r));
		}

		// Token: 0x04001603 RID: 5635
		internal readonly SyntaxToken refKeyword;

		// Token: 0x04001604 RID: 5636
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken readOnlyKeyword;

		// Token: 0x04001605 RID: 5637
		internal readonly TypeSyntax type;
	}
}
