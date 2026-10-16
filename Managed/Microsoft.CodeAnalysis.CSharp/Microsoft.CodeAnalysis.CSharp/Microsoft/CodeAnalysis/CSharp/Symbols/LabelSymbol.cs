using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004EA RID: 1258
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class LabelSymbol : Symbol
	{
		// Token: 0x17001B5A RID: 7002
		// (get) Token: 0x06006A69 RID: 27241 RVA: 0x0018C4D6 File Offset: 0x0018A6D6
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B5B RID: 7003
		// (get) Token: 0x06006A6A RID: 27242 RVA: 0x0018C4D9 File Offset: 0x0018A6D9
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B5C RID: 7004
		// (get) Token: 0x06006A6B RID: 27243 RVA: 0x0018C4DC File Offset: 0x0018A6DC
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B5D RID: 7005
		// (get) Token: 0x06006A6C RID: 27244 RVA: 0x0018C4DF File Offset: 0x0018A6DF
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B5E RID: 7006
		// (get) Token: 0x06006A6D RID: 27245 RVA: 0x0018C4E2 File Offset: 0x0018A6E2
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B5F RID: 7007
		// (get) Token: 0x06006A6E RID: 27246 RVA: 0x0018C4E5 File Offset: 0x0018A6E5
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001B60 RID: 7008
		// (get) Token: 0x06006A6F RID: 27247 RVA: 0x0018C4E8 File Offset: 0x0018A6E8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001B61 RID: 7009
		// (get) Token: 0x06006A70 RID: 27248 RVA: 0x0018C4EB File Offset: 0x0018A6EB
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.NotApplicable;
			}
		}

		// Token: 0x17001B62 RID: 7010
		// (get) Token: 0x06006A71 RID: 27249 RVA: 0x0018C4EE File Offset: 0x0018A6EE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001B63 RID: 7011
		// (get) Token: 0x06006A72 RID: 27250 RVA: 0x0018C4F8 File Offset: 0x0018A6F8
		internal virtual SyntaxNodeOrToken IdentifierNodeOrToken
		{
			get
			{
				return default(SyntaxNodeOrToken);
			}
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x0018C50E File Offset: 0x0018A70E
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitLabel(this, argument);
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x0018C518 File Offset: 0x0018A718
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitLabel(this);
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x0018C521 File Offset: 0x0018A721
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitLabel(this);
		}

		// Token: 0x17001B64 RID: 7012
		// (get) Token: 0x06006A76 RID: 27254 RVA: 0x0018C52A File Offset: 0x0018A72A
		public virtual MethodSymbol ContainingMethod
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001B65 RID: 7013
		// (get) Token: 0x06006A77 RID: 27255 RVA: 0x0018C531 File Offset: 0x0018A731
		public override Symbol ContainingSymbol
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001B66 RID: 7014
		// (get) Token: 0x06006A78 RID: 27256 RVA: 0x0018C538 File Offset: 0x0018A738
		public override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Label;
			}
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x0018C53B File Offset: 0x0018A73B
		protected sealed override ISymbol CreateISymbol()
		{
			return new LabelSymbol(this);
		}
	}
}
