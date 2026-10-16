using System;
using System.Collections;
using System.IO;
using System.Web.Caching;

namespace System.Web.Hosting
{
	// Token: 0x0200054A RID: 1354
	internal sealed class DefaultVirtualPathProvider : VirtualPathProvider
	{
		// Token: 0x06003A89 RID: 14985 RVA: 0x0009BDA1 File Offset: 0x00099FA1
		internal DefaultVirtualPathProvider()
		{
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x00003946 File Offset: 0x00001B46
		protected override void Initialize()
		{
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x0009BDA9 File Offset: 0x00099FA9
		public override bool DirectoryExists(string virtualDir)
		{
			if (string.IsNullOrEmpty(virtualDir))
			{
				throw new ArgumentNullException("virtualDir");
			}
			return Directory.Exists(HostingEnvironment.MapPath(virtualDir));
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x0009BDC9 File Offset: 0x00099FC9
		public override bool FileExists(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			return File.Exists(HostingEnvironment.MapPath(virtualPath));
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
		{
			return null;
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override string GetCacheKey(string virtualPath)
		{
			return null;
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x0009BDE9 File Offset: 0x00099FE9
		public override VirtualDirectory GetDirectory(string virtualDir)
		{
			if (string.IsNullOrEmpty(virtualDir))
			{
				throw new ArgumentNullException("virtualDir");
			}
			return new DefaultVirtualDirectory(virtualDir);
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x0009BE04 File Offset: 0x0009A004
		public override VirtualFile GetFile(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			return new DefaultVirtualFile(virtualPath);
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x0009BE1F File Offset: 0x0009A01F
		public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
		{
			if (virtualPath == null || virtualPathDependencies == null)
			{
				throw new NullReferenceException();
			}
			return virtualPath;
		}
	}
}
