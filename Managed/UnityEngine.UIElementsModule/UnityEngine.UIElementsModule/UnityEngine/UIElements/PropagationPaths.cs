using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A1 RID: 673
	internal class PropagationPaths : IDisposable
	{
		// Token: 0x0600136C RID: 4972 RVA: 0x0004ECEA File Offset: 0x0004CEEA
		public PropagationPaths()
		{
			this.trickleDownPath = new List<VisualElement>(8);
			this.bubbleUpPath = new List<VisualElement>(8);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		public PropagationPaths(PropagationPaths paths)
			: this()
		{
			bool flag = paths != null;
			if (flag)
			{
				this.trickleDownPath.AddRange(paths.trickleDownPath);
				this.bubbleUpPath.AddRange(paths.bubbleUpPath);
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004ED50 File Offset: 0x0004CF50
		[NotNull]
		public static PropagationPaths Build(VisualElement elem, EventBase evt, int eventCategories)
		{
			PropagationPaths propagationPaths = PropagationPaths.s_Pool.Get();
			bool flag = elem.HasTrickleDownEventInterests(eventCategories);
			if (flag)
			{
				propagationPaths.trickleDownPath.Add(elem);
			}
			bool flag2 = elem.HasBubbleUpEventInterests(eventCategories);
			if (flag2)
			{
				propagationPaths.bubbleUpPath.Add(elem);
			}
			for (VisualElement visualElement = elem.nextParentWithEventInterests; visualElement != null; visualElement = visualElement.nextParentWithEventInterests)
			{
				bool flag3 = !visualElement.HasParentEventInterests(eventCategories);
				if (flag3)
				{
					break;
				}
				bool flag4 = evt.tricklesDown && visualElement.HasTrickleDownEventInterests(eventCategories);
				if (flag4)
				{
					propagationPaths.trickleDownPath.Add(visualElement);
				}
				bool flag5 = evt.bubbles && visualElement.HasBubbleUpEventInterests(eventCategories);
				if (flag5)
				{
					propagationPaths.bubbleUpPath.Add(visualElement);
				}
			}
			return propagationPaths;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0004EE20 File Offset: 0x0004D020
		public void Dispose()
		{
			this.bubbleUpPath.Clear();
			this.trickleDownPath.Clear();
			PropagationPaths.s_Pool.Release(this);
		}

		// Token: 0x04000AA8 RID: 2728
		private static readonly ObjectPool<PropagationPaths> s_Pool = new ObjectPool<PropagationPaths>(() => new PropagationPaths(), 100);

		// Token: 0x04000AA9 RID: 2729
		public readonly List<VisualElement> trickleDownPath;

		// Token: 0x04000AAA RID: 2730
		public readonly List<VisualElement> bubbleUpPath;

		// Token: 0x04000AAB RID: 2731
		private const int k_DefaultPropagationDepth = 8;
	}
}
