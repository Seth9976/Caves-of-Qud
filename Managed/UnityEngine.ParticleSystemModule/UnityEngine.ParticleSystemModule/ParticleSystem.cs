using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemModulesScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[RequireComponent(typeof(Transform))]
	[UsedByNativeCode]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemGeometryJob.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	public sealed class ParticleSystem : Component
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.startSize = size;
			particle.rotation3D = Vector3.zero;
			particle.angularVelocity3D = Vector3.zero;
			particle.startColor = color;
			particle.randomSeed = 5U;
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020CB File Offset: 0x000002CB
		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(ParticleSystem.Particle particle)
		{
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		[Obsolete("startDelay property is deprecated. Use main.startDelay or main.startDelayMultiplier instead.", false)]
		public float startDelay
		{
			get
			{
				return this.main.startDelayMultiplier;
			}
			set
			{
				this.main.startDelayMultiplier = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002118 File Offset: 0x00000318
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002138 File Offset: 0x00000338
		[Obsolete("loop property is deprecated. Use main.loop instead.", false)]
		public bool loop
		{
			get
			{
				return this.main.loop;
			}
			set
			{
				this.main.loop = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		[Obsolete("playOnAwake property is deprecated. Use main.playOnAwake instead.", false)]
		public bool playOnAwake
		{
			get
			{
				return this.main.playOnAwake;
			}
			set
			{
				this.main.playOnAwake = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002198 File Offset: 0x00000398
		[Obsolete("duration property is deprecated. Use main.duration instead.", false)]
		public float duration
		{
			get
			{
				return this.main.duration;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021D8 File Offset: 0x000003D8
		[Obsolete("playbackSpeed property is deprecated. Use main.simulationSpeed instead.", false)]
		public float playbackSpeed
		{
			get
			{
				return this.main.simulationSpeed;
			}
			set
			{
				this.main.simulationSpeed = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002218 File Offset: 0x00000418
		[Obsolete("enableEmission property is deprecated. Use emission.enabled instead.", false)]
		public bool enableEmission
		{
			get
			{
				return this.emission.enabled;
			}
			set
			{
				this.emission.enabled = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002238 File Offset: 0x00000438
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002258 File Offset: 0x00000458
		[Obsolete("emissionRate property is deprecated. Use emission.rateOverTime, emission.rateOverDistance, emission.rateOverTimeMultiplier or emission.rateOverDistanceMultiplier instead.", false)]
		public float emissionRate
		{
			get
			{
				return this.emission.rateOverTimeMultiplier;
			}
			set
			{
				this.emission.rateOverTime = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000229C File Offset: 0x0000049C
		[Obsolete("startSpeed property is deprecated. Use main.startSpeed or main.startSpeedMultiplier instead.", false)]
		public float startSpeed
		{
			get
			{
				return this.main.startSpeedMultiplier;
			}
			set
			{
				this.main.startSpeedMultiplier = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022DC File Offset: 0x000004DC
		[Obsolete("startSize property is deprecated. Use main.startSize or main.startSizeMultiplier instead.", false)]
		public float startSize
		{
			get
			{
				return this.main.startSizeMultiplier;
			}
			set
			{
				this.main.startSizeMultiplier = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002324 File Offset: 0x00000524
		[Obsolete("startColor property is deprecated. Use main.startColor instead.", false)]
		public Color startColor
		{
			get
			{
				return this.main.startColor.color;
			}
			set
			{
				this.main.startColor = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002348 File Offset: 0x00000548
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002368 File Offset: 0x00000568
		[Obsolete("startRotation property is deprecated. Use main.startRotation or main.startRotationMultiplier instead.", false)]
		public float startRotation
		{
			get
			{
				return this.main.startRotationMultiplier;
			}
			set
			{
				this.main.startRotationMultiplier = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002388 File Offset: 0x00000588
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023CC File Offset: 0x000005CC
		[Obsolete("startRotation3D property is deprecated. Use main.startRotationX, main.startRotationY and main.startRotationZ instead. (Or main.startRotationXMultiplier, main.startRotationYMultiplier and main.startRotationZMultiplier).", false)]
		public Vector3 startRotation3D
		{
			get
			{
				return new Vector3(this.main.startRotationXMultiplier, this.main.startRotationYMultiplier, this.main.startRotationZMultiplier);
			}
			set
			{
				ParticleSystem.MainModule main = this.main;
				main.startRotationXMultiplier = value.x;
				main.startRotationYMultiplier = value.y;
				main.startRotationZMultiplier = value.z;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000242C File Offset: 0x0000062C
		[Obsolete("startLifetime property is deprecated. Use main.startLifetime or main.startLifetimeMultiplier instead.", false)]
		public float startLifetime
		{
			get
			{
				return this.main.startLifetimeMultiplier;
			}
			set
			{
				this.main.startLifetimeMultiplier = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000244C File Offset: 0x0000064C
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000246C File Offset: 0x0000066C
		[Obsolete("gravityModifier property is deprecated. Use main.gravityModifier or main.gravityModifierMultiplier instead.", false)]
		public float gravityModifier
		{
			get
			{
				return this.main.gravityModifierMultiplier;
			}
			set
			{
				this.main.gravityModifierMultiplier = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000248C File Offset: 0x0000068C
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000024AC File Offset: 0x000006AC
		[Obsolete("maxParticles property is deprecated. Use main.maxParticles instead.", false)]
		public int maxParticles
		{
			get
			{
				return this.main.maxParticles;
			}
			set
			{
				this.main.maxParticles = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024EC File Offset: 0x000006EC
		[Obsolete("simulationSpace property is deprecated. Use main.simulationSpace instead.", false)]
		public ParticleSystemSimulationSpace simulationSpace
		{
			get
			{
				return this.main.simulationSpace;
			}
			set
			{
				this.main.simulationSpace = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000250C File Offset: 0x0000070C
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000252C File Offset: 0x0000072C
		[Obsolete("scalingMode property is deprecated. Use main.scalingMode instead.", false)]
		public ParticleSystemScalingMode scalingMode
		{
			get
			{
				return this.main.scalingMode;
			}
			set
			{
				this.main.scalingMode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000254C File Offset: 0x0000074C
		[Obsolete("automaticCullingEnabled property is deprecated. Use proceduralSimulationSupported instead (UnityUpgradable) -> proceduralSimulationSupported", true)]
		public bool automaticCullingEnabled
		{
			get
			{
				return this.proceduralSimulationSupported;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002564 File Offset: 0x00000764
		public bool isPlaying
		{
			[NativeName("SyncJobs(false)->IsPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002588 File Offset: 0x00000788
		public bool isEmitting
		{
			[NativeName("SyncJobs(false)->IsEmitting")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_isEmitting_Injected(intPtr);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025AC File Offset: 0x000007AC
		public bool isStopped
		{
			[NativeName("SyncJobs(false)->IsStopped")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_isStopped_Injected(intPtr);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025D0 File Offset: 0x000007D0
		public bool isPaused
		{
			[NativeName("SyncJobs(false)->IsPaused")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_isPaused_Injected(intPtr);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025F4 File Offset: 0x000007F4
		public int particleCount
		{
			[NativeName("SyncJobs(false)->GetParticleCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_particleCount_Injected(intPtr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002618 File Offset: 0x00000818
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000263C File Offset: 0x0000083C
		public float time
		{
			[NativeName("SyncJobs(false)->GetSecPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_time_Injected(intPtr);
			}
			[NativeName("SyncJobs(false)->SetSecPosition")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002660 File Offset: 0x00000860
		public float totalTime
		{
			[NativeName("SyncJobs(false)->GetTotalSecPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_totalTime_Injected(intPtr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002684 File Offset: 0x00000884
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000026A8 File Offset: 0x000008A8
		public uint randomSeed
		{
			[NativeName("GetRandomSeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_randomSeed_Injected(intPtr);
			}
			[NativeName("SyncJobs(false)->SetRandomSeed")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.set_randomSeed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026CC File Offset: 0x000008CC
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026F0 File Offset: 0x000008F0
		public bool useAutoRandomSeed
		{
			[NativeName("GetAutoRandomSeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_useAutoRandomSeed_Injected(intPtr);
			}
			[NativeName("SyncJobs(false)->SetAutoRandomSeed")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.set_useAutoRandomSeed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002714 File Offset: 0x00000914
		public bool proceduralSimulationSupported
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_proceduralSimulationSupported_Injected(intPtr);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002738 File Offset: 0x00000938
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize", HasExplicitThis = true)]
		internal float GetParticleCurrentSize(ref ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystem.GetParticleCurrentSize_Injected(intPtr, ref particle);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000275C File Offset: 0x0000095C
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize3D", HasExplicitThis = true)]
		internal Vector3 GetParticleCurrentSize3D(ref ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			ParticleSystem.GetParticleCurrentSize3D_Injected(intPtr, ref particle, out vector);
			return vector;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002784 File Offset: 0x00000984
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentColor", HasExplicitThis = true)]
		internal Color32 GetParticleCurrentColor(ref ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color32 color;
			ParticleSystem.GetParticleCurrentColor_Injected(intPtr, ref particle, out color);
			return color;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027AC File Offset: 0x000009AC
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleMeshIndex", HasExplicitThis = true)]
		internal int GetParticleMeshIndex(ref ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystem.GetParticleMeshIndex_Injected(intPtr, ref particle);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027D0 File Offset: 0x000009D0
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticles", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetParticles([Out] ParticleSystem.Particle[] particles, int size, int offset)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (particles != null)
				{
					fixed (ParticleSystem.Particle[] array = particles)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				ParticleSystem.SetParticles_Injected(intPtr, out blittableArrayWrapper, size, offset);
			}
			finally
			{
				ParticleSystem.Particle[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<ParticleSystem.Particle>(ref array);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002830 File Offset: 0x00000A30
		public void SetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000283D File Offset: 0x00000A3D
		public void SetParticles([Out] ParticleSystem.Particle[] particles)
		{
			this.SetParticles(particles, -1);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000284C File Offset: 0x00000A4C
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private void SetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.SetParticlesWithNativeArray_Injected(intPtr, particles, particlesLength, size, offset);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002873 File Offset: 0x00000A73
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			this.SetParticlesWithNativeArray((IntPtr)particles.GetUnsafeReadOnlyPtr<ParticleSystem.Particle>(), particles.Length, size, offset);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002893 File Offset: 0x00000A93
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			this.SetParticles(particles, size, 0);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028A0 File Offset: 0x00000AA0
		public void SetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			this.SetParticles(particles, -1);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028AC File Offset: 0x00000AAC
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticles", HasExplicitThis = true, ThrowsException = true)]
		public unsafe int GetParticles([NotNull] [Out] ParticleSystem.Particle[] particles, int size, int offset)
		{
			if (particles == null)
			{
				ThrowHelper.ThrowArgumentNullException(particles, "particles");
			}
			int particles_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (ParticleSystem.Particle[] array = particles)
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					particles_Injected = ParticleSystem.GetParticles_Injected(intPtr, out blittableArrayWrapper, size, offset);
				}
			}
			finally
			{
				ParticleSystem.Particle[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<ParticleSystem.Particle>(ref array);
			}
			return particles_Injected;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000291C File Offset: 0x00000B1C
		public int GetParticles([Out] ParticleSystem.Particle[] particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002938 File Offset: 0x00000B38
		public int GetParticles([Out] ParticleSystem.Particle[] particles)
		{
			return this.GetParticles(particles, -1);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002954 File Offset: 0x00000B54
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private int GetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystem.GetParticlesWithNativeArray_Injected(intPtr, particles, particlesLength, size, offset);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000297C File Offset: 0x00000B7C
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size, int offset)
		{
			return this.GetParticlesWithNativeArray((IntPtr)particles.GetUnsafePtr<ParticleSystem.Particle>(), particles.Length, size, offset);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000029AC File Offset: 0x00000BAC
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles, int size)
		{
			return this.GetParticles(particles, size, 0);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000029C8 File Offset: 0x00000BC8
		public int GetParticles([Out] NativeArray<ParticleSystem.Particle> particles)
		{
			return this.GetParticles(particles, -1);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029E4 File Offset: 0x00000BE4
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void SetCustomParticleData([NotNull] List<Vector4> customData, ParticleSystemCustomData streamIndex)
		{
			if (customData == null)
			{
				ThrowHelper.ThrowArgumentNullException(customData, "customData");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector4[] array = NoAllocHelpers.ExtractArrayFromList<Vector4>(customData))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, customData.Count);
					ParticleSystem.SetCustomParticleData_Injected(intPtr, ref blittableListWrapper, streamIndex);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector4>(customData);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A64 File Offset: 0x00000C64
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		public unsafe int GetCustomParticleData([NotNull] List<Vector4> customData, ParticleSystemCustomData streamIndex)
		{
			if (customData == null)
			{
				ThrowHelper.ThrowArgumentNullException(customData, "customData");
			}
			int customParticleData_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector4[] array = NoAllocHelpers.ExtractArrayFromList<Vector4>(customData))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, customData.Count);
					customParticleData_Injected = ParticleSystem.GetCustomParticleData_Injected(intPtr, ref blittableListWrapper, streamIndex);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector4>(customData);
			}
			return customParticleData_Injected;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public ParticleSystem.PlaybackState GetPlaybackState()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.PlaybackState playbackState;
			ParticleSystem.GetPlaybackState_Injected(intPtr, out playbackState);
			return playbackState;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B10 File Offset: 0x00000D10
		public void SetPlaybackState(ParticleSystem.PlaybackState playbackState)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.SetPlaybackState_Injected(intPtr, ref playbackState);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B34 File Offset: 0x00000D34
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTrailData", HasExplicitThis = true)]
		private void GetTrailDataInternal(ref ParticleSystem.Trails trailData)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.GetTrailDataInternal_Injected(intPtr, ref trailData);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B58 File Offset: 0x00000D58
		public ParticleSystem.Trails GetTrails()
		{
			ParticleSystem.Trails trails = default(ParticleSystem.Trails);
			trails.Allocate();
			this.GetTrailDataInternal(ref trails);
			return trails;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B84 File Offset: 0x00000D84
		public int GetTrails(ref ParticleSystem.Trails trailData)
		{
			trailData.Allocate();
			this.GetTrailDataInternal(ref trailData);
			return trailData.positions.Count;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BB0 File Offset: 0x00000DB0
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTrailData", HasExplicitThis = true)]
		public void SetTrails(ParticleSystem.Trails trailData)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.SetTrails_Injected(intPtr, ref trailData);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BD4 File Offset: 0x00000DD4
		[FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart, [DefaultValue("true")] bool fixedTimeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Simulate_Injected(intPtr, t, withChildren, restart, fixedTimeStep);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BFB File Offset: 0x00000DFB
		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart)
		{
			this.Simulate(t, withChildren, restart, true);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C09 File Offset: 0x00000E09
		public void Simulate(float t, [DefaultValue("true")] bool withChildren)
		{
			this.Simulate(t, withChildren, true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C16 File Offset: 0x00000E16
		public void Simulate(float t)
		{
			this.Simulate(t, true);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C24 File Offset: 0x00000E24
		[FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
		public void Play([DefaultValue("true")] bool withChildren)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Play_Injected(intPtr, withChildren);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C47 File Offset: 0x00000E47
		public void Play()
		{
			this.Play(true);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C54 File Offset: 0x00000E54
		[FreeFunction(Name = "ParticleSystemScriptBindings::Pause", HasExplicitThis = true)]
		public void Pause([DefaultValue("true")] bool withChildren)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Pause_Injected(intPtr, withChildren);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002C77 File Offset: 0x00000E77
		public void Pause()
		{
			this.Pause(true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C84 File Offset: 0x00000E84
		[FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
		public void Stop([DefaultValue("true")] bool withChildren, [DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Stop_Injected(intPtr, withChildren, stopBehavior);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CA8 File Offset: 0x00000EA8
		public void Stop([DefaultValue("true")] bool withChildren)
		{
			this.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CC0 File Offset: 0x00000EC0
		[FreeFunction(Name = "ParticleSystemScriptBindings::Clear", HasExplicitThis = true)]
		public void Clear([DefaultValue("true")] bool withChildren)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Clear_Injected(intPtr, withChildren);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002CE3 File Offset: 0x00000EE3
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002CF0 File Offset: 0x00000EF0
		[FreeFunction(Name = "ParticleSystemScriptBindings::IsAlive", HasExplicitThis = true)]
		public bool IsAlive([DefaultValue("true")] bool withChildren)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystem.IsAlive_Injected(intPtr, withChildren);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D14 File Offset: 0x00000F14
		public bool IsAlive()
		{
			return this.IsAlive(true);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D2D File Offset: 0x00000F2D
		[RequiredByNativeCode]
		public void Emit(int count)
		{
			this.Emit_Internal(count);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D38 File Offset: 0x00000F38
		[NativeName("SyncJobs()->Emit")]
		private void Emit_Internal(int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Emit_Internal_Injected(intPtr, count);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D5C File Offset: 0x00000F5C
		[NativeName("SyncJobs()->EmitParticlesExternal")]
		public void Emit(ParticleSystem.EmitParams emitParams, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Emit_Injected(intPtr, ref emitParams, count);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002D84 File Offset: 0x00000F84
		[NativeName("SyncJobs()->EmitParticleExternal")]
		private void EmitOld_Internal(ref ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.EmitOld_Internal_Injected(intPtr, ref particle);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002DA7 File Offset: 0x00000FA7
		public void TriggerSubEmitter(int subEmitterIndex)
		{
			this.TriggerSubEmitterForAllParticles(subEmitterIndex);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public void TriggerSubEmitter(int subEmitterIndex, ref ParticleSystem.Particle particle)
		{
			this.TriggerSubEmitterForParticle(subEmitterIndex, particle);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public void TriggerSubEmitter(int subEmitterIndex, List<ParticleSystem.Particle> particles)
		{
			bool flag = particles == null;
			if (flag)
			{
				this.TriggerSubEmitterForAllParticles(subEmitterIndex);
			}
			else
			{
				this.TriggerSubEmitterForParticles(subEmitterIndex, particles);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DF0 File Offset: 0x00000FF0
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticle", HasExplicitThis = true)]
		internal void TriggerSubEmitterForParticle(int subEmitterIndex, ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.TriggerSubEmitterForParticle_Injected(intPtr, subEmitterIndex, ref particle);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002E18 File Offset: 0x00001018
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticles", HasExplicitThis = true)]
		private unsafe void TriggerSubEmitterForParticles(int subEmitterIndex, List<ParticleSystem.Particle> particles)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (particles != null)
				{
					fixed (ParticleSystem.Particle[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystem.Particle>(particles))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, particles.Count);
					}
				}
				ParticleSystem.TriggerSubEmitterForParticles_Injected(intPtr, subEmitterIndex, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystem.Particle>(particles);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E8C File Offset: 0x0000108C
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForAllParticles", HasExplicitThis = true)]
		private void TriggerSubEmitterForAllParticles(int subEmitterIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.TriggerSubEmitterForAllParticles_Injected(intPtr, subEmitterIndex);
		}

		// Token: 0x06000065 RID: 101
		[FreeFunction(Name = "ParticleSystemGeometryJob::ResetPreMappedBufferMemory")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetPreMappedBufferMemory();

		// Token: 0x06000066 RID: 102
		[FreeFunction(Name = "ParticleSystemGeometryJob::SetMaximumPreMappedBufferCounts")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMaximumPreMappedBufferCounts(int vertexBuffersCount, int indexBuffersCount);

		// Token: 0x06000067 RID: 103 RVA: 0x00002EB0 File Offset: 0x000010B0
		[NativeName("SetUsesAxisOfRotation")]
		public void AllocateAxisOfRotationAttribute()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.AllocateAxisOfRotationAttribute_Injected(intPtr);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002ED4 File Offset: 0x000010D4
		[NativeName("SetUsesMeshIndex")]
		public void AllocateMeshIndexAttribute()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.AllocateMeshIndexAttribute_Injected(intPtr);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EF8 File Offset: 0x000010F8
		[NativeName("SetUsesCustomData")]
		public void AllocateCustomDataAttribute(ParticleSystemCustomData stream)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.AllocateCustomDataAttribute_Injected(intPtr, stream);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002F1C File Offset: 0x0000111C
		public bool has3DParticleRotations
		{
			[NativeName("Has3DParticleRotations")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_has3DParticleRotations_Injected(intPtr);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F40 File Offset: 0x00001140
		public bool hasNonUniformParticleSizes
		{
			[NativeName("HasNonUniformParticleSizes")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_hasNonUniformParticleSizes_Injected(intPtr);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002F64 File Offset: 0x00001164
		internal unsafe void* GetManagedJobData()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystem.GetManagedJobData_Injected(intPtr);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002F88 File Offset: 0x00001188
		internal JobHandle GetManagedJobHandle()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			JobHandle jobHandle;
			ParticleSystem.GetManagedJobHandle_Injected(intPtr, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002FB0 File Offset: 0x000011B0
		internal void SetManagedJobHandle(JobHandle handle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.SetManagedJobHandle_Injected(intPtr, ref handle);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FD4 File Offset: 0x000011D4
		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		internal unsafe static JobHandle ScheduleManagedJob(ref JobsUtility.JobScheduleParameters parameters, void* additionalData)
		{
			JobHandle jobHandle;
			ParticleSystem.ScheduleManagedJob_Injected(ref parameters, additionalData, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000070 RID: 112
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CopyManagedJobData(void* systemPtr, out NativeParticleData particleData);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool UserJobCanBeScheduled();

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002FEC File Offset: 0x000011EC
		public ParticleSystem.MainModule main
		{
			get
			{
				return new ParticleSystem.MainModule(this);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003004 File Offset: 0x00001204
		public ParticleSystem.EmissionModule emission
		{
			get
			{
				return new ParticleSystem.EmissionModule(this);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000301C File Offset: 0x0000121C
		public ParticleSystem.ShapeModule shape
		{
			get
			{
				return new ParticleSystem.ShapeModule(this);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003034 File Offset: 0x00001234
		public ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime
		{
			get
			{
				return new ParticleSystem.VelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000304C File Offset: 0x0000124C
		public ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime
		{
			get
			{
				return new ParticleSystem.LimitVelocityOverLifetimeModule(this);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003064 File Offset: 0x00001264
		public ParticleSystem.InheritVelocityModule inheritVelocity
		{
			get
			{
				return new ParticleSystem.InheritVelocityModule(this);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000307C File Offset: 0x0000127C
		public ParticleSystem.LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed
		{
			get
			{
				return new ParticleSystem.LifetimeByEmitterSpeedModule(this);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003094 File Offset: 0x00001294
		public ParticleSystem.ForceOverLifetimeModule forceOverLifetime
		{
			get
			{
				return new ParticleSystem.ForceOverLifetimeModule(this);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000030AC File Offset: 0x000012AC
		public ParticleSystem.ColorOverLifetimeModule colorOverLifetime
		{
			get
			{
				return new ParticleSystem.ColorOverLifetimeModule(this);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000030C4 File Offset: 0x000012C4
		public ParticleSystem.ColorBySpeedModule colorBySpeed
		{
			get
			{
				return new ParticleSystem.ColorBySpeedModule(this);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000030DC File Offset: 0x000012DC
		public ParticleSystem.SizeOverLifetimeModule sizeOverLifetime
		{
			get
			{
				return new ParticleSystem.SizeOverLifetimeModule(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000030F4 File Offset: 0x000012F4
		public ParticleSystem.SizeBySpeedModule sizeBySpeed
		{
			get
			{
				return new ParticleSystem.SizeBySpeedModule(this);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000310C File Offset: 0x0000130C
		public ParticleSystem.RotationOverLifetimeModule rotationOverLifetime
		{
			get
			{
				return new ParticleSystem.RotationOverLifetimeModule(this);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003124 File Offset: 0x00001324
		public ParticleSystem.RotationBySpeedModule rotationBySpeed
		{
			get
			{
				return new ParticleSystem.RotationBySpeedModule(this);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000313C File Offset: 0x0000133C
		public ParticleSystem.ExternalForcesModule externalForces
		{
			get
			{
				return new ParticleSystem.ExternalForcesModule(this);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003154 File Offset: 0x00001354
		public ParticleSystem.NoiseModule noise
		{
			get
			{
				return new ParticleSystem.NoiseModule(this);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000316C File Offset: 0x0000136C
		public ParticleSystem.CollisionModule collision
		{
			get
			{
				return new ParticleSystem.CollisionModule(this);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003184 File Offset: 0x00001384
		public ParticleSystem.TriggerModule trigger
		{
			get
			{
				return new ParticleSystem.TriggerModule(this);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000319C File Offset: 0x0000139C
		public ParticleSystem.SubEmittersModule subEmitters
		{
			get
			{
				return new ParticleSystem.SubEmittersModule(this);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000031B4 File Offset: 0x000013B4
		public ParticleSystem.TextureSheetAnimationModule textureSheetAnimation
		{
			get
			{
				return new ParticleSystem.TextureSheetAnimationModule(this);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000031CC File Offset: 0x000013CC
		public ParticleSystem.LightsModule lights
		{
			get
			{
				return new ParticleSystem.LightsModule(this);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000031E4 File Offset: 0x000013E4
		public ParticleSystem.TrailModule trails
		{
			get
			{
				return new ParticleSystem.TrailModule(this);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000031FC File Offset: 0x000013FC
		public ParticleSystem.CustomDataModule customData
		{
			get
			{
				return new ParticleSystem.CustomDataModule(this);
			}
		}

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isEmitting_Injected(IntPtr _unity_self);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStopped_Injected(IntPtr _unity_self);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPaused_Injected(IntPtr _unity_self);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_particleCount_Injected(IntPtr _unity_self);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_totalTime_Injected(IntPtr _unity_self);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_randomSeed_Injected(IntPtr _unity_self);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_randomSeed_Injected(IntPtr _unity_self, uint value);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useAutoRandomSeed_Injected(IntPtr _unity_self);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useAutoRandomSeed_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_proceduralSimulationSupported_Injected(IntPtr _unity_self);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetParticleCurrentSize_Injected(IntPtr _unity_self, ref ParticleSystem.Particle particle);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParticleCurrentSize3D_Injected(IntPtr _unity_self, ref ParticleSystem.Particle particle, out Vector3 ret);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParticleCurrentColor_Injected(IntPtr _unity_self, ref ParticleSystem.Particle particle, out Color32 ret);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetParticleMeshIndex_Injected(IntPtr _unity_self, ref ParticleSystem.Particle particle);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParticles_Injected(IntPtr _unity_self, out BlittableArrayWrapper particles, int size, int offset);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParticlesWithNativeArray_Injected(IntPtr _unity_self, IntPtr particles, int particlesLength, int size, int offset);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetParticles_Injected(IntPtr _unity_self, out BlittableArrayWrapper particles, int size, int offset);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetParticlesWithNativeArray_Injected(IntPtr _unity_self, IntPtr particles, int particlesLength, int size, int offset);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomParticleData_Injected(IntPtr _unity_self, ref BlittableListWrapper customData, ParticleSystemCustomData streamIndex);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCustomParticleData_Injected(IntPtr _unity_self, ref BlittableListWrapper customData, ParticleSystemCustomData streamIndex);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPlaybackState_Injected(IntPtr _unity_self, out ParticleSystem.PlaybackState ret);

		// Token: 0x060000A2 RID: 162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlaybackState_Injected(IntPtr _unity_self, [In] ref ParticleSystem.PlaybackState playbackState);

		// Token: 0x060000A3 RID: 163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrailDataInternal_Injected(IntPtr _unity_self, ref ParticleSystem.Trails trailData);

		// Token: 0x060000A4 RID: 164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTrails_Injected(IntPtr _unity_self, [In] ref ParticleSystem.Trails trailData);

		// Token: 0x060000A5 RID: 165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Simulate_Injected(IntPtr _unity_self, float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart, [DefaultValue("true")] bool fixedTimeStep);

		// Token: 0x060000A6 RID: 166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren);

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren, [DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior);

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAlive_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Emit_Internal_Injected(IntPtr _unity_self, int count);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Emit_Injected(IntPtr _unity_self, [In] ref ParticleSystem.EmitParams emitParams, int count);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EmitOld_Internal_Injected(IntPtr _unity_self, ref ParticleSystem.Particle particle);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TriggerSubEmitterForParticle_Injected(IntPtr _unity_self, int subEmitterIndex, [In] ref ParticleSystem.Particle particle);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TriggerSubEmitterForParticles_Injected(IntPtr _unity_self, int subEmitterIndex, ref BlittableListWrapper particles);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TriggerSubEmitterForAllParticles_Injected(IntPtr _unity_self, int subEmitterIndex);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AllocateAxisOfRotationAttribute_Injected(IntPtr _unity_self);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AllocateMeshIndexAttribute_Injected(IntPtr _unity_self);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AllocateCustomDataAttribute_Injected(IntPtr _unity_self, ParticleSystemCustomData stream);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_has3DParticleRotations_Injected(IntPtr _unity_self);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasNonUniformParticleSizes_Injected(IntPtr _unity_self);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* GetManagedJobData_Injected(IntPtr _unity_self);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetManagedJobHandle_Injected(IntPtr _unity_self, out JobHandle ret);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetManagedJobHandle_Injected(IntPtr _unity_self, [In] ref JobHandle handle);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleManagedJob_Injected(ref JobsUtility.JobScheduleParameters parameters, void* additionalData, out JobHandle ret);

		// Token: 0x02000004 RID: 4
		public struct MainModule
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000BA RID: 186 RVA: 0x00003220 File Offset: 0x00001420
			// (set) Token: 0x060000BB RID: 187 RVA: 0x00003238 File Offset: 0x00001438
			[Obsolete("Please use flipRotation instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/MainModule.flipRotation", false)]
			public float randomizeRotationDirection
			{
				get
				{
					return this.flipRotation;
				}
				set
				{
					this.flipRotation = value;
				}
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00003243 File Offset: 0x00001443
			internal MainModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00003250 File Offset: 0x00001450
			// (set) Token: 0x060000BE RID: 190 RVA: 0x00003268 File Offset: 0x00001468
			public Vector3 emitterVelocity
			{
				get
				{
					Vector3 vector;
					ParticleSystem.MainModule.get_emitterVelocity_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_emitterVelocity_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000BF RID: 191
			// (set) Token: 0x060000C0 RID: 192
			public extern float duration
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000C1 RID: 193
			// (set) Token: 0x060000C2 RID: 194
			public extern bool loop
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000C3 RID: 195
			// (set) Token: 0x060000C4 RID: 196
			public extern bool prewarm
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000327D File Offset: 0x0000147D
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000328A File Offset: 0x0000148A
			public ParticleSystem.MinMaxCurve startDelay
			{
				get
				{
					return this.startDelayBlittable;
				}
				set
				{
					this.startDelayBlittable = value;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000329C File Offset: 0x0000149C
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x000032B4 File Offset: 0x000014B4
			[NativeName("StartDelay")]
			private ParticleSystem.MinMaxCurveBlittable startDelayBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startDelayBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startDelayBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000C9 RID: 201
			// (set) Token: 0x060000CA RID: 202
			public extern float startDelayMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000CB RID: 203 RVA: 0x000032C9 File Offset: 0x000014C9
			// (set) Token: 0x060000CC RID: 204 RVA: 0x000032D6 File Offset: 0x000014D6
			public ParticleSystem.MinMaxCurve startLifetime
			{
				get
				{
					return this.startLifetimeBlittable;
				}
				set
				{
					this.startLifetimeBlittable = value;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000CD RID: 205 RVA: 0x000032E8 File Offset: 0x000014E8
			// (set) Token: 0x060000CE RID: 206 RVA: 0x00003300 File Offset: 0x00001500
			[NativeName("StartLifetime")]
			private ParticleSystem.MinMaxCurveBlittable startLifetimeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startLifetimeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startLifetimeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000CF RID: 207
			// (set) Token: 0x060000D0 RID: 208
			public extern float startLifetimeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003315 File Offset: 0x00001515
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003322 File Offset: 0x00001522
			public ParticleSystem.MinMaxCurve startSpeed
			{
				get
				{
					return this.startSpeedBlittable;
				}
				set
				{
					this.startSpeedBlittable = value;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003334 File Offset: 0x00001534
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000334C File Offset: 0x0000154C
			[NativeName("StartSpeed")]
			private ParticleSystem.MinMaxCurveBlittable startSpeedBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startSpeedBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSpeedBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000D5 RID: 213
			// (set) Token: 0x060000D6 RID: 214
			public extern float startSpeedMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000D7 RID: 215
			// (set) Token: 0x060000D8 RID: 216
			public extern bool startSize3D
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003361 File Offset: 0x00001561
			// (set) Token: 0x060000DA RID: 218 RVA: 0x0000336E File Offset: 0x0000156E
			public ParticleSystem.MinMaxCurve startSize
			{
				get
				{
					return this.startSizeBlittable;
				}
				set
				{
					this.startSizeBlittable = value;
				}
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00003380 File Offset: 0x00001580
			// (set) Token: 0x060000DC RID: 220 RVA: 0x00003398 File Offset: 0x00001598
			[NativeName("StartSizeX")]
			private ParticleSystem.MinMaxCurveBlittable startSizeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startSizeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000DD RID: 221
			// (set) Token: 0x060000DE RID: 222
			[NativeName("StartSizeXMultiplier")]
			public extern float startSizeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000DF RID: 223 RVA: 0x000033AD File Offset: 0x000015AD
			// (set) Token: 0x060000E0 RID: 224 RVA: 0x000033BA File Offset: 0x000015BA
			public ParticleSystem.MinMaxCurve startSizeX
			{
				get
				{
					return this.startSizeXBlittable;
				}
				set
				{
					this.startSizeXBlittable = value;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000E1 RID: 225 RVA: 0x000033CC File Offset: 0x000015CC
			// (set) Token: 0x060000E2 RID: 226 RVA: 0x000033E4 File Offset: 0x000015E4
			[NativeName("StartSizeX")]
			private ParticleSystem.MinMaxCurveBlittable startSizeXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startSizeXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000E3 RID: 227
			// (set) Token: 0x060000E4 RID: 228
			public extern float startSizeXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000E5 RID: 229 RVA: 0x000033F9 File Offset: 0x000015F9
			// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003406 File Offset: 0x00001606
			public ParticleSystem.MinMaxCurve startSizeY
			{
				get
				{
					return this.startSizeYBlittable;
				}
				set
				{
					this.startSizeYBlittable = value;
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003418 File Offset: 0x00001618
			// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003430 File Offset: 0x00001630
			[NativeName("StartSizeY")]
			private ParticleSystem.MinMaxCurveBlittable startSizeYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startSizeYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000E9 RID: 233
			// (set) Token: 0x060000EA RID: 234
			public extern float startSizeYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000EB RID: 235 RVA: 0x00003445 File Offset: 0x00001645
			// (set) Token: 0x060000EC RID: 236 RVA: 0x00003452 File Offset: 0x00001652
			public ParticleSystem.MinMaxCurve startSizeZ
			{
				get
				{
					return this.startSizeZBlittable;
				}
				set
				{
					this.startSizeZBlittable = value;
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000ED RID: 237 RVA: 0x00003464 File Offset: 0x00001664
			// (set) Token: 0x060000EE RID: 238 RVA: 0x0000347C File Offset: 0x0000167C
			[NativeName("StartSizeZ")]
			private ParticleSystem.MinMaxCurveBlittable startSizeZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startSizeZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startSizeZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000EF RID: 239
			// (set) Token: 0x060000F0 RID: 240
			public extern float startSizeZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000F1 RID: 241
			// (set) Token: 0x060000F2 RID: 242
			public extern bool startRotation3D
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003491 File Offset: 0x00001691
			// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000349E File Offset: 0x0000169E
			public ParticleSystem.MinMaxCurve startRotation
			{
				get
				{
					return this.startRotationBlittable;
				}
				set
				{
					this.startRotationBlittable = value;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000F5 RID: 245 RVA: 0x000034B0 File Offset: 0x000016B0
			// (set) Token: 0x060000F6 RID: 246 RVA: 0x000034C8 File Offset: 0x000016C8
			[NativeName("StartRotationZ")]
			private ParticleSystem.MinMaxCurveBlittable startRotationBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startRotationBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000F7 RID: 247
			// (set) Token: 0x060000F8 RID: 248
			[NativeName("StartRotationZMultiplier")]
			public extern float startRotationMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000F9 RID: 249 RVA: 0x000034DD File Offset: 0x000016DD
			// (set) Token: 0x060000FA RID: 250 RVA: 0x000034EA File Offset: 0x000016EA
			public ParticleSystem.MinMaxCurve startRotationX
			{
				get
				{
					return this.startRotationXBlittable;
				}
				set
				{
					this.startRotationXBlittable = value;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000FB RID: 251 RVA: 0x000034FC File Offset: 0x000016FC
			// (set) Token: 0x060000FC RID: 252 RVA: 0x00003514 File Offset: 0x00001714
			[NativeName("StartRotationX")]
			private ParticleSystem.MinMaxCurveBlittable startRotationXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startRotationXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000FD RID: 253
			// (set) Token: 0x060000FE RID: 254
			public extern float startRotationXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00003529 File Offset: 0x00001729
			// (set) Token: 0x06000100 RID: 256 RVA: 0x00003536 File Offset: 0x00001736
			public ParticleSystem.MinMaxCurve startRotationY
			{
				get
				{
					return this.startRotationYBlittable;
				}
				set
				{
					this.startRotationYBlittable = value;
				}
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000101 RID: 257 RVA: 0x00003548 File Offset: 0x00001748
			// (set) Token: 0x06000102 RID: 258 RVA: 0x00003560 File Offset: 0x00001760
			[NativeName("StartRotationY")]
			private ParticleSystem.MinMaxCurveBlittable startRotationYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startRotationYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000103 RID: 259
			// (set) Token: 0x06000104 RID: 260
			public extern float startRotationYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000105 RID: 261 RVA: 0x00003575 File Offset: 0x00001775
			// (set) Token: 0x06000106 RID: 262 RVA: 0x00003582 File Offset: 0x00001782
			public ParticleSystem.MinMaxCurve startRotationZ
			{
				get
				{
					return this.startRotationZBlittable;
				}
				set
				{
					this.startRotationZBlittable = value;
				}
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000107 RID: 263 RVA: 0x00003594 File Offset: 0x00001794
			// (set) Token: 0x06000108 RID: 264 RVA: 0x000035AC File Offset: 0x000017AC
			[NativeName("StartRotationZ")]
			private ParticleSystem.MinMaxCurveBlittable startRotationZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_startRotationZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startRotationZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x06000109 RID: 265
			// (set) Token: 0x0600010A RID: 266
			public extern float startRotationZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600010B RID: 267
			// (set) Token: 0x0600010C RID: 268
			public extern float flipRotation
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600010D RID: 269 RVA: 0x000035C1 File Offset: 0x000017C1
			// (set) Token: 0x0600010E RID: 270 RVA: 0x000035CE File Offset: 0x000017CE
			public ParticleSystem.MinMaxGradient startColor
			{
				get
				{
					return this.startColorBlittable;
				}
				set
				{
					this.startColorBlittable = value;
				}
			}

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x0600010F RID: 271 RVA: 0x000035E0 File Offset: 0x000017E0
			// (set) Token: 0x06000110 RID: 272 RVA: 0x000035F8 File Offset: 0x000017F8
			[NativeName("StartColor")]
			private ParticleSystem.MinMaxGradientBlittable startColorBlittable
			{
				get
				{
					ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable;
					ParticleSystem.MainModule.get_startColorBlittable_Injected(ref this, out minMaxGradientBlittable);
					return minMaxGradientBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_startColorBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000111 RID: 273
			// (set) Token: 0x06000112 RID: 274
			public extern ParticleSystemGravitySource gravitySource
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000113 RID: 275 RVA: 0x0000360D File Offset: 0x0000180D
			// (set) Token: 0x06000114 RID: 276 RVA: 0x0000361A File Offset: 0x0000181A
			public ParticleSystem.MinMaxCurve gravityModifier
			{
				get
				{
					return this.gravityModifierBlittable;
				}
				set
				{
					this.gravityModifierBlittable = value;
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000115 RID: 277 RVA: 0x0000362C File Offset: 0x0000182C
			// (set) Token: 0x06000116 RID: 278 RVA: 0x00003644 File Offset: 0x00001844
			[NativeName("GravityModifier")]
			private ParticleSystem.MinMaxCurveBlittable gravityModifierBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.MainModule.get_gravityModifierBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_gravityModifierBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000117 RID: 279
			// (set) Token: 0x06000118 RID: 280
			public extern float gravityModifierMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000119 RID: 281
			// (set) Token: 0x0600011A RID: 282
			public extern ParticleSystemSimulationSpace simulationSpace
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600011B RID: 283 RVA: 0x0000365C File Offset: 0x0000185C
			// (set) Token: 0x0600011C RID: 284 RVA: 0x00003674 File Offset: 0x00001874
			public Transform customSimulationSpace
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<Transform>(ParticleSystem.MainModule.get_customSimulationSpace_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_customSimulationSpace_Injected(ref this, Object.MarshalledUnityObject.Marshal<Transform>(value));
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600011D RID: 285
			// (set) Token: 0x0600011E RID: 286
			public extern float simulationSpeed
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x0600011F RID: 287
			// (set) Token: 0x06000120 RID: 288
			public extern bool useUnscaledTime
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000121 RID: 289
			// (set) Token: 0x06000122 RID: 290
			public extern ParticleSystemScalingMode scalingMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000123 RID: 291
			// (set) Token: 0x06000124 RID: 292
			public extern bool playOnAwake
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000125 RID: 293
			// (set) Token: 0x06000126 RID: 294
			public extern int maxParticles
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000127 RID: 295
			// (set) Token: 0x06000128 RID: 296
			public extern ParticleSystemEmitterVelocityMode emitterVelocityMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000129 RID: 297
			// (set) Token: 0x0600012A RID: 298
			public extern ParticleSystemStopAction stopAction
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600012B RID: 299
			// (set) Token: 0x0600012C RID: 300
			public extern ParticleSystemRingBufferMode ringBufferMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x0600012D RID: 301 RVA: 0x00003690 File Offset: 0x00001890
			// (set) Token: 0x0600012E RID: 302 RVA: 0x000036A8 File Offset: 0x000018A8
			public Vector2 ringBufferLoopRange
			{
				get
				{
					Vector2 vector;
					ParticleSystem.MainModule.get_ringBufferLoopRange_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.MainModule.set_ringBufferLoopRange_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600012F RID: 303
			// (set) Token: 0x06000130 RID: 304
			public extern ParticleSystemCullingMode cullingMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000131 RID: 305
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_emitterVelocity_Injected(ref ParticleSystem.MainModule _unity_self, out Vector3 ret);

			// Token: 0x06000132 RID: 306
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_emitterVelocity_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref Vector3 value);

			// Token: 0x06000133 RID: 307
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startDelayBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000134 RID: 308
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startDelayBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000135 RID: 309
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startLifetimeBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000136 RID: 310
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startLifetimeBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000137 RID: 311
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSpeedBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000138 RID: 312
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSpeedBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000139 RID: 313
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600013A RID: 314
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600013B RID: 315
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeXBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600013C RID: 316
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeXBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600013D RID: 317
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeYBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600013E RID: 318
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeYBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600013F RID: 319
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startSizeZBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000140 RID: 320
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startSizeZBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000141 RID: 321
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000142 RID: 322
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000143 RID: 323
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationXBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000144 RID: 324
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationXBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000145 RID: 325
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationYBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000146 RID: 326
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationYBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000147 RID: 327
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startRotationZBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000148 RID: 328
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startRotationZBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000149 RID: 329
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startColorBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxGradientBlittable ret);

			// Token: 0x0600014A RID: 330
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startColorBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxGradientBlittable value);

			// Token: 0x0600014B RID: 331
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_gravityModifierBlittable_Injected(ref ParticleSystem.MainModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600014C RID: 332
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_gravityModifierBlittable_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600014D RID: 333
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self);

			// Token: 0x0600014E RID: 334
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_customSimulationSpace_Injected(ref ParticleSystem.MainModule _unity_self, IntPtr value);

			// Token: 0x0600014F RID: 335
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, out Vector2 ret);

			// Token: 0x06000150 RID: 336
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_ringBufferLoopRange_Injected(ref ParticleSystem.MainModule _unity_self, [In] ref Vector2 value);

			// Token: 0x04000004 RID: 4
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000005 RID: 5
		public struct EmissionModule
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000151 RID: 337 RVA: 0x000036C0 File Offset: 0x000018C0
			// (set) Token: 0x06000152 RID: 338 RVA: 0x000036D3 File Offset: 0x000018D3
			[Obsolete("ParticleSystemEmissionType no longer does anything. Time and Distance based emission are now both always active.", false)]
			public ParticleSystemEmissionType type
			{
				get
				{
					return ParticleSystemEmissionType.Time;
				}
				set
				{
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000153 RID: 339 RVA: 0x000036D8 File Offset: 0x000018D8
			// (set) Token: 0x06000154 RID: 340 RVA: 0x000036F0 File Offset: 0x000018F0
			[Obsolete("rate property is deprecated. Use rateOverTime or rateOverDistance instead.", false)]
			public ParticleSystem.MinMaxCurve rate
			{
				get
				{
					return this.rateOverTime;
				}
				set
				{
					this.rateOverTime = value;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000155 RID: 341 RVA: 0x000036FC File Offset: 0x000018FC
			// (set) Token: 0x06000156 RID: 342 RVA: 0x00003714 File Offset: 0x00001914
			[Obsolete("rateMultiplier property is deprecated. Use rateOverTimeMultiplier or rateOverDistanceMultiplier instead.", false)]
			public float rateMultiplier
			{
				get
				{
					return this.rateOverTimeMultiplier;
				}
				set
				{
					this.rateOverTimeMultiplier = value;
				}
			}

			// Token: 0x06000157 RID: 343 RVA: 0x0000371F File Offset: 0x0000191F
			internal EmissionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000158 RID: 344
			// (set) Token: 0x06000159 RID: 345
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600015A RID: 346 RVA: 0x00003729 File Offset: 0x00001929
			// (set) Token: 0x0600015B RID: 347 RVA: 0x00003736 File Offset: 0x00001936
			public ParticleSystem.MinMaxCurve rateOverTime
			{
				get
				{
					return this.rateOverTimeBlittable;
				}
				set
				{
					this.rateOverTimeBlittable = value;
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600015C RID: 348 RVA: 0x00003748 File Offset: 0x00001948
			// (set) Token: 0x0600015D RID: 349 RVA: 0x00003760 File Offset: 0x00001960
			[NativeName("RateOverTime")]
			private ParticleSystem.MinMaxCurveBlittable rateOverTimeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.EmissionModule.get_rateOverTimeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverTimeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600015E RID: 350
			// (set) Token: 0x0600015F RID: 351
			public extern float rateOverTimeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000160 RID: 352 RVA: 0x00003775 File Offset: 0x00001975
			// (set) Token: 0x06000161 RID: 353 RVA: 0x00003782 File Offset: 0x00001982
			public ParticleSystem.MinMaxCurve rateOverDistance
			{
				get
				{
					return this.rateOverDistanceBlittable;
				}
				set
				{
					this.rateOverDistanceBlittable = value;
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000162 RID: 354 RVA: 0x00003794 File Offset: 0x00001994
			// (set) Token: 0x06000163 RID: 355 RVA: 0x000037AC File Offset: 0x000019AC
			[NativeName("RateOverDistance")]
			private ParticleSystem.MinMaxCurveBlittable rateOverDistanceBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.EmissionModule.get_rateOverDistanceBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.EmissionModule.set_rateOverDistanceBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000164 RID: 356
			// (set) Token: 0x06000165 RID: 357
			public extern float rateOverDistanceMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000166 RID: 358 RVA: 0x000037C1 File Offset: 0x000019C1
			public void SetBursts(ParticleSystem.Burst[] bursts)
			{
				this.SetBursts(bursts, bursts.Length);
			}

			// Token: 0x06000167 RID: 359 RVA: 0x000037D0 File Offset: 0x000019D0
			public void SetBursts(ParticleSystem.Burst[] bursts, int size)
			{
				this.burstCount = size;
				for (int i = 0; i < size; i++)
				{
					this.SetBurst(i, bursts[i]);
				}
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00003808 File Offset: 0x00001A08
			public int GetBursts(ParticleSystem.Burst[] bursts)
			{
				int burstCount = this.burstCount;
				for (int i = 0; i < burstCount; i++)
				{
					bursts[i] = this.GetBurst(i);
				}
				return burstCount;
			}

			// Token: 0x06000169 RID: 361 RVA: 0x00003840 File Offset: 0x00001A40
			[NativeThrows]
			public void SetBurst(int index, ParticleSystem.Burst burst)
			{
				ParticleSystem.EmissionModule.SetBurst_Injected(ref this, index, ref burst);
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00003858 File Offset: 0x00001A58
			[NativeThrows]
			public ParticleSystem.Burst GetBurst(int index)
			{
				ParticleSystem.Burst burst;
				ParticleSystem.EmissionModule.GetBurst_Injected(ref this, index, out burst);
				return burst;
			}

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x0600016B RID: 363
			// (set) Token: 0x0600016C RID: 364
			public extern int burstCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x0600016D RID: 365
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rateOverTimeBlittable_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600016E RID: 366
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverTimeBlittable_Injected(ref ParticleSystem.EmissionModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600016F RID: 367
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rateOverDistanceBlittable_Injected(ref ParticleSystem.EmissionModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000170 RID: 368
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rateOverDistanceBlittable_Injected(ref ParticleSystem.EmissionModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000171 RID: 369
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, [In] ref ParticleSystem.Burst burst);

			// Token: 0x06000172 RID: 370
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBurst_Injected(ref ParticleSystem.EmissionModule _unity_self, int index, out ParticleSystem.Burst ret);

			// Token: 0x04000005 RID: 5
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000006 RID: 6
		public struct ShapeModule
		{
			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000173 RID: 371 RVA: 0x00003870 File Offset: 0x00001A70
			// (set) Token: 0x06000174 RID: 372 RVA: 0x00003888 File Offset: 0x00001A88
			[Obsolete("Please use scale instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/ShapeModule.scale", false)]
			public Vector3 box
			{
				get
				{
					return this.scale;
				}
				set
				{
					this.scale = value;
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000175 RID: 373 RVA: 0x00003894 File Offset: 0x00001A94
			// (set) Token: 0x06000176 RID: 374 RVA: 0x000038B1 File Offset: 0x00001AB1
			[Obsolete("meshScale property is deprecated.Please use scale instead.", false)]
			public float meshScale
			{
				get
				{
					return this.scale.x;
				}
				set
				{
					this.scale = new Vector3(value, value, value);
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x06000177 RID: 375 RVA: 0x000038C4 File Offset: 0x00001AC4
			// (set) Token: 0x06000178 RID: 376 RVA: 0x000038E6 File Offset: 0x00001AE6
			[Obsolete("randomDirection property is deprecated. Use randomDirectionAmount instead.", false)]
			public bool randomDirection
			{
				get
				{
					return this.randomDirectionAmount >= 0.5f;
				}
				set
				{
					this.randomDirectionAmount = (value ? 1f : 0f);
				}
			}

			// Token: 0x06000179 RID: 377 RVA: 0x000038FF File Offset: 0x00001AFF
			internal ShapeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600017A RID: 378
			// (set) Token: 0x0600017B RID: 379
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600017C RID: 380
			// (set) Token: 0x0600017D RID: 381
			public extern ParticleSystemShapeType shapeType
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600017E RID: 382
			// (set) Token: 0x0600017F RID: 383
			public extern float randomDirectionAmount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000180 RID: 384
			// (set) Token: 0x06000181 RID: 385
			public extern float sphericalDirectionAmount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000182 RID: 386
			// (set) Token: 0x06000183 RID: 387
			public extern float randomPositionAmount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000184 RID: 388
			// (set) Token: 0x06000185 RID: 389
			public extern bool alignToDirection
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000186 RID: 390
			// (set) Token: 0x06000187 RID: 391
			public extern float radius
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000188 RID: 392
			// (set) Token: 0x06000189 RID: 393
			public extern ParticleSystemShapeMultiModeValue radiusMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x0600018A RID: 394
			// (set) Token: 0x0600018B RID: 395
			public extern float radiusSpread
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600018C RID: 396 RVA: 0x00003909 File Offset: 0x00001B09
			// (set) Token: 0x0600018D RID: 397 RVA: 0x00003916 File Offset: 0x00001B16
			public ParticleSystem.MinMaxCurve radiusSpeed
			{
				get
				{
					return this.radiusSpeedBlittable;
				}
				set
				{
					this.radiusSpeedBlittable = value;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x0600018E RID: 398 RVA: 0x00003928 File Offset: 0x00001B28
			// (set) Token: 0x0600018F RID: 399 RVA: 0x00003940 File Offset: 0x00001B40
			[NativeName("RadiusSpeed")]
			private ParticleSystem.MinMaxCurveBlittable radiusSpeedBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ShapeModule.get_radiusSpeedBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_radiusSpeedBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000190 RID: 400
			// (set) Token: 0x06000191 RID: 401
			public extern float radiusSpeedMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000192 RID: 402
			// (set) Token: 0x06000193 RID: 403
			public extern float radiusThickness
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000194 RID: 404
			// (set) Token: 0x06000195 RID: 405
			public extern float angle
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000196 RID: 406
			// (set) Token: 0x06000197 RID: 407
			public extern float length
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000198 RID: 408 RVA: 0x00003958 File Offset: 0x00001B58
			// (set) Token: 0x06000199 RID: 409 RVA: 0x00003970 File Offset: 0x00001B70
			public Vector3 boxThickness
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_boxThickness_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_boxThickness_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600019A RID: 410
			// (set) Token: 0x0600019B RID: 411
			public extern ParticleSystemMeshShapeType meshShapeType
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x0600019C RID: 412 RVA: 0x00003988 File Offset: 0x00001B88
			// (set) Token: 0x0600019D RID: 413 RVA: 0x000039A0 File Offset: 0x00001BA0
			public Mesh mesh
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<Mesh>(ParticleSystem.ShapeModule.get_mesh_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_mesh_Injected(ref this, Object.MarshalledUnityObject.Marshal<Mesh>(value));
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600019E RID: 414 RVA: 0x000039BC File Offset: 0x00001BBC
			// (set) Token: 0x0600019F RID: 415 RVA: 0x000039D4 File Offset: 0x00001BD4
			public MeshRenderer meshRenderer
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<MeshRenderer>(ParticleSystem.ShapeModule.get_meshRenderer_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshRenderer_Injected(ref this, Object.MarshalledUnityObject.Marshal<MeshRenderer>(value));
				}
			}

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060001A0 RID: 416 RVA: 0x000039F0 File Offset: 0x00001BF0
			// (set) Token: 0x060001A1 RID: 417 RVA: 0x00003A08 File Offset: 0x00001C08
			public SkinnedMeshRenderer skinnedMeshRenderer
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<SkinnedMeshRenderer>(ParticleSystem.ShapeModule.get_skinnedMeshRenderer_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_skinnedMeshRenderer_Injected(ref this, Object.MarshalledUnityObject.Marshal<SkinnedMeshRenderer>(value));
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003A24 File Offset: 0x00001C24
			// (set) Token: 0x060001A3 RID: 419 RVA: 0x00003A3C File Offset: 0x00001C3C
			public Sprite sprite
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<Sprite>(ParticleSystem.ShapeModule.get_sprite_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_sprite_Injected(ref this, Object.MarshalledUnityObject.Marshal<Sprite>(value));
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003A58 File Offset: 0x00001C58
			// (set) Token: 0x060001A5 RID: 421 RVA: 0x00003A70 File Offset: 0x00001C70
			public SpriteRenderer spriteRenderer
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<SpriteRenderer>(ParticleSystem.ShapeModule.get_spriteRenderer_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_spriteRenderer_Injected(ref this, Object.MarshalledUnityObject.Marshal<SpriteRenderer>(value));
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060001A6 RID: 422
			// (set) Token: 0x060001A7 RID: 423
			public extern bool useMeshMaterialIndex
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060001A8 RID: 424
			// (set) Token: 0x060001A9 RID: 425
			public extern int meshMaterialIndex
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060001AA RID: 426
			// (set) Token: 0x060001AB RID: 427
			public extern bool useMeshColors
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060001AC RID: 428
			// (set) Token: 0x060001AD RID: 429
			public extern float normalOffset
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060001AE RID: 430
			// (set) Token: 0x060001AF RID: 431
			public extern ParticleSystemShapeMultiModeValue meshSpawnMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060001B0 RID: 432
			// (set) Token: 0x060001B1 RID: 433
			public extern float meshSpawnSpread
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003A89 File Offset: 0x00001C89
			// (set) Token: 0x060001B3 RID: 435 RVA: 0x00003A96 File Offset: 0x00001C96
			public ParticleSystem.MinMaxCurve meshSpawnSpeed
			{
				get
				{
					return this.meshSpawnSpeedBlittable;
				}
				set
				{
					this.meshSpawnSpeedBlittable = value;
				}
			}

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003AA8 File Offset: 0x00001CA8
			// (set) Token: 0x060001B5 RID: 437 RVA: 0x00003AC0 File Offset: 0x00001CC0
			[NativeName("MeshSpawnSpeed")]
			private ParticleSystem.MinMaxCurveBlittable meshSpawnSpeedBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ShapeModule.get_meshSpawnSpeedBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_meshSpawnSpeedBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060001B6 RID: 438
			// (set) Token: 0x060001B7 RID: 439
			public extern float meshSpawnSpeedMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060001B8 RID: 440
			// (set) Token: 0x060001B9 RID: 441
			public extern float arc
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060001BA RID: 442
			// (set) Token: 0x060001BB RID: 443
			public extern ParticleSystemShapeMultiModeValue arcMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060001BC RID: 444
			// (set) Token: 0x060001BD RID: 445
			public extern float arcSpread
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060001BE RID: 446 RVA: 0x00003AD5 File Offset: 0x00001CD5
			// (set) Token: 0x060001BF RID: 447 RVA: 0x00003AE2 File Offset: 0x00001CE2
			public ParticleSystem.MinMaxCurve arcSpeed
			{
				get
				{
					return this.arcSpeedBlittable;
				}
				set
				{
					this.arcSpeedBlittable = value;
				}
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060001C0 RID: 448 RVA: 0x00003AF4 File Offset: 0x00001CF4
			// (set) Token: 0x060001C1 RID: 449 RVA: 0x00003B0C File Offset: 0x00001D0C
			[NativeName("ArcSpeed")]
			private ParticleSystem.MinMaxCurveBlittable arcSpeedBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ShapeModule.get_arcSpeedBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_arcSpeedBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060001C2 RID: 450
			// (set) Token: 0x060001C3 RID: 451
			public extern float arcSpeedMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060001C4 RID: 452
			// (set) Token: 0x060001C5 RID: 453
			public extern float donutRadius
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x00003B24 File Offset: 0x00001D24
			// (set) Token: 0x060001C7 RID: 455 RVA: 0x00003B3C File Offset: 0x00001D3C
			public Vector3 position
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_position_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_position_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003B54 File Offset: 0x00001D54
			// (set) Token: 0x060001C9 RID: 457 RVA: 0x00003B6C File Offset: 0x00001D6C
			public Vector3 rotation
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_rotation_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_rotation_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060001CA RID: 458 RVA: 0x00003B84 File Offset: 0x00001D84
			// (set) Token: 0x060001CB RID: 459 RVA: 0x00003B9C File Offset: 0x00001D9C
			public Vector3 scale
			{
				get
				{
					Vector3 vector;
					ParticleSystem.ShapeModule.get_scale_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_scale_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060001CC RID: 460 RVA: 0x00003BB4 File Offset: 0x00001DB4
			// (set) Token: 0x060001CD RID: 461 RVA: 0x00003BCC File Offset: 0x00001DCC
			public Texture2D texture
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<Texture2D>(ParticleSystem.ShapeModule.get_texture_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ShapeModule.set_texture_Injected(ref this, Object.MarshalledUnityObject.Marshal<Texture2D>(value));
				}
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060001CE RID: 462
			// (set) Token: 0x060001CF RID: 463
			public extern ParticleSystemShapeTextureChannel textureClipChannel
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060001D0 RID: 464
			// (set) Token: 0x060001D1 RID: 465
			public extern float textureClipThreshold
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060001D2 RID: 466
			// (set) Token: 0x060001D3 RID: 467
			public extern bool textureColorAffectsParticles
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060001D4 RID: 468
			// (set) Token: 0x060001D5 RID: 469
			public extern bool textureAlphaAffectsParticles
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060001D6 RID: 470
			// (set) Token: 0x060001D7 RID: 471
			public extern bool textureBilinearFiltering
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060001D8 RID: 472
			// (set) Token: 0x060001D9 RID: 473
			public extern int textureUVChannel
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x060001DA RID: 474
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_radiusSpeedBlittable_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060001DB RID: 475
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radiusSpeedBlittable_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060001DC RID: 476
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x060001DD RID: 477
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_boxThickness_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref Vector3 value);

			// Token: 0x060001DE RID: 478
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001DF RID: 479
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_mesh_Injected(ref ParticleSystem.ShapeModule _unity_self, IntPtr value);

			// Token: 0x060001E0 RID: 480
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E1 RID: 481
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, IntPtr value);

			// Token: 0x060001E2 RID: 482
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E3 RID: 483
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_skinnedMeshRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, IntPtr value);

			// Token: 0x060001E4 RID: 484
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E5 RID: 485
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sprite_Injected(ref ParticleSystem.ShapeModule _unity_self, IntPtr value);

			// Token: 0x060001E6 RID: 486
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001E7 RID: 487
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_spriteRenderer_Injected(ref ParticleSystem.ShapeModule _unity_self, IntPtr value);

			// Token: 0x060001E8 RID: 488
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_meshSpawnSpeedBlittable_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060001E9 RID: 489
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_meshSpawnSpeedBlittable_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060001EA RID: 490
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_arcSpeedBlittable_Injected(ref ParticleSystem.ShapeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060001EB RID: 491
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_arcSpeedBlittable_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060001EC RID: 492
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_position_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x060001ED RID: 493
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_position_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref Vector3 value);

			// Token: 0x060001EE RID: 494
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x060001EF RID: 495
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rotation_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref Vector3 value);

			// Token: 0x060001F0 RID: 496
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, out Vector3 ret);

			// Token: 0x060001F1 RID: 497
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scale_Injected(ref ParticleSystem.ShapeModule _unity_self, [In] ref Vector3 value);

			// Token: 0x060001F2 RID: 498
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_texture_Injected(ref ParticleSystem.ShapeModule _unity_self);

			// Token: 0x060001F3 RID: 499
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_texture_Injected(ref ParticleSystem.ShapeModule _unity_self, IntPtr value);

			// Token: 0x04000006 RID: 6
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000007 RID: 7
		public struct CollisionModule
		{
			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003BE8 File Offset: 0x00001DE8
			[Obsolete("The maxPlaneCount restriction has been removed. Please use planeCount instead to find out how many planes there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/CollisionModule.planeCount", false)]
			public int maxPlaneCount
			{
				get
				{
					return this.planeCount;
				}
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x00003C00 File Offset: 0x00001E00
			internal CollisionModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060001F6 RID: 502
			// (set) Token: 0x060001F7 RID: 503
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060001F8 RID: 504
			// (set) Token: 0x060001F9 RID: 505
			public extern ParticleSystemCollisionType type
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060001FA RID: 506
			// (set) Token: 0x060001FB RID: 507
			public extern ParticleSystemCollisionMode mode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060001FC RID: 508 RVA: 0x00003C0A File Offset: 0x00001E0A
			// (set) Token: 0x060001FD RID: 509 RVA: 0x00003C17 File Offset: 0x00001E17
			public ParticleSystem.MinMaxCurve dampen
			{
				get
				{
					return this.dampenBlittable;
				}
				set
				{
					this.dampenBlittable = value;
				}
			}

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060001FE RID: 510 RVA: 0x00003C28 File Offset: 0x00001E28
			// (set) Token: 0x060001FF RID: 511 RVA: 0x00003C40 File Offset: 0x00001E40
			[NativeName("Dampen")]
			private ParticleSystem.MinMaxCurveBlittable dampenBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.CollisionModule.get_dampenBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_dampenBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x06000200 RID: 512
			// (set) Token: 0x06000201 RID: 513
			public extern float dampenMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x06000202 RID: 514 RVA: 0x00003C55 File Offset: 0x00001E55
			// (set) Token: 0x06000203 RID: 515 RVA: 0x00003C62 File Offset: 0x00001E62
			public ParticleSystem.MinMaxCurve bounce
			{
				get
				{
					return this.bounceBlittable;
				}
				set
				{
					this.bounceBlittable = value;
				}
			}

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x06000204 RID: 516 RVA: 0x00003C74 File Offset: 0x00001E74
			// (set) Token: 0x06000205 RID: 517 RVA: 0x00003C8C File Offset: 0x00001E8C
			[NativeName("Bounce")]
			private ParticleSystem.MinMaxCurveBlittable bounceBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.CollisionModule.get_bounceBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_bounceBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x06000206 RID: 518
			// (set) Token: 0x06000207 RID: 519
			public extern float bounceMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x06000208 RID: 520 RVA: 0x00003CA1 File Offset: 0x00001EA1
			// (set) Token: 0x06000209 RID: 521 RVA: 0x00003CAE File Offset: 0x00001EAE
			public ParticleSystem.MinMaxCurve lifetimeLoss
			{
				get
				{
					return this.lifetimeLossBlittable;
				}
				set
				{
					this.lifetimeLossBlittable = value;
				}
			}

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x0600020A RID: 522 RVA: 0x00003CC0 File Offset: 0x00001EC0
			// (set) Token: 0x0600020B RID: 523 RVA: 0x00003CD8 File Offset: 0x00001ED8
			[NativeName("LifetimeLoss")]
			private ParticleSystem.MinMaxCurveBlittable lifetimeLossBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.CollisionModule.get_lifetimeLossBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_lifetimeLossBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x0600020C RID: 524
			// (set) Token: 0x0600020D RID: 525
			public extern float lifetimeLossMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x0600020E RID: 526
			// (set) Token: 0x0600020F RID: 527
			public extern float minKillSpeed
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x06000210 RID: 528
			// (set) Token: 0x06000211 RID: 529
			public extern float maxKillSpeed
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000212 RID: 530 RVA: 0x00003CF0 File Offset: 0x00001EF0
			// (set) Token: 0x06000213 RID: 531 RVA: 0x00003D08 File Offset: 0x00001F08
			public LayerMask collidesWith
			{
				get
				{
					LayerMask layerMask;
					ParticleSystem.CollisionModule.get_collidesWith_Injected(ref this, out layerMask);
					return layerMask;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.CollisionModule.set_collidesWith_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000214 RID: 532
			// (set) Token: 0x06000215 RID: 533
			public extern bool enableDynamicColliders
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x06000216 RID: 534
			// (set) Token: 0x06000217 RID: 535
			public extern int maxCollisionShapes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000218 RID: 536
			// (set) Token: 0x06000219 RID: 537
			public extern ParticleSystemCollisionQuality quality
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600021A RID: 538
			// (set) Token: 0x0600021B RID: 539
			public extern float voxelSize
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600021C RID: 540
			// (set) Token: 0x0600021D RID: 541
			public extern float radiusScale
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600021E RID: 542
			// (set) Token: 0x0600021F RID: 543
			public extern bool sendCollisionMessages
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000220 RID: 544
			// (set) Token: 0x06000221 RID: 545
			public extern float colliderForce
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000222 RID: 546
			// (set) Token: 0x06000223 RID: 547
			public extern bool multiplyColliderForceByCollisionAngle
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x06000224 RID: 548
			// (set) Token: 0x06000225 RID: 549
			public extern bool multiplyColliderForceByParticleSpeed
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x06000226 RID: 550
			// (set) Token: 0x06000227 RID: 551
			public extern bool multiplyColliderForceByParticleSize
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000228 RID: 552 RVA: 0x00003D20 File Offset: 0x00001F20
			[NativeThrows]
			public void AddPlane(Transform transform)
			{
				ParticleSystem.CollisionModule.AddPlane_Injected(ref this, Object.MarshalledUnityObject.Marshal<Transform>(transform));
			}

			// Token: 0x06000229 RID: 553
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void RemovePlane(int index);

			// Token: 0x0600022A RID: 554 RVA: 0x00003D39 File Offset: 0x00001F39
			public void RemovePlane(Transform transform)
			{
				this.RemovePlaneObject(transform);
			}

			// Token: 0x0600022B RID: 555 RVA: 0x00003D44 File Offset: 0x00001F44
			[NativeThrows]
			private void RemovePlaneObject(Transform transform)
			{
				ParticleSystem.CollisionModule.RemovePlaneObject_Injected(ref this, Object.MarshalledUnityObject.Marshal<Transform>(transform));
			}

			// Token: 0x0600022C RID: 556 RVA: 0x00003D60 File Offset: 0x00001F60
			[NativeThrows]
			public void SetPlane(int index, Transform transform)
			{
				ParticleSystem.CollisionModule.SetPlane_Injected(ref this, index, Object.MarshalledUnityObject.Marshal<Transform>(transform));
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00003D7C File Offset: 0x00001F7C
			[NativeThrows]
			public Transform GetPlane(int index)
			{
				return Unmarshal.UnmarshalUnityObject<Transform>(ParticleSystem.CollisionModule.GetPlane_Injected(ref this, index));
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600022E RID: 558
			[NativeThrows]
			public extern int planeCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600022F RID: 559
			// (set) Token: 0x06000230 RID: 560
			[Obsolete("enableInteriorCollisions property is deprecated and is no longer required and has no effect on the particle system.", false)]
			public extern bool enableInteriorCollisions
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000231 RID: 561
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_dampenBlittable_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000232 RID: 562
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dampenBlittable_Injected(ref ParticleSystem.CollisionModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000233 RID: 563
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_bounceBlittable_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000234 RID: 564
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_bounceBlittable_Injected(ref ParticleSystem.CollisionModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000235 RID: 565
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_lifetimeLossBlittable_Injected(ref ParticleSystem.CollisionModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000236 RID: 566
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeLossBlittable_Injected(ref ParticleSystem.CollisionModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000237 RID: 567
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, out LayerMask ret);

			// Token: 0x06000238 RID: 568
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_collidesWith_Injected(ref ParticleSystem.CollisionModule _unity_self, [In] ref LayerMask value);

			// Token: 0x06000239 RID: 569
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, IntPtr transform);

			// Token: 0x0600023A RID: 570
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlaneObject_Injected(ref ParticleSystem.CollisionModule _unity_self, IntPtr transform);

			// Token: 0x0600023B RID: 571
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index, IntPtr transform);

			// Token: 0x0600023C RID: 572
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetPlane_Injected(ref ParticleSystem.CollisionModule _unity_self, int index);

			// Token: 0x04000007 RID: 7
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000008 RID: 8
		public struct TriggerModule
		{
			// Token: 0x170000CB RID: 203
			// (get) Token: 0x0600023D RID: 573 RVA: 0x00003D98 File Offset: 0x00001F98
			[Obsolete("The maxColliderCount restriction has been removed. Please use colliderCount instead to find out how many colliders there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/TriggerModule.colliderCount", false)]
			public int maxColliderCount
			{
				get
				{
					return this.colliderCount;
				}
			}

			// Token: 0x0600023E RID: 574 RVA: 0x00003DB0 File Offset: 0x00001FB0
			internal TriggerModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x0600023F RID: 575
			// (set) Token: 0x06000240 RID: 576
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000241 RID: 577
			// (set) Token: 0x06000242 RID: 578
			public extern ParticleSystemOverlapAction inside
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000243 RID: 579
			// (set) Token: 0x06000244 RID: 580
			public extern ParticleSystemOverlapAction outside
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000245 RID: 581
			// (set) Token: 0x06000246 RID: 582
			public extern ParticleSystemOverlapAction enter
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000247 RID: 583
			// (set) Token: 0x06000248 RID: 584
			public extern ParticleSystemOverlapAction exit
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000249 RID: 585
			// (set) Token: 0x0600024A RID: 586
			public extern ParticleSystemColliderQueryMode colliderQueryMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x0600024B RID: 587
			// (set) Token: 0x0600024C RID: 588
			public extern float radiusScale
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x0600024D RID: 589 RVA: 0x00003DBC File Offset: 0x00001FBC
			[NativeThrows]
			public void AddCollider(Component collider)
			{
				ParticleSystem.TriggerModule.AddCollider_Injected(ref this, Object.MarshalledUnityObject.Marshal<Component>(collider));
			}

			// Token: 0x0600024E RID: 590
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void RemoveCollider(int index);

			// Token: 0x0600024F RID: 591 RVA: 0x00003DD5 File Offset: 0x00001FD5
			public void RemoveCollider(Component collider)
			{
				this.RemoveColliderObject(collider);
			}

			// Token: 0x06000250 RID: 592 RVA: 0x00003DE0 File Offset: 0x00001FE0
			[NativeThrows]
			private void RemoveColliderObject(Component collider)
			{
				ParticleSystem.TriggerModule.RemoveColliderObject_Injected(ref this, Object.MarshalledUnityObject.Marshal<Component>(collider));
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00003DFC File Offset: 0x00001FFC
			[NativeThrows]
			public void SetCollider(int index, Component collider)
			{
				ParticleSystem.TriggerModule.SetCollider_Injected(ref this, index, Object.MarshalledUnityObject.Marshal<Component>(collider));
			}

			// Token: 0x06000252 RID: 594 RVA: 0x00003E18 File Offset: 0x00002018
			[NativeThrows]
			public Component GetCollider(int index)
			{
				return Unmarshal.UnmarshalUnityObject<Component>(ParticleSystem.TriggerModule.GetCollider_Injected(ref this, index));
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x06000253 RID: 595
			[NativeThrows]
			public extern int colliderCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x06000254 RID: 596
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, IntPtr collider);

			// Token: 0x06000255 RID: 597
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveColliderObject_Injected(ref ParticleSystem.TriggerModule _unity_self, IntPtr collider);

			// Token: 0x06000256 RID: 598
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index, IntPtr collider);

			// Token: 0x06000257 RID: 599
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetCollider_Injected(ref ParticleSystem.TriggerModule _unity_self, int index);

			// Token: 0x04000008 RID: 8
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000009 RID: 9
		public struct SubEmittersModule
		{
			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x06000258 RID: 600 RVA: 0x00003E34 File Offset: 0x00002034
			// (set) Token: 0x06000259 RID: 601 RVA: 0x00003E4D File Offset: 0x0000204D
			[Obsolete("birth0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600025A RID: 602 RVA: 0x00003E58 File Offset: 0x00002058
			// (set) Token: 0x0600025B RID: 603 RVA: 0x00003E4D File Offset: 0x0000204D
			[Obsolete("birth1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x0600025C RID: 604 RVA: 0x00003E74 File Offset: 0x00002074
			// (set) Token: 0x0600025D RID: 605 RVA: 0x00003E4D File Offset: 0x0000204D
			[Obsolete("collision0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x0600025E RID: 606 RVA: 0x00003E90 File Offset: 0x00002090
			// (set) Token: 0x0600025F RID: 607 RVA: 0x00003E4D File Offset: 0x0000204D
			[Obsolete("collision1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000260 RID: 608 RVA: 0x00003EAC File Offset: 0x000020AC
			// (set) Token: 0x06000261 RID: 609 RVA: 0x00003E4D File Offset: 0x0000204D
			[Obsolete("death0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death0
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000262 RID: 610 RVA: 0x00003EC8 File Offset: 0x000020C8
			// (set) Token: 0x06000263 RID: 611 RVA: 0x00003E4D File Offset: 0x0000204D
			[Obsolete("death1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death1
			{
				get
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
					return null;
				}
				set
				{
					ParticleSystem.SubEmittersModule.ThrowNotImplemented();
				}
			}

			// Token: 0x06000264 RID: 612 RVA: 0x00003EE1 File Offset: 0x000020E1
			private static void ThrowNotImplemented()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000265 RID: 613 RVA: 0x00003EE9 File Offset: 0x000020E9
			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000266 RID: 614
			// (set) Token: 0x06000267 RID: 615
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000268 RID: 616
			public extern int subEmittersCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x06000269 RID: 617 RVA: 0x00003EF4 File Offset: 0x000020F4
			[NativeThrows]
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability)
			{
				ParticleSystem.SubEmittersModule.AddSubEmitter_Injected(ref this, Object.MarshalledUnityObject.Marshal<ParticleSystem>(subEmitter), type, properties, emitProbability);
			}

			// Token: 0x0600026A RID: 618 RVA: 0x00003F11 File Offset: 0x00002111
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties)
			{
				this.AddSubEmitter(subEmitter, type, properties, 1f);
			}

			// Token: 0x0600026B RID: 619
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void RemoveSubEmitter(int index);

			// Token: 0x0600026C RID: 620 RVA: 0x00003F23 File Offset: 0x00002123
			public void RemoveSubEmitter(ParticleSystem subEmitter)
			{
				this.RemoveSubEmitterObject(subEmitter);
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00003F30 File Offset: 0x00002130
			[NativeThrows]
			private void RemoveSubEmitterObject(ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.RemoveSubEmitterObject_Injected(ref this, Object.MarshalledUnityObject.Marshal<ParticleSystem>(subEmitter));
			}

			// Token: 0x0600026E RID: 622 RVA: 0x00003F4C File Offset: 0x0000214C
			[NativeThrows]
			public void SetSubEmitterSystem(int index, ParticleSystem subEmitter)
			{
				ParticleSystem.SubEmittersModule.SetSubEmitterSystem_Injected(ref this, index, Object.MarshalledUnityObject.Marshal<ParticleSystem>(subEmitter));
			}

			// Token: 0x0600026F RID: 623
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void SetSubEmitterType(int index, ParticleSystemSubEmitterType type);

			// Token: 0x06000270 RID: 624
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void SetSubEmitterProperties(int index, ParticleSystemSubEmitterProperties properties);

			// Token: 0x06000271 RID: 625
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void SetSubEmitterEmitProbability(int index, float emitProbability);

			// Token: 0x06000272 RID: 626 RVA: 0x00003F68 File Offset: 0x00002168
			[NativeThrows]
			public ParticleSystem GetSubEmitterSystem(int index)
			{
				return Unmarshal.UnmarshalUnityObject<ParticleSystem>(ParticleSystem.SubEmittersModule.GetSubEmitterSystem_Injected(ref this, index));
			}

			// Token: 0x06000273 RID: 627
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ParticleSystemSubEmitterType GetSubEmitterType(int index);

			// Token: 0x06000274 RID: 628
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ParticleSystemSubEmitterProperties GetSubEmitterProperties(int index);

			// Token: 0x06000275 RID: 629
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern float GetSubEmitterEmitProbability(int index);

			// Token: 0x06000276 RID: 630
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSubEmitter_Injected(ref ParticleSystem.SubEmittersModule _unity_self, IntPtr subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability);

			// Token: 0x06000277 RID: 631
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitterObject_Injected(ref ParticleSystem.SubEmittersModule _unity_self, IntPtr subEmitter);

			// Token: 0x06000278 RID: 632
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index, IntPtr subEmitter);

			// Token: 0x06000279 RID: 633
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x04000009 RID: 9
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200000A RID: 10
		public struct TextureSheetAnimationModule
		{
			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600027A RID: 634 RVA: 0x00003F84 File Offset: 0x00002184
			// (set) Token: 0x0600027B RID: 635 RVA: 0x00003FAC File Offset: 0x000021AC
			[Obsolete("flipU property is deprecated. Use ParticleSystemRenderer.flip.x instead.", false)]
			public float flipU
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.x;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.x = value;
					component.flip = flip;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600027C RID: 636 RVA: 0x00003FE0 File Offset: 0x000021E0
			// (set) Token: 0x0600027D RID: 637 RVA: 0x00004008 File Offset: 0x00002208
			[Obsolete("flipV property is deprecated. Use ParticleSystemRenderer.flip.y instead.", false)]
			public float flipV
			{
				get
				{
					return this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.y;
				}
				set
				{
					ParticleSystemRenderer component = this.m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.y = value;
					component.flip = flip;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600027F RID: 639 RVA: 0x0000404C File Offset: 0x0000224C
			// (set) Token: 0x0600027E RID: 638 RVA: 0x00004039 File Offset: 0x00002239
			[Obsolete("useRandomRow property is deprecated. Use rowMode instead.", false)]
			public bool useRandomRow
			{
				get
				{
					return this.rowMode == ParticleSystemAnimationRowMode.Random;
				}
				set
				{
					this.rowMode = (value ? ParticleSystemAnimationRowMode.Random : ParticleSystemAnimationRowMode.Custom);
				}
			}

			// Token: 0x06000280 RID: 640 RVA: 0x00004067 File Offset: 0x00002267
			internal TextureSheetAnimationModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x06000281 RID: 641
			// (set) Token: 0x06000282 RID: 642
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000283 RID: 643
			// (set) Token: 0x06000284 RID: 644
			public extern ParticleSystemAnimationMode mode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000285 RID: 645
			// (set) Token: 0x06000286 RID: 646
			public extern ParticleSystemAnimationTimeMode timeMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x06000287 RID: 647
			// (set) Token: 0x06000288 RID: 648
			public extern float fps
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000289 RID: 649
			// (set) Token: 0x0600028A RID: 650
			public extern int numTilesX
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x0600028B RID: 651
			// (set) Token: 0x0600028C RID: 652
			public extern int numTilesY
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x0600028D RID: 653
			// (set) Token: 0x0600028E RID: 654
			public extern ParticleSystemAnimationType animation
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600028F RID: 655
			// (set) Token: 0x06000290 RID: 656
			public extern ParticleSystemAnimationRowMode rowMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x06000291 RID: 657 RVA: 0x00004071 File Offset: 0x00002271
			// (set) Token: 0x06000292 RID: 658 RVA: 0x0000407E File Offset: 0x0000227E
			public ParticleSystem.MinMaxCurve frameOverTime
			{
				get
				{
					return this.frameOverTimeBlittable;
				}
				set
				{
					this.frameOverTimeBlittable = value;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000293 RID: 659 RVA: 0x00004090 File Offset: 0x00002290
			// (set) Token: 0x06000294 RID: 660 RVA: 0x000040A8 File Offset: 0x000022A8
			[NativeName("FrameOverTime")]
			private ParticleSystem.MinMaxCurveBlittable frameOverTimeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.TextureSheetAnimationModule.get_frameOverTimeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_frameOverTimeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x06000295 RID: 661
			// (set) Token: 0x06000296 RID: 662
			public extern float frameOverTimeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x06000297 RID: 663 RVA: 0x000040BD File Offset: 0x000022BD
			// (set) Token: 0x06000298 RID: 664 RVA: 0x000040CA File Offset: 0x000022CA
			public ParticleSystem.MinMaxCurve startFrame
			{
				get
				{
					return this.startFrameBlittable;
				}
				set
				{
					this.startFrameBlittable = value;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x06000299 RID: 665 RVA: 0x000040DC File Offset: 0x000022DC
			// (set) Token: 0x0600029A RID: 666 RVA: 0x000040F4 File Offset: 0x000022F4
			[NativeName("StartFrame")]
			private ParticleSystem.MinMaxCurveBlittable startFrameBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.TextureSheetAnimationModule.get_startFrameBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_startFrameBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x0600029B RID: 667
			// (set) Token: 0x0600029C RID: 668
			public extern float startFrameMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x0600029D RID: 669
			// (set) Token: 0x0600029E RID: 670
			public extern int cycleCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x0600029F RID: 671
			// (set) Token: 0x060002A0 RID: 672
			public extern int rowIndex
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x060002A1 RID: 673
			// (set) Token: 0x060002A2 RID: 674
			public extern UVChannelFlags uvChannelMask
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x060002A3 RID: 675
			public extern int spriteCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000410C File Offset: 0x0000230C
			// (set) Token: 0x060002A5 RID: 677 RVA: 0x00004124 File Offset: 0x00002324
			public Vector2 speedRange
			{
				get
				{
					Vector2 vector;
					ParticleSystem.TextureSheetAnimationModule.get_speedRange_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TextureSheetAnimationModule.set_speedRange_Injected(ref this, ref value);
				}
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x0000413C File Offset: 0x0000233C
			[NativeThrows]
			public void AddSprite(Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.AddSprite_Injected(ref this, Object.MarshalledUnityObject.Marshal<Sprite>(sprite));
			}

			// Token: 0x060002A7 RID: 679
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void RemoveSprite(int index);

			// Token: 0x060002A8 RID: 680 RVA: 0x00004158 File Offset: 0x00002358
			[NativeThrows]
			public void SetSprite(int index, Sprite sprite)
			{
				ParticleSystem.TextureSheetAnimationModule.SetSprite_Injected(ref this, index, Object.MarshalledUnityObject.Marshal<Sprite>(sprite));
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x00004174 File Offset: 0x00002374
			[NativeThrows]
			public Sprite GetSprite(int index)
			{
				return Unmarshal.UnmarshalUnityObject<Sprite>(ParticleSystem.TextureSheetAnimationModule.GetSprite_Injected(ref this, index));
			}

			// Token: 0x060002AA RID: 682
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_frameOverTimeBlittable_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060002AB RID: 683
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_frameOverTimeBlittable_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060002AC RID: 684
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_startFrameBlittable_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060002AD RID: 685
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_startFrameBlittable_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060002AE RID: 686
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, out Vector2 ret);

			// Token: 0x060002AF RID: 687
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedRange_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, [In] ref Vector2 value);

			// Token: 0x060002B0 RID: 688
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, IntPtr sprite);

			// Token: 0x060002B1 RID: 689
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index, IntPtr sprite);

			// Token: 0x060002B2 RID: 690
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetSprite_Injected(ref ParticleSystem.TextureSheetAnimationModule _unity_self, int index);

			// Token: 0x0400000A RID: 10
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200000B RID: 11
		[RequiredByNativeCode("particleSystemParticle", Optional = true)]
		public struct Particle
		{
			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x00004190 File Offset: 0x00002390
			// (set) Token: 0x060002B4 RID: 692 RVA: 0x000041A8 File Offset: 0x000023A8
			[Obsolete("Please use Particle.remainingLifetime instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/Particle.remainingLifetime", false)]
			public float lifetime
			{
				get
				{
					return this.remainingLifetime;
				}
				set
				{
					this.remainingLifetime = value;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x060002B5 RID: 693 RVA: 0x000041B4 File Offset: 0x000023B4
			// (set) Token: 0x060002B6 RID: 694 RVA: 0x000041D7 File Offset: 0x000023D7
			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.", false)]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(this.m_RandomSeed), 0);
				}
				set
				{
					this.m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x000041EC File Offset: 0x000023EC
			// (set) Token: 0x060002B8 RID: 696 RVA: 0x00004204 File Offset: 0x00002404
			[Obsolete("size property is deprecated. Use startSize or GetCurrentSize() instead.", false)]
			public float size
			{
				get
				{
					return this.startSize;
				}
				set
				{
					this.startSize = value;
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x060002B9 RID: 697 RVA: 0x00004210 File Offset: 0x00002410
			// (set) Token: 0x060002BA RID: 698 RVA: 0x00004228 File Offset: 0x00002428
			[Obsolete("color property is deprecated. Use startColor or GetCurrentColor() instead.", false)]
			public Color32 color
			{
				get
				{
					return this.startColor;
				}
				set
				{
					this.startColor = value;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x060002BB RID: 699 RVA: 0x00004234 File Offset: 0x00002434
			// (set) Token: 0x060002BC RID: 700 RVA: 0x0000424C File Offset: 0x0000244C
			public Vector3 position
			{
				get
				{
					return this.m_Position;
				}
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x060002BD RID: 701 RVA: 0x00004258 File Offset: 0x00002458
			// (set) Token: 0x060002BE RID: 702 RVA: 0x00004270 File Offset: 0x00002470
			public Vector3 velocity
			{
				get
				{
					return this.m_Velocity;
				}
				set
				{
					this.m_Velocity = value;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x060002BF RID: 703 RVA: 0x0000427C File Offset: 0x0000247C
			public Vector3 animatedVelocity
			{
				get
				{
					return this.m_AnimatedVelocity;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x060002C0 RID: 704 RVA: 0x00004294 File Offset: 0x00002494
			public Vector3 totalVelocity
			{
				get
				{
					return this.m_Velocity + this.m_AnimatedVelocity;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x060002C1 RID: 705 RVA: 0x000042B8 File Offset: 0x000024B8
			// (set) Token: 0x060002C2 RID: 706 RVA: 0x000042D0 File Offset: 0x000024D0
			public float remainingLifetime
			{
				get
				{
					return this.m_Lifetime;
				}
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x060002C3 RID: 707 RVA: 0x000042DC File Offset: 0x000024DC
			// (set) Token: 0x060002C4 RID: 708 RVA: 0x000042F4 File Offset: 0x000024F4
			public float startLifetime
			{
				get
				{
					return this.m_StartLifetime;
				}
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x060002C5 RID: 709 RVA: 0x00004300 File Offset: 0x00002500
			// (set) Token: 0x060002C6 RID: 710 RVA: 0x00004318 File Offset: 0x00002518
			public Color32 startColor
			{
				get
				{
					return this.m_StartColor;
				}
				set
				{
					this.m_StartColor = value;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x00004324 File Offset: 0x00002524
			// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000433C File Offset: 0x0000253C
			public uint randomSeed
			{
				get
				{
					return this.m_RandomSeed;
				}
				set
				{
					this.m_RandomSeed = value;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x00004348 File Offset: 0x00002548
			// (set) Token: 0x060002CA RID: 714 RVA: 0x00004360 File Offset: 0x00002560
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_AxisOfRotation;
				}
				set
				{
					this.m_AxisOfRotation = value;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x060002CB RID: 715 RVA: 0x0000436C File Offset: 0x0000256C
			// (set) Token: 0x060002CC RID: 716 RVA: 0x00004389 File Offset: 0x00002589
			public float startSize
			{
				get
				{
					return this.m_StartSize.x;
				}
				set
				{
					this.m_StartSize = new Vector3(value, value, value);
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x060002CD RID: 717 RVA: 0x0000439C File Offset: 0x0000259C
			// (set) Token: 0x060002CE RID: 718 RVA: 0x000043B4 File Offset: 0x000025B4
			public Vector3 startSize3D
			{
				get
				{
					return this.m_StartSize;
				}
				set
				{
					this.m_StartSize = value;
					this.m_Flags |= 1U;
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x060002CF RID: 719 RVA: 0x000043CC File Offset: 0x000025CC
			// (set) Token: 0x060002D0 RID: 720 RVA: 0x000043EF File Offset: 0x000025EF
			public float rotation
			{
				get
				{
					return this.m_Rotation.z * 57.29578f;
				}
				set
				{
					this.m_Rotation = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x060002D1 RID: 721 RVA: 0x00004410 File Offset: 0x00002610
			// (set) Token: 0x060002D2 RID: 722 RVA: 0x00004432 File Offset: 0x00002632
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Rotation * 57.29578f;
				}
				set
				{
					this.m_Rotation = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x060002D3 RID: 723 RVA: 0x00004454 File Offset: 0x00002654
			// (set) Token: 0x060002D4 RID: 724 RVA: 0x00004477 File Offset: 0x00002677
			public float angularVelocity
			{
				get
				{
					return this.m_AngularVelocity.z * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = new Vector3(0f, 0f, value * 0.017453292f);
				}
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x060002D5 RID: 725 RVA: 0x00004498 File Offset: 0x00002698
			// (set) Token: 0x060002D6 RID: 726 RVA: 0x000044BA File Offset: 0x000026BA
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_AngularVelocity * 57.29578f;
				}
				set
				{
					this.m_AngularVelocity = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x000044DC File Offset: 0x000026DC
			public float GetCurrentSize(ParticleSystem system)
			{
				return system.GetParticleCurrentSize(ref this);
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x000044F8 File Offset: 0x000026F8
			public Vector3 GetCurrentSize3D(ParticleSystem system)
			{
				return system.GetParticleCurrentSize3D(ref this);
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x00004514 File Offset: 0x00002714
			public Color32 GetCurrentColor(ParticleSystem system)
			{
				return system.GetParticleCurrentColor(ref this);
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000452D File Offset: 0x0000272D
			public void SetMeshIndex(int index)
			{
				this.m_MeshIndex = index;
				this.m_Flags |= 4U;
			}

			// Token: 0x060002DB RID: 731 RVA: 0x00004548 File Offset: 0x00002748
			public int GetMeshIndex(ParticleSystem system)
			{
				return system.GetParticleMeshIndex(ref this);
			}

			// Token: 0x0400000B RID: 11
			private Vector3 m_Position;

			// Token: 0x0400000C RID: 12
			private Vector3 m_Velocity;

			// Token: 0x0400000D RID: 13
			private Vector3 m_AnimatedVelocity;

			// Token: 0x0400000E RID: 14
			private Vector3 m_InitialVelocity;

			// Token: 0x0400000F RID: 15
			private Vector3 m_AxisOfRotation;

			// Token: 0x04000010 RID: 16
			private Vector3 m_Rotation;

			// Token: 0x04000011 RID: 17
			private Vector3 m_AngularVelocity;

			// Token: 0x04000012 RID: 18
			private Vector3 m_StartSize;

			// Token: 0x04000013 RID: 19
			private Color32 m_StartColor;

			// Token: 0x04000014 RID: 20
			private uint m_RandomSeed;

			// Token: 0x04000015 RID: 21
			private uint m_ParentRandomSeed;

			// Token: 0x04000016 RID: 22
			private float m_Lifetime;

			// Token: 0x04000017 RID: 23
			private float m_StartLifetime;

			// Token: 0x04000018 RID: 24
			private int m_MeshIndex;

			// Token: 0x04000019 RID: 25
			private float m_EmitAccumulator0;

			// Token: 0x0400001A RID: 26
			private float m_EmitAccumulator1;

			// Token: 0x0400001B RID: 27
			private uint m_Flags;

			// Token: 0x0200000C RID: 12
			[Flags]
			private enum Flags
			{
				// Token: 0x0400001D RID: 29
				Size3D = 1,
				// Token: 0x0400001E RID: 30
				Rotation3D = 2,
				// Token: 0x0400001F RID: 31
				MeshIndex = 4
			}
		}

		// Token: 0x0200000D RID: 13
		[NativeType(CodegenOptions.Custom, "MonoBurst", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct Burst
		{
			// Token: 0x060002DC RID: 732 RVA: 0x00004564 File Offset: 0x00002764
			public Burst(float _time, short _count)
			{
				this.m_Time = _time;
				ParticleSystem.MinMaxCurve minMaxCurve = (float)_count;
				this.m_Count = ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in minMaxCurve);
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002DD RID: 733 RVA: 0x000045AC File Offset: 0x000027AC
			public Burst(float _time, short _minCount, short _maxCount)
			{
				this.m_Time = _time;
				ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_Count = ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in minMaxCurve);
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002DE RID: 734 RVA: 0x000045F8 File Offset: 0x000027F8
			public Burst(float _time, short _minCount, short _maxCount, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				ParticleSystem.MinMaxCurve minMaxCurve = new ParticleSystem.MinMaxCurve((float)_minCount, (float)_maxCount);
				this.m_Count = ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in minMaxCurve);
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002DF RID: 735 RVA: 0x00004641 File Offset: 0x00002841
			public Burst(float _time, ParticleSystem.MinMaxCurve _count)
			{
				this.m_Time = _time;
				this.m_Count = ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in _count);
				this.m_RepeatCount = 0;
				this.m_RepeatInterval = 0f;
				this.m_InvProbability = 0f;
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x00004675 File Offset: 0x00002875
			public Burst(float _time, ParticleSystem.MinMaxCurve _count, int _cycleCount, float _repeatInterval)
			{
				this.m_Time = _time;
				this.m_Count = ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in _count);
				this.m_RepeatCount = _cycleCount - 1;
				this.m_RepeatInterval = _repeatInterval;
				this.m_InvProbability = 0f;
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x000046A8 File Offset: 0x000028A8
			// (set) Token: 0x060002E2 RID: 738 RVA: 0x000046C0 File Offset: 0x000028C0
			public float time
			{
				get
				{
					return this.m_Time;
				}
				set
				{
					this.m_Time = value;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x000046CC File Offset: 0x000028CC
			// (set) Token: 0x060002E4 RID: 740 RVA: 0x000046E9 File Offset: 0x000028E9
			public ParticleSystem.MinMaxCurve count
			{
				get
				{
					return ParticleSystem.MinMaxCurveBlittable.ToMinMaxCurve(in this.m_Count);
				}
				set
				{
					this.m_Count = ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in value);
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x000046FC File Offset: 0x000028FC
			// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000471A File Offset: 0x0000291A
			public short minCount
			{
				get
				{
					return (short)this.m_Count.m_ConstantMin;
				}
				set
				{
					this.m_Count.m_ConstantMin = (float)value;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000472C File Offset: 0x0000292C
			// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000474A File Offset: 0x0000294A
			public short maxCount
			{
				get
				{
					return (short)this.m_Count.m_ConstantMax;
				}
				set
				{
					this.m_Count.m_ConstantMax = (float)value;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000475C File Offset: 0x0000295C
			// (set) Token: 0x060002EA RID: 746 RVA: 0x00004778 File Offset: 0x00002978
			public int cycleCount
			{
				get
				{
					return this.m_RepeatCount + 1;
				}
				set
				{
					bool flag = value < 0;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("cycleCount", "cycleCount must be at least 0: " + value.ToString());
					}
					this.m_RepeatCount = value - 1;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x060002EB RID: 747 RVA: 0x000047B4 File Offset: 0x000029B4
			// (set) Token: 0x060002EC RID: 748 RVA: 0x000047CC File Offset: 0x000029CC
			public float repeatInterval
			{
				get
				{
					return this.m_RepeatInterval;
				}
				set
				{
					bool flag = value <= 0f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("repeatInterval", "repeatInterval must be greater than 0.0f: " + value.ToString());
					}
					this.m_RepeatInterval = value;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000480C File Offset: 0x00002A0C
			// (set) Token: 0x060002EE RID: 750 RVA: 0x0000482C File Offset: 0x00002A2C
			public float probability
			{
				get
				{
					return 1f - this.m_InvProbability;
				}
				set
				{
					bool flag = value < 0f || value > 1f;
					if (flag)
					{
						throw new ArgumentOutOfRangeException("probability", "probability must be between 0.0f and 1.0f: " + value.ToString());
					}
					this.m_InvProbability = 1f - value;
				}
			}

			// Token: 0x04000020 RID: 32
			private float m_Time;

			// Token: 0x04000021 RID: 33
			private ParticleSystem.MinMaxCurveBlittable m_Count;

			// Token: 0x04000022 RID: 34
			private int m_RepeatCount;

			// Token: 0x04000023 RID: 35
			private float m_RepeatInterval;

			// Token: 0x04000024 RID: 36
			private float m_InvProbability;
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public struct MinMaxCurve
		{
			// Token: 0x060002EF RID: 751 RVA: 0x0000487A File Offset: 0x00002A7A
			public MinMaxCurve(float constant)
			{
				this.m_Mode = ParticleSystemCurveMode.Constant;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = constant;
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x000048AF File Offset: 0x00002AAF
			public MinMaxCurve(float multiplier, AnimationCurve curve)
			{
				this.m_Mode = ParticleSystemCurveMode.Curve;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = null;
				this.m_CurveMax = curve;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x000048E4 File Offset: 0x00002AE4
			public MinMaxCurve(float multiplier, AnimationCurve min, AnimationCurve max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoCurves;
				this.m_CurveMultiplier = multiplier;
				this.m_CurveMin = min;
				this.m_CurveMax = max;
				this.m_ConstantMin = 0f;
				this.m_ConstantMax = 0f;
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x00004919 File Offset: 0x00002B19
			public MinMaxCurve(float min, float max)
			{
				this.m_Mode = ParticleSystemCurveMode.TwoConstants;
				this.m_CurveMultiplier = 0f;
				this.m_CurveMin = null;
				this.m_CurveMax = null;
				this.m_ConstantMin = min;
				this.m_ConstantMax = max;
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000494C File Offset: 0x00002B4C
			// (set) Token: 0x060002F4 RID: 756 RVA: 0x00004964 File Offset: 0x00002B64
			public ParticleSystemCurveMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060002F5 RID: 757 RVA: 0x00004970 File Offset: 0x00002B70
			// (set) Token: 0x060002F6 RID: 758 RVA: 0x00004988 File Offset: 0x00002B88
			public float curveMultiplier
			{
				get
				{
					return this.m_CurveMultiplier;
				}
				set
				{
					this.m_CurveMultiplier = value;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060002F7 RID: 759 RVA: 0x00004994 File Offset: 0x00002B94
			// (set) Token: 0x060002F8 RID: 760 RVA: 0x000049AC File Offset: 0x00002BAC
			public AnimationCurve curveMax
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x000049B8 File Offset: 0x00002BB8
			// (set) Token: 0x060002FA RID: 762 RVA: 0x000049D0 File Offset: 0x00002BD0
			public AnimationCurve curveMin
			{
				get
				{
					return this.m_CurveMin;
				}
				set
				{
					this.m_CurveMin = value;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x060002FB RID: 763 RVA: 0x000049DC File Offset: 0x00002BDC
			// (set) Token: 0x060002FC RID: 764 RVA: 0x000049F4 File Offset: 0x00002BF4
			public float constantMax
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060002FD RID: 765 RVA: 0x00004A00 File Offset: 0x00002C00
			// (set) Token: 0x060002FE RID: 766 RVA: 0x00004A18 File Offset: 0x00002C18
			public float constantMin
			{
				get
				{
					return this.m_ConstantMin;
				}
				set
				{
					this.m_ConstantMin = value;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060002FF RID: 767 RVA: 0x00004A24 File Offset: 0x00002C24
			// (set) Token: 0x06000300 RID: 768 RVA: 0x000049F4 File Offset: 0x00002BF4
			public float constant
			{
				get
				{
					return this.m_ConstantMax;
				}
				set
				{
					this.m_ConstantMax = value;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000301 RID: 769 RVA: 0x00004A3C File Offset: 0x00002C3C
			// (set) Token: 0x06000302 RID: 770 RVA: 0x000049AC File Offset: 0x00002BAC
			public AnimationCurve curve
			{
				get
				{
					return this.m_CurveMax;
				}
				set
				{
					this.m_CurveMax = value;
				}
			}

			// Token: 0x06000303 RID: 771 RVA: 0x00004A54 File Offset: 0x00002C54
			public float Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			// Token: 0x06000304 RID: 772 RVA: 0x00004A74 File Offset: 0x00002C74
			public float Evaluate(float time, float lerpFactor)
			{
				switch (this.mode)
				{
				case ParticleSystemCurveMode.Constant:
					return this.m_ConstantMax;
				case ParticleSystemCurveMode.TwoCurves:
					return Mathf.Lerp(this.m_CurveMin.Evaluate(time), this.m_CurveMax.Evaluate(time), lerpFactor) * this.m_CurveMultiplier;
				case ParticleSystemCurveMode.TwoConstants:
					return Mathf.Lerp(this.m_ConstantMin, this.m_ConstantMax, lerpFactor);
				}
				return this.m_CurveMax.Evaluate(time) * this.m_CurveMultiplier;
			}

			// Token: 0x06000305 RID: 773 RVA: 0x00004B00 File Offset: 0x00002D00
			public static implicit operator ParticleSystem.MinMaxCurve(float constant)
			{
				return new ParticleSystem.MinMaxCurve(constant);
			}

			// Token: 0x04000025 RID: 37
			[SerializeField]
			internal ParticleSystemCurveMode m_Mode;

			// Token: 0x04000026 RID: 38
			[SerializeField]
			internal float m_CurveMultiplier;

			// Token: 0x04000027 RID: 39
			[SerializeField]
			internal AnimationCurve m_CurveMin;

			// Token: 0x04000028 RID: 40
			[SerializeField]
			internal AnimationCurve m_CurveMax;

			// Token: 0x04000029 RID: 41
			[SerializeField]
			internal float m_ConstantMin;

			// Token: 0x0400002A RID: 42
			[SerializeField]
			internal float m_ConstantMax;
		}

		// Token: 0x0200000F RID: 15
		[RequiredByNativeCode]
		[NativeType(CodegenOptions.Custom, "MonoMinMaxCurve", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		internal struct MinMaxCurveBlittable
		{
			// Token: 0x06000306 RID: 774 RVA: 0x00004B18 File Offset: 0x00002D18
			public static implicit operator ParticleSystem.MinMaxCurve(ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable)
			{
				return ParticleSystem.MinMaxCurveBlittable.ToMinMaxCurve(in minMaxCurveBlittable);
			}

			// Token: 0x06000307 RID: 775 RVA: 0x00004B21 File Offset: 0x00002D21
			public static implicit operator ParticleSystem.MinMaxCurveBlittable(ParticleSystem.MinMaxCurve minMaxCurve)
			{
				return ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in minMaxCurve);
			}

			// Token: 0x06000308 RID: 776 RVA: 0x00004B2C File Offset: 0x00002D2C
			internal static ParticleSystem.MinMaxCurveBlittable FromMixMaxCurve(in ParticleSystem.MinMaxCurve minMaxCurve)
			{
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable = new ParticleSystem.MinMaxCurveBlittable
				{
					m_Mode = minMaxCurve.m_Mode,
					m_CurveMultiplier = minMaxCurve.m_CurveMultiplier,
					m_ConstantMin = minMaxCurve.m_ConstantMin,
					m_ConstantMax = minMaxCurve.m_ConstantMax
				};
				bool flag = minMaxCurve.m_CurveMin != null;
				if (flag)
				{
					minMaxCurveBlittable.m_CurveMin = minMaxCurve.m_CurveMin.m_Ptr;
				}
				bool flag2 = minMaxCurve.m_CurveMax != null;
				if (flag2)
				{
					minMaxCurveBlittable.m_CurveMax = minMaxCurve.m_CurveMax.m_Ptr;
				}
				return minMaxCurveBlittable;
			}

			// Token: 0x06000309 RID: 777 RVA: 0x00004BC0 File Offset: 0x00002DC0
			internal static ParticleSystem.MinMaxCurve ToMinMaxCurve(in ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable)
			{
				ParticleSystem.MinMaxCurve minMaxCurve = default(ParticleSystem.MinMaxCurve);
				minMaxCurve.m_Mode = minMaxCurveBlittable.m_Mode;
				minMaxCurve.m_CurveMultiplier = minMaxCurveBlittable.m_CurveMultiplier;
				bool flag = minMaxCurveBlittable.m_CurveMin != IntPtr.Zero;
				if (flag)
				{
					minMaxCurve.m_CurveMin = new AnimationCurve(minMaxCurveBlittable.m_CurveMin, false);
				}
				bool flag2 = minMaxCurveBlittable.m_CurveMax != IntPtr.Zero;
				if (flag2)
				{
					minMaxCurve.m_CurveMax = new AnimationCurve(minMaxCurveBlittable.m_CurveMax, false);
				}
				minMaxCurve.m_ConstantMin = minMaxCurveBlittable.m_ConstantMin;
				minMaxCurve.m_ConstantMax = minMaxCurveBlittable.m_ConstantMax;
				return minMaxCurve;
			}

			// Token: 0x0400002B RID: 43
			private ParticleSystemCurveMode m_Mode;

			// Token: 0x0400002C RID: 44
			private float m_CurveMultiplier;

			// Token: 0x0400002D RID: 45
			private IntPtr m_CurveMin;

			// Token: 0x0400002E RID: 46
			private IntPtr m_CurveMax;

			// Token: 0x0400002F RID: 47
			internal float m_ConstantMin;

			// Token: 0x04000030 RID: 48
			internal float m_ConstantMax;
		}

		// Token: 0x02000010 RID: 16
		[Serializable]
		public struct MinMaxGradient
		{
			// Token: 0x0600030A RID: 778 RVA: 0x00004C5D File Offset: 0x00002E5D
			public MinMaxGradient(Color color)
			{
				this.m_Mode = ParticleSystemGradientMode.Color;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = color;
			}

			// Token: 0x0600030B RID: 779 RVA: 0x00004C87 File Offset: 0x00002E87
			public MinMaxGradient(Gradient gradient)
			{
				this.m_Mode = ParticleSystemGradientMode.Gradient;
				this.m_GradientMin = null;
				this.m_GradientMax = gradient;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x0600030C RID: 780 RVA: 0x00004CB5 File Offset: 0x00002EB5
			public MinMaxGradient(Color min, Color max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoColors;
				this.m_GradientMin = null;
				this.m_GradientMax = null;
				this.m_ColorMin = min;
				this.m_ColorMax = max;
			}

			// Token: 0x0600030D RID: 781 RVA: 0x00004CDB File Offset: 0x00002EDB
			public MinMaxGradient(Gradient min, Gradient max)
			{
				this.m_Mode = ParticleSystemGradientMode.TwoGradients;
				this.m_GradientMin = min;
				this.m_GradientMax = max;
				this.m_ColorMin = Color.black;
				this.m_ColorMax = Color.black;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x0600030E RID: 782 RVA: 0x00004D0C File Offset: 0x00002F0C
			// (set) Token: 0x0600030F RID: 783 RVA: 0x00004D24 File Offset: 0x00002F24
			public ParticleSystemGradientMode mode
			{
				get
				{
					return this.m_Mode;
				}
				set
				{
					this.m_Mode = value;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000310 RID: 784 RVA: 0x00004D30 File Offset: 0x00002F30
			// (set) Token: 0x06000311 RID: 785 RVA: 0x00004D48 File Offset: 0x00002F48
			public Gradient gradientMax
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000312 RID: 786 RVA: 0x00004D54 File Offset: 0x00002F54
			// (set) Token: 0x06000313 RID: 787 RVA: 0x00004D6C File Offset: 0x00002F6C
			public Gradient gradientMin
			{
				get
				{
					return this.m_GradientMin;
				}
				set
				{
					this.m_GradientMin = value;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000314 RID: 788 RVA: 0x00004D78 File Offset: 0x00002F78
			// (set) Token: 0x06000315 RID: 789 RVA: 0x00004D90 File Offset: 0x00002F90
			public Color colorMax
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000316 RID: 790 RVA: 0x00004D9C File Offset: 0x00002F9C
			// (set) Token: 0x06000317 RID: 791 RVA: 0x00004DB4 File Offset: 0x00002FB4
			public Color colorMin
			{
				get
				{
					return this.m_ColorMin;
				}
				set
				{
					this.m_ColorMin = value;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000318 RID: 792 RVA: 0x00004DC0 File Offset: 0x00002FC0
			// (set) Token: 0x06000319 RID: 793 RVA: 0x00004D90 File Offset: 0x00002F90
			public Color color
			{
				get
				{
					return this.m_ColorMax;
				}
				set
				{
					this.m_ColorMax = value;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600031A RID: 794 RVA: 0x00004DD8 File Offset: 0x00002FD8
			// (set) Token: 0x0600031B RID: 795 RVA: 0x00004D48 File Offset: 0x00002F48
			public Gradient gradient
			{
				get
				{
					return this.m_GradientMax;
				}
				set
				{
					this.m_GradientMax = value;
				}
			}

			// Token: 0x0600031C RID: 796 RVA: 0x00004DF0 File Offset: 0x00002FF0
			public Color Evaluate(float time)
			{
				return this.Evaluate(time, 1f);
			}

			// Token: 0x0600031D RID: 797 RVA: 0x00004E10 File Offset: 0x00003010
			public Color Evaluate(float time, float lerpFactor)
			{
				switch (this.m_Mode)
				{
				case ParticleSystemGradientMode.Color:
					return this.m_ColorMax;
				case ParticleSystemGradientMode.TwoColors:
					return Color.Lerp(this.m_ColorMin, this.m_ColorMax, lerpFactor);
				case ParticleSystemGradientMode.TwoGradients:
					return Color.Lerp(this.m_GradientMin.Evaluate(time), this.m_GradientMax.Evaluate(time), lerpFactor);
				case ParticleSystemGradientMode.RandomColor:
					return this.m_GradientMax.Evaluate(lerpFactor);
				}
				return this.m_GradientMax.Evaluate(time);
			}

			// Token: 0x0600031E RID: 798 RVA: 0x00004EA4 File Offset: 0x000030A4
			public static implicit operator ParticleSystem.MinMaxGradient(Color color)
			{
				return new ParticleSystem.MinMaxGradient(color);
			}

			// Token: 0x0600031F RID: 799 RVA: 0x00004EBC File Offset: 0x000030BC
			public static implicit operator ParticleSystem.MinMaxGradient(Gradient gradient)
			{
				return new ParticleSystem.MinMaxGradient(gradient);
			}

			// Token: 0x04000031 RID: 49
			[SerializeField]
			internal ParticleSystemGradientMode m_Mode;

			// Token: 0x04000032 RID: 50
			[SerializeField]
			internal Gradient m_GradientMin;

			// Token: 0x04000033 RID: 51
			[SerializeField]
			internal Gradient m_GradientMax;

			// Token: 0x04000034 RID: 52
			[SerializeField]
			internal Color m_ColorMin;

			// Token: 0x04000035 RID: 53
			[SerializeField]
			internal Color m_ColorMax;
		}

		// Token: 0x02000011 RID: 17
		[NativeType(CodegenOptions.Custom, "MonoMinMaxGradient", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[RequiredByNativeCode]
		[Serializable]
		internal struct MinMaxGradientBlittable
		{
			// Token: 0x06000320 RID: 800 RVA: 0x00004ED4 File Offset: 0x000030D4
			public static implicit operator ParticleSystem.MinMaxGradient(ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable)
			{
				return ParticleSystem.MinMaxGradientBlittable.ToMinMaxGradient(in minMaxGradientBlittable);
			}

			// Token: 0x06000321 RID: 801 RVA: 0x00004EDD File Offset: 0x000030DD
			public static implicit operator ParticleSystem.MinMaxGradientBlittable(ParticleSystem.MinMaxGradient minMaxGradient)
			{
				return ParticleSystem.MinMaxGradientBlittable.FromMixMaxGradient(in minMaxGradient);
			}

			// Token: 0x06000322 RID: 802 RVA: 0x00004EE8 File Offset: 0x000030E8
			internal static ParticleSystem.MinMaxGradientBlittable FromMixMaxGradient(in ParticleSystem.MinMaxGradient minMaxGradient)
			{
				ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable = new ParticleSystem.MinMaxGradientBlittable
				{
					m_Mode = minMaxGradient.m_Mode,
					m_ColorMin = minMaxGradient.m_ColorMin,
					m_ColorMax = minMaxGradient.m_ColorMax
				};
				bool flag = minMaxGradient.m_GradientMin != null;
				if (flag)
				{
					minMaxGradientBlittable.m_GradientMin = minMaxGradient.m_GradientMin.m_Ptr;
				}
				bool flag2 = minMaxGradient.m_GradientMax != null;
				if (flag2)
				{
					minMaxGradientBlittable.m_GradientMax = minMaxGradient.m_GradientMax.m_Ptr;
				}
				return minMaxGradientBlittable;
			}

			// Token: 0x06000323 RID: 803 RVA: 0x00004F6C File Offset: 0x0000316C
			internal static ParticleSystem.MinMaxGradient ToMinMaxGradient(in ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable)
			{
				ParticleSystem.MinMaxGradient minMaxGradient = default(ParticleSystem.MinMaxGradient);
				minMaxGradient.m_Mode = minMaxGradientBlittable.m_Mode;
				bool flag = minMaxGradientBlittable.m_GradientMin != IntPtr.Zero;
				if (flag)
				{
					minMaxGradient.m_GradientMin = new Gradient(minMaxGradientBlittable.m_GradientMin);
				}
				bool flag2 = minMaxGradientBlittable.m_GradientMax != IntPtr.Zero;
				if (flag2)
				{
					minMaxGradient.m_GradientMax = new Gradient(minMaxGradientBlittable.m_GradientMax);
				}
				minMaxGradient.m_ColorMin = minMaxGradientBlittable.m_ColorMin;
				minMaxGradient.m_ColorMax = minMaxGradientBlittable.m_ColorMax;
				return minMaxGradient;
			}

			// Token: 0x04000036 RID: 54
			private ParticleSystemGradientMode m_Mode;

			// Token: 0x04000037 RID: 55
			private IntPtr m_GradientMin;

			// Token: 0x04000038 RID: 56
			private IntPtr m_GradientMax;

			// Token: 0x04000039 RID: 57
			private Color m_ColorMin;

			// Token: 0x0400003A RID: 58
			private Color m_ColorMax;
		}

		// Token: 0x02000012 RID: 18
		public struct EmitParams
		{
			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000324 RID: 804 RVA: 0x00004FFC File Offset: 0x000031FC
			// (set) Token: 0x06000325 RID: 805 RVA: 0x00005014 File Offset: 0x00003214
			public ParticleSystem.Particle particle
			{
				get
				{
					return this.m_Particle;
				}
				set
				{
					this.m_Particle = value;
					this.m_PositionSet = true;
					this.m_VelocitySet = true;
					this.m_AxisOfRotationSet = true;
					this.m_RotationSet = true;
					this.m_AngularVelocitySet = true;
					this.m_StartSizeSet = true;
					this.m_StartColorSet = true;
					this.m_RandomSeedSet = true;
					this.m_StartLifetimeSet = true;
					this.m_MeshIndexSet = true;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06000326 RID: 806 RVA: 0x00005070 File Offset: 0x00003270
			// (set) Token: 0x06000327 RID: 807 RVA: 0x0000508D File Offset: 0x0000328D
			public Vector3 position
			{
				get
				{
					return this.m_Particle.position;
				}
				set
				{
					this.m_Particle.position = value;
					this.m_PositionSet = true;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000328 RID: 808 RVA: 0x000050A4 File Offset: 0x000032A4
			// (set) Token: 0x06000329 RID: 809 RVA: 0x000050BC File Offset: 0x000032BC
			public bool applyShapeToPosition
			{
				get
				{
					return this.m_ApplyShapeToPosition;
				}
				set
				{
					this.m_ApplyShapeToPosition = value;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x0600032A RID: 810 RVA: 0x000050C8 File Offset: 0x000032C8
			// (set) Token: 0x0600032B RID: 811 RVA: 0x000050E5 File Offset: 0x000032E5
			public Vector3 velocity
			{
				get
				{
					return this.m_Particle.velocity;
				}
				set
				{
					this.m_Particle.velocity = value;
					this.m_VelocitySet = true;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x0600032C RID: 812 RVA: 0x000050FC File Offset: 0x000032FC
			// (set) Token: 0x0600032D RID: 813 RVA: 0x00005119 File Offset: 0x00003319
			public float startLifetime
			{
				get
				{
					return this.m_Particle.startLifetime;
				}
				set
				{
					this.m_Particle.startLifetime = value;
					this.m_StartLifetimeSet = true;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x0600032E RID: 814 RVA: 0x00005130 File Offset: 0x00003330
			// (set) Token: 0x0600032F RID: 815 RVA: 0x0000514D File Offset: 0x0000334D
			public float startSize
			{
				get
				{
					return this.m_Particle.startSize;
				}
				set
				{
					this.m_Particle.startSize = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000330 RID: 816 RVA: 0x00005164 File Offset: 0x00003364
			// (set) Token: 0x06000331 RID: 817 RVA: 0x00005181 File Offset: 0x00003381
			public Vector3 startSize3D
			{
				get
				{
					return this.m_Particle.startSize3D;
				}
				set
				{
					this.m_Particle.startSize3D = value;
					this.m_StartSizeSet = true;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000332 RID: 818 RVA: 0x00005198 File Offset: 0x00003398
			// (set) Token: 0x06000333 RID: 819 RVA: 0x000051B5 File Offset: 0x000033B5
			public Vector3 axisOfRotation
			{
				get
				{
					return this.m_Particle.axisOfRotation;
				}
				set
				{
					this.m_Particle.axisOfRotation = value;
					this.m_AxisOfRotationSet = true;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000334 RID: 820 RVA: 0x000051CC File Offset: 0x000033CC
			// (set) Token: 0x06000335 RID: 821 RVA: 0x000051E9 File Offset: 0x000033E9
			public float rotation
			{
				get
				{
					return this.m_Particle.rotation;
				}
				set
				{
					this.m_Particle.rotation = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000336 RID: 822 RVA: 0x00005200 File Offset: 0x00003400
			// (set) Token: 0x06000337 RID: 823 RVA: 0x0000521D File Offset: 0x0000341D
			public Vector3 rotation3D
			{
				get
				{
					return this.m_Particle.rotation3D;
				}
				set
				{
					this.m_Particle.rotation3D = value;
					this.m_RotationSet = true;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000338 RID: 824 RVA: 0x00005234 File Offset: 0x00003434
			// (set) Token: 0x06000339 RID: 825 RVA: 0x00005251 File Offset: 0x00003451
			public float angularVelocity
			{
				get
				{
					return this.m_Particle.angularVelocity;
				}
				set
				{
					this.m_Particle.angularVelocity = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x0600033A RID: 826 RVA: 0x00005268 File Offset: 0x00003468
			// (set) Token: 0x0600033B RID: 827 RVA: 0x00005285 File Offset: 0x00003485
			public Vector3 angularVelocity3D
			{
				get
				{
					return this.m_Particle.angularVelocity3D;
				}
				set
				{
					this.m_Particle.angularVelocity3D = value;
					this.m_AngularVelocitySet = true;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x0600033C RID: 828 RVA: 0x0000529C File Offset: 0x0000349C
			// (set) Token: 0x0600033D RID: 829 RVA: 0x000052B9 File Offset: 0x000034B9
			public Color32 startColor
			{
				get
				{
					return this.m_Particle.startColor;
				}
				set
				{
					this.m_Particle.startColor = value;
					this.m_StartColorSet = true;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x0600033E RID: 830 RVA: 0x000052D0 File Offset: 0x000034D0
			// (set) Token: 0x0600033F RID: 831 RVA: 0x000052ED File Offset: 0x000034ED
			public uint randomSeed
			{
				get
				{
					return this.m_Particle.randomSeed;
				}
				set
				{
					this.m_Particle.randomSeed = value;
					this.m_RandomSeedSet = true;
				}
			}

			// Token: 0x17000129 RID: 297
			// (set) Token: 0x06000340 RID: 832 RVA: 0x00005304 File Offset: 0x00003504
			public int meshIndex
			{
				set
				{
					this.m_Particle.SetMeshIndex(value);
					this.m_MeshIndexSet = true;
				}
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000531B File Offset: 0x0000351B
			public void ResetPosition()
			{
				this.m_PositionSet = false;
			}

			// Token: 0x06000342 RID: 834 RVA: 0x00005325 File Offset: 0x00003525
			public void ResetVelocity()
			{
				this.m_VelocitySet = false;
			}

			// Token: 0x06000343 RID: 835 RVA: 0x0000532F File Offset: 0x0000352F
			public void ResetAxisOfRotation()
			{
				this.m_AxisOfRotationSet = false;
			}

			// Token: 0x06000344 RID: 836 RVA: 0x00005339 File Offset: 0x00003539
			public void ResetRotation()
			{
				this.m_RotationSet = false;
			}

			// Token: 0x06000345 RID: 837 RVA: 0x00005343 File Offset: 0x00003543
			public void ResetAngularVelocity()
			{
				this.m_AngularVelocitySet = false;
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000534D File Offset: 0x0000354D
			public void ResetStartSize()
			{
				this.m_StartSizeSet = false;
			}

			// Token: 0x06000347 RID: 839 RVA: 0x00005357 File Offset: 0x00003557
			public void ResetStartColor()
			{
				this.m_StartColorSet = false;
			}

			// Token: 0x06000348 RID: 840 RVA: 0x00005361 File Offset: 0x00003561
			public void ResetRandomSeed()
			{
				this.m_RandomSeedSet = false;
			}

			// Token: 0x06000349 RID: 841 RVA: 0x0000536B File Offset: 0x0000356B
			public void ResetStartLifetime()
			{
				this.m_StartLifetimeSet = false;
			}

			// Token: 0x0600034A RID: 842 RVA: 0x00005375 File Offset: 0x00003575
			public void ResetMeshIndex()
			{
				this.m_MeshIndexSet = false;
			}

			// Token: 0x0400003B RID: 59
			[NativeName("particle")]
			private ParticleSystem.Particle m_Particle;

			// Token: 0x0400003C RID: 60
			[NativeName("positionSet")]
			private bool m_PositionSet;

			// Token: 0x0400003D RID: 61
			[NativeName("velocitySet")]
			private bool m_VelocitySet;

			// Token: 0x0400003E RID: 62
			[NativeName("axisOfRotationSet")]
			private bool m_AxisOfRotationSet;

			// Token: 0x0400003F RID: 63
			[NativeName("rotationSet")]
			private bool m_RotationSet;

			// Token: 0x04000040 RID: 64
			[NativeName("rotationalSpeedSet")]
			private bool m_AngularVelocitySet;

			// Token: 0x04000041 RID: 65
			[NativeName("startSizeSet")]
			private bool m_StartSizeSet;

			// Token: 0x04000042 RID: 66
			[NativeName("startColorSet")]
			private bool m_StartColorSet;

			// Token: 0x04000043 RID: 67
			[NativeName("randomSeedSet")]
			private bool m_RandomSeedSet;

			// Token: 0x04000044 RID: 68
			[NativeName("startLifetimeSet")]
			private bool m_StartLifetimeSet;

			// Token: 0x04000045 RID: 69
			[NativeName("meshIndexSet")]
			private bool m_MeshIndexSet;

			// Token: 0x04000046 RID: 70
			[NativeName("applyShapeToPosition")]
			private bool m_ApplyShapeToPosition;
		}

		// Token: 0x02000013 RID: 19
		public struct PlaybackState
		{
			// Token: 0x04000047 RID: 71
			internal float m_AccumulatedDt;

			// Token: 0x04000048 RID: 72
			internal float m_StartDelay;

			// Token: 0x04000049 RID: 73
			internal float m_PlaybackTime;

			// Token: 0x0400004A RID: 74
			internal int m_RingBufferIndex;

			// Token: 0x0400004B RID: 75
			internal ParticleSystem.PlaybackState.Emission m_Emission;

			// Token: 0x0400004C RID: 76
			internal ParticleSystem.PlaybackState.Initial m_Initial;

			// Token: 0x0400004D RID: 77
			internal ParticleSystem.PlaybackState.Shape m_Shape;

			// Token: 0x0400004E RID: 78
			internal ParticleSystem.PlaybackState.Force m_Force;

			// Token: 0x0400004F RID: 79
			internal ParticleSystem.PlaybackState.Collision m_Collision;

			// Token: 0x04000050 RID: 80
			internal ParticleSystem.PlaybackState.Noise m_Noise;

			// Token: 0x04000051 RID: 81
			internal ParticleSystem.PlaybackState.Lights m_Lights;

			// Token: 0x04000052 RID: 82
			internal ParticleSystem.PlaybackState.Trail m_Trail;

			// Token: 0x02000014 RID: 20
			internal struct Seed
			{
				// Token: 0x04000053 RID: 83
				public uint x;

				// Token: 0x04000054 RID: 84
				public uint y;

				// Token: 0x04000055 RID: 85
				public uint z;

				// Token: 0x04000056 RID: 86
				public uint w;
			}

			// Token: 0x02000015 RID: 21
			internal struct Seed4
			{
				// Token: 0x04000057 RID: 87
				public ParticleSystem.PlaybackState.Seed x;

				// Token: 0x04000058 RID: 88
				public ParticleSystem.PlaybackState.Seed y;

				// Token: 0x04000059 RID: 89
				public ParticleSystem.PlaybackState.Seed z;

				// Token: 0x0400005A RID: 90
				public ParticleSystem.PlaybackState.Seed w;
			}

			// Token: 0x02000016 RID: 22
			internal struct Emission
			{
				// Token: 0x0400005B RID: 91
				public float m_ParticleSpacing;

				// Token: 0x0400005C RID: 92
				public float m_ToEmitAccumulator;

				// Token: 0x0400005D RID: 93
				public ParticleSystem.PlaybackState.Seed m_Random;
			}

			// Token: 0x02000017 RID: 23
			internal struct Initial
			{
				// Token: 0x0400005E RID: 94
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x02000018 RID: 24
			internal struct Shape
			{
				// Token: 0x0400005F RID: 95
				public ParticleSystem.PlaybackState.Seed4 m_Random;

				// Token: 0x04000060 RID: 96
				public float m_RadiusTimer;

				// Token: 0x04000061 RID: 97
				public float m_RadiusTimerPrev;

				// Token: 0x04000062 RID: 98
				public float m_ArcTimer;

				// Token: 0x04000063 RID: 99
				public float m_ArcTimerPrev;

				// Token: 0x04000064 RID: 100
				public float m_MeshSpawnTimer;

				// Token: 0x04000065 RID: 101
				public float m_MeshSpawnTimerPrev;

				// Token: 0x04000066 RID: 102
				public int m_OrderedMeshVertexIndex;
			}

			// Token: 0x02000019 RID: 25
			internal struct Force
			{
				// Token: 0x04000067 RID: 103
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x0200001A RID: 26
			internal struct Collision
			{
				// Token: 0x04000068 RID: 104
				public ParticleSystem.PlaybackState.Seed4 m_Random;
			}

			// Token: 0x0200001B RID: 27
			internal struct Noise
			{
				// Token: 0x04000069 RID: 105
				public float m_ScrollOffset;
			}

			// Token: 0x0200001C RID: 28
			internal struct Lights
			{
				// Token: 0x0400006A RID: 106
				public ParticleSystem.PlaybackState.Seed m_Random;

				// Token: 0x0400006B RID: 107
				public float m_ParticleEmissionCounter;
			}

			// Token: 0x0200001D RID: 29
			internal struct Trail
			{
				// Token: 0x0400006C RID: 108
				public float m_Timer;
			}
		}

		// Token: 0x0200001E RID: 30
		[NativeType(CodegenOptions.Custom, "MonoParticleTrails")]
		public struct Trails
		{
			// Token: 0x0600034B RID: 843 RVA: 0x00005380 File Offset: 0x00003580
			internal void Allocate()
			{
				bool flag = this.positions == null;
				if (flag)
				{
					this.positions = new List<Vector4>();
				}
				bool flag2 = this.frontPositions == null;
				if (flag2)
				{
					this.frontPositions = new List<int>();
				}
				bool flag3 = this.backPositions == null;
				if (flag3)
				{
					this.backPositions = new List<int>();
				}
				bool flag4 = this.positionCounts == null;
				if (flag4)
				{
					this.positionCounts = new List<int>();
				}
				bool flag5 = this.textureOffsets == null;
				if (flag5)
				{
					this.textureOffsets = new List<float>();
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x0600034D RID: 845 RVA: 0x00005460 File Offset: 0x00003660
			// (set) Token: 0x0600034C RID: 844 RVA: 0x00005408 File Offset: 0x00003608
			public int capacity
			{
				get
				{
					bool flag = this.positions == null;
					int num;
					if (flag)
					{
						num = 0;
					}
					else
					{
						num = this.positions.Capacity;
					}
					return num;
				}
				set
				{
					this.Allocate();
					this.positions.Capacity = value;
					this.frontPositions.Capacity = value;
					this.backPositions.Capacity = value;
					this.positionCounts.Capacity = value;
					this.textureOffsets.Capacity = value;
				}
			}

			// Token: 0x0400006D RID: 109
			internal List<Vector4> positions;

			// Token: 0x0400006E RID: 110
			internal List<int> frontPositions;

			// Token: 0x0400006F RID: 111
			internal List<int> backPositions;

			// Token: 0x04000070 RID: 112
			internal List<int> positionCounts;

			// Token: 0x04000071 RID: 113
			internal List<float> textureOffsets;

			// Token: 0x04000072 RID: 114
			internal int maxTrailCount;

			// Token: 0x04000073 RID: 115
			internal int maxPositionsPerTrailCount;
		}

		// Token: 0x0200001F RID: 31
		public struct ColliderData
		{
			// Token: 0x0600034E RID: 846 RVA: 0x00005490 File Offset: 0x00003690
			public int GetColliderCount(int particleIndex)
			{
				bool flag = particleIndex < this.particleStartIndices.Length - 1;
				int num;
				if (flag)
				{
					num = this.particleStartIndices[particleIndex + 1] - this.particleStartIndices[particleIndex];
				}
				else
				{
					num = this.colliderIndices.Length - this.particleStartIndices[particleIndex];
				}
				return num;
			}

			// Token: 0x0600034F RID: 847 RVA: 0x000054DC File Offset: 0x000036DC
			public Component GetCollider(int particleIndex, int colliderIndex)
			{
				bool flag = colliderIndex >= this.GetColliderCount(particleIndex);
				if (flag)
				{
					throw new IndexOutOfRangeException("colliderIndex exceeded the total number of colliders for the requested particle");
				}
				int num = this.particleStartIndices[particleIndex] + colliderIndex;
				return this.colliders[this.colliderIndices[num]];
			}

			// Token: 0x04000074 RID: 116
			internal Component[] colliders;

			// Token: 0x04000075 RID: 117
			internal int[] colliderIndices;

			// Token: 0x04000076 RID: 118
			internal int[] particleStartIndices;
		}

		// Token: 0x02000020 RID: 32
		public struct VelocityOverLifetimeModule
		{
			// Token: 0x06000350 RID: 848 RVA: 0x00005524 File Offset: 0x00003724
			internal VelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x06000351 RID: 849
			// (set) Token: 0x06000352 RID: 850
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000353 RID: 851 RVA: 0x0000552E File Offset: 0x0000372E
			// (set) Token: 0x06000354 RID: 852 RVA: 0x0000553B File Offset: 0x0000373B
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					return this.xBlittable;
				}
				set
				{
					this.xBlittable = value;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06000355 RID: 853 RVA: 0x0000554C File Offset: 0x0000374C
			// (set) Token: 0x06000356 RID: 854 RVA: 0x00005564 File Offset: 0x00003764
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable xBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_xBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_xBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000357 RID: 855 RVA: 0x00005579 File Offset: 0x00003779
			// (set) Token: 0x06000358 RID: 856 RVA: 0x00005586 File Offset: 0x00003786
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					return this.yBlittable;
				}
				set
				{
					this.yBlittable = value;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x06000359 RID: 857 RVA: 0x00005598 File Offset: 0x00003798
			// (set) Token: 0x0600035A RID: 858 RVA: 0x000055B0 File Offset: 0x000037B0
			[NativeName("Y")]
			private ParticleSystem.MinMaxCurveBlittable yBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_yBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_yBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x0600035B RID: 859 RVA: 0x000055C5 File Offset: 0x000037C5
			// (set) Token: 0x0600035C RID: 860 RVA: 0x000055D2 File Offset: 0x000037D2
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					return this.zBlittable;
				}
				set
				{
					this.zBlittable = value;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x0600035D RID: 861 RVA: 0x000055E4 File Offset: 0x000037E4
			// (set) Token: 0x0600035E RID: 862 RVA: 0x000055FC File Offset: 0x000037FC
			[NativeName("Z")]
			private ParticleSystem.MinMaxCurveBlittable zBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_zBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_zBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x0600035F RID: 863
			// (set) Token: 0x06000360 RID: 864
			public extern float xMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000361 RID: 865
			// (set) Token: 0x06000362 RID: 866
			public extern float yMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000363 RID: 867
			// (set) Token: 0x06000364 RID: 868
			public extern float zMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x06000365 RID: 869 RVA: 0x00005611 File Offset: 0x00003811
			// (set) Token: 0x06000366 RID: 870 RVA: 0x0000561E File Offset: 0x0000381E
			public ParticleSystem.MinMaxCurve orbitalX
			{
				get
				{
					return this.orbitalXBlittable;
				}
				set
				{
					this.orbitalXBlittable = value;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000367 RID: 871 RVA: 0x00005630 File Offset: 0x00003830
			// (set) Token: 0x06000368 RID: 872 RVA: 0x00005648 File Offset: 0x00003848
			[NativeName("OrbitalX")]
			private ParticleSystem.MinMaxCurveBlittable orbitalXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000369 RID: 873 RVA: 0x0000565D File Offset: 0x0000385D
			// (set) Token: 0x0600036A RID: 874 RVA: 0x0000566A File Offset: 0x0000386A
			public ParticleSystem.MinMaxCurve orbitalY
			{
				get
				{
					return this.orbitalYBlittable;
				}
				set
				{
					this.orbitalYBlittable = value;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x0600036B RID: 875 RVA: 0x0000567C File Offset: 0x0000387C
			// (set) Token: 0x0600036C RID: 876 RVA: 0x00005694 File Offset: 0x00003894
			[NativeName("OrbitalY")]
			private ParticleSystem.MinMaxCurveBlittable orbitalYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x0600036D RID: 877 RVA: 0x000056A9 File Offset: 0x000038A9
			// (set) Token: 0x0600036E RID: 878 RVA: 0x000056B6 File Offset: 0x000038B6
			public ParticleSystem.MinMaxCurve orbitalZ
			{
				get
				{
					return this.orbitalZBlittable;
				}
				set
				{
					this.orbitalZBlittable = value;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x0600036F RID: 879 RVA: 0x000056C8 File Offset: 0x000038C8
			// (set) Token: 0x06000370 RID: 880 RVA: 0x000056E0 File Offset: 0x000038E0
			[NativeName("OrbitalZ")]
			private ParticleSystem.MinMaxCurveBlittable orbitalZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x06000371 RID: 881
			// (set) Token: 0x06000372 RID: 882
			public extern float orbitalXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000373 RID: 883
			// (set) Token: 0x06000374 RID: 884
			public extern float orbitalYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000375 RID: 885
			// (set) Token: 0x06000376 RID: 886
			public extern float orbitalZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x06000377 RID: 887 RVA: 0x000056F5 File Offset: 0x000038F5
			// (set) Token: 0x06000378 RID: 888 RVA: 0x00005702 File Offset: 0x00003902
			public ParticleSystem.MinMaxCurve orbitalOffsetX
			{
				get
				{
					return this.orbitalOffsetXBlittable;
				}
				set
				{
					this.orbitalOffsetXBlittable = value;
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x06000379 RID: 889 RVA: 0x00005714 File Offset: 0x00003914
			// (set) Token: 0x0600037A RID: 890 RVA: 0x0000572C File Offset: 0x0000392C
			[NativeName("OrbitalOffsetX")]
			private ParticleSystem.MinMaxCurveBlittable orbitalOffsetXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x0600037B RID: 891 RVA: 0x00005741 File Offset: 0x00003941
			// (set) Token: 0x0600037C RID: 892 RVA: 0x0000574E File Offset: 0x0000394E
			public ParticleSystem.MinMaxCurve orbitalOffsetY
			{
				get
				{
					return this.orbitalOffsetYBlittable;
				}
				set
				{
					this.orbitalOffsetYBlittable = value;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x0600037D RID: 893 RVA: 0x00005760 File Offset: 0x00003960
			// (set) Token: 0x0600037E RID: 894 RVA: 0x00005778 File Offset: 0x00003978
			[NativeName("OrbitalOffsetY")]
			private ParticleSystem.MinMaxCurveBlittable orbitalOffsetYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x0600037F RID: 895 RVA: 0x0000578D File Offset: 0x0000398D
			// (set) Token: 0x06000380 RID: 896 RVA: 0x0000579A File Offset: 0x0000399A
			public ParticleSystem.MinMaxCurve orbitalOffsetZ
			{
				get
				{
					return this.orbitalOffsetZBlittable;
				}
				set
				{
					this.orbitalOffsetZBlittable = value;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x06000381 RID: 897 RVA: 0x000057AC File Offset: 0x000039AC
			// (set) Token: 0x06000382 RID: 898 RVA: 0x000057C4 File Offset: 0x000039C4
			[NativeName("OrbitalOffsetZ")]
			private ParticleSystem.MinMaxCurveBlittable orbitalOffsetZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_orbitalOffsetZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_orbitalOffsetZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000383 RID: 899
			// (set) Token: 0x06000384 RID: 900
			public extern float orbitalOffsetXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000385 RID: 901
			// (set) Token: 0x06000386 RID: 902
			public extern float orbitalOffsetYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000387 RID: 903
			// (set) Token: 0x06000388 RID: 904
			public extern float orbitalOffsetZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000389 RID: 905 RVA: 0x000057D9 File Offset: 0x000039D9
			// (set) Token: 0x0600038A RID: 906 RVA: 0x000057E6 File Offset: 0x000039E6
			public ParticleSystem.MinMaxCurve radial
			{
				get
				{
					return this.radialBlittable;
				}
				set
				{
					this.radialBlittable = value;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600038B RID: 907 RVA: 0x000057F8 File Offset: 0x000039F8
			// (set) Token: 0x0600038C RID: 908 RVA: 0x00005810 File Offset: 0x00003A10
			[NativeName("Radial")]
			private ParticleSystem.MinMaxCurveBlittable radialBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_radialBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_radialBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600038D RID: 909
			// (set) Token: 0x0600038E RID: 910
			public extern float radialMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x0600038F RID: 911 RVA: 0x00005825 File Offset: 0x00003A25
			// (set) Token: 0x06000390 RID: 912 RVA: 0x00005832 File Offset: 0x00003A32
			public ParticleSystem.MinMaxCurve speedModifier
			{
				get
				{
					return this.speedModifierBlittable;
				}
				set
				{
					this.speedModifierBlittable = value;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000391 RID: 913 RVA: 0x00005844 File Offset: 0x00003A44
			// (set) Token: 0x06000392 RID: 914 RVA: 0x0000585C File Offset: 0x00003A5C
			[NativeName("SpeedModifier")]
			private ParticleSystem.MinMaxCurveBlittable speedModifierBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.VelocityOverLifetimeModule.get_speedModifierBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.VelocityOverLifetimeModule.set_speedModifierBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000393 RID: 915
			// (set) Token: 0x06000394 RID: 916
			public extern float speedModifierMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000395 RID: 917
			// (set) Token: 0x06000396 RID: 918
			public extern ParticleSystemSimulationSpace space
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000397 RID: 919
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_xBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000398 RID: 920
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000399 RID: 921
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_yBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600039A RID: 922
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600039B RID: 923
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_zBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600039C RID: 924
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600039D RID: 925
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalXBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600039E RID: 926
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalXBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600039F RID: 927
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalYBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003A0 RID: 928
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalYBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003A1 RID: 929
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalZBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003A2 RID: 930
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalZBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003A3 RID: 931
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetXBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003A4 RID: 932
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetXBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003A5 RID: 933
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetYBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003A6 RID: 934
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetYBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003A7 RID: 935
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_orbitalOffsetZBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003A8 RID: 936
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_orbitalOffsetZBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003A9 RID: 937
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_radialBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003AA RID: 938
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_radialBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003AB RID: 939
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_speedModifierBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003AC RID: 940
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_speedModifierBlittable_Injected(ref ParticleSystem.VelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x04000077 RID: 119
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000021 RID: 33
		public struct LimitVelocityOverLifetimeModule
		{
			// Token: 0x060003AD RID: 941 RVA: 0x00005871 File Offset: 0x00003A71
			internal LimitVelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060003AE RID: 942
			// (set) Token: 0x060003AF RID: 943
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000587B File Offset: 0x00003A7B
			// (set) Token: 0x060003B1 RID: 945 RVA: 0x00005888 File Offset: 0x00003A88
			public ParticleSystem.MinMaxCurve limitX
			{
				get
				{
					return this.limitXBlittable;
				}
				set
				{
					this.limitXBlittable = value;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060003B2 RID: 946 RVA: 0x00005898 File Offset: 0x00003A98
			// (set) Token: 0x060003B3 RID: 947 RVA: 0x000058B0 File Offset: 0x00003AB0
			[NativeName("LimitX")]
			private ParticleSystem.MinMaxCurveBlittable limitXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060003B4 RID: 948
			// (set) Token: 0x060003B5 RID: 949
			public extern float limitXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x000058C5 File Offset: 0x00003AC5
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x000058D2 File Offset: 0x00003AD2
			public ParticleSystem.MinMaxCurve limitY
			{
				get
				{
					return this.limitYBlittable;
				}
				set
				{
					this.limitYBlittable = value;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x000058E4 File Offset: 0x00003AE4
			// (set) Token: 0x060003B9 RID: 953 RVA: 0x000058FC File Offset: 0x00003AFC
			[NativeName("LimitY")]
			private ParticleSystem.MinMaxCurveBlittable limitYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060003BA RID: 954
			// (set) Token: 0x060003BB RID: 955
			public extern float limitYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060003BC RID: 956 RVA: 0x00005911 File Offset: 0x00003B11
			// (set) Token: 0x060003BD RID: 957 RVA: 0x0000591E File Offset: 0x00003B1E
			public ParticleSystem.MinMaxCurve limitZ
			{
				get
				{
					return this.limitZBlittable;
				}
				set
				{
					this.limitZBlittable = value;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060003BE RID: 958 RVA: 0x00005930 File Offset: 0x00003B30
			// (set) Token: 0x060003BF RID: 959 RVA: 0x00005948 File Offset: 0x00003B48
			[NativeName("LimitZ")]
			private ParticleSystem.MinMaxCurveBlittable limitZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060003C0 RID: 960
			// (set) Token: 0x060003C1 RID: 961
			public extern float limitZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000595D File Offset: 0x00003B5D
			// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000596A File Offset: 0x00003B6A
			public ParticleSystem.MinMaxCurve limit
			{
				get
				{
					return this.limitBlittable;
				}
				set
				{
					this.limitBlittable = value;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000597C File Offset: 0x00003B7C
			// (set) Token: 0x060003C5 RID: 965 RVA: 0x00005994 File Offset: 0x00003B94
			[NativeName("Magnitude")]
			private ParticleSystem.MinMaxCurveBlittable limitBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_limitBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_limitBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060003C6 RID: 966
			// (set) Token: 0x060003C7 RID: 967
			[NativeName("MagnitudeMultiplier")]
			public extern float limitMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060003C8 RID: 968
			// (set) Token: 0x060003C9 RID: 969
			public extern float dampen
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060003CA RID: 970
			// (set) Token: 0x060003CB RID: 971
			public extern bool separateAxes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060003CC RID: 972
			// (set) Token: 0x060003CD RID: 973
			public extern ParticleSystemSimulationSpace space
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060003CE RID: 974 RVA: 0x000059A9 File Offset: 0x00003BA9
			// (set) Token: 0x060003CF RID: 975 RVA: 0x000059B6 File Offset: 0x00003BB6
			public ParticleSystem.MinMaxCurve drag
			{
				get
				{
					return this.dragBlittable;
				}
				set
				{
					this.dragBlittable = value;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x000059C8 File Offset: 0x00003BC8
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x000059E0 File Offset: 0x00003BE0
			[NativeName("Drag")]
			private ParticleSystem.MinMaxCurveBlittable dragBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LimitVelocityOverLifetimeModule.get_dragBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LimitVelocityOverLifetimeModule.set_dragBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060003D2 RID: 978
			// (set) Token: 0x060003D3 RID: 979
			public extern float dragMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060003D4 RID: 980
			// (set) Token: 0x060003D5 RID: 981
			public extern bool multiplyDragByParticleSize
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060003D6 RID: 982
			// (set) Token: 0x060003D7 RID: 983
			public extern bool multiplyDragByParticleVelocity
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x060003D8 RID: 984
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitXBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003D9 RID: 985
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitXBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003DA RID: 986
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitYBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003DB RID: 987
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitYBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003DC RID: 988
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitZBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003DD RID: 989
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitZBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003DE RID: 990
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_limitBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003DF RID: 991
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_limitBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003E0 RID: 992
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_dragBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003E1 RID: 993
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_dragBlittable_Injected(ref ParticleSystem.LimitVelocityOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x04000078 RID: 120
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000022 RID: 34
		public struct InheritVelocityModule
		{
			// Token: 0x060003E2 RID: 994 RVA: 0x000059F5 File Offset: 0x00003BF5
			internal InheritVelocityModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060003E3 RID: 995
			// (set) Token: 0x060003E4 RID: 996
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x060003E5 RID: 997
			// (set) Token: 0x060003E6 RID: 998
			public extern ParticleSystemInheritVelocityMode mode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x000059FF File Offset: 0x00003BFF
			// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00005A0C File Offset: 0x00003C0C
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					return this.curveBlittable;
				}
				set
				{
					this.curveBlittable = value;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00005A1C File Offset: 0x00003C1C
			// (set) Token: 0x060003EA RID: 1002 RVA: 0x00005A34 File Offset: 0x00003C34
			[NativeName("Curve")]
			private ParticleSystem.MinMaxCurveBlittable curveBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.InheritVelocityModule.get_curveBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.InheritVelocityModule.set_curveBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060003EB RID: 1003
			// (set) Token: 0x060003EC RID: 1004
			public extern float curveMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x060003ED RID: 1005
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_curveBlittable_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003EE RID: 1006
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curveBlittable_Injected(ref ParticleSystem.InheritVelocityModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x04000079 RID: 121
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000023 RID: 35
		public struct LifetimeByEmitterSpeedModule
		{
			// Token: 0x060003EF RID: 1007 RVA: 0x00005A49 File Offset: 0x00003C49
			internal LifetimeByEmitterSpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x060003F0 RID: 1008
			// (set) Token: 0x060003F1 RID: 1009
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00005A53 File Offset: 0x00003C53
			// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00005A60 File Offset: 0x00003C60
			public ParticleSystem.MinMaxCurve curve
			{
				get
				{
					return this.curveBlittable;
				}
				set
				{
					this.curveBlittable = value;
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00005A70 File Offset: 0x00003C70
			// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00005A88 File Offset: 0x00003C88
			[NativeName("Curve")]
			private ParticleSystem.MinMaxCurveBlittable curveBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_curveBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_curveBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x060003F6 RID: 1014
			// (set) Token: 0x060003F7 RID: 1015
			public extern float curveMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00005AA0 File Offset: 0x00003CA0
			// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00005AB8 File Offset: 0x00003CB8
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.LifetimeByEmitterSpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LifetimeByEmitterSpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x060003FA RID: 1018
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_curveBlittable_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060003FB RID: 1019
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_curveBlittable_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060003FC RID: 1020
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, out Vector2 ret);

			// Token: 0x060003FD RID: 1021
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.LifetimeByEmitterSpeedModule _unity_self, [In] ref Vector2 value);

			// Token: 0x0400007A RID: 122
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000024 RID: 36
		public struct ForceOverLifetimeModule
		{
			// Token: 0x060003FE RID: 1022 RVA: 0x00005ACD File Offset: 0x00003CCD
			internal ForceOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x060003FF RID: 1023
			// (set) Token: 0x06000400 RID: 1024
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000401 RID: 1025 RVA: 0x00005AD7 File Offset: 0x00003CD7
			// (set) Token: 0x06000402 RID: 1026 RVA: 0x00005AE4 File Offset: 0x00003CE4
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					return this.xBlittable;
				}
				set
				{
					this.xBlittable = value;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000403 RID: 1027 RVA: 0x00005AF4 File Offset: 0x00003CF4
			// (set) Token: 0x06000404 RID: 1028 RVA: 0x00005B0C File Offset: 0x00003D0C
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable xBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ForceOverLifetimeModule.get_xBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_xBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x00005B21 File Offset: 0x00003D21
			// (set) Token: 0x06000406 RID: 1030 RVA: 0x00005B2E File Offset: 0x00003D2E
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					return this.yBlittable;
				}
				set
				{
					this.yBlittable = value;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x00005B40 File Offset: 0x00003D40
			// (set) Token: 0x06000408 RID: 1032 RVA: 0x00005B58 File Offset: 0x00003D58
			[NativeName("Y")]
			private ParticleSystem.MinMaxCurveBlittable yBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ForceOverLifetimeModule.get_yBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_yBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x00005B6D File Offset: 0x00003D6D
			// (set) Token: 0x0600040A RID: 1034 RVA: 0x00005B7A File Offset: 0x00003D7A
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					return this.zBlittable;
				}
				set
				{
					this.zBlittable = value;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600040B RID: 1035 RVA: 0x00005B8C File Offset: 0x00003D8C
			// (set) Token: 0x0600040C RID: 1036 RVA: 0x00005BA4 File Offset: 0x00003DA4
			[NativeName("Z")]
			private ParticleSystem.MinMaxCurveBlittable zBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ForceOverLifetimeModule.get_zBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ForceOverLifetimeModule.set_zBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600040D RID: 1037
			// (set) Token: 0x0600040E RID: 1038
			public extern float xMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600040F RID: 1039
			// (set) Token: 0x06000410 RID: 1040
			public extern float yMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000411 RID: 1041
			// (set) Token: 0x06000412 RID: 1042
			public extern float zMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000413 RID: 1043
			// (set) Token: 0x06000414 RID: 1044
			public extern ParticleSystemSimulationSpace space
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000415 RID: 1045
			// (set) Token: 0x06000416 RID: 1046
			public extern bool randomized
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000417 RID: 1047
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_xBlittable_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000418 RID: 1048
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xBlittable_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000419 RID: 1049
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_yBlittable_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600041A RID: 1050
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yBlittable_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600041B RID: 1051
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_zBlittable_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600041C RID: 1052
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zBlittable_Injected(ref ParticleSystem.ForceOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0400007B RID: 123
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000025 RID: 37
		public struct ColorOverLifetimeModule
		{
			// Token: 0x0600041D RID: 1053 RVA: 0x00005BB9 File Offset: 0x00003DB9
			internal ColorOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x0600041E RID: 1054
			// (set) Token: 0x0600041F RID: 1055
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x00005BC3 File Offset: 0x00003DC3
			// (set) Token: 0x06000421 RID: 1057 RVA: 0x00005BD0 File Offset: 0x00003DD0
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					return this.colorBlittable;
				}
				set
				{
					this.colorBlittable = value;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x00005BE0 File Offset: 0x00003DE0
			// (set) Token: 0x06000423 RID: 1059 RVA: 0x00005BF8 File Offset: 0x00003DF8
			[NativeName("Color")]
			private ParticleSystem.MinMaxGradientBlittable colorBlittable
			{
				get
				{
					ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable;
					ParticleSystem.ColorOverLifetimeModule.get_colorBlittable_Injected(ref this, out minMaxGradientBlittable);
					return minMaxGradientBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorOverLifetimeModule.set_colorBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000424 RID: 1060
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorBlittable_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, out ParticleSystem.MinMaxGradientBlittable ret);

			// Token: 0x06000425 RID: 1061
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorBlittable_Injected(ref ParticleSystem.ColorOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxGradientBlittable value);

			// Token: 0x0400007C RID: 124
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000026 RID: 38
		public struct ColorBySpeedModule
		{
			// Token: 0x06000426 RID: 1062 RVA: 0x00005C0D File Offset: 0x00003E0D
			internal ColorBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000427 RID: 1063
			// (set) Token: 0x06000428 RID: 1064
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000429 RID: 1065 RVA: 0x00005C17 File Offset: 0x00003E17
			// (set) Token: 0x0600042A RID: 1066 RVA: 0x00005C24 File Offset: 0x00003E24
			public ParticleSystem.MinMaxGradient color
			{
				get
				{
					return this.colorBlittable;
				}
				set
				{
					this.colorBlittable = value;
				}
			}

			// Token: 0x1700017E RID: 382
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00005C34 File Offset: 0x00003E34
			// (set) Token: 0x0600042C RID: 1068 RVA: 0x00005C4C File Offset: 0x00003E4C
			[NativeName("Color")]
			private ParticleSystem.MinMaxGradientBlittable colorBlittable
			{
				get
				{
					ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable;
					ParticleSystem.ColorBySpeedModule.get_colorBlittable_Injected(ref this, out minMaxGradientBlittable);
					return minMaxGradientBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_colorBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x0600042D RID: 1069 RVA: 0x00005C64 File Offset: 0x00003E64
			// (set) Token: 0x0600042E RID: 1070 RVA: 0x00005C7C File Offset: 0x00003E7C
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.ColorBySpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ColorBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x0600042F RID: 1071
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorBlittable_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out ParticleSystem.MinMaxGradientBlittable ret);

			// Token: 0x06000430 RID: 1072
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorBlittable_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxGradientBlittable value);

			// Token: 0x06000431 RID: 1073
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x06000432 RID: 1074
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.ColorBySpeedModule _unity_self, [In] ref Vector2 value);

			// Token: 0x0400007D RID: 125
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000027 RID: 39
		public struct SizeOverLifetimeModule
		{
			// Token: 0x06000433 RID: 1075 RVA: 0x00005C91 File Offset: 0x00003E91
			internal SizeOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x06000434 RID: 1076
			// (set) Token: 0x06000435 RID: 1077
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x06000436 RID: 1078 RVA: 0x00005C9B File Offset: 0x00003E9B
			// (set) Token: 0x06000437 RID: 1079 RVA: 0x00005CA8 File Offset: 0x00003EA8
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					return this.sizeBlittable;
				}
				set
				{
					this.sizeBlittable = value;
				}
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000438 RID: 1080 RVA: 0x00005CB8 File Offset: 0x00003EB8
			// (set) Token: 0x06000439 RID: 1081 RVA: 0x00005CD0 File Offset: 0x00003ED0
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable sizeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeOverLifetimeModule.get_sizeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_sizeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x0600043A RID: 1082
			// (set) Token: 0x0600043B RID: 1083
			[NativeName("XMultiplier")]
			public extern float sizeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x0600043C RID: 1084 RVA: 0x00005CE5 File Offset: 0x00003EE5
			// (set) Token: 0x0600043D RID: 1085 RVA: 0x00005CF2 File Offset: 0x00003EF2
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					return this.xBlittable;
				}
				set
				{
					this.xBlittable = value;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x00005D04 File Offset: 0x00003F04
			// (set) Token: 0x0600043F RID: 1087 RVA: 0x00005D1C File Offset: 0x00003F1C
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable xBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeOverLifetimeModule.get_xBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_xBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000440 RID: 1088
			// (set) Token: 0x06000441 RID: 1089
			public extern float xMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00005D31 File Offset: 0x00003F31
			// (set) Token: 0x06000443 RID: 1091 RVA: 0x00005D3E File Offset: 0x00003F3E
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					return this.yBlittable;
				}
				set
				{
					this.yBlittable = value;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x00005D50 File Offset: 0x00003F50
			// (set) Token: 0x06000445 RID: 1093 RVA: 0x00005D68 File Offset: 0x00003F68
			[NativeName("Y")]
			private ParticleSystem.MinMaxCurveBlittable yBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeOverLifetimeModule.get_yBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_yBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000446 RID: 1094
			// (set) Token: 0x06000447 RID: 1095
			public extern float yMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x00005D7D File Offset: 0x00003F7D
			// (set) Token: 0x06000449 RID: 1097 RVA: 0x00005D8A File Offset: 0x00003F8A
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					return this.zBlittable;
				}
				set
				{
					this.zBlittable = value;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x00005D9C File Offset: 0x00003F9C
			// (set) Token: 0x0600044B RID: 1099 RVA: 0x00005DB4 File Offset: 0x00003FB4
			[NativeName("Z")]
			private ParticleSystem.MinMaxCurveBlittable zBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeOverLifetimeModule.get_zBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeOverLifetimeModule.set_zBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x0600044C RID: 1100
			// (set) Token: 0x0600044D RID: 1101
			public extern float zMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x0600044E RID: 1102
			// (set) Token: 0x0600044F RID: 1103
			public extern bool separateAxes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000450 RID: 1104
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_sizeBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000451 RID: 1105
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000452 RID: 1106
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_xBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000453 RID: 1107
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000454 RID: 1108
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_yBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000455 RID: 1109
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000456 RID: 1110
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_zBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000457 RID: 1111
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zBlittable_Injected(ref ParticleSystem.SizeOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0400007E RID: 126
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000028 RID: 40
		public struct SizeBySpeedModule
		{
			// Token: 0x06000458 RID: 1112 RVA: 0x00005DC9 File Offset: 0x00003FC9
			internal SizeBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06000459 RID: 1113
			// (set) Token: 0x0600045A RID: 1114
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x0600045B RID: 1115 RVA: 0x00005DD3 File Offset: 0x00003FD3
			// (set) Token: 0x0600045C RID: 1116 RVA: 0x00005DE0 File Offset: 0x00003FE0
			public ParticleSystem.MinMaxCurve size
			{
				get
				{
					return this.sizeBlittable;
				}
				set
				{
					this.sizeBlittable = value;
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x0600045D RID: 1117 RVA: 0x00005DF0 File Offset: 0x00003FF0
			// (set) Token: 0x0600045E RID: 1118 RVA: 0x00005E08 File Offset: 0x00004008
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable sizeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeBySpeedModule.get_sizeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_sizeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x0600045F RID: 1119
			// (set) Token: 0x06000460 RID: 1120
			[NativeName("XMultiplier")]
			public extern float sizeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x06000461 RID: 1121 RVA: 0x00005E1D File Offset: 0x0000401D
			// (set) Token: 0x06000462 RID: 1122 RVA: 0x00005E2A File Offset: 0x0000402A
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					return this.xBlittable;
				}
				set
				{
					this.xBlittable = value;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x00005E3C File Offset: 0x0000403C
			// (set) Token: 0x06000464 RID: 1124 RVA: 0x00005E54 File Offset: 0x00004054
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable xBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeBySpeedModule.get_xBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_xBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000465 RID: 1125
			// (set) Token: 0x06000466 RID: 1126
			public extern float xMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000467 RID: 1127 RVA: 0x00005E69 File Offset: 0x00004069
			// (set) Token: 0x06000468 RID: 1128 RVA: 0x00005E76 File Offset: 0x00004076
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					return this.yBlittable;
				}
				set
				{
					this.yBlittable = value;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x00005E88 File Offset: 0x00004088
			// (set) Token: 0x0600046A RID: 1130 RVA: 0x00005EA0 File Offset: 0x000040A0
			[NativeName("Y")]
			private ParticleSystem.MinMaxCurveBlittable yBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeBySpeedModule.get_yBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_yBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x0600046B RID: 1131
			// (set) Token: 0x0600046C RID: 1132
			public extern float yMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600046D RID: 1133 RVA: 0x00005EB5 File Offset: 0x000040B5
			// (set) Token: 0x0600046E RID: 1134 RVA: 0x00005EC2 File Offset: 0x000040C2
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					return this.zBlittable;
				}
				set
				{
					this.zBlittable = value;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x00005ED4 File Offset: 0x000040D4
			// (set) Token: 0x06000470 RID: 1136 RVA: 0x00005EEC File Offset: 0x000040EC
			[NativeName("Z")]
			private ParticleSystem.MinMaxCurveBlittable zBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.SizeBySpeedModule.get_zBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_zBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000471 RID: 1137
			// (set) Token: 0x06000472 RID: 1138
			public extern float zMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000473 RID: 1139
			// (set) Token: 0x06000474 RID: 1140
			public extern bool separateAxes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000475 RID: 1141 RVA: 0x00005F04 File Offset: 0x00004104
			// (set) Token: 0x06000476 RID: 1142 RVA: 0x00005F1C File Offset: 0x0000411C
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.SizeBySpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.SizeBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000477 RID: 1143
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_sizeBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000478 RID: 1144
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000479 RID: 1145
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_xBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600047A RID: 1146
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600047B RID: 1147
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_yBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600047C RID: 1148
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600047D RID: 1149
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_zBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600047E RID: 1150
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zBlittable_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600047F RID: 1151
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x06000480 RID: 1152
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.SizeBySpeedModule _unity_self, [In] ref Vector2 value);

			// Token: 0x0400007F RID: 127
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000029 RID: 41
		public struct RotationOverLifetimeModule
		{
			// Token: 0x06000481 RID: 1153 RVA: 0x00005F31 File Offset: 0x00004131
			internal RotationOverLifetimeModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000482 RID: 1154
			// (set) Token: 0x06000483 RID: 1155
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700019E RID: 414
			// (get) Token: 0x06000484 RID: 1156 RVA: 0x00005F3B File Offset: 0x0000413B
			// (set) Token: 0x06000485 RID: 1157 RVA: 0x00005F48 File Offset: 0x00004148
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					return this.xBlittable;
				}
				set
				{
					this.xBlittable = value;
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x06000486 RID: 1158 RVA: 0x00005F58 File Offset: 0x00004158
			// (set) Token: 0x06000487 RID: 1159 RVA: 0x00005F70 File Offset: 0x00004170
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable xBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.RotationOverLifetimeModule.get_xBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_xBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x06000488 RID: 1160
			// (set) Token: 0x06000489 RID: 1161
			public extern float xMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x0600048A RID: 1162 RVA: 0x00005F85 File Offset: 0x00004185
			// (set) Token: 0x0600048B RID: 1163 RVA: 0x00005F92 File Offset: 0x00004192
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					return this.yBlittable;
				}
				set
				{
					this.yBlittable = value;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x0600048C RID: 1164 RVA: 0x00005FA4 File Offset: 0x000041A4
			// (set) Token: 0x0600048D RID: 1165 RVA: 0x00005FBC File Offset: 0x000041BC
			[NativeName("Y")]
			private ParticleSystem.MinMaxCurveBlittable yBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.RotationOverLifetimeModule.get_yBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_yBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x0600048E RID: 1166
			// (set) Token: 0x0600048F RID: 1167
			public extern float yMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x06000490 RID: 1168 RVA: 0x00005FD1 File Offset: 0x000041D1
			// (set) Token: 0x06000491 RID: 1169 RVA: 0x00005FDE File Offset: 0x000041DE
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					return this.zBlittable;
				}
				set
				{
					this.zBlittable = value;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x06000492 RID: 1170 RVA: 0x00005FF0 File Offset: 0x000041F0
			// (set) Token: 0x06000493 RID: 1171 RVA: 0x00006008 File Offset: 0x00004208
			[NativeName("Z")]
			private ParticleSystem.MinMaxCurveBlittable zBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.RotationOverLifetimeModule.get_zBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationOverLifetimeModule.set_zBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x06000494 RID: 1172
			// (set) Token: 0x06000495 RID: 1173
			public extern float zMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000496 RID: 1174
			// (set) Token: 0x06000497 RID: 1175
			public extern bool separateAxes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000498 RID: 1176
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_xBlittable_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000499 RID: 1177
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xBlittable_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600049A RID: 1178
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_yBlittable_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600049B RID: 1179
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yBlittable_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600049C RID: 1180
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_zBlittable_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600049D RID: 1181
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zBlittable_Injected(ref ParticleSystem.RotationOverLifetimeModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x04000080 RID: 128
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002A RID: 42
		public struct RotationBySpeedModule
		{
			// Token: 0x0600049E RID: 1182 RVA: 0x0000601D File Offset: 0x0000421D
			internal RotationBySpeedModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x0600049F RID: 1183
			// (set) Token: 0x060004A0 RID: 1184
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00006027 File Offset: 0x00004227
			// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00006034 File Offset: 0x00004234
			public ParticleSystem.MinMaxCurve x
			{
				get
				{
					return this.xBlittable;
				}
				set
				{
					this.xBlittable = value;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00006044 File Offset: 0x00004244
			// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000605C File Offset: 0x0000425C
			[NativeName("X")]
			private ParticleSystem.MinMaxCurveBlittable xBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.RotationBySpeedModule.get_xBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_xBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060004A5 RID: 1189
			// (set) Token: 0x060004A6 RID: 1190
			public extern float xMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00006071 File Offset: 0x00004271
			// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000607E File Offset: 0x0000427E
			public ParticleSystem.MinMaxCurve y
			{
				get
				{
					return this.yBlittable;
				}
				set
				{
					this.yBlittable = value;
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00006090 File Offset: 0x00004290
			// (set) Token: 0x060004AA RID: 1194 RVA: 0x000060A8 File Offset: 0x000042A8
			[NativeName("Y")]
			private ParticleSystem.MinMaxCurveBlittable yBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.RotationBySpeedModule.get_yBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_yBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x060004AB RID: 1195
			// (set) Token: 0x060004AC RID: 1196
			public extern float yMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x060004AD RID: 1197 RVA: 0x000060BD File Offset: 0x000042BD
			// (set) Token: 0x060004AE RID: 1198 RVA: 0x000060CA File Offset: 0x000042CA
			public ParticleSystem.MinMaxCurve z
			{
				get
				{
					return this.zBlittable;
				}
				set
				{
					this.zBlittable = value;
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x060004AF RID: 1199 RVA: 0x000060DC File Offset: 0x000042DC
			// (set) Token: 0x060004B0 RID: 1200 RVA: 0x000060F4 File Offset: 0x000042F4
			[NativeName("Z")]
			private ParticleSystem.MinMaxCurveBlittable zBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.RotationBySpeedModule.get_zBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_zBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x060004B1 RID: 1201
			// (set) Token: 0x060004B2 RID: 1202
			public extern float zMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x060004B3 RID: 1203
			// (set) Token: 0x060004B4 RID: 1204
			public extern bool separateAxes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000610C File Offset: 0x0000430C
			// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00006124 File Offset: 0x00004324
			public Vector2 range
			{
				get
				{
					Vector2 vector;
					ParticleSystem.RotationBySpeedModule.get_range_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.RotationBySpeedModule.set_range_Injected(ref this, ref value);
				}
			}

			// Token: 0x060004B7 RID: 1207
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_xBlittable_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060004B8 RID: 1208
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_xBlittable_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060004B9 RID: 1209
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_yBlittable_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060004BA RID: 1210
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_yBlittable_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060004BB RID: 1211
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_zBlittable_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060004BC RID: 1212
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_zBlittable_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060004BD RID: 1213
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, out Vector2 ret);

			// Token: 0x060004BE RID: 1214
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_range_Injected(ref ParticleSystem.RotationBySpeedModule _unity_self, [In] ref Vector2 value);

			// Token: 0x04000081 RID: 129
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002B RID: 43
		public struct ExternalForcesModule
		{
			// Token: 0x060004BF RID: 1215 RVA: 0x00006139 File Offset: 0x00004339
			internal ExternalForcesModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x060004C0 RID: 1216
			// (set) Token: 0x060004C1 RID: 1217
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x060004C2 RID: 1218
			// (set) Token: 0x060004C3 RID: 1219
			public extern float multiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00006143 File Offset: 0x00004343
			// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00006150 File Offset: 0x00004350
			public ParticleSystem.MinMaxCurve multiplierCurve
			{
				get
				{
					return this.multiplierCurveBlittable;
				}
				set
				{
					this.multiplierCurveBlittable = value;
				}
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00006160 File Offset: 0x00004360
			// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00006178 File Offset: 0x00004378
			[NativeName("MultiplierCurve")]
			private ParticleSystem.MinMaxCurveBlittable multiplierCurveBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.ExternalForcesModule.get_multiplierCurveBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_multiplierCurveBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x060004C8 RID: 1224
			// (set) Token: 0x060004C9 RID: 1225
			public extern ParticleSystemGameObjectFilter influenceFilter
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x060004CA RID: 1226 RVA: 0x00006190 File Offset: 0x00004390
			// (set) Token: 0x060004CB RID: 1227 RVA: 0x000061A8 File Offset: 0x000043A8
			public LayerMask influenceMask
			{
				get
				{
					LayerMask layerMask;
					ParticleSystem.ExternalForcesModule.get_influenceMask_Injected(ref this, out layerMask);
					return layerMask;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.ExternalForcesModule.set_influenceMask_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x060004CC RID: 1228
			[NativeThrows]
			public extern int influenceCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x000061C0 File Offset: 0x000043C0
			public bool IsAffectedBy(ParticleSystemForceField field)
			{
				return ParticleSystem.ExternalForcesModule.IsAffectedBy_Injected(ref this, Object.MarshalledUnityObject.Marshal<ParticleSystemForceField>(field));
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x000061DC File Offset: 0x000043DC
			[NativeThrows]
			public void AddInfluence([NotNull] ParticleSystemForceField field)
			{
				if (field == null)
				{
					ThrowHelper.ThrowArgumentNullException(field, "field");
				}
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(field);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(field, "field");
				}
				ParticleSystem.ExternalForcesModule.AddInfluence_Injected(ref this, intPtr);
			}

			// Token: 0x060004CF RID: 1231
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private extern void RemoveInfluenceAtIndex(int index);

			// Token: 0x060004D0 RID: 1232 RVA: 0x00006213 File Offset: 0x00004413
			public void RemoveInfluence(int index)
			{
				this.RemoveInfluenceAtIndex(index);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x00006220 File Offset: 0x00004420
			[NativeThrows]
			public void RemoveInfluence([NotNull] ParticleSystemForceField field)
			{
				if (field == null)
				{
					ThrowHelper.ThrowArgumentNullException(field, "field");
				}
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(field);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(field, "field");
				}
				ParticleSystem.ExternalForcesModule.RemoveInfluence_Injected(ref this, intPtr);
			}

			// Token: 0x060004D2 RID: 1234
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void RemoveAllInfluences();

			// Token: 0x060004D3 RID: 1235 RVA: 0x00006258 File Offset: 0x00004458
			[NativeThrows]
			public void SetInfluence(int index, [NotNull] ParticleSystemForceField field)
			{
				if (field == null)
				{
					ThrowHelper.ThrowArgumentNullException(field, "field");
				}
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemForceField>(field);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(field, "field");
				}
				ParticleSystem.ExternalForcesModule.SetInfluence_Injected(ref this, index, intPtr);
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x00006290 File Offset: 0x00004490
			[NativeThrows]
			public ParticleSystemForceField GetInfluence(int index)
			{
				return Unmarshal.UnmarshalUnityObject<ParticleSystemForceField>(ParticleSystem.ExternalForcesModule.GetInfluence_Injected(ref this, index));
			}

			// Token: 0x060004D5 RID: 1237
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_multiplierCurveBlittable_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060004D6 RID: 1238
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_multiplierCurveBlittable_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060004D7 RID: 1239
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, out LayerMask ret);

			// Token: 0x060004D8 RID: 1240
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_influenceMask_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, [In] ref LayerMask value);

			// Token: 0x060004D9 RID: 1241
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool IsAffectedBy_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, IntPtr field);

			// Token: 0x060004DA RID: 1242
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, IntPtr field);

			// Token: 0x060004DB RID: 1243
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, IntPtr field);

			// Token: 0x060004DC RID: 1244
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index, IntPtr field);

			// Token: 0x060004DD RID: 1245
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetInfluence_Injected(ref ParticleSystem.ExternalForcesModule _unity_self, int index);

			// Token: 0x04000082 RID: 130
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002C RID: 44
		public struct NoiseModule
		{
			// Token: 0x060004DE RID: 1246 RVA: 0x000062A9 File Offset: 0x000044A9
			internal NoiseModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x060004DF RID: 1247
			// (set) Token: 0x060004E0 RID: 1248
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x060004E1 RID: 1249
			// (set) Token: 0x060004E2 RID: 1250
			public extern bool separateAxes
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000062B3 File Offset: 0x000044B3
			// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000062C0 File Offset: 0x000044C0
			public ParticleSystem.MinMaxCurve strength
			{
				get
				{
					return this.strengthBlittable;
				}
				set
				{
					this.strengthBlittable = value;
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000062D0 File Offset: 0x000044D0
			// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000062E8 File Offset: 0x000044E8
			[NativeName("StrengthX")]
			private ParticleSystem.MinMaxCurveBlittable strengthBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_strengthBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x060004E7 RID: 1255
			// (set) Token: 0x060004E8 RID: 1256
			[NativeName("StrengthXMultiplier")]
			public extern float strengthMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000062FD File Offset: 0x000044FD
			// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000630A File Offset: 0x0000450A
			public ParticleSystem.MinMaxCurve strengthX
			{
				get
				{
					return this.strengthXBlittable;
				}
				set
				{
					this.strengthXBlittable = value;
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000631C File Offset: 0x0000451C
			// (set) Token: 0x060004EC RID: 1260 RVA: 0x00006334 File Offset: 0x00004534
			[NativeName("StrengthX")]
			private ParticleSystem.MinMaxCurveBlittable strengthXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_strengthXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x060004ED RID: 1261
			// (set) Token: 0x060004EE RID: 1262
			public extern float strengthXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x00006349 File Offset: 0x00004549
			// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00006356 File Offset: 0x00004556
			public ParticleSystem.MinMaxCurve strengthY
			{
				get
				{
					return this.strengthYBlittable;
				}
				set
				{
					this.strengthYBlittable = value;
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00006368 File Offset: 0x00004568
			// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00006380 File Offset: 0x00004580
			[NativeName("StrengthY")]
			private ParticleSystem.MinMaxCurveBlittable strengthYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_strengthYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x060004F3 RID: 1267
			// (set) Token: 0x060004F4 RID: 1268
			public extern float strengthYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00006395 File Offset: 0x00004595
			// (set) Token: 0x060004F6 RID: 1270 RVA: 0x000063A2 File Offset: 0x000045A2
			public ParticleSystem.MinMaxCurve strengthZ
			{
				get
				{
					return this.strengthZBlittable;
				}
				set
				{
					this.strengthZBlittable = value;
				}
			}

			// Token: 0x170001C7 RID: 455
			// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000063B4 File Offset: 0x000045B4
			// (set) Token: 0x060004F8 RID: 1272 RVA: 0x000063CC File Offset: 0x000045CC
			[NativeName("StrengthZ")]
			private ParticleSystem.MinMaxCurveBlittable strengthZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_strengthZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_strengthZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001C8 RID: 456
			// (get) Token: 0x060004F9 RID: 1273
			// (set) Token: 0x060004FA RID: 1274
			public extern float strengthZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001C9 RID: 457
			// (get) Token: 0x060004FB RID: 1275
			// (set) Token: 0x060004FC RID: 1276
			public extern float frequency
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001CA RID: 458
			// (get) Token: 0x060004FD RID: 1277
			// (set) Token: 0x060004FE RID: 1278
			public extern bool damping
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001CB RID: 459
			// (get) Token: 0x060004FF RID: 1279
			// (set) Token: 0x06000500 RID: 1280
			public extern int octaveCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x06000501 RID: 1281
			// (set) Token: 0x06000502 RID: 1282
			public extern float octaveMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001CD RID: 461
			// (get) Token: 0x06000503 RID: 1283
			// (set) Token: 0x06000504 RID: 1284
			public extern float octaveScale
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06000505 RID: 1285
			// (set) Token: 0x06000506 RID: 1286
			public extern ParticleSystemNoiseQuality quality
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06000507 RID: 1287 RVA: 0x000063E1 File Offset: 0x000045E1
			// (set) Token: 0x06000508 RID: 1288 RVA: 0x000063EE File Offset: 0x000045EE
			public ParticleSystem.MinMaxCurve scrollSpeed
			{
				get
				{
					return this.scrollSpeedBlittable;
				}
				set
				{
					this.scrollSpeedBlittable = value;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06000509 RID: 1289 RVA: 0x00006400 File Offset: 0x00004600
			// (set) Token: 0x0600050A RID: 1290 RVA: 0x00006418 File Offset: 0x00004618
			[NativeName("ScrollSpeed")]
			private ParticleSystem.MinMaxCurveBlittable scrollSpeedBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_scrollSpeedBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_scrollSpeedBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x0600050B RID: 1291
			// (set) Token: 0x0600050C RID: 1292
			public extern float scrollSpeedMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x0600050D RID: 1293
			// (set) Token: 0x0600050E RID: 1294
			public extern bool remapEnabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000642D File Offset: 0x0000462D
			// (set) Token: 0x06000510 RID: 1296 RVA: 0x0000643A File Offset: 0x0000463A
			public ParticleSystem.MinMaxCurve remap
			{
				get
				{
					return this.remapBlittable;
				}
				set
				{
					this.remapBlittable = value;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000644C File Offset: 0x0000464C
			// (set) Token: 0x06000512 RID: 1298 RVA: 0x00006464 File Offset: 0x00004664
			[NativeName("RemapX")]
			private ParticleSystem.MinMaxCurveBlittable remapBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_remapBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000513 RID: 1299
			// (set) Token: 0x06000514 RID: 1300
			[NativeName("RemapXMultiplier")]
			public extern float remapMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000515 RID: 1301 RVA: 0x00006479 File Offset: 0x00004679
			// (set) Token: 0x06000516 RID: 1302 RVA: 0x00006486 File Offset: 0x00004686
			public ParticleSystem.MinMaxCurve remapX
			{
				get
				{
					return this.remapXBlittable;
				}
				set
				{
					this.remapXBlittable = value;
				}
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06000517 RID: 1303 RVA: 0x00006498 File Offset: 0x00004698
			// (set) Token: 0x06000518 RID: 1304 RVA: 0x000064B0 File Offset: 0x000046B0
			[NativeName("RemapX")]
			private ParticleSystem.MinMaxCurveBlittable remapXBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_remapXBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapXBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06000519 RID: 1305
			// (set) Token: 0x0600051A RID: 1306
			public extern float remapXMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x0600051B RID: 1307 RVA: 0x000064C5 File Offset: 0x000046C5
			// (set) Token: 0x0600051C RID: 1308 RVA: 0x000064D2 File Offset: 0x000046D2
			public ParticleSystem.MinMaxCurve remapY
			{
				get
				{
					return this.remapYBlittable;
				}
				set
				{
					this.remapYBlittable = value;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x0600051D RID: 1309 RVA: 0x000064E4 File Offset: 0x000046E4
			// (set) Token: 0x0600051E RID: 1310 RVA: 0x000064FC File Offset: 0x000046FC
			[NativeName("RemapY")]
			private ParticleSystem.MinMaxCurveBlittable remapYBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_remapYBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapYBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x0600051F RID: 1311
			// (set) Token: 0x06000520 RID: 1312
			public extern float remapYMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000521 RID: 1313 RVA: 0x00006511 File Offset: 0x00004711
			// (set) Token: 0x06000522 RID: 1314 RVA: 0x0000651E File Offset: 0x0000471E
			public ParticleSystem.MinMaxCurve remapZ
			{
				get
				{
					return this.remapZBlittable;
				}
				set
				{
					this.remapZBlittable = value;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000523 RID: 1315 RVA: 0x00006530 File Offset: 0x00004730
			// (set) Token: 0x06000524 RID: 1316 RVA: 0x00006548 File Offset: 0x00004748
			[NativeName("RemapZ")]
			private ParticleSystem.MinMaxCurveBlittable remapZBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_remapZBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_remapZBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000525 RID: 1317
			// (set) Token: 0x06000526 RID: 1318
			public extern float remapZMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000655D File Offset: 0x0000475D
			// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000656A File Offset: 0x0000476A
			public ParticleSystem.MinMaxCurve positionAmount
			{
				get
				{
					return this.positionAmountBlittable;
				}
				set
				{
					this.positionAmountBlittable = value;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000657C File Offset: 0x0000477C
			// (set) Token: 0x0600052A RID: 1322 RVA: 0x00006594 File Offset: 0x00004794
			[NativeName("PositionAmount")]
			private ParticleSystem.MinMaxCurveBlittable positionAmountBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_positionAmountBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_positionAmountBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x0600052B RID: 1323 RVA: 0x000065A9 File Offset: 0x000047A9
			// (set) Token: 0x0600052C RID: 1324 RVA: 0x000065B6 File Offset: 0x000047B6
			public ParticleSystem.MinMaxCurve rotationAmount
			{
				get
				{
					return this.rotationAmountBlittable;
				}
				set
				{
					this.rotationAmountBlittable = value;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x0600052D RID: 1325 RVA: 0x000065C8 File Offset: 0x000047C8
			// (set) Token: 0x0600052E RID: 1326 RVA: 0x000065E0 File Offset: 0x000047E0
			[NativeName("RotationAmount")]
			private ParticleSystem.MinMaxCurveBlittable rotationAmountBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_rotationAmountBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_rotationAmountBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600052F RID: 1327 RVA: 0x000065F5 File Offset: 0x000047F5
			// (set) Token: 0x06000530 RID: 1328 RVA: 0x00006602 File Offset: 0x00004802
			public ParticleSystem.MinMaxCurve sizeAmount
			{
				get
				{
					return this.sizeAmountBlittable;
				}
				set
				{
					this.sizeAmountBlittable = value;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000531 RID: 1329 RVA: 0x00006614 File Offset: 0x00004814
			// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000662C File Offset: 0x0000482C
			[NativeName("SizeAmount")]
			private ParticleSystem.MinMaxCurveBlittable sizeAmountBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.NoiseModule.get_sizeAmountBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.NoiseModule.set_sizeAmountBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x06000533 RID: 1331
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000534 RID: 1332
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000535 RID: 1333
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthXBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000536 RID: 1334
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthXBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000537 RID: 1335
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthYBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000538 RID: 1336
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthYBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000539 RID: 1337
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_strengthZBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600053A RID: 1338
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_strengthZBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600053B RID: 1339
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_scrollSpeedBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600053C RID: 1340
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_scrollSpeedBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600053D RID: 1341
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600053E RID: 1342
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600053F RID: 1343
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapXBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000540 RID: 1344
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapXBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000541 RID: 1345
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapYBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000542 RID: 1346
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapYBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000543 RID: 1347
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_remapZBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000544 RID: 1348
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_remapZBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000545 RID: 1349
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_positionAmountBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000546 RID: 1350
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_positionAmountBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000547 RID: 1351
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rotationAmountBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x06000548 RID: 1352
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rotationAmountBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x06000549 RID: 1353
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_sizeAmountBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600054A RID: 1354
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_sizeAmountBlittable_Injected(ref ParticleSystem.NoiseModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x04000083 RID: 131
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002D RID: 45
		public struct LightsModule
		{
			// Token: 0x0600054B RID: 1355 RVA: 0x00006641 File Offset: 0x00004841
			internal LightsModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x0600054C RID: 1356
			// (set) Token: 0x0600054D RID: 1357
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x0600054E RID: 1358
			// (set) Token: 0x0600054F RID: 1359
			public extern float ratio
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000550 RID: 1360
			// (set) Token: 0x06000551 RID: 1361
			public extern bool useRandomDistribution
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000664C File Offset: 0x0000484C
			// (set) Token: 0x06000553 RID: 1363 RVA: 0x00006664 File Offset: 0x00004864
			public Light light
			{
				get
				{
					return Unmarshal.UnmarshalUnityObject<Light>(ParticleSystem.LightsModule.get_light_Injected(ref this));
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_light_Injected(ref this, Object.MarshalledUnityObject.Marshal<Light>(value));
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000554 RID: 1364
			// (set) Token: 0x06000555 RID: 1365
			public extern bool useParticleColor
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000556 RID: 1366
			// (set) Token: 0x06000557 RID: 1367
			public extern bool sizeAffectsRange
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000558 RID: 1368
			// (set) Token: 0x06000559 RID: 1369
			public extern bool alphaAffectsIntensity
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000667D File Offset: 0x0000487D
			// (set) Token: 0x0600055B RID: 1371 RVA: 0x0000668A File Offset: 0x0000488A
			public ParticleSystem.MinMaxCurve range
			{
				get
				{
					return this.rangeBlittable;
				}
				set
				{
					this.rangeBlittable = value;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000669C File Offset: 0x0000489C
			// (set) Token: 0x0600055D RID: 1373 RVA: 0x000066B4 File Offset: 0x000048B4
			[NativeName("Range")]
			private ParticleSystem.MinMaxCurveBlittable rangeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LightsModule.get_rangeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_rangeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600055E RID: 1374
			// (set) Token: 0x0600055F RID: 1375
			public extern float rangeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000560 RID: 1376 RVA: 0x000066C9 File Offset: 0x000048C9
			// (set) Token: 0x06000561 RID: 1377 RVA: 0x000066D6 File Offset: 0x000048D6
			public ParticleSystem.MinMaxCurve intensity
			{
				get
				{
					return this.intensityBlittable;
				}
				set
				{
					this.intensityBlittable = value;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000562 RID: 1378 RVA: 0x000066E8 File Offset: 0x000048E8
			// (set) Token: 0x06000563 RID: 1379 RVA: 0x00006700 File Offset: 0x00004900
			[NativeName("Intensity")]
			private ParticleSystem.MinMaxCurveBlittable intensityBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.LightsModule.get_intensityBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.LightsModule.set_intensityBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06000564 RID: 1380
			// (set) Token: 0x06000565 RID: 1381
			public extern float intensityMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000566 RID: 1382
			// (set) Token: 0x06000567 RID: 1383
			public extern int maxLights
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x06000568 RID: 1384
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr get_light_Injected(ref ParticleSystem.LightsModule _unity_self);

			// Token: 0x06000569 RID: 1385
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_light_Injected(ref ParticleSystem.LightsModule _unity_self, IntPtr value);

			// Token: 0x0600056A RID: 1386
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_rangeBlittable_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600056B RID: 1387
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_rangeBlittable_Injected(ref ParticleSystem.LightsModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x0600056C RID: 1388
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_intensityBlittable_Injected(ref ParticleSystem.LightsModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x0600056D RID: 1389
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_intensityBlittable_Injected(ref ParticleSystem.LightsModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x04000084 RID: 132
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002E RID: 46
		public struct TrailModule
		{
			// Token: 0x0600056E RID: 1390 RVA: 0x00006715 File Offset: 0x00004915
			internal TrailModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x0600056F RID: 1391
			// (set) Token: 0x06000570 RID: 1392
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06000571 RID: 1393
			// (set) Token: 0x06000572 RID: 1394
			public extern ParticleSystemTrailMode mode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06000573 RID: 1395
			// (set) Token: 0x06000574 RID: 1396
			public extern float ratio
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000671F File Offset: 0x0000491F
			// (set) Token: 0x06000576 RID: 1398 RVA: 0x0000672C File Offset: 0x0000492C
			public ParticleSystem.MinMaxCurve lifetime
			{
				get
				{
					return this.lifetimeBlittable;
				}
				set
				{
					this.lifetimeBlittable = value;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000673C File Offset: 0x0000493C
			// (set) Token: 0x06000578 RID: 1400 RVA: 0x00006754 File Offset: 0x00004954
			[NativeName("Lifetime")]
			private ParticleSystem.MinMaxCurveBlittable lifetimeBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.TrailModule.get_lifetimeBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_lifetimeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000579 RID: 1401
			// (set) Token: 0x0600057A RID: 1402
			public extern float lifetimeMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x0600057B RID: 1403
			// (set) Token: 0x0600057C RID: 1404
			public extern float minVertexDistance
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x0600057D RID: 1405
			// (set) Token: 0x0600057E RID: 1406
			public extern ParticleSystemTrailTextureMode textureMode
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000676C File Offset: 0x0000496C
			// (set) Token: 0x06000580 RID: 1408 RVA: 0x00006784 File Offset: 0x00004984
			public Vector2 textureScale
			{
				get
				{
					Vector2 vector;
					ParticleSystem.TrailModule.get_textureScale_Injected(ref this, out vector);
					return vector;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_textureScale_Injected(ref this, ref value);
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06000581 RID: 1409
			// (set) Token: 0x06000582 RID: 1410
			public extern bool worldSpace
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000583 RID: 1411
			// (set) Token: 0x06000584 RID: 1412
			public extern bool dieWithParticles
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000585 RID: 1413
			// (set) Token: 0x06000586 RID: 1414
			public extern bool sizeAffectsWidth
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000587 RID: 1415
			// (set) Token: 0x06000588 RID: 1416
			public extern bool sizeAffectsLifetime
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06000589 RID: 1417
			// (set) Token: 0x0600058A RID: 1418
			public extern bool inheritParticleColor
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x0600058B RID: 1419 RVA: 0x00006799 File Offset: 0x00004999
			// (set) Token: 0x0600058C RID: 1420 RVA: 0x000067A6 File Offset: 0x000049A6
			public ParticleSystem.MinMaxGradient colorOverLifetime
			{
				get
				{
					return this.colorOverLifetimeBlittable;
				}
				set
				{
					this.colorOverLifetimeBlittable = value;
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x0600058D RID: 1421 RVA: 0x000067B8 File Offset: 0x000049B8
			// (set) Token: 0x0600058E RID: 1422 RVA: 0x000067D0 File Offset: 0x000049D0
			[NativeName("ColorOverLifetime")]
			private ParticleSystem.MinMaxGradientBlittable colorOverLifetimeBlittable
			{
				get
				{
					ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable;
					ParticleSystem.TrailModule.get_colorOverLifetimeBlittable_Injected(ref this, out minMaxGradientBlittable);
					return minMaxGradientBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverLifetimeBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x0600058F RID: 1423 RVA: 0x000067E5 File Offset: 0x000049E5
			// (set) Token: 0x06000590 RID: 1424 RVA: 0x000067F2 File Offset: 0x000049F2
			public ParticleSystem.MinMaxCurve widthOverTrail
			{
				get
				{
					return this.widthOverTrailBlittable;
				}
				set
				{
					this.widthOverTrailBlittable = value;
				}
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000591 RID: 1425 RVA: 0x00006804 File Offset: 0x00004A04
			// (set) Token: 0x06000592 RID: 1426 RVA: 0x0000681C File Offset: 0x00004A1C
			[NativeName("WidthOverTrail")]
			private ParticleSystem.MinMaxCurveBlittable widthOverTrailBlittable
			{
				get
				{
					ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
					ParticleSystem.TrailModule.get_widthOverTrailBlittable_Injected(ref this, out minMaxCurveBlittable);
					return minMaxCurveBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_widthOverTrailBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000593 RID: 1427
			// (set) Token: 0x06000594 RID: 1428
			public extern float widthOverTrailMultiplier
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000595 RID: 1429 RVA: 0x00006831 File Offset: 0x00004A31
			// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000683E File Offset: 0x00004A3E
			public ParticleSystem.MinMaxGradient colorOverTrail
			{
				get
				{
					return this.colorOverTrailBlittable;
				}
				set
				{
					this.colorOverTrailBlittable = value;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x00006850 File Offset: 0x00004A50
			// (set) Token: 0x06000598 RID: 1432 RVA: 0x00006868 File Offset: 0x00004A68
			[NativeName("ColorOverTrail")]
			private ParticleSystem.MinMaxGradientBlittable colorOverTrailBlittable
			{
				get
				{
					ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable;
					ParticleSystem.TrailModule.get_colorOverTrailBlittable_Injected(ref this, out minMaxGradientBlittable);
					return minMaxGradientBlittable;
				}
				[NativeThrows]
				set
				{
					ParticleSystem.TrailModule.set_colorOverTrailBlittable_Injected(ref this, ref value);
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000599 RID: 1433
			// (set) Token: 0x0600059A RID: 1434
			public extern bool generateLightingData
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x0600059B RID: 1435
			// (set) Token: 0x0600059C RID: 1436
			public extern int ribbonCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x0600059D RID: 1437
			// (set) Token: 0x0600059E RID: 1438
			public extern float shadowBias
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x0600059F RID: 1439
			// (set) Token: 0x060005A0 RID: 1440
			public extern bool splitSubEmitterRibbons
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x060005A1 RID: 1441
			// (set) Token: 0x060005A2 RID: 1442
			public extern bool attachRibbonsToTransform
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x060005A3 RID: 1443
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_lifetimeBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060005A4 RID: 1444
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_lifetimeBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060005A5 RID: 1445
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_textureScale_Injected(ref ParticleSystem.TrailModule _unity_self, out Vector2 ret);

			// Token: 0x060005A6 RID: 1446
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_textureScale_Injected(ref ParticleSystem.TrailModule _unity_self, [In] ref Vector2 value);

			// Token: 0x060005A7 RID: 1447
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorOverLifetimeBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradientBlittable ret);

			// Token: 0x060005A8 RID: 1448
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorOverLifetimeBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, [In] ref ParticleSystem.MinMaxGradientBlittable value);

			// Token: 0x060005A9 RID: 1449
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_widthOverTrailBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060005AA RID: 1450
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_widthOverTrailBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, [In] ref ParticleSystem.MinMaxCurveBlittable value);

			// Token: 0x060005AB RID: 1451
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void get_colorOverTrailBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, out ParticleSystem.MinMaxGradientBlittable ret);

			// Token: 0x060005AC RID: 1452
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void set_colorOverTrailBlittable_Injected(ref ParticleSystem.TrailModule _unity_self, [In] ref ParticleSystem.MinMaxGradientBlittable value);

			// Token: 0x04000085 RID: 133
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x0200002F RID: 47
		public struct CustomDataModule
		{
			// Token: 0x060005AD RID: 1453 RVA: 0x0000687D File Offset: 0x00004A7D
			internal CustomDataModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x060005AE RID: 1454
			// (set) Token: 0x060005AF RID: 1455
			public extern bool enabled
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x060005B0 RID: 1456
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void SetMode(ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode);

			// Token: 0x060005B1 RID: 1457
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ParticleSystemCustomDataMode GetMode(ParticleSystemCustomData stream);

			// Token: 0x060005B2 RID: 1458
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern void SetVectorComponentCount(ParticleSystemCustomData stream, int count);

			// Token: 0x060005B3 RID: 1459
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern int GetVectorComponentCount(ParticleSystemCustomData stream);

			// Token: 0x060005B4 RID: 1460 RVA: 0x00006887 File Offset: 0x00004A87
			public void SetVector(ParticleSystemCustomData stream, int component, ParticleSystem.MinMaxCurve curve)
			{
				this.SetVectorInternal(stream, component, ParticleSystem.MinMaxCurveBlittable.FromMixMaxCurve(in curve));
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0000689C File Offset: 0x00004A9C
			[NativeThrows]
			private void SetVectorInternal(ParticleSystemCustomData stream, int component, ParticleSystem.MinMaxCurveBlittable curve)
			{
				ParticleSystem.CustomDataModule.SetVectorInternal_Injected(ref this, stream, component, ref curve);
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x000068B4 File Offset: 0x00004AB4
			public ParticleSystem.MinMaxCurve GetVector(ParticleSystemCustomData stream, int component)
			{
				ParticleSystem.MinMaxCurveBlittable vectorInternal = this.GetVectorInternal(stream, component);
				return ParticleSystem.MinMaxCurveBlittable.ToMinMaxCurve(in vectorInternal);
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x000068D8 File Offset: 0x00004AD8
			[NativeThrows]
			private ParticleSystem.MinMaxCurveBlittable GetVectorInternal(ParticleSystemCustomData stream, int component)
			{
				ParticleSystem.MinMaxCurveBlittable minMaxCurveBlittable;
				ParticleSystem.CustomDataModule.GetVectorInternal_Injected(ref this, stream, component, out minMaxCurveBlittable);
				return minMaxCurveBlittable;
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x000068F0 File Offset: 0x00004AF0
			public void SetColor(ParticleSystemCustomData stream, ParticleSystem.MinMaxGradient gradient)
			{
				this.SetColorInternal(stream, ParticleSystem.MinMaxGradientBlittable.FromMixMaxGradient(in gradient));
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x00006904 File Offset: 0x00004B04
			[NativeThrows]
			private void SetColorInternal(ParticleSystemCustomData stream, ParticleSystem.MinMaxGradientBlittable gradient)
			{
				ParticleSystem.CustomDataModule.SetColorInternal_Injected(ref this, stream, ref gradient);
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0000691C File Offset: 0x00004B1C
			public ParticleSystem.MinMaxGradient GetColor(ParticleSystemCustomData stream)
			{
				ParticleSystem.MinMaxGradientBlittable colorInternal = this.GetColorInternal(stream);
				return ParticleSystem.MinMaxGradientBlittable.ToMinMaxGradient(in colorInternal);
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x00006940 File Offset: 0x00004B40
			[NativeThrows]
			private ParticleSystem.MinMaxGradientBlittable GetColorInternal(ParticleSystemCustomData stream)
			{
				ParticleSystem.MinMaxGradientBlittable minMaxGradientBlittable;
				ParticleSystem.CustomDataModule.GetColorInternal_Injected(ref this, stream, out minMaxGradientBlittable);
				return minMaxGradientBlittable;
			}

			// Token: 0x060005BC RID: 1468
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVectorInternal_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, [In] ref ParticleSystem.MinMaxCurveBlittable curve);

			// Token: 0x060005BD RID: 1469
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetVectorInternal_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, out ParticleSystem.MinMaxCurveBlittable ret);

			// Token: 0x060005BE RID: 1470
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetColorInternal_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, [In] ref ParticleSystem.MinMaxGradientBlittable gradient);

			// Token: 0x060005BF RID: 1471
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetColorInternal_Injected(ref ParticleSystem.CustomDataModule _unity_self, ParticleSystemCustomData stream, out ParticleSystem.MinMaxGradientBlittable ret);

			// Token: 0x04000086 RID: 134
			internal ParticleSystem m_ParticleSystem;
		}
	}
}
