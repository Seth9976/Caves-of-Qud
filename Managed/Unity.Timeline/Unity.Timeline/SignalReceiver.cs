using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002F RID: 47
	public class SignalReceiver : MonoBehaviour, INotificationReceiver
	{
		// Token: 0x06000255 RID: 597 RVA: 0x000088F8 File Offset: 0x00006AF8
		public void OnNotify(Playable origin, INotification notification, object context)
		{
			SignalEmitter signalEmitter = notification as SignalEmitter;
			UnityEvent unityEvent;
			if (signalEmitter != null && signalEmitter.asset != null && this.m_Events.TryGetValue(signalEmitter.asset, out unityEvent) && unityEvent != null)
			{
				unityEvent.Invoke();
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008944 File Offset: 0x00006B44
		public void AddReaction(SignalAsset asset, UnityEvent reaction)
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("SignalAsset already used.");
			}
			this.m_Events.Append(asset, reaction);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008990 File Offset: 0x00006B90
		public int AddEmptyReaction(UnityEvent reaction)
		{
			this.m_Events.Append(null, reaction);
			return this.m_Events.events.Count - 1;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000089B1 File Offset: 0x00006BB1
		public void Remove(SignalAsset asset)
		{
			if (!this.m_Events.signals.Contains(asset))
			{
				throw new ArgumentException("The SignalAsset is not registered with this receiver.");
			}
			this.m_Events.Remove(asset);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000089DD File Offset: 0x00006BDD
		public IEnumerable<SignalAsset> GetRegisteredSignals()
		{
			return this.m_Events.signals;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000089EC File Offset: 0x00006BEC
		public UnityEvent GetReaction(SignalAsset key)
		{
			UnityEvent unityEvent;
			if (this.m_Events.TryGetValue(key, out unityEvent))
			{
				return unityEvent;
			}
			return null;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008A0C File Offset: 0x00006C0C
		public int Count()
		{
			return this.m_Events.signals.Count;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008A20 File Offset: 0x00006C20
		public void ChangeSignalAtIndex(int idx, SignalAsset newKey)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			if (this.m_Events.signals[idx] == newKey)
			{
				return;
			}
			bool flag = this.m_Events.signals.Contains(newKey);
			if (newKey == null || this.m_Events.signals[idx] == null || !flag)
			{
				this.m_Events.signals[idx] = newKey;
			}
			if (newKey != null && flag)
			{
				throw new ArgumentException("SignalAsset already used.");
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008AC5 File Offset: 0x00006CC5
		public void RemoveAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.Remove(idx);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008AF2 File Offset: 0x00006CF2
		public void ChangeReactionAtIndex(int idx, UnityEvent reaction)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			this.m_Events.events[idx] = reaction;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008B25 File Offset: 0x00006D25
		public UnityEvent GetReactionAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.events.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.events[idx];
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008B57 File Offset: 0x00006D57
		public SignalAsset GetSignalAssetAtIndex(int idx)
		{
			if (idx < 0 || idx > this.m_Events.signals.Count - 1)
			{
				throw new IndexOutOfRangeException();
			}
			return this.m_Events.signals[idx];
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008B89 File Offset: 0x00006D89
		private void OnEnable()
		{
		}

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		private SignalReceiver.EventKeyValue m_Events = new SignalReceiver.EventKeyValue();

		// Token: 0x02000078 RID: 120
		[Serializable]
		private class EventKeyValue
		{
			// Token: 0x0600037D RID: 893 RVA: 0x0000C448 File Offset: 0x0000A648
			public bool TryGetValue(SignalAsset key, out UnityEvent value)
			{
				int num = this.m_Signals.IndexOf(key);
				if (num != -1)
				{
					value = this.m_Events[num];
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0000C47A File Offset: 0x0000A67A
			public void Append(SignalAsset key, UnityEvent value)
			{
				this.m_Signals.Add(key);
				this.m_Events.Add(value);
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000C494 File Offset: 0x0000A694
			public void Remove(int idx)
			{
				if (idx != -1)
				{
					this.m_Signals.RemoveAt(idx);
					this.m_Events.RemoveAt(idx);
				}
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
			public void Remove(SignalAsset key)
			{
				int num = this.m_Signals.IndexOf(key);
				if (num != -1)
				{
					this.m_Signals.RemoveAt(num);
					this.m_Events.RemoveAt(num);
				}
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000381 RID: 897 RVA: 0x0000C4EA File Offset: 0x0000A6EA
			public List<SignalAsset> signals
			{
				get
				{
					return this.m_Signals;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C4F2 File Offset: 0x0000A6F2
			public List<UnityEvent> events
			{
				get
				{
					return this.m_Events;
				}
			}

			// Token: 0x0400017E RID: 382
			[SerializeField]
			private List<SignalAsset> m_Signals = new List<SignalAsset>();

			// Token: 0x0400017F RID: 383
			[SerializeField]
			[CustomSignalEventDrawer]
			private List<UnityEvent> m_Events = new List<UnityEvent>();
		}
	}
}
