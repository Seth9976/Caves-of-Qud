using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Provides a base set of functionality for classes involved in parsing ASP.NET page requests and server controls.</summary>
	// Token: 0x0200019E RID: 414
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BaseParser
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x00029668 File Offset: 0x00027868
		internal string MapPath(string path)
		{
			return this.MapPath(path, true);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00029672 File Offset: 0x00027872
		internal string MapPath(string path, bool allowCrossAppMapping)
		{
			if (this.context == null)
			{
				throw new HttpException("context is null!!");
			}
			return this.context.Request.MapPath(path, this.BaseVirtualDir, allowCrossAppMapping);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002969F File Offset: 0x0002789F
		internal string PhysicalPath(string path)
		{
			if (Path.DirectorySeparatorChar != '/')
			{
				path = path.Replace('/', '\\');
			}
			return Path.Combine(this.BaseDir, path);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000296C4 File Offset: 0x000278C4
		internal bool GetBool(IDictionary hash, string key, bool deflt)
		{
			string text = hash[key] as string;
			if (text == null)
			{
				return deflt;
			}
			hash.Remove(key);
			bool flag = false;
			if (string.Compare(text, "true", true, Helpers.InvariantCulture) == 0)
			{
				flag = true;
			}
			else if (string.Compare(text, "false", true, Helpers.InvariantCulture) != 0)
			{
				this.ThrowParseException("Invalid value for " + key, Array.Empty<object>());
			}
			return flag;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00029730 File Offset: 0x00027930
		internal static string GetString(IDictionary hash, string key, string deflt)
		{
			string text = hash[key] as string;
			if (text == null)
			{
				return deflt;
			}
			hash.Remove(key);
			return text;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00029758 File Offset: 0x00027958
		internal static bool IsDirective(string value, char directiveChar)
		{
			if (value == null || value == string.Empty)
			{
				return false;
			}
			value = value.Trim();
			if (!StrUtils.StartsWith(value, "<%") || !StrUtils.EndsWith(value, "%>"))
			{
				return false;
			}
			int i = value.IndexOf(directiveChar, 2);
			if (i == -1)
			{
				return false;
			}
			if (i == 2)
			{
				return true;
			}
			for (i--; i >= 2; i--)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000297CF File Offset: 0x000279CF
		internal static bool IsDataBound(string value)
		{
			return BaseParser.IsDirective(value, '#');
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000297D9 File Offset: 0x000279D9
		internal static bool IsExpression(string value)
		{
			return BaseParser.IsDirective(value, '$');
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000297E3 File Offset: 0x000279E3
		internal void ThrowParseException(string message, params object[] parms)
		{
			if (parms == null)
			{
				throw new ParseException(this.location, message);
			}
			throw new ParseException(this.location, string.Format(message, parms));
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00029807 File Offset: 0x00027A07
		internal void ThrowParseException(string message, Exception inner, params object[] parms)
		{
			if (parms == null || parms.Length == 0)
			{
				throw new ParseException(this.location, message, inner);
			}
			throw new ParseException(this.location, string.Format(message, parms), inner);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00029831 File Offset: 0x00027A31
		internal void ThrowParseFileNotFound(string path, params object[] parms)
		{
			this.ThrowParseException("The file '" + path + "' does not exist", parms);
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0002984A File Offset: 0x00027A4A
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00029852 File Offset: 0x00027A52
		internal ILocation Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0002985B File Offset: 0x00027A5B
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00029863 File Offset: 0x00027A63
		internal HttpContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0002986C File Offset: 0x00027A6C
		internal string BaseDir
		{
			get
			{
				if (this.baseDir == null)
				{
					this.baseDir = this.MapPath(this.BaseVirtualDir, false);
				}
				return this.baseDir;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0002988F File Offset: 0x00027A8F
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x000298BA File Offset: 0x00027ABA
		internal virtual string BaseVirtualDir
		{
			get
			{
				if (this.baseVDir == null)
				{
					this.baseVDir = VirtualPathUtility.GetDirectory(this.context.Request.FilePath);
				}
				return this.baseVDir;
			}
			set
			{
				if (VirtualPathUtility.IsRooted(value))
				{
					this.baseVDir = VirtualPathUtility.ToAbsolute(value);
					return;
				}
				this.baseVDir = value;
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000298D8 File Offset: 0x00027AD8
		internal TSection GetConfigSection<TSection>(string section) where TSection : ConfigurationSection
		{
			VirtualPath virtualPath = this.VirtualPath;
			string text = ((virtualPath != null) ? virtualPath.Absolute : null);
			if (text == null)
			{
				return WebConfigurationManager.GetSection(section) as TSection;
			}
			return WebConfigurationManager.GetSection(section, text) as TSection;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0002991E File Offset: 0x00027B1E
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00029926 File Offset: 0x00027B26
		internal VirtualPath VirtualPath { get; set; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0002992F File Offset: 0x00027B2F
		internal CompilationSection CompilationConfig
		{
			get
			{
				return this.GetConfigSection<CompilationSection>("system.web/compilation");
			}
		}

		// Token: 0x04001343 RID: 4931
		private HttpContext context;

		// Token: 0x04001344 RID: 4932
		private string baseDir;

		// Token: 0x04001345 RID: 4933
		private string baseVDir;

		// Token: 0x04001346 RID: 4934
		private ILocation location;
	}
}
