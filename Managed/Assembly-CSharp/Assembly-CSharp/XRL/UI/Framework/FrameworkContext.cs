using System;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x02001508 RID: 5384
	public class FrameworkContext : MonoBehaviour
	{
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600D1EF RID: 53743 RVA: 0x004DF3E6 File Offset: 0x004DD5E6
		// (set) Token: 0x0600D1F0 RID: 53744 RVA: 0x004DF3EE File Offset: 0x004DD5EE
		public virtual NavigationContext context { get; set; }

		// Token: 0x0600D1F1 RID: 53745 RVA: 0x004DF3F8 File Offset: 0x004DD5F8
		public T RequireContext<T>() where T : NavigationContext
		{
			if (this.context != null)
			{
				T t = this.context as T;
				T t2 = t;
				return t;
			}
			this.context = Activator.CreateInstance<T>();
			return this.context as T;
		}
	}
}
