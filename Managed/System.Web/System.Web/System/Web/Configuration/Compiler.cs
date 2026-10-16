using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines a compiler that is used to support the compilation infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x0200058F RID: 1423
	public sealed class Compiler : ConfigurationElement
	{
		// Token: 0x06003C38 RID: 15416 RVA: 0x0009E81C File Offset: 0x0009CA1C
		static Compiler()
		{
			Compiler.properties.Add(Compiler.compilerOptionsProp);
			Compiler.properties.Add(Compiler.extensionProp);
			Compiler.properties.Add(Compiler.languageProp);
			Compiler.properties.Add(Compiler.typeProp);
			Compiler.properties.Add(Compiler.warningLevelProp);
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal Compiler()
		{
		}

		/// <summary>Creates an instance of a <see cref="T:System.Web.Configuration.Compiler" /> initialized to the provided values.</summary>
		/// <param name="compilerOptions">Lists additional compiler-specific options to pass during compilation.</param>
		/// <param name="extension">Provides a semicolon-separated list of file-name extensions used for dynamic code-behind files. For example, ".cs".</param>
		/// <param name="language">Provides a semicolon-separated list of languages used in dynamic compilation files. For example, "c#;cs;csharp".</param>
		/// <param name="type">Specifies a comma-separated class/assembly combination that indicates the .NET Framework class.</param>
		/// <param name="warningLevel">Specifies compiler warning levels.</param>
		// Token: 0x06003C3A RID: 15418 RVA: 0x0009E92E File Offset: 0x0009CB2E
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
		{
			this.CompilerOptions = compilerOptions;
			this.Extension = extension;
			this.Language = language;
			this.Type = type;
			this.WarningLevel = warningLevel;
		}

		/// <summary>Gets a list of compiler-specific options to use during compilation.</summary>
		/// <returns>A value specifying the compiler-specific options to use during compilation. This is not a merged set but rather overrides any previously defined values in earlier configuration entries.</returns>
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06003C3B RID: 15419 RVA: 0x0009E95B File Offset: 0x0009CB5B
		// (set) Token: 0x06003C3C RID: 15420 RVA: 0x0009E96D File Offset: 0x0009CB6D
		[ConfigurationProperty("compilerOptions", DefaultValue = "")]
		public string CompilerOptions
		{
			get
			{
				return (string)base[Compiler.compilerOptionsProp];
			}
			internal set
			{
				base[Compiler.compilerOptionsProp] = value;
			}
		}

		/// <summary>Gets a list of file-name extensions used for dynamic code-behind files. </summary>
		/// <returns>A value specifying the file-name extensions used for dynamic code-behind files, files in the code directory, and other referenced files.</returns>
		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x0009E97B File Offset: 0x0009CB7B
		// (set) Token: 0x06003C3E RID: 15422 RVA: 0x0009E98D File Offset: 0x0009CB8D
		[ConfigurationProperty("extension", DefaultValue = "")]
		public string Extension
		{
			get
			{
				return (string)base[Compiler.extensionProp];
			}
			internal set
			{
				base[Compiler.extensionProp] = value;
			}
		}

		/// <summary>Gets a list of programming languages to use in dynamic compilation files.</summary>
		/// <returns>A value specifying the programming languages to use in dynamic compilation files.</returns>
		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x0009E99B File Offset: 0x0009CB9B
		// (set) Token: 0x06003C40 RID: 15424 RVA: 0x0009E9AD File Offset: 0x0009CBAD
		[ConfigurationProperty("language", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Language
		{
			get
			{
				return (string)base[Compiler.languageProp];
			}
			internal set
			{
				base[Compiler.languageProp] = value;
			}
		}

		/// <summary>Gets the compiler type name of the language provider for dynamic compilation files. </summary>
		/// <returns>A value specifying the type name of the language compiler to use in dynamic compilation files.</returns>
		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x0009E9BB File Offset: 0x0009CBBB
		// (set) Token: 0x06003C42 RID: 15426 RVA: 0x0009E9CD File Offset: 0x0009CBCD
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[Compiler.typeProp];
			}
			internal set
			{
				base[Compiler.typeProp] = value;
			}
		}

		/// <summary>Gets the compiler warning level.</summary>
		/// <returns>A value specifying the compiler warning level.</returns>
		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x0009E9DB File Offset: 0x0009CBDB
		// (set) Token: 0x06003C44 RID: 15428 RVA: 0x0009E9ED File Offset: 0x0009CBED
		[ConfigurationProperty("warningLevel", DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		public int WarningLevel
		{
			get
			{
				return (int)base[Compiler.warningLevelProp];
			}
			internal set
			{
				base[Compiler.warningLevelProp] = value;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x0009EA00 File Offset: 0x0009CC00
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler.properties;
			}
		}

		// Token: 0x040020C0 RID: 8384
		private static ConfigurationProperty compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), "");

		// Token: 0x040020C1 RID: 8385
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), "");

		// Token: 0x040020C2 RID: 8386
		private static ConfigurationProperty languageProp = new ConfigurationProperty("language", typeof(string), "", ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020C3 RID: 8387
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020C4 RID: 8388
		private static ConfigurationProperty warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x040020C5 RID: 8389
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
