using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200005B RID: 91
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemRendererScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemRenderer.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	public sealed class ParticleSystemRenderer : Renderer
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00006AD3 File Offset: 0x00004CD3
		[Obsolete("EnableVertexStreams is deprecated. Use SetActiveVertexStreams instead.", false)]
		public void EnableVertexStreams(ParticleSystemVertexStreams streams)
		{
			this.Internal_SetVertexStreams(streams, true);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00006ADF File Offset: 0x00004CDF
		[Obsolete("DisableVertexStreams is deprecated. Use SetActiveVertexStreams instead.", false)]
		public void DisableVertexStreams(ParticleSystemVertexStreams streams)
		{
			this.Internal_SetVertexStreams(streams, false);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00006AEC File Offset: 0x00004CEC
		[Obsolete("AreVertexStreamsEnabled is deprecated. Use GetActiveVertexStreams instead.", false)]
		public bool AreVertexStreamsEnabled(ParticleSystemVertexStreams streams)
		{
			return this.Internal_GetEnabledVertexStreams(streams) == streams;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00006B08 File Offset: 0x00004D08
		[Obsolete("GetEnabledVertexStreams is deprecated. Use GetActiveVertexStreams instead.", false)]
		public ParticleSystemVertexStreams GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			return this.Internal_GetEnabledVertexStreams(streams);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00006B24 File Offset: 0x00004D24
		[Obsolete("Internal_SetVertexStreams is deprecated. Use SetActiveVertexStreams instead.", false)]
		internal void Internal_SetVertexStreams(ParticleSystemVertexStreams streams, bool enabled)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(this.activeVertexStreamsCount);
			this.GetActiveVertexStreams(list);
			if (enabled)
			{
				bool flag = (streams & ParticleSystemVertexStreams.Position) > ParticleSystemVertexStreams.None;
				if (flag)
				{
					bool flag2 = !list.Contains(ParticleSystemVertexStream.Position);
					if (flag2)
					{
						list.Add(ParticleSystemVertexStream.Position);
					}
				}
				bool flag3 = (streams & ParticleSystemVertexStreams.Normal) > ParticleSystemVertexStreams.None;
				if (flag3)
				{
					bool flag4 = !list.Contains(ParticleSystemVertexStream.Normal);
					if (flag4)
					{
						list.Add(ParticleSystemVertexStream.Normal);
					}
				}
				bool flag5 = (streams & ParticleSystemVertexStreams.Tangent) > ParticleSystemVertexStreams.None;
				if (flag5)
				{
					bool flag6 = !list.Contains(ParticleSystemVertexStream.Tangent);
					if (flag6)
					{
						list.Add(ParticleSystemVertexStream.Tangent);
					}
				}
				bool flag7 = (streams & ParticleSystemVertexStreams.Color) > ParticleSystemVertexStreams.None;
				if (flag7)
				{
					bool flag8 = !list.Contains(ParticleSystemVertexStream.Color);
					if (flag8)
					{
						list.Add(ParticleSystemVertexStream.Color);
					}
				}
				bool flag9 = (streams & ParticleSystemVertexStreams.UV) > ParticleSystemVertexStreams.None;
				if (flag9)
				{
					bool flag10 = !list.Contains(ParticleSystemVertexStream.UV);
					if (flag10)
					{
						list.Add(ParticleSystemVertexStream.UV);
					}
				}
				bool flag11 = (streams & ParticleSystemVertexStreams.UV2BlendAndFrame) > ParticleSystemVertexStreams.None;
				if (flag11)
				{
					bool flag12 = !list.Contains(ParticleSystemVertexStream.UV2);
					if (flag12)
					{
						list.Add(ParticleSystemVertexStream.UV2);
						list.Add(ParticleSystemVertexStream.AnimBlend);
						list.Add(ParticleSystemVertexStream.AnimFrame);
					}
				}
				bool flag13 = (streams & ParticleSystemVertexStreams.CenterAndVertexID) > ParticleSystemVertexStreams.None;
				if (flag13)
				{
					bool flag14 = !list.Contains(ParticleSystemVertexStream.Center);
					if (flag14)
					{
						list.Add(ParticleSystemVertexStream.Center);
						list.Add(ParticleSystemVertexStream.VertexID);
					}
				}
				bool flag15 = (streams & ParticleSystemVertexStreams.Size) > ParticleSystemVertexStreams.None;
				if (flag15)
				{
					bool flag16 = !list.Contains(ParticleSystemVertexStream.SizeXYZ);
					if (flag16)
					{
						list.Add(ParticleSystemVertexStream.SizeXYZ);
					}
				}
				bool flag17 = (streams & ParticleSystemVertexStreams.Rotation) > ParticleSystemVertexStreams.None;
				if (flag17)
				{
					bool flag18 = !list.Contains(ParticleSystemVertexStream.Rotation3D);
					if (flag18)
					{
						list.Add(ParticleSystemVertexStream.Rotation3D);
					}
				}
				bool flag19 = (streams & ParticleSystemVertexStreams.Velocity) > ParticleSystemVertexStreams.None;
				if (flag19)
				{
					bool flag20 = !list.Contains(ParticleSystemVertexStream.Velocity);
					if (flag20)
					{
						list.Add(ParticleSystemVertexStream.Velocity);
					}
				}
				bool flag21 = (streams & ParticleSystemVertexStreams.Lifetime) > ParticleSystemVertexStreams.None;
				if (flag21)
				{
					bool flag22 = !list.Contains(ParticleSystemVertexStream.AgePercent);
					if (flag22)
					{
						list.Add(ParticleSystemVertexStream.AgePercent);
						list.Add(ParticleSystemVertexStream.InvStartLifetime);
					}
				}
				bool flag23 = (streams & ParticleSystemVertexStreams.Custom1) > ParticleSystemVertexStreams.None;
				if (flag23)
				{
					bool flag24 = !list.Contains(ParticleSystemVertexStream.Custom1XYZW);
					if (flag24)
					{
						list.Add(ParticleSystemVertexStream.Custom1XYZW);
					}
				}
				bool flag25 = (streams & ParticleSystemVertexStreams.Custom2) > ParticleSystemVertexStreams.None;
				if (flag25)
				{
					bool flag26 = !list.Contains(ParticleSystemVertexStream.Custom2XYZW);
					if (flag26)
					{
						list.Add(ParticleSystemVertexStream.Custom2XYZW);
					}
				}
				bool flag27 = (streams & ParticleSystemVertexStreams.Random) > ParticleSystemVertexStreams.None;
				if (flag27)
				{
					bool flag28 = !list.Contains(ParticleSystemVertexStream.StableRandomXYZ);
					if (flag28)
					{
						list.Add(ParticleSystemVertexStream.StableRandomXYZ);
						list.Add(ParticleSystemVertexStream.VaryingRandomX);
					}
				}
			}
			else
			{
				bool flag29 = (streams & ParticleSystemVertexStreams.Position) > ParticleSystemVertexStreams.None;
				if (flag29)
				{
					list.Remove(ParticleSystemVertexStream.Position);
				}
				bool flag30 = (streams & ParticleSystemVertexStreams.Normal) > ParticleSystemVertexStreams.None;
				if (flag30)
				{
					list.Remove(ParticleSystemVertexStream.Normal);
				}
				bool flag31 = (streams & ParticleSystemVertexStreams.Tangent) > ParticleSystemVertexStreams.None;
				if (flag31)
				{
					list.Remove(ParticleSystemVertexStream.Tangent);
				}
				bool flag32 = (streams & ParticleSystemVertexStreams.Color) > ParticleSystemVertexStreams.None;
				if (flag32)
				{
					list.Remove(ParticleSystemVertexStream.Color);
				}
				bool flag33 = (streams & ParticleSystemVertexStreams.UV) > ParticleSystemVertexStreams.None;
				if (flag33)
				{
					list.Remove(ParticleSystemVertexStream.UV);
				}
				bool flag34 = (streams & ParticleSystemVertexStreams.UV2BlendAndFrame) > ParticleSystemVertexStreams.None;
				if (flag34)
				{
					list.Remove(ParticleSystemVertexStream.UV2);
					list.Remove(ParticleSystemVertexStream.AnimBlend);
					list.Remove(ParticleSystemVertexStream.AnimFrame);
				}
				bool flag35 = (streams & ParticleSystemVertexStreams.CenterAndVertexID) > ParticleSystemVertexStreams.None;
				if (flag35)
				{
					list.Remove(ParticleSystemVertexStream.Center);
					list.Remove(ParticleSystemVertexStream.VertexID);
				}
				bool flag36 = (streams & ParticleSystemVertexStreams.Size) > ParticleSystemVertexStreams.None;
				if (flag36)
				{
					list.Remove(ParticleSystemVertexStream.SizeXYZ);
				}
				bool flag37 = (streams & ParticleSystemVertexStreams.Rotation) > ParticleSystemVertexStreams.None;
				if (flag37)
				{
					list.Remove(ParticleSystemVertexStream.Rotation3D);
				}
				bool flag38 = (streams & ParticleSystemVertexStreams.Velocity) > ParticleSystemVertexStreams.None;
				if (flag38)
				{
					list.Remove(ParticleSystemVertexStream.Velocity);
				}
				bool flag39 = (streams & ParticleSystemVertexStreams.Lifetime) > ParticleSystemVertexStreams.None;
				if (flag39)
				{
					list.Remove(ParticleSystemVertexStream.AgePercent);
					list.Remove(ParticleSystemVertexStream.InvStartLifetime);
				}
				bool flag40 = (streams & ParticleSystemVertexStreams.Custom1) > ParticleSystemVertexStreams.None;
				if (flag40)
				{
					list.Remove(ParticleSystemVertexStream.Custom1XYZW);
				}
				bool flag41 = (streams & ParticleSystemVertexStreams.Custom2) > ParticleSystemVertexStreams.None;
				if (flag41)
				{
					list.Remove(ParticleSystemVertexStream.Custom2XYZW);
				}
				bool flag42 = (streams & ParticleSystemVertexStreams.Random) > ParticleSystemVertexStreams.None;
				if (flag42)
				{
					list.Remove(ParticleSystemVertexStream.StableRandomXYZW);
					list.Remove(ParticleSystemVertexStream.VaryingRandomX);
				}
			}
			this.SetActiveVertexStreams(list);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00006F70 File Offset: 0x00005170
		[Obsolete("Internal_GetVertexStreams is deprecated. Use GetActiveVertexStreams instead.", false)]
		internal ParticleSystemVertexStreams Internal_GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(this.activeVertexStreamsCount);
			this.GetActiveVertexStreams(list);
			ParticleSystemVertexStreams particleSystemVertexStreams = ParticleSystemVertexStreams.None;
			bool flag = list.Contains(ParticleSystemVertexStream.Position);
			if (flag)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Position;
			}
			bool flag2 = list.Contains(ParticleSystemVertexStream.Normal);
			if (flag2)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Normal;
			}
			bool flag3 = list.Contains(ParticleSystemVertexStream.Tangent);
			if (flag3)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Tangent;
			}
			bool flag4 = list.Contains(ParticleSystemVertexStream.Color);
			if (flag4)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Color;
			}
			bool flag5 = list.Contains(ParticleSystemVertexStream.UV);
			if (flag5)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV;
			}
			bool flag6 = list.Contains(ParticleSystemVertexStream.UV2);
			if (flag6)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV2BlendAndFrame;
			}
			bool flag7 = list.Contains(ParticleSystemVertexStream.Center);
			if (flag7)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.CenterAndVertexID;
			}
			bool flag8 = list.Contains(ParticleSystemVertexStream.SizeXYZ);
			if (flag8)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Size;
			}
			bool flag9 = list.Contains(ParticleSystemVertexStream.Rotation3D);
			if (flag9)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Rotation;
			}
			bool flag10 = list.Contains(ParticleSystemVertexStream.Velocity);
			if (flag10)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Velocity;
			}
			bool flag11 = list.Contains(ParticleSystemVertexStream.AgePercent);
			if (flag11)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Lifetime;
			}
			bool flag12 = list.Contains(ParticleSystemVertexStream.Custom1XYZW);
			if (flag12)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom1;
			}
			bool flag13 = list.Contains(ParticleSystemVertexStream.Custom2XYZW);
			if (flag13)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom2;
			}
			bool flag14 = list.Contains(ParticleSystemVertexStream.StableRandomXYZ);
			if (flag14)
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Random;
			}
			return particleSystemVertexStreams & streams;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000070AE File Offset: 0x000052AE
		[Obsolete("BakeMesh with useTransform is deprecated. Use BakeMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000070BF File Offset: 0x000052BF
		[Obsolete("BakeMesh with useTransform is deprecated. Use BakeMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeMesh(Mesh mesh, Camera camera, bool useTransform = false)
		{
			this.BakeMesh(mesh, camera, useTransform ? ParticleSystemBakeMeshOptions.BakeRotationAndScale : ParticleSystemBakeMeshOptions.Default);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000070D2 File Offset: 0x000052D2
		[Obsolete("BakeTrailsMesh with useTransform is deprecated. Use BakeTrailsMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeTrailsMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeTrailsMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000070E3 File Offset: 0x000052E3
		[Obsolete("BakeTrailsMesh with useTransform is deprecated. Use BakeTrailsMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeTrailsMesh(Mesh mesh, Camera camera, bool useTransform = false)
		{
			this.BakeTrailsMesh(mesh, camera, useTransform ? ParticleSystemBakeMeshOptions.BakeRotationAndScale : ParticleSystemBakeMeshOptions.Default);
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000070F8 File Offset: 0x000052F8
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0000711C File Offset: 0x0000531C
		[NativeName("RenderAlignment")]
		public ParticleSystemRenderSpace alignment
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_alignment_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_alignment_Injected(intPtr, value);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00007140 File Offset: 0x00005340
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00007164 File Offset: 0x00005364
		public ParticleSystemRenderMode renderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_renderMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_renderMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00007188 File Offset: 0x00005388
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x000071AC File Offset: 0x000053AC
		public ParticleSystemMeshDistribution meshDistribution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_meshDistribution_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_meshDistribution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000071D0 File Offset: 0x000053D0
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x000071F4 File Offset: 0x000053F4
		public ParticleSystemSortMode sortMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_sortMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_sortMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00007218 File Offset: 0x00005418
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0000723C File Offset: 0x0000543C
		public float lengthScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_lengthScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_lengthScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00007260 File Offset: 0x00005460
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00007284 File Offset: 0x00005484
		public float velocityScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_velocityScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_velocityScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000072A8 File Offset: 0x000054A8
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000072CC File Offset: 0x000054CC
		public float cameraVelocityScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_cameraVelocityScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_cameraVelocityScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000072F0 File Offset: 0x000054F0
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00007314 File Offset: 0x00005514
		public float normalDirection
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_normalDirection_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_normalDirection_Injected(intPtr, value);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00007338 File Offset: 0x00005538
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x0000735C File Offset: 0x0000555C
		public float shadowBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_shadowBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_shadowBias_Injected(intPtr, value);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00007380 File Offset: 0x00005580
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x000073A4 File Offset: 0x000055A4
		public float sortingFudge
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_sortingFudge_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_sortingFudge_Injected(intPtr, value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000073C8 File Offset: 0x000055C8
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000073EC File Offset: 0x000055EC
		public float minParticleSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_minParticleSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_minParticleSize_Injected(intPtr, value);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00007410 File Offset: 0x00005610
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00007434 File Offset: 0x00005634
		public float maxParticleSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_maxParticleSize_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_maxParticleSize_Injected(intPtr, value);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00007458 File Offset: 0x00005658
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00007480 File Offset: 0x00005680
		public Vector3 pivot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ParticleSystemRenderer.get_pivot_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_pivot_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000074A4 File Offset: 0x000056A4
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x000074CC File Offset: 0x000056CC
		public Vector3 flip
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ParticleSystemRenderer.get_flip_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_flip_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000074F0 File Offset: 0x000056F0
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00007514 File Offset: 0x00005714
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00007538 File Offset: 0x00005738
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00007560 File Offset: 0x00005760
		public Material trailMaterial
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Material>(ParticleSystemRenderer.get_trailMaterial_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_trailMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x1700021E RID: 542
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x00007588 File Offset: 0x00005788
		internal Material oldTrailMaterial
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_oldTrailMaterial_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Material>(value));
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x000075B0 File Offset: 0x000057B0
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x000075D4 File Offset: 0x000057D4
		public bool enableGPUInstancing
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_enableGPUInstancing_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_enableGPUInstancing_Injected(intPtr, value);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x000075F8 File Offset: 0x000057F8
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000761C File Offset: 0x0000581C
		public bool allowRoll
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_allowRoll_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_allowRoll_Injected(intPtr, value);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00007640 File Offset: 0x00005840
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00007664 File Offset: 0x00005864
		public bool freeformStretching
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_freeformStretching_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_freeformStretching_Injected(intPtr, value);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00007688 File Offset: 0x00005888
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x000076AC File Offset: 0x000058AC
		public bool rotateWithStretchDirection
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_rotateWithStretchDirection_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_rotateWithStretchDirection_Injected(intPtr, value);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000076D0 File Offset: 0x000058D0
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x000076F8 File Offset: 0x000058F8
		public Mesh mesh
		{
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMesh", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(ParticleSystemRenderer.get_mesh_Injected(intPtr));
			}
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMesh", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystemRenderer.set_mesh_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00007720 File Offset: 0x00005920
		[RequiredByNativeCode]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshes", HasExplicitThis = true)]
		public int GetMeshes([NotNull] [Out] Mesh[] meshes)
		{
			if (meshes == null)
			{
				ThrowHelper.ThrowArgumentNullException(meshes, "meshes");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ParticleSystemRenderer.GetMeshes_Injected(intPtr, meshes);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00007754 File Offset: 0x00005954
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMeshes", HasExplicitThis = true)]
		public void SetMeshes([NotNull] Mesh[] meshes, int size)
		{
			if (meshes == null)
			{
				ThrowHelper.ThrowArgumentNullException(meshes, "meshes");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystemRenderer.SetMeshes_Injected(intPtr, meshes, size);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00007787 File Offset: 0x00005987
		public void SetMeshes(Mesh[] meshes)
		{
			this.SetMeshes(meshes, meshes.Length);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00007798 File Offset: 0x00005998
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshWeightings", HasExplicitThis = true)]
		public unsafe int GetMeshWeightings([NotNull] [Out] float[] weightings)
		{
			if (weightings == null)
			{
				ThrowHelper.ThrowArgumentNullException(weightings, "weightings");
			}
			int meshWeightings_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (float[] array = weightings)
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					meshWeightings_Injected = ParticleSystemRenderer.GetMeshWeightings_Injected(intPtr, out blittableArrayWrapper);
				}
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
			return meshWeightings_Injected;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00007804 File Offset: 0x00005A04
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMeshWeightings", HasExplicitThis = true)]
		public unsafe void SetMeshWeightings([NotNull] float[] weightings, int size)
		{
			if (weightings == null)
			{
				ThrowHelper.ThrowArgumentNullException(weightings, "weightings");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(weightings);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ParticleSystemRenderer.SetMeshWeightings_Injected(intPtr, ref managedSpanWrapper, size);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000785C File Offset: 0x00005A5C
		public void SetMeshWeightings(float[] weightings)
		{
			this.SetMeshWeightings(weightings, weightings.Length);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000786C File Offset: 0x00005A6C
		public int meshCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_meshCount_Injected(intPtr);
			}
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000788E File Offset: 0x00005A8E
		public void BakeMesh(Mesh mesh, ParticleSystemBakeMeshOptions options)
		{
			this.BakeMesh(mesh, Camera.main, options);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000078A0 File Offset: 0x00005AA0
		public void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, ParticleSystemBakeMeshOptions options)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ParticleSystemRenderer.BakeMesh_Injected(intPtr, intPtr2, intPtr3, options);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000790B File Offset: 0x00005B0B
		public void BakeTrailsMesh(Mesh mesh, ParticleSystemBakeMeshOptions options)
		{
			this.BakeTrailsMesh(mesh, Camera.main, options);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000791C File Offset: 0x00005B1C
		public void BakeTrailsMesh([NotNull] Mesh mesh, [NotNull] Camera camera, ParticleSystemBakeMeshOptions options)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ParticleSystemRenderer.BakeTrailsMesh_Injected(intPtr, intPtr2, intPtr3, options);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00007988 File Offset: 0x00005B88
		public int BakeTexture(ref Texture2D verticesTexture, ParticleSystemBakeTextureOptions options)
		{
			return this.BakeTexture(ref verticesTexture, Camera.main, options);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000079A8 File Offset: 0x00005BA8
		public int BakeTexture(ref Texture2D verticesTexture, Camera camera, ParticleSystemBakeTextureOptions options)
		{
			bool flag = this.renderMode == ParticleSystemRenderMode.Mesh;
			if (flag)
			{
				throw new InvalidOperationException("Baking mesh particles to texture requires supplying an indices texture");
			}
			int num;
			verticesTexture = this.BakeTextureNoIndicesInternal(verticesTexture, camera, options, out num);
			return num;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000079E4 File Offset: 0x00005BE4
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::BakeTextureNoIndices", HasExplicitThis = true)]
		private Texture2D BakeTextureNoIndicesInternal(Texture2D verticesTexture, [NotNull] Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Texture2D>(verticesTexture);
			IntPtr intPtr3 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr3 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(ParticleSystemRenderer.BakeTextureNoIndicesInternal_Injected(intPtr, intPtr2, intPtr3, options, out indexCount));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00007A38 File Offset: 0x00005C38
		public int BakeTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, ParticleSystemBakeTextureOptions options)
		{
			return this.BakeTexture(ref verticesTexture, ref indicesTexture, Camera.main, options);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00007A58 File Offset: 0x00005C58
		public int BakeTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, Camera camera, ParticleSystemBakeTextureOptions options)
		{
			int num;
			ParticleSystemRenderer.BakeTextureOutput bakeTextureOutput = this.BakeTextureInternal(verticesTexture, indicesTexture, camera, options, out num);
			verticesTexture = bakeTextureOutput.vertices;
			indicesTexture = bakeTextureOutput.indices;
			return num;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00007A8C File Offset: 0x00005C8C
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::BakeTexture", HasExplicitThis = true)]
		private ParticleSystemRenderer.BakeTextureOutput BakeTextureInternal(Texture2D verticesTexture, Texture2D indicesTexture, [NotNull] Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Texture2D>(verticesTexture);
			IntPtr intPtr3 = Object.MarshalledUnityObject.Marshal<Texture2D>(indicesTexture);
			IntPtr intPtr4 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr4 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ParticleSystemRenderer.BakeTextureOutput bakeTextureOutput;
			ParticleSystemRenderer.BakeTextureInternal_Injected(intPtr, intPtr2, intPtr3, intPtr4, options, out indexCount, out bakeTextureOutput);
			return bakeTextureOutput;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public int BakeTrailsTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, ParticleSystemBakeTextureOptions options)
		{
			return this.BakeTrailsTexture(ref verticesTexture, ref indicesTexture, Camera.main, options);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00007B08 File Offset: 0x00005D08
		public int BakeTrailsTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, Camera camera, ParticleSystemBakeTextureOptions options)
		{
			int num;
			ParticleSystemRenderer.BakeTextureOutput bakeTextureOutput = this.BakeTrailsTextureInternal(verticesTexture, indicesTexture, camera, options, out num);
			verticesTexture = bakeTextureOutput.vertices;
			indicesTexture = bakeTextureOutput.indices;
			return num;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00007B3C File Offset: 0x00005D3C
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::BakeTrailsTexture", HasExplicitThis = true)]
		private ParticleSystemRenderer.BakeTextureOutput BakeTrailsTextureInternal(Texture2D verticesTexture, Texture2D indicesTexture, [NotNull] Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount)
		{
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.Marshal<Texture2D>(verticesTexture);
			IntPtr intPtr3 = Object.MarshalledUnityObject.Marshal<Texture2D>(indicesTexture);
			IntPtr intPtr4 = Object.MarshalledUnityObject.MarshalNotNull<Camera>(camera);
			if (intPtr4 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			ParticleSystemRenderer.BakeTextureOutput bakeTextureOutput;
			ParticleSystemRenderer.BakeTrailsTextureInternal_Injected(intPtr, intPtr2, intPtr3, intPtr4, options, out indexCount, out bakeTextureOutput);
			return bakeTextureOutput;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00007B98 File Offset: 0x00005D98
		public int activeVertexStreamsCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_activeVertexStreamsCount_Injected(intPtr);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00007BBC File Offset: 0x00005DBC
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetActiveVertexStreams", HasExplicitThis = true)]
		public unsafe void SetActiveVertexStreams([NotNull] List<ParticleSystemVertexStream> streams)
		{
			if (streams == null)
			{
				ThrowHelper.ThrowArgumentNullException(streams, "streams");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (ParticleSystemVertexStream[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystemVertexStream>(streams))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, streams.Count);
					ParticleSystemRenderer.SetActiveVertexStreams_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystemVertexStream>(streams);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00007C38 File Offset: 0x00005E38
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetActiveVertexStreams", HasExplicitThis = true)]
		public unsafe void GetActiveVertexStreams([NotNull] List<ParticleSystemVertexStream> streams)
		{
			if (streams == null)
			{
				ThrowHelper.ThrowArgumentNullException(streams, "streams");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (ParticleSystemVertexStream[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystemVertexStream>(streams))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, streams.Count);
					ParticleSystemRenderer.GetActiveVertexStreams_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystemVertexStream>(streams);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public int activeTrailVertexStreamsCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystemRenderer.get_activeTrailVertexStreamsCount_Injected(intPtr);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00007CD8 File Offset: 0x00005ED8
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetActiveTrailVertexStreams", HasExplicitThis = true)]
		public unsafe void SetActiveTrailVertexStreams([NotNull] List<ParticleSystemVertexStream> streams)
		{
			if (streams == null)
			{
				ThrowHelper.ThrowArgumentNullException(streams, "streams");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (ParticleSystemVertexStream[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystemVertexStream>(streams))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, streams.Count);
					ParticleSystemRenderer.SetActiveTrailVertexStreams_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystemVertexStream>(streams);
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00007D54 File Offset: 0x00005F54
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetActiveTrailVertexStreams", HasExplicitThis = true)]
		public unsafe void GetActiveTrailVertexStreams([NotNull] List<ParticleSystemVertexStream> streams)
		{
			if (streams == null)
			{
				ThrowHelper.ThrowArgumentNullException(streams, "streams");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystemRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (ParticleSystemVertexStream[] array = NoAllocHelpers.ExtractArrayFromList<ParticleSystemVertexStream>(streams))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, streams.Count);
					ParticleSystemRenderer.GetActiveTrailVertexStreams_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ParticleSystemVertexStream>(streams);
			}
		}

		// Token: 0x06000618 RID: 1560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParticleSystemRenderSpace get_alignment_Injected(IntPtr _unity_self);

		// Token: 0x06000619 RID: 1561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alignment_Injected(IntPtr _unity_self, ParticleSystemRenderSpace value);

		// Token: 0x0600061A RID: 1562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParticleSystemRenderMode get_renderMode_Injected(IntPtr _unity_self);

		// Token: 0x0600061B RID: 1563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderMode_Injected(IntPtr _unity_self, ParticleSystemRenderMode value);

		// Token: 0x0600061C RID: 1564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParticleSystemMeshDistribution get_meshDistribution_Injected(IntPtr _unity_self);

		// Token: 0x0600061D RID: 1565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_meshDistribution_Injected(IntPtr _unity_self, ParticleSystemMeshDistribution value);

		// Token: 0x0600061E RID: 1566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParticleSystemSortMode get_sortMode_Injected(IntPtr _unity_self);

		// Token: 0x0600061F RID: 1567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortMode_Injected(IntPtr _unity_self, ParticleSystemSortMode value);

		// Token: 0x06000620 RID: 1568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_lengthScale_Injected(IntPtr _unity_self);

		// Token: 0x06000621 RID: 1569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lengthScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000622 RID: 1570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_velocityScale_Injected(IntPtr _unity_self);

		// Token: 0x06000623 RID: 1571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocityScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000624 RID: 1572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_cameraVelocityScale_Injected(IntPtr _unity_self);

		// Token: 0x06000625 RID: 1573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cameraVelocityScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000626 RID: 1574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_normalDirection_Injected(IntPtr _unity_self);

		// Token: 0x06000627 RID: 1575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_normalDirection_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000628 RID: 1576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowBias_Injected(IntPtr _unity_self);

		// Token: 0x06000629 RID: 1577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600062A RID: 1578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_sortingFudge_Injected(IntPtr _unity_self);

		// Token: 0x0600062B RID: 1579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingFudge_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600062C RID: 1580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minParticleSize_Injected(IntPtr _unity_self);

		// Token: 0x0600062D RID: 1581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minParticleSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600062E RID: 1582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxParticleSize_Injected(IntPtr _unity_self);

		// Token: 0x0600062F RID: 1583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxParticleSize_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000630 RID: 1584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivot_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000631 RID: 1585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pivot_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000632 RID: 1586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_flip_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000633 RID: 1587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_flip_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000634 RID: 1588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x06000635 RID: 1589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x06000636 RID: 1590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_trailMaterial_Injected(IntPtr _unity_self);

		// Token: 0x06000637 RID: 1591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_trailMaterial_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000638 RID: 1592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_oldTrailMaterial_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000639 RID: 1593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableGPUInstancing_Injected(IntPtr _unity_self);

		// Token: 0x0600063A RID: 1594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableGPUInstancing_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600063B RID: 1595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowRoll_Injected(IntPtr _unity_self);

		// Token: 0x0600063C RID: 1596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowRoll_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600063D RID: 1597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_freeformStretching_Injected(IntPtr _unity_self);

		// Token: 0x0600063E RID: 1598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_freeformStretching_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600063F RID: 1599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_rotateWithStretchDirection_Injected(IntPtr _unity_self);

		// Token: 0x06000640 RID: 1600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotateWithStretchDirection_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000641 RID: 1601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_mesh_Injected(IntPtr _unity_self);

		// Token: 0x06000642 RID: 1602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mesh_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000643 RID: 1603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMeshes_Injected(IntPtr _unity_self, [Out] Mesh[] meshes);

		// Token: 0x06000644 RID: 1604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMeshes_Injected(IntPtr _unity_self, Mesh[] meshes, int size);

		// Token: 0x06000645 RID: 1605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMeshWeightings_Injected(IntPtr _unity_self, out BlittableArrayWrapper weightings);

		// Token: 0x06000646 RID: 1606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMeshWeightings_Injected(IntPtr _unity_self, ref ManagedSpanWrapper weightings, int size);

		// Token: 0x06000647 RID: 1607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_meshCount_Injected(IntPtr _unity_self);

		// Token: 0x06000648 RID: 1608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeMesh_Injected(IntPtr _unity_self, IntPtr mesh, IntPtr camera, ParticleSystemBakeMeshOptions options);

		// Token: 0x06000649 RID: 1609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeTrailsMesh_Injected(IntPtr _unity_self, IntPtr mesh, IntPtr camera, ParticleSystemBakeMeshOptions options);

		// Token: 0x0600064A RID: 1610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr BakeTextureNoIndicesInternal_Injected(IntPtr _unity_self, IntPtr verticesTexture, IntPtr camera, ParticleSystemBakeTextureOptions options, out int indexCount);

		// Token: 0x0600064B RID: 1611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeTextureInternal_Injected(IntPtr _unity_self, IntPtr verticesTexture, IntPtr indicesTexture, IntPtr camera, ParticleSystemBakeTextureOptions options, out int indexCount, out ParticleSystemRenderer.BakeTextureOutput ret);

		// Token: 0x0600064C RID: 1612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeTrailsTextureInternal_Injected(IntPtr _unity_self, IntPtr verticesTexture, IntPtr indicesTexture, IntPtr camera, ParticleSystemBakeTextureOptions options, out int indexCount, out ParticleSystemRenderer.BakeTextureOutput ret);

		// Token: 0x0600064D RID: 1613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_activeVertexStreamsCount_Injected(IntPtr _unity_self);

		// Token: 0x0600064E RID: 1614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActiveVertexStreams_Injected(IntPtr _unity_self, ref BlittableListWrapper streams);

		// Token: 0x0600064F RID: 1615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveVertexStreams_Injected(IntPtr _unity_self, ref BlittableListWrapper streams);

		// Token: 0x06000650 RID: 1616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_activeTrailVertexStreamsCount_Injected(IntPtr _unity_self);

		// Token: 0x06000651 RID: 1617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActiveTrailVertexStreams_Injected(IntPtr _unity_self, ref BlittableListWrapper streams);

		// Token: 0x06000652 RID: 1618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveTrailVertexStreams_Injected(IntPtr _unity_self, ref BlittableListWrapper streams);

		// Token: 0x0200005C RID: 92
		internal struct BakeTextureOutput
		{
			// Token: 0x04000193 RID: 403
			[NativeName("first")]
			internal Texture2D vertices;

			// Token: 0x04000194 RID: 404
			[NativeName("second")]
			internal Texture2D indices;
		}
	}
}
