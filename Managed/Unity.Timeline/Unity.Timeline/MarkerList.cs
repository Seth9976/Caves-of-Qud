using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	internal struct MarkerList : ISerializationCallbackReceiver
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008536 File Offset: 0x00006736
		public List<IMarker> markers
		{
			get
			{
				this.BuildCache();
				return this.m_Cache;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008544 File Offset: 0x00006744
		public MarkerList(int capacity)
		{
			this.m_Objects = new List<ScriptableObject>(capacity);
			this.m_Cache = new List<IMarker>(capacity);
			this.m_CacheDirty = true;
			this.m_HasNotifications = false;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000856C File Offset: 0x0000676C
		public void Add(ScriptableObject item)
		{
			if (item == null)
			{
				return;
			}
			this.m_Objects.Add(item);
			this.m_CacheDirty = true;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000858B File Offset: 0x0000678B
		public bool Remove(IMarker item)
		{
			if (!(item is ScriptableObject))
			{
				throw new InvalidOperationException("Supplied type must be a ScriptableObject");
			}
			return this.Remove((ScriptableObject)item, item.parent.timelineAsset, item.parent);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000085BD File Offset: 0x000067BD
		public bool Remove(ScriptableObject item, TimelineAsset timelineAsset, PlayableAsset thingToDirty)
		{
			if (!this.m_Objects.Contains(item))
			{
				return false;
			}
			this.m_Objects.Remove(item);
			this.m_CacheDirty = true;
			TimelineUndo.PushDestroyUndo(timelineAsset, thingToDirty, item);
			return true;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000085EC File Offset: 0x000067EC
		public void Clear()
		{
			this.m_Objects.Clear();
			this.m_CacheDirty = true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008600 File Offset: 0x00006800
		public bool Contains(ScriptableObject item)
		{
			return this.m_Objects.Contains(item);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000860E File Offset: 0x0000680E
		public IEnumerable<IMarker> GetMarkers()
		{
			return this.markers;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00008616 File Offset: 0x00006816
		public int Count
		{
			get
			{
				return this.markers.Count;
			}
		}

		// Token: 0x170000AD RID: 173
		public IMarker this[int idx]
		{
			get
			{
				return this.markers[idx];
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008631 File Offset: 0x00006831
		public List<ScriptableObject> GetRawMarkerList()
		{
			return this.m_Objects;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000863C File Offset: 0x0000683C
		public IMarker CreateMarker(Type type, double time, TrackAsset owner)
		{
			if (!typeof(ScriptableObject).IsAssignableFrom(type) || !typeof(IMarker).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("The requested type needs to inherit from ScriptableObject and implement IMarker");
			}
			if (!owner.supportsNotifications && typeof(INotification).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Markers implementing the INotification interface cannot be added on tracks that do not support notifications");
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateInstance(type);
			IMarker marker = (IMarker)scriptableObject;
			marker.time = time;
			TimelineCreateUtilities.SaveAssetIntoObject(scriptableObject, owner);
			this.Add(scriptableObject);
			marker.Initialize(owner);
			return marker;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000086C6 File Offset: 0x000068C6
		public bool HasNotifications()
		{
			this.BuildCache();
			return this.m_HasNotifications;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000086D4 File Offset: 0x000068D4
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000086D6 File Offset: 0x000068D6
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.m_CacheDirty = true;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000086E0 File Offset: 0x000068E0
		private void BuildCache()
		{
			if (this.m_CacheDirty)
			{
				this.m_Cache = new List<IMarker>(this.m_Objects.Count);
				this.m_HasNotifications = false;
				foreach (ScriptableObject scriptableObject in this.m_Objects)
				{
					if (scriptableObject != null)
					{
						this.m_Cache.Add(scriptableObject as IMarker);
						if (scriptableObject is INotification)
						{
							this.m_HasNotifications = true;
						}
					}
				}
				this.m_CacheDirty = false;
			}
		}

		// Token: 0x040000CA RID: 202
		[SerializeField]
		[HideInInspector]
		private List<ScriptableObject> m_Objects;

		// Token: 0x040000CB RID: 203
		[HideInInspector]
		[NonSerialized]
		private List<IMarker> m_Cache;

		// Token: 0x040000CC RID: 204
		private bool m_CacheDirty;

		// Token: 0x040000CD RID: 205
		private bool m_HasNotifications;
	}
}
