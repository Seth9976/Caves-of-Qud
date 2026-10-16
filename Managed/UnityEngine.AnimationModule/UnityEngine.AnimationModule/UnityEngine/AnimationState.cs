using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000010 RID: 16
	[NativeHeader("Modules/Animation/AnimationState.h")]
	[UsedByNativeCode]
	public sealed class AnimationState : TrackedReference
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002B70 File Offset: 0x00000D70
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002B94 File Offset: 0x00000D94
		public bool enabled
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002BDC File Offset: 0x00000DDC
		public float weight
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002C00 File Offset: 0x00000E00
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002C24 File Offset: 0x00000E24
		public WrapMode wrapMode
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_wrapMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_wrapMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002C48 File Offset: 0x00000E48
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002C6C File Offset: 0x00000E6C
		public float time
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002C90 File Offset: 0x00000E90
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public float normalizedTime
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_normalizedTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_normalizedTime_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002CFC File Offset: 0x00000EFC
		public float speed
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_speed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_speed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002D20 File Offset: 0x00000F20
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002D44 File Offset: 0x00000F44
		public float normalizedSpeed
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_normalizedSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_normalizedSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002D68 File Offset: 0x00000F68
		public float length
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public int layer
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_layer_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_layer_Injected(intPtr, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public AnimationClip clip
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AnimationClip>(AnimationState.get_clip_Injected(intPtr));
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002DFC File Offset: 0x00000FFC
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002E3C File Offset: 0x0000103C
		public unsafe string name
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					AnimationState.get_name_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					AnimationState.set_name_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002EA0 File Offset: 0x000010A0
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002EC4 File Offset: 0x000010C4
		public AnimationBlendMode blendMode
		{
			get
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AnimationState.get_blendMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AnimationState.set_blendMode_Injected(intPtr, value);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002EE7 File Offset: 0x000010E7
		[ExcludeFromDocs]
		public void AddMixingTransform(Transform mix)
		{
			this.AddMixingTransform(mix, true);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002EF4 File Offset: 0x000010F4
		public void AddMixingTransform([NotNull] Transform mix, [DefaultValue("true")] bool recursive)
		{
			if (mix == null)
			{
				ThrowHelper.ThrowArgumentNullException(mix, "mix");
			}
			IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(mix);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mix, "mix");
			}
			AnimationState.AddMixingTransform_Injected(intPtr, intPtr2, recursive);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002F3C File Offset: 0x0000113C
		public void RemoveMixingTransform([NotNull] Transform mix)
		{
			if (mix == null)
			{
				ThrowHelper.ThrowArgumentNullException(mix, "mix");
			}
			IntPtr intPtr = AnimationState.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(mix);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mix, "mix");
			}
			AnimationState.RemoveMixingTransform_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern WrapMode get_wrapMode_Injected(IntPtr _unity_self);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wrapMode_Injected(IntPtr _unity_self, WrapMode value);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_normalizedTime_Injected(IntPtr _unity_self);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_normalizedTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_speed_Injected(IntPtr _unity_self);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_speed_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_normalizedSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_normalizedSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_length_Injected(IntPtr _unity_self);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layer_Injected(IntPtr _unity_self);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layer_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_clip_Injected(IntPtr _unity_self);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_name_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_name_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationBlendMode get_blendMode_Injected(IntPtr _unity_self);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_blendMode_Injected(IntPtr _unity_self, AnimationBlendMode value);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddMixingTransform_Injected(IntPtr _unity_self, IntPtr mix, [DefaultValue("true")] bool recursive);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveMixingTransform_Injected(IntPtr _unity_self, IntPtr mix);

		// Token: 0x02000011 RID: 17
		internal static class BindingsMarshaller
		{
			// Token: 0x060000AB RID: 171 RVA: 0x00002F8B File Offset: 0x0000118B
			public static IntPtr ConvertToNative(AnimationState animationState)
			{
				return animationState.m_Ptr;
			}
		}
	}
}
