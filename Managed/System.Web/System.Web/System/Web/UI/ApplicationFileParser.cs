using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200019C RID: 412
	internal sealed class ApplicationFileParser : TemplateParser
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x00029043 File Offset: 0x00027243
		public ApplicationFileParser(string fname, HttpContext context)
		{
			base.InputFile = fname;
			base.Context = context;
			base.VirtualPath = new VirtualPath("/" + Path.GetFileName(fname));
			this.LoadConfigDefaults();
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002907A File Offset: 0x0002727A
		internal ApplicationFileParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00029088 File Offset: 0x00027288
		internal ApplicationFileParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.Reader = reader;
			if (string.IsNullOrEmpty(inputFile))
			{
				base.InputFile = virtualPath.PhysicalPath;
			}
			else
			{
				base.InputFile = inputFile;
			}
			base.SetBaseType(null);
			this.LoadConfigDefaults();
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000290DB File Offset: 0x000272DB
		internal override Type CompileIntoType()
		{
			return GlobalAsaxCompiler.CompileApplicationType(this);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000290E4 File Offset: 0x000272E4
		internal static Type GetCompiledApplicationType(string inputFile, HttpContext context)
		{
			ApplicationFileParser applicationFileParser = new ApplicationFileParser(inputFile, context);
			Type compiledType = new AspGenerator(applicationFileParser).GetCompiledType();
			ApplicationFileParser.dependencies = applicationFileParser.Dependencies;
			return compiledType;
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00029110 File Offset: 0x00027310
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare(directive, "application", true, Helpers.InvariantCulture) != 0 && string.Compare(directive, "Import", true, Helpers.InvariantCulture) != 0 && string.Compare(directive, "Assembly", true, Helpers.InvariantCulture) != 0)
			{
				base.ThrowParseException("Invalid directive: " + directive, Array.Empty<object>());
			}
			base.AddDirective(directive, atts);
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00029174 File Offset: 0x00027374
		internal static List<string> FileDependencies
		{
			get
			{
				return ApplicationFileParser.dependencies;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0002917C File Offset: 0x0002737C
		internal override Type DefaultBaseType
		{
			get
			{
				Type defaultApplicationBaseType = PageParser.DefaultApplicationBaseType;
				if (defaultApplicationBaseType == null)
				{
					return base.DefaultBaseType;
				}
				return defaultApplicationBaseType;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x000291A0 File Offset: 0x000273A0
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.HttpApplication";
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x000291A7 File Offset: 0x000273A7
		internal override string DefaultDirectiveName
		{
			get
			{
				return "application";
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000291AE File Offset: 0x000273AE
		internal override string BaseVirtualDir
		{
			get
			{
				return base.Context.Request.ApplicationPath;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000291C0 File Offset: 0x000273C0
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x000291C8 File Offset: 0x000273C8
		internal override TextReader Reader
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

		// Token: 0x0400133E RID: 4926
		private static List<string> dependencies;

		// Token: 0x0400133F RID: 4927
		private TextReader reader;
	}
}
