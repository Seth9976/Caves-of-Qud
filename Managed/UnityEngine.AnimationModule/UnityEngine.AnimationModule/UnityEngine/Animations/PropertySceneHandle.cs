using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000064 RID: 100
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct PropertySceneHandle
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000ADDC File Offset: 0x00008FDC
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.HasValidTransform(ref stream);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000AE10 File Offset: 0x00009010
		private bool createdByNative
		{
			get
			{
				return this.valid > 0U;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000AE2C File Offset: 0x0000902C
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000AE4A File Offset: 0x0000904A
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			this.ResolveInternal(ref stream);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000AE60 File Offset: 0x00009060
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsBound(ref stream);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000AE88 File Offset: 0x00009088
		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = !this.createdByNative || !this.hasHandleIndex;
			if (flag)
			{
				throw new InvalidOperationException("The PropertySceneHandle is invalid. Please use proper function to create the handle.");
			}
			bool flag2 = !this.HasValidTransform(ref stream);
			if (flag2)
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000AEDC File Offset: 0x000090DC
		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetFloatInternal(ref stream);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetFloat(AnimationStream stream, float value)
		{
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000AF00 File Offset: 0x00009100
		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetIntInternal(ref stream);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetInt(AnimationStream stream, int value)
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000AF24 File Offset: 0x00009124
		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValid(ref stream);
			return this.GetBoolInternal(ref stream);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetBool(AnimationStream stream, bool value)
		{
		}

		// Token: 0x060005DC RID: 1500
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HasValidTransform(ref AnimationStream stream);

		// Token: 0x060005DD RID: 1501
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsBound(ref AnimationStream stream);

		// Token: 0x060005DE RID: 1502
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveInternal(ref AnimationStream stream);

		// Token: 0x060005DF RID: 1503
		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatInternal(ref AnimationStream stream);

		// Token: 0x060005E0 RID: 1504
		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntInternal(ref AnimationStream stream);

		// Token: 0x060005E1 RID: 1505
		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolInternal(ref AnimationStream stream);

		// Token: 0x04000185 RID: 389
		private uint valid;

		// Token: 0x04000186 RID: 390
		private int handleIndex;
	}
}
