using System;
using System.IO;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000E6 RID: 230
	internal class VirtualPath : IDisposable
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0002086C File Offset: 0x0001EA6C
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x00020874 File Offset: 0x0001EA74
		public bool IsAbsolute { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0002087D File Offset: 0x0001EA7D
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00020885 File Offset: 0x0001EA85
		public bool IsFake { get; private set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0002088E File Offset: 0x0001EA8E
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00020896 File Offset: 0x0001EA96
		public bool IsRooted { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0002089F File Offset: 0x0001EA9F
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x000208A7 File Offset: 0x0001EAA7
		public bool IsAppRelative { get; private set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x000208B0 File Offset: 0x0001EAB0
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x000208B8 File Offset: 0x0001EAB8
		public string Original { get; private set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000208C4 File Offset: 0x0001EAC4
		public string Absolute
		{
			get
			{
				if (this.IsAbsolute)
				{
					return this.Original;
				}
				if (this._absolute == null)
				{
					string original = this.Original;
					if (!VirtualPathUtility.IsRooted(original))
					{
						this._absolute = this.MakeRooted(original);
					}
					else
					{
						this._absolute = original;
					}
					if (VirtualPathUtility.IsAppRelative(this._absolute))
					{
						this._absolute = VirtualPathUtility.ToAbsolute(this._absolute);
					}
				}
				return this._absolute;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00020934 File Offset: 0x0001EB34
		public string AppRelative
		{
			get
			{
				if (this.IsAppRelative)
				{
					return this.Original;
				}
				if (this._appRelative == null)
				{
					string original = this.Original;
					if (!VirtualPathUtility.IsRooted(original))
					{
						this._appRelative = this.MakeRooted(original);
					}
					else
					{
						this._appRelative = original;
					}
					if (VirtualPathUtility.IsAbsolute(this._appRelative))
					{
						this._appRelative = VirtualPathUtility.ToAppRelative(this._appRelative);
					}
				}
				return this._appRelative;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000209A1 File Offset: 0x0001EBA1
		public string AppRelativeNotRooted
		{
			get
			{
				if (this._appRelativeNotRooted == null)
				{
					this._appRelativeNotRooted = this.AppRelative.Substring(2);
				}
				return this._appRelativeNotRooted;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x000209C3 File Offset: 0x0001EBC3
		public string Extension
		{
			get
			{
				if (this._extension == null)
				{
					this._extension = VirtualPathUtility.GetExtension(this.Original);
				}
				return this._extension;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000209E4 File Offset: 0x0001EBE4
		public string Directory
		{
			get
			{
				if (this._directory == null)
				{
					this._directory = VirtualPathUtility.GetDirectory(this.Absolute);
				}
				return this._directory;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00020A05 File Offset: 0x0001EC05
		public string DirectoryNoNormalize
		{
			get
			{
				if (this._directoryNoNormalize == null)
				{
					this._directoryNoNormalize = VirtualPathUtility.GetDirectory(this.Absolute, false);
				}
				return this._directoryNoNormalize;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00020A28 File Offset: 0x0001EC28
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00020A67 File Offset: 0x0001EC67
		public string CurrentRequestDirectory
		{
			get
			{
				if (this._currentRequestDirectory != null)
				{
					return this._currentRequestDirectory;
				}
				HttpContext httpContext = HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest != null)
				{
					return VirtualPathUtility.GetDirectory(httpRequest.CurrentExecutionFilePath);
				}
				return null;
			}
			set
			{
				this._currentRequestDirectory = value;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00020A70 File Offset: 0x0001EC70
		public string PhysicalPath
		{
			get
			{
				if (this._physicalPath != null)
				{
					return this._physicalPath;
				}
				HttpContext httpContext = HttpContext.Current;
				HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
				if (httpRequest != null)
				{
					this._physicalPath = httpRequest.MapPath(this.Absolute);
					return this._physicalPath;
				}
				return null;
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00020ABE File Offset: 0x0001ECBE
		public VirtualPath(string vpath)
			: this(vpath, null, false)
		{
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00020AC9 File Offset: 0x0001ECC9
		public VirtualPath(string vpath, string baseVirtualDir)
			: this(vpath, null, false)
		{
			this.CurrentRequestDirectory = baseVirtualDir;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00020ADC File Offset: 0x0001ECDC
		public VirtualPath(string vpath, string physicalPath, bool isFake)
		{
			this.IsRooted = VirtualPathUtility.IsRooted(vpath);
			this.IsAbsolute = VirtualPathUtility.IsAbsolute(vpath);
			this.IsAppRelative = VirtualPathUtility.IsAppRelative(vpath);
			if (!isFake)
			{
				this.Original = vpath;
				this.IsFake = false;
				return;
			}
			if (string.IsNullOrEmpty(physicalPath))
			{
				throw new ArgumentException("physicalPath");
			}
			this._physicalPath = physicalPath;
			this.Original = "~/" + Path.GetFileName(this._physicalPath);
			this.IsFake = true;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00020B61 File Offset: 0x0001ED61
		public bool StartsWith(string s)
		{
			return StrUtils.StartsWith(this.Original, s);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00020B70 File Offset: 0x0001ED70
		private string MakeRooted(string original)
		{
			string currentRequestDirectory = this.CurrentRequestDirectory;
			if (!string.IsNullOrEmpty(currentRequestDirectory))
			{
				return VirtualPathUtility.Combine(currentRequestDirectory, original);
			}
			return VirtualPathUtility.Combine(HttpRuntime.AppDomainAppVirtualPath, original);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00020B9F File Offset: 0x0001ED9F
		public void Dispose()
		{
			this._absolute = null;
			this._appRelative = null;
			this._appRelativeNotRooted = null;
			this._extension = null;
			this._directory = null;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00020BC4 File Offset: 0x0001EDC4
		public override string ToString()
		{
			string text = this.Original;
			if (string.IsNullOrEmpty(text))
			{
				return base.GetType().ToString();
			}
			if (this.IsFake)
			{
				text = text + " [fake: " + this.PhysicalPath + "]";
			}
			return text;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public static VirtualPath PhysicalToVirtual(string physical_path)
		{
			if (string.IsNullOrEmpty(physical_path))
			{
				return null;
			}
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (!StrUtils.StartsWith(physical_path, appDomainAppPath))
			{
				return null;
			}
			string text = physical_path.Substring(appDomainAppPath.Length - 1);
			if (text[0] != '/')
			{
				return null;
			}
			return new VirtualPath(text);
		}

		// Token: 0x04001105 RID: 4357
		private string _absolute;

		// Token: 0x04001106 RID: 4358
		private string _appRelative;

		// Token: 0x04001107 RID: 4359
		private string _appRelativeNotRooted;

		// Token: 0x04001108 RID: 4360
		private string _extension;

		// Token: 0x04001109 RID: 4361
		private string _directory;

		// Token: 0x0400110A RID: 4362
		private string _directoryNoNormalize;

		// Token: 0x0400110B RID: 4363
		private string _currentRequestDirectory;

		// Token: 0x0400110C RID: 4364
		private string _physicalPath;
	}
}
