using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	// Token: 0x0200015A RID: 346
	internal static class BeforeRenderHelper
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x00014490 File Offset: 0x00012690
		private static int GetUpdateOrder(UnityAction callback)
		{
			object[] customAttributes = callback.Method.GetCustomAttributes(typeof(BeforeRenderOrderAttribute), true);
			BeforeRenderOrderAttribute beforeRenderOrderAttribute = ((customAttributes != null && customAttributes.Length != 0) ? (customAttributes[0] as BeforeRenderOrderAttribute) : null);
			return (beforeRenderOrderAttribute != null) ? beforeRenderOrderAttribute.order : 0;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000144D8 File Offset: 0x000126D8
		public static void RegisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag2 = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag2)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Combine(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						return;
					}
					num++;
				}
				BeforeRenderHelper.OrderBlock orderBlock2 = default(BeforeRenderHelper.OrderBlock);
				orderBlock2.order = updateOrder;
				orderBlock2.callback = (UnityAction)Delegate.Combine(orderBlock2.callback, callback);
				BeforeRenderHelper.s_OrderBlocks.Insert(num, orderBlock2);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000145D4 File Offset: 0x000127D4
		public static void UnregisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag2 = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag2)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Remove(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						bool flag3 = orderBlock.callback == null;
						if (flag3)
						{
							BeforeRenderHelper.s_OrderBlocks.RemoveAt(num);
						}
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000146BC File Offset: 0x000128BC
		public static void Invoke()
		{
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				for (int i = 0; i < BeforeRenderHelper.s_OrderBlocks.Count; i++)
				{
					UnityAction callback = BeforeRenderHelper.s_OrderBlocks[i].callback;
					bool flag2 = callback != null;
					if (flag2)
					{
						callback();
					}
				}
			}
		}

		// Token: 0x0400047F RID: 1151
		private static List<BeforeRenderHelper.OrderBlock> s_OrderBlocks = new List<BeforeRenderHelper.OrderBlock>();

		// Token: 0x0200015B RID: 347
		private struct OrderBlock
		{
			// Token: 0x04000480 RID: 1152
			internal int order;

			// Token: 0x04000481 RID: 1153
			internal UnityAction callback;
		}
	}
}
