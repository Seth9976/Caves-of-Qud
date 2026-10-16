using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x020004F0 RID: 1264
	public static class RenderPipelineGraphicsSettingsExtensions
	{
		// Token: 0x06003195 RID: 12693 RVA: 0x0005F599 File Offset: 0x0005D799
		public static void SetValueAndNotify<T>(this IRenderPipelineGraphicsSettings settings, ref T currentPropertyValue, T newValue, [CallerMemberName] string propertyName = null)
		{
			throw new Exception("Changing values of a property in standalone builds is forbidden.");
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x0005F5A6 File Offset: 0x0005D7A6
		public static void NotifyValueChanged(this IRenderPipelineGraphicsSettings settings, [CallerMemberName] string propertyName = null)
		{
			throw new Exception("Notifying change of a property in standalone builds is forbidden.");
		}
	}
}
