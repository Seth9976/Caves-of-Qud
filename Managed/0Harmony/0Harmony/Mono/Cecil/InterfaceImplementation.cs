using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000270 RID: 624
	public sealed class InterfaceImplementation : ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0002F053 File Offset: 0x0002D253
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0002F05B File Offset: 0x0002D25B
		public TypeReference InterfaceType
		{
			get
			{
				return this.interface_type;
			}
			set
			{
				this.interface_type = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0002F064 File Offset: 0x0002D264
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.type != null && this.GetHasCustomAttributes(this.type.Module);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0002F098 File Offset: 0x0002D298
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				if (this.type == null)
				{
					if (this.custom_attributes == null)
					{
						Interlocked.CompareExchange<Collection<CustomAttribute>>(ref this.custom_attributes, new Collection<CustomAttribute>(), null);
					}
					return this.custom_attributes;
				}
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.type.Module);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0002F0EF File Offset: 0x0002D2EF
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x0002F0F7 File Offset: 0x0002D2F7
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

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002F100 File Offset: 0x0002D300
		internal InterfaceImplementation(TypeReference interfaceType, MetadataToken token)
		{
			this.interface_type = interfaceType;
			this.token = token;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002F116 File Offset: 0x0002D316
		public InterfaceImplementation(TypeReference interfaceType)
		{
			Mixin.CheckType(interfaceType, Mixin.Argument.interfaceType);
			this.interface_type = interfaceType;
			this.token = new MetadataToken(TokenType.InterfaceImpl);
		}

		// Token: 0x04000545 RID: 1349
		internal TypeDefinition type;

		// Token: 0x04000546 RID: 1350
		internal MetadataToken token;

		// Token: 0x04000547 RID: 1351
		private TypeReference interface_type;

		// Token: 0x04000548 RID: 1352
		private Collection<CustomAttribute> custom_attributes;
	}
}
