using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Compilation;
using System.Web.Hosting;

namespace System.Web.UI
{
	// Token: 0x020001E5 RID: 485
	internal sealed class MasterPageParser : UserControlParser
	{
		// Token: 0x06001382 RID: 4994 RVA: 0x00032DF8 File Offset: 0x00030FF8
		internal MasterPageParser(VirtualPath virtualPath, string inputFile, HttpContext context)
			: base(virtualPath, inputFile, context, "System.Web.UI.MasterPage")
		{
			this.cacheEntryName = string.Concat(new object[] { "@@MasterPagePHIDS:", virtualPath, ":", inputFile });
			this.contentPlaceHolderIds = HttpRuntime.InternalCache.Get(this.cacheEntryName) as List<string>;
			this.LoadConfigDefaults();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00032E5D File Offset: 0x0003105D
		internal MasterPageParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00032E6C File Offset: 0x0003106C
		internal MasterPageParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
			: base(virtualPath, inputFile, reader, context)
		{
			this.cacheEntryName = string.Concat(new object[] { "@@MasterPagePHIDS:", virtualPath, ":", base.InputFile });
			this.contentPlaceHolderIds = HttpRuntime.InternalCache.Get(this.cacheEntryName) as List<string>;
			this.LoadConfigDefaults();
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00032ED3 File Offset: 0x000310D3
		public static MasterPage GetCompiledMasterInstance(string virtualPath, string inputFile, HttpContext context)
		{
			return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(MasterPage)) as MasterPage;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00032EEA File Offset: 0x000310EA
		public static Type GetCompiledMasterType(string virtualPath, string inputFile, HttpContext context)
		{
			return BuildManager.GetCompiledType(virtualPath);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00032EF2 File Offset: 0x000310F2
		internal override void HandleOptions(object obj)
		{
			base.HandleOptions(obj);
			((MasterPage)obj).MasterPageFile = base.MasterPageFile;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00032F0C File Offset: 0x0003110C
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare("MasterType", directive, StringComparison.OrdinalIgnoreCase) == 0)
			{
				PageParserFilter pageParserFilter = base.PageParserFilter;
				if (pageParserFilter != null)
				{
					pageParserFilter.PreprocessDirective(directive.ToLowerInvariant(), atts);
				}
				string @string = BaseParser.GetString(atts, "TypeName", null);
				if (@string != null)
				{
					this.masterType = base.LoadType(@string);
					if (this.masterType == null)
					{
						base.ThrowParseException("Could not load type '" + @string + "'.", Array.Empty<object>());
					}
				}
				else
				{
					string text = BaseParser.GetString(atts, "VirtualPath", null);
					if (!string.IsNullOrEmpty(text))
					{
						VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
						if (!virtualPathProvider.FileExists(text))
						{
							base.ThrowParseFileNotFound(text, Array.Empty<object>());
						}
						text = virtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, VirtualPathUtility.ToAbsolute(text));
						this.masterTypeVirtualPath = text;
						this.AddDependency(text);
					}
					else
					{
						base.ThrowParseException("The MasterType directive must have either a TypeName or a VirtualPath attribute.", Array.Empty<object>());
					}
				}
				if (this.masterType != null)
				{
					this.AddAssembly(this.masterType.Assembly, true);
					return;
				}
			}
			else
			{
				base.AddDirective(directive, atts);
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0003301B File Offset: 0x0003121B
		internal void AddContentPlaceHolderId(string id)
		{
			if (this.contentPlaceHolderIds == null)
			{
				this.contentPlaceHolderIds = new List<string>(1);
				HttpRuntime.InternalCache.Insert(this.cacheEntryName, this.contentPlaceHolderIds);
			}
			this.contentPlaceHolderIds.Add(id);
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00033053 File Offset: 0x00031253
		internal Type MasterType
		{
			get
			{
				if (this.masterType == null && !string.IsNullOrEmpty(this.masterTypeVirtualPath))
				{
					this.masterType = BuildManager.GetCompiledType(this.masterTypeVirtualPath);
				}
				return this.masterType;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00033087 File Offset: 0x00031287
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.UI.MasterPage";
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0003308E File Offset: 0x0003128E
		internal override string DefaultDirectiveName
		{
			get
			{
				return "master";
			}
		}

		// Token: 0x04001475 RID: 5237
		private Type masterType;

		// Token: 0x04001476 RID: 5238
		private string masterTypeVirtualPath;

		// Token: 0x04001477 RID: 5239
		private List<string> contentPlaceHolderIds;

		// Token: 0x04001478 RID: 5240
		private string cacheEntryName;
	}
}
