using System;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B3 RID: 179
	public class Box : VisualElement
	{
		// Token: 0x060006EB RID: 1771 RVA: 0x0001FD9E File Offset: 0x0001DF9E
		public Box()
		{
			base.AddToClassList(Box.ussClassName);
		}

		// Token: 0x040003D5 RID: 981
		public static readonly string ussClassName = "unity-box";

		// Token: 0x020000B4 RID: 180
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x060006ED RID: 1773 RVA: 0x0001FDC0 File Offset: 0x0001DFC0
			public override object CreateInstance()
			{
				return new Box();
			}
		}

		// Token: 0x020000B5 RID: 181
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Box>
		{
		}
	}
}
