using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005F RID: 95
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	public struct AnimationStream
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00009E84 File Offset: 0x00008084
		internal uint animatorBindingsVersion
		{
			get
			{
				return this.m_AnimatorBindingsVersion;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00009E9C File Offset: 0x0000809C
		public bool isValid
		{
			get
			{
				return this.m_AnimatorBindingsVersion >= 2U && this.constant != IntPtr.Zero && this.input != IntPtr.Zero && this.output != IntPtr.Zero && this.workspace != IntPtr.Zero && this.animationHandleBinder != IntPtr.Zero;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00009F14 File Offset: 0x00008114
		internal void CheckIsValid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationStream is invalid.");
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00009F3C File Offset: 0x0000813C
		public float deltaTime
		{
			get
			{
				this.CheckIsValid();
				return this.GetDeltaTime();
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00009F5C File Offset: 0x0000815C
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00009F7B File Offset: 0x0000817B
		public Vector3 velocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetVelocity(value);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00009F90 File Offset: 0x00008190
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00009FAF File Offset: 0x000081AF
		public Vector3 angularVelocity
		{
			get
			{
				this.CheckIsValid();
				return this.GetAngularVelocity();
			}
			set
			{
				this.CheckIsValid();
				this.SetAngularVelocity(value);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00009FC4 File Offset: 0x000081C4
		public Vector3 rootMotionPosition
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionPosition();
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00009FE4 File Offset: 0x000081E4
		public Quaternion rootMotionRotation
		{
			get
			{
				this.CheckIsValid();
				return this.GetRootMotionRotation();
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000A004 File Offset: 0x00008204
		public bool isHumanStream
		{
			get
			{
				this.CheckIsValid();
				return this.GetIsHumanStream();
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000A024 File Offset: 0x00008224
		public AnimationHumanStream AsHuman()
		{
			this.CheckIsValid();
			bool flag = !this.GetIsHumanStream();
			if (flag)
			{
				throw new InvalidOperationException("Cannot create an AnimationHumanStream for a generic rig.");
			}
			return this.GetHumanStream();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000A05C File Offset: 0x0000825C
		public int inputStreamCount
		{
			get
			{
				this.CheckIsValid();
				return this.GetInputStreamCount();
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000A07C File Offset: 0x0000827C
		public AnimationStream GetInputStream(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputStream(index);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000A09C File Offset: 0x0000829C
		public float GetInputWeight(int index)
		{
			this.CheckIsValid();
			return this.InternalGetInputWeight(index);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000A0BC File Offset: 0x000082BC
		public void CopyAnimationStreamMotion(AnimationStream animationStream)
		{
			this.CheckIsValid();
			animationStream.CheckIsValid();
			this.CopyAnimationStreamMotionInternal(animationStream);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000A0D6 File Offset: 0x000082D6
		private void ReadSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalReadSceneTransforms();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000A0E7 File Offset: 0x000082E7
		private void WriteSceneTransforms()
		{
			this.CheckIsValid();
			this.InternalWriteSceneTransforms();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000A0F8 File Offset: 0x000082F8
		[NativeMethod(Name = "AnimationStreamBindings::CopyAnimationStreamMotion", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void CopyAnimationStreamMotionInternal(AnimationStream animationStream)
		{
			AnimationStream.CopyAnimationStreamMotionInternal_Injected(ref this, ref animationStream);
		}

		// Token: 0x06000538 RID: 1336
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetDeltaTime();

		// Token: 0x06000539 RID: 1337
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetIsHumanStream();

		// Token: 0x0600053A RID: 1338 RVA: 0x0000A110 File Offset: 0x00008310
		[NativeMethod(Name = "AnimationStreamBindings::GetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetVelocity()
		{
			Vector3 vector;
			AnimationStream.GetVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000A128 File Offset: 0x00008328
		[NativeMethod(Name = "AnimationStreamBindings::SetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetVelocity(Vector3 velocity)
		{
			AnimationStream.SetVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000A140 File Offset: 0x00008340
		[NativeMethod(Name = "AnimationStreamBindings::GetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetAngularVelocity()
		{
			Vector3 vector;
			AnimationStream.GetAngularVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000A158 File Offset: 0x00008358
		[NativeMethod(Name = "AnimationStreamBindings::SetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetAngularVelocity(Vector3 velocity)
		{
			AnimationStream.SetAngularVelocity_Injected(ref this, ref velocity);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000A170 File Offset: 0x00008370
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRootMotionPosition()
		{
			Vector3 vector;
			AnimationStream.GetRootMotionPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000A188 File Offset: 0x00008388
		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRootMotionRotation()
		{
			Quaternion quaternion;
			AnimationStream.GetRootMotionRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06000540 RID: 1344
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetInputStreamCount();

		// Token: 0x06000541 RID: 1345 RVA: 0x0000A1A0 File Offset: 0x000083A0
		[NativeMethod(Name = "GetInputStream", IsThreadSafe = true)]
		private AnimationStream InternalGetInputStream(int index)
		{
			AnimationStream animationStream;
			AnimationStream.InternalGetInputStream_Injected(ref this, index, out animationStream);
			return animationStream;
		}

		// Token: 0x06000542 RID: 1346
		[NativeMethod(Name = "GetInputWeight", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetInputWeight(int index);

		// Token: 0x06000543 RID: 1347 RVA: 0x0000A1B8 File Offset: 0x000083B8
		[NativeMethod(IsThreadSafe = true)]
		private AnimationHumanStream GetHumanStream()
		{
			AnimationHumanStream animationHumanStream;
			AnimationStream.GetHumanStream_Injected(ref this, out animationHumanStream);
			return animationHumanStream;
		}

		// Token: 0x06000544 RID: 1348
		[NativeMethod(Name = "ReadSceneTransforms", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalReadSceneTransforms();

		// Token: 0x06000545 RID: 1349
		[NativeMethod(Name = "WriteSceneTransforms", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalWriteSceneTransforms();

		// Token: 0x06000546 RID: 1350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyAnimationStreamMotionInternal_Injected(ref AnimationStream _unity_self, [In] ref AnimationStream animationStream);

		// Token: 0x06000547 RID: 1351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x06000548 RID: 1352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVelocity_Injected(ref AnimationStream _unity_self, [In] ref Vector3 velocity);

		// Token: 0x06000549 RID: 1353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAngularVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x0600054A RID: 1354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAngularVelocity_Injected(ref AnimationStream _unity_self, [In] ref Vector3 velocity);

		// Token: 0x0600054B RID: 1355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionPosition_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		// Token: 0x0600054C RID: 1356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionRotation_Injected(ref AnimationStream _unity_self, out Quaternion ret);

		// Token: 0x0600054D RID: 1357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetInputStream_Injected(ref AnimationStream _unity_self, int index, out AnimationStream ret);

		// Token: 0x0600054E RID: 1358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHumanStream_Injected(ref AnimationStream _unity_self, out AnimationHumanStream ret);

		// Token: 0x0400016C RID: 364
		private uint m_AnimatorBindingsVersion;

		// Token: 0x0400016D RID: 365
		private IntPtr constant;

		// Token: 0x0400016E RID: 366
		private IntPtr input;

		// Token: 0x0400016F RID: 367
		private IntPtr output;

		// Token: 0x04000170 RID: 368
		private IntPtr workspace;

		// Token: 0x04000171 RID: 369
		private IntPtr inputStreamAccessor;

		// Token: 0x04000172 RID: 370
		private IntPtr animationHandleBinder;

		// Token: 0x04000173 RID: 371
		internal const int InvalidIndex = -1;
	}
}
