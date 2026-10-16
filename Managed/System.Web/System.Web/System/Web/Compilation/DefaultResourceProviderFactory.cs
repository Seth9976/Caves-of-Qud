using System;

namespace System.Web.Compilation
{
	// Token: 0x0200064B RID: 1611
	internal sealed class DefaultResourceProviderFactory : ResourceProviderFactory
	{
		// Token: 0x0600453A RID: 17722 RVA: 0x000BB886 File Offset: 0x000B9A86
		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
			return new DefaultResourceProvider(classKey, true);
		}

		// Token: 0x0600453B RID: 17723 RVA: 0x000BB88F File Offset: 0x000B9A8F
		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
			return new DefaultResourceProvider(virtualPath, false);
		}
	}
}
