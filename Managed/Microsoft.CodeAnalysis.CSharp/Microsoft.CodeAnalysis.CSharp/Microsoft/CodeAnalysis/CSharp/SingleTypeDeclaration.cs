using System;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200012E RID: 302
	internal sealed class SingleTypeDeclaration : SingleNamespaceOrTypeDeclaration
	{
		// Token: 0x060018BE RID: 6334 RVA: 0x00084EEB File Offset: 0x000830EB
		internal SingleTypeDeclaration(DeclarationKind kind, string name, int arity, DeclarationModifiers modifiers, SingleTypeDeclaration.TypeDeclarationFlags declFlags, SyntaxReference syntaxReference, SourceLocation nameLocation, ImmutableHashSet<string> memberNames, ImmutableArray<SingleTypeDeclaration> children, ImmutableArray<Diagnostic> diagnostics)
			: base(name, syntaxReference, nameLocation, diagnostics)
		{
			this._kind = kind;
			this._arity = (ushort)arity;
			this._modifiers = modifiers;
			this.MemberNames = memberNames;
			this._children = children;
			this._flags = declFlags;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x00084F29 File Offset: 0x00083129
		public override DeclarationKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x00084F31 File Offset: 0x00083131
		public new ImmutableArray<SingleTypeDeclaration> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00084F39 File Offset: 0x00083139
		public int Arity
		{
			get
			{
				return (int)this._arity;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x00084F41 File Offset: 0x00083141
		public DeclarationModifiers Modifiers
		{
			get
			{
				return this._modifiers;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00084F49 File Offset: 0x00083149
		public ImmutableHashSet<string> MemberNames { get; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00084F51 File Offset: 0x00083151
		public bool AnyMemberHasExtensionMethodSyntax
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.AnyMemberHasExtensionMethodSyntax) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00084F5E File Offset: 0x0008315E
		public bool HasAnyAttributes
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.HasAnyAttributes) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00084F6B File Offset: 0x0008316B
		public bool HasBaseDeclarations
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.HasBaseDeclarations) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00084F78 File Offset: 0x00083178
		public bool AnyMemberHasAttributes
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.AnyMemberHasAttributes) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00084F86 File Offset: 0x00083186
		public bool HasAnyNontypeMembers
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.HasAnyNontypeMembers) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00084F94 File Offset: 0x00083194
		public bool HasAwaitExpressions
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.HasAwaitExpressions) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00084FA2 File Offset: 0x000831A2
		public bool HasReturnWithExpression
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.HasReturnWithExpression) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00084FB3 File Offset: 0x000831B3
		public bool IsIterator
		{
			get
			{
				return (this._flags & SingleTypeDeclaration.TypeDeclarationFlags.IsIterator) > SingleTypeDeclaration.TypeDeclarationFlags.None;
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00084FC4 File Offset: 0x000831C4
		protected override ImmutableArray<SingleNamespaceOrTypeDeclaration> GetNamespaceOrTypeDeclarationChildren()
		{
			return StaticCast<SingleNamespaceOrTypeDeclaration>.From<SingleTypeDeclaration>(this._children);
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00084FD1 File Offset: 0x000831D1
		internal SingleTypeDeclaration.TypeDeclarationIdentity Identity
		{
			get
			{
				return new SingleTypeDeclaration.TypeDeclarationIdentity(this);
			}
		}

		// Token: 0x040006D8 RID: 1752
		private readonly DeclarationKind _kind;

		// Token: 0x040006D9 RID: 1753
		private readonly SingleTypeDeclaration.TypeDeclarationFlags _flags;

		// Token: 0x040006DA RID: 1754
		private readonly ushort _arity;

		// Token: 0x040006DB RID: 1755
		private readonly DeclarationModifiers _modifiers;

		// Token: 0x040006DC RID: 1756
		private readonly ImmutableArray<SingleTypeDeclaration> _children;

		// Token: 0x020006D6 RID: 1750
		[Flags]
		internal enum TypeDeclarationFlags : ushort
		{
			// Token: 0x040021B6 RID: 8630
			None = 0,
			// Token: 0x040021B7 RID: 8631
			AnyMemberHasExtensionMethodSyntax = 2,
			// Token: 0x040021B8 RID: 8632
			HasAnyAttributes = 4,
			// Token: 0x040021B9 RID: 8633
			HasBaseDeclarations = 8,
			// Token: 0x040021BA RID: 8634
			AnyMemberHasAttributes = 16,
			// Token: 0x040021BB RID: 8635
			HasAnyNontypeMembers = 32,
			// Token: 0x040021BC RID: 8636
			HasAwaitExpressions = 64,
			// Token: 0x040021BD RID: 8637
			IsIterator = 128,
			// Token: 0x040021BE RID: 8638
			HasReturnWithExpression = 256
		}

		// Token: 0x020006D7 RID: 1751
		internal struct TypeDeclarationIdentity : IEquatable<SingleTypeDeclaration.TypeDeclarationIdentity>
		{
			// Token: 0x0600848C RID: 33932 RVA: 0x001E0790 File Offset: 0x001DE990
			internal TypeDeclarationIdentity(SingleTypeDeclaration decl)
			{
				this._decl = decl;
			}

			// Token: 0x0600848D RID: 33933 RVA: 0x001E0799 File Offset: 0x001DE999
			public override bool Equals(object obj)
			{
				return obj is SingleTypeDeclaration.TypeDeclarationIdentity && this.Equals((SingleTypeDeclaration.TypeDeclarationIdentity)obj);
			}

			// Token: 0x0600848E RID: 33934 RVA: 0x001E07B4 File Offset: 0x001DE9B4
			public bool Equals(SingleTypeDeclaration.TypeDeclarationIdentity other)
			{
				SingleTypeDeclaration decl = this._decl;
				SingleTypeDeclaration decl2 = other._decl;
				return decl == decl2 || (decl._arity == decl2._arity && decl._kind == decl2._kind && !(decl.name != decl2.name) && decl._kind != DeclarationKind.Enum && decl._kind != DeclarationKind.Delegate);
			}

			// Token: 0x0600848F RID: 33935 RVA: 0x001E081C File Offset: 0x001DEA1C
			public override int GetHashCode()
			{
				SingleTypeDeclaration decl = this._decl;
				return Hash.Combine(decl.Name.GetHashCode(), Hash.Combine(decl.Arity.GetHashCode(), (int)decl.Kind));
			}

			// Token: 0x040021BF RID: 8639
			private readonly SingleTypeDeclaration _decl;
		}
	}
}
