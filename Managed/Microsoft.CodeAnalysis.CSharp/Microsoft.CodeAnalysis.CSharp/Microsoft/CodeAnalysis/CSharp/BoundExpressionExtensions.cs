using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000AF RID: 175
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class BoundExpressionExtensions
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00055DF0 File Offset: 0x00053FF0
		public static RefKind GetRefKind(this BoundExpression node)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.Parameter)
			{
				if (kind == BoundKind.Local)
				{
					return ((BoundLocal)node).LocalSymbol.RefKind;
				}
				if (kind == BoundKind.Parameter)
				{
					return ((BoundParameter)node).ParameterSymbol.RefKind;
				}
			}
			else
			{
				if (kind == BoundKind.Call)
				{
					return ((BoundCall)node).Method.RefKind;
				}
				if (kind == BoundKind.PropertyAccess)
				{
					return ((BoundPropertyAccess)node).PropertySymbol.RefKind;
				}
			}
			return RefKind.None;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00055E6C File Offset: 0x0005406C
		public static bool IsLiteralNull(this BoundExpression node)
		{
			if (node != null && node.Kind == BoundKind.Literal)
			{
				ConstantValue constantValue = node.ConstantValue;
				if (constantValue != null)
				{
					return constantValue.Discriminator == ConstantValueTypeDiscriminator.Nothing;
				}
			}
			return false;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00055E9B File Offset: 0x0005409B
		public static bool IsLiteralDefault(this BoundExpression node)
		{
			return node.Kind == BoundKind.DefaultLiteral;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00055EA7 File Offset: 0x000540A7
		public static bool IsImplicitObjectCreation(this BoundExpression node)
		{
			return node.Kind == BoundKind.UnconvertedObjectCreationExpression;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00055EB6 File Offset: 0x000540B6
		public static bool IsLiteralDefaultOrImplicitObjectCreation(this BoundExpression node)
		{
			return node.IsLiteralDefault() || node.IsImplicitObjectCreation();
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00055EC8 File Offset: 0x000540C8
		public static bool IsDefaultValue(this BoundExpression node)
		{
			if (node.Kind == BoundKind.DefaultExpression || node.Kind == BoundKind.DefaultLiteral)
			{
				return true;
			}
			ConstantValue constantValue = node.ConstantValue;
			return constantValue != null && constantValue.IsDefaultValue;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00055F03 File Offset: 0x00054103
		public static bool HasExpressionType(this BoundExpression node)
		{
			return node.Type != null;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00055F10 File Offset: 0x00054110
		public static bool HasDynamicType(this BoundExpression node)
		{
			TypeSymbol type = node.Type;
			return type != null && type.IsDynamic();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00055F2F File Offset: 0x0005412F
		public static bool MethodGroupReceiverIsDynamic(this BoundMethodGroup node)
		{
			return node.InstanceOpt != null && node.InstanceOpt.HasDynamicType();
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00055F48 File Offset: 0x00054148
		public static bool HasExpressionSymbols(this BoundExpression node)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.Local)
			{
				if (kind <= BoundKind.TypeExpression)
				{
					if (kind == BoundKind.BadExpression)
					{
						return ((BoundBadExpression)node).Symbols.Length > 0;
					}
					if (kind != BoundKind.TypeExpression)
					{
						return false;
					}
				}
				else if (kind != BoundKind.NamespaceExpression && kind != BoundKind.Local)
				{
					return false;
				}
			}
			else if (kind <= BoundKind.Call)
			{
				if (kind != BoundKind.MethodGroup && kind != BoundKind.Call)
				{
					return false;
				}
			}
			else if (kind != BoundKind.ObjectCreationExpression && kind != BoundKind.FieldAccess && kind - BoundKind.PropertyAccess > 2)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00055FCC File Offset: 0x000541CC
		public static void GetExpressionSymbols(this BoundExpression node, ArrayBuilder<Symbol> symbols, BoundNode parent, Binder binder)
		{
			BoundKind kind = node.Kind;
			if (kind <= BoundKind.MethodGroup)
			{
				if (kind == BoundKind.BadExpression)
				{
					foreach (Symbol symbol in ((BoundBadExpression)node).Symbols)
					{
						if (symbol != null)
						{
							symbols.Add(symbol);
						}
					}
					return;
				}
				if (kind == BoundKind.MethodGroup)
				{
					BoundDelegateCreationExpression boundDelegateCreationExpression = parent as BoundDelegateCreationExpression;
					if (boundDelegateCreationExpression != null && boundDelegateCreationExpression.MethodOpt != null)
					{
						symbols.Add(boundDelegateCreationExpression.MethodOpt);
						return;
					}
					symbols.AddRange<MethodSymbol>(CSharpSemanticModel.GetReducedAndFilteredMethodGroupSymbols(binder, (BoundMethodGroup)node));
					return;
				}
			}
			else if (kind != BoundKind.Call)
			{
				if (kind != BoundKind.DelegateCreationExpression)
				{
					if (kind == BoundKind.IndexerAccess)
					{
						ImmutableArray<PropertySymbol> originalIndexersOpt = ((BoundIndexerAccess)node).OriginalIndexersOpt;
						if (!originalIndexersOpt.IsDefault)
						{
							symbols.AddRange<PropertySymbol>(originalIndexersOpt);
							return;
						}
					}
				}
				else
				{
					Symbol symbol2 = ((BoundDelegateCreationExpression)node).Type.GetMembers(".ctor").FirstOrDefault<Symbol>();
					if (symbol2 != null)
					{
						symbols.Add(symbol2);
						return;
					}
					return;
				}
			}
			else
			{
				ImmutableArray<MethodSymbol> originalMethodsOpt = ((BoundCall)node).OriginalMethodsOpt;
				if (!originalMethodsOpt.IsDefault)
				{
					symbols.AddRange<MethodSymbol>(originalMethodsOpt);
					return;
				}
			}
			Symbol expressionSymbol = node.ExpressionSymbol;
			if (expressionSymbol != null)
			{
				symbols.Add(expressionSymbol);
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00056101 File Offset: 0x00054301
		public static Conversion GetConversion(this BoundExpression boundNode)
		{
			if (boundNode.Kind == BoundKind.Conversion)
			{
				return ((BoundConversion)boundNode).Conversion;
			}
			return Conversion.Identity;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00056120 File Offset: 0x00054320
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsExpressionOfComImportType([NotNullWhen(true)] this BoundExpression expressionOpt)
		{
			if (expressionOpt == null)
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = expressionOpt.Type as NamedTypeSymbol;
			return namedTypeSymbol != null && namedTypeSymbol.Kind == SymbolKind.NamedType && namedTypeSymbol.IsComImport;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00056154 File Offset: 0x00054354
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool NullableAlwaysHasValue(this BoundExpression expr)
		{
			if (expr.Type == null)
			{
				return false;
			}
			if (expr.Type.IsDynamic())
			{
				return false;
			}
			if (!expr.Type.IsNullableType())
			{
				return true;
			}
			if (expr.Kind == BoundKind.ObjectCreationExpression)
			{
				return ((BoundObjectCreationExpression)expr).Constructor.ParameterCount != 0;
			}
			if (expr.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)expr;
				ConversionKind conversionKind = boundConversion.ConversionKind;
				if (conversionKind == ConversionKind.ImplicitEnumeration)
				{
					return boundConversion.Operand.NullableAlwaysHasValue();
				}
				if (conversionKind == ConversionKind.ImplicitNullable || conversionKind == ConversionKind.ExplicitNullable)
				{
					return boundConversion.Operand.NullableAlwaysHasValue();
				}
			}
			return false;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000561EC File Offset: 0x000543EC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool NullableNeverHasValue(this BoundExpression expr)
		{
			if (expr.Type == null && expr.ConstantValue == ConstantValue.Null)
			{
				return true;
			}
			if (expr.Type == null || !expr.Type.IsNullableType())
			{
				return false;
			}
			if (expr is BoundDefaultLiteral || expr is BoundDefaultExpression)
			{
				return true;
			}
			if (expr.Kind == BoundKind.ObjectCreationExpression)
			{
				return ((BoundObjectCreationExpression)expr).Constructor.ParameterCount == 0;
			}
			if (expr.Kind == BoundKind.Conversion)
			{
				BoundConversion boundConversion = (BoundConversion)expr;
				ConversionKind conversionKind = boundConversion.ConversionKind;
				if (conversionKind <= ConversionKind.NullLiteral)
				{
					if (conversionKind != ConversionKind.ImplicitNullable)
					{
						if (conversionKind != ConversionKind.NullLiteral)
						{
							return false;
						}
						return true;
					}
				}
				else if (conversionKind != ConversionKind.ExplicitNullable)
				{
					if (conversionKind != ConversionKind.DefaultLiteral)
					{
						return false;
					}
					return true;
				}
				return boundConversion.Operand.NullableNeverHasValue();
			}
			return false;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x000562A5 File Offset: 0x000544A5
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static bool IsNullableNonBoolean(this BoundExpression expr)
		{
			return expr.Type.IsNullableType() && expr.Type.GetNullableUnderlyingType().SpecialType != SpecialType.System_Boolean;
		}
	}
}
