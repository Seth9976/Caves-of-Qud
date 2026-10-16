using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000224 RID: 548
	internal abstract class CSharpSymbolVisitor
	{
		// Token: 0x060027F3 RID: 10227 RVA: 0x000DABD9 File Offset: 0x000D8DD9
		public virtual void Visit(Symbol symbol)
		{
			if (symbol != null)
			{
				symbol.Accept(this);
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000DABE5 File Offset: 0x000D8DE5
		public virtual void DefaultVisit(Symbol symbol)
		{
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000DABE7 File Offset: 0x000D8DE7
		public virtual void VisitAlias(AliasSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000DABF0 File Offset: 0x000D8DF0
		public virtual void VisitArrayType(ArrayTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000DABF9 File Offset: 0x000D8DF9
		public virtual void VisitAssembly(AssemblySymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000DAC02 File Offset: 0x000D8E02
		public virtual void VisitDynamicType(DynamicTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000DAC0B File Offset: 0x000D8E0B
		public virtual void VisitDiscard(DiscardSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000DAC14 File Offset: 0x000D8E14
		public virtual void VisitEvent(EventSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000DAC1D File Offset: 0x000D8E1D
		public virtual void VisitField(FieldSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000DAC26 File Offset: 0x000D8E26
		public virtual void VisitLabel(LabelSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000DAC2F File Offset: 0x000D8E2F
		public virtual void VisitLocal(LocalSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000DAC38 File Offset: 0x000D8E38
		public virtual void VisitMethod(MethodSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000DAC41 File Offset: 0x000D8E41
		public virtual void VisitModule(ModuleSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000DAC4A File Offset: 0x000D8E4A
		public virtual void VisitNamedType(NamedTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000DAC53 File Offset: 0x000D8E53
		public virtual void VisitNamespace(NamespaceSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000DAC5C File Offset: 0x000D8E5C
		public virtual void VisitParameter(ParameterSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000DAC65 File Offset: 0x000D8E65
		public virtual void VisitPointerType(PointerTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000DAC6E File Offset: 0x000D8E6E
		public virtual void VisitFunctionPointerType(FunctionPointerTypeSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000DAC77 File Offset: 0x000D8E77
		public virtual void VisitProperty(PropertySymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000DAC80 File Offset: 0x000D8E80
		public virtual void VisitRangeVariable(RangeVariableSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000DAC89 File Offset: 0x000D8E89
		public virtual void VisitTypeParameter(TypeParameterSymbol symbol)
		{
			this.DefaultVisit(symbol);
		}
	}
}
