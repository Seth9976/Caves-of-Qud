using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005B4 RID: 1460
	public static class VisualElementDebugExtensions
	{
		// Token: 0x06002B6F RID: 11119 RVA: 0x0009CDB8 File Offset: 0x0009AFB8
		internal static string GetDisplayName(this VisualElement ve, bool withHashCode = true)
		{
			bool flag = ve == null;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				string text2 = ve.GetType().Name;
				bool flag2 = !string.IsNullOrEmpty(ve.name);
				if (flag2)
				{
					text2 = text2 + "#" + ve.name;
				}
				if (withHashCode)
				{
					text2 = text2 + " (" + ve.GetHashCode().ToString("x8") + ")";
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0009CE3B File Offset: 0x0009B03B
		public static void DebugIncrementVersionChange(VisualElement ve, VersionChangeType changeType)
		{
			ve.IncrementVersion(changeType);
		}
	}
}
