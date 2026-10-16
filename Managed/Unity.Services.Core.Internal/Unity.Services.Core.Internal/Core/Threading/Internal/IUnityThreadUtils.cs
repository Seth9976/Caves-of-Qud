using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Threading.Internal
{
	// Token: 0x02000013 RID: 19
	public interface IUnityThreadUtils : IServiceComponent
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001E RID: 30
		bool IsRunningOnUnityThread { get; }

		// Token: 0x0600001F RID: 31
		Task PostAsync([NotNull] Action action);

		// Token: 0x06000020 RID: 32
		Task PostAsync([NotNull] Action<object> action, object state);

		// Token: 0x06000021 RID: 33
		Task<T> PostAsync<T>([NotNull] Func<T> action);

		// Token: 0x06000022 RID: 34
		Task<T> PostAsync<T>([NotNull] Func<object, T> action, object state);

		// Token: 0x06000023 RID: 35
		void Send([NotNull] Action action);

		// Token: 0x06000024 RID: 36
		void Send([NotNull] Action<object> action, object state);

		// Token: 0x06000025 RID: 37
		T Send<T>([NotNull] Func<T> action);

		// Token: 0x06000026 RID: 38
		T Send<T>([NotNull] Func<object, T> action, object state);
	}
}
