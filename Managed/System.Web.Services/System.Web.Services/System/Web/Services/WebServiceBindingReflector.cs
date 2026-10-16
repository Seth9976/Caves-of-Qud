using System;
using System.Web.Services.Protocols;

namespace System.Web.Services
{
	// Token: 0x02000015 RID: 21
	internal class WebServiceBindingReflector
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000210D File Offset: 0x0000030D
		private WebServiceBindingReflector()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002968 File Offset: 0x00000B68
		internal static WebServiceBindingAttribute GetAttribute(Type type)
		{
			while (type != null)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(WebServiceBindingAttribute), false);
				if (customAttributes.Length != 0)
				{
					if (customAttributes.Length > 1)
					{
						throw new ArgumentException(Res.GetString("Only one WebServiceBinding attribute may be specified on type '{0}'.", new object[] { type.FullName }), "type");
					}
					return (WebServiceBindingAttribute)customAttributes[0];
				}
				else
				{
					type = type.BaseType;
				}
			}
			return null;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029D4 File Offset: 0x00000BD4
		internal static WebServiceBindingAttribute GetAttribute(LogicalMethodInfo methodInfo, string binding)
		{
			if (methodInfo.Binding != null)
			{
				if (binding.Length > 0 && methodInfo.Binding.Name != binding)
				{
					throw new InvalidOperationException(Res.GetString("The Binding property '{0}' does not match interface binding '{1}'.", new object[]
					{
						binding,
						methodInfo.Binding.Name
					}));
				}
				return methodInfo.Binding;
			}
			else
			{
				Type declaringType = methodInfo.DeclaringType;
				object[] customAttributes = declaringType.GetCustomAttributes(typeof(WebServiceBindingAttribute), false);
				WebServiceBindingAttribute webServiceBindingAttribute = null;
				foreach (WebServiceBindingAttribute webServiceBindingAttribute2 in customAttributes)
				{
					if (webServiceBindingAttribute2.Name == binding)
					{
						if (webServiceBindingAttribute != null)
						{
							throw new ArgumentException(Res.GetString("Type '{0}' has multiple WebServiceBinding attributes that define a binding named '{1}'.", new object[] { declaringType.FullName, binding, "methodInfo" }));
						}
						webServiceBindingAttribute = webServiceBindingAttribute2;
					}
				}
				if (webServiceBindingAttribute == null && binding != null && binding.Length > 0)
				{
					throw new ArgumentException(Res.GetString("Type '{0}' is missing WebServiceBinding attribute that defines a binding named '{1}'.", new object[] { declaringType.FullName, binding }), "methodInfo");
				}
				return webServiceBindingAttribute;
			}
		}
	}
}
