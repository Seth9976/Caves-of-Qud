using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200005E RID: 94
	internal class ParticleSystemExtensionsImpl
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x00007E5C File Offset: 0x0000605C
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeCollisionEventSize")]
		internal static int GetSafeCollisionEventSize([NotNull] ParticleSystem ps)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize_Injected(intPtr);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00007E94 File Offset: 0x00006094
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEventsDeprecated")]
		internal unsafe static int GetCollisionEventsDeprecated([NotNull] ParticleSystem ps, GameObject go, [Out] ParticleCollisionEvent[] collisionEvents)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			int collisionEventsDeprecated_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(ps, "ps");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<GameObject>(go);
				BlittableArrayWrapper blittableArrayWrapper;
				if (collisionEvents != null)
				{
					fixed (ParticleCollisionEvent[] array = collisionEvents)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				collisionEventsDeprecated_Injected = ParticleSystemExtensionsImpl.GetCollisionEventsDeprecated_Injected(intPtr, intPtr2, out blittableArrayWrapper);
			}
			finally
			{
				ParticleCollisionEvent[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<ParticleCollisionEvent>(ref array);
			}
			return collisionEventsDeprecated_Injected;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00007F10 File Offset: 0x00006110
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeTriggerParticlesSize")]
		internal static int GetSafeTriggerParticlesSize([NotNull] ParticleSystem ps, int type)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize_Injected(intPtr, type);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00007F48 File Offset: 0x00006148
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEvents")]
		internal unsafe static int GetCollisionEvents([NotNull] ParticleSystem ps, [NotNull] GameObject go, [NotNull] List<ParticleCollisionEvent> collisionEvents)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			if (collisionEvents == null)
			{
				ThrowHelper.ThrowArgumentNullException(collisionEvents, "collisionEvents");
			}
			int collisionEvents_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(ps, "ps");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(go, "go");
				}
				fixed (ParticleCollisionEvent[] array = NoAllocHelpers.ExtractArrayFromList<ParticleCollisionEvent>(collisionEvents))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, collisionEvents.Count);
					collisionEvents_Injected = ParticleSystemExtensionsImpl.GetCollisionEvents_Injected(intPtr, intPtr2, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleCollisionEvent>(collisionEvents);
			}
			return collisionEvents_Injected;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00008000 File Offset: 0x00006200
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticles")]
		internal unsafe static int GetTriggerParticles([NotNull] ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			if (particles == null)
			{
				ThrowHelper.ThrowArgumentNullException(particles, "particles");
			}
			int triggerParticles_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(ps, "ps");
				}
				fixed (ParticleSystem.Particle[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystem.Particle>(particles))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, particles.Count);
					triggerParticles_Injected = ParticleSystemExtensionsImpl.GetTriggerParticles_Injected(intPtr, type, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystem.Particle>(particles);
			}
			return triggerParticles_Injected;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00008098 File Offset: 0x00006298
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticlesWithData")]
		internal unsafe static int GetTriggerParticlesWithData([NotNull] ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles, ref ParticleSystem.ColliderData colliderData)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			if (particles == null)
			{
				ThrowHelper.ThrowArgumentNullException(particles, "particles");
			}
			int triggerParticlesWithData_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(ps, "ps");
				}
				fixed (ParticleSystem.Particle[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystem.Particle>(particles))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, particles.Count);
					triggerParticlesWithData_Injected = ParticleSystemExtensionsImpl.GetTriggerParticlesWithData_Injected(intPtr, type, ref blittableListWrapper, ref colliderData);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystem.Particle>(particles);
			}
			return triggerParticlesWithData_Injected;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00008130 File Offset: 0x00006330
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTriggerParticles")]
		internal unsafe static void SetTriggerParticles([NotNull] ParticleSystem ps, int type, [NotNull] List<ParticleSystem.Particle> particles, int offset, int count)
		{
			if (ps == null)
			{
				ThrowHelper.ThrowArgumentNullException(ps, "ps");
			}
			if (particles == null)
			{
				ThrowHelper.ThrowArgumentNullException(particles, "particles");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(ps);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(ps, "ps");
				}
				fixed (ParticleSystem.Particle[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystem.Particle>(particles))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, particles.Count);
					ParticleSystemExtensionsImpl.SetTriggerParticles_Injected(intPtr, type, ref blittableListWrapper, offset, count);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystem.Particle>(particles);
			}
		}

		// Token: 0x06000661 RID: 1633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSafeCollisionEventSize_Injected(IntPtr ps);

		// Token: 0x06000662 RID: 1634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCollisionEventsDeprecated_Injected(IntPtr ps, IntPtr go, out BlittableArrayWrapper collisionEvents);

		// Token: 0x06000663 RID: 1635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSafeTriggerParticlesSize_Injected(IntPtr ps, int type);

		// Token: 0x06000664 RID: 1636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCollisionEvents_Injected(IntPtr ps, IntPtr go, ref BlittableListWrapper collisionEvents);

		// Token: 0x06000665 RID: 1637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTriggerParticles_Injected(IntPtr ps, int type, ref BlittableListWrapper particles);

		// Token: 0x06000666 RID: 1638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTriggerParticlesWithData_Injected(IntPtr ps, int type, ref BlittableListWrapper particles, ref ParticleSystem.ColliderData colliderData);

		// Token: 0x06000667 RID: 1639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerParticles_Injected(IntPtr ps, int type, ref BlittableListWrapper particles, int offset, int count);
	}
}
