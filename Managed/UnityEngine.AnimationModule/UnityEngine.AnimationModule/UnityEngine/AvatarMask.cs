using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[UsedByNativeCode]
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	[NativeHeader("Modules/Animation/AvatarMask.h")]
	public sealed class AvatarMask : Object
	{
		// Token: 0x06000339 RID: 825 RVA: 0x00007198 File Offset: 0x00005398
		public AvatarMask()
		{
			AvatarMask.Internal_Create(this);
		}

		// Token: 0x0600033A RID: 826
		[FreeFunction("AnimationBindings::CreateAvatarMask")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AvatarMask self);

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000071AC File Offset: 0x000053AC
		[Obsolete("AvatarMask.humanoidBodyPartCount is deprecated, use AvatarMaskBodyPart.LastBodyPart instead.")]
		public int humanoidBodyPartCount
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000071C0 File Offset: 0x000053C0
		[NativeMethod("GetBodyPart")]
		public bool GetHumanoidBodyPartActive(AvatarMaskBodyPart index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AvatarMask.GetHumanoidBodyPartActive_Injected(intPtr, index);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000071E4 File Offset: 0x000053E4
		[NativeMethod("SetBodyPart")]
		public void SetHumanoidBodyPartActive(AvatarMaskBodyPart index, bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AvatarMask.SetHumanoidBodyPartActive_Injected(intPtr, index, value);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00007208 File Offset: 0x00005408
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000722C File Offset: 0x0000542C
		public int transformCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AvatarMask.get_transformCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AvatarMask.set_transformCount_Injected(intPtr, value);
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000724F File Offset: 0x0000544F
		public void AddTransformPath(Transform transform)
		{
			this.AddTransformPath(transform, true);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000725C File Offset: 0x0000545C
		public void AddTransformPath([NotNull] Transform transform, [DefaultValue("true")] bool recursive)
		{
			if (transform == null)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(transform);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			AvatarMask.AddTransformPath_Injected(intPtr, intPtr2, recursive);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000072A3 File Offset: 0x000054A3
		public void RemoveTransformPath(Transform transform)
		{
			this.RemoveTransformPath(transform, true);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000072B0 File Offset: 0x000054B0
		public void RemoveTransformPath([NotNull] Transform transform, [DefaultValue("true")] bool recursive)
		{
			if (transform == null)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Transform>(transform);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(transform, "transform");
			}
			AvatarMask.RemoveTransformPath_Injected(intPtr, intPtr2, recursive);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000072F8 File Offset: 0x000054F8
		public string GetTransformPath(int index)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				AvatarMask.GetTransformPath_Injected(intPtr, index, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00007338 File Offset: 0x00005538
		public unsafe void SetTransformPath(int index, string path)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				AvatarMask.SetTransformPath_Injected(intPtr, index, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000073A0 File Offset: 0x000055A0
		private float GetTransformWeight(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AvatarMask.GetTransformWeight_Injected(intPtr, index);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000073C4 File Offset: 0x000055C4
		private void SetTransformWeight(int index, float weight)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AvatarMask.SetTransformWeight_Injected(intPtr, index, weight);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000073E8 File Offset: 0x000055E8
		public bool GetTransformActive(int index)
		{
			return this.GetTransformWeight(index) > 0.5f;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007408 File Offset: 0x00005608
		public void SetTransformActive(int index, bool value)
		{
			this.SetTransformWeight(index, value ? 1f : 0f);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00007424 File Offset: 0x00005624
		internal bool hasFeetIK
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AvatarMask>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AvatarMask.get_hasFeetIK_Injected(intPtr);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00007448 File Offset: 0x00005648
		internal void Copy(AvatarMask other)
		{
			for (AvatarMaskBodyPart avatarMaskBodyPart = AvatarMaskBodyPart.Root; avatarMaskBodyPart < AvatarMaskBodyPart.LastBodyPart; avatarMaskBodyPart++)
			{
				this.SetHumanoidBodyPartActive(avatarMaskBodyPart, other.GetHumanoidBodyPartActive(avatarMaskBodyPart));
			}
			this.transformCount = other.transformCount;
			for (int i = 0; i < other.transformCount; i++)
			{
				this.SetTransformPath(i, other.GetTransformPath(i));
				this.SetTransformActive(i, other.GetTransformActive(i));
			}
		}

		// Token: 0x0600034C RID: 844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetHumanoidBodyPartActive_Injected(IntPtr _unity_self, AvatarMaskBodyPart index);

		// Token: 0x0600034D RID: 845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHumanoidBodyPartActive_Injected(IntPtr _unity_self, AvatarMaskBodyPart index, bool value);

		// Token: 0x0600034E RID: 846
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_transformCount_Injected(IntPtr _unity_self);

		// Token: 0x0600034F RID: 847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_transformCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000350 RID: 848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTransformPath_Injected(IntPtr _unity_self, IntPtr transform, [DefaultValue("true")] bool recursive);

		// Token: 0x06000351 RID: 849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveTransformPath_Injected(IntPtr _unity_self, IntPtr transform, [DefaultValue("true")] bool recursive);

		// Token: 0x06000352 RID: 850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTransformPath_Injected(IntPtr _unity_self, int index, out ManagedSpanWrapper ret);

		// Token: 0x06000353 RID: 851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransformPath_Injected(IntPtr _unity_self, int index, ref ManagedSpanWrapper path);

		// Token: 0x06000354 RID: 852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetTransformWeight_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000355 RID: 853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransformWeight_Injected(IntPtr _unity_self, int index, float weight);

		// Token: 0x06000356 RID: 854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasFeetIK_Injected(IntPtr _unity_self);
	}
}
