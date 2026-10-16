using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000007 RID: 7
	public class Channel
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000024CB File Offset: 0x000006CB
		public Channel(Color _C)
		{
			this._color = _C;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024F8 File Offset: 0x000006F8
		public void Feed(float val)
		{
			for (int i = 1023; i >= 1; i--)
			{
				this._data[i] = this._data[i - 1];
			}
			this._data[0] = val;
		}

		// Token: 0x04000006 RID: 6
		public float[] _data = new float[1024];

		// Token: 0x04000007 RID: 7
		public Color _color = Color.white;

		// Token: 0x04000008 RID: 8
		public bool isActive;
	}
}
