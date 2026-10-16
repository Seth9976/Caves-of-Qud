using System;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002D RID: 45
	[UxmlObject]
	public abstract class CustomBinding : Binding
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00007D34 File Offset: 0x00005F34
		protected CustomBinding()
		{
			base.updateTrigger = BindingUpdateTrigger.EveryUpdate;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007D48 File Offset: 0x00005F48
		protected internal virtual BindingResult Update(in BindingContext context)
		{
			return new BindingResult(BindingStatus.Success, null);
		}

		// Token: 0x0200002E RID: 46
		[ExcludeFromDocs]
		[Serializable]
		public new abstract class UxmlSerializedData : Binding.UxmlSerializedData
		{
		}
	}
}
