using System;
using System.Collections;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM
{
	// Token: 0x02000144 RID: 324
	internal class WindowCollection : DOMObject, IWindowCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x00009FFA File Offset: 0x000081FA
		public WindowCollection(WebBrowser control, nsIDOMWindowCollection windowCol)
			: base(control)
		{
			if (control.platform != control.enginePlatform)
			{
				this.unmanagedWindows = nsDOMWindowCollection.GetProxy(control, windowCol);
				return;
			}
			this.unmanagedWindows = windowCol;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0000A028 File Offset: 0x00008228
		public WindowCollection(WebBrowser control)
			: base(control)
		{
			IWindow[] array = new Window[0];
			this.windows = array;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0000A04A File Offset: 0x0000824A
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.Clear();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0000A064 File Offset: 0x00008264
		protected void Clear()
		{
			if (this.windows != null)
			{
				for (int i = 0; i < this.windowCount; i++)
				{
					this.windows[i] = null;
				}
				this.windowCount = 0;
				this.windows = null;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000A0A4 File Offset: 0x000082A4
		internal void Load()
		{
			this.Clear();
			uint num;
			this.unmanagedWindows.getLength(out num);
			Window[] array = new Window[num];
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				nsIDOMWindow nsIDOMWindow;
				this.unmanagedWindows.item((uint)num2, out nsIDOMWindow);
				Window[] array2 = array;
				int num3 = this.windowCount;
				this.windowCount = num3 + 1;
				array2[num3] = new Window(this.control, nsIDOMWindow);
				num2++;
			}
			IWindow[] array3 = new Window[this.windowCount];
			this.windows = array3;
			Array.Copy(array, this.windows, this.windowCount);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000A132 File Offset: 0x00008332
		public IEnumerator GetEnumerator()
		{
			return new WindowCollection.WindowEnumerator(this);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000A13A File Offset: 0x0000833A
		public void CopyTo(Array dest, int index)
		{
			if (this.windows != null)
			{
				Array.Copy(this.windows, 0, dest, index, this.windowCount);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0000A158 File Offset: 0x00008358
		public int Count
		{
			get
			{
				if (this.unmanagedWindows != null && this.windows == null)
				{
					this.Load();
				}
				return this.windowCount;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0000A176 File Offset: 0x00008376
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0000A179 File Offset: 0x00008379
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0000A17C File Offset: 0x0000837C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0000A17F File Offset: 0x0000837F
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0000A182 File Offset: 0x00008382
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0000A18C File Offset: 0x0000838C
		public void RemoveAt(int index)
		{
			if (index > this.windowCount || index < 0)
			{
				return;
			}
			Array.Copy(this.windows, index + 1, this.windows, index, this.windowCount - index - 1);
			this.windowCount--;
			this.windows[this.windowCount] = null;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000A1E2 File Offset: 0x000083E2
		public void Remove(IWindow window)
		{
			this.RemoveAt(this.IndexOf(window));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000A1F1 File Offset: 0x000083F1
		void IList.Remove(object window)
		{
			this.Remove(window as IWindow);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000A200 File Offset: 0x00008400
		public void Insert(int index, IWindow value)
		{
			if (index > this.windowCount)
			{
				index = this.windowCount;
			}
			IWindow[] array = new Window[this.windowCount + 1];
			IWindow[] array2 = array;
			if (index > 0)
			{
				Array.Copy(this.windows, 0, array2, 0, index);
			}
			array2[index] = value;
			if (index < this.windowCount)
			{
				Array.Copy(this.windows, index, array2, index + 1, this.windowCount - index);
			}
			this.windows = array2;
			this.windowCount++;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000A27B File Offset: 0x0000847B
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as IWindow);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000A28A File Offset: 0x0000848A
		public int IndexOf(IWindow window)
		{
			return Array.IndexOf<IWindow>(this.windows, window);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000A298 File Offset: 0x00008498
		int IList.IndexOf(object window)
		{
			return this.IndexOf(window as IWindow);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000A2A6 File Offset: 0x000084A6
		public bool Contains(IWindow window)
		{
			return this.IndexOf(window) != -1;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000A2B5 File Offset: 0x000084B5
		bool IList.Contains(object window)
		{
			return this.Contains(window as IWindow);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000A2C3 File Offset: 0x000084C3
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000A2CB File Offset: 0x000084CB
		public int Add(IWindow window)
		{
			this.Insert(this.windowCount + 1, window);
			return this.windowCount - 1;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000A2E4 File Offset: 0x000084E4
		int IList.Add(object window)
		{
			return this.Add(window as IWindow);
		}

		// Token: 0x1700011B RID: 283
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = value as IWindow;
			}
		}

		// Token: 0x1700011C RID: 284
		public IWindow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.windowCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.windows[index];
			}
			set
			{
				if (index < 0 || index >= this.windowCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.windows[index] = value;
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000A34F File Offset: 0x0000854F
		public override int GetHashCode()
		{
			if (this.unmanagedWindows != null)
			{
				return this.unmanagedWindows.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04000136 RID: 310
		protected nsIDOMWindowCollection unmanagedWindows;

		// Token: 0x04000137 RID: 311
		protected IWindow[] windows;

		// Token: 0x04000138 RID: 312
		protected int windowCount;

		// Token: 0x0200014D RID: 333
		internal class WindowEnumerator : IEnumerator
		{
			// Token: 0x06000A79 RID: 2681 RVA: 0x0000A421 File Offset: 0x00008621
			public WindowEnumerator(WindowCollection collection)
			{
				this.collection = collection;
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0000A437 File Offset: 0x00008637
			public object Current
			{
				get
				{
					if (this.index == -1)
					{
						return null;
					}
					return this.collection[this.index];
				}
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x0000A455 File Offset: 0x00008655
			public bool MoveNext()
			{
				if (this.index + 1 >= this.collection.Count)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x0000A47D File Offset: 0x0000867D
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04000171 RID: 369
			private WindowCollection collection;

			// Token: 0x04000172 RID: 370
			private int index = -1;
		}
	}
}
