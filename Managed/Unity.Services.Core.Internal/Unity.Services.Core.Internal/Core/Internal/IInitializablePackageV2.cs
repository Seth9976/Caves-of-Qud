using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000048 RID: 72
	public interface IInitializablePackageV2 : IInitializablePackage
	{
		// Token: 0x0600013A RID: 314
		void Register(CorePackageRegistry registry);

		// Token: 0x0600013B RID: 315
		Task InitializeInstanceAsync(CoreRegistry registry);
	}
}
