using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000062 RID: 98
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextHandleTemporaryCache
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00023D28 File Offset: 0x00021F28
		public void ClearTemporaryCache()
		{
			for (int i = 0; i < this.s_TextInfoPool.Count; i++)
			{
				this.s_TextInfoPool.First.Value.RemoveFromCache();
			}
			this.s_TextInfoPool.Clear();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00023D74 File Offset: 0x00021F74
		public void AddTextInfoToCache(TextHandle textHandle, int hashCode)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool isCachedPermanent = textHandle.IsCachedPermanent;
				if (isCachedPermanent)
				{
					return;
				}
				bool flag2 = !TextGenerator.IsExecutingJob;
				bool flag3 = flag2;
				if (flag3)
				{
					this.currentFrame = Time.frameCount;
				}
				bool flag4 = this.s_TextInfoPool.Count > 0 && ((double)this.currentFrame - this.s_TextInfoPool.Last.Value.lastTimeInCache < 0.0 || (double)this.currentFrame - this.s_TextInfoPool.First.Value.lastTimeInCache < 0.0);
				if (flag4)
				{
					this.ClearTemporaryCache();
				}
				bool isCachedTemporary = textHandle.IsCachedTemporary;
				if (isCachedTemporary)
				{
					this.RefreshCaching(textHandle);
					return;
				}
				bool flag5 = this.s_TextInfoPool.Count > 0 && (double)this.currentFrame - this.s_TextInfoPool.Last.Value.lastTimeInCache > 2.0;
				if (flag5)
				{
					this.RecycleTextInfoFromCache(textHandle);
				}
				else
				{
					TextInfo textInfo = new TextInfo();
					textHandle.TextInfoNode = new LinkedListNode<TextInfo>(textInfo);
					this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
					textInfo.lastTimeInCache = (double)this.currentFrame;
					TextInfo textInfo2 = textInfo;
					textInfo2.removedFromCache = (Action)Delegate.Combine(textInfo2.removedFromCache, new Action(textHandle.RemoveTextInfoFromTemporaryCache));
				}
			}
			textHandle.IsCachedTemporary = true;
			textHandle.SetDirty();
			textHandle.UpdateWithHash(hashCode);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00023F34 File Offset: 0x00022134
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		public virtual void RemoveTextInfoFromCache(TextHandle textHandle)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = !textHandle.IsCachedTemporary;
				if (!flag2)
				{
					textHandle.IsCachedTemporary = false;
					textHandle.TextInfoNode.Value.lastTimeInCache = 0.0;
					textHandle.TextInfoNode.Value.removedFromCache = null;
					bool flag3 = textHandle.TextInfoNode != null;
					if (flag3)
					{
						this.s_TextInfoPool.Remove(textHandle.TextInfoNode);
						this.s_TextInfoPool.AddLast(textHandle.TextInfoNode);
					}
					textHandle.TextInfoNode = null;
				}
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00023FF0 File Offset: 0x000221F0
		private void RefreshCaching(TextHandle textHandle)
		{
			bool flag = !TextGenerator.IsExecutingJob;
			if (flag)
			{
				this.currentFrame = Time.frameCount;
			}
			textHandle.TextInfoNode.Value.lastTimeInCache = (double)this.currentFrame;
			this.s_TextInfoPool.Remove(textHandle.TextInfoNode);
			this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00024050 File Offset: 0x00022250
		private void RecycleTextInfoFromCache(TextHandle textHandle)
		{
			bool flag = !TextGenerator.IsExecutingJob;
			if (flag)
			{
				this.currentFrame = Time.frameCount;
			}
			textHandle.TextInfoNode = this.s_TextInfoPool.Last;
			textHandle.TextInfoNode.Value.RemoveFromCache();
			this.s_TextInfoPool.RemoveLast();
			this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
			textHandle.IsCachedTemporary = true;
			TextInfo value = textHandle.TextInfoNode.Value;
			value.removedFromCache = (Action)Delegate.Combine(value.removedFromCache, new Action(textHandle.RemoveTextInfoFromTemporaryCache));
			textHandle.TextInfoNode.Value.lastTimeInCache = (double)this.currentFrame;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00024101 File Offset: 0x00022301
		public void UpdateCurrentFrame()
		{
			this.currentFrame = Time.frameCount;
		}

		// Token: 0x04000378 RID: 888
		internal LinkedList<TextInfo> s_TextInfoPool = new LinkedList<TextInfo>();

		// Token: 0x04000379 RID: 889
		internal const int s_MinFramesInCache = 2;

		// Token: 0x0400037A RID: 890
		internal int currentFrame;

		// Token: 0x0400037B RID: 891
		private object syncRoot = new object();
	}
}
