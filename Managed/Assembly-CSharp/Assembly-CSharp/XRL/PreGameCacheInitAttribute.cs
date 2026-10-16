using System;

namespace XRL
{
	/// <summary> Valid on either [HasModSensitiveCache] or [HasGameBasedStaticCache] types </summary>
	// Token: 0x020004BE RID: 1214
	[AttributeUsage(AttributeTargets.Method)]
	public class PreGameCacheInitAttribute : Attribute
	{
	}
}
