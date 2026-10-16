using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Provides base functionality for parsing Web handler files.</summary>
	// Token: 0x02000221 RID: 545
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class SimpleWebHandlerParser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SimpleWebHandlerParser" /> class. </summary>
		/// <param name="context">Pass null. Parameter is now obsolete.</param>
		/// <param name="virtualPath">The path of the current virtual directory.</param>
		/// <param name="physicalPath">Pass null. Parameter is now obsolete.</param>
		// Token: 0x06001631 RID: 5681 RVA: 0x0003964C File Offset: 0x0003784C
		protected SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
			: this(context, virtualPath, physicalPath, null)
		{
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00039658 File Offset: 0x00037858
		internal SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath, TextReader reader)
		{
			this.reader = reader;
			this.cachedType = CachingCompiler.GetTypeFromCache(physicalPath);
			if (this.cachedType != null)
			{
				return;
			}
			if (context != null)
			{
				this.context = context;
			}
			else
			{
				this.context = HttpContext.Current;
			}
			this.vPath = virtualPath;
			this.AddDependency(virtualPath);
			if (physicalPath != null && physicalPath.Length > 0)
			{
				this.physPath = physicalPath;
			}
			else
			{
				HttpRequest httpRequest = ((this.context != null) ? context.Request : null);
				if (httpRequest != null)
				{
					this.physPath = httpRequest.MapPath(virtualPath);
				}
			}
			this.assemblies = new ArrayList();
			string assemblyLocation = this.Context.ApplicationInstance.AssemblyLocation;
			if (assemblyLocation != typeof(TemplateParser).Assembly.Location)
			{
				this.appAssemblyIndex = this.assemblies.Add(assemblyLocation);
			}
			bool flag = false;
			foreach (object obj in this.CompilationConfig.Assemblies)
			{
				AssemblyInfo assemblyInfo = (AssemblyInfo)obj;
				if (assemblyInfo.Assembly == "*")
				{
					flag = true;
				}
				else
				{
					this.AddAssemblyByName(assemblyInfo.Assembly, null);
				}
			}
			if (flag)
			{
				this.AddAssembliesInBin();
			}
			this.language = this.CompilationConfig.DefaultLanguage;
			this.GetDirectivesAndContent();
		}

		/// <summary>Returns the type for the compiled object from the virtual path.</summary>
		/// <returns>The <see cref="T:System.Type" /> assigned to the virtual path.</returns>
		// Token: 0x06001633 RID: 5683 RVA: 0x000397D4 File Offset: 0x000379D4
		protected Type GetCompiledTypeFromCache()
		{
			return this.cachedType;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x000397DC File Offset: 0x000379DC
		private void GetDirectivesAndContent()
		{
			bool flag = false;
			bool flag2 = false;
			StringBuilder stringBuilder = null;
			StringBuilder stringBuilder2 = new StringBuilder();
			StreamReader streamReader;
			if (this.reader != null)
			{
				streamReader = this.reader as StreamReader;
			}
			else
			{
				streamReader = new StreamReader(File.OpenRead(this.physPath), WebEncoding.FileEncoding);
			}
			using (streamReader)
			{
				string text;
				while ((text = streamReader.ReadLine()) != null && this.cachedType == null)
				{
					int length = text.Length;
					if (length == 0)
					{
						stringBuilder2.Append("\n");
					}
					else
					{
						int num = text.IndexOf("<%");
						if (num > -1)
						{
							int num2 = text.IndexOf("%>");
							if (num > 0)
							{
								stringBuilder2.Append(text.Substring(0, num));
							}
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							else
							{
								stringBuilder.Length = 0;
							}
							if (num2 <= -1)
							{
								flag2 = true;
								flag = false;
								stringBuilder.Append(text.Substring(num));
								continue;
							}
							flag = true;
							flag2 = false;
							stringBuilder.Append(text.Substring(num, num2 - num + 2));
							if (num2 < length - 2)
							{
								stringBuilder2.Append(text.Substring(num2 + 2, length - num2 - 2));
							}
						}
						if (flag2)
						{
							int num3 = text.IndexOf("%>");
							if (num3 <= -1)
							{
								stringBuilder.Append(text);
								continue;
							}
							stringBuilder.Append(text.Substring(0, num3 + 2));
							if (num3 < length)
							{
								stringBuilder2.Append(text.Substring(num3 + 2) + "\n");
							}
							flag2 = false;
							flag = true;
						}
						if (flag)
						{
							this.ParseDirective(stringBuilder.ToString());
							flag = false;
							if (this.gotDefault)
							{
								this.cachedType = CachingCompiler.GetTypeFromCache(this.physPath);
								if (this.cachedType != null)
								{
									break;
								}
							}
						}
						else
						{
							stringBuilder2.Append(text + "\n");
						}
					}
				}
			}
			if (!this.gotDefault)
			{
				throw new ParseException(null, "No @" + this.DefaultDirectiveName + " directive found");
			}
			if (this.cachedType == null)
			{
				this.program = stringBuilder2.ToString();
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x00039A1C File Offset: 0x00037C1C
		private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
		{
			if (tagtype != TagType.Directive)
			{
				throw new ParseException(location, "Unexpected tag");
			}
			if (tagid == null || tagid.Length == 0 || string.Compare(tagid, this.DefaultDirectiveName, true, Helpers.InvariantCulture) == 0)
			{
				this.AddDefaultDirective(location, attributes);
				return;
			}
			if (string.Compare(tagid, "Assembly", true, Helpers.InvariantCulture) == 0)
			{
				this.AddAssemblyDirective(location, attributes);
				return;
			}
			throw new ParseException(location, "Unexpected directive: " + tagid);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x00039A90 File Offset: 0x00037C90
		private void TextParsed(ILocation location, string text)
		{
			if (text.Trim() != "")
			{
				throw new ParseException(location, "Text not allowed here");
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00039AB0 File Offset: 0x00037CB0
		private void ParseError(ILocation location, string message)
		{
			throw new ParseException(location, message);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00039AB9 File Offset: 0x00037CB9
		private static string GetAndRemove(IDictionary table, string key)
		{
			string text = table[key] as string;
			table.Remove(key);
			return text;
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00039AD0 File Offset: 0x00037CD0
		private void ParseDirective(string line)
		{
			AspParser aspParser;
			using (StringReader stringReader = new StringReader(line))
			{
				aspParser = new AspParser(this.physPath, stringReader);
			}
			aspParser.Error += this.ParseError;
			aspParser.TagParsed += this.TagParsed;
			aspParser.TextParsed += this.TextParsed;
			aspParser.Parse();
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x00039B4C File Offset: 0x00037D4C
		internal virtual void AddDefaultDirective(ILocation location, TagAttributes attrs)
		{
			CompilationSection compilationConfig = this.CompilationConfig;
			if (this.gotDefault)
			{
				throw new ParseException(location, "duplicate " + this.DefaultDirectiveName + " directive");
			}
			this.gotDefault = true;
			IDictionary dictionary = attrs.GetDictionary(null);
			this.className = SimpleWebHandlerParser.GetAndRemove(dictionary, "class");
			if (this.className == null)
			{
				throw new ParseException(null, "No Class attribute found.");
			}
			string andRemove = SimpleWebHandlerParser.GetAndRemove(dictionary, "debug");
			if (andRemove != null)
			{
				this.debug = string.Compare(andRemove, "true", true, Helpers.InvariantCulture) == 0;
				if (!this.debug && string.Compare(andRemove, "false", true, Helpers.InvariantCulture) != 0)
				{
					throw new ParseException(null, "Invalid value for Debug attribute");
				}
			}
			else
			{
				this.debug = compilationConfig.Debug;
			}
			this.language = SimpleWebHandlerParser.GetAndRemove(dictionary, "language");
			if (this.language == null)
			{
				this.language = compilationConfig.DefaultLanguage;
			}
			SimpleWebHandlerParser.GetAndRemove(dictionary, "codebehind");
			if (dictionary.Count > 0)
			{
				throw new ParseException(location, "Unrecognized attribute in " + this.DefaultDirectiveName + " directive");
			}
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00039C6C File Offset: 0x00037E6C
		internal virtual void AddAssemblyDirective(ILocation location, TagAttributes attrs)
		{
			IDictionary dictionary = attrs.GetDictionary(null);
			string andRemove = SimpleWebHandlerParser.GetAndRemove(dictionary, "Name");
			string andRemove2 = SimpleWebHandlerParser.GetAndRemove(dictionary, "Src");
			if (andRemove == null && andRemove2 == null)
			{
				throw new ParseException(location, "You gotta specify Src or Name");
			}
			if (andRemove != null && andRemove2 != null)
			{
				throw new ParseException(location, "Src and Name cannot be used together");
			}
			if (andRemove != null)
			{
				this.AddAssemblyByName(andRemove, location);
			}
			else
			{
				this.GetAssemblyFromSource(andRemove2, location);
			}
			if (dictionary.Count > 0)
			{
				throw new ParseException(location, "Unrecognized attribute in Assembly directive");
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00039CE8 File Offset: 0x00037EE8
		internal virtual void AddAssembly(Assembly assembly, bool fullPath)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (this.anames == null)
			{
				this.anames = new Hashtable();
			}
			string name = assembly.GetName().Name;
			string location = assembly.Location;
			if (fullPath)
			{
				if (!this.assemblies.Contains(location))
				{
					this.assemblies.Add(location);
				}
				this.anames[name] = location;
				this.anames[location] = assembly;
				return;
			}
			if (!this.assemblies.Contains(name))
			{
				this.assemblies.Add(name);
			}
			this.anames[name] = assembly;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00039D90 File Offset: 0x00037F90
		internal virtual Assembly AddAssemblyByName(string name, ILocation location)
		{
			if (this.anames == null)
			{
				this.anames = new Hashtable();
			}
			if (this.anames.Contains(name))
			{
				object obj = this.anames[name];
				if (obj is string)
				{
					obj = this.anames[obj];
				}
				return (Assembly)obj;
			}
			Assembly assembly = this.LoadAssemblyFromBin(name);
			if (assembly != null)
			{
				this.AddAssembly(assembly, true);
				return assembly;
			}
			Exception ex = null;
			try
			{
				assembly = Assembly.LoadWithPartialName(name);
			}
			catch (Exception ex)
			{
				assembly = null;
			}
			if (assembly == null)
			{
				throw new ParseException(location, string.Format("Assembly '{0}' not found", name), ex);
			}
			this.AddAssembly(assembly, true);
			return assembly;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00039E48 File Offset: 0x00038048
		private void AddAssembliesInBin()
		{
			foreach (string text in HttpApplication.BinDirectoryAssemblies)
			{
				Exception ex = null;
				try
				{
					Assembly assembly = Assembly.LoadFrom(text);
					this.AddAssembly(assembly, true);
				}
				catch (FileLoadException ex)
				{
				}
				catch (BadImageFormatException ex)
				{
				}
				catch (Exception ex2)
				{
					throw new Exception("Error while loading " + text, ex2);
				}
				if (ex != null && RuntimeHelpers.DebuggingEnabled)
				{
					Console.WriteLine("**** DEBUG MODE *****");
					Console.WriteLine("Bad assembly found in bin/. Exception (ignored):");
					Console.WriteLine(ex);
				}
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00039EEC File Offset: 0x000380EC
		private Assembly LoadAssemblyFromBin(string name)
		{
			foreach (string text in HttpApplication.BinDirectoryAssemblies)
			{
				if (!(Path.ChangeExtension(Path.GetFileName(text), null) != name))
				{
					return Assembly.LoadFrom(text);
				}
			}
			return null;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00039F30 File Offset: 0x00038130
		private Assembly GetAssemblyFromSource(string vpath, ILocation location)
		{
			vpath = UrlUtils.Combine(this.BaseVirtualDir, vpath);
			string text = this.context.Request.MapPath(vpath);
			if (!File.Exists(text))
			{
				throw new ParseException(location, "File " + vpath + " not found");
			}
			this.AddDependency(vpath);
			CompilerResults compilerResults = CachingCompiler.Compile(this.language, text, text, this.assemblies);
			if (compilerResults.NativeCompilerReturnValue != 0)
			{
				using (StreamReader streamReader = new StreamReader(text))
				{
					throw new CompilationException(text, compilerResults.Errors, streamReader.ReadToEnd());
				}
			}
			this.AddAssembly(compilerResults.CompiledAssembly, true);
			return compilerResults.CompiledAssembly;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00039FE8 File Offset: 0x000381E8
		internal Type GetTypeFromBin(string tname)
		{
			if (tname == null || tname.Length == 0)
			{
				throw new ArgumentNullException("tname");
			}
			Type type = null;
			int num = tname.IndexOf(',');
			string text;
			string text2;
			if (num != -1)
			{
				text = tname.Substring(0, num).Trim();
				text2 = tname.Substring(num + 1).Trim();
			}
			else
			{
				text = tname;
				text2 = null;
			}
			Type type2 = null;
			Assembly assembly = null;
			if (text2 != null)
			{
				assembly = Assembly.Load(text2);
				if (assembly != null)
				{
					type2 = assembly.GetType(text, false);
				}
				if (type2 != null)
				{
					return type2;
				}
			}
			IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
			if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
			{
				foreach (object obj in topLevelAssemblies)
				{
					type2 = ((Assembly)obj).GetType(text, false);
					if (type2 != null)
					{
						if (type != null)
						{
							throw new HttpException(string.Format("Type {0} is not unique.", text));
						}
						type = type2;
					}
				}
			}
			string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
			int i = 0;
			while (i < binDirectoryAssemblies.Length)
			{
				string text3 = binDirectoryAssemblies[i];
				try
				{
					assembly = Assembly.LoadFrom(text3);
				}
				catch (FileLoadException)
				{
					goto IL_014D;
				}
				catch (BadImageFormatException)
				{
					goto IL_014D;
				}
				goto IL_011B;
				IL_014D:
				i++;
				continue;
				IL_011B:
				type2 = assembly.GetType(text, false);
				if (!(type2 != null))
				{
					goto IL_014D;
				}
				if (type != null)
				{
					throw new HttpException(string.Format("Type {0} is not unique.", text));
				}
				type = type2;
				goto IL_014D;
			}
			if (type == null)
			{
				throw new HttpException(string.Format("Type {0} not found.", text));
			}
			return type;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0003A194 File Offset: 0x00038394
		internal virtual void AddDependency(string filename)
		{
			if (this.dependencies == null)
			{
				this.dependencies = new ArrayList();
			}
			if (!this.dependencies.Contains(filename))
			{
				this.dependencies.Add(filename);
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the main directive from a &lt;%@ %&gt; block.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the default directive name.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001643 RID: 5699
		protected abstract string DefaultDirectiveName { get; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0003A1C4 File Offset: 0x000383C4
		internal HttpContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0003A1CC File Offset: 0x000383CC
		internal string VirtualPath
		{
			get
			{
				return this.vPath;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0003A1D4 File Offset: 0x000383D4
		internal string PhysicalPath
		{
			get
			{
				return this.physPath;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x0003A1DC File Offset: 0x000383DC
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0003A1E4 File Offset: 0x000383E4
		internal bool Debug
		{
			get
			{
				return this.debug;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0003A1EC File Offset: 0x000383EC
		internal string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0003A1F4 File Offset: 0x000383F4
		internal string Program
		{
			get
			{
				if (this.program != null)
				{
					return this.program;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0003A20C File Offset: 0x0003840C
		internal ArrayList Assemblies
		{
			get
			{
				if (this.appAssemblyIndex != -1)
				{
					object obj = this.assemblies[this.appAssemblyIndex];
					this.assemblies.RemoveAt(this.appAssemblyIndex);
					this.assemblies.Add(obj);
					this.appAssemblyIndex = -1;
				}
				return this.assemblies;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0003A25F File Offset: 0x0003845F
		internal ArrayList Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0003A267 File Offset: 0x00038467
		internal string BaseDir
		{
			get
			{
				if (this.baseDir == null)
				{
					this.baseDir = this.context.Request.MapPath(this.BaseVirtualDir);
				}
				return this.baseDir;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0003A293 File Offset: 0x00038493
		internal virtual string BaseVirtualDir
		{
			get
			{
				if (this.baseVDir == null)
				{
					this.baseVDir = UrlUtils.GetDirectory(this.context.Request.FilePath);
				}
				return this.baseVDir;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0003A2C0 File Offset: 0x000384C0
		private CompilationSection CompilationConfig
		{
			get
			{
				string virtualPath = this.VirtualPath;
				if (string.IsNullOrEmpty(virtualPath))
				{
					return WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
				}
				return WebConfigurationManager.GetSection("system.web/compilation", virtualPath) as CompilationSection;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0003A2FC File Offset: 0x000384FC
		// (set) Token: 0x06001651 RID: 5713 RVA: 0x0003A304 File Offset: 0x00038504
		internal TextReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
			}
		}

		// Token: 0x04001563 RID: 5475
		private HttpContext context;

		// Token: 0x04001564 RID: 5476
		private string vPath;

		// Token: 0x04001565 RID: 5477
		private string physPath;

		// Token: 0x04001566 RID: 5478
		private string className;

		// Token: 0x04001567 RID: 5479
		private bool debug;

		// Token: 0x04001568 RID: 5480
		private string language;

		// Token: 0x04001569 RID: 5481
		private string program;

		// Token: 0x0400156A RID: 5482
		private bool gotDefault;

		// Token: 0x0400156B RID: 5483
		private ArrayList assemblies;

		// Token: 0x0400156C RID: 5484
		private ArrayList dependencies;

		// Token: 0x0400156D RID: 5485
		private Hashtable anames;

		// Token: 0x0400156E RID: 5486
		private string baseDir;

		// Token: 0x0400156F RID: 5487
		private string baseVDir;

		// Token: 0x04001570 RID: 5488
		private TextReader reader;

		// Token: 0x04001571 RID: 5489
		private int appAssemblyIndex = -1;

		// Token: 0x04001572 RID: 5490
		private Type cachedType;
	}
}
