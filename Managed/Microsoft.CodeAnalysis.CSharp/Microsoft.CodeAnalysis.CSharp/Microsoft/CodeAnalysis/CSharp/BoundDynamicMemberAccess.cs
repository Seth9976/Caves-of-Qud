using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D8 RID: 216
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDynamicMemberAccess : BoundExpression
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0005E315 File Offset: 0x0005C515
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Receiver);
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0005E324 File Offset: 0x0005C524
		public BoundDynamicMemberAccess(SyntaxNode syntax, BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> typeArgumentsOpt, string name, bool invoked, bool indexed, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DynamicMemberAccess, syntax, type, hasErrors || receiver.HasErrors())
		{
			this.Receiver = receiver;
			this.TypeArgumentsOpt = typeArgumentsOpt;
			this.Name = name;
			this.Invoked = invoked;
			this.Indexed = indexed;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0005E372 File Offset: 0x0005C572
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0005E37A File Offset: 0x0005C57A
		public BoundExpression Receiver { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0005E382 File Offset: 0x0005C582
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<TypeWithAnnotations> TypeArgumentsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0005E38A File Offset: 0x0005C58A
		public string Name { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600116C RID: 4460 RVA: 0x0005E392 File Offset: 0x0005C592
		public bool Invoked { get; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0005E39A File Offset: 0x0005C59A
		public bool Indexed { get; }

		// Token: 0x0600116E RID: 4462 RVA: 0x0005E3A2 File Offset: 0x0005C5A2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDynamicMemberAccess(this);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0005E3AC File Offset: 0x0005C5AC
		public BoundDynamicMemberAccess Update(BoundExpression receiver, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> typeArgumentsOpt, string name, bool invoked, bool indexed, TypeSymbol type)
		{
			if (receiver != this.Receiver || typeArgumentsOpt != this.TypeArgumentsOpt || name != this.Name || invoked != this.Invoked || indexed != this.Indexed || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDynamicMemberAccess boundDynamicMemberAccess = new BoundDynamicMemberAccess(this.Syntax, receiver, typeArgumentsOpt, name, invoked, indexed, type, base.HasErrors);
				boundDynamicMemberAccess.CopyAttributes(this);
				return boundDynamicMemberAccess;
			}
			return this;
		}
	}
}
