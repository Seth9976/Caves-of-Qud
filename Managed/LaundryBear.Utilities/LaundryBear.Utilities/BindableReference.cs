using System;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class BindableReference<T> where T : Component
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024B0 File Offset: 0x000006B0
		public T Value
		{
			get
			{
				return this.m_runtimeReference;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024B8 File Offset: 0x000006B8
		public bool TryGetBindableReference(Transform transform)
		{
			if (this.m_isRelative)
			{
				Transform transform2 = transform.Find(this.m_gameObjectPath);
				return transform2 != null && transform2.TryGetComponent<T>(out this.m_runtimeReference);
			}
			GameObject gameObject = GameObject.Find(this.m_gameObjectPath);
			return gameObject != null && gameObject.TryGetComponent<T>(out this.m_runtimeReference);
		}

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private bool m_isRelative;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private string m_gameObjectPath;

		// Token: 0x0400000C RID: 12
		private T m_runtimeReference;
	}
}
