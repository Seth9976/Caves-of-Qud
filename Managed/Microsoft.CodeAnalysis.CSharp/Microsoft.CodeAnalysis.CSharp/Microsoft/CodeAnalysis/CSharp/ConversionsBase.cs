using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200004A RID: 74
	internal abstract class ConversionsBase
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0003E1AB File Offset: 0x0003C3AB
		protected ConversionsBase(AssemblySymbol corLibrary, int currentRecursionDepth, bool includeNullability, ConversionsBase otherNullabilityOpt)
		{
			this.corLibrary = corLibrary;
			this.currentRecursionDepth = currentRecursionDepth;
			this.IncludeNullability = includeNullability;
			this._lazyOtherNullability = otherNullabilityOpt;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0003E1D0 File Offset: 0x0003C3D0
		internal ConversionsBase WithNullability(bool includeNullability)
		{
			if (this.IncludeNullability == includeNullability)
			{
				return this;
			}
			if (this._lazyOtherNullability == null)
			{
				Interlocked.CompareExchange<ConversionsBase>(ref this._lazyOtherNullability, this.WithNullabilityCore(includeNullability), null);
			}
			return this._lazyOtherNullability;
		}

		// Token: 0x06000776 RID: 1910
		protected abstract ConversionsBase WithNullabilityCore(bool includeNullability);

		// Token: 0x06000777 RID: 1911
		public abstract Conversion GetMethodGroupDelegateConversion(BoundMethodGroup source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo);

		// Token: 0x06000778 RID: 1912
		public abstract Conversion GetMethodGroupFunctionPointerConversion(BoundMethodGroup source, FunctionPointerTypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo);

		// Token: 0x06000779 RID: 1913
		public abstract Conversion GetStackAllocConversion(BoundStackAllocArrayCreation sourceExpression, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo);

		// Token: 0x0600077A RID: 1914
		protected abstract ConversionsBase CreateInstance(int currentRecursionDepth);

		// Token: 0x0600077B RID: 1915
		protected abstract Conversion GetInterpolatedStringConversion(BoundUnconvertedInterpolatedString source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo);

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0003E1FF File Offset: 0x0003C3FF
		internal AssemblySymbol CorLibrary
		{
			get
			{
				return this.corLibrary;
			}
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0003E208 File Offset: 0x0003C408
		public Conversion ClassifyImplicitConversionFromExpression(BoundExpression sourceExpression, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol type = sourceExpression.Type;
			if (type != null && this.HasIdentityConversionInternal(type, destination))
			{
				return Conversion.Identity;
			}
			Conversion conversion = this.ClassifyImplicitBuiltInConversionFromExpression(sourceExpression, type, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			if (type != null)
			{
				Conversion conversion2 = ConversionsBase.FastClassifyConversion(type, destination);
				if (conversion2.Exists)
				{
					if (conversion2.IsImplicit)
					{
						return conversion2;
					}
				}
				else
				{
					conversion = this.ClassifyImplicitBuiltInConversionSlow(type, destination, ref useSiteInfo);
					if (conversion.Exists)
					{
						return conversion;
					}
				}
			}
			conversion = this.GetImplicitUserDefinedConversion(sourceExpression, type, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			conversion = this.GetSwitchExpressionConversion(sourceExpression, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			return this.GetConditionalExpressionConversion(sourceExpression, destination, ref useSiteInfo);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0003E2B0 File Offset: 0x0003C4B0
		public Conversion ClassifyImplicitConversionFromType(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.HasIdentityConversionInternal(source, destination))
			{
				return Conversion.Identity;
			}
			Conversion conversion = ConversionsBase.FastClassifyConversion(source, destination);
			if (conversion.Exists)
			{
				if (!conversion.IsImplicit)
				{
					return Conversion.NoConversion;
				}
				return conversion;
			}
			else
			{
				Conversion conversion2 = this.ClassifyImplicitBuiltInConversionSlow(source, destination, ref useSiteInfo);
				if (conversion2.Exists)
				{
					return conversion2;
				}
				return this.GetImplicitUserDefinedConversion(null, source, destination, ref useSiteInfo);
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0003E30E File Offset: 0x0003C50E
		public Conversion ClassifyConversionFromExpressionType(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (ConversionsBase.HasImplicitDynamicConversionFromExpression(source, destination))
			{
				return Conversion.ImplicitDynamic;
			}
			return this.ClassifyConversionFromType(source, destination, ref useSiteInfo, false);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0003E32C File Offset: 0x0003C52C
		private static bool TryGetVoidConversion(TypeSymbol source, TypeSymbol destination, out Conversion conversion)
		{
			bool flag = source != null && source.SpecialType == SpecialType.System_Void;
			bool flag2 = destination.SpecialType == SpecialType.System_Void;
			if (flag && flag2)
			{
				conversion = Conversion.Identity;
				return true;
			}
			if (flag || flag2)
			{
				conversion = Conversion.NoConversion;
				return true;
			}
			conversion = default(Conversion);
			return false;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0003E380 File Offset: 0x0003C580
		public Conversion ClassifyConversionFromExpression(BoundExpression sourceExpression, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast = false)
		{
			Conversion conversion;
			if (ConversionsBase.TryGetVoidConversion(sourceExpression.Type, destination, out conversion))
			{
				return conversion;
			}
			if (forCast)
			{
				return this.ClassifyConversionFromExpressionForCast(sourceExpression, destination, ref useSiteInfo);
			}
			Conversion conversion2 = this.ClassifyImplicitConversionFromExpression(sourceExpression, destination, ref useSiteInfo);
			if (conversion2.Exists)
			{
				return conversion2;
			}
			return this.ClassifyExplicitOnlyConversionFromExpression(sourceExpression, destination, ref useSiteInfo, false);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0003E3CC File Offset: 0x0003C5CC
		public Conversion ClassifyConversionFromType(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast = false)
		{
			Conversion conversion;
			if (ConversionsBase.TryGetVoidConversion(source, destination, out conversion))
			{
				return conversion;
			}
			if (forCast)
			{
				return this.ClassifyConversionFromTypeForCast(source, destination, ref useSiteInfo);
			}
			Conversion conversion2 = ConversionsBase.FastClassifyConversion(source, destination);
			if (conversion2.Exists)
			{
				return conversion2;
			}
			Conversion conversion3 = this.ClassifyImplicitBuiltInConversionSlow(source, destination, ref useSiteInfo);
			if (conversion3.Exists)
			{
				return conversion3;
			}
			Conversion conversion4 = this.GetImplicitUserDefinedConversion(null, source, destination, ref useSiteInfo);
			if (conversion4.Exists)
			{
				return conversion4;
			}
			conversion4 = this.ClassifyExplicitBuiltInOnlyConversion(source, destination, ref useSiteInfo, false);
			if (conversion4.Exists)
			{
				return conversion4;
			}
			return this.GetExplicitUserDefinedConversion(null, source, destination, ref useSiteInfo);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0003E454 File Offset: 0x0003C654
		private Conversion ClassifyConversionFromExpressionForCast(BoundExpression source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyImplicitConversionFromExpression(source, destination, ref useSiteInfo);
			if (conversion.Exists && !ConversionsBase.ExplicitConversionMayDifferFromImplicit(conversion))
			{
				return conversion;
			}
			Conversion conversion2 = this.ClassifyExplicitOnlyConversionFromExpression(source, destination, ref useSiteInfo, true);
			if (conversion2.Exists)
			{
				return conversion2;
			}
			return conversion;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003E498 File Offset: 0x0003C698
		private Conversion ClassifyConversionFromTypeForCast(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = ConversionsBase.FastClassifyConversion(source, destination);
			if (conversion.Exists)
			{
				return conversion;
			}
			Conversion conversion2 = this.ClassifyImplicitBuiltInConversionSlow(source, destination, ref useSiteInfo);
			if (conversion2.Exists && !ConversionsBase.ExplicitConversionMayDifferFromImplicit(conversion2))
			{
				return conversion2;
			}
			Conversion conversion3 = this.ClassifyExplicitBuiltInOnlyConversion(source, destination, ref useSiteInfo, true);
			if (conversion3.Exists)
			{
				return conversion3;
			}
			if (conversion2.Exists)
			{
				return conversion2;
			}
			Conversion explicitUserDefinedConversion = this.GetExplicitUserDefinedConversion(null, source, destination, ref useSiteInfo);
			if (explicitUserDefinedConversion.Exists)
			{
				return explicitUserDefinedConversion;
			}
			return this.GetImplicitUserDefinedConversion(null, source, destination, ref useSiteInfo);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0003E518 File Offset: 0x0003C718
		public static Conversion FastClassifyConversion(TypeSymbol source, TypeSymbol target)
		{
			ConversionKind conversionKind = ConversionsBase.ConversionEasyOut.ClassifyConversion(source, target);
			if (conversionKind != ConversionKind.ImplicitNullable && conversionKind != ConversionKind.ExplicitNullable)
			{
				return Conversion.GetTrivialConversion(conversionKind);
			}
			return Conversion.MakeNullableConversion(conversionKind, ConversionsBase.FastClassifyConversion(source.StrippedType(), target.StrippedType()));
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0003E558 File Offset: 0x0003C758
		public Conversion ClassifyBuiltInConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = ConversionsBase.FastClassifyConversion(source, destination);
			if (conversion.Exists)
			{
				return conversion;
			}
			Conversion conversion2 = this.ClassifyImplicitBuiltInConversionSlow(source, destination, ref useSiteInfo);
			if (conversion2.Exists)
			{
				return conversion2;
			}
			return this.ClassifyExplicitBuiltInOnlyConversion(source, destination, ref useSiteInfo, false);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003E598 File Offset: 0x0003C798
		public Conversion ClassifyStandardConversion(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyStandardImplicitConversion(sourceExpression, source, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			if (source != null)
			{
				return this.DeriveStandardExplicitFromOppositeStandardImplicitConversion(source, destination, ref useSiteInfo);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0003E5D0 File Offset: 0x0003C7D0
		private Conversion ClassifyStandardImplicitConversion(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyImplicitBuiltInConversionFromExpression(sourceExpression, source, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			if (source != null)
			{
				return this.ClassifyStandardImplicitConversion(source, destination, ref useSiteInfo);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0003E608 File Offset: 0x0003C808
		private Conversion ClassifyStandardImplicitConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.HasIdentityConversionInternal(source, destination))
			{
				return Conversion.Identity;
			}
			if (ConversionsBase.HasImplicitNumericConversion(source, destination))
			{
				return Conversion.ImplicitNumeric;
			}
			Conversion conversion = this.ClassifyImplicitNullableConversion(source, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			if (this.HasImplicitReferenceConversion(source, destination, ref useSiteInfo))
			{
				return Conversion.ImplicitReference;
			}
			if (this.HasBoxingConversion(source, destination, ref useSiteInfo))
			{
				return Conversion.Boxing;
			}
			if (ConversionsBase.HasImplicitPointerToVoidConversion(source, destination))
			{
				return Conversion.PointerToVoid;
			}
			if (this.HasImplicitPointerConversion(source, destination, ref useSiteInfo))
			{
				return Conversion.ImplicitPointer;
			}
			Conversion conversion2 = this.ClassifyImplicitTupleConversion(source, destination, ref useSiteInfo);
			if (conversion2.Exists)
			{
				return conversion2;
			}
			return Conversion.NoConversion;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
		private Conversion ClassifyImplicitBuiltInConversionSlow(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.IsVoidType() || destination.IsVoidType())
			{
				return Conversion.NoConversion;
			}
			Conversion conversion = this.ClassifyStandardImplicitConversion(source, destination, ref useSiteInfo);
			if (conversion.Exists)
			{
				return conversion;
			}
			return Conversion.NoConversion;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003E6E5 File Offset: 0x0003C8E5
		private Conversion GetImplicitUserDefinedConversion(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return new Conversion(this.AnalyzeImplicitUserDefinedConversions(sourceExpression, source, destination, ref useSiteInfo), true);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		private Conversion ClassifyExplicitBuiltInOnlyConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast)
		{
			if (source.IsVoidType() || destination.IsVoidType())
			{
				return Conversion.NoConversion;
			}
			if (ConversionsBase.HasSpecialIntPtrConversion(source, destination))
			{
				return Conversion.IntPtr;
			}
			if (ConversionsBase.HasExplicitEnumerationConversion(source, destination))
			{
				return Conversion.ExplicitEnumeration;
			}
			Conversion conversion = this.ClassifyExplicitNullableConversion(source, destination, ref useSiteInfo, forCast);
			if (conversion.Exists)
			{
				return conversion;
			}
			if (this.HasExplicitReferenceConversion(source, destination, ref useSiteInfo))
			{
				if (source.Kind != SymbolKind.DynamicType)
				{
					return Conversion.ExplicitReference;
				}
				return Conversion.ExplicitDynamic;
			}
			else
			{
				if (this.HasUnboxingConversion(source, destination, ref useSiteInfo))
				{
					return Conversion.Unboxing;
				}
				Conversion conversion2 = this.ClassifyExplicitTupleConversion(source, destination, ref useSiteInfo, forCast);
				if (conversion2.Exists)
				{
					return conversion2;
				}
				if (ConversionsBase.HasPointerToPointerConversion(source, destination))
				{
					return Conversion.PointerToPointer;
				}
				if (ConversionsBase.HasPointerToIntegerConversion(source, destination))
				{
					return Conversion.PointerToInteger;
				}
				if (ConversionsBase.HasIntegerToPointerConversion(source, destination))
				{
					return Conversion.IntegerToPointer;
				}
				if (ConversionsBase.HasExplicitDynamicConversion(source, destination))
				{
					return Conversion.ExplicitDynamic;
				}
				return Conversion.NoConversion;
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0003E7D9 File Offset: 0x0003C9D9
		private Conversion GetExplicitUserDefinedConversion(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return new Conversion(this.AnalyzeExplicitUserDefinedConversions(sourceExpression, source, destination, ref useSiteInfo), false);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0003E7EC File Offset: 0x0003C9EC
		private Conversion DeriveStandardExplicitFromOppositeStandardImplicitConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyStandardImplicitConversion(destination, source, ref useSiteInfo);
			switch (conversion.Kind)
			{
			case ConversionKind.NoConversion:
				return Conversion.NoConversion;
			case ConversionKind.Identity:
				return Conversion.Identity;
			case ConversionKind.ImplicitNumeric:
				return Conversion.ExplicitNumeric;
			case ConversionKind.ImplicitTuple:
				return Conversion.NoConversion;
			case ConversionKind.ImplicitNullable:
			{
				TypeSymbol typeSymbol = source.StrippedType();
				TypeSymbol typeSymbol2 = destination.StrippedType();
				Conversion conversion2 = this.DeriveStandardExplicitFromOppositeStandardImplicitConversion(typeSymbol, typeSymbol2, ref useSiteInfo);
				return conversion2.Exists ? Conversion.MakeNullableConversion(ConversionKind.ExplicitNullable, conversion2) : Conversion.NoConversion;
			}
			case ConversionKind.ImplicitReference:
				return Conversion.ExplicitReference;
			case ConversionKind.Boxing:
				return Conversion.Unboxing;
			case ConversionKind.ImplicitPointerToVoid:
				return Conversion.PointerToPointer;
			}
			throw ExceptionUtilities.UnexpectedValue(conversion.Kind);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0003E8D0 File Offset: 0x0003CAD0
		public bool IsBaseInterface(TypeSymbol baseType, TypeSymbol derivedType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!baseType.IsInterfaceType())
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = derivedType as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (this.HasIdentityConversionInternal(namedTypeSymbol2, baseType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0003E924 File Offset: 0x0003CB24
		public bool IsBaseClass(TypeSymbol derivedType, TypeSymbol baseType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!baseType.IsClassType())
			{
				return false;
			}
			for (TypeSymbol typeSymbol = derivedType.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo); typeSymbol != null; typeSymbol = typeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (this.HasIdentityConversionInternal(typeSymbol, baseType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0003E960 File Offset: 0x0003CB60
		private static bool ExplicitConversionMayDifferFromImplicit(Conversion implicitConversion)
		{
			ConversionKind kind = implicitConversion.Kind;
			if (kind <= ConversionKind.ImplicitNullable)
			{
				if (kind - ConversionKind.ImplicitTupleLiteral > 1 && kind != ConversionKind.ImplicitNullable)
				{
					return false;
				}
			}
			else if (kind != ConversionKind.ImplicitDynamic && kind != ConversionKind.ImplicitUserDefined && kind != ConversionKind.ConditionalExpression)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0003E99C File Offset: 0x0003CB9C
		private Conversion ClassifyImplicitBuiltInConversionFromExpression(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (ConversionsBase.HasImplicitDynamicConversionFromExpression(source, destination))
			{
				return Conversion.ImplicitDynamic;
			}
			if (sourceExpression == null)
			{
				return Conversion.NoConversion;
			}
			if (ConversionsBase.HasImplicitEnumerationConversion(sourceExpression, destination))
			{
				return Conversion.ImplicitEnumeration;
			}
			Conversion conversion = ConversionsBase.ClassifyImplicitConstantExpressionConversion(sourceExpression, destination);
			if (conversion.Exists)
			{
				return conversion;
			}
			BoundKind kind = sourceExpression.Kind;
			if (kind <= BoundKind.UnconvertedObjectCreationExpression)
			{
				if (kind <= BoundKind.DefaultLiteral)
				{
					if (kind != BoundKind.UnconvertedAddressOfOperator)
					{
						if (kind == BoundKind.DefaultLiteral)
						{
							return Conversion.DefaultLiteral;
						}
					}
					else
					{
						FunctionPointerTypeSymbol functionPointerTypeSymbol = destination as FunctionPointerTypeSymbol;
						if (functionPointerTypeSymbol != null)
						{
							Conversion methodGroupFunctionPointerConversion = this.GetMethodGroupFunctionPointerConversion(((BoundUnconvertedAddressOfOperator)sourceExpression).Operand, functionPointerTypeSymbol, ref useSiteInfo);
							if (methodGroupFunctionPointerConversion.Exists)
							{
								return methodGroupFunctionPointerConversion;
							}
						}
					}
				}
				else if (kind != BoundKind.Literal)
				{
					if (kind != BoundKind.MethodGroup)
					{
						if (kind == BoundKind.UnconvertedObjectCreationExpression)
						{
							return Conversion.ObjectCreation;
						}
					}
					else
					{
						Conversion methodGroupDelegateConversion = this.GetMethodGroupDelegateConversion((BoundMethodGroup)sourceExpression, destination, ref useSiteInfo);
						if (methodGroupDelegateConversion.Exists)
						{
							return methodGroupDelegateConversion;
						}
					}
				}
				else
				{
					Conversion conversion2 = ConversionsBase.ClassifyNullLiteralConversion(sourceExpression, destination);
					if (conversion2.Exists)
					{
						return conversion2;
					}
				}
			}
			else if (kind <= BoundKind.UnboundLambda)
			{
				if (kind != BoundKind.TupleLiteral)
				{
					if (kind != BoundKind.StackAllocArrayCreation)
					{
						if (kind == BoundKind.UnboundLambda)
						{
							if (ConversionsBase.HasAnonymousFunctionConversion(sourceExpression, destination))
							{
								return Conversion.AnonymousFunction;
							}
						}
					}
					else
					{
						Conversion stackAllocConversion = this.GetStackAllocConversion((BoundStackAllocArrayCreation)sourceExpression, destination, ref useSiteInfo);
						if (stackAllocConversion.Exists)
						{
							return stackAllocConversion;
						}
					}
				}
				else
				{
					Conversion conversion3 = this.ClassifyImplicitTupleLiteralConversion((BoundTupleLiteral)sourceExpression, destination, ref useSiteInfo);
					if (conversion3.Exists)
					{
						return conversion3;
					}
				}
			}
			else if (kind != BoundKind.UnconvertedInterpolatedString)
			{
				if (kind == BoundKind.ThrowExpression)
				{
					return Conversion.ImplicitThrow;
				}
				if (kind == BoundKind.ExpressionWithNullability)
				{
					BoundExpression expression = ((BoundExpressionWithNullability)sourceExpression).Expression;
					Conversion conversion4 = this.ClassifyImplicitBuiltInConversionFromExpression(expression, expression.Type, destination, ref useSiteInfo);
					if (conversion4.Exists)
					{
						return conversion4;
					}
				}
			}
			else
			{
				Conversion interpolatedStringConversion = this.GetInterpolatedStringConversion((BoundUnconvertedInterpolatedString)sourceExpression, destination, ref useSiteInfo);
				if (interpolatedStringConversion.Exists)
				{
					return interpolatedStringConversion;
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		private Conversion GetSwitchExpressionConversion(BoundExpression source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source is BoundConvertedSwitchExpression)
			{
				return Conversion.NoConversion;
			}
			BoundUnconvertedSwitchExpression boundUnconvertedSwitchExpression = source as BoundUnconvertedSwitchExpression;
			if (boundUnconvertedSwitchExpression == null)
			{
				return Conversion.NoConversion;
			}
			ArrayBuilder<Conversion> instance = ArrayBuilder<Conversion>.GetInstance(boundUnconvertedSwitchExpression.SwitchArms.Length);
			foreach (BoundSwitchExpressionArm boundSwitchExpressionArm in boundUnconvertedSwitchExpression.SwitchArms)
			{
				Conversion conversion = this.ClassifyImplicitConversionFromExpression(boundSwitchExpressionArm.Value, destination, ref useSiteInfo);
				if (!conversion.Exists)
				{
					instance.Free();
					return Conversion.NoConversion;
				}
				instance.Add(conversion);
			}
			return Conversion.MakeSwitchExpression(instance.ToImmutableAndFree());
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0003EC40 File Offset: 0x0003CE40
		private Conversion GetConditionalExpressionConversion(BoundExpression source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			BoundUnconvertedConditionalOperator boundUnconvertedConditionalOperator = source as BoundUnconvertedConditionalOperator;
			if (boundUnconvertedConditionalOperator == null)
			{
				return Conversion.NoConversion;
			}
			Conversion conversion = this.ClassifyImplicitConversionFromExpression(boundUnconvertedConditionalOperator.Consequence, destination, ref useSiteInfo);
			if (!conversion.Exists)
			{
				return Conversion.NoConversion;
			}
			Conversion conversion2 = this.ClassifyImplicitConversionFromExpression(boundUnconvertedConditionalOperator.Alternative, destination, ref useSiteInfo);
			if (!conversion2.Exists)
			{
				return Conversion.NoConversion;
			}
			return Conversion.MakeConditionalExpression(ImmutableArray.Create<Conversion>(conversion, conversion2));
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0003ECA5 File Offset: 0x0003CEA5
		private static Conversion ClassifyNullLiteralConversion(BoundExpression source, TypeSymbol destination)
		{
			if (!source.IsLiteralNull())
			{
				return Conversion.NoConversion;
			}
			if (destination.IsNullableType())
			{
				return Conversion.NullLiteral;
			}
			if (destination.IsReferenceType)
			{
				return Conversion.ImplicitReference;
			}
			if (destination.IsPointerOrFunctionPointer())
			{
				return Conversion.NullToPointer;
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0003ECE4 File Offset: 0x0003CEE4
		private static Conversion ClassifyImplicitConstantExpressionConversion(BoundExpression source, TypeSymbol destination)
		{
			if (ConversionsBase.HasImplicitConstantExpressionConversion(source, destination))
			{
				return Conversion.ImplicitConstant;
			}
			if (destination.Kind == SymbolKind.NamedType)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)destination;
				if (namedTypeSymbol.OriginalDefinition.GetSpecialTypeSafe() == SpecialType.System_Nullable_T && ConversionsBase.HasImplicitConstantExpressionConversion(source, namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type))
				{
					return new Conversion(ConversionKind.ImplicitNullable, Conversion.ImplicitConstantUnderlying);
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003ED54 File Offset: 0x0003CF54
		private Conversion ClassifyImplicitTupleLiteralConversion(BoundTupleLiteral source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion implicitTupleLiteralConversion = this.GetImplicitTupleLiteralConversion(source, destination, ref useSiteInfo);
			if (implicitTupleLiteralConversion.Exists)
			{
				return implicitTupleLiteralConversion;
			}
			if (destination.Kind == SymbolKind.NamedType)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)destination;
				if (namedTypeSymbol.OriginalDefinition.GetSpecialTypeSafe() == SpecialType.System_Nullable_T)
				{
					Conversion implicitTupleLiteralConversion2 = this.GetImplicitTupleLiteralConversion(source, namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type, ref useSiteInfo);
					if (implicitTupleLiteralConversion2.Exists)
					{
						return new Conversion(ConversionKind.ImplicitNullable, ImmutableArray.Create<Conversion>(implicitTupleLiteralConversion2));
					}
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0003EDD4 File Offset: 0x0003CFD4
		private Conversion ClassifyExplicitTupleLiteralConversion(BoundTupleLiteral source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast)
		{
			Conversion explicitTupleLiteralConversion = this.GetExplicitTupleLiteralConversion(source, destination, ref useSiteInfo, forCast);
			if (explicitTupleLiteralConversion.Exists)
			{
				return explicitTupleLiteralConversion;
			}
			if (destination.Kind == SymbolKind.NamedType)
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)destination;
				if (namedTypeSymbol.OriginalDefinition.GetSpecialTypeSafe() == SpecialType.System_Nullable_T)
				{
					Conversion explicitTupleLiteralConversion2 = this.GetExplicitTupleLiteralConversion(source, namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type, ref useSiteInfo, forCast);
					if (explicitTupleLiteralConversion2.Exists)
					{
						return new Conversion(ConversionKind.ExplicitNullable, ImmutableArray.Create<Conversion>(explicitTupleLiteralConversion2));
					}
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0003EE58 File Offset: 0x0003D058
		internal static bool HasImplicitConstantExpressionConversion(BoundExpression source, TypeSymbol destination)
		{
			ConstantValue constantValue = source.ConstantValue;
			if (constantValue == null || source.Type == null)
			{
				return false;
			}
			SpecialType specialTypeSafe = source.Type.GetSpecialTypeSafe();
			if (specialTypeSafe == SpecialType.System_Int32)
			{
				int num = (constantValue.IsBad ? 0 : constantValue.Int32Value);
				switch (destination.GetSpecialTypeSafe())
				{
				case SpecialType.System_SByte:
					return -128 <= num && num <= 127;
				case SpecialType.System_Byte:
					return 0 <= num && num <= 255;
				case SpecialType.System_Int16:
					return -32768 <= num && num <= 32767;
				case SpecialType.System_UInt16:
					return 0 <= num && num <= 65535;
				case SpecialType.System_Int32:
				case SpecialType.System_Int64:
				case SpecialType.System_Decimal:
				case SpecialType.System_Single:
				case SpecialType.System_Double:
				case SpecialType.System_String:
					return false;
				case SpecialType.System_UInt32:
					break;
				case SpecialType.System_UInt64:
					return 0 <= num;
				case SpecialType.System_IntPtr:
					if (destination.IsNativeIntegerType)
					{
						return true;
					}
					return false;
				case SpecialType.System_UIntPtr:
					if (!destination.IsNativeIntegerType)
					{
						return false;
					}
					break;
				default:
					return false;
				}
				return 0L <= (long)num;
			}
			return specialTypeSafe == SpecialType.System_Int64 && destination.GetSpecialTypeSafe() == SpecialType.System_UInt64 && (constantValue.IsBad || 0L <= constantValue.Int64Value);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0003EF84 File Offset: 0x0003D184
		private Conversion ClassifyExplicitOnlyConversionFromExpression(BoundExpression sourceExpression, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast)
		{
			TypeSymbol type = sourceExpression.Type;
			if (sourceExpression.Kind == BoundKind.TupleLiteral)
			{
				Conversion conversion = this.ClassifyExplicitTupleLiteralConversion((BoundTupleLiteral)sourceExpression, destination, ref useSiteInfo, forCast);
				if (conversion.Exists)
				{
					return conversion;
				}
			}
			if (type != null)
			{
				Conversion conversion2 = ConversionsBase.FastClassifyConversion(type, destination);
				if (conversion2.Exists)
				{
					return conversion2;
				}
				Conversion conversion3 = this.ClassifyExplicitBuiltInOnlyConversion(type, destination, ref useSiteInfo, forCast);
				if (conversion3.Exists)
				{
					return conversion3;
				}
			}
			return this.GetExplicitUserDefinedConversion(sourceExpression, type, destination, ref useSiteInfo);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool HasImplicitEnumerationConversion(BoundExpression source, TypeSymbol destination)
		{
			if (!destination.IsEnumType() && (!destination.IsNullableType() || !destination.GetNullableUnderlyingType().IsEnumType()))
			{
				return false;
			}
			ConstantValue constantValue = source.ConstantValue;
			return constantValue != null && source.Type != null && ConversionsBase.IsNumericType(source.Type) && ConversionsBase.IsConstantNumericZero(constantValue);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003F058 File Offset: 0x0003D258
		private static LambdaConversionResult IsAnonymousFunctionCompatibleWithDelegate(UnboundLambda anonymousFunction, TypeSymbol type)
		{
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type;
			MethodSymbol delegateInvokeMethod = namedTypeSymbol.DelegateInvokeMethod;
			if (delegateInvokeMethod == null || delegateInvokeMethod.HasUseSiteError)
			{
				return LambdaConversionResult.BadTargetType;
			}
			ImmutableArray<ParameterSymbol> parameters = delegateInvokeMethod.Parameters;
			if (anonymousFunction.HasSignature)
			{
				if (anonymousFunction.ParameterCount != delegateInvokeMethod.ParameterCount)
				{
					return LambdaConversionResult.BadParameterCount;
				}
				if (anonymousFunction.HasExplicitlyTypedParameterList)
				{
					for (int i = 0; i < parameters.Length; i++)
					{
						if (parameters[i].RefKind != anonymousFunction.RefKind(i) || !parameters[i].Type.Equals(anonymousFunction.ParameterType(i), TypeCompareKind.AllIgnoreOptions))
						{
							return LambdaConversionResult.MismatchedParameterType;
						}
					}
				}
				else
				{
					for (int j = 0; j < parameters.Length; j++)
					{
						if (parameters[j].RefKind != RefKind.None)
						{
							return LambdaConversionResult.RefInImplicitlyTypedLambda;
						}
					}
					for (int k = 0; k < parameters.Length; k++)
					{
						if (parameters[k].TypeWithAnnotations.IsStatic)
						{
							return LambdaConversionResult.StaticTypeInImplicitlyTypedLambda;
						}
					}
				}
			}
			else
			{
				for (int l = 0; l < parameters.Length; l++)
				{
					if (parameters[l].RefKind == RefKind.Out)
					{
						return LambdaConversionResult.MissingSignatureWithOutParameter;
					}
				}
			}
			if (ErrorFacts.PreventsSuccessfulDelegateConversion(anonymousFunction.Bind(namedTypeSymbol).Diagnostics.Diagnostics))
			{
				return LambdaConversionResult.BindingFailed;
			}
			return LambdaConversionResult.Success;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003F19C File Offset: 0x0003D39C
		private static LambdaConversionResult IsAnonymousFunctionCompatibleWithExpressionTree(UnboundLambda anonymousFunction, NamedTypeSymbol type)
		{
			TypeSymbol typeSymbol = ((type.Arity == 0) ? null : type.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[0].Type);
			if (typeSymbol != null && !typeSymbol.IsDelegateType())
			{
				return LambdaConversionResult.ExpressionTreeMustHaveDelegateTypeArgument;
			}
			if (anonymousFunction.Syntax.Kind() == SyntaxKind.AnonymousMethodExpression)
			{
				return LambdaConversionResult.ExpressionTreeFromAnonymousMethod;
			}
			if (typeSymbol == null)
			{
				return LambdaConversionResult.Success;
			}
			return ConversionsBase.IsAnonymousFunctionCompatibleWithDelegate(anonymousFunction, typeSymbol);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0003F1FC File Offset: 0x0003D3FC
		public static LambdaConversionResult IsAnonymousFunctionCompatibleWithType(UnboundLambda anonymousFunction, TypeSymbol type)
		{
			if (type.SpecialType == SpecialType.System_Delegate)
			{
				if (ConversionsBase.IsFeatureInferredDelegateTypeEnabled(anonymousFunction))
				{
					return LambdaConversionResult.Success;
				}
			}
			else
			{
				if (type.IsDelegateType())
				{
					return ConversionsBase.IsAnonymousFunctionCompatibleWithDelegate(anonymousFunction, type);
				}
				bool flag;
				if (type.IsGenericOrNonGenericExpressionType(out flag) && (flag || ConversionsBase.IsFeatureInferredDelegateTypeEnabled(anonymousFunction)))
				{
					return ConversionsBase.IsAnonymousFunctionCompatibleWithExpressionTree(anonymousFunction, (NamedTypeSymbol)type);
				}
			}
			return LambdaConversionResult.BadTargetType;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003F24F File Offset: 0x0003D44F
		internal static bool IsFeatureInferredDelegateTypeEnabled(BoundExpression expr)
		{
			return expr.Syntax.IsFeatureEnabled(MessageID.IDS_FeatureInferredDelegateType);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0003F261 File Offset: 0x0003D461
		private static bool HasAnonymousFunctionConversion(BoundExpression source, TypeSymbol destination)
		{
			return source.Kind == BoundKind.UnboundLambda && ConversionsBase.IsAnonymousFunctionCompatibleWithType((UnboundLambda)source, destination) == LambdaConversionResult.Success;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0003F284 File Offset: 0x0003D484
		internal Conversion ClassifyImplicitUserDefinedConversionForV6SwitchGoverningType(TypeSymbol sourceType, out TypeSymbol switchGoverningType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			UserDefinedConversionResult userDefinedConversionResult = this.AnalyzeImplicitUserDefinedConversionForV6SwitchGoverningType(sourceType, ref useSiteInfo);
			if (userDefinedConversionResult.Kind == UserDefinedConversionResultKind.Valid)
			{
				UserDefinedConversionAnalysis userDefinedConversionAnalysis = userDefinedConversionResult.Results[userDefinedConversionResult.Best];
				switchGoverningType = userDefinedConversionAnalysis.ToType;
			}
			else
			{
				switchGoverningType = null;
			}
			return new Conversion(userDefinedConversionResult, true);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0003F2CC File Offset: 0x0003D4CC
		internal Conversion GetCallerLineNumberConversion(TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			SyntaxNode syntaxNode = new Microsoft.CodeAnalysis.CSharp.Syntax.LiteralExpressionSyntax(new Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax.LiteralExpressionSyntax(SyntaxKind.NumericLiteralExpression, new SyntaxToken(SyntaxKind.NumericLiteralToken)), null, 0);
			TypeSymbol specialType = this.corLibrary.GetSpecialType(SpecialType.System_Int32);
			BoundLiteral boundLiteral = new BoundLiteral(syntaxNode, ConstantValue.Create(int.MaxValue), specialType);
			return this.ClassifyStandardImplicitConversion(boundLiteral, specialType, destination, ref useSiteInfo);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0003F320 File Offset: 0x0003D520
		internal bool HasCallerLineNumberConversion(TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.GetCallerLineNumberConversion(destination, ref useSiteInfo).Exists;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0003F340 File Offset: 0x0003D540
		internal bool HasCallerInfoStringConversion(TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol specialType = this.corLibrary.GetSpecialType(SpecialType.System_String);
			return this.ClassifyStandardImplicitConversion(specialType, destination, ref useSiteInfo).Exists;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0003F36C File Offset: 0x0003D56C
		public static bool HasIdentityConversion(TypeSymbol type1, TypeSymbol type2)
		{
			return ConversionsBase.HasIdentityConversionInternal(type1, type2, false);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0003F378 File Offset: 0x0003D578
		private static bool HasIdentityConversionInternal(TypeSymbol type1, TypeSymbol type2, bool includeNullability)
		{
			TypeCompareKind typeCompareKind = (includeNullability ? (TypeCompareKind.IgnoreCustomModifiersAndArraySizesAndLowerBounds | TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.ObliviousNullableModifierMatchesAny | TypeCompareKind.IgnoreNativeIntegers) : TypeCompareKind.AllIgnoreOptions);
			return type1.Equals(type2, typeCompareKind);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0003F397 File Offset: 0x0003D597
		private bool HasIdentityConversionInternal(TypeSymbol type1, TypeSymbol type2)
		{
			return ConversionsBase.HasIdentityConversionInternal(type1, type2, this.IncludeNullability);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0003F3A8 File Offset: 0x0003D5A8
		internal bool HasTopLevelNullabilityIdentityConversion(TypeWithAnnotations source, TypeWithAnnotations destination)
		{
			if (!this.IncludeNullability)
			{
				return true;
			}
			if (source.NullableAnnotation.IsOblivious() || destination.NullableAnnotation.IsOblivious())
			{
				return true;
			}
			bool flag = ConversionsBase.IsPossiblyNullableTypeTypeParameter(in source);
			bool flag2 = ConversionsBase.IsPossiblyNullableTypeTypeParameter(in destination);
			if (flag && !flag2)
			{
				return destination.NullableAnnotation.IsAnnotated();
			}
			if (flag2 && !flag)
			{
				return source.NullableAnnotation.IsAnnotated();
			}
			return source.NullableAnnotation.IsAnnotated() == destination.NullableAnnotation.IsAnnotated();
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0003F428 File Offset: 0x0003D628
		internal bool HasTopLevelNullabilityImplicitConversion(TypeWithAnnotations source, TypeWithAnnotations destination)
		{
			return !this.IncludeNullability || (source.NullableAnnotation.IsOblivious() || destination.NullableAnnotation.IsOblivious() || destination.NullableAnnotation.IsAnnotated()) || ((!ConversionsBase.IsPossiblyNullableTypeTypeParameter(in source) || ConversionsBase.IsPossiblyNullableTypeTypeParameter(in destination)) && !source.NullableAnnotation.IsAnnotated());
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0003F48C File Offset: 0x0003D68C
		private static bool IsPossiblyNullableTypeTypeParameter(in TypeWithAnnotations typeWithAnnotations)
		{
			TypeSymbol type = typeWithAnnotations.Type;
			return type != null && (type.IsPossiblyNullableReferenceTypeTypeParameter() || type.IsNullableTypeOrTypeParameter());
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0003F4B8 File Offset: 0x0003D6B8
		public bool HasAnyNullabilityImplicitConversion(TypeWithAnnotations source, TypeWithAnnotations destination)
		{
			CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
			return this.HasTopLevelNullabilityImplicitConversion(source, destination) && this.ClassifyImplicitConversionFromType(source.Type, destination.Type, ref discarded).Kind != ConversionKind.NoConversion;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0003F4FC File Offset: 0x0003D6FC
		public static bool HasIdentityConversionToAny<T>(T type, ArrayBuilder<T> targetTypes) where T : TypeSymbol
		{
			foreach (T t in targetTypes)
			{
				if (ConversionsBase.HasIdentityConversionInternal(type, t, false))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0003F53C File Offset: 0x0003D73C
		public Conversion ConvertExtensionMethodThisArg(TypeSymbol parameterType, TypeSymbol thisType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyImplicitExtensionMethodThisArgConversion(null, thisType, parameterType, ref useSiteInfo);
			if (!ConversionsBase.IsValidExtensionMethodThisArgConversion(conversion))
			{
				return Conversion.NoConversion;
			}
			return conversion;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0003F564 File Offset: 0x0003D764
		public Conversion ClassifyImplicitExtensionMethodThisArgConversion(BoundExpression sourceExpressionOpt, TypeSymbol sourceType, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (sourceType != null)
			{
				if (this.HasIdentityConversionInternal(sourceType, destination))
				{
					return Conversion.Identity;
				}
				if (this.HasBoxingConversion(sourceType, destination, ref useSiteInfo))
				{
					return Conversion.Boxing;
				}
				if (this.HasImplicitReferenceConversion(sourceType, destination, ref useSiteInfo))
				{
					return Conversion.ImplicitReference;
				}
			}
			if (sourceExpressionOpt != null && sourceExpressionOpt.Kind == BoundKind.TupleLiteral)
			{
				Conversion tupleLiteralConversion = this.GetTupleLiteralConversion((BoundTupleLiteral)sourceExpressionOpt, destination, ref useSiteInfo, ConversionKind.ImplicitTupleLiteral, delegate(ConversionsBase conversions, BoundExpression s, TypeWithAnnotations d, ref CompoundUseSiteInfo<AssemblySymbol> u, bool a)
				{
					return conversions.ClassifyImplicitExtensionMethodThisArgConversion(s, s.Type, d.Type, ref u);
				}, false);
				if (tupleLiteralConversion.Exists)
				{
					return tupleLiteralConversion;
				}
			}
			if (sourceType != null)
			{
				Conversion conversion = this.ClassifyTupleConversion(sourceType, destination, ref useSiteInfo, ConversionKind.ImplicitTuple, delegate(ConversionsBase conversions, TypeWithAnnotations s, TypeWithAnnotations d, ref CompoundUseSiteInfo<AssemblySymbol> u, bool a)
				{
					if (!conversions.HasTopLevelNullabilityImplicitConversion(s, d))
					{
						return Conversion.NoConversion;
					}
					return conversions.ClassifyImplicitExtensionMethodThisArgConversion(null, s.Type, d.Type, ref u);
				}, false);
				if (conversion.Exists)
				{
					return conversion;
				}
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0003F634 File Offset: 0x0003D834
		public static bool IsValidExtensionMethodThisArgConversion(Conversion conversion)
		{
			ConversionKind kind = conversion.Kind;
			if (kind != ConversionKind.Identity)
			{
				if (kind - ConversionKind.ImplicitTupleLiteral <= 1)
				{
					ImmutableArray<Conversion>.Enumerator enumerator = conversion.UnderlyingConversions.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (!ConversionsBase.IsValidExtensionMethodThisArgConversion(enumerator.Current))
						{
							return false;
						}
					}
					return true;
				}
				if (kind - ConversionKind.ImplicitReference > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0003F68C File Offset: 0x0003D88C
		private static int GetNumericTypeIndex(SpecialType specialType)
		{
			switch (specialType)
			{
			case SpecialType.System_Char:
				return 8;
			case SpecialType.System_SByte:
				return 0;
			case SpecialType.System_Byte:
				return 1;
			case SpecialType.System_Int16:
				return 2;
			case SpecialType.System_UInt16:
				return 3;
			case SpecialType.System_Int32:
				return 4;
			case SpecialType.System_UInt32:
				return 5;
			case SpecialType.System_Int64:
				return 6;
			case SpecialType.System_UInt64:
				return 7;
			case SpecialType.System_Decimal:
				return 11;
			case SpecialType.System_Single:
				return 9;
			case SpecialType.System_Double:
				return 10;
			default:
				return -1;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool HasImplicitNumericConversion(TypeSymbol source, TypeSymbol destination)
		{
			int numericTypeIndex = ConversionsBase.GetNumericTypeIndex(source.SpecialType);
			if (numericTypeIndex < 0)
			{
				return false;
			}
			int numericTypeIndex2 = ConversionsBase.GetNumericTypeIndex(destination.SpecialType);
			return numericTypeIndex2 >= 0 && ConversionsBase.s_implicitNumericConversions[numericTypeIndex, numericTypeIndex2];
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0003F730 File Offset: 0x0003D930
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool HasExplicitNumericConversion(TypeSymbol source, TypeSymbol destination)
		{
			int numericTypeIndex = ConversionsBase.GetNumericTypeIndex(source.SpecialType);
			if (numericTypeIndex < 0)
			{
				return false;
			}
			int numericTypeIndex2 = ConversionsBase.GetNumericTypeIndex(destination.SpecialType);
			return numericTypeIndex2 >= 0 && ConversionsBase.s_explicitNumericConversions[numericTypeIndex, numericTypeIndex2];
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0003F770 File Offset: 0x0003D970
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool IsConstantNumericZero(ConstantValue value)
		{
			switch (value.Discriminator)
			{
			case ConstantValueTypeDiscriminator.SByte:
				return value.SByteValue == 0;
			case ConstantValueTypeDiscriminator.Byte:
				return value.ByteValue == 0;
			case ConstantValueTypeDiscriminator.Int16:
				return value.Int16Value == 0;
			case ConstantValueTypeDiscriminator.UInt16:
				return value.UInt16Value == 0;
			case ConstantValueTypeDiscriminator.Int32:
			case ConstantValueTypeDiscriminator.NInt:
				return value.Int32Value == 0;
			case ConstantValueTypeDiscriminator.UInt32:
			case ConstantValueTypeDiscriminator.NUInt:
				return value.UInt32Value == 0U;
			case ConstantValueTypeDiscriminator.Int64:
				return value.Int64Value == 0L;
			case ConstantValueTypeDiscriminator.UInt64:
				return value.UInt64Value == 0UL;
			case ConstantValueTypeDiscriminator.Single:
			case ConstantValueTypeDiscriminator.Double:
				return value.DoubleValue == 0.0;
			case ConstantValueTypeDiscriminator.Decimal:
				return value.DecimalValue == 0m;
			}
			return false;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0003F844 File Offset: 0x0003DA44
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool IsNumericType(TypeSymbol type)
		{
			switch (type.SpecialType)
			{
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
			case SpecialType.System_Single:
			case SpecialType.System_Double:
				break;
			case SpecialType.System_String:
				return false;
			case SpecialType.System_IntPtr:
				if (!type.IsNativeIntegerType)
				{
					return false;
				}
				break;
			case SpecialType.System_UIntPtr:
				if (!type.IsNativeIntegerType)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			return true;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0003F8B4 File Offset: 0x0003DAB4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool HasSpecialIntPtrConversion(TypeSymbol source, TypeSymbol target)
		{
			TypeSymbol typeSymbol = source.StrippedType();
			TypeSymbol typeSymbol2 = target.StrippedType();
			TypeSymbol typeSymbol3;
			if (ConversionsBase.<HasSpecialIntPtrConversion>g__isIntPtrOrUIntPtr|76_0(typeSymbol))
			{
				typeSymbol3 = typeSymbol2;
			}
			else
			{
				if (!ConversionsBase.<HasSpecialIntPtrConversion>g__isIntPtrOrUIntPtr|76_0(typeSymbol2))
				{
					return false;
				}
				typeSymbol3 = typeSymbol;
			}
			if (typeSymbol3.IsPointerOrFunctionPointer())
			{
				return true;
			}
			if (typeSymbol3.TypeKind == TypeKind.Enum)
			{
				return true;
			}
			SpecialType specialType = typeSymbol3.SpecialType;
			return specialType - SpecialType.System_Char <= 11;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0003F90F File Offset: 0x0003DB0F
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private static bool HasExplicitEnumerationConversion(TypeSymbol source, TypeSymbol destination)
		{
			return (ConversionsBase.IsNumericType(source) && destination.IsEnumType()) || (ConversionsBase.IsNumericType(destination) && source.IsEnumType()) || (source.IsEnumType() && destination.IsEnumType());
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0003F948 File Offset: 0x0003DB48
		private Conversion ClassifyImplicitNullableConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!destination.IsNullableType())
			{
				return Conversion.NoConversion;
			}
			TypeSymbol nullableUnderlyingType = destination.GetNullableUnderlyingType();
			TypeSymbol typeSymbol = source.StrippedType();
			if (!typeSymbol.IsValueType)
			{
				return Conversion.NoConversion;
			}
			if (this.HasIdentityConversionInternal(typeSymbol, nullableUnderlyingType))
			{
				return new Conversion(ConversionKind.ImplicitNullable, Conversion.IdentityUnderlying);
			}
			if (ConversionsBase.HasImplicitNumericConversion(typeSymbol, nullableUnderlyingType))
			{
				return new Conversion(ConversionKind.ImplicitNullable, Conversion.ImplicitNumericUnderlying);
			}
			Conversion conversion = this.ClassifyImplicitTupleConversion(typeSymbol, nullableUnderlyingType, ref useSiteInfo);
			if (conversion.Exists)
			{
				return new Conversion(ConversionKind.ImplicitNullable, ImmutableArray.Create<Conversion>(conversion));
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0003F9D2 File Offset: 0x0003DBD2
		private Conversion GetImplicitTupleLiteralConversion(BoundTupleLiteral source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.GetTupleLiteralConversion(source, destination, ref useSiteInfo, ConversionKind.ImplicitTupleLiteral, delegate(ConversionsBase conversions, BoundExpression s, TypeWithAnnotations d, ref CompoundUseSiteInfo<AssemblySymbol> u, bool a)
			{
				return conversions.ClassifyImplicitConversionFromExpression(s, d.Type, ref u);
			}, false);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0003F9FE File Offset: 0x0003DBFE
		private Conversion GetExplicitTupleLiteralConversion(BoundTupleLiteral source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast)
		{
			return this.GetTupleLiteralConversion(source, destination, ref useSiteInfo, ConversionKind.ExplicitTupleLiteral, delegate(ConversionsBase conversions, BoundExpression s, TypeWithAnnotations d, ref CompoundUseSiteInfo<AssemblySymbol> u, bool a)
			{
				return conversions.ClassifyConversionFromExpression(s, d.Type, ref u, a);
			}, forCast);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0003FA2C File Offset: 0x0003DC2C
		private Conversion GetTupleLiteralConversion(BoundTupleLiteral source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConversionKind kind, ConversionsBase.ClassifyConversionFromExpressionDelegate classifyConversion, bool arg)
		{
			ImmutableArray<BoundExpression> arguments = source.Arguments;
			if (!destination.IsTupleTypeOfCardinality(arguments.Length))
			{
				return Conversion.NoConversion;
			}
			ImmutableArray<TypeWithAnnotations> tupleElementTypesWithAnnotations = destination.TupleElementTypesWithAnnotations;
			ArrayBuilder<Conversion> instance = ArrayBuilder<Conversion>.GetInstance(arguments.Length);
			for (int i = 0; i < arguments.Length; i++)
			{
				BoundExpression boundExpression = arguments[i];
				Conversion conversion = classifyConversion(this, boundExpression, tupleElementTypesWithAnnotations[i], ref useSiteInfo, arg);
				if (!conversion.Exists)
				{
					instance.Free();
					return Conversion.NoConversion;
				}
				instance.Add(conversion);
			}
			return new Conversion(kind, instance.ToImmutableAndFree());
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0003FAC6 File Offset: 0x0003DCC6
		private Conversion ClassifyImplicitTupleConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.ClassifyTupleConversion(source, destination, ref useSiteInfo, ConversionKind.ImplicitTuple, delegate(ConversionsBase conversions, TypeWithAnnotations s, TypeWithAnnotations d, ref CompoundUseSiteInfo<AssemblySymbol> u, bool a)
			{
				if (!conversions.HasTopLevelNullabilityImplicitConversion(s, d))
				{
					return Conversion.NoConversion;
				}
				return conversions.ClassifyImplicitConversionFromType(s.Type, d.Type, ref u);
			}, false);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003FAF2 File Offset: 0x0003DCF2
		private Conversion ClassifyExplicitTupleConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast)
		{
			return this.ClassifyTupleConversion(source, destination, ref useSiteInfo, ConversionKind.ExplicitTuple, delegate(ConversionsBase conversions, TypeWithAnnotations s, TypeWithAnnotations d, ref CompoundUseSiteInfo<AssemblySymbol> u, bool a)
			{
				if (!conversions.HasTopLevelNullabilityImplicitConversion(s, d))
				{
					return Conversion.NoConversion;
				}
				return conversions.ClassifyConversionFromType(s.Type, d.Type, ref u, a);
			}, forCast);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003FB20 File Offset: 0x0003DD20
		private Conversion ClassifyTupleConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConversionKind kind, ConversionsBase.ClassifyConversionFromTypeDelegate classifyConversion, bool arg)
		{
			ImmutableArray<TypeWithAnnotations> immutableArray;
			ImmutableArray<TypeWithAnnotations> immutableArray2;
			if (!source.TryGetElementTypesWithAnnotationsIfTupleType(out immutableArray) || !destination.TryGetElementTypesWithAnnotationsIfTupleType(out immutableArray2) || immutableArray.Length != immutableArray2.Length)
			{
				return Conversion.NoConversion;
			}
			ArrayBuilder<Conversion> instance = ArrayBuilder<Conversion>.GetInstance(immutableArray.Length);
			for (int i = 0; i < immutableArray.Length; i++)
			{
				Conversion conversion = classifyConversion(this, immutableArray[i], immutableArray2[i], ref useSiteInfo, arg);
				if (!conversion.Exists)
				{
					instance.Free();
					return Conversion.NoConversion;
				}
				instance.Add(conversion);
			}
			return new Conversion(kind, instance.ToImmutableAndFree());
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0003FBC0 File Offset: 0x0003DDC0
		private Conversion ClassifyExplicitNullableConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool forCast)
		{
			if (!source.IsNullableType() && !destination.IsNullableType())
			{
				return Conversion.NoConversion;
			}
			TypeSymbol typeSymbol = source.StrippedType();
			TypeSymbol typeSymbol2 = destination.StrippedType();
			if (this.HasIdentityConversionInternal(typeSymbol, typeSymbol2))
			{
				return new Conversion(ConversionKind.ExplicitNullable, Conversion.IdentityUnderlying);
			}
			if (ConversionsBase.HasImplicitNumericConversion(typeSymbol, typeSymbol2))
			{
				return new Conversion(ConversionKind.ExplicitNullable, Conversion.ImplicitNumericUnderlying);
			}
			if (ConversionsBase.HasExplicitNumericConversion(typeSymbol, typeSymbol2))
			{
				return new Conversion(ConversionKind.ExplicitNullable, Conversion.ExplicitNumericUnderlying);
			}
			Conversion conversion = this.ClassifyExplicitTupleConversion(typeSymbol, typeSymbol2, ref useSiteInfo, forCast);
			if (conversion.Exists)
			{
				return new Conversion(ConversionKind.ExplicitNullable, ImmutableArray.Create<Conversion>(conversion));
			}
			if (ConversionsBase.HasExplicitEnumerationConversion(typeSymbol, typeSymbol2))
			{
				return new Conversion(ConversionKind.ExplicitNullable, Conversion.ExplicitEnumerationUnderlying);
			}
			if (ConversionsBase.HasPointerToIntegerConversion(typeSymbol, typeSymbol2))
			{
				return new Conversion(ConversionKind.ExplicitNullable, Conversion.PointerToIntegerUnderlying);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0003FC88 File Offset: 0x0003DE88
		private bool HasCovariantArrayConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayTypeSymbol arrayTypeSymbol = source as ArrayTypeSymbol;
			ArrayTypeSymbol arrayTypeSymbol2 = destination as ArrayTypeSymbol;
			return arrayTypeSymbol != null && arrayTypeSymbol2 != null && arrayTypeSymbol.HasSameShapeAs(arrayTypeSymbol2) && this.HasImplicitReferenceConversion(arrayTypeSymbol.ElementTypeWithAnnotations, arrayTypeSymbol2.ElementTypeWithAnnotations, ref useSiteInfo);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003FCC9 File Offset: 0x0003DEC9
		public bool HasIdentityOrImplicitReferenceConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.HasIdentityConversionInternal(source, destination) || this.HasImplicitReferenceConversion(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0003FCE0 File Offset: 0x0003DEE0
		private static bool HasImplicitDynamicConversionFromExpression(TypeSymbol expressionType, TypeSymbol destination)
		{
			return expressionType != null && expressionType.Kind == SymbolKind.DynamicType && !destination.IsPointerOrFunctionPointer();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0003FCF9 File Offset: 0x0003DEF9
		private static bool HasExplicitDynamicConversion(TypeSymbol source, TypeSymbol destination)
		{
			return source.Kind == SymbolKind.DynamicType && !destination.IsPointerOrFunctionPointer();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0003FD10 File Offset: 0x0003DF10
		private bool HasArrayConversionToInterface(ArrayTypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!source.IsSZArray)
			{
				return false;
			}
			if (!destination.IsInterfaceType())
			{
				return false;
			}
			if (destination.SpecialType == SpecialType.System_Collections_IEnumerable)
			{
				return true;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)destination;
			if (namedTypeSymbol.AllTypeArgumentCount() != 1)
			{
				return false;
			}
			if (!namedTypeSymbol.IsPossibleArrayGenericInterface())
			{
				return false;
			}
			TypeWithAnnotations elementTypeWithAnnotations = source.ElementTypeWithAnnotations;
			TypeWithAnnotations typeWithAnnotations = namedTypeSymbol.TypeArgumentWithDefinitionUseSiteDiagnostics(0, ref useSiteInfo);
			return (!this.IncludeNullability || this.HasTopLevelNullabilityImplicitConversion(elementTypeWithAnnotations, typeWithAnnotations)) && this.HasIdentityOrImplicitReferenceConversion(elementTypeWithAnnotations.Type, typeWithAnnotations.Type, ref useSiteInfo);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0003FD94 File Offset: 0x0003DF94
		private bool HasImplicitReferenceConversion(TypeWithAnnotations source, TypeWithAnnotations destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.IncludeNullability)
			{
				if (!this.HasTopLevelNullabilityImplicitConversion(source, destination))
				{
					return false;
				}
				if (source.NullableAnnotation != destination.NullableAnnotation && ConversionsBase.HasIdentityConversionInternal(source.Type, destination.Type, true))
				{
					return true;
				}
			}
			return this.HasImplicitReferenceConversion(source.Type, destination.Type, ref useSiteInfo);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0003FDF0 File Offset: 0x0003DFF0
		internal bool HasImplicitReferenceConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.IsErrorType())
			{
				return false;
			}
			if (!source.IsReferenceType)
			{
				return false;
			}
			if (destination.SpecialType == SpecialType.System_Object || destination.Kind == SymbolKind.DynamicType)
			{
				return true;
			}
			TypeKind typeKind = source.TypeKind;
			switch (typeKind)
			{
			case TypeKind.Array:
				return this.HasImplicitConversionFromArray(source, destination, ref useSiteInfo);
			case TypeKind.Class:
				return (destination.IsClassType() && this.IsBaseClass(source, destination, ref useSiteInfo)) || this.HasImplicitConversionToInterface(source, destination, ref useSiteInfo);
			case TypeKind.Delegate:
				return this.HasImplicitConversionFromDelegate(source, destination, ref useSiteInfo);
			case TypeKind.Dynamic:
			case TypeKind.Enum:
			case TypeKind.Error:
				break;
			case TypeKind.Interface:
				return this.HasImplicitConversionToInterface(source, destination, ref useSiteInfo);
			default:
				if (typeKind == TypeKind.TypeParameter)
				{
					return this.HasImplicitReferenceTypeParameterConversion((TypeParameterSymbol)source, destination, ref useSiteInfo);
				}
				break;
			}
			return false;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0003FEA4 File Offset: 0x0003E0A4
		private bool HasImplicitConversionToInterface(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!destination.IsInterfaceType())
			{
				return false;
			}
			if (source.IsClassType())
			{
				return this.HasAnyBaseInterfaceConversion(source, destination, ref useSiteInfo);
			}
			if (source.IsInterfaceType())
			{
				if (this.HasAnyBaseInterfaceConversion(source, destination, ref useSiteInfo))
				{
					return true;
				}
				if (!this.HasIdentityConversionInternal(source, destination) && this.HasInterfaceVarianceConversion(source, destination, ref useSiteInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0003FEFC File Offset: 0x0003E0FC
		private bool HasImplicitConversionFromArray(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayTypeSymbol arrayTypeSymbol = source as ArrayTypeSymbol;
			return arrayTypeSymbol != null && (this.HasCovariantArrayConversion(source, destination, ref useSiteInfo) || destination.GetSpecialTypeSafe() == SpecialType.System_Array || this.IsBaseInterface(destination, this.corLibrary.GetDeclaredSpecialType(SpecialType.System_Array), ref useSiteInfo) || this.HasArrayConversionToInterface(arrayTypeSymbol, destination, ref useSiteInfo));
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0003FF58 File Offset: 0x0003E158
		private bool HasImplicitConversionFromDelegate(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!source.IsDelegateType())
			{
				return false;
			}
			SpecialType specialTypeSafe = destination.GetSpecialTypeSafe();
			return specialTypeSafe == SpecialType.System_MulticastDelegate || specialTypeSafe == SpecialType.System_Delegate || this.IsBaseInterface(destination, this.corLibrary.GetDeclaredSpecialType(SpecialType.System_MulticastDelegate), ref useSiteInfo) || this.HasDelegateVarianceConversion(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0003FFA4 File Offset: 0x0003E1A4
		public bool HasImplicitTypeParameterConversion(TypeParameterSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.HasImplicitReferenceTypeParameterConversion(source, destination, ref useSiteInfo) || this.HasImplicitBoxingTypeParameterConversion(source, destination, ref useSiteInfo) || (destination.TypeKind == TypeKind.TypeParameter && source.DependsOn((TypeParameterSymbol)destination));
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0003FFDC File Offset: 0x0003E1DC
		private bool HasImplicitReferenceTypeParameterConversion(TypeParameterSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return !source.IsValueType && (this.HasImplicitEffectiveBaseConversion(source, destination, ref useSiteInfo) || this.HasImplicitEffectiveInterfaceSetConversion(source, destination, ref useSiteInfo) || (destination.TypeKind == TypeKind.TypeParameter && source.DependsOn((TypeParameterSymbol)destination)));
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00040028 File Offset: 0x0003E228
		private bool HasImplicitEffectiveBaseConversion(TypeParameterSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol namedTypeSymbol = source.EffectiveBaseClass(ref useSiteInfo);
			return this.HasIdentityConversionInternal(namedTypeSymbol, destination) || this.IsBaseClass(namedTypeSymbol, destination, ref useSiteInfo) || this.HasAnyBaseInterfaceConversion(namedTypeSymbol, destination, ref useSiteInfo);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00040064 File Offset: 0x0003E264
		private bool HasImplicitEffectiveInterfaceSetConversion(TypeParameterSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!destination.IsInterfaceType())
			{
				return false;
			}
			foreach (NamedTypeSymbol namedTypeSymbol in source.AllEffectiveInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (this.HasInterfaceVarianceConversion(namedTypeSymbol, destination, ref useSiteInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000400AC File Offset: 0x0003E2AC
		private bool HasAnyBaseInterfaceConversion(TypeSymbol derivedType, TypeSymbol baseType, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (!baseType.IsInterfaceType())
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = derivedType as NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			foreach (NamedTypeSymbol namedTypeSymbol2 in namedTypeSymbol.AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (this.HasInterfaceVarianceConversion(namedTypeSymbol2, baseType, ref useSiteInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00040100 File Offset: 0x0003E300
		private bool HasInterfaceVarianceConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol namedTypeSymbol = source as NamedTypeSymbol;
			NamedTypeSymbol namedTypeSymbol2 = destination as NamedTypeSymbol;
			return namedTypeSymbol != null && namedTypeSymbol2 != null && namedTypeSymbol.IsInterfaceType() && namedTypeSymbol2.IsInterfaceType() && this.HasVariantConversion(namedTypeSymbol, namedTypeSymbol2, ref useSiteInfo);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00040140 File Offset: 0x0003E340
		private bool HasDelegateVarianceConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			NamedTypeSymbol namedTypeSymbol = source as NamedTypeSymbol;
			NamedTypeSymbol namedTypeSymbol2 = destination as NamedTypeSymbol;
			return namedTypeSymbol != null && namedTypeSymbol2 != null && namedTypeSymbol.IsDelegateType() && namedTypeSymbol2.IsDelegateType() && this.HasVariantConversion(namedTypeSymbol, namedTypeSymbol2, ref useSiteInfo);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00040180 File Offset: 0x0003E380
		private bool HasVariantConversion(NamedTypeSymbol source, NamedTypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this.currentRecursionDepth >= 50)
			{
				return false;
			}
			ThreeState threeState = this.HasVariantConversionQuick(source, destination);
			if (threeState.HasValue())
			{
				return threeState.Value();
			}
			return this.CreateInstance(this.currentRecursionDepth + 1).HasVariantConversionNoCycleCheck(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000401C7 File Offset: 0x0003E3C7
		private ThreeState HasVariantConversionQuick(NamedTypeSymbol source, NamedTypeSymbol destination)
		{
			if (this.HasIdentityConversionInternal(source, destination))
			{
				return ThreeState.True;
			}
			if (!TypeSymbol.Equals(source.OriginalDefinition, destination.OriginalDefinition, TypeCompareKind.ConsiderEverything))
			{
				return ThreeState.False;
			}
			return ThreeState.Unknown;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000401EC File Offset: 0x0003E3EC
		private bool HasVariantConversionNoCycleCheck(NamedTypeSymbol source, NamedTypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance2 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<TypeWithAnnotations> instance3 = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			try
			{
				source.OriginalDefinition.GetAllTypeArguments(instance, ref useSiteInfo);
				source.GetAllTypeArguments(instance2, ref useSiteInfo);
				destination.GetAllTypeArguments(instance3, ref useSiteInfo);
				for (int i = 0; i < instance.Count; i++)
				{
					TypeWithAnnotations typeWithAnnotations = instance2[i];
					TypeWithAnnotations typeWithAnnotations2 = instance3[i];
					if (!this.HasIdentityConversionInternal(typeWithAnnotations.Type, typeWithAnnotations2.Type) || !this.HasTopLevelNullabilityIdentityConversion(typeWithAnnotations, typeWithAnnotations2))
					{
						TypeParameterSymbol typeParameterSymbol = (TypeParameterSymbol)instance[i].Type;
						switch (typeParameterSymbol.Variance)
						{
						case VarianceKind.None:
							if (ConversionsBase.<HasVariantConversionNoCycleCheck>g__isTypeIEquatable|107_0(destination.OriginalDefinition) && TypeSymbol.Equals(typeWithAnnotations2.Type, typeWithAnnotations.Type, TypeCompareKind.AllNullableIgnoreOptions) && this.HasAnyNullabilityImplicitConversion(typeWithAnnotations2, typeWithAnnotations))
							{
								return true;
							}
							return false;
						case VarianceKind.Out:
							if (!this.HasImplicitReferenceConversion(typeWithAnnotations, typeWithAnnotations2, ref useSiteInfo))
							{
								return false;
							}
							break;
						case VarianceKind.In:
							if (!this.HasImplicitReferenceConversion(typeWithAnnotations2, typeWithAnnotations, ref useSiteInfo))
							{
								return false;
							}
							break;
						default:
							throw ExceptionUtilities.UnexpectedValue(typeParameterSymbol.Variance);
						}
					}
				}
			}
			finally
			{
				instance.Free();
				instance2.Free();
				instance3.Free();
			}
			return true;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00040350 File Offset: 0x0003E550
		private bool HasImplicitBoxingTypeParameterConversion(TypeParameterSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return !source.IsReferenceType && (this.HasImplicitEffectiveBaseConversion(source, destination, ref useSiteInfo) || this.HasImplicitEffectiveInterfaceSetConversion(source, destination, ref useSiteInfo) || (destination.TypeKind == TypeKind.TypeParameter && source.DependsOn((TypeParameterSymbol)destination)) || destination.Kind == SymbolKind.DynamicType);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000403A8 File Offset: 0x0003E5A8
		public bool HasBoxingConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.TypeKind == TypeKind.TypeParameter && this.HasImplicitBoxingTypeParameterConversion((TypeParameterSymbol)source, destination, ref useSiteInfo))
			{
				return true;
			}
			if (!source.IsValueType || !destination.IsReferenceType)
			{
				return false;
			}
			if (source.IsNullableType())
			{
				return this.HasBoxingConversion(source.GetNullableUnderlyingType(), destination, ref useSiteInfo);
			}
			if (source.IsRestrictedType(false))
			{
				return false;
			}
			if (destination.Kind == SymbolKind.DynamicType)
			{
				return !source.IsPointerOrFunctionPointer();
			}
			return this.IsBaseClass(source, destination, ref useSiteInfo) || this.HasAnyBaseInterfaceConversion(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00040434 File Offset: 0x0003E634
		internal static bool HasImplicitPointerToVoidConversion(TypeSymbol source, TypeSymbol destination)
		{
			if (source.IsPointerOrFunctionPointer())
			{
				PointerTypeSymbol pointerTypeSymbol = destination as PointerTypeSymbol;
				if (pointerTypeSymbol != null)
				{
					TypeSymbol pointedAtType = pointerTypeSymbol.PointedAtType;
					if (pointedAtType != null)
					{
						return pointedAtType.SpecialType == SpecialType.System_Void;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0004046C File Offset: 0x0003E66C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal bool HasImplicitPointerConversion(TypeSymbol source, TypeSymbol destination, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			FunctionPointerTypeSymbol functionPointerTypeSymbol = source as FunctionPointerTypeSymbol;
			if (functionPointerTypeSymbol != null)
			{
				FunctionPointerMethodSymbol signature = functionPointerTypeSymbol.Signature;
				if (signature != null)
				{
					functionPointerTypeSymbol = destination as FunctionPointerTypeSymbol;
					if (functionPointerTypeSymbol != null)
					{
						FunctionPointerMethodSymbol signature2 = functionPointerTypeSymbol.Signature;
						if (signature2 != null)
						{
							if (signature.ParameterCount != signature2.ParameterCount || signature.CallingConvention != signature2.CallingConvention)
							{
								return false;
							}
							if (signature.CallingConvention == CallingConvention.Unmanaged && !signature.GetCallingConventionModifiers().SetEquals(signature2.GetCallingConventionModifiers()))
							{
								return false;
							}
							for (int i = 0; i < signature.ParameterCount; i++)
							{
								ParameterSymbol parameterSymbol = signature.Parameters[i];
								ParameterSymbol parameterSymbol2 = signature2.Parameters[i];
								if (parameterSymbol.RefKind != parameterSymbol2.RefKind)
								{
									return false;
								}
								if (!this.<HasImplicitPointerConversion>g__hasConversion|111_0(parameterSymbol.RefKind, signature2.Parameters[i].TypeWithAnnotations, signature.Parameters[i].TypeWithAnnotations, ref useSiteInfo))
								{
									return false;
								}
							}
							return signature.RefKind == signature2.RefKind && this.<HasImplicitPointerConversion>g__hasConversion|111_0(signature.RefKind, signature.ReturnTypeWithAnnotations, signature2.ReturnTypeWithAnnotations, ref useSiteInfo);
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0004058C File Offset: 0x0003E78C
		private bool HasIdentityOrReferenceConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.HasIdentityConversionInternal(source, destination) || this.HasImplicitReferenceConversion(source, destination, ref useSiteInfo) || this.HasExplicitReferenceConversion(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000405B8 File Offset: 0x0003E7B8
		private bool HasExplicitReferenceConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source.SpecialType == SpecialType.System_Object)
			{
				if (destination.IsReferenceType)
				{
					return true;
				}
			}
			else if (source.Kind == SymbolKind.DynamicType && destination.IsReferenceType)
			{
				return true;
			}
			return (destination.IsClassType() && this.IsBaseClass(destination, source, ref useSiteInfo)) || (source.IsClassType() && destination.IsInterfaceType() && !source.IsSealed && !this.HasAnyBaseInterfaceConversion(source, destination, ref useSiteInfo)) || (source.IsInterfaceType() && destination.IsClassType() && (!destination.IsSealed || this.HasAnyBaseInterfaceConversion(destination, source, ref useSiteInfo))) || (source.IsInterfaceType() && destination.IsInterfaceType() && !this.HasImplicitConversionToInterface(source, destination, ref useSiteInfo)) || this.HasExplicitArrayConversion(source, destination, ref useSiteInfo) || this.HasExplicitDelegateConversion(source, destination, ref useSiteInfo) || this.HasExplicitReferenceTypeParameterConversion(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00040690 File Offset: 0x0003E890
		private bool HasExplicitReferenceTypeParameterConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeParameterSymbol typeParameterSymbol = source as TypeParameterSymbol;
			TypeParameterSymbol typeParameterSymbol2 = destination as TypeParameterSymbol;
			if (typeParameterSymbol2 != null && typeParameterSymbol2.IsReferenceType)
			{
				for (NamedTypeSymbol namedTypeSymbol = typeParameterSymbol2.EffectiveBaseClass(ref useSiteInfo); namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
				{
					if (this.HasIdentityConversionInternal(namedTypeSymbol, source))
					{
						return true;
					}
				}
			}
			return (typeParameterSymbol2 != null && source.IsInterfaceType() && typeParameterSymbol2.IsReferenceType) || (typeParameterSymbol != null && typeParameterSymbol.IsReferenceType && destination.IsInterfaceType() && !this.HasImplicitReferenceTypeParameterConversion(typeParameterSymbol, destination, ref useSiteInfo)) || (typeParameterSymbol != null && typeParameterSymbol2 != null && typeParameterSymbol2.IsReferenceType && typeParameterSymbol2.DependsOn(typeParameterSymbol));
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00040728 File Offset: 0x0003E928
		private bool HasUnboxingTypeParameterConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeParameterSymbol typeParameterSymbol = source as TypeParameterSymbol;
			TypeParameterSymbol typeParameterSymbol2 = destination as TypeParameterSymbol;
			if (typeParameterSymbol2 != null && !typeParameterSymbol2.IsReferenceType)
			{
				for (NamedTypeSymbol namedTypeSymbol = typeParameterSymbol2.EffectiveBaseClass(ref useSiteInfo); namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
				{
					if (TypeSymbol.Equals(namedTypeSymbol, source, TypeCompareKind.ConsiderEverything))
					{
						return true;
					}
				}
			}
			return (source.IsInterfaceType() && typeParameterSymbol2 != null && !typeParameterSymbol2.IsReferenceType) || (typeParameterSymbol != null && !typeParameterSymbol.IsReferenceType && destination.IsInterfaceType() && !this.HasImplicitReferenceTypeParameterConversion(typeParameterSymbol, destination, ref useSiteInfo)) || (typeParameterSymbol != null && typeParameterSymbol2 != null && !typeParameterSymbol2.IsReferenceType && typeParameterSymbol2.DependsOn(typeParameterSymbol));
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000407C0 File Offset: 0x0003E9C0
		private bool HasExplicitDelegateConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (destination.IsDelegateType())
			{
				if (source.SpecialType == SpecialType.System_Delegate || source.SpecialType == SpecialType.System_MulticastDelegate)
				{
					return true;
				}
				if (this.HasImplicitConversionToInterface(this.corLibrary.GetDeclaredSpecialType(SpecialType.System_Delegate), source, ref useSiteInfo))
				{
					return true;
				}
			}
			if (!source.IsDelegateType() || !destination.IsDelegateType())
			{
				return false;
			}
			if (!TypeSymbol.Equals(source.OriginalDefinition, destination.OriginalDefinition, TypeCompareKind.ConsiderEverything))
			{
				return false;
			}
			NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)source;
			NamedTypeSymbol namedTypeSymbol2 = (NamedTypeSymbol)destination;
			NamedTypeSymbol originalDefinition = namedTypeSymbol.OriginalDefinition;
			if (this.HasIdentityConversionInternal(source, destination))
			{
				return false;
			}
			if (this.HasDelegateVarianceConversion(source, destination, ref useSiteInfo))
			{
				return false;
			}
			ImmutableArray<TypeWithAnnotations> immutableArray = namedTypeSymbol.TypeArgumentsWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			ImmutableArray<TypeWithAnnotations> immutableArray2 = namedTypeSymbol2.TypeArgumentsWithDefinitionUseSiteDiagnostics(ref useSiteInfo);
			for (int i = 0; i < immutableArray.Length; i++)
			{
				TypeSymbol type = immutableArray[i].Type;
				TypeSymbol type2 = immutableArray2[i].Type;
				switch (originalDefinition.TypeParameters[i].Variance)
				{
				case VarianceKind.None:
					if (!this.HasIdentityConversionInternal(type, type2))
					{
						return false;
					}
					break;
				case VarianceKind.Out:
					if (!this.HasIdentityOrReferenceConversion(type, type2, ref useSiteInfo))
					{
						return false;
					}
					break;
				case VarianceKind.In:
				{
					bool flag = this.HasIdentityConversionInternal(type, type2);
					bool flag2 = type.IsReferenceType && type2.IsReferenceType;
					if (!flag && !flag2)
					{
						return false;
					}
					break;
				}
				}
			}
			return true;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00040920 File Offset: 0x0003EB20
		private bool HasExplicitArrayConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayTypeSymbol arrayTypeSymbol = source as ArrayTypeSymbol;
			ArrayTypeSymbol arrayTypeSymbol2 = destination as ArrayTypeSymbol;
			if (arrayTypeSymbol != null && arrayTypeSymbol2 != null)
			{
				return arrayTypeSymbol.HasSameShapeAs(arrayTypeSymbol2) && this.HasExplicitReferenceConversion(arrayTypeSymbol.ElementType, arrayTypeSymbol2.ElementType, ref useSiteInfo);
			}
			if (arrayTypeSymbol2 != null)
			{
				if (source.SpecialType == SpecialType.System_Array)
				{
					return true;
				}
				foreach (NamedTypeSymbol namedTypeSymbol in this.corLibrary.GetDeclaredSpecialType(SpecialType.System_Array).AllInterfacesWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
				{
					if (this.HasIdentityConversionInternal(namedTypeSymbol, source))
					{
						return true;
					}
				}
			}
			if (arrayTypeSymbol != null && arrayTypeSymbol.IsSZArray && destination.IsPossibleArrayGenericInterface() && this.HasExplicitReferenceConversion(arrayTypeSymbol.ElementType, ((NamedTypeSymbol)destination).TypeArgumentWithDefinitionUseSiteDiagnostics(0, ref useSiteInfo).Type, ref useSiteInfo))
			{
				return true;
			}
			if (arrayTypeSymbol2 != null && arrayTypeSymbol2.IsSZArray)
			{
				SpecialType specialType = source.OriginalDefinition.SpecialType;
				if (specialType == SpecialType.System_Collections_Generic_IList_T || specialType == SpecialType.System_Collections_Generic_ICollection_T || specialType == SpecialType.System_Collections_Generic_IEnumerable_T || specialType == SpecialType.System_Collections_Generic_IReadOnlyList_T || specialType == SpecialType.System_Collections_Generic_IReadOnlyCollection_T)
				{
					TypeSymbol type = ((NamedTypeSymbol)source).TypeArgumentWithDefinitionUseSiteDiagnostics(0, ref useSiteInfo).Type;
					TypeSymbol elementType = arrayTypeSymbol2.ElementType;
					if (this.HasIdentityConversionInternal(type, elementType))
					{
						return true;
					}
					if (this.HasImplicitReferenceConversion(type, elementType, ref useSiteInfo))
					{
						return true;
					}
					if (this.HasExplicitReferenceConversion(type, elementType, ref useSiteInfo))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00040A6C File Offset: 0x0003EC6C
		private bool HasUnboxingConversion(TypeSymbol source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (destination.IsPointerOrFunctionPointer())
			{
				return false;
			}
			if (destination.IsRestrictedType(false))
			{
				return false;
			}
			SpecialType specialType = source.SpecialType;
			return ((specialType == SpecialType.System_Object || specialType == SpecialType.System_ValueType) && destination.IsValueType && !destination.IsNullableType()) || (source.IsInterfaceType() && destination.IsValueType && !destination.IsNullableType() && this.HasBoxingConversion(destination, source, ref useSiteInfo)) || (source.SpecialType == SpecialType.System_Enum && destination.IsEnumType()) || (source.IsReferenceType && destination.IsNullableType() && this.HasUnboxingConversion(source, destination.GetNullableUnderlyingType(), ref useSiteInfo)) || this.HasUnboxingTypeParameterConversion(source, destination, ref useSiteInfo);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00040B17 File Offset: 0x0003ED17
		private static bool HasPointerToPointerConversion(TypeSymbol source, TypeSymbol destination)
		{
			return source.IsPointerOrFunctionPointer() && destination.IsPointerOrFunctionPointer();
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00040B29 File Offset: 0x0003ED29
		private static bool HasPointerToIntegerConversion(TypeSymbol source, TypeSymbol destination)
		{
			return source.IsPointerOrFunctionPointer() && ConversionsBase.IsIntegerTypeSupportingPointerConversions(destination.StrippedType());
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00040B40 File Offset: 0x0003ED40
		private static bool HasIntegerToPointerConversion(TypeSymbol source, TypeSymbol destination)
		{
			return destination.IsPointerOrFunctionPointer() && ConversionsBase.IsIntegerTypeSupportingPointerConversions(source);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00040B54 File Offset: 0x0003ED54
		private static bool IsIntegerTypeSupportingPointerConversions(TypeSymbol type)
		{
			SpecialType specialType = type.SpecialType;
			return specialType - SpecialType.System_SByte <= 7 || (specialType - SpecialType.System_IntPtr <= 1 && type.IsNativeIntegerType);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00040B82 File Offset: 0x0003ED82
		private static TypeSymbol GetUnderlyingEffectiveType(TypeSymbol type, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (type != null)
			{
				type = type.StrippedType();
				if (type.IsTypeParameter())
				{
					type = ((TypeParameterSymbol)type).EffectiveBaseClass(ref useSiteInfo);
				}
			}
			return type;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		public static void AddTypesParticipatingInUserDefinedConversion(ArrayBuilder<NamedTypeSymbol> result, TypeSymbol type, bool includeBaseTypes, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (type == null)
			{
				return;
			}
			bool flag = result.Count > 0;
			if (type.IsClassType() || type.IsStructType())
			{
				NamedTypeSymbol namedTypeSymbol = (NamedTypeSymbol)type;
				if (!flag || !ConversionsBase.HasIdentityConversionToAny<NamedTypeSymbol>(namedTypeSymbol, result))
				{
					result.Add(namedTypeSymbol);
				}
			}
			if (!includeBaseTypes)
			{
				return;
			}
			for (NamedTypeSymbol namedTypeSymbol2 = type.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo); namedTypeSymbol2 != null; namedTypeSymbol2 = namedTypeSymbol2.BaseTypeWithDefinitionUseSiteDiagnostics(ref useSiteInfo))
			{
				if (!flag || !ConversionsBase.HasIdentityConversionToAny<NamedTypeSymbol>(namedTypeSymbol2, result))
				{
					result.Add(namedTypeSymbol2);
				}
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00040C1C File Offset: 0x0003EE1C
		private UserDefinedConversionResult AnalyzeExplicitUserDefinedConversions(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			ConversionsBase.ComputeUserDefinedExplicitConversionTypeSet(source, target, instance, ref useSiteInfo);
			ArrayBuilder<UserDefinedConversionAnalysis> instance2 = ArrayBuilder<UserDefinedConversionAnalysis>.GetInstance();
			this.ComputeApplicableUserDefinedExplicitConversionSet(sourceExpression, source, target, instance, instance2, ref useSiteInfo);
			instance.Free();
			ImmutableArray<UserDefinedConversionAnalysis> immutableArray = instance2.ToImmutableAndFree();
			if (immutableArray.Length == 0)
			{
				return UserDefinedConversionResult.NoApplicableOperators(immutableArray);
			}
			TypeSymbol typeSymbol = this.MostSpecificSourceTypeForExplicitUserDefinedConversion(immutableArray, sourceExpression, source, ref useSiteInfo);
			if (typeSymbol == null)
			{
				return UserDefinedConversionResult.NoBestSourceType(immutableArray);
			}
			TypeSymbol typeSymbol2 = this.MostSpecificTargetTypeForExplicitUserDefinedConversion(immutableArray, target, ref useSiteInfo);
			if (typeSymbol2 == null)
			{
				return UserDefinedConversionResult.NoBestTargetType(immutableArray);
			}
			int? num = ConversionsBase.MostSpecificConversionOperator(typeSymbol, typeSymbol2, immutableArray);
			if (num == null)
			{
				return UserDefinedConversionResult.Ambiguous(immutableArray);
			}
			return UserDefinedConversionResult.Valid(immutableArray, num.Value);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00040CC0 File Offset: 0x0003EEC0
		private static void ComputeUserDefinedExplicitConversionTypeSet(TypeSymbol source, TypeSymbol target, ArrayBuilder<NamedTypeSymbol> d, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol underlyingEffectiveType = ConversionsBase.GetUnderlyingEffectiveType(source, ref useSiteInfo);
			TypeSymbol underlyingEffectiveType2 = ConversionsBase.GetUnderlyingEffectiveType(target, ref useSiteInfo);
			ConversionsBase.AddTypesParticipatingInUserDefinedConversion(d, underlyingEffectiveType, true, ref useSiteInfo);
			ConversionsBase.AddTypesParticipatingInUserDefinedConversion(d, underlyingEffectiveType2, true, ref useSiteInfo);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00040CF0 File Offset: 0x0003EEF0
		private void ComputeApplicableUserDefinedExplicitConversionSet(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol target, ArrayBuilder<NamedTypeSymbol> d, ArrayBuilder<UserDefinedConversionAnalysis> u, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			foreach (NamedTypeSymbol namedTypeSymbol in d)
			{
				this.AddUserDefinedConversionsToExplicitCandidateSet(sourceExpression, source, target, u, namedTypeSymbol, "op_Explicit", ref useSiteInfo);
				this.AddUserDefinedConversionsToExplicitCandidateSet(sourceExpression, source, target, u, namedTypeSymbol, "op_Implicit", ref useSiteInfo);
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00040D40 File Offset: 0x0003EF40
		private void AddUserDefinedConversionsToExplicitCandidateSet(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol target, ArrayBuilder<UserDefinedConversionAnalysis> u, NamedTypeSymbol declaringType, string operatorName, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if ((source != null && source.IsInterfaceType()) || target.IsInterfaceType())
			{
				return;
			}
			foreach (MethodSymbol methodSymbol in declaringType.GetOperators(operatorName))
			{
				if (!methodSymbol.ReturnsVoid && methodSymbol.ParameterCount == 1 && methodSymbol.ReturnType.TypeKind != TypeKind.Error)
				{
					TypeSymbol typeSymbol = methodSymbol.GetParameterType(0);
					TypeSymbol typeSymbol2 = methodSymbol.ReturnType;
					Conversion conversion = this.EncompassingExplicitConversion(sourceExpression, source, typeSymbol, ref useSiteInfo);
					Conversion conversion2 = this.EncompassingExplicitConversion(null, typeSymbol2, target, ref useSiteInfo);
					if (!conversion.Exists && source != null && source.IsNullableType() && this.EncompassingExplicitConversion(null, source.GetNullableUnderlyingType(), typeSymbol, ref useSiteInfo).Exists)
					{
						conversion = this.ClassifyBuiltInConversion(source, typeSymbol, ref useSiteInfo);
					}
					if (!conversion2.Exists && target != null && target.IsNullableType() && this.EncompassingExplicitConversion(null, typeSymbol2, target.GetNullableUnderlyingType(), ref useSiteInfo).Exists)
					{
						conversion2 = this.ClassifyBuiltInConversion(typeSymbol2, target, ref useSiteInfo);
					}
					if (conversion.Exists && conversion2.Exists)
					{
						if (source != null && source.IsNullableType() && typeSymbol.IsNonNullableValueType() && target.CanBeAssignedNull())
						{
							TypeSymbol typeSymbol3 = this.MakeNullableType(typeSymbol);
							TypeSymbol typeSymbol4 = (typeSymbol2.IsNonNullableValueType() ? this.MakeNullableType(typeSymbol2) : typeSymbol2);
							Conversion conversion3 = this.EncompassingExplicitConversion(sourceExpression, source, typeSymbol3, ref useSiteInfo);
							Conversion conversion4 = this.EncompassingExplicitConversion(null, typeSymbol4, target, ref useSiteInfo);
							u.Add(UserDefinedConversionAnalysis.Lifted(methodSymbol, conversion3, conversion4, typeSymbol3, typeSymbol4));
						}
						else
						{
							if (target.IsNullableType() && typeSymbol2.IsNonNullableValueType())
							{
								typeSymbol2 = this.MakeNullableType(typeSymbol2);
								conversion2 = this.EncompassingExplicitConversion(null, typeSymbol2, target, ref useSiteInfo);
							}
							if (source != null && source.IsNullableType() && typeSymbol.IsNonNullableValueType())
							{
								typeSymbol = this.MakeNullableType(typeSymbol);
								conversion = this.EncompassingExplicitConversion(null, typeSymbol, source, ref useSiteInfo);
							}
							u.Add(UserDefinedConversionAnalysis.Normal(methodSymbol, conversion, conversion2, typeSymbol, typeSymbol2));
						}
					}
				}
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00040F44 File Offset: 0x0003F144
		private TypeSymbol MostSpecificSourceTypeForExplicitUserDefinedConversion(ImmutableArray<UserDefinedConversionAnalysis> u, BoundExpression sourceExpression, TypeSymbol source, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source != null)
			{
				if (u.Any<UserDefinedConversionAnalysis>((UserDefinedConversionAnalysis conv) => TypeSymbol.Equals(conv.FromType, source, TypeCompareKind.ConsiderEverything)))
				{
					return source;
				}
				CompoundUseSiteInfo<AssemblySymbol> inLambdaUseSiteInfo = useSiteInfo;
				Func<UserDefinedConversionAnalysis, bool> func = (UserDefinedConversionAnalysis conv) => this.IsEncompassedBy(sourceExpression, source, conv.FromType, ref inLambdaUseSiteInfo);
				if (u.Any<UserDefinedConversionAnalysis>(func))
				{
					TypeSymbol typeSymbol = this.MostEncompassedType<UserDefinedConversionAnalysis>(u, func, (UserDefinedConversionAnalysis conv) => conv.FromType, ref inLambdaUseSiteInfo);
					useSiteInfo = inLambdaUseSiteInfo;
					return typeSymbol;
				}
				useSiteInfo = inLambdaUseSiteInfo;
			}
			return this.MostEncompassingType<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => conv.FromType, ref useSiteInfo);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0004103C File Offset: 0x0003F23C
		private TypeSymbol MostSpecificTargetTypeForExplicitUserDefinedConversion(ImmutableArray<UserDefinedConversionAnalysis> u, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (u.Any<UserDefinedConversionAnalysis>((UserDefinedConversionAnalysis conv) => TypeSymbol.Equals(conv.ToType, target, TypeCompareKind.ConsiderEverything)))
			{
				return target;
			}
			CompoundUseSiteInfo<AssemblySymbol> inLambdaUseSiteInfo = useSiteInfo;
			Func<UserDefinedConversionAnalysis, bool> func = (UserDefinedConversionAnalysis conv) => this.IsEncompassedBy(null, conv.ToType, target, ref inLambdaUseSiteInfo);
			if (u.Any<UserDefinedConversionAnalysis>(func))
			{
				TypeSymbol typeSymbol = this.MostEncompassingType<UserDefinedConversionAnalysis>(u, func, (UserDefinedConversionAnalysis conv) => conv.ToType, ref inLambdaUseSiteInfo);
				useSiteInfo = inLambdaUseSiteInfo;
				return typeSymbol;
			}
			useSiteInfo = inLambdaUseSiteInfo;
			return this.MostEncompassedType<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => conv.ToType, ref useSiteInfo);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00041108 File Offset: 0x0003F308
		private Conversion EncompassingExplicitConversion(BoundExpression expr, TypeSymbol a, TypeSymbol b, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyStandardConversion(expr, a, b, ref useSiteInfo);
			if (!conversion.IsEnumeration)
			{
				return conversion;
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00041134 File Offset: 0x0003F334
		private UserDefinedConversionResult AnalyzeImplicitUserDefinedConversions(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			ConversionsBase.ComputeUserDefinedImplicitConversionTypeSet(source, target, instance, ref useSiteInfo);
			ArrayBuilder<UserDefinedConversionAnalysis> instance2 = ArrayBuilder<UserDefinedConversionAnalysis>.GetInstance();
			this.ComputeApplicableUserDefinedImplicitConversionSet(sourceExpression, source, target, instance, instance2, ref useSiteInfo, false);
			instance.Free();
			ImmutableArray<UserDefinedConversionAnalysis> immutableArray = instance2.ToImmutableAndFree();
			if (immutableArray.Length == 0)
			{
				return UserDefinedConversionResult.NoApplicableOperators(immutableArray);
			}
			TypeSymbol typeSymbol = this.MostSpecificSourceTypeForImplicitUserDefinedConversion(immutableArray, source, ref useSiteInfo);
			if (typeSymbol == null)
			{
				return UserDefinedConversionResult.NoBestSourceType(immutableArray);
			}
			TypeSymbol typeSymbol2 = this.MostSpecificTargetTypeForImplicitUserDefinedConversion(immutableArray, target, ref useSiteInfo);
			if (typeSymbol2 == null)
			{
				return UserDefinedConversionResult.NoBestTargetType(immutableArray);
			}
			int? num = ConversionsBase.MostSpecificConversionOperator(typeSymbol, typeSymbol2, immutableArray);
			if (num == null)
			{
				return UserDefinedConversionResult.Ambiguous(immutableArray);
			}
			return UserDefinedConversionResult.Valid(immutableArray, num.Value);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000411D8 File Offset: 0x0003F3D8
		private static void ComputeUserDefinedImplicitConversionTypeSet(TypeSymbol s, TypeSymbol t, ArrayBuilder<NamedTypeSymbol> d, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			TypeSymbol underlyingEffectiveType = ConversionsBase.GetUnderlyingEffectiveType(s, ref useSiteInfo);
			TypeSymbol underlyingEffectiveType2 = ConversionsBase.GetUnderlyingEffectiveType(t, ref useSiteInfo);
			ConversionsBase.AddTypesParticipatingInUserDefinedConversion(d, underlyingEffectiveType, true, ref useSiteInfo);
			ConversionsBase.AddTypesParticipatingInUserDefinedConversion(d, underlyingEffectiveType2, false, ref useSiteInfo);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00041208 File Offset: 0x0003F408
		private void ComputeApplicableUserDefinedImplicitConversionSet(BoundExpression sourceExpression, TypeSymbol source, TypeSymbol target, ArrayBuilder<NamedTypeSymbol> d, ArrayBuilder<UserDefinedConversionAnalysis> u, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool allowAnyTarget = false)
		{
			if ((source != null && source.IsInterfaceType()) || (target != null && target.IsInterfaceType()))
			{
				return;
			}
			foreach (NamedTypeSymbol namedTypeSymbol in d)
			{
				foreach (MethodSymbol methodSymbol in namedTypeSymbol.GetOperators("op_Implicit"))
				{
					if (!methodSymbol.ReturnsVoid && methodSymbol.ParameterCount == 1)
					{
						TypeSymbol parameterType = methodSymbol.GetParameterType(0);
						TypeSymbol typeSymbol = methodSymbol.ReturnType;
						Conversion conversion = this.EncompassingImplicitConversion(sourceExpression, source, parameterType, ref useSiteInfo);
						Conversion conversion2 = (allowAnyTarget ? Conversion.Identity : this.EncompassingImplicitConversion(null, typeSymbol, target, ref useSiteInfo));
						if (conversion.Exists && conversion2.Exists)
						{
							if (target != null && target.IsNullableType() && typeSymbol.IsNonNullableValueType())
							{
								typeSymbol = this.MakeNullableType(typeSymbol);
								conversion2 = (allowAnyTarget ? Conversion.Identity : this.EncompassingImplicitConversion(null, typeSymbol, target, ref useSiteInfo));
							}
							u.Add(UserDefinedConversionAnalysis.Normal(methodSymbol, conversion, conversion2, parameterType, typeSymbol));
						}
						else if (source != null && source.IsNullableType() && parameterType.IsNonNullableValueType() && (allowAnyTarget || target.CanBeAssignedNull()))
						{
							TypeSymbol typeSymbol2 = this.MakeNullableType(parameterType);
							TypeSymbol typeSymbol3 = (typeSymbol.IsNonNullableValueType() ? this.MakeNullableType(typeSymbol) : typeSymbol);
							Conversion conversion3 = this.EncompassingImplicitConversion(sourceExpression, source, typeSymbol2, ref useSiteInfo);
							Conversion conversion4 = ((!allowAnyTarget) ? this.EncompassingImplicitConversion(null, typeSymbol3, target, ref useSiteInfo) : Conversion.Identity);
							if (conversion3.Exists && conversion4.Exists)
							{
								u.Add(UserDefinedConversionAnalysis.Lifted(methodSymbol, conversion3, conversion4, typeSymbol2, typeSymbol3));
							}
						}
					}
				}
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000413C0 File Offset: 0x0003F5C0
		private TypeSymbol MostSpecificSourceTypeForImplicitUserDefinedConversion(ImmutableArray<UserDefinedConversionAnalysis> u, TypeSymbol source, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (source != null && u.Any<UserDefinedConversionAnalysis>((UserDefinedConversionAnalysis conv) => TypeSymbol.Equals(conv.FromType, source, TypeCompareKind.ConsiderEverything)))
			{
				return source;
			}
			return this.MostEncompassedType<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => conv.FromType, ref useSiteInfo);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00041424 File Offset: 0x0003F624
		private TypeSymbol MostSpecificTargetTypeForImplicitUserDefinedConversion(ImmutableArray<UserDefinedConversionAnalysis> u, TypeSymbol target, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (u.Any<UserDefinedConversionAnalysis>((UserDefinedConversionAnalysis conv) => TypeSymbol.Equals(conv.ToType, target, TypeCompareKind.ConsiderEverything)))
			{
				return target;
			}
			return this.MostEncompassingType<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => conv.ToType, ref useSiteInfo);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00041480 File Offset: 0x0003F680
		private static int LiftingCount(UserDefinedConversionAnalysis conv)
		{
			int num = 0;
			if (!TypeSymbol.Equals(conv.FromType, conv.Operator.GetParameterType(0), TypeCompareKind.ConsiderEverything))
			{
				num++;
			}
			if (!TypeSymbol.Equals(conv.ToType, conv.Operator.ReturnType, TypeCompareKind.ConsiderEverything))
			{
				num++;
			}
			return num;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000414CB File Offset: 0x0003F6CB
		private static int? MostSpecificConversionOperator(TypeSymbol sx, TypeSymbol tx, ImmutableArray<UserDefinedConversionAnalysis> u)
		{
			return ConversionsBase.MostSpecificConversionOperator((UserDefinedConversionAnalysis conv) => TypeSymbol.Equals(conv.FromType, sx, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(conv.ToType, tx, TypeCompareKind.ConsiderEverything), u);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000414F4 File Offset: 0x0003F6F4
		private static int? MostSpecificConversionOperator(Func<UserDefinedConversionAnalysis, bool> constraint, ImmutableArray<UserDefinedConversionAnalysis> u)
		{
			BestIndex bestIndex = ConversionsBase.UniqueIndex<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => constraint.Invoke(conv) && ConversionsBase.LiftingCount(conv) == 0);
			if (bestIndex.Kind == BestIndexKind.Best)
			{
				return new int?(bestIndex.Best);
			}
			if (bestIndex.Kind == BestIndexKind.Ambiguous)
			{
				return default(int?);
			}
			BestIndex bestIndex2 = ConversionsBase.UniqueIndex<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => constraint.Invoke(conv) && ConversionsBase.LiftingCount(conv) == 1);
			if (bestIndex2.Kind == BestIndexKind.Best)
			{
				return new int?(bestIndex2.Best);
			}
			if (bestIndex2.Kind == BestIndexKind.Ambiguous)
			{
				return default(int?);
			}
			BestIndex bestIndex3 = ConversionsBase.UniqueIndex<UserDefinedConversionAnalysis>(u, (UserDefinedConversionAnalysis conv) => constraint.Invoke(conv) && ConversionsBase.LiftingCount(conv) == 2);
			if (bestIndex3.Kind == BestIndexKind.Best)
			{
				return new int?(bestIndex3.Best);
			}
			BestIndexKind kind = bestIndex3.Kind;
			return default(int?);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000415C4 File Offset: 0x0003F7C4
		private static BestIndex UniqueIndex<T>(ImmutableArray<T> items, Func<T, bool> predicate)
		{
			if (items.IsEmpty)
			{
				return BestIndex.None();
			}
			int? num = default(int?);
			for (int i = 0; i < items.Length; i++)
			{
				if (predicate.Invoke(items[i]))
				{
					if (num != null)
					{
						return BestIndex.IsAmbiguous(num.Value, i);
					}
					num = new int?(i);
				}
			}
			if (num != null)
			{
				return BestIndex.HasBest(num.Value);
			}
			return BestIndex.None();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00041648 File Offset: 0x0003F848
		private bool IsEncompassedBy(BoundExpression aExpr, TypeSymbol a, TypeSymbol b, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.EncompassingImplicitConversion(aExpr, a, b, ref useSiteInfo).Exists;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00041668 File Offset: 0x0003F868
		private Conversion EncompassingImplicitConversion(BoundExpression aExpr, TypeSymbol a, TypeSymbol b, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			Conversion conversion = this.ClassifyStandardImplicitConversion(aExpr, a, b, ref useSiteInfo);
			if (!ConversionsBase.IsEncompassingImplicitConversionKind(conversion.Kind))
			{
				return Conversion.NoConversion;
			}
			return conversion;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00041698 File Offset: 0x0003F898
		private static bool IsEncompassingImplicitConversionKind(ConversionKind kind)
		{
			switch (kind)
			{
			case ConversionKind.NoConversion:
			case ConversionKind.ImplicitEnumeration:
			case ConversionKind.ExplicitTupleLiteral:
			case ConversionKind.ExplicitTuple:
			case ConversionKind.ImplicitDynamic:
			case ConversionKind.ExplicitDynamic:
			case ConversionKind.ImplicitUserDefined:
			case ConversionKind.AnonymousFunction:
			case ConversionKind.MethodGroup:
			case ConversionKind.ExplicitNumeric:
			case ConversionKind.ExplicitEnumeration:
			case ConversionKind.ExplicitNullable:
			case ConversionKind.ExplicitReference:
			case ConversionKind.Unboxing:
			case ConversionKind.ExplicitUserDefined:
			case ConversionKind.ExplicitPointerToPointer:
			case ConversionKind.ExplicitIntegerToPointer:
			case ConversionKind.ExplicitPointerToInteger:
			case ConversionKind.IntPtr:
			case ConversionKind.InterpolatedString:
			case ConversionKind.SwitchExpression:
			case ConversionKind.ConditionalExpression:
			case ConversionKind.StackAllocToPointerType:
			case ConversionKind.StackAllocToSpanType:
				return false;
			case ConversionKind.Identity:
			case ConversionKind.ImplicitNumeric:
			case ConversionKind.ImplicitThrow:
			case ConversionKind.ImplicitTupleLiteral:
			case ConversionKind.ImplicitTuple:
			case ConversionKind.ImplicitNullable:
			case ConversionKind.NullLiteral:
			case ConversionKind.ImplicitReference:
			case ConversionKind.Boxing:
			case ConversionKind.ImplicitPointerToVoid:
			case ConversionKind.ImplicitNullToPointer:
			case ConversionKind.ImplicitConstant:
			case ConversionKind.DefaultLiteral:
				return true;
			}
			throw ExceptionUtilities.UnexpectedValue(kind);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0004175E File Offset: 0x0003F95E
		private TypeSymbol MostEncompassedType<T>(ImmutableArray<T> items, Func<T, TypeSymbol> extract, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.MostEncompassedType<T>(items, (T x) => true, extract, ref useSiteInfo);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00041788 File Offset: 0x0003F988
		private TypeSymbol MostEncompassedType<T>(ImmutableArray<T> items, Func<T, bool> valid, Func<T, TypeSymbol> extract, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			CompoundUseSiteInfo<AssemblySymbol> inLambdaUseSiteInfo = useSiteInfo;
			int? num = ConversionsBase.UniqueBestValidIndex<T>(items, valid, delegate(T left, T right)
			{
				TypeSymbol typeSymbol = extract.Invoke(left);
				TypeSymbol typeSymbol2 = extract.Invoke(right);
				if (TypeSymbol.Equals(typeSymbol, typeSymbol2, TypeCompareKind.ConsiderEverything))
				{
					return BetterResult.Equal;
				}
				bool flag = this.IsEncompassedBy(null, typeSymbol, typeSymbol2, ref inLambdaUseSiteInfo);
				bool flag2 = this.IsEncompassedBy(null, typeSymbol2, typeSymbol, ref inLambdaUseSiteInfo);
				if (flag == flag2)
				{
					return BetterResult.Neither;
				}
				if (!flag)
				{
					return BetterResult.Right;
				}
				return BetterResult.Left;
			});
			useSiteInfo = inLambdaUseSiteInfo;
			if (num != null)
			{
				return extract.Invoke(items[num.Value]);
			}
			return null;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000417FB File Offset: 0x0003F9FB
		private TypeSymbol MostEncompassingType<T>(ImmutableArray<T> items, Func<T, TypeSymbol> extract, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			return this.MostEncompassingType<T>(items, (T x) => true, extract, ref useSiteInfo);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00041828 File Offset: 0x0003FA28
		private TypeSymbol MostEncompassingType<T>(ImmutableArray<T> items, Func<T, bool> valid, Func<T, TypeSymbol> extract, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			CompoundUseSiteInfo<AssemblySymbol> inLambdaUseSiteInfo = useSiteInfo;
			int? num = ConversionsBase.UniqueBestValidIndex<T>(items, valid, delegate(T left, T right)
			{
				TypeSymbol typeSymbol = extract.Invoke(left);
				TypeSymbol typeSymbol2 = extract.Invoke(right);
				if (TypeSymbol.Equals(typeSymbol, typeSymbol2, TypeCompareKind.ConsiderEverything))
				{
					return BetterResult.Equal;
				}
				bool flag = this.IsEncompassedBy(null, typeSymbol2, typeSymbol, ref inLambdaUseSiteInfo);
				bool flag2 = this.IsEncompassedBy(null, typeSymbol, typeSymbol2, ref inLambdaUseSiteInfo);
				if (flag == flag2)
				{
					return BetterResult.Neither;
				}
				if (!flag)
				{
					return BetterResult.Right;
				}
				return BetterResult.Left;
			});
			useSiteInfo = inLambdaUseSiteInfo;
			if (num != null)
			{
				return extract.Invoke(items[num.Value]);
			}
			return null;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0004189C File Offset: 0x0003FA9C
		private static int? UniqueBestValidIndex<T>(ImmutableArray<T> items, Func<T, bool> valid, Func<T, T, BetterResult> better)
		{
			if (items.IsEmpty)
			{
				return default(int?);
			}
			int? num = default(int?);
			T t = default(T);
			for (int i = 0; i < items.Length; i++)
			{
				T t2 = items[i];
				if (valid.Invoke(t2))
				{
					if (num == null)
					{
						num = new int?(i);
						t = t2;
					}
					else
					{
						BetterResult betterResult = better.Invoke(t, t2);
						if (betterResult != BetterResult.Equal)
						{
							if (betterResult == BetterResult.Neither)
							{
								num = default(int?);
								t = default(T);
							}
							else if (betterResult == BetterResult.Right)
							{
								num = new int?(i);
								t = t2;
							}
						}
					}
				}
			}
			if (num == null)
			{
				return default(int?);
			}
			for (int j = 0; j < num.Value; j++)
			{
				T t3 = items[j];
				if (valid.Invoke(t3))
				{
					BetterResult betterResult2 = better.Invoke(t, t3);
					if (betterResult2 != BetterResult.Left && betterResult2 != BetterResult.Equal)
					{
						return default(int?);
					}
				}
			}
			return num;
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0004199C File Offset: 0x0003FB9C
		private NamedTypeSymbol MakeNullableType(TypeSymbol type)
		{
			return this.corLibrary.GetDeclaredSpecialType(SpecialType.System_Nullable_T).Construct(new TypeSymbol[] { type });
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000419BC File Offset: 0x0003FBBC
		protected UserDefinedConversionResult AnalyzeImplicitUserDefinedConversionForV6SwitchGoverningType(TypeSymbol source, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
			ConversionsBase.ComputeUserDefinedImplicitConversionTypeSet(source, null, instance, ref useSiteInfo);
			ArrayBuilder<UserDefinedConversionAnalysis> instance2 = ArrayBuilder<UserDefinedConversionAnalysis>.GetInstance();
			this.ComputeApplicableUserDefinedImplicitConversionSet(null, source, null, instance, instance2, ref useSiteInfo, true);
			instance.Free();
			ImmutableArray<UserDefinedConversionAnalysis> immutableArray = instance2.ToImmutableAndFree();
			int? num = ConversionsBase.MostSpecificConversionOperator((UserDefinedConversionAnalysis conv) => conv.ToType.IsValidV6SwitchGoverningType(true), immutableArray);
			if (num != null)
			{
				return UserDefinedConversionResult.Valid(immutableArray, num.Value);
			}
			return UserDefinedConversionResult.NoApplicableOperators(immutableArray);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00041A6F File Offset: 0x0003FC6F
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static bool <HasSpecialIntPtrConversion>g__isIntPtrOrUIntPtr|76_0(TypeSymbol type)
		{
			return (type.SpecialType == SpecialType.System_IntPtr || type.SpecialType == SpecialType.System_UIntPtr) && !type.IsNativeIntegerType;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00041A90 File Offset: 0x0003FC90
		[CompilerGenerated]
		internal static bool <HasVariantConversionNoCycleCheck>g__isTypeIEquatable|107_0(NamedTypeSymbol type)
		{
			if (type != null && type.IsInterface && type.Name == "IEquatable")
			{
				NamespaceSymbol containingNamespace = type.ContainingNamespace;
				if (containingNamespace != null && containingNamespace.Name == "System")
				{
					NamespaceSymbol containingNamespace2 = containingNamespace.ContainingNamespace;
					if (containingNamespace2 != null && containingNamespace2.IsGlobalNamespace)
					{
						Symbol containingSymbol = type.ContainingSymbol;
						if (containingSymbol != null && containingSymbol.Kind == SymbolKind.Namespace)
						{
							return type.TypeParameters.Length == 1;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00041B10 File Offset: 0x0003FD10
		[CompilerGenerated]
		private bool <HasImplicitPointerConversion>g__hasConversion|111_0(RefKind refKind, TypeWithAnnotations sourceType, TypeWithAnnotations destinationType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (refKind == RefKind.None)
			{
				return (!this.IncludeNullability || this.HasTopLevelNullabilityImplicitConversion(sourceType, destinationType)) && (this.HasIdentityOrImplicitReferenceConversion(sourceType.Type, destinationType.Type, ref useSiteInfo) || ConversionsBase.HasImplicitPointerToVoidConversion(sourceType.Type, destinationType.Type) || this.HasImplicitPointerConversion(sourceType.Type, destinationType.Type, ref useSiteInfo));
			}
			return (!this.IncludeNullability || this.HasTopLevelNullabilityIdentityConversion(sourceType, destinationType)) && ConversionsBase.HasIdentityConversion(sourceType.Type, destinationType.Type);
		}

		// Token: 0x040000E5 RID: 229
		private const int MaximumRecursionDepth = 50;

		// Token: 0x040000E6 RID: 230
		protected readonly AssemblySymbol corLibrary;

		// Token: 0x040000E7 RID: 231
		protected readonly int currentRecursionDepth;

		// Token: 0x040000E8 RID: 232
		internal readonly bool IncludeNullability;

		// Token: 0x040000E9 RID: 233
		private ConversionsBase _lazyOtherNullability;

		// Token: 0x040000EA RID: 234
		private const bool F = false;

		// Token: 0x040000EB RID: 235
		private const bool T = true;

		// Token: 0x040000EC RID: 236
		private static readonly bool[,] s_implicitNumericConversions = new bool[,]
		{
			{
				false, false, true, false, true, false, true, false, false, true,
				true, true
			},
			{
				false, false, true, true, true, true, true, true, false, true,
				true, true
			},
			{
				false, false, false, false, true, false, true, false, false, true,
				true, true
			},
			{
				false, false, false, false, true, true, true, true, false, true,
				true, true
			},
			{
				false, false, false, false, false, false, true, false, false, true,
				true, true
			},
			{
				false, false, false, false, false, false, true, true, false, true,
				true, true
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				true, true
			},
			{
				false, false, false, false, false, false, false, false, false, true,
				true, true
			},
			{
				false, false, false, true, true, true, true, true, false, true,
				true, true
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				true, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false
			},
			{
				false, false, false, false, false, false, false, false, false, false,
				false, false
			}
		};

		// Token: 0x040000ED RID: 237
		private static readonly bool[,] s_explicitNumericConversions = new bool[,]
		{
			{
				false, true, false, true, false, true, false, true, true, false,
				false, false
			},
			{
				true, false, false, false, false, false, false, false, true, false,
				false, false
			},
			{
				true, true, false, true, false, true, false, true, true, false,
				false, false
			},
			{
				true, true, true, false, false, false, false, false, true, false,
				false, false
			},
			{
				true, true, true, true, false, true, false, true, true, false,
				false, false
			},
			{
				true, true, true, true, true, false, false, false, true, false,
				false, false
			},
			{
				true, true, true, true, true, true, false, true, true, false,
				false, false
			},
			{
				true, true, true, true, true, true, true, false, true, false,
				false, false
			},
			{
				true, true, true, false, false, false, false, false, false, false,
				false, false
			},
			{
				true, true, true, true, true, true, true, true, true, false,
				false, true
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				false, true
			},
			{
				true, true, true, true, true, true, true, true, true, true,
				true, false
			}
		};

		// Token: 0x02000651 RID: 1617
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private static class ConversionEasyOut
		{
			// Token: 0x06008285 RID: 33413 RVA: 0x001DA738 File Offset: 0x001D8938
			public static ConversionKind ClassifyConversion(TypeSymbol source, TypeSymbol target)
			{
				int num = source.TypeToIndex();
				if (num < 0)
				{
					return ConversionKind.NoConversion;
				}
				int num2 = target.TypeToIndex();
				if (num2 < 0)
				{
					return ConversionKind.NoConversion;
				}
				return (ConversionKind)ConversionsBase.ConversionEasyOut.s_convkind[num, num2];
			}

			// Token: 0x04001FB3 RID: 8115
			private static readonly byte[,] s_convkind = new byte[,]
			{
				{
					2, 26, 27, 27, 27, 27, 27, 27, 27, 27,
					27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
					27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
					27, 27
				},
				{
					12, 2, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1
				},
				{
					13, 1, 2, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 10, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1
				},
				{
					13, 1, 1, 2, 23, 23, 3, 3, 23, 3,
					3, 3, 3, 3, 3, 3, 3, 1, 10, 25,
					25, 10, 10, 25, 10, 10, 10, 10, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 2, 3, 3, 3, 23, 23,
					23, 23, 3, 23, 3, 3, 3, 1, 25, 10,
					10, 10, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 2, 3, 3, 23, 23,
					23, 23, 3, 23, 3, 3, 3, 1, 25, 25,
					10, 10, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 2, 3, 23, 23,
					23, 23, 3, 23, 3, 3, 3, 1, 25, 25,
					25, 10, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 23, 2, 23, 23,
					23, 23, 23, 23, 3, 3, 3, 1, 25, 25,
					25, 25, 10, 25, 25, 25, 25, 25, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 3, 3, 3, 2, 3,
					3, 3, 3, 3, 3, 3, 3, 1, 25, 25,
					10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 3, 3, 23, 2,
					3, 3, 3, 3, 3, 3, 3, 1, 25, 25,
					25, 10, 10, 25, 10, 10, 10, 10, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 23, 3, 23, 23,
					2, 3, 23, 3, 3, 3, 3, 1, 25, 25,
					25, 25, 10, 25, 25, 10, 10, 25, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 23, 23, 23, 23,
					23, 2, 23, 23, 3, 3, 3, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 10, 25, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 23, 3, 23, 23,
					23, 23, 2, 23, 3, 3, 3, 1, 25, 25,
					25, 25, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 23, 23, 23, 23,
					23, 3, 23, 2, 3, 3, 3, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 10, 25, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 23, 23, 23, 23, 23, 23, 23,
					23, 23, 23, 23, 2, 3, 23, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 25, 25, 10,
					10, 25
				},
				{
					13, 1, 1, 23, 23, 23, 23, 23, 23, 23,
					23, 23, 23, 23, 23, 2, 23, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
					10, 25
				},
				{
					13, 1, 1, 23, 23, 23, 23, 23, 23, 23,
					23, 23, 23, 23, 23, 23, 2, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
					25, 10
				},
				{
					13, 1, 25, 1, 1, 1, 1, 1, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 2, 1, 1,
					1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
					1, 1
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 2, 25,
					25, 10, 10, 25, 10, 10, 10, 10, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 2,
					10, 10, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					2, 10, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 2, 10, 25, 25, 25, 25, 10, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 2, 25, 25, 25, 25, 25, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					10, 10, 10, 2, 10, 10, 10, 10, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 10, 10, 25, 2, 10, 10, 10, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 10, 25, 25, 2, 10, 25, 10, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 2, 25, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 10, 25, 25, 25, 25, 2, 25, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 10, 25, 2, 10,
					10, 10
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 25, 25, 2,
					10, 25
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
					2, 25
				},
				{
					13, 1, 1, 25, 25, 25, 25, 25, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 1, 25, 25,
					25, 25, 25, 25, 25, 25, 25, 25, 25, 25,
					25, 2
				}
			};
		}

		// Token: 0x02000652 RID: 1618
		// (Invoke) Token: 0x06008287 RID: 33415
		private delegate Conversion ClassifyConversionFromExpressionDelegate(ConversionsBase conversions, BoundExpression sourceExpression, TypeWithAnnotations destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool arg);

		// Token: 0x02000653 RID: 1619
		// (Invoke) Token: 0x0600828B RID: 33419
		private delegate Conversion ClassifyConversionFromTypeDelegate(ConversionsBase conversions, TypeWithAnnotations source, TypeWithAnnotations destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, bool arg);
	}
}
