using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x020005AD RID: 1453
	[NativeType(Header = "Runtime/2D/Common/ClipperOffsetWrapper.h")]
	internal struct ClipperOffset2D
	{
		// Token: 0x060037D6 RID: 14294 RVA: 0x0006DA90 File Offset: 0x0006BC90
		public unsafe static void Execute(ref ClipperOffset2D.Solution solution, NativeArray<Vector2> inPoints, NativeArray<int> inPathSizes, NativeArray<ClipperOffset2D.PathArguments> inPathArguments, Allocator inSolutionAllocator, double inDelta = 0.0, double inMiterLimit = 2.0, double inRoundPrecision = 0.25, double inArcTolerance = 0.0, double inIntScale = 65536.0, bool useRounding = false)
		{
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			int num2;
			ClipperOffset2D.Internal_Execute(out intPtr, out num, out intPtr2, out num2, new IntPtr(inPoints.m_Buffer), inPoints.Length, new IntPtr(inPathSizes.m_Buffer), new IntPtr(inPathArguments.m_Buffer), inPathSizes.Length, inDelta, inMiterLimit, inRoundPrecision, inArcTolerance, inIntScale, useRounding);
			bool flag = !solution.pathSizes.IsCreated;
			if (flag)
			{
				solution.pathSizes = new NativeArray<int>(num2, inSolutionAllocator, NativeArrayOptions.ClearMemory);
			}
			bool flag2 = !solution.points.IsCreated;
			if (flag2)
			{
				solution.points = new NativeArray<Vector2>(num, inSolutionAllocator, NativeArrayOptions.ClearMemory);
			}
			bool flag3 = solution.points.Length >= num && solution.pathSizes.Length >= num2;
			if (flag3)
			{
				UnsafeUtility.MemCpy(solution.points.m_Buffer, intPtr.ToPointer(), (long)(num * sizeof(Vector2)));
				UnsafeUtility.MemCpy(solution.pathSizes.m_Buffer, intPtr2.ToPointer(), (long)(num2 * 4));
				ClipperOffset2D.Internal_Execute_Cleanup(intPtr, intPtr2);
				return;
			}
			ClipperOffset2D.Internal_Execute_Cleanup(intPtr, intPtr2);
			throw new IndexOutOfRangeException();
		}

		// Token: 0x060037D7 RID: 14295
		[NativeMethod(Name = "ClipperOffset2D::Execute", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute(out IntPtr outClippedPoints, out int outClippedPointsCount, out IntPtr outClippedPathSizes, out int outClippedPathCount, IntPtr inPoints, int inPointCount, IntPtr inPathSizes, IntPtr inPathArguments, int inPathCount, double inDelta, double inMiterLimit, double inRoundPrecision, double inArcTolerance, double inIntScale, bool useRounding);

		// Token: 0x060037D8 RID: 14296
		[NativeMethod(Name = "ClipperOffset2D::Execute_Cleanup", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute_Cleanup(IntPtr inPoints, IntPtr inPathSizes);

		// Token: 0x020005AE RID: 1454
		public enum JoinType
		{
			// Token: 0x040013F5 RID: 5109
			jtSquare,
			// Token: 0x040013F6 RID: 5110
			jtRound,
			// Token: 0x040013F7 RID: 5111
			jtMiter
		}

		// Token: 0x020005AF RID: 1455
		public enum EndType
		{
			// Token: 0x040013F9 RID: 5113
			etClosedPolygon,
			// Token: 0x040013FA RID: 5114
			etClosedLine,
			// Token: 0x040013FB RID: 5115
			etOpenButt,
			// Token: 0x040013FC RID: 5116
			etOpenSquare,
			// Token: 0x040013FD RID: 5117
			etOpenRound
		}

		// Token: 0x020005B0 RID: 1456
		[NativeType(Header = "Runtime/2D/Common/ClipperOffsetWrapper.h")]
		public struct PathArguments
		{
			// Token: 0x060037D9 RID: 14297 RVA: 0x0006DBAF File Offset: 0x0006BDAF
			public PathArguments(ClipperOffset2D.JoinType inJoinType = ClipperOffset2D.JoinType.jtSquare, ClipperOffset2D.EndType inEndType = ClipperOffset2D.EndType.etClosedPolygon)
			{
				this.joinType = inJoinType;
				this.endType = inEndType;
			}

			// Token: 0x040013FE RID: 5118
			public ClipperOffset2D.JoinType joinType;

			// Token: 0x040013FF RID: 5119
			public ClipperOffset2D.EndType endType;
		}

		// Token: 0x020005B1 RID: 1457
		public struct Solution
		{
			// Token: 0x060037DA RID: 14298 RVA: 0x0006DBC0 File Offset: 0x0006BDC0
			public Solution(int pointsBufferSize, int pathSizesBufferSize, Allocator allocator)
			{
				this.points = new NativeArray<Vector2>(pointsBufferSize, allocator, NativeArrayOptions.ClearMemory);
				this.pathSizes = new NativeArray<int>(pathSizesBufferSize, allocator, NativeArrayOptions.ClearMemory);
			}

			// Token: 0x060037DB RID: 14299 RVA: 0x0006DBE0 File Offset: 0x0006BDE0
			public void Dispose()
			{
				bool isCreated = this.points.IsCreated;
				if (isCreated)
				{
					this.points.Dispose();
				}
				bool isCreated2 = this.pathSizes.IsCreated;
				if (isCreated2)
				{
					this.pathSizes.Dispose();
				}
			}

			// Token: 0x04001400 RID: 5120
			public NativeArray<Vector2> points;

			// Token: 0x04001401 RID: 5121
			public NativeArray<int> pathSizes;
		}
	}
}
