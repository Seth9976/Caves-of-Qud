using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035C RID: 860
	[ConfigurationCollection(typeof(Compiler), AddItemName = "compiler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal sealed class CompilerCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C6B RID: 7275 RVA: 0x00066FE0 File Offset: 0x000651E0
		static CompilerCollection()
		{
			CompilerInfo compilerInfo = new CompilerInfo(null, "Microsoft.CSharp.CSharpCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", new string[] { "c#", "cs", "csharp" }, new string[] { ".cs" });
			compilerInfo.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo);
			CompilerInfo compilerInfo2 = new CompilerInfo(null, "Microsoft.VisualBasic.VBCodeProvider, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", new string[] { "vb", "vbs", "visualbasic", "vbscript" }, new string[] { ".vb" });
			compilerInfo2.ProviderOptions["CompilerVersion"] = CompilerCollection.defaultCompilerVersion;
			CompilerCollection.AddCompilerInfo(compilerInfo2);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000670D8 File Offset: 0x000652D8
		private static void AddCompilerInfo(CompilerInfo ci)
		{
			ci.CreateProvider();
			CompilerCollection.compiler_infos.Add(ci);
			string[] languages = ci.GetLanguages();
			if (languages != null)
			{
				foreach (string text in languages)
				{
					CompilerCollection.compiler_languages[text] = ci;
				}
			}
			string[] extensions = ci.GetExtensions();
			if (extensions != null)
			{
				foreach (string text2 in extensions)
				{
					CompilerCollection.compiler_extensions[text2] = ci;
				}
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00067150 File Offset: 0x00065350
		private static void AddCompilerInfo(Compiler compiler)
		{
			CompilerCollection.AddCompilerInfo(new CompilerInfo(null, compiler.Type, new string[] { compiler.Extension }, new string[] { compiler.Language })
			{
				CompilerParams = 
				{
					CompilerOptions = compiler.CompilerOptions,
					WarningLevel = compiler.WarningLevel
				}
			});
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000671B0 File Offset: 0x000653B0
		protected override void BaseAdd(ConfigurationElement element)
		{
			Compiler compiler = element as Compiler;
			if (compiler != null)
			{
				CompilerCollection.AddCompilerInfo(compiler);
			}
			base.BaseAdd(element);
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00003062 File Offset: 0x00001262
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000671D4 File Offset: 0x000653D4
		protected override ConfigurationElement CreateNewElement()
		{
			return new Compiler();
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000671DC File Offset: 0x000653DC
		public CompilerInfo GetCompilerInfoForLanguage(string language)
		{
			if (CompilerCollection.compiler_languages.Count == 0)
			{
				return null;
			}
			CompilerInfo compilerInfo;
			if (CompilerCollection.compiler_languages.TryGetValue(language, out compilerInfo))
			{
				return compilerInfo;
			}
			return null;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0006720C File Offset: 0x0006540C
		public CompilerInfo GetCompilerInfoForExtension(string extension)
		{
			if (CompilerCollection.compiler_extensions.Count == 0)
			{
				return null;
			}
			CompilerInfo compilerInfo;
			if (CompilerCollection.compiler_extensions.TryGetValue(extension, out compilerInfo))
			{
				return compilerInfo;
			}
			return null;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0006723C File Offset: 0x0006543C
		public string GetLanguageFromExtension(string extension)
		{
			CompilerInfo compilerInfoForExtension = this.GetCompilerInfoForExtension(extension);
			if (compilerInfoForExtension == null)
			{
				return null;
			}
			string[] languages = compilerInfoForExtension.GetLanguages();
			if (languages != null && languages.Length != 0)
			{
				return languages[0];
			}
			return null;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00067269 File Offset: 0x00065469
		public Compiler Get(int index)
		{
			return (Compiler)base.BaseGet(index);
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00067277 File Offset: 0x00065477
		public Compiler Get(string language)
		{
			return (Compiler)base.BaseGet(language);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00067285 File Offset: 0x00065485
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((Compiler)element).Language;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x00067292 File Offset: 0x00065492
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x000672A0 File Offset: 0x000654A0
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[CompilerCollection.compiler_infos.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = string.Join(";", CompilerCollection.compiler_infos[i].GetLanguages());
				}
				return array;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000672EC File Offset: 0x000654EC
		protected override string ElementName
		{
			get
			{
				return "compiler";
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000672F3 File Offset: 0x000654F3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerCollection.properties;
			}
		}

		// Token: 0x170005BA RID: 1466
		public Compiler this[int index]
		{
			get
			{
				return (Compiler)base.BaseGet(index);
			}
		}

		// Token: 0x170005BB RID: 1467
		public CompilerInfo this[string language]
		{
			get
			{
				return this.GetCompilerInfoForLanguage(language);
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x00067303 File Offset: 0x00065503
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				return CompilerCollection.compiler_infos.ToArray();
			}
		}

		// Token: 0x04000EA1 RID: 3745
		private static readonly string defaultCompilerVersion = "4.0";

		// Token: 0x04000EA2 RID: 3746
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000EA3 RID: 3747
		private static List<CompilerInfo> compiler_infos = new List<CompilerInfo>();

		// Token: 0x04000EA4 RID: 3748
		private static Dictionary<string, CompilerInfo> compiler_languages = new Dictionary<string, CompilerInfo>(16, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000EA5 RID: 3749
		private static Dictionary<string, CompilerInfo> compiler_extensions = new Dictionary<string, CompilerInfo>(4, StringComparer.OrdinalIgnoreCase);
	}
}
