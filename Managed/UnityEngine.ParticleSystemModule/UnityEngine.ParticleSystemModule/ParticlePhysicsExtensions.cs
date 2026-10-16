using System;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	public static class ParticlePhysicsExtensions
	{
		// Token: 0x060005C0 RID: 1472 RVA: 0x00006958 File Offset: 0x00004B58
		[Obsolete("GetCollisionEvents function using ParticleCollisionEvent[] is deprecated. Use List<ParticleCollisionEvent> instead.", false)]
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new ArgumentNullException("go");
			}
			bool flag2 = collisionEvents == null;
			if (flag2)
			{
				throw new ArgumentNullException("collisionEvents");
			}
			return ParticleSystemExtensionsImpl.GetCollisionEventsDeprecated(ps, go, collisionEvents);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000699C File Offset: 0x00004B9C
		public static int GetSafeCollisionEventSize(this ParticleSystem ps)
		{
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000069B4 File Offset: 0x00004BB4
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, List<ParticleCollisionEvent> collisionEvents)
		{
			return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000069D0 File Offset: 0x00004BD0
		public static int GetSafeTriggerParticlesSize(this ParticleSystem ps, ParticleSystemTriggerEventType type)
		{
			return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize(ps, (int)type);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000069EC File Offset: 0x00004BEC
		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			return ParticleSystemExtensionsImpl.GetTriggerParticles(ps, (int)type, particles);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00006A08 File Offset: 0x00004C08
		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, out ParticleSystem.ColliderData colliderData)
		{
			bool flag = type == ParticleSystemTriggerEventType.Exit;
			if (flag)
			{
				throw new InvalidOperationException("Querying the collider data for the Exit event is not currently supported.");
			}
			bool flag2 = type == ParticleSystemTriggerEventType.Outside;
			if (flag2)
			{
				throw new InvalidOperationException("Querying the collider data for the Outside event is not supported, because when a particle is outside the collision volume, it is always outside every collider.");
			}
			colliderData = default(ParticleSystem.ColliderData);
			return ParticleSystemExtensionsImpl.GetTriggerParticlesWithData(ps, (int)type, particles, ref colliderData);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00006A50 File Offset: 0x00004C50
		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, int offset, int count)
		{
			bool flag = particles == null;
			if (flag)
			{
				throw new ArgumentNullException("particles");
			}
			bool flag2 = offset >= particles.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("offset", "offset should be smaller than the size of the particles list.");
			}
			bool flag3 = offset + count >= particles.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("count", "offset+count should be smaller than the size of the particles list.");
			}
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, offset, count);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, 0, particles.Count);
		}
	}
}
