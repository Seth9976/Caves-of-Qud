using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000458 RID: 1112
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConversionOperatorMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x06005DE7 RID: 24039 RVA: 0x0016D41C File Offset: 0x0016B61C
		internal ConversionOperatorMemberCrefSyntax(SyntaxKind kind, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(implicitOrExplicitKeyword);
			this.implicitOrExplicitKeyword = implicitOrExplicitKeyword;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DE8 RID: 24040 RVA: 0x0016D47C File Offset: 0x0016B67C
		internal ConversionOperatorMemberCrefSyntax(SyntaxKind kind, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(implicitOrExplicitKeyword);
			this.implicitOrExplicitKeyword = implicitOrExplicitKeyword;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DE9 RID: 24041 RVA: 0x0016D4E0 File Offset: 0x0016B6E0
		internal ConversionOperatorMemberCrefSyntax(SyntaxKind kind, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(implicitOrExplicitKeyword);
			this.implicitOrExplicitKeyword = implicitOrExplicitKeyword;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x170015C7 RID: 5575
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x0016D53B File Offset: 0x0016B73B
		public SyntaxToken ImplicitOrExplicitKeyword
		{
			get
			{
				return this.implicitOrExplicitKeyword;
			}
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x0016D543 File Offset: 0x0016B743
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return this.operatorKeyword;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06005DEC RID: 24044 RVA: 0x0016D54B File Offset: 0x0016B74B
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x0016D553 File Offset: 0x0016B753
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x0016D55C File Offset: 0x0016B75C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.implicitOrExplicitKeyword;
				break;
			case 1:
				greenNode = this.operatorKeyword;
				break;
			case 2:
				greenNode = this.type;
				break;
			case 3:
				greenNode = this.parameters;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x0016D5A8 File Offset: 0x0016B7A8
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConversionOperatorMemberCrefSyntax(this, parent, position);
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x0016D5B2 File Offset: 0x0016B7B2
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConversionOperatorMemberCref(this);
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x0016D5BB File Offset: 0x0016B7BB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConversionOperatorMemberCref(this);
		}

		// Token: 0x06005DF2 RID: 24050 RVA: 0x0016D5C4 File Offset: 0x0016B7C4
		public ConversionOperatorMemberCrefSyntax Update(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, CrefParameterListSyntax parameters)
		{
			if (implicitOrExplicitKeyword != this.ImplicitOrExplicitKeyword || operatorKeyword != this.OperatorKeyword || type != this.Type || parameters != this.Parameters)
			{
				ConversionOperatorMemberCrefSyntax conversionOperatorMemberCrefSyntax = SyntaxFactory.ConversionOperatorMemberCref(implicitOrExplicitKeyword, operatorKeyword, type, parameters);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					conversionOperatorMemberCrefSyntax = conversionOperatorMemberCrefSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					conversionOperatorMemberCrefSyntax = conversionOperatorMemberCrefSyntax.WithAnnotationsGreen(annotations);
				}
				return conversionOperatorMemberCrefSyntax;
			}
			return this;
		}

		// Token: 0x06005DF3 RID: 24051 RVA: 0x0016D630 File Offset: 0x0016B830
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConversionOperatorMemberCrefSyntax(base.Kind, this.implicitOrExplicitKeyword, this.operatorKeyword, this.type, this.parameters, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x0016D65C File Offset: 0x0016B85C
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConversionOperatorMemberCrefSyntax(base.Kind, this.implicitOrExplicitKeyword, this.operatorKeyword, this.type, this.parameters, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x0016D688 File Offset: 0x0016B888
		internal ConversionOperatorMemberCrefSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.implicitOrExplicitKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.operatorKeyword = syntaxToken2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			CrefParameterListSyntax crefParameterListSyntax = (CrefParameterListSyntax)reader.ReadValue();
			if (crefParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(crefParameterListSyntax);
				this.parameters = crefParameterListSyntax;
			}
		}

		// Token: 0x06005DF6 RID: 24054 RVA: 0x0016D70E File Offset: 0x0016B90E
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.implicitOrExplicitKeyword);
			writer.WriteValue(this.operatorKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.parameters);
		}

		// Token: 0x06005DF7 RID: 24055 RVA: 0x0016D747 File Offset: 0x0016B947
		static ConversionOperatorMemberCrefSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConversionOperatorMemberCrefSyntax), (ObjectReader r) => new ConversionOperatorMemberCrefSyntax(r));
		}

		// Token: 0x040018B4 RID: 6324
		internal readonly SyntaxToken implicitOrExplicitKeyword;

		// Token: 0x040018B5 RID: 6325
		internal readonly SyntaxToken operatorKeyword;

		// Token: 0x040018B6 RID: 6326
		internal readonly TypeSyntax type;

		// Token: 0x040018B7 RID: 6327
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly CrefParameterListSyntax parameters;
	}
}
