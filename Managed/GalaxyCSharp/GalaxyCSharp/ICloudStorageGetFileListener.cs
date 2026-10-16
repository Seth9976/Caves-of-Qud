using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000C4 RID: 196
	public abstract class ICloudStorageGetFileListener : GalaxyTypeAwareListenerCloudStorageGetFile
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x000176F6 File Offset: 0x000158F6
		internal ICloudStorageGetFileListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ICloudStorageGetFileListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ICloudStorageGetFileListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001771E File Offset: 0x0001591E
		public ICloudStorageGetFileListener()
			: this(GalaxyInstancePINVOKE.new_ICloudStorageGetFileListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00017742 File Offset: 0x00015942
		internal static HandleRef getCPtr(ICloudStorageGetFileListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00017760 File Offset: 0x00015960
		~ICloudStorageGetFileListener()
		{
			this.Dispose();
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00017790 File Offset: 0x00015990
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorageGetFileListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ICloudStorageGetFileListener.listeners.ContainsKey(handle))
					{
						ICloudStorageGetFileListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00017840 File Offset: 0x00015A40
		public virtual void OnGetFileSuccess(string container, string name, uint fileSize, SavegameType savegameType, string savegameID)
		{
			GalaxyInstancePINVOKE.ICloudStorageGetFileListener_OnGetFileSuccess(this.swigCPtr, container, name, fileSize, (int)savegameType, savegameID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00017864 File Offset: 0x00015A64
		public virtual void OnGetFileFailure(string container, string name, ICloudStorageGetFileListener.FailureReason failureReason)
		{
			GalaxyInstancePINVOKE.ICloudStorageGetFileListener_OnGetFileFailure(this.swigCPtr, container, name, (int)failureReason);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00017884 File Offset: 0x00015A84
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnGetFileSuccess", ICloudStorageGetFileListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_0(ICloudStorageGetFileListener.SwigDirectorOnGetFileSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnGetFileFailure", ICloudStorageGetFileListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_1(ICloudStorageGetFileListener.SwigDirectorOnGetFileFailure);
			}
			GalaxyInstancePINVOKE.ICloudStorageGetFileListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000178F8 File Offset: 0x00015AF8
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ICloudStorageGetFileListener));
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001792E File Offset: 0x00015B2E
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_0))]
		private static void SwigDirectorOnGetFileSuccess(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint fileSize, int savegameType, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string savegameID)
		{
			if (ICloudStorageGetFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListener.listeners[cPtr].OnGetFileSuccess(container, name, fileSize, (SavegameType)savegameType, savegameID);
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00017957 File Offset: 0x00015B57
		[MonoPInvokeCallback(typeof(ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_1))]
		private static void SwigDirectorOnGetFileFailure(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason)
		{
			if (ICloudStorageGetFileListener.listeners.ContainsKey(cPtr))
			{
				ICloudStorageGetFileListener.listeners[cPtr].OnGetFileFailure(container, name, (ICloudStorageGetFileListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400011B RID: 283
		private static Dictionary<IntPtr, ICloudStorageGetFileListener> listeners = new Dictionary<IntPtr, ICloudStorageGetFileListener>();

		// Token: 0x0400011C RID: 284
		private HandleRef swigCPtr;

		// Token: 0x0400011D RID: 285
		private ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_0 swigDelegate0;

		// Token: 0x0400011E RID: 286
		private ICloudStorageGetFileListener.SwigDelegateICloudStorageGetFileListener_1 swigDelegate1;

		// Token: 0x0400011F RID: 287
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(uint),
			typeof(SavegameType),
			typeof(string)
		};

		// Token: 0x04000120 RID: 288
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(ICloudStorageGetFileListener.FailureReason)
		};

		// Token: 0x020000C5 RID: 197
		// (Invoke) Token: 0x06000906 RID: 2310
		public delegate void SwigDelegateICloudStorageGetFileListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, uint fileSize, int savegameType, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string savegameID);

		// Token: 0x020000C6 RID: 198
		// (Invoke) Token: 0x0600090A RID: 2314
		public delegate void SwigDelegateICloudStorageGetFileListener_1(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string container, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name, int failureReason);

		// Token: 0x020000C7 RID: 199
		public enum FailureReason
		{
			// Token: 0x04000122 RID: 290
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000123 RID: 291
			FAILURE_REASON_UNAUTHORIZED,
			// Token: 0x04000124 RID: 292
			FAILURE_REASON_FORBIDDEN,
			// Token: 0x04000125 RID: 293
			FAILURE_REASON_NOT_FOUND,
			// Token: 0x04000126 RID: 294
			FAILURE_REASON_UNAVAILABLE,
			// Token: 0x04000127 RID: 295
			FAILURE_REASON_ABORTED,
			// Token: 0x04000128 RID: 296
			FAILURE_REASON_CONNECTION_FAILURE,
			// Token: 0x04000129 RID: 297
			FAILURE_REASON_BUFFER_TOO_SMALL,
			// Token: 0x0400012A RID: 298
			FAILURE_REASON_WRITE_FUNC_ERROR
		}
	}
}
