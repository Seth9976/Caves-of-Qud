using System;

namespace Mono.Cecil.Rocks
{
	// Token: 0x0200041F RID: 1055
	internal static class MethodDefinitionRocks
	{
		// Token: 0x06001648 RID: 5704 RVA: 0x000481E0 File Offset: 0x000463E0
		public static MethodDefinition GetBaseMethod(this MethodDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (!self.IsVirtual)
			{
				return self;
			}
			if (self.IsNewSlot)
			{
				return self;
			}
			for (TypeDefinition typeDefinition = MethodDefinitionRocks.ResolveBaseType(self.DeclaringType); typeDefinition != null; typeDefinition = MethodDefinitionRocks.ResolveBaseType(typeDefinition))
			{
				MethodDefinition matchingMethod = MethodDefinitionRocks.GetMatchingMethod(typeDefinition, self);
				if (matchingMethod != null)
				{
					return matchingMethod;
				}
			}
			return self;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00048238 File Offset: 0x00046438
		public static MethodDefinition GetOriginalBaseMethod(this MethodDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			for (;;)
			{
				MethodDefinition baseMethod = self.GetBaseMethod();
				if (baseMethod == self)
				{
					break;
				}
				self = baseMethod;
			}
			return self;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00048264 File Offset: 0x00046464
		private static TypeDefinition ResolveBaseType(TypeDefinition type)
		{
			if (type == null)
			{
				return null;
			}
			TypeReference baseType = type.BaseType;
			if (baseType == null)
			{
				return null;
			}
			return baseType.Resolve();
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00048288 File Offset: 0x00046488
		private static MethodDefinition GetMatchingMethod(TypeDefinition type, MethodDefinition method)
		{
			return MetadataResolver.GetMethod(type.Methods, method);
		}
	}
}
