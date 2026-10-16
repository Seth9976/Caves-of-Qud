using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F1 RID: 1265
	internal abstract class MetadataOrSourceAssemblySymbol : NonMissingAssemblySymbol
	{
		// Token: 0x06006B5F RID: 27487 RVA: 0x0018EB8C File Offset: 0x0018CD8C
		internal sealed override NamedTypeSymbol GetDeclaredSpecialType(SpecialType type)
		{
			if (this._lazySpecialTypes == null || this._lazySpecialTypes[(int)type] == null)
			{
				MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(type.GetMetadataName(), true, -1);
				ModuleSymbol moduleSymbol = this.Modules[0];
				NamedTypeSymbol namedTypeSymbol = moduleSymbol.LookupTopLevelMetadataType(ref metadataTypeName);
				if (namedTypeSymbol.Kind != SymbolKind.ErrorType && namedTypeSymbol.DeclaredAccessibility != Accessibility.Public)
				{
					namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(moduleSymbol, ref metadataTypeName, type, null);
				}
				this.RegisterDeclaredSpecialType(namedTypeSymbol);
			}
			return this._lazySpecialTypes[(int)type];
		}

		// Token: 0x06006B60 RID: 27488 RVA: 0x0018EC00 File Offset: 0x0018CE00
		internal sealed override void RegisterDeclaredSpecialType(NamedTypeSymbol corType)
		{
			SpecialType specialType = corType.SpecialType;
			if (this._lazySpecialTypes == null)
			{
				Interlocked.CompareExchange<NamedTypeSymbol[]>(ref this._lazySpecialTypes, new NamedTypeSymbol[46], null);
			}
			if (Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazySpecialTypes[(int)specialType], corType, null) == null)
			{
				Interlocked.Increment(ref this._cachedSpecialTypes);
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06006B61 RID: 27489 RVA: 0x0018EC51 File Offset: 0x0018CE51
		internal override bool KeepLookingForDeclaredSpecialTypes
		{
			get
			{
				return base.CorLibrary == this && this._cachedSpecialTypes < 45;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06006B62 RID: 27490 RVA: 0x0018EC68 File Offset: 0x0018CE68
		public override ICollection<string> TypeNames
		{
			get
			{
				if (this._lazyTypeNames == null)
				{
					Interlocked.CompareExchange<ICollection<string>>(ref this._lazyTypeNames, UnionCollection<string>.Create<ModuleSymbol>(this.Modules, (ModuleSymbol m) => m.TypeNames), null);
				}
				return this._lazyTypeNames;
			}
		}

		// Token: 0x06006B63 RID: 27491 RVA: 0x0018ECBC File Offset: 0x0018CEBC
		internal sealed override NamedTypeSymbol GetNativeIntegerType(NamedTypeSymbol underlyingType)
		{
			if (this._lazyNativeIntegerTypes == null)
			{
				Interlocked.CompareExchange<NativeIntegerTypeSymbol[]>(ref this._lazyNativeIntegerTypes, new NativeIntegerTypeSymbol[2], null);
			}
			SpecialType specialType = underlyingType.SpecialType;
			int num;
			if (specialType != SpecialType.System_IntPtr)
			{
				if (specialType != SpecialType.System_UIntPtr)
				{
					throw ExceptionUtilities.UnexpectedValue(underlyingType.SpecialType);
				}
				num = 1;
			}
			else
			{
				num = 0;
			}
			int num2 = num;
			if (this._lazyNativeIntegerTypes[num2] == null)
			{
				Interlocked.CompareExchange<NativeIntegerTypeSymbol>(ref this._lazyNativeIntegerTypes[num2], new NativeIntegerTypeSymbol(underlyingType), null);
			}
			return this._lazyNativeIntegerTypes[num2];
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06006B64 RID: 27492 RVA: 0x0018ED40 File Offset: 0x0018CF40
		public override ICollection<string> NamespaceNames
		{
			get
			{
				if (this._lazyNamespaceNames == null)
				{
					Interlocked.CompareExchange<ICollection<string>>(ref this._lazyNamespaceNames, UnionCollection<string>.Create<ModuleSymbol>(this.Modules, (ModuleSymbol m) => m.NamespaceNames), null);
				}
				return this._lazyNamespaceNames;
			}
		}

		// Token: 0x06006B65 RID: 27493 RVA: 0x0018ED94 File Offset: 0x0018CF94
		internal override Symbol GetDeclaredSpecialTypeMember(SpecialMember member)
		{
			if (this._lazySpecialTypeMembers == null || this._lazySpecialTypeMembers[(int)member] == ErrorTypeSymbol.UnknownResultType)
			{
				if (this._lazySpecialTypeMembers == null)
				{
					Symbol[] array = new Symbol[124];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = ErrorTypeSymbol.UnknownResultType;
					}
					Interlocked.CompareExchange<Symbol[]>(ref this._lazySpecialTypeMembers, array, null);
				}
				MemberDescriptor descriptor = SpecialMembers.GetDescriptor(member);
				NamedTypeSymbol declaredSpecialType = this.GetDeclaredSpecialType((SpecialType)descriptor.DeclaringTypeId);
				Symbol symbol = null;
				if (!declaredSpecialType.IsErrorType())
				{
					symbol = CSharpCompilation.GetRuntimeMember(declaredSpecialType, in descriptor, CSharpCompilation.SpecialMembersSignatureComparer.Instance, null);
				}
				Interlocked.CompareExchange<Symbol>(ref this._lazySpecialTypeMembers[(int)member], symbol, ErrorTypeSymbol.UnknownResultType);
			}
			return this._lazySpecialTypeMembers[(int)member];
		}

		// Token: 0x06006B66 RID: 27494 RVA: 0x0018EE40 File Offset: 0x0018D040
		protected IVTConclusion MakeFinalIVTDetermination(AssemblySymbol potentialGiverOfAccess)
		{
			IVTConclusion ivtconclusion;
			if (this.AssembliesToWhichInternalAccessHasBeenDetermined.TryGetValue(potentialGiverOfAccess, ref ivtconclusion))
			{
				return ivtconclusion;
			}
			ivtconclusion = IVTConclusion.NoRelationshipClaimed;
			IEnumerable<ImmutableArray<byte>> internalsVisibleToPublicKeys = potentialGiverOfAccess.GetInternalsVisibleToPublicKeys(this.Name);
			if (Enumerable.Any<ImmutableArray<byte>>(internalsVisibleToPublicKeys) && this.IsNetModule())
			{
				return IVTConclusion.Match;
			}
			foreach (ImmutableArray<byte> immutableArray in internalsVisibleToPublicKeys)
			{
				ivtconclusion = potentialGiverOfAccess.Identity.PerformIVTCheck(this.PublicKey, immutableArray);
				if (ivtconclusion == IVTConclusion.Match)
				{
					break;
				}
				if (ivtconclusion == IVTConclusion.OneSignedOneNot)
				{
					break;
				}
			}
			this.AssembliesToWhichInternalAccessHasBeenDetermined.TryAdd(potentialGiverOfAccess, ivtconclusion);
			return ivtconclusion;
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06006B67 RID: 27495 RVA: 0x0018EEE0 File Offset: 0x0018D0E0
		private ConcurrentDictionary<AssemblySymbol, IVTConclusion> AssembliesToWhichInternalAccessHasBeenDetermined
		{
			get
			{
				if (this._assembliesToWhichInternalAccessHasBeenAnalyzed == null)
				{
					Interlocked.CompareExchange<ConcurrentDictionary<AssemblySymbol, IVTConclusion>>(ref this._assembliesToWhichInternalAccessHasBeenAnalyzed, new ConcurrentDictionary<AssemblySymbol, IVTConclusion>(), null);
				}
				return this._assembliesToWhichInternalAccessHasBeenAnalyzed;
			}
		}

		// Token: 0x06006B68 RID: 27496 RVA: 0x0018EF02 File Offset: 0x0018D102
		internal virtual bool IsNetModule()
		{
			return false;
		}

		// Token: 0x04001AA8 RID: 6824
		private NamedTypeSymbol[] _lazySpecialTypes;

		// Token: 0x04001AA9 RID: 6825
		private int _cachedSpecialTypes;

		// Token: 0x04001AAA RID: 6826
		private NativeIntegerTypeSymbol[] _lazyNativeIntegerTypes;

		// Token: 0x04001AAB RID: 6827
		private ICollection<string> _lazyTypeNames;

		// Token: 0x04001AAC RID: 6828
		private ICollection<string> _lazyNamespaceNames;

		// Token: 0x04001AAD RID: 6829
		private Symbol[] _lazySpecialTypeMembers;

		// Token: 0x04001AAE RID: 6830
		private ConcurrentDictionary<AssemblySymbol, IVTConclusion> _assembliesToWhichInternalAccessHasBeenAnalyzed;
	}
}
