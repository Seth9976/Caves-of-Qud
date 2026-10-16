using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.DocumentationComments;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005EA RID: 1514
	internal sealed class PEMethodSymbol : MethodSymbol
	{
		// Token: 0x06007E46 RID: 32326 RVA: 0x001C5EB4 File Offset: 0x001C40B4
		private PEMethodSymbol.UncommonFields CreateUncommonFields()
		{
			PEMethodSymbol.UncommonFields uncommonFields = new PEMethodSymbol.UncommonFields();
			if (!this._packedFlags.IsObsoleteAttributePopulated)
			{
				uncommonFields._lazyObsoleteAttributeData = ObsoleteAttributeData.Uninitialized;
			}
			if (!this._packedFlags.IsUnmanagedCallersOnlyAttributePopulated)
			{
				uncommonFields._lazyUnmanagedCallersOnlyAttributeData = UnmanagedCallersOnlyAttributeData.Uninitialized;
			}
			if (this._packedFlags.IsCustomAttributesPopulated)
			{
				uncommonFields._lazyCustomAttributes = ImmutableArray<CSharpAttributeData>.Empty;
			}
			if (this._packedFlags.IsConditionalPopulated)
			{
				uncommonFields._lazyConditionalAttributeSymbols = ImmutableArray<string>.Empty;
			}
			if (this._packedFlags.IsOverriddenOrHiddenMembersPopulated)
			{
				uncommonFields._lazyOverriddenOrHiddenMembersResult = OverriddenOrHiddenMembersResult.Empty;
			}
			if (this._packedFlags.IsMemberNotNullPopulated)
			{
				uncommonFields._lazyNotNullMembers = ImmutableArray<string>.Empty;
				uncommonFields._lazyNotNullMembersWhenTrue = ImmutableArray<string>.Empty;
				uncommonFields._lazyNotNullMembersWhenFalse = ImmutableArray<string>.Empty;
			}
			if (this._packedFlags.IsExplicitOverrideIsPopulated)
			{
				uncommonFields._lazyExplicitClassOverride = null;
			}
			return uncommonFields;
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x001C5F82 File Offset: 0x001C4182
		private PEMethodSymbol.UncommonFields AccessUncommonFields()
		{
			return this._uncommonFields ?? InterlockedOperations.Initialize<PEMethodSymbol.UncommonFields>(ref this._uncommonFields, this.CreateUncommonFields());
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x001C5FA0 File Offset: 0x001C41A0
		internal PEMethodSymbol(PEModuleSymbol moduleSymbol, PENamedTypeSymbol containingType, MethodDefinitionHandle methodDef)
		{
			this._handle = methodDef;
			this._containingType = containingType;
			MethodAttributes methodAttributes = 0;
			try
			{
				MethodImplAttributes methodImplAttributes;
				int num;
				moduleSymbol.Module.GetMethodDefPropsOrThrow(methodDef, out this._name, out methodImplAttributes, out methodAttributes, out num);
				this._implFlags = methodImplAttributes;
			}
			catch (BadImageFormatException)
			{
				if (this._name == null)
				{
					this._name = string.Empty;
				}
				this.InitializeUseSiteDiagnostic(new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this })));
			}
			this._flags = methodAttributes;
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x001C6034 File Offset: 0x001C4234
		internal override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			ParameterSymbol parameterSymbol;
			if (!this.IsStatic)
			{
				PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
				parameterSymbol = ((uncommonFields != null) ? uncommonFields._lazyThisParameter : null) ?? InterlockedOperations.Initialize<ParameterSymbol>(ref this.AccessUncommonFields()._lazyThisParameter, new ThisParameterSymbol(this));
			}
			else
			{
				parameterSymbol = null;
			}
			thisParameter = parameterSymbol;
			return true;
		}

		// Token: 0x17002565 RID: 9573
		// (get) Token: 0x06007E4A RID: 32330 RVA: 0x001C6070 File Offset: 0x001C4270
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002566 RID: 9574
		// (get) Token: 0x06007E4B RID: 32331 RVA: 0x001C6078 File Offset: 0x001C4278
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x06007E4C RID: 32332 RVA: 0x001C6080 File Offset: 0x001C4280
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x001C6088 File Offset: 0x001C4288
		private bool HasFlag(MethodAttributes flag)
		{
			return (flag & this._flags) > 0;
		}

		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x06007E4E RID: 32334 RVA: 0x001C6096 File Offset: 0x001C4296
		internal MethodAttributes Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x06007E4F RID: 32335 RVA: 0x001C609E File Offset: 0x001C429E
		internal override bool HasSpecialName
		{
			get
			{
				return this.HasFlag(2048);
			}
		}

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x06007E50 RID: 32336 RVA: 0x001C60AB File Offset: 0x001C42AB
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return this.HasFlag(4096);
			}
		}

		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x06007E51 RID: 32337 RVA: 0x001C60B8 File Offset: 0x001C42B8
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return this._implFlags;
			}
		}

		// Token: 0x1700256C RID: 9580
		// (get) Token: 0x06007E52 RID: 32338 RVA: 0x001C60C0 File Offset: 0x001C42C0
		internal override bool RequiresSecurityObject
		{
			get
			{
				return this.HasFlag(32768);
			}
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x001C60CD File Offset: 0x001C42CD
		public override DllImportData GetDllImportData()
		{
			if (!this.HasFlag(8192))
			{
				return null;
			}
			return this._containingType.ContainingPEModule.Module.GetDllImportData(this._handle);
		}

		// Token: 0x1700256D RID: 9581
		// (get) Token: 0x06007E54 RID: 32340 RVA: 0x001C60F9 File Offset: 0x001C42F9
		internal override bool ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return this.ReturnTypeParameter.IsMarshalledExplicitly;
			}
		}

		// Token: 0x1700256E RID: 9582
		// (get) Token: 0x06007E55 RID: 32341 RVA: 0x001C6106 File Offset: 0x001C4306
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return this.ReturnTypeParameter.MarshallingInformation;
			}
		}

		// Token: 0x1700256F RID: 9583
		// (get) Token: 0x06007E56 RID: 32342 RVA: 0x001C6113 File Offset: 0x001C4313
		internal override ImmutableArray<byte> ReturnValueMarshallingDescriptor
		{
			get
			{
				return this.ReturnTypeParameter.MarshallingDescriptor;
			}
		}

		// Token: 0x17002570 RID: 9584
		// (get) Token: 0x06007E57 RID: 32343 RVA: 0x001C6120 File Offset: 0x001C4320
		internal override bool IsAccessCheckedOnOverride
		{
			get
			{
				return this.HasFlag(512);
			}
		}

		// Token: 0x17002571 RID: 9585
		// (get) Token: 0x06007E58 RID: 32344 RVA: 0x001C612D File Offset: 0x001C432D
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return this.HasFlag(16384);
			}
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x001C613A File Offset: 0x001C433A
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17002572 RID: 9586
		// (get) Token: 0x06007E5A RID: 32346 RVA: 0x001C6144 File Offset: 0x001C4344
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				switch (this.Flags & 7)
				{
				case 0:
				case 1:
					return Accessibility.Private;
				case 2:
					return Accessibility.ProtectedAndInternal;
				case 3:
					return Accessibility.Internal;
				case 4:
					return Accessibility.Protected;
				case 5:
					return Accessibility.ProtectedOrInternal;
				case 6:
					return Accessibility.Public;
				default:
					return Accessibility.Private;
				}
			}
		}

		// Token: 0x17002573 RID: 9587
		// (get) Token: 0x06007E5B RID: 32347 RVA: 0x001C618B File Offset: 0x001C438B
		public override bool IsExtern
		{
			get
			{
				return this.HasFlag(8192);
			}
		}

		// Token: 0x17002574 RID: 9588
		// (get) Token: 0x06007E5C RID: 32348 RVA: 0x001C6198 File Offset: 0x001C4398
		internal override bool IsExternal
		{
			get
			{
				return this.IsExtern || (this.ImplementationAttributes & 3) > 0;
			}
		}

		// Token: 0x17002575 RID: 9589
		// (get) Token: 0x06007E5D RID: 32349 RVA: 0x001C61B0 File Offset: 0x001C43B0
		public override bool IsVararg
		{
			get
			{
				return this.Signature.Header.CallingConvention == SignatureCallingConvention.VarArgs;
			}
		}

		// Token: 0x17002576 RID: 9590
		// (get) Token: 0x06007E5E RID: 32350 RVA: 0x001C61D3 File Offset: 0x001C43D3
		public override bool IsGenericMethod
		{
			get
			{
				return this.Arity > 0;
			}
		}

		// Token: 0x17002577 RID: 9591
		// (get) Token: 0x06007E5F RID: 32351 RVA: 0x001C61DE File Offset: 0x001C43DE
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002578 RID: 9592
		// (get) Token: 0x06007E60 RID: 32352 RVA: 0x001C61E4 File Offset: 0x001C43E4
		public override int Arity
		{
			get
			{
				if (!this._lazyTypeParameters.IsDefault)
				{
					return this._lazyTypeParameters.Length;
				}
				int num3;
				try
				{
					int num;
					int num2;
					MetadataDecoder<PEModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetSignatureCountsOrThrow(this._containingType.ContainingPEModule.Module, this._handle, out num, out num2);
					num3 = num2;
				}
				catch (BadImageFormatException)
				{
					num3 = this.TypeParameters.Length;
				}
				return num3;
			}
		}

		// Token: 0x17002579 RID: 9593
		// (get) Token: 0x06007E61 RID: 32353 RVA: 0x001C6254 File Offset: 0x001C4454
		internal MethodDefinitionHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x1700257A RID: 9594
		// (get) Token: 0x06007E62 RID: 32354 RVA: 0x001C625C File Offset: 0x001C445C
		public override bool IsAbstract
		{
			get
			{
				return this.HasFlag(1024);
			}
		}

		// Token: 0x1700257B RID: 9595
		// (get) Token: 0x06007E63 RID: 32355 RVA: 0x001C626C File Offset: 0x001C446C
		public override bool IsSealed
		{
			get
			{
				if (!this.IsMetadataFinal)
				{
					return false;
				}
				if (!this._containingType.IsInterface)
				{
					return !this.IsAbstract && this.IsOverride;
				}
				return this.IsAbstract && this.IsMetadataVirtual(false) && !this.IsMetadataNewSlot(false);
			}
		}

		// Token: 0x1700257C RID: 9596
		// (get) Token: 0x06007E64 RID: 32356 RVA: 0x001C62BE File Offset: 0x001C44BE
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return !this.HasFlag(128);
			}
		}

		// Token: 0x1700257D RID: 9597
		// (get) Token: 0x06007E65 RID: 32357 RVA: 0x001C62D0 File Offset: 0x001C44D0
		public override bool IsVirtual
		{
			get
			{
				if (!this.IsMetadataVirtual(false) || this.IsDestructor || this.IsMetadataFinal || this.IsAbstract)
				{
					return false;
				}
				if (!this._containingType.IsInterface)
				{
					return !this.IsOverride;
				}
				return this.IsMetadataNewSlot(false);
			}
		}

		// Token: 0x1700257E RID: 9598
		// (get) Token: 0x06007E66 RID: 32358 RVA: 0x001C631E File Offset: 0x001C451E
		public override bool IsOverride
		{
			get
			{
				return !this._containingType.IsInterface && this.IsMetadataVirtual(false) && !this.IsDestructor && ((!this.IsMetadataNewSlot(false) && this._containingType.BaseTypeNoUseSiteDiagnostics != null) || this.IsExplicitClassOverride);
			}
		}

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x06007E67 RID: 32359 RVA: 0x001C635E File Offset: 0x001C455E
		public override bool IsStatic
		{
			get
			{
				return this.HasFlag(16);
			}
		}

		// Token: 0x06007E68 RID: 32360 RVA: 0x001C6368 File Offset: 0x001C4568
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return this.HasFlag(64);
		}

		// Token: 0x06007E69 RID: 32361 RVA: 0x001C6372 File Offset: 0x001C4572
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return this.HasFlag(256);
		}

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x06007E6A RID: 32362 RVA: 0x001C637F File Offset: 0x001C457F
		internal override bool IsMetadataFinal
		{
			get
			{
				return this.HasFlag(32);
			}
		}

		// Token: 0x17002581 RID: 9601
		// (get) Token: 0x06007E6B RID: 32363 RVA: 0x001C6389 File Offset: 0x001C4589
		private bool IsExplicitFinalizerOverride
		{
			get
			{
				if (!this._packedFlags.IsExplicitOverrideIsPopulated)
				{
					ImmutableArray<MethodSymbol> explicitInterfaceImplementations = this.ExplicitInterfaceImplementations;
				}
				return this._packedFlags.IsExplicitFinalizerOverride;
			}
		}

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x06007E6C RID: 32364 RVA: 0x001C63AA File Offset: 0x001C45AA
		private bool IsExplicitClassOverride
		{
			get
			{
				if (!this._packedFlags.IsExplicitOverrideIsPopulated)
				{
					ImmutableArray<MethodSymbol> explicitInterfaceImplementations = this.ExplicitInterfaceImplementations;
				}
				return this._packedFlags.IsExplicitClassOverride;
			}
		}

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x06007E6D RID: 32365 RVA: 0x001C63CB File Offset: 0x001C45CB
		private bool IsDestructor
		{
			get
			{
				return this.MethodKind == MethodKind.Destructor;
			}
		}

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x06007E6E RID: 32366 RVA: 0x001C63D6 File Offset: 0x001C45D6
		public override bool ReturnsVoid
		{
			get
			{
				return base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x06007E6F RID: 32367 RVA: 0x001C63E4 File Offset: 0x001C45E4
		internal override int ParameterCount
		{
			get
			{
				if (this._lazySignature != null)
				{
					return this._lazySignature.Parameters.Length;
				}
				int num3;
				try
				{
					int num;
					int num2;
					MetadataDecoder<PEModuleSymbol, TypeSymbol, MethodSymbol, FieldSymbol, Symbol>.GetSignatureCountsOrThrow(this._containingType.ContainingPEModule.Module, this._handle, out num, out num2);
					num3 = num;
				}
				catch (BadImageFormatException)
				{
					num3 = this.Parameters.Length;
				}
				return num3;
			}
		}

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x06007E70 RID: 32368 RVA: 0x001C6454 File Offset: 0x001C4654
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this.Signature.Parameters;
			}
		}

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x06007E71 RID: 32369 RVA: 0x001C6461 File Offset: 0x001C4661
		internal PEParameterSymbol ReturnTypeParameter
		{
			get
			{
				return this.Signature.ReturnParam;
			}
		}

		// Token: 0x17002588 RID: 9608
		// (get) Token: 0x06007E72 RID: 32370 RVA: 0x001C646E File Offset: 0x001C466E
		public override RefKind RefKind
		{
			get
			{
				return this.Signature.ReturnParam.RefKind;
			}
		}

		// Token: 0x17002589 RID: 9609
		// (get) Token: 0x06007E73 RID: 32371 RVA: 0x001C6480 File Offset: 0x001C4680
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this.Signature.ReturnParam.TypeWithAnnotations;
			}
		}

		// Token: 0x1700258A RID: 9610
		// (get) Token: 0x06007E74 RID: 32372 RVA: 0x001C6492 File Offset: 0x001C4692
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return this.Signature.ReturnParam.FlowAnalysisAnnotations;
			}
		}

		// Token: 0x1700258B RID: 9611
		// (get) Token: 0x06007E75 RID: 32373 RVA: 0x001C64A4 File Offset: 0x001C46A4
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return this.Signature.ReturnParam.NotNullIfParameterNotNull;
			}
		}

		// Token: 0x1700258C RID: 9612
		// (get) Token: 0x06007E76 RID: 32374 RVA: 0x001C64B8 File Offset: 0x001C46B8
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				if (!this._packedFlags.IsDoesNotReturnPopulated)
				{
					bool flag = this._containingType.ContainingPEModule.Module.HasDoesNotReturnAttribute(this._handle);
					this._packedFlags.InitializeDoesNotReturn(flag);
				}
				if (!this._packedFlags.DoesNotReturn)
				{
					return FlowAnalysisAnnotations.None;
				}
				return FlowAnalysisAnnotations.DoesNotReturn;
			}
		}

		// Token: 0x1700258D RID: 9613
		// (get) Token: 0x06007E77 RID: 32375 RVA: 0x001C6514 File Offset: 0x001C4714
		internal override ImmutableArray<string> NotNullMembers
		{
			get
			{
				if (!this._packedFlags.IsMemberNotNullPopulated)
				{
					this.PopulateMemberNotNullData();
				}
				PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
				if (uncommonFields == null)
				{
					return ImmutableArray<string>.Empty;
				}
				ImmutableArray<string> lazyNotNullMembers = uncommonFields._lazyNotNullMembers;
				if (!lazyNotNullMembers.IsDefault)
				{
					return lazyNotNullMembers;
				}
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x06007E78 RID: 32376 RVA: 0x001C655C File Offset: 0x001C475C
		private void PopulateMemberNotNullData()
		{
			PEModule module = this._containingType.ContainingPEModule.Module;
			ImmutableArray<string> memberNotNullAttributeValues = module.GetMemberNotNullAttributeValues(this._handle);
			if (!memberNotNullAttributeValues.IsEmpty)
			{
				InterlockedOperations.Initialize<string>(ref this.AccessUncommonFields()._lazyNotNullMembers, memberNotNullAttributeValues);
			}
			ValueTuple<ImmutableArray<string>, ImmutableArray<string>> memberNotNullWhenAttributeValues = module.GetMemberNotNullWhenAttributeValues(this._handle);
			ImmutableArray<string> item = memberNotNullWhenAttributeValues.Item1;
			ImmutableArray<string> item2 = memberNotNullWhenAttributeValues.Item2;
			if (!item.IsEmpty)
			{
				InterlockedOperations.Initialize<string>(ref this.AccessUncommonFields()._lazyNotNullMembersWhenTrue, item);
			}
			if (!item2.IsEmpty)
			{
				InterlockedOperations.Initialize<string>(ref this.AccessUncommonFields()._lazyNotNullMembersWhenFalse, item2);
			}
			this._packedFlags.SetIsMemberNotNullPopulated();
		}

		// Token: 0x1700258E RID: 9614
		// (get) Token: 0x06007E79 RID: 32377 RVA: 0x001C6604 File Offset: 0x001C4804
		internal override ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				if (!this._packedFlags.IsMemberNotNullPopulated)
				{
					this.PopulateMemberNotNullData();
				}
				PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
				if (uncommonFields == null)
				{
					return ImmutableArray<string>.Empty;
				}
				ImmutableArray<string> lazyNotNullMembersWhenTrue = uncommonFields._lazyNotNullMembersWhenTrue;
				if (!lazyNotNullMembersWhenTrue.IsDefault)
				{
					return lazyNotNullMembersWhenTrue;
				}
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x1700258F RID: 9615
		// (get) Token: 0x06007E7A RID: 32378 RVA: 0x001C664C File Offset: 0x001C484C
		internal override ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				if (!this._packedFlags.IsMemberNotNullPopulated)
				{
					this.PopulateMemberNotNullData();
				}
				PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
				if (uncommonFields == null)
				{
					return ImmutableArray<string>.Empty;
				}
				ImmutableArray<string> lazyNotNullMembersWhenFalse = uncommonFields._lazyNotNullMembersWhenFalse;
				if (!lazyNotNullMembersWhenFalse.IsDefault)
				{
					return lazyNotNullMembersWhenFalse;
				}
				return ImmutableArray<string>.Empty;
			}
		}

		// Token: 0x17002590 RID: 9616
		// (get) Token: 0x06007E7B RID: 32379 RVA: 0x001C6693 File Offset: 0x001C4893
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this.Signature.ReturnParam.RefCustomModifiers;
			}
		}

		// Token: 0x06007E7C RID: 32380 RVA: 0x001C66A5 File Offset: 0x001C48A5
		internal bool SetAssociatedProperty(PEPropertySymbol propertySymbol, MethodKind methodKind)
		{
			return this.SetAssociatedPropertyOrEvent(propertySymbol, methodKind);
		}

		// Token: 0x06007E7D RID: 32381 RVA: 0x001C66AF File Offset: 0x001C48AF
		internal bool SetAssociatedEvent(PEEventSymbol eventSymbol, MethodKind methodKind)
		{
			return this.SetAssociatedPropertyOrEvent(eventSymbol, methodKind);
		}

		// Token: 0x06007E7E RID: 32382 RVA: 0x001C66B9 File Offset: 0x001C48B9
		private bool SetAssociatedPropertyOrEvent(Symbol propertyOrEventSymbol, MethodKind methodKind)
		{
			if (this._associatedPropertyOrEventOpt == null)
			{
				this._associatedPropertyOrEventOpt = propertyOrEventSymbol;
				this._packedFlags.MethodKind = methodKind;
				return true;
			}
			return false;
		}

		// Token: 0x17002591 RID: 9617
		// (get) Token: 0x06007E7F RID: 32383 RVA: 0x001C66D9 File Offset: 0x001C48D9
		internal PEMethodSymbol.SignatureData Signature
		{
			get
			{
				return this._lazySignature ?? this.LoadSignature();
			}
		}

		// Token: 0x06007E80 RID: 32384 RVA: 0x001C66EC File Offset: 0x001C48EC
		private PEMethodSymbol.SignatureData LoadSignature()
		{
			PEModuleSymbol containingPEModule = this._containingType.ContainingPEModule;
			SignatureHeader signatureHeader;
			BadImageFormatException ex;
			ParamInfo<TypeSymbol>[] signatureForMethod = new MetadataDecoder(containingPEModule, this).GetSignatureForMethod(this._handle, out signatureHeader, out ex, true);
			bool flag = ex != null;
			if (!signatureHeader.IsGeneric && this._lazyTypeParameters.IsDefault)
			{
				ImmutableInterlocked.InterlockedInitialize<TypeParameterSymbol>(ref this._lazyTypeParameters, ImmutableArray<TypeParameterSymbol>.Empty);
			}
			int num = signatureForMethod.Length - 1;
			bool flag2;
			ImmutableArray<ParameterSymbol> immutableArray;
			if (num > 0)
			{
				ImmutableArray<ParameterSymbol>.Builder builder = ImmutableArray.CreateBuilder<ParameterSymbol>(num);
				for (int i = 0; i < num; i++)
				{
					builder.Add(PEParameterSymbol.Create(containingPEModule, this, this.IsMetadataVirtual(false), i, signatureForMethod[i + 1], this, false, out flag2));
					if (flag2)
					{
						flag = true;
					}
				}
				immutableArray = builder.ToImmutable();
			}
			else
			{
				immutableArray = ImmutableArray<ParameterSymbol>.Empty;
			}
			TypeSymbol typeSymbol = signatureForMethod[0].Type.AsDynamicIfNoPia(this._containingType);
			signatureForMethod[0].Type = typeSymbol;
			PEParameterSymbol peparameterSymbol = PEParameterSymbol.Create(containingPEModule, this, this.IsMetadataVirtual(false), 0, signatureForMethod[0], this, true, out flag2);
			if (flag || flag2)
			{
				this.InitializeUseSiteDiagnostic(new UseSiteInfo<AssemblySymbol>(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this })));
			}
			PEMethodSymbol.SignatureData signatureData = new PEMethodSymbol.SignatureData(signatureHeader, immutableArray, peparameterSymbol);
			return InterlockedOperations.Initialize<PEMethodSymbol.SignatureData>(ref this._lazySignature, signatureData);
		}

		// Token: 0x17002592 RID: 9618
		// (get) Token: 0x06007E81 RID: 32385 RVA: 0x001C6834 File Offset: 0x001C4A34
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				DiagnosticInfo diagnosticInfo = null;
				ImmutableArray<TypeParameterSymbol> immutableArray = this.EnsureTypeParametersAreLoaded(ref diagnosticInfo);
				if (diagnosticInfo != null)
				{
					this.InitializeUseSiteDiagnostic(new UseSiteInfo<AssemblySymbol>(diagnosticInfo));
				}
				return immutableArray;
			}
		}

		// Token: 0x06007E82 RID: 32386 RVA: 0x001C685C File Offset: 0x001C4A5C
		private ImmutableArray<TypeParameterSymbol> EnsureTypeParametersAreLoaded(ref DiagnosticInfo diagnosticInfo)
		{
			ImmutableArray<TypeParameterSymbol> lazyTypeParameters = this._lazyTypeParameters;
			if (!lazyTypeParameters.IsDefault)
			{
				return lazyTypeParameters;
			}
			return InterlockedOperations.Initialize<TypeParameterSymbol>(ref this._lazyTypeParameters, this.LoadTypeParameters(ref diagnosticInfo));
		}

		// Token: 0x06007E83 RID: 32387 RVA: 0x001C6890 File Offset: 0x001C4A90
		private ImmutableArray<TypeParameterSymbol> LoadTypeParameters(ref DiagnosticInfo diagnosticInfo)
		{
			ImmutableArray<TypeParameterSymbol> immutableArray;
			try
			{
				PEModuleSymbol containingPEModule = this._containingType.ContainingPEModule;
				GenericParameterHandleCollection genericParametersForMethodOrThrow = containingPEModule.Module.GetGenericParametersForMethodOrThrow(this._handle);
				if (genericParametersForMethodOrThrow.Count == 0)
				{
					immutableArray = ImmutableArray<TypeParameterSymbol>.Empty;
				}
				else
				{
					ImmutableArray<TypeParameterSymbol>.Builder builder = ImmutableArray.CreateBuilder<TypeParameterSymbol>(genericParametersForMethodOrThrow.Count);
					for (int i = 0; i < genericParametersForMethodOrThrow.Count; i++)
					{
						builder.Add(new PETypeParameterSymbol(containingPEModule, this, (ushort)i, genericParametersForMethodOrThrow[i]));
					}
					immutableArray = builder.ToImmutable();
				}
			}
			catch (BadImageFormatException)
			{
				diagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this });
				immutableArray = ImmutableArray<TypeParameterSymbol>.Empty;
			}
			return immutableArray;
		}

		// Token: 0x17002593 RID: 9619
		// (get) Token: 0x06007E84 RID: 32388 RVA: 0x001C6944 File Offset: 0x001C4B44
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				if (!this.IsGenericMethod)
				{
					return ImmutableArray<TypeWithAnnotations>.Empty;
				}
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17002594 RID: 9620
		// (get) Token: 0x06007E85 RID: 32389 RVA: 0x001C695A File Offset: 0x001C4B5A
		public override Symbol AssociatedSymbol
		{
			get
			{
				return this._associatedPropertyOrEventOpt;
			}
		}

		// Token: 0x17002595 RID: 9621
		// (get) Token: 0x06007E86 RID: 32390 RVA: 0x001C6964 File Offset: 0x001C4B64
		public override bool IsExtensionMethod
		{
			get
			{
				if (!this._packedFlags.IsExtensionMethodIsPopulated)
				{
					bool flag = false;
					if (this.MethodKind == MethodKind.Ordinary && this.IsValidExtensionMethodSignature() && this.ContainingType.MightContainExtensionMethods)
					{
						flag = this._containingType.ContainingPEModule.Module.HasExtensionAttribute(this._handle, false);
					}
					this._packedFlags.InitializeIsExtensionMethod(flag);
				}
				return this._packedFlags.IsExtensionMethod;
			}
		}

		// Token: 0x17002596 RID: 9622
		// (get) Token: 0x06007E87 RID: 32391 RVA: 0x001C69D8 File Offset: 0x001C4BD8
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._containingType.ContainingPEModule.MetadataLocation.Cast<MetadataLocation, Location>();
			}
		}

		// Token: 0x17002597 RID: 9623
		// (get) Token: 0x06007E88 RID: 32392 RVA: 0x001C69EF File Offset: 0x001C4BEF
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06007E89 RID: 32393 RVA: 0x001C69F8 File Offset: 0x001C4BF8
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (!this._packedFlags.IsCustomAttributesPopulated)
			{
				ImmutableArray<CSharpAttributeData> immutableArray = default(ImmutableArray<CSharpAttributeData>);
				PEModuleSymbol containingPEModule = this._containingType.ContainingPEModule;
				bool isExtensionMethodIsPopulated = this._packedFlags.IsExtensionMethodIsPopulated;
				bool flag = (isExtensionMethodIsPopulated ? this._packedFlags.IsExtensionMethod : (this.MethodKind == MethodKind.Ordinary && this.IsValidExtensionMethodSignature() && this._containingType.MightContainExtensionMethods));
				bool isReadOnlyPopulated = this._packedFlags.IsReadOnlyPopulated;
				bool flag2 = (isReadOnlyPopulated ? this._packedFlags.IsReadOnly : base.IsValidReadOnlyTarget);
				bool flag3 = false;
				bool flag4 = false;
				if (flag || flag2)
				{
					containingPEModule.LoadCustomAttributesFilterCompilerAttributes(this._handle, ref immutableArray, out flag3, out flag4);
				}
				else
				{
					containingPEModule.LoadCustomAttributes(this._handle, ref immutableArray);
				}
				if (!isExtensionMethodIsPopulated)
				{
					this._packedFlags.InitializeIsExtensionMethod(flag3);
				}
				if (!isReadOnlyPopulated)
				{
					this._packedFlags.InitializeIsReadOnly(flag4);
				}
				if (!immutableArray.IsEmpty)
				{
					immutableArray = InterlockedOperations.Initialize<CSharpAttributeData>(ref this.AccessUncommonFields()._lazyCustomAttributes, immutableArray);
				}
				this._packedFlags.SetIsCustomAttributesPopulated();
				return immutableArray;
			}
			PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
			if (uncommonFields == null)
			{
				return ImmutableArray<CSharpAttributeData>.Empty;
			}
			ImmutableArray<CSharpAttributeData> lazyCustomAttributes = uncommonFields._lazyCustomAttributes;
			if (!lazyCustomAttributes.IsDefault)
			{
				return lazyCustomAttributes;
			}
			return InterlockedOperations.Initialize<CSharpAttributeData>(ref uncommonFields._lazyCustomAttributes, ImmutableArray<CSharpAttributeData>.Empty);
		}

		// Token: 0x06007E8A RID: 32394 RVA: 0x001C6B3A File Offset: 0x001C4D3A
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.GetAttributes();
		}

		// Token: 0x06007E8B RID: 32395 RVA: 0x001C6B47 File Offset: 0x001C4D47
		public override ImmutableArray<CSharpAttributeData> GetReturnTypeAttributes()
		{
			return this.Signature.ReturnParam.GetAttributes();
		}

		// Token: 0x06007E8C RID: 32396 RVA: 0x001C6B5C File Offset: 0x001C4D5C
		internal override byte? GetNullableContextValue()
		{
			byte? b;
			if (!this._packedFlags.TryGetNullableContext(out b))
			{
				byte b2;
				b = (this._containingType.ContainingPEModule.Module.HasNullableContextAttribute(this._handle, out b2) ? new byte?(b2) : this._containingType.GetNullableContextValue());
				this._packedFlags.SetNullableContext(b);
			}
			return b;
		}

		// Token: 0x06007E8D RID: 32397 RVA: 0x001C6BBE File Offset: 0x001C4DBE
		internal override byte? GetLocalNullableContextValue()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17002598 RID: 9624
		// (get) Token: 0x06007E8E RID: 32398 RVA: 0x001C6BC5 File Offset: 0x001C4DC5
		public override MethodKind MethodKind
		{
			get
			{
				if (!this._packedFlags.MethodKindIsPopulated)
				{
					this._packedFlags.InitializeMethodKind(this.ComputeMethodKind());
				}
				return this._packedFlags.MethodKind;
			}
		}

		// Token: 0x06007E8F RID: 32399 RVA: 0x001C6BF0 File Offset: 0x001C4DF0
		private bool IsValidExtensionMethodSignature()
		{
			if (!this.IsStatic)
			{
				return false;
			}
			ImmutableArray<ParameterSymbol> parameters = this.Parameters;
			if (parameters.Length == 0)
			{
				return false;
			}
			ParameterSymbol parameterSymbol = parameters[0];
			RefKind refKind = parameterSymbol.RefKind;
			return (refKind <= RefKind.Ref || refKind == RefKind.In) && !parameterSymbol.IsParams;
		}

		// Token: 0x06007E90 RID: 32400 RVA: 0x001C6C3C File Offset: 0x001C4E3C
		private bool IsValidUserDefinedOperatorSignature(int parameterCount)
		{
			if (this.ReturnsVoid || this.IsGenericMethod || this.IsVararg || this.ParameterCount != parameterCount || this.IsParams())
			{
				return false;
			}
			if (base.ParameterRefKinds.IsDefault)
			{
				return true;
			}
			foreach (RefKind refKind in base.ParameterRefKinds)
			{
				switch (refKind)
				{
				case RefKind.None:
				case RefKind.In:
					break;
				case RefKind.Ref:
				case RefKind.Out:
					return false;
				default:
					throw ExceptionUtilities.UnexpectedValue(refKind);
				}
			}
			return true;
		}

		// Token: 0x06007E91 RID: 32401 RVA: 0x001C6CD0 File Offset: 0x001C4ED0
		private MethodKind ComputeMethodKind()
		{
			if (this.HasSpecialName)
			{
				if (this._name.StartsWith(".", 4))
				{
					if ((this.Flags & 4160) == 4096 && this._name.Equals(this.IsStatic ? ".cctor" : ".ctor") && this.ReturnsVoid && this.Arity == 0)
					{
						if (!this.IsStatic)
						{
							return MethodKind.Constructor;
						}
						if (this.Parameters.Length == 0)
						{
							return MethodKind.StaticConstructor;
						}
					}
					return MethodKind.Ordinary;
				}
				if (!this.HasRuntimeSpecialName && this.IsStatic && this.DeclaredAccessibility == Accessibility.Public)
				{
					string text = this._name;
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1915672496U)
					{
						if (num <= 1195761148U)
						{
							if (num <= 835846267U)
							{
								if (num != 90588446U)
								{
									if (num != 215197780U)
									{
										if (num != 835846267U)
										{
											return MethodKind.Ordinary;
										}
										if (!(text == "op_BitwiseAnd"))
										{
											return MethodKind.Ordinary;
										}
										goto IL_0445;
									}
									else if (!(text == "op_Implicit"))
									{
										return MethodKind.Ordinary;
									}
								}
								else
								{
									if (!(text == "op_OnesComplement"))
									{
										return MethodKind.Ordinary;
									}
									goto IL_0454;
								}
							}
							else if (num != 906583475U)
							{
								if (num != 1034931220U)
								{
									if (num != 1195761148U)
									{
										return MethodKind.Ordinary;
									}
									if (!(text == "op_GreaterThan"))
									{
										return MethodKind.Ordinary;
									}
									goto IL_0445;
								}
								else
								{
									if (!(text == "op_Increment"))
									{
										return MethodKind.Ordinary;
									}
									goto IL_0454;
								}
							}
							else
							{
								if (!(text == "op_Addition"))
								{
									return MethodKind.Ordinary;
								}
								goto IL_0445;
							}
						}
						else if (num <= 1516143579U)
						{
							if (num != 1234170120U)
							{
								if (num != 1258540185U)
								{
									if (num != 1516143579U)
									{
										return MethodKind.Ordinary;
									}
									if (!(text == "op_Equality"))
									{
										return MethodKind.Ordinary;
									}
									goto IL_0445;
								}
								else
								{
									if (!(text == "op_LessThan"))
									{
										return MethodKind.Ordinary;
									}
									goto IL_0445;
								}
							}
							else
							{
								if (!(text == "op_LessThanOrEqual"))
								{
									return MethodKind.Ordinary;
								}
								goto IL_0445;
							}
						}
						else if (num <= 1587019679U)
						{
							if (num != 1548478473U)
							{
								if (num != 1587019679U)
								{
									return MethodKind.Ordinary;
								}
								if (!(text == "op_Explicit"))
								{
									return MethodKind.Ordinary;
								}
							}
							else
							{
								if (!(text == "op_RightShift"))
								{
									return MethodKind.Ordinary;
								}
								goto IL_0445;
							}
						}
						else if (num != 1850069070U)
						{
							if (num != 1915672496U)
							{
								return MethodKind.Ordinary;
							}
							if (!(text == "op_Division"))
							{
								return MethodKind.Ordinary;
							}
							goto IL_0445;
						}
						else
						{
							if (!(text == "op_False"))
							{
								return MethodKind.Ordinary;
							}
							goto IL_0454;
						}
						if (!this.IsValidUserDefinedOperatorSignature(1))
						{
							return MethodKind.Ordinary;
						}
						return MethodKind.Conversion;
					}
					else if (num <= 2574677899U)
					{
						if (num <= 2429678952U)
						{
							if (num != 2242295702U)
							{
								if (num != 2366795836U)
								{
									if (num != 2429678952U)
									{
										return MethodKind.Ordinary;
									}
									if (!(text == "op_Modulus"))
									{
										return MethodKind.Ordinary;
									}
								}
								else if (!(text == "op_ExclusiveOr"))
								{
									return MethodKind.Ordinary;
								}
							}
							else if (!(text == "op_LeftShift"))
							{
								return MethodKind.Ordinary;
							}
						}
						else if (num != 2459852411U)
						{
							if (num != 2536726348U)
							{
								if (num != 2574677899U)
								{
									return MethodKind.Ordinary;
								}
								if (!(text == "op_LogicalNot"))
								{
									return MethodKind.Ordinary;
								}
								goto IL_0454;
							}
							else
							{
								if (!(text == "op_Decrement"))
								{
									return MethodKind.Ordinary;
								}
								goto IL_0454;
							}
						}
						else if (!(text == "op_GreaterThanOrEqual"))
						{
							return MethodKind.Ordinary;
						}
					}
					else if (num <= 3279419199U)
					{
						if (num != 2958252495U)
						{
							if (num != 3075696130U)
							{
								if (num != 3279419199U)
								{
									return MethodKind.Ordinary;
								}
								if (!(text == "op_Subtraction"))
								{
									return MethodKind.Ordinary;
								}
							}
							else
							{
								if (!(text == "op_UnaryPlus"))
								{
									return MethodKind.Ordinary;
								}
								goto IL_0454;
							}
						}
						else if (!(text == "op_Multiply"))
						{
							return MethodKind.Ordinary;
						}
					}
					else if (num <= 3568900899U)
					{
						if (num != 3492550567U)
						{
							if (num != 3568900899U)
							{
								return MethodKind.Ordinary;
							}
							if (!(text == "op_True"))
							{
								return MethodKind.Ordinary;
							}
							goto IL_0454;
						}
						else if (!(text == "op_BitwiseOr"))
						{
							return MethodKind.Ordinary;
						}
					}
					else if (num != 3716665893U)
					{
						if (num != 3794317784U)
						{
							return MethodKind.Ordinary;
						}
						if (!(text == "op_Inequality"))
						{
							return MethodKind.Ordinary;
						}
					}
					else
					{
						if (!(text == "op_UnaryNegation"))
						{
							return MethodKind.Ordinary;
						}
						goto IL_0454;
					}
					IL_0445:
					if (!this.IsValidUserDefinedOperatorSignature(2))
					{
						return MethodKind.Ordinary;
					}
					return MethodKind.UserDefinedOperator;
					IL_0454:
					if (!this.IsValidUserDefinedOperatorSignature(1))
					{
						return MethodKind.Ordinary;
					}
					return MethodKind.UserDefinedOperator;
				}
			}
			if (!this.IsStatic)
			{
				string text = this._name;
				if (!(text == "Finalize"))
				{
					if (!(text == "Invoke"))
					{
						if (!SyntaxFacts.IsValidIdentifier(this.Name) && !this.ExplicitInterfaceImplementations.IsEmpty)
						{
							return MethodKind.ExplicitInterfaceImplementation;
						}
					}
					else if (this._containingType.TypeKind == TypeKind.Delegate)
					{
						return MethodKind.DelegateInvoke;
					}
				}
				else if ((this.ContainingType.TypeKind == TypeKind.Class && this.IsRuntimeFinalizer(true)) || this.IsExplicitFinalizerOverride)
				{
					return MethodKind.Destructor;
				}
			}
			return MethodKind.Ordinary;
		}

		// Token: 0x17002599 RID: 9625
		// (get) Token: 0x06007E92 RID: 32402 RVA: 0x001C71D0 File Offset: 0x001C53D0
		internal override CallingConvention CallingConvention
		{
			get
			{
				return (CallingConvention)this.Signature.Header.RawValue;
			}
		}

		// Token: 0x1700259A RID: 9626
		// (get) Token: 0x06007E93 RID: 32403 RVA: 0x001C71F0 File Offset: 0x001C53F0
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				ImmutableArray<MethodSymbol> immutableArray = this._lazyExplicitMethodImplementations;
				if (!immutableArray.IsDefault)
				{
					return immutableArray;
				}
				ImmutableArray<MethodSymbol> explicitlyOverriddenMethods = new MetadataDecoder(this._containingType.ContainingPEModule, this._containingType).GetExplicitlyOverriddenMethods(this._containingType.Handle, this._handle, this.ContainingType);
				bool flag = false;
				bool flag2 = false;
				foreach (MethodSymbol methodSymbol in explicitlyOverriddenMethods)
				{
					if (!methodSymbol.ContainingType.IsInterface)
					{
						flag = true;
						flag2 = methodSymbol.ContainingType.SpecialType == SpecialType.System_Object && methodSymbol.Name == "Finalize" && methodSymbol.MethodKind == MethodKind.Destructor;
					}
					if (flag && flag2)
					{
						break;
					}
				}
				immutableArray = explicitlyOverriddenMethods;
				if (flag)
				{
					ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
					foreach (MethodSymbol methodSymbol2 in explicitlyOverriddenMethods)
					{
						if (methodSymbol2.ContainingType.IsInterface)
						{
							instance.Add(methodSymbol2);
						}
					}
					immutableArray = instance.ToImmutableAndFree();
					MethodSymbol methodSymbol3 = null;
					foreach (MethodSymbol methodSymbol4 in explicitlyOverriddenMethods)
					{
						if (methodSymbol4.ContainingType.IsClassType())
						{
							if (methodSymbol3 != null)
							{
								methodSymbol3 = null;
								break;
							}
							methodSymbol3 = methodSymbol4;
						}
					}
					if (methodSymbol3 != null)
					{
						Interlocked.CompareExchange<MethodSymbol>(ref this.AccessUncommonFields()._lazyExplicitClassOverride, methodSymbol3, null);
					}
				}
				this._packedFlags.InitializeIsExplicitOverride(flag2, flag);
				return InterlockedOperations.Initialize<MethodSymbol>(ref this._lazyExplicitMethodImplementations, immutableArray);
			}
		}

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x06007E94 RID: 32404 RVA: 0x001C735F File Offset: 0x001C555F
		internal MethodSymbol ExplicitlyOverriddenClassMethod
		{
			get
			{
				if (!this.IsExplicitClassOverride)
				{
					return null;
				}
				return this.AccessUncommonFields()._lazyExplicitClassOverride;
			}
		}

		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x06007E95 RID: 32405 RVA: 0x001C7378 File Offset: 0x001C5578
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				if (!this._packedFlags.IsReadOnlyPopulated)
				{
					bool flag = false;
					if (base.IsValidReadOnlyTarget)
					{
						flag = this._containingType.ContainingPEModule.Module.HasIsReadOnlyAttribute(this._handle);
					}
					this._packedFlags.InitializeIsReadOnly(flag);
				}
				return this._packedFlags.IsReadOnly;
			}
		}

		// Token: 0x1700259D RID: 9629
		// (get) Token: 0x06007E96 RID: 32406 RVA: 0x001C73D4 File Offset: 0x001C55D4
		internal override bool IsInitOnly
		{
			get
			{
				if (!this._packedFlags.IsInitOnlyPopulated)
				{
					bool flag = !this.IsStatic && this.MethodKind == MethodKind.PropertySet && this.ReturnTypeWithAnnotations.CustomModifiers.HasIsExternalInitModifier();
					this._packedFlags.InitializeIsInitOnly(flag);
				}
				return this._packedFlags.IsInitOnly;
			}
		}

		// Token: 0x06007E97 RID: 32407 RVA: 0x001C742E File Offset: 0x001C562E
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return PEDocumentationCommentUtils.GetDocumentationComment(this, this._containingType.ContainingPEModule, preferredCulture, cancellationToken, ref this.AccessUncommonFields()._lazyDocComment);
		}

		// Token: 0x06007E98 RID: 32408 RVA: 0x001C7450 File Offset: 0x001C5650
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (!this._packedFlags.IsUseSiteDiagnosticPopulated)
			{
				UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
				base.CalculateUseSiteDiagnostic(ref useSiteInfo);
				DiagnosticInfo diagnosticInfo = useSiteInfo.DiagnosticInfo;
				this.EnsureTypeParametersAreLoaded(ref diagnosticInfo);
				if (diagnosticInfo == null && this.GetUnmanagedCallersOnlyAttributeData(true) != null && base.CheckAndReportValidUnmanagedCallersOnlyTarget(null, null))
				{
					diagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { this });
				}
				return this.InitializeUseSiteDiagnostic(useSiteInfo.AdjustDiagnosticInfo(diagnosticInfo));
			}
			return this.GetCachedUseSiteInfo();
		}

		// Token: 0x06007E99 RID: 32409 RVA: 0x001C74D0 File Offset: 0x001C56D0
		private UseSiteInfo<AssemblySymbol> GetCachedUseSiteInfo()
		{
			PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
			return ((uncommonFields != null) ? uncommonFields._lazyCachedUseSiteInfo : default(CachedUseSiteInfo<AssemblySymbol>)).ToUseSiteInfo(base.PrimaryDependency);
		}

		// Token: 0x06007E9A RID: 32410 RVA: 0x001C7508 File Offset: 0x001C5708
		private UseSiteInfo<AssemblySymbol> InitializeUseSiteDiagnostic(UseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			if (this._packedFlags.IsUseSiteDiagnosticPopulated)
			{
				return this.GetCachedUseSiteInfo();
			}
			if (useSiteInfo.DiagnosticInfo != null || !useSiteInfo.SecondaryDependencies.IsNullOrEmpty<AssemblySymbol>())
			{
				useSiteInfo = this.AccessUncommonFields()._lazyCachedUseSiteInfo.InterlockedInitialize(base.PrimaryDependency, useSiteInfo);
			}
			this._packedFlags.SetIsUseSiteDiagnosticPopulated();
			return useSiteInfo;
		}

		// Token: 0x06007E9B RID: 32411 RVA: 0x001C7564 File Offset: 0x001C5764
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			if (!this._packedFlags.IsConditionalPopulated)
			{
				ImmutableArray<string> immutableArray = this._containingType.ContainingPEModule.Module.GetConditionalAttributeValues(this._handle);
				if (!immutableArray.IsEmpty)
				{
					immutableArray = InterlockedOperations.Initialize<string>(ref this.AccessUncommonFields()._lazyConditionalAttributeSymbols, immutableArray);
				}
				this._packedFlags.SetIsConditionalAttributePopulated();
				return immutableArray;
			}
			PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
			if (uncommonFields == null)
			{
				return ImmutableArray<string>.Empty;
			}
			ImmutableArray<string> lazyConditionalAttributeSymbols = uncommonFields._lazyConditionalAttributeSymbols;
			if (!lazyConditionalAttributeSymbols.IsDefault)
			{
				return lazyConditionalAttributeSymbols;
			}
			return InterlockedOperations.Initialize<string>(ref uncommonFields._lazyConditionalAttributeSymbols, ImmutableArray<string>.Empty);
		}

		// Token: 0x06007E9C RID: 32412 RVA: 0x001C75F9 File Offset: 0x001C57F9
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x06007E9D RID: 32413 RVA: 0x001C7600 File Offset: 0x001C5800
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				if (!this._packedFlags.IsObsoleteAttributePopulated)
				{
					ObsoleteAttributeData obsoleteAttributeData = ObsoleteAttributeHelpers.GetObsoleteDataFromMetadata(this._handle, (PEModuleSymbol)this.ContainingModule, false);
					if (obsoleteAttributeData != null)
					{
						obsoleteAttributeData = InterlockedOperations.Initialize<ObsoleteAttributeData>(ref this.AccessUncommonFields()._lazyObsoleteAttributeData, obsoleteAttributeData, ObsoleteAttributeData.Uninitialized);
					}
					this._packedFlags.SetIsObsoleteAttributePopulated();
					return obsoleteAttributeData;
				}
				PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
				if (uncommonFields == null)
				{
					return null;
				}
				ObsoleteAttributeData lazyObsoleteAttributeData = uncommonFields._lazyObsoleteAttributeData;
				if (lazyObsoleteAttributeData != ObsoleteAttributeData.Uninitialized)
				{
					return lazyObsoleteAttributeData;
				}
				return InterlockedOperations.Initialize<ObsoleteAttributeData>(ref uncommonFields._lazyObsoleteAttributeData, null, ObsoleteAttributeData.Uninitialized);
			}
		}

		// Token: 0x06007E9E RID: 32414 RVA: 0x001C768C File Offset: 0x001C588C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			if (!this._packedFlags.IsUnmanagedCallersOnlyAttributePopulated)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this.ContainingModule;
				UnmanagedCallersOnlyAttributeData unmanagedCallersOnlyAttributeData = pemoduleSymbol.Module.TryGetUnmanagedCallersOnlyAttribute(this._handle, new MetadataDecoder(pemoduleSymbol), (string name, TypedConstant value, bool isField) => MethodSymbol.TryDecodeUnmanagedCallersOnlyCallConvsField(name, value, isField, null, null));
				UnmanagedCallersOnlyAttributeData unmanagedCallersOnlyAttributeData2 = InterlockedOperations.Initialize<UnmanagedCallersOnlyAttributeData>(ref this.AccessUncommonFields()._lazyUnmanagedCallersOnlyAttributeData, unmanagedCallersOnlyAttributeData, UnmanagedCallersOnlyAttributeData.Uninitialized);
				this._packedFlags.SetIsUnmanagedCallersOnlyAttributePopulated();
				return unmanagedCallersOnlyAttributeData2;
			}
			PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
			if (uncommonFields == null)
			{
				return null;
			}
			return uncommonFields._lazyUnmanagedCallersOnlyAttributeData;
		}

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x06007E9F RID: 32415 RVA: 0x001C7721 File Offset: 0x001C5921
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x06007EA0 RID: 32416 RVA: 0x001C7724 File Offset: 0x001C5924
		internal override OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				if (!this._packedFlags.IsOverriddenOrHiddenMembersPopulated)
				{
					OverriddenOrHiddenMembersResult overriddenOrHiddenMembersResult = base.OverriddenOrHiddenMembers;
					if (overriddenOrHiddenMembersResult != OverriddenOrHiddenMembersResult.Empty)
					{
						overriddenOrHiddenMembersResult = InterlockedOperations.Initialize<OverriddenOrHiddenMembersResult>(ref this.AccessUncommonFields()._lazyOverriddenOrHiddenMembersResult, overriddenOrHiddenMembersResult);
					}
					this._packedFlags.SetIsOverriddenOrHiddenMembersPopulated();
					return overriddenOrHiddenMembersResult;
				}
				PEMethodSymbol.UncommonFields uncommonFields = this._uncommonFields;
				if (uncommonFields == null)
				{
					return OverriddenOrHiddenMembersResult.Empty;
				}
				return uncommonFields._lazyOverriddenOrHiddenMembersResult ?? InterlockedOperations.Initialize<OverriddenOrHiddenMembersResult>(ref uncommonFields._lazyOverriddenOrHiddenMembersResult, OverriddenOrHiddenMembersResult.Empty);
			}
		}

		// Token: 0x06007EA1 RID: 32417 RVA: 0x001C7796 File Offset: 0x001C5996
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007EA2 RID: 32418 RVA: 0x001C779D File Offset: 0x001C599D
		internal override void AddSynthesizedReturnTypeAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x06007EA3 RID: 32419 RVA: 0x001C77A4 File Offset: 0x001C59A4
		public override bool AreLocalsZeroed
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x06007EA4 RID: 32420 RVA: 0x001C77AB File Offset: 0x001C59AB
		internal override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x06007EA5 RID: 32421 RVA: 0x001C77AE File Offset: 0x001C59AE
		internal bool TestIsExtensionBitSet
		{
			get
			{
				return this._packedFlags.IsExtensionMethodIsPopulated;
			}
		}

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x06007EA6 RID: 32422 RVA: 0x001C77BB File Offset: 0x001C59BB
		internal bool TestIsExtensionBitTrue
		{
			get
			{
				return this._packedFlags.IsExtensionMethod;
			}
		}

		// Token: 0x06007EA7 RID: 32423 RVA: 0x001C77C8 File Offset: 0x001C59C8
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001DCB RID: 7627
		private readonly MethodDefinitionHandle _handle;

		// Token: 0x04001DCC RID: 7628
		private readonly string _name;

		// Token: 0x04001DCD RID: 7629
		private readonly PENamedTypeSymbol _containingType;

		// Token: 0x04001DCE RID: 7630
		private Symbol _associatedPropertyOrEventOpt;

		// Token: 0x04001DCF RID: 7631
		private PEMethodSymbol.PackedFlags _packedFlags;

		// Token: 0x04001DD0 RID: 7632
		private readonly ushort _flags;

		// Token: 0x04001DD1 RID: 7633
		private readonly ushort _implFlags;

		// Token: 0x04001DD2 RID: 7634
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x04001DD3 RID: 7635
		private PEMethodSymbol.SignatureData _lazySignature;

		// Token: 0x04001DD4 RID: 7636
		private ImmutableArray<MethodSymbol> _lazyExplicitMethodImplementations;

		// Token: 0x04001DD5 RID: 7637
		private PEMethodSymbol.UncommonFields _uncommonFields;

		// Token: 0x0200097D RID: 2429
		internal class SignatureData
		{
			// Token: 0x0600920D RID: 37389 RVA: 0x001FF723 File Offset: 0x001FD923
			public SignatureData(SignatureHeader header, ImmutableArray<ParameterSymbol> parameters, PEParameterSymbol returnParam)
			{
				this.Header = header;
				this.Parameters = parameters;
				this.ReturnParam = returnParam;
			}

			// Token: 0x040028D5 RID: 10453
			public readonly SignatureHeader Header;

			// Token: 0x040028D6 RID: 10454
			public readonly ImmutableArray<ParameterSymbol> Parameters;

			// Token: 0x040028D7 RID: 10455
			public readonly PEParameterSymbol ReturnParam;
		}

		// Token: 0x0200097E RID: 2430
		private struct PackedFlags
		{
			// Token: 0x17002901 RID: 10497
			// (get) Token: 0x0600920E RID: 37390 RVA: 0x001FF740 File Offset: 0x001FD940
			// (set) Token: 0x0600920F RID: 37391 RVA: 0x001FF74B File Offset: 0x001FD94B
			public MethodKind MethodKind
			{
				get
				{
					return (MethodKind)(this._bits & 31);
				}
				set
				{
					this._bits = (this._bits & -32) | (int)(value & (MethodKind)31) | 32;
				}
			}

			// Token: 0x17002902 RID: 10498
			// (get) Token: 0x06009210 RID: 37392 RVA: 0x001FF764 File Offset: 0x001FD964
			public bool MethodKindIsPopulated
			{
				get
				{
					return (this._bits & 32) != 0;
				}
			}

			// Token: 0x17002903 RID: 10499
			// (get) Token: 0x06009211 RID: 37393 RVA: 0x001FF772 File Offset: 0x001FD972
			public bool IsExtensionMethod
			{
				get
				{
					return (this._bits & 64) != 0;
				}
			}

			// Token: 0x17002904 RID: 10500
			// (get) Token: 0x06009212 RID: 37394 RVA: 0x001FF780 File Offset: 0x001FD980
			public bool IsExtensionMethodIsPopulated
			{
				get
				{
					return (this._bits & 128) != 0;
				}
			}

			// Token: 0x17002905 RID: 10501
			// (get) Token: 0x06009213 RID: 37395 RVA: 0x001FF791 File Offset: 0x001FD991
			public bool IsExplicitFinalizerOverride
			{
				get
				{
					return (this._bits & 256) != 0;
				}
			}

			// Token: 0x17002906 RID: 10502
			// (get) Token: 0x06009214 RID: 37396 RVA: 0x001FF7A2 File Offset: 0x001FD9A2
			public bool IsExplicitClassOverride
			{
				get
				{
					return (this._bits & 512) != 0;
				}
			}

			// Token: 0x17002907 RID: 10503
			// (get) Token: 0x06009215 RID: 37397 RVA: 0x001FF7B3 File Offset: 0x001FD9B3
			public bool IsExplicitOverrideIsPopulated
			{
				get
				{
					return (this._bits & 1024) != 0;
				}
			}

			// Token: 0x17002908 RID: 10504
			// (get) Token: 0x06009216 RID: 37398 RVA: 0x001FF7C4 File Offset: 0x001FD9C4
			public bool IsObsoleteAttributePopulated
			{
				get
				{
					return (this._bits & 2048) != 0;
				}
			}

			// Token: 0x17002909 RID: 10505
			// (get) Token: 0x06009217 RID: 37399 RVA: 0x001FF7D5 File Offset: 0x001FD9D5
			public bool IsCustomAttributesPopulated
			{
				get
				{
					return (this._bits & 4096) != 0;
				}
			}

			// Token: 0x1700290A RID: 10506
			// (get) Token: 0x06009218 RID: 37400 RVA: 0x001FF7E6 File Offset: 0x001FD9E6
			public bool IsUseSiteDiagnosticPopulated
			{
				get
				{
					return (this._bits & 8192) != 0;
				}
			}

			// Token: 0x1700290B RID: 10507
			// (get) Token: 0x06009219 RID: 37401 RVA: 0x001FF7F7 File Offset: 0x001FD9F7
			public bool IsConditionalPopulated
			{
				get
				{
					return (this._bits & 16384) != 0;
				}
			}

			// Token: 0x1700290C RID: 10508
			// (get) Token: 0x0600921A RID: 37402 RVA: 0x001FF808 File Offset: 0x001FDA08
			public bool IsOverriddenOrHiddenMembersPopulated
			{
				get
				{
					return (this._bits & 32768) != 0;
				}
			}

			// Token: 0x1700290D RID: 10509
			// (get) Token: 0x0600921B RID: 37403 RVA: 0x001FF819 File Offset: 0x001FDA19
			public bool IsReadOnly
			{
				get
				{
					return (this._bits & 65536) != 0;
				}
			}

			// Token: 0x1700290E RID: 10510
			// (get) Token: 0x0600921C RID: 37404 RVA: 0x001FF82A File Offset: 0x001FDA2A
			public bool IsReadOnlyPopulated
			{
				get
				{
					return (this._bits & 131072) != 0;
				}
			}

			// Token: 0x1700290F RID: 10511
			// (get) Token: 0x0600921D RID: 37405 RVA: 0x001FF83B File Offset: 0x001FDA3B
			public bool DoesNotReturn
			{
				get
				{
					return (this._bits & 2097152) != 0;
				}
			}

			// Token: 0x17002910 RID: 10512
			// (get) Token: 0x0600921E RID: 37406 RVA: 0x001FF84C File Offset: 0x001FDA4C
			public bool IsDoesNotReturnPopulated
			{
				get
				{
					return (this._bits & 4194304) != 0;
				}
			}

			// Token: 0x17002911 RID: 10513
			// (get) Token: 0x0600921F RID: 37407 RVA: 0x001FF85D File Offset: 0x001FDA5D
			public bool IsMemberNotNullPopulated
			{
				get
				{
					return (this._bits & 8388608) != 0;
				}
			}

			// Token: 0x17002912 RID: 10514
			// (get) Token: 0x06009220 RID: 37408 RVA: 0x001FF86E File Offset: 0x001FDA6E
			public bool IsInitOnly
			{
				get
				{
					return (this._bits & 16777216) != 0;
				}
			}

			// Token: 0x17002913 RID: 10515
			// (get) Token: 0x06009221 RID: 37409 RVA: 0x001FF87F File Offset: 0x001FDA7F
			public bool IsInitOnlyPopulated
			{
				get
				{
					return (this._bits & 33554432) != 0;
				}
			}

			// Token: 0x17002914 RID: 10516
			// (get) Token: 0x06009222 RID: 37410 RVA: 0x001FF890 File Offset: 0x001FDA90
			public bool IsUnmanagedCallersOnlyAttributePopulated
			{
				get
				{
					return (this._bits & 67108864) != 0;
				}
			}

			// Token: 0x06009223 RID: 37411 RVA: 0x001FF8A1 File Offset: 0x001FDAA1
			private static bool BitsAreUnsetOrSame(int bits, int mask)
			{
				return (bits & mask) == 0 || (bits & mask) == mask;
			}

			// Token: 0x06009224 RID: 37412 RVA: 0x001FF8B0 File Offset: 0x001FDAB0
			public void InitializeIsExtensionMethod(bool isExtensionMethod)
			{
				int num = (isExtensionMethod ? 64 : 0) | 128;
				ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009225 RID: 37413 RVA: 0x001FF8DC File Offset: 0x001FDADC
			public void InitializeIsReadOnly(bool isReadOnly)
			{
				int num = (isReadOnly ? 65536 : 0) | 131072;
				ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009226 RID: 37414 RVA: 0x001FF908 File Offset: 0x001FDB08
			public void InitializeMethodKind(MethodKind methodKind)
			{
				int num = (int)((methodKind & (MethodKind)31) | (MethodKind)32);
				ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009227 RID: 37415 RVA: 0x001FF92C File Offset: 0x001FDB2C
			public void InitializeIsExplicitOverride(bool isExplicitFinalizerOverride, bool isExplicitClassOverride)
			{
				int num = (isExplicitFinalizerOverride ? 256 : 0) | (isExplicitClassOverride ? 512 : 0) | 1024;
				ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009228 RID: 37416 RVA: 0x001FF964 File Offset: 0x001FDB64
			public void SetIsObsoleteAttributePopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 2048);
			}

			// Token: 0x06009229 RID: 37417 RVA: 0x001FF977 File Offset: 0x001FDB77
			public void SetIsCustomAttributesPopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 4096);
			}

			// Token: 0x0600922A RID: 37418 RVA: 0x001FF98A File Offset: 0x001FDB8A
			public void SetIsUseSiteDiagnosticPopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 8192);
			}

			// Token: 0x0600922B RID: 37419 RVA: 0x001FF99D File Offset: 0x001FDB9D
			public void SetIsConditionalAttributePopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 16384);
			}

			// Token: 0x0600922C RID: 37420 RVA: 0x001FF9B0 File Offset: 0x001FDBB0
			public void SetIsOverriddenOrHiddenMembersPopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 32768);
			}

			// Token: 0x0600922D RID: 37421 RVA: 0x001FF9C3 File Offset: 0x001FDBC3
			public bool TryGetNullableContext(out byte? value)
			{
				return ((NullableContextKind)((this._bits >> 18) & 7)).TryGetByte(out value);
			}

			// Token: 0x0600922E RID: 37422 RVA: 0x001FF9D7 File Offset: 0x001FDBD7
			public bool SetNullableContext(byte? value)
			{
				return ThreadSafeFlagOperations.Set(ref this._bits, (int)((int)(value.ToNullableContextFlags() & (NullableContextKind)7) << 18));
			}

			// Token: 0x0600922F RID: 37423 RVA: 0x001FF9F0 File Offset: 0x001FDBF0
			public bool InitializeDoesNotReturn(bool value)
			{
				int num = 4194304;
				if (value)
				{
					num |= 2097152;
				}
				return ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009230 RID: 37424 RVA: 0x001FFA1A File Offset: 0x001FDC1A
			public void SetIsMemberNotNullPopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 8388608);
			}

			// Token: 0x06009231 RID: 37425 RVA: 0x001FFA30 File Offset: 0x001FDC30
			public void InitializeIsInitOnly(bool isInitOnly)
			{
				int num = (isInitOnly ? 16777216 : 0) | 33554432;
				ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009232 RID: 37426 RVA: 0x001FFA5C File Offset: 0x001FDC5C
			public void SetIsUnmanagedCallersOnlyAttributePopulated()
			{
				ThreadSafeFlagOperations.Set(ref this._bits, 67108864);
			}

			// Token: 0x040028D8 RID: 10456
			private const int MethodKindOffset = 0;

			// Token: 0x040028D9 RID: 10457
			private const int MethodKindMask = 31;

			// Token: 0x040028DA RID: 10458
			private const int MethodKindIsPopulatedBit = 32;

			// Token: 0x040028DB RID: 10459
			private const int IsExtensionMethodBit = 64;

			// Token: 0x040028DC RID: 10460
			private const int IsExtensionMethodIsPopulatedBit = 128;

			// Token: 0x040028DD RID: 10461
			private const int IsExplicitFinalizerOverrideBit = 256;

			// Token: 0x040028DE RID: 10462
			private const int IsExplicitClassOverrideBit = 512;

			// Token: 0x040028DF RID: 10463
			private const int IsExplicitOverrideIsPopulatedBit = 1024;

			// Token: 0x040028E0 RID: 10464
			private const int IsObsoleteAttributePopulatedBit = 2048;

			// Token: 0x040028E1 RID: 10465
			private const int IsCustomAttributesPopulatedBit = 4096;

			// Token: 0x040028E2 RID: 10466
			private const int IsUseSiteDiagnosticPopulatedBit = 8192;

			// Token: 0x040028E3 RID: 10467
			private const int IsConditionalPopulatedBit = 16384;

			// Token: 0x040028E4 RID: 10468
			private const int IsOverriddenOrHiddenMembersPopulatedBit = 32768;

			// Token: 0x040028E5 RID: 10469
			private const int IsReadOnlyBit = 65536;

			// Token: 0x040028E6 RID: 10470
			private const int IsReadOnlyPopulatedBit = 131072;

			// Token: 0x040028E7 RID: 10471
			private const int NullableContextOffset = 18;

			// Token: 0x040028E8 RID: 10472
			private const int NullableContextMask = 7;

			// Token: 0x040028E9 RID: 10473
			private const int DoesNotReturnBit = 2097152;

			// Token: 0x040028EA RID: 10474
			private const int IsDoesNotReturnPopulatedBit = 4194304;

			// Token: 0x040028EB RID: 10475
			private const int IsMemberNotNullPopulatedBit = 8388608;

			// Token: 0x040028EC RID: 10476
			private const int IsInitOnlyBit = 16777216;

			// Token: 0x040028ED RID: 10477
			private const int IsInitOnlyPopulatedBit = 33554432;

			// Token: 0x040028EE RID: 10478
			private const int IsUnmanagedCallersOnlyAttributePopulatedBit = 67108864;

			// Token: 0x040028EF RID: 10479
			private int _bits;
		}

		// Token: 0x0200097F RID: 2431
		private sealed class UncommonFields
		{
			// Token: 0x040028F0 RID: 10480
			public ParameterSymbol _lazyThisParameter;

			// Token: 0x040028F1 RID: 10481
			public Tuple<CultureInfo, string> _lazyDocComment;

			// Token: 0x040028F2 RID: 10482
			public OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembersResult;

			// Token: 0x040028F3 RID: 10483
			public ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

			// Token: 0x040028F4 RID: 10484
			public ImmutableArray<string> _lazyConditionalAttributeSymbols;

			// Token: 0x040028F5 RID: 10485
			public ObsoleteAttributeData _lazyObsoleteAttributeData;

			// Token: 0x040028F6 RID: 10486
			public UnmanagedCallersOnlyAttributeData _lazyUnmanagedCallersOnlyAttributeData;

			// Token: 0x040028F7 RID: 10487
			public CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo;

			// Token: 0x040028F8 RID: 10488
			public ImmutableArray<string> _lazyNotNullMembers;

			// Token: 0x040028F9 RID: 10489
			public ImmutableArray<string> _lazyNotNullMembersWhenTrue;

			// Token: 0x040028FA RID: 10490
			public ImmutableArray<string> _lazyNotNullMembersWhenFalse;

			// Token: 0x040028FB RID: 10491
			public MethodSymbol _lazyExplicitClassOverride;
		}
	}
}
