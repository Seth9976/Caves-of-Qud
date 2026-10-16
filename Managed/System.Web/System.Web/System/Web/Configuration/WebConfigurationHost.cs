using System;
using System.Configuration;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x020005EA RID: 1514
	internal class WebConfigurationHost : IInternalConfigHost
	{
		// Token: 0x0600419A RID: 16794 RVA: 0x000A95A8 File Offset: 0x000A77A8
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return new WebContext(WebApplicationLevel.AtApplication, "", "", configPath, locationSubPath);
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000A95BD File Offset: 0x000A77BD
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return new HttpConfigurationContext(configPath);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000A95C5 File Offset: 0x000A77C5
		public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			if (protectedSection == null)
			{
				throw new ArgumentNullException("protectedSection");
			}
			return protectedSection.EncryptSection(encryptedXml, protectionProvider);
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000A95DD File Offset: 0x000A77DD
		public virtual void DeleteStream(string streamName)
		{
			File.Delete(streamName);
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000A95C5 File Offset: 0x000A77C5
		public virtual string EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
		{
			if (protectedSection == null)
			{
				throw new ArgumentNullException("protectedSection");
			}
			return protectedSection.EncryptSection(clearXml, protectionProvider);
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000A95E8 File Offset: 0x000A77E8
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			if (!string.IsNullOrEmpty(locationSubPath) && !string.IsNullOrEmpty(configPath))
			{
				string text = ((configPath.Length == 1) ? null : (configPath.Substring(1) + "/"));
				if (text != null && locationSubPath.StartsWith(text, StringComparison.Ordinal))
				{
					locationSubPath = locationSubPath.Substring(text.Length);
				}
			}
			string text2 = configPath + "/" + locationSubPath;
			if (!string.IsNullOrEmpty(text2) && text2[0] == '/')
			{
				return text2.Substring(1);
			}
			return text2;
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000A9667 File Offset: 0x000A7867
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			Type type = HttpApplication.LoadType(typeName);
			if (type == null && throwOnError)
			{
				throw new ConfigurationErrorsException("Type not found: '" + typeName + "'");
			}
			return type;
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000A9690 File Offset: 0x000A7890
		public virtual string GetConfigTypeName(Type t)
		{
			return t.AssemblyQualifiedName;
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000A9698 File Offset: 0x000A7898
		public virtual string GetStreamName(string configPath)
		{
			if (configPath == ":machine:")
			{
				if (this.map == null)
				{
					return RuntimeEnvironment.SystemConfigurationFile;
				}
				return this.map.MachineConfigFilename;
			}
			else
			{
				if (configPath == ":web:")
				{
					string text;
					if (this.map == null)
					{
						text = Path.GetDirectoryName(RuntimeEnvironment.SystemConfigurationFile);
					}
					else
					{
						text = Path.GetDirectoryName(this.map.MachineConfigFilename);
					}
					return WebConfigurationHost.GetWebConfigFileName(text);
				}
				return WebConfigurationHost.GetWebConfigFileName(this.MapPath(configPath));
			}
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual object GetStreamVersion(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void Init(IInternalConfigRoot root, params object[] hostInitParams)
		{
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000A9714 File Offset: 0x000A7914
		public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			string text = (string)hostInitConfigurationParams[1];
			this.map = (WebConfigurationFileMap)hostInitConfigurationParams[0];
			bool flag = false;
			if (hostInitConfigurationParams.Length > 7 && hostInitConfigurationParams[7] is bool)
			{
				flag = (bool)hostInitConfigurationParams[7];
			}
			if (flag)
			{
				this.appVirtualPath = text;
			}
			else
			{
				this.appVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			}
			if (locationSubPath == ":web:")
			{
				locationSubPath = ":machine:";
				configPath = ":web:";
				locationConfigPath = null;
				return;
			}
			if (locationSubPath == ":machine:")
			{
				locationSubPath = null;
				configPath = ":machine:";
				locationConfigPath = null;
				return;
			}
			if (locationSubPath == null)
			{
				configPath = text;
				if (configPath.Length > 1)
				{
					configPath = VirtualPathUtility.RemoveTrailingSlash(configPath);
				}
			}
			else
			{
				configPath = locationSubPath;
			}
			int num;
			if (configPath == HttpRuntime.AppDomainAppVirtualPath || configPath == "/")
			{
				num = -1;
			}
			else
			{
				num = configPath.LastIndexOf("/");
			}
			if (num == -1)
			{
				locationSubPath = ":web:";
				locationConfigPath = null;
				return;
			}
			locationConfigPath = configPath.Substring(num + 1);
			if (num == 0)
			{
				locationSubPath = "/";
				return;
			}
			locationSubPath = text.Substring(0, num);
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000A9828 File Offset: 0x000A7A28
		public string MapPath(string virtualPath)
		{
			if (!string.IsNullOrEmpty(virtualPath) && virtualPath.StartsWith("/@@MonoFakeVirtualPath@@", StringComparison.Ordinal))
			{
				return HttpRuntime.AppDomainAppPath;
			}
			if (this.map != null)
			{
				return this.MapPathFromMapper(virtualPath);
			}
			if (HttpContext.Current != null && HttpContext.Current.Request != null)
			{
				return HttpContext.Current.Request.MapPath(virtualPath);
			}
			if (HttpRuntime.AppDomainAppVirtualPath == null || !virtualPath.StartsWith(HttpRuntime.AppDomainAppVirtualPath))
			{
				return virtualPath;
			}
			if (virtualPath == HttpRuntime.AppDomainAppVirtualPath)
			{
				return HttpRuntime.AppDomainAppPath;
			}
			return UrlUtils.Combine(HttpRuntime.AppDomainAppPath, virtualPath.Substring(HttpRuntime.AppDomainAppVirtualPath.Length));
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000A98C8 File Offset: 0x000A7AC8
		public string NormalizeVirtualPath(string virtualPath)
		{
			if (virtualPath == null || virtualPath.Length == 0)
			{
				virtualPath = ".";
			}
			else
			{
				virtualPath = virtualPath.Trim();
			}
			if (virtualPath[0] == '~' && virtualPath.Length > 2 && virtualPath[1] == '/')
			{
				virtualPath = virtualPath.Substring(1);
			}
			if (Path.DirectorySeparatorChar != '/')
			{
				virtualPath = virtualPath.Replace(Path.DirectorySeparatorChar, '/');
			}
			if (UrlUtils.IsRooted(virtualPath))
			{
				virtualPath = UrlUtils.Canonic(virtualPath);
			}
			else if (this.map.VirtualDirectories.Count > 0)
			{
				virtualPath = UrlUtils.Combine(this.map.VirtualDirectories[0].VirtualDirectory, virtualPath);
				virtualPath = UrlUtils.Canonic(virtualPath);
			}
			return virtualPath;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000A9980 File Offset: 0x000A7B80
		public string MapPathFromMapper(string virtualPath)
		{
			string text = this.NormalizeVirtualPath(virtualPath);
			for (int i = 0; i < this.map.VirtualDirectories.Count; i++)
			{
				VirtualDirectoryMapping virtualDirectoryMapping = this.map.VirtualDirectories.Get(i);
				if (text.StartsWith(virtualDirectoryMapping.VirtualDirectory))
				{
					int length = virtualDirectoryMapping.VirtualDirectory.Length;
					if (text.Length == length)
					{
						return virtualDirectoryMapping.PhysicalDirectory;
					}
					if (text[length] == '/')
					{
						string text2 = text.Substring(length + 1).Replace('/', Path.DirectorySeparatorChar);
						return Path.Combine(virtualDirectoryMapping.PhysicalDirectory, text2);
					}
				}
			}
			throw new HttpException("Invalid virtual directory: " + virtualPath);
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000A9A30 File Offset: 0x000A7C30
		internal static string GetWebConfigFileName(string dir)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			if (currentDomain.GetData(".:!MonoAspNetHostedApp!:.") as string == "yes")
			{
				return ApplicationHost.FindWebConfig(dir);
			}
			string fileName = Path.GetFileName((Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).Location);
			string[] array = new string[]
			{
				fileName + ".config",
				fileName + ".Config"
			};
			string baseDirectory = currentDomain.BaseDirectory;
			foreach (string text in array)
			{
				string text2 = Path.Combine(baseDirectory, text);
				if (File.Exists(text2))
				{
					return text2;
				}
			}
			return null;
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000A9AD8 File Offset: 0x000A7CD8
		public virtual bool IsAboveApplication(string configPath)
		{
			return !configPath.Contains(HttpRuntime.AppDomainAppPath);
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000A9AE8 File Offset: 0x000A7CE8
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			if (allowDefinition == ConfigurationAllowDefinition.MachineOnly)
			{
				return configPath == ":machine:" || configPath == ":web:";
			}
			if (allowDefinition != ConfigurationAllowDefinition.MachineToWebRoot && allowDefinition != ConfigurationAllowDefinition.MachineToApplication)
			{
				return true;
			}
			if (string.IsNullOrEmpty(configPath))
			{
				return true;
			}
			string text;
			if (VirtualPathUtility.IsRooted(configPath))
			{
				text = VirtualPathUtility.Normalize(configPath);
			}
			else
			{
				text = configPath;
			}
			return string.Compare(text, ":machine:", StringComparison.Ordinal) == 0 || string.Compare(text, ":web:", StringComparison.Ordinal) == 0 || string.Compare(text, this.appVirtualPath) == 0 || this.IsApplication(text);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x00008B4A File Offset: 0x00006D4A
		[global::System.MonoTODO("Should return false in case strPath points to the root of an application.")]
		internal bool IsApplication(string strPath)
		{
			return true;
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual bool IsFile(string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual bool IsLocationApplicable(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000A9B76 File Offset: 0x000A7D76
		public virtual Stream OpenStreamForRead(string streamName)
		{
			if (!File.Exists(streamName))
			{
				return null;
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read);
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000A9B8A File Offset: 0x000A7D8A
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			if (!this.IsAboveApplication(streamName))
			{
				WebConfigurationManager.SuppressAppReload(true);
			}
			return new FileStream(streamName, FileMode.Create, FileAccess.Write);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x00003A2B File Offset: 0x00001C2B
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000A9BA4 File Offset: 0x000A7DA4
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			if (!this.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
			{
				throw new ConfigurationErrorsException("The section can't be defined in this file (the allowed definition context is '" + allowDefinition.ToString() + "').", errorInfo.Filename, errorInfo.LineNumber);
			}
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000A9BE1 File Offset: 0x000A7DE1
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
			this.WriteCompleted(streamName, success, writeContext, false);
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000A9BED File Offset: 0x000A7DED
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			if (!this.IsAboveApplication(streamName))
			{
				WebConfigurationManager.SuppressAppReload(true);
			}
		}

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x060041BF RID: 16831 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool SupportsLocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool SupportsPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool SupportsRefresh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x00008A4D File Offset: 0x00006C4D
		[global::System.MonoTODO("Always returns false")]
		public virtual bool IsRemote
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsSecondaryRoot(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002343 RID: 9027
		private WebConfigurationFileMap map;

		// Token: 0x04002344 RID: 9028
		private const string MachinePath = ":machine:";

		// Token: 0x04002345 RID: 9029
		private const string MachineWebPath = ":web:";

		// Token: 0x04002346 RID: 9030
		private string appVirtualPath;
	}
}
