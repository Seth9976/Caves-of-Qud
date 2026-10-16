using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000043 RID: 67
	internal sealed class ScriptLocalScopeBinder : LocalScopeBinder
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x0003CD99 File Offset: 0x0003AF99
		internal ScriptLocalScopeBinder(ScriptLocalScopeBinder.Labels labels, Binder next)
			: base(next)
		{
			this._labels = labels;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0003CDA9 File Offset: 0x0003AFA9
		internal override Symbol ContainingMemberOrLambda
		{
			get
			{
				return this._labels.ScriptInitializer;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0003CDB6 File Offset: 0x0003AFB6
		protected override ImmutableArray<LabelSymbol> BuildLabels()
		{
			return this._labels.GetLabels();
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0003CDC3 File Offset: 0x0003AFC3
		internal override bool IsLabelsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0003CDC6 File Offset: 0x0003AFC6
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0003CDCD File Offset: 0x0003AFCD
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040000D7 RID: 215
		private readonly ScriptLocalScopeBinder.Labels _labels;

		// Token: 0x0200064D RID: 1613
		internal new sealed class Labels
		{
			// Token: 0x0600827B RID: 33403 RVA: 0x001DA56B File Offset: 0x001D876B
			internal Labels(SynthesizedInteractiveInitializerMethod scriptInitializer, CompilationUnitSyntax syntax)
			{
				this._scriptInitializer = scriptInitializer;
				this._syntax = syntax;
			}

			// Token: 0x1700267F RID: 9855
			// (get) Token: 0x0600827C RID: 33404 RVA: 0x001DA581 File Offset: 0x001D8781
			internal SynthesizedInteractiveInitializerMethod ScriptInitializer
			{
				get
				{
					return this._scriptInitializer;
				}
			}

			// Token: 0x0600827D RID: 33405 RVA: 0x001DA58C File Offset: 0x001D878C
			internal ImmutableArray<LabelSymbol> GetLabels()
			{
				if (new ImmutableArray<LabelSymbol>?(this._lazyLabels) == default(ImmutableArray<LabelSymbol>?))
				{
					ImmutableInterlocked.InterlockedInitialize<LabelSymbol>(ref this._lazyLabels, ScriptLocalScopeBinder.Labels.GetLabels(this._scriptInitializer, this._syntax));
				}
				return this._lazyLabels;
			}

			// Token: 0x0600827E RID: 33406 RVA: 0x001DA5D8 File Offset: 0x001D87D8
			private static ImmutableArray<LabelSymbol> GetLabels(SynthesizedInteractiveInitializerMethod scriptInitializer, CompilationUnitSyntax syntax)
			{
				ArrayBuilder<LabelSymbol> instance = ArrayBuilder<LabelSymbol>.GetInstance();
				foreach (MemberDeclarationSyntax memberDeclarationSyntax in syntax.Members)
				{
					if (memberDeclarationSyntax.Kind() == SyntaxKind.GlobalStatement)
					{
						LocalScopeBinder.BuildLabels(scriptInitializer, ((GlobalStatementSyntax)memberDeclarationSyntax).Statement, ref instance);
					}
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x04001FA3 RID: 8099
			private readonly SynthesizedInteractiveInitializerMethod _scriptInitializer;

			// Token: 0x04001FA4 RID: 8100
			private readonly CompilationUnitSyntax _syntax;

			// Token: 0x04001FA5 RID: 8101
			private ImmutableArray<LabelSymbol> _lazyLabels;
		}
	}
}
