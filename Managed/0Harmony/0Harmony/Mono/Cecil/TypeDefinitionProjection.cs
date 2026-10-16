using System;
using System.Collections.Generic;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200027F RID: 639
	internal sealed class TypeDefinitionProjection
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00031291 File Offset: 0x0002F491
		public TypeDefinitionProjection(TypeDefinition type, TypeDefinitionTreatment treatment, Collection<MethodDefinition> redirectedMethods, Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> redirectedInterfaces)
		{
			this.Attributes = type.Attributes;
			this.Name = type.Name;
			this.Treatment = treatment;
			this.RedirectedMethods = redirectedMethods;
			this.RedirectedInterfaces = redirectedInterfaces;
		}

		// Token: 0x040005AF RID: 1455
		public readonly TypeAttributes Attributes;

		// Token: 0x040005B0 RID: 1456
		public readonly string Name;

		// Token: 0x040005B1 RID: 1457
		public readonly TypeDefinitionTreatment Treatment;

		// Token: 0x040005B2 RID: 1458
		public readonly Collection<MethodDefinition> RedirectedMethods;

		// Token: 0x040005B3 RID: 1459
		public readonly Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> RedirectedInterfaces;
	}
}
