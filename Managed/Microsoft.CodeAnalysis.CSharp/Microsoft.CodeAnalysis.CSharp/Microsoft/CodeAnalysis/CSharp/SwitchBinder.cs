using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000072 RID: 114
	internal class SwitchBinder : LocalScopeBinder
	{
		// Token: 0x06000A89 RID: 2697 RVA: 0x0004F30A File Offset: 0x0004D50A
		private SwitchBinder(Binder next, SwitchStatementSyntax switchSyntax)
			: base(next)
		{
			this.SwitchSyntax = switchSyntax;
			this._breakLabel = new GeneratedLabelSymbol("break");
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0004F32A File Offset: 0x0004D52A
		protected bool PatternsEnabled
		{
			get
			{
				CSharpParseOptions csharpParseOptions = (CSharpParseOptions)this.SwitchSyntax.SyntaxTree.Options;
				return csharpParseOptions == null || csharpParseOptions.IsFeatureEnabled(MessageID.IDS_FeaturePatternMatching);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0004F351 File Offset: 0x0004D551
		protected BoundExpression SwitchGoverningExpression
		{
			get
			{
				this.EnsureSwitchGoverningExpressionAndDiagnosticsBound();
				return this._switchGoverningExpression;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0004F35F File Offset: 0x0004D55F
		protected TypeSymbol SwitchGoverningType
		{
			get
			{
				return this.SwitchGoverningExpression.Type;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0004F36C File Offset: 0x0004D56C
		protected uint SwitchGoverningValEscape
		{
			get
			{
				return Binder.GetValEscape(this.SwitchGoverningExpression, this.LocalScopeDepth);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0004F37F File Offset: 0x0004D57F
		protected BindingDiagnosticBag SwitchGoverningDiagnostics
		{
			get
			{
				this.EnsureSwitchGoverningExpressionAndDiagnosticsBound();
				return this._switchGoverningDiagnostics;
			}
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0004F390 File Offset: 0x0004D590
		private void EnsureSwitchGoverningExpressionAndDiagnosticsBound()
		{
			if (this._switchGoverningExpression == null)
			{
				BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag();
				BoundExpression boundExpression = this.BindSwitchGoverningExpression(bindingDiagnosticBag);
				this._switchGoverningDiagnostics = bindingDiagnosticBag;
				Interlocked.CompareExchange<BoundExpression>(ref this._switchGoverningExpression, boundExpression, null);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0004F3C8 File Offset: 0x0004D5C8
		private Dictionary<object, SourceLabelSymbol> LabelsByValue
		{
			get
			{
				if (this._lazySwitchLabelsMap == null && this.Labels.Length > 0)
				{
					this._lazySwitchLabelsMap = SwitchBinder.BuildLabelsByValue(this.Labels);
				}
				return this._lazySwitchLabelsMap;
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0004F408 File Offset: 0x0004D608
		private static Dictionary<object, SourceLabelSymbol> BuildLabelsByValue(ImmutableArray<LabelSymbol> labels)
		{
			Dictionary<object, SourceLabelSymbol> dictionary = new Dictionary<object, SourceLabelSymbol>(labels.Length, new SwitchConstantValueHelper.SwitchLabelsComparer());
			foreach (LabelSymbol labelSymbol in labels)
			{
				SourceLabelSymbol sourceLabelSymbol = (SourceLabelSymbol)labelSymbol;
				SyntaxKind syntaxKind = sourceLabelSymbol.IdentifierNodeOrToken.Kind();
				if (syntaxKind != SyntaxKind.IdentifierToken)
				{
					ConstantValue switchCaseLabelConstant = sourceLabelSymbol.SwitchCaseLabelConstant;
					object obj;
					if (switchCaseLabelConstant != null && !switchCaseLabelConstant.IsBad)
					{
						obj = SwitchBinder.KeyForConstant(switchCaseLabelConstant);
					}
					else if (syntaxKind == SyntaxKind.DefaultSwitchLabel)
					{
						obj = SwitchBinder.s_defaultKey;
					}
					else
					{
						obj = sourceLabelSymbol.IdentifierNodeOrToken.AsNode();
					}
					if (!dictionary.ContainsKey(obj))
					{
						dictionary.Add(obj, sourceLabelSymbol);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0004F4B4 File Offset: 0x0004D6B4
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			foreach (SwitchSectionSyntax switchSectionSyntax in this.SwitchSyntax.Sections)
			{
				instance.AddRange(base.BuildLocals(switchSectionSyntax.Statements, this.GetBinder(switchSectionSyntax)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0004F510 File Offset: 0x0004D710
		protected override ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions()
		{
			ArrayBuilder<LocalFunctionSymbol> instance = ArrayBuilder<LocalFunctionSymbol>.GetInstance();
			foreach (SwitchSectionSyntax switchSectionSyntax in this.SwitchSyntax.Sections)
			{
				instance.AddRange(base.BuildLocalFunctions(switchSectionSyntax.Statements));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0004F562 File Offset: 0x0004D762
		internal override bool IsLocalFunctionsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0004F565 File Offset: 0x0004D765
		internal override GeneratedLabelSymbol BreakLabel
		{
			get
			{
				return this._breakLabel;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0004F570 File Offset: 0x0004D770
		protected override ImmutableArray<LabelSymbol> BuildLabels()
		{
			ArrayBuilder<LabelSymbol> instance = ArrayBuilder<LabelSymbol>.GetInstance();
			foreach (SwitchSectionSyntax switchSectionSyntax in this.SwitchSyntax.Sections)
			{
				this.BuildSwitchLabels(switchSectionSyntax.Labels, this.GetBinder(switchSectionSyntax), instance, BindingDiagnosticBag.Discarded);
				base.BuildLabels(switchSectionSyntax.Statements, ref instance);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0004F5D7 File Offset: 0x0004D7D7
		internal override bool IsLabelsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0004F5DC File Offset: 0x0004D7DC
		private void BuildSwitchLabels(SyntaxList<SwitchLabelSyntax> labelsSyntax, Binder sectionBinder, ArrayBuilder<LabelSymbol> labels, BindingDiagnosticBag tempDiagnosticBag)
		{
			foreach (SwitchLabelSyntax switchLabelSyntax in labelsSyntax)
			{
				ConstantValue constantValue = null;
				SyntaxKind syntaxKind = switchLabelSyntax.Kind();
				if (syntaxKind != SyntaxKind.CaseSwitchLabel)
				{
					if (syntaxKind == SyntaxKind.CasePatternSwitchLabel)
					{
						CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = (CasePatternSwitchLabelSyntax)switchLabelSyntax;
						sectionBinder.BindPattern(casePatternSwitchLabelSyntax.Pattern, this.SwitchGoverningType, this.SwitchGoverningValEscape, true, switchLabelSyntax.HasErrors, tempDiagnosticBag, false);
					}
				}
				else
				{
					CaseSwitchLabelSyntax caseSwitchLabelSyntax = (CaseSwitchLabelSyntax)switchLabelSyntax;
					BoundExpression boundExpression = sectionBinder.BindTypeOrRValue(caseSwitchLabelSyntax.Value, tempDiagnosticBag);
					if (!(boundExpression is BoundTypeExpression))
					{
						this.ConvertCaseExpression(switchLabelSyntax, boundExpression, sectionBinder, out constantValue, tempDiagnosticBag, false);
					}
				}
				labels.Add(new SourceLabelSymbol((MethodSymbol)this.ContainingMemberOrLambda, switchLabelSyntax, constantValue));
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0004F6A4 File Offset: 0x0004D8A4
		protected BoundExpression ConvertCaseExpression(CSharpSyntaxNode node, BoundExpression caseExpression, Binder sectionBinder, out ConstantValue constantValueOpt, BindingDiagnosticBag diagnostics, bool isGotoCaseExpr = false)
		{
			bool flag = false;
			if (isGotoCaseExpr)
			{
				CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
				Conversion conversion = base.Conversions.ClassifyConversionFromExpression(caseExpression, this.SwitchGoverningType, ref newCompoundUseSiteInfo, false);
				diagnostics.Add(node, newCompoundUseSiteInfo);
				if (!conversion.IsValid)
				{
					base.GenerateImplicitConversionError(diagnostics, node, conversion, caseExpression, this.SwitchGoverningType);
					flag = true;
				}
				else if (!conversion.IsImplicit)
				{
					diagnostics.Add(ErrorCode.WRN_GotoCaseShouldConvert, node.Location, new object[] { this.SwitchGoverningType });
					flag = true;
				}
				caseExpression = base.CreateConversion(caseExpression, conversion, this.SwitchGoverningType, diagnostics);
			}
			return base.ConvertPatternExpression(this.SwitchGoverningType, node, caseExpression, out constantValueOpt, flag, diagnostics);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0004F753 File Offset: 0x0004D953
		protected static object KeyForConstant(ConstantValue constantValue)
		{
			if (!constantValue.IsNull)
			{
				return constantValue.Value;
			}
			return SwitchBinder.s_nullKey;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0004F76C File Offset: 0x0004D96C
		protected SourceLabelSymbol FindMatchingSwitchCaseLabel(ConstantValue constantValue, CSharpSyntaxNode labelSyntax)
		{
			object obj;
			if (constantValue != null && !constantValue.IsBad)
			{
				obj = SwitchBinder.KeyForConstant(constantValue);
			}
			else
			{
				obj = labelSyntax;
			}
			return this.FindMatchingSwitchLabel(obj);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0004F796 File Offset: 0x0004D996
		private SourceLabelSymbol GetDefaultLabel()
		{
			return this.FindMatchingSwitchLabel(SwitchBinder.s_defaultKey);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0004F7A4 File Offset: 0x0004D9A4
		private SourceLabelSymbol FindMatchingSwitchLabel(object key)
		{
			Dictionary<object, SourceLabelSymbol> labelsByValue = this.LabelsByValue;
			SourceLabelSymbol sourceLabelSymbol;
			if (labelsByValue != null && labelsByValue.TryGetValue(key, ref sourceLabelSymbol))
			{
				return sourceLabelSymbol;
			}
			return null;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0004F7C9 File Offset: 0x0004D9C9
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this.SwitchSyntax == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0004F7E0 File Offset: 0x0004D9E0
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			if (this.SwitchSyntax == scopeDesignator)
			{
				return this.LocalFunctions;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0004F7F7 File Offset: 0x0004D9F7
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this.SwitchSyntax;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0004F800 File Offset: 0x0004DA00
		private BoundExpression BindSwitchGoverningExpression(BindingDiagnosticBag diagnostics)
		{
			ExpressionSyntax expression = this.SwitchSyntax.Expression;
			Binder binder = this.GetBinder(expression);
			BoundExpression boundExpression = binder.BindRValueWithoutTargetType(expression, diagnostics, true);
			TypeSymbol typeSymbol = boundExpression.Type;
			if (typeSymbol != null && !typeSymbol.IsErrorType())
			{
				if (typeSymbol.IsValidV6SwitchGoverningType(false))
				{
					if (typeSymbol.SpecialType == SpecialType.System_Boolean)
					{
						Binder.CheckFeatureAvailability(expression, MessageID.IDS_FeatureSwitchOnBool, diagnostics, null);
					}
					return boundExpression;
				}
				CompoundUseSiteInfo<AssemblySymbol> newCompoundUseSiteInfo = base.GetNewCompoundUseSiteInfo(diagnostics);
				TypeSymbol typeSymbol2;
				Conversion conversion = binder.Conversions.ClassifyImplicitUserDefinedConversionForV6SwitchGoverningType(typeSymbol, out typeSymbol2, ref newCompoundUseSiteInfo);
				diagnostics.Add(expression, newCompoundUseSiteInfo);
				if (conversion.IsValid)
				{
					return binder.CreateConversion(expression, boundExpression, conversion, false, null, typeSymbol2, diagnostics);
				}
				if (!typeSymbol.IsVoidType())
				{
					if (!this.PatternsEnabled)
					{
						diagnostics.Add(ErrorCode.ERR_V6SwitchGoverningTypeValueExpected, expression.Location);
					}
					return boundExpression;
				}
				typeSymbol = base.CreateErrorType(typeSymbol.Name);
			}
			if (!boundExpression.HasAnyErrors)
			{
				diagnostics.Add(ErrorCode.ERR_SwitchExpressionValueExpected, expression.Location, new object[] { boundExpression.Display });
			}
			return new BoundBadExpression(expression, LookupResultKind.Empty, ImmutableArray<Symbol>.Empty, ImmutableArray.Create<BoundExpression>(boundExpression), typeSymbol ?? base.CreateErrorType(""));
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0004F920 File Offset: 0x0004DB20
		protected Dictionary<SyntaxNode, LabelSymbol> LabelsByNode
		{
			get
			{
				if (this._labelsByNode == null)
				{
					Dictionary<SyntaxNode, LabelSymbol> dictionary = new Dictionary<SyntaxNode, LabelSymbol>();
					foreach (LabelSymbol labelSymbol in this.Labels)
					{
						SyntaxNode syntaxNode = ((SourceLabelSymbol)labelSymbol).IdentifierNodeOrToken.AsNode();
						if (syntaxNode != null)
						{
							dictionary.Add(syntaxNode, labelSymbol);
						}
					}
					this._labelsByNode = dictionary;
				}
				return this._labelsByNode;
			}
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0004F990 File Offset: 0x0004DB90
		internal BoundStatement BindGotoCaseOrDefault(GotoStatementSyntax node, Binder gotoBinder, BindingDiagnosticBag diagnostics)
		{
			BoundExpression boundExpression = null;
			if (!node.HasErrors)
			{
				ConstantValue constantValue = null;
				bool flag = false;
				SourceLabelSymbol sourceLabelSymbol;
				if (node.Expression != null)
				{
					boundExpression = gotoBinder.BindValue(node.Expression, diagnostics, Binder.BindValueKind.RValue);
					boundExpression = this.ConvertCaseExpression(node, boundExpression, gotoBinder, out constantValue, diagnostics, true);
					flag = flag || boundExpression.HasAnyErrors;
					if (!flag && constantValue == null)
					{
						diagnostics.Add(ErrorCode.ERR_ConstantExpected, node.Location);
						flag = true;
					}
					ConstantValueUtils.CheckLangVersionForConstantValue(boundExpression, diagnostics);
					sourceLabelSymbol = this.FindMatchingSwitchCaseLabel(constantValue, node);
				}
				else
				{
					sourceLabelSymbol = this.GetDefaultLabel();
				}
				if (sourceLabelSymbol != null)
				{
					return new BoundGotoStatement(node, sourceLabelSymbol, boundExpression, null, flag);
				}
				if (!flag)
				{
					string text = SyntaxFacts.GetText(node.CaseOrDefaultKeyword.Kind());
					if (node.Kind() == SyntaxKind.GotoCaseStatement)
					{
						string text2 = text;
						string text3 = " ";
						object value = constantValue.Value;
						text = text2 + text3 + ((value != null) ? value.ToString() : null);
					}
					text += ":";
					diagnostics.Add(ErrorCode.ERR_LabelNotFound, node.Location, new object[] { text });
				}
			}
			return new BoundBadStatement(node, (boundExpression != null) ? ImmutableArray.Create<BoundNode>(boundExpression) : ImmutableArray<BoundNode>.Empty, true);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0004FAAF File Offset: 0x0004DCAF
		internal static SwitchBinder Create(Binder next, SwitchStatementSyntax switchSyntax)
		{
			return new SwitchBinder(next, switchSyntax);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0004FAB8 File Offset: 0x0004DCB8
		internal override BoundStatement BindSwitchStatementCore(SwitchStatementSyntax node, Binder originalBinder, BindingDiagnosticBag diagnostics)
		{
			if (node.Sections.Count == 0)
			{
				diagnostics.Add(ErrorCode.WRN_EmptySwitch, node.OpenBraceToken.GetLocation());
			}
			BoundExpression switchGoverningExpression = this.SwitchGoverningExpression;
			diagnostics.AddRange(this.SwitchGoverningDiagnostics, true);
			BoundSwitchLabel boundSwitchLabel;
			ImmutableArray<BoundSwitchSection> immutableArray = this.BindSwitchSections(originalBinder, diagnostics, out boundSwitchLabel);
			ImmutableArray<LocalSymbol> declaredLocalsForScope = this.GetDeclaredLocalsForScope(node);
			ImmutableArray<LocalFunctionSymbol> declaredLocalFunctionsForScope = this.GetDeclaredLocalFunctionsForScope(node);
			BoundDecisionDag boundDecisionDag = DecisionDagBuilder.CreateDecisionDagForSwitchStatement(base.Compilation, node, switchGoverningExpression, immutableArray, ((boundSwitchLabel != null) ? boundSwitchLabel.Label : null) ?? this.BreakLabel, diagnostics);
			this.CheckSwitchErrors(node, switchGoverningExpression, ref immutableArray, boundDecisionDag, diagnostics);
			boundDecisionDag = boundDecisionDag.SimplifyDecisionDagIfConstantInput(switchGoverningExpression);
			BoundExpression boundExpression = switchGoverningExpression;
			ImmutableArray<LocalSymbol> immutableArray2 = declaredLocalsForScope;
			ImmutableArray<LocalFunctionSymbol> immutableArray3 = declaredLocalFunctionsForScope;
			ImmutableArray<BoundSwitchSection> immutableArray4 = immutableArray;
			BoundSwitchLabel boundSwitchLabel2 = boundSwitchLabel;
			GeneratedLabelSymbol breakLabel = this.BreakLabel;
			return new BoundSwitchStatement(node, boundExpression, immutableArray2, immutableArray3, immutableArray4, boundDecisionDag, boundSwitchLabel2, breakLabel, false);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0004FB7C File Offset: 0x0004DD7C
		private void CheckSwitchErrors(SwitchStatementSyntax node, BoundExpression boundSwitchGoverningExpression, ref ImmutableArray<BoundSwitchSection> switchSections, BoundDecisionDag decisionDag, BindingDiagnosticBag diagnostics)
		{
			SwitchBinder.<>c__DisplayClass48_0 CS$<>8__locals1 = new SwitchBinder.<>c__DisplayClass48_0();
			CS$<>8__locals1.reachableLabels = decisionDag.ReachableLabels;
			if (!switchSections.Any<BoundSwitchSection>(delegate(BoundSwitchSection s)
			{
				ImmutableArray<BoundSwitchLabel> switchLabels = s.SwitchLabels;
				Func<BoundSwitchLabel, bool> func;
				if ((func = CS$<>8__locals1.<>9__2) == null)
				{
					func = (CS$<>8__locals1.<>9__2 = (BoundSwitchLabel l) => base.<CheckSwitchErrors>g__isSubsumed|0(l));
				}
				return switchLabels.Any<BoundSwitchLabel>(func);
			}))
			{
				return;
			}
			ArrayBuilder<BoundSwitchSection> instance = ArrayBuilder<BoundSwitchSection>.GetInstance(switchSections.Length);
			bool flag = false;
			foreach (BoundSwitchSection boundSwitchSection in switchSections)
			{
				ArrayBuilder<BoundSwitchLabel> instance2 = ArrayBuilder<BoundSwitchLabel>.GetInstance(boundSwitchSection.SwitchLabels.Length);
				foreach (BoundSwitchLabel boundSwitchLabel in boundSwitchSection.SwitchLabels)
				{
					BoundSwitchLabel boundSwitchLabel2 = boundSwitchLabel;
					if (!boundSwitchLabel.HasErrors && CS$<>8__locals1.<CheckSwitchErrors>g__isSubsumed|0(boundSwitchLabel) && boundSwitchLabel.Syntax.Kind() != SyntaxKind.DefaultSwitchLabel)
					{
						SyntaxNode syntax = boundSwitchLabel.Syntax;
						CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = syntax as CasePatternSwitchLabelSyntax;
						if (casePatternSwitchLabelSyntax == null)
						{
							CaseSwitchLabelSyntax caseSwitchLabelSyntax = syntax as CaseSwitchLabelSyntax;
							if (caseSwitchLabelSyntax == null)
							{
								throw ExceptionUtilities.UnexpectedValue(syntax.Kind());
							}
							BoundConstantPattern boundConstantPattern = boundSwitchLabel.Pattern as BoundConstantPattern;
							if (boundConstantPattern != null && !boundConstantPattern.ConstantValue.IsBad && this.FindMatchingSwitchCaseLabel(boundConstantPattern.ConstantValue, caseSwitchLabelSyntax) != boundSwitchLabel.Label)
							{
								diagnostics.Add(ErrorCode.ERR_DuplicateCaseLabel, syntax.Location, new object[] { boundConstantPattern.ConstantValue.GetValueToDisplay() });
							}
							else if (!boundSwitchLabel.Pattern.HasErrors && !flag)
							{
								diagnostics.Add(ErrorCode.ERR_SwitchCaseSubsumed, caseSwitchLabelSyntax.Value.Location);
							}
						}
						else if (!casePatternSwitchLabelSyntax.Pattern.HasErrors && !flag)
						{
							diagnostics.Add(ErrorCode.ERR_SwitchCaseSubsumed, casePatternSwitchLabelSyntax.Pattern.Location);
						}
						boundSwitchLabel2 = new BoundSwitchLabel(boundSwitchLabel.Syntax, boundSwitchLabel.Label, boundSwitchLabel.Pattern, boundSwitchLabel.WhenClause, true);
					}
					flag |= boundSwitchLabel.HasErrors;
					instance2.Add(boundSwitchLabel2);
				}
				instance.Add(boundSwitchSection.Update(boundSwitchSection.Locals, instance2.ToImmutableAndFree(), boundSwitchSection.Statements));
			}
			switchSections = instance.ToImmutableAndFree();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0004FDC4 File Offset: 0x0004DFC4
		internal override void BindPatternSwitchLabelForInference(CasePatternSwitchLabelSyntax node, BindingDiagnosticBag diagnostics)
		{
			BoundSwitchLabel boundSwitchLabel = null;
			this.BindSwitchSectionLabel(this.GetBinder(node.Parent), node, this.LabelsByNode[node], ref boundSwitchLabel, diagnostics);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		private ImmutableArray<BoundSwitchSection> BindSwitchSections(Binder originalBinder, BindingDiagnosticBag diagnostics, out BoundSwitchLabel defaultLabel)
		{
			ArrayBuilder<BoundSwitchSection> instance = ArrayBuilder<BoundSwitchSection>.GetInstance(this.SwitchSyntax.Sections.Count);
			defaultLabel = null;
			foreach (SwitchSectionSyntax switchSectionSyntax in this.SwitchSyntax.Sections)
			{
				BoundSwitchSection boundSwitchSection = this.BindSwitchSection(switchSectionSyntax, originalBinder, ref defaultLabel, diagnostics);
				instance.Add(boundSwitchSection);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0004FE64 File Offset: 0x0004E064
		private BoundSwitchSection BindSwitchSection(SwitchSectionSyntax node, Binder originalBinder, ref BoundSwitchLabel defaultLabel, BindingDiagnosticBag diagnostics)
		{
			ArrayBuilder<BoundSwitchLabel> instance = ArrayBuilder<BoundSwitchLabel>.GetInstance(node.Labels.Count);
			Binder binder = originalBinder.GetBinder(node);
			Dictionary<SyntaxNode, LabelSymbol> labelsByNode = this.LabelsByNode;
			foreach (SwitchLabelSyntax switchLabelSyntax in node.Labels)
			{
				LabelSymbol labelSymbol = labelsByNode[switchLabelSyntax];
				BoundSwitchLabel boundSwitchLabel = this.BindSwitchSectionLabel(binder, switchLabelSyntax, labelSymbol, ref defaultLabel, diagnostics);
				instance.Add(boundSwitchLabel);
			}
			ArrayBuilder<BoundStatement> instance2 = ArrayBuilder<BoundStatement>.GetInstance(node.Statements.Count);
			foreach (StatementSyntax statementSyntax in node.Statements)
			{
				BoundStatement boundStatement = binder.BindStatement(statementSyntax, diagnostics);
				if (SwitchBinder.ContainsUsingVariable(boundStatement))
				{
					diagnostics.Add(ErrorCode.ERR_UsingVarInSwitchCase, statementSyntax.Location);
				}
				instance2.Add(boundStatement);
			}
			return new BoundSwitchSection(node, binder.GetDeclaredLocalsForScope(node), instance.ToImmutableAndFree(), instance2.ToImmutableAndFree(), false);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0004FF64 File Offset: 0x0004E164
		internal static bool ContainsUsingVariable(BoundStatement boundStatement)
		{
			BoundLocalDeclaration boundLocalDeclaration = boundStatement as BoundLocalDeclaration;
			if (boundLocalDeclaration != null)
			{
				return boundLocalDeclaration.LocalSymbol.IsUsing;
			}
			BoundMultipleLocalDeclarationsBase boundMultipleLocalDeclarationsBase = boundStatement as BoundMultipleLocalDeclarationsBase;
			return boundMultipleLocalDeclarationsBase != null && !boundMultipleLocalDeclarationsBase.LocalDeclarations.IsDefaultOrEmpty && boundMultipleLocalDeclarationsBase.LocalDeclarations[0].LocalSymbol.IsUsing;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0004FFBC File Offset: 0x0004E1BC
		private BoundSwitchLabel BindSwitchSectionLabel(Binder sectionBinder, SwitchLabelSyntax node, LabelSymbol label, ref BoundSwitchLabel defaultLabel, BindingDiagnosticBag diagnostics)
		{
			SwitchBinder.<>c__DisplayClass53_0 CS$<>8__locals1;
			CS$<>8__locals1.diagnostics = diagnostics;
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind == SyntaxKind.CaseSwitchLabel)
			{
				CaseSwitchLabelSyntax caseSwitchLabelSyntax = (CaseSwitchLabelSyntax)node;
				bool hasErrors = node.HasErrors;
				BoundPattern boundPattern = sectionBinder.BindConstantPatternWithFallbackToTypePattern(caseSwitchLabelSyntax.Value, caseSwitchLabelSyntax.Value, this.SwitchGoverningType, hasErrors, CS$<>8__locals1.diagnostics);
				boundPattern.WasCompilerGenerated = true;
				SwitchBinder.<BindSwitchSectionLabel>g__reportIfConstantNamedUnderscore|53_0(boundPattern, caseSwitchLabelSyntax.Value, ref CS$<>8__locals1);
				return new BoundSwitchLabel(node, label, boundPattern, null, boundPattern.HasErrors);
			}
			if (syntaxKind != SyntaxKind.DefaultSwitchLabel)
			{
				if (syntaxKind != SyntaxKind.CasePatternSwitchLabel)
				{
					throw ExceptionUtilities.UnexpectedValue(node);
				}
				CasePatternSwitchLabelSyntax casePatternSwitchLabelSyntax = (CasePatternSwitchLabelSyntax)node;
				BoundPattern boundPattern2 = sectionBinder.BindPattern(casePatternSwitchLabelSyntax.Pattern, this.SwitchGoverningType, this.SwitchGoverningValEscape, true, node.HasErrors, CS$<>8__locals1.diagnostics, false);
				ConstantPatternSyntax constantPatternSyntax = casePatternSwitchLabelSyntax.Pattern as ConstantPatternSyntax;
				if (constantPatternSyntax != null)
				{
					SwitchBinder.<BindSwitchSectionLabel>g__reportIfConstantNamedUnderscore|53_0(boundPattern2, constantPatternSyntax.Expression, ref CS$<>8__locals1);
				}
				return new BoundSwitchLabel(node, label, boundPattern2, (casePatternSwitchLabelSyntax.WhenClause != null) ? sectionBinder.BindBooleanExpression(casePatternSwitchLabelSyntax.WhenClause.Condition, CS$<>8__locals1.diagnostics) : null, node.HasErrors);
			}
			else
			{
				BoundDiscardPattern boundDiscardPattern = new BoundDiscardPattern(node, this.SwitchGoverningType, this.SwitchGoverningType);
				bool flag = boundDiscardPattern.HasErrors;
				if (defaultLabel != null)
				{
					CS$<>8__locals1.diagnostics.Add(ErrorCode.ERR_DuplicateCaseLabel, node.Location, new object[] { label.Name });
					flag = true;
					return new BoundSwitchLabel(node, label, boundDiscardPattern, null, flag);
				}
				BoundSwitchLabel boundSwitchLabel;
				defaultLabel = (boundSwitchLabel = new BoundSwitchLabel(node, label, boundDiscardPattern, null, flag));
				return boundSwitchLabel;
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00050161 File Offset: 0x0004E361
		[CompilerGenerated]
		internal static void <BindSwitchSectionLabel>g__reportIfConstantNamedUnderscore|53_0(BoundPattern pattern, ExpressionSyntax expression, ref SwitchBinder.<>c__DisplayClass53_0 A_2)
		{
			if (pattern is BoundConstantPattern && !pattern.HasErrors && Binder.IsUnderscore(expression))
			{
				A_2.diagnostics.Add(ErrorCode.WRN_CaseConstantNamedUnderscore, expression.Location);
			}
		}

		// Token: 0x04000467 RID: 1127
		protected readonly SwitchStatementSyntax SwitchSyntax;

		// Token: 0x04000468 RID: 1128
		private readonly GeneratedLabelSymbol _breakLabel;

		// Token: 0x04000469 RID: 1129
		private BoundExpression _switchGoverningExpression;

		// Token: 0x0400046A RID: 1130
		private BindingDiagnosticBag _switchGoverningDiagnostics;

		// Token: 0x0400046B RID: 1131
		private Dictionary<object, SourceLabelSymbol> _lazySwitchLabelsMap;

		// Token: 0x0400046C RID: 1132
		private static readonly object s_defaultKey = new object();

		// Token: 0x0400046D RID: 1133
		private static readonly object s_nullKey = new object();

		// Token: 0x0400046E RID: 1134
		private Dictionary<SyntaxNode, LabelSymbol> _labelsByNode;
	}
}
