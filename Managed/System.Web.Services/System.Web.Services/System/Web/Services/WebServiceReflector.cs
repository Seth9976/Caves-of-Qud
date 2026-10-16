using System;
using System.Web.Services.Protocols;

namespace System.Web.Services
{
	// Token: 0x02000013 RID: 19
	internal class WebServiceReflector
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000210D File Offset: 0x0000030D
		private WebServiceReflector()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002818 File Offset: 0x00000A18
		internal static WebServiceAttribute GetAttribute(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(WebServiceAttribute), false);
			if (customAttributes.Length == 0)
			{
				return new WebServiceAttribute();
			}
			return (WebServiceAttribute)customAttributes[0];
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002849 File Offset: 0x00000A49
		internal static WebServiceAttribute GetAttribute(LogicalMethodInfo[] methodInfos)
		{
			if (methodInfos.Length == 0)
			{
				return new WebServiceAttribute();
			}
			return WebServiceReflector.GetAttribute(WebServiceReflector.GetMostDerivedType(methodInfos));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002860 File Offset: 0x00000A60
		internal static Type GetMostDerivedType(LogicalMethodInfo[] methodInfos)
		{
			if (methodInfos.Length == 0)
			{
				return null;
			}
			Type type = methodInfos[0].DeclaringType;
			for (int i = 1; i < methodInfos.Length; i++)
			{
				Type declaringType = methodInfos[i].DeclaringType;
				if (declaringType.IsSubclassOf(type))
				{
					type = declaringType;
				}
			}
			return type;
		}
	}
}
