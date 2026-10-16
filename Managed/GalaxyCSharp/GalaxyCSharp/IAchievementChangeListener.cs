using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000A7 RID: 167
	public abstract class IAchievementChangeListener : GalaxyTypeAwareListenerAchievementChange
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x0000C2F8 File Offset: 0x0000A4F8
		internal IAchievementChangeListener(IntPtr cPtr, bool cMemoryOwn)
			: base(GalaxyInstancePINVOKE.IAchievementChangeListener_SWIGUpcast(cPtr), cMemoryOwn)
		{
			IAchievementChangeListener.listeners.Add(cPtr, this);
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0000C320 File Offset: 0x0000A520
		public IAchievementChangeListener()
			: this(GalaxyInstancePINVOKE.new_IAchievementChangeListener(), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			this.SwigDirectorConnect();
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0000C344 File Offset: 0x0000A544
		internal static HandleRef getCPtr(IAchievementChangeListener obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0000C364 File Offset: 0x0000A564
		~IAchievementChangeListener()
		{
			this.Dispose();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0000C394 File Offset: 0x0000A594
		public override void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IAchievementChangeListener(this.swigCPtr);
					}
					IntPtr handle = this.swigCPtr.Handle;
					if (IAchievementChangeListener.listeners.ContainsKey(handle))
					{
						IAchievementChangeListener.listeners.Remove(handle);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		// Token: 0x06000840 RID: 2112
		public abstract void OnAchievementUnlocked(string name);

		// Token: 0x06000841 RID: 2113 RVA: 0x0000C444 File Offset: 0x0000A644
		private void SwigDirectorConnect()
		{
			if (this.SwigDerivedClassHasMethod("OnAchievementUnlocked", IAchievementChangeListener.swigMethodTypes0))
			{
				this.swigDelegate0 = new IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0(IAchievementChangeListener.SwigDirectorOnAchievementUnlocked);
			}
			GalaxyInstancePINVOKE.IAchievementChangeListener_director_connect(this.swigCPtr, this.swigDelegate0);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0000C480 File Offset: 0x0000A680
		private bool SwigDerivedClassHasMethod(string methodName, Type[] methodTypes)
		{
			MethodInfo method = base.GetType().GetMethod(methodName, 52, null, methodTypes, null);
			return method.DeclaringType.IsSubclassOf(typeof(IAchievementChangeListener));
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
		[MonoPInvokeCallback(typeof(IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0))]
		private static void SwigDirectorOnAchievementUnlocked(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name)
		{
			if (IAchievementChangeListener.listeners.ContainsKey(cPtr))
			{
				IAchievementChangeListener.listeners[cPtr].OnAchievementUnlocked(name);
			}
		}

		// Token: 0x040000CB RID: 203
		private static Dictionary<IntPtr, IAchievementChangeListener> listeners = new Dictionary<IntPtr, IAchievementChangeListener>();

		// Token: 0x040000CC RID: 204
		private HandleRef swigCPtr;

		// Token: 0x040000CD RID: 205
		private IAchievementChangeListener.SwigDelegateIAchievementChangeListener_0 swigDelegate0;

		// Token: 0x040000CE RID: 206
		private static Type[] swigMethodTypes0 = new Type[] { typeof(string) };

		// Token: 0x020000A8 RID: 168
		// (Invoke) Token: 0x06000846 RID: 2118
		public delegate void SwigDelegateIAchievementChangeListener_0(IntPtr cPtr, [MarshalAs(44, MarshalTypeRef = Galaxy.Api.GalaxyInstancePINVOKE/UTF8Marshaler)] string name);
	}
}
