using System;

namespace System.Reflection
{
	// Token: 0x020008DA RID: 2266
	public static class PropertyInfoExtensions
	{
		// Token: 0x06004B7F RID: 19327 RVA: 0x000F08D0 File Offset: 0x000EEAD0
		public static MethodInfo[] GetAccessors(PropertyInfo property)
		{
			Requires.NotNull(property, "property");
			return property.GetAccessors();
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x000F08E3 File Offset: 0x000EEAE3
		public static MethodInfo[] GetAccessors(PropertyInfo property, bool nonPublic)
		{
			Requires.NotNull(property, "property");
			return property.GetAccessors(nonPublic);
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x000F08F7 File Offset: 0x000EEAF7
		public static MethodInfo GetGetMethod(PropertyInfo property)
		{
			Requires.NotNull(property, "property");
			return property.GetGetMethod();
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x000F090A File Offset: 0x000EEB0A
		public static MethodInfo GetGetMethod(PropertyInfo property, bool nonPublic)
		{
			Requires.NotNull(property, "property");
			return property.GetGetMethod(nonPublic);
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x000F091E File Offset: 0x000EEB1E
		public static MethodInfo GetSetMethod(PropertyInfo property)
		{
			Requires.NotNull(property, "property");
			return property.GetSetMethod();
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x000F0931 File Offset: 0x000EEB31
		public static MethodInfo GetSetMethod(PropertyInfo property, bool nonPublic)
		{
			Requires.NotNull(property, "property");
			return property.GetSetMethod(nonPublic);
		}
	}
}
