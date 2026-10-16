using System;
using Unity.Profiling;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200031A RID: 794
	internal class UIRAtlasAllocator : IDisposable
	{
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00058162 File Offset: 0x00056362
		public int maxAtlasSize { get; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0005816A File Offset: 0x0005636A
		public int maxImageWidth { get; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00058172 File Offset: 0x00056372
		public int maxImageHeight { get; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0005817A File Offset: 0x0005637A
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x00058182 File Offset: 0x00056382
		public int virtualWidth { get; private set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0005818B File Offset: 0x0005638B
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x00058193 File Offset: 0x00056393
		public int virtualHeight { get; private set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0005819C File Offset: 0x0005639C
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x000581A4 File Offset: 0x000563A4
		public int physicalWidth { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x000581AD File Offset: 0x000563AD
		// (set) Token: 0x060016CF RID: 5839 RVA: 0x000581B5 File Offset: 0x000563B5
		public int physicalHeight { get; private set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000581BE File Offset: 0x000563BE
		// (set) Token: 0x060016D1 RID: 5841 RVA: 0x000581C6 File Offset: 0x000563C6
		private protected bool disposed { protected get; private set; }

		// Token: 0x060016D2 RID: 5842 RVA: 0x000581CF File Offset: 0x000563CF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x000581E4 File Offset: 0x000563E4
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_OpenRows.Length; i++)
					{
						UIRAtlasAllocator.Row row = this.m_OpenRows[i];
						bool flag = row != null;
						if (flag)
						{
							row.Release();
						}
					}
					this.m_OpenRows = null;
					UIRAtlasAllocator.AreaNode next;
					for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = next)
					{
						next = areaNode.next;
						areaNode.Release();
					}
					this.m_FirstUnpartitionedArea = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x0005827C File Offset: 0x0005647C
		private static int GetLog2OfNextPower(int n)
		{
			float num = (float)Mathf.NextPowerOfTwo(n);
			float num2 = Mathf.Log(num, 2f);
			return Mathf.RoundToInt(num2);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x000582A8 File Offset: 0x000564A8
		public UIRAtlasAllocator(int initialAtlasSize, int maxAtlasSize, int sidePadding = 1)
		{
			Assert.IsTrue(initialAtlasSize > 0 && initialAtlasSize <= maxAtlasSize);
			Assert.IsTrue(initialAtlasSize == Mathf.NextPowerOfTwo(initialAtlasSize));
			Assert.IsTrue(maxAtlasSize == Mathf.NextPowerOfTwo(maxAtlasSize));
			this.m_1SidePadding = sidePadding;
			this.m_2SidePadding = sidePadding << 1;
			this.maxAtlasSize = maxAtlasSize;
			this.maxImageWidth = maxAtlasSize;
			this.maxImageHeight = ((initialAtlasSize == maxAtlasSize) ? (maxAtlasSize / 2 + this.m_2SidePadding) : (maxAtlasSize / 4 + this.m_2SidePadding));
			this.virtualWidth = initialAtlasSize;
			this.virtualHeight = initialAtlasSize;
			int num = UIRAtlasAllocator.GetLog2OfNextPower(maxAtlasSize) + 1;
			this.m_OpenRows = new UIRAtlasAllocator.Row[num];
			RectInt rectInt = new RectInt(0, 0, initialAtlasSize, initialAtlasSize);
			this.m_FirstUnpartitionedArea = UIRAtlasAllocator.AreaNode.Acquire(rectInt);
			this.BuildAreas();
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00058370 File Offset: 0x00056570
		public bool TryAllocate(int width, int height, out RectInt location)
		{
			bool flag;
			using (UIRAtlasAllocator.s_MarkerTryAllocate.Auto())
			{
				location = default(RectInt);
				bool disposed = this.disposed;
				if (disposed)
				{
					flag = false;
				}
				else
				{
					bool flag2 = width < 1 || height < 1;
					if (flag2)
					{
						flag = false;
					}
					else
					{
						bool flag3 = width > this.maxImageWidth || height > this.maxImageHeight;
						if (flag3)
						{
							flag = false;
						}
						else
						{
							int log2OfNextPower = UIRAtlasAllocator.GetLog2OfNextPower(Mathf.Max(height - this.m_2SidePadding, 1));
							int num = (1 << log2OfNextPower) + this.m_2SidePadding;
							UIRAtlasAllocator.Row row = this.m_OpenRows[log2OfNextPower];
							bool flag4 = row != null && row.width - row.Cursor < width;
							if (flag4)
							{
								row = null;
							}
							bool flag5 = row == null;
							if (flag5)
							{
								for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = areaNode.next)
								{
									bool flag6 = this.TryPartitionArea(areaNode, log2OfNextPower, num, width);
									if (flag6)
									{
										row = this.m_OpenRows[log2OfNextPower];
										break;
									}
								}
								bool flag7 = row == null;
								if (flag7)
								{
									return false;
								}
							}
							location = new RectInt(row.offsetX + row.Cursor, row.offsetY, width, height);
							row.Cursor += width;
							Assert.IsTrue(row.Cursor <= row.width);
							this.physicalWidth = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalWidth, location.xMax));
							this.physicalHeight = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalHeight, location.yMax));
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00058538 File Offset: 0x00056738
		private bool TryPartitionArea(UIRAtlasAllocator.AreaNode areaNode, int rowIndex, int rowHeight, int minWidth)
		{
			RectInt rect = areaNode.rect;
			bool flag = rect.height < rowHeight || rect.width < minWidth;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
				bool flag3 = row != null;
				if (flag3)
				{
					row.Release();
				}
				row = UIRAtlasAllocator.Row.Acquire(rect.x, rect.y, rect.width, rowHeight);
				this.m_OpenRows[rowIndex] = row;
				rect.y += rowHeight;
				rect.height -= rowHeight;
				bool flag4 = rect.height == 0;
				if (flag4)
				{
					bool flag5 = areaNode == this.m_FirstUnpartitionedArea;
					if (flag5)
					{
						this.m_FirstUnpartitionedArea = areaNode.next;
					}
					areaNode.RemoveFromChain();
					areaNode.Release();
				}
				else
				{
					areaNode.rect = rect;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00058618 File Offset: 0x00056818
		private void BuildAreas()
		{
			UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea;
			while (this.virtualWidth < this.maxAtlasSize || this.virtualHeight < this.maxAtlasSize)
			{
				bool flag = this.virtualWidth > this.virtualHeight;
				RectInt rectInt;
				if (flag)
				{
					rectInt = new RectInt(0, this.virtualHeight, this.virtualWidth, this.virtualHeight);
					this.virtualHeight *= 2;
				}
				else
				{
					rectInt = new RectInt(this.virtualWidth, 0, this.virtualWidth, this.virtualHeight);
					this.virtualWidth *= 2;
				}
				UIRAtlasAllocator.AreaNode areaNode2 = UIRAtlasAllocator.AreaNode.Acquire(rectInt);
				areaNode2.AddAfter(areaNode);
				areaNode = areaNode2;
			}
		}

		// Token: 0x04000C75 RID: 3189
		private UIRAtlasAllocator.AreaNode m_FirstUnpartitionedArea;

		// Token: 0x04000C76 RID: 3190
		private UIRAtlasAllocator.Row[] m_OpenRows;

		// Token: 0x04000C77 RID: 3191
		private int m_1SidePadding;

		// Token: 0x04000C78 RID: 3192
		private int m_2SidePadding;

		// Token: 0x04000C79 RID: 3193
		private static ProfilerMarker s_MarkerTryAllocate = new ProfilerMarker("UIRAtlasAllocator.TryAllocate");

		// Token: 0x0200031B RID: 795
		private class Row
		{
			// Token: 0x17000455 RID: 1109
			// (get) Token: 0x060016DA RID: 5850 RVA: 0x000586E5 File Offset: 0x000568E5
			// (set) Token: 0x060016DB RID: 5851 RVA: 0x000586ED File Offset: 0x000568ED
			public int offsetX { get; private set; }

			// Token: 0x17000456 RID: 1110
			// (get) Token: 0x060016DC RID: 5852 RVA: 0x000586F6 File Offset: 0x000568F6
			// (set) Token: 0x060016DD RID: 5853 RVA: 0x000586FE File Offset: 0x000568FE
			public int offsetY { get; private set; }

			// Token: 0x17000457 RID: 1111
			// (get) Token: 0x060016DE RID: 5854 RVA: 0x00058707 File Offset: 0x00056907
			// (set) Token: 0x060016DF RID: 5855 RVA: 0x0005870F File Offset: 0x0005690F
			public int width { get; private set; }

			// Token: 0x17000458 RID: 1112
			// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00058718 File Offset: 0x00056918
			// (set) Token: 0x060016E1 RID: 5857 RVA: 0x00058720 File Offset: 0x00056920
			public int height { get; private set; }

			// Token: 0x060016E2 RID: 5858 RVA: 0x0005872C File Offset: 0x0005692C
			public static UIRAtlasAllocator.Row Acquire(int offsetX, int offsetY, int width, int height)
			{
				UIRAtlasAllocator.Row row = UIRAtlasAllocator.Row.s_Pool.Get();
				row.offsetX = offsetX;
				row.offsetY = offsetY;
				row.width = width;
				row.height = height;
				row.Cursor = 0;
				return row;
			}

			// Token: 0x060016E3 RID: 5859 RVA: 0x00058771 File Offset: 0x00056971
			public void Release()
			{
				UIRAtlasAllocator.Row.s_Pool.Release(this);
				this.offsetX = -1;
				this.offsetY = -1;
				this.width = -1;
				this.height = -1;
				this.Cursor = -1;
			}

			// Token: 0x04000C7B RID: 3195
			private static ObjectPool<UIRAtlasAllocator.Row> s_Pool = new ObjectPool<UIRAtlasAllocator.Row>(() => new UIRAtlasAllocator.Row(), 100);

			// Token: 0x04000C80 RID: 3200
			public int Cursor;
		}

		// Token: 0x0200031D RID: 797
		private class AreaNode
		{
			// Token: 0x060016E9 RID: 5865 RVA: 0x000587D8 File Offset: 0x000569D8
			public static UIRAtlasAllocator.AreaNode Acquire(RectInt rect)
			{
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.s_Pool.Get();
				areaNode.rect = rect;
				areaNode.previous = null;
				areaNode.next = null;
				return areaNode;
			}

			// Token: 0x060016EA RID: 5866 RVA: 0x0005880B File Offset: 0x00056A0B
			public void Release()
			{
				UIRAtlasAllocator.AreaNode.s_Pool.Release(this);
			}

			// Token: 0x060016EB RID: 5867 RVA: 0x0005881C File Offset: 0x00056A1C
			public void RemoveFromChain()
			{
				bool flag = this.previous != null;
				if (flag)
				{
					this.previous.next = this.next;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this.previous;
				}
				this.previous = null;
				this.next = null;
			}

			// Token: 0x060016EC RID: 5868 RVA: 0x00058874 File Offset: 0x00056A74
			public void AddAfter(UIRAtlasAllocator.AreaNode previous)
			{
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.previous);
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.next);
				this.previous = previous;
				bool flag = previous != null;
				if (flag)
				{
					this.next = previous.next;
					previous.next = this;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this;
				}
			}

			// Token: 0x04000C82 RID: 3202
			private static ObjectPool<UIRAtlasAllocator.AreaNode> s_Pool = new ObjectPool<UIRAtlasAllocator.AreaNode>(() => new UIRAtlasAllocator.AreaNode(), 100);

			// Token: 0x04000C83 RID: 3203
			public RectInt rect;

			// Token: 0x04000C84 RID: 3204
			public UIRAtlasAllocator.AreaNode previous;

			// Token: 0x04000C85 RID: 3205
			public UIRAtlasAllocator.AreaNode next;
		}
	}
}
