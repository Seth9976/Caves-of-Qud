using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005B2 RID: 1458
	internal abstract class WrappedMethodSymbol : MethodSymbol
	{
		// Token: 0x06007A60 RID: 31328 RVA: 0x001BF5B8 File Offset: 0x001BD7B8
		public WrappedMethodSymbol()
		{
		}

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x06007A61 RID: 31329
		public abstract MethodSymbol UnderlyingMethod { get; }

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x06007A62 RID: 31330 RVA: 0x001BF5C0 File Offset: 0x001BD7C0
		public override bool IsVararg
		{
			get
			{
				return this.UnderlyingMethod.IsVararg;
			}
		}

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x06007A63 RID: 31331 RVA: 0x001BF5CD File Offset: 0x001BD7CD
		public override bool IsGenericMethod
		{
			get
			{
				return this.UnderlyingMethod.IsGenericMethod;
			}
		}

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x06007A64 RID: 31332 RVA: 0x001BF5DA File Offset: 0x001BD7DA
		public override int Arity
		{
			get
			{
				return this.UnderlyingMethod.Arity;
			}
		}

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x06007A65 RID: 31333 RVA: 0x001BF5E7 File Offset: 0x001BD7E7
		public override RefKind RefKind
		{
			get
			{
				return this.UnderlyingMethod.RefKind;
			}
		}

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x06007A66 RID: 31334 RVA: 0x001BF5F4 File Offset: 0x001BD7F4
		internal override int ParameterCount
		{
			get
			{
				return this.UnderlyingMethod.ParameterCount;
			}
		}

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x06007A67 RID: 31335 RVA: 0x001BF601 File Offset: 0x001BD801
		public override bool IsExtensionMethod
		{
			get
			{
				return this.UnderlyingMethod.IsExtensionMethod;
			}
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x06007A68 RID: 31336 RVA: 0x001BF60E File Offset: 0x001BD80E
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return this.UnderlyingMethod.HidesBaseMethodsByName;
			}
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x06007A69 RID: 31337 RVA: 0x001BF61B File Offset: 0x001BD81B
		public override bool AreLocalsZeroed
		{
			get
			{
				return this.UnderlyingMethod.AreLocalsZeroed;
			}
		}

		// Token: 0x170022FC RID: 8956
		// (get) Token: 0x06007A6A RID: 31338 RVA: 0x001BF628 File Offset: 0x001BD828
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.UnderlyingMethod.Locations;
			}
		}

		// Token: 0x170022FD RID: 8957
		// (get) Token: 0x06007A6B RID: 31339 RVA: 0x001BF635 File Offset: 0x001BD835
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return this.UnderlyingMethod.DeclaringSyntaxReferences;
			}
		}

		// Token: 0x170022FE RID: 8958
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x001BF642 File Offset: 0x001BD842
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return this.UnderlyingMethod.DeclaredAccessibility;
			}
		}

		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x06007A6D RID: 31341 RVA: 0x001BF64F File Offset: 0x001BD84F
		public override bool IsStatic
		{
			get
			{
				return this.UnderlyingMethod.IsStatic;
			}
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x001BF65C File Offset: 0x001BD85C
		public override bool RequiresInstanceReceiver
		{
			get
			{
				return this.UnderlyingMethod.RequiresInstanceReceiver;
			}
		}

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x06007A6F RID: 31343 RVA: 0x001BF669 File Offset: 0x001BD869
		public override bool IsVirtual
		{
			get
			{
				return this.UnderlyingMethod.IsVirtual;
			}
		}

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x06007A70 RID: 31344 RVA: 0x001BF676 File Offset: 0x001BD876
		public override bool IsAsync
		{
			get
			{
				return this.UnderlyingMethod.IsAsync;
			}
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x06007A71 RID: 31345 RVA: 0x001BF683 File Offset: 0x001BD883
		public override bool IsOverride
		{
			get
			{
				return this.UnderlyingMethod.IsOverride;
			}
		}

		// Token: 0x17002304 RID: 8964
		// (get) Token: 0x06007A72 RID: 31346 RVA: 0x001BF690 File Offset: 0x001BD890
		public override bool IsAbstract
		{
			get
			{
				return this.UnderlyingMethod.IsAbstract;
			}
		}

		// Token: 0x17002305 RID: 8965
		// (get) Token: 0x06007A73 RID: 31347 RVA: 0x001BF69D File Offset: 0x001BD89D
		public override bool IsSealed
		{
			get
			{
				return this.UnderlyingMethod.IsSealed;
			}
		}

		// Token: 0x17002306 RID: 8966
		// (get) Token: 0x06007A74 RID: 31348 RVA: 0x001BF6AA File Offset: 0x001BD8AA
		public override bool IsExtern
		{
			get
			{
				return this.UnderlyingMethod.IsExtern;
			}
		}

		// Token: 0x17002307 RID: 8967
		// (get) Token: 0x06007A75 RID: 31349 RVA: 0x001BF6B7 File Offset: 0x001BD8B7
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return this.UnderlyingMethod.IsImplicitlyDeclared;
			}
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x001BF6C4 File Offset: 0x001BD8C4
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return this.UnderlyingMethod.IsMetadataVirtual(ignoreInterfaceImplementationChanges);
		}

		// Token: 0x17002308 RID: 8968
		// (get) Token: 0x06007A77 RID: 31351 RVA: 0x001BF6D2 File Offset: 0x001BD8D2
		internal override bool IsMetadataFinal
		{
			get
			{
				return this.UnderlyingMethod.IsMetadataFinal;
			}
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x001BF6DF File Offset: 0x001BD8DF
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return this.UnderlyingMethod.IsMetadataNewSlot(ignoreInterfaceImplementationChanges);
		}

		// Token: 0x17002309 RID: 8969
		// (get) Token: 0x06007A79 RID: 31353 RVA: 0x001BF6ED File Offset: 0x001BD8ED
		internal override bool RequiresSecurityObject
		{
			get
			{
				return this.UnderlyingMethod.RequiresSecurityObject;
			}
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x001BF6FA File Offset: 0x001BD8FA
		public override DllImportData GetDllImportData()
		{
			return this.UnderlyingMethod.GetDllImportData();
		}

		// Token: 0x1700230A RID: 8970
		// (get) Token: 0x06007A7B RID: 31355 RVA: 0x001BF707 File Offset: 0x001BD907
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return this.UnderlyingMethod.ReturnValueMarshallingInformation;
			}
		}

		// Token: 0x1700230B RID: 8971
		// (get) Token: 0x06007A7C RID: 31356 RVA: 0x001BF714 File Offset: 0x001BD914
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return this.UnderlyingMethod.HasDeclarativeSecurity;
			}
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x001BF721 File Offset: 0x001BD921
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			return this.UnderlyingMethod.GetSecurityInformation();
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x001BF72E File Offset: 0x001BD92E
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return this.UnderlyingMethod.GetAppliedConditionalSymbols();
		}

		// Token: 0x1700230C RID: 8972
		// (get) Token: 0x06007A7F RID: 31359 RVA: 0x001BF73B File Offset: 0x001BD93B
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return this.UnderlyingMethod.ObsoleteAttributeData;
			}
		}

		// Token: 0x1700230D RID: 8973
		// (get) Token: 0x06007A80 RID: 31360 RVA: 0x001BF748 File Offset: 0x001BD948
		public override string Name
		{
			get
			{
				return this.UnderlyingMethod.Name;
			}
		}

		// Token: 0x1700230E RID: 8974
		// (get) Token: 0x06007A81 RID: 31361 RVA: 0x001BF755 File Offset: 0x001BD955
		internal override bool HasSpecialName
		{
			get
			{
				return this.UnderlyingMethod.HasSpecialName;
			}
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x001BF762 File Offset: 0x001BD962
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.UnderlyingMethod.GetDocumentationCommentXml(preferredCulture, expandIncludes, cancellationToken);
		}

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x06007A83 RID: 31363 RVA: 0x001BF772 File Offset: 0x001BD972
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return this.UnderlyingMethod.ImplementationAttributes;
			}
		}

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x06007A84 RID: 31364 RVA: 0x001BF77F File Offset: 0x001BD97F
		public override MethodKind MethodKind
		{
			get
			{
				return this.UnderlyingMethod.MethodKind;
			}
		}

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x06007A85 RID: 31365 RVA: 0x001BF78C File Offset: 0x001BD98C
		internal override CallingConvention CallingConvention
		{
			get
			{
				return this.UnderlyingMethod.CallingConvention;
			}
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x06007A86 RID: 31366 RVA: 0x001BF799 File Offset: 0x001BD999
		internal override bool IsAccessCheckedOnOverride
		{
			get
			{
				return this.UnderlyingMethod.IsAccessCheckedOnOverride;
			}
		}

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x06007A87 RID: 31367 RVA: 0x001BF7A6 File Offset: 0x001BD9A6
		internal override bool IsExternal
		{
			get
			{
				return this.UnderlyingMethod.IsExternal;
			}
		}

		// Token: 0x17002314 RID: 8980
		// (get) Token: 0x06007A88 RID: 31368 RVA: 0x001BF7B3 File Offset: 0x001BD9B3
		internal override bool HasRuntimeSpecialName
		{
			get
			{
				return this.UnderlyingMethod.HasRuntimeSpecialName;
			}
		}

		// Token: 0x17002315 RID: 8981
		// (get) Token: 0x06007A89 RID: 31369 RVA: 0x001BF7C0 File Offset: 0x001BD9C0
		public sealed override bool ReturnsVoid
		{
			get
			{
				return this.UnderlyingMethod.ReturnsVoid;
			}
		}

		// Token: 0x17002316 RID: 8982
		// (get) Token: 0x06007A8A RID: 31370 RVA: 0x001BF7CD File Offset: 0x001BD9CD
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return this.UnderlyingMethod.ReturnTypeFlowAnalysisAnnotations;
			}
		}

		// Token: 0x17002317 RID: 8983
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x001BF7DA File Offset: 0x001BD9DA
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return this.UnderlyingMethod.ReturnNotNullIfParameterNotNull;
			}
		}

		// Token: 0x17002318 RID: 8984
		// (get) Token: 0x06007A8C RID: 31372 RVA: 0x001BF7E7 File Offset: 0x001BD9E7
		public sealed override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return this.UnderlyingMethod.FlowAnalysisAnnotations;
			}
		}

		// Token: 0x17002319 RID: 8985
		// (get) Token: 0x06007A8D RID: 31373 RVA: 0x001BF7F4 File Offset: 0x001BD9F4
		internal sealed override ImmutableArray<string> NotNullMembers
		{
			get
			{
				return this.UnderlyingMethod.NotNullMembers;
			}
		}

		// Token: 0x1700231A RID: 8986
		// (get) Token: 0x06007A8E RID: 31374 RVA: 0x001BF801 File Offset: 0x001BDA01
		internal sealed override ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				return this.UnderlyingMethod.NotNullWhenTrueMembers;
			}
		}

		// Token: 0x1700231B RID: 8987
		// (get) Token: 0x06007A8F RID: 31375 RVA: 0x001BF80E File Offset: 0x001BDA0E
		internal sealed override ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				return this.UnderlyingMethod.NotNullWhenFalseMembers;
			}
		}

		// Token: 0x1700231C RID: 8988
		// (get) Token: 0x06007A90 RID: 31376 RVA: 0x001BF81B File Offset: 0x001BDA1B
		internal override bool ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return this.UnderlyingMethod.ReturnValueIsMarshalledExplicitly;
			}
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x06007A91 RID: 31377 RVA: 0x001BF828 File Offset: 0x001BDA28
		internal override ImmutableArray<byte> ReturnValueMarshallingDescriptor
		{
			get
			{
				return this.UnderlyingMethod.ReturnValueMarshallingDescriptor;
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x06007A92 RID: 31378 RVA: 0x001BF835 File Offset: 0x001BDA35
		internal override bool GenerateDebugInfo
		{
			get
			{
				return this.UnderlyingMethod.GenerateDebugInfo;
			}
		}

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x06007A93 RID: 31379 RVA: 0x001BF842 File Offset: 0x001BDA42
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return this.UnderlyingMethod.IsDeclaredReadOnly;
			}
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x06007A94 RID: 31380 RVA: 0x001BF84F File Offset: 0x001BDA4F
		internal override bool IsInitOnly
		{
			get
			{
				return this.UnderlyingMethod.IsInitOnly;
			}
		}
	}
}
