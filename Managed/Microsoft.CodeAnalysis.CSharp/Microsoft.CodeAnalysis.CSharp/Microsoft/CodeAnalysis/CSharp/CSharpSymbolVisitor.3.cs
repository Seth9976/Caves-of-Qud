using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000226 RID: 550
	internal abstract class CSharpSymbolVisitor<TArgument, TResult>
	{
		// Token: 0x0600281F RID: 10271 RVA: 0x000DAD8C File Offset: 0x000D8F8C
		public virtual TResult Visit(Symbol symbol, TArgument argument = default(TArgument))
		{
			if (symbol == null)
			{
				return default(TResult);
			}
			return symbol.Accept<TArgument, TResult>(this, argument);
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000DADB0 File Offset: 0x000D8FB0
		public virtual TResult DefaultVisit(Symbol symbol, TArgument argument)
		{
			return default(TResult);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000DADC6 File Offset: 0x000D8FC6
		public virtual TResult VisitAssembly(AssemblySymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000DADD0 File Offset: 0x000D8FD0
		public virtual TResult VisitModule(ModuleSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000DADDA File Offset: 0x000D8FDA
		public virtual TResult VisitNamespace(NamespaceSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000DADE4 File Offset: 0x000D8FE4
		public virtual TResult VisitNamedType(NamedTypeSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000DADEE File Offset: 0x000D8FEE
		public virtual TResult VisitArrayType(ArrayTypeSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x000DADF8 File Offset: 0x000D8FF8
		public virtual TResult VisitPointerType(PointerTypeSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000DAE02 File Offset: 0x000D9002
		public virtual TResult VisitFunctionPointerType(FunctionPointerTypeSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000DAE0C File Offset: 0x000D900C
		public virtual TResult VisitErrorType(ErrorTypeSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000DAE16 File Offset: 0x000D9016
		public virtual TResult VisitTypeParameter(TypeParameterSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000DAE20 File Offset: 0x000D9020
		public virtual TResult VisitDynamicType(DynamicTypeSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000DAE2A File Offset: 0x000D902A
		public virtual TResult VisitDiscard(DiscardSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000DAE34 File Offset: 0x000D9034
		public virtual TResult VisitMethod(MethodSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000DAE3E File Offset: 0x000D903E
		public virtual TResult VisitField(FieldSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000DAE48 File Offset: 0x000D9048
		public virtual TResult VisitProperty(PropertySymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000DAE52 File Offset: 0x000D9052
		public virtual TResult VisitEvent(EventSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000DAE5C File Offset: 0x000D905C
		public virtual TResult VisitParameter(ParameterSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x000DAE66 File Offset: 0x000D9066
		public virtual TResult VisitLocal(LocalSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000DAE70 File Offset: 0x000D9070
		public virtual TResult VisitLabel(LabelSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000DAE7A File Offset: 0x000D907A
		public virtual TResult VisitAlias(AliasSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000DAE84 File Offset: 0x000D9084
		public virtual TResult VisitRangeVariable(RangeVariableSymbol symbol, TArgument argument)
		{
			return this.DefaultVisit(symbol, argument);
		}
	}
}
