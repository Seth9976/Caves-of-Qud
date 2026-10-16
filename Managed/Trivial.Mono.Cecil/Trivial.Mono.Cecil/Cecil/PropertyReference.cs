using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A4 RID: 164
	public abstract class PropertyReference : MemberReference
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00015087 File Offset: 0x00013287
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0001508F File Offset: 0x0001328F
		public TypeReference PropertyType
		{
			get
			{
				return this.property_type;
			}
			set
			{
				this.property_type = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006DA RID: 1754
		public abstract Collection<ParameterDefinition> Parameters { get; }

		// Token: 0x060006DB RID: 1755 RVA: 0x00015098 File Offset: 0x00013298
		internal PropertyReference(string name, TypeReference propertyType)
			: base(name)
		{
			Mixin.CheckType(propertyType, Mixin.Argument.propertyType);
			this.property_type = propertyType;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000150B0 File Offset: 0x000132B0
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x060006DD RID: 1757
		public new abstract PropertyDefinition Resolve();

		// Token: 0x0400023F RID: 575
		private TypeReference property_type;
	}
}
