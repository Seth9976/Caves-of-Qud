using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000185 RID: 389
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/LineRenderer.h")]
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x06000EDE RID: 3806 RVA: 0x0001A8EC File Offset: 0x00018AEC
		[Obsolete("Use startWidth, endWidth or widthCurve instead.", false)]
		public void SetWidth(float start, float end)
		{
			this.startWidth = start;
			this.endWidth = end;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0001A8FF File Offset: 0x00018AFF
		[Obsolete("Use startColor, endColor or colorGradient instead.", false)]
		public void SetColors(Color start, Color end)
		{
			this.startColor = start;
			this.endColor = end;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0001A912 File Offset: 0x00018B12
		[Obsolete("Use positionCount instead.", false)]
		public void SetVertexCount(int count)
		{
			this.positionCount = count;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0001A920 File Offset: 0x00018B20
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0001A912 File Offset: 0x00018B12
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
			set
			{
				this.positionCount = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0001A938 File Offset: 0x00018B38
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0001A95C File Offset: 0x00018B5C
		public float startWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_startWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_startWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0001A980 File Offset: 0x00018B80
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		public float endWidth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_endWidth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_endWidth_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0001A9EC File Offset: 0x00018BEC
		public float widthMultiplier
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_widthMultiplier_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_widthMultiplier_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0001AA10 File Offset: 0x00018C10
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0001AA34 File Offset: 0x00018C34
		public int numCornerVertices
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_numCornerVertices_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_numCornerVertices_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0001AA58 File Offset: 0x00018C58
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0001AA7C File Offset: 0x00018C7C
		public int numCapVertices
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_numCapVertices_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_numCapVertices_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0001AAA0 File Offset: 0x00018CA0
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public bool useWorldSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_useWorldSpace_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_useWorldSpace_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		// (set) Token: 0x06000EF0 RID: 3824 RVA: 0x0001AB0C File Offset: 0x00018D0C
		public bool loop
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_loop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_loop_Injected(intPtr, value);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0001AB30 File Offset: 0x00018D30
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x0001AB58 File Offset: 0x00018D58
		public Color startColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				LineRenderer.get_startColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_startColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0001AB7C File Offset: 0x00018D7C
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		public Color endColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				LineRenderer.get_endColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_endColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0001ABC8 File Offset: 0x00018DC8
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0001ABEC File Offset: 0x00018DEC
		[NativeProperty("PositionsCount")]
		public int positionCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_positionCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_positionCount_Injected(intPtr, value);
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0001AC10 File Offset: 0x00018E10
		public void SetPosition(int index, Vector3 position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LineRenderer.SetPosition_Injected(intPtr, index, ref position);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0001AC38 File Offset: 0x00018E38
		public Vector3 GetPosition(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			LineRenderer.GetPosition_Injected(intPtr, index, out vector);
			return vector;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0001AC60 File Offset: 0x00018E60
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x0001AC88 File Offset: 0x00018E88
		public Vector2 textureScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				LineRenderer.get_textureScale_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_textureScale_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x0001ACAC File Offset: 0x00018EAC
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0001ACD0 File Offset: 0x00018ED0
		public float shadowBias
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_shadowBias_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_shadowBias_Injected(intPtr, value);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0001AD18 File Offset: 0x00018F18
		public bool generateLightingData
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_generateLightingData_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_generateLightingData_Injected(intPtr, value);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0001AD3C File Offset: 0x00018F3C
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0001AD60 File Offset: 0x00018F60
		public LineTextureMode textureMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_textureMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_textureMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0001AD84 File Offset: 0x00018F84
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		public LineAlignment alignment
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_alignment_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_alignment_Injected(intPtr, value);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0001ADCC File Offset: 0x00018FCC
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		public SpriteMaskInteraction maskInteraction
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LineRenderer.get_maskInteraction_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_maskInteraction_Injected(intPtr, value);
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0001AE14 File Offset: 0x00019014
		public void Simplify(float tolerance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LineRenderer.Simplify_Injected(intPtr, tolerance);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0001AE37 File Offset: 0x00019037
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0001AE48 File Offset: 0x00019048
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
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
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
			LineRenderer.BakeMesh_Injected(intPtr, intPtr2, intPtr3, useTransform);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0001AEB4 File Offset: 0x000190B4
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0001AECC File Offset: 0x000190CC
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

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0001AED8 File Offset: 0x000190D8
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0001AEF0 File Offset: 0x000190F0
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

		// Token: 0x06000F0C RID: 3852 RVA: 0x0001AEFC File Offset: 0x000190FC
		private AnimationCurve GetWidthCurveCopy()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr widthCurveCopy_Injected = LineRenderer.GetWidthCurveCopy_Injected(intPtr);
			return (widthCurveCopy_Injected == 0) ? null : AnimationCurve.BindingsMarshaller.ConvertToManaged(widthCurveCopy_Injected);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0001AF2C File Offset: 0x0001912C
		private void SetWidthCurve([NotNull] AnimationCurve curve)
		{
			if (curve == null)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = AnimationCurve.BindingsMarshaller.ConvertToNative(curve);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			LineRenderer.SetWidthCurve_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0001AF74 File Offset: 0x00019174
		private Gradient GetColorGradientCopy()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr colorGradientCopy_Injected = LineRenderer.GetColorGradientCopy_Injected(intPtr);
			return (colorGradientCopy_Injected == 0) ? null : Gradient.BindingsMarshaller.ConvertToManaged(colorGradientCopy_Injected);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0001AFA4 File Offset: 0x000191A4
		private void SetColorGradient([NotNull] Gradient curve)
		{
			if (curve == null)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Gradient.BindingsMarshaller.ConvertToNative(curve);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(curve, "curve");
			}
			LineRenderer.SetColorGradient_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0001AFEC File Offset: 0x000191EC
		[FreeFunction(Name = "LineRendererScripting::GetPositions", HasExplicitThis = true)]
		public unsafe int GetPositions([NotNull] [Out] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			int positions_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
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
					positions_Injected = LineRenderer.GetPositions_Injected(intPtr, out blittableArrayWrapper);
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

		// Token: 0x06000F11 RID: 3857 RVA: 0x0001B058 File Offset: 0x00019258
		[FreeFunction(Name = "LineRendererScripting::SetPositions", HasExplicitThis = true)]
		public unsafe void SetPositions([NotNull] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3> span = new Span<Vector3>(positions);
			fixed (Vector3* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				LineRenderer.SetPositions_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0001B0AF File Offset: 0x000192AF
		public void SetPositions(NativeArray<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0001B0CD File Offset: 0x000192CD
		public void SetPositions(NativeSlice<Vector3> positions)
		{
			this.SetPositionsWithNativeContainer((IntPtr)positions.GetUnsafeReadOnlyPtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0001B0EC File Offset: 0x000192EC
		public int GetPositions([Out] NativeArray<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0001B118 File Offset: 0x00019318
		public int GetPositions([Out] NativeSlice<Vector3> positions)
		{
			return this.GetPositionsWithNativeContainer((IntPtr)positions.GetUnsafePtr<Vector3>(), positions.Length);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0001B144 File Offset: 0x00019344
		[FreeFunction(Name = "LineRendererScripting::SetPositionsWithNativeContainer", HasExplicitThis = true)]
		private void SetPositionsWithNativeContainer(IntPtr positions, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LineRenderer.SetPositionsWithNativeContainer_Injected(intPtr, positions, count);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0001B168 File Offset: 0x00019368
		[FreeFunction(Name = "LineRendererScripting::GetPositionsWithNativeContainer", HasExplicitThis = true)]
		private int GetPositionsWithNativeContainer(IntPtr positions, int length)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return LineRenderer.GetPositionsWithNativeContainer_Injected(intPtr, positions, length);
		}

		// Token: 0x06000F19 RID: 3865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_startWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000F1A RID: 3866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000F1B RID: 3867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_endWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000F1C RID: 3868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endWidth_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000F1D RID: 3869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_widthMultiplier_Injected(IntPtr _unity_self);

		// Token: 0x06000F1E RID: 3870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_widthMultiplier_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000F1F RID: 3871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_numCornerVertices_Injected(IntPtr _unity_self);

		// Token: 0x06000F20 RID: 3872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_numCornerVertices_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000F21 RID: 3873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_numCapVertices_Injected(IntPtr _unity_self);

		// Token: 0x06000F22 RID: 3874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_numCapVertices_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000F23 RID: 3875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useWorldSpace_Injected(IntPtr _unity_self);

		// Token: 0x06000F24 RID: 3876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useWorldSpace_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000F25 RID: 3877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loop_Injected(IntPtr _unity_self);

		// Token: 0x06000F26 RID: 3878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loop_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000F27 RID: 3879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_startColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000F28 RID: 3880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000F29 RID: 3881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_endColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x06000F2A RID: 3882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000F2B RID: 3883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_positionCount_Injected(IntPtr _unity_self);

		// Token: 0x06000F2C RID: 3884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_positionCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000F2D RID: 3885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPosition_Injected(IntPtr _unity_self, int index, [In] ref Vector3 position);

		// Token: 0x06000F2E RID: 3886
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPosition_Injected(IntPtr _unity_self, int index, out Vector3 ret);

		// Token: 0x06000F2F RID: 3887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_textureScale_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000F30 RID: 3888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textureScale_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000F31 RID: 3889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowBias_Injected(IntPtr _unity_self);

		// Token: 0x06000F32 RID: 3890
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowBias_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000F33 RID: 3891
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_generateLightingData_Injected(IntPtr _unity_self);

		// Token: 0x06000F34 RID: 3892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_generateLightingData_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000F35 RID: 3893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LineTextureMode get_textureMode_Injected(IntPtr _unity_self);

		// Token: 0x06000F36 RID: 3894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textureMode_Injected(IntPtr _unity_self, LineTextureMode value);

		// Token: 0x06000F37 RID: 3895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LineAlignment get_alignment_Injected(IntPtr _unity_self);

		// Token: 0x06000F38 RID: 3896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_alignment_Injected(IntPtr _unity_self, LineAlignment value);

		// Token: 0x06000F39 RID: 3897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteMaskInteraction get_maskInteraction_Injected(IntPtr _unity_self);

		// Token: 0x06000F3A RID: 3898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskInteraction_Injected(IntPtr _unity_self, SpriteMaskInteraction value);

		// Token: 0x06000F3B RID: 3899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Simplify_Injected(IntPtr _unity_self, float tolerance);

		// Token: 0x06000F3C RID: 3900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BakeMesh_Injected(IntPtr _unity_self, IntPtr mesh, IntPtr camera, bool useTransform);

		// Token: 0x06000F3D RID: 3901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetWidthCurveCopy_Injected(IntPtr _unity_self);

		// Token: 0x06000F3E RID: 3902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWidthCurve_Injected(IntPtr _unity_self, IntPtr curve);

		// Token: 0x06000F3F RID: 3903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetColorGradientCopy_Injected(IntPtr _unity_self);

		// Token: 0x06000F40 RID: 3904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorGradient_Injected(IntPtr _unity_self, IntPtr curve);

		// Token: 0x06000F41 RID: 3905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPositions_Injected(IntPtr _unity_self, out BlittableArrayWrapper positions);

		// Token: 0x06000F42 RID: 3906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositions_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions);

		// Token: 0x06000F43 RID: 3907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionsWithNativeContainer_Injected(IntPtr _unity_self, IntPtr positions, int count);

		// Token: 0x06000F44 RID: 3908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPositionsWithNativeContainer_Injected(IntPtr _unity_self, IntPtr positions, int length);
	}
}
