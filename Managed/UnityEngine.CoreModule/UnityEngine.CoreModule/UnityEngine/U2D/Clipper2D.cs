using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x020005A5 RID: 1445
	[NativeType(Header = "Runtime/2D/Common/ClipperWrapper.h")]
	internal struct Clipper2D
	{
		// Token: 0x060037CE RID: 14286 RVA: 0x0006D7EC File Offset: 0x0006B9EC
		public unsafe static void Execute(ref Clipper2D.Solution solution, NativeArray<Vector2> inPoints, NativeArray<int> inPathSizes, NativeArray<Clipper2D.PathArguments> inPathArguments, Clipper2D.ExecuteArguments inExecuteArguments, Allocator inSolutionAllocator, int inIntScale = 65536, bool useRounding = false)
		{
			bool flag = !solution.boundingRect.IsCreated;
			if (flag)
			{
				solution.boundingRect = new NativeArray<Rect>(1, inSolutionAllocator, NativeArrayOptions.ClearMemory);
			}
			IntPtr intPtr;
			int num;
			IntPtr intPtr2;
			int num2;
			solution.boundingRect[0] = Clipper2D.Internal_Execute(out intPtr, out num, out intPtr2, out num2, new IntPtr(inPoints.m_Buffer), inPoints.Length, new IntPtr(inPathSizes.m_Buffer), new IntPtr(inPathArguments.m_Buffer), inPathSizes.Length, inExecuteArguments, (float)inIntScale, useRounding);
			bool flag2 = num > 0;
			if (flag2)
			{
				bool flag3 = !solution.pathSizes.IsCreated;
				if (flag3)
				{
					solution.pathSizes = new NativeArray<int>(num2, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
				bool flag4 = !solution.points.IsCreated;
				if (flag4)
				{
					solution.points = new NativeArray<Vector2>(num, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
				bool flag5 = solution.points.Length >= num && solution.pathSizes.Length >= num2;
				if (!flag5)
				{
					Clipper2D.Internal_Execute_Cleanup(intPtr, intPtr2);
					throw new IndexOutOfRangeException();
				}
				UnsafeUtility.MemCpy(solution.points.m_Buffer, intPtr.ToPointer(), (long)(num * sizeof(Vector2)));
				UnsafeUtility.MemCpy(solution.pathSizes.m_Buffer, intPtr2.ToPointer(), (long)(num2 * 4));
				Clipper2D.Internal_Execute_Cleanup(intPtr, intPtr2);
			}
			else
			{
				bool flag6 = !solution.pathSizes.IsCreated;
				if (flag6)
				{
					solution.points = new NativeArray<Vector2>(0, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
				bool flag7 = !solution.points.IsCreated;
				if (flag7)
				{
					solution.pathSizes = new NativeArray<int>(0, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
			}
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x0006D990 File Offset: 0x0006BB90
		[NativeMethod(Name = "Clipper2D::Execute", IsFreeFunction = true, IsThreadSafe = true)]
		private static Rect Internal_Execute(out IntPtr outClippedPoints, out int outClippedPointsCount, out IntPtr outClippedPathSizes, out int outClippedPathCount, IntPtr inPoints, int inPointCount, IntPtr inPathSizes, IntPtr inPathArguments, int inPathCount, Clipper2D.ExecuteArguments inExecuteArguments, float inIntScale, bool useRounding)
		{
			Rect rect;
			Clipper2D.Internal_Execute_Injected(out outClippedPoints, out outClippedPointsCount, out outClippedPathSizes, out outClippedPathCount, inPoints, inPointCount, inPathSizes, inPathArguments, inPathCount, ref inExecuteArguments, inIntScale, useRounding, out rect);
			return rect;
		}

		// Token: 0x060037D0 RID: 14288
		[NativeMethod(Name = "Clipper2D::Execute_Cleanup", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute_Cleanup(IntPtr inPoints, IntPtr inPathSizes);

		// Token: 0x060037D1 RID: 14289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute_Injected(out IntPtr outClippedPoints, out int outClippedPointsCount, out IntPtr outClippedPathSizes, out int outClippedPathCount, IntPtr inPoints, int inPointCount, IntPtr inPathSizes, IntPtr inPathArguments, int inPathCount, [In] ref Clipper2D.ExecuteArguments inExecuteArguments, float inIntScale, bool useRounding, out Rect ret);

		// Token: 0x020005A6 RID: 1446
		public enum ClipType
		{
			// Token: 0x040013D7 RID: 5079
			ctIntersection,
			// Token: 0x040013D8 RID: 5080
			ctUnion,
			// Token: 0x040013D9 RID: 5081
			ctDifference,
			// Token: 0x040013DA RID: 5082
			ctXor
		}

		// Token: 0x020005A7 RID: 1447
		public enum PolyType
		{
			// Token: 0x040013DC RID: 5084
			ptSubject,
			// Token: 0x040013DD RID: 5085
			ptClip
		}

		// Token: 0x020005A8 RID: 1448
		public enum PolyFillType
		{
			// Token: 0x040013DF RID: 5087
			pftEvenOdd,
			// Token: 0x040013E0 RID: 5088
			pftNonZero,
			// Token: 0x040013E1 RID: 5089
			pftPositive,
			// Token: 0x040013E2 RID: 5090
			pftNegative
		}

		// Token: 0x020005A9 RID: 1449
		public enum InitOptions
		{
			// Token: 0x040013E4 RID: 5092
			ioDefault,
			// Token: 0x040013E5 RID: 5093
			oReverseSolution,
			// Token: 0x040013E6 RID: 5094
			ioStrictlySimple,
			// Token: 0x040013E7 RID: 5095
			ioPreserveCollinear = 4
		}

		// Token: 0x020005AA RID: 1450
		[NativeType(Header = "Runtime/2D/Common/ClipperWrapper.h")]
		public struct PathArguments
		{
			// Token: 0x060037D2 RID: 14290 RVA: 0x0006D9B9 File Offset: 0x0006BBB9
			public PathArguments(Clipper2D.PolyType inPolyType = Clipper2D.PolyType.ptSubject, bool inClosed = false)
			{
				this.polyType = inPolyType;
				this.closed = inClosed;
			}

			// Token: 0x040013E8 RID: 5096
			public Clipper2D.PolyType polyType;

			// Token: 0x040013E9 RID: 5097
			public bool closed;
		}

		// Token: 0x020005AB RID: 1451
		[NativeType(Header = "Runtime/2D/Common/ClipperWrapper.h")]
		public struct ExecuteArguments
		{
			// Token: 0x060037D3 RID: 14291 RVA: 0x0006D9CA File Offset: 0x0006BBCA
			public ExecuteArguments(Clipper2D.InitOptions inInitOption = Clipper2D.InitOptions.ioDefault, Clipper2D.ClipType inClipType = Clipper2D.ClipType.ctIntersection, Clipper2D.PolyFillType inSubjFillType = Clipper2D.PolyFillType.pftEvenOdd, Clipper2D.PolyFillType inClipFillType = Clipper2D.PolyFillType.pftEvenOdd, bool inReverseSolution = false, bool inStrictlySimple = false, bool inPreserveColinear = false)
			{
				this.initOption = inInitOption;
				this.clipType = inClipType;
				this.subjFillType = inSubjFillType;
				this.clipFillType = inClipFillType;
				this.reverseSolution = inReverseSolution;
				this.strictlySimple = inStrictlySimple;
				this.preserveColinear = inPreserveColinear;
			}

			// Token: 0x040013EA RID: 5098
			public Clipper2D.InitOptions initOption;

			// Token: 0x040013EB RID: 5099
			public Clipper2D.ClipType clipType;

			// Token: 0x040013EC RID: 5100
			public Clipper2D.PolyFillType subjFillType;

			// Token: 0x040013ED RID: 5101
			public Clipper2D.PolyFillType clipFillType;

			// Token: 0x040013EE RID: 5102
			public bool reverseSolution;

			// Token: 0x040013EF RID: 5103
			public bool strictlySimple;

			// Token: 0x040013F0 RID: 5104
			public bool preserveColinear;
		}

		// Token: 0x020005AC RID: 1452
		public struct Solution : IDisposable
		{
			// Token: 0x060037D4 RID: 14292 RVA: 0x0006DA02 File Offset: 0x0006BC02
			public Solution(int pointsBufferSize, int pathSizesBufferSize, Allocator allocator)
			{
				this.points = new NativeArray<Vector2>(pointsBufferSize, allocator, NativeArrayOptions.ClearMemory);
				this.pathSizes = new NativeArray<int>(pathSizesBufferSize, allocator, NativeArrayOptions.ClearMemory);
				this.boundingRect = new NativeArray<Rect>(1, allocator, NativeArrayOptions.ClearMemory);
			}

			// Token: 0x060037D5 RID: 14293 RVA: 0x0006DA30 File Offset: 0x0006BC30
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
				bool isCreated3 = this.boundingRect.IsCreated;
				if (isCreated3)
				{
					this.boundingRect.Dispose();
				}
			}

			// Token: 0x040013F1 RID: 5105
			public NativeArray<Vector2> points;

			// Token: 0x040013F2 RID: 5106
			public NativeArray<int> pathSizes;

			// Token: 0x040013F3 RID: 5107
			public NativeArray<Rect> boundingRect;
		}
	}
}
