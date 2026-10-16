using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D9 RID: 1241
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class SynthesizedStaticField : IFieldDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IFieldReference
	{
		// Token: 0x060031F0 RID: 12784 RVA: 0x0008E785 File Offset: 0x0008C985
		internal SynthesizedStaticField(string name, INamedTypeDefinition containingType, ITypeReference type)
		{
			this._containingType = containingType;
			this._type = type;
			this._name = name;
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x0008E7A2 File Offset: 0x0008C9A2
		public override string ToString()
		{
			return string.Format("{0} {1}.{2}", this._type.GetInternalSymbol() ?? this._type, this._containingType.GetInternalSymbol() ?? this._containingType, this.Name);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x0008E7DE File Offset: 0x0008C9DE
		[NullableContext(2)]
		public MetadataConstant GetCompileTimeValue(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060031F3 RID: 12787
		[Nullable(0)]
		public abstract ImmutableArray<byte> MappedData
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x0008E7E1 File Offset: 0x0008C9E1
		public bool IsCompileTimeConstant
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060031F5 RID: 12789 RVA: 0x0008E7E4 File Offset: 0x0008C9E4
		public bool IsNotSerialized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x0008E7E7 File Offset: 0x0008C9E7
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060031F7 RID: 12791 RVA: 0x0008E7EA File Offset: 0x0008C9EA
		public bool IsRuntimeSpecial
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x0008E7ED File Offset: 0x0008C9ED
		public bool IsSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060031F9 RID: 12793 RVA: 0x0008E7F0 File Offset: 0x0008C9F0
		public bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x0008E7F3 File Offset: 0x0008C9F3
		public bool IsMarshalledExplicitly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060031FB RID: 12795 RVA: 0x0008E7F6 File Offset: 0x0008C9F6
		[Nullable(2)]
		public IMarshallingInformation MarshallingInformation
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060031FC RID: 12796 RVA: 0x0008E7FC File Offset: 0x0008C9FC
		[Nullable(0)]
		public ImmutableArray<byte> MarshallingDescriptor
		{
			[NullableContext(0)]
			get
			{
				return default(ImmutableArray<byte>);
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x0008E812 File Offset: 0x0008CA12
		public int Offset
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x0008E819 File Offset: 0x0008CA19
		public ITypeDefinition ContainingTypeDefinition
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060031FF RID: 12799 RVA: 0x0008E821 File Offset: 0x0008CA21
		public TypeMemberVisibility Visibility
		{
			get
			{
				return TypeMemberVisibility.Assembly;
			}
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x0008E824 File Offset: 0x0008CA24
		public ITypeReference GetContainingType(EmitContext context)
		{
			return this._containingType;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x0008E82C File Offset: 0x0008CA2C
		public IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x0008E833 File Offset: 0x0008CA33
		public void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x0008E83C File Offset: 0x0008CA3C
		public IDefinition AsDefinition(EmitContext context)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x0008E843 File Offset: 0x0008CA43
		[NullableContext(2)]
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x0008E846 File Offset: 0x0008CA46
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x0008E84E File Offset: 0x0008CA4E
		public bool IsContextualNamedEntity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x0008E851 File Offset: 0x0008CA51
		public ITypeReference GetType(EmitContext context)
		{
			return this._type;
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x0008E859 File Offset: 0x0008CA59
		internal ITypeReference Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x0008E861 File Offset: 0x0008CA61
		public IFieldDefinition GetResolvedField(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x0008E864 File Offset: 0x0008CA64
		[Nullable(2)]
		public ISpecializedFieldReference AsSpecializedFieldReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x0008E867 File Offset: 0x0008CA67
		public MetadataConstant Constant
		{
			get
			{
				throw ExceptionUtilities.Unreachable;
			}
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x0008E86E File Offset: 0x0008CA6E
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x0008E875 File Offset: 0x0008CA75
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001357 RID: 4951
		private readonly INamedTypeDefinition _containingType;

		// Token: 0x04001358 RID: 4952
		private readonly ITypeReference _type;

		// Token: 0x04001359 RID: 4953
		private readonly string _name;
	}
}
