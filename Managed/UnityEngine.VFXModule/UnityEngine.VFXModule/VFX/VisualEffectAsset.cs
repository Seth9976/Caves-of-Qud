using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200001F RID: 31
	[NativeHeader("Modules/VFX/Public/VisualEffectAsset.h")]
	[UsedByNativeCode]
	[NativeHeader("VFXScriptingClasses.h")]
	public class VisualEffectAsset : VisualEffectObject
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00003424 File Offset: 0x00001624
		[FreeFunction(Name = "VisualEffectAssetBindings::GetTextureDimension", HasExplicitThis = true)]
		public TextureDimension GetTextureDimension(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffectAsset.GetTextureDimension_Injected(intPtr, nameID);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003448 File Offset: 0x00001648
		[FreeFunction(Name = "VisualEffectAssetBindings::GetExposedSpace", HasExplicitThis = true)]
		public VFXSpace GetExposedSpace(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffectAsset.GetExposedSpace_Injected(intPtr, nameID);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000346C File Offset: 0x0000166C
		[FreeFunction(Name = "VisualEffectAssetBindings::GetExposedProperties", HasExplicitThis = true)]
		public void GetExposedProperties([NotNull] List<VFXExposedProperty> exposedProperties)
		{
			if (exposedProperties == null)
			{
				ThrowHelper.ThrowArgumentNullException(exposedProperties, "exposedProperties");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffectAsset.GetExposedProperties_Injected(intPtr, exposedProperties);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000034A0 File Offset: 0x000016A0
		[FreeFunction(Name = "VisualEffectAssetBindings::GetEvents", HasExplicitThis = true)]
		public void GetEvents([NotNull] List<string> names)
		{
			if (names == null)
			{
				ThrowHelper.ThrowArgumentNullException(names, "names");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffectAsset.GetEvents_Injected(intPtr, names);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000034D4 File Offset: 0x000016D4
		[FreeFunction(Name = "VisualEffectAssetBindings::HasSystemFromScript", HasExplicitThis = true)]
		internal bool HasSystem(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffectAsset.HasSystem_Injected(intPtr, nameID);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000034F8 File Offset: 0x000016F8
		[FreeFunction(Name = "VisualEffectAssetBindings::GetSystemNamesFromScript", HasExplicitThis = true)]
		internal void GetSystemNames([NotNull] List<string> names)
		{
			if (names == null)
			{
				ThrowHelper.ThrowArgumentNullException(names, "names");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffectAsset.GetSystemNames_Injected(intPtr, names);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000352C File Offset: 0x0000172C
		[FreeFunction(Name = "VisualEffectAssetBindings::GetParticleSystemNamesFromScript", HasExplicitThis = true)]
		internal void GetParticleSystemNames([NotNull] List<string> names)
		{
			if (names == null)
			{
				ThrowHelper.ThrowArgumentNullException(names, "names");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffectAsset.GetParticleSystemNames_Injected(intPtr, names);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003560 File Offset: 0x00001760
		[FreeFunction(Name = "VisualEffectAssetBindings::GetOutputEventNamesFromScript", HasExplicitThis = true)]
		internal void GetOutputEventNames([NotNull] List<string> names)
		{
			if (names == null)
			{
				ThrowHelper.ThrowArgumentNullException(names, "names");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffectAsset.GetOutputEventNames_Injected(intPtr, names);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003594 File Offset: 0x00001794
		[FreeFunction(Name = "VisualEffectAssetBindings::GetSpawnSystemNamesFromScript", HasExplicitThis = true)]
		internal void GetSpawnSystemNames([NotNull] List<string> names)
		{
			if (names == null)
			{
				ThrowHelper.ThrowArgumentNullException(names, "names");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffectAsset>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffectAsset.GetSpawnSystemNames_Injected(intPtr, names);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000035C8 File Offset: 0x000017C8
		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000035E8 File Offset: 0x000017E8
		public VFXSpace GetExposedSpace(string name)
		{
			return this.GetExposedSpace(Shader.PropertyToID(name));
		}

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetTextureDimension_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VFXSpace GetExposedSpace_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetExposedProperties_Injected(IntPtr _unity_self, List<VFXExposedProperty> exposedProperties);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetEvents_Injected(IntPtr _unity_self, List<string> names);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasSystem_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSystemNames_Injected(IntPtr _unity_self, List<string> names);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParticleSystemNames_Injected(IntPtr _unity_self, List<string> names);

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputEventNames_Injected(IntPtr _unity_self, List<string> names);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpawnSystemNames_Injected(IntPtr _unity_self, List<string> names);

		// Token: 0x04000129 RID: 297
		public const string PlayEventName = "OnPlay";

		// Token: 0x0400012A RID: 298
		public const string StopEventName = "OnStop";

		// Token: 0x0400012B RID: 299
		public static readonly int PlayEventID = Shader.PropertyToID("OnPlay");

		// Token: 0x0400012C RID: 300
		public static readonly int StopEventID = Shader.PropertyToID("OnStop");
	}
}
