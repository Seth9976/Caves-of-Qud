using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F4 RID: 500
	internal sealed class SynthesizedClosureEnvironment : SynthesizedContainer, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000B4643 File Offset: 0x000B2843
		public override TypeKind TypeKind { get; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000B464B File Offset: 0x000B284B
		internal override MethodSymbol Constructor { get; }

		// Token: 0x0600222A RID: 8746 RVA: 0x000B4654 File Offset: 0x000B2854
		internal SynthesizedClosureEnvironment(MethodSymbol topLevelMethod, MethodSymbol containingMethod, bool isStruct, SyntaxNode scopeSyntaxOpt, DebugId methodId, DebugId closureId)
			: base(SynthesizedClosureEnvironment.MakeName(scopeSyntaxOpt, methodId, closureId), containingMethod)
		{
			this.TypeKind = (isStruct ? TypeKind.Struct : TypeKind.Class);
			this._topLevelMethod = topLevelMethod;
			this.OriginalContainingMethodOpt = containingMethod;
			this.Constructor = (isStruct ? null : new SynthesizedClosureEnvironmentConstructor(this));
			this.ClosureOrdinal = closureId.Ordinal;
			if (scopeSyntaxOpt == null)
			{
				this.StaticConstructor = new SynthesizedStaticConstructor(this);
				string text = GeneratedNames.MakeCachedFrameInstanceFieldName();
				this.SingletonCache = new SynthesizedLambdaCacheFieldSymbol(this, this, text, topLevelMethod, true, true);
			}
			this.ScopeSyntaxOpt = scopeSyntaxOpt;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000B46E8 File Offset: 0x000B28E8
		internal void AddHoistedField(LambdaCapturedVariable captured)
		{
			this._membersBuilder.Add(captured);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000B46F6 File Offset: 0x000B28F6
		private static string MakeName(SyntaxNode scopeSyntaxOpt, DebugId methodId, DebugId closureId)
		{
			if (scopeSyntaxOpt == null)
			{
				return GeneratedNames.MakeStaticLambdaDisplayClassName(methodId.Ordinal, methodId.Generation);
			}
			return GeneratedNames.MakeLambdaDisplayClassName(methodId.Ordinal, methodId.Generation, closureId.Ordinal, closureId.Generation);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000B472A File Offset: 0x000B292A
		[Conditional("DEBUG")]
		private static void AssertIsClosureScopeSyntax(SyntaxNode syntaxOpt)
		{
			if (syntaxOpt == null)
			{
				return;
			}
			if (LambdaUtilities.IsClosureScope(syntaxOpt))
			{
				return;
			}
			throw ExceptionUtilities.UnexpectedValue(syntaxOpt.Kind());
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000B474C File Offset: 0x000B294C
		public override ImmutableArray<Symbol> GetMembers()
		{
			if (this._members.IsDefault)
			{
				ArrayBuilder<Symbol> membersBuilder = this._membersBuilder;
				if (this.StaticConstructor != null)
				{
					membersBuilder.Add(this.StaticConstructor);
					membersBuilder.Add(this.SingletonCache);
				}
				membersBuilder.AddRange(base.GetMembers());
				this._members = membersBuilder.ToImmutableAndFree();
				this._membersBuilder = null;
			}
			return this._members;
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000B47B2 File Offset: 0x000B29B2
		internal override IEnumerable<FieldSymbol> GetFieldsToEmit()
		{
			if (this.SingletonCache == null)
			{
				return SpecializedCollections.EmptyEnumerable<FieldSymbol>();
			}
			return SpecializedCollections.SingletonEnumerable<FieldSymbol>(this.SingletonCache);
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000B47CD File Offset: 0x000B29CD
		public override bool IsSerializable
		{
			get
			{
				return this.SingletonCache != null;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x000B47D8 File Offset: 0x000B29D8
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._topLevelMethod.ContainingSymbol;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x000B47E5 File Offset: 0x000B29E5
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x000B47E8 File Offset: 0x000B29E8
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000B47EB File Offset: 0x000B29EB
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			get
			{
				return this._topLevelMethod;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000B47F3 File Offset: 0x000B29F3
		internal override bool IsRecord
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000B47F6 File Offset: 0x000B29F6
		internal override bool IsRecordStruct
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000B47F9 File Offset: 0x000B29F9
		internal override bool HasPossibleWellKnownCloneMethod()
		{
			return false;
		}

		// Token: 0x04001092 RID: 4242
		private readonly MethodSymbol _topLevelMethod;

		// Token: 0x04001093 RID: 4243
		internal readonly SyntaxNode ScopeSyntaxOpt;

		// Token: 0x04001094 RID: 4244
		internal readonly int ClosureOrdinal;

		// Token: 0x04001095 RID: 4245
		internal readonly MethodSymbol OriginalContainingMethodOpt;

		// Token: 0x04001096 RID: 4246
		internal readonly FieldSymbol SingletonCache;

		// Token: 0x04001097 RID: 4247
		internal readonly MethodSymbol StaticConstructor;

		// Token: 0x04001098 RID: 4248
		private ArrayBuilder<Symbol> _membersBuilder = ArrayBuilder<Symbol>.GetInstance();

		// Token: 0x04001099 RID: 4249
		private ImmutableArray<Symbol> _members;
	}
}
