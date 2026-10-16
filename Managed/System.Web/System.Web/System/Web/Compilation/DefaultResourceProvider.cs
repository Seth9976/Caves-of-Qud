using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x02000649 RID: 1609
	internal sealed class DefaultResourceProvider : IResourceProvider
	{
		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06004532 RID: 17714 RVA: 0x000BB63C File Offset: 0x000B983C
		public IResourceReader ResourceReader
		{
			get
			{
				Assembly assembly;
				string text;
				if (this.isGlobal)
				{
					assembly = HttpContext.AppGlobalResourcesAssembly;
					text = this.resource;
				}
				else
				{
					assembly = this.GetLocalResourcesAssembly();
					text = Path.GetFileName(this.resource);
					if (string.IsNullOrEmpty(text))
					{
						return null;
					}
					text += ".resources";
				}
				if (assembly == null)
				{
					return null;
				}
				Stream manifestResourceStream = assembly.GetManifestResourceStream(text);
				if (manifestResourceStream == null)
				{
					return null;
				}
				return new ResourceReader(manifestResourceStream);
			}
		}

		// Token: 0x06004533 RID: 17715 RVA: 0x000BB6A7 File Offset: 0x000B98A7
		public DefaultResourceProvider(string resource, bool isGlobal)
		{
			if (string.IsNullOrEmpty(resource))
			{
				throw new ArgumentNullException("resource");
			}
			this.resource = resource;
			this.isGlobal = isGlobal;
		}

		// Token: 0x06004534 RID: 17716 RVA: 0x000BB6D0 File Offset: 0x000B98D0
		public object GetObject(string resourceKey, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(resourceKey))
			{
				return null;
			}
			ResourceManager resourceManager = this.GetResourceManager();
			if (resourceManager == null)
			{
				return null;
			}
			return resourceManager.GetObject(resourceKey, culture);
		}

		// Token: 0x06004535 RID: 17717 RVA: 0x000BB6FC File Offset: 0x000B98FC
		private Assembly GetLocalResourcesAssembly()
		{
			string directory = VirtualPathUtility.GetDirectory(this.resource);
			Assembly assembly = AppResourcesCompiler.GetCachedLocalResourcesAssembly(directory);
			if (assembly == null)
			{
				assembly = new AppResourcesCompiler(directory).Compile();
				if (assembly == null)
				{
					throw new MissingManifestResourceException("A resource object was not found at the specified virtualPath.");
				}
			}
			return assembly;
		}

		// Token: 0x06004536 RID: 17718 RVA: 0x000BB748 File Offset: 0x000B9948
		private ResourceManager GetResourceManager()
		{
			Assembly assembly;
			string fileName;
			if (this.isGlobal)
			{
				assembly = HttpContext.AppGlobalResourcesAssembly;
				fileName = this.resource;
			}
			else
			{
				assembly = this.GetLocalResourcesAssembly();
				fileName = Path.GetFileName(this.resource);
				if (string.IsNullOrEmpty(fileName))
				{
					return null;
				}
			}
			if (assembly == null)
			{
				return null;
			}
			ResourceManager resourceManager2;
			try
			{
				if (DefaultResourceProvider.resourceManagerCache == null)
				{
					DefaultResourceProvider.resourceManagerCache = new Dictionary<DefaultResourceProvider.ResourceManagerCacheKey, ResourceManager>();
				}
				DefaultResourceProvider.ResourceManagerCacheKey resourceManagerCacheKey = new DefaultResourceProvider.ResourceManagerCacheKey(fileName, assembly);
				ResourceManager resourceManager;
				if (!DefaultResourceProvider.resourceManagerCache.TryGetValue(resourceManagerCacheKey, out resourceManager))
				{
					resourceManager = new ResourceManager(fileName, assembly);
					resourceManager.IgnoreCase = true;
					DefaultResourceProvider.resourceManagerCache.Add(resourceManagerCacheKey, resourceManager);
				}
				resourceManager2 = resourceManager;
			}
			catch (MissingManifestResourceException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to retrieve the specified global resource object.", ex);
			}
			return resourceManager2;
		}

		// Token: 0x040024DC RID: 9436
		[ThreadStatic]
		private static Dictionary<DefaultResourceProvider.ResourceManagerCacheKey, ResourceManager> resourceManagerCache;

		// Token: 0x040024DD RID: 9437
		private string resource;

		// Token: 0x040024DE RID: 9438
		private bool isGlobal;

		// Token: 0x0200064A RID: 1610
		private sealed class ResourceManagerCacheKey
		{
			// Token: 0x06004537 RID: 17719 RVA: 0x000BB810 File Offset: 0x000B9A10
			public ResourceManagerCacheKey(string name, Assembly asm)
			{
				this._name = name;
				this._asm = asm;
			}

			// Token: 0x06004538 RID: 17720 RVA: 0x000BB828 File Offset: 0x000B9A28
			public override bool Equals(object obj)
			{
				if (!(obj is DefaultResourceProvider.ResourceManagerCacheKey))
				{
					return false;
				}
				DefaultResourceProvider.ResourceManagerCacheKey resourceManagerCacheKey = (DefaultResourceProvider.ResourceManagerCacheKey)obj;
				return resourceManagerCacheKey._asm == this._asm && this._name.Equals(resourceManagerCacheKey._name, StringComparison.Ordinal);
			}

			// Token: 0x06004539 RID: 17721 RVA: 0x000BB86D File Offset: 0x000B9A6D
			public override int GetHashCode()
			{
				return this._name.GetHashCode() + this._asm.GetHashCode();
			}

			// Token: 0x040024DF RID: 9439
			private readonly string _name;

			// Token: 0x040024E0 RID: 9440
			private readonly Assembly _asm;
		}
	}
}
