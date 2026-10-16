using System;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x02000171 RID: 369
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class ProgressBar : AbstractProgressBar
	{
		// Token: 0x02000172 RID: 370
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : AbstractProgressBar.UxmlSerializedData
		{
			// Token: 0x06000BC8 RID: 3016 RVA: 0x00033B75 File Offset: 0x00031D75
			public override object CreateInstance()
			{
				return new ProgressBar();
			}
		}

		// Token: 0x02000173 RID: 371
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<ProgressBar, AbstractProgressBar.UxmlTraits>
		{
		}
	}
}
