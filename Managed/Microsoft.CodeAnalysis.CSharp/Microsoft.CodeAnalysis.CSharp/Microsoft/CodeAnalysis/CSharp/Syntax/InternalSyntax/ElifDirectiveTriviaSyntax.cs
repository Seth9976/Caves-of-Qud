using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200046F RID: 1135
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ElifDirectiveTriviaSyntax : ConditionalDirectiveTriviaSyntax
	{
		// Token: 0x06005F27 RID: 24359 RVA: 0x00170A78 File Offset: 0x0016EC78
		internal ElifDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(elifKeyword);
			this.elifKeyword = elifKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
			this.conditionValue = conditionValue;
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x00170AEC File Offset: 0x0016ECEC
		internal ElifDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(elifKeyword);
			this.elifKeyword = elifKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
			this.conditionValue = conditionValue;
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x00170B64 File Offset: 0x0016ED64
		internal ElifDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(elifKeyword);
			this.elifKeyword = elifKeyword;
			base.AdjustFlagsAndWidth(condition);
			this.condition = condition;
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
			this.branchTaken = branchTaken;
			this.conditionValue = conditionValue;
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06005F2A RID: 24362 RVA: 0x00170BD3 File Offset: 0x0016EDD3
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06005F2B RID: 24363 RVA: 0x00170BDB File Offset: 0x0016EDDB
		public SyntaxToken ElifKeyword
		{
			get
			{
				return this.elifKeyword;
			}
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06005F2C RID: 24364 RVA: 0x00170BE3 File Offset: 0x0016EDE3
		public override ExpressionSyntax Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x00170BEB File Offset: 0x0016EDEB
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06005F2E RID: 24366 RVA: 0x00170BF3 File Offset: 0x0016EDF3
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x00170BFB File Offset: 0x0016EDFB
		public override bool BranchTaken
		{
			get
			{
				return this.branchTaken;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x00170C03 File Offset: 0x0016EE03
		public override bool ConditionValue
		{
			get
			{
				return this.conditionValue;
			}
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x00170C0C File Offset: 0x0016EE0C
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
				greenNode = this.elifKeyword;
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

		// Token: 0x06005F32 RID: 24370 RVA: 0x00170C58 File Offset: 0x0016EE58
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ElifDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x00170C62 File Offset: 0x0016EE62
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitElifDirectiveTrivia(this);
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x00170C6B File Offset: 0x0016EE6B
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitElifDirectiveTrivia(this);
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x00170C74 File Offset: 0x0016EE74
		public ElifDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			if (hashToken != this.HashToken || elifKeyword != this.ElifKeyword || condition != this.Condition || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				ElifDirectiveTriviaSyntax elifDirectiveTriviaSyntax = SyntaxFactory.ElifDirectiveTrivia(hashToken, elifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					elifDirectiveTriviaSyntax = elifDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					elifDirectiveTriviaSyntax = elifDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return elifDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x00170CE8 File Offset: 0x0016EEE8
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ElifDirectiveTriviaSyntax(base.Kind, this.hashToken, this.elifKeyword, this.condition, this.endOfDirectiveToken, this.isActive, this.branchTaken, this.conditionValue, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x00170D34 File Offset: 0x0016EF34
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ElifDirectiveTriviaSyntax(base.Kind, this.hashToken, this.elifKeyword, this.condition, this.endOfDirectiveToken, this.isActive, this.branchTaken, this.conditionValue, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x00170D80 File Offset: 0x0016EF80
		internal ElifDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.elifKeyword = syntaxToken2;
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

		// Token: 0x06005F39 RID: 24377 RVA: 0x00170E28 File Offset: 0x0016F028
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.elifKeyword);
			writer.WriteValue(this.condition);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
			writer.WriteBoolean(this.branchTaken);
			writer.WriteBoolean(this.conditionValue);
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x00170E90 File Offset: 0x0016F090
		static ElifDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ElifDirectiveTriviaSyntax), (ObjectReader r) => new ElifDirectiveTriviaSyntax(r));
		}

		// Token: 0x040018F3 RID: 6387
		internal readonly SyntaxToken hashToken;

		// Token: 0x040018F4 RID: 6388
		internal readonly SyntaxToken elifKeyword;

		// Token: 0x040018F5 RID: 6389
		internal readonly ExpressionSyntax condition;

		// Token: 0x040018F6 RID: 6390
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x040018F7 RID: 6391
		internal readonly bool isActive;

		// Token: 0x040018F8 RID: 6392
		internal readonly bool branchTaken;

		// Token: 0x040018F9 RID: 6393
		internal readonly bool conditionValue;
	}
}
