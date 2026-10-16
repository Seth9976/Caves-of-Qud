using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000073 RID: 115
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SwitchExpressionArmBinder : Binder
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x00050192 File Offset: 0x0004E392
		public SwitchExpressionArmBinder(SwitchExpressionArmSyntax arm, ExpressionVariableBinder armScopeBinder, SwitchExpressionBinder switchExpressionBinder)
			: base(armScopeBinder, null)
		{
			this._arm = arm;
			this._armScopeBinder = armScopeBinder;
			this._switchExpressionBinder = switchExpressionBinder;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000501B4 File Offset: 0x0004E3B4
		internal BoundSwitchExpressionArm BindSwitchExpressionArm(SwitchExpressionArmSyntax node, BindingDiagnosticBag diagnostics)
		{
			ValueTuple<TypeSymbol, uint> inputTypeAndValEscape = this._switchExpressionBinder.GetInputTypeAndValEscape(null);
			TypeSymbol item = inputTypeAndValEscape.Item1;
			uint item2 = inputTypeAndValEscape.Item2;
			return this.BindSwitchExpressionArm(node, item, item2, diagnostics);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000501E4 File Offset: 0x0004E3E4
		internal override BoundSwitchExpressionArm BindSwitchExpressionArm(SwitchExpressionArmSyntax node, TypeSymbol switchGoverningType, uint switchGoverningValEscape, BindingDiagnosticBag diagnostics)
		{
			Binder requiredBinder = base.GetRequiredBinder(node);
			bool flag = switchGoverningType.IsErrorType();
			ImmutableArray<LocalSymbol> locals = this._armScopeBinder.Locals;
			BoundPattern boundPattern = requiredBinder.BindPattern(node.Pattern, switchGoverningType, switchGoverningValEscape, true, flag, diagnostics, false);
			BoundExpression boundExpression = ((node.WhenClause != null) ? requiredBinder.BindBooleanExpression(node.WhenClause.Condition, diagnostics) : null);
			BoundExpression boundExpression2 = requiredBinder.BindValue(node.Expression, diagnostics, Binder.BindValueKind.RValue);
			GeneratedLabelSymbol generatedLabelSymbol = new GeneratedLabelSymbol("arm");
			return new BoundSwitchExpressionArm(node, locals, boundPattern, boundExpression, boundExpression2, generatedLabelSymbol, flag | boundPattern.HasErrors);
		}

		// Token: 0x0400046F RID: 1135
		private readonly SwitchExpressionArmSyntax _arm;

		// Token: 0x04000470 RID: 1136
		private readonly ExpressionVariableBinder _armScopeBinder;

		// Token: 0x04000471 RID: 1137
		private readonly SwitchExpressionBinder _switchExpressionBinder;
	}
}
