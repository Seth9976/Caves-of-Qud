using System;
using System.Collections.Generic;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000BE RID: 190
	internal sealed class TypeDefinitionProjection
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00018846 File Offset: 0x00016A46
		public TypeDefinitionProjection(TypeDefinition type, TypeDefinitionTreatment treatment, Collection<MethodDefinition> redirectedMethods, Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> redirectedInterfaces)
		{
			this.Attributes = type.Attributes;
			this.Name = type.Name;
			this.Treatment = treatment;
			this.RedirectedMethods = redirectedMethods;
			this.RedirectedInterfaces = redirectedInterfaces;
		}

		// Token: 0x040002D4 RID: 724
		public readonly TypeAttributes Attributes;

		// Token: 0x040002D5 RID: 725
		public readonly string Name;

		// Token: 0x040002D6 RID: 726
		public readonly TypeDefinitionTreatment Treatment;

		// Token: 0x040002D7 RID: 727
		public readonly Collection<MethodDefinition> RedirectedMethods;

		// Token: 0x040002D8 RID: 728
		public readonly Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> RedirectedInterfaces;
	}
}
