using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000184 RID: 388
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/TrailRenderer.h")]
	public sealed class TrailRenderer : Renderer
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00019E50 File Offset: 0x00018050
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00019E68 File Offset: 0x00018068
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00019E8C File Offset: 0x0001808C
		public float time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00019EB0 File Offset: 0x000180B0
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00019ED4 File Offset: 0x000180D4
		public float startWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_startWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_startWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00019EF8 File Offset: 0x000180F8
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00019F1C File Offset: 0x0001811C
		public float endWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_endWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_endWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00019F40 File Offset: 0x00018140
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00019F64 File Offset: 0x00018164
		public float widthMultiplier
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_widthMultiplier_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_widthMultiplier_Injected(intPtr, value);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x00019F88 File Offset: 0x00018188
		// (set) Token: 0x06000E71 RID: 3697 RVA: 0x00019FAC File Offset: 0x000181AC
		public bool autodestruct
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_autodestruct_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_autodestruct_Injected(intPtr, value);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00019FD0 File Offset: 0x000181D0
		// (set) Token: 0x06000E73 RID: 3699 RVA: 0x00019FF4 File Offset: 0x000181F4
		public bool emitting
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_emitting_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_emitting_Injected(intPtr, value);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0001A018 File Offset: 0x00018218
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0001A03C File Offset: 0x0001823C
		public int numCornerVertices
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_numCornerVertices_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_numCornerVertices_Injected(intPtr, value);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0001A060 File Offset: 0x00018260
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x0001A084 File Offset: 0x00018284
		public int numCapVertices
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_numCapVertices_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_numCapVertices_Injected(intPtr, value);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x0001A0A8 File Offset: 0x000182A8
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0001A0CC File Offset: 0x000182CC
		public float minVertexDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_minVertexDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_minVertexDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x0001A0F0 File Offset: 0x000182F0
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x0001A118 File Offset: 0x00018318
		public Color startColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				TrailRenderer.get_startColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_startColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0001A13C File Offset: 0x0001833C
		// (set) Token: 0x06000E7D RID: 3709 RVA: 0x0001A164 File Offset: 0x00018364
		public Color endColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				TrailRenderer.get_endColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_endColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0001A188 File Offset: 0x00018388
		[NativeProperty("PositionsCount")]
		public int positionCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_positionCount_Injected(intPtr);
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0001A1AC File Offset: 0x000183AC
		public void SetPosition(int index, Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TrailRenderer.SetPosition_Injected(intPtr, index, ref position);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0001A1D4 File Offset: 0x000183D4
		public Vector3 GetPosition(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			TrailRenderer.GetPosition_Injected(intPtr, index, out vector);
			return vector;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0001A1FC File Offset: 0x000183FC
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0001A224 File Offset: 0x00018424
		public Vector2 textureScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				TrailRenderer.get_textureScale_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_textureScale_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0001A248 File Offset: 0x00018448
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0001A26C File Offset: 0x0001846C
		public float shadowBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_shadowBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_shadowBias_Injected(intPtr, value);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0001A290 File Offset: 0x00018490
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public bool generateLightingData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_generateLightingData_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_generateLightingData_Injected(intPtr, value);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0001A2D8 File Offset: 0x000184D8
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0001A2FC File Offset: 0x000184FC
		public LineTextureMode textureMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_textureMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_textureMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0001A320 File Offset: 0x00018520
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0001A344 File Offset: 0x00018544
		public LineAlignment alignment
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_alignment_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_alignment_Injected(intPtr, value);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0001A368 File Offset: 0x00018568
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0001A38C File Offset: 0x0001858C
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TrailRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TrailRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0001A3B0 File Offset: 0x000185B0
		public void Clear()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TrailRenderer.Clear_Injected(intPtr);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0001A3D2 File Offset: 0x000185D2
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0001A3E4 File Offset: 0x000185E4
		public void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false)
		{
			if (mesh == null)
			{
				ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
			}
			if (camera == null)
			{
				ThrowHelper.ThrowArgumentNullException(camera, "camera");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
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
			TrailRenderer.BakeMesh_Injected(intPtr, intPtr2, intPtr3, useTransform);
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0001A450 File Offset: 0x00018650
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x0001A468 File Offset: 0x00018668
		public AnimationCurve widthCurve
		{
			get
			{
				return this.GetWidthCurveCopy();
			}
			set
			{
				this.SetWidthCurve(value);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0001A474 File Offset: 0x00018674
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0001A48C File Offset: 0x0001868C
		public Gradient colorGradient
		{
			get
			{
				return this.GetColorGradientCopy();
			}
			set
			{
				this.SetColorGradient(value);
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0001A498 File Offset: 0x00018698
		private AnimationCurve GetWidthCurveCopy()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr widthCurveCopy_Injected = TrailRenderer.GetWidthCurveCopy_Injected(intPtr);
			return (widthCurveCopy_Injected == 0) ? null : AnimationCurve.BindingsMarshaller.ConvertToManaged(widthCurveCopy_Injected);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0001A4C8 File Offset: 0x000186C8
		private void SetWidthCurve([NotNull] AnimationCurve curve)
		{
			if (curve == null)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = AnimationCurve.BindingsMarshaller.ConvertToNative(curve);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			TrailRenderer.SetWidthCurve_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0001A510 File Offset: 0x00018710
		private Gradient GetColorGradientCopy()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr colorGradientCopy_Injected = TrailRenderer.GetColorGradientCopy_Injected(intPtr);
			return (colorGradientCopy_Injected == 0) ? null : Gradient.BindingsMarshaller.ConvertToManaged(colorGradientCopy_Injected);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0001A540 File Offset: 0x00018740
		private void SetColorGradient([NotNull] Gradient curve)
		{
			if (curve == null)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Gradient.BindingsMarshaller.ConvertToNative(curve);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			TrailRenderer.SetColorGradient_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0001A588 File Offset: 0x00018788
		[FreeFunction(Name = "TrailRendererScripting::GetPositions", HasExplicitThis = true)]
		public unsafe int GetPositions([NotNull] [Out] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			int positions_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector3[] array = positions)
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					positions_Injected = TrailRenderer.GetPositions_Injected(intPtr, out blittableArrayWrapper);
				}
			}
			finally
			{
				Vector3[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
			}
			return positions_Injected;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0001A5F4 File Offset: 0x000187F4
		[FreeFunction(Name = "TrailRendererScripting::GetVisiblePositions", HasExplicitThis = true)]
		public unsafe int GetVisiblePositions([NotNull] [Out] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			int visiblePositions_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector3[] array = positions)
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					visiblePositions_Injected = TrailRenderer.GetVisiblePositions_Injected(intPtr, out blittableArrayWrapper);
				}
			}
			finally
			{
				Vector3[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
			}
			return visiblePositions_Injected;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0001A660 File Offset: 0x00018860
		[FreeFunction(Name = "TrailRendererScripting::SetPositions", HasExplicitThis = true)]
		public unsafe void SetPositions([NotNull] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3> span = new Span<Vector3>(positions);
			fixed (Vector3* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				TrailRenderer.SetPositions_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0001A6B8 File Offset: 0x000188B8
		[FreeFunction(Name = "TrailRendererScripting::AddPosition", HasExplicitThis = true)]
		public void AddPosition(Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TrailRenderer.AddPosition_Injected(intPtr, ref position);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0001A6DC File Offset: 0x000188DC
		[FreeFunction(Name = "TrailRendererScripting::AddPositions", HasExplicitThis = true)]
		public unsafe void AddPositions([NotNull] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3> span = new Span<Vector3>(positions);
			fixed (Vector3* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				TrailRenderer.AddPositions_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0001A733 File Offset: 0x00018933
		public void SetPositions(NativeArray<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0001A751 File Offset: 0x00018951
		public void SetPositions(NativeSlice<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0001A770 File Offset: 0x00018970
		public int GetPositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0001A79C File Offset: 0x0001899C
		public int GetPositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0001A7C8 File Offset: 0x000189C8
		public int GetVisiblePositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetVisiblePositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public int GetVisiblePositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetVisiblePositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0001A81F File Offset: 0x00018A1F
		public void AddPositions([Out] NativeArray<Vector3> positions)
		{
			this.AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0001A83D File Offset: 0x00018A3D
		public void AddPositions([Out] NativeSlice<Vector3> positions)
		{
			this.AddPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0001A85C File Offset: 0x00018A5C
		[FreeFunction(Name = "TrailRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
		private void SetPositionsWithNativeContainer(IntPtr positions, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TrailRenderer.SetPositionsWithNativeContainer_Injected(intPtr, positions, count);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0001A880 File Offset: 0x00018A80
		[FreeFunction(Name = "TrailRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
		private int GetPositionsWithNativeContainer(IntPtr positions, int length)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TrailRenderer.GetPositionsWithNativeContainer_Injected(intPtr, positions, length);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		[FreeFunction(Name = "TrailRendererScripting::GetVisiblePositionsWithNativeContainer", HasExplicitThis = true)]
		private int GetVisiblePositionsWithNativeContainer(IntPtr positions, int length)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TrailRenderer.GetVisiblePositionsWithNativeContainer_Injected(intPtr, positions, length);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		[FreeFunction(Name = "TrailRendererScripting::AddPositionsWithNativeContainer", HasExplicitThis = true)]
		private void AddPositionsWithNativeContainer(IntPtr positions, int length)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TrailRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TrailRenderer.AddPositionsWithNativeContainer_Injected(intPtr, positions, length);
		}

		// Token: 0x06000EAA RID: 3754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x06000EAB RID: 3755
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000EAC RID: 3756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_startWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000EAD RID: 3757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000EAE RID: 3758
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_endWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000EAF RID: 3759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000EB0 RID: 3760
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_widthMultiplier_Injected(IntPtr _unity_self);

		// Token: 0x06000EB1 RID: 3761
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_widthMultiplier_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000EB2 RID: 3762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autodestruct_Injected(IntPtr _unity_self);

		// Token: 0x06000EB3 RID: 3763
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autodestruct_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000EB4 RID: 3764
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_emitting_Injected(IntPtr _unity_self);

		// Token: 0x06000EB5 RID: 3765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_emitting_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000EB6 RID: 3766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_numCornerVertices_Injected(IntPtr _unity_self);

		// Token: 0x06000EB7 RID: 3767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_numCornerVertices_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000EB8 RID: 3768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_numCapVertices_Injected(IntPtr _unity_self);

		// Token: 0x06000EB9 RID: 3769
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_numCapVertices_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000EBA RID: 3770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minVertexDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000EBB RID: 3771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minVertexDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000EBC RID: 3772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_startColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000EBD RID: 3773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000EBE RID: 3774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_endColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000EBF RID: 3775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000EC0 RID: 3776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_positionCount_Injected(IntPtr _unity_self);

		// Token: 0x06000EC1 RID: 3777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPosition_Injected(IntPtr _unity_self, int index, [In] ref Vector3 position);

		// Token: 0x06000EC2 RID: 3778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition_Injected(IntPtr _unity_self, int index, out Vector3 ret);

		// Token: 0x06000EC3 RID: 3779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_textureScale_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000EC4 RID: 3780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textureScale_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000EC5 RID: 3781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowBias_Injected(IntPtr _unity_self);

		// Token: 0x06000EC6 RID: 3782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000EC7 RID: 3783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_generateLightingData_Injected(IntPtr _unity_self);

		// Token: 0x06000EC8 RID: 3784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_generateLightingData_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000EC9 RID: 3785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LineTextureMode get_textureMode_Injected(IntPtr _unity_self);

		// Token: 0x06000ECA RID: 3786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textureMode_Injected(IntPtr _unity_self, LineTextureMode value);

		// Token: 0x06000ECB RID: 3787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LineAlignment get_alignment_Injected(IntPtr _unity_self);

		// Token: 0x06000ECC RID: 3788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alignment_Injected(IntPtr _unity_self, LineAlignment value);

		// Token: 0x06000ECD RID: 3789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x06000ECE RID: 3790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x06000ECF RID: 3791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x06000ED0 RID: 3792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeMesh_Injected(IntPtr _unity_self, IntPtr mesh, IntPtr camera, bool useTransform);

		// Token: 0x06000ED1 RID: 3793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetWidthCurveCopy_Injected(IntPtr _unity_self);

		// Token: 0x06000ED2 RID: 3794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWidthCurve_Injected(IntPtr _unity_self, IntPtr curve);

		// Token: 0x06000ED3 RID: 3795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetColorGradientCopy_Injected(IntPtr _unity_self);

		// Token: 0x06000ED4 RID: 3796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorGradient_Injected(IntPtr _unity_self, IntPtr curve);

		// Token: 0x06000ED5 RID: 3797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPositions_Injected(IntPtr _unity_self, out BlittableArrayWrapper positions);

		// Token: 0x06000ED6 RID: 3798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVisiblePositions_Injected(IntPtr _unity_self, out BlittableArrayWrapper positions);

		// Token: 0x06000ED7 RID: 3799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositions_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions);

		// Token: 0x06000ED8 RID: 3800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddPosition_Injected(IntPtr _unity_self, [In] ref Vector3 position);

		// Token: 0x06000ED9 RID: 3801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddPositions_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions);

		// Token: 0x06000EDA RID: 3802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionsWithNativeContainer_Injected(IntPtr _unity_self, IntPtr positions, int count);

		// Token: 0x06000EDB RID: 3803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPositionsWithNativeContainer_Injected(IntPtr _unity_self, IntPtr positions, int length);

		// Token: 0x06000EDC RID: 3804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVisiblePositionsWithNativeContainer_Injected(IntPtr _unity_self, IntPtr positions, int length);

		// Token: 0x06000EDD RID: 3805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddPositionsWithNativeContainer_Injected(IntPtr _unity_self, IntPtr positions, int length);
	}
}
