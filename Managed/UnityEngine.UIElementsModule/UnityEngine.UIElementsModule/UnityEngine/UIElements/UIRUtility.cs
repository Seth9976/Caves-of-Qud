using System;
using System.Runtime.CompilerServices;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000336 RID: 822
	internal static class UIRUtility
	{
		// Token: 0x060017CF RID: 6095 RVA: 0x0005C250 File Offset: 0x0005A450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ShapeWindingIsClockwise(int maskDepth, int stencilRef)
		{
			Debug.Assert(maskDepth == stencilRef || maskDepth == stencilRef + 1);
			return maskDepth == stencilRef;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0005C27C File Offset: 0x0005A47C
		public static void GetVerticesTransformInfo(VisualElement ve, out Matrix4x4 transform)
		{
			bool flag = RenderChainVEData.AllocatesID(ve.renderChainData.transformID) || ve.renderChainData.isGroupTransform;
			if (flag)
			{
				transform = Matrix4x4.identity;
			}
			else
			{
				bool flag2 = ve.renderChainData.boneTransformAncestor != null;
				if (flag2)
				{
					bool worldTransformScaleZero = ve.renderChainData.boneTransformAncestor.renderChainData.worldTransformScaleZero;
					if (worldTransformScaleZero)
					{
						UIRUtility.ComputeTransformMatrix(ve, ve.renderChainData.boneTransformAncestor, out transform);
					}
					else
					{
						VisualElement.MultiplyMatrix34(ve.renderChainData.boneTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
					}
				}
				else
				{
					bool flag3 = ve.renderChainData.groupTransformAncestor != null;
					if (flag3)
					{
						bool worldTransformScaleZero2 = ve.renderChainData.groupTransformAncestor.renderChainData.worldTransformScaleZero;
						if (worldTransformScaleZero2)
						{
							UIRUtility.ComputeTransformMatrix(ve, ve.renderChainData.groupTransformAncestor, out transform);
						}
						else
						{
							VisualElement.MultiplyMatrix34(ve.renderChainData.groupTransformAncestor.worldTransformInverse, ve.worldTransformRef, out transform);
						}
					}
					else
					{
						transform = ve.worldTransform;
					}
				}
			}
			BaseVisualElementPanel elementPanel = ve.elementPanel;
			bool flag4 = elementPanel != null && elementPanel.isFlat;
			if (flag4)
			{
				transform.m22 = 1f;
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0005C3BC File Offset: 0x0005A5BC
		internal static void ComputeTransformMatrix(VisualElement ve, VisualElement ancestor, out Matrix4x4 result)
		{
			ve.GetPivotedMatrixWithLayout(out result);
			VisualElement visualElement = ve.hierarchy.parent;
			bool flag = visualElement == null || ancestor == visualElement;
			if (!flag)
			{
				Matrix4x4 matrix4x = default(Matrix4x4);
				bool flag2 = true;
				do
				{
					Matrix4x4 matrix4x2;
					visualElement.GetPivotedMatrixWithLayout(out matrix4x2);
					bool flag3 = flag2;
					if (flag3)
					{
						VisualElement.MultiplyMatrix34(ref matrix4x2, ref result, out matrix4x);
					}
					else
					{
						VisualElement.MultiplyMatrix34(ref matrix4x2, ref matrix4x, out result);
					}
					visualElement = visualElement.hierarchy.parent;
					flag2 = !flag2;
				}
				while (visualElement != null && ancestor != visualElement);
				bool flag4 = !flag2;
				if (flag4)
				{
					result = matrix4x;
				}
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005C460 File Offset: 0x0005A660
		public static Vector4 ToVector4(Rect rc)
		{
			return new Vector4(rc.xMin, rc.yMin, rc.xMax, rc.yMax);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0005C494 File Offset: 0x0005A694
		public static bool IsRoundRect(VisualElement ve)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			return resolvedStyle.borderTopLeftRadius >= 1E-30f || resolvedStyle.borderTopRightRadius >= 1E-30f || resolvedStyle.borderBottomLeftRadius >= 1E-30f || resolvedStyle.borderBottomRightRadius >= 1E-30f;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005C4E8 File Offset: 0x0005A6E8
		public static void Multiply2D(this Quaternion rotation, ref Vector2 point)
		{
			float num = rotation.z * 2f;
			float num2 = 1f - rotation.z * num;
			float num3 = rotation.w * num;
			point = new Vector2(num2 * point.x - num3 * point.y, num3 * point.x + num2 * point.y);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0005C548 File Offset: 0x0005A748
		public static bool IsVectorImageBackground(VisualElement ve)
		{
			return ve.computedStyle.backgroundImage.vectorImage != null;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0005C574 File Offset: 0x0005A774
		public static bool IsElementSelfHidden(VisualElement ve)
		{
			return ve.resolvedStyle.visibility == Visibility.Hidden;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0005C594 File Offset: 0x0005A794
		public static void Destroy(Object obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					Object.Destroy(obj);
				}
				else
				{
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0005C5C8 File Offset: 0x0005A7C8
		public static int GetPrevPow2(int n)
		{
			int num = 0;
			while (n > 1)
			{
				n >>= 1;
				num++;
			}
			return 1 << num;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0005C5F8 File Offset: 0x0005A7F8
		public static int GetNextPow2(int n)
		{
			int i;
			for (i = 1; i < n; i <<= 1)
			{
			}
			return i;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0005C61C File Offset: 0x0005A81C
		public static int GetNextPow2Exp(int n)
		{
			int i = 1;
			int num = 0;
			while (i < n)
			{
				i <<= 1;
				num++;
			}
			return num;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0005C648 File Offset: 0x0005A848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetThreadIndex()
		{
			int? num = UIRUtility.s_ThreadIndex;
			bool flag = num != null;
			int num2;
			if (flag)
			{
				num2 = num.Value;
			}
			else
			{
				int threadIndex = JobsUtility.ThreadIndex;
				UIRUtility.s_ThreadIndex = new int?(threadIndex);
				num2 = threadIndex;
			}
			return num2;
		}

		// Token: 0x04000D05 RID: 3333
		private static readonly ProfilerMarker k_ComputeTransformMatrixMarker = new ProfilerMarker("UIR.ComputeTransformMatrix");

		// Token: 0x04000D06 RID: 3334
		public static readonly string k_DefaultShaderName = Shaders.k_Runtime;

		// Token: 0x04000D07 RID: 3335
		public static readonly string k_DefaultWorldSpaceShaderName = Shaders.k_RuntimeWorld;

		// Token: 0x04000D08 RID: 3336
		public const float k_Epsilon = 1E-30f;

		// Token: 0x04000D09 RID: 3337
		public const float k_ClearZ = 0.99f;

		// Token: 0x04000D0A RID: 3338
		public const float k_MeshPosZ = 0f;

		// Token: 0x04000D0B RID: 3339
		public const float k_MaskPosZ = 1f;

		// Token: 0x04000D0C RID: 3340
		public const int k_MaxMaskDepth = 7;

		// Token: 0x04000D0D RID: 3341
		public const byte k_DynamicColorDisabled = 0;

		// Token: 0x04000D0E RID: 3342
		public const byte k_DynamicColorEnabled = 1;

		// Token: 0x04000D0F RID: 3343
		public const byte k_DynamicColorEnabledText = 2;

		// Token: 0x04000D10 RID: 3344
		[ThreadStatic]
		private static int? s_ThreadIndex;
	}
}
