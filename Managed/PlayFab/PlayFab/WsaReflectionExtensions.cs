using System;
using System.Reflection;

namespace PlayFab
{
	// Token: 0x0200001E RID: 30
	public static class WsaReflectionExtensions
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000F934 File Offset: 0x0000DB34
		public static Delegate CreateDelegate(this MethodInfo methodInfo, Type delegateType, object instance)
		{
			return Delegate.CreateDelegate(delegateType, instance, methodInfo);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000F93E File Offset: 0x0000DB3E
		public static Type GetTypeInfo(this Type type)
		{
			return type;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000F941 File Offset: 0x0000DB41
		public static Type AsType(this Type type)
		{
			return type;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000F944 File Offset: 0x0000DB44
		public static string GetDelegateName(this Delegate delegateInstance)
		{
			return delegateInstance.Method.Name;
		}
	}
}
