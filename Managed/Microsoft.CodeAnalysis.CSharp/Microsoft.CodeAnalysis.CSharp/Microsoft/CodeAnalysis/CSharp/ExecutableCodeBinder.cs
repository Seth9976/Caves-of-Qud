using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200001C RID: 28
	internal sealed class ExecutableCodeBinder : Binder
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x000353AB File Offset: 0x000335AB
		internal ExecutableCodeBinder(SyntaxNode root, Symbol memberSymbol, Binder next, Action<Binder, SyntaxNode> binderUpdatedHandler = null)
			: this(root, memberSymbol, next, next.Flags)
		{
			this._binderUpdatedHandler = binderUpdatedHandler;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000353C4 File Offset: 0x000335C4
		internal ExecutableCodeBinder(SyntaxNode root, Symbol memberSymbol, Binder next, BinderFlags additionalFlags)
			: base(next, (next.Flags | additionalFlags) & ~(BinderFlags.InLockBody | BinderFlags.InCatchBlock | BinderFlags.InFinallyBlock | BinderFlags.InTryBlockOfTryCatch | BinderFlags.InCatchFilter | BinderFlags.InNestedFinallyBlock))
		{
			this._memberSymbol = memberSymbol;
			this._root = root;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000353EA File Offset: 0x000335EA
		internal override Symbol ContainingMemberOrLambda
		{
			get
			{
				return this._memberSymbol ?? base.Next.ContainingMemberOrLambda;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00035401 File Offset: 0x00033601
		protected override bool InExecutableBinder
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00035404 File Offset: 0x00033604
		internal Symbol MemberSymbol
		{
			get
			{
				return this._memberSymbol;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0003540C File Offset: 0x0003360C
		internal override Binder GetBinder(SyntaxNode node)
		{
			Binder binder;
			if (!this.BinderMap.TryGetValue(node, out binder))
			{
				return base.Next.GetBinder(node);
			}
			return binder;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00035438 File Offset: 0x00033638
		private void ComputeBinderMap()
		{
			SynthesizedSimpleProgramEntryPointSymbol synthesizedSimpleProgramEntryPointSymbol = this._memberSymbol as SynthesizedSimpleProgramEntryPointSymbol;
			SmallDictionary<SyntaxNode, Binder> smallDictionary;
			if (synthesizedSimpleProgramEntryPointSymbol != null && this._root == synthesizedSimpleProgramEntryPointSymbol.SyntaxNode)
			{
				SimpleProgramBinder simpleProgramBinder = new SimpleProgramBinder(this, synthesizedSimpleProgramEntryPointSymbol);
				smallDictionary = LocalBinderFactory.BuildMap(this._memberSymbol, this._root, simpleProgramBinder, this._binderUpdatedHandler);
				smallDictionary.Add(this._root, simpleProgramBinder);
			}
			else if (this._memberSymbol != null && this._root != null)
			{
				smallDictionary = LocalBinderFactory.BuildMap(this._memberSymbol, this._root, this, this._binderUpdatedHandler);
			}
			else
			{
				smallDictionary = SmallDictionary<SyntaxNode, Binder>.Empty;
			}
			Interlocked.CompareExchange<SmallDictionary<SyntaxNode, Binder>>(ref this._lazyBinderMap, smallDictionary, null);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x000354D1 File Offset: 0x000336D1
		private SmallDictionary<SyntaxNode, Binder> BinderMap
		{
			get
			{
				if (this._lazyBinderMap == null)
				{
					this.ComputeBinderMap();
				}
				return this._lazyBinderMap;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000354E8 File Offset: 0x000336E8
		public static void ValidateIteratorMethod(CSharpCompilation compilation, MethodSymbol iterator, BindingDiagnosticBag diagnostics)
		{
			if (!iterator.IsIterator)
			{
				return;
			}
			foreach (ParameterSymbol parameterSymbol in iterator.Parameters)
			{
				if (parameterSymbol.RefKind != RefKind.None)
				{
					diagnostics.Add(ErrorCode.ERR_BadIteratorArgType, parameterSymbol.Locations[0]);
				}
				else if (parameterSymbol.Type.IsUnsafe())
				{
					diagnostics.Add(ErrorCode.ERR_UnsafeIteratorArgType, parameterSymbol.Locations[0]);
				}
			}
			SynthesizedSimpleProgramEntryPointSymbol synthesizedSimpleProgramEntryPointSymbol = iterator as SynthesizedSimpleProgramEntryPointSymbol;
			Location location = ((synthesizedSimpleProgramEntryPointSymbol != null) ? synthesizedSimpleProgramEntryPointSymbol.ReturnTypeSyntax.GetLocation() : null) ?? iterator.Locations[0];
			if (iterator.IsVararg)
			{
				diagnostics.Add(ErrorCode.ERR_VarargsIterator, location);
			}
			SourceMemberMethodSymbol sourceMemberMethodSymbol = iterator as SourceMemberMethodSymbol;
			if (sourceMemberMethodSymbol == null || !sourceMemberMethodSymbol.IsUnsafe)
			{
				LocalFunctionSymbol localFunctionSymbol = iterator as LocalFunctionSymbol;
				if (localFunctionSymbol == null || !localFunctionSymbol.IsUnsafe)
				{
					goto IL_0104;
				}
			}
			if (compilation.Options.AllowUnsafe)
			{
				diagnostics.Add(ErrorCode.ERR_IllegalInnerUnsafe, location);
			}
			IL_0104:
			TypeSymbol returnType = iterator.ReturnType;
			RefKind refKind = iterator.RefKind;
			if (InMethodBinder.GetIteratorElementTypeFromReturnType(compilation, refKind, returnType, location, diagnostics).IsDefault)
			{
				if (refKind != RefKind.None)
				{
					Binder.Error(diagnostics, ErrorCode.ERR_BadIteratorReturnRef, location, new object[] { iterator });
				}
				else if (!returnType.IsErrorType())
				{
					Binder.Error(diagnostics, ErrorCode.ERR_BadIteratorReturn, location, new object[] { iterator, returnType });
				}
			}
			if (InMethodBinder.IsAsyncStreamInterface(compilation, refKind, returnType) && !iterator.IsAsync)
			{
				diagnostics.Add(ErrorCode.ERR_IteratorMustBeAsync, location, new object[] { iterator, returnType });
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly Symbol _memberSymbol;

		// Token: 0x04000052 RID: 82
		private readonly SyntaxNode _root;

		// Token: 0x04000053 RID: 83
		private readonly Action<Binder, SyntaxNode> _binderUpdatedHandler;

		// Token: 0x04000054 RID: 84
		private SmallDictionary<SyntaxNode, Binder> _lazyBinderMap;
	}
}
