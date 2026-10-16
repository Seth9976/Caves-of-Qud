using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectBindings.h")]
	[NativeHeader("Modules/VFX/Public/VisualEffect.h")]
	[RequireComponent(typeof(Transform))]
	public class VisualEffect : Behaviour
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003650 File Offset: 0x00001850
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003674 File Offset: 0x00001874
		public bool pause
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_pause_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualEffect.set_pause_Injected(intPtr, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003698 File Offset: 0x00001898
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000036BC File Offset: 0x000018BC
		public float playRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_playRate_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualEffect.set_playRate_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000036E0 File Offset: 0x000018E0
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00003704 File Offset: 0x00001904
		public uint startSeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_startSeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualEffect.set_startSeed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003728 File Offset: 0x00001928
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000374C File Offset: 0x0000194C
		public bool resetSeedOnPlay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_resetSeedOnPlay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualEffect.set_resetSeedOnPlay_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003770 File Offset: 0x00001970
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00003794 File Offset: 0x00001994
		public int initialEventID
		{
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventID", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_initialEventID_Injected(intPtr);
			}
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventID", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualEffect.set_initialEventID_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000037B8 File Offset: 0x000019B8
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000037F8 File Offset: 0x000019F8
		public unsafe string initialEventName
		{
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventName", HasExplicitThis = true)]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					VisualEffect.get_initialEventName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventName", HasExplicitThis = true)]
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
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
					VisualEffect.set_initialEventName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000385C File Offset: 0x00001A5C
		public bool culled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_culled_Injected(intPtr);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003880 File Offset: 0x00001A80
		// (set) Token: 0x06000112 RID: 274 RVA: 0x000038A8 File Offset: 0x00001AA8
		public VisualEffectAsset visualEffectAsset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<VisualEffectAsset>(VisualEffect.get_visualEffectAsset_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualEffect.set_visualEffectAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<VisualEffectAsset>(value));
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000038D0 File Offset: 0x00001AD0
		public VFXEventAttribute CreateVFXEventAttribute()
		{
			bool flag = this.visualEffectAsset == null;
			VFXEventAttribute vfxeventAttribute;
			if (flag)
			{
				vfxeventAttribute = null;
			}
			else
			{
				VFXEventAttribute vfxeventAttribute2 = VFXEventAttribute.Internal_InstanciateVFXEventAttribute(this.visualEffectAsset);
				vfxeventAttribute = vfxeventAttribute2;
			}
			return vfxeventAttribute;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003904 File Offset: 0x00001B04
		private void CheckValidVFXEventAttribute(VFXEventAttribute eventAttribute)
		{
			bool flag = eventAttribute != null && eventAttribute.vfxAsset != this.visualEffectAsset;
			if (flag)
			{
				throw new InvalidOperationException("Invalid VFXEventAttribute provided to VisualEffect. It has been created with another VisualEffectAsset. Use CreateVFXEventAttribute.");
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000393C File Offset: 0x00001B3C
		[FreeFunction(Name = "VisualEffectBindings::SendEventFromScript", HasExplicitThis = true)]
		private void SendEventFromScript(int eventNameID, VFXEventAttribute eventAttribute)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SendEventFromScript_Injected(intPtr, eventNameID, (eventAttribute == null) ? ((IntPtr)0) : VFXEventAttribute.BindingsMarshaller.ConvertToNative(eventAttribute));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000396E File Offset: 0x00001B6E
		public void SendEvent(int eventNameID, VFXEventAttribute eventAttribute)
		{
			this.CheckValidVFXEventAttribute(eventAttribute);
			this.SendEventFromScript(eventNameID, eventAttribute);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003982 File Offset: 0x00001B82
		public void SendEvent(string eventName, VFXEventAttribute eventAttribute)
		{
			this.SendEvent(Shader.PropertyToID(eventName), eventAttribute);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003993 File Offset: 0x00001B93
		public void SendEvent(int eventNameID)
		{
			this.SendEventFromScript(eventNameID, null);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000399F File Offset: 0x00001B9F
		public void SendEvent(string eventName)
		{
			this.SendEvent(Shader.PropertyToID(eventName), null);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000039B0 File Offset: 0x00001BB0
		public void Play(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.PlayEventID, eventAttribute);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000039C0 File Offset: 0x00001BC0
		public void Play()
		{
			this.SendEvent(VisualEffectAsset.PlayEventID);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000039CF File Offset: 0x00001BCF
		public void Stop(VFXEventAttribute eventAttribute)
		{
			this.SendEvent(VisualEffectAsset.StopEventID, eventAttribute);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000039DF File Offset: 0x00001BDF
		public void Stop()
		{
			this.SendEvent(VisualEffectAsset.StopEventID);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000039EE File Offset: 0x00001BEE
		public void Reinit()
		{
			this.Reinit(true);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000039FC File Offset: 0x00001BFC
		internal void Reinit(bool sendInitialEventAndPrewarm = true)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.Reinit_Injected(intPtr, sendInitialEventAndPrewarm);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003A20 File Offset: 0x00001C20
		public void AdvanceOneFrame()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.AdvanceOneFrame_Injected(intPtr);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003A44 File Offset: 0x00001C44
		internal void RecreateData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.RecreateData_Injected(intPtr);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003A68 File Offset: 0x00001C68
		[FreeFunction(Name = "VisualEffectBindings::GetGPUTaskMarkerName", HasExplicitThis = true, ThrowsException = true)]
		[NativeConditional("ENABLE_PROFILER")]
		private string GetGPUTaskMarkerName(int nameID, int taskIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VisualEffect.GetGPUTaskMarkerName_Injected(intPtr, nameID, taskIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003AAC File Offset: 0x00001CAC
		[FreeFunction(Name = "VisualEffectBindings::GetCPUEffectMarkerName", HasExplicitThis = true, ThrowsException = true)]
		[NativeConditional("ENABLE_PROFILER")]
		internal string GetCPUEffectMarkerName(int markerIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VisualEffect.GetCPUEffectMarkerName_Injected(intPtr, markerIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003AEC File Offset: 0x00001CEC
		[FreeFunction(Name = "VisualEffectBindings::GetCPUSystemMarkerName", HasExplicitThis = true, ThrowsException = true)]
		[NativeConditional("ENABLE_PROFILER")]
		private string GetCPUSystemMarkerName(int nameID)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VisualEffect.GetCPUSystemMarkerName_Injected(intPtr, nameID, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003B2C File Offset: 0x00001D2C
		[NativeConditional("ENABLE_PROFILER")]
		[FreeFunction(Name = "VisualEffectBindings::RegisterForProfiling", HasExplicitThis = true, ThrowsException = false)]
		internal void RegisterForProfiling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.RegisterForProfiling_Injected(intPtr);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003B50 File Offset: 0x00001D50
		[FreeFunction(Name = "VisualEffectBindings::UnregisterForProfiling", HasExplicitThis = true, ThrowsException = false)]
		[NativeConditional("ENABLE_PROFILER")]
		internal void UnregisterForProfiling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.UnregisterForProfiling_Injected(intPtr);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003B74 File Offset: 0x00001D74
		[FreeFunction(Name = "VisualEffectBindings::IsRegisteredForProfiling", HasExplicitThis = true, ThrowsException = false)]
		[NativeConditional("ENABLE_PROFILER")]
		internal bool IsRegisteredForProfiling()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.IsRegisteredForProfiling_Injected(intPtr);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003B98 File Offset: 0x00001D98
		[FreeFunction(Name = "VisualEffectBindings::ResetOverrideFromScript", HasExplicitThis = true)]
		public void ResetOverride(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.ResetOverride_Injected(intPtr, nameID);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003BBC File Offset: 0x00001DBC
		[FreeFunction(Name = "VisualEffectBindings::GetTextureDimensionFromScript", HasExplicitThis = true)]
		public TextureDimension GetTextureDimension(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.GetTextureDimension_Injected(intPtr, nameID);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003BE0 File Offset: 0x00001DE0
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<bool>", HasExplicitThis = true)]
		public bool HasBool(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasBool_Injected(intPtr, nameID);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003C04 File Offset: 0x00001E04
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<int>", HasExplicitThis = true)]
		public bool HasInt(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasInt_Injected(intPtr, nameID);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003C28 File Offset: 0x00001E28
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<UInt32>", HasExplicitThis = true)]
		public bool HasUInt(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasUInt_Injected(intPtr, nameID);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003C4C File Offset: 0x00001E4C
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<float>", HasExplicitThis = true)]
		public bool HasFloat(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasFloat_Injected(intPtr, nameID);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003C70 File Offset: 0x00001E70
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector2f>", HasExplicitThis = true)]
		public bool HasVector2(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasVector2_Injected(intPtr, nameID);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003C94 File Offset: 0x00001E94
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector3f>", HasExplicitThis = true)]
		public bool HasVector3(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasVector3_Injected(intPtr, nameID);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003CB8 File Offset: 0x00001EB8
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector4f>", HasExplicitThis = true)]
		public bool HasVector4(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasVector4_Injected(intPtr, nameID);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003CDC File Offset: 0x00001EDC
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public bool HasMatrix4x4(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasMatrix4x4_Injected(intPtr, nameID);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003D00 File Offset: 0x00001F00
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Texture*>", HasExplicitThis = true)]
		public bool HasTexture(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasTexture_Injected(intPtr, nameID);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003D24 File Offset: 0x00001F24
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		public bool HasAnimationCurve(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasAnimationCurve_Injected(intPtr, nameID);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003D48 File Offset: 0x00001F48
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Gradient*>", HasExplicitThis = true)]
		public bool HasGradient(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasGradient_Injected(intPtr, nameID);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003D6C File Offset: 0x00001F6C
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Mesh*>", HasExplicitThis = true)]
		public bool HasMesh(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasMesh_Injected(intPtr, nameID);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003D90 File Offset: 0x00001F90
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		public bool HasSkinnedMeshRenderer(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasSkinnedMeshRenderer_Injected(intPtr, nameID);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		public bool HasGraphicsBuffer(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasGraphicsBuffer_Injected(intPtr, nameID);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003DD8 File Offset: 0x00001FD8
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<bool>", HasExplicitThis = true)]
		public void SetBool(int nameID, bool b)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetBool_Injected(intPtr, nameID, b);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003DFC File Offset: 0x00001FFC
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<int>", HasExplicitThis = true)]
		public void SetInt(int nameID, int i)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetInt_Injected(intPtr, nameID, i);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003E20 File Offset: 0x00002020
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<UInt32>", HasExplicitThis = true)]
		public void SetUInt(int nameID, uint i)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetUInt_Injected(intPtr, nameID, i);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00003E44 File Offset: 0x00002044
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<float>", HasExplicitThis = true)]
		public void SetFloat(int nameID, float f)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetFloat_Injected(intPtr, nameID, f);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003E68 File Offset: 0x00002068
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public void SetVector2(int nameID, Vector2 v)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetVector2_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003E90 File Offset: 0x00002090
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public void SetVector3(int nameID, Vector3 v)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetVector3_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003EB8 File Offset: 0x000020B8
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public void SetVector4(int nameID, Vector4 v)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetVector4_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003EE0 File Offset: 0x000020E0
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetMatrix4x4_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003F08 File Offset: 0x00002108
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Texture*>", HasExplicitThis = true)]
		public void SetTexture(int nameID, [NotNull] Texture t)
		{
			if (t == null)
			{
				ThrowHelper.ThrowArgumentNullException(t, "t");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(t);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(t, "t");
			}
			VisualEffect.SetTexture_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003F50 File Offset: 0x00002150
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		public void SetAnimationCurve(int nameID, [NotNull] AnimationCurve c)
		{
			if (c == null)
			{
				ThrowHelper.ThrowArgumentNullException(c, "c");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = AnimationCurve.BindingsMarshaller.ConvertToNative(c);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(c, "c");
			}
			VisualEffect.SetAnimationCurve_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003F98 File Offset: 0x00002198
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Gradient*>", HasExplicitThis = true)]
		public void SetGradient(int nameID, [NotNull] Gradient g)
		{
			if (g == null)
			{
				ThrowHelper.ThrowArgumentNullException(g, "g");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Gradient.BindingsMarshaller.ConvertToNative(g);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(g, "g");
			}
			VisualEffect.SetGradient_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003FE0 File Offset: 0x000021E0
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Mesh*>", HasExplicitThis = true)]
		public void SetMesh(int nameID, [NotNull] Mesh m)
		{
			if (m == null)
			{
				ThrowHelper.ThrowArgumentNullException(m, "m");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(m);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(m, "m");
			}
			VisualEffect.SetMesh_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004028 File Offset: 0x00002228
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		public void SetSkinnedMeshRenderer(int nameID, SkinnedMeshRenderer m)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetSkinnedMeshRenderer_Injected(intPtr, nameID, Object.MarshalledUnityObject.Marshal<SkinnedMeshRenderer>(m));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004054 File Offset: 0x00002254
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		public void SetGraphicsBuffer(int nameID, GraphicsBuffer g)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.SetGraphicsBuffer_Injected(intPtr, nameID, (g == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(g));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004088 File Offset: 0x00002288
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<bool>", HasExplicitThis = true)]
		public bool GetBool(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.GetBool_Injected(intPtr, nameID);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000040AC File Offset: 0x000022AC
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<int>", HasExplicitThis = true)]
		public int GetInt(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.GetInt_Injected(intPtr, nameID);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000040D0 File Offset: 0x000022D0
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<UInt32>", HasExplicitThis = true)]
		public uint GetUInt(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.GetUInt_Injected(intPtr, nameID);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000040F4 File Offset: 0x000022F4
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<float>", HasExplicitThis = true)]
		public float GetFloat(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.GetFloat_Injected(intPtr, nameID);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004118 File Offset: 0x00002318
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public Vector2 GetVector2(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			VisualEffect.GetVector2_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004140 File Offset: 0x00002340
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public Vector3 GetVector3(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			VisualEffect.GetVector3_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004168 File Offset: 0x00002368
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public Vector4 GetVector4(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			VisualEffect.GetVector4_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004190 File Offset: 0x00002390
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			VisualEffect.GetMatrix4x4_Injected(intPtr, nameID, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000041B8 File Offset: 0x000023B8
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Texture*>", HasExplicitThis = true)]
		public Texture GetTexture(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture>(VisualEffect.GetTexture_Injected(intPtr, nameID));
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000041E0 File Offset: 0x000023E0
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Mesh*>", HasExplicitThis = true)]
		public Mesh GetMesh(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Mesh>(VisualEffect.GetMesh_Injected(intPtr, nameID));
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004208 File Offset: 0x00002408
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		public SkinnedMeshRenderer GetSkinnedMeshRenderer(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<SkinnedMeshRenderer>(VisualEffect.GetSkinnedMeshRenderer_Injected(intPtr, nameID));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004230 File Offset: 0x00002430
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		internal GraphicsBuffer GetGraphicsBuffer(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr graphicsBuffer_Injected = VisualEffect.GetGraphicsBuffer_Injected(intPtr, nameID);
			return (graphicsBuffer_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(graphicsBuffer_Injected);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004264 File Offset: 0x00002464
		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradient(nameID, gradient);
			return gradient;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004288 File Offset: 0x00002488
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetGradientFromScript", HasExplicitThis = true)]
		private void Internal_GetGradient(int nameID, Gradient gradient)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.Internal_GetGradient_Injected(intPtr, nameID, (gradient == null) ? ((IntPtr)0) : Gradient.BindingsMarshaller.ConvertToNative(gradient));
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000042BC File Offset: 0x000024BC
		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurve(nameID, animationCurve);
			return animationCurve;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000042E0 File Offset: 0x000024E0
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetAnimationCurveFromScript", HasExplicitThis = true)]
		private void Internal_GetAnimationCurve(int nameID, AnimationCurve curve)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.Internal_GetAnimationCurve_Injected(intPtr, nameID, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004314 File Offset: 0x00002514
		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		public VFXParticleSystemInfo GetParticleSystemInfo(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXParticleSystemInfo vfxparticleSystemInfo;
			VisualEffect.GetParticleSystemInfo_Injected(intPtr, nameID, out vfxparticleSystemInfo);
			return vfxparticleSystemInfo;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000433C File Offset: 0x0000253C
		[FreeFunction(Name = "VisualEffectBindings::GetSpawnSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		private void GetSpawnSystemInfo(int nameID, IntPtr spawnerState)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.GetSpawnSystemInfo_Injected(intPtr, nameID, spawnerState);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004360 File Offset: 0x00002560
		public bool HasAnySystemAwake()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualEffect.HasAnySystemAwake_Injected(intPtr);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00004384 File Offset: 0x00002584
		[FreeFunction(Name = "VisualEffectBindings::GetComputedBounds", HasExplicitThis = true)]
		internal Bounds GetComputedBounds(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Bounds bounds;
			VisualEffect.GetComputedBounds_Injected(intPtr, nameID, out bounds);
			return bounds;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000043AC File Offset: 0x000025AC
		[FreeFunction(Name = "VisualEffectBindings::GetCurrentBoundsPadding", HasExplicitThis = true)]
		internal Vector3 GetCurrentBoundsPadding(int nameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			VisualEffect.GetCurrentBoundsPadding_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000043D4 File Offset: 0x000025D4
		public void GetSpawnSystemInfo(int nameID, VFXSpawnerState spawnState)
		{
			bool flag = spawnState == null;
			if (flag)
			{
				throw new NullReferenceException("GetSpawnSystemInfo expects a non null VFXSpawnerState.");
			}
			IntPtr ptr = spawnState.GetPtr();
			bool flag2 = ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new NullReferenceException("GetSpawnSystemInfo use an unexpected not owned VFXSpawnerState.");
			}
			this.GetSpawnSystemInfo(nameID, ptr);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004420 File Offset: 0x00002620
		public VFXSpawnerState GetSpawnSystemInfo(int nameID)
		{
			VFXSpawnerState vfxspawnerState = new VFXSpawnerState();
			this.GetSpawnSystemInfo(nameID, vfxspawnerState);
			return vfxspawnerState;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004444 File Offset: 0x00002644
		public bool HasSystem(int nameID)
		{
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			return visualEffectAsset != null && visualEffectAsset.HasSystem(nameID);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004470 File Offset: 0x00002670
		public void GetSystemNames(List<string> names)
		{
			bool flag = names == null;
			if (flag)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			bool flag2 = visualEffectAsset;
			if (flag2)
			{
				visualEffectAsset.GetSystemNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000044B4 File Offset: 0x000026B4
		public void GetParticleSystemNames(List<string> names)
		{
			bool flag = names == null;
			if (flag)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			bool flag2 = visualEffectAsset;
			if (flag2)
			{
				visualEffectAsset.GetParticleSystemNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000044F8 File Offset: 0x000026F8
		public void GetOutputEventNames(List<string> names)
		{
			bool flag = names == null;
			if (flag)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			bool flag2 = visualEffectAsset;
			if (flag2)
			{
				visualEffectAsset.GetOutputEventNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000453C File Offset: 0x0000273C
		public void GetSpawnSystemNames(List<string> names)
		{
			bool flag = names == null;
			if (flag)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			bool flag2 = visualEffectAsset;
			if (flag2)
			{
				visualEffectAsset.GetSpawnSystemNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000457F File Offset: 0x0000277F
		public void ResetOverride(string name)
		{
			this.ResetOverride(Shader.PropertyToID(name));
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004590 File Offset: 0x00002790
		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000045B0 File Offset: 0x000027B0
		public bool HasUInt(string name)
		{
			return this.HasUInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000045D0 File Offset: 0x000027D0
		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000045F0 File Offset: 0x000027F0
		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00004610 File Offset: 0x00002810
		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004630 File Offset: 0x00002830
		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004650 File Offset: 0x00002850
		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004670 File Offset: 0x00002870
		public bool HasTexture(string name)
		{
			return this.HasTexture(Shader.PropertyToID(name));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004690 File Offset: 0x00002890
		public TextureDimension GetTextureDimension(string name)
		{
			return this.GetTextureDimension(Shader.PropertyToID(name));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000046B0 File Offset: 0x000028B0
		public bool HasAnimationCurve(string name)
		{
			return this.HasAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000046D0 File Offset: 0x000028D0
		public bool HasGradient(string name)
		{
			return this.HasGradient(Shader.PropertyToID(name));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000046F0 File Offset: 0x000028F0
		public bool HasMesh(string name)
		{
			return this.HasMesh(Shader.PropertyToID(name));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004710 File Offset: 0x00002910
		public bool HasSkinnedMeshRenderer(string name)
		{
			return this.HasSkinnedMeshRenderer(Shader.PropertyToID(name));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004730 File Offset: 0x00002930
		public bool HasGraphicsBuffer(string name)
		{
			return this.HasGraphicsBuffer(Shader.PropertyToID(name));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004750 File Offset: 0x00002950
		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000476E File Offset: 0x0000296E
		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000477F File Offset: 0x0000297F
		public void SetUInt(string name, uint i)
		{
			this.SetUInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004790 File Offset: 0x00002990
		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000047A1 File Offset: 0x000029A1
		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000047B2 File Offset: 0x000029B2
		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000047C3 File Offset: 0x000029C3
		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000047D4 File Offset: 0x000029D4
		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000047E5 File Offset: 0x000029E5
		public void SetTexture(string name, Texture t)
		{
			this.SetTexture(Shader.PropertyToID(name), t);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000047F6 File Offset: 0x000029F6
		public void SetAnimationCurve(string name, AnimationCurve c)
		{
			this.SetAnimationCurve(Shader.PropertyToID(name), c);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004807 File Offset: 0x00002A07
		public void SetGradient(string name, Gradient g)
		{
			this.SetGradient(Shader.PropertyToID(name), g);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004818 File Offset: 0x00002A18
		public void SetMesh(string name, Mesh m)
		{
			this.SetMesh(Shader.PropertyToID(name), m);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004829 File Offset: 0x00002A29
		public void SetSkinnedMeshRenderer(string name, SkinnedMeshRenderer m)
		{
			this.SetSkinnedMeshRenderer(Shader.PropertyToID(name), m);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000483A File Offset: 0x00002A3A
		public void SetGraphicsBuffer(string name, GraphicsBuffer g)
		{
			this.SetGraphicsBuffer(Shader.PropertyToID(name), g);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000484B File Offset: 0x00002A4B
		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000485C File Offset: 0x00002A5C
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000487C File Offset: 0x00002A7C
		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000489C File Offset: 0x00002A9C
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000048BC File Offset: 0x00002ABC
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000048DC File Offset: 0x00002ADC
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000048FC File Offset: 0x00002AFC
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000491C File Offset: 0x00002B1C
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000493C File Offset: 0x00002B3C
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000495C File Offset: 0x00002B5C
		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000497C File Offset: 0x00002B7C
		public SkinnedMeshRenderer GetSkinnedMeshRenderer(string name)
		{
			return this.GetSkinnedMeshRenderer(Shader.PropertyToID(name));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000499C File Offset: 0x00002B9C
		internal GraphicsBuffer GetGraphicsBuffer(string name)
		{
			return this.GetGraphicsBuffer(Shader.PropertyToID(name));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000049BC File Offset: 0x00002BBC
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000049DC File Offset: 0x00002BDC
		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000049FC File Offset: 0x00002BFC
		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004A1C File Offset: 0x00002C1C
		public bool HasSystem(string name)
		{
			return this.HasSystem(Shader.PropertyToID(name));
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004A3C File Offset: 0x00002C3C
		public VFXParticleSystemInfo GetParticleSystemInfo(string name)
		{
			return this.GetParticleSystemInfo(Shader.PropertyToID(name));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004A5C File Offset: 0x00002C5C
		internal string GetGPUTaskMarkerName(string systemName, int taskIndex)
		{
			return this.GetGPUTaskMarkerName(Shader.PropertyToID(systemName), taskIndex);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004A7C File Offset: 0x00002C7C
		internal string GetCPUSystemMarkerName(string systemName)
		{
			return this.GetCPUSystemMarkerName(Shader.PropertyToID(systemName));
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004A9C File Offset: 0x00002C9C
		internal string GetCPUEffectMarkerName(VisualEffect.VFXCPUEffectMarkers markerId)
		{
			return this.GetCPUEffectMarkerName((int)markerId);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public VFXSpawnerState GetSpawnSystemInfo(string name)
		{
			return this.GetSpawnSystemInfo(Shader.PropertyToID(name));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004AD8 File Offset: 0x00002CD8
		internal Bounds GetComputedBounds(string name)
		{
			return this.GetComputedBounds(Shader.PropertyToID(name));
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004AF8 File Offset: 0x00002CF8
		internal Vector3 GetCurrentBoundsPadding(string name)
		{
			return this.GetCurrentBoundsPadding(Shader.PropertyToID(name));
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00004B18 File Offset: 0x00002D18
		public int aliveParticleCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_aliveParticleCount_Injected(intPtr);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004B3C File Offset: 0x00002D3C
		internal float time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VisualEffect.get_time_Injected(intPtr);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004B60 File Offset: 0x00002D60
		public void Simulate(float stepDeltaTime, uint stepCount = 1U)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VisualEffect>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualEffect.Simulate_Injected(intPtr, stepDeltaTime, stepCount);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004B84 File Offset: 0x00002D84
		[RequiredByNativeCode]
		private static VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(VisualEffect source)
		{
			bool flag = source.outputEventReceived == null;
			VFXEventAttribute vfxeventAttribute;
			if (flag)
			{
				vfxeventAttribute = null;
			}
			else
			{
				bool flag2 = source.m_cachedEventAttribute == null;
				if (flag2)
				{
					source.m_cachedEventAttribute = source.CreateVFXEventAttribute();
				}
				vfxeventAttribute = source.m_cachedEventAttribute;
			}
			return vfxeventAttribute;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004BC8 File Offset: 0x00002DC8
		[RequiredByNativeCode]
		private static void InvokeOutputEventReceived_Internal(VisualEffect source, int eventNameId)
		{
			VFXOutputEventArgs e = new VFXOutputEventArgs(eventNameId, source.m_cachedEventAttribute);
			source.outputEventReceived(e);
		}

		// Token: 0x0600019C RID: 412
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_pause_Injected(IntPtr _unity_self);

		// Token: 0x0600019D RID: 413
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pause_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600019E RID: 414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_playRate_Injected(IntPtr _unity_self);

		// Token: 0x0600019F RID: 415
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playRate_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001A0 RID: 416
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_startSeed_Injected(IntPtr _unity_self);

		// Token: 0x060001A1 RID: 417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startSeed_Injected(IntPtr _unity_self, uint value);

		// Token: 0x060001A2 RID: 418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_resetSeedOnPlay_Injected(IntPtr _unity_self);

		// Token: 0x060001A3 RID: 419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_resetSeedOnPlay_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_initialEventID_Injected(IntPtr _unity_self);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initialEventID_Injected(IntPtr _unity_self, int value);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_initialEventName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initialEventName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_culled_Injected(IntPtr _unity_self);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_visualEffectAsset_Injected(IntPtr _unity_self);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_visualEffectAsset_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendEventFromScript_Injected(IntPtr _unity_self, int eventNameID, IntPtr eventAttribute);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reinit_Injected(IntPtr _unity_self, bool sendInitialEventAndPrewarm);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AdvanceOneFrame_Injected(IntPtr _unity_self);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecreateData_Injected(IntPtr _unity_self);

		// Token: 0x060001AF RID: 431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGPUTaskMarkerName_Injected(IntPtr _unity_self, int nameID, int taskIndex, out ManagedSpanWrapper ret);

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCPUEffectMarkerName_Injected(IntPtr _unity_self, int markerIndex, out ManagedSpanWrapper ret);

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCPUSystemMarkerName_Injected(IntPtr _unity_self, int nameID, out ManagedSpanWrapper ret);

		// Token: 0x060001B2 RID: 434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterForProfiling_Injected(IntPtr _unity_self);

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnregisterForProfiling_Injected(IntPtr _unity_self);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRegisteredForProfiling_Injected(IntPtr _unity_self);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetOverride_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetTextureDimension_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasBool_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001B8 RID: 440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001B9 RID: 441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasUInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001BA RID: 442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFloat_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001BB RID: 443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVector2_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001BC RID: 444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVector3_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001BD RID: 445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVector4_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001BE RID: 446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMatrix4x4_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001BF RID: 447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasTexture_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001C0 RID: 448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAnimationCurve_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001C1 RID: 449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasGradient_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001C2 RID: 450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMesh_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001C3 RID: 451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasSkinnedMeshRenderer_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001C4 RID: 452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasGraphicsBuffer_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001C5 RID: 453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBool_Injected(IntPtr _unity_self, int nameID, bool b);

		// Token: 0x060001C6 RID: 454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInt_Injected(IntPtr _unity_self, int nameID, int i);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetUInt_Injected(IntPtr _unity_self, int nameID, uint i);

		// Token: 0x060001C8 RID: 456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloat_Injected(IntPtr _unity_self, int nameID, float f);

		// Token: 0x060001C9 RID: 457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector2_Injected(IntPtr _unity_self, int nameID, [In] ref Vector2 v);

		// Token: 0x060001CA RID: 458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector3_Injected(IntPtr _unity_self, int nameID, [In] ref Vector3 v);

		// Token: 0x060001CB RID: 459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector4_Injected(IntPtr _unity_self, int nameID, [In] ref Vector4 v);

		// Token: 0x060001CC RID: 460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix4x4_Injected(IntPtr _unity_self, int nameID, [In] ref Matrix4x4 v);

		// Token: 0x060001CD RID: 461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTexture_Injected(IntPtr _unity_self, int nameID, IntPtr t);

		// Token: 0x060001CE RID: 462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAnimationCurve_Injected(IntPtr _unity_self, int nameID, IntPtr c);

		// Token: 0x060001CF RID: 463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGradient_Injected(IntPtr _unity_self, int nameID, IntPtr g);

		// Token: 0x060001D0 RID: 464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMesh_Injected(IntPtr _unity_self, int nameID, IntPtr m);

		// Token: 0x060001D1 RID: 465
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSkinnedMeshRenderer_Injected(IntPtr _unity_self, int nameID, IntPtr m);

		// Token: 0x060001D2 RID: 466
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGraphicsBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr g);

		// Token: 0x060001D3 RID: 467
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBool_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001D4 RID: 468
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001D5 RID: 469
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetUInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001D6 RID: 470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloat_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001D7 RID: 471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector2_Injected(IntPtr _unity_self, int nameID, out Vector2 ret);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector3_Injected(IntPtr _unity_self, int nameID, out Vector3 ret);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector4_Injected(IntPtr _unity_self, int nameID, out Vector4 ret);

		// Token: 0x060001DA RID: 474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix4x4_Injected(IntPtr _unity_self, int nameID, out Matrix4x4 ret);

		// Token: 0x060001DB RID: 475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTexture_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001DC RID: 476
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMesh_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001DD RID: 477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSkinnedMeshRenderer_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001DE RID: 478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetGraphicsBuffer_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x060001DF RID: 479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetGradient_Injected(IntPtr _unity_self, int nameID, IntPtr gradient);

		// Token: 0x060001E0 RID: 480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAnimationCurve_Injected(IntPtr _unity_self, int nameID, IntPtr curve);

		// Token: 0x060001E1 RID: 481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParticleSystemInfo_Injected(IntPtr _unity_self, int nameID, out VFXParticleSystemInfo ret);

		// Token: 0x060001E2 RID: 482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSpawnSystemInfo_Injected(IntPtr _unity_self, int nameID, IntPtr spawnerState);

		// Token: 0x060001E3 RID: 483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAnySystemAwake_Injected(IntPtr _unity_self);

		// Token: 0x060001E4 RID: 484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetComputedBounds_Injected(IntPtr _unity_self, int nameID, out Bounds ret);

		// Token: 0x060001E5 RID: 485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentBoundsPadding_Injected(IntPtr _unity_self, int nameID, out Vector3 ret);

		// Token: 0x060001E6 RID: 486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_aliveParticleCount_Injected(IntPtr _unity_self);

		// Token: 0x060001E7 RID: 487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x060001E8 RID: 488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Simulate_Injected(IntPtr _unity_self, float stepDeltaTime, uint stepCount);

		// Token: 0x0400012F RID: 303
		private VFXEventAttribute m_cachedEventAttribute;

		// Token: 0x04000130 RID: 304
		public Action<VFXOutputEventArgs> outputEventReceived;

		// Token: 0x02000022 RID: 34
		internal enum VFXCPUEffectMarkers
		{
			// Token: 0x04000132 RID: 306
			FullUpdate,
			// Token: 0x04000133 RID: 307
			ProcessUpdate,
			// Token: 0x04000134 RID: 308
			EvaluateExpressions
		}
	}
}
