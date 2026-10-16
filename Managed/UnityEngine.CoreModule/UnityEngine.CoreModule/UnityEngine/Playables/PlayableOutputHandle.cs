using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200045B RID: 1115
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Export/Director/PlayableOutputHandle.bindings.h")]
	public struct PlayableOutputHandle : IEquatable<PlayableOutputHandle>
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x000529D4 File Offset: 0x00050BD4
		public static PlayableOutputHandle Null
		{
			get
			{
				return PlayableOutputHandle.m_Null;
			}
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000529EC File Offset: 0x00050BEC
		[VisibleToOtherModules]
		internal bool IsPlayableOutputOfType<T>()
		{
			return this.GetPlayableOutputType() == typeof(T);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00052A14 File Offset: 0x00050C14
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x00052A40 File Offset: 0x00050C40
		public static bool operator ==(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x00052A5C File Offset: 0x00050C5C
		public static bool operator !=(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return !PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x00052A78 File Offset: 0x00050C78
		public override bool Equals(object p)
		{
			return p is PlayableOutputHandle && this.Equals((PlayableOutputHandle)p);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00052AA4 File Offset: 0x00050CA4
		public bool Equals(PlayableOutputHandle other)
		{
			return PlayableOutputHandle.CompareVersion(this, other);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00052AC4 File Offset: 0x00050CC4
		internal static bool CompareVersion(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x06002B39 RID: 11065
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsNull();

		// Token: 0x06002B3A RID: 11066
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsValid();

		// Token: 0x06002B3B RID: 11067
		[FreeFunction("PlayableOutputHandleBindings::GetPlayableOutputType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetPlayableOutputType();

		// Token: 0x06002B3C RID: 11068 RVA: 0x00052AFC File Offset: 0x00050CFC
		[FreeFunction("PlayableOutputHandleBindings::GetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal Object GetReferenceObject()
		{
			return Unmarshal.UnmarshalUnityObject<Object>(PlayableOutputHandle.GetReferenceObject_Injected(ref this));
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00052B14 File Offset: 0x00050D14
		[FreeFunction("PlayableOutputHandleBindings::SetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal void SetReferenceObject(Object target)
		{
			PlayableOutputHandle.SetReferenceObject_Injected(ref this, Object.MarshalledUnityObject.Marshal<Object>(target));
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00052B30 File Offset: 0x00050D30
		[FreeFunction("PlayableOutputHandleBindings::GetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal Object GetUserData()
		{
			return Unmarshal.UnmarshalUnityObject<Object>(PlayableOutputHandle.GetUserData_Injected(ref this));
		}

		// Token: 0x06002B3F RID: 11071
		[FreeFunction("PlayableOutputHandleBindings::SetUserData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetUserData([Writable] Object target);

		// Token: 0x06002B40 RID: 11072 RVA: 0x00052B48 File Offset: 0x00050D48
		[FreeFunction("PlayableOutputHandleBindings::GetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetSourcePlayable()
		{
			PlayableHandle playableHandle;
			PlayableOutputHandle.GetSourcePlayable_Injected(ref this, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x00052B60 File Offset: 0x00050D60
		[FreeFunction("PlayableOutputHandleBindings::SetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSourcePlayable(PlayableHandle target, int port)
		{
			PlayableOutputHandle.SetSourcePlayable_Injected(ref this, ref target, port);
		}

		// Token: 0x06002B42 RID: 11074
		[FreeFunction("PlayableOutputHandleBindings::GetSourceOutputPort", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetSourceOutputPort();

		// Token: 0x06002B43 RID: 11075
		[FreeFunction("PlayableOutputHandleBindings::GetWeight", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetWeight();

		// Token: 0x06002B44 RID: 11076
		[FreeFunction("PlayableOutputHandleBindings::SetWeight", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetWeight(float weight);

		// Token: 0x06002B45 RID: 11077 RVA: 0x00052B78 File Offset: 0x00050D78
		[FreeFunction("PlayableOutputHandleBindings::PushNotification", HasExplicitThis = true, ThrowsException = true)]
		internal void PushNotification(PlayableHandle origin, INotification notification, object context)
		{
			PlayableOutputHandle.PushNotification_Injected(ref this, ref origin, notification, context);
		}

		// Token: 0x06002B46 RID: 11078
		[FreeFunction("PlayableOutputHandleBindings::GetNotificationReceivers", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern INotificationReceiver[] GetNotificationReceivers();

		// Token: 0x06002B47 RID: 11079
		[FreeFunction("PlayableOutputHandleBindings::AddNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void AddNotificationReceiver(INotificationReceiver receiver);

		// Token: 0x06002B48 RID: 11080
		[FreeFunction("PlayableOutputHandleBindings::RemoveNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RemoveNotificationReceiver(INotificationReceiver receiver);

		// Token: 0x06002B4A RID: 11082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetReferenceObject_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002B4B RID: 11083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReferenceObject_Injected(ref PlayableOutputHandle _unity_self, IntPtr target);

		// Token: 0x06002B4C RID: 11084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUserData_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x06002B4D RID: 11085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, out PlayableHandle ret);

		// Token: 0x06002B4E RID: 11086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, [In] ref PlayableHandle target, int port);

		// Token: 0x06002B4F RID: 11087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PushNotification_Injected(ref PlayableOutputHandle _unity_self, [In] ref PlayableHandle origin, INotification notification, object context);

		// Token: 0x04000C3A RID: 3130
		internal IntPtr m_Handle;

		// Token: 0x04000C3B RID: 3131
		internal uint m_Version;

		// Token: 0x04000C3C RID: 3132
		private static readonly PlayableOutputHandle m_Null = default(PlayableOutputHandle);
	}
}
