using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Web.Hosting;
using System.Web.Util;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Provides compilation services for ASP.NET applications.</summary>
	// Token: 0x02000643 RID: 1603
	public sealed class ClientBuildManager : MarshalByRefObject, IDisposable
	{
		/// <summary>Occurs when an application domain is shut down. </summary>
		// Token: 0x14000112 RID: 274
		// (add) Token: 0x060044E3 RID: 17635 RVA: 0x000BAD9A File Offset: 0x000B8F9A
		// (remove) Token: 0x060044E4 RID: 17636 RVA: 0x000BADAD File Offset: 0x000B8FAD
		public event BuildManagerHostUnloadEventHandler AppDomainShutdown
		{
			add
			{
				this.events.AddHandler(ClientBuildManager.appDomainShutdownEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ClientBuildManager.appDomainShutdownEvent, value);
			}
		}

		/// <summary>Occurs when an application domain is started. </summary>
		// Token: 0x14000113 RID: 275
		// (add) Token: 0x060044E5 RID: 17637 RVA: 0x000BADC0 File Offset: 0x000B8FC0
		// (remove) Token: 0x060044E6 RID: 17638 RVA: 0x000BADD3 File Offset: 0x000B8FD3
		public event EventHandler AppDomainStarted
		{
			add
			{
				this.events.AddHandler(ClientBuildManager.appDomainStartedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ClientBuildManager.appDomainStartedEvent, value);
			}
		}

		/// <summary>Occurs when an application domain is unloaded. </summary>
		// Token: 0x14000114 RID: 276
		// (add) Token: 0x060044E7 RID: 17639 RVA: 0x000BADE6 File Offset: 0x000B8FE6
		// (remove) Token: 0x060044E8 RID: 17640 RVA: 0x000BADF9 File Offset: 0x000B8FF9
		public event BuildManagerHostUnloadEventHandler AppDomainUnloaded
		{
			add
			{
				this.events.AddHandler(ClientBuildManager.appDomainUnloadedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ClientBuildManager.appDomainUnloadedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class without a specified target directory or precompilation flags. </summary>
		/// <param name="appVirtualDir">The virtual path to the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
		// Token: 0x060044E9 RID: 17641 RVA: 0x000BAE0C File Offset: 0x000B900C
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir)
		{
			this.events = new EventHandlerList();
			base..ctor();
			if (appVirtualDir == null || appVirtualDir == "")
			{
				throw new ArgumentNullException("appVirtualDir");
			}
			if (appPhysicalSourceDir == null || appPhysicalSourceDir == "")
			{
				throw new ArgumentNullException("appPhysicalSourceDir");
			}
			this.virt_dir = appVirtualDir;
			this.phys_src_dir = appPhysicalSourceDir;
			this.manager = ApplicationManager.GetApplicationManager();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class with the specified target directory. </summary>
		/// <param name="appVirtualDir">The virtual path to the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
		/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
		// Token: 0x060044EA RID: 17642 RVA: 0x000BAE79 File Offset: 0x000B9079
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir)
			: this(appVirtualDir, appPhysicalSourceDir)
		{
			if (appPhysicalTargetDir == null || appPhysicalTargetDir == "")
			{
				throw new ArgumentNullException("appPhysicalTargetDir");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class with the specified target directory and compilation parameter.</summary>
		/// <param name="appVirtualDir">The virtual path to the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
		/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
		/// <param name="parameter">Values that determine the precompilation behavior.</param>
		// Token: 0x060044EB RID: 17643 RVA: 0x000BAE9E File Offset: 0x000B909E
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter)
			: this(appVirtualDir, appPhysicalSourceDir, appPhysicalTargetDir)
		{
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x000BAEAC File Offset: 0x000B90AC
		private BareApplicationHost Host
		{
			get
			{
				if (this.host != null)
				{
					return this.host;
				}
				int num = this.virt_dir.GetHashCode();
				if (this.app_id != null)
				{
					num ^= int.Parse(this.app_id);
				}
				this.app_id = num.ToString(Helpers.InvariantCulture);
				this.host = this.manager.CreateHostWithCheck(this.app_id, this.virt_dir, this.phys_src_dir);
				this.cache_path = "";
				int num2 = this.virt_dir.GetHashCode() << 5 + this.phys_src_dir.GetHashCode();
				this.cache_path = Path.Combine(this.cache_path, num2.ToString(Helpers.InvariantCulture));
				Directory.CreateDirectory(this.cache_path);
				this.OnAppDomainStarted();
				return this.host;
			}
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x000BAF7C File Offset: 0x000B917C
		private void OnAppDomainStarted()
		{
			EventHandler eventHandler = this.events[ClientBuildManager.appDomainStartedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x000BAFB0 File Offset: 0x000B91B0
		private void OnAppDomainShutdown(ApplicationShutdownReason reason)
		{
			BuildManagerHostUnloadEventHandler buildManagerHostUnloadEventHandler = this.events[ClientBuildManager.appDomainShutdownEvent] as BuildManagerHostUnloadEventHandler;
			if (buildManagerHostUnloadEventHandler != null)
			{
				BuildManagerHostUnloadEventArgs e = new BuildManagerHostUnloadEventArgs(reason);
				buildManagerHostUnloadEventHandler(this, e);
			}
		}

		/// <summary>Compiles application-dependent files, such as files in the App_Code directory, the Global.asax file, resource files, and Web references.</summary>
		// Token: 0x060044EF RID: 17647 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void CompileApplicationDependencies()
		{
			throw new NotImplementedException();
		}

		/// <summary>Compiles the file represented by the virtual path.</summary>
		/// <param name="virtualPath">The path to the file to be compiled.</param>
		// Token: 0x060044F0 RID: 17648 RVA: 0x000BAFE5 File Offset: 0x000B91E5
		public void CompileFile(string virtualPath)
		{
			this.CompileFile(virtualPath, null);
		}

		/// <summary>Compiles the file represented by the virtual path and provides a callback class to receive status information about the build.</summary>
		/// <param name="virtualPath">The path to the file to be compiled.</param>
		/// <param name="callback">The object to receive status information from compilation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044F1 RID: 17649 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void CompileFile(string virtualPath, ClientBuildManagerCallback callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object in the application domain of the ASP.NET runtime.</summary>
		/// <returns>An object in the application domain of the ASP.NET runtime.</returns>
		/// <param name="type">The type of object to be created.</param>
		/// <param name="failIfExists">true to throw an exception if the object has already been created in the application domain of the ASP.NET runtime; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The object already exists in the application domain and <paramref name="failIfExists" /> is true.</exception>
		// Token: 0x060044F2 RID: 17650 RVA: 0x000BAFEF File Offset: 0x000B91EF
		public IRegisteredObject CreateObject(Type type, bool failIfExists)
		{
			return this.manager.CreateObject(this.app_id, type, this.virt_dir, this.phys_src_dir, failIfExists);
		}

		/// <summary>Generates code from the contents of a file.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the generated code.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="virtualFileString">The contents of the file.</param>
		/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044F3 RID: 17651 RVA: 0x000BB010 File Offset: 0x000B9210
		[global::System.MonoTODO("Currently does not return the GeneratedCode")]
		public string GenerateCode(string virtualPath, string virtualFileString, out IDictionary linePragmasTable)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			Type type;
			CompilerParameters compilerParameters;
			this.GenerateCodeCompileUnit(virtualPath, virtualFileString, out type, out compilerParameters, out linePragmasTable);
			return null;
		}

		/// <summary>Returns the contents, codeDOM tree, compiler type, and compiler parameters for a file represented by a virtual path.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> for the given file.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="virtualFileString">The contents of the file represented by the <paramref name="virtualPath" /> parameter.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the codeDOM provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file represented by the <paramref name="virtualPath" /> parameter will be compiled.</param>
		/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044F4 RID: 17652 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, string virtualFileString, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the codeDOM tree, compiler type, and compiler parameters for a file represented by a virtual path.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> for the given file.</returns>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the codeDOM provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
		/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
		// Token: 0x060044F5 RID: 17653 RVA: 0x000BB03F File Offset: 0x000B923F
		public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
		{
			return this.GenerateCodeCompileUnit(virtualPath, out codeDomProviderType, out compilerParameters, out linePragmasTable);
		}

		/// <summary>Gets the directories with files that, when changed, cause the application domain to shut down.</summary>
		/// <returns>A <see cref="T:System.String" /> array containing the top-level directory names.</returns>
		// Token: 0x060044F6 RID: 17654 RVA: 0x000BB04C File Offset: 0x000B924C
		public string[] GetAppDomainShutdownDirectories()
		{
			return ClientBuildManager.shutdown_directories;
		}

		/// <summary>Gets a collection of browser elements.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing browser elements.</returns>
		// Token: 0x060044F7 RID: 17655 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public IDictionary GetBrowserDefinitions()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets information about the compiler type, compiler parameters, and the directory in which to store code files generated from non-code files such as .wsdl files.</summary>
		/// <param name="virtualCodeDir">The directory about which to retrieve information.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
		/// <param name="generatedFilesDir">When this method returns, contains the directory for files generated from non-code files.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualCodeDir" /> is null.</exception>
		// Token: 0x060044F8 RID: 17656 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void GetCodeDirectoryInformation(string virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
		{
			throw new NotImplementedException();
		}

		/// <summary>Compiles the file represented by the virtual path and returns its compiled type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the compiled file.</returns>
		/// <param name="virtualPath">The virtual path of the file to compile. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044F9 RID: 17657 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public Type GetCompiledType(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the compiler type and parameters that are used to build a file represented by a virtual path.</summary>
		/// <param name="virtualPath">The virtual path to the file.</param>
		/// <param name="codeDomProviderType">When this method returns, contains the provider type used for code generation and compilation.</param>
		/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044FA RID: 17658 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void GetCompilerParameters(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the virtual path of a generated file.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the virtual path for <paramref name="filePath" />.</returns>
		/// <param name="filePath">The full physical path to a generated file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filePath" /> is null.</exception>
		// Token: 0x060044FB RID: 17659 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public string GetGeneratedFileVirtualPath(string filePath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the physical path to the generated file for a virtual path. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the physical path to the generated file.</returns>
		/// <param name="virtualPath">The virtual path of the file to retrieve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044FC RID: 17660 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public string GetGeneratedSourceFile(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an array of the assemblies defined in the Bin directory and the &lt;assembly&gt; section of the Web configuration file.</summary>
		/// <returns>A <see cref="T:System.String" /> array containing paths to code bases in the Bin directory and the &lt;assembly&gt; section of the Web configuration file. </returns>
		/// <param name="virtualPath">The configuration name and path.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null.</exception>
		// Token: 0x060044FD RID: 17661 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public string[] GetTopLevelAssemblyReferences(string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the virtual paths to the App_Code directory and its subdirectories in an ASP.NET application.</summary>
		/// <returns>A <see cref="T:System.String" /> array containing all the virtual paths to code directories in an application.</returns>
		// Token: 0x060044FE RID: 17662 RVA: 0x00003A2B File Offset: 0x00001C2B
		public string[] GetVirtualCodeDirectories()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gives the application domain an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x060044FF RID: 17663 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Indicates whether an assembly is a code assembly.</summary>
		/// <returns>true if the <paramref name="assemblyName" /> parameter matches one of the generated code assemblies; otherwise, false.</returns>
		/// <param name="assemblyName">The name of the assembly to be identified as a code assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.</exception>
		// Token: 0x06004500 RID: 17664 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public bool IsCodeAssembly(string assemblyName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Precompiles an ASP.NET application.</summary>
		// Token: 0x06004501 RID: 17665 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void PrecompileApplication()
		{
			throw new NotImplementedException();
		}

		/// <summary>Precompiles an ASP.NET application and provides a callback method to receive status information about the build.</summary>
		/// <param name="callback">A <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> containing the method to call when reporting the result of compilation.</param>
		// Token: 0x06004502 RID: 17666 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void PrecompileApplication(ClientBuildManagerCallback callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Precompiles an ASP.NET application, provides a callback method to receive status information about the build, and indicates whether to create a clean build.</summary>
		/// <param name="callback">A <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> containing the method to call when reporting the result of compilation.</param>
		/// <param name="forceCleanBuild">true to perform a clean build, which will first delete all object and intermediate files; false to rebuild only those files that have changed. Set to true if there is a chance that a dependency might not be picked up by the build environment.</param>
		// Token: 0x06004503 RID: 17667 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public void PrecompileApplication(ClientBuildManagerCallback callback, bool forceCleanBuild)
		{
			throw new NotImplementedException();
		}

		/// <summary>Unloads the application domain for compiling ASP.NET Web applications.</summary>
		/// <returns>true if the application domain is unloaded; otherwise, false.</returns>
		// Token: 0x06004504 RID: 17668 RVA: 0x000BB053 File Offset: 0x000B9253
		public bool Unload()
		{
			if (this.host != null)
			{
				this.host.Shutdown();
				this.OnAppDomainShutdown(ApplicationShutdownReason.None);
				this.host = null;
			}
			return true;
		}

		/// <summary>Gets the physical path to the directory used for code generation.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the directory path used for code generation.</returns>
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x000BB077 File Offset: 0x000B9277
		public string CodeGenDir
		{
			get
			{
				return this.Host.GetCodeGenDir();
			}
		}

		/// <summary>Gets a value that indicates whether an application domain for compiling ASP.NET Web applications has been created.</summary>
		/// <returns>true if the application domain for compiling ASP.NET Web applications has been created; otherwise, false.</returns>
		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06004506 RID: 17670 RVA: 0x000BB084 File Offset: 0x000B9284
		public bool IsHostCreated
		{
			get
			{
				return this.host != null;
			}
		}

		/// <summary>Terminates the current ASP.NET application.</summary>
		// Token: 0x06004507 RID: 17671 RVA: 0x000BB08F File Offset: 0x000B928F
		void IDisposable.Dispose()
		{
			this.Unload();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class by using the specified virtual directory, source directory, target directory, compilation parameter, and type-description provider.</summary>
		/// <param name="appVirtualDir">The virtual path of the application root.</param>
		/// <param name="appPhysicalSourceDir">The physical path of the application root.</param>
		/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
		/// <param name="parameter">Values that determine the precompilation behavior.</param>
		/// <param name="typeDescriptionProvider">The type-description provider to use. This parameter is primarily used to support the multi-targeting infrastructure in Visual Studio. It is used to retrieve metadata about types that is filtered for specific versions of the .NET Framework.</param>
		// Token: 0x06004509 RID: 17673 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter, TypeDescriptionProvider typeDescriptionProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040024C6 RID: 9414
		private static readonly object appDomainShutdownEvent = new object();

		// Token: 0x040024C7 RID: 9415
		private static readonly object appDomainStartedEvent = new object();

		// Token: 0x040024C8 RID: 9416
		private static readonly object appDomainUnloadedEvent = new object();

		// Token: 0x040024C9 RID: 9417
		private string virt_dir;

		// Token: 0x040024CA RID: 9418
		private string phys_src_dir;

		// Token: 0x040024CB RID: 9419
		private BareApplicationHost host;

		// Token: 0x040024CC RID: 9420
		private ApplicationManager manager;

		// Token: 0x040024CD RID: 9421
		private string app_id;

		// Token: 0x040024CE RID: 9422
		private string cache_path;

		// Token: 0x040024CF RID: 9423
		private EventHandlerList events;

		// Token: 0x040024D0 RID: 9424
		private static string[] shutdown_directories = new string[] { "bin", "App_GlobalResources", "App_Code", "App_WebReferences", "App_Browsers" };
	}
}
