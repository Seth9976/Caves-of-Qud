using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200056E RID: 1390
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[UsedByNativeCode]
	public enum ShaderKeywordType
	{
		// Token: 0x04001230 RID: 4656
		None,
		// Token: 0x04001231 RID: 4657
		BuiltinDefault = 2,
		// Token: 0x04001232 RID: 4658
		[Obsolete("Shader keyword type BuiltinExtra is no longer used. Use BuiltinDefault instead. (UnityUpgradable) -> BuiltinDefault")]
		BuiltinExtra = 6,
		// Token: 0x04001233 RID: 4659
		[Obsolete("Shader keyword type BuiltinAutoStripped is no longer used. Use BuiltinDefault instead. (UnityUpgradable) -> BuiltinDefault")]
		BuiltinAutoStripped = 10,
		// Token: 0x04001234 RID: 4660
		UserDefined = 16,
		// Token: 0x04001235 RID: 4661
		Plugin = 32
	}
}
