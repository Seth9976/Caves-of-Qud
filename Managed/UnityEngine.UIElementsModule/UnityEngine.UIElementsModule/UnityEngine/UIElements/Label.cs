using System;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012D RID: 301
	public class Label : TextElement
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x0002AF2E File Offset: 0x0002912E
		public Label()
			: this(string.Empty)
		{
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002AF3D File Offset: 0x0002913D
		public Label(string text)
		{
			base.AddToClassList(Label.ussClassName);
			this.text = text;
		}

		// Token: 0x0400058F RID: 1423
		public new static readonly string ussClassName = "unity-label";

		// Token: 0x0200012E RID: 302
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : TextElement.UxmlSerializedData
		{
			// Token: 0x060009C9 RID: 2505 RVA: 0x0002AF67 File Offset: 0x00029167
			public override object CreateInstance()
			{
				return new Label();
			}
		}

		// Token: 0x0200012F RID: 303
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Label, Label.UxmlTraits>
		{
		}

		// Token: 0x02000130 RID: 304
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}
	}
}
