using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E2 RID: 226
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundMethodOrPropertyGroup : BoundExpression
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0005EB64 File Offset: 0x0005CD64
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.ReceiverOpt);
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0005EB71 File Offset: 0x0005CD71
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BoundMethodOrPropertyGroup(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, LookupResultKind resultKind, bool hasErrors = false)
			: base(kind, syntax, null, hasErrors)
		{
			this.ReceiverOpt = receiverOpt;
			this._ResultKind = resultKind;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0005EB8D File Offset: 0x0005CD8D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x0005EB95 File Offset: 0x0005CD95
		public BoundExpression ReceiverOpt { get; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0005EB9D File Offset: 0x0005CD9D
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x040005AF RID: 1455
		private readonly LookupResultKind _ResultKind;
	}
}
