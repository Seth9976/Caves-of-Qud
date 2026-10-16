using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class Price : ISerializationCallbackReceiver
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00008720 File Offset: 0x00006920
		public void OnBeforeSerialize()
		{
			this.data = decimal.GetBits(this.value);
			this.num = decimal.ToDouble(this.value);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00008744 File Offset: 0x00006944
		public void OnAfterDeserialize()
		{
			if (this.data != null && this.data.Length == 4)
			{
				this.value = new decimal(this.data);
			}
		}

		// Token: 0x04000130 RID: 304
		public decimal value;

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private int[] data;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private double num;
	}
}
