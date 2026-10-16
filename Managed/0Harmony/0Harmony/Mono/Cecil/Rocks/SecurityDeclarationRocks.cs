using System;
using System.Security;
using System.Security.Permissions;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000424 RID: 1060
	internal static class SecurityDeclarationRocks
	{
		// Token: 0x06001653 RID: 5715 RVA: 0x00048320 File Offset: 0x00046520
		public static PermissionSet ToPermissionSet(this SecurityDeclaration self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			PermissionSet permissionSet;
			if (SecurityDeclarationRocks.TryProcessPermissionSetAttribute(self, out permissionSet))
			{
				return permissionSet;
			}
			return SecurityDeclarationRocks.CreatePermissionSet(self);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00048350 File Offset: 0x00046550
		private static bool TryProcessPermissionSetAttribute(SecurityDeclaration declaration, out PermissionSet set)
		{
			set = null;
			if (!declaration.HasSecurityAttributes && declaration.SecurityAttributes.Count != 1)
			{
				return false;
			}
			SecurityAttribute securityAttribute = declaration.SecurityAttributes[0];
			if (!securityAttribute.AttributeType.IsTypeOf("System.Security.Permissions", "PermissionSetAttribute"))
			{
				return false;
			}
			PermissionSetAttribute permissionSetAttribute = new PermissionSetAttribute((SecurityAction)declaration.Action);
			CustomAttributeNamedArgument customAttributeNamedArgument = securityAttribute.Properties[0];
			string text = (string)customAttributeNamedArgument.Argument.Value;
			string name = customAttributeNamedArgument.Name;
			if (!(name == "XML"))
			{
				if (!(name == "Name"))
				{
					throw new NotImplementedException(customAttributeNamedArgument.Name);
				}
				permissionSetAttribute.Name = text;
			}
			else
			{
				permissionSetAttribute.XML = text;
			}
			set = permissionSetAttribute.CreatePermissionSet();
			return true;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0004841C File Offset: 0x0004661C
		private static PermissionSet CreatePermissionSet(SecurityDeclaration declaration)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			foreach (SecurityAttribute securityAttribute in declaration.SecurityAttributes)
			{
				IPermission permission = SecurityDeclarationRocks.CreatePermission(declaration, securityAttribute);
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00048480 File Offset: 0x00046680
		private static IPermission CreatePermission(SecurityDeclaration declaration, SecurityAttribute attribute)
		{
			Type type = Type.GetType(attribute.AttributeType.FullName);
			if (type == null)
			{
				throw new ArgumentException("attribute");
			}
			SecurityAttribute securityAttribute = SecurityDeclarationRocks.CreateSecurityAttribute(type, declaration);
			if (securityAttribute == null)
			{
				throw new InvalidOperationException();
			}
			SecurityDeclarationRocks.CompleteSecurityAttribute(securityAttribute, attribute);
			return securityAttribute.CreatePermission();
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000484CC File Offset: 0x000466CC
		private static void CompleteSecurityAttribute(SecurityAttribute security_attribute, SecurityAttribute attribute)
		{
			if (attribute.HasFields)
			{
				SecurityDeclarationRocks.CompleteSecurityAttributeFields(security_attribute, attribute);
			}
			if (attribute.HasProperties)
			{
				SecurityDeclarationRocks.CompleteSecurityAttributeProperties(security_attribute, attribute);
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000484EC File Offset: 0x000466EC
		private static void CompleteSecurityAttributeFields(SecurityAttribute security_attribute, SecurityAttribute attribute)
		{
			Type type = security_attribute.GetType();
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attribute.Fields)
			{
				type.GetField(customAttributeNamedArgument.Name).SetValue(security_attribute, customAttributeNamedArgument.Argument.Value);
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00048564 File Offset: 0x00046764
		private static void CompleteSecurityAttributeProperties(SecurityAttribute security_attribute, SecurityAttribute attribute)
		{
			Type type = security_attribute.GetType();
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attribute.Properties)
			{
				type.GetProperty(customAttributeNamedArgument.Name).SetValue(security_attribute, customAttributeNamedArgument.Argument.Value, null);
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000485DC File Offset: 0x000467DC
		private static SecurityAttribute CreateSecurityAttribute(Type attribute_type, SecurityDeclaration declaration)
		{
			SecurityAttribute securityAttribute;
			try
			{
				securityAttribute = (SecurityAttribute)Activator.CreateInstance(attribute_type, new object[] { (SecurityAction)declaration.Action });
			}
			catch (MissingMethodException)
			{
				securityAttribute = (SecurityAttribute)Activator.CreateInstance(attribute_type, new object[0]);
			}
			return securityAttribute;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00048634 File Offset: 0x00046834
		public static SecurityDeclaration ToSecurityDeclaration(this PermissionSet self, SecurityAction action, ModuleDefinition module)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			SecurityDeclaration securityDeclaration = new SecurityDeclaration(action);
			SecurityAttribute securityAttribute = new SecurityAttribute(module.TypeSystem.LookupType("System.Security.Permissions", "PermissionSetAttribute"));
			securityAttribute.Properties.Add(new CustomAttributeNamedArgument("XML", new CustomAttributeArgument(module.TypeSystem.String, self.ToXml().ToString())));
			securityDeclaration.SecurityAttributes.Add(securityAttribute);
			return securityDeclaration;
		}
	}
}
