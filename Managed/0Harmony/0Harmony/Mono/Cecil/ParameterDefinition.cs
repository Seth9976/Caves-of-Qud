using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000252 RID: 594
	public sealed class ParameterDefinition : ParameterReference, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider, IMarshalInfoProvider
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002D4D9 File Offset: 0x0002B6D9
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x0002D4E1 File Offset: 0x0002B6E1
		public ParameterAttributes Attributes
		{
			get
			{
				return (ParameterAttributes)this.attributes;
			}
			set
			{
				this.attributes = (ushort)value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0002D4EA File Offset: 0x0002B6EA
		public IMethodSignature Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0002D4F2 File Offset: 0x0002B6F2
		public int Sequence
		{
			get
			{
				if (this.method == null)
				{
					return -1;
				}
				if (!this.method.HasImplicitThis())
				{
					return this.index;
				}
				return this.index + 1;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0002D51A File Offset: 0x0002B71A
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x0002D543 File Offset: 0x0002B743
		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref this.constant, this.parameter_type.Module);
				return this.constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					this.constant = Mixin.NoValue;
				}
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0002D553 File Offset: 0x0002B753
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x0002D565 File Offset: 0x0002B765
		public object Constant
		{
			get
			{
				if (!this.HasConstant)
				{
					return null;
				}
				return this.constant;
			}
			set
			{
				this.constant = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0002D56E File Offset: 0x0002B76E
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.parameter_type.Module);
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0002D598 File Offset: 0x0002B798
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.parameter_type.Module);
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0002D5BB File Offset: 0x0002B7BB
		public bool HasMarshalInfo
		{
			get
			{
				return this.marshal_info != null || this.GetHasMarshalInfo(this.parameter_type.Module);
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0002D5D8 File Offset: 0x0002B7D8
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0002D5FB File Offset: 0x0002B7FB
		public MarshalInfo MarshalInfo
		{
			get
			{
				return this.marshal_info ?? this.GetMarshalInfo(ref this.marshal_info, this.parameter_type.Module);
			}
			set
			{
				this.marshal_info = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0002D604 File Offset: 0x0002B804
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0002D612 File Offset: 0x0002B812
		public bool IsIn
		{
			get
			{
				return this.attributes.GetAttributes(1);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(1, value);
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0002D627 File Offset: 0x0002B827
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x0002D635 File Offset: 0x0002B835
		public bool IsOut
		{
			get
			{
				return this.attributes.GetAttributes(2);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(2, value);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0002D64A File Offset: 0x0002B84A
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x0002D658 File Offset: 0x0002B858
		public bool IsLcid
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

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0002D66D File Offset: 0x0002B86D
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x0002D67B File Offset: 0x0002B87B
		public bool IsReturnValue
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

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0002D690 File Offset: 0x0002B890
		// (set) Token: 0x06000E43 RID: 3651 RVA: 0x0002D69F File Offset: 0x0002B89F
		public bool IsOptional
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

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0002D6B5 File Offset: 0x0002B8B5
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x0002D6C7 File Offset: 0x0002B8C7
		public bool HasDefault
		{
			get
			{
				return this.attributes.GetAttributes(4096);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(4096, value);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0002D6F2 File Offset: 0x0002B8F2
		public bool HasFieldMarshal
		{
			get
			{
				return this.attributes.GetAttributes(8192);
			}
			set
			{
				this.attributes = this.attributes.SetAttributes(8192, value);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002D70B File Offset: 0x0002B90B
		internal ParameterDefinition(TypeReference parameterType, IMethodSignature method)
			: this(string.Empty, ParameterAttributes.None, parameterType)
		{
			this.method = method;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0002D721 File Offset: 0x0002B921
		public ParameterDefinition(TypeReference parameterType)
			: this(string.Empty, ParameterAttributes.None, parameterType)
		{
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002D730 File Offset: 0x0002B930
		public ParameterDefinition(string name, ParameterAttributes attributes, TypeReference parameterType)
			: base(name, parameterType)
		{
			this.attributes = (ushort)attributes;
			this.token = new MetadataToken(TokenType.Param);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00018380 File Offset: 0x00016580
		public override ParameterDefinition Resolve()
		{
			return this;
		}

		// Token: 0x0400048F RID: 1167
		private ushort attributes;

		// Token: 0x04000490 RID: 1168
		internal IMethodSignature method;

		// Token: 0x04000491 RID: 1169
		private object constant = Mixin.NotResolved;

		// Token: 0x04000492 RID: 1170
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000493 RID: 1171
		private MarshalInfo marshal_info;
	}
}
