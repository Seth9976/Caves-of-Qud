using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000129 RID: 297
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal sealed class MergedTypeDeclaration : MergedNamespaceOrTypeDeclaration
	{
		// Token: 0x06001898 RID: 6296 RVA: 0x00084958 File Offset: 0x00082B58
		internal MergedTypeDeclaration(ImmutableArray<SingleTypeDeclaration> declarations)
			: base(declarations[0].Name)
		{
			this._declarations = declarations;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x00084974 File Offset: 0x00082B74
		public ImmutableArray<SingleTypeDeclaration> Declarations
		{
			get
			{
				return this._declarations;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0008497C File Offset: 0x00082B7C
		public ImmutableArray<SyntaxReference> SyntaxReferences
		{
			get
			{
				return this._declarations.SelectAsArray<SingleTypeDeclaration, SyntaxReference>((SingleTypeDeclaration r) => r.SyntaxReference);
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x000849A8 File Offset: 0x00082BA8
		public ImmutableArray<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			ArrayBuilder<SyntaxList<AttributeListSyntax>> instance = ArrayBuilder<SyntaxList<AttributeListSyntax>>.GetInstance();
			foreach (SingleTypeDeclaration singleTypeDeclaration in this._declarations)
			{
				if (singleTypeDeclaration.HasAnyAttributes)
				{
					SyntaxNode syntax = singleTypeDeclaration.SyntaxReference.GetSyntax(default(CancellationToken));
					SyntaxKind syntaxKind = syntax.Kind();
					SyntaxList<AttributeListSyntax> syntaxList;
					switch (syntaxKind)
					{
					case SyntaxKind.ClassDeclaration:
					case SyntaxKind.StructDeclaration:
					case SyntaxKind.InterfaceDeclaration:
						goto IL_0080;
					case SyntaxKind.EnumDeclaration:
						syntaxList = ((EnumDeclarationSyntax)syntax).AttributeLists;
						break;
					case SyntaxKind.DelegateDeclaration:
						syntaxList = ((DelegateDeclarationSyntax)syntax).AttributeLists;
						break;
					default:
						if (syntaxKind == SyntaxKind.RecordDeclaration || syntaxKind == SyntaxKind.RecordStructDeclaration)
						{
							goto IL_0080;
						}
						throw ExceptionUtilities.UnexpectedValue(syntax.Kind());
					}
					IL_00C2:
					instance.Add(syntaxList);
					continue;
					IL_0080:
					syntaxList = ((TypeDeclarationSyntax)syntax).AttributeLists;
					goto IL_00C2;
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x00084A94 File Offset: 0x00082C94
		public override DeclarationKind Kind
		{
			get
			{
				return this.Declarations[0].Kind;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x00084AB8 File Offset: 0x00082CB8
		public int Arity
		{
			get
			{
				return this.Declarations[0].Arity;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00084ADC File Offset: 0x00082CDC
		public bool ContainsExtensionMethods
		{
			get
			{
				ImmutableArray<SingleTypeDeclaration>.Enumerator enumerator = this.Declarations.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.AnyMemberHasExtensionMethodSyntax)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00084B14 File Offset: 0x00082D14
		public bool AnyMemberHasAttributes
		{
			get
			{
				ImmutableArray<SingleTypeDeclaration>.Enumerator enumerator = this.Declarations.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.AnyMemberHasAttributes)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00084B4C File Offset: 0x00082D4C
		public LexicalSortKey GetLexicalSortKey(CSharpCompilation compilation)
		{
			LexicalSortKey lexicalSortKey = new LexicalSortKey(this.Declarations[0].NameLocation, compilation);
			for (int i = 1; i < this.Declarations.Length; i++)
			{
				lexicalSortKey = LexicalSortKey.First(lexicalSortKey, new LexicalSortKey(this.Declarations[i].NameLocation, compilation));
			}
			return lexicalSortKey;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x00084BB0 File Offset: 0x00082DB0
		public ImmutableArray<SourceLocation> NameLocations
		{
			get
			{
				if (this.Declarations.Length == 1)
				{
					return ImmutableArray.Create<SourceLocation>(this.Declarations[0].NameLocation);
				}
				ArrayBuilder<SourceLocation> instance = ArrayBuilder<SourceLocation>.GetInstance();
				foreach (SingleTypeDeclaration singleTypeDeclaration in this.Declarations)
				{
					SourceLocation nameLocation = singleTypeDeclaration.NameLocation;
					if (nameLocation != null)
					{
						instance.Add(nameLocation);
					}
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00084C2C File Offset: 0x00082E2C
		private ImmutableArray<MergedTypeDeclaration> MakeChildren()
		{
			ArrayBuilder<SingleTypeDeclaration> arrayBuilder = null;
			foreach (SingleTypeDeclaration singleTypeDeclaration in this.Declarations)
			{
				foreach (SingleTypeDeclaration singleTypeDeclaration2 in singleTypeDeclaration.Children)
				{
					if (singleTypeDeclaration2 != null)
					{
						if (arrayBuilder == null)
						{
							arrayBuilder = ArrayBuilder<SingleTypeDeclaration>.GetInstance();
						}
						arrayBuilder.Add(singleTypeDeclaration2);
					}
				}
			}
			ArrayBuilder<MergedTypeDeclaration> instance = ArrayBuilder<MergedTypeDeclaration>.GetInstance();
			if (arrayBuilder != null)
			{
				Dictionary<SingleTypeDeclaration.TypeDeclarationIdentity, ImmutableArray<SingleTypeDeclaration>> dictionary = arrayBuilder.ToDictionary<SingleTypeDeclaration.TypeDeclarationIdentity>((SingleTypeDeclaration t) => t.Identity, null);
				arrayBuilder.Free();
				foreach (ImmutableArray<SingleTypeDeclaration> immutableArray in dictionary.Values)
				{
					instance.Add(new MergedTypeDeclaration(immutableArray));
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00084D1C File Offset: 0x00082F1C
		public new ImmutableArray<MergedTypeDeclaration> Children
		{
			get
			{
				if (this._lazyChildren.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<MergedTypeDeclaration>(ref this._lazyChildren, this.MakeChildren());
				}
				return this._lazyChildren;
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00084D43 File Offset: 0x00082F43
		protected override ImmutableArray<Declaration> GetDeclarationChildren()
		{
			return StaticCast<Declaration>.From<MergedTypeDeclaration>(this.Children);
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00084D50 File Offset: 0x00082F50
		public ICollection<string> MemberNames
		{
			get
			{
				if (this._lazyMemberNames == null)
				{
					ICollection<string> collection = UnionCollection<string>.Create<SingleTypeDeclaration>(this.Declarations, (SingleTypeDeclaration d) => d.MemberNames);
					Interlocked.CompareExchange<ICollection<string>>(ref this._lazyMemberNames, collection, null);
				}
				return this._lazyMemberNames;
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00084DA4 File Offset: 0x00082FA4
		internal string GetDebuggerDisplay()
		{
			return "MergedTypeDeclaration " + base.Name;
		}

		// Token: 0x040006CA RID: 1738
		private readonly ImmutableArray<SingleTypeDeclaration> _declarations;

		// Token: 0x040006CB RID: 1739
		private ImmutableArray<MergedTypeDeclaration> _lazyChildren;

		// Token: 0x040006CC RID: 1740
		private ICollection<string> _lazyMemberNames;
	}
}
