using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C7 RID: 455
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDynamicObjectInitializerMember : BoundExpression
	{
		// Token: 0x06001FC2 RID: 8130 RVA: 0x000A2FF8 File Offset: 0x000A11F8
		public BoundDynamicObjectInitializerMember(SyntaxNode syntax, string memberName, TypeSymbol receiverType, TypeSymbol type, bool hasErrors)
			: base(BoundKind.DynamicObjectInitializerMember, syntax, type, hasErrors)
		{
			this.MemberName = memberName;
			this.ReceiverType = receiverType;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000A3018 File Offset: 0x000A1218
		public BoundDynamicObjectInitializerMember(SyntaxNode syntax, string memberName, TypeSymbol receiverType, TypeSymbol type)
			: base(BoundKind.DynamicObjectInitializerMember, syntax, type)
		{
			this.MemberName = memberName;
			this.ReceiverType = receiverType;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x000A3036 File Offset: 0x000A1236
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x000A303E File Offset: 0x000A123E
		public string MemberName { get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001FC6 RID: 8134 RVA: 0x000A3046 File Offset: 0x000A1246
		public TypeSymbol ReceiverType { get; }

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000A304E File Offset: 0x000A124E
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDynamicObjectInitializerMember(this);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000A3058 File Offset: 0x000A1258
		public BoundDynamicObjectInitializerMember Update(string memberName, TypeSymbol receiverType, TypeSymbol type)
		{
			if (memberName != this.MemberName || !TypeSymbol.Equals(receiverType, this.ReceiverType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDynamicObjectInitializerMember boundDynamicObjectInitializerMember = new BoundDynamicObjectInitializerMember(this.Syntax, memberName, receiverType, type, base.HasErrors);
				boundDynamicObjectInitializerMember.CopyAttributes(this);
				return boundDynamicObjectInitializerMember;
			}
			return this;
		}
	}
}
