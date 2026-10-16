using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200054B RID: 1355
	internal abstract class SourceParameterSymbolBase : ParameterSymbol
	{
		// Token: 0x060072C8 RID: 29384 RVA: 0x001AD2EA File Offset: 0x001AB4EA
		public SourceParameterSymbolBase(Symbol containingSymbol, int ordinal)
		{
			this._ordinal = (ushort)ordinal;
			this._containingSymbol = containingSymbol;
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x001AD304 File Offset: 0x001AB504
		public sealed override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (obj == this)
			{
				return true;
			}
			NativeIntegerParameterSymbol nativeIntegerParameterSymbol = obj as NativeIntegerParameterSymbol;
			if (nativeIntegerParameterSymbol != null)
			{
				return nativeIntegerParameterSymbol.Equals(this, compareKind);
			}
			SourceParameterSymbolBase sourceParameterSymbolBase = obj as SourceParameterSymbolBase;
			return sourceParameterSymbolBase != null && sourceParameterSymbolBase.Ordinal == this.Ordinal && sourceParameterSymbolBase._containingSymbol.Equals(this._containingSymbol, compareKind);
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x001AD356 File Offset: 0x001AB556
		public sealed override int GetHashCode()
		{
			return Hash.Combine(this._containingSymbol.GetHashCode(), this.Ordinal);
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x060072CB RID: 29387 RVA: 0x001AD36E File Offset: 0x001AB56E
		public sealed override int Ordinal
		{
			get
			{
				return (int)this._ordinal;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x001AD376 File Offset: 0x001AB576
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingSymbol;
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x060072CD RID: 29389 RVA: 0x001AD37E File Offset: 0x001AB57E
		public sealed override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._containingSymbol.ContainingAssembly;
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x060072CE RID: 29390
		internal abstract ConstantValue DefaultValueFromAttributes { get; }

		// Token: 0x060072CF RID: 29391 RVA: 0x001AD38C File Offset: 0x001AB58C
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (this.IsParams)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_ParamArrayAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
			}
			ConstantValue explicitDefaultConstantValue = this.ExplicitDefaultConstantValue;
			if (explicitDefaultConstantValue != null && explicitDefaultConstantValue.SpecialType == SpecialType.System_Decimal && this.DefaultValueFromAttributes == null)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDecimalConstantAttribute(explicitDefaultConstantValue.DecimalValue));
			}
			TypeWithAnnotations typeWithAnnotations = this.TypeWithAnnotations;
			if (typeWithAnnotations.Type.ContainsDynamic())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(typeWithAnnotations.Type, typeWithAnnotations.CustomModifiers.Length + this.RefCustomModifiers.Length, this.RefKind));
			}
			if (typeWithAnnotations.Type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, typeWithAnnotations.Type));
			}
			if (typeWithAnnotations.Type.ContainsTupleNames())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(typeWithAnnotations.Type));
			}
			if (this.RefKind == RefKind.In)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsReadOnlyAttribute(this));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.GetNullableContextValue(), typeWithAnnotations));
			}
		}

		// Token: 0x060072D0 RID: 29392
		internal abstract ParameterSymbol WithCustomModifiersAndParams(TypeSymbol newType, ImmutableArray<CustomModifier> newCustomModifiers, ImmutableArray<CustomModifier> newRefCustomModifiers, bool newIsParams);

		// Token: 0x04001BEF RID: 7151
		private readonly Symbol _containingSymbol;

		// Token: 0x04001BF0 RID: 7152
		private readonly ushort _ordinal;
	}
}
