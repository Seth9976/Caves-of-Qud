using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000157 RID: 343
	public abstract class ISendInvitationListener : GalaxyTypeAwareListenerSendInvitation
	{
		// Token: 0x06000CB3 RID: 3251 RVA: 0x0001471C File Offset: 0x0001291C
		internal ISendInvitationListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.ISendInvitationListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			ISendInvitationListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x00014744 File Offset: 0x00012944
		public ISendInvitationListener()
			: this(GalaxyInstancePINVOKE.new_ISendInvitationListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00014768 File Offset: 0x00012968
		internal static HandleRef getCPtr(ISendInvitationListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00014788 File Offset: 0x00012988
		~ISendInvitationListener()
		{
			this.Dispose();
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000147B8 File Offset: 0x000129B8
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ISendInvitationListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (ISendInvitationListener.listeners.ContainsKey(handle))
					{
						ISendInvitationListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000CB8 RID: 3256
		public abstract void OnInvitationSendSuccess(GalaxyID userID, string connectionString);

		// Token: 0x06000CB9 RID: 3257
		public abstract void OnInvitationSendFailure(GalaxyID userID, string connectionString, ISendInvitationListener.FailureReason failureReason);

		// Token: 0x06000CBA RID: 3258 RVA: 0x00014868 File Offset: 0x00012A68
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnInvitationSendSuccess", ISendInvitationListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new ISendInvitationListener.SwigDelegateISendInvitationListener_0(ISendInvitationListener.SwigDirectorOnInvitationSendSuccess);
			}
			if (this.SwigDerivedClassHasMethod("OnInvitationSendFailure", ISendInvitationListener.swigMethodTypes1))
			{
				this.swigDelegate1 = new ISendInvitationListener.SwigDelegateISendInvitationListener_1(ISendInvitationListener.SwigDirectorOnInvitationSendFailure);
			}
			GalaxyInstancePINVOKE.ISendInvitationListener_director_connect(this.swigCPtr, this.swigDelegate0, this.swigDelegate1);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000148DC File Offset: 0x00012ADC
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(ISendInvitationListener));
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00014912 File Offset: 0x00012B12
		[MonoPInvokeCallback(typeof(ISendInvitationListener.SwigDelegateISendInvitationListener_0))]
		private static void SwigDirectorOnInvitationSendSuccess(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString)
		{
			if (ISendInvitationListener.listeners.ContainsKey(cPtr))
			{
				ISendInvitationListener.listeners[cPtr].OnInvitationSendSuccess(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString);
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00014946 File Offset: 0x00012B46
		[MonoPInvokeCallback(typeof(ISendInvitationListener.SwigDelegateISendInvitationListener_1))]
		private static void SwigDirectorOnInvitationSendFailure(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason)
		{
			if (ISendInvitationListener.listeners.ContainsKey(cPtr))
			{
				ISendInvitationListener.listeners[cPtr].OnInvitationSendFailure(new GalaxyID(new GalaxyID(userID, false).ToUint64()), connectionString, (ISendInvitationListener.FailureReason)failureReason);
			}
		}

		// Token: 0x0400027F RID: 639
		private static Dictionary<IntPtr, ISendInvitationListener> listeners = new Dictionary<IntPtr, ISendInvitationListener>();

		// Token: 0x04000280 RID: 640
		private HandleRef swigCPtr;

		// Token: 0x04000281 RID: 641
		private ISendInvitationListener.SwigDelegateISendInvitationListener_0 swigDelegate0;

		// Token: 0x04000282 RID: 642
		private ISendInvitationListener.SwigDelegateISendInvitationListener_1 swigDelegate1;

		// Token: 0x04000283 RID: 643
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string)
		};

		// Token: 0x04000284 RID: 644
		private static Type[] swigMethodTypes1 = new Type[]
		{
			typeof(GalaxyID),
			typeof(string),
			typeof(ISendInvitationListener.FailureReason)
		};

		// Token: 0x02000158 RID: 344
		// (Invoke) Token: 0x06000CC0 RID: 3264
		public delegate void SwigDelegateISendInvitationListener_0(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString);

		// Token: 0x02000159 RID: 345
		// (Invoke) Token: 0x06000CC4 RID: 3268
		public delegate void SwigDelegateISendInvitationListener_1(IntPtr cPtr, IntPtr userID, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string connectionString, int failureReason);

		// Token: 0x0200015A RID: 346
		public enum FailureReason
		{
			// Token: 0x04000286 RID: 646
			FAILURE_REASON_UNDEFINED,
			// Token: 0x04000287 RID: 647
			FAILURE_REASON_USER_DOES_NOT_EXIST,
			// Token: 0x04000288 RID: 648
			FAILURE_REASON_RECEIVER_DOES_NOT_ALLOW_INVITING,
			// Token: 0x04000289 RID: 649
			FAILURE_REASON_SENDER_DOES_NOT_ALLOW_INVITING,
			// Token: 0x0400028A RID: 650
			FAILURE_REASON_RECEIVER_BLOCKED,
			// Token: 0x0400028B RID: 651
			FAILURE_REASON_SENDER_BLOCKED,
			// Token: 0x0400028C RID: 652
			FAILURE_REASON_CONNECTION_FAILURE
		}
	}
}
