using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.Resources.Tools
{
	/// <summary>Provides support for strongly typed resources. This class cannot be inherited. </summary>
	// Token: 0x020000E1 RID: 225
	public static class StronglyTypedResourceBuilder
	{
		/// <summary>Generates a class file that contains strongly typed properties that match the resources in the specified .resx file.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
		/// <param name="resxFile">The name of a .resx file used as input.</param>
		/// <param name="baseName">The name of the class to be generated.</param>
		/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  class that provides the language in which the class will be generated.</param>
		/// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
		/// <param name="unmatchable">A <see cref="T:System.String" /> array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="basename" /> or <paramref name="codeProvider" /> is null.</exception>
		// Token: 0x0600067A RID: 1658 RVA: 0x000098D0 File Offset: 0x00007AD0
		public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			return StronglyTypedResourceBuilder.Create(resxFile, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
		}

		/// <summary>Generates a class file that contains strongly typed properties that match the resources in the specified .resx file.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
		/// <param name="resxFile">The name of a .resx file used as input.</param>
		/// <param name="baseName">The name of the class to be generated.</param>
		/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
		/// <param name="resourcesNamespace">The namespace of the resource to be generated. </param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  class that provides the language in which the class will be generated.</param>
		/// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
		/// <param name="unmatchable">A <see cref="T:System.String" /> array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="basename " />or <paramref name="codeProvider" /> is null.</exception>
		// Token: 0x0600067B RID: 1659 RVA: 0x000098E0 File Offset: 0x00007AE0
		public static CodeCompileUnit Create(string resxFile, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			if (resxFile == null)
			{
				throw new ArgumentNullException("Parameter resxFile must not be null");
			}
			List<char> list = new List<char>(Path.GetInvalidPathChars());
			foreach (char c in resxFile.ToCharArray())
			{
				if (list.Contains(c))
				{
					throw new ArgumentException("Invalid character in resxFileName");
				}
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			using (ResXResourceReader resXResourceReader = new ResXResourceReader(resxFile))
			{
				foreach (object obj in resXResourceReader)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			return StronglyTypedResourceBuilder.Create(dictionary, baseName, generatedCodeNamespace, resourcesNamespace, codeProvider, internalClass, out unmatchable);
		}

		/// <summary>Generates a class file that contains strongly typed properties that match the resources referenced in the specified collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
		/// <param name="resourceList">An <see cref="T:System.Collections.IDictionary" /> collection where each dictionary entry key/value pair is the name of a resource and the value of the resource.</param>
		/// <param name="baseName">The name of the class to be generated.</param>
		/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" />  class that provides the language in which the class will be generated.</param>
		/// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
		/// <param name="unmatchable">An array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceList" />, <paramref name="basename" />, or <paramref name="codeProvider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A resource node name does not match its key in <paramref name="resourceList" />.</exception>
		// Token: 0x0600067C RID: 1660 RVA: 0x000099CC File Offset: 0x00007BCC
		public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			return StronglyTypedResourceBuilder.Create(resourceList, baseName, generatedCodeNamespace, null, codeProvider, internalClass, out unmatchable);
		}

		/// <summary>Generates a class file that contains strongly typed properties that match the resources referenced in the specified collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> container.</returns>
		/// <param name="resourceList">An <see cref="T:System.Collections.IDictionary" /> collection where each dictionary entry key/value pair is the name of a resource and the value of the resource.</param>
		/// <param name="baseName">The name of the class to be generated.</param>
		/// <param name="generatedCodeNamespace">The namespace of the class to be generated.</param>
		/// <param name="resourcesNamespace">The namespace of the resource to be generated. </param>
		/// <param name="codeProvider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> object that provides the language in which the class will be generated.</param>
		/// <param name="internalClass">true to generate an internal class; false to generate a public class.</param>
		/// <param name="unmatchable">A <see cref="T:System.String" /> array that contains each resource name for which a property cannot be generated. Typically, a property cannot be generated because the resource name is not a valid identifier.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="resourceList" />, <paramref name="basename" />, or <paramref name="codeProvider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">A resource node name does not match its key in <paramref name="resourceList" />.</exception>
		// Token: 0x0600067D RID: 1661 RVA: 0x000099DC File Offset: 0x00007BDC
		public static CodeCompileUnit Create(IDictionary resourceList, string baseName, string generatedCodeNamespace, string resourcesNamespace, CodeDomProvider codeProvider, bool internalClass, out string[] unmatchable)
		{
			if (resourceList == null)
			{
				throw new ArgumentNullException("Parameter resourceList must not be null");
			}
			if (codeProvider == null)
			{
				throw new ArgumentNullException("Parameter: codeProvider must not be null");
			}
			if (baseName == null)
			{
				throw new ArgumentNullException("Parameter: baseName must not be null");
			}
			string text = StronglyTypedResourceBuilder.VerifyResourceName(baseName, codeProvider);
			if (text == null)
			{
				throw new ArgumentException("Parameter: baseName is invalid");
			}
			string text2;
			if (generatedCodeNamespace == null)
			{
				text2 = "";
			}
			else
			{
				text2 = StronglyTypedResourceBuilder.CleanNamespaceChars(generatedCodeNamespace);
				text2 = codeProvider.CreateValidIdentifier(text2);
			}
			string text3;
			if (resourcesNamespace == null)
			{
				text3 = text2 + "." + text;
			}
			else if (resourcesNamespace == string.Empty)
			{
				text3 = text;
			}
			else
			{
				text3 = resourcesNamespace + "." + text;
			}
			Dictionary<string, StronglyTypedResourceBuilder.ResourceItem> dictionary = new Dictionary<string, StronglyTypedResourceBuilder.ResourceItem>(StringComparer.OrdinalIgnoreCase);
			foreach (object obj in resourceList)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				dictionary.Add((string)dictionaryEntry.Key, new StronglyTypedResourceBuilder.ResourceItem(dictionaryEntry.Value));
			}
			StronglyTypedResourceBuilder.ProcessResourceList(dictionary, codeProvider);
			CodeCompileUnit codeCompileUnit = StronglyTypedResourceBuilder.GenerateCodeDOMBase(text, text2, text3, internalClass);
			unmatchable = StronglyTypedResourceBuilder.ResourcePropertyGeneration(codeCompileUnit.Namespaces[0].Types[0], dictionary, internalClass);
			return codeCompileUnit;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00009B20 File Offset: 0x00007D20
		private static string[] ResourcePropertyGeneration(CodeTypeDeclaration resType, Dictionary<string, StronglyTypedResourceBuilder.ResourceItem> resourceItemDict, bool internalClass)
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, StronglyTypedResourceBuilder.ResourceItem> keyValuePair in resourceItemDict)
			{
				if (keyValuePair.Value.isUnmatchable)
				{
					list.Add(keyValuePair.Key);
				}
				else if (!keyValuePair.Value.toIgnore)
				{
					if (keyValuePair.Value.Resource is Stream)
					{
						resType.Members.Add(StronglyTypedResourceBuilder.GenerateStreamResourceProp(keyValuePair.Value.VerifiedKey, keyValuePair.Key, internalClass));
					}
					else if (keyValuePair.Value.Resource is string)
					{
						resType.Members.Add(StronglyTypedResourceBuilder.GenerateStringResourceProp(keyValuePair.Value.VerifiedKey, keyValuePair.Key, internalClass));
					}
					else
					{
						resType.Members.Add(StronglyTypedResourceBuilder.GenerateStandardResourceProp(keyValuePair.Value.VerifiedKey, keyValuePair.Key, keyValuePair.Value.Resource.GetType(), internalClass));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00009C54 File Offset: 0x00007E54
		private static CodeCompileUnit GenerateCodeDOMBase(string baseNameToUse, string generatedCodeNamespaceToUse, string resourcesToUse, bool internalClass)
		{
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.ReferencedAssemblies.Add("System.dll");
			CodeNamespace codeNamespace = new CodeNamespace(generatedCodeNamespaceToUse);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
			CodeTypeDeclaration codeTypeDeclaration = StronglyTypedResourceBuilder.GenerateBaseType(baseNameToUse, internalClass);
			codeNamespace.Types.Add(codeTypeDeclaration);
			StronglyTypedResourceBuilder.GenerateFields(codeTypeDeclaration);
			codeTypeDeclaration.Members.Add(StronglyTypedResourceBuilder.GenerateConstructor());
			codeTypeDeclaration.Members.Add(StronglyTypedResourceBuilder.GenerateResourceManagerProp(baseNameToUse, resourcesToUse, internalClass));
			codeTypeDeclaration.Members.Add(StronglyTypedResourceBuilder.GenerateCultureProp(internalClass));
			return codeCompileUnit;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private static void ProcessResourceList(Dictionary<string, StronglyTypedResourceBuilder.ResourceItem> resourceItemDict, CodeDomProvider codeProvider)
		{
			foreach (KeyValuePair<string, StronglyTypedResourceBuilder.ResourceItem> keyValuePair in resourceItemDict)
			{
				if (keyValuePair.Key.StartsWith(">>") || keyValuePair.Key.StartsWith("$"))
				{
					keyValuePair.Value.toIgnore = true;
				}
				else if (keyValuePair.Key == "ResourceManager" || keyValuePair.Key == "Culture")
				{
					keyValuePair.Value.isUnmatchable = true;
				}
				else
				{
					keyValuePair.Value.VerifiedKey = StronglyTypedResourceBuilder.VerifyResourceName(keyValuePair.Key, codeProvider);
					if (keyValuePair.Value.VerifiedKey == null)
					{
						keyValuePair.Value.isUnmatchable = true;
					}
					else
					{
						foreach (KeyValuePair<string, StronglyTypedResourceBuilder.ResourceItem> keyValuePair2 in resourceItemDict)
						{
							if (keyValuePair2.Value != keyValuePair.Value && keyValuePair2.Value.VerifiedKey != null && string.Equals(keyValuePair2.Value.VerifiedKey, keyValuePair.Value.VerifiedKey, StringComparison.OrdinalIgnoreCase))
							{
								keyValuePair2.Value.isUnmatchable = true;
								keyValuePair.Value.isUnmatchable = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00009E90 File Offset: 0x00008090
		private static CodeTypeDeclaration GenerateBaseType(string baseNameToUse, bool internalClass)
		{
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(baseNameToUse);
			codeTypeDeclaration.IsClass = true;
			if (internalClass)
			{
				codeTypeDeclaration.TypeAttributes = TypeAttributes.NotPublic;
			}
			else
			{
				codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;
			}
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.CodeDom.Compiler.GeneratedCodeAttribute", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression("System.Resources.Tools.StronglyTypedResourceBuilder")),
				new CodeAttributeArgument(new CodePrimitiveExpression("4.0.0.0"))
			}));
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.Diagnostics.DebuggerNonUserCodeAttribute"));
			codeTypeDeclaration.CustomAttributes.Add(new CodeAttributeDeclaration("System.Runtime.CompilerServices.CompilerGeneratedAttribute"));
			return codeTypeDeclaration;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00009F2C File Offset: 0x0000812C
		private static void GenerateFields(CodeTypeDeclaration resType)
		{
			CodeMemberField codeMemberField = new CodeMemberField();
			codeMemberField.Attributes = (MemberAttributes)20483;
			codeMemberField.Name = "resourceMan";
			codeMemberField.Type = new CodeTypeReference(typeof(ResourceManager));
			resType.Members.Add(codeMemberField);
			CodeMemberField codeMemberField2 = new CodeMemberField();
			codeMemberField2.Attributes = (MemberAttributes)20483;
			codeMemberField2.Name = "resourceCulture";
			codeMemberField2.Type = new CodeTypeReference(typeof(CultureInfo));
			resType.Members.Add(codeMemberField2);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00009FB8 File Offset: 0x000081B8
		private static CodeConstructor GenerateConstructor()
		{
			return new CodeConstructor
			{
				Attributes = MemberAttributes.FamilyAndAssembly,
				CustomAttributes = 
				{
					new CodeAttributeDeclaration("System.Diagnostics.CodeAnalysis.SuppressMessageAttribute", new CodeAttributeArgument[]
					{
						new CodeAttributeArgument(new CodePrimitiveExpression("Microsoft.Performance")),
						new CodeAttributeArgument(new CodePrimitiveExpression("CA1811:AvoidUncalledPrivateCode"))
					})
				}
			};
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000A017 File Offset: 0x00008217
		private static CodeAttributeDeclaration DefaultPropertyAttribute()
		{
			return new CodeAttributeDeclaration("System.ComponentModel.EditorBrowsableAttribute", new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression("System.ComponentModel.EditorBrowsableState"), "Advanced"))
			});
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000A048 File Offset: 0x00008248
		private static CodeMemberProperty GenerateCultureProp(bool internalClass)
		{
			CodeMemberProperty codeMemberProperty = StronglyTypedResourceBuilder.GeneratePropertyBase("Culture", typeof(CultureInfo), internalClass, true, true);
			codeMemberProperty.CustomAttributes.Add(StronglyTypedResourceBuilder.DefaultPropertyAttribute());
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, "resourceCulture")));
			codeMemberProperty.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(null, "resourceCulture"), new CodePropertySetValueReferenceExpression()));
			return codeMemberProperty;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000A0BC File Offset: 0x000082BC
		private static CodeMemberProperty GenerateResourceManagerProp(string baseNameToUse, string resourcesToUse, bool internalClass)
		{
			CodeMemberProperty codeMemberProperty = StronglyTypedResourceBuilder.GeneratePropertyBase("ResourceManager", typeof(ResourceManager), internalClass, true, false);
			codeMemberProperty.CustomAttributes.Add(StronglyTypedResourceBuilder.DefaultPropertyAttribute());
			CodeStatement[] array = new CodeStatement[]
			{
				new CodeVariableDeclarationStatement(new CodeTypeReference("System.Resources.ResourceManager"), "temp", new CodeObjectCreateExpression(new CodeTypeReference("System.Resources.ResourceManager"), new CodeExpression[]
				{
					new CodePrimitiveExpression(resourcesToUse),
					new CodePropertyReferenceExpression(new CodeTypeOfExpression(baseNameToUse), "Assembly")
				})),
				new CodeAssignStatement(new CodeFieldReferenceExpression(null, "resourceMan"), new CodeVariableReferenceExpression("temp"))
			};
			codeMemberProperty.GetStatements.Add(new CodeConditionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression("System.Object"), "Equals"), new CodeExpression[]
			{
				new CodePrimitiveExpression(null),
				new CodeFieldReferenceExpression(null, "resourceMan")
			}), array));
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(null, "resourceMan")));
			return codeMemberProperty;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000A1C4 File Offset: 0x000083C4
		private static CodeMemberProperty GenerateStandardResourceProp(string propName, string resName, Type propertyType, bool isInternal)
		{
			CodeMemberProperty codeMemberProperty = StronglyTypedResourceBuilder.GeneratePropertyBase(propName, propertyType, isInternal, true, false);
			codeMemberProperty.GetStatements.Add(new CodeVariableDeclarationStatement(new CodeTypeReference("System.Object"), "obj", new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(null, "ResourceManager"), "GetObject", new CodeExpression[]
			{
				new CodePrimitiveExpression(resName),
				new CodeFieldReferenceExpression(null, "resourceCulture")
			})));
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(new CodeTypeReference(propertyType), new CodeVariableReferenceExpression("obj"))));
			return codeMemberProperty;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000A258 File Offset: 0x00008458
		private static CodeMemberProperty GenerateStringResourceProp(string propName, string resName, bool isInternal)
		{
			CodeMemberProperty codeMemberProperty = StronglyTypedResourceBuilder.GeneratePropertyBase(propName, typeof(string), isInternal, true, false);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(null, "ResourceManager"), "GetString"), new CodeExpression[]
			{
				new CodePrimitiveExpression(resName),
				new CodeFieldReferenceExpression(null, "resourceCulture")
			})));
			return codeMemberProperty;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000A2C4 File Offset: 0x000084C4
		private static CodeMemberProperty GenerateStreamResourceProp(string propName, string resName, bool isInternal)
		{
			CodeMemberProperty codeMemberProperty = StronglyTypedResourceBuilder.GeneratePropertyBase(propName, typeof(UnmanagedMemoryStream), isInternal, true, false);
			codeMemberProperty.GetStatements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(null, "ResourceManager"), "GetStream"), new CodeExpression[]
			{
				new CodePrimitiveExpression(resName),
				new CodeFieldReferenceExpression(null, "resourceCulture")
			})));
			return codeMemberProperty;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000A330 File Offset: 0x00008530
		private static CodeMemberProperty GeneratePropertyBase(string name, Type propertyType, bool isInternal, bool hasGet, bool hasSet)
		{
			CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
			codeMemberProperty.Name = name;
			codeMemberProperty.Type = new CodeTypeReference(propertyType);
			if (isInternal)
			{
				codeMemberProperty.Attributes = (MemberAttributes)4099;
			}
			else
			{
				codeMemberProperty.Attributes = (MemberAttributes)24579;
			}
			codeMemberProperty.HasGet = hasGet;
			codeMemberProperty.HasSet = hasSet;
			return codeMemberProperty;
		}

		/// <summary>Generates a valid resource string based on the specified input string and code provider.</summary>
		/// <returns>A valid resource name derived from the <paramref name="key" /> parameter. Any invalid tokens are replaced with the underscore (_) character, or null if the derived string still contains invalid characters according to the language specified by the <paramref name="provider" /> parameter.</returns>
		/// <param name="key">The string to verify and, if necessary, convert to a valid resource name.</param>
		/// <param name="provider">A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> object that specifies the target language to use.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> or <paramref name="provider" /> is null.</exception>
		// Token: 0x0600068B RID: 1675 RVA: 0x0000A384 File Offset: 0x00008584
		public static string VerifyResourceName(string key, CodeDomProvider provider)
		{
			if (key == null)
			{
				throw new ArgumentNullException("Parameter: key must not be null");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("Parameter: provider must not be null");
			}
			string text;
			if (key == string.Empty)
			{
				text = "_";
			}
			else
			{
				char[] array = key.ToCharArray();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = StronglyTypedResourceBuilder.VerifySpecialChar(array[i]);
				}
				text = new string(array);
			}
			text = provider.CreateValidIdentifier(text);
			if (provider.IsValidIdentifier(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000A400 File Offset: 0x00008600
		private static char VerifySpecialChar(char ch)
		{
			for (int i = 0; i < StronglyTypedResourceBuilder.specialChars.Length; i++)
			{
				if (StronglyTypedResourceBuilder.specialChars[i] == ch)
				{
					return '_';
				}
			}
			return ch;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000A430 File Offset: 0x00008630
		private static string CleanNamespaceChars(string name)
		{
			char[] array = name.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (char c in StronglyTypedResourceBuilder.specialCharsNameSpace)
				{
					if (array[i] == c)
					{
						array[i] = '_';
					}
				}
			}
			return new string(array);
		}

		// Token: 0x04000155 RID: 341
		private static char[] specialChars = new char[]
		{
			' ', '\u00a0', '.', ',', ';', '|', '~', '@', '#', '%',
			'^', '&', '*', '+', '-', '/', '\\', '<', '>', '?',
			'[', ']', '(', ')', '{', '}', '"', '\'', ':', '!'
		};

		// Token: 0x04000156 RID: 342
		private static char[] specialCharsNameSpace = new char[]
		{
			' ', '\u00a0', ',', ';', '|', '~', '@', '#', '%', '^',
			'&', '*', '+', '-', '/', '\\', '<', '>', '?', '[',
			']', '(', ')', '{', '}', '"', '\'', '!'
		};

		// Token: 0x020000E2 RID: 226
		private class ResourceItem
		{
			// Token: 0x1700018E RID: 398
			// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000A4AD File Offset: 0x000086AD
			// (set) Token: 0x06000690 RID: 1680 RVA: 0x0000A4B5 File Offset: 0x000086B5
			public string VerifiedKey { get; set; }

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x06000691 RID: 1681 RVA: 0x0000A4BE File Offset: 0x000086BE
			// (set) Token: 0x06000692 RID: 1682 RVA: 0x0000A4C6 File Offset: 0x000086C6
			public object Resource { get; set; }

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000A4CF File Offset: 0x000086CF
			// (set) Token: 0x06000694 RID: 1684 RVA: 0x0000A4D7 File Offset: 0x000086D7
			public bool isUnmatchable { get; set; }

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000A4E0 File Offset: 0x000086E0
			// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000A4E8 File Offset: 0x000086E8
			public bool toIgnore { get; set; }

			// Token: 0x06000697 RID: 1687 RVA: 0x0000A4F1 File Offset: 0x000086F1
			public ResourceItem(object value)
			{
				this.Resource = value;
			}
		}
	}
}
