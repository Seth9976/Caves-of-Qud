using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000014 RID: 20
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class BuckStopsHereBinder : Binder
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0003321A File Offset: 0x0003141A
		internal BuckStopsHereBinder(CSharpCompilation compilation)
			: base(compilation)
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00033223 File Offset: 0x00031423
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ImportChain ImportChain
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00033226 File Offset: 0x00031426
		internal override QuickAttributeChecker QuickAttributeChecker
		{
			get
			{
				return QuickAttributeChecker.Predefined;
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0003322D File Offset: 0x0003142D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override SourceLocalSymbol LookupLocal(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00033230 File Offset: 0x00031430
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override LocalFunctionSymbol LookupLocalFunction(SyntaxToken nameToken)
		{
			return null;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00033233 File Offset: 0x00031433
		internal override uint LocalScopeDepth
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00033236 File Offset: 0x00031436
		protected override bool InExecutableBinder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00033239 File Offset: 0x00031439
		internal override bool IsAccessibleHelper(Symbol symbol, TypeSymbol accessThroughType, out bool failedThroughTypeCheck, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo, ConsList<TypeSymbol> basesBeingResolved)
		{
			failedThroughTypeCheck = false;
			return Binder.IsSymbolAccessibleConditional(symbol, base.Compilation.Assembly, ref useSiteInfo);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x00033251 File Offset: 0x00031451
		internal override ConstantFieldsInProgress ConstantFieldsInProgress
		{
			get
			{
				return ConstantFieldsInProgress.Empty;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00033258 File Offset: 0x00031458
		internal override ConsList<FieldSymbol> FieldsBeingBound
		{
			get
			{
				return ConsList<FieldSymbol>.Empty;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0003325F File Offset: 0x0003145F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override LocalSymbol LocalInProgress
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00033262 File Offset: 0x00031462
		protected override bool IsUnboundTypeAllowed(GenericNameSyntax syntax)
		{
			return false;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00033265 File Offset: 0x00031465
		internal override bool IsInMethodBody
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00033268 File Offset: 0x00031468
		internal override bool IsDirectlyInIterator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0003326B File Offset: 0x0003146B
		internal override bool IsIndirectlyInIterator
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0003326E File Offset: 0x0003146E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override GeneratedLabelSymbol BreakLabel
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00033271 File Offset: 0x00031471
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override GeneratedLabelSymbol ContinueLabel
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00033274 File Offset: 0x00031474
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override BoundExpression ConditionalReceiverExpression
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00033277 File Offset: 0x00031477
		internal override TypeWithAnnotations GetIteratorElementType()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0003327E File Offset: 0x0003147E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override Symbol ContainingMemberOrLambda
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00033281 File Offset: 0x00031481
		internal override bool AreNullableAnnotationsGloballyEnabled()
		{
			return base.GetGlobalAnnotationState();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00033289 File Offset: 0x00031489
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal override Binder GetBinder(SyntaxNode node)
		{
			return null;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0003328C File Offset: 0x0003148C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalSymbol> GetDeclaredLocalsForScope(SyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00033293 File Offset: 0x00031493
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<LocalFunctionSymbol> GetDeclaredLocalFunctionsForScope(CSharpSyntaxNode scopeDesignator)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0003329A File Offset: 0x0003149A
		internal override BoundStatement BindSwitchStatementCore(SwitchStatementSyntax node, Binder originalBinder, BindingDiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000332A1 File Offset: 0x000314A1
		internal override BoundExpression BindSwitchExpressionCore(SwitchExpressionSyntax node, Binder originalBinder, BindingDiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000332A8 File Offset: 0x000314A8
		internal override void BindPatternSwitchLabelForInference(CasePatternSwitchLabelSyntax node, BindingDiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000332AF File Offset: 0x000314AF
		internal override BoundSwitchExpressionArm BindSwitchExpressionArm(SwitchExpressionArmSyntax node, TypeSymbol switchGoverningType, uint switchGoverningValEscape, BindingDiagnosticBag diagnostics)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000332B6 File Offset: 0x000314B6
		internal override BoundForStatement BindForParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000332BD File Offset: 0x000314BD
		internal override BoundStatement BindForEachParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x000332C4 File Offset: 0x000314C4
		internal override BoundStatement BindForEachDeconstruction(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000332CB File Offset: 0x000314CB
		internal override BoundWhileStatement BindWhileParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x000332D2 File Offset: 0x000314D2
		internal override BoundDoStatement BindDoParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x000332D9 File Offset: 0x000314D9
		internal override BoundStatement BindUsingStatementParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x000332E0 File Offset: 0x000314E0
		internal override BoundStatement BindLockStatementParts(BindingDiagnosticBag diagnostics, Binder originalBinder)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x000332E7 File Offset: 0x000314E7
		internal override ImmutableHashSet<Symbol> LockedOrDisposedVariables
		{
			get
			{
				return ImmutableHashSet.Create<Symbol>();
			}
		}
	}
}
