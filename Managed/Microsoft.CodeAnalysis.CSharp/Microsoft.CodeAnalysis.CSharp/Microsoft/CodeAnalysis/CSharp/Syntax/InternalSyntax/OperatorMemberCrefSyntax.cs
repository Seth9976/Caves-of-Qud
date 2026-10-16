using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000457 RID: 1111
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OperatorMemberCrefSyntax : MemberCrefSyntax
	{
		// Token: 0x06005DD7 RID: 24023 RVA: 0x0016D15C File Offset: 0x0016B35C
		internal OperatorMemberCrefSyntax(SyntaxKind kind, SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x0016D1AC File Offset: 0x0016B3AC
		internal OperatorMemberCrefSyntax(SyntaxKind kind, SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x0016D1FF File Offset: 0x0016B3FF
		internal OperatorMemberCrefSyntax(SyntaxKind kind, SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x0016D23F File Offset: 0x0016B43F
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return this.operatorKeyword;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x06005DDB RID: 24027 RVA: 0x0016D247 File Offset: 0x0016B447
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x06005DDC RID: 24028 RVA: 0x0016D24F File Offset: 0x0016B44F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public CrefParameterListSyntax Parameters
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x0016D258 File Offset: 0x0016B458
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.operatorKeyword;
				break;
			case 1:
				greenNode = this.operatorToken;
				break;
			case 2:
				greenNode = this.parameters;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x0016D297 File Offset: 0x0016B497
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new OperatorMemberCrefSyntax(this, parent, position);
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x0016D2A1 File Offset: 0x0016B4A1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOperatorMemberCref(this);
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x0016D2AA File Offset: 0x0016B4AA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOperatorMemberCref(this);
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x0016D2B4 File Offset: 0x0016B4B4
		public OperatorMemberCrefSyntax Update(SyntaxToken operatorKeyword, SyntaxToken operatorToken, CrefParameterListSyntax parameters)
		{
			if (operatorKeyword != this.OperatorKeyword || operatorToken != this.OperatorToken || parameters != this.Parameters)
			{
				OperatorMemberCrefSyntax operatorMemberCrefSyntax = SyntaxFactory.OperatorMemberCref(operatorKeyword, operatorToken, parameters);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					operatorMemberCrefSyntax = operatorMemberCrefSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					operatorMemberCrefSyntax = operatorMemberCrefSyntax.WithAnnotationsGreen(annotations);
				}
				return operatorMemberCrefSyntax;
			}
			return this;
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x0016D314 File Offset: 0x0016B514
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new OperatorMemberCrefSyntax(base.Kind, this.operatorKeyword, this.operatorToken, this.parameters, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x0016D33A File Offset: 0x0016B53A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new OperatorMemberCrefSyntax(base.Kind, this.operatorKeyword, this.operatorToken, this.parameters, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x0016D360 File Offset: 0x0016B560
		internal OperatorMemberCrefSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.operatorToken = syntaxToken2;
			CrefParameterListSyntax crefParameterListSyntax = (CrefParameterListSyntax)reader.ReadValue();
			if (crefParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(crefParameterListSyntax);
				this.parameters = crefParameterListSyntax;
			}
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x0016D3CC File Offset: 0x0016B5CC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.operatorKeyword);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.parameters);
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x0016D3F9 File Offset: 0x0016B5F9
		static OperatorMemberCrefSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(OperatorMemberCrefSyntax), (ObjectReader r) => new OperatorMemberCrefSyntax(r));
		}

		// Token: 0x040018B1 RID: 6321
		internal readonly SyntaxToken operatorKeyword;

		// Token: 0x040018B2 RID: 6322
		internal readonly SyntaxToken operatorToken;

		// Token: 0x040018B3 RID: 6323
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly CrefParameterListSyntax parameters;
	}
}
