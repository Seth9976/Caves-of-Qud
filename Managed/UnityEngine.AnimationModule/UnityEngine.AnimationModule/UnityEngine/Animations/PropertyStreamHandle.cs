using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000062 RID: 98
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	public struct PropertyStreamHandle
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0000A740 File Offset: 0x00008940
		public bool IsValid(AnimationStream stream)
		{
			return this.IsValidInternal(ref stream);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000A75C File Offset: 0x0000895C
		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && this.createdByNative && this.hasHandleIndex && this.hasBindType;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000A790 File Offset: 0x00008990
		private bool createdByNative
		{
			get
			{
				return this.animatorBindingsVersion > 0U;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000A7AC File Offset: 0x000089AC
		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return this.animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000A7CC File Offset: 0x000089CC
		private bool hasHandleIndex
		{
			get
			{
				return this.handleIndex != -1;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000A7EC File Offset: 0x000089EC
		private bool hasValueArrayIndex
		{
			get
			{
				return this.valueArrayIndex != -1;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000A80C File Offset: 0x00008A0C
		private bool hasBindType
		{
			get
			{
				return this.bindType != 0;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000A834 File Offset: 0x00008A34
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000A827 File Offset: 0x00008A27
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
			private set
			{
				this.m_AnimatorBindingsVersion = value;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000A84C File Offset: 0x00008A4C
		public void Resolve(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000A858 File Offset: 0x00008A58
		public bool IsResolved(AnimationStream stream)
		{
			return this.IsResolvedInternal(ref stream);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000A874 File Offset: 0x00008A74
		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return this.IsValidInternal(ref stream) && this.IsSameVersionAsStream(ref stream) && this.hasValueArrayIndex;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			bool flag = this.IsResolvedInternal(ref stream);
			if (!flag)
			{
				bool flag2 = !this.createdByNative || !this.hasHandleIndex || !this.hasBindType;
				if (flag2)
				{
					throw new InvalidOperationException("The PropertyStreamHandle is invalid. Please use proper function to create the handle.");
				}
				bool flag3 = !this.IsSameVersionAsStream(ref stream) || (this.hasHandleIndex && !this.hasValueArrayIndex);
				if (flag3)
				{
					this.ResolveInternal(ref stream);
				}
				bool flag4 = this.hasHandleIndex && !this.hasValueArrayIndex;
				if (flag4)
				{
					throw new InvalidOperationException("The PropertyStreamHandle cannot be resolved.");
				}
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000A944 File Offset: 0x00008B44
		public float GetFloat(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 5;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetFloatInternal(ref stream);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000A984 File Offset: 0x00008B84
		public void SetFloat(AnimationStream stream, float value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 5;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetFloatInternal(ref stream, value);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public int GetInt(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 10 && this.bindType != 11 && this.bindType != 9;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetIntInternal(ref stream);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000AA18 File Offset: 0x00008C18
		public void SetInt(AnimationStream stream, int value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 10 && this.bindType != 11 && this.bindType != 9;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetIntInternal(ref stream, value);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000AA6C File Offset: 0x00008C6C
		public bool GetBool(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 6 && this.bindType != 7;
			if (flag)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return this.GetBoolInternal(ref stream);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		public void SetBool(AnimationStream stream, bool value)
		{
			this.CheckIsValidAndResolve(ref stream);
			bool flag = this.bindType != 6 && this.bindType != 7;
			if (flag)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			this.SetBoolInternal(ref stream, value);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000AB00 File Offset: 0x00008D00
		public bool GetReadMask(AnimationStream stream)
		{
			this.CheckIsValidAndResolve(ref stream);
			return this.GetReadMaskInternal(ref stream);
		}

		// Token: 0x060005A4 RID: 1444
		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ResolveInternal(ref AnimationStream stream);

		// Token: 0x060005A5 RID: 1445
		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFloatInternal(ref AnimationStream stream);

		// Token: 0x060005A6 RID: 1446
		[NativeMethod(Name = "SetFloat", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFloatInternal(ref AnimationStream stream, float value);

		// Token: 0x060005A7 RID: 1447
		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetIntInternal(ref AnimationStream stream);

		// Token: 0x060005A8 RID: 1448
		[NativeMethod(Name = "SetInt", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIntInternal(ref AnimationStream stream, int value);

		// Token: 0x060005A9 RID: 1449
		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetBoolInternal(ref AnimationStream stream);

		// Token: 0x060005AA RID: 1450
		[NativeMethod(Name = "SetBool", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoolInternal(ref AnimationStream stream, bool value);

		// Token: 0x060005AB RID: 1451
		[NativeMethod(Name = "GetReadMask", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetReadMaskInternal(ref AnimationStream stream);

		// Token: 0x0400017F RID: 383
		private uint m_AnimatorBindingsVersion;

		// Token: 0x04000180 RID: 384
		private int handleIndex;

		// Token: 0x04000181 RID: 385
		private int valueArrayIndex;

		// Token: 0x04000182 RID: 386
		private int bindType;
	}
}
