using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000061 RID: 97
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextHandlePermanentCache
	{
		// Token: 0x06000316 RID: 790 RVA: 0x00023BD4 File Offset: 0x00021DD4
		public virtual void AddTextInfoToCache(TextHandle textHandle)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool isCachedPermanent = textHandle.IsCachedPermanent;
				if (isCachedPermanent)
				{
					return;
				}
				bool isCachedTemporary = textHandle.IsCachedTemporary;
				if (isCachedTemporary)
				{
					textHandle.RemoveTextInfoFromTemporaryCache();
				}
				bool flag2 = this.s_TextInfoPool.Count > 0;
				if (flag2)
				{
					textHandle.TextInfoNode = this.s_TextInfoPool.Last;
					this.s_TextInfoPool.RemoveLast();
				}
				else
				{
					TextInfo textInfo = new TextInfo();
					textHandle.TextInfoNode = new LinkedListNode<TextInfo>(textInfo);
				}
			}
			textHandle.IsCachedPermanent = true;
			textHandle.SetDirty();
			textHandle.Update();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00023C98 File Offset: 0x00021E98
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		public void RemoveTextInfoFromCache(TextHandle textHandle)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = !textHandle.IsCachedPermanent;
				if (!flag2)
				{
					this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
					textHandle.TextInfoNode = null;
					textHandle.IsCachedPermanent = false;
				}
			}
		}

		// Token: 0x04000376 RID: 886
		internal LinkedList<TextInfo> s_TextInfoPool = new LinkedList<TextInfo>();

		// Token: 0x04000377 RID: 887
		private object syncRoot = new object();
	}
}
