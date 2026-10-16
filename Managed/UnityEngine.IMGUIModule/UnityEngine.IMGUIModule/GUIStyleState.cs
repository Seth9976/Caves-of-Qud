using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyleState
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C238 File Offset: 0x0000A438
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000C260 File Offset: 0x0000A460
		[NativeProperty("Background", false, TargetType.Function)]
		public Texture2D background
		{
			get
			{
				IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(GUIStyleState.get_background_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyleState.set_background_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture2D>(value));
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C288 File Offset: 0x0000A488
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		[NativeProperty("textColor", false, TargetType.Field)]
		public Color textColor
		{
			get
			{
				IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				GUIStyleState.get_textColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyleState.set_textColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000319 RID: 793
		[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		// Token: 0x0600031A RID: 794 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		private void Cleanup()
		{
			IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyleState.Cleanup_Injected(intPtr);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C2F6 File Offset: 0x0000A4F6
		public GUIStyleState()
		{
			this.m_Ptr = GUIStyleState.Init();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C30B File Offset: 0x0000A50B
		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C324 File Offset: 0x0000A524
		internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C340 File Offset: 0x0000A540
		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C35C File Offset: 0x0000A55C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Cleanup();
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000320 RID: 800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_background_Injected(IntPtr _unity_self);

		// Token: 0x06000321 RID: 801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_background_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000322 RID: 802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_textColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000323 RID: 803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000324 RID: 804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cleanup_Injected(IntPtr _unity_self);

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000CF RID: 207
		private readonly GUIStyle m_SourceStyle;

		// Token: 0x0200002C RID: 44
		internal static class BindingsMarshaller
		{
			// Token: 0x06000325 RID: 805 RVA: 0x0000C3A8 File Offset: 0x0000A5A8
			public static IntPtr ConvertToNative(GUIStyleState guiStyleState)
			{
				return guiStyleState.m_Ptr;
			}
		}
	}
}
