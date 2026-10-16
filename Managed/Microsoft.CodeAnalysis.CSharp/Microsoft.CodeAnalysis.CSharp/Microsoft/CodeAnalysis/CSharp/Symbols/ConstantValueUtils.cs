using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D3 RID: 1235
	internal static class ConstantValueUtils
	{
		// Token: 0x060068B1 RID: 26801 RVA: 0x0018896C File Offset: 0x00186B6C
		public static ConstantValue EvaluateFieldConstant(SourceFieldSymbol symbol, EqualsValueClauseSyntax equalsValueNode, HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics)
		{
			Binder binder = symbol.DeclaringCompilation.GetBinderFactory(symbol.Locations[0].SourceTree, false).GetBinder(equalsValueNode, null, null);
			if (earlyDecodingWellKnownAttributes)
			{
				binder = new EarlyWellKnownAttributeBinder(binder);
			}
			BoundEqualsValue boundEqualsValue = ConstantValueUtils.BindFieldOrEnumInitializer(new ConstantFieldsInProgressBinder(new ConstantFieldsInProgress(symbol, dependencies), binder), symbol, equalsValueNode, diagnostics);
			Location location = equalsValueNode.Value.Location;
			return ConstantValueUtils.GetAndValidateConstantValue(boundEqualsValue.Value, symbol, symbol.Type, location, diagnostics);
		}

		// Token: 0x060068B2 RID: 26802 RVA: 0x001889E4 File Offset: 0x00186BE4
		private static BoundFieldEqualsValue BindFieldOrEnumInitializer(Binder binder, FieldSymbol fieldSymbol, EqualsValueClauseSyntax initializer, BindingDiagnosticBag diagnostics)
		{
			SourceEnumConstantSymbol sourceEnumConstantSymbol = fieldSymbol as SourceEnumConstantSymbol;
			Binder binder2 = new LocalScopeBinder(binder);
			binder2 = new ExecutableCodeBinder(initializer, fieldSymbol, binder2, null);
			BoundFieldEqualsValue boundFieldEqualsValue;
			if (sourceEnumConstantSymbol != null)
			{
				boundFieldEqualsValue = binder2.BindEnumConstantInitializer(sourceEnumConstantSymbol, initializer, diagnostics);
			}
			else
			{
				boundFieldEqualsValue = binder2.BindFieldInitializer(fieldSymbol, initializer, diagnostics);
			}
			return boundFieldEqualsValue;
		}

		// Token: 0x060068B3 RID: 26803 RVA: 0x00188A24 File Offset: 0x00186C24
		internal static string UnescapeInterpolatedStringLiteral(string s)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			int length = s.Length;
			for (int i = 0; i < length; i++)
			{
				char c = s.get_Chars(i);
				builder.Append(c);
				if ((c == '{' || c == '}') && i + 1 < length && s.get_Chars(i + 1) == c)
				{
					i++;
				}
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060068B4 RID: 26804 RVA: 0x00188A8C File Offset: 0x00186C8C
		internal static ConstantValue GetAndValidateConstantValue(BoundExpression boundValue, Symbol thisSymbol, TypeSymbol typeSymbol, Location initValueNodeLocation, BindingDiagnosticBag diagnostics)
		{
			ConstantValue constantValue = ConstantValue.Bad;
			ConstantValueUtils.CheckLangVersionForConstantValue(boundValue, diagnostics);
			if (!boundValue.HasAnyErrors)
			{
				if (typeSymbol.TypeKind == TypeKind.TypeParameter)
				{
					diagnostics.Add(ErrorCode.ERR_InvalidConstantDeclarationType, initValueNodeLocation, new object[] { thisSymbol, typeSymbol });
				}
				else
				{
					bool flag = false;
					BoundExpression boundExpression = boundValue;
					while (boundExpression.Kind == BoundKind.Conversion)
					{
						BoundConversion boundConversion = (BoundConversion)boundExpression;
						flag = flag || boundConversion.ConversionKind.IsDynamic();
						boundExpression = boundConversion.Operand;
					}
					ConstantValue constantValue2 = boundValue.ConstantValue;
					ConstantValue constantValue3 = boundExpression.ConstantValue;
					if (constantValue3 != null && !constantValue3.IsNull && typeSymbol.IsReferenceType && typeSymbol.SpecialType != SpecialType.System_String)
					{
						diagnostics.Add(ErrorCode.ERR_NotNullConstRefField, initValueNodeLocation, new object[] { thisSymbol, typeSymbol });
						constantValue2 = constantValue2 ?? constantValue3;
					}
					if (constantValue2 != null && !flag)
					{
						constantValue = constantValue2;
					}
					else
					{
						diagnostics.Add(ErrorCode.ERR_NotConstantExpression, initValueNodeLocation, new object[] { thisSymbol });
					}
				}
			}
			return constantValue;
		}

		// Token: 0x060068B5 RID: 26805 RVA: 0x00188B91 File Offset: 0x00186D91
		internal static void CheckLangVersionForConstantValue(BoundExpression expression, BindingDiagnosticBag diagnostics)
		{
			if (expression.Type != null && expression.Type.IsStringType())
			{
				new ConstantValueUtils.CheckConstantInterpolatedStringValidity(diagnostics).Visit(expression);
			}
		}

		// Token: 0x020008F0 RID: 2288
		private sealed class CheckConstantInterpolatedStringValidity : BoundTreeWalkerWithStackGuardWithoutRecursionOnTheLeftOfBinaryOperator
		{
			// Token: 0x06008F16 RID: 36630 RVA: 0x001F8874 File Offset: 0x001F6A74
			public CheckConstantInterpolatedStringValidity(BindingDiagnosticBag diagnostics)
			{
				this.diagnostics = diagnostics;
			}

			// Token: 0x06008F17 RID: 36631 RVA: 0x001F8883 File Offset: 0x001F6A83
			public override BoundNode VisitInterpolatedString(BoundInterpolatedString node)
			{
				Binder.CheckFeatureAvailability(node.Syntax, MessageID.IDS_FeatureConstantInterpolatedStrings, this.diagnostics, null);
				return null;
			}

			// Token: 0x04002709 RID: 9993
			internal readonly BindingDiagnosticBag diagnostics;
		}
	}
}
