using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003B RID: 59
	public class ParticleControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x000094FC File Offset: 0x000076FC
		public static ScriptPlayable<ParticleControlPlayable> Create(PlayableGraph graph, ParticleSystem component, uint randomSeed)
		{
			if (component == null)
			{
				return ScriptPlayable<ParticleControlPlayable>.Null;
			}
			ScriptPlayable<ParticleControlPlayable> scriptPlayable = ScriptPlayable<ParticleControlPlayable>.Create(graph, 0);
			scriptPlayable.GetBehaviour().Initialize(component, randomSeed);
			return scriptPlayable;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000952F File Offset: 0x0000772F
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x00009537 File Offset: 0x00007737
		public ParticleSystem particleSystem { get; private set; }

		// Token: 0x060002A6 RID: 678 RVA: 0x00009540 File Offset: 0x00007740
		public void Initialize(ParticleSystem ps, uint randomSeed)
		{
			this.m_RandomSeed = Math.Max(1U, randomSeed);
			this.particleSystem = ps;
			ParticleControlPlayable.SetRandomSeed(this.particleSystem, this.m_RandomSeed);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009568 File Offset: 0x00007768
		private static void SetRandomSeed(ParticleSystem particleSystem, uint randomSeed)
		{
			if (particleSystem == null)
			{
				return;
			}
			particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			if (particleSystem.useAutoRandomSeed)
			{
				particleSystem.useAutoRandomSeed = false;
				particleSystem.randomSeed = randomSeed;
			}
			for (int i = 0; i < particleSystem.subEmitters.subEmittersCount; i++)
			{
				ParticleControlPlayable.SetRandomSeed(particleSystem.subEmitters.GetSubEmitterSystem(i), randomSeed += 1U);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000095D0 File Offset: 0x000077D0
		public override void PrepareFrame(Playable playable, FrameData data)
		{
			if (this.particleSystem == null || !this.particleSystem.gameObject.activeInHierarchy)
			{
				this.m_LastPlayableTime = float.MaxValue;
				return;
			}
			float num = (float)playable.GetTime<Playable>();
			float time = this.particleSystem.time;
			if (this.m_LastPlayableTime > num || !Mathf.Approximately(time, this.m_LastParticleTime))
			{
				this.Simulate(num, true);
			}
			else if (this.m_LastPlayableTime < num)
			{
				this.Simulate(num - this.m_LastPlayableTime, false);
			}
			this.m_LastPlayableTime = num;
			this.m_LastParticleTime = this.particleSystem.time;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000966E File Offset: 0x0000786E
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			this.m_LastPlayableTime = float.MaxValue;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000967B File Offset: 0x0000787B
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			this.m_LastPlayableTime = float.MaxValue;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009688 File Offset: 0x00007888
		private void Simulate(float time, bool restart)
		{
			float maximumDeltaTime = Time.maximumDeltaTime;
			if (restart)
			{
				this.particleSystem.Simulate(0f, false, true, false);
			}
			while (time > maximumDeltaTime)
			{
				this.particleSystem.Simulate(maximumDeltaTime, false, false, false);
				time -= maximumDeltaTime;
			}
			if (time > 0f)
			{
				this.particleSystem.Simulate(time, false, false, false);
			}
		}

		// Token: 0x040000E3 RID: 227
		private const float kUnsetTime = 3.4028235E+38f;

		// Token: 0x040000E4 RID: 228
		private float m_LastPlayableTime = float.MaxValue;

		// Token: 0x040000E5 RID: 229
		private float m_LastParticleTime = float.MaxValue;

		// Token: 0x040000E6 RID: 230
		private uint m_RandomSeed = 1U;
	}
}
