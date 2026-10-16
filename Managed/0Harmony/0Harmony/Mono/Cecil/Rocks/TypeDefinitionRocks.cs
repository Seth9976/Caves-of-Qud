using System;
using System.Collections.Generic;
using System.Linq;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000425 RID: 1061
	public static class TypeDefinitionRocks
	{
		// Token: 0x0600165C RID: 5724 RVA: 0x000486BC File Offset: 0x000468BC
		public static IEnumerable<MethodDefinition> GetConstructors(this TypeDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (!self.HasMethods)
			{
				return Empty<MethodDefinition>.Array;
			}
			return self.Methods.Where<MethodDefinition>((MethodDefinition method) => method.IsConstructor);
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00048710 File Offset: 0x00046910
		public static MethodDefinition GetStaticConstructor(this TypeDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (!self.HasMethods)
			{
				return null;
			}
			return self.GetConstructors().FirstOrDefault<MethodDefinition>((MethodDefinition ctor) => ctor.IsStatic);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00048760 File Offset: 0x00046960
		public static IEnumerable<MethodDefinition> GetMethods(this TypeDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (!self.HasMethods)
			{
				return Empty<MethodDefinition>.Array;
			}
			return self.Methods.Where<MethodDefinition>((MethodDefinition method) => !method.IsConstructor);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000487B3 File Offset: 0x000469B3
		public static TypeReference GetEnumUnderlyingType(this TypeDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (!self.IsEnum)
			{
				throw new ArgumentException();
			}
			return self.GetEnumUnderlyingType();
		}
	}
}
