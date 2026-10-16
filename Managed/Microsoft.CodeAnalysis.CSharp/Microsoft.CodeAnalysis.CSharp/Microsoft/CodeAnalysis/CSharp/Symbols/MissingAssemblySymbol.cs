using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F3 RID: 1267
	internal class MissingAssemblySymbol : AssemblySymbol
	{
		// Token: 0x06006B76 RID: 27510 RVA: 0x0018F1B6 File Offset: 0x0018D3B6
		public MissingAssemblySymbol(AssemblyIdentity identity)
		{
			this.identity = identity;
			this.moduleSymbol = new MissingModuleSymbol(this, 0);
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06006B77 RID: 27511 RVA: 0x0018F1D2 File Offset: 0x0018D3D2
		internal sealed override bool IsMissing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x06006B78 RID: 27512 RVA: 0x0018F1D5 File Offset: 0x0018D3D5
		internal override bool IsLinked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006B79 RID: 27513 RVA: 0x0018F1D8 File Offset: 0x0018D3D8
		internal override Symbol GetDeclaredSpecialTypeMember(SpecialMember member)
		{
			return null;
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x06006B7A RID: 27514 RVA: 0x0018F1DB File Offset: 0x0018D3DB
		public override AssemblyIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x06006B7B RID: 27515 RVA: 0x0018F1E3 File Offset: 0x0018D3E3
		public override Version AssemblyVersionPattern
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06006B7C RID: 27516 RVA: 0x0018F1E6 File Offset: 0x0018D3E6
		internal override ImmutableArray<byte> PublicKey
		{
			get
			{
				return this.Identity.PublicKey;
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06006B7D RID: 27517 RVA: 0x0018F1F3 File Offset: 0x0018D3F3
		public override ImmutableArray<ModuleSymbol> Modules
		{
			get
			{
				if (this._lazyModules.IsDefault)
				{
					this._lazyModules = ImmutableArray.Create<ModuleSymbol>(this.moduleSymbol);
				}
				return this._lazyModules;
			}
		}

		// Token: 0x06006B7E RID: 27518 RVA: 0x0018F219 File Offset: 0x0018D419
		public override int GetHashCode()
		{
			return this.identity.GetHashCode();
		}

		// Token: 0x06006B7F RID: 27519 RVA: 0x0018F226 File Offset: 0x0018D426
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			return this.Equals(obj as MissingAssemblySymbol);
		}

		// Token: 0x06006B80 RID: 27520 RVA: 0x0018F234 File Offset: 0x0018D434
		public bool Equals(MissingAssemblySymbol other)
		{
			return other != null && (this == other || this.identity.Equals(other.Identity));
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06006B81 RID: 27521 RVA: 0x0018F252 File Offset: 0x0018D452
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x06006B82 RID: 27522 RVA: 0x0018F259 File Offset: 0x0018D459
		internal override void SetLinkedReferencedAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006B83 RID: 27523 RVA: 0x0018F260 File Offset: 0x0018D460
		internal override ImmutableArray<AssemblySymbol> GetLinkedReferencedAssemblies()
		{
			return ImmutableArray<AssemblySymbol>.Empty;
		}

		// Token: 0x06006B84 RID: 27524 RVA: 0x0018F267 File Offset: 0x0018D467
		internal override void SetNoPiaResolutionAssemblies(ImmutableArray<AssemblySymbol> assemblies)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006B85 RID: 27525 RVA: 0x0018F26E File Offset: 0x0018D46E
		internal override ImmutableArray<AssemblySymbol> GetNoPiaResolutionAssemblies()
		{
			return ImmutableArray<AssemblySymbol>.Empty;
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x06006B86 RID: 27526 RVA: 0x0018F275 File Offset: 0x0018D475
		public sealed override NamespaceSymbol GlobalNamespace
		{
			get
			{
				return this.moduleSymbol.GlobalNamespace;
			}
		}

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x06006B87 RID: 27527 RVA: 0x0018F282 File Offset: 0x0018D482
		public override ICollection<string> TypeNames
		{
			get
			{
				return SpecializedCollections.EmptyCollection<string>();
			}
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x06006B88 RID: 27528 RVA: 0x0018F289 File Offset: 0x0018D489
		public override ICollection<string> NamespaceNames
		{
			get
			{
				return SpecializedCollections.EmptyCollection<string>();
			}
		}

		// Token: 0x06006B89 RID: 27529 RVA: 0x0018F290 File Offset: 0x0018D490
		internal override NamedTypeSymbol LookupTopLevelMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies, bool digThroughForwardedTypes)
		{
			return this.moduleSymbol.LookupTopLevelMetadataType(ref emittedName);
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x0018F29E File Offset: 0x0018D49E
		internal override NamedTypeSymbol GetDeclaredSpecialType(SpecialType type)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x0018F2A5 File Offset: 0x0018D4A5
		internal override bool AreInternalsVisibleToThisAssembly(AssemblySymbol other)
		{
			return false;
		}

		// Token: 0x06006B8C RID: 27532 RVA: 0x0018F2A8 File Offset: 0x0018D4A8
		internal override IEnumerable<ImmutableArray<byte>> GetInternalsVisibleToPublicKeys(string simpleName)
		{
			return SpecializedCollections.EmptyEnumerable<ImmutableArray<byte>>();
		}

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x06006B8D RID: 27533 RVA: 0x0018F2AF File Offset: 0x0018D4AF
		public override bool MightContainExtensionMethods
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006B8E RID: 27534 RVA: 0x0018F2B2 File Offset: 0x0018D4B2
		public override AssemblyMetadata GetMetadata()
		{
			return null;
		}

		// Token: 0x06006B8F RID: 27535 RVA: 0x0018F2B5 File Offset: 0x0018D4B5
		internal sealed override IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes()
		{
			return SpecializedCollections.EmptyEnumerable<NamedTypeSymbol>();
		}

		// Token: 0x04001AAF RID: 6831
		protected readonly AssemblyIdentity identity;

		// Token: 0x04001AB0 RID: 6832
		protected readonly MissingModuleSymbol moduleSymbol;

		// Token: 0x04001AB1 RID: 6833
		private ImmutableArray<ModuleSymbol> _lazyModules;
	}
}
