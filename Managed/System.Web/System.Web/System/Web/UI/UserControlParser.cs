using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200023E RID: 574
	internal class UserControlParser : TemplateControlParser
	{
		// Token: 0x060017A0 RID: 6048 RVA: 0x0003E077 File Offset: 0x0003C277
		internal UserControlParser(VirtualPath virtualPath, string inputFile, HttpContext context)
			: this(virtualPath, inputFile, context, null)
		{
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0003E083 File Offset: 0x0003C283
		internal UserControlParser(VirtualPath virtualPath, string inputFile, List<string> deps, HttpContext context)
			: this(virtualPath, inputFile, context, null)
		{
			base.Dependencies = deps;
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0003E097 File Offset: 0x0003C297
		internal UserControlParser(VirtualPath virtualPath, string inputFile, HttpContext context, string type)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.BaseVirtualDir = virtualPath.DirectoryNoNormalize;
			base.InputFile = inputFile;
			base.SetBaseType(type);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0003E0D4 File Offset: 0x0003C2D4
		internal UserControlParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0003E0E0 File Offset: 0x0003C2E0
		internal UserControlParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.BaseVirtualDir = virtualPath.DirectoryNoNormalize;
			if (string.IsNullOrEmpty(inputFile))
			{
				base.InputFile = virtualPath.PhysicalPath;
			}
			else
			{
				base.InputFile = inputFile;
			}
			this.Reader = reader;
			base.SetBaseType(null);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0003E148 File Offset: 0x0003C348
		internal UserControlParser(TextReader reader, int? uniqueSuffix, HttpContext context)
		{
			base.Context = context;
			string filePath = context.Request.FilePath;
			base.VirtualPath = new VirtualPath(filePath);
			this.BaseVirtualDir = VirtualPathUtility.GetDirectory(filePath, false);
			base.InputFile = VirtualPathUtility.GetFileName(filePath) + "#" + ((uniqueSuffix != null) ? uniqueSuffix.Value.ToString("x") : "0");
			this.Reader = reader;
			base.SetBaseType(null);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0003E1DB File Offset: 0x0003C3DB
		internal static Type GetCompiledType(TextReader reader, int? inputHashCode, HttpContext context)
		{
			return new UserControlParser(reader, inputHashCode, context).CompileIntoType();
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0003E1EA File Offset: 0x0003C3EA
		internal static Type GetCompiledType(string virtualPath, string inputFile, List<string> deps, HttpContext context)
		{
			return new UserControlParser(new VirtualPath(virtualPath), inputFile, deps, context).CompileIntoType();
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0003E1FF File Offset: 0x0003C3FF
		public static Type GetCompiledType(string virtualPath, string inputFile, HttpContext context)
		{
			return new UserControlParser(new VirtualPath(virtualPath), inputFile, context).CompileIntoType();
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00038745 File Offset: 0x00036945
		internal override Type CompileIntoType()
		{
			return new AspGenerator(this).GetCompiledType();
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0003E213 File Offset: 0x0003C413
		internal override void ProcessMainAttributes(IDictionary atts)
		{
			this.masterPage = BaseParser.GetString(atts, "MasterPageFile", null);
			if (this.masterPage != null)
			{
				this.AddDependency(this.masterPage);
			}
			base.ProcessMainAttributes(atts);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0003E242 File Offset: 0x0003C442
		internal override void ProcessOutputCacheAttributes(IDictionary atts)
		{
			this.providerName = BaseParser.GetString(atts, "ProviderName", null);
			base.ProcessOutputCacheAttributes(atts);
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0003E260 File Offset: 0x0003C460
		internal override Type DefaultBaseType
		{
			get
			{
				Type defaultUserControlBaseType = PageParser.DefaultUserControlBaseType;
				if (defaultUserControlBaseType == null)
				{
					return base.DefaultBaseType;
				}
				return defaultUserControlBaseType;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0003E284 File Offset: 0x0003C484
		internal override string DefaultBaseTypeName
		{
			get
			{
				return base.PagesConfig.UserControlBaseType;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0003E291 File Offset: 0x0003C491
		internal override string DefaultDirectiveName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0003E298 File Offset: 0x0003C498
		internal string MasterPageFile
		{
			get
			{
				return this.masterPage;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0003E2A0 File Offset: 0x0003C4A0
		internal string ProviderName
		{
			get
			{
				return this.providerName;
			}
		}

		// Token: 0x040015F5 RID: 5621
		private string masterPage;

		// Token: 0x040015F6 RID: 5622
		private string providerName;
	}
}
