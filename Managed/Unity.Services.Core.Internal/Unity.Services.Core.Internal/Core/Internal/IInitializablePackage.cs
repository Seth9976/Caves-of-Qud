using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000047 RID: 71
	public interface IInitializablePackage
	{
		// Token: 0x06000139 RID: 313
		Task Initialize(CoreRegistry registry);
	}
}
