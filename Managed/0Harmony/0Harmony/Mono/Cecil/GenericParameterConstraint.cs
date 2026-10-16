using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200020E RID: 526
	public sealed class GenericParameterConstraint : ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000280A9 File Offset: 0x000262A9
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x000280B1 File Offset: 0x000262B1
		public TypeReference ConstraintType
		{
			get
			{
				return this.constraint_type;
			}
			set
			{
				this.constraint_type = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x000280BA File Offset: 0x000262BA
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.generic_parameter != null && this.GetHasCustomAttributes(this.generic_parameter.Module);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000280F0 File Offset: 0x000262F0
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				if (this.generic_parameter == null)
				{
					if (this.custom_attributes == null)
					{
						Interlocked.CompareExchange<Collection<CustomAttribute>>(ref this.custom_attributes, new Collection<CustomAttribute>(), null);
					}
					return this.custom_attributes;
				}
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.generic_parameter.Module);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00028147 File Offset: 0x00026347
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x0002814F File Offset: 0x0002634F
		public MetadataToken MetadataToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00028158 File Offset: 0x00026358
		internal GenericParameterConstraint(TypeReference constraintType, MetadataToken token)
		{
			this.constraint_type = constraintType;
			this.token = token;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002816E File Offset: 0x0002636E
		public GenericParameterConstraint(TypeReference constraintType)
		{
			Mixin.CheckType(constraintType, Mixin.Argument.constraintType);
			this.constraint_type = constraintType;
			this.token = new MetadataToken(TokenType.GenericParamConstraint);
		}

		// Token: 0x04000339 RID: 825
		internal GenericParameter generic_parameter;

		// Token: 0x0400033A RID: 826
		internal MetadataToken token;

		// Token: 0x0400033B RID: 827
		private TypeReference constraint_type;

		// Token: 0x0400033C RID: 828
		private Collection<CustomAttribute> custom_attributes;
	}
}
