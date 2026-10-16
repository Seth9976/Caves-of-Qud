using System;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	internal class AndroidJavaObjectUnityOwned : AndroidJavaObject
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00004B95 File Offset: 0x00002D95
		public AndroidJavaObjectUnityOwned(IntPtr jobject)
			: base(jobject)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004BA0 File Offset: 0x00002DA0
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				throw new Exception("The object is owned by Unity runtime, you shouldn't call Dispose on it.");
			}
			base.Dispose(disposing);
		}
	}
}
