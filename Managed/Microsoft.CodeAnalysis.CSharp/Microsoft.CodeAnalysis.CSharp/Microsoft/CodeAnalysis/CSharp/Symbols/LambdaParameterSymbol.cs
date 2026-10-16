using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000521 RID: 1313
	internal sealed class LambdaParameterSymbol : SourceComplexParameterSymbol
	{
		// Token: 0x06006E29 RID: 28201 RVA: 0x00194CAC File Offset: 0x00192EAC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public LambdaParameterSymbol(LambdaSymbol owner, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, TypeWithAnnotations parameterType, int ordinal, RefKind refKind, string name, bool isDiscard, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations)
			: base(owner, ordinal, parameterType, refKind, name, locations, null, false, false)
		{
			this._attributeLists = attributeLists;
			this.IsDiscard = isDiscard;
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06006E2A RID: 28202 RVA: 0x00194CDB File Offset: 0x00192EDB
		public override bool IsDiscard { get; }

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x06006E2B RID: 28203 RVA: 0x00194CE3 File Offset: 0x00192EE3
		internal override bool IsMetadataOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x06006E2C RID: 28204 RVA: 0x00194CE6 File Offset: 0x00192EE6
		public override bool IsParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x06006E2D RID: 28205 RVA: 0x00194CE9 File Offset: 0x00192EE9
		internal override bool HasDefaultArgumentSyntax
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x06006E2E RID: 28206 RVA: 0x00194CEC File Offset: 0x00192EEC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x06006E2F RID: 28207 RVA: 0x00194CF3 File Offset: 0x00192EF3
		internal override bool IsExtensionMethodThis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x00194CF6 File Offset: 0x00192EF6
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(this._attributeLists);
		}

		// Token: 0x04001B21 RID: 6945
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly SyntaxList<AttributeListSyntax> _attributeLists;
	}
}
