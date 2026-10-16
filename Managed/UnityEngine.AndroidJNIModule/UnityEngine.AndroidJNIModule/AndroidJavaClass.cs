using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00004BC6 File Offset: 0x00002DC6
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004BD8 File Offset: 0x00002DD8
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			IntPtr intPtr = AndroidJNISafe.FindClass(className.Replace('.', '/'));
			this.m_jclass = new GlobalJavaObjectRef(intPtr);
			this.m_jobject = null;
			AndroidJNISafe.DeleteLocalRef(intPtr);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004C24 File Offset: 0x00002E24
		internal AndroidJavaClass(IntPtr jclass)
		{
			bool flag = jclass == IntPtr.Zero;
			if (flag)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = new GlobalJavaObjectRef(jclass);
			this.m_jobject = null;
		}
	}
}
