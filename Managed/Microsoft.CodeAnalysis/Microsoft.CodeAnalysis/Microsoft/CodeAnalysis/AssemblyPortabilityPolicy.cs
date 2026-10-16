using System;
using System.IO;
using System.Xml;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000163 RID: 355
	internal struct AssemblyPortabilityPolicy : IEquatable<AssemblyPortabilityPolicy>
	{
		// Token: 0x06000E4F RID: 3663 RVA: 0x00029528 File Offset: 0x00027728
		public AssemblyPortabilityPolicy(bool suppressSilverlightPlatformAssembliesPortability, bool suppressSilverlightLibraryAssembliesPortability)
		{
			this.SuppressSilverlightLibraryAssembliesPortability = suppressSilverlightLibraryAssembliesPortability;
			this.SuppressSilverlightPlatformAssembliesPortability = suppressSilverlightPlatformAssembliesPortability;
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00029538 File Offset: 0x00027738
		public override bool Equals(object obj)
		{
			return obj is AssemblyPortabilityPolicy && this.Equals((AssemblyPortabilityPolicy)obj);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00029550 File Offset: 0x00027750
		public bool Equals(AssemblyPortabilityPolicy other)
		{
			return this.SuppressSilverlightLibraryAssembliesPortability == other.SuppressSilverlightLibraryAssembliesPortability && this.SuppressSilverlightPlatformAssembliesPortability == other.SuppressSilverlightPlatformAssembliesPortability;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00029570 File Offset: 0x00027770
		public override int GetHashCode()
		{
			return (this.SuppressSilverlightLibraryAssembliesPortability ? 1 : 0) | (this.SuppressSilverlightPlatformAssembliesPortability ? 2 : 0);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0002958B File Offset: 0x0002778B
		private static bool ReadToChild(XmlReader reader, int depth, string elementName, string elementNamespace = "")
		{
			return reader.ReadToDescendant(elementName, elementNamespace) && reader.Depth == depth;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000295A4 File Offset: 0x000277A4
		internal static AssemblyPortabilityPolicy LoadFromXml(Stream input)
		{
			AssemblyPortabilityPolicy assemblyPortabilityPolicy;
			using (XmlReader xmlReader = XmlReader.Create(input, AssemblyPortabilityPolicy.s_xmlSettings))
			{
				if (!AssemblyPortabilityPolicy.ReadToChild(xmlReader, 0, "configuration", "") || !AssemblyPortabilityPolicy.ReadToChild(xmlReader, 1, "runtime", "") || !AssemblyPortabilityPolicy.ReadToChild(xmlReader, 2, "assemblyBinding", "urn:schemas-microsoft-com:asm.v1") || !AssemblyPortabilityPolicy.ReadToChild(xmlReader, 3, "supportPortability", "urn:schemas-microsoft-com:asm.v1"))
				{
					assemblyPortabilityPolicy = default(AssemblyPortabilityPolicy);
					assemblyPortabilityPolicy = assemblyPortabilityPolicy;
				}
				else
				{
					bool flag = false;
					bool flag2 = false;
					do
					{
						string attribute = xmlReader.GetAttribute("PKT");
						string attribute2 = xmlReader.GetAttribute("enable");
						bool? flag3 = (string.Equals(attribute2, "false", 5) ? new bool?(false) : (string.Equals(attribute2, "true", 5) ? new bool?(true) : default(bool?)));
						if (flag3 != null)
						{
							if (string.Equals(attribute, "31bf3856ad364e35", 5))
							{
								flag = !flag3.Value;
							}
							else if (string.Equals(attribute, "7cec85d7bea7798e", 5))
							{
								flag2 = !flag3.Value;
							}
						}
					}
					while (xmlReader.ReadToNextSibling("supportPortability", "urn:schemas-microsoft-com:asm.v1"));
					assemblyPortabilityPolicy = new AssemblyPortabilityPolicy(flag2, flag);
				}
			}
			return assemblyPortabilityPolicy;
		}

		// Token: 0x040003F9 RID: 1017
		public readonly bool SuppressSilverlightPlatformAssembliesPortability;

		// Token: 0x040003FA RID: 1018
		public readonly bool SuppressSilverlightLibraryAssembliesPortability;

		// Token: 0x040003FB RID: 1019
		private static readonly XmlReaderSettings s_xmlSettings = new XmlReaderSettings
		{
			DtdProcessing = 0
		};
	}
}
