using System;
using System.Diagnostics;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000262 RID: 610
	[DebuggerDisplay("{AttributeType}")]
	public sealed class SecurityAttribute : ICustomAttribute
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0002E123 File Offset: 0x0002C323
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x0002E12B File Offset: 0x0002C32B
		public TypeReference AttributeType
		{
			get
			{
				return this.attribute_type;
			}
			set
			{
				this.attribute_type = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0002E134 File Offset: 0x0002C334
		public bool HasFields
		{
			get
			{
				return !this.fields.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0002E144 File Offset: 0x0002C344
		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				if (this.fields == null)
				{
					Interlocked.CompareExchange<Collection<CustomAttributeNamedArgument>>(ref this.fields, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return this.fields;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0002E166 File Offset: 0x0002C366
		public bool HasProperties
		{
			get
			{
				return !this.properties.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002E176 File Offset: 0x0002C376
		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				if (this.properties == null)
				{
					Interlocked.CompareExchange<Collection<CustomAttributeNamedArgument>>(ref this.properties, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return this.properties;
			}
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0002E198 File Offset: 0x0002C398
		public SecurityAttribute(TypeReference attributeType)
		{
			this.attribute_type = attributeType;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00018318 File Offset: 0x00016518
		bool ICustomAttribute.HasConstructorArguments
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00003B32 File Offset: 0x00001D32
		Collection<CustomAttributeArgument> ICustomAttribute.ConstructorArguments
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040004D6 RID: 1238
		private TypeReference attribute_type;

		// Token: 0x040004D7 RID: 1239
		internal Collection<CustomAttributeNamedArgument> fields;

		// Token: 0x040004D8 RID: 1240
		internal Collection<CustomAttributeNamedArgument> properties;
	}
}
