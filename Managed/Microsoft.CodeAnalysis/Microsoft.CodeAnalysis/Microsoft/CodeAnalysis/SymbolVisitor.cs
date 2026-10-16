using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000262 RID: 610
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class SymbolVisitor
	{
		// Token: 0x06001735 RID: 5941 RVA: 0x00045D7E File Offset: 0x00043F7E
		[NullableContext(2)]
		public virtual void Visit(ISymbol symbol)
		{
			if (symbol != null)
			{
				symbol.Accept(this);
			}
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00045D8A File Offset: 0x00043F8A
		public virtual void DefaultVisit(ISymbol symbol)
		{
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00045D8C File Offset: 0x00043F8C
		public virtual void VisitAlias(IAliasSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00045D95 File Offset: 0x00043F95
		public virtual void VisitArrayType(IArrayTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00045D9E File Offset: 0x00043F9E
		public virtual void VisitAssembly(IAssemblySymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00045DA7 File Offset: 0x00043FA7
		public virtual void VisitDiscard(IDiscardSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00045DB0 File Offset: 0x00043FB0
		public virtual void VisitDynamicType(IDynamicTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00045DB9 File Offset: 0x00043FB9
		public virtual void VisitEvent(IEventSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00045DC2 File Offset: 0x00043FC2
		public virtual void VisitField(IFieldSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00045DCB File Offset: 0x00043FCB
		public virtual void VisitLabel(ILabelSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00045DD4 File Offset: 0x00043FD4
		public virtual void VisitLocal(ILocalSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00045DDD File Offset: 0x00043FDD
		public virtual void VisitMethod(IMethodSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00045DE6 File Offset: 0x00043FE6
		public virtual void VisitModule(IModuleSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00045DEF File Offset: 0x00043FEF
		public virtual void VisitNamedType(INamedTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00045DF8 File Offset: 0x00043FF8
		public virtual void VisitNamespace(INamespaceSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00045E01 File Offset: 0x00044001
		public virtual void VisitParameter(IParameterSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00045E0A File Offset: 0x0004400A
		public virtual void VisitPointerType(IPointerTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00045E13 File Offset: 0x00044013
		public virtual void VisitFunctionPointerType(IFunctionPointerTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00045E1C File Offset: 0x0004401C
		public virtual void VisitProperty(IPropertySymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00045E25 File Offset: 0x00044025
		public virtual void VisitRangeVariable(IRangeVariableSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00045E2E File Offset: 0x0004402E
		public virtual void VisitTypeParameter(ITypeParameterSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}
	}
}
