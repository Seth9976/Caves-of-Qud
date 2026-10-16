using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000F RID: 15
	[MovedFrom("UnityEngine")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/OffMeshLink.html")]
	[Obsolete("The OffMeshLink component is no longer supported and will be removed. Use NavMeshLink instead.")]
	public sealed class OffMeshLink : Behaviour
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000038E8 File Offset: 0x00001AE8
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000390C File Offset: 0x00001B0C
		[Obsolete("activated has been deprecated together with the class. Declare the object as NavMeshLink and use activated as before.")]
		public bool activated
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OffMeshLink.get_activated_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_activated_Injected(intPtr, value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003930 File Offset: 0x00001B30
		[Obsolete("occupied has been deprecated together with the class. Declare the object as NavMeshLink and use occupied as before.")]
		public bool occupied
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OffMeshLink.get_occupied_Injected(intPtr);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00003954 File Offset: 0x00001B54
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00003978 File Offset: 0x00001B78
		[Obsolete("costOverride has been deprecated together with the class. Declare the object as NavMeshLink and use costModifier instead.")]
		public float costOverride
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OffMeshLink.get_costOverride_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_costOverride_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000399C File Offset: 0x00001B9C
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000039C0 File Offset: 0x00001BC0
		[Obsolete("biDirectional has been deprecated together with the class. Declare the object as NavMeshLink and use bidirectional instead.")]
		public bool biDirectional
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OffMeshLink.get_biDirectional_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_biDirectional_Injected(intPtr, value);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000039E4 File Offset: 0x00001BE4
		[Obsolete("UpdatePositions() has been deprecated together with the class. Declare the object as NavMeshLink and use UpdateLink() instead.")]
		public void UpdatePositions()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			OffMeshLink.UpdatePositions_Injected(intPtr);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003A08 File Offset: 0x00001C08
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00003A20 File Offset: 0x00001C20
		[Obsolete("navMeshLayer has been deprecated together with the class. Declare the object as NavMeshLink and use area instead. (UnityUpgradable) -> area")]
		public int navMeshLayer
		{
			get
			{
				return this.area;
			}
			set
			{
				this.area = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003A2C File Offset: 0x00001C2C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00003A50 File Offset: 0x00001C50
		[Obsolete("area has been deprecated together with the class. Declare the object as NavMeshLink and use area as before.")]
		public int area
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OffMeshLink.get_area_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_area_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003A74 File Offset: 0x00001C74
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00003A98 File Offset: 0x00001C98
		[Obsolete("autoUpdatePositions has been deprecated together with the class. Declare the object as NavMeshLink and use autoUpdate instead.")]
		public bool autoUpdatePositions
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return OffMeshLink.get_autoUpdatePositions_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_autoUpdatePositions_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003ABC File Offset: 0x00001CBC
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00003AE4 File Offset: 0x00001CE4
		[Obsolete("startTransform has been deprecated together with the class. Declare the object as NavMeshLink and use startTransform as before.")]
		public Transform startTransform
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(OffMeshLink.get_startTransform_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_startTransform_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(value));
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003B0C File Offset: 0x00001D0C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00003B34 File Offset: 0x00001D34
		[Obsolete("endTransform has been deprecated together with the class. Declare the object as NavMeshLink and use endTransform as before.")]
		public Transform endTransform
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(OffMeshLink.get_endTransform_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<OffMeshLink>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				OffMeshLink.set_endTransform_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(value));
			}
		}

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_activated_Injected(IntPtr _unity_self);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_activated_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_occupied_Injected(IntPtr _unity_self);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_costOverride_Injected(IntPtr _unity_self);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_costOverride_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_biDirectional_Injected(IntPtr _unity_self);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_biDirectional_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000142 RID: 322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdatePositions_Injected(IntPtr _unity_self);

		// Token: 0x06000143 RID: 323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_area_Injected(IntPtr _unity_self);

		// Token: 0x06000144 RID: 324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_area_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000145 RID: 325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoUpdatePositions_Injected(IntPtr _unity_self);

		// Token: 0x06000146 RID: 326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoUpdatePositions_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000147 RID: 327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_startTransform_Injected(IntPtr _unity_self);

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startTransform_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000149 RID: 329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_endTransform_Injected(IntPtr _unity_self);

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endTransform_Injected(IntPtr _unity_self, IntPtr value);
	}
}
