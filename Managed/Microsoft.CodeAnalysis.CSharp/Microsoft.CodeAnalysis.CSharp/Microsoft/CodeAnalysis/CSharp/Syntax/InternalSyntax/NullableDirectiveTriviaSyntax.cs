using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200047F RID: 1151
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NullableDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06006048 RID: 24648 RVA: 0x001743E4 File Offset: 0x001725E4
		internal NullableDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(nullableKeyword);
			this.nullableKeyword = nullableKeyword;
			base.AdjustFlagsAndWidth(settingToken);
			this.settingToken = settingToken;
			if (targetToken != null)
			{
				base.AdjustFlagsAndWidth(targetToken);
				this.targetToken = targetToken;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x0017445C File Offset: 0x0017265C
		internal NullableDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(nullableKeyword);
			this.nullableKeyword = nullableKeyword;
			base.AdjustFlagsAndWidth(settingToken);
			this.settingToken = settingToken;
			if (targetToken != null)
			{
				base.AdjustFlagsAndWidth(targetToken);
				this.targetToken = targetToken;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x001744D8 File Offset: 0x001726D8
		internal NullableDirectiveTriviaSyntax(SyntaxKind kind, SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
			: base(kind)
		{
			base.SlotCount = 5;
			base.AdjustFlagsAndWidth(hashToken);
			this.hashToken = hashToken;
			base.AdjustFlagsAndWidth(nullableKeyword);
			this.nullableKeyword = nullableKeyword;
			base.AdjustFlagsAndWidth(settingToken);
			this.settingToken = settingToken;
			if (targetToken != null)
			{
				base.AdjustFlagsAndWidth(targetToken);
				this.targetToken = targetToken;
			}
			base.AdjustFlagsAndWidth(endOfDirectiveToken);
			this.endOfDirectiveToken = endOfDirectiveToken;
			this.isActive = isActive;
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x0017454B File Offset: 0x0017274B
		public override SyntaxToken HashToken
		{
			get
			{
				return this.hashToken;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x0600604C RID: 24652 RVA: 0x00174553 File Offset: 0x00172753
		public SyntaxToken NullableKeyword
		{
			get
			{
				return this.nullableKeyword;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x0600604D RID: 24653 RVA: 0x0017455B File Offset: 0x0017275B
		public SyntaxToken SettingToken
		{
			get
			{
				return this.settingToken;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x0600604E RID: 24654 RVA: 0x00174563 File Offset: 0x00172763
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken TargetToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.targetToken;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x0600604F RID: 24655 RVA: 0x0017456B File Offset: 0x0017276B
		public override SyntaxToken EndOfDirectiveToken
		{
			get
			{
				return this.endOfDirectiveToken;
			}
		}

		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06006050 RID: 24656 RVA: 0x00174573 File Offset: 0x00172773
		public override bool IsActive
		{
			get
			{
				return this.isActive;
			}
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x0017457C File Offset: 0x0017277C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			SyntaxToken syntaxToken;
			switch (index)
			{
			case 0:
				syntaxToken = this.hashToken;
				break;
			case 1:
				syntaxToken = this.nullableKeyword;
				break;
			case 2:
				syntaxToken = this.settingToken;
				break;
			case 3:
				syntaxToken = this.targetToken;
				break;
			case 4:
				syntaxToken = this.endOfDirectiveToken;
				break;
			default:
				syntaxToken = null;
				break;
			}
			return syntaxToken;
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001745D5 File Offset: 0x001727D5
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new NullableDirectiveTriviaSyntax(this, parent, position);
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x001745DF File Offset: 0x001727DF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNullableDirectiveTrivia(this);
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x001745E8 File Offset: 0x001727E8
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNullableDirectiveTrivia(this);
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x001745F4 File Offset: 0x001727F4
		public NullableDirectiveTriviaSyntax Update(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			if (hashToken != this.HashToken || nullableKeyword != this.NullableKeyword || settingToken != this.SettingToken || targetToken != this.TargetToken || endOfDirectiveToken != this.EndOfDirectiveToken)
			{
				NullableDirectiveTriviaSyntax nullableDirectiveTriviaSyntax = SyntaxFactory.NullableDirectiveTrivia(hashToken, nullableKeyword, settingToken, targetToken, endOfDirectiveToken, isActive);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					nullableDirectiveTriviaSyntax = nullableDirectiveTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					nullableDirectiveTriviaSyntax = nullableDirectiveTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return nullableDirectiveTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x00174670 File Offset: 0x00172870
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new NullableDirectiveTriviaSyntax(base.Kind, this.hashToken, this.nullableKeyword, this.settingToken, this.targetToken, this.endOfDirectiveToken, this.isActive, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x001746B4 File Offset: 0x001728B4
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new NullableDirectiveTriviaSyntax(base.Kind, this.hashToken, this.nullableKeyword, this.settingToken, this.targetToken, this.endOfDirectiveToken, this.isActive, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x001746F8 File Offset: 0x001728F8
		internal NullableDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.hashToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.nullableKeyword = syntaxToken2;
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken3);
			this.settingToken = syntaxToken3;
			SyntaxToken syntaxToken4 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken4 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken4);
				this.targetToken = syntaxToken4;
			}
			SyntaxToken syntaxToken5 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken5);
			this.endOfDirectiveToken = syntaxToken5;
			this.isActive = reader.ReadBoolean();
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x001747A8 File Offset: 0x001729A8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.hashToken);
			writer.WriteValue(this.nullableKeyword);
			writer.WriteValue(this.settingToken);
			writer.WriteValue(this.targetToken);
			writer.WriteValue(this.endOfDirectiveToken);
			writer.WriteBoolean(this.isActive);
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x00174804 File Offset: 0x00172A04
		static NullableDirectiveTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(NullableDirectiveTriviaSyntax), (ObjectReader r) => new NullableDirectiveTriviaSyntax(r));
		}

		// Token: 0x04001944 RID: 6468
		internal readonly SyntaxToken hashToken;

		// Token: 0x04001945 RID: 6469
		internal readonly SyntaxToken nullableKeyword;

		// Token: 0x04001946 RID: 6470
		internal readonly SyntaxToken settingToken;

		// Token: 0x04001947 RID: 6471
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken targetToken;

		// Token: 0x04001948 RID: 6472
		internal readonly SyntaxToken endOfDirectiveToken;

		// Token: 0x04001949 RID: 6473
		internal readonly bool isActive;
	}
}
