using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000225 RID: 549
	internal abstract class CSharpSymbolVisitor<TResult>
	{
		// Token: 0x06002809 RID: 10249 RVA: 0x000DAC9C File Offset: 0x000D8E9C
		public virtual TResult Visit(Symbol symbol)
		{
			if (symbol != null)
			{
				return symbol.Accept<TResult>(this);
			}
			return default(TResult);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000DACC0 File Offset: 0x000D8EC0
		public virtual TResult DefaultVisit(Symbol symbol)
		{
			return default(TResult);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000DACD6 File Offset: 0x000D8ED6
		public virtual TResult VisitAlias(AliasSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000DACDF File Offset: 0x000D8EDF
		public virtual TResult VisitArrayType(ArrayTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000DACE8 File Offset: 0x000D8EE8
		public virtual TResult VisitAssembly(AssemblySymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x000DACF1 File Offset: 0x000D8EF1
		public virtual TResult VisitDynamicType(DynamicTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000DACFA File Offset: 0x000D8EFA
		public virtual TResult VisitDiscard(DiscardSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000DAD03 File Offset: 0x000D8F03
		public virtual TResult VisitEvent(EventSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000DAD0C File Offset: 0x000D8F0C
		public virtual TResult VisitField(FieldSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000DAD15 File Offset: 0x000D8F15
		public virtual TResult VisitLabel(LabelSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x000DAD1E File Offset: 0x000D8F1E
		public virtual TResult VisitLocal(LocalSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000DAD27 File Offset: 0x000D8F27
		public virtual TResult VisitMethod(MethodSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000DAD30 File Offset: 0x000D8F30
		public virtual TResult VisitModule(ModuleSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000DAD39 File Offset: 0x000D8F39
		public virtual TResult VisitNamedType(NamedTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000DAD42 File Offset: 0x000D8F42
		public virtual TResult VisitNamespace(NamespaceSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000DAD4B File Offset: 0x000D8F4B
		public virtual TResult VisitParameter(ParameterSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000DAD54 File Offset: 0x000D8F54
		public virtual TResult VisitPointerType(PointerTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000DAD5D File Offset: 0x000D8F5D
		public virtual TResult VisitFunctionPointerType(FunctionPointerTypeSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000DAD66 File Offset: 0x000D8F66
		public virtual TResult VisitProperty(PropertySymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000DAD6F File Offset: 0x000D8F6F
		public virtual TResult VisitRangeVariable(RangeVariableSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000DAD78 File Offset: 0x000D8F78
		public virtual TResult VisitTypeParameter(TypeParameterSymbol symbol)
		{
			return this.DefaultVisit(symbol);
		}
	}
}
