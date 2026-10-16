using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200046E RID: 1134
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IfDirectiveTriviaSyntax : ConditionalDirectiveTriviaSyntax
	{
		// Token: 0x06005F13 RID: 24339 RVA: 0x0017063C File Offset: 0x0016E83C
		internal IfDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(ifKeyword);
			this.ifKeyword = ifKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
			this.conditionValue = conditionValue;
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x001706B0 File Offset: 0x0016E8B0
		internal IfDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(ifKeyword);
			this.ifKeyword = ifKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
			this.conditionValue = conditionValue;
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x00170728 File Offset: 0x0016E928
		internal IfDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(ifKeyword);
			this.ifKeyword = ifKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
			this.conditionValue = conditionValue;
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06005F16 RID: 24342 RVA: 0x00170797 File Offset: 0x0016E997
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06005F17 RID: 24343 RVA: 0x0017079F File Offset: 0x0016E99F
		public SyntaxToken IfKeyword
		{
			get
			{
				return this.ifKeyword;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x06005F18 RID: 24344 RVA: 0x001707A7 File Offset: 0x0016E9A7
		public override ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x06005F19 RID: 24345 RVA: 0x001707AF File Offset: 0x0016E9AF
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06005F1A RID: 24346 RVA: 0x001707B7 File Offset: 0x0016E9B7
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (get) Token: 0x06005F1B RID: 24347 RVA: 0x001707BF File Offset: 0x0016E9BF
		public override bool BranchTaken
		{
			get
			{
				return this.branchTaken;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (get) Token: 0x06005F1C RID: 24348 RVA: 0x001707C7 File Offset: 0x0016E9C7
		public override bool ConditionValue
		{
			get
			{
				return this.conditionValue;
			}
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x001707D0 File Offset: 0x0016E9D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.hashToken;
				break;
			case 1:
				greenNode = this.ifKeyword;
				break;
			case 2:
				greenNode = this.condition;
				break;
			case 3:
				greenNode = this.endOfDirectiveToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x0017081C File Offset: 0x0016EA1C
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IfDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x00170826 File Offset: 0x0016EA26
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIfDirectiveTrivia(this);
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x0017082F File Offset: 0x0016EA2F
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIfDirectiveTrivia(this);
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x00170838 File Offset: 0x0016EA38
		public IfDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			if (hashToken != this.HashToken || ifKeyword != this.IfKeyword || condition != this.Condition || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				IfDirectiveTriviaSyntax ifDirectiveTriviaSyntax = SyntaxFactory.IfDirectiveTrivia(hashToken, ifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					ifDirectiveTriviaSyntax = ifDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					ifDirectiveTriviaSyntax = ifDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return ifDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x001708AC File Offset: 0x0016EAAC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IfDirectiveTriviaSyntax(base.Kind, this.hashToken, this.ifKeyword, this.condition, this.endOfDirectiveToken, this.isActive, this.branchTaken, this.conditionValue, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x001708F8 File Offset: 0x0016EAF8
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IfDirectiveTriviaSyntax(base.Kind, this.hashToken, this.ifKeyword, this.condition, this.endOfDirectiveToken, this.isActive, this.branchTaken, this.conditionValue, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x00170944 File Offset: 0x0016EB44
		internal IfDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.ifKeyword = syntaxToken2;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.condition = expressionSyntax;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.endOfDirectiveToken = syntaxToken3;
			this.isActive = reader.ReadBoolean();
			this.branchTaken = reader.ReadBoolean();
			this.conditionValue = reader.ReadBoolean();
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x001709EC File Offset: 0x0016EBEC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.ifKeyword);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
			writer.WriteBoolean(this.branchTaken);
			writer.WriteBoolean(this.conditionValue);
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x00170A54 File Offset: 0x0016EC54
		static IfDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IfDirectiveTriviaSyntax), (ObjectReader r) => new IfDirectiveTriviaSyntax(r));
		}

		// Token: 0x040018EC RID: 6380
		internal readonly SyntaxToken hashToken;

		// Token: 0x040018ED RID: 6381
		internal readonly SyntaxToken ifKeyword;

		// Token: 0x040018EE RID: 6382
		internal readonly ExpressionSyntax condition;

		// Token: 0x040018EF RID: 6383
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x040018F0 RID: 6384
		internal readonly bool isActive;

		// Token: 0x040018F1 RID: 6385
		internal readonly bool branchTaken;

		// Token: 0x040018F2 RID: 6386
		internal readonly bool conditionValue;
	}
}
