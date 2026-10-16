using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000263 RID: 611
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SymbolVisitor<[Nullable(2)] TResult>
	{
		// Token: 0x0600174B RID: 5963 RVA: 0x00045E40 File Offset: 0x00044040
		[NullableContext(2)]
		public virtual TResult Visit(ISymbol symbol)
		{
			if (symbol != null)
			{
				return symbol.Accept<TResult>(this);
			}
			return default(TResult);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00045E64 File Offset: 0x00044064
		[return: Nullable(2)]
		public virtual TResult DefaultVisit(ISymbol symbol)
		{
			return default(TResult);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00045E7A File Offset: 0x0004407A
		[return: Nullable(2)]
		public virtual TResult VisitAlias(IAliasSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00045E83 File Offset: 0x00044083
		[return: Nullable(2)]
		public virtual TResult VisitArrayType(IArrayTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00045E8C File Offset: 0x0004408C
		[return: Nullable(2)]
		public virtual TResult VisitAssembly(IAssemblySymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x00045E95 File Offset: 0x00044095
		[return: Nullable(2)]
		public virtual TResult VisitDiscard(IDiscardSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00045E9E File Offset: 0x0004409E
		[return: Nullable(2)]
		public virtual TResult VisitDynamicType(IDynamicTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00045EA7 File Offset: 0x000440A7
		[return: Nullable(2)]
		public virtual TResult VisitEvent(IEventSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00045EB0 File Offset: 0x000440B0
		[return: Nullable(2)]
		public virtual TResult VisitField(IFieldSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00045EB9 File Offset: 0x000440B9
		[return: Nullable(2)]
		public virtual TResult VisitLabel(ILabelSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00045EC2 File Offset: 0x000440C2
		[return: Nullable(2)]
		public virtual TResult VisitLocal(ILocalSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00045ECB File Offset: 0x000440CB
		[return: Nullable(2)]
		public virtual TResult VisitMethod(IMethodSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00045ED4 File Offset: 0x000440D4
		[return: Nullable(2)]
		public virtual TResult VisitModule(IModuleSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00045EDD File Offset: 0x000440DD
		[return: Nullable(2)]
		public virtual TResult VisitNamedType(INamedTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00045EE6 File Offset: 0x000440E6
		[return: Nullable(2)]
		public virtual TResult VisitNamespace(INamespaceSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x00045EEF File Offset: 0x000440EF
		[return: Nullable(2)]
		public virtual TResult VisitParameter(IParameterSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00045EF8 File Offset: 0x000440F8
		[return: Nullable(2)]
		public virtual TResult VisitPointerType(IPointerTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00045F01 File Offset: 0x00044101
		[return: Nullable(2)]
		public virtual TResult VisitFunctionPointerType(IFunctionPointerTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00045F0A File Offset: 0x0004410A
		[return: Nullable(2)]
		public virtual TResult VisitProperty(IPropertySymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00045F13 File Offset: 0x00044113
		[return: Nullable(2)]
		public virtual TResult VisitRangeVariable(IRangeVariableSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00045F1C File Offset: 0x0004411C
		[return: Nullable(2)]
		public virtual TResult VisitTypeParameter(ITypeParameterSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}
	}
}
