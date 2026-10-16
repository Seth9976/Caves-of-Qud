using System;
using System.Reflection;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020007A8 RID: 1960
	[Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
	internal interface IExpando : IReflect
	{
		// Token: 0x06004520 RID: 17696
		FieldInfo AddField(string name);

		// Token: 0x06004521 RID: 17697
		PropertyInfo AddProperty(string name);

		// Token: 0x06004522 RID: 17698
		MethodInfo AddMethod(string name, Delegate method);

		// Token: 0x06004523 RID: 17699
		void RemoveMember(MemberInfo m);
	}
}
