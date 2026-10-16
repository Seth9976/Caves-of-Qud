using System;
using System.Collections.Generic;

namespace Mono.Web.Util
{
	// Token: 0x02000006 RID: 6
	public interface ISectionSettingsMapper
	{
		// Token: 0x0600000E RID: 14
		object MapSection(object section, List<SettingsMappingWhat> whats);
	}
}
