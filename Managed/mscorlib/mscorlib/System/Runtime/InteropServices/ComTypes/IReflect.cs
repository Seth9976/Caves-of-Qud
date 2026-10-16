using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020007AC RID: 1964
	[Guid("AFBF15E5-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IReflect
	{
		// Token: 0x0600453E RID: 17726
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x0600453F RID: 17727
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		// Token: 0x06004540 RID: 17728
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		// Token: 0x06004541 RID: 17729
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		// Token: 0x06004542 RID: 17730
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		// Token: 0x06004543 RID: 17731
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		// Token: 0x06004544 RID: 17732
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06004545 RID: 17733
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		// Token: 0x06004546 RID: 17734
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		// Token: 0x06004547 RID: 17735
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		// Token: 0x06004548 RID: 17736
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004549 RID: 17737
		Type UnderlyingSystemType { get; }
	}
}
