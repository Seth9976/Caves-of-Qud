using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000149 RID: 329
	public abstract class IPersonaDataChangedListener : GalaxyTypeAwareListenerPersonaDataChanged
	{
		// Token: 0x06000C6B RID: 3179 RVA: 0x00013BC4 File Offset: 0x00011DC4
		internal IPersonaDataChangedListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IPersonaDataChangedListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IPersonaDataChangedListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00013BEC File Offset: 0x00011DEC
		public IPersonaDataChangedListener()
			: this(GalaxyInstancePINVOKE.new_IPersonaDataChangedListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00013C10 File Offset: 0x00011E10
		internal static HandleRef getCPtr(IPersonaDataChangedListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00013C30 File Offset: 0x00011E30
		~IPersonaDataChangedListener()
		{
			this.Dispose();
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00013C60 File Offset: 0x00011E60
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IPersonaDataChangedListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IPersonaDataChangedListener.listeners.ContainsKey(handle))
					{
						IPersonaDataChangedListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000C70 RID: 3184
		public abstract void OnPersonaDataChanged(GalaxyID userID, uint personaStateChange);

		// Token: 0x06000C71 RID: 3185 RVA: 0x00013D10 File Offset: 0x00011F10
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnPersonaDataChanged", IPersonaDataChangedListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0(IPersonaDataChangedListener.SwigDirectorOnPersonaDataChanged);
			}
			GalaxyInstancePINVOKE.IPersonaDataChangedListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00013D4C File Offset: 0x00011F4C
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IPersonaDataChangedListener));
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00013D82 File Offset: 0x00011F82
		[MonoPInvokeCallback(typeof(IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0))]
		private static void SwigDirectorOnPersonaDataChanged(IntPtr cPtr, IntPtr userID, uint personaStateChange)
		{
			if (IPersonaDataChangedListener.listeners.ContainsKey(cPtr))
			{
				IPersonaDataChangedListener.listeners[cPtr].OnPersonaDataChanged(new GalaxyID(new GalaxyID(userID, false).ToUint64()), personaStateChange);
			}
		}

		// Token: 0x0400025C RID: 604
		private static Dictionary<IntPtr, IPersonaDataChangedListener> listeners = new Dictionary<IntPtr, IPersonaDataChangedListener>();

		// Token: 0x0400025D RID: 605
		private HandleRef swigCPtr;

		// Token: 0x0400025E RID: 606
		private IPersonaDataChangedListener.SwigDelegateIPersonaDataChangedListener_0 swigDelegate0;

		// Token: 0x0400025F RID: 607
		private static Type[] swigMethodTypes0 = new Type[]
		{
			typeof(GalaxyID),
			typeof(uint)
		};

		// Token: 0x0200014A RID: 330
		// (Invoke) Token: 0x06000C76 RID: 3190
		public delegate void SwigDelegateIPersonaDataChangedListener_0(IntPtr cPtr, IntPtr userID, uint personaStateChange);

		// Token: 0x0200014B RID: 331
		public enum PersonaStateChange
		{
			// Token: 0x04000261 RID: 609
			PERSONA_CHANGE_NONE,
			// Token: 0x04000262 RID: 610
			PERSONA_CHANGE_NAME,
			// Token: 0x04000263 RID: 611
			PERSONA_CHANGE_AVATAR,
			// Token: 0x04000264 RID: 612
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_SMALL = 4,
			// Token: 0x04000265 RID: 613
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_MEDIUM = 8,
			// Token: 0x04000266 RID: 614
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_LARGE = 16,
			// Token: 0x04000267 RID: 615
			PERSONA_CHANGE_AVATAR_DOWNLOADED_IMAGE_ANY = 28
		}
	}
}
