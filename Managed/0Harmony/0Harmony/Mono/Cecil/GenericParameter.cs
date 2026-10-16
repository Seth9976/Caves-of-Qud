using System;
using System.Threading;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200020B RID: 523
	public sealed class GenericParameter : TypeReference, ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00027C01 File Offset: 0x00025E01
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x00027C09 File Offset: 0x00025E09
		public GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00027C12 File Offset: 0x00025E12
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00027C1A File Offset: 0x00025E1A
		public GenericParameterType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00027C22 File Offset: 0x00025E22
		public IGenericParameterProvider Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00027C2C File Offset: 0x00025E2C
		public bool HasConstraints
		{
			get
			{
				if (this.constraints != null)
				{
					return this.constraints.Count > 0;
				}
				if (base.HasImage)
				{
					return this.Module.Read<GenericParameter, bool>(this, (GenericParameter generic_parameter, MetadataReader reader) => reader.HasGenericConstraints(generic_parameter));
				}
				return false;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00027C88 File Offset: 0x00025E88
		public Collection<GenericParameterConstraint> Constraints
		{
			get
			{
				if (this.constraints != null)
				{
					return this.constraints;
				}
				if (base.HasImage)
				{
					return this.Module.Read<GenericParameter, GenericParameterConstraintCollection>(ref this.constraints, this, (GenericParameter generic_parameter, MetadataReader reader) => reader.ReadGenericConstraints(generic_parameter));
				}
				Interlocked.CompareExchange<GenericParameterConstraintCollection>(ref this.constraints, new GenericParameterConstraintCollection(this), null);
				return this.constraints;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00027CF7 File Offset: 0x00025EF7
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.Module);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00027D1C File Offset: 0x00025F1C
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.Module);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00027D3A File Offset: 0x00025F3A
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x000189AE File Offset: 0x00016BAE
		public override IMetadataScope Scope
		{
			get
			{
				if (this.owner == null)
				{
					return null;
				}
				if (this.owner.GenericParameterType != GenericParameterType.Method)
				{
					return ((TypeReference)this.owner).Scope;
				}
				return ((MethodReference)this.owner).DeclaringType.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00027D7A File Offset: 0x00025F7A
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x000189AE File Offset: 0x00016BAE
		public override TypeReference DeclaringType
		{
			get
			{
				return this.owner as TypeReference;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00027D87 File Offset: 0x00025F87
		public MethodReference DeclaringMethod
		{
			get
			{
				return this.owner as MethodReference;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00027D94 File Offset: 0x00025F94
		public override ModuleDefinition Module
		{
			get
			{
				return this.module ?? this.owner.Module;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00027DAC File Offset: 0x00025FAC
		public override string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Name))
				{
					return base.Name;
				}
				return base.Name = ((this.type == GenericParameterType.Method) ? "!!" : "!") + this.position.ToString();
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002640D File Offset: 0x0002460D
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x000189AE File Offset: 0x00016BAE
		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00027DFB File Offset: 0x00025FFB
		public override string FullName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool ContainsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00027E03 File Offset: 0x00026003
		public override MetadataType MetadataType
		{
			get
			{
				return (MetadataType)this.etype;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00027E0B File Offset: 0x0002600B
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x00027E1A File Offset: 0x0002601A
		public bool IsNonVariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 0U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 0U, value);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00027E30 File Offset: 0x00026030
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00027E3F File Offset: 0x0002603F
		public bool IsCovariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 1U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 1U, value);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00027E55 File Offset: 0x00026055
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x00027E64 File Offset: 0x00026064
		public bool IsContravariant
		{
			get
			{
				return this.attributes.GetMaskedAttributes(3, 2U);
			}
			set
			{
				this.attributes = this.attributes.SetMaskedAttributes(3, 2U, value);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00027E7A File Offset: 0x0002607A
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x00027E88 File Offset: 0x00026088
		public bool HasReferenceTypeConstraint
		{
			get
			{
				return this.attributes.GetAttributes(4);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4, value);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00027E9D File Offset: 0x0002609D
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00027EAB File Offset: 0x000260AB
		public bool HasNotNullableValueTypeConstraint
		{
			get
			{
				return this.attributes.GetAttributes(8);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8, value);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00027EC0 File Offset: 0x000260C0
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x00027ECF File Offset: 0x000260CF
		public bool HasDefaultConstructorConstraint
		{
			get
			{
				return this.attributes.GetAttributes(16);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(16, value);
			}
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00027EE5 File Offset: 0x000260E5
		public GenericParameter(IGenericParameterProvider owner)
			: this(string.Empty, owner)
		{
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00027EF4 File Offset: 0x000260F4
		public GenericParameter(string name, IGenericParameterProvider owner)
			: base(string.Empty, name)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			this.position = -1;
			this.owner = owner;
			this.type = owner.GenericParameterType;
			this.etype = GenericParameter.ConvertGenericParameterType(this.type);
			this.token = new MetadataToken(TokenType.GenericParam);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00027F54 File Offset: 0x00026154
		internal GenericParameter(int position, GenericParameterType type, ModuleDefinition module)
			: base(string.Empty, string.Empty)
		{
			Mixin.CheckModule(module);
			this.position = position;
			this.type = type;
			this.etype = GenericParameter.ConvertGenericParameterType(type);
			this.module = module;
			this.token = new MetadataToken(TokenType.GenericParam);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00027FA8 File Offset: 0x000261A8
		private static ElementType ConvertGenericParameterType(GenericParameterType type)
		{
			if (type == GenericParameterType.Type)
			{
				return ElementType.Var;
			}
			if (type != GenericParameterType.Method)
			{
				throw new ArgumentOutOfRangeException();
			}
			return ElementType.MVar;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00027A5D File Offset: 0x00025C5D
		public override TypeDefinition Resolve()
		{
			return null;
		}

		// Token: 0x0400032F RID: 815
		internal int position;

		// Token: 0x04000330 RID: 816
		internal GenericParameterType type;

		// Token: 0x04000331 RID: 817
		internal IGenericParameterProvider owner;

		// Token: 0x04000332 RID: 818
		private ushort attributes;

		// Token: 0x04000333 RID: 819
		private GenericParameterConstraintCollection constraints;

		// Token: 0x04000334 RID: 820
		private Collection<CustomAttribute> custom_attributes;
	}
}
