using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005EF RID: 1519
	internal class PEParameterSymbol : ParameterSymbol
	{
		// Token: 0x06007F60 RID: 32608 RVA: 0x001CA680 File Offset: 0x001C8880
		internal static PEParameterSymbol Create(PEModuleSymbol moduleSymbol, PEMethodSymbol containingSymbol, bool isContainingSymbolVirtual, int ordinal, ParamInfo<TypeSymbol> parameterInfo, Symbol nullableContext, bool isReturn, out bool isBad)
		{
			return PEParameterSymbol.Create(moduleSymbol, containingSymbol, isContainingSymbolVirtual, ordinal, parameterInfo.IsByRef, parameterInfo.RefCustomModifiers, parameterInfo.Type, parameterInfo.Handle, nullableContext, parameterInfo.CustomModifiers, isReturn, out isBad);
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x001CA6C0 File Offset: 0x001C88C0
		internal static PEParameterSymbol Create(PEModuleSymbol moduleSymbol, PEPropertySymbol containingSymbol, bool isContainingSymbolVirtual, int ordinal, ParameterHandle handle, ParamInfo<TypeSymbol> parameterInfo, Symbol nullableContext, out bool isBad)
		{
			return PEParameterSymbol.Create(moduleSymbol, containingSymbol, isContainingSymbolVirtual, ordinal, parameterInfo.IsByRef, parameterInfo.RefCustomModifiers, parameterInfo.Type, handle, nullableContext, parameterInfo.CustomModifiers, false, out isBad);
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x001CA6FC File Offset: 0x001C88FC
		private PEParameterSymbol(PEModuleSymbol moduleSymbol, Symbol containingSymbol, int ordinal, bool isByRef, TypeWithAnnotations typeWithAnnotations, ParameterHandle handle, Symbol nullableContext, int countOfCustomModifiers, out bool isBad)
		{
			isBad = false;
			this._moduleSymbol = moduleSymbol;
			this._containingSymbol = containingSymbol;
			this._ordinal = (ushort)ordinal;
			this._handle = handle;
			RefKind refKind = RefKind.None;
			if (handle.IsNil)
			{
				refKind = (isByRef ? RefKind.Ref : RefKind.None);
				byte? nullableContextValue = nullableContext.GetNullableContextValue();
				if (nullableContextValue != null)
				{
					typeWithAnnotations = NullableTypeDecoder.TransformType(typeWithAnnotations, nullableContextValue.GetValueOrDefault(), default(ImmutableArray<byte>));
				}
				this._lazyCustomAttributes = ImmutableArray<CSharpAttributeData>.Empty;
				this._lazyHiddenAttributes = ImmutableArray<CSharpAttributeData>.Empty;
				this._lazyDefaultValue = null;
				this._lazyIsParams = ThreeState.False;
			}
			else
			{
				try
				{
					moduleSymbol.Module.GetParamPropsOrThrow(handle, out this._name, out this._flags);
				}
				catch (BadImageFormatException)
				{
					isBad = true;
				}
				if (isByRef)
				{
					if ((this._flags & 3) == 2)
					{
						refKind = RefKind.Out;
					}
					else if (moduleSymbol.Module.HasIsReadOnlyAttribute(handle))
					{
						refKind = RefKind.In;
					}
					else
					{
						refKind = RefKind.Ref;
					}
				}
				TypeSymbol typeSymbol = DynamicTypeDecoder.TransformType(typeWithAnnotations.Type, countOfCustomModifiers, handle, moduleSymbol, refKind);
				typeSymbol = NativeIntegerTypeDecoder.TransformType(typeSymbol, handle, moduleSymbol);
				typeWithAnnotations = typeWithAnnotations.WithTypeAndModifiers(typeSymbol, typeWithAnnotations.CustomModifiers);
				Symbol symbol = ((containingSymbol.Kind == SymbolKind.Property) ? containingSymbol.ContainingSymbol : containingSymbol);
				typeWithAnnotations = NullableTypeDecoder.TransformType(typeWithAnnotations, handle, moduleSymbol, symbol, nullableContext);
				typeWithAnnotations = TupleTypeDecoder.DecodeTupleTypesIfApplicable(typeWithAnnotations, handle, moduleSymbol);
			}
			this._typeWithAnnotations = typeWithAnnotations;
			bool flag = !string.IsNullOrEmpty(this._name);
			if (!flag)
			{
				this._name = "value";
			}
			this._packedFlags = new PEParameterSymbol.PackedFlags(refKind, handle.IsNil, flag);
		}

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x06007F63 RID: 32611 RVA: 0x001CA8B4 File Offset: 0x001C8AB4
		private bool HasNameInMetadata
		{
			get
			{
				return this._packedFlags.HasNameInMetadata;
			}
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x001CA8C4 File Offset: 0x001C8AC4
		private static PEParameterSymbol Create(PEModuleSymbol moduleSymbol, Symbol containingSymbol, bool isContainingSymbolVirtual, int ordinal, bool isByRef, ImmutableArray<ModifierInfo<TypeSymbol>> refCustomModifiers, TypeSymbol type, ParameterHandle handle, Symbol nullableContext, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers, bool isReturn, out bool isBad)
		{
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(type, NullableAnnotation.Oblivious, CSharpCustomModifier.Convert(customModifiers));
			PEParameterSymbol peparameterSymbol = ((customModifiers.IsDefaultOrEmpty && refCustomModifiers.IsDefaultOrEmpty) ? new PEParameterSymbol(moduleSymbol, containingSymbol, ordinal, isByRef, typeWithAnnotations, handle, nullableContext, 0, out isBad) : new PEParameterSymbol.PEParameterSymbolWithCustomModifiers(moduleSymbol, containingSymbol, ordinal, isByRef, refCustomModifiers, typeWithAnnotations, handle, nullableContext, out isBad));
			bool flag = peparameterSymbol.RefCustomModifiers.HasInAttributeModifier();
			if (isReturn)
			{
				isBad |= peparameterSymbol.RefKind == RefKind.In != flag;
			}
			else if (peparameterSymbol.RefKind == RefKind.In)
			{
				isBad |= isContainingSymbolVirtual != flag;
			}
			else if (flag)
			{
				isBad = true;
			}
			return peparameterSymbol;
		}

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x06007F65 RID: 32613 RVA: 0x001CA964 File Offset: 0x001C8B64
		public override RefKind RefKind
		{
			get
			{
				return this._packedFlags.RefKind;
			}
		}

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x06007F66 RID: 32614 RVA: 0x001CA971 File Offset: 0x001C8B71
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x06007F67 RID: 32615 RVA: 0x001CA979 File Offset: 0x001C8B79
		public override string MetadataName
		{
			get
			{
				if (!this.HasNameInMetadata)
				{
					return string.Empty;
				}
				return this._name;
			}
		}

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x06007F68 RID: 32616 RVA: 0x001CA98F File Offset: 0x001C8B8F
		internal ParameterAttributes Flags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x170025FA RID: 9722
		// (get) Token: 0x06007F69 RID: 32617 RVA: 0x001CA997 File Offset: 0x001C8B97
		public override int Ordinal
		{
			get
			{
				return (int)this._ordinal;
			}
		}

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x06007F6A RID: 32618 RVA: 0x001CA99F File Offset: 0x001C8B9F
		public override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170025FC RID: 9724
		// (get) Token: 0x06007F6B RID: 32619 RVA: 0x001CA9A2 File Offset: 0x001C8BA2
		internal ParameterHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x170025FD RID: 9725
		// (get) Token: 0x06007F6C RID: 32620 RVA: 0x001CA9AA File Offset: 0x001C8BAA
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x170025FE RID: 9726
		// (get) Token: 0x06007F6D RID: 32621 RVA: 0x001CA9B2 File Offset: 0x001C8BB2
		internal override bool HasMetadataConstantValue
		{
			get
			{
				return (this._flags & 4096) > 0;
			}
		}

		// Token: 0x06007F6E RID: 32622 RVA: 0x001CA9C4 File Offset: 0x001C8BC4
		internal ConstantValue ImportConstantValue(bool ignoreAttributes = false)
		{
			ConstantValue constantValue = null;
			if ((this._flags & 4096) != null)
			{
				constantValue = this._moduleSymbol.Module.GetParamDefaultValue(this._handle);
			}
			if (constantValue == null && !ignoreAttributes)
			{
				constantValue = this.GetDefaultDecimalOrDateTimeValue();
			}
			return constantValue;
		}

		// Token: 0x170025FF RID: 9727
		// (get) Token: 0x06007F6F RID: 32623 RVA: 0x001CAA0C File Offset: 0x001C8C0C
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			get
			{
				if (this._lazyDefaultValue == ConstantValue.Unset)
				{
					ConstantValue constantValue = this.ImportConstantValue(!this.IsMetadataOptional);
					Interlocked.CompareExchange<ConstantValue>(ref this._lazyDefaultValue, constantValue, ConstantValue.Unset);
				}
				return this._lazyDefaultValue;
			}
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x001CAA54 File Offset: 0x001C8C54
		private ConstantValue GetDefaultDecimalOrDateTimeValue()
		{
			ConstantValue constantValue = null;
			if (this._moduleSymbol.Module.HasDateTimeConstantAttribute(this._handle, out constantValue))
			{
				return constantValue;
			}
			this._moduleSymbol.Module.HasDecimalConstantAttribute(this._handle, out constantValue);
			return constantValue;
		}

		// Token: 0x17002600 RID: 9728
		// (get) Token: 0x06007F71 RID: 32625 RVA: 0x001CAAA3 File Offset: 0x001C8CA3
		internal override bool IsMetadataOptional
		{
			get
			{
				return (this._flags & 16) > 0;
			}
		}

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x06007F72 RID: 32626 RVA: 0x001CAAB4 File Offset: 0x001C8CB4
		internal override bool IsIDispatchConstant
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasIDispatchConstantAttribute, out flag))
				{
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasIDispatchConstantAttribute, this._moduleSymbol.Module.HasAttribute(this._handle, AttributeDescription.IDispatchConstantAttribute));
				}
				return flag;
			}
		}

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x001CAB00 File Offset: 0x001C8D00
		internal override bool IsIUnknownConstant
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasIUnknownConstantAttribute, out flag))
				{
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasIUnknownConstantAttribute, this._moduleSymbol.Module.HasAttribute(this._handle, AttributeDescription.IUnknownConstantAttribute));
				}
				return flag;
			}
		}

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x06007F74 RID: 32628 RVA: 0x001CAB4C File Offset: 0x001C8D4C
		private bool HasCallerLineNumberAttribute
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasCallerLineNumberAttribute, out flag))
				{
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasCallerLineNumberAttribute, this._moduleSymbol.Module.HasAttribute(this._handle, AttributeDescription.CallerLineNumberAttribute));
				}
				return flag;
			}
		}

		// Token: 0x17002604 RID: 9732
		// (get) Token: 0x06007F75 RID: 32629 RVA: 0x001CAB98 File Offset: 0x001C8D98
		private bool HasCallerFilePathAttribute
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasCallerFilePathAttribute, out flag))
				{
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasCallerFilePathAttribute, this._moduleSymbol.Module.HasAttribute(this._handle, AttributeDescription.CallerFilePathAttribute));
				}
				return flag;
			}
		}

		// Token: 0x17002605 RID: 9733
		// (get) Token: 0x06007F76 RID: 32630 RVA: 0x001CABE4 File Offset: 0x001C8DE4
		private bool HasCallerMemberNameAttribute
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasCallerMemberNameAttribute, out flag))
				{
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.HasCallerMemberNameAttribute, this._moduleSymbol.Module.HasAttribute(this._handle, AttributeDescription.CallerMemberNameAttribute));
				}
				return flag;
			}
		}

		// Token: 0x17002606 RID: 9734
		// (get) Token: 0x06007F77 RID: 32631 RVA: 0x001CAC34 File Offset: 0x001C8E34
		internal override bool IsCallerLineNumber
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.IsCallerLineNumber, out flag))
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					bool flag2 = this.HasCallerLineNumberAttribute && new TypeConversions(this.ContainingAssembly, false).HasCallerLineNumberConversion(base.Type, ref discarded);
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.IsCallerLineNumber, flag2);
				}
				return flag;
			}
		}

		// Token: 0x17002607 RID: 9735
		// (get) Token: 0x06007F78 RID: 32632 RVA: 0x001CAC90 File Offset: 0x001C8E90
		internal override bool IsCallerFilePath
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.IsCallerFilePath, out flag))
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					bool flag2 = !this.HasCallerLineNumberAttribute && this.HasCallerFilePathAttribute && new TypeConversions(this.ContainingAssembly, false).HasCallerInfoStringConversion(base.Type, ref discarded);
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.IsCallerFilePath, flag2);
				}
				return flag;
			}
		}

		// Token: 0x17002608 RID: 9736
		// (get) Token: 0x06007F79 RID: 32633 RVA: 0x001CACF4 File Offset: 0x001C8EF4
		internal override bool IsCallerMemberName
		{
			get
			{
				bool flag;
				if (!this._packedFlags.TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.IsCallerMemberName, out flag))
				{
					CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
					bool flag2 = !this.HasCallerLineNumberAttribute && !this.HasCallerFilePathAttribute && this.HasCallerMemberNameAttribute && new TypeConversions(this.ContainingAssembly, false).HasCallerInfoStringConversion(base.Type, ref discarded);
					flag = this._packedFlags.SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags.IsCallerMemberName, flag2);
				}
				return flag;
			}
		}

		// Token: 0x17002609 RID: 9737
		// (get) Token: 0x06007F7A RID: 32634 RVA: 0x001CAD64 File Offset: 0x001C8F64
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				FlowAnalysisAnnotations flowAnalysisAnnotations;
				if (!this._packedFlags.TryGetFlowAnalysisAnnotations(out flowAnalysisAnnotations))
				{
					flowAnalysisAnnotations = PEParameterSymbol.DecodeFlowAnalysisAttributes(this._moduleSymbol.Module, this._handle);
					this._packedFlags.SetFlowAnalysisAnnotations(flowAnalysisAnnotations);
				}
				return flowAnalysisAnnotations;
			}
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x001CADA8 File Offset: 0x001C8FA8
		private static FlowAnalysisAnnotations DecodeFlowAnalysisAttributes(PEModule module, ParameterHandle handle)
		{
			FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
			if (module.HasAttribute(handle, AttributeDescription.AllowNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
			}
			if (module.HasAttribute(handle, AttributeDescription.DisallowNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
			}
			bool flag;
			if (module.HasAttribute(handle, AttributeDescription.MaybeNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.MaybeNull;
			}
			else if (module.HasMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(handle, AttributeDescription.MaybeNullWhenAttribute, out flag))
			{
				flowAnalysisAnnotations |= (flag ? FlowAnalysisAnnotations.MaybeNullWhenTrue : FlowAnalysisAnnotations.MaybeNullWhenFalse);
			}
			bool flag2;
			if (module.HasAttribute(handle, AttributeDescription.NotNullAttribute))
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.NotNull;
			}
			else if (module.HasMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(handle, AttributeDescription.NotNullWhenAttribute, out flag2))
			{
				flowAnalysisAnnotations |= (flag2 ? FlowAnalysisAnnotations.NotNullWhenTrue : FlowAnalysisAnnotations.NotNullWhenFalse);
			}
			bool flag3;
			if (module.HasMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(handle, AttributeDescription.DoesNotReturnIfAttribute, out flag3))
			{
				flowAnalysisAnnotations |= (flag3 ? FlowAnalysisAnnotations.DoesNotReturnIfTrue : FlowAnalysisAnnotations.DoesNotReturnIfFalse);
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x1700260A RID: 9738
		// (get) Token: 0x06007F7C RID: 32636 RVA: 0x001CAE7E File Offset: 0x001C907E
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return this._moduleSymbol.Module.GetStringValuesOfNotNullIfNotNullAttribute(this._handle);
			}
		}

		// Token: 0x1700260B RID: 9739
		// (get) Token: 0x06007F7D RID: 32637 RVA: 0x001CAE9B File Offset: 0x001C909B
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._typeWithAnnotations;
			}
		}

		// Token: 0x1700260C RID: 9740
		// (get) Token: 0x06007F7E RID: 32638 RVA: 0x001CAEA3 File Offset: 0x001C90A3
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x06007F7F RID: 32639 RVA: 0x001CAEAA File Offset: 0x001C90AA
		internal override bool IsMetadataIn
		{
			get
			{
				return (this._flags & 1) > 0;
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x06007F80 RID: 32640 RVA: 0x001CAEB7 File Offset: 0x001C90B7
		internal override bool IsMetadataOut
		{
			get
			{
				return (this._flags & 2) > 0;
			}
		}

		// Token: 0x1700260F RID: 9743
		// (get) Token: 0x06007F81 RID: 32641 RVA: 0x001CAEC4 File Offset: 0x001C90C4
		internal override bool IsMarshalledExplicitly
		{
			get
			{
				return (this._flags & 8192) > 0;
			}
		}

		// Token: 0x17002610 RID: 9744
		// (get) Token: 0x06007F82 RID: 32642 RVA: 0x001CAED5 File Offset: 0x001C90D5
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002611 RID: 9745
		// (get) Token: 0x06007F83 RID: 32643 RVA: 0x001CAED8 File Offset: 0x001C90D8
		internal override ImmutableArray<byte> MarshallingDescriptor
		{
			get
			{
				if ((this._flags & 8192) == null)
				{
					return default(ImmutableArray<byte>);
				}
				return this._moduleSymbol.Module.GetMarshallingDescriptor(this._handle);
			}
		}

		// Token: 0x17002612 RID: 9746
		// (get) Token: 0x06007F84 RID: 32644 RVA: 0x001CAF18 File Offset: 0x001C9118
		internal override UnmanagedType MarshallingType
		{
			get
			{
				if ((this._flags & 8192) == null)
				{
					return 0;
				}
				return this._moduleSymbol.Module.GetMarshallingType(this._handle);
			}
		}

		// Token: 0x17002613 RID: 9747
		// (get) Token: 0x06007F85 RID: 32645 RVA: 0x001CAF45 File Offset: 0x001C9145
		public override bool IsParams
		{
			get
			{
				if (!this._lazyIsParams.HasValue())
				{
					this._lazyIsParams = this._moduleSymbol.Module.HasParamsAttribute(this._handle).ToThreeState();
				}
				return this._lazyIsParams.Value();
			}
		}

		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x06007F86 RID: 32646 RVA: 0x001CAF85 File Offset: 0x001C9185
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this._containingSymbol.Locations;
			}
		}

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x06007F87 RID: 32647 RVA: 0x001CAF92 File Offset: 0x001C9192
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x001CAF9C File Offset: 0x001C919C
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			if (this._lazyCustomAttributes.IsDefault)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)this.ContainingModule;
				bool flag = !this._lazyIsParams.HasValue() || this._lazyIsParams.Value();
				ConstantValue explicitDefaultConstantValue = this.ExplicitDefaultConstantValue;
				AttributeDescription attributeDescription = default(AttributeDescription);
				if (explicitDefaultConstantValue != null)
				{
					if (explicitDefaultConstantValue.Discriminator == ConstantValueTypeDiscriminator.DateTime)
					{
						attributeDescription = AttributeDescription.DateTimeConstantAttribute;
					}
					else if (explicitDefaultConstantValue.Discriminator == ConstantValueTypeDiscriminator.Decimal)
					{
						attributeDescription = AttributeDescription.DecimalConstantAttribute;
					}
				}
				bool flag2 = this.RefKind == RefKind.In;
				if (flag || attributeDescription.Signatures != null || flag2)
				{
					CustomAttributeHandle customAttributeHandle;
					CustomAttributeHandle customAttributeHandle2;
					CustomAttributeHandle customAttributeHandle3;
					CustomAttributeHandle customAttributeHandle4;
					ImmutableArray<CSharpAttributeData> customAttributesForToken = pemoduleSymbol.GetCustomAttributesForToken(this._handle, out customAttributeHandle, flag ? AttributeDescription.ParamArrayAttribute : default(AttributeDescription), out customAttributeHandle2, attributeDescription, out customAttributeHandle3, flag2 ? AttributeDescription.IsReadOnlyAttribute : default(AttributeDescription), out customAttributeHandle4, default(AttributeDescription));
					if (!customAttributeHandle.IsNil || !customAttributeHandle2.IsNil)
					{
						ArrayBuilder<CSharpAttributeData> instance = ArrayBuilder<CSharpAttributeData>.GetInstance();
						if (!customAttributeHandle.IsNil)
						{
							instance.Add(new PEAttributeData(pemoduleSymbol, customAttributeHandle));
						}
						if (!customAttributeHandle2.IsNil)
						{
							instance.Add(new PEAttributeData(pemoduleSymbol, customAttributeHandle2));
						}
						ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyHiddenAttributes, instance.ToImmutableAndFree());
					}
					else
					{
						ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyHiddenAttributes, ImmutableArray<CSharpAttributeData>.Empty);
					}
					if (!this._lazyIsParams.HasValue())
					{
						this._lazyIsParams = (!customAttributeHandle.IsNil).ToThreeState();
					}
					ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyCustomAttributes, customAttributesForToken);
				}
				else
				{
					ImmutableInterlocked.InterlockedInitialize<CSharpAttributeData>(ref this._lazyHiddenAttributes, ImmutableArray<CSharpAttributeData>.Empty);
					pemoduleSymbol.LoadCustomAttributes(this._handle, ref this._lazyCustomAttributes);
				}
			}
			return this._lazyCustomAttributes;
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x001CB155 File Offset: 0x001C9355
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			foreach (CSharpAttributeData csharpAttributeData in this.GetAttributes())
			{
				yield return csharpAttributeData;
			}
			ImmutableArray<CSharpAttributeData>.Enumerator enumerator = default(ImmutableArray<CSharpAttributeData>.Enumerator);
			foreach (CSharpAttributeData csharpAttributeData2 in this._lazyHiddenAttributes)
			{
				yield return csharpAttributeData2;
			}
			enumerator = default(ImmutableArray<CSharpAttributeData>.Enumerator);
			yield break;
		}

		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x06007F8A RID: 32650 RVA: 0x001CB165 File Offset: 0x001C9365
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x001CB168 File Offset: 0x001C9368
		public sealed override bool Equals(Symbol other, TypeCompareKind compareKind)
		{
			NativeIntegerParameterSymbol nativeIntegerParameterSymbol = other as NativeIntegerParameterSymbol;
			if (nativeIntegerParameterSymbol == null)
			{
				return base.Equals(other, compareKind);
			}
			return nativeIntegerParameterSymbol.Equals(this, compareKind);
		}

		// Token: 0x04001E03 RID: 7683
		private readonly Symbol _containingSymbol;

		// Token: 0x04001E04 RID: 7684
		private readonly string _name;

		// Token: 0x04001E05 RID: 7685
		private readonly TypeWithAnnotations _typeWithAnnotations;

		// Token: 0x04001E06 RID: 7686
		private readonly ParameterHandle _handle;

		// Token: 0x04001E07 RID: 7687
		private readonly ParameterAttributes _flags;

		// Token: 0x04001E08 RID: 7688
		private readonly PEModuleSymbol _moduleSymbol;

		// Token: 0x04001E09 RID: 7689
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001E0A RID: 7690
		private ConstantValue _lazyDefaultValue = ConstantValue.Unset;

		// Token: 0x04001E0B RID: 7691
		private ThreeState _lazyIsParams;

		// Token: 0x04001E0C RID: 7692
		private ImmutableArray<CSharpAttributeData> _lazyHiddenAttributes;

		// Token: 0x04001E0D RID: 7693
		private readonly ushort _ordinal;

		// Token: 0x04001E0E RID: 7694
		private PEParameterSymbol.PackedFlags _packedFlags;

		// Token: 0x0200098D RID: 2445
		[Flags]
		private enum WellKnownAttributeFlags
		{
			// Token: 0x04002949 RID: 10569
			HasIDispatchConstantAttribute = 1,
			// Token: 0x0400294A RID: 10570
			HasIUnknownConstantAttribute = 2,
			// Token: 0x0400294B RID: 10571
			HasCallerFilePathAttribute = 4,
			// Token: 0x0400294C RID: 10572
			HasCallerLineNumberAttribute = 8,
			// Token: 0x0400294D RID: 10573
			HasCallerMemberNameAttribute = 16,
			// Token: 0x0400294E RID: 10574
			IsCallerFilePath = 32,
			// Token: 0x0400294F RID: 10575
			IsCallerLineNumber = 64,
			// Token: 0x04002950 RID: 10576
			IsCallerMemberName = 128
		}

		// Token: 0x0200098E RID: 2446
		private struct PackedFlags
		{
			// Token: 0x17002929 RID: 10537
			// (get) Token: 0x06009282 RID: 37506 RVA: 0x00200952 File Offset: 0x001FEB52
			public RefKind RefKind
			{
				get
				{
					return (RefKind)((this._bits >> 16) & 3);
				}
			}

			// Token: 0x1700292A RID: 10538
			// (get) Token: 0x06009283 RID: 37507 RVA: 0x00200960 File Offset: 0x001FEB60
			public bool HasNameInMetadata
			{
				get
				{
					return (this._bits & 262144) != 0;
				}
			}

			// Token: 0x06009284 RID: 37508 RVA: 0x00200974 File Offset: 0x001FEB74
			public PackedFlags(RefKind refKind, bool attributesAreComplete, bool hasNameInMetadata)
			{
				int num = (int)((int)(refKind & RefKind.In) << 16);
				int num2 = (attributesAreComplete ? 65280 : 0);
				int num3 = (hasNameInMetadata ? 262144 : 0);
				this._bits = num | num2 | num3;
			}

			// Token: 0x06009285 RID: 37509 RVA: 0x002009AC File Offset: 0x001FEBAC
			public bool SetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags flag, bool value)
			{
				int num = (int)((int)flag << 8);
				if (value)
				{
					num |= (int)flag;
				}
				ThreadSafeFlagOperations.Set(ref this._bits, num);
				return value;
			}

			// Token: 0x06009286 RID: 37510 RVA: 0x002009D4 File Offset: 0x001FEBD4
			public bool TryGetWellKnownAttribute(PEParameterSymbol.WellKnownAttributeFlags flag, out bool value)
			{
				int bits = this._bits;
				value = (bits & (int)flag) != 0;
				return (bits & (int)((int)flag << 8)) != 0;
			}

			// Token: 0x06009287 RID: 37511 RVA: 0x002009F8 File Offset: 0x001FEBF8
			public bool SetFlowAnalysisAnnotations(FlowAnalysisAnnotations value)
			{
				int num = (int)((FlowAnalysisAnnotations)524288 | ((int)(value & (FlowAnalysisAnnotations.AllowNull | FlowAnalysisAnnotations.DisallowNull | FlowAnalysisAnnotations.MaybeNullWhenTrue | FlowAnalysisAnnotations.MaybeNullWhenFalse | FlowAnalysisAnnotations.NotNullWhenTrue | FlowAnalysisAnnotations.NotNullWhenFalse | FlowAnalysisAnnotations.DoesNotReturnIfFalse | FlowAnalysisAnnotations.DoesNotReturnIfTrue)) << 20));
				return ThreadSafeFlagOperations.Set(ref this._bits, num);
			}

			// Token: 0x06009288 RID: 37512 RVA: 0x00200A24 File Offset: 0x001FEC24
			public bool TryGetFlowAnalysisAnnotations(out FlowAnalysisAnnotations value)
			{
				int bits = this._bits;
				value = (FlowAnalysisAnnotations)((bits >> 20) & 255);
				return (bits & 524288) != 0;
			}

			// Token: 0x04002951 RID: 10577
			private const int WellKnownAttributeDataOffset = 0;

			// Token: 0x04002952 RID: 10578
			private const int WellKnownAttributeCompletionFlagOffset = 8;

			// Token: 0x04002953 RID: 10579
			private const int RefKindOffset = 16;

			// Token: 0x04002954 RID: 10580
			private const int FlowAnalysisAnnotationsOffset = 20;

			// Token: 0x04002955 RID: 10581
			private const int RefKindMask = 3;

			// Token: 0x04002956 RID: 10582
			private const int WellKnownAttributeDataMask = 255;

			// Token: 0x04002957 RID: 10583
			private const int WellKnownAttributeCompletionFlagMask = 255;

			// Token: 0x04002958 RID: 10584
			private const int FlowAnalysisAnnotationsMask = 255;

			// Token: 0x04002959 RID: 10585
			private const int HasNameInMetadataBit = 262144;

			// Token: 0x0400295A RID: 10586
			private const int FlowAnalysisAnnotationsCompletionBit = 524288;

			// Token: 0x0400295B RID: 10587
			private const int AllWellKnownAttributesCompleteNoData = 65280;

			// Token: 0x0400295C RID: 10588
			private int _bits;
		}

		// Token: 0x0200098F RID: 2447
		private sealed class PEParameterSymbolWithCustomModifiers : PEParameterSymbol
		{
			// Token: 0x06009289 RID: 37513 RVA: 0x00200A50 File Offset: 0x001FEC50
			public PEParameterSymbolWithCustomModifiers(PEModuleSymbol moduleSymbol, Symbol containingSymbol, int ordinal, bool isByRef, ImmutableArray<ModifierInfo<TypeSymbol>> refCustomModifiers, TypeWithAnnotations type, ParameterHandle handle, Symbol nullableContext, out bool isBad)
				: base(moduleSymbol, containingSymbol, ordinal, isByRef, type, handle, nullableContext, refCustomModifiers.NullToEmpty<ModifierInfo<TypeSymbol>>().Length + type.CustomModifiers.Length, out isBad)
			{
				this._refCustomModifiers = CSharpCustomModifier.Convert(refCustomModifiers);
			}

			// Token: 0x1700292B RID: 10539
			// (get) Token: 0x0600928A RID: 37514 RVA: 0x00200A9C File Offset: 0x001FEC9C
			public override ImmutableArray<CustomModifier> RefCustomModifiers
			{
				get
				{
					return this._refCustomModifiers;
				}
			}

			// Token: 0x0400295D RID: 10589
			private readonly ImmutableArray<CustomModifier> _refCustomModifiers;
		}
	}
}
