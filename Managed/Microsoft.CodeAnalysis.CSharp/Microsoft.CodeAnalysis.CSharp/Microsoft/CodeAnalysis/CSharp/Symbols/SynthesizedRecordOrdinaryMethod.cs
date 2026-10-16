using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200057C RID: 1404
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SynthesizedRecordOrdinaryMethod : SourceOrdinaryMethodSymbolBase
	{
		// Token: 0x06007572 RID: 30066 RVA: 0x001B5DC0 File Offset: 0x001B3FC0
		protected SynthesizedRecordOrdinaryMethod(SourceMemberContainerTypeSymbol containingType, string name, bool hasBody, int memberOffset, BindingDiagnosticBag diagnostics)
			: base(containingType, name, containingType.Locations[0], (CSharpSyntaxNode)containingType.SyntaxReferences[0].GetSyntax(default(CancellationToken)), MethodKind.Ordinary, false, false, false, hasBody, false, diagnostics)
		{
			this._memberOffset = memberOffset;
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06007573 RID: 30067 RVA: 0x001B5E17 File Offset: 0x001B4017
		protected sealed override bool HasAnyBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x06007574 RID: 30068 RVA: 0x001B5E1A File Offset: 0x001B401A
		internal sealed override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x06007575 RID: 30069 RVA: 0x001B5E1D File Offset: 0x001B401D
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x06007576 RID: 30070 RVA: 0x001B5E20 File Offset: 0x001B4020
		protected sealed override Location ReturnTypeLocation
		{
			get
			{
				return this.Locations[0];
			}
		}

		// Token: 0x06007577 RID: 30071 RVA: 0x001B5E3C File Offset: 0x001B403C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected sealed override MethodSymbol FindExplicitlyImplementedMethod(BindingDiagnosticBag diagnostics)
		{
			return null;
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x001B5E3F File Offset: 0x001B403F
		internal sealed override LexicalSortKey GetLexicalSortKey()
		{
			return LexicalSortKey.GetSynthesizedMemberKey(this._memberOffset);
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x001B5E4C File Offset: 0x001B404C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected sealed override ImmutableArray<TypeParameterSymbol> MakeTypeParameters(CSharpSyntaxNode node, BindingDiagnosticBag diagnostics)
		{
			return ImmutableArray<TypeParameterSymbol>.Empty;
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x001B5E53 File Offset: 0x001B4053
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public sealed override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x001B5E5A File Offset: 0x001B405A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public sealed override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x0600757C RID: 30076 RVA: 0x001B5E61 File Offset: 0x001B4061
		protected sealed override void PartialMethodChecks(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x0600757D RID: 30077 RVA: 0x001B5E63 File Offset: 0x001B4063
		protected sealed override void ExtensionMethodChecks(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x001B5E65 File Offset: 0x001B4065
		protected sealed override void CompleteAsyncMethodChecksBetweenStartAndFinish()
		{
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x0600757F RID: 30079 RVA: 0x001B5E67 File Offset: 0x001B4067
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected sealed override TypeSymbol ExplicitInterfaceType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x001B5E6A File Offset: 0x001B406A
		protected sealed override void CheckConstraintsForExplicitInterfaceType(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x06007581 RID: 30081 RVA: 0x001B5E6C File Offset: 0x001B406C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected sealed override SourceMemberMethodSymbol BoundAttributesSource
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x001B5E70 File Offset: 0x001B4070
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		internal sealed override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x001B5E8B File Offset: 0x001B408B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public sealed override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x06007584 RID: 30084 RVA: 0x001B5E8E File Offset: 0x001B408E
		public sealed override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x06007585 RID: 30085 RVA: 0x001B5E91 File Offset: 0x001B4091
		public sealed override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06007586 RID: 30086 RVA: 0x001B5E94 File Offset: 0x001B4094
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x06007587 RID: 30087 RVA: 0x001B5E97 File Offset: 0x001B4097
		internal sealed override bool SynthesizesLoweredBoundBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001CA3 RID: 7331
		private readonly int _memberOffset;
	}
}
