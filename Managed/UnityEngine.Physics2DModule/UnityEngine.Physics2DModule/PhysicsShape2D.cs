using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
	[UsedByNativeCode]
	public struct PhysicsShape2D
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000067C4 File Offset: 0x000049C4
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000067DC File Offset: 0x000049DC
		public PhysicsShapeType2D shapeType
		{
			get
			{
				return this.m_ShapeType;
			}
			set
			{
				this.m_ShapeType = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000067E8 File Offset: 0x000049E8
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00006800 File Offset: 0x00004A00
		public float radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("radius cannot be negative.");
				}
				bool flag2 = float.IsNaN(value) || float.IsInfinity(value);
				if (flag2)
				{
					throw new ArgumentException("radius contains an invalid value.");
				}
				this.m_Radius = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000684C File Offset: 0x00004A4C
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00006864 File Offset: 0x00004A64
		public int vertexStartIndex
		{
			get
			{
				return this.m_VertexStartIndex;
			}
			set
			{
				bool flag = value < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("vertexStartIndex cannot be negative.");
				}
				this.m_VertexStartIndex = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000688C File Offset: 0x00004A8C
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000068A4 File Offset: 0x00004AA4
		public int vertexCount
		{
			get
			{
				return this.m_VertexCount;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("vertexCount cannot be less than one.");
				}
				this.m_VertexCount = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000068CC File Offset: 0x00004ACC
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000068E7 File Offset: 0x00004AE7
		public bool useAdjacentStart
		{
			get
			{
				return this.m_UseAdjacentStart != 0;
			}
			set
			{
				this.m_UseAdjacentStart = (value ? 1 : 0);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000068F8 File Offset: 0x00004AF8
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00006913 File Offset: 0x00004B13
		public bool useAdjacentEnd
		{
			get
			{
				return this.m_UseAdjacentEnd != 0;
			}
			set
			{
				this.m_UseAdjacentEnd = (value ? 1 : 0);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006924 File Offset: 0x00004B24
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000693C File Offset: 0x00004B3C
		public Vector2 adjacentStart
		{
			get
			{
				return this.m_AdjacentStart;
			}
			set
			{
				bool flag = float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsInfinity(value.x) || float.IsInfinity(value.y);
				if (flag)
				{
					throw new ArgumentException("adjacentStart contains an invalid value.");
				}
				this.m_AdjacentStart = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00006998 File Offset: 0x00004B98
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000069B0 File Offset: 0x00004BB0
		public Vector2 adjacentEnd
		{
			get
			{
				return this.m_AdjacentEnd;
			}
			set
			{
				bool flag = float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsInfinity(value.x) || float.IsInfinity(value.y);
				if (flag)
				{
					throw new ArgumentException("adjacentEnd contains an invalid value.");
				}
				this.m_AdjacentEnd = value;
			}
		}

		// Token: 0x04000053 RID: 83
		private PhysicsShapeType2D m_ShapeType;

		// Token: 0x04000054 RID: 84
		private float m_Radius;

		// Token: 0x04000055 RID: 85
		private int m_VertexStartIndex;

		// Token: 0x04000056 RID: 86
		private int m_VertexCount;

		// Token: 0x04000057 RID: 87
		private int m_UseAdjacentStart;

		// Token: 0x04000058 RID: 88
		private int m_UseAdjacentEnd;

		// Token: 0x04000059 RID: 89
		private Vector2 m_AdjacentStart;

		// Token: 0x0400005A RID: 90
		private Vector2 m_AdjacentEnd;
	}
}
