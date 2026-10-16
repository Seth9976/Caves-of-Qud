using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Handlers
{
	/// <summary>Provides an HTTP handler used to load embedded resources from assemblies. This class cannot be inherited.</summary>
	// Token: 0x02000101 RID: 257
	public sealed class AssemblyResourceLoader : IHttpHandler
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x000243DC File Offset: 0x000225DC
		private static KeyedHashAlgorithm ReusableHashAlgorithm
		{
			get
			{
				if (!AssemblyResourceLoader.canReuseHashAlg)
				{
					return null;
				}
				if (AssemblyResourceLoader.hashAlg == null)
				{
					MachineKeySection config = MachineKeySection.Config;
					AssemblyResourceLoader.hashAlg = MachineKeySectionUtils.GetValidationAlgorithm(config);
					if (!AssemblyResourceLoader.hashAlg.CanReuseTransform)
					{
						AssemblyResourceLoader.canReuseHashAlg = false;
						AssemblyResourceLoader.hashAlg = null;
						return null;
					}
					AssemblyResourceLoader.hashAlg.Key = MachineKeySectionUtils.GetValidationKey(config);
				}
				if (AssemblyResourceLoader.hashAlg != null)
				{
					AssemblyResourceLoader.hashAlg.Initialize();
				}
				return AssemblyResourceLoader.hashAlg;
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002444C File Offset: 0x0002264C
		private static string GetStringHash(KeyedHashAlgorithm kha, string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return string.Empty;
			}
			string text;
			try
			{
				AssemblyResourceLoader._stringHashCacheLock.EnterUpgradeableReadLock();
				if (AssemblyResourceLoader.stringHashCache.TryGetValue(str, out text))
				{
					return text;
				}
				try
				{
					AssemblyResourceLoader._stringHashCacheLock.EnterWriteLock();
					if (AssemblyResourceLoader.stringHashCache.TryGetValue(str, out text))
					{
						return text;
					}
					text = Convert.ToBase64String(kha.ComputeHash(Encoding.UTF8.GetBytes(str)));
					AssemblyResourceLoader.stringHashCache.Add(str, text);
				}
				finally
				{
					AssemblyResourceLoader._stringHashCacheLock.ExitWriteLock();
				}
			}
			finally
			{
				AssemblyResourceLoader._stringHashCacheLock.ExitUpgradeableReadLock();
			}
			return text;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00024500 File Offset: 0x00022700
		private static void InitEmbeddedResourcesUrls(KeyedHashAlgorithm kha, Assembly assembly, string assemblyName, string assemblyHash, AssemblyResourceLoader.AssemblyEmbeddedResources entry)
		{
			WebResourceAttribute[] array = (WebResourceAttribute[])assembly.GetCustomAttributes(typeof(WebResourceAttribute), false);
			string location = assembly.Location;
			foreach (WebResourceAttribute webResourceAttribute in array)
			{
				string webResource = webResourceAttribute.WebResource;
				if (!string.IsNullOrEmpty(webResource))
				{
					string stringHash = AssemblyResourceLoader.GetStringHash(kha, webResource);
					if (!entry.Resources.ContainsKey(stringHash))
					{
						AssemblyResourceLoader.EmbeddedResource embeddedResource = new AssemblyResourceLoader.EmbeddedResource
						{
							Name = webResource,
							Attribute = webResourceAttribute,
							Url = AssemblyResourceLoader.CreateResourceUrl(kha, assemblyName, assemblyHash, location, stringHash, false, false, true)
						};
						entry.Resources.Add(stringHash, embeddedResource);
					}
				}
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000245A2 File Offset: 0x000227A2
		internal static string GetResourceUrl(Type type, string resourceName)
		{
			return AssemblyResourceLoader.GetResourceUrl(type.Assembly, resourceName, false);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000245B4 File Offset: 0x000227B4
		private static AssemblyResourceLoader.EmbeddedResource DecryptAssemblyResource(string val, out AssemblyResourceLoader.AssemblyEmbeddedResources entry)
		{
			entry = null;
			string[] array = val.Split('_', StringSplitOptions.None);
			if (array.Length != 3)
			{
				return null;
			}
			string text = array[0];
			string text2 = array[1];
			AssemblyResourceLoader.EmbeddedResource embeddedResource;
			try
			{
				AssemblyResourceLoader._embeddedResourcesLock.EnterReadLock();
				AssemblyResourceLoader.EmbeddedResource embeddedResource2;
				if (!AssemblyResourceLoader._embeddedResources.TryGetValue(text, out entry) || entry == null)
				{
					embeddedResource = null;
				}
				else if (!entry.Resources.TryGetValue(text2, out embeddedResource2) || embeddedResource2 == null)
				{
					embeddedResource = null;
				}
				else
				{
					embeddedResource = embeddedResource2;
				}
			}
			finally
			{
				AssemblyResourceLoader._embeddedResourcesLock.ExitReadLock();
			}
			return embeddedResource;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002463C File Offset: 0x0002283C
		private static void GetAssemblyNameAndHashes(KeyedHashAlgorithm kha, Assembly assembly, string resourceName, out string assemblyName, out string assemblyNameHash, out string resourceNameHash)
		{
			assemblyName = ((assembly == AssemblyResourceLoader.currAsm) ? "s" : assembly.GetName().FullName);
			assemblyNameHash = AssemblyResourceLoader.GetStringHash(kha, assemblyName);
			resourceNameHash = AssemblyResourceLoader.GetStringHash(kha, resourceName);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00024674 File Offset: 0x00022874
		private static AssemblyResourceLoader.AssemblyEmbeddedResources GetAssemblyEmbeddedResource(KeyedHashAlgorithm kha, Assembly assembly, string assemblyNameHash, string assemblyName)
		{
			AssemblyResourceLoader.AssemblyEmbeddedResources assemblyEmbeddedResources;
			if (!AssemblyResourceLoader._embeddedResources.TryGetValue(assemblyNameHash, out assemblyEmbeddedResources) || assemblyEmbeddedResources == null)
			{
				try
				{
					AssemblyResourceLoader._embeddedResourcesLock.EnterWriteLock();
					assemblyEmbeddedResources = new AssemblyResourceLoader.AssemblyEmbeddedResources
					{
						AssemblyName = assemblyName
					};
					AssemblyResourceLoader.InitEmbeddedResourcesUrls(kha, assembly, assemblyName, assemblyNameHash, assemblyEmbeddedResources);
					AssemblyResourceLoader._embeddedResources.Add(assemblyNameHash, assemblyEmbeddedResources);
				}
				finally
				{
					AssemblyResourceLoader._embeddedResourcesLock.ExitWriteLock();
				}
			}
			return assemblyEmbeddedResources;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000246E0 File Offset: 0x000228E0
		internal static string GetResourceUrl(Assembly assembly, string resourceName, bool notifyScriptLoaded)
		{
			if (assembly == null)
			{
				return string.Empty;
			}
			KeyedHashAlgorithm keyedHashAlgorithm = AssemblyResourceLoader.ReusableHashAlgorithm;
			if (keyedHashAlgorithm != null)
			{
				return AssemblyResourceLoader.GetResourceUrl(keyedHashAlgorithm, assembly, resourceName, notifyScriptLoaded);
			}
			MachineKeySection config = MachineKeySection.Config;
			KeyedHashAlgorithm validationAlgorithm;
			keyedHashAlgorithm = (validationAlgorithm = MachineKeySectionUtils.GetValidationAlgorithm(config));
			string resourceUrl;
			try
			{
				keyedHashAlgorithm.Key = MachineKeySectionUtils.GetValidationKey(config);
				resourceUrl = AssemblyResourceLoader.GetResourceUrl(keyedHashAlgorithm, assembly, resourceName, notifyScriptLoaded);
			}
			finally
			{
				if (validationAlgorithm != null)
				{
					((IDisposable)validationAlgorithm).Dispose();
				}
			}
			return resourceUrl;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00024754 File Offset: 0x00022954
		private static string GetResourceUrl(KeyedHashAlgorithm kha, Assembly assembly, string resourceName, bool notifyScriptLoaded)
		{
			string text;
			string text2;
			string text3;
			AssemblyResourceLoader.GetAssemblyNameAndHashes(kha, assembly, resourceName, out text, out text2, out text3);
			bool flag = false;
			bool flag2 = true;
			string text5;
			try
			{
				AssemblyResourceLoader._embeddedResourcesLock.EnterUpgradeableReadLock();
				AssemblyResourceLoader.AssemblyEmbeddedResources assemblyEmbeddedResource = AssemblyResourceLoader.GetAssemblyEmbeddedResource(kha, assembly, text2, text);
				string text4 = text3;
				AssemblyResourceLoader.EmbeddedResource embeddedResource;
				if (assemblyEmbeddedResource.Resources.TryGetValue(text4, out embeddedResource) && embeddedResource != null)
				{
					text5 = embeddedResource.Url;
				}
				else
				{
					text5 = null;
				}
			}
			finally
			{
				AssemblyResourceLoader._embeddedResourcesLock.ExitUpgradeableReadLock();
			}
			if (text5 == null)
			{
				text5 = AssemblyResourceLoader.CreateResourceUrl(kha, text, text2, assembly.Location, text3, flag, notifyScriptLoaded, flag2);
			}
			return text5;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000247E4 File Offset: 0x000229E4
		private static string CreateResourceUrl(KeyedHashAlgorithm kha, string assemblyName, string assemblyNameHash, string assemblyPath, string resourceNameHash, bool debug, bool notifyScriptLoaded, bool includeTimeStamp)
		{
			string text = string.Empty;
			string empty = string.Empty;
			if (includeTimeStamp)
			{
				if (!string.IsNullOrEmpty(assemblyPath) && File.Exists(assemblyPath))
				{
					text = "&t=" + File.GetLastWriteTimeUtc(assemblyPath).Ticks.ToString();
				}
				else
				{
					text = "&t=" + DateTime.UtcNow.Ticks.ToString();
				}
			}
			string text2 = HttpUtility.UrlEncode(assemblyNameHash + "_" + resourceNameHash + (debug ? "_t" : "_f"));
			string text3 = "WebResource.axd?d=" + text2 + text + empty;
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest != null)
			{
				text3 = VirtualPathUtility.AppendTrailingSlash(httpRequest.ApplicationPath) + text3;
			}
			return text3;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000248B8 File Offset: 0x00022AB8
		private bool HasIfModifiedSince(HttpRequest request, out DateTime modified)
		{
			string text = request.Headers["If-Modified-Since"];
			if (string.IsNullOrEmpty(text))
			{
				modified = DateTime.MinValue;
				return false;
			}
			try
			{
				if (DateTime.TryParseExact(text, "r", null, DateTimeStyles.None, out modified))
				{
					return true;
				}
			}
			catch
			{
				modified = DateTime.MinValue;
			}
			return false;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00024924 File Offset: 0x00022B24
		private void RespondWithNotModified(HttpContext context)
		{
			HttpResponse response = context.Response;
			response.Clear();
			response.StatusCode = 304;
			response.ContentType = null;
			context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00024950 File Offset: 0x00022B50
		private unsafe void SendEmbeddedResource(HttpContext context, out AssemblyResourceLoader.EmbeddedResource res, out Assembly assembly)
		{
			HttpRequest request = context.Request;
			string text = request.QueryString["d"];
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace(' ', '+');
			}
			AssemblyResourceLoader.AssemblyEmbeddedResources assemblyEmbeddedResources;
			res = AssemblyResourceLoader.DecryptAssemblyResource(text, out assemblyEmbeddedResources);
			WebResourceAttribute webResourceAttribute = ((res != null) ? res.Attribute : null);
			if (webResourceAttribute == null)
			{
				throw new HttpException(404, "Resource not found");
			}
			if (assemblyEmbeddedResources.AssemblyName == "s")
			{
				assembly = AssemblyResourceLoader.currAsm;
			}
			else
			{
				assembly = Assembly.Load(assemblyEmbeddedResources.AssemblyName);
			}
			DateTime dateTime;
			if (this.HasIfModifiedSince(request, out dateTime) && File.GetLastWriteTimeUtc(assembly.Location) <= dateTime)
			{
				this.RespondWithNotModified(context);
				return;
			}
			HttpResponse response = context.Response;
			response.ContentType = webResourceAttribute.ContentType;
			DateTime utcNow = DateTime.UtcNow;
			response.Headers.Add("Last-Modified", utcNow.ToString("r"));
			response.ExpiresAbsolute = utcNow.AddYears(1);
			response.CacheControl = "public";
			Stream manifestResourceStream = assembly.GetManifestResourceStream(res.Name);
			if (manifestResourceStream == null)
			{
				throw new HttpException(404, "Resource " + res.Name + " not found");
			}
			if (webResourceAttribute.PerformSubstitution)
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					TextWriter output = response.Output;
					new AssemblyResourceLoader.PerformSubstitutionHelper(assembly).PerformSubstitution(streamReader, output);
					return;
				}
			}
			if (response.OutputStream is HttpResponseStream)
			{
				UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)manifestResourceStream;
				((HttpResponseStream)response.OutputStream).WritePtr(new IntPtr((void*)unmanagedMemoryStream.PositionPointer), (int)unmanagedMemoryStream.Length);
				return;
			}
			byte[] array = new byte[1024];
			Stream outputStream = response.OutputStream;
			int num;
			do
			{
				num = manifestResourceStream.Read(array, 0, 1024);
				outputStream.Write(array, 0, num);
			}
			while (num > 0);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpHandler.ProcessRequest(System.Web.HttpContext)" />.</summary>
		/// <param name="context">The context of the request.</param>
		/// <exception cref="T:System.Web.HttpException">The Web resource request is invalid.- or -The assembly name could not be found.- or -The resource name could not be found in the assembly.</exception>
		// Token: 0x06000D80 RID: 3456 RVA: 0x00024B4C File Offset: 0x00022D4C
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			AssemblyResourceLoader.EmbeddedResource embeddedResource;
			Assembly assembly;
			this.SendEmbeddedResource(context, out embeddedResource, out assembly);
		}

		/// <summary>Gets a value that indicates whether another request can reuse the <see cref="T:System.Web.IHttpHandler" /> instance. </summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00008B4A File Offset: 0x00006D4A
		bool IHttpHandler.IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400115D RID: 4445
		private const string HandlerFileName = "WebResource.axd";

		// Token: 0x0400115E RID: 4446
		private static Assembly currAsm = typeof(AssemblyResourceLoader).Assembly;

		// Token: 0x0400115F RID: 4447
		private const char QueryParamSeparator = '&';

		// Token: 0x04001160 RID: 4448
		private static readonly Dictionary<string, AssemblyResourceLoader.AssemblyEmbeddedResources> _embeddedResources = new Dictionary<string, AssemblyResourceLoader.AssemblyEmbeddedResources>(StringComparer.Ordinal);

		// Token: 0x04001161 RID: 4449
		private static readonly ReaderWriterLockSlim _embeddedResourcesLock = new ReaderWriterLockSlim();

		// Token: 0x04001162 RID: 4450
		private static readonly ReaderWriterLockSlim _stringHashCacheLock = new ReaderWriterLockSlim();

		// Token: 0x04001163 RID: 4451
		private static readonly Dictionary<string, string> stringHashCache = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x04001164 RID: 4452
		[ThreadStatic]
		private static KeyedHashAlgorithm hashAlg;

		// Token: 0x04001165 RID: 4453
		private static bool canReuseHashAlg = true;

		// Token: 0x02000102 RID: 258
		private sealed class PerformSubstitutionHelper
		{
			// Token: 0x06000D84 RID: 3460 RVA: 0x00024BBD File Offset: 0x00022DBD
			public PerformSubstitutionHelper(Assembly assembly)
			{
				this._assembly = assembly;
			}

			// Token: 0x06000D85 RID: 3461 RVA: 0x00024BCC File Offset: 0x00022DCC
			public void PerformSubstitution(TextReader reader, TextWriter writer)
			{
				for (string text = reader.ReadLine(); text != null; text = reader.ReadLine())
				{
					if (text.Length > 0 && AssemblyResourceLoader.PerformSubstitutionHelper._regex.IsMatch(text))
					{
						text = AssemblyResourceLoader.PerformSubstitutionHelper._regex.Replace(text, new MatchEvaluator(this.PerformSubstitutionReplace));
					}
					writer.WriteLine(text);
				}
			}

			// Token: 0x06000D86 RID: 3462 RVA: 0x00024C24 File Offset: 0x00022E24
			private string PerformSubstitutionReplace(Match m)
			{
				string value = m.Groups[1].Value;
				return AssemblyResourceLoader.GetResourceUrl(this._assembly, value, false);
			}

			// Token: 0x04001166 RID: 4454
			private readonly Assembly _assembly;

			// Token: 0x04001167 RID: 4455
			private static readonly Regex _regex = new Regex("\\<%=[ ]*WebResource[ ]*\\([ ]*\"([^\"]+)\"[ ]*\\)[ ]*%\\>");
		}

		// Token: 0x02000103 RID: 259
		private sealed class EmbeddedResource
		{
			// Token: 0x04001168 RID: 4456
			public string Name;

			// Token: 0x04001169 RID: 4457
			public string Url;

			// Token: 0x0400116A RID: 4458
			public WebResourceAttribute Attribute;
		}

		// Token: 0x02000104 RID: 260
		private sealed class AssemblyEmbeddedResources
		{
			// Token: 0x0400116B RID: 4459
			public string AssemblyName = string.Empty;

			// Token: 0x0400116C RID: 4460
			public Dictionary<string, AssemblyResourceLoader.EmbeddedResource> Resources = new Dictionary<string, AssemblyResourceLoader.EmbeddedResource>(StringComparer.Ordinal);
		}
	}
}
