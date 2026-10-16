using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C1 RID: 449
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundEventAssignmentOperator : BoundExpression
	{
		// Token: 0x06001FA3 RID: 8099 RVA: 0x000A2C28 File Offset: 0x000A0E28
		public BoundEventAssignmentOperator(SyntaxNode syntax, EventSymbol @event, bool isAddition, bool isDynamic, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, BoundExpression argument, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.EventAssignmentOperator, syntax, type, hasErrors || receiverOpt.HasErrors() || argument.HasErrors())
		{
			this.Event = @event;
			this.IsAddition = isAddition;
			this.IsDynamic = isDynamic;
			this.ReceiverOpt = receiverOpt;
			this.Argument = argument;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x000A2C80 File Offset: 0x000A0E80
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x000A2C88 File Offset: 0x000A0E88
		public EventSymbol Event { get; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x000A2C90 File Offset: 0x000A0E90
		public bool IsAddition { get; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x000A2C98 File Offset: 0x000A0E98
		public bool IsDynamic { get; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x000A2CA0 File Offset: 0x000A0EA0
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x000A2CA8 File Offset: 0x000A0EA8
		public BoundExpression Argument { get; }

		// Token: 0x06001FAA RID: 8106 RVA: 0x000A2CB0 File Offset: 0x000A0EB0
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitEventAssignmentOperator(this);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x000A2CBC File Offset: 0x000A0EBC
		public BoundEventAssignmentOperator Update(EventSymbol @event, bool isAddition, bool isDynamic, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, BoundExpression argument, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(@event, this.Event) || isAddition != this.IsAddition || isDynamic != this.IsDynamic || receiverOpt != this.ReceiverOpt || argument != this.Argument || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundEventAssignmentOperator boundEventAssignmentOperator = new BoundEventAssignmentOperator(this.Syntax, @event, isAddition, isDynamic, receiverOpt, argument, type, base.HasErrors);
				boundEventAssignmentOperator.CopyAttributes(this);
				return boundEventAssignmentOperator;
			}
			return this;
		}
	}
}
