using System;
using System.Collections;
using System.IO;
using System.Web.Caching;

namespace System.Web.Hosting
{
	/// <summary>Provides a set of methods that enable a Web application to retrieve resources from a virtual file system.</summary>
	// Token: 0x02000555 RID: 1365
	public abstract class VirtualPathProvider : MarshalByRefObject
	{
		/// <summary>Gets a reference to a previously registered <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object in the compilation system.</summary>
		/// <returns>The next <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object in the compilation system.</returns>
		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x0009C592 File Offset: 0x0009A792
		protected internal VirtualPathProvider Previous
		{
			get
			{
				return this.prev;
			}
		}

		/// <summary>Initializes the <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance.</summary>
		// Token: 0x06003AFC RID: 15100 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void Initialize()
		{
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x0009C59A File Offset: 0x0009A79A
		internal void InitializeAndSetPrevious(VirtualPathProvider prev)
		{
			this.prev = prev;
			this.Initialize();
		}

		/// <summary>Combines a base path with a relative path to return a complete path to a virtual resource.</summary>
		/// <returns>The complete path to a virtual resource.</returns>
		/// <param name="basePath">The base path for the application.</param>
		/// <param name="relativePath">The path to the virtual resource, relative to the base path.</param>
		// Token: 0x06003AFE RID: 15102 RVA: 0x0009C5A9 File Offset: 0x0009A7A9
		public virtual string CombineVirtualPaths(string basePath, string relativePath)
		{
			return VirtualPathUtility.Combine(basePath, relativePath);
		}

		/// <summary>Gets a value that indicates whether a directory exists in the virtual file system.</summary>
		/// <returns>true if the directory exists in the virtual file system; otherwise, false.</returns>
		/// <param name="virtualDir">The path to the virtual directory.</param>
		// Token: 0x06003AFF RID: 15103 RVA: 0x0009C5B2 File Offset: 0x0009A7B2
		public virtual bool DirectoryExists(string virtualDir)
		{
			return this.prev != null && this.prev.DirectoryExists(virtualDir);
		}

		/// <summary>Gets a value that indicates whether a file exists in the virtual file system.</summary>
		/// <returns>true if the file exists in the virtual file system; otherwise, false.</returns>
		/// <param name="virtualPath">The path to the virtual file.</param>
		// Token: 0x06003B00 RID: 15104 RVA: 0x0009C5CA File Offset: 0x0009A7CA
		public virtual bool FileExists(string virtualPath)
		{
			return this.prev != null && this.prev.FileExists(virtualPath);
		}

		/// <summary>Creates a cache dependency based on the specified virtual paths.</summary>
		/// <returns>A <see cref="T:System.Web.Caching.CacheDependency" /> object for the specified virtual resources.</returns>
		/// <param name="virtualPath">The path to the primary virtual resource.</param>
		/// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
		/// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
		// Token: 0x06003B01 RID: 15105 RVA: 0x0009C5E2 File Offset: 0x0009A7E2
		public virtual CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			if (this.prev != null)
			{
				return this.prev.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
			}
			return null;
		}

		/// <summary>Returns a cache key to use for the specified virtual path.</summary>
		/// <returns>A cache key for the specified virtual resource.</returns>
		/// <param name="virtualPath">The path to the virtual resource.</param>
		// Token: 0x06003B02 RID: 15106 RVA: 0x0009C5FC File Offset: 0x0009A7FC
		public virtual string GetCacheKey(string virtualPath)
		{
			if (this.prev != null)
			{
				return this.prev.GetCacheKey(virtualPath);
			}
			return null;
		}

		/// <summary>Gets a virtual directory from the virtual file system.</summary>
		/// <returns>A descendent of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class that represents a directory in the virtual file system.</returns>
		/// <param name="virtualDir">The path to the virtual directory.</param>
		// Token: 0x06003B03 RID: 15107 RVA: 0x0009C614 File Offset: 0x0009A814
		public virtual VirtualDirectory GetDirectory(string virtualDir)
		{
			if (this.prev != null)
			{
				return this.prev.GetDirectory(virtualDir);
			}
			return null;
		}

		/// <summary>Gets a virtual file from the virtual file system.</summary>
		/// <returns>A descendent of the <see cref="T:System.Web.Hosting.VirtualFile" /> class that represents a file in the virtual file system.</returns>
		/// <param name="virtualPath">The path to the virtual file.</param>
		// Token: 0x06003B04 RID: 15108 RVA: 0x0009C62C File Offset: 0x0009A82C
		public virtual VirtualFile GetFile(string virtualPath)
		{
			if (this.prev != null)
			{
				return this.prev.GetFile(virtualPath);
			}
			return null;
		}

		/// <summary>Returns a hash of the specified virtual paths.</summary>
		/// <returns>A hash of the specified virtual paths.</returns>
		/// <param name="virtualPath">The path to the primary virtual resource.</param>
		/// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
		// Token: 0x06003B05 RID: 15109 RVA: 0x0009C644 File Offset: 0x0009A844
		public virtual string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			if (this.prev != null)
			{
				return this.prev.GetFileHash(virtualPath, virtualPathDependencies);
			}
			return null;
		}

		/// <summary>Gives the <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003B06 RID: 15110 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Returns a stream from a virtual file.</summary>
		/// <returns>A read-only <see cref="T:System.IO.Stream" /> object for the specified virtual file or resource.</returns>
		/// <param name="virtualPath">The path to the virtual file.</param>
		// Token: 0x06003B07 RID: 15111 RVA: 0x0009C660 File Offset: 0x0009A860
		public static Stream OpenFile(string virtualPath)
		{
			VirtualFile file = HostingEnvironment.VirtualPathProvider.GetFile(virtualPath);
			if (file != null)
			{
				return file.Open();
			}
			return null;
		}

		// Token: 0x04001FEE RID: 8174
		private VirtualPathProvider prev;
	}
}
