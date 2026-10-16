using System;

namespace System.Reflection
{
	// Token: 0x020008D4 RID: 2260
	public static class TypeExtensions
	{
		// Token: 0x06004B4F RID: 19279 RVA: 0x000F04D7 File Offset: 0x000EE6D7
		public static ConstructorInfo GetConstructor(Type type, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructor(types);
		}

		// Token: 0x06004B50 RID: 19280 RVA: 0x000F04EB File Offset: 0x000EE6EB
		public static ConstructorInfo[] GetConstructors(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructors();
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x000F04FE File Offset: 0x000EE6FE
		public static ConstructorInfo[] GetConstructors(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetConstructors(bindingAttr);
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x000F0512 File Offset: 0x000EE712
		public static MemberInfo[] GetDefaultMembers(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetDefaultMembers();
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x000F0525 File Offset: 0x000EE725
		public static EventInfo GetEvent(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetEvent(name);
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x000F0539 File Offset: 0x000EE739
		public static EventInfo GetEvent(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetEvent(name, bindingAttr);
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x000F054E File Offset: 0x000EE74E
		public static EventInfo[] GetEvents(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetEvents();
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x000F0561 File Offset: 0x000EE761
		public static EventInfo[] GetEvents(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetEvents(bindingAttr);
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x000F0575 File Offset: 0x000EE775
		public static FieldInfo GetField(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetField(name);
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000F0589 File Offset: 0x000EE789
		public static FieldInfo GetField(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetField(name, bindingAttr);
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x000F059E File Offset: 0x000EE79E
		public static FieldInfo[] GetFields(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetFields();
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x000F05B1 File Offset: 0x000EE7B1
		public static FieldInfo[] GetFields(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetFields(bindingAttr);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x000F05C5 File Offset: 0x000EE7C5
		public static Type[] GetGenericArguments(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetGenericArguments();
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x000F05D8 File Offset: 0x000EE7D8
		public static Type[] GetInterfaces(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetInterfaces();
		}

		// Token: 0x06004B5D RID: 19293 RVA: 0x000F05EB File Offset: 0x000EE7EB
		public static MemberInfo[] GetMember(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetMember(name);
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x000F05FF File Offset: 0x000EE7FF
		public static MemberInfo[] GetMember(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMember(name, bindingAttr);
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x000F0614 File Offset: 0x000EE814
		public static MemberInfo[] GetMembers(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetMembers();
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x000F0627 File Offset: 0x000EE827
		public static MemberInfo[] GetMembers(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMembers(bindingAttr);
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x000F063B File Offset: 0x000EE83B
		public static MethodInfo GetMethod(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name);
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x000F064F File Offset: 0x000EE84F
		public static MethodInfo GetMethod(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name, bindingAttr);
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x000F0664 File Offset: 0x000EE864
		public static MethodInfo GetMethod(Type type, string name, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetMethod(name, types);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x000F0679 File Offset: 0x000EE879
		public static MethodInfo[] GetMethods(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetMethods();
		}

		// Token: 0x06004B65 RID: 19301 RVA: 0x000F068C File Offset: 0x000EE88C
		public static MethodInfo[] GetMethods(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetMethods(bindingAttr);
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x000F06A0 File Offset: 0x000EE8A0
		public static Type GetNestedType(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetNestedType(name, bindingAttr);
		}

		// Token: 0x06004B67 RID: 19303 RVA: 0x000F06B5 File Offset: 0x000EE8B5
		public static Type[] GetNestedTypes(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetNestedTypes(bindingAttr);
		}

		// Token: 0x06004B68 RID: 19304 RVA: 0x000F06C9 File Offset: 0x000EE8C9
		public static PropertyInfo[] GetProperties(Type type)
		{
			Requires.NotNull(type, "type");
			return type.GetProperties();
		}

		// Token: 0x06004B69 RID: 19305 RVA: 0x000F06DC File Offset: 0x000EE8DC
		public static PropertyInfo[] GetProperties(Type type, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetProperties(bindingAttr);
		}

		// Token: 0x06004B6A RID: 19306 RVA: 0x000F06F0 File Offset: 0x000EE8F0
		public static PropertyInfo GetProperty(Type type, string name)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name);
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x000F0704 File Offset: 0x000EE904
		public static PropertyInfo GetProperty(Type type, string name, BindingFlags bindingAttr)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, bindingAttr);
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x000F0719 File Offset: 0x000EE919
		public static PropertyInfo GetProperty(Type type, string name, Type returnType)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, returnType);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x000F072E File Offset: 0x000EE92E
		public static PropertyInfo GetProperty(Type type, string name, Type returnType, Type[] types)
		{
			Requires.NotNull(type, "type");
			return type.GetProperty(name, returnType, types);
		}

		// Token: 0x06004B6E RID: 19310 RVA: 0x000F0744 File Offset: 0x000EE944
		public static bool IsAssignableFrom(Type type, Type c)
		{
			Requires.NotNull(type, "type");
			return type.IsAssignableFrom(c);
		}

		// Token: 0x06004B6F RID: 19311 RVA: 0x000F0758 File Offset: 0x000EE958
		public static bool IsInstanceOfType(Type type, object o)
		{
			Requires.NotNull(type, "type");
			return type.IsInstanceOfType(o);
		}
	}
}
