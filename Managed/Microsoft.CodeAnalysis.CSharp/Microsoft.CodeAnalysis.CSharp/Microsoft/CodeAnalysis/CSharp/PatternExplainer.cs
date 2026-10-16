using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000041 RID: 65
	internal static class PatternExplainer
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0003BCE4 File Offset: 0x00039EE4
		private static ImmutableArray<BoundDecisionDagNode> ShortestPathToNode(ImmutableArray<BoundDecisionDagNode> nodes, BoundDecisionDagNode node, bool nullPaths, out bool requiresFalseWhenClause)
		{
			PatternExplainer.<>c__DisplayClass0_0 CS$<>8__locals1;
			CS$<>8__locals1.dist = PooledDictionary<BoundDecisionDagNode, ValueTuple<int, BoundDecisionDagNode>>.GetInstance();
			int length = nodes.Length;
			CS$<>8__locals1.infinity = 2 * length + 2;
			for (int i = length - 1; i >= 0; i--)
			{
				BoundDecisionDagNode boundDecisionDagNode = nodes[i];
				PooledDictionary<BoundDecisionDagNode, ValueTuple<int, BoundDecisionDagNode>> dist = CS$<>8__locals1.dist;
				BoundDecisionDagNode boundDecisionDagNode2 = boundDecisionDagNode;
				BoundDecisionDagNode boundDecisionDagNode3 = boundDecisionDagNode;
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = boundDecisionDagNode3 as BoundEvaluationDecisionDagNode;
				ValueTuple<int, BoundDecisionDagNode> valueTuple;
				if (boundEvaluationDecisionDagNode == null)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode = boundDecisionDagNode3 as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode != null)
					{
						BoundDagTest test = boundTestDecisionDagNode.Test;
						if (!(test is BoundDagNonNullTest))
						{
							if (test is BoundDagExplicitNullTest)
							{
								BoundTestDecisionDagNode boundTestDecisionDagNode2 = boundTestDecisionDagNode;
								if (!nullPaths)
								{
									valueTuple = new ValueTuple<int, BoundDecisionDagNode>(1 + PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundTestDecisionDagNode2.WhenFalse, ref CS$<>8__locals1), boundTestDecisionDagNode2.WhenFalse);
									goto IL_01CD;
								}
							}
						}
						else
						{
							BoundTestDecisionDagNode boundTestDecisionDagNode3 = boundTestDecisionDagNode;
							if (!nullPaths)
							{
								valueTuple = new ValueTuple<int, BoundDecisionDagNode>(1 + PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundTestDecisionDagNode3.WhenTrue, ref CS$<>8__locals1), boundTestDecisionDagNode3.WhenTrue);
								goto IL_01CD;
							}
						}
						BoundTestDecisionDagNode boundTestDecisionDagNode4 = boundTestDecisionDagNode;
						int num = PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundTestDecisionDagNode4.WhenTrue, ref CS$<>8__locals1);
						int num2 = PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundTestDecisionDagNode4.WhenFalse, ref CS$<>8__locals1);
						valueTuple = ((num <= num2) ? new ValueTuple<int, BoundDecisionDagNode>(1 + num, boundTestDecisionDagNode4.WhenTrue) : new ValueTuple<int, BoundDecisionDagNode>(1 + num2, boundTestDecisionDagNode4.WhenFalse));
					}
					else
					{
						BoundWhenDecisionDagNode boundWhenDecisionDagNode = boundDecisionDagNode3 as BoundWhenDecisionDagNode;
						if (boundWhenDecisionDagNode == null)
						{
							valueTuple = new ValueTuple<int, BoundDecisionDagNode>((boundDecisionDagNode == node) ? 1 : CS$<>8__locals1.infinity, null);
						}
						else
						{
							BoundWhenDecisionDagNode boundWhenDecisionDagNode2 = boundWhenDecisionDagNode;
							int num3 = PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundWhenDecisionDagNode2.WhenTrue, ref CS$<>8__locals1);
							int num4 = PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundWhenDecisionDagNode2.WhenFalse, ref CS$<>8__locals1);
							valueTuple = ((num3 <= num4) ? new ValueTuple<int, BoundDecisionDagNode>(1 + num3, boundWhenDecisionDagNode2.WhenTrue) : new ValueTuple<int, BoundDecisionDagNode>(1 + ((num4 < length) ? length : 0) + num4, boundWhenDecisionDagNode2.WhenFalse));
						}
					}
				}
				else
				{
					BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode2 = boundEvaluationDecisionDagNode;
					valueTuple = new ValueTuple<int, BoundDecisionDagNode>(PatternExplainer.<ShortestPathToNode>g__distance|0_0(boundEvaluationDecisionDagNode2.Next, ref CS$<>8__locals1), boundEvaluationDecisionDagNode2.Next);
				}
				IL_01CD:
				dist.Add(boundDecisionDagNode2, valueTuple);
			}
			int item = CS$<>8__locals1.dist[nodes[0]].Item1;
			requiresFalseWhenClause = item > length;
			ArrayBuilder<BoundDecisionDagNode> instance = ArrayBuilder<BoundDecisionDagNode>.GetInstance(item);
			BoundDecisionDagNode boundDecisionDagNode4 = nodes[0];
			while (boundDecisionDagNode4 != node)
			{
				instance.Add(boundDecisionDagNode4);
				BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode3 = boundDecisionDagNode4 as BoundEvaluationDecisionDagNode;
				if (boundEvaluationDecisionDagNode3 == null)
				{
					BoundTestDecisionDagNode boundTestDecisionDagNode5 = boundDecisionDagNode4 as BoundTestDecisionDagNode;
					if (boundTestDecisionDagNode5 == null)
					{
						BoundWhenDecisionDagNode boundWhenDecisionDagNode3 = boundDecisionDagNode4 as BoundWhenDecisionDagNode;
						if (boundWhenDecisionDagNode3 == null)
						{
							throw ExceptionUtilities.Unreachable;
						}
						instance.RemoveLast();
						boundDecisionDagNode4 = boundWhenDecisionDagNode3.WhenFalse;
					}
					else
					{
						boundDecisionDagNode4 = CS$<>8__locals1.dist[boundTestDecisionDagNode5].Item2;
					}
				}
				else
				{
					boundDecisionDagNode4 = boundEvaluationDecisionDagNode3.Next;
				}
			}
			CS$<>8__locals1.dist.Free();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0003BF88 File Offset: 0x0003A188
		internal static string SamplePatternForPathToDagNode(BoundDagTemp rootIdentifier, ImmutableArray<BoundDecisionDagNode> nodes, BoundDecisionDagNode targetNode, bool nullPaths, out bool requiresFalseWhenClause, out bool unnamedEnumValue)
		{
			unnamedEnumValue = false;
			ImmutableArray<BoundDecisionDagNode> immutableArray = PatternExplainer.ShortestPathToNode(nodes, targetNode, nullPaths, out requiresFalseWhenClause);
			Dictionary<BoundDagTemp, ArrayBuilder<ValueTuple<BoundDagTest, bool>>> dictionary = new Dictionary<BoundDagTemp, ArrayBuilder<ValueTuple<BoundDagTest, bool>>>();
			Dictionary<BoundDagTemp, ArrayBuilder<BoundDagEvaluation>> dictionary2 = new Dictionary<BoundDagTemp, ArrayBuilder<BoundDagEvaluation>>();
			int i = 0;
			int length = immutableArray.Length;
			while (i < length)
			{
				BoundDecisionDagNode boundDecisionDagNode = immutableArray[i];
				BoundTestDecisionDagNode boundTestDecisionDagNode = boundDecisionDagNode as BoundTestDecisionDagNode;
				if (boundTestDecisionDagNode == null)
				{
					BoundEvaluationDecisionDagNode boundEvaluationDecisionDagNode = boundDecisionDagNode as BoundEvaluationDecisionDagNode;
					if (boundEvaluationDecisionDagNode != null)
					{
						BoundDagTemp input = boundEvaluationDecisionDagNode.Evaluation.Input;
						ArrayBuilder<BoundDagEvaluation> arrayBuilder;
						if (!dictionary2.TryGetValue(input, ref arrayBuilder))
						{
							dictionary2.Add(input, arrayBuilder = new ArrayBuilder<BoundDagEvaluation>());
						}
						arrayBuilder.Add(boundEvaluationDecisionDagNode.Evaluation);
					}
				}
				else
				{
					BoundDecisionDagNode boundDecisionDagNode2 = ((i < length - 1) ? immutableArray[i + 1] : targetNode);
					bool flag = boundTestDecisionDagNode.WhenTrue == boundDecisionDagNode2 || (boundTestDecisionDagNode.WhenFalse != boundDecisionDagNode2 && boundTestDecisionDagNode.WhenTrue is BoundWhenDecisionDagNode);
					BoundDagTest test = boundTestDecisionDagNode.Test;
					BoundDagTemp input2 = test.Input;
					if (!(test is BoundDagTypeTest) || flag)
					{
						ArrayBuilder<ValueTuple<BoundDagTest, bool>> arrayBuilder2;
						if (!dictionary.TryGetValue(input2, ref arrayBuilder2))
						{
							dictionary.Add(input2, arrayBuilder2 = new ArrayBuilder<ValueTuple<BoundDagTest, bool>>());
						}
						arrayBuilder2.Add(new ValueTuple<BoundDagTest, bool>(test, flag));
					}
				}
				i++;
			}
			return PatternExplainer.SamplePatternForTemp(rootIdentifier, dictionary, dictionary2, false, ref unnamedEnumValue);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0003C0CC File Offset: 0x0003A2CC
		private static string SamplePatternForTemp(BoundDagTemp input, [TupleElementNames(new string[] { "test", "sense" })] Dictionary<BoundDagTemp, ArrayBuilder<ValueTuple<BoundDagTest, bool>>> constraintMap, Dictionary<BoundDagTemp, ArrayBuilder<BoundDagEvaluation>> evaluationMap, bool requireExactType, ref bool unnamedEnumValue)
		{
			PatternExplainer.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.requireExactType = requireExactType;
			CS$<>8__locals1.input = input;
			CS$<>8__locals1.constraintMap = constraintMap;
			CS$<>8__locals1.evaluationMap = evaluationMap;
			CS$<>8__locals1.constraints = PatternExplainer.<SamplePatternForTemp>g__getArray|2_0<ValueTuple<BoundDagTest, bool>>(CS$<>8__locals1.constraintMap, CS$<>8__locals1.input);
			CS$<>8__locals1.evaluations = PatternExplainer.<SamplePatternForTemp>g__getArray|2_0<BoundDagEvaluation>(CS$<>8__locals1.evaluationMap, CS$<>8__locals1.input);
			string text;
			if ((text = PatternExplainer.<SamplePatternForTemp>g__tryHandleSingleTest|2_1(ref CS$<>8__locals1)) == null && (text = PatternExplainer.<SamplePatternForTemp>g__tryHandleTypeTestAndTypeEvaluation|2_2(ref unnamedEnumValue, ref CS$<>8__locals1)) == null && (text = PatternExplainer.<SamplePatternForTemp>g__tryHandleUnboxNullableValueType|2_3(ref unnamedEnumValue, ref CS$<>8__locals1)) == null && (text = PatternExplainer.<SamplePatternForTemp>g__tryHandleTuplePattern|2_4(ref unnamedEnumValue, ref CS$<>8__locals1)) == null && (text = PatternExplainer.<SamplePatternForTemp>g__tryHandleNumericLimits|2_5(ref unnamedEnumValue, ref CS$<>8__locals1)) == null)
			{
				text = PatternExplainer.<SamplePatternForTemp>g__tryHandleRecursivePattern|2_6(ref unnamedEnumValue, ref CS$<>8__locals1) ?? PatternExplainer.<SamplePatternForTemp>g__produceFallbackPattern|2_7(ref CS$<>8__locals1);
			}
			return text;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0003C17C File Offset: 0x0003A37C
		private static string SampleValueString(IValueSet remainingValues, TypeSymbol type, bool requireExactType, ref bool unnamedEnumValue)
		{
			NamedTypeSymbol namedTypeSymbol = type as NamedTypeSymbol;
			if (namedTypeSymbol != null && type.TypeKind == TypeKind.Enum)
			{
				foreach (Symbol symbol in namedTypeSymbol.GetMembers())
				{
					FieldSymbol fieldSymbol = symbol as FieldSymbol;
					if (fieldSymbol != null && fieldSymbol.IsConst && symbol.IsStatic && symbol.DeclaredAccessibility == Accessibility.Public)
					{
						ConstantValue constantValue = fieldSymbol.GetConstantValue(ConstantFieldsInProgress.Empty, false);
						if (constantValue != null && remainingValues.Any(BinaryOperatorKind.Equal, constantValue))
						{
							return fieldSymbol.ToDisplayString(null);
						}
					}
				}
				unnamedEnumValue = true;
			}
			ConstantValue sample = remainingValues.Sample;
			if (sample != null)
			{
				return PatternExplainer.ValueString(sample, type, requireExactType);
			}
			TypeSymbol typeSymbol = type.EnumUnderlyingTypeOrSelf();
			if (typeSymbol.SpecialType == SpecialType.System_IntPtr)
			{
				if (remainingValues.Any(BinaryOperatorKind.GreaterThan, ConstantValue.Create(2147483647)))
				{
					return "> (" + type.ToDisplayString(null) + ")int.MaxValue";
				}
				if (remainingValues.Any(BinaryOperatorKind.LessThan, ConstantValue.Create(-2147483648)))
				{
					return "< (" + type.ToDisplayString(null) + ")int.MinValue";
				}
			}
			else if (typeSymbol.SpecialType == SpecialType.System_UIntPtr && remainingValues.Any(BinaryOperatorKind.GreaterThan, ConstantValue.Create(4294967295U)))
			{
				return "> (" + type.ToDisplayString(null) + ")uint.MaxValue";
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0003C2DC File Offset: 0x0003A4DC
		private static string ValueString(ConstantValue value, TypeSymbol type, bool requireExactType)
		{
			bool flag = (type.IsEnumType() || requireExactType || type.IsNativeIntegerType) && (!PatternExplainer.<ValueString>g__typeHasExactTypeLiteral|4_0(type) || value.IsNull);
			string text = PatternExplainer.PrimitiveValueString(value, type.EnumUnderlyingTypeOrSelf());
			if (!flag)
			{
				return text;
			}
			return "(" + type.ToDisplayString(null) + ")" + text;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0003C338 File Offset: 0x0003A538
		private static string PrimitiveValueString(ConstantValue value, TypeSymbol type)
		{
			if (value.IsNull)
			{
				return "null";
			}
			switch (type.SpecialType)
			{
			case SpecialType.System_Boolean:
			case SpecialType.System_Char:
			case SpecialType.System_SByte:
			case SpecialType.System_Byte:
			case SpecialType.System_Int16:
			case SpecialType.System_UInt16:
			case SpecialType.System_Int32:
			case SpecialType.System_UInt32:
			case SpecialType.System_Int64:
			case SpecialType.System_UInt64:
			case SpecialType.System_Decimal:
			case SpecialType.System_String:
				break;
			case SpecialType.System_Single:
			{
				float singleValue = value.SingleValue;
				string text;
				if (singleValue != float.NegativeInfinity)
				{
					if (singleValue != float.PositiveInfinity)
					{
						if (float.IsNaN(singleValue))
						{
							text = "float.NaN";
						}
						else
						{
							text = ObjectDisplay.FormatPrimitive(singleValue, ObjectDisplayOptions.IncludeTypeSuffix);
						}
					}
					else
					{
						text = "float.PositiveInfinity";
					}
				}
				else
				{
					text = "float.NegativeInfinity";
				}
				return text;
			}
			case SpecialType.System_Double:
			{
				double doubleValue = value.DoubleValue;
				string text;
				if (doubleValue != double.NegativeInfinity)
				{
					if (doubleValue != double.PositiveInfinity)
					{
						if (double.IsNaN(doubleValue))
						{
							text = "double.NaN";
						}
						else
						{
							text = ObjectDisplay.FormatPrimitive(doubleValue, ObjectDisplayOptions.IncludeTypeSuffix);
						}
					}
					else
					{
						text = "double.PositiveInfinity";
					}
				}
				else
				{
					text = "double.NegativeInfinity";
				}
				return text;
			}
			case SpecialType.System_IntPtr:
				if (!type.IsNativeIntegerType)
				{
					goto IL_011C;
				}
				break;
			case SpecialType.System_UIntPtr:
				if (!type.IsNativeIntegerType)
				{
					goto IL_011C;
				}
				break;
			default:
				goto IL_011C;
			}
			return ObjectDisplay.FormatPrimitive(value.Value, ObjectDisplayOptions.IncludeTypeSuffix | ObjectDisplayOptions.UseQuotes | ObjectDisplayOptions.EscapeNonPrintableCharacters);
			IL_011C:
			return "_";
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0003C468 File Offset: 0x0003A668
		[CompilerGenerated]
		internal static int <ShortestPathToNode>g__distance|0_0(BoundDecisionDagNode x, ref PatternExplainer.<>c__DisplayClass0_0 A_1)
		{
			if (x == null)
			{
				return A_1.infinity;
			}
			ValueTuple<int, BoundDecisionDagNode> valueTuple;
			if (A_1.dist.TryGetValue(x, ref valueTuple))
			{
				return valueTuple.Item1;
			}
			return A_1.infinity;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0003C49C File Offset: 0x0003A69C
		[CompilerGenerated]
		internal static ImmutableArray<T> <SamplePatternForTemp>g__getArray|2_0<T>(Dictionary<BoundDagTemp, ArrayBuilder<T>> map, BoundDagTemp temp)
		{
			ArrayBuilder<T> arrayBuilder;
			if (!map.TryGetValue(temp, ref arrayBuilder))
			{
				return ImmutableArray<T>.Empty;
			}
			return arrayBuilder.ToImmutable();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0003C4C0 File Offset: 0x0003A6C0
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__tryHandleSingleTest|2_1(ref PatternExplainer.<>c__DisplayClass2_0 A_0)
		{
			if (A_0.evaluations.IsEmpty && A_0.constraints.Length == 1)
			{
				ValueTuple<BoundDagTest, bool> valueTuple = A_0.constraints[0];
				BoundDagTest item = valueTuple.Item1;
				if (item is BoundDagNonNullTest)
				{
					bool flag = valueTuple.Item2;
					if (!flag)
					{
						return "null";
					}
					if (!A_0.requireExactType)
					{
						return "not null";
					}
					return A_0.input.Type.ToDisplayString(null);
				}
				else if (item is BoundDagExplicitNullTest)
				{
					if (valueTuple.Item2)
					{
						return "null";
					}
					if (!A_0.requireExactType)
					{
						return "not null";
					}
					return A_0.input.Type.ToDisplayString(null);
				}
				else
				{
					BoundDagTypeTest boundDagTypeTest = item as BoundDagTypeTest;
					if (boundDagTypeTest != null)
					{
						TypeSymbol type = boundDagTypeTest.Type;
						bool flag = valueTuple.Item2;
						return type.ToDisplayString(null);
					}
				}
			}
			return null;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0003C59C File Offset: 0x0003A79C
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__tryHandleTypeTestAndTypeEvaluation|2_2(ref bool unnamedEnumValue, ref PatternExplainer.<>c__DisplayClass2_0 A_1)
		{
			if (A_1.evaluations.Length == 1 && A_1.constraints.Length == 1)
			{
				ValueTuple<BoundDagTest, bool> valueTuple = A_1.constraints[0];
				BoundDagTypeTest boundDagTypeTest = valueTuple.Item1 as BoundDagTypeTest;
				if (boundDagTypeTest != null)
				{
					TypeSymbol type = boundDagTypeTest.Type;
					if (valueTuple.Item2)
					{
						BoundDagTypeEvaluation boundDagTypeEvaluation = A_1.evaluations[0] as BoundDagTypeEvaluation;
						if (boundDagTypeEvaluation != null)
						{
							TypeSymbol type2 = boundDagTypeEvaluation.Type;
							if (type.Equals(type2, TypeCompareKind.AllIgnoreOptions))
							{
								return PatternExplainer.SamplePatternForTemp(new BoundDagTemp(boundDagTypeEvaluation.Syntax, boundDagTypeEvaluation.Type, boundDagTypeEvaluation), A_1.constraintMap, A_1.evaluationMap, true, ref unnamedEnumValue);
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0003C644 File Offset: 0x0003A844
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__tryHandleUnboxNullableValueType|2_3(ref bool unnamedEnumValue, ref PatternExplainer.<>c__DisplayClass2_0 A_1)
		{
			if (A_1.evaluations.Length == 1 && A_1.constraints.Length == 1)
			{
				ValueTuple<BoundDagTest, bool> valueTuple = A_1.constraints[0];
				if (valueTuple.Item1 is BoundDagNonNullTest && valueTuple.Item2)
				{
					BoundDagTypeEvaluation boundDagTypeEvaluation = A_1.evaluations[0] as BoundDagTypeEvaluation;
					if (boundDagTypeEvaluation != null)
					{
						TypeSymbol type = boundDagTypeEvaluation.Type;
						if (A_1.input.Type.IsNullableType() && A_1.input.Type.GetNullableUnderlyingType().Equals(type, TypeCompareKind.AllIgnoreOptions))
						{
							string text = PatternExplainer.SamplePatternForTemp(new BoundDagTemp(boundDagTypeEvaluation.Syntax, boundDagTypeEvaluation.Type, boundDagTypeEvaluation), A_1.constraintMap, A_1.evaluationMap, false, ref unnamedEnumValue);
							if (!(text == "_"))
							{
								return text;
							}
							return "not null";
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0003C720 File Offset: 0x0003A920
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__tryHandleTuplePattern|2_4(ref bool unnamedEnumValue, ref PatternExplainer.<>c__DisplayClass2_0 A_1)
		{
			if (A_1.input.Type.IsTupleType && A_1.constraints.IsEmpty)
			{
				if (A_1.evaluations.All<BoundDagEvaluation>(delegate(BoundDagEvaluation e)
				{
					BoundDagFieldEvaluation boundDagFieldEvaluation2 = e as BoundDagFieldEvaluation;
					if (boundDagFieldEvaluation2 != null)
					{
						FieldSymbol field = boundDagFieldEvaluation2.Field;
						return field.IsTupleElement();
					}
					return false;
				}))
				{
					int length = A_1.input.Type.TupleElements.Length;
					ArrayBuilder<string> arrayBuilder = new ArrayBuilder<string>(length);
					arrayBuilder.AddMany("_", length);
					foreach (BoundDagEvaluation boundDagEvaluation in A_1.evaluations)
					{
						BoundDagFieldEvaluation boundDagFieldEvaluation = (BoundDagFieldEvaluation)boundDagEvaluation;
						BoundDagTemp boundDagTemp = new BoundDagTemp(boundDagFieldEvaluation.Syntax, boundDagFieldEvaluation.Field.Type, boundDagFieldEvaluation);
						int tupleElementIndex = boundDagFieldEvaluation.Field.TupleElementIndex;
						if (tupleElementIndex < 0 || tupleElementIndex >= length)
						{
							return null;
						}
						string text = arrayBuilder[tupleElementIndex];
						string text2 = PatternExplainer.SamplePatternForTemp(boundDagTemp, A_1.constraintMap, A_1.evaluationMap, false, ref unnamedEnumValue);
						arrayBuilder[tupleElementIndex] = PatternExplainer.<SamplePatternForTemp>g__makeConjunct|2_9(text, text2);
					}
					return "(" + string.Join(", ", arrayBuilder) + ")" + ((arrayBuilder.Count == 1) ? " { }" : null);
				}
			}
			return null;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0003C868 File Offset: 0x0003AA68
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__makeConjunct|2_9(string oldPattern, string newPattern)
		{
			string text;
			if (!(oldPattern == "_"))
			{
				if (!(newPattern == "_"))
				{
					text = oldPattern + " and " + newPattern;
				}
				else
				{
					text = oldPattern;
				}
			}
			else
			{
				text = newPattern;
			}
			return text;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0003C8B0 File Offset: 0x0003AAB0
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__tryHandleNumericLimits|2_5(ref bool unnamedEnumValue, ref PatternExplainer.<>c__DisplayClass2_0 A_1)
		{
			if (A_1.evaluations.IsEmpty)
			{
				if (A_1.constraints.All<ValueTuple<BoundDagTest, bool>>(delegate([TupleElementNames(new string[] { "test", "sense" })] ValueTuple<BoundDagTest, bool> t)
				{
					BoundDagTest item2 = t.Item1;
					bool flag;
					if (!(item2 is BoundDagValueTest))
					{
						if (!(item2 is BoundDagRelationalTest))
						{
							if (item2 is BoundDagExplicitNullTest)
							{
								if (!t.Item2)
								{
									return true;
								}
							}
							else if (item2 is BoundDagNonNullTest)
							{
								if (t.Item2)
								{
									return true;
								}
							}
							flag = false;
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						flag = true;
					}
					return flag;
				}))
				{
					PatternExplainer.<>c__DisplayClass2_1 CS$<>8__locals1;
					CS$<>8__locals1.fac = ValueSetFactory.ForType(A_1.input.Type);
					if (CS$<>8__locals1.fac != null)
					{
						PatternExplainer.<>c__DisplayClass2_2 CS$<>8__locals2;
						CS$<>8__locals2.remainingValues = CS$<>8__locals1.fac.AllValues;
						foreach (ValueTuple<BoundDagTest, bool> valueTuple in A_1.constraints)
						{
							BoundDagTest item = valueTuple.Item1;
							PatternExplainer.<>c__DisplayClass2_3 CS$<>8__locals3;
							CS$<>8__locals3.sense = valueTuple.Item2;
							BoundDagValueTest boundDagValueTest = item as BoundDagValueTest;
							if (boundDagValueTest == null)
							{
								BoundDagRelationalTest boundDagRelationalTest = item as BoundDagRelationalTest;
								if (boundDagRelationalTest != null)
								{
									PatternExplainer.<SamplePatternForTemp>g__addRelation|2_11(boundDagRelationalTest.Relation, boundDagRelationalTest.Value, ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
								}
							}
							else
							{
								PatternExplainer.<SamplePatternForTemp>g__addRelation|2_11(BinaryOperatorKind.Equal, boundDagValueTest.Value, ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3);
							}
						}
						if (CS$<>8__locals2.remainingValues.Complement().IsEmpty)
						{
							return "_";
						}
						return PatternExplainer.SampleValueString(CS$<>8__locals2.remainingValues, A_1.input.Type, A_1.requireExactType, ref unnamedEnumValue);
					}
				}
			}
			return null;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0003C9EC File Offset: 0x0003ABEC
		[CompilerGenerated]
		internal static void <SamplePatternForTemp>g__addRelation|2_11(BinaryOperatorKind relation, ConstantValue value, ref PatternExplainer.<>c__DisplayClass2_1 A_2, ref PatternExplainer.<>c__DisplayClass2_2 A_3, ref PatternExplainer.<>c__DisplayClass2_3 A_4)
		{
			if (value.IsBad)
			{
				return;
			}
			IValueSet valueSet = A_2.fac.Related(relation, value);
			if (!A_4.sense)
			{
				valueSet = valueSet.Complement();
			}
			A_3.remainingValues = A_3.remainingValues.Intersect(valueSet);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0003CA34 File Offset: 0x0003AC34
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__tryHandleRecursivePattern|2_6(ref bool unnamedEnumValue, ref PatternExplainer.<>c__DisplayClass2_0 A_1)
		{
			if (A_1.constraints.IsEmpty && A_1.evaluations.IsEmpty)
			{
				return null;
			}
			if (!A_1.constraints.All<ValueTuple<BoundDagTest, bool>>(delegate([TupleElementNames(new string[] { "test", "sense" })] ValueTuple<BoundDagTest, bool> c)
			{
				BoundDagTest item = c.Item1;
				if (item is BoundDagNonNullTest)
				{
					if (c.Item2)
					{
						return true;
					}
				}
				else if (item is BoundDagExplicitNullTest)
				{
					if (!c.Item2)
					{
						return true;
					}
				}
				return false;
			}))
			{
				return null;
			}
			string text = null;
			Dictionary<Symbol, string> dictionary = new Dictionary<Symbol, string>();
			bool flag = false;
			foreach (BoundDagEvaluation boundDagEvaluation in A_1.evaluations)
			{
				BoundDagDeconstructEvaluation boundDagDeconstructEvaluation = boundDagEvaluation as BoundDagDeconstructEvaluation;
				if (boundDagDeconstructEvaluation == null)
				{
					BoundDagFieldEvaluation boundDagFieldEvaluation = boundDagEvaluation as BoundDagFieldEvaluation;
					if (boundDagFieldEvaluation == null)
					{
						BoundDagPropertyEvaluation boundDagPropertyEvaluation = boundDagEvaluation as BoundDagPropertyEvaluation;
						if (boundDagPropertyEvaluation == null)
						{
							return null;
						}
						string text2 = PatternExplainer.SamplePatternForTemp(new BoundDagTemp(boundDagPropertyEvaluation.Syntax, boundDagPropertyEvaluation.Property.Type, boundDagPropertyEvaluation), A_1.constraintMap, A_1.evaluationMap, false, ref unnamedEnumValue);
						dictionary.Add(boundDagPropertyEvaluation.Property, text2);
					}
					else
					{
						string text3 = PatternExplainer.SamplePatternForTemp(new BoundDagTemp(boundDagFieldEvaluation.Syntax, boundDagFieldEvaluation.Field.Type, boundDagFieldEvaluation), A_1.constraintMap, A_1.evaluationMap, false, ref unnamedEnumValue);
						dictionary.Add(boundDagFieldEvaluation.Field, text3);
					}
				}
				else
				{
					MethodSymbol deconstructMethod = boundDagDeconstructEvaluation.DeconstructMethod;
					int num = (deconstructMethod.RequiresInstanceReceiver ? 0 : 1);
					int num2 = deconstructMethod.Parameters.Length - num;
					StringBuilder stringBuilder = new StringBuilder("(");
					for (int i = 0; i < num2; i++)
					{
						string text4 = PatternExplainer.SamplePatternForTemp(new BoundDagTemp(boundDagDeconstructEvaluation.Syntax, deconstructMethod.Parameters[i + num].Type, boundDagDeconstructEvaluation, i, false), A_1.constraintMap, A_1.evaluationMap, false, ref unnamedEnumValue);
						if (i != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(text4);
					}
					stringBuilder.Append(")");
					string text5 = stringBuilder.ToString();
					if (text != null && flag)
					{
						text += " { }";
						flag = dictionary.Count != 0;
					}
					text = ((text == null) ? text5 : (text + " and " + text5));
					flag |= num2 == 1;
				}
			}
			string text6 = (A_1.requireExactType ? A_1.input.Type.ToDisplayString(null) : null);
			string text7;
			if (!(flag | ((text == null && text6 == null) || dictionary.Count != 0)))
			{
				text7 = null;
			}
			else
			{
				text7 = ((text != null) ? " {" : "{") + string.Join(", ", Enumerable.Select<KeyValuePair<Symbol, string>, string>(dictionary, (KeyValuePair<Symbol, string> kvp) => " " + kvp.Key.Name + ": " + kvp.Value)) + " }";
			}
			string text8 = text7;
			return text6 + text + text8;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0003CCF6 File Offset: 0x0003AEF6
		[CompilerGenerated]
		internal static string <SamplePatternForTemp>g__produceFallbackPattern|2_7(ref PatternExplainer.<>c__DisplayClass2_0 A_0)
		{
			if (!A_0.requireExactType)
			{
				return "_";
			}
			return A_0.input.Type.ToDisplayString(null);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0003CD18 File Offset: 0x0003AF18
		[CompilerGenerated]
		internal static bool <ValueString>g__typeHasExactTypeLiteral|4_0(TypeSymbol type)
		{
			switch (type.SpecialType)
			{
			case SpecialType.System_Boolean:
				return true;
			case SpecialType.System_Char:
				return true;
			case SpecialType.System_Int32:
				return true;
			case SpecialType.System_UInt32:
				return true;
			case SpecialType.System_Int64:
				return true;
			case SpecialType.System_UInt64:
				return true;
			case SpecialType.System_Decimal:
				return true;
			case SpecialType.System_Single:
				return true;
			case SpecialType.System_Double:
				return true;
			case SpecialType.System_String:
				return true;
			}
			return false;
		}
	}
}
