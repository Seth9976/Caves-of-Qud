using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000019 RID: 25
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DecisionDagBuilder
	{
		// Token: 0x06000526 RID: 1318 RVA: 0x0003347A File Offset: 0x0003167A
		private DecisionDagBuilder(CSharpCompilation compilation, LabelSymbol defaultLabel, BindingDiagnosticBag diagnostics)
		{
			this._compilation = compilation;
			this._conversions = compilation.Conversions;
			this._diagnostics = diagnostics;
			this._defaultLabel = defaultLabel;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000334A3 File Offset: 0x000316A3
		public static BoundDecisionDag CreateDecisionDagForSwitchStatement(CSharpCompilation compilation, SyntaxNode syntax, BoundExpression switchGoverningExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchSection> switchSections, LabelSymbol defaultLabel, BindingDiagnosticBag diagnostics)
		{
			return new DecisionDagBuilder(compilation, defaultLabel, diagnostics).CreateDecisionDagForSwitchStatement(syntax, switchGoverningExpression, switchSections);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000334B7 File Offset: 0x000316B7
		public static BoundDecisionDag CreateDecisionDagForSwitchExpression(CSharpCompilation compilation, SyntaxNode syntax, BoundExpression switchExpressionInput, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms, LabelSymbol defaultLabel, BindingDiagnosticBag diagnostics)
		{
			return new DecisionDagBuilder(compilation, defaultLabel, diagnostics).CreateDecisionDagForSwitchExpression(syntax, switchExpressionInput, switchArms);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000334CB File Offset: 0x000316CB
		public static BoundDecisionDag CreateDecisionDagForIsPattern(CSharpCompilation compilation, SyntaxNode syntax, BoundExpression inputExpression, BoundPattern pattern, LabelSymbol whenTrueLabel, LabelSymbol whenFalseLabel, BindingDiagnosticBag diagnostics)
		{
			return new DecisionDagBuilder(compilation, whenFalseLabel, diagnostics).CreateDecisionDagForIsPattern(syntax, inputExpression, pattern, whenTrueLabel);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000334E4 File Offset: 0x000316E4
		private BoundDecisionDag CreateDecisionDagForIsPattern(SyntaxNode syntax, BoundExpression inputExpression, BoundPattern pattern, LabelSymbol whenTrueLabel)
		{
			BoundDagTemp boundDagTemp = BoundDagTemp.ForOriginalInput(inputExpression);
			return this.MakeBoundDecisionDag(syntax, ImmutableArray.Create<DecisionDagBuilder.StateForCase>(this.MakeTestsForPattern(1, pattern.Syntax, boundDagTemp, pattern, null, whenTrueLabel)));
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00033518 File Offset: 0x00031718
		private BoundDecisionDag CreateDecisionDagForSwitchStatement(SyntaxNode syntax, BoundExpression switchGoverningExpression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchSection> switchSections)
		{
			BoundDagTemp boundDagTemp = BoundDagTemp.ForOriginalInput(switchGoverningExpression);
			int num = 0;
			ArrayBuilder<DecisionDagBuilder.StateForCase> instance = ArrayBuilder<DecisionDagBuilder.StateForCase>.GetInstance(switchSections.Length);
			foreach (BoundSwitchSection boundSwitchSection in switchSections)
			{
				foreach (BoundSwitchLabel boundSwitchLabel in boundSwitchSection.SwitchLabels)
				{
					if (boundSwitchLabel.Syntax.Kind() != SyntaxKind.DefaultSwitchLabel)
					{
						instance.Add(this.MakeTestsForPattern(++num, boundSwitchLabel.Syntax, boundDagTemp, boundSwitchLabel.Pattern, boundSwitchLabel.WhenClause, boundSwitchLabel.Label));
					}
				}
			}
			return this.MakeBoundDecisionDag(syntax, instance.ToImmutableAndFree());
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000335C8 File Offset: 0x000317C8
		private BoundDecisionDag CreateDecisionDagForSwitchExpression(SyntaxNode syntax, BoundExpression switchExpressionInput, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchExpressionArm> switchArms)
		{
			BoundDagTemp boundDagTemp = BoundDagTemp.ForOriginalInput(switchExpressionInput);
			int num = 0;
			ArrayBuilder<DecisionDagBuilder.StateForCase> instance = ArrayBuilder<DecisionDagBuilder.StateForCase>.GetInstance(switchArms.Length);
			foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in switchArms)
			{
				instance.Add(this.MakeTestsForPattern(++num, boundSwitchExpressionArm.Syntax, boundDagTemp, boundSwitchExpressionArm.Pattern, boundSwitchExpressionArm.WhenClause, boundSwitchExpressionArm.Label));
			}
			return this.MakeBoundDecisionDag(syntax, instance.ToImmutableAndFree());
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00033644 File Offset: 0x00031844
		private DecisionDagBuilder.StateForCase MakeTestsForPattern(int index, SyntaxNode syntax, BoundDagTemp input, BoundPattern pattern, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression whenClause, LabelSymbol label)
		{
			ImmutableArray<BoundPatternBinding> immutableArray;
			DecisionDagBuilder.Tests tests = this.MakeAndSimplifyTestsAndBindings(input, pattern, out immutableArray);
			return new DecisionDagBuilder.StateForCase(index, syntax, tests, immutableArray, whenClause, label);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0003366C File Offset: 0x0003186C
		private DecisionDagBuilder.Tests MakeAndSimplifyTestsAndBindings(BoundDagTemp input, BoundPattern pattern, [global::System.Runtime.CompilerServices.Nullable(0)] out ImmutableArray<BoundPatternBinding> bindings)
		{
			ArrayBuilder<BoundPatternBinding> instance = ArrayBuilder<BoundPatternBinding>.GetInstance();
			DecisionDagBuilder.Tests tests = DecisionDagBuilder.SimplifyTestsAndBindings(this.MakeTestsAndBindings(input, pattern, instance), instance);
			bindings = instance.ToImmutableAndFree();
			return tests;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0003369C File Offset: 0x0003189C
		private static DecisionDagBuilder.Tests SimplifyTestsAndBindings(DecisionDagBuilder.Tests tests, ArrayBuilder<BoundPatternBinding> bindingsBuilder)
		{
			DecisionDagBuilder.<>c__DisplayClass13_0 CS$<>8__locals1;
			CS$<>8__locals1.usedValues = PooledHashSet<BoundDagEvaluation>.GetInstance();
			foreach (BoundPatternBinding boundPatternBinding in bindingsBuilder)
			{
				BoundDagTemp tempContainingValue = boundPatternBinding.TempContainingValue;
				if (tempContainingValue.Source != null)
				{
					CS$<>8__locals1.usedValues.Add(tempContainingValue.Source);
				}
			}
			DecisionDagBuilder.Tests tests2 = DecisionDagBuilder.<SimplifyTestsAndBindings>g__scanAndSimplify|13_0(tests, ref CS$<>8__locals1);
			CS$<>8__locals1.usedValues.Free();
			return tests2;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00033704 File Offset: 0x00031904
		private DecisionDagBuilder.Tests MakeTestsAndBindings(BoundDagTemp input, BoundPattern pattern, ArrayBuilder<BoundPatternBinding> bindings)
		{
			BoundDagTemp boundDagTemp;
			return this.MakeTestsAndBindings(input, pattern, out boundDagTemp, bindings);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0003371C File Offset: 0x0003191C
		private DecisionDagBuilder.Tests MakeTestsAndBindings(BoundDagTemp input, BoundPattern pattern, out BoundDagTemp output, ArrayBuilder<BoundPatternBinding> bindings)
		{
			BoundDeclarationPattern boundDeclarationPattern = pattern as BoundDeclarationPattern;
			if (boundDeclarationPattern != null)
			{
				return this.MakeTestsAndBindingsForDeclarationPattern(input, boundDeclarationPattern, out output, bindings);
			}
			BoundConstantPattern boundConstantPattern = pattern as BoundConstantPattern;
			if (boundConstantPattern != null)
			{
				return this.MakeTestsForConstantPattern(input, boundConstantPattern, out output);
			}
			if (pattern is BoundDiscardPattern)
			{
				output = input;
				return DecisionDagBuilder.Tests.True.Instance;
			}
			BoundRecursivePattern boundRecursivePattern = pattern as BoundRecursivePattern;
			if (boundRecursivePattern != null)
			{
				return this.MakeTestsAndBindingsForRecursivePattern(input, boundRecursivePattern, out output, bindings);
			}
			BoundITuplePattern boundITuplePattern = pattern as BoundITuplePattern;
			if (boundITuplePattern != null)
			{
				return this.MakeTestsAndBindingsForITuplePattern(input, boundITuplePattern, out output, bindings);
			}
			BoundTypePattern boundTypePattern = pattern as BoundTypePattern;
			if (boundTypePattern != null)
			{
				return this.MakeTestsForTypePattern(input, boundTypePattern, out output);
			}
			BoundRelationalPattern boundRelationalPattern = pattern as BoundRelationalPattern;
			if (boundRelationalPattern != null)
			{
				return this.MakeTestsAndBindingsForRelationalPattern(input, boundRelationalPattern, out output);
			}
			BoundNegatedPattern boundNegatedPattern = pattern as BoundNegatedPattern;
			if (boundNegatedPattern != null)
			{
				output = input;
				return this.MakeTestsAndBindingsForNegatedPattern(input, boundNegatedPattern, bindings);
			}
			BoundBinaryPattern boundBinaryPattern = pattern as BoundBinaryPattern;
			if (boundBinaryPattern == null)
			{
				throw ExceptionUtilities.UnexpectedValue(pattern.Kind);
			}
			return this.MakeTestsAndBindingsForBinaryPattern(input, boundBinaryPattern, out output, bindings);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00033804 File Offset: 0x00031A04
		private DecisionDagBuilder.Tests MakeTestsAndBindingsForITuplePattern(BoundDagTemp input, BoundITuplePattern pattern, out BoundDagTemp output, ArrayBuilder<BoundPatternBinding> bindings)
		{
			SyntaxNode syntax = pattern.Syntax;
			int length = pattern.Subpatterns.Length;
			NamedTypeSymbol specialType = this._compilation.GetSpecialType(SpecialType.System_Object);
			PropertySymbol propertySymbol = (PropertySymbol)pattern.GetLengthMethod.AssociatedSymbol;
			PropertySymbol propertySymbol2 = (PropertySymbol)pattern.GetItemMethod.AssociatedSymbol;
			NamedTypeSymbol containingType = propertySymbol.ContainingType;
			ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(4 + length * 2);
			instance.Add(new DecisionDagBuilder.Tests.One(new BoundDagTypeTest(syntax, containingType, input, false)));
			BoundDagTypeEvaluation boundDagTypeEvaluation = new BoundDagTypeEvaluation(syntax, containingType, input, false);
			instance.Add(new DecisionDagBuilder.Tests.One(boundDagTypeEvaluation));
			BoundDagTemp boundDagTemp = new BoundDagTemp(syntax, containingType, boundDagTypeEvaluation);
			output = boundDagTemp;
			BoundDagPropertyEvaluation boundDagPropertyEvaluation = new BoundDagPropertyEvaluation(syntax, propertySymbol, this.OriginalInput(boundDagTemp, propertySymbol), false);
			instance.Add(new DecisionDagBuilder.Tests.One(boundDagPropertyEvaluation));
			BoundDagTemp boundDagTemp2 = new BoundDagTemp(syntax, this._compilation.GetSpecialType(SpecialType.System_Int32), boundDagPropertyEvaluation);
			instance.Add(new DecisionDagBuilder.Tests.One(new BoundDagValueTest(syntax, ConstantValue.Create(length), boundDagTemp2, false)));
			BoundDagTemp boundDagTemp3 = this.OriginalInput(boundDagTemp, propertySymbol2);
			for (int i = 0; i < length; i++)
			{
				BoundDagIndexEvaluation boundDagIndexEvaluation = new BoundDagIndexEvaluation(syntax, propertySymbol2, i, boundDagTemp3, false);
				instance.Add(new DecisionDagBuilder.Tests.One(boundDagIndexEvaluation));
				BoundDagTemp boundDagTemp4 = new BoundDagTemp(syntax, specialType, boundDagIndexEvaluation);
				instance.Add(this.MakeTestsAndBindings(boundDagTemp4, pattern.Subpatterns[i].Pattern, bindings));
			}
			return DecisionDagBuilder.Tests.AndSequence.Create(instance);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00033974 File Offset: 0x00031B74
		private BoundDagTemp OriginalInput(BoundDagTemp input, Symbol symbol)
		{
			for (;;)
			{
				BoundDagTypeEvaluation boundDagTypeEvaluation = input.Source as BoundDagTypeEvaluation;
				if (boundDagTypeEvaluation == null || !this.IsDerivedType(boundDagTypeEvaluation.Input.Type, symbol.ContainingType))
				{
					break;
				}
				input = boundDagTypeEvaluation.Input;
			}
			return input;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000339B4 File Offset: 0x00031BB4
		private bool IsDerivedType(TypeSymbol possibleDerived, TypeSymbol possibleBase)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return this._conversions.HasIdentityOrImplicitReferenceConversion(possibleDerived, possibleBase, ref discarded);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000339D8 File Offset: 0x00031BD8
		private DecisionDagBuilder.Tests MakeTestsAndBindingsForDeclarationPattern(BoundDagTemp input, BoundDeclarationPattern declaration, out BoundDagTemp output, ArrayBuilder<BoundPatternBinding> bindings)
		{
			BoundTypeExpression declaredType = declaration.DeclaredType;
			TypeSymbol typeSymbol = ((declaredType != null) ? declaredType.Type : null);
			ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(1);
			if (!declaration.IsVar)
			{
				input = this.MakeConvertToType(input, declaration.Syntax, typeSymbol, false, instance);
			}
			BoundExpression variableAccess = declaration.VariableAccess;
			if (variableAccess != null)
			{
				bindings.Add(new BoundPatternBinding(variableAccess, input));
			}
			output = input;
			return DecisionDagBuilder.Tests.AndSequence.Create(instance);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00033A3C File Offset: 0x00031C3C
		private DecisionDagBuilder.Tests MakeTestsForTypePattern(BoundDagTemp input, BoundTypePattern typePattern, out BoundDagTemp output)
		{
			TypeSymbol type = typePattern.DeclaredType.Type;
			ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(4);
			output = this.MakeConvertToType(input, typePattern.Syntax, type, typePattern.IsExplicitNotNullTest, instance);
			return DecisionDagBuilder.Tests.AndSequence.Create(instance);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00033A79 File Offset: 0x00031C79
		private static void MakeCheckNotNull(BoundDagTemp input, SyntaxNode syntax, bool isExplicitTest, ArrayBuilder<DecisionDagBuilder.Tests> tests)
		{
			if (input.Type.CanContainNull())
			{
				tests.Add(new DecisionDagBuilder.Tests.One(new BoundDagNonNullTest(syntax, isExplicitTest, input, false)));
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00033A9C File Offset: 0x00031C9C
		private BoundDagTemp MakeConvertToType(BoundDagTemp input, SyntaxNode syntax, TypeSymbol type, bool isExplicitTest, ArrayBuilder<DecisionDagBuilder.Tests> tests)
		{
			DecisionDagBuilder.MakeCheckNotNull(input, syntax, isExplicitTest, tests);
			if (!input.Type.Equals(type, TypeCompareKind.AllIgnoreOptions))
			{
				TypeSymbol typeSymbol = input.Type.StrippedType();
				CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(this._diagnostics, this._compilation.Assembly);
				Conversion conversion = this._conversions.ClassifyBuiltInConversion(typeSymbol, type, ref compoundUseSiteInfo);
				this._diagnostics.Add(syntax, compoundUseSiteInfo);
				if (!(input.Type.IsDynamic() ? (type.SpecialType == SpecialType.System_Object) : conversion.IsImplicit))
				{
					tests.Add(new DecisionDagBuilder.Tests.One(new BoundDagTypeTest(syntax, type, input, false)));
				}
				BoundDagTypeEvaluation boundDagTypeEvaluation = new BoundDagTypeEvaluation(syntax, type, input, false);
				input = new BoundDagTemp(syntax, type, boundDagTypeEvaluation);
				tests.Add(new DecisionDagBuilder.Tests.One(boundDagTypeEvaluation));
			}
			return input;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00033B64 File Offset: 0x00031D64
		private DecisionDagBuilder.Tests MakeTestsForConstantPattern(BoundDagTemp input, BoundConstantPattern constant, out BoundDagTemp output)
		{
			if (constant.ConstantValue == ConstantValue.Null)
			{
				output = input;
				return new DecisionDagBuilder.Tests.One(new BoundDagExplicitNullTest(constant.Syntax, input, false));
			}
			ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(2);
			BoundDagTemp boundDagTemp = this.MakeConvertToType(input, constant.Syntax, constant.Value.Type, false, instance);
			output = boundDagTemp;
			instance.Add(new DecisionDagBuilder.Tests.One(new BoundDagValueTest(constant.Syntax, constant.ConstantValue, boundDagTemp, false)));
			return DecisionDagBuilder.Tests.AndSequence.Create(instance);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00033BE4 File Offset: 0x00031DE4
		private DecisionDagBuilder.Tests MakeTestsAndBindingsForRecursivePattern(BoundDagTemp input, BoundRecursivePattern recursive, out BoundDagTemp output, ArrayBuilder<BoundPatternBinding> bindings)
		{
			BoundTypeExpression declaredType = recursive.DeclaredType;
			TypeSymbol typeSymbol = ((declaredType != null) ? declaredType.Type : null) ?? input.Type.StrippedType();
			ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(5);
			output = (input = this.MakeConvertToType(input, recursive.Syntax, typeSymbol, recursive.IsExplicitNotNullTest, instance));
			if (!recursive.Deconstruction.IsDefault)
			{
				if (recursive.DeconstructMethod != null)
				{
					MethodSymbol deconstructMethod = recursive.DeconstructMethod;
					BoundDagDeconstructEvaluation boundDagDeconstructEvaluation = new BoundDagDeconstructEvaluation(recursive.Syntax, deconstructMethod, this.OriginalInput(input, deconstructMethod), false);
					instance.Add(new DecisionDagBuilder.Tests.One(boundDagDeconstructEvaluation));
					int num = (deconstructMethod.IsStatic ? 1 : 0);
					int num2 = Math.Min(deconstructMethod.ParameterCount - num, recursive.Deconstruction.Length);
					for (int i = 0; i < num2; i++)
					{
						BoundPattern pattern = recursive.Deconstruction[i].Pattern;
						BoundDagTemp boundDagTemp = new BoundDagTemp(pattern.Syntax, deconstructMethod.Parameters[i + num].Type, boundDagDeconstructEvaluation, i, false);
						instance.Add(this.MakeTestsAndBindings(boundDagTemp, pattern, bindings));
					}
				}
				else if (!Binder.IsZeroElementTupleType(typeSymbol))
				{
					if (typeSymbol.IsTupleType)
					{
						ImmutableArray<FieldSymbol> tupleElements = typeSymbol.TupleElements;
						int num3 = Math.Min(typeSymbol.TupleElementTypesWithAnnotations.Length, recursive.Deconstruction.Length);
						for (int j = 0; j < num3; j++)
						{
							BoundPattern pattern2 = recursive.Deconstruction[j].Pattern;
							SyntaxNode syntax = pattern2.Syntax;
							FieldSymbol fieldSymbol = tupleElements[j];
							BoundDagFieldEvaluation boundDagFieldEvaluation = new BoundDagFieldEvaluation(syntax, fieldSymbol, this.OriginalInput(input, fieldSymbol), false);
							instance.Add(new DecisionDagBuilder.Tests.One(boundDagFieldEvaluation));
							BoundDagTemp boundDagTemp2 = new BoundDagTemp(syntax, fieldSymbol.Type, boundDagFieldEvaluation);
							instance.Add(this.MakeTestsAndBindings(boundDagTemp2, pattern2, bindings));
						}
					}
					else
					{
						instance.Add(new DecisionDagBuilder.Tests.One(new BoundDagTypeTest(recursive.Syntax, this.ErrorType(""), input, true)));
					}
				}
			}
			if (!recursive.Properties.IsDefault)
			{
				int k = 0;
				while (k < recursive.Properties.Length)
				{
					BoundSubpattern boundSubpattern = recursive.Properties[k];
					Symbol symbol = boundSubpattern.Symbol;
					BoundPattern pattern3 = boundSubpattern.Pattern;
					PropertySymbol propertySymbol = symbol as PropertySymbol;
					BoundDagEvaluation boundDagEvaluation;
					if (propertySymbol != null)
					{
						boundDagEvaluation = new BoundDagPropertyEvaluation(pattern3.Syntax, propertySymbol, this.OriginalInput(input, propertySymbol), false);
						goto IL_02C0;
					}
					FieldSymbol fieldSymbol2 = symbol as FieldSymbol;
					if (fieldSymbol2 != null)
					{
						boundDagEvaluation = new BoundDagFieldEvaluation(pattern3.Syntax, fieldSymbol2, this.OriginalInput(input, fieldSymbol2), false);
						goto IL_02C0;
					}
					instance.Add(new DecisionDagBuilder.Tests.One(new BoundDagTypeTest(recursive.Syntax, this.ErrorType(""), input, true)));
					IL_02FF:
					k++;
					continue;
					IL_02C0:
					instance.Add(new DecisionDagBuilder.Tests.One(boundDagEvaluation));
					BoundDagTemp boundDagTemp3 = new BoundDagTemp(pattern3.Syntax, symbol.GetTypeOrReturnType().Type, boundDagEvaluation);
					instance.Add(this.MakeTestsAndBindings(boundDagTemp3, pattern3, bindings));
					goto IL_02FF;
				}
			}
			if (recursive.VariableAccess != null)
			{
				bindings.Add(new BoundPatternBinding(recursive.VariableAccess, input));
			}
			return DecisionDagBuilder.Tests.AndSequence.Create(instance);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00033F2C File Offset: 0x0003212C
		private DecisionDagBuilder.Tests MakeTestsAndBindingsForNegatedPattern(BoundDagTemp input, BoundNegatedPattern neg, ArrayBuilder<BoundPatternBinding> bindings)
		{
			return DecisionDagBuilder.Tests.Not.Create(this.MakeTestsAndBindings(input, neg.Negated, bindings));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00033F44 File Offset: 0x00032144
		private DecisionDagBuilder.Tests MakeTestsAndBindingsForBinaryPattern(BoundDagTemp input, BoundBinaryPattern bin, out BoundDagTemp output, ArrayBuilder<BoundPatternBinding> bindings)
		{
			ArrayBuilder<DecisionDagBuilder.Tests> arrayBuilder = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(2);
			if (!bin.Disjunction)
			{
				BoundDagTemp boundDagTemp;
				arrayBuilder.Add(this.MakeTestsAndBindings(input, bin.Left, out boundDagTemp, bindings));
				BoundDagTemp boundDagTemp2;
				arrayBuilder.Add(this.MakeTestsAndBindings(boundDagTemp, bin.Right, out boundDagTemp2, bindings));
				output = boundDagTemp2;
				return DecisionDagBuilder.Tests.AndSequence.Create(arrayBuilder);
			}
			arrayBuilder.Add(this.MakeTestsAndBindings(input, bin.Left, bindings));
			arrayBuilder.Add(this.MakeTestsAndBindings(input, bin.Right, bindings));
			DecisionDagBuilder.Tests tests = DecisionDagBuilder.Tests.OrSequence.Create(arrayBuilder);
			if (bin.InputType.Equals(bin.NarrowedType))
			{
				output = input;
				return tests;
			}
			arrayBuilder = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(2);
			arrayBuilder.Add(tests);
			output = this.MakeConvertToType(input, bin.Syntax, bin.NarrowedType, false, arrayBuilder);
			return DecisionDagBuilder.Tests.AndSequence.Create(arrayBuilder);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0003400C File Offset: 0x0003220C
		private DecisionDagBuilder.Tests MakeTestsAndBindingsForRelationalPattern(BoundDagTemp input, BoundRelationalPattern rel, out BoundDagTemp output)
		{
			ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(2);
			output = this.MakeConvertToType(input, rel.Syntax, rel.Value.Type, false, instance);
			IValueSetFactory valueSetFactory = ValueSetFactory.ForType(input.Type);
			IValueSet valueSet = ((valueSetFactory != null) ? valueSetFactory.Related(rel.Relation.Operator(), rel.ConstantValue) : null);
			if (valueSet != null && valueSet.IsEmpty)
			{
				instance.Add(DecisionDagBuilder.Tests.False.Instance);
			}
			else if (valueSet == null || !valueSet.Complement().IsEmpty)
			{
				instance.Add(new DecisionDagBuilder.Tests.One(new BoundDagRelationalTest(rel.Syntax, rel.Relation, rel.ConstantValue, output, rel.HasErrors)));
			}
			return DecisionDagBuilder.Tests.AndSequence.Create(instance);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000340C3 File Offset: 0x000322C3
		private TypeSymbol ErrorType(string name = "")
		{
			return new ExtendedErrorTypeSymbol(this._compilation, name, 0, null, false, false);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000340D8 File Offset: 0x000322D8
		private BoundDecisionDag MakeBoundDecisionDag(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.StateForCase> cases)
		{
			DecisionDagBuilder.DecisionDag decisionDag = this.MakeDecisionDag(cases);
			BoundLeafDecisionDagNode boundLeafDecisionDagNode = new BoundLeafDecisionDagNode(syntax, this._defaultLabel);
			this.ComputeBoundDecisionDagNodes(decisionDag, boundLeafDecisionDagNode);
			BoundDecisionDagNode dag = decisionDag.RootNode.Dag;
			return new BoundDecisionDag(dag.Syntax, dag, false);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0003411C File Offset: 0x0003231C
		private DecisionDagBuilder.DecisionDag MakeDecisionDag([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.StateForCase> casesForRootNode)
		{
			DecisionDagBuilder.<>c__DisplayClass30_0 CS$<>8__locals1;
			CS$<>8__locals1.workList = ArrayBuilder<DecisionDagBuilder.DagState>.GetInstance();
			CS$<>8__locals1.uniqueState = new Dictionary<DecisionDagBuilder.DagState, DecisionDagBuilder.DagState>(DecisionDagBuilder.DagStateEquivalence.Instance);
			ArrayBuilder<DecisionDagBuilder.StateForCase> instance = ArrayBuilder<DecisionDagBuilder.StateForCase>.GetInstance(casesForRootNode.Length);
			foreach (DecisionDagBuilder.StateForCase stateForCase in casesForRootNode)
			{
				if (!stateForCase.IsImpossible)
				{
					instance.Add(stateForCase);
					if (stateForCase.IsFullyMatched)
					{
						break;
					}
				}
			}
			DecisionDagBuilder.DagState dagState = DecisionDagBuilder.<MakeDecisionDag>g__uniqifyState|30_0(instance.ToImmutableAndFree(), ImmutableDictionary<BoundDagTemp, IValueSet>.Empty, ref CS$<>8__locals1);
			while (CS$<>8__locals1.workList.Count != 0)
			{
				DecisionDagBuilder.DagState dagState2 = CS$<>8__locals1.workList.Pop<DecisionDagBuilder.DagState>();
				if (!dagState2.Cases.IsDefaultOrEmpty)
				{
					DecisionDagBuilder.StateForCase stateForCase2 = dagState2.Cases[0];
					if (stateForCase2.PatternIsSatisfied)
					{
						if (!stateForCase2.IsFullyMatched)
						{
							ImmutableArray<DecisionDagBuilder.StateForCase> immutableArray = dagState2.Cases.RemoveAt(0);
							dagState2.FalseBranch = DecisionDagBuilder.<MakeDecisionDag>g__uniqifyState|30_0(immutableArray, dagState2.RemainingValues, ref CS$<>8__locals1);
						}
					}
					else
					{
						BoundDagTest boundDagTest = (dagState2.SelectedTest = dagState2.ComputeSelectedTest());
						BoundDagEvaluation boundDagEvaluation = boundDagTest as BoundDagEvaluation;
						if (boundDagEvaluation == null)
						{
							if (boundDagTest == null)
							{
								throw ExceptionUtilities.UnexpectedValue(boundDagTest.Kind);
							}
							bool flag = false;
							ImmutableArray<DecisionDagBuilder.StateForCase> immutableArray2;
							ImmutableArray<DecisionDagBuilder.StateForCase> immutableArray3;
							ImmutableDictionary<BoundDagTemp, IValueSet> immutableDictionary;
							ImmutableDictionary<BoundDagTemp, IValueSet> immutableDictionary2;
							this.SplitCases(dagState2.Cases, dagState2.RemainingValues, boundDagTest, out immutableArray2, out immutableArray3, out immutableDictionary, out immutableDictionary2, ref flag);
							dagState2.TrueBranch = DecisionDagBuilder.<MakeDecisionDag>g__uniqifyState|30_0(immutableArray2, immutableDictionary, ref CS$<>8__locals1);
							dagState2.FalseBranch = DecisionDagBuilder.<MakeDecisionDag>g__uniqifyState|30_0(immutableArray3, immutableDictionary2, ref CS$<>8__locals1);
							if (flag)
							{
								BoundDagNonNullTest boundDagNonNullTest = boundDagTest as BoundDagNonNullTest;
								if (boundDagNonNullTest != null && !boundDagNonNullTest.IsExplicitTest)
								{
									dagState2.SelectedTest = new BoundDagNonNullTest(boundDagNonNullTest.Syntax, true, boundDagNonNullTest.Input, boundDagNonNullTest.HasErrors);
								}
							}
						}
						else
						{
							dagState2.TrueBranch = DecisionDagBuilder.<MakeDecisionDag>g__uniqifyState|30_0(DecisionDagBuilder.RemoveEvaluation(dagState2.Cases, boundDagEvaluation), dagState2.RemainingValues, ref CS$<>8__locals1);
						}
					}
				}
			}
			CS$<>8__locals1.workList.Free();
			return new DecisionDagBuilder.DecisionDag(dagState);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00034328 File Offset: 0x00032528
		private void ComputeBoundDecisionDagNodes(DecisionDagBuilder.DecisionDag decisionDag, BoundLeafDecisionDagNode defaultDecision)
		{
			ImmutableArray<DecisionDagBuilder.DagState> immutableArray;
			if (!decisionDag.TryGetTopologicallySortedReachableStates(out immutableArray))
			{
				decisionDag.RootNode.Dag = defaultDecision;
				return;
			}
			DecisionDagBuilder.<>c__DisplayClass31_0 CS$<>8__locals1;
			CS$<>8__locals1.uniqueNodes = PooledDictionary<BoundDecisionDagNode, BoundDecisionDagNode>.GetInstance();
			DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(defaultDecision, ref CS$<>8__locals1);
			for (int i = immutableArray.Length - 1; i >= 0; i--)
			{
				DecisionDagBuilder.DagState dagState = immutableArray[i];
				if (dagState.Cases.IsDefaultOrEmpty)
				{
					dagState.Dag = defaultDecision;
				}
				else
				{
					DecisionDagBuilder.StateForCase stateForCase = dagState.Cases[0];
					if (stateForCase.PatternIsSatisfied)
					{
						if (stateForCase.IsFullyMatched)
						{
							dagState.Dag = DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__finalState|31_1(stateForCase.Syntax, stateForCase.CaseLabel, stateForCase.Bindings, ref CS$<>8__locals1);
						}
						else
						{
							BoundDecisionDagNode boundDecisionDagNode = DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__finalState|31_1(stateForCase.Syntax, stateForCase.CaseLabel, default(ImmutableArray<BoundPatternBinding>), ref CS$<>8__locals1);
							BoundDecisionDagNode dag = dagState.FalseBranch.Dag;
							dagState.Dag = DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(new BoundWhenDecisionDagNode(stateForCase.Syntax, stateForCase.Bindings, stateForCase.WhenClause, boundDecisionDagNode, dag, false), ref CS$<>8__locals1);
						}
					}
					else
					{
						BoundDagTest selectedTest = dagState.SelectedTest;
						BoundDagEvaluation boundDagEvaluation = selectedTest as BoundDagEvaluation;
						if (boundDagEvaluation == null)
						{
							if (selectedTest == null)
							{
								BoundDagTest boundDagTest = selectedTest;
								throw ExceptionUtilities.UnexpectedValue((boundDagTest != null) ? new BoundKind?(boundDagTest.Kind) : default(BoundKind?));
							}
							BoundDecisionDagNode dag2 = dagState.TrueBranch.Dag;
							BoundDecisionDagNode dag3 = dagState.FalseBranch.Dag;
							dagState.Dag = DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(new BoundTestDecisionDagNode(selectedTest.Syntax, selectedTest, dag2, dag3, false), ref CS$<>8__locals1);
						}
						else
						{
							BoundDecisionDagNode dag4 = dagState.TrueBranch.Dag;
							dagState.Dag = DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(new BoundEvaluationDecisionDagNode(boundDagEvaluation.Syntax, boundDagEvaluation, dag4, false), ref CS$<>8__locals1);
						}
					}
				}
			}
			CS$<>8__locals1.uniqueNodes.Free();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00034504 File Offset: 0x00032704
		private void SplitCase(DecisionDagBuilder.StateForCase stateForCase, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.StateForCase whenTrue, out DecisionDagBuilder.StateForCase whenFalse, ref bool foundExplicitNullTest)
		{
			DecisionDagBuilder.<>c__DisplayClass32_0 CS$<>8__locals1;
			CS$<>8__locals1.stateForCase = stateForCase;
			DecisionDagBuilder.Tests tests;
			DecisionDagBuilder.Tests tests2;
			CS$<>8__locals1.stateForCase.RemainingTests.Filter(this, test, whenTrueValues, whenFalseValues, out tests, out tests2, ref foundExplicitNullTest);
			whenTrue = DecisionDagBuilder.<SplitCase>g__makeNext|32_0(tests, ref CS$<>8__locals1);
			whenFalse = DecisionDagBuilder.<SplitCase>g__makeNext|32_0(tests2, ref CS$<>8__locals1);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0003454C File Offset: 0x0003274C
		private void SplitCases([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.StateForCase> statesForCases, ImmutableDictionary<BoundDagTemp, IValueSet> values, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<DecisionDagBuilder.StateForCase> whenTrue, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<DecisionDagBuilder.StateForCase> whenFalse, out ImmutableDictionary<BoundDagTemp, IValueSet> whenTrueValues, out ImmutableDictionary<BoundDagTemp, IValueSet> whenFalseValues, ref bool foundExplicitNullTest)
		{
			ArrayBuilder<DecisionDagBuilder.StateForCase> instance = ArrayBuilder<DecisionDagBuilder.StateForCase>.GetInstance(statesForCases.Length);
			ArrayBuilder<DecisionDagBuilder.StateForCase> instance2 = ArrayBuilder<DecisionDagBuilder.StateForCase>.GetInstance(statesForCases.Length);
			ValueTuple<ImmutableDictionary<BoundDagTemp, IValueSet>, ImmutableDictionary<BoundDagTemp, IValueSet>, bool, bool> valueTuple = DecisionDagBuilder.SplitValues(values, test);
			whenTrueValues = valueTuple.Item1;
			whenFalseValues = valueTuple.Item2;
			bool item = valueTuple.Item3;
			bool item2 = valueTuple.Item4;
			foreach (DecisionDagBuilder.StateForCase stateForCase in statesForCases)
			{
				IValueSet valueSet;
				IValueSet valueSet2;
				DecisionDagBuilder.StateForCase stateForCase2;
				DecisionDagBuilder.StateForCase stateForCase3;
				this.SplitCase(stateForCase, test, whenTrueValues.TryGetValue(test.Input, out valueSet) ? valueSet : null, whenFalseValues.TryGetValue(test.Input, out valueSet2) ? valueSet2 : null, out stateForCase2, out stateForCase3, ref foundExplicitNullTest);
				if (item && !stateForCase2.IsImpossible && (!instance.Any() || !instance.Last().IsFullyMatched))
				{
					instance.Add(stateForCase2);
				}
				if (item2 && !stateForCase3.IsImpossible && (!instance2.Any() || !instance2.Last().IsFullyMatched))
				{
					instance2.Add(stateForCase3);
				}
			}
			whenTrue = instance.ToImmutableAndFree();
			whenFalse = instance2.ToImmutableAndFree();
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0003466C File Offset: 0x0003286C
		[return: TupleElementNames(new string[] { "whenTrueValues", "whenFalseValues", "truePossible", "falsePossible" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 1 })]
		private static ValueTuple<ImmutableDictionary<BoundDagTemp, IValueSet>, ImmutableDictionary<BoundDagTemp, IValueSet>, bool, bool> SplitValues(ImmutableDictionary<BoundDagTemp, IValueSet> values, BoundDagTest test)
		{
			DecisionDagBuilder.<>c__DisplayClass34_0 CS$<>8__locals1;
			CS$<>8__locals1.test = test;
			CS$<>8__locals1.values = values;
			if (CS$<>8__locals1.test is BoundDagEvaluation || CS$<>8__locals1.test is BoundDagExplicitNullTest || CS$<>8__locals1.test is BoundDagNonNullTest || CS$<>8__locals1.test is BoundDagTypeTest)
			{
				return new ValueTuple<ImmutableDictionary<BoundDagTemp, IValueSet>, ImmutableDictionary<BoundDagTemp, IValueSet>, bool, bool>(CS$<>8__locals1.values, CS$<>8__locals1.values, true, true);
			}
			BoundDagValueTest boundDagValueTest = CS$<>8__locals1.test as BoundDagValueTest;
			if (boundDagValueTest != null)
			{
				return DecisionDagBuilder.<SplitValues>g__resultForRelation|34_0(BinaryOperatorKind.Equal, boundDagValueTest.Value, ref CS$<>8__locals1);
			}
			BoundDagRelationalTest boundDagRelationalTest = CS$<>8__locals1.test as BoundDagRelationalTest;
			if (boundDagRelationalTest == null)
			{
				throw ExceptionUtilities.UnexpectedValue(CS$<>8__locals1.test);
			}
			return DecisionDagBuilder.<SplitValues>g__resultForRelation|34_0(boundDagRelationalTest.Relation, boundDagRelationalTest.Value, ref CS$<>8__locals1);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00034724 File Offset: 0x00032924
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<DecisionDagBuilder.StateForCase> RemoveEvaluation([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.StateForCase> cases, BoundDagEvaluation e)
		{
			ArrayBuilder<DecisionDagBuilder.StateForCase> instance = ArrayBuilder<DecisionDagBuilder.StateForCase>.GetInstance(cases.Length);
			foreach (DecisionDagBuilder.StateForCase stateForCase in cases)
			{
				DecisionDagBuilder.Tests tests = stateForCase.RemainingTests.RemoveEvaluation(e);
				if (!(tests is DecisionDagBuilder.Tests.False))
				{
					instance.Add(new DecisionDagBuilder.StateForCase(stateForCase.Index, stateForCase.Syntax, tests, stateForCase.Bindings, stateForCase.WhenClause, stateForCase.CaseLabel));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000347A0 File Offset: 0x000329A0
		private void CheckConsistentDecision(BoundDagTest test, BoundDagTest other, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, SyntaxNode syntax, out bool trueTestPermitsTrueOther, out bool falseTestPermitsTrueOther, out bool trueTestImpliesTrueOther, out bool falseTestImpliesTrueOther, ref bool foundExplicitNullTest)
		{
			DecisionDagBuilder.<>c__DisplayClass36_0 CS$<>8__locals1;
			CS$<>8__locals1.test = test;
			CS$<>8__locals1.other = other;
			CS$<>8__locals1.whenTrueValues = whenTrueValues;
			CS$<>8__locals1.whenFalseValues = whenFalseValues;
			trueTestPermitsTrueOther = true;
			falseTestPermitsTrueOther = true;
			trueTestImpliesTrueOther = false;
			falseTestImpliesTrueOther = false;
			if (!CS$<>8__locals1.test.Input.Equals(CS$<>8__locals1.other.Input))
			{
				return;
			}
			if (!(CS$<>8__locals1.test is BoundDagNonNullTest))
			{
				BoundDagTypeTest boundDagTypeTest = CS$<>8__locals1.test as BoundDagTypeTest;
				if (boundDagTypeTest == null)
				{
					if (!(CS$<>8__locals1.test is BoundDagValueTest) && !(CS$<>8__locals1.test is BoundDagRelationalTest))
					{
						if (!(CS$<>8__locals1.test is BoundDagExplicitNullTest))
						{
							return;
						}
						foundExplicitNullTest = true;
						BoundDagNonNullTest boundDagNonNullTest = CS$<>8__locals1.other as BoundDagNonNullTest;
						if (boundDagNonNullTest != null)
						{
							if (boundDagNonNullTest.IsExplicitTest)
							{
								foundExplicitNullTest = true;
							}
							trueTestPermitsTrueOther = false;
							falseTestImpliesTrueOther = true;
							return;
						}
						if (CS$<>8__locals1.other is BoundDagTypeTest)
						{
							trueTestPermitsTrueOther = false;
							return;
						}
						if (CS$<>8__locals1.other is BoundDagExplicitNullTest)
						{
							foundExplicitNullTest = true;
							trueTestImpliesTrueOther = true;
							falseTestPermitsTrueOther = false;
							return;
						}
						if (!(CS$<>8__locals1.other is BoundDagValueTest))
						{
							return;
						}
						trueTestPermitsTrueOther = false;
					}
					else
					{
						BoundDagNonNullTest boundDagNonNullTest2 = CS$<>8__locals1.other as BoundDagNonNullTest;
						if (boundDagNonNullTest2 != null)
						{
							if (boundDagNonNullTest2.IsExplicitTest)
							{
								foundExplicitNullTest = true;
							}
							trueTestImpliesTrueOther = true;
							return;
						}
						if (!(CS$<>8__locals1.other is BoundDagTypeTest))
						{
							if (CS$<>8__locals1.other is BoundDagExplicitNullTest)
							{
								foundExplicitNullTest = true;
								trueTestPermitsTrueOther = false;
								return;
							}
							BoundDagRelationalTest boundDagRelationalTest = CS$<>8__locals1.other as BoundDagRelationalTest;
							if (boundDagRelationalTest != null)
							{
								DecisionDagBuilder.<CheckConsistentDecision>g__handleRelationWithValue|36_0(boundDagRelationalTest.Relation, boundDagRelationalTest.Value, out trueTestPermitsTrueOther, out falseTestPermitsTrueOther, out trueTestImpliesTrueOther, out falseTestImpliesTrueOther, ref CS$<>8__locals1);
								return;
							}
							BoundDagValueTest boundDagValueTest = CS$<>8__locals1.other as BoundDagValueTest;
							if (boundDagValueTest == null)
							{
								return;
							}
							DecisionDagBuilder.<CheckConsistentDecision>g__handleRelationWithValue|36_0(BinaryOperatorKind.Equal, boundDagValueTest.Value, out trueTestPermitsTrueOther, out falseTestPermitsTrueOther, out trueTestImpliesTrueOther, out falseTestImpliesTrueOther, ref CS$<>8__locals1);
							return;
						}
					}
				}
				else
				{
					BoundDagNonNullTest boundDagNonNullTest3 = CS$<>8__locals1.other as BoundDagNonNullTest;
					if (boundDagNonNullTest3 != null)
					{
						if (boundDagNonNullTest3.IsExplicitTest)
						{
							foundExplicitNullTest = true;
						}
						trueTestImpliesTrueOther = true;
						return;
					}
					BoundDagTypeTest boundDagTypeTest2 = CS$<>8__locals1.other as BoundDagTypeTest;
					if (boundDagTypeTest2 == null)
					{
						if (!(CS$<>8__locals1.other is BoundDagValueTest))
						{
							if (!(CS$<>8__locals1.other is BoundDagExplicitNullTest))
							{
								return;
							}
							foundExplicitNullTest = true;
							trueTestPermitsTrueOther = false;
							return;
						}
					}
					else
					{
						CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(this._diagnostics, this._compilation.Assembly);
						bool? flag = this.ExpressionOfTypeMatchesPatternTypeForLearningFromSuccessfulTypeTest(boundDagTypeTest.Type, boundDagTypeTest2.Type, ref compoundUseSiteInfo);
						bool? flag2 = flag;
						bool flag3 = false;
						if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
						{
							trueTestPermitsTrueOther = false;
						}
						else
						{
							flag2 = flag;
							flag3 = true;
							if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
							{
								trueTestImpliesTrueOther = true;
							}
						}
						Conversion conversion;
						flag = Binder.ExpressionOfTypeMatchesPatternType(this._conversions, boundDagTypeTest2.Type, boundDagTypeTest.Type, ref compoundUseSiteInfo, out conversion, null, false);
						this._diagnostics.Add(syntax, compoundUseSiteInfo);
						flag2 = flag;
						flag3 = true;
						if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
						{
							falseTestPermitsTrueOther = false;
							return;
						}
					}
				}
				return;
			}
			if (CS$<>8__locals1.other is BoundDagValueTest)
			{
				falseTestPermitsTrueOther = false;
				return;
			}
			if (CS$<>8__locals1.other is BoundDagExplicitNullTest)
			{
				foundExplicitNullTest = true;
				trueTestPermitsTrueOther = false;
				falseTestImpliesTrueOther = true;
				return;
			}
			BoundDagNonNullTest boundDagNonNullTest4 = CS$<>8__locals1.other as BoundDagNonNullTest;
			if (boundDagNonNullTest4 == null)
			{
				falseTestPermitsTrueOther = false;
				return;
			}
			if (boundDagNonNullTest4.IsExplicitTest)
			{
				foundExplicitNullTest = true;
			}
			trueTestImpliesTrueOther = true;
			falseTestPermitsTrueOther = false;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00034AD4 File Offset: 0x00032CD4
		private bool? ExpressionOfTypeMatchesPatternTypeForLearningFromSuccessfulTypeTest(TypeSymbol expressionType, TypeSymbol patternType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion;
			bool? flag = Binder.ExpressionOfTypeMatchesPatternType(this._conversions, expressionType, patternType, ref useSiteInfo, out conversion, null, false);
			if (conversion.Exists || !DecisionDagBuilder.<ExpressionOfTypeMatchesPatternTypeForLearningFromSuccessfulTypeTest>g__isRuntimeSimilar|37_0(expressionType, patternType))
			{
				return flag;
			}
			return default(bool?);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00034B14 File Offset: 0x00032D14
		[CompilerGenerated]
		internal static DecisionDagBuilder.Tests <SimplifyTestsAndBindings>g__scanAndSimplify|13_0(DecisionDagBuilder.Tests tests, ref DecisionDagBuilder.<>c__DisplayClass13_0 A_1)
		{
			DecisionDagBuilder.Tests.SequenceTests sequenceTests = tests as DecisionDagBuilder.Tests.SequenceTests;
			if (sequenceTests != null)
			{
				ImmutableArray<DecisionDagBuilder.Tests> remainingTests = sequenceTests.RemainingTests;
				int length = remainingTests.Length;
				ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(length);
				instance.AddRange(remainingTests);
				for (int i = length - 1; i >= 0; i--)
				{
					instance[i] = DecisionDagBuilder.<SimplifyTestsAndBindings>g__scanAndSimplify|13_0(instance[i], ref A_1);
				}
				return sequenceTests.Update(instance);
			}
			if (!(tests is DecisionDagBuilder.Tests.True) && !(tests is DecisionDagBuilder.Tests.False))
			{
				DecisionDagBuilder.Tests.One one = tests as DecisionDagBuilder.Tests.One;
				if (one != null)
				{
					BoundDagTest boundDagTest;
					one.Deconstruct(out boundDagTest);
					BoundDagEvaluation boundDagEvaluation = boundDagTest as BoundDagEvaluation;
					if (boundDagEvaluation == null)
					{
						if (boundDagTest != null)
						{
							if (boundDagTest.Input.Source != null)
							{
								A_1.usedValues.Add(boundDagTest.Input.Source);
							}
							return tests;
						}
					}
					else
					{
						if (A_1.usedValues.Contains(boundDagEvaluation))
						{
							if (boundDagEvaluation.Input.Source != null)
							{
								A_1.usedValues.Add(boundDagEvaluation.Input.Source);
							}
							return tests;
						}
						return DecisionDagBuilder.Tests.True.Instance;
					}
				}
				else
				{
					DecisionDagBuilder.Tests.Not not = tests as DecisionDagBuilder.Tests.Not;
					if (not != null)
					{
						return DecisionDagBuilder.Tests.Not.Create(DecisionDagBuilder.<SimplifyTestsAndBindings>g__scanAndSimplify|13_0(not.Negated, ref A_1));
					}
				}
				throw ExceptionUtilities.UnexpectedValue(tests);
			}
			return tests;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00034C4C File Offset: 0x00032E4C
		[CompilerGenerated]
		internal static DecisionDagBuilder.DagState <MakeDecisionDag>g__uniqifyState|30_0([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.StateForCase> cases, ImmutableDictionary<BoundDagTemp, IValueSet> remainingValues, ref DecisionDagBuilder.<>c__DisplayClass30_0 A_2)
		{
			DecisionDagBuilder.DagState dagState = new DecisionDagBuilder.DagState(cases, remainingValues);
			DecisionDagBuilder.DagState dagState2;
			if (A_2.uniqueState.TryGetValue(dagState, ref dagState2))
			{
				ImmutableDictionary<BoundDagTemp, IValueSet>.Builder newRemainingValues = ImmutableDictionary.CreateBuilder<BoundDagTemp, IValueSet>();
				foreach (KeyValuePair<BoundDagTemp, IValueSet> keyValuePair in remainingValues)
				{
					BoundDagTemp boundDagTemp;
					IValueSet valueSet;
					keyValuePair.Deconstruct<BoundDagTemp, IValueSet>(out boundDagTemp, out valueSet);
					BoundDagTemp boundDagTemp2 = boundDagTemp;
					IValueSet valueSet2 = valueSet;
					IValueSet valueSet3;
					if (dagState2.RemainingValues.TryGetValue(boundDagTemp2, out valueSet3))
					{
						IValueSet valueSet4 = valueSet3.Union(valueSet2);
						newRemainingValues.Add(boundDagTemp2, valueSet4);
					}
				}
				if (dagState2.RemainingValues.Count != newRemainingValues.Count || !Enumerable.All<KeyValuePair<BoundDagTemp, IValueSet>>(dagState2.RemainingValues, delegate(KeyValuePair<BoundDagTemp, IValueSet> kv)
				{
					IValueSet valueSet5;
					return newRemainingValues.TryGetValue(kv.Key, out valueSet5) && kv.Value.Equals(valueSet5);
				}))
				{
					dagState2.UpdateRemainingValues(newRemainingValues.ToImmutable());
					if (!A_2.workList.Contains(dagState2))
					{
						A_2.workList.Push(dagState2);
					}
				}
				return dagState2;
			}
			A_2.uniqueState.Add(dagState, dagState);
			A_2.workList.Push(dagState);
			return dagState;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00034D74 File Offset: 0x00032F74
		[CompilerGenerated]
		internal static BoundDecisionDagNode <ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(BoundDecisionDagNode node, ref DecisionDagBuilder.<>c__DisplayClass31_0 A_1)
		{
			return A_1.uniqueNodes.GetOrAdd(node, node);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00034D84 File Offset: 0x00032F84
		[CompilerGenerated]
		internal static BoundDecisionDagNode <ComputeBoundDecisionDagNodes>g__finalState|31_1(SyntaxNode syntax, LabelSymbol label, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<BoundPatternBinding> bindings, ref DecisionDagBuilder.<>c__DisplayClass31_0 A_3)
		{
			BoundDecisionDagNode boundDecisionDagNode = DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(new BoundLeafDecisionDagNode(syntax, label), ref A_3);
			if (!bindings.IsDefaultOrEmpty)
			{
				return DecisionDagBuilder.<ComputeBoundDecisionDagNodes>g__uniqifyDagNode|31_0(new BoundWhenDecisionDagNode(syntax, bindings, null, boundDecisionDagNode, null, false), ref A_3);
			}
			return boundDecisionDagNode;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00034DBC File Offset: 0x00032FBC
		[CompilerGenerated]
		internal static DecisionDagBuilder.StateForCase <SplitCase>g__makeNext|32_0(DecisionDagBuilder.Tests remainingTests, ref DecisionDagBuilder.<>c__DisplayClass32_0 A_1)
		{
			if (!remainingTests.Equals(A_1.stateForCase.RemainingTests))
			{
				return new DecisionDagBuilder.StateForCase(A_1.stateForCase.Index, A_1.stateForCase.Syntax, remainingTests, A_1.stateForCase.Bindings, A_1.stateForCase.WhenClause, A_1.stateForCase.CaseLabel);
			}
			return A_1.stateForCase;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00034E20 File Offset: 0x00033020
		[CompilerGenerated]
		[return: TupleElementNames(new string[] { "whenTrueValues", "whenFalseValues", "truePossible", "falsePossible" })]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1, 1, 1, 1 })]
		internal static ValueTuple<ImmutableDictionary<BoundDagTemp, IValueSet>, ImmutableDictionary<BoundDagTemp, IValueSet>, bool, bool> <SplitValues>g__resultForRelation|34_0(BinaryOperatorKind relation, ConstantValue value, ref DecisionDagBuilder.<>c__DisplayClass34_0 A_2)
		{
			BoundDagTemp input = A_2.test.Input;
			IValueSetFactory valueSetFactory = ValueSetFactory.ForType(input.Type);
			if (valueSetFactory == null || value.IsBad)
			{
				return new ValueTuple<ImmutableDictionary<BoundDagTemp, IValueSet>, ImmutableDictionary<BoundDagTemp, IValueSet>, bool, bool>(A_2.values, A_2.values, true, true);
			}
			IValueSet valueSet = valueSetFactory.Related(relation.Operator(), value);
			IValueSet valueSet2 = valueSet.Complement();
			IValueSet valueSet3;
			if (A_2.values.TryGetValue(A_2.test.Input, out valueSet3))
			{
				valueSet = valueSet.Intersect(valueSet3);
				valueSet2 = valueSet2.Intersect(valueSet3);
			}
			ImmutableDictionary<BoundDagTemp, IValueSet> immutableDictionary = A_2.values.SetItem(input, valueSet);
			ImmutableDictionary<BoundDagTemp, IValueSet> immutableDictionary2 = A_2.values.SetItem(input, valueSet2);
			return new ValueTuple<ImmutableDictionary<BoundDagTemp, IValueSet>, ImmutableDictionary<BoundDagTemp, IValueSet>, bool, bool>(immutableDictionary, immutableDictionary2, !valueSet.IsEmpty, !valueSet2.IsEmpty);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00034EDC File Offset: 0x000330DC
		[CompilerGenerated]
		internal static void <CheckConsistentDecision>g__handleRelationWithValue|36_0(BinaryOperatorKind relation, ConstantValue value, out bool trueTestPermitsTrueOther, out bool falseTestPermitsTrueOther, out bool trueTestImpliesTrueOther, out bool falseTestImpliesTrueOther, ref DecisionDagBuilder.<>c__DisplayClass36_0 A_6)
		{
			bool flag = A_6.test.Equals(A_6.other);
			IValueSet whenTrueValues = A_6.whenTrueValues;
			trueTestPermitsTrueOther = whenTrueValues == null || whenTrueValues.Any(relation, value);
			bool flag2;
			if (!flag)
			{
				if (trueTestPermitsTrueOther)
				{
					IValueSet whenTrueValues2 = A_6.whenTrueValues;
					flag2 = whenTrueValues2 != null && whenTrueValues2.All(relation, value);
				}
				else
				{
					flag2 = false;
				}
			}
			else
			{
				flag2 = true;
			}
			trueTestImpliesTrueOther = flag2;
			bool flag3;
			if (!flag)
			{
				IValueSet whenFalseValues = A_6.whenFalseValues;
				flag3 = whenFalseValues == null || whenFalseValues.Any(relation, value);
			}
			else
			{
				flag3 = false;
			}
			falseTestPermitsTrueOther = flag3;
			bool flag4;
			if (falseTestPermitsTrueOther)
			{
				IValueSet whenFalseValues2 = A_6.whenFalseValues;
				flag4 = whenFalseValues2 != null && whenFalseValues2.All(relation, value);
			}
			else
			{
				flag4 = false;
			}
			falseTestImpliesTrueOther = flag4;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00034F78 File Offset: 0x00033178
		[CompilerGenerated]
		internal static bool <ExpressionOfTypeMatchesPatternTypeForLearningFromSuccessfulTypeTest>g__isRuntimeSimilar|37_0(TypeSymbol expressionType, TypeSymbol patternType)
		{
			for (;;)
			{
				ArrayTypeSymbol arrayTypeSymbol = expressionType as ArrayTypeSymbol;
				if (arrayTypeSymbol == null)
				{
					return false;
				}
				TypeSymbol typeSymbol = arrayTypeSymbol.ElementType;
				bool isSZArray = arrayTypeSymbol.IsSZArray;
				int rank = arrayTypeSymbol.Rank;
				arrayTypeSymbol = patternType as ArrayTypeSymbol;
				if (arrayTypeSymbol == null)
				{
					return false;
				}
				TypeSymbol typeSymbol2 = arrayTypeSymbol.ElementType;
				bool isSZArray2 = arrayTypeSymbol.IsSZArray;
				int rank2 = arrayTypeSymbol.Rank;
				if (isSZArray != isSZArray2 || rank != rank2)
				{
					return false;
				}
				typeSymbol = typeSymbol.EnumUnderlyingTypeOrSelf();
				typeSymbol2 = typeSymbol2.EnumUnderlyingTypeOrSelf();
				SpecialType specialType = typeSymbol.SpecialType;
				SpecialType specialType2 = typeSymbol2.SpecialType;
				if (specialType == specialType2)
				{
					break;
				}
				switch (specialType)
				{
				case SpecialType.System_SByte:
					if (specialType2 == SpecialType.System_Byte)
					{
						return true;
					}
					break;
				case SpecialType.System_Byte:
					if (specialType2 == SpecialType.System_SByte)
					{
						return true;
					}
					break;
				case SpecialType.System_Int16:
					if (specialType2 == SpecialType.System_UInt16)
					{
						return true;
					}
					break;
				case SpecialType.System_UInt16:
					if (specialType2 == SpecialType.System_Int16)
					{
						return true;
					}
					break;
				case SpecialType.System_Int32:
					if (specialType2 == SpecialType.System_UInt32 || specialType2 - SpecialType.System_IntPtr <= 1)
					{
						return true;
					}
					break;
				case SpecialType.System_UInt32:
					if (specialType2 == SpecialType.System_Int32 || specialType2 - SpecialType.System_IntPtr <= 1)
					{
						return true;
					}
					break;
				case SpecialType.System_Int64:
					if (specialType2 == SpecialType.System_UInt64 || specialType2 - SpecialType.System_IntPtr <= 1)
					{
						return true;
					}
					break;
				case SpecialType.System_UInt64:
					if (specialType2 == SpecialType.System_Int64 || specialType2 - SpecialType.System_IntPtr <= 1)
					{
						return true;
					}
					break;
				case SpecialType.System_IntPtr:
					if (specialType2 - SpecialType.System_Int32 <= 3 || specialType2 == SpecialType.System_UIntPtr)
					{
						return true;
					}
					break;
				case SpecialType.System_UIntPtr:
					if (specialType2 - SpecialType.System_Int32 <= 3 || specialType2 == SpecialType.System_IntPtr)
					{
						return true;
					}
					break;
				}
				TypeSymbol typeSymbol3 = typeSymbol;
				TypeSymbol typeSymbol4 = typeSymbol2;
				expressionType = typeSymbol3;
				patternType = typeSymbol4;
			}
			return true;
		}

		// Token: 0x0400004C RID: 76
		private readonly CSharpCompilation _compilation;

		// Token: 0x0400004D RID: 77
		private readonly Conversions _conversions;

		// Token: 0x0400004E RID: 78
		private readonly BindingDiagnosticBag _diagnostics;

		// Token: 0x0400004F RID: 79
		private readonly LabelSymbol _defaultLabel;

		// Token: 0x0200062E RID: 1582
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class DecisionDag
		{
			// Token: 0x06008234 RID: 33332 RVA: 0x001D9E5E File Offset: 0x001D805E
			public DecisionDag(DecisionDagBuilder.DagState rootNode)
			{
				this.RootNode = rootNode;
			}

			// Token: 0x06008235 RID: 33333 RVA: 0x001D9E70 File Offset: 0x001D8070
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<DecisionDagBuilder.DagState> Successor(DecisionDagBuilder.DagState state)
			{
				if (state.TrueBranch != null && state.FalseBranch != null)
				{
					return ImmutableArray.Create<DecisionDagBuilder.DagState>(state.FalseBranch, state.TrueBranch);
				}
				if (state.TrueBranch != null)
				{
					return ImmutableArray.Create<DecisionDagBuilder.DagState>(state.TrueBranch);
				}
				if (state.FalseBranch != null)
				{
					return ImmutableArray.Create<DecisionDagBuilder.DagState>(state.FalseBranch);
				}
				return ImmutableArray<DecisionDagBuilder.DagState>.Empty;
			}

			// Token: 0x06008236 RID: 33334 RVA: 0x001D9ECC File Offset: 0x001D80CC
			public bool TryGetTopologicallySortedReachableStates([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<DecisionDagBuilder.DagState> result)
			{
				return TopologicalSort.TryIterativeSort<DecisionDagBuilder.DagState>(SpecializedCollections.SingletonEnumerable<DecisionDagBuilder.DagState>(this.RootNode), new Func<DecisionDagBuilder.DagState, ImmutableArray<DecisionDagBuilder.DagState>>(DecisionDagBuilder.DecisionDag.Successor), out result);
			}

			// Token: 0x04001F51 RID: 8017
			public readonly DecisionDagBuilder.DagState RootNode;
		}

		// Token: 0x0200062F RID: 1583
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class DagState
		{
			// Token: 0x17002679 RID: 9849
			// (get) Token: 0x06008237 RID: 33335 RVA: 0x001D9EEB File Offset: 0x001D80EB
			// (set) Token: 0x06008238 RID: 33336 RVA: 0x001D9EF3 File Offset: 0x001D80F3
			public ImmutableDictionary<BoundDagTemp, IValueSet> RemainingValues { get; private set; }

			// Token: 0x06008239 RID: 33337 RVA: 0x001D9EFC File Offset: 0x001D80FC
			public DagState([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.StateForCase> cases, ImmutableDictionary<BoundDagTemp, IValueSet> remainingValues)
			{
				this.Cases = cases;
				this.RemainingValues = remainingValues;
			}

			// Token: 0x0600823A RID: 33338 RVA: 0x001D9F14 File Offset: 0x001D8114
			internal BoundDagTest ComputeSelectedTest()
			{
				return this.Cases[0].RemainingTests.ComputeSelectedTest();
			}

			// Token: 0x0600823B RID: 33339 RVA: 0x001D9F3A File Offset: 0x001D813A
			internal void UpdateRemainingValues(ImmutableDictionary<BoundDagTemp, IValueSet> newRemainingValues)
			{
				this.RemainingValues = newRemainingValues;
				this.SelectedTest = null;
				this.TrueBranch = null;
				this.FalseBranch = null;
			}

			// Token: 0x04001F53 RID: 8019
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public readonly ImmutableArray<DecisionDagBuilder.StateForCase> Cases;

			// Token: 0x04001F54 RID: 8020
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public BoundDagTest SelectedTest;

			// Token: 0x04001F55 RID: 8021
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public DecisionDagBuilder.DagState TrueBranch;

			// Token: 0x04001F56 RID: 8022
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public DecisionDagBuilder.DagState FalseBranch;

			// Token: 0x04001F57 RID: 8023
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public BoundDecisionDagNode Dag;
		}

		// Token: 0x02000630 RID: 1584
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class DagStateEquivalence : IEqualityComparer<DecisionDagBuilder.DagState>
		{
			// Token: 0x0600823C RID: 33340 RVA: 0x001D9F58 File Offset: 0x001D8158
			private DagStateEquivalence()
			{
			}

			// Token: 0x0600823D RID: 33341 RVA: 0x001D9F60 File Offset: 0x001D8160
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public bool Equals(DecisionDagBuilder.DagState x, DecisionDagBuilder.DagState y)
			{
				if (x != y)
				{
					return x.Cases.SequenceEqual<DecisionDagBuilder.StateForCase, DecisionDagBuilder.StateForCase>(y.Cases, (DecisionDagBuilder.StateForCase a, DecisionDagBuilder.StateForCase b) => a.Equals(b));
				}
				return true;
			}

			// Token: 0x0600823E RID: 33342 RVA: 0x001D9F98 File Offset: 0x001D8198
			public int GetHashCode(DecisionDagBuilder.DagState x)
			{
				return Hash.Combine(Hash.CombineValues<DecisionDagBuilder.StateForCase>(x.Cases, int.MaxValue), x.Cases.Length);
			}

			// Token: 0x04001F58 RID: 8024
			public static readonly DecisionDagBuilder.DagStateEquivalence Instance = new DecisionDagBuilder.DagStateEquivalence();
		}

		// Token: 0x02000631 RID: 1585
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private sealed class StateForCase
		{
			// Token: 0x06008240 RID: 33344 RVA: 0x001D9FD4 File Offset: 0x001D81D4
			public StateForCase(int Index, SyntaxNode Syntax, DecisionDagBuilder.Tests RemainingTests, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<BoundPatternBinding> Bindings, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression WhenClause, LabelSymbol CaseLabel)
			{
				this.Index = Index;
				this.Syntax = Syntax;
				this.RemainingTests = RemainingTests;
				this.Bindings = Bindings;
				this.WhenClause = WhenClause;
				this.CaseLabel = CaseLabel;
			}

			// Token: 0x1700267A RID: 9850
			// (get) Token: 0x06008241 RID: 33345 RVA: 0x001DA009 File Offset: 0x001D8209
			public bool IsFullyMatched
			{
				get
				{
					return this.RemainingTests is DecisionDagBuilder.Tests.True && (this.WhenClause == null || this.WhenClause.ConstantValue == ConstantValue.True);
				}
			}

			// Token: 0x1700267B RID: 9851
			// (get) Token: 0x06008242 RID: 33346 RVA: 0x001DA039 File Offset: 0x001D8239
			public bool PatternIsSatisfied
			{
				get
				{
					return this.RemainingTests is DecisionDagBuilder.Tests.True;
				}
			}

			// Token: 0x1700267C RID: 9852
			// (get) Token: 0x06008243 RID: 33347 RVA: 0x001DA049 File Offset: 0x001D8249
			public bool IsImpossible
			{
				get
				{
					return this.RemainingTests is DecisionDagBuilder.Tests.False;
				}
			}

			// Token: 0x06008244 RID: 33348 RVA: 0x001DA059 File Offset: 0x001D8259
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x06008245 RID: 33349 RVA: 0x001DA060 File Offset: 0x001D8260
			public bool Equals(DecisionDagBuilder.StateForCase other)
			{
				return this == other || (other != null && this.Index == other.Index && this.RemainingTests.Equals(other.RemainingTests));
			}

			// Token: 0x06008246 RID: 33350 RVA: 0x001DA08C File Offset: 0x001D828C
			public override int GetHashCode()
			{
				return Hash.Combine(this.RemainingTests.GetHashCode(), this.Index);
			}

			// Token: 0x04001F59 RID: 8025
			public readonly int Index;

			// Token: 0x04001F5A RID: 8026
			public readonly SyntaxNode Syntax;

			// Token: 0x04001F5B RID: 8027
			public readonly DecisionDagBuilder.Tests RemainingTests;

			// Token: 0x04001F5C RID: 8028
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public readonly ImmutableArray<BoundPatternBinding> Bindings;

			// Token: 0x04001F5D RID: 8029
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public readonly BoundExpression WhenClause;

			// Token: 0x04001F5E RID: 8030
			public readonly LabelSymbol CaseLabel;
		}

		// Token: 0x02000632 RID: 1586
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private abstract class Tests
		{
			// Token: 0x06008247 RID: 33351 RVA: 0x001DA0A4 File Offset: 0x001D82A4
			private Tests()
			{
			}

			// Token: 0x06008248 RID: 33352
			public abstract void Filter(DecisionDagBuilder builder, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.Tests whenTrue, out DecisionDagBuilder.Tests whenFalse, ref bool foundExplicitNullTest);

			// Token: 0x06008249 RID: 33353 RVA: 0x001DA0AC File Offset: 0x001D82AC
			public virtual BoundDagTest ComputeSelectedTest()
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x0600824A RID: 33354 RVA: 0x001DA0B3 File Offset: 0x001D82B3
			public virtual DecisionDagBuilder.Tests RemoveEvaluation(BoundDagEvaluation e)
			{
				return this;
			}

			// Token: 0x0600824B RID: 33355
			public abstract string Dump(Func<BoundDagTest, string> dump);

			// Token: 0x020009B8 RID: 2488
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public sealed class True : DecisionDagBuilder.Tests
			{
				// Token: 0x060092C4 RID: 37572 RVA: 0x0020113B File Offset: 0x001FF33B
				public override string Dump(Func<BoundDagTest, string> dump)
				{
					return "TRUE";
				}

				// Token: 0x060092C5 RID: 37573 RVA: 0x00201144 File Offset: 0x001FF344
				public override void Filter(DecisionDagBuilder builder, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.Tests whenTrue, out DecisionDagBuilder.Tests whenFalse, ref bool foundExplicitNullTest)
				{
					whenFalse = this;
					whenTrue = this;
				}

				// Token: 0x04002998 RID: 10648
				public static readonly DecisionDagBuilder.Tests.True Instance = new DecisionDagBuilder.Tests.True();
			}

			// Token: 0x020009B9 RID: 2489
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public sealed class False : DecisionDagBuilder.Tests
			{
				// Token: 0x060092C8 RID: 37576 RVA: 0x0020116F File Offset: 0x001FF36F
				public override string Dump(Func<BoundDagTest, string> dump)
				{
					return "FALSE";
				}

				// Token: 0x060092C9 RID: 37577 RVA: 0x00201178 File Offset: 0x001FF378
				public override void Filter(DecisionDagBuilder builder, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.Tests whenTrue, out DecisionDagBuilder.Tests whenFalse, ref bool foundExplicitNullTest)
				{
					whenFalse = this;
					whenTrue = this;
				}

				// Token: 0x04002999 RID: 10649
				public static readonly DecisionDagBuilder.Tests.False Instance = new DecisionDagBuilder.Tests.False();
			}

			// Token: 0x020009BA RID: 2490
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public sealed class One : DecisionDagBuilder.Tests
			{
				// Token: 0x060092CC RID: 37580 RVA: 0x002011A3 File Offset: 0x001FF3A3
				public One(BoundDagTest test)
				{
					this.Test = test;
				}

				// Token: 0x060092CD RID: 37581 RVA: 0x002011B2 File Offset: 0x001FF3B2
				public void Deconstruct(out BoundDagTest Test)
				{
					Test = this.Test;
				}

				// Token: 0x060092CE RID: 37582 RVA: 0x002011BC File Offset: 0x001FF3BC
				public override void Filter(DecisionDagBuilder builder, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.Tests whenTrue, out DecisionDagBuilder.Tests whenFalse, ref bool foundExplicitNullTest)
				{
					bool flag;
					bool flag2;
					bool flag3;
					bool flag4;
					builder.CheckConsistentDecision(test, this.Test, whenTrueValues, whenFalseValues, test.Syntax, out flag, out flag2, out flag3, out flag4, ref foundExplicitNullTest);
					whenTrue = (flag3 ? DecisionDagBuilder.Tests.True.Instance : (flag ? this : DecisionDagBuilder.Tests.False.Instance));
					whenFalse = (flag4 ? DecisionDagBuilder.Tests.True.Instance : (flag2 ? this : DecisionDagBuilder.Tests.False.Instance));
				}

				// Token: 0x060092CF RID: 37583 RVA: 0x00201219 File Offset: 0x001FF419
				public override BoundDagTest ComputeSelectedTest()
				{
					return this.Test;
				}

				// Token: 0x060092D0 RID: 37584 RVA: 0x00201221 File Offset: 0x001FF421
				public override DecisionDagBuilder.Tests RemoveEvaluation(BoundDagEvaluation e)
				{
					if (!e.Equals(this.Test))
					{
						return this;
					}
					return DecisionDagBuilder.Tests.True.Instance;
				}

				// Token: 0x060092D1 RID: 37585 RVA: 0x00201238 File Offset: 0x001FF438
				public override string Dump(Func<BoundDagTest, string> dump)
				{
					return dump.Invoke(this.Test);
				}

				// Token: 0x060092D2 RID: 37586 RVA: 0x00201248 File Offset: 0x001FF448
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				public override bool Equals(object obj)
				{
					if (this != obj)
					{
						DecisionDagBuilder.Tests.One one = obj as DecisionDagBuilder.Tests.One;
						return one != null && this.Test.Equals(one.Test);
					}
					return true;
				}

				// Token: 0x060092D3 RID: 37587 RVA: 0x00201278 File Offset: 0x001FF478
				public override int GetHashCode()
				{
					return this.Test.GetHashCode();
				}

				// Token: 0x0400299A RID: 10650
				public readonly BoundDagTest Test;
			}

			// Token: 0x020009BB RID: 2491
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public sealed class Not : DecisionDagBuilder.Tests
			{
				// Token: 0x060092D4 RID: 37588 RVA: 0x00201285 File Offset: 0x001FF485
				private Not(DecisionDagBuilder.Tests negated)
				{
					this.Negated = negated;
				}

				// Token: 0x060092D5 RID: 37589 RVA: 0x00201294 File Offset: 0x001FF494
				public static DecisionDagBuilder.Tests Create(DecisionDagBuilder.Tests negated)
				{
					DecisionDagBuilder.Tests tests;
					if (!(negated is DecisionDagBuilder.Tests.True))
					{
						if (!(negated is DecisionDagBuilder.Tests.False))
						{
							DecisionDagBuilder.Tests.Not not = negated as DecisionDagBuilder.Tests.Not;
							if (not == null)
							{
								DecisionDagBuilder.Tests.AndSequence andSequence = negated as DecisionDagBuilder.Tests.AndSequence;
								if (andSequence == null)
								{
									DecisionDagBuilder.Tests.OrSequence orSequence = negated as DecisionDagBuilder.Tests.OrSequence;
									if (orSequence == null)
									{
										DecisionDagBuilder.Tests.One one = negated as DecisionDagBuilder.Tests.One;
										if (one == null)
										{
											throw ExceptionUtilities.UnexpectedValue(negated);
										}
										tests = new DecisionDagBuilder.Tests.Not(one);
									}
									else
									{
										tests = DecisionDagBuilder.Tests.AndSequence.Create(DecisionDagBuilder.Tests.Not.NegateSequenceElements(orSequence.RemainingTests));
									}
								}
								else
								{
									tests = new DecisionDagBuilder.Tests.Not(andSequence);
								}
							}
							else
							{
								tests = not.Negated;
							}
						}
						else
						{
							tests = DecisionDagBuilder.Tests.True.Instance;
						}
					}
					else
					{
						tests = DecisionDagBuilder.Tests.False.Instance;
					}
					return tests;
				}

				// Token: 0x060092D6 RID: 37590 RVA: 0x00201328 File Offset: 0x001FF528
				private static ArrayBuilder<DecisionDagBuilder.Tests> NegateSequenceElements([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.Tests> seq)
				{
					ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(seq.Length);
					foreach (DecisionDagBuilder.Tests tests in seq)
					{
						instance.Add(DecisionDagBuilder.Tests.Not.Create(tests));
					}
					return instance;
				}

				// Token: 0x060092D7 RID: 37591 RVA: 0x0020136A File Offset: 0x001FF56A
				public override DecisionDagBuilder.Tests RemoveEvaluation(BoundDagEvaluation e)
				{
					return DecisionDagBuilder.Tests.Not.Create(this.Negated.RemoveEvaluation(e));
				}

				// Token: 0x060092D8 RID: 37592 RVA: 0x0020137D File Offset: 0x001FF57D
				public override BoundDagTest ComputeSelectedTest()
				{
					return this.Negated.ComputeSelectedTest();
				}

				// Token: 0x060092D9 RID: 37593 RVA: 0x0020138A File Offset: 0x001FF58A
				public override string Dump(Func<BoundDagTest, string> dump)
				{
					return "Not (" + this.Negated.Dump(dump) + ")";
				}

				// Token: 0x060092DA RID: 37594 RVA: 0x002013A8 File Offset: 0x001FF5A8
				public override void Filter(DecisionDagBuilder builder, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.Tests whenTrue, out DecisionDagBuilder.Tests whenFalse, ref bool foundExplicitNullTest)
				{
					DecisionDagBuilder.Tests tests;
					DecisionDagBuilder.Tests tests2;
					this.Negated.Filter(builder, test, whenTrueValues, whenFalseValues, out tests, out tests2, ref foundExplicitNullTest);
					whenTrue = DecisionDagBuilder.Tests.Not.Create(tests);
					whenFalse = DecisionDagBuilder.Tests.Not.Create(tests2);
				}

				// Token: 0x060092DB RID: 37595 RVA: 0x002013E0 File Offset: 0x001FF5E0
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				public override bool Equals(object obj)
				{
					if (this != obj)
					{
						DecisionDagBuilder.Tests.Not not = obj as DecisionDagBuilder.Tests.Not;
						return not != null && this.Negated.Equals(not.Negated);
					}
					return true;
				}

				// Token: 0x060092DC RID: 37596 RVA: 0x00201410 File Offset: 0x001FF610
				public override int GetHashCode()
				{
					return Hash.Combine(this.Negated.GetHashCode(), typeof(DecisionDagBuilder.Tests.Not).GetHashCode());
				}

				// Token: 0x0400299B RID: 10651
				public readonly DecisionDagBuilder.Tests Negated;
			}

			// Token: 0x020009BC RID: 2492
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public abstract class SequenceTests : DecisionDagBuilder.Tests
			{
				// Token: 0x060092DD RID: 37597 RVA: 0x00201431 File Offset: 0x001FF631
				protected SequenceTests([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.Tests> remainingTests)
				{
					this.RemainingTests = remainingTests;
				}

				// Token: 0x060092DE RID: 37598
				public abstract DecisionDagBuilder.Tests Update(ArrayBuilder<DecisionDagBuilder.Tests> remainingTests);

				// Token: 0x060092DF RID: 37599 RVA: 0x00201440 File Offset: 0x001FF640
				public override void Filter(DecisionDagBuilder builder, BoundDagTest test, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenTrueValues, [global::System.Runtime.CompilerServices.Nullable(2)] IValueSet whenFalseValues, out DecisionDagBuilder.Tests whenTrue, out DecisionDagBuilder.Tests whenFalse, ref bool foundExplicitNullTest)
				{
					ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(this.RemainingTests.Length);
					ArrayBuilder<DecisionDagBuilder.Tests> instance2 = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(this.RemainingTests.Length);
					foreach (DecisionDagBuilder.Tests tests in this.RemainingTests)
					{
						DecisionDagBuilder.Tests tests2;
						DecisionDagBuilder.Tests tests3;
						tests.Filter(builder, test, whenTrueValues, whenFalseValues, out tests2, out tests3, ref foundExplicitNullTest);
						instance.Add(tests2);
						instance2.Add(tests3);
					}
					whenTrue = this.Update(instance);
					whenFalse = this.Update(instance2);
				}

				// Token: 0x060092E0 RID: 37600 RVA: 0x002014CC File Offset: 0x001FF6CC
				public override DecisionDagBuilder.Tests RemoveEvaluation(BoundDagEvaluation e)
				{
					ArrayBuilder<DecisionDagBuilder.Tests> instance = ArrayBuilder<DecisionDagBuilder.Tests>.GetInstance(this.RemainingTests.Length);
					foreach (DecisionDagBuilder.Tests tests in this.RemainingTests)
					{
						instance.Add(tests.RemoveEvaluation(e));
					}
					return this.Update(instance);
				}

				// Token: 0x060092E1 RID: 37601 RVA: 0x00201524 File Offset: 0x001FF724
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				public override bool Equals(object obj)
				{
					if (this != obj)
					{
						DecisionDagBuilder.Tests.SequenceTests sequenceTests = obj as DecisionDagBuilder.Tests.SequenceTests;
						return sequenceTests != null && base.GetType() == sequenceTests.GetType() && this.RemainingTests.SequenceEqual<DecisionDagBuilder.Tests, DecisionDagBuilder.Tests>(sequenceTests.RemainingTests, null);
					}
					return true;
				}

				// Token: 0x060092E2 RID: 37602 RVA: 0x00201568 File Offset: 0x001FF768
				public override int GetHashCode()
				{
					int num = Hash.Combine(this.RemainingTests.Length, base.GetType().GetHashCode());
					return Hash.Combine(Hash.CombineValues<DecisionDagBuilder.Tests>(this.RemainingTests, int.MaxValue), num);
				}

				// Token: 0x0400299C RID: 10652
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				public readonly ImmutableArray<DecisionDagBuilder.Tests> RemainingTests;
			}

			// Token: 0x020009BD RID: 2493
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public sealed class AndSequence : DecisionDagBuilder.Tests.SequenceTests
			{
				// Token: 0x060092E3 RID: 37603 RVA: 0x002015AC File Offset: 0x001FF7AC
				private AndSequence([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.Tests> remainingTests)
					: base(remainingTests)
				{
				}

				// Token: 0x060092E4 RID: 37604 RVA: 0x002015B5 File Offset: 0x001FF7B5
				public override DecisionDagBuilder.Tests Update(ArrayBuilder<DecisionDagBuilder.Tests> remainingTests)
				{
					return DecisionDagBuilder.Tests.AndSequence.Create(remainingTests);
				}

				// Token: 0x060092E5 RID: 37605 RVA: 0x002015C0 File Offset: 0x001FF7C0
				public static DecisionDagBuilder.Tests Create(ArrayBuilder<DecisionDagBuilder.Tests> remainingTests)
				{
					DecisionDagBuilder.Tests tests;
					for (int i = remainingTests.Count - 1; i >= 0; i--)
					{
						tests = remainingTests[i];
						if (!(tests is DecisionDagBuilder.Tests.True))
						{
							DecisionDagBuilder.Tests.False @false = tests as DecisionDagBuilder.Tests.False;
							if (@false != null)
							{
								remainingTests.Free();
								return @false;
							}
							DecisionDagBuilder.Tests.AndSequence andSequence = tests as DecisionDagBuilder.Tests.AndSequence;
							if (andSequence != null)
							{
								ImmutableArray<DecisionDagBuilder.Tests> remainingTests2 = andSequence.RemainingTests;
								remainingTests.RemoveAt(i);
								int j = 0;
								int length = remainingTests2.Length;
								while (j < length)
								{
									remainingTests.Insert(i + j, remainingTests2[j]);
									j++;
								}
							}
						}
						else
						{
							remainingTests.RemoveAt(i);
						}
					}
					int count = remainingTests.Count;
					if (count != 0)
					{
						if (count != 1)
						{
							tests = new DecisionDagBuilder.Tests.AndSequence(remainingTests.ToImmutable());
						}
						else
						{
							tests = remainingTests[0];
						}
					}
					else
					{
						tests = DecisionDagBuilder.Tests.True.Instance;
					}
					DecisionDagBuilder.Tests tests2 = tests;
					remainingTests.Free();
					return tests2;
				}

				// Token: 0x060092E6 RID: 37606 RVA: 0x00201694 File Offset: 0x001FF894
				public override BoundDagTest ComputeSelectedTest()
				{
					DecisionDagBuilder.Tests.One one = this.RemainingTests[0] as DecisionDagBuilder.Tests.One;
					if (one != null)
					{
						BoundDagTest test = one.Test;
						if (test != null && test.Kind == BoundKind.DagNonNullTest)
						{
							one = this.RemainingTests[1] as DecisionDagBuilder.Tests.One;
							if (one != null)
							{
								BoundDagTest test2 = one.Test;
								if (test2 != null)
								{
									BoundKind kind = test2.Kind;
									if (kind != BoundKind.DagTypeTest)
									{
										if (kind == BoundKind.DagValueTest)
										{
											BoundDagTest boundDagTest = test2;
											if (test.Input != boundDagTest.Input)
											{
												return test;
											}
											return boundDagTest;
										}
									}
									else
									{
										if (test.Input != test2.Input)
										{
											return test;
										}
										return test2;
									}
								}
							}
						}
					}
					return this.RemainingTests[0].ComputeSelectedTest();
				}

				// Token: 0x060092E7 RID: 37607 RVA: 0x00201744 File Offset: 0x001FF944
				public override string Dump(Func<BoundDagTest, string> dump)
				{
					return "AND(" + string.Join(", ", this.RemainingTests.Select<DecisionDagBuilder.Tests, string>((DecisionDagBuilder.Tests t) => t.Dump(dump))) + ")";
				}
			}

			// Token: 0x020009BE RID: 2494
			[global::System.Runtime.CompilerServices.Nullable(0)]
			public sealed class OrSequence : DecisionDagBuilder.Tests.SequenceTests
			{
				// Token: 0x060092E8 RID: 37608 RVA: 0x0020178E File Offset: 0x001FF98E
				private OrSequence([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DecisionDagBuilder.Tests> remainingTests)
					: base(remainingTests)
				{
				}

				// Token: 0x060092E9 RID: 37609 RVA: 0x00201798 File Offset: 0x001FF998
				public override BoundDagTest ComputeSelectedTest()
				{
					return this.RemainingTests[0].ComputeSelectedTest();
				}

				// Token: 0x060092EA RID: 37610 RVA: 0x002017B9 File Offset: 0x001FF9B9
				public override DecisionDagBuilder.Tests Update(ArrayBuilder<DecisionDagBuilder.Tests> remainingTests)
				{
					return DecisionDagBuilder.Tests.OrSequence.Create(remainingTests);
				}

				// Token: 0x060092EB RID: 37611 RVA: 0x002017C4 File Offset: 0x001FF9C4
				public static DecisionDagBuilder.Tests Create(ArrayBuilder<DecisionDagBuilder.Tests> remainingTests)
				{
					DecisionDagBuilder.Tests tests;
					for (int i = remainingTests.Count - 1; i >= 0; i--)
					{
						tests = remainingTests[i];
						if (!(tests is DecisionDagBuilder.Tests.False))
						{
							DecisionDagBuilder.Tests.True @true = tests as DecisionDagBuilder.Tests.True;
							if (@true != null)
							{
								remainingTests.Free();
								return @true;
							}
							DecisionDagBuilder.Tests.OrSequence orSequence = tests as DecisionDagBuilder.Tests.OrSequence;
							if (orSequence != null)
							{
								remainingTests.RemoveAt(i);
								ImmutableArray<DecisionDagBuilder.Tests> remainingTests2 = orSequence.RemainingTests;
								int j = 0;
								int length = remainingTests2.Length;
								while (j < length)
								{
									remainingTests.Insert(i + j, remainingTests2[j]);
									j++;
								}
							}
						}
						else
						{
							remainingTests.RemoveAt(i);
						}
					}
					int count = remainingTests.Count;
					if (count != 0)
					{
						if (count != 1)
						{
							tests = new DecisionDagBuilder.Tests.OrSequence(remainingTests.ToImmutable());
						}
						else
						{
							tests = remainingTests[0];
						}
					}
					else
					{
						tests = DecisionDagBuilder.Tests.False.Instance;
					}
					DecisionDagBuilder.Tests tests2 = tests;
					remainingTests.Free();
					return tests2;
				}

				// Token: 0x060092EC RID: 37612 RVA: 0x00201898 File Offset: 0x001FFA98
				public override string Dump(Func<BoundDagTest, string> dump)
				{
					return "OR(" + string.Join(", ", this.RemainingTests.Select<DecisionDagBuilder.Tests, string>((DecisionDagBuilder.Tests t) => t.Dump(dump))) + ")";
				}
			}
		}
	}
}
