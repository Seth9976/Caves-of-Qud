using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200052C RID: 1324
	[UsedByNativeCode]
	public struct CameraProperties : IEquatable<CameraProperties>
	{
		// Token: 0x060032C7 RID: 12999 RVA: 0x00062974 File Offset: 0x00060B74
		public unsafe Plane GetShadowCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_ShadowCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000629E0 File Offset: 0x00060BE0
		public unsafe void SetShadowCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_ShadowCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00062A4C File Offset: 0x00060C4C
		public unsafe Plane GetCameraCullingPlane(int index)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_CameraCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x00062AB8 File Offset: 0x00060CB8
		public unsafe void SetCameraCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= 6;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, 6));
			}
			fixed (byte* ptr = &this.m_CameraCullPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x00062B24 File Offset: 0x00060D24
		public unsafe bool Equals(CameraProperties other)
		{
			for (int i = 0; i < 6; i++)
			{
				bool flag = !this.GetShadowCullingPlane(i).Equals(other.GetShadowCullingPlane(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < 6; j++)
			{
				bool flag2 = !this.GetCameraCullingPlane(j).Equals(other.GetCameraCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				for (int k = 0; k < 32; k++)
				{
					bool flag3 = ptr2[k] != *((ref other.layerCullDistances.FixedElementField) + (IntPtr)k * 4);
					if (flag3)
					{
						return false;
					}
				}
			}
			return this.screenRect.Equals(other.screenRect) && this.viewDir.Equals(other.viewDir) && this.projectionNear.Equals(other.projectionNear) && this.projectionFar.Equals(other.projectionFar) && this.cameraNear.Equals(other.cameraNear) && this.cameraFar.Equals(other.cameraFar) && this.cameraAspect.Equals(other.cameraAspect) && this.cameraToWorld.Equals(other.cameraToWorld) && this.actualWorldToClip.Equals(other.actualWorldToClip) && this.cameraClipToWorld.Equals(other.cameraClipToWorld) && this.cameraWorldToClip.Equals(other.cameraWorldToClip) && this.implicitProjection.Equals(other.implicitProjection) && this.stereoWorldToClipLeft.Equals(other.stereoWorldToClipLeft) && this.stereoWorldToClipRight.Equals(other.stereoWorldToClipRight) && this.worldToCamera.Equals(other.worldToCamera) && this.up.Equals(other.up) && this.right.Equals(other.right) && this.transformDirection.Equals(other.transformDirection) && this.cameraEuler.Equals(other.cameraEuler) && this.velocity.Equals(other.velocity) && this.farPlaneWorldSpaceLength.Equals(other.farPlaneWorldSpaceLength) && this.rendererCount == other.rendererCount && this.baseFarDistance.Equals(other.baseFarDistance) && this.shadowCullCenter.Equals(other.shadowCullCenter) && this.layerCullSpherical == other.layerCullSpherical && this.coreCameraValues.Equals(other.coreCameraValues) && this.cameraType == other.cameraType && this.projectionIsOblique == other.projectionIsOblique && this.isImplicitProjectionMatrix == other.isImplicitProjectionMatrix;
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x00062E78 File Offset: 0x00061078
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is CameraProperties && this.Equals((CameraProperties)obj);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x00062EB0 File Offset: 0x000610B0
		public unsafe override int GetHashCode()
		{
			int num = this.screenRect.GetHashCode();
			num = (num * 397) ^ this.viewDir.GetHashCode();
			num = (num * 397) ^ this.projectionNear.GetHashCode();
			num = (num * 397) ^ this.projectionFar.GetHashCode();
			num = (num * 397) ^ this.cameraNear.GetHashCode();
			num = (num * 397) ^ this.cameraFar.GetHashCode();
			num = (num * 397) ^ this.cameraAspect.GetHashCode();
			num = (num * 397) ^ this.cameraToWorld.GetHashCode();
			num = (num * 397) ^ this.actualWorldToClip.GetHashCode();
			num = (num * 397) ^ this.cameraClipToWorld.GetHashCode();
			num = (num * 397) ^ this.cameraWorldToClip.GetHashCode();
			num = (num * 397) ^ this.implicitProjection.GetHashCode();
			num = (num * 397) ^ this.stereoWorldToClipLeft.GetHashCode();
			num = (num * 397) ^ this.stereoWorldToClipRight.GetHashCode();
			num = (num * 397) ^ this.worldToCamera.GetHashCode();
			num = (num * 397) ^ this.up.GetHashCode();
			num = (num * 397) ^ this.right.GetHashCode();
			num = (num * 397) ^ this.transformDirection.GetHashCode();
			num = (num * 397) ^ this.cameraEuler.GetHashCode();
			num = (num * 397) ^ this.velocity.GetHashCode();
			num = (num * 397) ^ this.farPlaneWorldSpaceLength.GetHashCode();
			num = (num * 397) ^ (int)this.rendererCount;
			for (int i = 0; i < 6; i++)
			{
				num = (num * 397) ^ this.GetShadowCullingPlane(i).GetHashCode();
			}
			for (int j = 0; j < 6; j++)
			{
				num = (num * 397) ^ this.GetCameraCullingPlane(j).GetHashCode();
			}
			num = (num * 397) ^ this.baseFarDistance.GetHashCode();
			num = (num * 397) ^ this.shadowCullCenter.GetHashCode();
			fixed (float* ptr = &this.layerCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				for (int k = 0; k < 32; k++)
				{
					num = (num * 397) ^ ptr2[k].GetHashCode();
				}
			}
			num = (num * 397) ^ this.layerCullSpherical;
			num = (num * 397) ^ this.coreCameraValues.GetHashCode();
			num = (num * 397) ^ (int)this.cameraType;
			num = (num * 397) ^ this.projectionIsOblique;
			return (num * 397) ^ this.isImplicitProjectionMatrix;
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x00063200 File Offset: 0x00061400
		public static bool operator ==(CameraProperties left, CameraProperties right)
		{
			return left.Equals(right);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x0006321C File Offset: 0x0006141C
		public static bool operator !=(CameraProperties left, CameraProperties right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010BE RID: 4286
		private const int k_NumLayers = 32;

		// Token: 0x040010BF RID: 4287
		private Rect screenRect;

		// Token: 0x040010C0 RID: 4288
		private Vector3 viewDir;

		// Token: 0x040010C1 RID: 4289
		private float projectionNear;

		// Token: 0x040010C2 RID: 4290
		private float projectionFar;

		// Token: 0x040010C3 RID: 4291
		private float cameraNear;

		// Token: 0x040010C4 RID: 4292
		private float cameraFar;

		// Token: 0x040010C5 RID: 4293
		private float cameraAspect;

		// Token: 0x040010C6 RID: 4294
		private Matrix4x4 cameraToWorld;

		// Token: 0x040010C7 RID: 4295
		private Matrix4x4 actualWorldToClip;

		// Token: 0x040010C8 RID: 4296
		private Matrix4x4 cameraClipToWorld;

		// Token: 0x040010C9 RID: 4297
		private Matrix4x4 cameraWorldToClip;

		// Token: 0x040010CA RID: 4298
		private Matrix4x4 implicitProjection;

		// Token: 0x040010CB RID: 4299
		private Matrix4x4 stereoWorldToClipLeft;

		// Token: 0x040010CC RID: 4300
		private Matrix4x4 stereoWorldToClipRight;

		// Token: 0x040010CD RID: 4301
		private Matrix4x4 worldToCamera;

		// Token: 0x040010CE RID: 4302
		private Vector3 up;

		// Token: 0x040010CF RID: 4303
		private Vector3 right;

		// Token: 0x040010D0 RID: 4304
		private Vector3 transformDirection;

		// Token: 0x040010D1 RID: 4305
		private Vector3 cameraEuler;

		// Token: 0x040010D2 RID: 4306
		private Vector3 velocity;

		// Token: 0x040010D3 RID: 4307
		private float farPlaneWorldSpaceLength;

		// Token: 0x040010D4 RID: 4308
		private uint rendererCount;

		// Token: 0x040010D5 RID: 4309
		private const int k_PlaneCount = 6;

		// Token: 0x040010D6 RID: 4310
		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_ShadowCullPlanes>e__FixedBuffer m_ShadowCullPlanes;

		// Token: 0x040010D7 RID: 4311
		[FixedBuffer(typeof(byte), 96)]
		internal CameraProperties.<m_CameraCullPlanes>e__FixedBuffer m_CameraCullPlanes;

		// Token: 0x040010D8 RID: 4312
		private float baseFarDistance;

		// Token: 0x040010D9 RID: 4313
		private Vector3 shadowCullCenter;

		// Token: 0x040010DA RID: 4314
		[FixedBuffer(typeof(float), 32)]
		internal CameraProperties.<layerCullDistances>e__FixedBuffer layerCullDistances;

		// Token: 0x040010DB RID: 4315
		private int layerCullSpherical;

		// Token: 0x040010DC RID: 4316
		private CoreCameraValues coreCameraValues;

		// Token: 0x040010DD RID: 4317
		private uint cameraType;

		// Token: 0x040010DE RID: 4318
		private int projectionIsOblique;

		// Token: 0x040010DF RID: 4319
		private int isImplicitProjectionMatrix;

		// Token: 0x040010E0 RID: 4320
		internal bool useInteractiveLightBakingData;

		// Token: 0x0200052D RID: 1325
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <layerCullDistances>e__FixedBuffer
		{
			// Token: 0x040010E1 RID: 4321
			public float FixedElementField;
		}

		// Token: 0x0200052E RID: 1326
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <m_CameraCullPlanes>e__FixedBuffer
		{
			// Token: 0x040010E2 RID: 4322
			public byte FixedElementField;
		}

		// Token: 0x0200052F RID: 1327
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 96)]
		public struct <m_ShadowCullPlanes>e__FixedBuffer
		{
			// Token: 0x040010E3 RID: 4323
			public byte FixedElementField;
		}
	}
}
