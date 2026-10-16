using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200054F RID: 1359
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SourceSimpleParameterSymbol : SourceParameterSymbol
	{
		// Token: 0x0600737A RID: 29562 RVA: 0x001B0487 File Offset: 0x001AE687
		public SourceSimpleParameterSymbol(Symbol owner, TypeWithAnnotations parameterType, int ordinal, RefKind refKind, string name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations)
			: base(owner, parameterType, ordinal, refKind, name, locations)
		{
		}

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x0600737B RID: 29563 RVA: 0x001B0498 File Offset: 0x001AE698
		public override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x001B049B File Offset: 0x001AE69B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x0600737D RID: 29565 RVA: 0x001B049E File Offset: 0x001AE69E
		internal override bool IsMetadataOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x001B04A1 File Offset: 0x001AE6A1
		public override bool IsParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x0600737F RID: 29567 RVA: 0x001B04A4 File Offset: 0x001AE6A4
		internal override bool HasDefaultArgumentSyntax
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x001B04A7 File Offset: 0x001AE6A7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06007381 RID: 29569 RVA: 0x001B04AE File Offset: 0x001AE6AE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override SyntaxReference SyntaxReference
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x001B04B1 File Offset: 0x001AE6B1
		internal override bool IsExtensionMethodThis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x06007383 RID: 29571 RVA: 0x001B04B4 File Offset: 0x001AE6B4
		internal override bool IsIDispatchConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x001B04B7 File Offset: 0x001AE6B7
		internal override bool IsIUnknownConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06007385 RID: 29573 RVA: 0x001B04BA File Offset: 0x001AE6BA
		internal override bool IsCallerFilePath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x001B04BD File Offset: 0x001AE6BD
		internal override bool IsCallerLineNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x06007387 RID: 29575 RVA: 0x001B04C0 File Offset: 0x001AE6C0
		internal override bool IsCallerMemberName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x06007388 RID: 29576 RVA: 0x001B04C3 File Offset: 0x001AE6C3
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x06007389 RID: 29577 RVA: 0x001B04C6 File Offset: 0x001AE6C6
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x0600738A RID: 29578 RVA: 0x001B04CD File Offset: 0x001AE6CD
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x0600738B RID: 29579 RVA: 0x001B04D0 File Offset: 0x001AE6D0
		internal override bool HasOptionalAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x0600738C RID: 29580 RVA: 0x001B04D4 File Offset: 0x001AE6D4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override SyntaxList<AttributeListSyntax> AttributeDeclarationList
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return default(SyntaxList<AttributeListSyntax>);
			}
		}

		// Token: 0x0600738D RID: 29581 RVA: 0x001B04EA File Offset: 0x001AE6EA
		internal override CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			this.state.NotePartComplete(CompletionPart.Attributes);
			return CustomAttributesBag<CSharpAttributeData>.Empty;
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x0600738E RID: 29582 RVA: 0x001B04FE File Offset: 0x001AE6FE
		internal override ConstantValue DefaultValueFromAttributes
		{
			get
			{
				return null;
			}
		}
	}
}
