using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200025C RID: 604
	public abstract class PropertyReference : MemberReference
	{
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0002E035 File Offset: 0x0002C235
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x0002E03D File Offset: 0x0002C23D
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000EB0 RID: 3760
		public abstract Collection<ParameterDefinition> Parameters { get; }

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002E046 File Offset: 0x0002C246
		internal PropertyReference(string name, TypeReference propertyType)
			: base(name)
		{
			Mixin.CheckType(propertyType, Mixin.Argument.propertyType);
			this.property_type = propertyType;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002E05E File Offset: 0x0002C25E
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000EB3 RID: 3763
		public new abstract PropertyDefinition Resolve();

		// Token: 0x040004BF RID: 1215
		private TypeReference property_type;
	}
}
