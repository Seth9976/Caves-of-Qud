using System;
using System.Diagnostics;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000B0 RID: 176
	[DebuggerDisplay("{AttributeType}")]
	public sealed class SecurityAttribute : ICustomAttribute
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x00015C31 File Offset: 0x00013E31
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x00015C39 File Offset: 0x00013E39
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

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00015C42 File Offset: 0x00013E42
		public bool HasFields
		{
			get
			{
				return !this.fields.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x00015C54 File Offset: 0x00013E54
		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				Collection<CustomAttributeNamedArgument> collection;
				if ((collection = this.fields) == null)
				{
					collection = (this.fields = new Collection<CustomAttributeNamedArgument>());
				}
				return collection;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00015C79 File Offset: 0x00013E79
		public bool HasProperties
		{
			get
			{
				return !this.properties.IsNullOrEmpty<CustomAttributeNamedArgument>();
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00015C8C File Offset: 0x00013E8C
		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				Collection<CustomAttributeNamedArgument> collection;
				if ((collection = this.properties) == null)
				{
					collection = (this.properties = new Collection<CustomAttributeNamedArgument>());
				}
				return collection;
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00015CB1 File Offset: 0x00013EB1
		public SecurityAttribute(TypeReference attributeType)
		{
			this.attribute_type = attributeType;
		}

		// Token: 0x0400026B RID: 619
		private TypeReference attribute_type;

		// Token: 0x0400026C RID: 620
		internal Collection<CustomAttributeNamedArgument> fields;

		// Token: 0x0400026D RID: 621
		internal Collection<CustomAttributeNamedArgument> properties;
	}
}
