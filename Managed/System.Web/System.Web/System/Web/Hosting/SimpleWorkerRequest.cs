using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	/// <summary>Provides a simple implementation of the <see cref="T:System.Web.HttpWorkerRequest" /> abstract class that can be used to host ASP.NET applications outside an Internet Information Services (IIS) application. You can employ SimpleWorkerRequest directly or extend it.</summary>
	// Token: 0x02000551 RID: 1361
	[ComVisible(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class SimpleWorkerRequest : HttpWorkerRequest
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.SimpleWorkerRequest" /> class when the target application domain has been created using the <see cref="M:System.Web.Hosting.ApplicationHost.CreateApplicationHost(System.Type,System.String,System.String)" /> method.</summary>
		/// <param name="page">The page to be requested (or the virtual path to the page, relative to the application directory). </param>
		/// <param name="query">The text of the query string. </param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> that captures output from the response </param>
		// Token: 0x06003ACD RID: 15053 RVA: 0x0009C14A File Offset: 0x0009A34A
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SimpleWorkerRequest(string page, string query, TextWriter output)
		{
			this.page = page;
			this.query = query;
			this.output = output;
			this.app_virtual_dir = HttpRuntime.AppDomainAppVirtualPath;
			this.app_physical_dir = HttpRuntime.AppDomainAppPath;
			this.hosted = true;
			this.InitializePaths();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.SimpleWorkerRequest" /> class for use in an arbitrary application domain, when the user code creates an <see cref="T:System.Web.HttpContext" /> (passing the SimpleWorkerRequest as an argument to the HttpContext constructor).</summary>
		/// <param name="appVirtualDir">The virtual path to the application directory; for example, "/app". </param>
		/// <param name="appPhysicalDir">The physical path to the application directory; for example, "c:\app". </param>
		/// <param name="page">The virtual path for the request (relative to the application directory). </param>
		/// <param name="query">The text of the query string. </param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> that captures the output from the response. </param>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="appVirtualDir" /> parameter cannot be overridden in this context.</exception>
		// Token: 0x06003ACE RID: 15054 RVA: 0x0009C18A File Offset: 0x0009A38A
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SimpleWorkerRequest(string appVirtualDir, string appPhysicalDir, string page, string query, TextWriter output)
		{
			this.page = page;
			this.query = query;
			this.output = output;
			this.app_virtual_dir = appVirtualDir;
			this.app_physical_dir = appPhysicalDir;
			this.InitializePaths();
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x0009C1C0 File Offset: 0x0009A3C0
		private void InitializePaths()
		{
			int num = this.page.IndexOf('/');
			if (num >= 0)
			{
				this.path_info = this.page.Substring(num);
				this.page = this.page.Substring(0, num);
				return;
			}
			this.path_info = "";
		}

		/// <summary>Gets the full physical path to the Machine.config file.</summary>
		/// <returns>The physical path to the Machine.config file.</returns>
		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x0009C210 File Offset: 0x0009A410
		public override string MachineConfigPath
		{
			get
			{
				if (this.hosted)
				{
					string machineConfigPath = ICalls.GetMachineConfigPath();
					if (SecurityManager.SecurityEnabled && machineConfigPath != null && machineConfigPath.Length > 0)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, machineConfigPath).Demand();
					}
					return machineConfigPath;
				}
				return null;
			}
		}

		/// <summary>Gets the physical path to the directory where the ASP.NET binaries are installed.</summary>
		/// <returns>The physical directory to the ASP.NET binary files.</returns>
		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x0009C250 File Offset: 0x0009A450
		public override string MachineInstallDirectory
		{
			get
			{
				if (this.hosted)
				{
					string machineInstallDirectory = ICalls.GetMachineInstallDirectory();
					if (SecurityManager.SecurityEnabled && machineInstallDirectory != null && machineInstallDirectory.Length > 0)
					{
						new FileIOPermission(FileIOPermissionAccess.PathDiscovery, machineInstallDirectory).Demand();
					}
					return machineInstallDirectory;
				}
				return null;
			}
		}

		/// <summary>Gets the full physical path to the root Web.config file.</summary>
		/// <returns>The physical path to the root Web.config file.</returns>
		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x0009C28D File Offset: 0x0009A48D
		public override string RootWebConfigPath
		{
			get
			{
				return WebConfigurationManager.OpenWebConfiguration("~").FilePath;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Web.HttpWorkerRequest" /> that request processing for the current request is complete.</summary>
		// Token: 0x06003AD3 RID: 15059 RVA: 0x00003946 File Offset: 0x00001B46
		public override void EndOfRequest()
		{
		}

		/// <summary>Sends all pending response data to the client.</summary>
		/// <param name="finalFlush">true if this is the last time response data will be flushed; otherwise, false. </param>
		// Token: 0x06003AD4 RID: 15060 RVA: 0x00003946 File Offset: 0x00001B46
		public override void FlushResponse(bool finalFlush)
		{
		}

		/// <summary>Returns the virtual path to the currently executing server application.</summary>
		/// <returns>The virtual path of the current application.</returns>
		// Token: 0x06003AD5 RID: 15061 RVA: 0x0009C29E File Offset: 0x0009A49E
		public override string GetAppPath()
		{
			return this.app_virtual_dir;
		}

		/// <summary>Returns the UNC-translated path to the currently executing server application.</summary>
		/// <returns>The physical path of the current application.</returns>
		// Token: 0x06003AD6 RID: 15062 RVA: 0x0009C2A6 File Offset: 0x0009A4A6
		public override string GetAppPathTranslated()
		{
			if (SecurityManager.SecurityEnabled && this.app_physical_dir != null && this.app_physical_dir.Length > 0)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, this.app_physical_dir).Demand();
			}
			return this.app_physical_dir;
		}

		/// <summary>Returns the physical path to the requested URI.</summary>
		/// <returns>The physical path to the requested URI.</returns>
		// Token: 0x06003AD7 RID: 15063 RVA: 0x0009C2DC File Offset: 0x0009A4DC
		public override string GetFilePath()
		{
			string text = UrlUtils.Combine(this.app_virtual_dir, this.page);
			if (!(text == ""))
			{
				return text;
			}
			if (!(this.app_virtual_dir == "/"))
			{
				return this.app_virtual_dir + "/";
			}
			return this.app_virtual_dir;
		}

		/// <summary>Returns the physical file path to the requested URI (and translates it from virtual path to physical path: for example, "/proj1/page.aspx" to "c:\dir\page.aspx") </summary>
		/// <returns>The translated physical file path to the requested URI.</returns>
		// Token: 0x06003AD8 RID: 15064 RVA: 0x0009C334 File Offset: 0x0009A534
		public override string GetFilePathTranslated()
		{
			string text;
			if (Path.DirectorySeparatorChar == '\\')
			{
				text = this.page.Replace('/', '\\');
			}
			else
			{
				text = this.page;
			}
			string text2 = Path.Combine(this.app_physical_dir, text);
			if (SecurityManager.SecurityEnabled && text2 != null && text2.Length > 0)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text2).Demand();
			}
			return text2;
		}

		/// <summary>Returns the HTTP request verb.</summary>
		/// <returns>The HTTP verb for this request.</returns>
		// Token: 0x06003AD9 RID: 15065 RVA: 0x0009C390 File Offset: 0x0009A590
		public override string GetHttpVerbName()
		{
			return "GET";
		}

		/// <summary>Returns the HTTP version string of the request (for example, "HTTP/1.1").</summary>
		/// <returns>The HTTP version string returned in the request header.</returns>
		// Token: 0x06003ADA RID: 15066 RVA: 0x0009C397 File Offset: 0x0009A597
		public override string GetHttpVersion()
		{
			return "HTTP/1.0";
		}

		/// <summary>Returns the server IP address of the interface on which the request was received.</summary>
		/// <returns>The server IP address of the interface on which the request was received.</returns>
		// Token: 0x06003ADB RID: 15067 RVA: 0x0009C39E File Offset: 0x0009A59E
		public override string GetLocalAddress()
		{
			return "127.0.0.1";
		}

		/// <summary>Returns the port number on which the request was received.</summary>
		/// <returns>The server port number on which the request was received.</returns>
		// Token: 0x06003ADC RID: 15068 RVA: 0x000892C8 File Offset: 0x000874C8
		public override int GetLocalPort()
		{
			return 80;
		}

		/// <summary>Returns additional path information for a resource with a URL extension. That is, for the path /virdir/page.html/tail, the return value is /tail.</summary>
		/// <returns>Additional path information for a resource.</returns>
		// Token: 0x06003ADD RID: 15069 RVA: 0x0009C3A5 File Offset: 0x0009A5A5
		public override string GetPathInfo()
		{
			return this.path_info;
		}

		/// <summary>Returns the query string specified in the request URL.</summary>
		/// <returns>The request query string.</returns>
		// Token: 0x06003ADE RID: 15070 RVA: 0x0009C3AD File Offset: 0x0009A5AD
		public override string GetQueryString()
		{
			return this.query;
		}

		/// <summary>Returns the URL path contained in the header with the query string appended.</summary>
		/// <returns>The raw URL path of the request header.NoteThe returned URL is not normalized. Using the URL for access control, or security-sensitive decisions can expose your application to canonicalization security vulnerabilities.</returns>
		// Token: 0x06003ADF RID: 15071 RVA: 0x0009C3B8 File Offset: 0x0009A5B8
		public override string GetRawUrl()
		{
			if (this.raw_url == null)
			{
				string text = ((this.query == null || this.query == "") ? "" : ("?" + this.query));
				this.raw_url = UrlUtils.Combine(this.app_virtual_dir, this.page);
				if (this.path_info != "")
				{
					this.raw_url = this.raw_url + "/" + this.path_info + text;
				}
				else
				{
					this.raw_url += text;
				}
			}
			return this.raw_url;
		}

		/// <summary>Returns the IP address of the client.</summary>
		/// <returns>The client's IP address.</returns>
		// Token: 0x06003AE0 RID: 15072 RVA: 0x0009C39E File Offset: 0x0009A59E
		public override string GetRemoteAddress()
		{
			return "127.0.0.1";
		}

		/// <summary>Returns the client's port number.</summary>
		/// <returns>The client's port number.</returns>
		// Token: 0x06003AE1 RID: 15073 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override int GetRemotePort()
		{
			return 0;
		}

		/// <summary>Returns a single server variable from a dictionary of server variables associated with the request.</summary>
		/// <returns>The requested server variable.</returns>
		/// <param name="name">The name of the requested server variable. </param>
		// Token: 0x06003AE2 RID: 15074 RVA: 0x00018A33 File Offset: 0x00016C33
		public override string GetServerVariable(string name)
		{
			return "";
		}

		/// <summary>Returns the virtual path to the requested URI.</summary>
		/// <returns>The path to the requested URI.</returns>
		// Token: 0x06003AE3 RID: 15075 RVA: 0x0009C464 File Offset: 0x0009A664
		public override string GetUriPath()
		{
			if (this.app_virtual_dir == "/")
			{
				return this.app_virtual_dir + this.page + this.path_info;
			}
			return this.app_virtual_dir + "/" + this.page + this.path_info;
		}

		/// <summary>Returns the client's impersonation token.</summary>
		/// <returns>A value representing the client's impersonation token. The default is <see cref="F:System.IntPtr.Zero" />.</returns>
		// Token: 0x06003AE4 RID: 15076 RVA: 0x00018A5F File Offset: 0x00016C5F
		public override IntPtr GetUserToken()
		{
			return IntPtr.Zero;
		}

		/// <summary>Returns the physical path corresponding to the specified virtual path.</summary>
		/// <returns>The physical path that corresponds to the virtual path specified in the <paramref name="path" /> parameter.</returns>
		/// <param name="path">The virtual path. </param>
		// Token: 0x06003AE5 RID: 15077 RVA: 0x0009C4B8 File Offset: 0x0009A6B8
		public override string MapPath(string path)
		{
			if (!this.hosted)
			{
				return null;
			}
			if (path != null && path.Length == 0)
			{
				return this.app_physical_dir;
			}
			if (!path.StartsWith(this.app_virtual_dir))
			{
				throw new ArgumentNullException("path is not rooted in the virtual directory");
			}
			string text = path.Substring(this.app_virtual_dir.Length);
			if (text.Length > 0 && text[0] == '/')
			{
				text = text.Substring(1);
			}
			if (Path.DirectorySeparatorChar != '/')
			{
				text = text.Replace('/', Path.DirectorySeparatorChar);
			}
			return Path.Combine(this.app_physical_dir, text);
		}

		/// <summary>Adds a standard HTTP header to the response.</summary>
		/// <param name="index">The header index. For example, <see cref="F:System.Web.HttpWorkerRequest.HeaderContentLength" />. </param>
		/// <param name="value">The header value. </param>
		// Token: 0x06003AE6 RID: 15078 RVA: 0x00003946 File Offset: 0x00001B46
		public override void SendKnownResponseHeader(int index, string value)
		{
		}

		/// <summary>Adds the contents of the file with the specified handle to the response and specifies the starting position in the file and the number of bytes to send.</summary>
		/// <param name="handle">The handle of the file to send. </param>
		/// <param name="offset">The starting position in the file. </param>
		/// <param name="length">The number of bytes to send. </param>
		// Token: 0x06003AE7 RID: 15079 RVA: 0x00003946 File Offset: 0x00001B46
		public override void SendResponseFromFile(IntPtr handle, long offset, long length)
		{
		}

		/// <summary>Adds the contents of the file with the specified name to the response and specifies the starting position in the file and the number of bytes to send.</summary>
		/// <param name="filename">The name of the file to send. </param>
		/// <param name="offset">The starting position in the file. </param>
		/// <param name="length">The number of bytes to send. </param>
		// Token: 0x06003AE8 RID: 15080 RVA: 0x00003946 File Offset: 0x00001B46
		public override void SendResponseFromFile(string filename, long offset, long length)
		{
		}

		/// <summary>Adds the contents of a byte array to the response and specifies the number of bytes to send.</summary>
		/// <param name="data">The byte array to send. </param>
		/// <param name="length">The number of bytes to send. </param>
		// Token: 0x06003AE9 RID: 15081 RVA: 0x0009C54B File Offset: 0x0009A74B
		public override void SendResponseFromMemory(byte[] data, int length)
		{
			this.output.Write(Encoding.Default.GetChars(data, 0, length));
		}

		/// <summary>Specifies the HTTP status code and status description of the response; for example, SendStatus(200, "Ok").</summary>
		/// <param name="statusCode">The status code to send </param>
		/// <param name="statusDescription">The status description to send. </param>
		// Token: 0x06003AEA RID: 15082 RVA: 0x00003946 File Offset: 0x00001B46
		public override void SendStatus(int statusCode, string statusDescription)
		{
		}

		/// <summary>Adds a nonstandard HTTP header to the response.</summary>
		/// <param name="name">The name of the header to send.</param>
		/// <param name="value">The value of the header.</param>
		// Token: 0x06003AEB RID: 15083 RVA: 0x00003946 File Offset: 0x00001B46
		public override void SendUnknownResponseHeader(string name, string value)
		{
		}

		// Token: 0x04001FE5 RID: 8165
		private string page;

		// Token: 0x04001FE6 RID: 8166
		private string query;

		// Token: 0x04001FE7 RID: 8167
		private string app_virtual_dir;

		// Token: 0x04001FE8 RID: 8168
		private string app_physical_dir;

		// Token: 0x04001FE9 RID: 8169
		private string path_info;

		// Token: 0x04001FEA RID: 8170
		private TextWriter output;

		// Token: 0x04001FEB RID: 8171
		private bool hosted;

		// Token: 0x04001FEC RID: 8172
		private string raw_url;
	}
}
