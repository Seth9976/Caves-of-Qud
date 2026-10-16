using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x020000A9 RID: 169
	public class IApps : IDisposable
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x00016920 File Offset: 0x00014B20
		internal IApps(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001693C File Offset: 0x00014B3C
		internal static HandleRef getCPtr(IApps obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001695C File Offset: 0x00014B5C
		~IApps()
		{
			this.Dispose();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001698C File Offset: 0x00014B8C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IApps(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00016A0C File Offset: 0x00014C0C
		public virtual bool IsDlcInstalled(ulong productID)
		{
			bool flag = GalaxyInstancePINVOKE.IApps_IsDlcInstalled(this.swigCPtr, productID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00016A38 File Offset: 0x00014C38
		public virtual string GetCurrentGameLanguage(ulong productID)
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguage__SWIG_0(this.swigCPtr, productID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00016A64 File Offset: 0x00014C64
		public virtual string GetCurrentGameLanguage()
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguage__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00016A90 File Offset: 0x00014C90
		public virtual void GetCurrentGameLanguageCopy(out string buffer, uint bufferLength, ulong productID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCopy__SWIG_0(this.swigCPtr, array, bufferLength, productID);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00016AE8 File Offset: 0x00014CE8
		public virtual void GetCurrentGameLanguageCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCopy__SWIG_1(this.swigCPtr, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00016B40 File Offset: 0x00014D40
		public virtual string GetCurrentGameLanguageCode(ulong productID)
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCode__SWIG_0(this.swigCPtr, productID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00016B6C File Offset: 0x00014D6C
		public virtual string GetCurrentGameLanguageCode()
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCode__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00016B98 File Offset: 0x00014D98
		public virtual void GetCurrentGameLanguageCodeCopy(out string buffer, uint bufferLength, ulong productID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCodeCopy__SWIG_0(this.swigCPtr, array, bufferLength, productID);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00016BF0 File Offset: 0x00014DF0
		public virtual void GetCurrentGameLanguageCodeCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCodeCopy__SWIG_1(this.swigCPtr, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x040000CF RID: 207
		private HandleRef swigCPtr;

		// Token: 0x040000D0 RID: 208
		protected bool swigCMemOwn;
	}
}
