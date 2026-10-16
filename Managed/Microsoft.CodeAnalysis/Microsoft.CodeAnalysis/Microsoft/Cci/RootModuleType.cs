using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000C2 RID: 194
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootModuleType : INamespaceTypeDefinition, INamedTypeDefinition, ITypeDefinition, IDefinition, IReference, ITypeReference, INamedTypeReference, INamedEntity, INamespaceTypeReference
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x000178A9 File Offset: 0x00015AA9
		[NullableContext(0)]
		public void SetStaticConstructorBody(ImmutableArray<byte> il)
		{
			this._methods = SpecializedCollections.SingletonReadOnlyList<RootModuleStaticConstructor>(new RootModuleStaticConstructor(this, il));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000178C0 File Offset: 0x00015AC0
		public IEnumerable<IMethodDefinition> GetMethods(EmitContext context)
		{
			IReadOnlyList<IMethodDefinition> readOnlyList;
			if ((readOnlyList = this._methods) == null)
			{
				readOnlyList = (this._methods = SpecializedCollections.EmptyReadOnlyList<IMethodDefinition>());
			}
			return readOnlyList;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x000178E8 File Offset: 0x00015AE8
		public TypeDefinitionHandle TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000178FE File Offset: 0x00015AFE
		public ITypeDefinition ResolvedType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017901 File Offset: 0x00015B01
		public IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00017908 File Offset: 0x00015B08
		public bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001790B File Offset: 0x00015B0B
		public string Name
		{
			get
			{
				return "<Module>";
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00017912 File Offset: 0x00015B12
		public ushort Alignment
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017915 File Offset: 0x00015B15
		[NullableContext(2)]
		public ITypeReference GetBaseClass(EmitContext context)
		{
			return null;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00017918 File Offset: 0x00015B18
		public IEnumerable<IEventDefinition> GetEvents(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IEventDefinition>();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001791F File Offset: 0x00015B1F
		public IEnumerable<MethodImplementation> GetExplicitImplementationOverrides(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<MethodImplementation>();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00017926 File Offset: 0x00015B26
		public IEnumerable<IFieldDefinition> GetFields(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IFieldDefinition>();
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001792D File Offset: 0x00015B2D
		public bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00017930 File Offset: 0x00015B30
		public IEnumerable<TypeReferenceWithAttributes> Interfaces(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<TypeReferenceWithAttributes>();
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00017937 File Offset: 0x00015B37
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001793A File Offset: 0x00015B3A
		public bool IsBeforeFieldInit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0001793D File Offset: 0x00015B3D
		public bool IsComObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00017940 File Offset: 0x00015B40
		public bool IsGeneric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00017943 File Offset: 0x00015B43
		public bool IsInterface
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00017946 File Offset: 0x00015B46
		public bool IsDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00017949 File Offset: 0x00015B49
		public bool IsRuntimeSpecial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001794C File Offset: 0x00015B4C
		public bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001794F File Offset: 0x00015B4F
		public bool IsSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00017952 File Offset: 0x00015B52
		public bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00017955 File Offset: 0x00015B55
		public bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00017958 File Offset: 0x00015B58
		public LayoutKind Layout
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001795B File Offset: 0x00015B5B
		public IEnumerable<INestedTypeDefinition> GetNestedTypes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<INestedTypeDefinition>();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00017962 File Offset: 0x00015B62
		public IEnumerable<IPropertyDefinition> GetProperties(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IPropertyDefinition>();
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00017969 File Offset: 0x00015B69
		public uint SizeOf
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001796C File Offset: 0x00015B6C
		public CharSet StringFormat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001796F File Offset: 0x00015B6F
		public bool IsPublic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00017972 File Offset: 0x00015B72
		public bool IsNested
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00017975 File Offset: 0x00015B75
		IEnumerable<IGenericTypeParameter> ITypeDefinition.GenericParameters
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001797C File Offset: 0x00015B7C
		ushort ITypeDefinition.GenericParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001797F File Offset: 0x00015B7F
		IEnumerable<SecurityAttribute> ITypeDefinition.SecurityAttributes
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00017986 File Offset: 0x00015B86
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001798D File Offset: 0x00015B8D
		bool ITypeReference.IsEnum
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00017994 File Offset: 0x00015B94
		bool ITypeReference.IsValueType
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001799B File Offset: 0x00015B9B
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001799E File Offset: 0x00015B9E
		PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x000179A5 File Offset: 0x00015BA5
		ushort INamedTypeReference.GenericParameterCount
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000179AC File Offset: 0x00015BAC
		IUnitReference INamespaceTypeReference.GetUnit(EmitContext context)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x000179B3 File Offset: 0x00015BB3
		string INamespaceTypeReference.NamespaceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x000179BA File Offset: 0x00015BBA
		[Nullable(2)]
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x000179BD File Offset: 0x00015BBD
		[Nullable(2)]
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x000179C0 File Offset: 0x00015BC0
		[Nullable(2)]
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000179C3 File Offset: 0x00015BC3
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x000179C6 File Offset: 0x00015BC6
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000179C9 File Offset: 0x00015BC9
		[NullableContext(2)]
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x000179CC File Offset: 0x00015BCC
		[Nullable(2)]
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x000179CF File Offset: 0x00015BCF
		[Nullable(2)]
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000179D2 File Offset: 0x00015BD2
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000179D5 File Offset: 0x00015BD5
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000179D8 File Offset: 0x00015BD8
		[NullableContext(2)]
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000179DB File Offset: 0x00015BDB
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000179E2 File Offset: 0x00015BE2
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040001FA RID: 506
		[Nullable(new byte[] { 2, 1 })]
		private IReadOnlyList<IMethodDefinition> _methods;
	}
}
