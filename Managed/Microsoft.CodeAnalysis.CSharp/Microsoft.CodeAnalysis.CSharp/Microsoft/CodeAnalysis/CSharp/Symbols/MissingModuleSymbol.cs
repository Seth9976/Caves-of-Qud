using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F6 RID: 1270
	internal class MissingModuleSymbol : ModuleSymbol
	{
		// Token: 0x06006B98 RID: 27544 RVA: 0x0018F44A File Offset: 0x0018D64A
		public MissingModuleSymbol(AssemblySymbol assembly, int ordinal)
		{
			this.assembly = assembly;
			this.ordinal = ordinal;
			this.globalNamespace = new MissingNamespaceSymbol(this);
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x06006B99 RID: 27545 RVA: 0x0018F46C File Offset: 0x0018D66C
		internal override int Ordinal
		{
			get
			{
				return this.ordinal;
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x06006B9A RID: 27546 RVA: 0x0018F474 File Offset: 0x0018D674
		internal override Machine Machine
		{
			get
			{
				return Machine.I386;
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x06006B9B RID: 27547 RVA: 0x0018F47B File Offset: 0x0018D67B
		internal override bool Bit32Required
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x06006B9C RID: 27548 RVA: 0x0018F47E File Offset: 0x0018D67E
		internal sealed override bool IsMissing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x06006B9D RID: 27549 RVA: 0x0018F481 File Offset: 0x0018D681
		public override string Name
		{
			get
			{
				return "<Missing Module>";
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x06006B9E RID: 27550 RVA: 0x0018F488 File Offset: 0x0018D688
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x06006B9F RID: 27551 RVA: 0x0018F490 File Offset: 0x0018D690
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x06006BA0 RID: 27552 RVA: 0x0018F498 File Offset: 0x0018D698
		public override NamespaceSymbol GlobalNamespace
		{
			get
			{
				return this.globalNamespace;
			}
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x0018F4A0 File Offset: 0x0018D6A0
		public override int GetHashCode()
		{
			return this.assembly.GetHashCode();
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x0018F4B0 File Offset: 0x0018D6B0
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			MissingModuleSymbol missingModuleSymbol = obj as MissingModuleSymbol;
			return missingModuleSymbol != null && this.assembly.Equals(missingModuleSymbol.assembly, compareKind);
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x06006BA3 RID: 27555 RVA: 0x0018F4E1 File Offset: 0x0018D6E1
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x06006BA4 RID: 27556 RVA: 0x0018F4E8 File Offset: 0x0018D6E8
		internal override ICollection<string> NamespaceNames
		{
			get
			{
				return SpecializedCollections.EmptyCollection<string>();
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x06006BA5 RID: 27557 RVA: 0x0018F4EF File Offset: 0x0018D6EF
		internal override ICollection<string> TypeNames
		{
			get
			{
				return SpecializedCollections.EmptyCollection<string>();
			}
		}

		// Token: 0x06006BA6 RID: 27558 RVA: 0x0018F4F6 File Offset: 0x0018D6F6
		internal override NamedTypeSymbol LookupTopLevelMetadataType(ref MetadataTypeName emittedName)
		{
			return new MissingMetadataTypeSymbol.TopLevel(this, ref emittedName, null);
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x0018F500 File Offset: 0x0018D700
		internal override ImmutableArray<AssemblyIdentity> GetReferencedAssemblies()
		{
			return ImmutableArray<AssemblyIdentity>.Empty;
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x0018F507 File Offset: 0x0018D707
		internal override ImmutableArray<AssemblySymbol> GetReferencedAssemblySymbols()
		{
			return ImmutableArray<AssemblySymbol>.Empty;
		}

		// Token: 0x06006BA9 RID: 27561 RVA: 0x0018F50E File Offset: 0x0018D70E
		internal override void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol originatingSourceAssemblyDebugOnly)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x06006BAA RID: 27562 RVA: 0x0018F515 File Offset: 0x0018D715
		internal override bool HasUnifiedReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006BAB RID: 27563 RVA: 0x0018F518 File Offset: 0x0018D718
		internal override bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x06006BAC RID: 27564 RVA: 0x0018F51F File Offset: 0x0018D71F
		internal override bool HasAssemblyCompilationRelaxationsAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x06006BAD RID: 27565 RVA: 0x0018F522 File Offset: 0x0018D722
		internal override bool HasAssemblyRuntimeCompatibilityAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x06006BAE RID: 27566 RVA: 0x0018F528 File Offset: 0x0018D728
		internal override CharSet? DefaultMarshallingCharSet
		{
			get
			{
				return default(CharSet?);
			}
		}

		// Token: 0x06006BAF RID: 27567 RVA: 0x0018F53E File Offset: 0x0018D73E
		public override ModuleMetadata GetMetadata()
		{
			return null;
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x0018F541 File Offset: 0x0018D741
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x04001AB7 RID: 6839
		protected readonly AssemblySymbol assembly;

		// Token: 0x04001AB8 RID: 6840
		protected readonly int ordinal;

		// Token: 0x04001AB9 RID: 6841
		protected readonly MissingNamespaceSymbol globalNamespace;
	}
}
