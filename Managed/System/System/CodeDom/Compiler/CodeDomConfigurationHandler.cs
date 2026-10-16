using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035A RID: 858
	internal sealed class CodeDomConfigurationHandler : ConfigurationSection
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00066CF0 File Offset: 0x00064EF0
		static CodeDomConfigurationHandler()
		{
			CodeDomConfigurationHandler.properties.Add(CodeDomConfigurationHandler.compilersProp);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00066D3E File Offset: 0x00064F3E
		protected override void InitializeDefault()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00066D5E File Offset: 0x00064F5E
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000075E1 File Offset: 0x000057E1
		protected override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00066D66 File Offset: 0x00064F66
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00066D78 File Offset: 0x00064F78
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				CompilerCollection compilerCollection = (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
				if (compilerCollection == null)
				{
					return null;
				}
				return compilerCollection.CompilerInfos;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00066D95 File Offset: 0x00064F95
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeDomConfigurationHandler.properties;
			}
		}

		// Token: 0x04000E97 RID: 3735
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000E98 RID: 3736
		private static ConfigurationProperty compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);

		// Token: 0x04000E99 RID: 3737
		private static CompilerCollection default_compilers = new CompilerCollection();
	}
}
