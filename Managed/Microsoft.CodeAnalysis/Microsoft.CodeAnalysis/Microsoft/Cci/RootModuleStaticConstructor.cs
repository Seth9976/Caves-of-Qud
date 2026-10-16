using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Debugging;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000C1 RID: 193
	internal sealed class RootModuleStaticConstructor : IMethodDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IMethodReference, ISignature, IMethodBody
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x00017726 File Offset: 0x00015926
		public RootModuleStaticConstructor(ITypeDefinition containingTypeDefinition, ImmutableArray<byte> il)
		{
			this.ContainingTypeDefinition = containingTypeDefinition;
			this.IL = il;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001773C File Offset: 0x0001593C
		public ITypeDefinition ContainingTypeDefinition { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00017744 File Offset: 0x00015944
		public string Name
		{
			get
			{
				return ".cctor";
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001774B File Offset: 0x0001594B
		public IMethodBody GetBody(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001774E File Offset: 0x0001594E
		public IEnumerable<IGenericMethodParameter> GenericParameters
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<IGenericMethodParameter>();
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00017755 File Offset: 0x00015955
		public bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00017758 File Offset: 0x00015958
		public bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001775B File Offset: 0x0001595B
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001775E File Offset: 0x0001595E
		public bool IsAccessCheckedOnOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00017761 File Offset: 0x00015961
		public bool IsConstructor
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00017764 File Offset: 0x00015964
		public bool IsExternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00017767 File Offset: 0x00015967
		public bool IsHiddenBySignature
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001776A File Offset: 0x0001596A
		public bool IsNewSlot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001776D File Offset: 0x0001596D
		public bool IsPlatformInvoke
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00017770 File Offset: 0x00015970
		public bool IsRuntimeSpecial
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x00017773 File Offset: 0x00015973
		public bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00017776 File Offset: 0x00015976
		public bool IsSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x00017779 File Offset: 0x00015979
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001777C File Offset: 0x0001597C
		public bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001777F File Offset: 0x0001597F
		public ImmutableArray<IParameterDefinition> Parameters
		{
			get
			{
				return ImmutableArray<IParameterDefinition>.Empty;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00017786 File Offset: 0x00015986
		public IPlatformInvokeInformation PlatformInvokeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00017789 File Offset: 0x00015989
		public bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001778C File Offset: 0x0001598C
		public bool ReturnValueIsMarshalledExplicitly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001778F File Offset: 0x0001598F
		public IMarshallingInformation ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00017794 File Offset: 0x00015994
		public ImmutableArray<byte> ReturnValueMarshallingDescriptor
		{
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x000177AA File Offset: 0x000159AA
		public IEnumerable<SecurityAttribute> SecurityAttributes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000177AD File Offset: 0x000159AD
		public INamespace ContainingNamespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000177B0 File Offset: 0x000159B0
		public TypeMemberVisibility Visibility
		{
			get
			{
				return TypeMemberVisibility.Private;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000177B3 File Offset: 0x000159B3
		public bool AcceptsExtraArguments
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000177B6 File Offset: 0x000159B6
		public ushort GenericParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000177B9 File Offset: 0x000159B9
		public bool IsGeneric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000177BC File Offset: 0x000159BC
		public ImmutableArray<IParameterTypeInformation> ExtraParameters
		{
			get
			{
				return ImmutableArray<IParameterTypeInformation>.Empty;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000177C3 File Offset: 0x000159C3
		public IGenericMethodInstanceReference AsGenericMethodInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000177C6 File Offset: 0x000159C6
		public ISpecializedMethodReference AsSpecializedMethodReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x000177C9 File Offset: 0x000159C9
		public CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x000177CC File Offset: 0x000159CC
		public ushort ParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000177CF File Offset: 0x000159CF
		public ImmutableArray<ICustomModifier> ReturnValueCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000177D6 File Offset: 0x000159D6
		public ImmutableArray<ICustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000177DD File Offset: 0x000159DD
		public bool ReturnValueIsByRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000177E0 File Offset: 0x000159E0
		public IDefinition AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000177E3 File Offset: 0x000159E3
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000177E6 File Offset: 0x000159E6
		public void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x000177EF File Offset: 0x000159EF
		public IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000177F6 File Offset: 0x000159F6
		public ITypeReference GetContainingType(EmitContext context)
		{
			return this.ContainingTypeDefinition;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000177FE File Offset: 0x000159FE
		public MethodImplAttributes GetImplementationAttributes(EmitContext context)
		{
			return 0;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00017801 File Offset: 0x00015A01
		public ImmutableArray<IParameterTypeInformation> GetParameters(EmitContext context)
		{
			return ImmutableArray<IParameterTypeInformation>.Empty;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00017808 File Offset: 0x00015A08
		public IMethodDefinition GetResolvedMethod(EmitContext context)
		{
			return this;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001780B File Offset: 0x00015A0B
		public IEnumerable<ICustomAttribute> GetReturnValueAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00017812 File Offset: 0x00015A12
		public ITypeReference GetType(EmitContext context)
		{
			return context.Module.GetPlatformType(PlatformType.SystemVoid, context);
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00017821 File Offset: 0x00015A21
		public ushort MaxStack
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00017824 File Offset: 0x00015A24
		public ImmutableArray<byte> IL { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001782C File Offset: 0x00015A2C
		public IMethodDefinition MethodDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001782F File Offset: 0x00015A2F
		public ImmutableArray<ExceptionHandlerRegion> ExceptionRegions
		{
			get
			{
				return ImmutableArray<ExceptionHandlerRegion>.Empty;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00017836 File Offset: 0x00015A36
		public bool AreLocalsZeroed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00017839 File Offset: 0x00015A39
		public bool HasStackalloc
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001783C File Offset: 0x00015A3C
		public ImmutableArray<ILocalDefinition> LocalVariables
		{
			get
			{
				return ImmutableArray<ILocalDefinition>.Empty;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00017843 File Offset: 0x00015A43
		public StateMachineMoveNextBodyDebugInfo MoveNextBodyInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00017846 File Offset: 0x00015A46
		public ImmutableArray<SequencePoint> SequencePoints
		{
			get
			{
				return ImmutableArray<SequencePoint>.Empty;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001784D File Offset: 0x00015A4D
		public bool HasDynamicLocalVariables
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00017850 File Offset: 0x00015A50
		public ImmutableArray<LocalScope> LocalScopes
		{
			get
			{
				return ImmutableArray<LocalScope>.Empty;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00017857 File Offset: 0x00015A57
		public IImportScope ImportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0001785C File Offset: 0x00015A5C
		public DebugId MethodId
		{
			get
			{
				return default(DebugId);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00017872 File Offset: 0x00015A72
		public ImmutableArray<StateMachineHoistedLocalScope> StateMachineHoistedLocalScopes
		{
			get
			{
				return ImmutableArray<StateMachineHoistedLocalScope>.Empty;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00017879 File Offset: 0x00015A79
		public string StateMachineTypeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0001787C File Offset: 0x00015A7C
		public ImmutableArray<EncHoistedLocalInfo> StateMachineHoistedLocalSlots
		{
			get
			{
				return ImmutableArray<EncHoistedLocalInfo>.Empty;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00017883 File Offset: 0x00015A83
		public ImmutableArray<ITypeReference> StateMachineAwaiterSlots
		{
			get
			{
				return ImmutableArray<ITypeReference>.Empty;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0001788A File Offset: 0x00015A8A
		public ImmutableArray<ClosureDebugInfo> ClosureDebugInfo
		{
			get
			{
				return ImmutableArray<Microsoft.CodeAnalysis.CodeGen.ClosureDebugInfo>.Empty;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00017891 File Offset: 0x00015A91
		public ImmutableArray<LambdaDebugInfo> LambdaDebugInfo
		{
			get
			{
				return ImmutableArray<Microsoft.CodeAnalysis.CodeGen.LambdaDebugInfo>.Empty;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x00017898 File Offset: 0x00015A98
		public DynamicAnalysisMethodBodyData DynamicAnalysisData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001789B File Offset: 0x00015A9B
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000178A2 File Offset: 0x00015AA2
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}
	}
}
