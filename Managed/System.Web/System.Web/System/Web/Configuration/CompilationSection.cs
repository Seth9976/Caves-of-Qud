using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the compilation infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x0200058E RID: 1422
	public sealed class CompilationSection : ConfigurationSection
	{
		// Token: 0x06003C07 RID: 15367 RVA: 0x0009E1B8 File Offset: 0x0009C3B8
		static CompilationSection()
		{
			CompilationSection.properties.Add(CompilationSection.assembliesProp);
			CompilationSection.properties.Add(CompilationSection.assemblyPostProcessorTypeProp);
			CompilationSection.properties.Add(CompilationSection.batchProp);
			CompilationSection.properties.Add(CompilationSection.buildProvidersProp);
			CompilationSection.properties.Add(CompilationSection.batchTimeoutProp);
			CompilationSection.properties.Add(CompilationSection.codeSubDirectoriesProp);
			CompilationSection.properties.Add(CompilationSection.compilersProp);
			CompilationSection.properties.Add(CompilationSection.debugProp);
			CompilationSection.properties.Add(CompilationSection.defaultLanguageProp);
			CompilationSection.properties.Add(CompilationSection.expressionBuildersProp);
			CompilationSection.properties.Add(CompilationSection.explicitProp);
			CompilationSection.properties.Add(CompilationSection.maxBatchSizeProp);
			CompilationSection.properties.Add(CompilationSection.maxBatchGeneratedFileSizeProp);
			CompilationSection.properties.Add(CompilationSection.numRecompilesBeforeAppRestartProp);
			CompilationSection.properties.Add(CompilationSection.strictProp);
			CompilationSection.properties.Add(CompilationSection.tempDirectoryProp);
			CompilationSection.properties.Add(CompilationSection.urlLinePragmasProp);
			CompilationSection.properties.Add(CompilationSection.optimizeCompilationsProp);
			CompilationSection.properties.Add(CompilationSection.targetFrameworkProp);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0009D4C1 File Offset: 0x0009B6C1
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x00002058 File Offset: 0x00000258
		[global::System.MonoTODO("why override this?")]
		protected internal override object GetRuntimeObject()
		{
			return this;
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.AssemblyCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.AssemblyCollection" /> that contains the assembly objects used during compilation of an ASP.NET resource.</returns>
		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x0009E557 File Offset: 0x0009C757
		[ConfigurationProperty("assemblies")]
		public AssemblyCollection Assemblies
		{
			get
			{
				return (AssemblyCollection)base[CompilationSection.assembliesProp];
			}
		}

		/// <summary>Gets or sets a value specifying a post-process compilation step for an assembly.</summary>
		/// <returns>A string value specifying the post-process compilation step for an assembly.</returns>
		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x0009E569 File Offset: 0x0009C769
		// (set) Token: 0x06003C0D RID: 15373 RVA: 0x0009E57B File Offset: 0x0009C77B
		[ConfigurationProperty("assemblyPostProcessorType", DefaultValue = "")]
		public string AssemblyPostProcessorType
		{
			get
			{
				return (string)base[CompilationSection.assemblyPostProcessorTypeProp];
			}
			set
			{
				base[CompilationSection.assemblyPostProcessorTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether batch compilation is attempted.</summary>
		/// <returns>true if batch compilation is attempted; otherwise, false. The default is true.</returns>
		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003C0E RID: 15374 RVA: 0x0009E589 File Offset: 0x0009C789
		// (set) Token: 0x06003C0F RID: 15375 RVA: 0x0009E59B File Offset: 0x0009C79B
		[ConfigurationProperty("batch", DefaultValue = "True")]
		public bool Batch
		{
			get
			{
				return (bool)base[CompilationSection.batchProp];
			}
			set
			{
				base[CompilationSection.batchProp] = value;
			}
		}

		/// <summary>Gets or sets the time-out period, in seconds, for batch compilation.</summary>
		/// <returns>A value indicating the amount of time in seconds granted for batch compilation to occur.  </returns>
		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003C10 RID: 15376 RVA: 0x0009E5AE File Offset: 0x0009C7AE
		// (set) Token: 0x06003C11 RID: 15377 RVA: 0x0009E5C0 File Offset: 0x0009C7C0
		[ConfigurationProperty("batchTimeout", DefaultValue = "00:15:00")]
		[TimeSpanValidator(MinValueString = "00:00:00")]
		[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
		public TimeSpan BatchTimeout
		{
			get
			{
				return (TimeSpan)base[CompilationSection.batchTimeoutProp];
			}
			set
			{
				base[CompilationSection.batchTimeoutProp] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProviderCollection" />  collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BuildProviderCollection" /> that contains the build providers used during a compilation.</returns>
		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003C12 RID: 15378 RVA: 0x0009E5D3 File Offset: 0x0009C7D3
		[ConfigurationProperty("buildProviders")]
		public BuildProviderCollection BuildProviders
		{
			get
			{
				return (BuildProviderCollection)base[CompilationSection.buildProvidersProp];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CodeSubDirectoriesCollection" /> collection that contains an ordered collection of subdirectories containing files compiled at run time.</returns>
		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x0009E5E5 File Offset: 0x0009C7E5
		[ConfigurationProperty("codeSubDirectories")]
		public CodeSubDirectoriesCollection CodeSubDirectories
		{
			get
			{
				return (CodeSubDirectoriesCollection)base[CompilationSection.codeSubDirectoriesProp];
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.CompilerCollection" /> collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CompilerCollection" /> collection that contains a collection of <see cref="T:System.Web.Configuration.Compiler" /> objects.</returns>
		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003C14 RID: 15380 RVA: 0x0009E5F7 File Offset: 0x0009C7F7
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CompilationSection.compilersProp];
			}
		}

		/// <summary>Gets or sets a value specifying whether to compile release binaries or debug binaries. </summary>
		/// <returns>true if debug binaries will be used for compilation; otherwise, false. false specifies that release binaries will be used for compilation. The default is false.</returns>
		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x0009E609 File Offset: 0x0009C809
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x0009E61B File Offset: 0x0009C81B
		[ConfigurationProperty("debug", DefaultValue = "False")]
		public bool Debug
		{
			get
			{
				return (bool)base[CompilationSection.debugProp];
			}
			set
			{
				base[CompilationSection.debugProp] = value;
			}
		}

		/// <summary>Gets or sets the default programming language to use in dynamic-compilation files.</summary>
		/// <returns>A value specifying the default programming language to use in dynamic-compilation files.</returns>
		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x0009E62E File Offset: 0x0009C82E
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x0009E640 File Offset: 0x0009C840
		[ConfigurationProperty("defaultLanguage", DefaultValue = "vb")]
		public string DefaultLanguage
		{
			get
			{
				return (string)base[CompilationSection.defaultLanguageProp];
			}
			set
			{
				base[CompilationSection.defaultLanguageProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to use the Microsoft Visual Basic explicit compile option.</summary>
		/// <returns>true if the Visual Basic explicit compile option is enabled; otherwise, false. false specifies that the Visual Basic explicit compile option is disabled. The default is true.</returns>
		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x0009E64E File Offset: 0x0009C84E
		// (set) Token: 0x06003C1A RID: 15386 RVA: 0x0009E660 File Offset: 0x0009C860
		[ConfigurationProperty("explicit", DefaultValue = "True")]
		public bool Explicit
		{
			get
			{
				return (bool)base[CompilationSection.explicitProp];
			}
			set
			{
				base[CompilationSection.explicitProp] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> of the <see cref="T:System.Web.Configuration.CompilationSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ExpressionBuilderCollection" /> that contains <see cref="T:System.Web.Configuration.ExpressionBuilder" /> objects.</returns>
		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x0009E673 File Offset: 0x0009C873
		[ConfigurationProperty("expressionBuilders")]
		public ExpressionBuilderCollection ExpressionBuilders
		{
			get
			{
				return (ExpressionBuilderCollection)base[CompilationSection.expressionBuildersProp];
			}
		}

		/// <summary>Gets or sets the maximum combined size of the generated source files per batched compilation.</summary>
		/// <returns>An integer value indicating the maximum combined size of the generated source files per batched compilation.</returns>
		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x0009E685 File Offset: 0x0009C885
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x0009E697 File Offset: 0x0009C897
		[ConfigurationProperty("maxBatchGeneratedFileSize", DefaultValue = "1000")]
		public int MaxBatchGeneratedFileSize
		{
			get
			{
				return (int)base[CompilationSection.maxBatchGeneratedFileSizeProp];
			}
			set
			{
				base[CompilationSection.maxBatchGeneratedFileSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of pages per batched compilation.</summary>
		/// <returns>An integer value indicating the maximum number of pages that will be compiled into a single batch. The default number of pages is 1000.</returns>
		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x0009E6AA File Offset: 0x0009C8AA
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x0009E6BC File Offset: 0x0009C8BC
		[ConfigurationProperty("maxBatchSize", DefaultValue = "1000")]
		public int MaxBatchSize
		{
			get
			{
				return (int)base[CompilationSection.maxBatchSizeProp];
			}
			set
			{
				base[CompilationSection.maxBatchSizeProp] = value;
			}
		}

		/// <summary>Gets or sets the number of dynamic recompiles of resources that can occur before the application restarts.</summary>
		/// <returns>A value indicating the number of dynamic recompiles of resources that can occur before the application restarts. The default is 15 recompilations.</returns>
		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x0009E6CF File Offset: 0x0009C8CF
		// (set) Token: 0x06003C21 RID: 15393 RVA: 0x0009E6E1 File Offset: 0x0009C8E1
		[ConfigurationProperty("numRecompilesBeforeAppRestart", DefaultValue = "15")]
		public int NumRecompilesBeforeAppRestart
		{
			get
			{
				return (int)base[CompilationSection.numRecompilesBeforeAppRestartProp];
			}
			set
			{
				base[CompilationSection.numRecompilesBeforeAppRestartProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the compilation must be optimized.</summary>
		/// <returns>true if the compilation must be optimized; otherwise, false. The default is false. </returns>
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x0009E6F4 File Offset: 0x0009C8F4
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x0009E706 File Offset: 0x0009C906
		[ConfigurationProperty("optimizeCompilations", DefaultValue = "False")]
		public bool OptimizeCompilations
		{
			get
			{
				return (bool)base[CompilationSection.optimizeCompilationsProp];
			}
			set
			{
				base[CompilationSection.optimizeCompilationsProp] = value;
			}
		}

		/// <summary>Gets or sets the Visual Basic strict compile option.</summary>
		/// <returns>true if the Visual Basic strict compile option is used; otherwise, false. The default is true. </returns>
		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x0009E719 File Offset: 0x0009C919
		// (set) Token: 0x06003C25 RID: 15397 RVA: 0x0009E72B File Offset: 0x0009C92B
		[ConfigurationProperty("strict", DefaultValue = "False")]
		public bool Strict
		{
			get
			{
				return (bool)base[CompilationSection.strictProp];
			}
			set
			{
				base[CompilationSection.strictProp] = value;
			}
		}

		/// <summary>Gets or sets the version of the .NET Framework that the Web site targets. </summary>
		/// <returns>The version of the .NET Framework that the Web site targets. The default value is null.</returns>
		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x0009E73E File Offset: 0x0009C93E
		// (set) Token: 0x06003C27 RID: 15399 RVA: 0x0009E750 File Offset: 0x0009C950
		[ConfigurationProperty("targetFramework", DefaultValue = null)]
		public string TargetFramework
		{
			get
			{
				return (string)base[CompilationSection.targetFrameworkProp];
			}
			set
			{
				base[CompilationSection.targetFrameworkProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the directory to use for temporary file storage during compilation.</summary>
		/// <returns>A value specifying the directory to use for temporary file storage during compilation.</returns>
		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x0009E75E File Offset: 0x0009C95E
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x0009E770 File Offset: 0x0009C970
		[ConfigurationProperty("tempDirectory", DefaultValue = "")]
		public string TempDirectory
		{
			get
			{
				return (string)base[CompilationSection.tempDirectoryProp];
			}
			set
			{
				base[CompilationSection.tempDirectoryProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether instructions to the compiler use physical paths or URLs.</summary>
		/// <returns>true if instructions to the compiler use URLs rather than physical paths; otherwise, false. The default is false. </returns>
		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x0009E77E File Offset: 0x0009C97E
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x0009E790 File Offset: 0x0009C990
		[ConfigurationProperty("urlLinePragmas", DefaultValue = "False")]
		public bool UrlLinePragmas
		{
			get
			{
				return (bool)base[CompilationSection.urlLinePragmasProp];
			}
			set
			{
				base[CompilationSection.urlLinePragmasProp] = value;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x0009E7A3 File Offset: 0x0009C9A3
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilationSection.properties;
			}
		}

		/// <summary>Gets or sets a string representing the object type used to intercept a <see cref="T:System.Web.UI.ControlBuilder" /> object and configure a container.</summary>
		/// <returns>A string representing the object type used to intercept a <see cref="T:System.Web.UI.ControlBuilder" /> object.</returns>
		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003C2D RID: 15405 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06003C2E RID: 15406 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ControlBuilderInterceptorType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets whether the "disableObsoleteWarnings" configuration value in the Compilation section is set.</summary>
		/// <returns>true if the "disableObsoleteWarnings" configuration value in the Compilation section is set; otherwise, false.</returns>
		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x0009E7AC File Offset: 0x0009C9AC
		// (set) Token: 0x06003C30 RID: 15408 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool DisableObsoleteWarnings
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether an ASP.NET application can take advantage of Windows 8 prefetch functionality.</summary>
		/// <returns>true if an ASP.NET application can take advantage of Windows 8 prefetch functionality; otherwise, false. The default is false.</returns>
		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x0009E7C8 File Offset: 0x0009C9C8
		// (set) Token: 0x06003C32 RID: 15410 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool EnablePrefetchOptimization
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.FolderLevelBuildProviderCollection" /> collection of the <see cref="T:System.Web.Configuration.CompilationSection" /> class, which represents the build providers that are used during compilation.</summary>
		/// <returns>The build providers that are used during compilation.</returns>
		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003C33 RID: 15411 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public FolderLevelBuildProviderCollection FolderLevelBuildProviders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets whether the " maxConcurrentCompilations" configuration value in the Compilation section is set.</summary>
		/// <returns>true if the " maxConcurrentCompilations" configuration value in the Compilation section is set; otherwise, false.</returns>
		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003C34 RID: 15412 RVA: 0x0009E7E4 File Offset: 0x0009C9E4
		// (set) Token: 0x06003C35 RID: 15413 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public int MaxConcurrentCompilations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the application is optimized for the deployed environment.</summary>
		/// <returns>A value that indicates whether the application is optimized for the deployed environment.</returns>
		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003C36 RID: 15414 RVA: 0x0009E800 File Offset: 0x0009CA00
		// (set) Token: 0x06003C37 RID: 15415 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ProfileGuidedOptimizationsFlags ProfileGuidedOptimizations
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ProfileGuidedOptimizationsFlags.None;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040020AC RID: 8364
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020AD RID: 8365
		private static ConfigurationProperty compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020AE RID: 8366
		private static ConfigurationProperty tempDirectoryProp = new ConfigurationProperty("tempDirectory", typeof(string), "");

		// Token: 0x040020AF RID: 8367
		private static ConfigurationProperty debugProp = new ConfigurationProperty("debug", typeof(bool), false);

		// Token: 0x040020B0 RID: 8368
		private static ConfigurationProperty strictProp = new ConfigurationProperty("strict", typeof(bool), false);

		// Token: 0x040020B1 RID: 8369
		private static ConfigurationProperty explicitProp = new ConfigurationProperty("explicit", typeof(bool), true);

		// Token: 0x040020B2 RID: 8370
		private static ConfigurationProperty batchProp = new ConfigurationProperty("batch", typeof(bool), true);

		// Token: 0x040020B3 RID: 8371
		private static ConfigurationProperty batchTimeoutProp = new ConfigurationProperty("batchTimeout", typeof(TimeSpan), new TimeSpan(0, 15, 0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020B4 RID: 8372
		private static ConfigurationProperty maxBatchSizeProp = new ConfigurationProperty("maxBatchSize", typeof(int), 1000);

		// Token: 0x040020B5 RID: 8373
		private static ConfigurationProperty maxBatchGeneratedFileSizeProp = new ConfigurationProperty("maxBatchGeneratedFileSize", typeof(int), 3000);

		// Token: 0x040020B6 RID: 8374
		private static ConfigurationProperty numRecompilesBeforeAppRestartProp = new ConfigurationProperty("numRecompilesBeforeAppRestart", typeof(int), 15);

		// Token: 0x040020B7 RID: 8375
		private static ConfigurationProperty defaultLanguageProp = new ConfigurationProperty("defaultLanguage", typeof(string), "vb");

		// Token: 0x040020B8 RID: 8376
		private static ConfigurationProperty assembliesProp = new ConfigurationProperty("assemblies", typeof(AssemblyCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020B9 RID: 8377
		private static ConfigurationProperty assemblyPostProcessorTypeProp = new ConfigurationProperty("assemblyPostProcessorType", typeof(string), "");

		// Token: 0x040020BA RID: 8378
		private static ConfigurationProperty buildProvidersProp = new ConfigurationProperty("buildProviders", typeof(BuildProviderCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020BB RID: 8379
		private static ConfigurationProperty expressionBuildersProp = new ConfigurationProperty("expressionBuilders", typeof(ExpressionBuilderCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020BC RID: 8380
		private static ConfigurationProperty urlLinePragmasProp = new ConfigurationProperty("urlLinePragmas", typeof(bool), false);

		// Token: 0x040020BD RID: 8381
		private static ConfigurationProperty codeSubDirectoriesProp = new ConfigurationProperty("codeSubDirectories", typeof(CodeSubDirectoriesCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020BE RID: 8382
		private static ConfigurationProperty optimizeCompilationsProp = new ConfigurationProperty("optimizeCompilations", typeof(bool), false);

		// Token: 0x040020BF RID: 8383
		private static ConfigurationProperty targetFrameworkProp = new ConfigurationProperty("targetFramework", typeof(string), null);
	}
}
