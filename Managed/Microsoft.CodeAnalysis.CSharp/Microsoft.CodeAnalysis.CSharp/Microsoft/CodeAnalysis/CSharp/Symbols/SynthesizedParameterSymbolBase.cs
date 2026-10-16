using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000599 RID: 1433
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SynthesizedParameterSymbolBase : ParameterSymbol
	{
		// Token: 0x060077F2 RID: 30706 RVA: 0x001B88E1 File Offset: 0x001B6AE1
		public SynthesizedParameterSymbolBase([global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol container, TypeWithAnnotations type, int ordinal, RefKind refKind, string name = "")
		{
			this._container = container;
			this._type = type;
			this._ordinal = ordinal;
			this._refKind = refKind;
			this._name = name;
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x060077F3 RID: 30707 RVA: 0x001B890E File Offset: 0x001B6B0E
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x060077F4 RID: 30708 RVA: 0x001B8916 File Offset: 0x001B6B16
		public override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x060077F5 RID: 30709 RVA: 0x001B891E File Offset: 0x001B6B1E
		public sealed override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x060077F6 RID: 30710 RVA: 0x001B8921 File Offset: 0x001B6B21
		internal override bool IsMetadataIn
		{
			get
			{
				return this.RefKind == RefKind.In;
			}
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x060077F7 RID: 30711 RVA: 0x001B892C File Offset: 0x001B6B2C
		internal override bool IsMetadataOut
		{
			get
			{
				return this.RefKind == RefKind.Out;
			}
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x060077F8 RID: 30712 RVA: 0x001B8937 File Offset: 0x001B6B37
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x060077F9 RID: 30713
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x060077FA RID: 30714 RVA: 0x001B893F File Offset: 0x001B6B3F
		public override int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x060077FB RID: 30715 RVA: 0x001B8947 File Offset: 0x001B6B47
		public override bool IsParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x060077FC RID: 30716 RVA: 0x001B894A File Offset: 0x001B6B4A
		internal override bool IsMetadataOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x060077FD RID: 30717 RVA: 0x001B894D File Offset: 0x001B6B4D
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x060077FE RID: 30718 RVA: 0x001B8950 File Offset: 0x001B6B50
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ConstantValue ExplicitDefaultConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x060077FF RID: 30719 RVA: 0x001B8953 File Offset: 0x001B6B53
		internal override bool IsIDispatchConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x06007800 RID: 30720 RVA: 0x001B8956 File Offset: 0x001B6B56
		internal override bool IsIUnknownConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x06007801 RID: 30721 RVA: 0x001B8959 File Offset: 0x001B6B59
		internal override bool IsCallerLineNumber
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x06007802 RID: 30722 RVA: 0x001B895C File Offset: 0x001B6B5C
		internal override bool IsCallerFilePath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x06007803 RID: 30723 RVA: 0x001B895F File Offset: 0x001B6B5F
		internal override bool IsCallerMemberName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x06007804 RID: 30724 RVA: 0x001B8962 File Offset: 0x001B6B62
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x06007805 RID: 30725 RVA: 0x001B8965 File Offset: 0x001B6B65
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x06007806 RID: 30726 RVA: 0x001B896C File Offset: 0x001B6B6C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ContainingSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._container;
			}
		}

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x06007807 RID: 30727 RVA: 0x001B8974 File Offset: 0x001B6B74
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x06007808 RID: 30728 RVA: 0x001B897B File Offset: 0x001B6B7B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06007809 RID: 30729 RVA: 0x001B8984 File Offset: 0x001B6B84
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			TypeWithAnnotations typeWithAnnotations = this.TypeWithAnnotations;
			if (typeWithAnnotations.Type.ContainsDynamic() && declaringCompilation.HasDynamicEmitAttributes(BindingDiagnosticBag.Discarded, Location.None) && declaringCompilation.CanEmitBoolean())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDynamicAttribute(typeWithAnnotations.Type, typeWithAnnotations.CustomModifiers.Length + this.RefCustomModifiers.Length, this.RefKind));
			}
			if (typeWithAnnotations.Type.ContainsNativeInteger())
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNativeIntegerAttribute(this, typeWithAnnotations.Type));
			}
			if (typeWithAnnotations.Type.ContainsTupleNames() && declaringCompilation.HasTupleNamesAttributes(BindingDiagnosticBag.Discarded, Location.None) && declaringCompilation.CanEmitSpecialType(SpecialType.System_String))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeTupleNamesAttribute(typeWithAnnotations.Type));
			}
			if (declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableAttributeIfNecessary(this, this.GetNullableContextValue(), typeWithAnnotations));
			}
			if (this.RefKind == RefKind.In)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsReadOnlyAttribute(this));
			}
		}

		// Token: 0x04001CEE RID: 7406
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly MethodSymbol _container;

		// Token: 0x04001CEF RID: 7407
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001CF0 RID: 7408
		private readonly int _ordinal;

		// Token: 0x04001CF1 RID: 7409
		private readonly string _name;

		// Token: 0x04001CF2 RID: 7410
		private readonly RefKind _refKind;
	}
}
