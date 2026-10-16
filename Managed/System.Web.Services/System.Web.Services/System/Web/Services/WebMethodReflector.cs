using System;
using System.Collections;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services
{
	// Token: 0x0200000F RID: 15
	internal class WebMethodReflector
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000210D File Offset: 0x0000030D
		private WebMethodReflector()
		{
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000022A4 File Offset: 0x000004A4
		internal static WebMethodAttribute GetAttribute(MethodInfo implementation, MethodInfo declaration)
		{
			WebMethodAttribute webMethodAttribute = null;
			WebMethodAttribute webMethodAttribute2 = null;
			object[] array;
			if (declaration != null)
			{
				array = declaration.GetCustomAttributes(typeof(WebMethodAttribute), false);
				if (array.Length != 0)
				{
					webMethodAttribute = (WebMethodAttribute)array[0];
				}
			}
			array = implementation.GetCustomAttributes(typeof(WebMethodAttribute), false);
			if (array.Length != 0)
			{
				webMethodAttribute2 = (WebMethodAttribute)array[0];
			}
			if (webMethodAttribute == null)
			{
				return webMethodAttribute2;
			}
			if (webMethodAttribute2 == null)
			{
				return webMethodAttribute;
			}
			if (webMethodAttribute2.MessageNameSpecified)
			{
				throw new InvalidOperationException(Res.GetString("Method {0} on type {1} is an implementation of the service contract interface {2}. Service Description altering attributes cannot be specified on service implementation.  Please place custom attribute on the interface declaration: interface: {2}, method: {3}, attribute: {4}.", new object[]
				{
					implementation.Name,
					implementation.DeclaringType.FullName,
					declaration.DeclaringType.FullName,
					declaration.ToString(),
					"WebMethod.MessageName"
				}));
			}
			return new WebMethodAttribute(webMethodAttribute2.EnableSessionSpecified ? webMethodAttribute2.EnableSession : webMethodAttribute.EnableSession)
			{
				TransactionOption = (webMethodAttribute2.TransactionOptionSpecified ? webMethodAttribute2.TransactionOption : webMethodAttribute.TransactionOption),
				CacheDuration = (webMethodAttribute2.CacheDurationSpecified ? webMethodAttribute2.CacheDuration : webMethodAttribute.CacheDuration),
				BufferResponse = (webMethodAttribute2.BufferResponseSpecified ? webMethodAttribute2.BufferResponse : webMethodAttribute.BufferResponse),
				Description = (webMethodAttribute2.DescriptionSpecified ? webMethodAttribute2.Description : webMethodAttribute.Description)
			};
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023E8 File Offset: 0x000005E8
		internal static MethodInfo FindInterfaceMethodInfo(Type type, string signature)
		{
			foreach (Type type2 in type.GetInterfaces())
			{
				InterfaceMapping interfaceMap = type.GetInterfaceMap(type2);
				MethodInfo[] targetMethods = interfaceMap.TargetMethods;
				for (int j = 0; j < targetMethods.Length; j++)
				{
					if (targetMethods[j].ToString() == signature)
					{
						return interfaceMap.InterfaceMethods[j];
					}
				}
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002450 File Offset: 0x00000650
		internal static LogicalMethodInfo[] GetMethods(Type type)
		{
			if (type.IsInterface)
			{
				throw new InvalidOperationException(Res.GetString("Cannot create an instance of an interface {0}. Please specify a concrete implementation of the interface instead.", new object[] { type.FullName }));
			}
			ArrayList arrayList = new ArrayList();
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			for (int i = 0; i < methods.Length; i++)
			{
				Type declaringType = methods[i].DeclaringType;
				if (!(declaringType == typeof(object)) && !(declaringType == typeof(WebService)))
				{
					string text = methods[i].ToString();
					MethodInfo methodInfo = WebMethodReflector.FindInterfaceMethodInfo(declaringType, text);
					WebServiceBindingAttribute webServiceBindingAttribute = null;
					if (methodInfo != null)
					{
						object[] customAttributes = methodInfo.DeclaringType.GetCustomAttributes(typeof(WebServiceBindingAttribute), false);
						if (customAttributes.Length != 0)
						{
							if (customAttributes.Length > 1)
							{
								throw new ArgumentException(Res.GetString("Only one WebServiceBinding attribute may be specified on type '{0}'.", new object[] { methodInfo.DeclaringType.FullName }), "type");
							}
							webServiceBindingAttribute = (WebServiceBindingAttribute)customAttributes[0];
							if (webServiceBindingAttribute.Name == null || webServiceBindingAttribute.Name.Length == 0)
							{
								webServiceBindingAttribute.Name = methodInfo.DeclaringType.Name;
							}
						}
						else
						{
							methodInfo = null;
						}
					}
					else if (!methods[i].IsPublic)
					{
						goto IL_01D4;
					}
					WebMethodAttribute attribute = WebMethodReflector.GetAttribute(methods[i], methodInfo);
					if (attribute != null)
					{
						WebMethod webMethod = new WebMethod(methodInfo, webServiceBindingAttribute, attribute);
						hashtable2.Add(methods[i], webMethod);
						MethodInfo methodInfo2 = (MethodInfo)hashtable[text];
						if (methodInfo2 == null)
						{
							hashtable.Add(text, methods[i]);
							arrayList.Add(methods[i]);
						}
						else if (methodInfo2.DeclaringType.IsAssignableFrom(methods[i].DeclaringType))
						{
							hashtable[text] = methods[i];
							arrayList[arrayList.IndexOf(methodInfo2)] = methods[i];
						}
					}
				}
				IL_01D4:;
			}
			return LogicalMethodInfo.Create((MethodInfo[])arrayList.ToArray(typeof(MethodInfo)), (LogicalMethodTypes)3, hashtable2);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002660 File Offset: 0x00000860
		internal static void IncludeTypes(LogicalMethodInfo[] methods, XmlReflectionImporter importer)
		{
			for (int i = 0; i < methods.Length; i++)
			{
				WebMethodReflector.IncludeTypes(methods[i], importer);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002684 File Offset: 0x00000884
		internal static void IncludeTypes(LogicalMethodInfo method, XmlReflectionImporter importer)
		{
			if (method.Declaration != null)
			{
				importer.IncludeTypes(method.Declaration.DeclaringType);
				importer.IncludeTypes(method.Declaration);
			}
			importer.IncludeTypes(method.DeclaringType);
			importer.IncludeTypes(method.CustomAttributeProvider);
		}
	}
}
