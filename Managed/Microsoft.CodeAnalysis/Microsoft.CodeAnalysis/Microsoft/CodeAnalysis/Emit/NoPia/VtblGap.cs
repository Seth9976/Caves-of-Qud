using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit.NoPia
{
	// Token: 0x02000499 RID: 1177
	internal sealed class VtblGap : IMethodDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IMethodReference, ISignature
	{
		// Token: 0x06002E53 RID: 11859 RVA: 0x000832CC File Offset: 0x000814CC
		public VtblGap(ITypeDefinition containingType, string name)
		{
			this.ContainingType = containingType;
			this._name = name;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000832E2 File Offset: 0x000814E2
		IMethodBody IMethodDefinition.GetBody(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000832E5 File Offset: 0x000814E5
		IEnumerable<IGenericMethodParameter> IMethodDefinition.GenericParameters
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<IGenericMethodParameter>();
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000832EC File Offset: 0x000814EC
		bool IMethodDefinition.HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000832EF File Offset: 0x000814EF
		bool IMethodDefinition.IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000832F2 File Offset: 0x000814F2
		bool IMethodDefinition.IsAccessCheckedOnOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000832F5 File Offset: 0x000814F5
		bool IMethodDefinition.IsConstructor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000832F8 File Offset: 0x000814F8
		bool IMethodDefinition.IsExternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000832FB File Offset: 0x000814FB
		bool IMethodDefinition.IsHiddenBySignature
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000832FE File Offset: 0x000814FE
		bool IMethodDefinition.IsNewSlot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x00083301 File Offset: 0x00081501
		bool IMethodDefinition.IsPlatformInvoke
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x00083304 File Offset: 0x00081504
		bool IMethodDefinition.IsRuntimeSpecial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x00083307 File Offset: 0x00081507
		bool IMethodDefinition.IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x0008330A File Offset: 0x0008150A
		bool IMethodDefinition.IsSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x0008330D File Offset: 0x0008150D
		bool IMethodDefinition.IsStatic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x00083310 File Offset: 0x00081510
		bool IMethodDefinition.IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x00083313 File Offset: 0x00081513
		MethodImplAttributes IMethodDefinition.GetImplementationAttributes(EmitContext context)
		{
			return 3;
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x00083316 File Offset: 0x00081516
		ImmutableArray<IParameterDefinition> IMethodDefinition.Parameters
		{
			get
			{
				return ImmutableArray<IParameterDefinition>.Empty;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x0008331D File Offset: 0x0008151D
		IPlatformInvokeInformation IMethodDefinition.PlatformInvokeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x00083320 File Offset: 0x00081520
		bool IMethodDefinition.RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x00083323 File Offset: 0x00081523
		IEnumerable<ICustomAttribute> IMethodDefinition.GetReturnValueAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x0008332A File Offset: 0x0008152A
		bool IMethodDefinition.ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x0008332D File Offset: 0x0008152D
		IMarshallingInformation IMethodDefinition.ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x00083330 File Offset: 0x00081530
		ImmutableArray<byte> IMethodDefinition.ReturnValueMarshallingDescriptor
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x00083346 File Offset: 0x00081546
		IEnumerable<SecurityAttribute> IMethodDefinition.SecurityAttributes
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x0008334D File Offset: 0x0008154D
		ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
		{
			get
			{
				return this.ContainingType;
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x00083355 File Offset: 0x00081555
		INamespace IMethodDefinition.ContainingNamespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x00083358 File Offset: 0x00081558
		TypeMemberVisibility ITypeDefinitionMember.Visibility
		{
			get
			{
				return TypeMemberVisibility.Public;
			}
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x0008335B File Offset: 0x0008155B
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return this.ContainingType;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x00083363 File Offset: 0x00081563
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x0008336A File Offset: 0x0008156A
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x00083373 File Offset: 0x00081573
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x00083376 File Offset: 0x00081576
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x00083379 File Offset: 0x00081579
		string INamedEntity.Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x00083381 File Offset: 0x00081581
		bool IMethodReference.AcceptsExtraArguments
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x00083384 File Offset: 0x00081584
		ushort IMethodReference.GenericParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x00083387 File Offset: 0x00081587
		bool IMethodReference.IsGeneric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0008338A File Offset: 0x0008158A
		IMethodDefinition IMethodReference.GetResolvedMethod(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x0008338D File Offset: 0x0008158D
		ImmutableArray<IParameterTypeInformation> IMethodReference.ExtraParameters
		{
			get
			{
				return ImmutableArray<IParameterTypeInformation>.Empty;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00083394 File Offset: 0x00081594
		IGenericMethodInstanceReference IMethodReference.AsGenericMethodInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x00083397 File Offset: 0x00081597
		ISpecializedMethodReference IMethodReference.AsSpecializedMethodReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x0008339A File Offset: 0x0008159A
		CallingConvention ISignature.CallingConvention
		{
			get
			{
				return CallingConvention.HasThis;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x0008339E File Offset: 0x0008159E
		ushort ISignature.ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000833A1 File Offset: 0x000815A1
		ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
		{
			return ImmutableArray<IParameterTypeInformation>.Empty;
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000833A8 File Offset: 0x000815A8
		ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000833AF File Offset: 0x000815AF
		ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000833B6 File Offset: 0x000815B6
		bool ISignature.ReturnValueIsByRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000833B9 File Offset: 0x000815B9
		ITypeReference ISignature.GetType(EmitContext context)
		{
			return context.Module.GetPlatformType(PlatformType.SystemVoid, context);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000833C8 File Offset: 0x000815C8
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000833CF File Offset: 0x000815CF
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001271 RID: 4721
		public readonly ITypeDefinition ContainingType;

		// Token: 0x04001272 RID: 4722
		private readonly string _name;
	}
}
