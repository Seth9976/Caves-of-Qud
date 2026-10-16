using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000127 RID: 295
	internal sealed class MergedNamespaceDeclaration : MergedNamespaceOrTypeDeclaration
	{
		// Token: 0x0600188D RID: 6285 RVA: 0x000845C7 File Offset: 0x000827C7
		private MergedNamespaceDeclaration(ImmutableArray<SingleNamespaceDeclaration> declarations)
			: base(declarations.IsEmpty ? string.Empty : declarations[0].Name)
		{
			this._declarations = declarations;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000845F3 File Offset: 0x000827F3
		public static MergedNamespaceDeclaration Create(ImmutableArray<SingleNamespaceDeclaration> declarations)
		{
			return new MergedNamespaceDeclaration(declarations);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x000845FB File Offset: 0x000827FB
		public static MergedNamespaceDeclaration Create(SingleNamespaceDeclaration declaration)
		{
			return new MergedNamespaceDeclaration(ImmutableArray.Create<SingleNamespaceDeclaration>(declaration));
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00084608 File Offset: 0x00082808
		public override DeclarationKind Kind
		{
			get
			{
				return DeclarationKind.Namespace;
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0008460C File Offset: 0x0008280C
		public LexicalSortKey GetLexicalSortKey(CSharpCompilation compilation)
		{
			LexicalSortKey lexicalSortKey = new LexicalSortKey(this._declarations[0].NameLocation, compilation);
			for (int i = 1; i < this._declarations.Length; i++)
			{
				lexicalSortKey = LexicalSortKey.First(lexicalSortKey, new LexicalSortKey(this._declarations[i].NameLocation, compilation));
			}
			return lexicalSortKey;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00084670 File Offset: 0x00082870
		public ImmutableArray<Location> NameLocations
		{
			get
			{
				if (this._declarations.Length == 1)
				{
					return ImmutableArray.Create<Location>(this._declarations[0].NameLocation);
				}
				ArrayBuilder<Location> instance = ArrayBuilder<Location>.GetInstance();
				foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in this._declarations)
				{
					SourceLocation nameLocation = singleNamespaceDeclaration.NameLocation;
					if (nameLocation != null)
					{
						instance.Add(nameLocation);
					}
				}
				return instance.ToImmutableAndFree();
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x000846EB File Offset: 0x000828EB
		public ImmutableArray<SingleNamespaceDeclaration> Declarations
		{
			get
			{
				return this._declarations;
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000846F3 File Offset: 0x000828F3
		protected override ImmutableArray<Declaration> GetDeclarationChildren()
		{
			return StaticCast<Declaration>.From<MergedNamespaceOrTypeDeclaration>(this.Children);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00084700 File Offset: 0x00082900
		private ImmutableArray<MergedNamespaceOrTypeDeclaration> MakeChildren()
		{
			ArrayBuilder<SingleNamespaceDeclaration> arrayBuilder = null;
			ArrayBuilder<SingleTypeDeclaration> arrayBuilder2 = null;
			bool flag = true;
			bool flag2 = true;
			foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in this._declarations)
			{
				foreach (SingleNamespaceOrTypeDeclaration singleNamespaceOrTypeDeclaration in singleNamespaceDeclaration.Children)
				{
					SingleTypeDeclaration singleTypeDeclaration = singleNamespaceOrTypeDeclaration as SingleTypeDeclaration;
					if (singleTypeDeclaration != null)
					{
						if (arrayBuilder2 == null)
						{
							arrayBuilder2 = ArrayBuilder<SingleTypeDeclaration>.GetInstance();
						}
						else if (flag2 && !singleTypeDeclaration.Identity.Equals(arrayBuilder2[0].Identity))
						{
							flag2 = false;
						}
						arrayBuilder2.Add(singleTypeDeclaration);
					}
					else
					{
						SingleNamespaceDeclaration singleNamespaceDeclaration2 = singleNamespaceOrTypeDeclaration as SingleNamespaceDeclaration;
						if (singleNamespaceDeclaration2 != null)
						{
							if (arrayBuilder == null)
							{
								arrayBuilder = ArrayBuilder<SingleNamespaceDeclaration>.GetInstance();
							}
							else if (flag && !singleNamespaceDeclaration2.Name.Equals(arrayBuilder[0].Name))
							{
								flag = false;
							}
							arrayBuilder.Add(singleNamespaceDeclaration2);
						}
					}
				}
			}
			ArrayBuilder<MergedNamespaceOrTypeDeclaration> instance = ArrayBuilder<MergedNamespaceOrTypeDeclaration>.GetInstance();
			if (arrayBuilder != null)
			{
				if (flag)
				{
					instance.Add(MergedNamespaceDeclaration.Create(arrayBuilder.ToImmutableAndFree()));
				}
				else
				{
					Dictionary<string, ImmutableArray<SingleNamespaceDeclaration>> dictionary = arrayBuilder.ToDictionary<string>((SingleNamespaceDeclaration n) => n.Name, StringOrdinalComparer.Instance);
					arrayBuilder.Free();
					foreach (ImmutableArray<SingleNamespaceDeclaration> immutableArray in dictionary.Values)
					{
						instance.Add(MergedNamespaceDeclaration.Create(immutableArray));
					}
				}
			}
			if (arrayBuilder2 != null)
			{
				if (flag2)
				{
					instance.Add(new MergedTypeDeclaration(arrayBuilder2.ToImmutableAndFree()));
				}
				else
				{
					Dictionary<SingleTypeDeclaration.TypeDeclarationIdentity, ImmutableArray<SingleTypeDeclaration>> dictionary2 = arrayBuilder2.ToDictionary<SingleTypeDeclaration.TypeDeclarationIdentity>((SingleTypeDeclaration t) => t.Identity, null);
					arrayBuilder2.Free();
					foreach (ImmutableArray<SingleTypeDeclaration> immutableArray2 in dictionary2.Values)
					{
						instance.Add(new MergedTypeDeclaration(immutableArray2));
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x00084928 File Offset: 0x00082B28
		public new ImmutableArray<MergedNamespaceOrTypeDeclaration> Children
		{
			get
			{
				if (this._lazyChildren.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<MergedNamespaceOrTypeDeclaration>(ref this._lazyChildren, this.MakeChildren());
				}
				return this._lazyChildren;
			}
		}

		// Token: 0x040006C8 RID: 1736
		private readonly ImmutableArray<SingleNamespaceDeclaration> _declarations;

		// Token: 0x040006C9 RID: 1737
		private ImmutableArray<MergedNamespaceOrTypeDeclaration> _lazyChildren;
	}
}
