using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200005E RID: 94
	public sealed class InterfaceImplementation : ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000072BA File Offset: 0x000054BA
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000072C2 File Offset: 0x000054C2
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000072CB File Offset: 0x000054CB
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00007300 File Offset: 0x00005500
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				if (this.type == null)
				{
					return this.custom_attributes = new Collection<CustomAttribute>();
				}
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.type.Module);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00007345 File Offset: 0x00005545
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000734D File Offset: 0x0000554D
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

		// Token: 0x060002BF RID: 703 RVA: 0x00007356 File Offset: 0x00005556
		internal InterfaceImplementation(TypeReference interfaceType, MetadataToken token)
		{
			this.interface_type = interfaceType;
			this.token = token;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000736C File Offset: 0x0000556C
		public InterfaceImplementation(TypeReference interfaceType)
		{
			Mixin.CheckType(interfaceType, Mixin.Argument.interfaceType);
			this.interface_type = interfaceType;
			this.token = new MetadataToken(TokenType.InterfaceImpl);
		}

		// Token: 0x040000A2 RID: 162
		internal TypeDefinition type;

		// Token: 0x040000A3 RID: 163
		internal MetadataToken token;

		// Token: 0x040000A4 RID: 164
		private TypeReference interface_type;

		// Token: 0x040000A5 RID: 165
		private Collection<CustomAttribute> custom_attributes;
	}
}
