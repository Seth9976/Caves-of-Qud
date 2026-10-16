using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000592 RID: 1426
	internal abstract class SynthesizedImplementationMethod : SynthesizedInstanceMethodSymbol
	{
		// Token: 0x06007707 RID: 30471 RVA: 0x001B7DD8 File Offset: 0x001B5FD8
		public SynthesizedImplementationMethod(MethodSymbol interfaceMethod, NamedTypeSymbol implementingType, string name = null, bool generateDebugInfo = true, PropertySymbol associatedProperty = null)
		{
			this._name = name ?? ExplicitInterfaceHelpers.GetMemberName(interfaceMethod.Name, interfaceMethod.ContainingType, null);
			this._implementingType = implementingType;
			this._generateDebugInfo = generateDebugInfo;
			this._associatedProperty = associatedProperty;
			this._explicitInterfaceImplementations = ImmutableArray.Create<MethodSymbol>(interfaceMethod);
			(interfaceMethod.ContainingType.TypeSubstitution ?? TypeMap.Empty).WithAlphaRename(interfaceMethod, this, out this._typeParameters);
			this._interfaceMethod = interfaceMethod.ConstructIfGeneric(this.TypeArgumentsWithAnnotations);
			this._parameters = SynthesizedParameterSymbol.DeriveParameters(this._interfaceMethod, this);
		}

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x06007708 RID: 30472 RVA: 0x001B7E71 File Offset: 0x001B6071
		public sealed override bool IsVararg
		{
			get
			{
				return this._interfaceMethod.IsVararg;
			}
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x06007709 RID: 30473 RVA: 0x001B7E7E File Offset: 0x001B607E
		public sealed override int Arity
		{
			get
			{
				return this._interfaceMethod.Arity;
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x0600770A RID: 30474 RVA: 0x001B7E8B File Offset: 0x001B608B
		public sealed override bool ReturnsVoid
		{
			get
			{
				return this._interfaceMethod.ReturnsVoid;
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x0600770B RID: 30475 RVA: 0x001B7E98 File Offset: 0x001B6098
		internal sealed override CallingConvention CallingConvention
		{
			get
			{
				return this._interfaceMethod.CallingConvention;
			}
		}

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x0600770C RID: 30476 RVA: 0x001B7EA5 File Offset: 0x001B60A5
		public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._interfaceMethod.RefCustomModifiers;
			}
		}

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x0600770D RID: 30477 RVA: 0x001B7EB2 File Offset: 0x001B60B2
		internal sealed override bool GenerateDebugInfo
		{
			get
			{
				return this._generateDebugInfo;
			}
		}

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x0600770E RID: 30478 RVA: 0x001B7EBA File Offset: 0x001B60BA
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return this._typeParameters;
			}
		}

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x0600770F RID: 30479 RVA: 0x001B7EC2 File Offset: 0x001B60C2
		public sealed override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06007710 RID: 30480 RVA: 0x001B7ECA File Offset: 0x001B60CA
		public override RefKind RefKind
		{
			get
			{
				return this._interfaceMethod.RefKind;
			}
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06007711 RID: 30481 RVA: 0x001B7ED7 File Offset: 0x001B60D7
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				return this._interfaceMethod.ReturnTypeWithAnnotations;
			}
		}

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06007712 RID: 30482 RVA: 0x001B7EE4 File Offset: 0x001B60E4
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06007713 RID: 30483 RVA: 0x001B7EE7 File Offset: 0x001B60E7
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17002159 RID: 8537
		// (get) Token: 0x06007714 RID: 30484 RVA: 0x001B7EEE File Offset: 0x001B60EE
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x1700215A RID: 8538
		// (get) Token: 0x06007715 RID: 30485 RVA: 0x001B7EF6 File Offset: 0x001B60F6
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._implementingType;
			}
		}

		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06007716 RID: 30486 RVA: 0x001B7EFE File Offset: 0x001B60FE
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._implementingType;
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x06007717 RID: 30487 RVA: 0x001B7F06 File Offset: 0x001B6106
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06007718 RID: 30488 RVA: 0x001B7F09 File Offset: 0x001B6109
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return this._explicitInterfaceImplementations;
			}
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06007719 RID: 30489 RVA: 0x001B7F11 File Offset: 0x001B6111
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.ExplicitInterfaceImplementation;
			}
		}

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x0600771A RID: 30490 RVA: 0x001B7F14 File Offset: 0x001B6114
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x0600771B RID: 30491 RVA: 0x001B7F17 File Offset: 0x001B6117
		public override Symbol AssociatedSymbol
		{
			get
			{
				return this._associatedProperty;
			}
		}

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x0600771C RID: 30492 RVA: 0x001B7F1F File Offset: 0x001B611F
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x0600771D RID: 30493 RVA: 0x001B7F22 File Offset: 0x001B6122
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x0600771E RID: 30494 RVA: 0x001B7F29 File Offset: 0x001B6129
		public override bool IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x0600771F RID: 30495 RVA: 0x001B7F2C File Offset: 0x001B612C
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06007720 RID: 30496 RVA: 0x001B7F2F File Offset: 0x001B612F
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x06007721 RID: 30497 RVA: 0x001B7F32 File Offset: 0x001B6132
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06007722 RID: 30498 RVA: 0x001B7F35 File Offset: 0x001B6135
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06007723 RID: 30499 RVA: 0x001B7F38 File Offset: 0x001B6138
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06007724 RID: 30500 RVA: 0x001B7F3B File Offset: 0x001B613B
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06007725 RID: 30501 RVA: 0x001B7F3E File Offset: 0x001B613E
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06007726 RID: 30502 RVA: 0x001B7F41 File Offset: 0x001B6141
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06007727 RID: 30503 RVA: 0x001B7F49 File Offset: 0x001B6149
		internal sealed override bool HasSpecialName
		{
			get
			{
				return this._interfaceMethod.HasSpecialName;
			}
		}

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06007728 RID: 30504 RVA: 0x001B7F56 File Offset: 0x001B6156
		internal sealed override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06007729 RID: 30505 RVA: 0x001B7F59 File Offset: 0x001B6159
		internal sealed override bool RequiresSecurityObject
		{
			get
			{
				return this._interfaceMethod.RequiresSecurityObject;
			}
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x001B7F66 File Offset: 0x001B6166
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return true;
		}

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x0600772B RID: 30507 RVA: 0x001B7F69 File Offset: 0x001B6169
		internal override bool IsMetadataFinal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600772C RID: 30508 RVA: 0x001B7F6C File Offset: 0x001B616C
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return true;
		}

		// Token: 0x0600772D RID: 30509 RVA: 0x001B7F6F File Offset: 0x001B616F
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x0600772E RID: 30510 RVA: 0x001B7F72 File Offset: 0x001B6172
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x0600772F RID: 30511 RVA: 0x001B7F75 File Offset: 0x001B6175
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x001B7F78 File Offset: 0x001B6178
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007731 RID: 30513 RVA: 0x001B7F7F File Offset: 0x001B617F
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x04001CD2 RID: 7378
		private readonly MethodSymbol _interfaceMethod;

		// Token: 0x04001CD3 RID: 7379
		private readonly NamedTypeSymbol _implementingType;

		// Token: 0x04001CD4 RID: 7380
		private readonly bool _generateDebugInfo;

		// Token: 0x04001CD5 RID: 7381
		private readonly PropertySymbol _associatedProperty;

		// Token: 0x04001CD6 RID: 7382
		private readonly ImmutableArray<MethodSymbol> _explicitInterfaceImplementations;

		// Token: 0x04001CD7 RID: 7383
		private readonly ImmutableArray<TypeParameterSymbol> _typeParameters;

		// Token: 0x04001CD8 RID: 7384
		private readonly ImmutableArray<ParameterSymbol> _parameters;

		// Token: 0x04001CD9 RID: 7385
		private readonly string _name;
	}
}
