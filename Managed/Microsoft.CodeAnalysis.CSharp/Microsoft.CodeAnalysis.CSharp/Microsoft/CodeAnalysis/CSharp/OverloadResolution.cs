using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000055 RID: 85
	internal sealed class OverloadResolution
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00042594 File Offset: 0x00040794
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private void BinaryOperatorEasyOut(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result)
		{
			TypeSymbol type = left.Type;
			if (type == null)
			{
				return;
			}
			TypeSymbol type2 = right.Type;
			if (type2 == null)
			{
				return;
			}
			if (OverloadResolution.PossiblyUnusualConstantOperation(left, right))
			{
				return;
			}
			BinaryOperatorKind binaryOperatorKind = OverloadResolution.BinopEasyOut.OpKind(kind, type, type2);
			if (binaryOperatorKind == BinaryOperatorKind.Error)
			{
				return;
			}
			BinaryOperatorSignature signature = this.Compilation.builtInOperators.GetSignature(binaryOperatorKind);
			Conversion conversion = ConversionsBase.FastClassifyConversion(type, signature.LeftType);
			Conversion conversion2 = ConversionsBase.FastClassifyConversion(type2, signature.RightType);
			result.Results.Add(BinaryOperatorAnalysisResult.Applicable(signature, conversion, conversion2));
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00042614 File Offset: 0x00040814
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool PossiblyUnusualConstantOperation(BoundExpression left, BoundExpression right)
		{
			return (!(left.ConstantValue == null) || !(right.ConstantValue == null)) && (left.Type.SpecialType != right.Type.SpecialType || (left.Type.SpecialType != SpecialType.System_Int32 && left.Type.SpecialType != SpecialType.System_Boolean && left.Type.SpecialType != SpecialType.System_String));
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00042688 File Offset: 0x00040888
		public void BinaryOperatorOverloadResolution(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.BinaryOperatorOverloadResolution_EasyOut(kind, left, right, result);
			if (result.Results.Count > 0)
			{
				return;
			}
			this.BinaryOperatorOverloadResolution_NoEasyOut(kind, left, right, result, ref useSiteInfo);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000426B4 File Offset: 0x000408B4
		internal void BinaryOperatorOverloadResolution_EasyOut(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result)
		{
			BinaryOperatorKind binaryOperatorKind = kind & ~BinaryOperatorKind.Logical;
			this.BinaryOperatorEasyOut(binaryOperatorKind, left, right, result);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000426D4 File Offset: 0x000408D4
		internal void BinaryOperatorOverloadResolution_NoEasyOut(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol type = left.Type;
			TypeSymbol typeSymbol = ((type != null) ? type.StrippedType() : null);
			TypeSymbol type2 = right.Type;
			TypeSymbol typeSymbol2 = ((type2 != null) ? type2.StrippedType() : null);
			bool flag = typeSymbol != null && typeSymbol.IsInterfaceType();
			bool flag2 = typeSymbol2 != null && typeSymbol2.IsInterfaceType();
			bool flag3 = false;
			if (typeSymbol != null && !flag)
			{
				flag3 = this.GetUserDefinedOperators(kind, typeSymbol, left, right, result.Results, ref useSiteInfo);
				if (!flag3)
				{
					result.Results.Clear();
				}
			}
			if (typeSymbol2 != null && !flag2 && !typeSymbol2.Equals(typeSymbol))
			{
				ArrayBuilder<BinaryOperatorAnalysisResult> instance = ArrayBuilder<BinaryOperatorAnalysisResult>.GetInstance();
				if (this.GetUserDefinedOperators(kind, typeSymbol2, left, right, instance, ref useSiteInfo))
				{
					flag3 = true;
					OverloadResolution.AddDistinctOperators(result.Results, instance);
				}
				instance.Free();
			}
			if (!flag3 && kind != BinaryOperatorKind.Equal && kind != BinaryOperatorKind.NotEqual)
			{
				result.Results.Clear();
				string text = OperatorFacts.BinaryOperatorNameFromOperatorKind(kind);
				PooledDictionary<TypeSymbol, bool> instance2 = PooledDictionary<TypeSymbol, bool>.GetInstance();
				flag3 = this.GetUserDefinedBinaryOperatorsFromInterfaces(kind, text, typeSymbol, flag, left, right, ref useSiteInfo, instance2, result.Results);
				if (!flag3)
				{
					result.Results.Clear();
				}
				if (typeSymbol2 != null && !typeSymbol2.Equals(typeSymbol))
				{
					ArrayBuilder<BinaryOperatorAnalysisResult> instance3 = ArrayBuilder<BinaryOperatorAnalysisResult>.GetInstance();
					if (this.GetUserDefinedBinaryOperatorsFromInterfaces(kind, text, typeSymbol2, flag2, left, right, ref useSiteInfo, instance2, instance3))
					{
						flag3 = true;
						OverloadResolution.AddDistinctOperators(result.Results, instance3);
					}
					instance3.Free();
				}
				instance2.Free();
			}
			if (!flag3)
			{
				result.Results.Clear();
				this.GetAllBuiltInOperators(kind, left, right, result.Results, ref useSiteInfo);
			}
			this.BinaryOperatorOverloadResolution(left, right, result, ref useSiteInfo);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00042864 File Offset: 0x00040A64
		private bool GetUserDefinedBinaryOperatorsFromInterfaces(BinaryOperatorKind kind, string name, TypeSymbol operatorSourceOpt, bool sourceIsInterface, BoundExpression left, BoundExpression right, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, Dictionary<TypeSymbol, bool> lookedInInterfaces, ArrayBuilder<BinaryOperatorAnalysisResult> candidates)
		{
			if (operatorSourceOpt == null)
			{
				return false;
			}
			bool flag = false;
			ImmutableArray<NamedTypeSymbol> immutableArray = default(ImmutableArray<NamedTypeSymbol>);
			if (sourceIsInterface)
			{
				bool flag2;
				if (!lookedInInterfaces.TryGetValue(operatorSourceOpt, ref flag2))
				{
					ArrayBuilder<BinaryOperatorSignature> instance = ArrayBuilder<BinaryOperatorSignature>.GetInstance();
					this.GetUserDefinedBinaryOperatorsFromType((NamedTypeSymbol)operatorSourceOpt, kind, name, instance);
					flag = this.CandidateOperators(instance, left, right, candidates, ref useSiteInfo);
					instance.Free();
					lookedInInterfaces.Add(operatorSourceOpt, flag);
					if (!flag)
					{
						candidates.Clear();
						immutableArray = operatorSourceOpt.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
					}
				}
			}
			else if (operatorSourceOpt.IsTypeParameter())
			{
				immutableArray = ((TypeParameterSymbol)operatorSourceOpt).AllEffectiveInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			if (!immutableArray.IsDefaultOrEmpty)
			{
				ArrayBuilder<BinaryOperatorSignature> instance2 = ArrayBuilder<BinaryOperatorSignature>.GetInstance();
				ArrayBuilder<BinaryOperatorAnalysisResult> instance3 = ArrayBuilder<BinaryOperatorAnalysisResult>.GetInstance();
				PooledHashSet<NamedTypeSymbol> instance4 = PooledHashSet<NamedTypeSymbol>.GetInstance();
				foreach (NamedTypeSymbol namedTypeSymbol in immutableArray)
				{
					if (namedTypeSymbol.IsInterface && !instance4.Contains(namedTypeSymbol))
					{
						bool flag3;
						if (lookedInInterfaces.TryGetValue(namedTypeSymbol, ref flag3))
						{
							if (flag3)
							{
								instance4.AddAll<NamedTypeSymbol>(namedTypeSymbol.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo));
							}
						}
						else
						{
							instance2.Clear();
							instance3.Clear();
							this.GetUserDefinedBinaryOperatorsFromType(namedTypeSymbol, kind, name, instance2);
							flag3 = this.CandidateOperators(instance2, left, right, instance3, ref useSiteInfo);
							lookedInInterfaces.Add(namedTypeSymbol, flag3);
							if (flag3)
							{
								flag = true;
								candidates.AddRange(instance3);
								instance4.AddAll<NamedTypeSymbol>(namedTypeSymbol.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo));
							}
						}
					}
				}
				instance2.Free();
				instance3.Free();
				instance4.Free();
			}
			return flag;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000429DC File Offset: 0x00040BDC
		private void AddDelegateOperation(BinaryOperatorKind kind, TypeSymbol delegateType, ArrayBuilder<BinaryOperatorSignature> operators)
		{
			if (kind <= BinaryOperatorKind.Subtraction)
			{
				if (kind != BinaryOperatorKind.Addition && kind != BinaryOperatorKind.Subtraction)
				{
				}
			}
			else if (kind == BinaryOperatorKind.Equal || kind == BinaryOperatorKind.NotEqual)
			{
				operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Delegate, delegateType, delegateType, this.Compilation.GetSpecialType(SpecialType.System_Boolean), null));
				return;
			}
			operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Delegate, delegateType, delegateType, delegateType, null));
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00042A48 File Offset: 0x00040C48
		private void GetDelegateOperations(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorSignature> operators, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (kind <= BinaryOperatorKind.NotEqual)
			{
				if (kind <= BinaryOperatorKind.Division)
				{
					if (kind <= BinaryOperatorKind.Addition)
					{
						if (kind == BinaryOperatorKind.Multiplication)
						{
							return;
						}
						if (kind != BinaryOperatorKind.Addition)
						{
							goto IL_00F5;
						}
					}
					else if (kind != BinaryOperatorKind.Subtraction)
					{
						if (kind != BinaryOperatorKind.Division)
						{
							goto IL_00F5;
						}
						return;
					}
				}
				else if (kind <= BinaryOperatorKind.LeftShift)
				{
					if (kind != BinaryOperatorKind.Remainder && kind != BinaryOperatorKind.LeftShift)
					{
						goto IL_00F5;
					}
					return;
				}
				else
				{
					if (kind == BinaryOperatorKind.RightShift)
					{
						return;
					}
					if (kind != BinaryOperatorKind.Equal && kind != BinaryOperatorKind.NotEqual)
					{
						goto IL_00F5;
					}
				}
				TypeSymbol type = left.Type;
				bool flag = type != null && type.IsDelegateType();
				TypeSymbol type2 = right.Type;
				bool flag2 = type2 != null && type2.IsDelegateType();
				if (!flag && !flag2)
				{
					BinaryOperatorKind binaryOperatorKind = kind.Operator();
					if (binaryOperatorKind == BinaryOperatorKind.Equal || binaryOperatorKind == BinaryOperatorKind.NotEqual)
					{
						TypeSymbol specialType = this._binder.Compilation.GetSpecialType(SpecialType.System_Delegate);
						specialType.AddUseSiteInfo(ref useSiteInfo, true);
						if (this.Conversions.ClassifyImplicitConversionFromExpression(left, specialType, ref useSiteInfo).IsValid && this.Conversions.ClassifyImplicitConversionFromExpression(right, specialType, ref useSiteInfo).IsValid)
						{
							this.AddDelegateOperation(kind, specialType, operators);
						}
					}
					return;
				}
				if (flag && flag2)
				{
					this.AddDelegateOperation(kind, type, operators);
					if (!((kind == BinaryOperatorKind.Equal || kind == BinaryOperatorKind.NotEqual) ? ConversionsBase.HasIdentityConversion(type, type2) : type.Equals(type2)))
					{
						this.AddDelegateOperation(kind, type2, operators);
					}
					return;
				}
				TypeSymbol typeSymbol = (flag ? type : type2);
				BoundExpression boundExpression = (flag ? right : left);
				if ((kind == BinaryOperatorKind.Equal || kind == BinaryOperatorKind.NotEqual) && boundExpression.Kind == BoundKind.UnboundLambda)
				{
					return;
				}
				this.AddDelegateOperation(kind, typeSymbol, operators);
				return;
			}
			else if (kind <= BinaryOperatorKind.LessThanOrEqual)
			{
				if (kind <= BinaryOperatorKind.LessThan)
				{
					if (kind != BinaryOperatorKind.GreaterThan && kind != BinaryOperatorKind.LessThan)
					{
						goto IL_00F5;
					}
				}
				else if (kind != BinaryOperatorKind.GreaterThanOrEqual && kind != BinaryOperatorKind.LessThanOrEqual)
				{
					goto IL_00F5;
				}
			}
			else if (kind <= BinaryOperatorKind.Xor)
			{
				if (kind != BinaryOperatorKind.And && kind != BinaryOperatorKind.Xor)
				{
					goto IL_00F5;
				}
			}
			else if (kind != BinaryOperatorKind.Or && kind != BinaryOperatorKind.LogicalAnd && kind != BinaryOperatorKind.LogicalOr)
			{
				goto IL_00F5;
			}
			return;
			IL_00F5:
			throw ExceptionUtilities.UnexpectedValue(kind);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00042C7C File Offset: 0x00040E7C
		private void GetEnumOperation(BinaryOperatorKind kind, TypeSymbol enumType, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorSignature> operators)
		{
			if (!enumType.IsValidEnumType())
			{
				return;
			}
			NamedTypeSymbol enumUnderlyingType = enumType.GetEnumUnderlyingType();
			NamedTypeSymbol specialType = this.Compilation.GetSpecialType(SpecialType.System_Nullable_T);
			NamedTypeSymbol namedTypeSymbol = specialType.Construct(new TypeSymbol[] { enumType });
			NamedTypeSymbol namedTypeSymbol2 = specialType.Construct(new TypeSymbol[] { enumUnderlyingType });
			if (kind <= BinaryOperatorKind.GreaterThan)
			{
				if (kind <= BinaryOperatorKind.Subtraction)
				{
					if (kind == BinaryOperatorKind.Addition)
					{
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.EnumAndUnderlyingAddition, enumType, enumUnderlyingType, enumType, null));
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.UnderlyingAndEnumAddition, enumUnderlyingType, enumType, enumType, null));
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.LiftedEnumAndUnderlyingAddition, namedTypeSymbol, namedTypeSymbol2, namedTypeSymbol, null));
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.LiftedUnderlyingAndEnumAddition, namedTypeSymbol2, namedTypeSymbol, namedTypeSymbol, null));
						return;
					}
					if (kind != BinaryOperatorKind.Subtraction)
					{
						return;
					}
					if (this.Strict)
					{
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.EnumSubtraction, enumType, enumType, enumUnderlyingType, null));
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.EnumAndUnderlyingSubtraction, enumType, enumUnderlyingType, enumType, null));
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.LiftedEnumSubtraction, namedTypeSymbol, namedTypeSymbol, namedTypeSymbol2, null));
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.LiftedEnumAndUnderlyingSubtraction, namedTypeSymbol, namedTypeSymbol2, namedTypeSymbol, null));
						return;
					}
					TypeSymbol type = right.Type;
					bool flag = TypeSymbol.Equals((type != null) ? type.StrippedType() : null, enumUnderlyingType, TypeCompareKind.ConsiderEverything);
					BinaryOperatorSignature binaryOperatorSignature = new BinaryOperatorSignature(BinaryOperatorKind.EnumSubtraction, enumType, enumType, enumUnderlyingType, null)
					{
						Priority = new int?(2)
					};
					operators.Add(binaryOperatorSignature);
					binaryOperatorSignature = new BinaryOperatorSignature(BinaryOperatorKind.EnumAndUnderlyingSubtraction, enumType, enumUnderlyingType, enumType, null)
					{
						Priority = new int?(flag ? 1 : 3)
					};
					operators.Add(binaryOperatorSignature);
					binaryOperatorSignature = new BinaryOperatorSignature(BinaryOperatorKind.LiftedEnumSubtraction, namedTypeSymbol, namedTypeSymbol, namedTypeSymbol2, null)
					{
						Priority = new int?(12)
					};
					operators.Add(binaryOperatorSignature);
					binaryOperatorSignature = new BinaryOperatorSignature(BinaryOperatorKind.LiftedEnumAndUnderlyingSubtraction, namedTypeSymbol, namedTypeSymbol2, namedTypeSymbol, null)
					{
						Priority = new int?(flag ? 11 : 13)
					};
					operators.Add(binaryOperatorSignature);
					binaryOperatorSignature = new BinaryOperatorSignature(BinaryOperatorKind.UnderlyingAndEnumSubtraction, enumUnderlyingType, enumType, enumType, null)
					{
						Priority = new int?(4)
					};
					operators.Add(binaryOperatorSignature);
					binaryOperatorSignature = new BinaryOperatorSignature(BinaryOperatorKind.LiftedUnderlyingAndEnumSubtraction, namedTypeSymbol2, namedTypeSymbol, namedTypeSymbol, null)
					{
						Priority = new int?(14)
					};
					operators.Add(binaryOperatorSignature);
					return;
				}
				else if (kind != BinaryOperatorKind.Equal && kind != BinaryOperatorKind.NotEqual && kind != BinaryOperatorKind.GreaterThan)
				{
					return;
				}
			}
			else if (kind <= BinaryOperatorKind.LessThanOrEqual)
			{
				if (kind != BinaryOperatorKind.LessThan && kind != BinaryOperatorKind.GreaterThanOrEqual && kind != BinaryOperatorKind.LessThanOrEqual)
				{
					return;
				}
			}
			else
			{
				if (kind != BinaryOperatorKind.And && kind != BinaryOperatorKind.Xor && kind != BinaryOperatorKind.Or)
				{
					return;
				}
				operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Enum, enumType, enumType, enumType, null));
				operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Lifted | BinaryOperatorKind.Enum, namedTypeSymbol, namedTypeSymbol, namedTypeSymbol, null));
				return;
			}
			NamedTypeSymbol specialType2 = this.Compilation.GetSpecialType(SpecialType.System_Boolean);
			operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Enum, enumType, enumType, specialType2, null));
			operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Lifted | BinaryOperatorKind.Enum, namedTypeSymbol, namedTypeSymbol, specialType2, null));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00042F90 File Offset: 0x00041190
		private void GetPointerArithmeticOperators(BinaryOperatorKind kind, PointerTypeSymbol pointerType, ArrayBuilder<BinaryOperatorSignature> operators)
		{
			if (kind == BinaryOperatorKind.Addition)
			{
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndIntAddition, pointerType, this.Compilation.GetSpecialType(SpecialType.System_Int32), pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndUIntAddition, pointerType, this.Compilation.GetSpecialType(SpecialType.System_UInt32), pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndLongAddition, pointerType, this.Compilation.GetSpecialType(SpecialType.System_Int64), pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndULongAddition, pointerType, this.Compilation.GetSpecialType(SpecialType.System_UInt64), pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.IntAndPointerAddition, this.Compilation.GetSpecialType(SpecialType.System_Int32), pointerType, pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.UIntAndPointerAddition, this.Compilation.GetSpecialType(SpecialType.System_UInt32), pointerType, pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.LongAndPointerAddition, this.Compilation.GetSpecialType(SpecialType.System_Int64), pointerType, pointerType, null));
				operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.ULongAndPointerAddition, this.Compilation.GetSpecialType(SpecialType.System_UInt64), pointerType, pointerType, null));
				return;
			}
			if (kind != BinaryOperatorKind.Subtraction)
			{
				return;
			}
			operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndIntSubtraction, pointerType, this.Compilation.GetSpecialType(SpecialType.System_Int32), pointerType, null));
			operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndUIntSubtraction, pointerType, this.Compilation.GetSpecialType(SpecialType.System_UInt32), pointerType, null));
			operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndLongSubtraction, pointerType, this.Compilation.GetSpecialType(SpecialType.System_Int64), pointerType, null));
			operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerAndULongSubtraction, pointerType, this.Compilation.GetSpecialType(SpecialType.System_UInt64), pointerType, null));
			operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.PointerSubtraction, pointerType, pointerType, this.Compilation.GetSpecialType(SpecialType.System_Int64), null));
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00043154 File Offset: 0x00041354
		private void GetPointerComparisonOperators(BinaryOperatorKind kind, ArrayBuilder<BinaryOperatorSignature> operators)
		{
			if (kind <= BinaryOperatorKind.GreaterThan)
			{
				if (kind != BinaryOperatorKind.Equal && kind != BinaryOperatorKind.NotEqual && kind != BinaryOperatorKind.GreaterThan)
				{
					return;
				}
			}
			else if (kind != BinaryOperatorKind.LessThan && kind != BinaryOperatorKind.GreaterThanOrEqual && kind != BinaryOperatorKind.LessThanOrEqual)
			{
				return;
			}
			PointerTypeSymbol pointerTypeSymbol = new PointerTypeSymbol(TypeWithAnnotations.Create(this.Compilation.GetSpecialType(SpecialType.System_Void), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
			operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Pointer, pointerTypeSymbol, pointerTypeSymbol, this.Compilation.GetSpecialType(SpecialType.System_Boolean), null));
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000431DC File Offset: 0x000413DC
		private void GetEnumOperations(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorSignature> results)
		{
			if (kind <= BinaryOperatorKind.Remainder)
			{
				if (kind != BinaryOperatorKind.Multiplication && kind != BinaryOperatorKind.Division && kind != BinaryOperatorKind.Remainder)
				{
					goto IL_004D;
				}
			}
			else if (kind <= BinaryOperatorKind.RightShift)
			{
				if (kind != BinaryOperatorKind.LeftShift && kind != BinaryOperatorKind.RightShift)
				{
					goto IL_004D;
				}
			}
			else if (kind != BinaryOperatorKind.LogicalAnd && kind != BinaryOperatorKind.LogicalOr)
			{
				goto IL_004D;
			}
			return;
			IL_004D:
			TypeSymbol typeSymbol = left.Type;
			if (typeSymbol != null)
			{
				typeSymbol = typeSymbol.StrippedType();
			}
			TypeSymbol typeSymbol2 = right.Type;
			if (typeSymbol2 != null)
			{
				typeSymbol2 = typeSymbol2.StrippedType();
			}
			bool flag;
			if (kind <= BinaryOperatorKind.GreaterThan)
			{
				if (kind <= BinaryOperatorKind.Subtraction)
				{
					if (kind == BinaryOperatorKind.Addition)
					{
						flag = true;
						goto IL_0101;
					}
					if (kind != BinaryOperatorKind.Subtraction)
					{
						goto IL_00F5;
					}
					flag = true;
					goto IL_0101;
				}
				else if (kind != BinaryOperatorKind.Equal && kind != BinaryOperatorKind.NotEqual && kind != BinaryOperatorKind.GreaterThan)
				{
					goto IL_00F5;
				}
			}
			else if (kind <= BinaryOperatorKind.LessThanOrEqual)
			{
				if (kind != BinaryOperatorKind.LessThan && kind != BinaryOperatorKind.GreaterThanOrEqual && kind != BinaryOperatorKind.LessThanOrEqual)
				{
					goto IL_00F5;
				}
			}
			else
			{
				if (kind == BinaryOperatorKind.And || kind == BinaryOperatorKind.Xor || kind == BinaryOperatorKind.Or)
				{
					flag = false;
					goto IL_0101;
				}
				goto IL_00F5;
			}
			flag = true;
			goto IL_0101;
			IL_00F5:
			throw ExceptionUtilities.UnexpectedValue(kind);
			IL_0101:
			if (typeSymbol != null)
			{
				this.GetEnumOperation(kind, typeSymbol, left, right, results);
			}
			if (typeSymbol2 != null && (typeSymbol == null || !(flag ? ConversionsBase.HasIdentityConversion(typeSymbol2, typeSymbol) : typeSymbol2.Equals(typeSymbol))))
			{
				this.GetEnumOperation(kind, typeSymbol2, left, right, results);
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00043320 File Offset: 0x00041520
		private void GetPointerOperators(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorSignature> results)
		{
			PointerTypeSymbol pointerTypeSymbol = left.Type as PointerTypeSymbol;
			PointerTypeSymbol pointerTypeSymbol2 = right.Type as PointerTypeSymbol;
			if (pointerTypeSymbol != null)
			{
				this.GetPointerArithmeticOperators(kind, pointerTypeSymbol, results);
			}
			if (pointerTypeSymbol2 != null && (pointerTypeSymbol == null || !ConversionsBase.HasIdentityConversion(pointerTypeSymbol2, pointerTypeSymbol)))
			{
				this.GetPointerArithmeticOperators(kind, pointerTypeSymbol2, results);
			}
			if (pointerTypeSymbol != null || pointerTypeSymbol2 != null || left.Type is FunctionPointerTypeSymbol || right.Type is FunctionPointerTypeSymbol)
			{
				this.GetPointerComparisonOperators(kind, results);
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00043394 File Offset: 0x00041594
		private void GetAllBuiltInOperators(BinaryOperatorKind kind, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorAnalysisResult> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			kind = kind.OperatorWithLogical();
			ArrayBuilder<BinaryOperatorSignature> instance = ArrayBuilder<BinaryOperatorSignature>.GetInstance();
			if ((kind == BinaryOperatorKind.Equal || kind == BinaryOperatorKind.NotEqual) && OverloadResolution.<GetAllBuiltInOperators>g__useOnlyReferenceEquality|14_0(this.Conversions, left, right, ref useSiteInfo))
			{
				this.GetReferenceEquality(kind, instance);
			}
			else
			{
				this.Compilation.builtInOperators.GetSimpleBuiltInOperators(kind, instance, !left.Type.IsNativeIntegerOrNullableNativeIntegerType() && !right.Type.IsNativeIntegerOrNullableNativeIntegerType());
				this.GetDelegateOperations(kind, left, right, instance, ref useSiteInfo);
				this.GetEnumOperations(kind, left, right, instance);
				this.GetPointerOperators(kind, left, right, instance);
			}
			this.CandidateOperators(instance, left, right, results, ref useSiteInfo);
			instance.Free();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00043444 File Offset: 0x00041644
		private void GetReferenceEquality(BinaryOperatorKind kind, ArrayBuilder<BinaryOperatorSignature> operators)
		{
			NamedTypeSymbol specialType = this.Compilation.GetSpecialType(SpecialType.System_Object);
			operators.Add(new BinaryOperatorSignature(kind | BinaryOperatorKind.Object, specialType, specialType, this.Compilation.GetSpecialType(SpecialType.System_Boolean), null));
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0004347C File Offset: 0x0004167C
		private bool CandidateOperators(ArrayBuilder<BinaryOperatorSignature> operators, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorAnalysisResult> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag = false;
			foreach (BinaryOperatorSignature binaryOperatorSignature in operators)
			{
				Conversion conversion = this.Conversions.ClassifyConversionFromExpression(left, binaryOperatorSignature.LeftType, ref useSiteInfo, false);
				Conversion conversion2 = this.Conversions.ClassifyConversionFromExpression(right, binaryOperatorSignature.RightType, ref useSiteInfo, false);
				if (conversion.IsImplicit && conversion2.IsImplicit)
				{
					results.Add(BinaryOperatorAnalysisResult.Applicable(binaryOperatorSignature, conversion, conversion2));
					flag = true;
				}
				else
				{
					results.Add(BinaryOperatorAnalysisResult.Inapplicable(binaryOperatorSignature, conversion, conversion2));
				}
			}
			return flag;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0004350C File Offset: 0x0004170C
		private static void AddDistinctOperators(ArrayBuilder<BinaryOperatorAnalysisResult> result, ArrayBuilder<BinaryOperatorAnalysisResult> additionalOperators)
		{
			int count = result.Count;
			foreach (BinaryOperatorAnalysisResult binaryOperatorAnalysisResult in additionalOperators)
			{
				bool flag = false;
				for (int i = 0; i < count; i++)
				{
					BinaryOperatorSignature signature = result[i].Signature;
					if (binaryOperatorAnalysisResult.Signature.Kind == signature.Kind && OverloadResolution.<AddDistinctOperators>g__equalsIgnoringNullable|17_0(binaryOperatorAnalysisResult.Signature.ReturnType, signature.ReturnType) && OverloadResolution.<AddDistinctOperators>g__equalsIgnoringNullableAndDynamic|17_1(binaryOperatorAnalysisResult.Signature.LeftType, signature.LeftType) && OverloadResolution.<AddDistinctOperators>g__equalsIgnoringNullableAndDynamic|17_1(binaryOperatorAnalysisResult.Signature.RightType, signature.RightType) && OverloadResolution.<AddDistinctOperators>g__equalsIgnoringNullableAndDynamic|17_1(binaryOperatorAnalysisResult.Signature.Method.ContainingType, signature.Method.ContainingType))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					result.Add(binaryOperatorAnalysisResult);
				}
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000435F8 File Offset: 0x000417F8
		private bool GetUserDefinedOperators(BinaryOperatorKind kind, TypeSymbol type0, BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorAnalysisResult> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (type0 == null || OperatorFacts.DefinitelyHasNoUserDefinedOperators(type0))
			{
				return false;
			}
			string text = OperatorFacts.BinaryOperatorNameFromOperatorKind(kind);
			ArrayBuilder<BinaryOperatorSignature> instance = ArrayBuilder<BinaryOperatorSignature>.GetInstance();
			bool flag = false;
			NamedTypeSymbol namedTypeSymbol = type0 as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				namedTypeSymbol = type0.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			if (namedTypeSymbol == null && type0.IsTypeParameter())
			{
				namedTypeSymbol = ((TypeParameterSymbol)type0).EffectiveBaseClass(ref useSiteInfo);
			}
			while (namedTypeSymbol != null)
			{
				instance.Clear();
				this.GetUserDefinedBinaryOperatorsFromType(namedTypeSymbol, kind, text, instance);
				results.Clear();
				if (this.CandidateOperators(instance, left, right, results, ref useSiteInfo))
				{
					flag = true;
					break;
				}
				namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			instance.Free();
			return flag;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00043690 File Offset: 0x00041890
		private void GetUserDefinedBinaryOperatorsFromType(NamedTypeSymbol type, BinaryOperatorKind kind, string name, ArrayBuilder<BinaryOperatorSignature> operators)
		{
			foreach (MethodSymbol methodSymbol in type.GetOperators(name))
			{
				if (methodSymbol.ParameterCount == 2 && !methodSymbol.ReturnsVoid)
				{
					TypeSymbol parameterType = methodSymbol.GetParameterType(0);
					TypeSymbol parameterType2 = methodSymbol.GetParameterType(1);
					TypeSymbol returnType = methodSymbol.ReturnType;
					operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.UserDefined | kind, parameterType, parameterType2, returnType, methodSymbol));
					OverloadResolution.LiftingResult liftingResult = OverloadResolution.UserDefinedBinaryOperatorCanBeLifted(parameterType, parameterType2, returnType, kind);
					if (liftingResult == OverloadResolution.LiftingResult.LiftOperandsAndResult)
					{
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.ULong | BinaryOperatorKind.PointerAndUInt | BinaryOperatorKind.Lifted | kind, this.MakeNullable(parameterType), this.MakeNullable(parameterType2), this.MakeNullable(returnType), methodSymbol));
					}
					else if (liftingResult == OverloadResolution.LiftingResult.LiftOperandsButNotResult)
					{
						operators.Add(new BinaryOperatorSignature(BinaryOperatorKind.ULong | BinaryOperatorKind.PointerAndUInt | BinaryOperatorKind.Lifted | kind, this.MakeNullable(parameterType), this.MakeNullable(parameterType2), returnType, methodSymbol));
					}
				}
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00043778 File Offset: 0x00041978
		private static OverloadResolution.LiftingResult UserDefinedBinaryOperatorCanBeLifted(TypeSymbol left, TypeSymbol right, TypeSymbol result, BinaryOperatorKind kind)
		{
			if (!left.IsValueType || left.IsNullableType() || !right.IsValueType || right.IsNullableType())
			{
				return OverloadResolution.LiftingResult.NotLifted;
			}
			if (kind <= BinaryOperatorKind.GreaterThan)
			{
				if (kind != BinaryOperatorKind.Equal && kind != BinaryOperatorKind.NotEqual)
				{
					if (kind != BinaryOperatorKind.GreaterThan)
					{
						goto IL_0077;
					}
				}
				else if (!TypeSymbol.Equals(left, right, TypeCompareKind.ConsiderEverything))
				{
					return OverloadResolution.LiftingResult.NotLifted;
				}
			}
			else if (kind != BinaryOperatorKind.LessThan && kind != BinaryOperatorKind.GreaterThanOrEqual && kind != BinaryOperatorKind.LessThanOrEqual)
			{
				goto IL_0077;
			}
			if (result.SpecialType != SpecialType.System_Boolean)
			{
				return OverloadResolution.LiftingResult.NotLifted;
			}
			return OverloadResolution.LiftingResult.LiftOperandsButNotResult;
			IL_0077:
			if (!result.IsValueType || result.IsNullableType())
			{
				return OverloadResolution.LiftingResult.NotLifted;
			}
			return OverloadResolution.LiftingResult.LiftOperandsAndResult;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00043810 File Offset: 0x00041A10
		private void BinaryOperatorOverloadResolution(BoundExpression left, BoundExpression right, BinaryOperatorOverloadResolutionResult result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (result.SingleValid())
			{
				return;
			}
			ArrayBuilder<BinaryOperatorAnalysisResult> results = result.Results;
			int theBestCandidateIndex = this.GetTheBestCandidateIndex(left, right, results, ref useSiteInfo);
			if (theBestCandidateIndex != -1)
			{
				for (int i = 0; i < results.Count; i++)
				{
					if (results[i].Kind != OperatorAnalysisResultKind.Inapplicable && i != theBestCandidateIndex)
					{
						results[i] = results[i].Worse();
					}
				}
				return;
			}
			for (int j = 1; j < results.Count; j++)
			{
				if (results[j].Kind == OperatorAnalysisResultKind.Applicable)
				{
					for (int k = 0; k < j; k++)
					{
						if (results[k].Kind != OperatorAnalysisResultKind.Inapplicable)
						{
							BetterResult betterResult = this.BetterOperator(results[j].Signature, results[k].Signature, left, right, ref useSiteInfo);
							if (betterResult == BetterResult.Left)
							{
								results[k] = results[k].Worse();
							}
							else if (betterResult == BetterResult.Right)
							{
								results[j] = results[j].Worse();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00043928 File Offset: 0x00041B28
		private int GetTheBestCandidateIndex(BoundExpression left, BoundExpression right, ArrayBuilder<BinaryOperatorAnalysisResult> candidates, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int num = -1;
			for (int i = 0; i < candidates.Count; i++)
			{
				if (candidates[i].Kind == OperatorAnalysisResultKind.Applicable)
				{
					if (num == -1)
					{
						num = i;
					}
					else
					{
						BetterResult betterResult = this.BetterOperator(candidates[num].Signature, candidates[i].Signature, left, right, ref useSiteInfo);
						if (betterResult == BetterResult.Right)
						{
							num = i;
						}
						else if (betterResult != BetterResult.Left)
						{
							num = -1;
						}
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (candidates[j].Kind != OperatorAnalysisResultKind.Inapplicable && this.BetterOperator(candidates[num].Signature, candidates[j].Signature, left, right, ref useSiteInfo) != BetterResult.Left)
				{
					return -1;
				}
			}
			return num;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x000439D4 File Offset: 0x00041BD4
		private BetterResult BetterOperator(BinaryOperatorSignature op1, BinaryOperatorSignature op2, BoundExpression left, BoundExpression right, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (op1.Priority != null && op1.Priority.GetValueOrDefault() != op2.Priority.GetValueOrDefault())
			{
				if (op1.Priority.GetValueOrDefault() >= op2.Priority.GetValueOrDefault())
				{
					return BetterResult.Right;
				}
				return BetterResult.Left;
			}
			else
			{
				BetterResult betterResult = this.BetterConversionFromExpression(left, op1.LeftType, op2.LeftType, ref useSiteInfo);
				BetterResult betterResult2 = this.BetterConversionFromExpression(right, op1.RightType, op2.RightType, ref useSiteInfo);
				if ((betterResult == BetterResult.Left && betterResult2 != BetterResult.Right) || (betterResult != BetterResult.Right && betterResult2 == BetterResult.Left))
				{
					return BetterResult.Left;
				}
				if ((betterResult == BetterResult.Right && betterResult2 != BetterResult.Left) || (betterResult != BetterResult.Left && betterResult2 == BetterResult.Right))
				{
					return BetterResult.Right;
				}
				if (ConversionsBase.HasIdentityConversion(op1.LeftType, op2.LeftType) && ConversionsBase.HasIdentityConversion(op1.RightType, op2.RightType))
				{
					BetterResult betterResult3 = this.MoreSpecificOperator(op1, op2, ref useSiteInfo);
					if (betterResult3 == BetterResult.Left || betterResult3 == BetterResult.Right)
					{
						return betterResult3;
					}
					bool flag = op1.Kind.IsLifted();
					bool flag2 = op2.Kind.IsLifted();
					if (flag && !flag2)
					{
						return BetterResult.Right;
					}
					if (!flag && flag2)
					{
						return BetterResult.Left;
					}
				}
				BetterResult betterResult4;
				if (op1.LeftRefKind == RefKind.None && op2.LeftRefKind == RefKind.In)
				{
					betterResult4 = BetterResult.Left;
				}
				else if (op2.LeftRefKind == RefKind.None && op1.LeftRefKind == RefKind.In)
				{
					betterResult4 = BetterResult.Right;
				}
				else
				{
					betterResult4 = BetterResult.Neither;
				}
				if (op1.RightRefKind == RefKind.None && op2.RightRefKind == RefKind.In)
				{
					if (betterResult4 == BetterResult.Right)
					{
						return BetterResult.Neither;
					}
					betterResult4 = BetterResult.Left;
				}
				else if (op2.RightRefKind == RefKind.None && op1.RightRefKind == RefKind.In)
				{
					if (betterResult4 == BetterResult.Left)
					{
						return BetterResult.Neither;
					}
					betterResult4 = BetterResult.Right;
				}
				return betterResult4;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00043B48 File Offset: 0x00041D48
		private BetterResult MoreSpecificOperator(BinaryOperatorSignature op1, BinaryOperatorSignature op2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol typeSymbol;
			TypeSymbol typeSymbol2;
			if (op1.Method != null)
			{
				ImmutableArray<ParameterSymbol> parameters = op1.Method.OriginalDefinition.GetParameters();
				typeSymbol = parameters[0].Type;
				typeSymbol2 = parameters[1].Type;
				if (op1.Kind.IsLifted())
				{
					typeSymbol = this.MakeNullable(typeSymbol);
					typeSymbol2 = this.MakeNullable(typeSymbol2);
				}
			}
			else
			{
				typeSymbol = op1.LeftType;
				typeSymbol2 = op1.RightType;
			}
			TypeSymbol typeSymbol3;
			TypeSymbol typeSymbol4;
			if (op2.Method != null)
			{
				ImmutableArray<ParameterSymbol> parameters2 = op2.Method.OriginalDefinition.GetParameters();
				typeSymbol3 = parameters2[0].Type;
				typeSymbol4 = parameters2[1].Type;
				if (op2.Kind.IsLifted())
				{
					typeSymbol3 = this.MakeNullable(typeSymbol3);
					typeSymbol4 = this.MakeNullable(typeSymbol4);
				}
			}
			else
			{
				typeSymbol3 = op2.LeftType;
				typeSymbol4 = op2.RightType;
			}
			ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance();
			ArrayBuilder<TypeSymbol> instance2 = ArrayBuilder<TypeSymbol>.GetInstance();
			instance.Add(typeSymbol);
			instance.Add(typeSymbol2);
			instance2.Add(typeSymbol3);
			instance2.Add(typeSymbol4);
			BetterResult betterResult = OverloadResolution.MoreSpecificType(instance, instance2, ref useSiteInfo);
			instance.Free();
			instance2.Free();
			return betterResult;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00043C5E File Offset: 0x00041E5E
		[Conditional("DEBUG")]
		private static void AssertNotChecked(BinaryOperatorKind kind)
		{
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00043C60 File Offset: 0x00041E60
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private void UnaryOperatorEasyOut(UnaryOperatorKind kind, BoundExpression operand, UnaryOperatorOverloadResolutionResult result)
		{
			TypeSymbol type = operand.Type;
			if (type == null)
			{
				return;
			}
			UnaryOperatorKind unaryOperatorKind = OverloadResolution.UnopEasyOut.OpKind(kind, type);
			if (unaryOperatorKind == UnaryOperatorKind.Error)
			{
				return;
			}
			UnaryOperatorSignature signature = this.Compilation.builtInOperators.GetSignature(unaryOperatorKind);
			Conversion? conversion = new Conversion?(ConversionsBase.FastClassifyConversion(type, signature.OperandType));
			result.Results.Add(UnaryOperatorAnalysisResult.Applicable(signature, conversion.Value));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00043CC1 File Offset: 0x00041EC1
		private NamedTypeSymbol MakeNullable(TypeSymbol type)
		{
			return this.Compilation.GetSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { type });
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00043CE0 File Offset: 0x00041EE0
		public void UnaryOperatorOverloadResolution(UnaryOperatorKind kind, BoundExpression operand, UnaryOperatorOverloadResolutionResult result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			this.UnaryOperatorEasyOut(kind, operand, result);
			if (result.Results.Count > 0)
			{
				return;
			}
			if (!this.GetUserDefinedOperators(kind, operand, result.Results, ref useSiteInfo))
			{
				result.Results.Clear();
				this.GetAllBuiltInOperators(kind, operand, result.Results, ref useSiteInfo);
			}
			this.UnaryOperatorOverloadResolution(operand, result, ref useSiteInfo);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00043D3C File Offset: 0x00041F3C
		private void UnaryOperatorOverloadResolution(BoundExpression operand, UnaryOperatorOverloadResolutionResult result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (result.SingleValid())
			{
				return;
			}
			ArrayBuilder<UnaryOperatorAnalysisResult> results = result.Results;
			int theBestCandidateIndex = this.GetTheBestCandidateIndex(operand, results, ref useSiteInfo);
			if (theBestCandidateIndex != -1)
			{
				for (int i = 0; i < results.Count; i++)
				{
					if (results[i].Kind != OperatorAnalysisResultKind.Inapplicable && i != theBestCandidateIndex)
					{
						results[i] = results[i].Worse();
					}
				}
				return;
			}
			for (int j = 1; j < results.Count; j++)
			{
				if (results[j].Kind == OperatorAnalysisResultKind.Applicable)
				{
					for (int k = 0; k < j; k++)
					{
						if (results[k].Kind != OperatorAnalysisResultKind.Inapplicable)
						{
							BetterResult betterResult = this.BetterOperator(results[j].Signature, results[k].Signature, operand, ref useSiteInfo);
							if (betterResult == BetterResult.Left)
							{
								results[k] = results[k].Worse();
							}
							else if (betterResult == BetterResult.Right)
							{
								results[j] = results[j].Worse();
							}
						}
					}
				}
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00043E50 File Offset: 0x00042050
		private int GetTheBestCandidateIndex(BoundExpression operand, ArrayBuilder<UnaryOperatorAnalysisResult> candidates, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int num = -1;
			for (int i = 0; i < candidates.Count; i++)
			{
				if (candidates[i].Kind == OperatorAnalysisResultKind.Applicable)
				{
					if (num == -1)
					{
						num = i;
					}
					else
					{
						BetterResult betterResult = this.BetterOperator(candidates[num].Signature, candidates[i].Signature, operand, ref useSiteInfo);
						if (betterResult == BetterResult.Right)
						{
							num = i;
						}
						else if (betterResult != BetterResult.Left)
						{
							num = -1;
						}
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (candidates[j].Kind != OperatorAnalysisResultKind.Inapplicable && this.BetterOperator(candidates[num].Signature, candidates[j].Signature, operand, ref useSiteInfo) != BetterResult.Left)
				{
					return -1;
				}
			}
			return num;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00043EF8 File Offset: 0x000420F8
		private BetterResult BetterOperator(UnaryOperatorSignature op1, UnaryOperatorSignature op2, BoundExpression operand, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			BetterResult betterResult = this.BetterConversionFromExpression(operand, op1.OperandType, op2.OperandType, ref useSiteInfo);
			if (betterResult == BetterResult.Left || betterResult == BetterResult.Right)
			{
				return betterResult;
			}
			if (ConversionsBase.HasIdentityConversion(op1.OperandType, op2.OperandType))
			{
				bool flag = op1.Kind.IsLifted();
				bool flag2 = op2.Kind.IsLifted();
				if (flag && !flag2)
				{
					return BetterResult.Right;
				}
				if (!flag && flag2)
				{
					return BetterResult.Left;
				}
			}
			if (op1.RefKind == RefKind.None && op2.RefKind == RefKind.In)
			{
				return BetterResult.Left;
			}
			if (op2.RefKind == RefKind.None && op1.RefKind == RefKind.In)
			{
				return BetterResult.Right;
			}
			return BetterResult.Neither;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00043F8C File Offset: 0x0004218C
		private void GetAllBuiltInOperators(UnaryOperatorKind kind, BoundExpression operand, ArrayBuilder<UnaryOperatorAnalysisResult> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<UnaryOperatorSignature> instance = ArrayBuilder<UnaryOperatorSignature>.GetInstance();
			this.Compilation.builtInOperators.GetSimpleBuiltInOperators(kind, instance, !operand.Type.IsNativeIntegerOrNullableNativeIntegerType());
			this.GetEnumOperations(kind, operand, instance);
			UnaryOperatorSignature? pointerOperation = OverloadResolution.GetPointerOperation(kind, operand);
			if (pointerOperation != null)
			{
				instance.Add(pointerOperation.Value);
			}
			this.CandidateOperators(instance, operand, results, ref useSiteInfo);
			instance.Free();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00043FF8 File Offset: 0x000421F8
		private bool CandidateOperators(ArrayBuilder<UnaryOperatorSignature> operators, BoundExpression operand, ArrayBuilder<UnaryOperatorAnalysisResult> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag = false;
			foreach (UnaryOperatorSignature unaryOperatorSignature in operators)
			{
				Conversion conversion = this.Conversions.ClassifyConversionFromExpression(operand, unaryOperatorSignature.OperandType, ref useSiteInfo, false);
				if (conversion.IsImplicit)
				{
					flag = true;
					results.Add(UnaryOperatorAnalysisResult.Applicable(unaryOperatorSignature, conversion));
				}
				else
				{
					results.Add(UnaryOperatorAnalysisResult.Inapplicable(unaryOperatorSignature, conversion));
				}
			}
			return flag;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00044060 File Offset: 0x00042260
		private void GetEnumOperations(UnaryOperatorKind kind, BoundExpression operand, ArrayBuilder<UnaryOperatorSignature> operators)
		{
			TypeSymbol typeSymbol = operand.Type;
			if (typeSymbol == null)
			{
				return;
			}
			typeSymbol = typeSymbol.StrippedType();
			if (!typeSymbol.IsValidEnumType())
			{
				return;
			}
			NamedTypeSymbol namedTypeSymbol = this.MakeNullable(typeSymbol);
			if (kind <= UnaryOperatorKind.PostfixDecrement)
			{
				if (kind != UnaryOperatorKind.PostfixIncrement && kind != UnaryOperatorKind.PostfixDecrement)
				{
					return;
				}
			}
			else if (kind != UnaryOperatorKind.PrefixIncrement && kind != UnaryOperatorKind.PrefixDecrement && kind != UnaryOperatorKind.BitwiseComplement)
			{
				return;
			}
			operators.Add(new UnaryOperatorSignature(kind | UnaryOperatorKind.Enum, typeSymbol, typeSymbol, null));
			operators.Add(new UnaryOperatorSignature(kind | UnaryOperatorKind.Lifted | UnaryOperatorKind.Enum, namedTypeSymbol, namedTypeSymbol, null));
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000440EC File Offset: 0x000422EC
		private static UnaryOperatorSignature? GetPointerOperation(UnaryOperatorKind kind, BoundExpression operand)
		{
			PointerTypeSymbol pointerTypeSymbol = operand.Type as PointerTypeSymbol;
			if (pointerTypeSymbol == null)
			{
				return default(UnaryOperatorSignature?);
			}
			UnaryOperatorSignature? unaryOperatorSignature = default(UnaryOperatorSignature?);
			if (kind <= UnaryOperatorKind.PostfixDecrement)
			{
				if (kind != UnaryOperatorKind.PostfixIncrement && kind != UnaryOperatorKind.PostfixDecrement)
				{
					return unaryOperatorSignature;
				}
			}
			else if (kind != UnaryOperatorKind.PrefixIncrement && kind != UnaryOperatorKind.PrefixDecrement)
			{
				return unaryOperatorSignature;
			}
			unaryOperatorSignature = new UnaryOperatorSignature?(new UnaryOperatorSignature(kind | UnaryOperatorKind.Pointer, pointerTypeSymbol, pointerTypeSymbol, null));
			return unaryOperatorSignature;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00044158 File Offset: 0x00042358
		private bool GetUserDefinedOperators(UnaryOperatorKind kind, BoundExpression operand, ArrayBuilder<UnaryOperatorAnalysisResult> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (operand.Type == null)
			{
				return false;
			}
			TypeSymbol typeSymbol = operand.Type.StrippedType();
			if (OperatorFacts.DefinitelyHasNoUserDefinedOperators(typeSymbol))
			{
				return false;
			}
			string text = OperatorFacts.UnaryOperatorNameFromOperatorKind(kind);
			ArrayBuilder<UnaryOperatorSignature> instance = ArrayBuilder<UnaryOperatorSignature>.GetInstance();
			bool flag = false;
			NamedTypeSymbol namedTypeSymbol = typeSymbol as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				namedTypeSymbol = typeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			if (namedTypeSymbol == null && typeSymbol.IsTypeParameter())
			{
				namedTypeSymbol = ((TypeParameterSymbol)typeSymbol).EffectiveBaseClass(ref useSiteInfo);
			}
			while (namedTypeSymbol != null)
			{
				instance.Clear();
				this.GetUserDefinedUnaryOperatorsFromType(namedTypeSymbol, kind, text, instance);
				results.Clear();
				if (this.CandidateOperators(instance, operand, results, ref useSiteInfo))
				{
					flag = true;
					break;
				}
				namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			}
			if (!flag)
			{
				ImmutableArray<NamedTypeSymbol> immutableArray = default(ImmutableArray<NamedTypeSymbol>);
				if (typeSymbol.IsInterfaceType())
				{
					immutableArray = typeSymbol.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
				}
				else if (typeSymbol.IsTypeParameter())
				{
					immutableArray = ((TypeParameterSymbol)typeSymbol).AllEffectiveInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
				}
				if (!immutableArray.IsDefaultOrEmpty)
				{
					PooledHashSet<NamedTypeSymbol> instance2 = PooledHashSet<NamedTypeSymbol>.GetInstance();
					ArrayBuilder<UnaryOperatorAnalysisResult> instance3 = ArrayBuilder<UnaryOperatorAnalysisResult>.GetInstance();
					results.Clear();
					foreach (NamedTypeSymbol namedTypeSymbol2 in immutableArray)
					{
						if (namedTypeSymbol2.IsInterface && !instance2.Contains(namedTypeSymbol2))
						{
							instance.Clear();
							instance3.Clear();
							this.GetUserDefinedUnaryOperatorsFromType(namedTypeSymbol2, kind, text, instance);
							if (this.CandidateOperators(instance, operand, instance3, ref useSiteInfo))
							{
								flag = true;
								results.AddRange(instance3);
								instance2.AddAll<NamedTypeSymbol>(namedTypeSymbol2.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo));
							}
						}
					}
					instance2.Free();
					instance3.Free();
				}
			}
			instance.Free();
			return flag;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000442E0 File Offset: 0x000424E0
		private void GetUserDefinedUnaryOperatorsFromType(NamedTypeSymbol type, UnaryOperatorKind kind, string name, ArrayBuilder<UnaryOperatorSignature> operators)
		{
			foreach (MethodSymbol methodSymbol in type.GetOperators(name))
			{
				if (methodSymbol.ParameterCount == 1 && !methodSymbol.ReturnsVoid)
				{
					TypeSymbol parameterType = methodSymbol.GetParameterType(0);
					TypeSymbol returnType = methodSymbol.ReturnType;
					operators.Add(new UnaryOperatorSignature(UnaryOperatorKind.UserDefined | kind, parameterType, returnType, methodSymbol));
					if (kind <= UnaryOperatorKind.PrefixDecrement)
					{
						if (kind <= UnaryOperatorKind.PostfixDecrement)
						{
							if (kind != UnaryOperatorKind.PostfixIncrement && kind != UnaryOperatorKind.PostfixDecrement)
							{
								continue;
							}
						}
						else if (kind != UnaryOperatorKind.PrefixIncrement && kind != UnaryOperatorKind.PrefixDecrement)
						{
							continue;
						}
					}
					else if (kind <= UnaryOperatorKind.UnaryMinus)
					{
						if (kind != UnaryOperatorKind.UnaryPlus && kind != UnaryOperatorKind.UnaryMinus)
						{
							continue;
						}
					}
					else if (kind != UnaryOperatorKind.LogicalNegation && kind != UnaryOperatorKind.BitwiseComplement)
					{
						continue;
					}
					if (parameterType.IsValueType && !parameterType.IsNullableType() && returnType.IsValueType && !returnType.IsNullableType())
					{
						operators.Add(new UnaryOperatorSignature(UnaryOperatorKind.ULong | UnaryOperatorKind._PointerAndUInt | UnaryOperatorKind.Lifted | kind, this.MakeNullable(parameterType), this.MakeNullable(returnType), methodSymbol));
					}
				}
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000443F7 File Offset: 0x000425F7
		public OverloadResolution(Binder binder)
		{
			this._binder = binder;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00044406 File Offset: 0x00042606
		private CSharpCompilation Compilation
		{
			get
			{
				return this._binder.Compilation;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00044413 File Offset: 0x00042613
		private Conversions Conversions
		{
			get
			{
				return this._binder.Conversions;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00044420 File Offset: 0x00042620
		private bool Strict
		{
			get
			{
				if (this._strict != null)
				{
					return this._strict.Value;
				}
				bool featureStrictEnabled = this._binder.Compilation.FeatureStrictEnabled;
				this._strict = new bool?(featureStrictEnabled);
				return featureStrictEnabled;
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00044464 File Offset: 0x00042664
		private static bool AnyValidResult<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results) where TMember : Symbol
		{
			foreach (MemberResolutionResult<TMember> memberResolutionResult in results)
			{
				if (memberResolutionResult.IsValid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00044498 File Offset: 0x00042698
		private static bool SingleValidResult<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results) where TMember : Symbol
		{
			bool flag = false;
			foreach (MemberResolutionResult<TMember> memberResolutionResult in results)
			{
				if (memberResolutionResult.IsValid)
				{
					if (flag)
					{
						return false;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000444D4 File Offset: 0x000426D4
		public void ObjectCreationOverloadResolution(ImmutableArray<MethodSymbol> constructors, AnalyzedArguments arguments, OverloadResolutionResult<MethodSymbol> result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<MemberResolutionResult<MethodSymbol>> resultsBuilder = result.ResultsBuilder;
			this.PerformObjectCreationOverloadResolution(resultsBuilder, constructors, arguments, false, ref useSiteInfo);
			if (!OverloadResolution.OverloadResolutionResultIsValid<MethodSymbol>(resultsBuilder, arguments.HasDynamicArgument))
			{
				result.Clear();
				this.PerformObjectCreationOverloadResolution(resultsBuilder, constructors, arguments, true, ref useSiteInfo);
			}
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00044514 File Offset: 0x00042714
		public void MethodInvocationOverloadResolution(ArrayBuilder<MethodSymbol> methods, ArrayBuilder<TypeWithAnnotations> typeArguments, BoundExpression receiver, AnalyzedArguments arguments, OverloadResolutionResult<MethodSymbol> result, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool isMethodGroupConversion = false, bool allowRefOmittedArguments = false, bool inferWithDynamic = false, bool allowUnexpandedForm = true, RefKind returnRefKind = RefKind.None, TypeSymbol returnType = null, bool isFunctionPointerResolution = false, in CallingConventionInfo callingConventionInfo = null)
		{
			this.MethodOrPropertyOverloadResolution<MethodSymbol>(methods, typeArguments, receiver, arguments, result, isMethodGroupConversion, allowRefOmittedArguments, ref useSiteInfo, inferWithDynamic, allowUnexpandedForm, returnRefKind, returnType, isFunctionPointerResolution, in callingConventionInfo);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00044540 File Offset: 0x00042740
		public void PropertyOverloadResolution(ArrayBuilder<PropertySymbol> indexers, BoundExpression receiverOpt, AnalyzedArguments arguments, OverloadResolutionResult<PropertySymbol> result, bool allowRefOmittedArguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> arrayBuilder = instance;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			RefKind refKind = RefKind.None;
			TypeSymbol typeSymbol = null;
			bool flag4 = false;
			CallingConventionInfo callingConventionInfo = default(CallingConventionInfo);
			this.MethodOrPropertyOverloadResolution<PropertySymbol>(indexers, arrayBuilder, receiverOpt, arguments, result, flag, allowRefOmittedArguments, ref useSiteInfo, flag2, flag3, refKind, typeSymbol, flag4, in callingConventionInfo);
			instance.Free();
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00044578 File Offset: 0x00042778
		internal void MethodOrPropertyOverloadResolution<TMember>(ArrayBuilder<TMember> members, ArrayBuilder<TypeWithAnnotations> typeArguments, BoundExpression receiver, AnalyzedArguments arguments, OverloadResolutionResult<TMember> result, bool isMethodGroupConversion, bool allowRefOmittedArguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool inferWithDynamic = false, bool allowUnexpandedForm = true, RefKind returnRefKind = RefKind.None, TypeSymbol returnType = null, bool isFunctionPointerResolution = false, in CallingConventionInfo callingConventionInfo = null) where TMember : Symbol
		{
			ArrayBuilder<MemberResolutionResult<TMember>> resultsBuilder = result.ResultsBuilder;
			this.PerformMemberOverloadResolution<TMember>(resultsBuilder, members, typeArguments, receiver, arguments, false, isMethodGroupConversion, returnRefKind, returnType, allowRefOmittedArguments, isFunctionPointerResolution, in callingConventionInfo, ref useSiteInfo, inferWithDynamic, allowUnexpandedForm);
			if (!OverloadResolution.OverloadResolutionResultIsValid<TMember>(resultsBuilder, arguments.HasDynamicArgument))
			{
				result.Clear();
				this.PerformMemberOverloadResolution<TMember>(resultsBuilder, members, typeArguments, receiver, arguments, true, isMethodGroupConversion, returnRefKind, returnType, allowRefOmittedArguments, isFunctionPointerResolution, in callingConventionInfo, ref useSiteInfo, false, allowUnexpandedForm);
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000445E0 File Offset: 0x000427E0
		private static bool OverloadResolutionResultIsValid<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, bool hasDynamicArgument) where TMember : Symbol
		{
			if (hasDynamicArgument)
			{
				foreach (MemberResolutionResult<TMember> memberResolutionResult in results)
				{
					if (memberResolutionResult.Result.IsApplicable)
					{
						return true;
					}
				}
				return false;
			}
			return OverloadResolution.SingleValidResult<TMember>(results);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00044628 File Offset: 0x00042828
		private void PerformMemberOverloadResolution<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, ArrayBuilder<TMember> members, ArrayBuilder<TypeWithAnnotations> typeArguments, BoundExpression receiver, AnalyzedArguments arguments, bool completeResults, bool isMethodGroupConversion, RefKind returnRefKind, TypeSymbol returnType, bool allowRefOmittedArguments, bool isFunctionPointerResolution, in CallingConventionInfo callingConventionInfo, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool inferWithDynamic = false, bool allowUnexpandedForm = true) where TMember : Symbol
		{
			Dictionary<NamedTypeSymbol, ArrayBuilder<TMember>> dictionary = null;
			if (members.Count > 50)
			{
				dictionary = OverloadResolution.PartitionMembersByContainingType<TMember>(members);
			}
			for (int i = 0; i < members.Count; i++)
			{
				this.AddMemberToCandidateSet<TMember>(members[i], results, members, typeArguments, receiver, arguments, completeResults, isMethodGroupConversion, allowRefOmittedArguments, dictionary, inferWithDynamic, ref useSiteInfo, allowUnexpandedForm);
			}
			OverloadResolution.ClearContainingTypeMap<TMember>(ref dictionary);
			this.RemoveInaccessibleTypeArguments<TMember>(results, ref useSiteInfo);
			OverloadResolution.RemoveLessDerivedMembers<TMember>(results, ref useSiteInfo);
			if (this.Compilation.LanguageVersion.AllowImprovedOverloadCandidates())
			{
				this.RemoveStaticInstanceMismatches<TMember>(results, arguments, receiver);
				this.RemoveConstraintViolations<TMember>(results, new CompoundUseSiteInfo<AssemblySymbol>(useSiteInfo));
				if (isMethodGroupConversion)
				{
					this.RemoveDelegateConversionsWithWrongReturnType<TMember>(results, ref useSiteInfo, new RefKind?(returnRefKind), returnType, isFunctionPointerResolution);
				}
			}
			if (isFunctionPointerResolution)
			{
				this.RemoveCallingConventionMismatches<TMember>(results, in callingConventionInfo);
				OverloadResolution.RemoveMethodsNotDeclaredStatic<TMember>(results);
			}
			OverloadResolution.ReportUseSiteInfo<TMember>(results, ref useSiteInfo);
			if (!OverloadResolution.AnyValidResult<TMember>(results))
			{
				return;
			}
			this.RemoveWorseMembers<TMember>(results, arguments, ref useSiteInfo);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0004470C File Offset: 0x0004290C
		internal void FunctionPointerOverloadResolution(ArrayBuilder<FunctionPointerMethodSymbol> funcPtrBuilder, AnalyzedArguments analyzedArguments, OverloadResolutionResult<FunctionPointerMethodSymbol> overloadResolutionResult, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			this.AddMemberToCandidateSet<FunctionPointerMethodSymbol>(funcPtrBuilder[0], overloadResolutionResult.ResultsBuilder, funcPtrBuilder, instance, null, analyzedArguments, true, false, false, null, false, ref useSiteInfo, true);
			OverloadResolution.ReportUseSiteInfo<FunctionPointerMethodSymbol>(overloadResolutionResult.ResultsBuilder, ref useSiteInfo);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0004474C File Offset: 0x0004294C
		private void RemoveStaticInstanceMismatches<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, AnalyzedArguments arguments, BoundExpression receiverOpt) where TMember : Symbol
		{
			if (arguments.IsExtensionMethodInvocation || Binder.IsTypeOrValueExpression(receiverOpt))
			{
				return;
			}
			bool flag = Binder.WasImplicitReceiver(receiverOpt);
			bool flag3;
			bool flag2 = !this._binder.HasThis(!flag, out flag3) || flag3;
			if (flag && !flag2)
			{
				return;
			}
			bool flag4 = (flag && flag2) || Binder.IsMemberAccessedThroughType(receiverOpt);
			OverloadResolution.RemoveStaticInstanceMismatches<TMember>(results, flag4);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000447A8 File Offset: 0x000429A8
		private static void RemoveStaticInstanceMismatches<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, bool requireStatic) where TMember : Symbol
		{
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				TMember member = memberResolutionResult.Member;
				if (memberResolutionResult.Result.IsValid && member.RequiresInstanceReceiver() == requireStatic)
				{
					results[i] = new MemberResolutionResult<TMember>(member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.StaticInstanceMismatch());
				}
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00044810 File Offset: 0x00042A10
		private static void RemoveMethodsNotDeclaredStatic<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results) where TMember : Symbol
		{
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				TMember member = memberResolutionResult.Member;
				if (memberResolutionResult.Result.IsValid && !member.IsStatic)
				{
					results[i] = new MemberResolutionResult<TMember>(member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.StaticInstanceMismatch());
				}
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00044878 File Offset: 0x00042A78
		private void RemoveConstraintViolations<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, CompoundUseSiteInfo<AssemblySymbol> template) where TMember : Symbol
		{
			if (typeof(TMember) != typeof(MethodSymbol))
			{
				return;
			}
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				MethodSymbol methodSymbol = (MethodSymbol)((object)memberResolutionResult.Member);
				ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder;
				if ((memberResolutionResult.Result.IsValid || memberResolutionResult.Result.Kind == MemberResolutionKind.ConstructedParameterFailedConstraintCheck) && this.FailsConstraintChecks(methodSymbol, out arrayBuilder, template))
				{
					results[i] = new MemberResolutionResult<TMember>(memberResolutionResult.Member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.ConstraintFailure(arrayBuilder.ToImmutableAndFree()));
				}
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00044920 File Offset: 0x00042B20
		private void RemoveCallingConventionMismatches<TMember>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<MemberResolutionResult<TMember>> results, in CallingConventionInfo expectedConvention) where TMember : Symbol
		{
			if (typeof(TMember) != typeof(MethodSymbol))
			{
				return;
			}
			if (this._binder.InAttributeArgument || (this._binder.Flags & BinderFlags.InContextualAttributeBinder) != BinderFlags.None)
			{
				return;
			}
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				MethodSymbol methodSymbol = (MethodSymbol)((object)memberResolutionResult.Member);
				if (memberResolutionResult.Result.IsValid)
				{
					UnmanagedCallersOnlyAttributeData unmanagedCallersOnlyAttributeData = methodSymbol.GetUnmanagedCallersOnlyAttributeData(true);
					CallingConvention callingConvention;
					ImmutableHashSet<INamedTypeSymbolInternal> immutableHashSet;
					if (unmanagedCallersOnlyAttributeData == null)
					{
						callingConvention = methodSymbol.CallingConvention;
						immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
					}
					else
					{
						ImmutableHashSet<INamedTypeSymbolInternal> callingConventionTypes = unmanagedCallersOnlyAttributeData.CallingConventionTypes;
						int count = callingConventionTypes.Count;
						if (count != 0)
						{
							if (count == 1)
							{
								string name = Enumerable.Single<INamedTypeSymbolInternal>(callingConventionTypes).Name;
								if (name == "CallConvCdecl")
								{
									callingConvention = CallingConvention.CDecl;
									immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
									goto IL_0143;
								}
								if (name == "CallConvStdcall")
								{
									callingConvention = CallingConvention.Standard;
									immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
									goto IL_0143;
								}
								if (name == "CallConvThiscall")
								{
									callingConvention = CallingConvention.ThisCall;
									immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
									goto IL_0143;
								}
								if (name == "CallConvFastcall")
								{
									callingConvention = CallingConvention.FastCall;
									immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
									goto IL_0143;
								}
							}
							callingConvention = CallingConvention.Unmanaged;
							immutableHashSet = callingConventionTypes;
						}
						else
						{
							callingConvention = CallingConvention.Unmanaged;
							immutableHashSet = ImmutableHashSet<INamedTypeSymbolInternal>.Empty;
						}
					}
					IL_0143:
					if (callingConvention.HasUnknownCallingConventionAttributeBits() || !callingConvention.IsCallingConvention(expectedConvention.CallKind))
					{
						results[i] = OverloadResolution.<RemoveCallingConventionMismatches>g__makeWrongCallingConvention|62_0<TMember>(memberResolutionResult);
					}
					else if (expectedConvention.CallKind.IsCallingConvention(CallingConvention.Unmanaged))
					{
						if (expectedConvention.UnmanagedCallingConventionTypes.Count != immutableHashSet.Count)
						{
							results[i] = OverloadResolution.<RemoveCallingConventionMismatches>g__makeWrongCallingConvention|62_0<TMember>(memberResolutionResult);
						}
						else
						{
							foreach (CustomModifier customModifier in expectedConvention.UnmanagedCallingConventionTypes)
							{
								if (!immutableHashSet.Contains(((CSharpCustomModifier)customModifier).ModifierSymbol))
								{
									results[i] = OverloadResolution.<RemoveCallingConventionMismatches>g__makeWrongCallingConvention|62_0<TMember>(memberResolutionResult);
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00044B44 File Offset: 0x00042D44
		private bool FailsConstraintChecks(MethodSymbol method, out ArrayBuilder<TypeParameterDiagnosticInfo> constraintFailureDiagnosticsOpt, CompoundUseSiteInfo<AssemblySymbol> template)
		{
			if (method.Arity == 0 || method.OriginalDefinition == method)
			{
				constraintFailureDiagnosticsOpt = null;
				return false;
			}
			ArrayBuilder<TypeParameterDiagnosticInfo> instance = ArrayBuilder<TypeParameterDiagnosticInfo>.GetInstance();
			ArrayBuilder<TypeParameterDiagnosticInfo> arrayBuilder = null;
			ConstraintsHelper.CheckConstraintsArgs checkConstraintsArgs = new ConstraintsHelper.CheckConstraintsArgs(this.Compilation, this.Conversions, false, NoLocation.Singleton, null, template);
			if (!ConstraintsHelper.CheckMethodConstraints(method, in checkConstraintsArgs, instance, null, ref arrayBuilder, default(BitVector)))
			{
				if (arrayBuilder != null)
				{
					instance.AddRange(arrayBuilder);
					arrayBuilder.Free();
				}
				constraintFailureDiagnosticsOpt = instance;
				return true;
			}
			instance.Free();
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
			constraintFailureDiagnosticsOpt = null;
			return false;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00044BC8 File Offset: 0x00042DC8
		private void RemoveDelegateConversionsWithWrongReturnType<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, RefKind? returnRefKind, TypeSymbol returnType, bool isFunctionPointerConversion) where TMember : Symbol
		{
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				if (memberResolutionResult.Result.IsValid)
				{
					MethodSymbol methodSymbol = (MethodSymbol)((object)memberResolutionResult.Member);
					bool flag;
					if (returnType == null || methodSymbol.ReturnType.Equals(returnType, TypeCompareKind.AllIgnoreOptions))
					{
						flag = true;
					}
					else
					{
						RefKind? refKind = returnRefKind;
						RefKind refKind2 = RefKind.None;
						if ((refKind.GetValueOrDefault() == refKind2) & (refKind != null))
						{
							flag = this.Conversions.HasIdentityOrImplicitReferenceConversion(methodSymbol.ReturnType, returnType, ref useSiteInfo);
							if (!flag && isFunctionPointerConversion)
							{
								flag = ConversionsBase.HasImplicitPointerToVoidConversion(methodSymbol.ReturnType, returnType) || this.Conversions.HasImplicitPointerConversion(methodSymbol.ReturnType, returnType, ref useSiteInfo);
							}
						}
						else
						{
							flag = false;
						}
					}
					if (!flag)
					{
						results[i] = new MemberResolutionResult<TMember>(memberResolutionResult.Member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.WrongReturnType());
					}
					else
					{
						RefKind refKind3 = methodSymbol.RefKind;
						RefKind? refKind = returnRefKind;
						if (!((refKind3 == refKind.GetValueOrDefault()) & (refKind != null)))
						{
							results[i] = new MemberResolutionResult<TMember>(memberResolutionResult.Member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.WrongRefKind());
						}
					}
				}
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00044CF8 File Offset: 0x00042EF8
		private static Dictionary<NamedTypeSymbol, ArrayBuilder<TMember>> PartitionMembersByContainingType<TMember>(ArrayBuilder<TMember> members) where TMember : Symbol
		{
			Dictionary<NamedTypeSymbol, ArrayBuilder<TMember>> dictionary = new Dictionary<NamedTypeSymbol, ArrayBuilder<TMember>>();
			for (int i = 0; i < members.Count; i++)
			{
				TMember tmember = members[i];
				NamedTypeSymbol containingType = tmember.ContainingType;
				ArrayBuilder<TMember> instance;
				if (!dictionary.TryGetValue(containingType, ref instance))
				{
					instance = ArrayBuilder<TMember>.GetInstance();
					dictionary[containingType] = instance;
				}
				instance.Add(tmember);
			}
			return dictionary;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00044D54 File Offset: 0x00042F54
		private static void ClearContainingTypeMap<TMember>(ref Dictionary<NamedTypeSymbol, ArrayBuilder<TMember>> containingTypeMapOpt) where TMember : Symbol
		{
			if (containingTypeMapOpt != null)
			{
				foreach (ArrayBuilder<TMember> arrayBuilder in containingTypeMapOpt.Values)
				{
					arrayBuilder.Free();
				}
				containingTypeMapOpt = null;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00044DAC File Offset: 0x00042FAC
		private void AddConstructorToCandidateSet(MethodSymbol constructor, ArrayBuilder<MemberResolutionResult<MethodSymbol>> results, AnalyzedArguments arguments, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (constructor.HasUnsupportedMetadata)
			{
				if (completeResults)
				{
					results.Add(new MemberResolutionResult<MethodSymbol>(constructor, constructor, MemberAnalysisResult.UnsupportedMetadata()));
				}
				return;
			}
			MemberAnalysisResult memberAnalysisResult = this.IsConstructorApplicableInNormalForm(constructor, arguments, completeResults, ref useSiteInfo);
			MemberAnalysisResult memberAnalysisResult2 = memberAnalysisResult;
			if (!memberAnalysisResult.IsValid && OverloadResolution.IsValidParams(constructor))
			{
				MemberAnalysisResult memberAnalysisResult3 = this.IsConstructorApplicableInExpandedForm(constructor, arguments, completeResults, ref useSiteInfo);
				if (memberAnalysisResult3.IsValid || completeResults)
				{
					memberAnalysisResult2 = memberAnalysisResult3;
				}
			}
			if (memberAnalysisResult2.IsValid || completeResults || memberAnalysisResult2.HasUseSiteDiagnosticToReportFor(constructor))
			{
				results.Add(new MemberResolutionResult<MethodSymbol>(constructor, constructor, memberAnalysisResult2));
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00044E38 File Offset: 0x00043038
		private MemberAnalysisResult IsConstructorApplicableInNormalForm(MethodSymbol constructor, AnalyzedArguments arguments, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArgumentAnalysisResult argumentAnalysisResult = OverloadResolution.AnalyzeArguments(constructor, arguments, false, false);
			if (!argumentAnalysisResult.IsValid)
			{
				return MemberAnalysisResult.ArgumentParameterMismatch(argumentAnalysisResult);
			}
			if (constructor.HasUseSiteError)
			{
				return MemberAnalysisResult.UseSiteError();
			}
			OverloadResolution.EffectiveParameters effectiveParametersInNormalForm = this.GetEffectiveParametersInNormalForm<MethodSymbol>(constructor, arguments.Arguments.Count, argumentAnalysisResult.ArgsToParamsOpt, arguments.RefKinds, false, false);
			return this.IsApplicable(constructor, effectiveParametersInNormalForm, arguments, argumentAnalysisResult.ArgsToParamsOpt, constructor.IsVararg, false, false, completeResults, ref useSiteInfo);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00044EA8 File Offset: 0x000430A8
		private MemberAnalysisResult IsConstructorApplicableInExpandedForm(MethodSymbol constructor, AnalyzedArguments arguments, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArgumentAnalysisResult argumentAnalysisResult = OverloadResolution.AnalyzeArguments(constructor, arguments, false, true);
			if (!argumentAnalysisResult.IsValid)
			{
				return MemberAnalysisResult.ArgumentParameterMismatch(argumentAnalysisResult);
			}
			if (constructor.HasUseSiteError)
			{
				return MemberAnalysisResult.UseSiteError();
			}
			OverloadResolution.EffectiveParameters effectiveParametersInExpandedForm = this.GetEffectiveParametersInExpandedForm<MethodSymbol>(constructor, arguments.Arguments.Count, argumentAnalysisResult.ArgsToParamsOpt, arguments.RefKinds, false, false);
			MemberAnalysisResult memberAnalysisResult = this.IsApplicable(constructor, effectiveParametersInExpandedForm, arguments, argumentAnalysisResult.ArgsToParamsOpt, false, false, false, completeResults, ref useSiteInfo);
			if (!memberAnalysisResult.IsValid)
			{
				return memberAnalysisResult;
			}
			return MemberAnalysisResult.ExpandedForm(memberAnalysisResult.ArgsToParamsOpt, memberAnalysisResult.ConversionsOpt, false);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00044F34 File Offset: 0x00043134
		private void AddMemberToCandidateSet<TMember>(TMember member, ArrayBuilder<MemberResolutionResult<TMember>> results, ArrayBuilder<TMember> members, ArrayBuilder<TypeWithAnnotations> typeArguments, BoundExpression receiverOpt, AnalyzedArguments arguments, bool completeResults, bool isMethodGroupConversion, bool allowRefOmittedArguments, Dictionary<NamedTypeSymbol, ArrayBuilder<TMember>> containingTypeMapOpt, bool inferWithDynamic, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool allowUnexpandedForm) where TMember : Symbol
		{
			if (members.Count >= 2)
			{
				if (containingTypeMapOpt == null)
				{
					if (OverloadResolution.MemberGroupContainsMoreDerivedOverride<TMember>(members, member, true, ref useSiteInfo))
					{
						return;
					}
					if (OverloadResolution.MemberGroupHidesByName<TMember>(members, member, ref useSiteInfo))
					{
						return;
					}
				}
				else if (containingTypeMapOpt.Count != 1)
				{
					NamedTypeSymbol containingType = member.ContainingType;
					foreach (KeyValuePair<NamedTypeSymbol, ArrayBuilder<TMember>> keyValuePair in containingTypeMapOpt)
					{
						if (keyValuePair.Key.IsDerivedFrom(containingType, TypeCompareKind.ConsiderEverything, ref useSiteInfo))
						{
							ArrayBuilder<TMember> value = keyValuePair.Value;
							if (OverloadResolution.MemberGroupContainsMoreDerivedOverride<TMember>(value, member, false, ref useSiteInfo))
							{
								return;
							}
							if (OverloadResolution.MemberGroupHidesByName<TMember>(value, member, ref useSiteInfo))
							{
								return;
							}
						}
					}
				}
			}
			TMember tmember = (TMember)((object)member.GetLeastOverriddenMember(this._binder.ContainingType));
			if (member.HasUnsupportedMetadata)
			{
				if (completeResults)
				{
					results.Add(new MemberResolutionResult<TMember>(member, tmember, MemberAnalysisResult.UnsupportedMetadata()));
				}
				return;
			}
			MemberResolutionResult<TMember> memberResolutionResult = ((allowUnexpandedForm || !OverloadResolution.IsValidParams(tmember)) ? this.IsMemberApplicableInNormalForm<TMember>(member, tmember, typeArguments, arguments, isMethodGroupConversion, allowRefOmittedArguments, inferWithDynamic, completeResults, ref useSiteInfo) : default(MemberResolutionResult<TMember>));
			MemberResolutionResult<TMember> memberResolutionResult2 = memberResolutionResult;
			if (!memberResolutionResult.Result.IsValid && !isMethodGroupConversion && OverloadResolution.IsValidParams(tmember))
			{
				MemberResolutionResult<TMember> memberResolutionResult3 = this.IsMemberApplicableInExpandedForm<TMember>(member, tmember, typeArguments, arguments, allowRefOmittedArguments, completeResults, ref useSiteInfo);
				if (OverloadResolution.PreferExpandedFormOverNormalForm(memberResolutionResult.Result, memberResolutionResult3.Result))
				{
					memberResolutionResult2 = memberResolutionResult3;
				}
			}
			if (memberResolutionResult2.Result.IsValid || completeResults || memberResolutionResult2.HasUseSiteDiagnosticToReport)
			{
				results.Add(memberResolutionResult2);
				return;
			}
			memberResolutionResult2.Member.AddUseSiteInfo(ref useSiteInfo, false);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00045100 File Offset: 0x00043300
		private static bool PreferExpandedFormOverNormalForm(MemberAnalysisResult normalResult, MemberAnalysisResult expandedResult)
		{
			if (expandedResult.IsValid)
			{
				return true;
			}
			MemberResolutionKind kind = normalResult.Kind;
			if (kind == MemberResolutionKind.NoCorrespondingParameter || kind == MemberResolutionKind.RequiredParameterMissing)
			{
				switch (expandedResult.Kind)
				{
				case MemberResolutionKind.NoCorrespondingNamedParameter:
				case MemberResolutionKind.DuplicateNamedArgument:
				case MemberResolutionKind.NameUsedForPositional:
				case MemberResolutionKind.BadNonTrailingNamedArgument:
				case MemberResolutionKind.UseSiteError:
				case MemberResolutionKind.BadArgumentConversion:
				case MemberResolutionKind.TypeInferenceFailed:
				case MemberResolutionKind.TypeInferenceExtensionInstanceArgument:
				case MemberResolutionKind.ConstructedParameterFailedConstraintCheck:
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00045168 File Offset: 0x00043368
		public static bool IsValidParams(Symbol member)
		{
			if (member.GetIsVararg())
			{
				return false;
			}
			if (member.GetParameterCount() == 0)
			{
				return false;
			}
			ParameterSymbol parameterSymbol = member.GetParameters().Last<ParameterSymbol>();
			return parameterSymbol.IsParams && parameterSymbol.OriginalDefinition.Type.IsSZArray();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000451B0 File Offset: 0x000433B0
		private static bool IsMoreDerivedOverride(Symbol member, Symbol moreDerivedOverride, bool checkOverrideContainingType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return moreDerivedOverride.IsOverride && (!checkOverrideContainingType || moreDerivedOverride.ContainingType.IsDerivedFrom(member.ContainingType, TypeCompareKind.ConsiderEverything, ref useSiteInfo)) && MemberSignatureComparer.SloppyOverrideComparer.Equals(member, moreDerivedOverride) && moreDerivedOverride.GetLeastOverriddenMember(null).OriginalDefinition == member.GetLeastOverriddenMember(null).OriginalDefinition;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0004520C File Offset: 0x0004340C
		private static bool MemberGroupContainsMoreDerivedOverride<TMember>(ArrayBuilder<TMember> members, TMember member, bool checkOverrideContainingType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			if (!member.IsVirtual && !member.IsAbstract && !member.IsOverride)
			{
				return false;
			}
			if (!member.ContainingType.IsClassType())
			{
				return false;
			}
			for (int i = 0; i < members.Count; i++)
			{
				if (OverloadResolution.IsMoreDerivedOverride(member, members[i], checkOverrideContainingType, ref useSiteInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00045288 File Offset: 0x00043488
		private static bool MemberGroupHidesByName<TMember>(ArrayBuilder<TMember> members, TMember member, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			NamedTypeSymbol containingType = member.ContainingType;
			foreach (TMember tmember in members)
			{
				NamedTypeSymbol containingType2 = tmember.ContainingType;
				if (OverloadResolution.HidesByName(tmember) && containingType2.IsDerivedFrom(containingType, TypeCompareKind.ConsiderEverything, ref useSiteInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000452E0 File Offset: 0x000434E0
		private static bool HidesByName(Symbol member)
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Method)
			{
				return ((MethodSymbol)member).HidesBaseMethodsByName;
			}
			if (kind != SymbolKind.Property)
			{
				throw ExceptionUtilities.UnexpectedValue(member.Kind);
			}
			return ((PropertySymbol)member).HidesBasePropertiesByName;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00045328 File Offset: 0x00043528
		private void RemoveInaccessibleTypeArguments<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				if (memberResolutionResult.Result.IsValid && !this.TypeArgumentsAccessible(memberResolutionResult.Member.GetMemberTypeArgumentsNoUseSiteDiagnostics(), ref useSiteInfo))
				{
					results[i] = new MemberResolutionResult<TMember>(memberResolutionResult.Member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.InaccessibleTypeArgument());
				}
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00045398 File Offset: 0x00043598
		private bool TypeArgumentsAccessible(ImmutableArray<TypeSymbol> typeArguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			foreach (TypeSymbol typeSymbol in typeArguments)
			{
				if (!this._binder.IsAccessible(typeSymbol, ref useSiteInfo, null, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000453D4 File Offset: 0x000435D4
		private static void RemoveLessDerivedMembers<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			OverloadResolution.RemoveAllInterfaceMembers<TMember>(results);
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				if ((memberResolutionResult.Result.IsValid || memberResolutionResult.HasUseSiteDiagnosticToReport) && OverloadResolution.IsLessDerivedThanAny<TMember>(memberResolutionResult.LeastOverriddenMember.ContainingType, results, ref useSiteInfo))
				{
					results[i] = new MemberResolutionResult<TMember>(memberResolutionResult.Member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.LessDerived());
				}
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00045454 File Offset: 0x00043654
		private static bool IsLessDerivedThanAny<TMember>(TypeSymbol type, ArrayBuilder<MemberResolutionResult<TMember>> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				if (memberResolutionResult.Result.IsValid)
				{
					NamedTypeSymbol containingType = memberResolutionResult.LeastOverriddenMember.ContainingType;
					if (type.SpecialType == SpecialType.System_Object && containingType.SpecialType != SpecialType.System_Object)
					{
						return true;
					}
					if (containingType.IsInterfaceType() && type.IsInterfaceType() && containingType.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo).Contains((NamedTypeSymbol)type))
					{
						return true;
					}
					if (containingType.IsClassType() && type.IsClassType() && containingType.IsDerivedFrom(type, TypeCompareKind.ConsiderEverything, ref useSiteInfo))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00045500 File Offset: 0x00043700
		private static void RemoveAllInterfaceMembers<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results) where TMember : Symbol
		{
			bool flag = false;
			for (int i = 0; i < results.Count; i++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[i];
				if (memberResolutionResult.Result.IsValid)
				{
					NamedTypeSymbol containingType = memberResolutionResult.LeastOverriddenMember.ContainingType;
					if (containingType.IsClassType() && containingType.GetSpecialTypeSafe() != SpecialType.System_Object)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			for (int j = 0; j < results.Count; j++)
			{
				MemberResolutionResult<TMember> memberResolutionResult2 = results[j];
				if (memberResolutionResult2.Result.IsValid)
				{
					TMember member = memberResolutionResult2.Member;
					if (member.ContainingType.IsInterfaceType())
					{
						results[j] = new MemberResolutionResult<TMember>(member, memberResolutionResult2.LeastOverriddenMember, MemberAnalysisResult.LessDerived());
					}
				}
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000455D0 File Offset: 0x000437D0
		private void PerformObjectCreationOverloadResolution(ArrayBuilder<MemberResolutionResult<MethodSymbol>> results, ImmutableArray<MethodSymbol> constructors, AnalyzedArguments arguments, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			foreach (MethodSymbol methodSymbol in constructors)
			{
				this.AddConstructorToCandidateSet(methodSymbol, results, arguments, completeResults, ref useSiteInfo);
			}
			OverloadResolution.ReportUseSiteInfo<MethodSymbol>(results, ref useSiteInfo);
			this.RemoveWorseMembers<MethodSymbol>(results, arguments, ref useSiteInfo);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00045618 File Offset: 0x00043818
		private static void ReportUseSiteInfo<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			foreach (MemberResolutionResult<TMember> memberResolutionResult in results)
			{
				memberResolutionResult.Member.AddUseSiteInfo(ref useSiteInfo, memberResolutionResult.HasUseSiteDiagnosticToReport);
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00045658 File Offset: 0x00043858
		private int GetTheBestCandidateIndex<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, AnalyzedArguments arguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			int num = -1;
			for (int i = 0; i < results.Count; i++)
			{
				if (results[i].IsValid)
				{
					if (num == -1)
					{
						num = i;
					}
					else if (results[num].Member == results[i].Member)
					{
						num = -1;
					}
					else
					{
						BetterResult betterResult = this.BetterFunctionMember<TMember>(results[num], results[i], arguments.Arguments, ref useSiteInfo);
						if (betterResult == BetterResult.Right)
						{
							num = i;
						}
						else if (betterResult != BetterResult.Left)
						{
							num = -1;
						}
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (results[j].IsValid)
				{
					if (results[num].Member == results[j].Member)
					{
						return -1;
					}
					if (this.BetterFunctionMember<TMember>(results[num], results[j], arguments.Arguments, ref useSiteInfo) != BetterResult.Left)
					{
						return -1;
					}
				}
			}
			return num;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00045768 File Offset: 0x00043968
		private void RemoveWorseMembers<TMember>(ArrayBuilder<MemberResolutionResult<TMember>> results, AnalyzedArguments arguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			if (OverloadResolution.SingleValidResult<TMember>(results))
			{
				return;
			}
			int theBestCandidateIndex = this.GetTheBestCandidateIndex<TMember>(results, arguments, ref useSiteInfo);
			if (theBestCandidateIndex != -1)
			{
				for (int i = 0; i < results.Count; i++)
				{
					if (results[i].IsValid && i != theBestCandidateIndex)
					{
						results[i] = results[i].Worse();
					}
				}
				return;
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance(results.Count, 0);
			int num = 0;
			int num2 = -1;
			for (int j = 0; j < results.Count; j++)
			{
				MemberResolutionResult<TMember> memberResolutionResult = results[j];
				if (memberResolutionResult.IsValid && instance[j] != 1)
				{
					for (int k = 0; k < results.Count; k++)
					{
						MemberResolutionResult<TMember> memberResolutionResult2 = results[k];
						if (memberResolutionResult2.IsValid && j != k && !(memberResolutionResult.Member == memberResolutionResult2.Member))
						{
							BetterResult betterResult = this.BetterFunctionMember<TMember>(memberResolutionResult, memberResolutionResult2, arguments.Arguments, ref useSiteInfo);
							if (betterResult == BetterResult.Left)
							{
								instance[k] = 1;
							}
							else if (betterResult == BetterResult.Right)
							{
								instance[j] = 1;
								break;
							}
						}
					}
					if (instance[j] == 0)
					{
						instance[j] = 2;
						num++;
						num2 = j;
					}
				}
			}
			if (num == 0)
			{
				for (int l = 0; l < instance.Count; l++)
				{
					if (instance[l] == 1)
					{
						results[l] = results[l].Worse();
					}
				}
			}
			else if (num == 1)
			{
				for (int m = 0; m < instance.Count; m++)
				{
					if (instance[m] == 1)
					{
						results[m] = ((this.BetterFunctionMember<TMember>(results[num2], results[m], arguments.Arguments, ref useSiteInfo) == BetterResult.Left) ? results[m].Worst() : results[m].Worse());
					}
				}
				results[num2] = results[num2].Worse();
			}
			else
			{
				for (int n = 0; n < instance.Count; n++)
				{
					if (instance[n] == 1)
					{
						results[n] = results[n].Worst();
					}
					else if (instance[n] == 2)
					{
						results[n] = results[n].Worse();
					}
				}
			}
			instance.Free();
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000459EC File Offset: 0x00043BEC
		private TypeSymbol GetParameterType(ParameterSymbol parameter, MemberAnalysisResult result)
		{
			TypeSymbol type = parameter.Type;
			if (result.Kind == MemberResolutionKind.ApplicableInExpandedForm && parameter.IsParams && type.IsSZArray())
			{
				return ((ArrayTypeSymbol)type).ElementType;
			}
			return type;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00045A28 File Offset: 0x00043C28
		private static ParameterSymbol GetParameter(int argIndex, MemberAnalysisResult result, ImmutableArray<ParameterSymbol> parameters)
		{
			int num = result.ParameterFromArgument(argIndex);
			return parameters[num];
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00045A48 File Offset: 0x00043C48
		private BetterResult BetterFunctionMember<TMember>(MemberResolutionResult<TMember> m1, MemberResolutionResult<TMember> m2, ArrayBuilder<BoundExpression> arguments, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			bool hasAnyRefOmittedArgument = m1.Result.HasAnyRefOmittedArgument;
			bool hasAnyRefOmittedArgument2 = m2.Result.HasAnyRefOmittedArgument;
			if (hasAnyRefOmittedArgument == hasAnyRefOmittedArgument2)
			{
				return this.BetterFunctionMember<TMember>(m1, m2, arguments, hasAnyRefOmittedArgument, ref useSiteInfo);
			}
			if (!hasAnyRefOmittedArgument)
			{
				return BetterResult.Left;
			}
			return BetterResult.Right;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00045A88 File Offset: 0x00043C88
		private BetterResult BetterFunctionMember<TMember>(MemberResolutionResult<TMember> m1, MemberResolutionResult<TMember> m2, ArrayBuilder<BoundExpression> arguments, bool considerRefKinds, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			BetterResult betterResult = BetterResult.Neither;
			bool flag = false;
			bool flag2 = false;
			ImmutableArray<ParameterSymbol> parameters = m1.LeastOverriddenMember.GetParameters();
			ImmutableArray<ParameterSymbol> parameters2 = m2.LeastOverriddenMember.GetParameters();
			bool flag3 = true;
			int i;
			for (i = 0; i < arguments.Count; i++)
			{
				if (arguments[i].Kind != BoundKind.ArgListOperator)
				{
					ParameterSymbol parameter = OverloadResolution.GetParameter(i, m1.Result, parameters);
					TypeSymbol parameterType = this.GetParameterType(parameter, m1.Result);
					ParameterSymbol parameter2 = OverloadResolution.GetParameter(i, m2.Result, parameters2);
					TypeSymbol parameterType2 = this.GetParameterType(parameter2, m2.Result);
					bool flag4;
					BetterResult betterResult2 = this.BetterConversionFromExpression(arguments[i], parameterType, m1.Result.ConversionForArg(i), parameter.RefKind, parameterType2, m2.Result.ConversionForArg(i), parameter2.RefKind, considerRefKinds, ref useSiteInfo, out flag4);
					TypeSymbol typeSymbol = parameterType;
					TypeSymbol typeSymbol2 = parameterType2;
					if (!this._binder.InAttributeArgument)
					{
						typeSymbol = parameterType.NormalizeTaskTypes(this.Compilation);
						typeSymbol2 = parameterType2.NormalizeTaskTypes(this.Compilation);
					}
					if (betterResult2 == BetterResult.Neither)
					{
						if (flag3 && this.Conversions.ClassifyImplicitConversionFromType(typeSymbol, typeSymbol2, ref useSiteInfo).Kind != ConversionKind.Identity)
						{
							flag3 = false;
						}
					}
					else
					{
						if (this.Conversions.ClassifyImplicitConversionFromType(typeSymbol, typeSymbol2, ref useSiteInfo).Kind != ConversionKind.Identity)
						{
							flag3 = false;
						}
						if (betterResult == BetterResult.Neither)
						{
							if (!flag2 || !flag4)
							{
								betterResult = betterResult2;
								flag = flag4;
							}
						}
						else if (betterResult != betterResult2)
						{
							if (flag)
							{
								if (flag4)
								{
									betterResult = BetterResult.Neither;
									flag = false;
									flag2 = true;
								}
								else
								{
									betterResult = betterResult2;
									flag = false;
								}
							}
							else if (!flag4)
							{
								betterResult = BetterResult.Neither;
								break;
							}
						}
						else
						{
							flag = flag && flag4;
						}
					}
				}
			}
			if (betterResult != BetterResult.Neither)
			{
				return betterResult;
			}
			int num;
			int num2;
			OverloadResolution.GetParameterCounts<TMember>(m1, arguments, out num, out num2);
			int num3;
			int num4;
			OverloadResolution.GetParameterCounts<TMember>(m2, arguments, out num3, out num4);
			if (flag3 && num2 == num4)
			{
				for (i++; i < arguments.Count; i++)
				{
					if (arguments[i].Kind != BoundKind.ArgListOperator)
					{
						ParameterSymbol parameter3 = OverloadResolution.GetParameter(i, m1.Result, parameters);
						TypeSymbol parameterType3 = this.GetParameterType(parameter3, m1.Result);
						ParameterSymbol parameter4 = OverloadResolution.GetParameter(i, m2.Result, parameters2);
						TypeSymbol parameterType4 = this.GetParameterType(parameter4, m2.Result);
						TypeSymbol typeSymbol3 = parameterType3;
						TypeSymbol typeSymbol4 = parameterType4;
						if (!this._binder.InAttributeArgument)
						{
							typeSymbol3 = parameterType3.NormalizeTaskTypes(this.Compilation);
							typeSymbol4 = parameterType4.NormalizeTaskTypes(this.Compilation);
						}
						if (this.Conversions.ClassifyImplicitConversionFromType(typeSymbol3, typeSymbol4, ref useSiteInfo).Kind != ConversionKind.Identity)
						{
							flag3 = false;
							break;
						}
					}
				}
			}
			if (!flag3 || num2 != num4)
			{
				if (num2 != num4)
				{
					if (m1.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm)
					{
						if (m2.Result.Kind != MemberResolutionKind.ApplicableInExpandedForm)
						{
							return BetterResult.Right;
						}
					}
					else if (m2.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm)
					{
						return BetterResult.Left;
					}
					if (num2 == arguments.Count)
					{
						return BetterResult.Left;
					}
					if (num4 == arguments.Count)
					{
						return BetterResult.Right;
					}
				}
				return OverloadResolution.PreferValOverInParameters<TMember>(arguments, m1, parameters, m2, parameters2);
			}
			if (m1.Member.GetMemberArity() == 0)
			{
				if (m2.Member.GetMemberArity() > 0)
				{
					return BetterResult.Left;
				}
			}
			else if (m2.Member.GetMemberArity() == 0)
			{
				return BetterResult.Right;
			}
			if (m1.Result.Kind == MemberResolutionKind.ApplicableInNormalForm && m2.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm)
			{
				return BetterResult.Left;
			}
			if (m1.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm && m2.Result.Kind == MemberResolutionKind.ApplicableInNormalForm)
			{
				return BetterResult.Right;
			}
			if (m1.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm && m2.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm)
			{
				if (num > num3)
				{
					return BetterResult.Left;
				}
				if (num < num3)
				{
					return BetterResult.Right;
				}
			}
			bool flag5 = m1.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm || num == arguments.Count;
			bool flag6 = m2.Result.Kind == MemberResolutionKind.ApplicableInExpandedForm || num3 == arguments.Count;
			if (flag5 && !flag6)
			{
				return BetterResult.Left;
			}
			if (!flag5 && flag6)
			{
				return BetterResult.Right;
			}
			ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance();
			ArrayBuilder<TypeSymbol> instance2 = ArrayBuilder<TypeSymbol>.GetInstance();
			ImmutableArray<ParameterSymbol> parameters3 = m1.LeastOverriddenMember.OriginalDefinition.GetParameters();
			ImmutableArray<ParameterSymbol> parameters4 = m2.LeastOverriddenMember.OriginalDefinition.GetParameters();
			for (i = 0; i < arguments.Count; i++)
			{
				if (arguments[i].Kind != BoundKind.ArgListOperator)
				{
					ParameterSymbol parameter5 = OverloadResolution.GetParameter(i, m1.Result, parameters3);
					instance.Add(this.GetParameterType(parameter5, m1.Result));
					ParameterSymbol parameter6 = OverloadResolution.GetParameter(i, m2.Result, parameters4);
					instance2.Add(this.GetParameterType(parameter6, m2.Result));
				}
			}
			betterResult = OverloadResolution.MoreSpecificType(instance, instance2, ref useSiteInfo);
			instance.Free();
			instance2.Free();
			if (betterResult != BetterResult.Neither)
			{
				return betterResult;
			}
			if (m1.Member.ContainingType.TypeKind == TypeKind.Submission && m2.Member.ContainingType.TypeKind == TypeKind.Submission)
			{
				Compilation declaringCompilation = m1.Member.DeclaringCompilation;
				CSharpCompilation declaringCompilation2 = m2.Member.DeclaringCompilation;
				int submissionSlotIndex = declaringCompilation.GetSubmissionSlotIndex();
				int submissionSlotIndex2 = declaringCompilation2.GetSubmissionSlotIndex();
				if (submissionSlotIndex > submissionSlotIndex2)
				{
					return BetterResult.Left;
				}
				if (submissionSlotIndex < submissionSlotIndex2)
				{
					return BetterResult.Right;
				}
			}
			int num5 = m1.LeastOverriddenMember.CustomModifierCount();
			int num6 = m2.LeastOverriddenMember.CustomModifierCount();
			if (num5 == num6)
			{
				return OverloadResolution.PreferValOverInParameters<TMember>(arguments, m1, parameters, m2, parameters2);
			}
			if (num5 >= num6)
			{
				return BetterResult.Right;
			}
			return BetterResult.Left;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00046024 File Offset: 0x00044224
		private static BetterResult PreferValOverInParameters<TMember>(ArrayBuilder<BoundExpression> arguments, MemberResolutionResult<TMember> m1, ImmutableArray<ParameterSymbol> parameters1, MemberResolutionResult<TMember> m2, ImmutableArray<ParameterSymbol> parameters2) where TMember : Symbol
		{
			BetterResult betterResult = BetterResult.Neither;
			for (int i = 0; i < arguments.Count; i++)
			{
				if (arguments[i].Kind != BoundKind.ArgListOperator)
				{
					ParameterSymbol parameter = OverloadResolution.GetParameter(i, m1.Result, parameters1);
					ParameterSymbol parameter2 = OverloadResolution.GetParameter(i, m2.Result, parameters2);
					if (parameter.RefKind == RefKind.None && parameter2.RefKind == RefKind.In)
					{
						if (betterResult == BetterResult.Right)
						{
							return BetterResult.Neither;
						}
						betterResult = BetterResult.Left;
					}
					else if (parameter2.RefKind == RefKind.None && parameter.RefKind == RefKind.In)
					{
						if (betterResult == BetterResult.Left)
						{
							return BetterResult.Neither;
						}
						betterResult = BetterResult.Right;
					}
				}
			}
			return betterResult;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000460A8 File Offset: 0x000442A8
		private static void GetParameterCounts<TMember>(MemberResolutionResult<TMember> m, ArrayBuilder<BoundExpression> arguments, out int declaredParameterCount, out int parametersUsedIncludingExpansionAndOptional) where TMember : Symbol
		{
			declaredParameterCount = m.Member.GetParameterCount();
			if (m.Result.Kind != MemberResolutionKind.ApplicableInExpandedForm)
			{
				parametersUsedIncludingExpansionAndOptional = declaredParameterCount;
				return;
			}
			if (arguments.Count >= declaredParameterCount)
			{
				parametersUsedIncludingExpansionAndOptional = arguments.Count;
				return;
			}
			ImmutableArray<int> argsToParamsOpt = m.Result.ArgsToParamsOpt;
			if (argsToParamsOpt.IsDefaultOrEmpty || !argsToParamsOpt.Contains(declaredParameterCount - 1))
			{
				parametersUsedIncludingExpansionAndOptional = declaredParameterCount - 1;
				return;
			}
			parametersUsedIncludingExpansionAndOptional = declaredParameterCount;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00046120 File Offset: 0x00044320
		private static BetterResult MoreSpecificType(ArrayBuilder<TypeSymbol> t1, ArrayBuilder<TypeSymbol> t2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			BetterResult betterResult = BetterResult.Neither;
			for (int i = 0; i < t1.Count; i++)
			{
				BetterResult betterResult2 = OverloadResolution.MoreSpecificType(t1[i], t2[i], ref useSiteInfo);
				if (betterResult2 != BetterResult.Neither)
				{
					if (betterResult == BetterResult.Neither)
					{
						betterResult = betterResult2;
					}
					else if (betterResult != betterResult2)
					{
						return BetterResult.Neither;
					}
				}
			}
			return betterResult;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00046168 File Offset: 0x00044368
		private static BetterResult MoreSpecificType(TypeSymbol t1, TypeSymbol t2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag = t1.IsTypeParameter();
			bool flag2 = t2.IsTypeParameter();
			if (flag && !flag2)
			{
				return BetterResult.Right;
			}
			if (!flag && flag2)
			{
				return BetterResult.Left;
			}
			if (flag && flag2)
			{
				return BetterResult.Neither;
			}
			if (t1.IsArray())
			{
				ArrayTypeSymbol arrayTypeSymbol = (ArrayTypeSymbol)t1;
				ArrayTypeSymbol arrayTypeSymbol2 = (ArrayTypeSymbol)t2;
				return OverloadResolution.MoreSpecificType(arrayTypeSymbol.ElementType, arrayTypeSymbol2.ElementType, ref useSiteInfo);
			}
			if (t1.TypeKind == TypeKind.Pointer)
			{
				PointerTypeSymbol pointerTypeSymbol = (PointerTypeSymbol)t1;
				PointerTypeSymbol pointerTypeSymbol2 = (PointerTypeSymbol)t2;
				return OverloadResolution.MoreSpecificType(pointerTypeSymbol.PointedAtType, pointerTypeSymbol2.PointedAtType, ref useSiteInfo);
			}
			if (t1.IsDynamic() || t2.IsDynamic())
			{
				return BetterResult.Neither;
			}
			NamedTypeSymbol namedTypeSymbol = t1 as NamedTypeSymbol;
			NamedTypeSymbol namedTypeSymbol2 = t2 as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return BetterResult.Neither;
			}
			ArrayBuilder<TypeSymbol> instance = ArrayBuilder<TypeSymbol>.GetInstance();
			ArrayBuilder<TypeSymbol> instance2 = ArrayBuilder<TypeSymbol>.GetInstance();
			namedTypeSymbol.GetAllTypeArguments(instance, ref useSiteInfo);
			namedTypeSymbol2.GetAllTypeArguments(instance2, ref useSiteInfo);
			BetterResult betterResult = OverloadResolution.MoreSpecificType(instance, instance2, ref useSiteInfo);
			instance.Free();
			instance2.Free();
			return betterResult;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00046250 File Offset: 0x00044450
		private BetterResult BetterConversionFromExpression(BoundExpression node, TypeSymbol t1, TypeSymbol t2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag;
			return this.BetterConversionFromExpression(node, t1, this.Conversions.ClassifyImplicitConversionFromExpression(node, t1, ref useSiteInfo), t2, this.Conversions.ClassifyImplicitConversionFromExpression(node, t2, ref useSiteInfo), ref useSiteInfo, out flag);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00046288 File Offset: 0x00044488
		private BetterResult BetterConversionFromExpression(BoundExpression node, TypeSymbol t1, Conversion conv1, RefKind refKind1, TypeSymbol t2, Conversion conv2, RefKind refKind2, bool considerRefKinds, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out bool okToDowngradeToNeither)
		{
			okToDowngradeToNeither = false;
			if (considerRefKinds)
			{
				if (refKind1 != refKind2)
				{
					if (refKind1 == RefKind.None)
					{
						if (conv1.Kind != ConversionKind.Identity)
						{
							return BetterResult.Neither;
						}
						return BetterResult.Left;
					}
					else
					{
						if (conv2.Kind != ConversionKind.Identity)
						{
							return BetterResult.Neither;
						}
						return BetterResult.Right;
					}
				}
				else if (refKind1 == RefKind.Ref)
				{
					return BetterResult.Neither;
				}
			}
			return this.BetterConversionFromExpression(node, t1, conv1, t2, conv2, ref useSiteInfo, out okToDowngradeToNeither);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000462DC File Offset: 0x000444DC
		private BetterResult BetterConversionFromExpression(BoundExpression node, TypeSymbol t1, Conversion conv1, TypeSymbol t2, Conversion conv2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out bool okToDowngradeToNeither)
		{
			okToDowngradeToNeither = false;
			if (ConversionsBase.HasIdentityConversion(t1, t2))
			{
				return BetterResult.Neither;
			}
			UnboundLambda unboundLambda = node as UnboundLambda;
			BoundKind kind = node.Kind;
			if (kind == BoundKind.OutVariablePendingInference || kind == BoundKind.OutDeconstructVarPendingInference || (kind == BoundKind.DiscardExpression && !node.HasExpressionType()))
			{
				okToDowngradeToNeither = false;
				return BetterResult.Neither;
			}
			bool flag = this.ExpressionMatchExactly(node, t1, ref useSiteInfo);
			bool flag2 = this.ExpressionMatchExactly(node, t2, ref useSiteInfo);
			if (flag)
			{
				if (!flag2)
				{
					okToDowngradeToNeither = unboundLambda != null && this.CanDowngradeConversionFromLambdaToNeither(BetterResult.Left, unboundLambda, t1, t2, ref useSiteInfo, false);
					return BetterResult.Left;
				}
			}
			else if (flag2)
			{
				okToDowngradeToNeither = unboundLambda != null && this.CanDowngradeConversionFromLambdaToNeither(BetterResult.Right, unboundLambda, t1, t2, ref useSiteInfo, false);
				return BetterResult.Right;
			}
			if (!conv1.IsConditionalExpression && conv2.IsConditionalExpression)
			{
				return BetterResult.Left;
			}
			if (!conv2.IsConditionalExpression && conv1.IsConditionalExpression)
			{
				return BetterResult.Right;
			}
			return this.BetterConversionTarget(node, t1, conv1, t2, conv2, ref useSiteInfo, out okToDowngradeToNeither);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000463B8 File Offset: 0x000445B8
		private bool ExpressionMatchExactly(BoundExpression node, TypeSymbol t, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (node.Type != null && ConversionsBase.HasIdentityConversion(node.Type, t))
			{
				return true;
			}
			if (node.Kind == BoundKind.TupleLiteral)
			{
				return this.ExpressionMatchExactly((BoundTupleLiteral)node, t, ref useSiteInfo);
			}
			NamedTypeSymbol delegateType;
			MethodSymbol delegateInvokeMethod;
			TypeSymbol typeSymbol;
			if (node.Kind == BoundKind.UnboundLambda && (delegateType = t.GetDelegateType()) != null && (delegateInvokeMethod = delegateType.DelegateInvokeMethod) != null && !(typeSymbol = delegateInvokeMethod.ReturnType).IsVoidType())
			{
				BoundLambda boundLambda = ((UnboundLambda)node).BindForReturnTypeInference(delegateType);
				TypeWithAnnotations inferredReturnType = boundLambda.GetInferredReturnType(ref useSiteInfo);
				if (inferredReturnType.HasType && ConversionsBase.HasIdentityConversion(inferredReturnType.Type, typeSymbol))
				{
					return true;
				}
				if (boundLambda.Symbol.IsAsync)
				{
					if (typeSymbol.OriginalDefinition.IsGenericTaskType(this.Compilation))
					{
						typeSymbol = ((NamedTypeSymbol)typeSymbol).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type;
					}
					else
					{
						typeSymbol = null;
					}
				}
				if (typeSymbol != null)
				{
					int length = boundLambda.Body.Statements.Length;
					if (length != 0)
					{
						if (length == 1 && boundLambda.Body.Statements[0].Kind == BoundKind.ReturnStatement)
						{
							BoundReturnStatement boundReturnStatement = (BoundReturnStatement)boundLambda.Body.Statements[0];
							if (boundReturnStatement.ExpressionOpt != null && this.ExpressionMatchExactly(boundReturnStatement.ExpressionOpt, typeSymbol, ref useSiteInfo))
							{
								return true;
							}
						}
						else
						{
							ArrayBuilder<BoundReturnStatement> instance = ArrayBuilder<BoundReturnStatement>.GetInstance();
							new OverloadResolution.ReturnStatements(instance).Visit(boundLambda.Body);
							bool flag = false;
							foreach (BoundReturnStatement boundReturnStatement2 in instance)
							{
								if (boundReturnStatement2.ExpressionOpt == null || !this.ExpressionMatchExactly(boundReturnStatement2.ExpressionOpt, typeSymbol, ref useSiteInfo))
								{
									flag = false;
									break;
								}
								flag = true;
							}
							instance.Free();
							if (flag)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00046590 File Offset: 0x00044790
		private bool ExpressionMatchExactly(BoundTupleLiteral tupleSource, TypeSymbol targetType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (targetType.Kind != SymbolKind.NamedType)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)targetType;
			ImmutableArray<BoundExpression> arguments = tupleSource.Arguments;
			if (!namedTypeSymbol.IsTupleTypeOfCardinality(arguments.Length))
			{
				return false;
			}
			ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = namedTypeSymbol.TupleElementTypesWithAnnotations;
			for (int i = 0; i < arguments.Length; i++)
			{
				if (!this.ExpressionMatchExactly(arguments[i], tupleElementTypesWithAnnotations[i].Type, ref useSiteInfo))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00046608 File Offset: 0x00044808
		private BetterResult BetterConversionTarget(TypeSymbol type1, TypeSymbol type2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			bool flag;
			return this.BetterConversionTargetCore(null, type1, default(Conversion), type2, default(Conversion), ref useSiteInfo, out flag, 100);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00046638 File Offset: 0x00044838
		private BetterResult BetterConversionTargetCore(TypeSymbol type1, TypeSymbol type2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, int betterConversionTargetRecursionLimit)
		{
			if (betterConversionTargetRecursionLimit < 0)
			{
				return BetterResult.Neither;
			}
			bool flag;
			return this.BetterConversionTargetCore(null, type1, default(Conversion), type2, default(Conversion), ref useSiteInfo, out flag, betterConversionTargetRecursionLimit - 1);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00046670 File Offset: 0x00044870
		private BetterResult BetterConversionTarget(BoundExpression node, TypeSymbol type1, Conversion conv1, TypeSymbol type2, Conversion conv2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out bool okToDowngradeToNeither)
		{
			return this.BetterConversionTargetCore(node, type1, conv1, type2, conv2, ref useSiteInfo, out okToDowngradeToNeither, 100);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00046690 File Offset: 0x00044890
		private BetterResult BetterConversionTargetCore(BoundExpression node, TypeSymbol type1, Conversion conv1, TypeSymbol type2, Conversion conv2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, out bool okToDowngradeToNeither, int betterConversionTargetRecursionLimit)
		{
			okToDowngradeToNeither = false;
			if (ConversionsBase.HasIdentityConversion(type1, type2))
			{
				return BetterResult.Neither;
			}
			bool isImplicit = this.Conversions.ClassifyImplicitConversionFromType(type1, type2, ref useSiteInfo).IsImplicit;
			bool isImplicit2 = this.Conversions.ClassifyImplicitConversionFromType(type2, type1, ref useSiteInfo).IsImplicit;
			UnboundLambda unboundLambda = node as UnboundLambda;
			if (isImplicit)
			{
				if (isImplicit2)
				{
					return BetterResult.Neither;
				}
				okToDowngradeToNeither = unboundLambda != null && this.CanDowngradeConversionFromLambdaToNeither(BetterResult.Left, unboundLambda, type1, type2, ref useSiteInfo, true);
				return BetterResult.Left;
			}
			else
			{
				if (isImplicit2)
				{
					okToDowngradeToNeither = unboundLambda != null && this.CanDowngradeConversionFromLambdaToNeither(BetterResult.Right, unboundLambda, type1, type2, ref useSiteInfo, true);
					return BetterResult.Right;
				}
				bool flag = type1.OriginalDefinition.IsGenericTaskType(this.Compilation);
				bool flag2 = type2.OriginalDefinition.IsGenericTaskType(this.Compilation);
				if (flag)
				{
					if (flag2)
					{
						return this.BetterConversionTargetCore(((NamedTypeSymbol)type1).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type, ((NamedTypeSymbol)type2).TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type, ref useSiteInfo, betterConversionTargetRecursionLimit);
					}
					return BetterResult.Neither;
				}
				else
				{
					if (flag2)
					{
						return BetterResult.Neither;
					}
					NamedTypeSymbol delegateType;
					if ((delegateType = type1.GetDelegateType()) != null)
					{
						NamedTypeSymbol delegateType2;
						if ((delegateType2 = type2.GetDelegateType()) != null)
						{
							MethodSymbol delegateInvokeMethod = delegateType.DelegateInvokeMethod;
							MethodSymbol delegateInvokeMethod2 = delegateType2.DelegateInvokeMethod;
							if (delegateInvokeMethod != null && delegateInvokeMethod2 != null)
							{
								TypeSymbol returnType = delegateInvokeMethod.ReturnType;
								TypeSymbol returnType2 = delegateInvokeMethod2.ReturnType;
								BetterResult betterResult = BetterResult.Neither;
								if (!returnType.IsVoidType())
								{
									if (returnType2.IsVoidType())
									{
										betterResult = BetterResult.Left;
									}
								}
								else if (!returnType2.IsVoidType())
								{
									betterResult = BetterResult.Right;
								}
								if (betterResult == BetterResult.Neither)
								{
									betterResult = this.BetterConversionTargetCore(returnType, returnType2, ref useSiteInfo, betterConversionTargetRecursionLimit);
								}
								if (node != null && node.Kind == BoundKind.MethodGroup)
								{
									BoundMethodGroup boundMethodGroup = (BoundMethodGroup)node;
									if (betterResult == BetterResult.Left)
									{
										if (this.IsMethodGroupConversionIncompatibleWithDelegate(boundMethodGroup, delegateType, conv1))
										{
											return BetterResult.Neither;
										}
									}
									else if (betterResult == BetterResult.Right && this.IsMethodGroupConversionIncompatibleWithDelegate(boundMethodGroup, delegateType2, conv2))
									{
										return BetterResult.Neither;
									}
								}
								return betterResult;
							}
						}
						return BetterResult.Neither;
					}
					if (type2.GetDelegateType() != null)
					{
						return BetterResult.Neither;
					}
					if (OverloadResolution.IsSignedIntegralType(type1))
					{
						if (OverloadResolution.IsUnsignedIntegralType(type2))
						{
							return BetterResult.Left;
						}
					}
					else if (OverloadResolution.IsUnsignedIntegralType(type1) && OverloadResolution.IsSignedIntegralType(type2))
					{
						return BetterResult.Right;
					}
					return BetterResult.Neither;
				}
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00046899 File Offset: 0x00044A99
		private bool IsMethodGroupConversionIncompatibleWithDelegate(BoundMethodGroup node, NamedTypeSymbol delegateType, Conversion conv)
		{
			return conv.IsMethodGroup && !this._binder.MethodIsCompatibleWithDelegateOrFunctionPointer(node.ReceiverOpt, conv.IsExtensionMethod, conv.Method, delegateType, Location.None, BindingDiagnosticBag.Discarded);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000468D4 File Offset: 0x00044AD4
		private bool CanDowngradeConversionFromLambdaToNeither(BetterResult currentResult, UnboundLambda lambda, TypeSymbol type1, TypeSymbol type2, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool fromTypeAnalysis)
		{
			NamedTypeSymbol delegateType;
			NamedTypeSymbol delegateType2;
			if ((delegateType = type1.GetDelegateType()) != null && (delegateType2 = type2.GetDelegateType()) != null)
			{
				MethodSymbol delegateInvokeMethod = delegateType.DelegateInvokeMethod;
				MethodSymbol delegateInvokeMethod2 = delegateType2.DelegateInvokeMethod;
				if (delegateInvokeMethod != null && delegateInvokeMethod2 != null)
				{
					if (!OverloadResolution.IdenticalParameters(delegateInvokeMethod.Parameters, delegateInvokeMethod2.Parameters))
					{
						return true;
					}
					TypeSymbol returnType = delegateInvokeMethod.ReturnType;
					TypeSymbol returnType2 = delegateInvokeMethod2.ReturnType;
					if (returnType.IsVoidType())
					{
						return returnType2.IsVoidType();
					}
					if (returnType2.IsVoidType())
					{
						return false;
					}
					if (ConversionsBase.HasIdentityConversion(returnType, returnType2))
					{
						return true;
					}
					if (!lambda.InferReturnType(this.Conversions, delegateType, ref useSiteInfo).HasType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00046980 File Offset: 0x00044B80
		private static bool IdenticalParameters(ImmutableArray<ParameterSymbol> p1, ImmutableArray<ParameterSymbol> p2)
		{
			if (p1.IsDefault || p2.IsDefault)
			{
				return false;
			}
			if (p1.Length != p2.Length)
			{
				return false;
			}
			for (int i = 0; i < p1.Length; i++)
			{
				ParameterSymbol parameterSymbol = p1[i];
				ParameterSymbol parameterSymbol2 = p2[i];
				if (parameterSymbol.RefKind != parameterSymbol2.RefKind)
				{
					return false;
				}
				if (!ConversionsBase.HasIdentityConversion(parameterSymbol.Type, parameterSymbol2.Type))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00046A00 File Offset: 0x00044C00
		private static bool IsSignedIntegralType(TypeSymbol type)
		{
			if (type != null && type.IsNullableType())
			{
				type = type.GetNullableUnderlyingType();
			}
			SpecialType specialTypeSafe = type.GetSpecialTypeSafe();
			switch (specialTypeSafe)
			{
			case SpecialType.System_SByte:
			case SpecialType.System_Int16:
			case SpecialType.System_Int32:
			case SpecialType.System_Int64:
				break;
			case SpecialType.System_Byte:
			case SpecialType.System_UInt16:
			case SpecialType.System_UInt32:
				return false;
			default:
				if (specialTypeSafe != SpecialType.System_IntPtr)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00046A54 File Offset: 0x00044C54
		private static bool IsUnsignedIntegralType(TypeSymbol type)
		{
			if (type != null && type.IsNullableType())
			{
				type = type.GetNullableUnderlyingType();
			}
			SpecialType specialTypeSafe = type.GetSpecialTypeSafe();
			switch (specialTypeSafe)
			{
			case SpecialType.System_Byte:
			case SpecialType.System_UInt16:
			case SpecialType.System_UInt32:
			case SpecialType.System_UInt64:
				break;
			case SpecialType.System_Int16:
			case SpecialType.System_Int32:
			case SpecialType.System_Int64:
				return false;
			default:
				if (specialTypeSafe != SpecialType.System_UIntPtr)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00046AA8 File Offset: 0x00044CA8
		internal static void GetEffectiveParameterTypes(MethodSymbol method, int argumentCount, ImmutableArray<int> argToParamMap, ArrayBuilder<RefKind> argumentRefKinds, bool isMethodGroupConversion, bool allowRefOmittedArguments, Binder binder, bool expanded, out ImmutableArray<TypeWithAnnotations> parameterTypes, out ImmutableArray<RefKind> parameterRefKinds)
		{
			bool flag;
			OverloadResolution.EffectiveParameters effectiveParameters = (expanded ? OverloadResolution.GetEffectiveParametersInExpandedForm<MethodSymbol>(method, argumentCount, argToParamMap, argumentRefKinds, isMethodGroupConversion, allowRefOmittedArguments, binder, out flag) : OverloadResolution.GetEffectiveParametersInNormalForm<MethodSymbol>(method, argumentCount, argToParamMap, argumentRefKinds, isMethodGroupConversion, allowRefOmittedArguments, binder, out flag));
			parameterTypes = effectiveParameters.ParameterTypes;
			parameterRefKinds = effectiveParameters.ParameterRefKinds;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00046AF8 File Offset: 0x00044CF8
		private OverloadResolution.EffectiveParameters GetEffectiveParametersInNormalForm<TMember>(TMember member, int argumentCount, ImmutableArray<int> argToParamMap, ArrayBuilder<RefKind> argumentRefKinds, bool isMethodGroupConversion, bool allowRefOmittedArguments) where TMember : Symbol
		{
			bool flag;
			return OverloadResolution.GetEffectiveParametersInNormalForm<TMember>(member, argumentCount, argToParamMap, argumentRefKinds, isMethodGroupConversion, allowRefOmittedArguments, this._binder, out flag);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00046B1C File Offset: 0x00044D1C
		private static OverloadResolution.EffectiveParameters GetEffectiveParametersInNormalForm<TMember>(TMember member, int argumentCount, ImmutableArray<int> argToParamMap, ArrayBuilder<RefKind> argumentRefKinds, bool isMethodGroupConversion, bool allowRefOmittedArguments, Binder binder, out bool hasAnyRefOmittedArgument) where TMember : Symbol
		{
			hasAnyRefOmittedArgument = false;
			ImmutableArray<ParameterSymbol> parameters = member.GetParameters();
			int num = member.GetParameterCount() + (member.GetIsVararg() ? 1 : 0);
			if (argumentCount == num && argToParamMap.IsDefaultOrEmpty)
			{
				ImmutableArray<RefKind> parameterRefKinds = member.GetParameterRefKinds();
				if (parameterRefKinds.IsDefaultOrEmpty)
				{
					return new OverloadResolution.EffectiveParameters(member.GetParameterTypes(), parameterRefKinds);
				}
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<RefKind> arrayBuilder = null;
			bool flag = argumentRefKinds.Any();
			for (int i = 0; i < argumentCount; i++)
			{
				int num2 = (argToParamMap.IsDefault ? i : argToParamMap[i]);
				if (num2 < parameters.Length)
				{
					ParameterSymbol parameterSymbol = parameters[num2];
					instance.Add(parameterSymbol.TypeWithAnnotations);
					RefKind refKind = (flag ? argumentRefKinds[i] : RefKind.None);
					RefKind effectiveParameterRefKind = OverloadResolution.GetEffectiveParameterRefKind(parameterSymbol, refKind, isMethodGroupConversion, allowRefOmittedArguments, binder, ref hasAnyRefOmittedArgument);
					if (arrayBuilder == null)
					{
						if (effectiveParameterRefKind != RefKind.None)
						{
							arrayBuilder = ArrayBuilder<RefKind>.GetInstance(i, RefKind.None);
							arrayBuilder.Add(effectiveParameterRefKind);
						}
					}
					else
					{
						arrayBuilder.Add(effectiveParameterRefKind);
					}
				}
			}
			ImmutableArray<RefKind> immutableArray = ((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : default(ImmutableArray<RefKind>));
			return new OverloadResolution.EffectiveParameters(instance.ToImmutableAndFree(), immutableArray);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00046C58 File Offset: 0x00044E58
		private static RefKind GetEffectiveParameterRefKind(ParameterSymbol parameter, RefKind argRefKind, bool isMethodGroupConversion, bool allowRefOmittedArguments, Binder binder, ref bool hasAnyRefOmittedArgument)
		{
			RefKind refKind = parameter.RefKind;
			if (!isMethodGroupConversion && argRefKind == RefKind.None && refKind == RefKind.In)
			{
				return RefKind.None;
			}
			if (allowRefOmittedArguments && refKind == RefKind.Ref && argRefKind == RefKind.None && !binder.InAttributeArgument)
			{
				hasAnyRefOmittedArgument = true;
				return RefKind.None;
			}
			return refKind;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00046C94 File Offset: 0x00044E94
		private OverloadResolution.EffectiveParameters GetEffectiveParametersInExpandedForm<TMember>(TMember member, int argumentCount, ImmutableArray<int> argToParamMap, ArrayBuilder<RefKind> argumentRefKinds, bool isMethodGroupConversion, bool allowRefOmittedArguments) where TMember : Symbol
		{
			bool flag;
			return OverloadResolution.GetEffectiveParametersInExpandedForm<TMember>(member, argumentCount, argToParamMap, argumentRefKinds, isMethodGroupConversion, allowRefOmittedArguments, this._binder, out flag);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00046CB8 File Offset: 0x00044EB8
		private static OverloadResolution.EffectiveParameters GetEffectiveParametersInExpandedForm<TMember>(TMember member, int argumentCount, ImmutableArray<int> argToParamMap, ArrayBuilder<RefKind> argumentRefKinds, bool isMethodGroupConversion, bool allowRefOmittedArguments, Binder binder, out bool hasAnyRefOmittedArgument) where TMember : Symbol
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<RefKind> instance2 = ArrayBuilder<RefKind>.GetInstance();
			bool flag = false;
			ImmutableArray<ParameterSymbol> parameters = member.GetParameters();
			bool flag2 = argumentRefKinds.Any();
			hasAnyRefOmittedArgument = false;
			for (int i = 0; i < argumentCount; i++)
			{
				int num = (argToParamMap.IsDefault ? i : argToParamMap[i]);
				ParameterSymbol parameterSymbol = parameters[num];
				TypeWithAnnotations typeWithAnnotations = parameterSymbol.TypeWithAnnotations;
				instance.Add((num == parameters.Length - 1) ? ((ArrayTypeSymbol)typeWithAnnotations.Type).ElementTypeWithAnnotations : typeWithAnnotations);
				RefKind refKind = (flag2 ? argumentRefKinds[i] : RefKind.None);
				RefKind effectiveParameterRefKind = OverloadResolution.GetEffectiveParameterRefKind(parameterSymbol, refKind, isMethodGroupConversion, allowRefOmittedArguments, binder, ref hasAnyRefOmittedArgument);
				instance2.Add(effectiveParameterRefKind);
				if (effectiveParameterRefKind != RefKind.None)
				{
					flag = true;
				}
			}
			ImmutableArray<RefKind> immutableArray = (flag ? instance2.ToImmutable() : default(ImmutableArray<RefKind>));
			instance2.Free();
			return new OverloadResolution.EffectiveParameters(instance.ToImmutableAndFree(), immutableArray);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00046DAC File Offset: 0x00044FAC
		private MemberResolutionResult<TMember> IsMemberApplicableInNormalForm<TMember>(TMember member, TMember leastOverriddenMember, ArrayBuilder<TypeWithAnnotations> typeArguments, AnalyzedArguments arguments, bool isMethodGroupConversion, bool allowRefOmittedArguments, bool inferWithDynamic, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			ArgumentAnalysisResult argumentAnalysisResult = OverloadResolution.AnalyzeArguments(member, arguments, isMethodGroupConversion, false);
			if (!argumentAnalysisResult.IsValid)
			{
				ArgumentAnalysisResultKind kind = argumentAnalysisResult.Kind;
				if ((kind != ArgumentAnalysisResultKind.NoCorrespondingParameter && kind - ArgumentAnalysisResultKind.DuplicateNamedArgument > 1) || !completeResults)
				{
					return new MemberResolutionResult<TMember>(member, leastOverriddenMember, MemberAnalysisResult.ArgumentParameterMismatch(argumentAnalysisResult));
				}
			}
			if (member.HasUseSiteError)
			{
				return new MemberResolutionResult<TMember>(member, leastOverriddenMember, MemberAnalysisResult.UseSiteError());
			}
			bool flag;
			OverloadResolution.EffectiveParameters effectiveParametersInNormalForm = OverloadResolution.GetEffectiveParametersInNormalForm<TMember>(OverloadResolution.GetConstructedFrom<TMember>(leastOverriddenMember), arguments.Arguments.Count, argumentAnalysisResult.ArgsToParamsOpt, arguments.RefKinds, isMethodGroupConversion, allowRefOmittedArguments, this._binder, out flag);
			OverloadResolution.EffectiveParameters effectiveParametersInNormalForm2 = this.GetEffectiveParametersInNormalForm<TMember>(leastOverriddenMember, arguments.Arguments.Count, argumentAnalysisResult.ArgsToParamsOpt, arguments.RefKinds, isMethodGroupConversion, allowRefOmittedArguments);
			MemberResolutionResult<TMember> memberResolutionResult = this.IsApplicable<TMember>(member, leastOverriddenMember, typeArguments, arguments, effectiveParametersInNormalForm, effectiveParametersInNormalForm2, argumentAnalysisResult.ArgsToParamsOpt, flag, inferWithDynamic, completeResults, ref useSiteInfo);
			if (completeResults && !argumentAnalysisResult.IsValid)
			{
				return new MemberResolutionResult<TMember>(member, leastOverriddenMember, MemberAnalysisResult.ArgumentParameterMismatch(argumentAnalysisResult));
			}
			return memberResolutionResult;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00046EA4 File Offset: 0x000450A4
		private MemberResolutionResult<TMember> IsMemberApplicableInExpandedForm<TMember>(TMember member, TMember leastOverriddenMember, ArrayBuilder<TypeWithAnnotations> typeArguments, AnalyzedArguments arguments, bool allowRefOmittedArguments, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			ArgumentAnalysisResult argumentAnalysisResult = OverloadResolution.AnalyzeArguments(member, arguments, false, true);
			if (!argumentAnalysisResult.IsValid)
			{
				return new MemberResolutionResult<TMember>(member, leastOverriddenMember, MemberAnalysisResult.ArgumentParameterMismatch(argumentAnalysisResult));
			}
			if (member.HasUseSiteError)
			{
				return new MemberResolutionResult<TMember>(member, leastOverriddenMember, MemberAnalysisResult.UseSiteError());
			}
			bool flag;
			OverloadResolution.EffectiveParameters effectiveParametersInExpandedForm = OverloadResolution.GetEffectiveParametersInExpandedForm<TMember>(OverloadResolution.GetConstructedFrom<TMember>(leastOverriddenMember), arguments.Arguments.Count, argumentAnalysisResult.ArgsToParamsOpt, arguments.RefKinds, false, allowRefOmittedArguments, this._binder, out flag);
			OverloadResolution.EffectiveParameters effectiveParametersInExpandedForm2 = this.GetEffectiveParametersInExpandedForm<TMember>(leastOverriddenMember, arguments.Arguments.Count, argumentAnalysisResult.ArgsToParamsOpt, arguments.RefKinds, false, allowRefOmittedArguments);
			MemberResolutionResult<TMember> memberResolutionResult = this.IsApplicable<TMember>(member, leastOverriddenMember, typeArguments, arguments, effectiveParametersInExpandedForm, effectiveParametersInExpandedForm2, argumentAnalysisResult.ArgsToParamsOpt, flag, false, completeResults, ref useSiteInfo);
			if (!memberResolutionResult.Result.IsValid)
			{
				return memberResolutionResult;
			}
			return new MemberResolutionResult<TMember>(memberResolutionResult.Member, memberResolutionResult.LeastOverriddenMember, MemberAnalysisResult.ExpandedForm(memberResolutionResult.Result.ArgsToParamsOpt, memberResolutionResult.Result.ConversionsOpt, flag));
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00046FA8 File Offset: 0x000451A8
		private MemberResolutionResult<TMember> IsApplicable<TMember>(TMember member, TMember leastOverriddenMember, ArrayBuilder<TypeWithAnnotations> typeArgumentsBuilder, AnalyzedArguments arguments, OverloadResolution.EffectiveParameters originalEffectiveParameters, OverloadResolution.EffectiveParameters constructedEffectiveParameters, ImmutableArray<int> argsToParamsMap, bool hasAnyRefOmittedArgument, bool inferWithDynamic, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo) where TMember : Symbol
		{
			MethodSymbol methodSymbol;
			bool flag;
			OverloadResolution.EffectiveParameters effectiveParameters;
			if (member.Kind == SymbolKind.Method && (methodSymbol = (MethodSymbol)((object)member)).Arity > 0)
			{
				if (typeArgumentsBuilder.Count == 0 && arguments.HasDynamicArgument && !inferWithDynamic)
				{
					flag = true;
					effectiveParameters = constructedEffectiveParameters;
				}
				else
				{
					MethodSymbol methodSymbol2 = (MethodSymbol)((object)leastOverriddenMember);
					ImmutableArray<TypeWithAnnotations> immutableArray;
					if (typeArgumentsBuilder.Count > 0)
					{
						immutableArray = typeArgumentsBuilder.ToImmutable();
					}
					else
					{
						MemberAnalysisResult memberAnalysisResult;
						immutableArray = this.InferMethodTypeArguments(methodSymbol, methodSymbol2.ConstructedFrom.TypeParameters, arguments, originalEffectiveParameters, out memberAnalysisResult, ref useSiteInfo);
						if (immutableArray.IsDefault)
						{
							return new MemberResolutionResult<TMember>(member, leastOverriddenMember, memberAnalysisResult);
						}
					}
					member = (TMember)((object)methodSymbol.Construct(immutableArray));
					leastOverriddenMember = (TMember)((object)methodSymbol2.ConstructedFrom.Construct(immutableArray));
					ImmutableArray<TypeWithAnnotations> parameterTypes = leastOverriddenMember.GetParameterTypes();
					for (int i = 0; i < parameterTypes.Length; i++)
					{
						if (!parameterTypes[i].Type.CheckAllConstraints(this.Compilation, this.Conversions))
						{
							return new MemberResolutionResult<TMember>(member, leastOverriddenMember, MemberAnalysisResult.ConstructedParameterFailedConstraintsCheck(i));
						}
					}
					TypeMap typeMap = new TypeMap(methodSymbol.TypeParameters, immutableArray, true);
					effectiveParameters = new OverloadResolution.EffectiveParameters(typeMap.SubstituteTypes(constructedEffectiveParameters.ParameterTypes), constructedEffectiveParameters.ParameterRefKinds);
					flag = false;
				}
			}
			else
			{
				effectiveParameters = constructedEffectiveParameters;
				flag = false;
			}
			MemberAnalysisResult memberAnalysisResult2 = this.IsApplicable(member, effectiveParameters, arguments, argsToParamsMap, member.GetIsVararg(), hasAnyRefOmittedArgument, flag, completeResults, ref useSiteInfo);
			return new MemberResolutionResult<TMember>(member, leastOverriddenMember, memberAnalysisResult2);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0004712C File Offset: 0x0004532C
		private ImmutableArray<TypeWithAnnotations> InferMethodTypeArguments(MethodSymbol method, ImmutableArray<TypeParameterSymbol> originalTypeParameters, AnalyzedArguments arguments, OverloadResolution.EffectiveParameters originalEffectiveParameters, out MemberAnalysisResult error, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ImmutableArray<BoundExpression> immutableArray = arguments.Arguments.ToImmutable();
			MethodTypeInferenceResult methodTypeInferenceResult = MethodTypeInferrer.Infer(this._binder, this._binder.Conversions, originalTypeParameters, method.ContainingType, originalEffectiveParameters.ParameterTypes, originalEffectiveParameters.ParameterRefKinds, immutableArray, ref useSiteInfo, null);
			if (methodTypeInferenceResult.Success)
			{
				error = default(MemberAnalysisResult);
				return methodTypeInferenceResult.InferredTypeArguments;
			}
			if (arguments.IsExtensionMethodInvocation && MethodTypeInferrer.InferTypeArgumentsFromFirstArgument(this._binder.Conversions, method, immutableArray, ref useSiteInfo).IsDefault)
			{
				error = MemberAnalysisResult.TypeInferenceExtensionInstanceArgumentFailed();
				return default(ImmutableArray<TypeWithAnnotations>);
			}
			error = MemberAnalysisResult.TypeInferenceFailed();
			return default(ImmutableArray<TypeWithAnnotations>);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000471E0 File Offset: 0x000453E0
		private MemberAnalysisResult IsApplicable(Symbol candidate, OverloadResolution.EffectiveParameters parameters, AnalyzedArguments arguments, ImmutableArray<int> argsToParameters, bool isVararg, bool hasAnyRefOmittedArgument, bool ignoreOpenTypes, bool completeResults, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			int num = parameters.ParameterTypes.Length + (isVararg ? 1 : 0);
			if (arguments.Arguments.Count < num)
			{
				num = arguments.Arguments.Count;
			}
			ArrayBuilder<Conversion> arrayBuilder = null;
			ArrayBuilder<int> arrayBuilder2 = null;
			for (int i = 0; i < num; i++)
			{
				BoundExpression boundExpression = arguments.Argument(i);
				Conversion conversion;
				if (isVararg && i == num - 1)
				{
					if (boundExpression.Kind == BoundKind.ArgListOperator)
					{
						conversion = Conversion.Identity;
					}
					else
					{
						arrayBuilder2 = arrayBuilder2 ?? ArrayBuilder<int>.GetInstance();
						arrayBuilder2.Add(i);
						conversion = Conversion.NoConversion;
					}
				}
				else
				{
					RefKind refKind = arguments.RefKind(i);
					RefKind refKind2 = (parameters.ParameterRefKinds.IsDefault ? RefKind.None : parameters.ParameterRefKinds[i]);
					bool flag = arguments.IsExtensionMethodThisArgument(i);
					if (flag && refKind2 == RefKind.Ref)
					{
						refKind = refKind2;
					}
					conversion = this.CheckArgumentForApplicability(candidate, boundExpression, refKind, parameters.ParameterTypes[i].Type, refKind2, ignoreOpenTypes, ref useSiteInfo, flag);
					if (flag && !ConversionsBase.IsValidExtensionMethodThisArgConversion(conversion))
					{
						return MemberAnalysisResult.BadArgumentConversions(argsToParameters, ImmutableArray.Create<int>(i), ImmutableArray.Create<Conversion>(conversion));
					}
					if (!conversion.Exists)
					{
						arrayBuilder2 = arrayBuilder2 ?? ArrayBuilder<int>.GetInstance();
						arrayBuilder2.Add(i);
					}
				}
				if (arrayBuilder != null)
				{
					arrayBuilder.Add(conversion);
				}
				else if (!conversion.IsIdentity)
				{
					arrayBuilder = ArrayBuilder<Conversion>.GetInstance(num);
					arrayBuilder.AddMany(Conversion.Identity, i);
					arrayBuilder.Add(conversion);
				}
				if (arrayBuilder2 != null && !completeResults)
				{
					break;
				}
			}
			ImmutableArray<Conversion> immutableArray = ((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : default(ImmutableArray<Conversion>));
			MemberAnalysisResult memberAnalysisResult;
			if (arrayBuilder2 != null)
			{
				memberAnalysisResult = MemberAnalysisResult.BadArgumentConversions(argsToParameters, arrayBuilder2.ToImmutableAndFree(), immutableArray);
			}
			else
			{
				memberAnalysisResult = MemberAnalysisResult.NormalForm(argsToParameters, immutableArray, hasAnyRefOmittedArgument);
			}
			return memberAnalysisResult;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x000473B0 File Offset: 0x000455B0
		private Conversion CheckArgumentForApplicability(Symbol candidate, BoundExpression argument, RefKind argRefKind, TypeSymbol parameterType, RefKind parRefKind, bool ignoreOpenTypes, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forExtensionMethodThisArg)
		{
			if (argRefKind != parRefKind && (argRefKind != RefKind.None || !argument.HasDynamicType()))
			{
				return Conversion.NoConversion;
			}
			if (ignoreOpenTypes && parameterType.ContainsTypeParameter((MethodSymbol)candidate))
			{
				return Conversion.ImplicitDynamic;
			}
			TypeSymbol type = argument.Type;
			if (argument.Kind == BoundKind.OutVariablePendingInference || argument.Kind == BoundKind.OutDeconstructVarPendingInference || (argument.Kind == BoundKind.DiscardExpression && type == null))
			{
				return Conversion.Identity;
			}
			if (argRefKind == RefKind.None)
			{
				if (!forExtensionMethodThisArg)
				{
					return this.Conversions.ClassifyImplicitConversionFromExpression(argument, parameterType, ref useSiteInfo);
				}
				return this.Conversions.ClassifyImplicitExtensionMethodThisArgConversion(argument, argument.Type, parameterType, ref useSiteInfo);
			}
			else
			{
				if (type != null && ConversionsBase.HasIdentityConversion(type, parameterType))
				{
					return Conversion.Identity;
				}
				return Conversion.NoConversion;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0004746C File Offset: 0x0004566C
		private static TMember GetConstructedFrom<TMember>(TMember member) where TMember : Symbol
		{
			SymbolKind kind = member.Kind;
			if (kind == SymbolKind.Method)
			{
				return (TMember)((object)(member as MethodSymbol).ConstructedFrom);
			}
			if (kind == SymbolKind.Property)
			{
				return member;
			}
			throw ExceptionUtilities.UnexpectedValue(member.Kind);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000474BC File Offset: 0x000456BC
		private static ArgumentAnalysisResult AnalyzeArguments(Symbol symbol, AnalyzedArguments arguments, bool isMethodGroupConversion, bool expanded)
		{
			ImmutableArray<ParameterSymbol> parameters = symbol.GetParameters();
			bool isVararg = symbol.GetIsVararg();
			if (!expanded && arguments.Names.Count == 0)
			{
				return OverloadResolution.AnalyzeArgumentsForNormalFormNoNamedArguments(parameters, arguments, isMethodGroupConversion, isVararg);
			}
			int count = arguments.Arguments.Count;
			int[] array = null;
			int? num = default(int?);
			bool? flag = default(bool?);
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = OverloadResolution.IsValidParams(symbol);
			for (int i = 0; i < count; i++)
			{
				bool flag5;
				int num2 = OverloadResolution.CorrespondsToAnyParameter(parameters, expanded, arguments, i, flag4, isVararg, out flag5, ref flag2, ref flag3) ?? (-1);
				if (num2 == -1 && num == null)
				{
					num = new int?(i);
					flag = new bool?(flag5);
				}
				if (num2 != i && array == null)
				{
					array = new int[count];
					for (int j = 0; j < i; j++)
					{
						array[j] = j;
					}
				}
				if (array != null)
				{
					array[i] = num2;
				}
			}
			OverloadResolution.ParameterMap parameterMap = new OverloadResolution.ParameterMap(array, count);
			int? num3 = OverloadResolution.CheckForBadNonTrailingNamedArgument(arguments, parameterMap, parameters);
			if (num3 != null)
			{
				return ArgumentAnalysisResult.BadNonTrailingNamedArgument(num3.Value);
			}
			if (num != null)
			{
				if (flag.Value)
				{
					return ArgumentAnalysisResult.NoCorrespondingNamedParameter(num.Value);
				}
				return ArgumentAnalysisResult.NoCorrespondingParameter(num.Value);
			}
			else
			{
				int? num4 = OverloadResolution.NameUsedForPositional(arguments, parameterMap);
				if (num4 != null)
				{
					return ArgumentAnalysisResult.NameUsedForPositional(num4.Value);
				}
				int? num5 = OverloadResolution.CheckForMissingRequiredParameter(parameterMap, parameters, isMethodGroupConversion, expanded);
				if (num5 != null)
				{
					return ArgumentAnalysisResult.RequiredParameterMissing(num5.Value);
				}
				if (arguments.Names.Any() && arguments.Names.Last() != null && isVararg)
				{
					return ArgumentAnalysisResult.RequiredParameterMissing(parameters.Length);
				}
				int? num6 = OverloadResolution.CheckForDuplicateNamedArgument(arguments);
				if (num6 != null)
				{
					return ArgumentAnalysisResult.DuplicateNamedArgument(num6.Value);
				}
				if (!expanded)
				{
					return ArgumentAnalysisResult.NormalForm(parameterMap.ToImmutableArray());
				}
				return ArgumentAnalysisResult.ExpandedForm(parameterMap.ToImmutableArray());
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000476B0 File Offset: 0x000458B0
		private static int? CheckForBadNonTrailingNamedArgument(AnalyzedArguments arguments, OverloadResolution.ParameterMap argsToParameters, ImmutableArray<ParameterSymbol> parameters)
		{
			if (argsToParameters.IsTrivial)
			{
				return default(int?);
			}
			int num = -1;
			int count = arguments.Arguments.Count;
			for (int i = 0; i < count; i++)
			{
				int num2 = argsToParameters[i];
				if (num2 != -1 && num2 != i && arguments.Name(i) != null)
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				for (int j = num + 1; j < count; j++)
				{
					if (arguments.Name(j) == null)
					{
						return new int?(num);
					}
				}
			}
			return default(int?);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0004773C File Offset: 0x0004593C
		private static int? CorrespondsToAnyParameter(ImmutableArray<ParameterSymbol> memberParameters, bool expanded, AnalyzedArguments arguments, int argumentPosition, bool isValidParams, bool isVararg, out bool isNamedArgument, ref bool seenNamedParams, ref bool seenOutOfPositionNamedArgument)
		{
			isNamedArgument = arguments.Names.Count > argumentPosition && arguments.Names[argumentPosition] != null;
			if (isNamedArgument)
			{
				IdentifierNameSyntax identifierNameSyntax = arguments.Names[argumentPosition];
				for (int i = 0; i < memberParameters.Length; i++)
				{
					if (memberParameters[i].Name == identifierNameSyntax.Identifier.ValueText)
					{
						if (isValidParams && i == memberParameters.Length - 1)
						{
							seenNamedParams = true;
						}
						if (i != argumentPosition)
						{
							seenOutOfPositionNamedArgument = true;
						}
						return new int?(i);
					}
				}
				return default(int?);
			}
			if (seenNamedParams)
			{
				return default(int?);
			}
			if (seenOutOfPositionNamedArgument)
			{
				return default(int?);
			}
			int num = memberParameters.Length + (isVararg ? 1 : 0);
			if (argumentPosition < num)
			{
				return new int?(argumentPosition);
			}
			if (!expanded)
			{
				return default(int?);
			}
			return new int?(num - 1);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00047830 File Offset: 0x00045A30
		private static ArgumentAnalysisResult AnalyzeArgumentsForNormalFormNoNamedArguments(ImmutableArray<ParameterSymbol> parameters, AnalyzedArguments arguments, bool isMethodGroupConversion, bool isVararg)
		{
			int num = parameters.Length + (isVararg ? 1 : 0);
			int count = arguments.Arguments.Count;
			if (count < num)
			{
				for (int i = count; i < num; i++)
				{
					if (parameters.Length == i || !OverloadResolution.CanBeOptional(parameters[i], isMethodGroupConversion))
					{
						return ArgumentAnalysisResult.RequiredParameterMissing(i);
					}
				}
			}
			else if (num < count)
			{
				return ArgumentAnalysisResult.NoCorrespondingParameter(num);
			}
			return ArgumentAnalysisResult.NormalForm(default(ImmutableArray<int>));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000478A5 File Offset: 0x00045AA5
		private static bool CanBeOptional(ParameterSymbol parameter, bool isMethodGroupConversion)
		{
			return !isMethodGroupConversion && parameter.IsOptional;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000478B4 File Offset: 0x00045AB4
		private static int? NameUsedForPositional(AnalyzedArguments arguments, OverloadResolution.ParameterMap argsToParameters)
		{
			if (argsToParameters.IsTrivial)
			{
				return default(int?);
			}
			for (int i = 0; i < argsToParameters.Length; i++)
			{
				if (arguments.Name(i) != null)
				{
					for (int j = 0; j < i; j++)
					{
						if (arguments.Name(j) == null && argsToParameters[i] == argsToParameters[j])
						{
							return new int?(i);
						}
					}
				}
			}
			return default(int?);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00047928 File Offset: 0x00045B28
		private static int? CheckForMissingRequiredParameter(OverloadResolution.ParameterMap argsToParameters, ImmutableArray<ParameterSymbol> parameters, bool isMethodGroupConversion, bool expanded)
		{
			int num = (expanded ? (parameters.Length - 1) : parameters.Length);
			if (argsToParameters.IsTrivial && num <= argsToParameters.Length)
			{
				return default(int?);
			}
			for (int i = 0; i < num; i++)
			{
				if (!OverloadResolution.CanBeOptional(parameters[i], isMethodGroupConversion))
				{
					bool flag = false;
					for (int j = 0; j < argsToParameters.Length; j++)
					{
						flag = argsToParameters[j] == i;
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						return new int?(i);
					}
				}
			}
			return default(int?);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000479C0 File Offset: 0x00045BC0
		private static int? CheckForDuplicateNamedArgument(AnalyzedArguments arguments)
		{
			if (arguments.Names.IsEmpty<IdentifierNameSyntax>())
			{
				return default(int?);
			}
			PooledHashSet<string> instance = PooledHashSet<string>.GetInstance();
			for (int i = 0; i < arguments.Names.Count; i++)
			{
				string text = arguments.Name(i);
				if (text != null && !instance.Add(text))
				{
					instance.Free();
					return new int?(i);
				}
			}
			instance.Free();
			return default(int?);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00047A30 File Offset: 0x00045C30
		[CompilerGenerated]
		internal static bool <GetAllBuiltInOperators>g__useOnlyReferenceEquality|14_0(Conversions conversions, BoundExpression left, BoundExpression right, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return BuiltInOperators.IsValidObjectEquality(conversions, left.Type, left.IsLiteralNull(), false, right.Type, right.IsLiteralNull(), false, ref useSiteInfo) && (left.Type == null || (!left.Type.IsDelegateType() && left.Type.SpecialType != SpecialType.System_String && left.Type.SpecialType != SpecialType.System_Delegate)) && (right.Type == null || (!right.Type.IsDelegateType() && right.Type.SpecialType != SpecialType.System_String && right.Type.SpecialType != SpecialType.System_Delegate));
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00047ACD File Offset: 0x00045CCD
		[CompilerGenerated]
		internal static bool <AddDistinctOperators>g__equalsIgnoringNullable|17_0(TypeSymbol a, TypeSymbol b)
		{
			return a.Equals(b, TypeCompareKind.AllNullableIgnoreOptions);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00047AD8 File Offset: 0x00045CD8
		[CompilerGenerated]
		internal static bool <AddDistinctOperators>g__equalsIgnoringNullableAndDynamic|17_1(TypeSymbol a, TypeSymbol b)
		{
			return a.Equals(b, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes | TypeCompareKind.ObliviousNullableModifierMatchesAny);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00047AE3 File Offset: 0x00045CE3
		[CompilerGenerated]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static MemberResolutionResult<TMember> <RemoveCallingConventionMismatches>g__makeWrongCallingConvention|62_0<TMember>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] MemberResolutionResult<TMember> result) where TMember : Symbol
		{
			return new MemberResolutionResult<TMember>(result.Member, result.LeastOverriddenMember, MemberAnalysisResult.WrongCallingConvention());
		}

		// Token: 0x0400013B RID: 315
		private readonly Binder _binder;

		// Token: 0x0400013C RID: 316
		private bool? _strict;

		// Token: 0x0400013D RID: 317
		private const int BetterConversionTargetRecursionLimit = 100;

		// Token: 0x02000660 RID: 1632
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class BinopEasyOut
		{
			// Token: 0x060082B8 RID: 33464 RVA: 0x001DAAE0 File Offset: 0x001D8CE0
			public static BinaryOperatorKind OpKind(BinaryOperatorKind kind, TypeSymbol left, TypeSymbol right)
			{
				int num = left.TypeToIndex();
				if (num < 0)
				{
					return BinaryOperatorKind.Error;
				}
				int num2 = right.TypeToIndex();
				if (num2 < 0)
				{
					return BinaryOperatorKind.Error;
				}
				BinaryOperatorKind binaryOperatorKind = BinaryOperatorKind.Error;
				if (!kind.IsLogical() || (num == 15 && num2 == 15))
				{
					binaryOperatorKind = OverloadResolution.BinopEasyOut.s_opkind[kind.OperatorIndex()][num, num2];
				}
				if (binaryOperatorKind != BinaryOperatorKind.Error)
				{
					return binaryOperatorKind | kind;
				}
				return binaryOperatorKind;
			}

			// Token: 0x04001FD9 RID: 8153
			private const BinaryOperatorKind ERR = BinaryOperatorKind.Error;

			// Token: 0x04001FDA RID: 8154
			private const BinaryOperatorKind OBJ = BinaryOperatorKind.Object;

			// Token: 0x04001FDB RID: 8155
			private const BinaryOperatorKind STR = BinaryOperatorKind.String;

			// Token: 0x04001FDC RID: 8156
			private const BinaryOperatorKind OSC = BinaryOperatorKind.ObjectAndString;

			// Token: 0x04001FDD RID: 8157
			private const BinaryOperatorKind SOC = BinaryOperatorKind.StringAndObject;

			// Token: 0x04001FDE RID: 8158
			private const BinaryOperatorKind INT = BinaryOperatorKind.Int;

			// Token: 0x04001FDF RID: 8159
			private const BinaryOperatorKind UIN = BinaryOperatorKind.UInt;

			// Token: 0x04001FE0 RID: 8160
			private const BinaryOperatorKind LNG = BinaryOperatorKind.Long;

			// Token: 0x04001FE1 RID: 8161
			private const BinaryOperatorKind ULG = BinaryOperatorKind.ULong;

			// Token: 0x04001FE2 RID: 8162
			private const BinaryOperatorKind NIN = BinaryOperatorKind.NInt;

			// Token: 0x04001FE3 RID: 8163
			private const BinaryOperatorKind NUI = BinaryOperatorKind.NUInt;

			// Token: 0x04001FE4 RID: 8164
			private const BinaryOperatorKind FLT = BinaryOperatorKind.Float;

			// Token: 0x04001FE5 RID: 8165
			private const BinaryOperatorKind DBL = BinaryOperatorKind.Double;

			// Token: 0x04001FE6 RID: 8166
			private const BinaryOperatorKind DEC = BinaryOperatorKind.Decimal;

			// Token: 0x04001FE7 RID: 8167
			private const BinaryOperatorKind BOL = BinaryOperatorKind.Bool;

			// Token: 0x04001FE8 RID: 8168
			private const BinaryOperatorKind LIN = BinaryOperatorKind.Int | BinaryOperatorKind.Lifted;

			// Token: 0x04001FE9 RID: 8169
			private const BinaryOperatorKind LUN = BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted;

			// Token: 0x04001FEA RID: 8170
			private const BinaryOperatorKind LLG = (BinaryOperatorKind)65543;

			// Token: 0x04001FEB RID: 8171
			private const BinaryOperatorKind LUL = BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted;

			// Token: 0x04001FEC RID: 8172
			private const BinaryOperatorKind LNI = (BinaryOperatorKind)65545;

			// Token: 0x04001FED RID: 8173
			private const BinaryOperatorKind LNU = (BinaryOperatorKind)65546;

			// Token: 0x04001FEE RID: 8174
			private const BinaryOperatorKind LFL = (BinaryOperatorKind)65548;

			// Token: 0x04001FEF RID: 8175
			private const BinaryOperatorKind LDB = BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted;

			// Token: 0x04001FF0 RID: 8176
			private const BinaryOperatorKind LDC = BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted;

			// Token: 0x04001FF1 RID: 8177
			private const BinaryOperatorKind LBL = (BinaryOperatorKind)65551;

			// Token: 0x04001FF2 RID: 8178
			private static readonly BinaryOperatorKind[,] s_arithmetic = new BinaryOperatorKind[,]
			{
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				}
			};

			// Token: 0x04001FF3 RID: 8179
			private static readonly BinaryOperatorKind[,] s_addition = new BinaryOperatorKind[,]
			{
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.String,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject,
					BinaryOperatorKind.StringAndObject
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ObjectAndString,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				}
			};

			// Token: 0x04001FF4 RID: 8180
			private static readonly BinaryOperatorKind[,] s_shift = new BinaryOperatorKind[,]
			{
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				}
			};

			// Token: 0x04001FF5 RID: 8181
			private static readonly BinaryOperatorKind[,] s_equality = new BinaryOperatorKind[,]
			{
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.String,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Bool,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65551,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Float,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Double,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Decimal,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65551,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65551,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					(BinaryOperatorKind)65548,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object
				},
				{
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.Object,
					BinaryOperatorKind.UInt | BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted
				}
			};

			// Token: 0x04001FF6 RID: 8182
			private static readonly BinaryOperatorKind[,] s_logical = new BinaryOperatorKind[,]
			{
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Bool,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65551,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.Int,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.UInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Long,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.ULong,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.NUInt,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65551,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65551,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Int | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.UInt | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65545,
					(BinaryOperatorKind)65543,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65545,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.ULong | BinaryOperatorKind.Lifted,
					BinaryOperatorKind.Error,
					(BinaryOperatorKind)65546,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				},
				{
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error,
					BinaryOperatorKind.Error
				}
			};

			// Token: 0x04001FF7 RID: 8183
			private static readonly BinaryOperatorKind[][,] s_opkind = new BinaryOperatorKind[][,]
			{
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_addition,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_shift,
				OverloadResolution.BinopEasyOut.s_shift,
				OverloadResolution.BinopEasyOut.s_equality,
				OverloadResolution.BinopEasyOut.s_equality,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_arithmetic,
				OverloadResolution.BinopEasyOut.s_logical,
				OverloadResolution.BinopEasyOut.s_logical,
				OverloadResolution.BinopEasyOut.s_logical
			};
		}

		// Token: 0x02000661 RID: 1633
		private enum LiftingResult
		{
			// Token: 0x04001FF9 RID: 8185
			NotLifted,
			// Token: 0x04001FFA RID: 8186
			LiftOperandsAndResult,
			// Token: 0x04001FFB RID: 8187
			LiftOperandsButNotResult
		}

		// Token: 0x02000662 RID: 1634
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class UnopEasyOut
		{
			// Token: 0x060082BA RID: 33466 RVA: 0x001DAC58 File Offset: 0x001D8E58
			public static UnaryOperatorKind OpKind(UnaryOperatorKind kind, TypeSymbol operand)
			{
				int num = operand.TypeToIndex();
				if (num < 0)
				{
					return UnaryOperatorKind.Error;
				}
				int num2 = kind.OperatorIndex();
				UnaryOperatorKind unaryOperatorKind = ((num2 >= OverloadResolution.UnopEasyOut.s_opkind.Length) ? UnaryOperatorKind.Error : OverloadResolution.UnopEasyOut.s_opkind[num2][num]);
				if (unaryOperatorKind != UnaryOperatorKind.Error)
				{
					return unaryOperatorKind | kind;
				}
				return unaryOperatorKind;
			}

			// Token: 0x060082BB RID: 33467 RVA: 0x001DAC98 File Offset: 0x001D8E98
			// Note: this type is marked as 'beforefieldinit'.
			static UnopEasyOut()
			{
				UnaryOperatorKind[] array = new UnaryOperatorKind[32];
				array[2] = UnaryOperatorKind.Bool;
				array[17] = UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;
				OverloadResolution.UnopEasyOut.s_logicalNegation = array;
				OverloadResolution.UnopEasyOut.s_bitwiseComplement = new UnaryOperatorKind[]
				{
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Int,
					UnaryOperatorKind.Int,
					UnaryOperatorKind.Int,
					UnaryOperatorKind.Int,
					UnaryOperatorKind.Long,
					UnaryOperatorKind.Int,
					UnaryOperatorKind.Int,
					UnaryOperatorKind.UInt,
					UnaryOperatorKind.ULong,
					UnaryOperatorKind.NInt,
					UnaryOperatorKind.NUInt,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.SByte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.Byte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error,
					UnaryOperatorKind.Error
				};
				OverloadResolution.UnopEasyOut.s_opkind = new UnaryOperatorKind[][]
				{
					OverloadResolution.UnopEasyOut.s_increment,
					OverloadResolution.UnopEasyOut.s_increment,
					OverloadResolution.UnopEasyOut.s_increment,
					OverloadResolution.UnopEasyOut.s_increment,
					OverloadResolution.UnopEasyOut.s_plus,
					OverloadResolution.UnopEasyOut.s_minus,
					OverloadResolution.UnopEasyOut.s_logicalNegation,
					OverloadResolution.UnopEasyOut.s_bitwiseComplement
				};
			}

			// Token: 0x04001FFC RID: 8188
			private const UnaryOperatorKind ERR = UnaryOperatorKind.Error;

			// Token: 0x04001FFD RID: 8189
			private const UnaryOperatorKind BOL = UnaryOperatorKind.Bool;

			// Token: 0x04001FFE RID: 8190
			private const UnaryOperatorKind CHR = UnaryOperatorKind.Char;

			// Token: 0x04001FFF RID: 8191
			private const UnaryOperatorKind I08 = UnaryOperatorKind.SByte;

			// Token: 0x04002000 RID: 8192
			private const UnaryOperatorKind U08 = UnaryOperatorKind.Byte;

			// Token: 0x04002001 RID: 8193
			private const UnaryOperatorKind I16 = UnaryOperatorKind.Short;

			// Token: 0x04002002 RID: 8194
			private const UnaryOperatorKind U16 = UnaryOperatorKind.UShort;

			// Token: 0x04002003 RID: 8195
			private const UnaryOperatorKind I32 = UnaryOperatorKind.Int;

			// Token: 0x04002004 RID: 8196
			private const UnaryOperatorKind U32 = UnaryOperatorKind.UInt;

			// Token: 0x04002005 RID: 8197
			private const UnaryOperatorKind I64 = UnaryOperatorKind.Long;

			// Token: 0x04002006 RID: 8198
			private const UnaryOperatorKind U64 = UnaryOperatorKind.ULong;

			// Token: 0x04002007 RID: 8199
			private const UnaryOperatorKind NIN = UnaryOperatorKind.NInt;

			// Token: 0x04002008 RID: 8200
			private const UnaryOperatorKind NUI = UnaryOperatorKind.NUInt;

			// Token: 0x04002009 RID: 8201
			private const UnaryOperatorKind R32 = UnaryOperatorKind.Float;

			// Token: 0x0400200A RID: 8202
			private const UnaryOperatorKind R64 = UnaryOperatorKind.Double;

			// Token: 0x0400200B RID: 8203
			private const UnaryOperatorKind DEC = UnaryOperatorKind.Decimal;

			// Token: 0x0400200C RID: 8204
			private const UnaryOperatorKind LBOL = UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x0400200D RID: 8205
			private const UnaryOperatorKind LCHR = UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x0400200E RID: 8206
			private const UnaryOperatorKind LI08 = UnaryOperatorKind.SByte | UnaryOperatorKind.Lifted;

			// Token: 0x0400200F RID: 8207
			private const UnaryOperatorKind LU08 = UnaryOperatorKind.Byte | UnaryOperatorKind.Lifted;

			// Token: 0x04002010 RID: 8208
			private const UnaryOperatorKind LI16 = UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.Lifted;

			// Token: 0x04002011 RID: 8209
			private const UnaryOperatorKind LU16 = UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted;

			// Token: 0x04002012 RID: 8210
			private const UnaryOperatorKind LI32 = UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted;

			// Token: 0x04002013 RID: 8211
			private const UnaryOperatorKind LU32 = UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted;

			// Token: 0x04002014 RID: 8212
			private const UnaryOperatorKind LI64 = UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted;

			// Token: 0x04002015 RID: 8213
			private const UnaryOperatorKind LU64 = UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x04002016 RID: 8214
			private const UnaryOperatorKind LNI = UnaryOperatorKind.SByte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x04002017 RID: 8215
			private const UnaryOperatorKind LNU = UnaryOperatorKind.Byte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x04002018 RID: 8216
			private const UnaryOperatorKind LR32 = UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x04002019 RID: 8217
			private const UnaryOperatorKind LR64 = UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x0400201A RID: 8218
			private const UnaryOperatorKind LDEC = UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted;

			// Token: 0x0400201B RID: 8219
			private static readonly UnaryOperatorKind[] s_increment = new UnaryOperatorKind[]
			{
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Char,
				UnaryOperatorKind.SByte,
				UnaryOperatorKind.Short,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Long,
				UnaryOperatorKind.Byte,
				UnaryOperatorKind.UShort,
				UnaryOperatorKind.UInt,
				UnaryOperatorKind.ULong,
				UnaryOperatorKind.NInt,
				UnaryOperatorKind.NUInt,
				UnaryOperatorKind.Float,
				UnaryOperatorKind.Double,
				UnaryOperatorKind.Decimal,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted
			};

			// Token: 0x0400201C RID: 8220
			private static readonly UnaryOperatorKind[] s_plus = new UnaryOperatorKind[]
			{
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Long,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.UInt,
				UnaryOperatorKind.ULong,
				UnaryOperatorKind.NInt,
				UnaryOperatorKind.NUInt,
				UnaryOperatorKind.Float,
				UnaryOperatorKind.Double,
				UnaryOperatorKind.Decimal,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted
			};

			// Token: 0x0400201D RID: 8221
			private static readonly UnaryOperatorKind[] s_minus = new UnaryOperatorKind[]
			{
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Long,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Int,
				UnaryOperatorKind.Long,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.NInt,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.Float,
				UnaryOperatorKind.Double,
				UnaryOperatorKind.Decimal,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.SByte | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Error,
				UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.SByte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted,
				UnaryOperatorKind.Byte | UnaryOperatorKind.UShort | UnaryOperatorKind.ULong | UnaryOperatorKind.Lifted
			};

			// Token: 0x0400201E RID: 8222
			private static readonly UnaryOperatorKind[] s_logicalNegation;

			// Token: 0x0400201F RID: 8223
			private static readonly UnaryOperatorKind[] s_bitwiseComplement;

			// Token: 0x04002020 RID: 8224
			private static readonly UnaryOperatorKind[][] s_opkind;
		}

		// Token: 0x02000663 RID: 1635
		private class ReturnStatements : BoundTreeWalker
		{
			// Token: 0x060082BC RID: 33468 RVA: 0x001DAD66 File Offset: 0x001D8F66
			public ReturnStatements(ArrayBuilder<BoundReturnStatement> returns)
			{
				this._returns = returns;
			}

			// Token: 0x060082BD RID: 33469 RVA: 0x001DAD75 File Offset: 0x001D8F75
			public override BoundNode Visit(BoundNode node)
			{
				if (!(node is BoundExpression))
				{
					return base.Visit(node);
				}
				return null;
			}

			// Token: 0x060082BE RID: 33470 RVA: 0x001DAD88 File Offset: 0x001D8F88
			protected override BoundExpression VisitExpressionWithoutStackGuard(BoundExpression node)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060082BF RID: 33471 RVA: 0x001DAD8F File Offset: 0x001D8F8F
			public override BoundNode VisitLocalFunctionStatement(BoundLocalFunctionStatement node)
			{
				return null;
			}

			// Token: 0x060082C0 RID: 33472 RVA: 0x001DAD92 File Offset: 0x001D8F92
			public override BoundNode VisitReturnStatement(BoundReturnStatement node)
			{
				this._returns.Add(node);
				return null;
			}

			// Token: 0x04002021 RID: 8225
			private readonly ArrayBuilder<BoundReturnStatement> _returns;
		}

		// Token: 0x02000664 RID: 1636
		private struct EffectiveParameters
		{
			// Token: 0x060082C1 RID: 33473 RVA: 0x001DADA1 File Offset: 0x001D8FA1
			internal EffectiveParameters(ImmutableArray<TypeWithAnnotations> types, ImmutableArray<RefKind> refKinds)
			{
				this.ParameterTypes = types;
				this.ParameterRefKinds = refKinds;
			}

			// Token: 0x04002022 RID: 8226
			internal readonly ImmutableArray<TypeWithAnnotations> ParameterTypes;

			// Token: 0x04002023 RID: 8227
			internal readonly ImmutableArray<RefKind> ParameterRefKinds;
		}

		// Token: 0x02000665 RID: 1637
		private struct ParameterMap
		{
			// Token: 0x060082C2 RID: 33474 RVA: 0x001DADB1 File Offset: 0x001D8FB1
			public ParameterMap(int[] parameters, int length)
			{
				this._parameters = parameters;
				this._length = length;
			}

			// Token: 0x17002682 RID: 9858
			// (get) Token: 0x060082C3 RID: 33475 RVA: 0x001DADC1 File Offset: 0x001D8FC1
			public bool IsTrivial
			{
				get
				{
					return this._parameters == null;
				}
			}

			// Token: 0x17002683 RID: 9859
			// (get) Token: 0x060082C4 RID: 33476 RVA: 0x001DADCC File Offset: 0x001D8FCC
			public int Length
			{
				get
				{
					return this._length;
				}
			}

			// Token: 0x17002684 RID: 9860
			public int this[int argument]
			{
				get
				{
					if (this._parameters != null)
					{
						return this._parameters[argument];
					}
					return argument;
				}
			}

			// Token: 0x060082C6 RID: 33478 RVA: 0x001DADE8 File Offset: 0x001D8FE8
			public ImmutableArray<int> ToImmutableArray()
			{
				return this._parameters.AsImmutableOrNull<int>();
			}

			// Token: 0x04002024 RID: 8228
			private readonly int[] _parameters;

			// Token: 0x04002025 RID: 8229
			private readonly int _length;
		}
	}
}
