using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000771 RID: 1905
	internal class NetAuthenticationModuleHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003BFA RID: 15354 RVA: 0x000CD42C File Offset: 0x000CB62C
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			if (section.Attributes != null && section.Attributes.Count != 0)
			{
				HandlersUtil.ThrowException("Unrecognized attribute", section);
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						HandlersUtil.ThrowException("Only elements allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						AuthenticationManager.Clear();
					}
					else
					{
						string text = HandlersUtil.ExtractAttributeValue("type", xmlNode);
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						if (name == "add")
						{
							AuthenticationManager.Register(NetAuthenticationModuleHandler.CreateInstance(text, xmlNode));
						}
						else if (name == "remove")
						{
							AuthenticationManager.Unregister(NetAuthenticationModuleHandler.CreateInstance(text, xmlNode));
						}
						else
						{
							HandlersUtil.ThrowException("Unexpected element", xmlNode);
						}
					}
				}
			}
			return AuthenticationManager.RegisteredModules;
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x000CD57C File Offset: 0x000CB77C
		private static IAuthenticationModule CreateInstance(string typeName, XmlNode node)
		{
			IAuthenticationModule authenticationModule = null;
			try
			{
				authenticationModule = (IAuthenticationModule)Activator.CreateInstance(Type.GetType(typeName, true));
			}
			catch (Exception ex)
			{
				HandlersUtil.ThrowException(ex.Message, node);
			}
			return authenticationModule;
		}
	}
}
