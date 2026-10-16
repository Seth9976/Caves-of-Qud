using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000C0 RID: 192
	public abstract class ICloudStorageDeleteFileListener : GalaxyTypeAwareListenerCloudStorageDeleteFile
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x00017447 File Offset: 0x00015647
		internal ICloudStorageDeleteFileListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStorageDeleteFileListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStorageDeleteFileListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001746F File Offset: 0x0001566F
		public ICloudStorageDeleteFileListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStorageDeleteFileListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00017493 File Offset: 0x00015693
		internal static HandleRef getCPtr(ICloudStorageDeleteFileListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000174B4 File Offset: 0x000156B4
		~ICloudStorageDeleteFileListener()
		{
			this.Dispose();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000174E4 File Offset: 0x000156E4
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorageDeleteFileListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStorageDeleteFileListener.listeners.ContainsKey(handle))
					{
						ICloudStorageDeleteFileListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008EA RID: 2282
		public abstract void OnDeleteFileSuccess(string container, string name);

		// Token: 0x060008EB RID: 2283
		public abstract void OnDeleteFileFailure(string container, string name, ICloudStorageDeleteFileListener.FailureReason failureReason);

		// Token: 0x060008EC RID: 2284 RVA: 0x00017594 File Offset: 0x00015794
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnDeleteFileSuccess", ICloudStorageDeleteFileListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_0(ICloudStorageDeleteFileListener.SwigDirectorOnDeleteFileSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnDeleteFileFailure", ICloudStorageDeleteFileListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_1(ICloudStorageDeleteFileListener.SwigDirectorOnDeleteFileFailure);
			}
			GalaxyInstancePINVOKE.ICloudStorageDeleteFileListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00017608 File Offset: 0x00015808
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStorageDeleteFileListener));
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001763E File Offset: 0x0001583E
		[MonoPInvokeCallback(typeof(ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_0))]
		private static void SwigDirectorOnDeleteFileSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (ICloudStorageDeleteFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageDeleteFileListener.listeners[cPtr].OnDeleteFileSuccess(container, name);
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00017662 File Offset: 0x00015862
		[MonoPInvokeCallback(typeof(ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_1))]
		private static void SwigDirectorOnDeleteFileFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ICloudStorageDeleteFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageDeleteFileListener.listeners[cPtr].OnDeleteFileFailure(container, name, (ICloudStorageDeleteFileListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400010C RID: 268
		private static Dictionary<IntPtr, ICloudStorageDeleteFileListener> listeners = new Dictionary<IntPtr, ICloudStorageDeleteFileListener>();

		// Token: 0x0400010D RID: 269
		private HandleRef swigCPtr;

		// Token: 0x0400010E RID: 270
		private ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_0 swigDelegate0;

		// Token: 0x0400010F RID: 271
		private ICloudStorageDeleteFileListener.SwigDelegateICloudStorageDeleteFileListener_1 swigDelegate1;

		// Token: 0x04000110 RID: 272
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(string)
		};

		// Token: 0x04000111 RID: 273
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(ICloudStorageDeleteFileListener.FailureReason)
		};

		// Token: 0x020000C1 RID: 193
		// (Invoke) Token: 0x060008F2 RID: 2290
		public delegate void SwigDelegateICloudStorageDeleteFileListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x060008F6 RID: 2294
		public delegate void SwigDelegateICloudStorageDeleteFileListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x020000C3 RID: 195
		public enum FailureReason
		{
			// Token: 0x04000113 RID: 275
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000114 RID: 276
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000115 RID: 277
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000116 RID: 278
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x04000117 RID: 279
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000118 RID: 280
			FAILURE_REASON_ABORTED,
			// Token: 0x04000119 RID: 281
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x0400011A RID: 282
			FAILURE_REASON_CONFLICT
		}
	}
}
