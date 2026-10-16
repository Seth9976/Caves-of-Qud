using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001F6 RID: 502
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedClosureMethod : SynthesizedMethodBaseSymbol, ISynthesizedMethodBodyImplementationSymbol, ISymbolInternal
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000B481A File Offset: 0x000B2A1A
		internal MethodSymbol TopLevelMethod { get; }

		// Token: 0x0600223C RID: 8764 RVA: 0x000B4824 File Offset: 0x000B2A24
		internal SynthesizedClosureMethod(NamedTypeSymbol containingType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<SynthesizedClosureEnvironment> structEnvironments, ClosureKind closureKind, MethodSymbol topLevelMethod, DebugId topLevelMethodId, MethodSymbol originalMethod, SyntaxReference blockSyntax, DebugId lambdaId)
			: base(containingType, originalMethod, blockSyntax, originalMethod.DeclaringSyntaxReferences[0].GetLocation(), (originalMethod is LocalFunctionSymbol) ? SynthesizedClosureMethod.MakeName(topLevelMethod.Name, originalMethod.Name, topLevelMethodId, closureKind, lambdaId) : SynthesizedClosureMethod.MakeName(topLevelMethod.Name, topLevelMethodId, closureKind, lambdaId), SynthesizedClosureMethod.MakeDeclarationModifiers(closureKind, originalMethod))
		{
			this.TopLevelMethod = topLevelMethod;
			this.ClosureKind = closureKind;
			this.LambdaId = lambdaId;
			SynthesizedClosureEnvironment synthesizedClosureEnvironment = this.ContainingType as SynthesizedClosureEnvironment;
			ImmutableArray<TypeParameterSymbol> immutableArray;
			TypeMap typeMap;
			switch (closureKind)
			{
			case ClosureKind.Static:
			case ClosureKind.ThisOnly:
			{
				ImmutableArray<TypeParameterSymbol> immutableArray2;
				typeMap = TypeMap.Empty.WithConcatAlphaRename(originalMethod, this, out immutableArray, out immutableArray2, null);
				break;
			}
			case ClosureKind.Singleton:
			case ClosureKind.General:
			{
				ImmutableArray<TypeParameterSymbol> immutableArray2;
				typeMap = synthesizedClosureEnvironment.TypeMap.WithConcatAlphaRename(originalMethod, this, out immutableArray, out immutableArray2, synthesizedClosureEnvironment.OriginalContainingMethodOpt);
				break;
			}
			default:
				throw ExceptionUtilities.UnexpectedValue(closureKind);
			}
			if (!structEnvironments.IsDefaultOrEmpty && immutableArray.Length != 0)
			{
				ArrayBuilder<NamedTypeSymbol> instance = ArrayBuilder<NamedTypeSymbol>.GetInstance();
				foreach (SynthesizedClosureEnvironment synthesizedClosureEnvironment2 in structEnvironments)
				{
					NamedTypeSymbol namedTypeSymbol;
					if (synthesizedClosureEnvironment2.Arity == 0)
					{
						namedTypeSymbol = synthesizedClosureEnvironment2;
					}
					else
					{
						ImmutableArray<TypeParameterSymbol> constructedFromTypeParameters = synthesizedClosureEnvironment2.ConstructedFromTypeParameters;
						ImmutableArray<TypeParameterSymbol> immutableArray3 = typeMap.SubstituteTypeParameters(constructedFromTypeParameters);
						namedTypeSymbol = synthesizedClosureEnvironment2.Construct(immutableArray3);
					}
					instance.Add(namedTypeSymbol);
				}
				this._structEnvironments = instance.ToImmutableAndFree();
			}
			else
			{
				this._structEnvironments = ImmutableArray<NamedTypeSymbol>.CastUp<SynthesizedClosureEnvironment>(structEnvironments);
			}
			base.AssignTypeMapAndTypeParameters(typeMap, immutableArray);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000B4998 File Offset: 0x000B2B98
		private static DeclarationModifiers MakeDeclarationModifiers(ClosureKind closureKind, MethodSymbol originalMethod)
		{
			DeclarationModifiers declarationModifiers = ((closureKind == ClosureKind.ThisOnly) ? DeclarationModifiers.Private : DeclarationModifiers.Internal);
			if (closureKind == ClosureKind.Static)
			{
				declarationModifiers |= DeclarationModifiers.Static;
			}
			if (originalMethod.IsAsync)
			{
				declarationModifiers |= DeclarationModifiers.Async;
			}
			if (originalMethod.IsExtern)
			{
				declarationModifiers |= DeclarationModifiers.Extern;
			}
			return declarationModifiers;
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000B49DB File Offset: 0x000B2BDB
		private static string MakeName(string topLevelMethodName, string localFunctionName, DebugId topLevelMethodId, ClosureKind closureKind, DebugId lambdaId)
		{
			return GeneratedNames.MakeLocalFunctionName(topLevelMethodName, localFunctionName, (closureKind == ClosureKind.General) ? (-1) : topLevelMethodId.Ordinal, topLevelMethodId.Generation, lambdaId.Ordinal, lambdaId.Generation);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000B4A05 File Offset: 0x000B2C05
		private static string MakeName(string topLevelMethodName, DebugId topLevelMethodId, ClosureKind closureKind, DebugId lambdaId)
		{
			return GeneratedNames.MakeLambdaMethodName(topLevelMethodName, (closureKind == ClosureKind.General) ? (-1) : topLevelMethodId.Ordinal, topLevelMethodId.Generation, lambdaId.Ordinal, lambdaId.Generation);
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x000B4A2C File Offset: 0x000B2C2C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<ParameterSymbol> BaseMethodParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.BaseMethod.Parameters;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000B4A39 File Offset: 0x000B2C39
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<TypeSymbol> ExtraSynthesizedRefParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeSymbol>.CastUp<NamedTypeSymbol>(this._structEnvironments);
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000B4A48 File Offset: 0x000B2C48
		internal int ExtraSynthesizedParameterCount
		{
			get
			{
				if (!this._structEnvironments.IsDefault)
				{
					return this._structEnvironments.Length;
				}
				return 0;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000B4A75 File Offset: 0x000B2C75
		internal override bool InheritsBaseMethodAttributes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x000B4A78 File Offset: 0x000B2C78
		internal override bool GenerateDebugInfo
		{
			get
			{
				return !this.IsAsync;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000B4A83 File Offset: 0x000B2C83
		internal override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000B4A86 File Offset: 0x000B2C86
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			return this.TopLevelMethod.CalculateLocalSyntaxOffset(localPosition, localTree);
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x000B4A95 File Offset: 0x000B2C95
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IMethodSymbolInternal ISynthesizedMethodBodyImplementationSymbol.Method
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.TopLevelMethod;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x000B4A9D File Offset: 0x000B2C9D
		bool ISynthesizedMethodBodyImplementationSymbol.HasMethodBodyDependency
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x000B4AA0 File Offset: 0x000B2CA0
		public ClosureKind ClosureKind { get; }

		// Token: 0x0400109C RID: 4252
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<NamedTypeSymbol> _structEnvironments;

		// Token: 0x0400109E RID: 4254
		internal readonly DebugId LambdaId;
	}
}
