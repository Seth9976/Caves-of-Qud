using System;
using System.Collections.Generic;
using System.Linq;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000420 RID: 1056
	public static class ModuleDefinitionRocks
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x00048296 File Offset: 0x00046496
		public static IEnumerable<TypeDefinition> GetAllTypes(this ModuleDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			return self.Types.SelectMany<TypeDefinition, TypeDefinition>(Functional.Y<TypeDefinition, IEnumerable<TypeDefinition>>((Func<TypeDefinition, IEnumerable<TypeDefinition>> f) => (TypeDefinition type) => type.NestedTypes.SelectMany<TypeDefinition, TypeDefinition>(f).Prepend(type)));
		}
	}
}
