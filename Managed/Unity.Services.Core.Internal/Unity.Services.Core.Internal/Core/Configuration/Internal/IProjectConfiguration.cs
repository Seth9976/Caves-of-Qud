using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Configuration.Internal
{
	// Token: 0x02000025 RID: 37
	public interface IProjectConfiguration : IServiceComponent
	{
		// Token: 0x06000067 RID: 103
		bool GetBool(string key, bool defaultValue = false);

		// Token: 0x06000068 RID: 104
		int GetInt(string key, int defaultValue = 0);

		// Token: 0x06000069 RID: 105
		float GetFloat(string key, float defaultValue = 0f);

		// Token: 0x0600006A RID: 106
		string GetString(string key, string defaultValue = null);
	}
}
