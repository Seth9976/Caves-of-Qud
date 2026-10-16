using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000356 RID: 854
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class NullableDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06004800 RID: 18432 RVA: 0x0011B08D File Offset: 0x0011928D
		internal NullableDirectiveTriviaSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x0011B098 File Offset: 0x00119298
		public override SyntaxToken HashToken
		{
			get
			{
				return new SyntaxToken(this, ((NullableDirectiveTriviaSyntax)base.Green).hashToken, base.Position, 0);
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x0011B0B7 File Offset: 0x001192B7
		public SyntaxToken NullableKeyword
		{
			get
			{
				return new SyntaxToken(this, ((NullableDirectiveTriviaSyntax)base.Green).nullableKeyword, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x0011B0DD File Offset: 0x001192DD
		public SyntaxToken SettingToken
		{
			get
			{
				return new SyntaxToken(this, ((NullableDirectiveTriviaSyntax)base.Green).settingToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x0011B104 File Offset: 0x00119304
		public SyntaxToken TargetToken
		{
			get
			{
				SyntaxToken targetToken = ((NullableDirectiveTriviaSyntax)base.Green).targetToken;
				if (targetToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, targetToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x0011B144 File Offset: 0x00119344
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return new SyntaxToken(this, ((NullableDirectiveTriviaSyntax)base.Green).endOfDirectiveToken, this.GetChildPosition(4), base.GetChildIndex(4));
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x0011B16A File Offset: 0x0011936A
		public override bool IsActive
		{
			get
			{
				return ((NullableDirectiveTriviaSyntax)base.Green).IsActive;
			}
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0011B17C File Offset: 0x0011937C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0011B17F File Offset: 0x0011937F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0011B182 File Offset: 0x00119382
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNullableDirectiveTrivia(this);
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0011B18B File Offset: 0x0011938B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNullableDirectiveTrivia(this);
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0011B194 File Offset: 0x00119394
		public NullableDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (!(hashToken != this.HashToken) && !(nullableKeyword != this.NullableKeyword) && !(settingToken != this.SettingToken) && !(targetToken != this.TargetToken) && !(endOfDirectiveToken != this.EndOfDirectiveToken))
			{
				return this;
			}
			NullableDirectiveTriviaSyntax nullableDirectiveTriviaSyntax = SyntaxFactory.NullableDirectiveTrivia(hashToken, nullableKeyword, settingToken, targetToken, endOfDirectiveToken, isActive);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return nullableDirectiveTriviaSyntax;
			}
			return nullableDirectiveTriviaSyntax.WithAnnotations(annotations);
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0011B211 File Offset: 0x00119411
		internal override DirectiveTriviaSyntax WithHashTokenCore(SyntaxToken hashToken)
		{
			return this.WithHashToken(hashToken);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0011B21A File Offset: 0x0011941A
		public new NullableDirectiveTriviaSyntax WithHashToken(SyntaxToken hashToken)
		{
			return this.Update(hashToken, this.NullableKeyword, this.SettingToken, this.TargetToken, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0011B241 File Offset: 0x00119441
		public NullableDirectiveTriviaSyntax WithNullableKeyword(SyntaxToken nullableKeyword)
		{
			return this.Update(this.HashToken, nullableKeyword, this.SettingToken, this.TargetToken, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x0011B268 File Offset: 0x00119468
		public NullableDirectiveTriviaSyntax WithSettingToken(SyntaxToken settingToken)
		{
			return this.Update(this.HashToken, this.NullableKeyword, settingToken, this.TargetToken, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0011B28F File Offset: 0x0011948F
		public NullableDirectiveTriviaSyntax WithTargetToken(SyntaxToken targetToken)
		{
			return this.Update(this.HashToken, this.NullableKeyword, this.SettingToken, targetToken, this.EndOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0011B2B6 File Offset: 0x001194B6
		internal override DirectiveTriviaSyntax WithEndOfDirectiveTokenCore(SyntaxToken endOfDirectiveToken)
		{
			return this.WithEndOfDirectiveToken(endOfDirectiveToken);
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0011B2BF File Offset: 0x001194BF
		public new NullableDirectiveTriviaSyntax WithEndOfDirectiveToken(SyntaxToken endOfDirectiveToken)
		{
			return this.Update(this.HashToken, this.NullableKeyword, this.SettingToken, this.TargetToken, endOfDirectiveToken, this.IsActive);
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0011B2E6 File Offset: 0x001194E6
		public NullableDirectiveTriviaSyntax WithIsActive(bool isActive)
		{
			return this.Update(this.HashToken, this.NullableKeyword, this.SettingToken, this.TargetToken, this.EndOfDirectiveToken, isActive);
		}
	}
}
