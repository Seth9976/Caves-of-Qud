using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200006F RID: 111
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SimpleProgramBinder : LocalScopeBinder
	{
		// Token: 0x06000A76 RID: 2678 RVA: 0x0004F109 File Offset: 0x0004D309
		public SimpleProgramBinder(Binder enclosing, SynthesizedSimpleProgramEntryPointSymbol entryPoint)
			: base(enclosing, enclosing.Flags)
		{
			this._entryPoint = entryPoint;
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0004F120 File Offset: 0x0004D320
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<LocalSymbol> BuildLocals()
		{
			ArrayBuilder<LocalSymbol> instance = ArrayBuilder<LocalSymbol>.GetInstance();
			foreach (MemberDeclarationSyntax memberDeclarationSyntax in this._entryPoint.CompilationUnit.Members)
			{
				GlobalStatementSyntax globalStatementSyntax = memberDeclarationSyntax as GlobalStatementSyntax;
				if (globalStatementSyntax != null)
				{
					base.BuildLocals(this, globalStatementSyntax.Statement, instance);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0004F17C File Offset: 0x0004D37C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<LocalFunctionSymbol> BuildLocalFunctions()
		{
			ArrayBuilder<LocalFunctionSymbol> arrayBuilder = null;
			foreach (MemberDeclarationSyntax memberDeclarationSyntax in this._entryPoint.CompilationUnit.Members)
			{
				GlobalStatementSyntax globalStatementSyntax = memberDeclarationSyntax as GlobalStatementSyntax;
				if (globalStatementSyntax != null)
				{
					base.BuildLocalFunctions(globalStatementSyntax.Statement, ref arrayBuilder);
				}
			}
			if (arrayBuilder == null)
			{
				return ImmutableArray<LocalFunctionSymbol>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0004F1DC File Offset: 0x0004D3DC
		internal override bool IsLocalFunctionsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0004F1E0 File Offset: 0x0004D3E0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<LabelSymbol> BuildLabels()
		{
			ArrayBuilder<LabelSymbol> arrayBuilder = null;
			foreach (MemberDeclarationSyntax memberDeclarationSyntax in this._entryPoint.CompilationUnit.Members)
			{
				GlobalStatementSyntax globalStatementSyntax = memberDeclarationSyntax as GlobalStatementSyntax;
				if (globalStatementSyntax != null)
				{
					LocalScopeBinder.BuildLabels(this._entryPoint, globalStatementSyntax.Statement, ref arrayBuilder);
				}
			}
			if (arrayBuilder == null)
			{
				return ImmutableArray<LabelSymbol>.Empty;
			}
			return arrayBuilder.ToImmutableAndFree();
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0004F245 File Offset: 0x0004D445
		internal override bool IsLabelsScopeBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0004F248 File Offset: 0x0004D448
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.Locals;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0004F25F File Offset: 0x0004D45F
		internal override SyntaxNode ScopeDesignator
		{
			get
			{
				return this._entryPoint.SyntaxNode;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0004F26C File Offset: 0x0004D46C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			if (this.ScopeDesignator == scopeDesignator)
			{
				return this.LocalFunctions;
			}
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000462 RID: 1122
		private readonly SynthesizedSimpleProgramEntryPointSymbol _entryPoint;
	}
}
