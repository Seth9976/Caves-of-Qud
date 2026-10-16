using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel
{
	// Token: 0x020005B9 RID: 1465
	internal abstract class AssemblySymbol : Symbol, IAssemblySymbol, ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06007B11 RID: 31505
		internal abstract AssemblySymbol UnderlyingAssemblySymbol { get; }

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x06007B12 RID: 31506 RVA: 0x001BFF1D File Offset: 0x001BE11D
		INamespaceSymbol IAssemblySymbol.GlobalNamespace
		{
			get
			{
				return this.UnderlyingAssemblySymbol.GlobalNamespace.GetPublicSymbol();
			}
		}

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x06007B13 RID: 31507 RVA: 0x001BFF2F File Offset: 0x001BE12F
		IEnumerable<IModuleSymbol> IAssemblySymbol.Modules
		{
			get
			{
				foreach (ModuleSymbol moduleSymbol in this.UnderlyingAssemblySymbol.Modules)
				{
					yield return moduleSymbol.GetPublicSymbol();
				}
				ImmutableArray<ModuleSymbol>.Enumerator enumerator = default(ImmutableArray<ModuleSymbol>.Enumerator);
				yield break;
			}
		}

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x06007B14 RID: 31508 RVA: 0x001BFF3F File Offset: 0x001BE13F
		bool IAssemblySymbol.IsInteractive
		{
			get
			{
				return this.UnderlyingAssemblySymbol.IsInteractive;
			}
		}

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x06007B15 RID: 31509 RVA: 0x001BFF4C File Offset: 0x001BE14C
		AssemblyIdentity IAssemblySymbol.Identity
		{
			get
			{
				return this.UnderlyingAssemblySymbol.Identity;
			}
		}

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x06007B16 RID: 31510 RVA: 0x001BFF59 File Offset: 0x001BE159
		ICollection<string> IAssemblySymbol.TypeNames
		{
			get
			{
				return this.UnderlyingAssemblySymbol.TypeNames;
			}
		}

		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06007B17 RID: 31511 RVA: 0x001BFF66 File Offset: 0x001BE166
		ICollection<string> IAssemblySymbol.NamespaceNames
		{
			get
			{
				return this.UnderlyingAssemblySymbol.NamespaceNames;
			}
		}

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06007B18 RID: 31512 RVA: 0x001BFF73 File Offset: 0x001BE173
		bool IAssemblySymbol.MightContainExtensionMethods
		{
			get
			{
				return this.UnderlyingAssemblySymbol.MightContainExtensionMethods;
			}
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x001BFF80 File Offset: 0x001BE180
		AssemblyMetadata IAssemblySymbol.GetMetadata()
		{
			return this.UnderlyingAssemblySymbol.GetMetadata();
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x001BFF8D File Offset: 0x001BE18D
		INamedTypeSymbol IAssemblySymbol.ResolveForwardedType(string fullyQualifiedMetadataName)
		{
			return this.UnderlyingAssemblySymbol.ResolveForwardedType(fullyQualifiedMetadataName).GetPublicSymbol();
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x001BFFA0 File Offset: 0x001BE1A0
		ImmutableArray<INamedTypeSymbol> IAssemblySymbol.GetForwardedTypes()
		{
			return Enumerable.OrderBy<INamedTypeSymbol, string>(Enumerable.Select<NamedTypeSymbol, INamedTypeSymbol>(this.UnderlyingAssemblySymbol.GetAllTopLevelForwardedTypes(), (NamedTypeSymbol t) => t.GetPublicSymbol()), (INamedTypeSymbol t) => t.ToDisplayString(SymbolDisplayFormat.QualifiedNameArityFormat)).AsImmutable<INamedTypeSymbol>();
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x001C0008 File Offset: 0x001BE208
		bool IAssemblySymbol.GivesAccessTo(IAssemblySymbol assemblyWantingAccess)
		{
			if (object.Equals(this, assemblyWantingAccess))
			{
				return true;
			}
			IEnumerable<ImmutableArray<byte>> internalsVisibleToPublicKeys = this.UnderlyingAssemblySymbol.GetInternalsVisibleToPublicKeys(assemblyWantingAccess.Name);
			if (Enumerable.Any<ImmutableArray<byte>>(internalsVisibleToPublicKeys))
			{
				if (assemblyWantingAccess.IsNetModule())
				{
					return true;
				}
				AssemblyIdentity identity = this.UnderlyingAssemblySymbol.Identity;
				foreach (ImmutableArray<byte> immutableArray in internalsVisibleToPublicKeys)
				{
					IVTConclusion ivtconclusion = identity.PerformIVTCheck(assemblyWantingAccess.Identity.PublicKey, immutableArray);
					if (ivtconclusion == IVTConclusion.Match || ivtconclusion == IVTConclusion.OneSignedOneNot)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x001C00AC File Offset: 0x001BE2AC
		INamedTypeSymbol IAssemblySymbol.GetTypeByMetadataName(string metadataName)
		{
			return this.UnderlyingAssemblySymbol.GetTypeByMetadataName(metadataName).GetPublicSymbol();
		}

		// Token: 0x06007B1E RID: 31518 RVA: 0x001C00BF File Offset: 0x001BE2BF
		protected override void Accept(SymbolVisitor visitor)
		{
			visitor.VisitAssembly(this);
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x001C00C8 File Offset: 0x001BE2C8
		protected override TResult Accept<TResult>(SymbolVisitor<TResult> visitor)
		{
			return visitor.VisitAssembly(this);
		}
	}
}
