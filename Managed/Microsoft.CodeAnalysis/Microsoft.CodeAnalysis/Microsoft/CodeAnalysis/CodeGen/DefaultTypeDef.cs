using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004DD RID: 1245
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class DefaultTypeDef : ITypeDefinition, IDefinition, IReference, ITypeReference
	{
		// Token: 0x06003214 RID: 12820 RVA: 0x0008E8F1 File Offset: 0x0008CAF1
		public IEnumerable<IEventDefinition> GetEvents(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IEventDefinition>();
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x0008E8F8 File Offset: 0x0008CAF8
		public IEnumerable<Microsoft.Cci.MethodImplementation> GetExplicitImplementationOverrides(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<Microsoft.Cci.MethodImplementation>();
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x0008E8FF File Offset: 0x0008CAFF
		public virtual IEnumerable<IFieldDefinition> GetFields(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IFieldDefinition>();
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003217 RID: 12823 RVA: 0x0008E906 File Offset: 0x0008CB06
		public IEnumerable<IGenericTypeParameter> GenericParameters
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<IGenericTypeParameter>();
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x0008E90D File Offset: 0x0008CB0D
		public ushort GenericParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x0008E910 File Offset: 0x0008CB10
		public bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x0008E913 File Offset: 0x0008CB13
		public IEnumerable<TypeReferenceWithAttributes> Interfaces(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<TypeReferenceWithAttributes>();
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x0008E91A File Offset: 0x0008CB1A
		public bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x0008E91D File Offset: 0x0008CB1D
		public bool IsBeforeFieldInit
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x0008E920 File Offset: 0x0008CB20
		public bool IsComObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x0008E923 File Offset: 0x0008CB23
		public bool IsGeneric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x0008E926 File Offset: 0x0008CB26
		public bool IsInterface
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x0008E929 File Offset: 0x0008CB29
		public bool IsDelegate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x0008E92C File Offset: 0x0008CB2C
		public bool IsRuntimeSpecial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x0008E92F File Offset: 0x0008CB2F
		public bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x0008E932 File Offset: 0x0008CB32
		public bool IsSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x0008E935 File Offset: 0x0008CB35
		public bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x0008E938 File Offset: 0x0008CB38
		public bool IsSealed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x0008E93B File Offset: 0x0008CB3B
		public virtual IEnumerable<IMethodDefinition> GetMethods(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IMethodDefinition>();
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x0008E942 File Offset: 0x0008CB42
		public virtual IEnumerable<INestedTypeDefinition> GetNestedTypes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<INestedTypeDefinition>();
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x0008E949 File Offset: 0x0008CB49
		public IEnumerable<IPropertyDefinition> GetProperties(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<IPropertyDefinition>();
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x0008E950 File Offset: 0x0008CB50
		public IEnumerable<SecurityAttribute> SecurityAttributes
		{
			get
			{
				return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x0008E957 File Offset: 0x0008CB57
		public CharSet StringFormat
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x0008E95A File Offset: 0x0008CB5A
		public virtual IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x0008E961 File Offset: 0x0008CB61
		public IDefinition AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0008E964 File Offset: 0x0008CB64
		[NullableContext(2)]
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x0008E967 File Offset: 0x0008CB67
		public bool IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x0008E96A File Offset: 0x0008CB6A
		public ITypeDefinition GetResolvedType(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x0008E96D File Offset: 0x0008CB6D
		public Microsoft.Cci.PrimitiveTypeCode TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x0008E971 File Offset: 0x0008CB71
		public TypeDefinitionHandle TypeDef
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x0008E978 File Offset: 0x0008CB78
		[Nullable(2)]
		public IGenericMethodParameterReference AsGenericMethodParameterReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x0008E97B File Offset: 0x0008CB7B
		[Nullable(2)]
		public IGenericTypeInstanceReference AsGenericTypeInstanceReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x0008E97E File Offset: 0x0008CB7E
		[Nullable(2)]
		public IGenericTypeParameterReference AsGenericTypeParameterReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x0008E981 File Offset: 0x0008CB81
		[NullableContext(2)]
		public virtual INamespaceTypeDefinition AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x0008E984 File Offset: 0x0008CB84
		[Nullable(2)]
		public virtual INamespaceTypeReference AsNamespaceTypeReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x0008E987 File Offset: 0x0008CB87
		[Nullable(2)]
		public ISpecializedNestedTypeReference AsSpecializedNestedTypeReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x0008E98A File Offset: 0x0008CB8A
		[NullableContext(2)]
		public virtual INestedTypeDefinition AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003239 RID: 12857 RVA: 0x0008E98D File Offset: 0x0008CB8D
		[Nullable(2)]
		public virtual INestedTypeReference AsNestedTypeReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x0008E990 File Offset: 0x0008CB90
		public ITypeDefinition AsTypeDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x0008E993 File Offset: 0x0008CB93
		public bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600323C RID: 12860 RVA: 0x0008E996 File Offset: 0x0008CB96
		public virtual ushort Alignment
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0008E999 File Offset: 0x0008CB99
		public virtual ITypeReference GetBaseClass(EmitContext context)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x0008E9A0 File Offset: 0x0008CBA0
		public virtual LayoutKind Layout
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x0008E9A3 File Offset: 0x0008CBA3
		public virtual uint SizeOf
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0008E9A6 File Offset: 0x0008CBA6
		public virtual void Dispatch(MetadataVisitor visitor)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x0008E9AD File Offset: 0x0008CBAD
		public virtual bool IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x0008E9B0 File Offset: 0x0008CBB0
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x0008E9B7 File Offset: 0x0008CBB7
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}
	}
}
