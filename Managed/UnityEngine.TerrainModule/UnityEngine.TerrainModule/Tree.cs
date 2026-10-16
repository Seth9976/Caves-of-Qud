using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/Terrain/Public/Tree.h")]
	[ExcludeFromPreset]
	public sealed class Tree : Component
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002F9C File Offset: 0x0000119C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002FC4 File Offset: 0x000011C4
		[NativeProperty("TreeData")]
		public ScriptableObject data
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tree>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<ScriptableObject>(Tree.get_data_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tree>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tree.set_data_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ScriptableObject>(value));
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002FEC File Offset: 0x000011EC
		public bool hasSpeedTreeWind
		{
			[NativeMethod("HasSpeedTreeWind")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tree>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Tree.get_hasSpeedTreeWind_Injected(intPtr);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003010 File Offset: 0x00001210
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003038 File Offset: 0x00001238
		[NativeProperty("SpeedTreeWindAsset")]
		public SpeedTreeWindAsset windAsset
		{
			[NativeMethod("GetSpeedTreeWind")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tree>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<SpeedTreeWindAsset>(Tree.get_windAsset_Injected(intPtr));
			}
			[NativeMethod("SetSpeedTreeWind")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tree>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tree.set_windAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<SpeedTreeWindAsset>(value));
			}
		}

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_data_Injected(IntPtr _unity_self);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_data_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasSpeedTreeWind_Injected(IntPtr _unity_self);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_windAsset_Injected(IntPtr _unity_self);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_windAsset_Injected(IntPtr _unity_self, IntPtr value);
	}
}
