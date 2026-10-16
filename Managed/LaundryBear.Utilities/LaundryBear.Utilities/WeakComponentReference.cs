using System;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class WeakComponentReference
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000310C File Offset: 0x0000130C
		public bool GetReference<T>(out T result) where T : Component
		{
			GameObject gameObject = GameObject.Find(this.m_fullPath);
			if (gameObject != null)
			{
				return gameObject.TryGetComponent<T>(out result);
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003140 File Offset: 0x00001340
		public bool GetReference<T>(Transform transform, out T result) where T : Component
		{
			Transform transform2 = transform.Find(this.m_path);
			if (transform2 != null)
			{
				return transform2.TryGetComponent<T>(out result);
			}
			result = default(T);
			return false;
		}

		// Token: 0x0400002D RID: 45
		[SerializeField]
		private string m_path;

		// Token: 0x0400002E RID: 46
		[SerializeField]
		private string m_fullPath;

		// Token: 0x0400002F RID: 47
		[SerializeField]
		private bool m_relative;

		// Token: 0x04000030 RID: 48
		[SerializeField]
		private SerializableSystemType m_type;
	}
}
