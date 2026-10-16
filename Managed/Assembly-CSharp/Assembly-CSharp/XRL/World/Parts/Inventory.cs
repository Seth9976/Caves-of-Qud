using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qud.API;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FE7 RID: 4071
	[Serializable]
	public class Inventory : IPart, IInventory
	{
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600A1C7 RID: 41415 RVA: 0x003AFE17 File Offset: 0x003AE017
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x0600A1C8 RID: 41416 RVA: 0x003AFE1E File Offset: 0x003AE01E
		public override void Attach()
		{
			this.ParentObject.Inventory = this;
		}

		// Token: 0x0600A1C9 RID: 41417 RVA: 0x003AFE2C File Offset: 0x003AE02C
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Inventory : null) == this)
			{
				this.ParentObject.Inventory = null;
			}
		}

		// Token: 0x0600A1CA RID: 41418 RVA: 0x003AFE4F File Offset: 0x003AE04F
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteGameObjectList(this.Objects);
			base.Write(Basis, Writer);
		}

		// Token: 0x0600A1CB RID: 41419 RVA: 0x003AFE68 File Offset: 0x003AE068
		public int Count(string blueprint)
		{
			int num = 0;
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				if (this.Objects[i].Blueprint == blueprint)
				{
					num += this.Objects[i].Count;
				}
			}
			return num;
		}

		// Token: 0x0600A1CC RID: 41420 RVA: 0x003AFEC0 File Offset: 0x003AE0C0
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadGameObjectList(this.Objects, null);
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				if (this.Objects[i] == null)
				{
					this.Objects.RemoveAt(i);
				}
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x0600A1CD RID: 41421 RVA: 0x003AFF14 File Offset: 0x003AE114
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A1CE RID: 41422 RVA: 0x003AFF18 File Offset: 0x003AE118
		public void Validate()
		{
			List<GameObject> list = null;
			if (this.Objects != null)
			{
				for (int i = this.Objects.Count - 1; i >= 0; i--)
				{
					if (!this.Objects[i].IsValid())
					{
						if (list == null)
						{
							list = new List<GameObject>(1) { this.Objects[i] };
						}
						else
						{
							list.Add(this.Objects[i]);
						}
					}
				}
			}
			if (list != null)
			{
				foreach (GameObject gameObject in list)
				{
					this.Objects.Remove(gameObject);
				}
				if (this.Objects.Count == 0)
				{
					this.CheckEmptyState();
				}
			}
		}

		// Token: 0x0600A1CF RID: 41423 RVA: 0x003AFFE8 File Offset: 0x003AE1E8
		public bool TryStoreBackup()
		{
			List<GameObject> objects = this.Objects;
			if (objects == null)
			{
				return false;
			}
			if (this.HasObjectDirect(new Predicate<GameObject>(Inventory.IsEmptyObject)))
			{
				MetricsManager.LogError("Inventory has invalid objects, skipping backup store.");
				return false;
			}
			SerializationWriter serializationWriter;
			if (!SerializationWriter.TryGetShared(out serializationWriter))
			{
				return false;
			}
			try
			{
				this.ParentObject.RemoveStringProperty("InventoryBase64");
				serializationWriter.Start(409, false);
				serializationWriter.WriteGameObjectList(objects);
				serializationWriter.FinalizeWrite();
				byte[] buffer = serializationWriter.Stream.GetBuffer();
				int num = (int)serializationWriter.Stream.Position;
				string text = Convert.ToBase64String(buffer, 0, num);
				this.ParentObject.SetStringProperty("InventoryBase64", text, false);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error writing container backup", ex, "game_exception");
				return false;
			}
			finally
			{
				SerializationWriter.ReleaseShared();
			}
			return true;
		}

		// Token: 0x0600A1D0 RID: 41424 RVA: 0x003B00C8 File Offset: 0x003AE2C8
		public bool TryRestoreBackup()
		{
			string stringProperty = this.ParentObject.GetStringProperty("InventoryBase64", null);
			if (stringProperty.IsNullOrEmpty())
			{
				return false;
			}
			SerializationReader serializationReader;
			if (!SerializationReader.TryGetShared(out serializationReader))
			{
				return false;
			}
			try
			{
				serializationReader.Stream.SetLength((long)((4 * stringProperty.Length / 3 + 3) & -4));
				byte[] buffer = serializationReader.Stream.GetBuffer();
				int num;
				if (!Convert.TryFromBase64String(stringProperty, buffer.AsSpan<byte>(), out num))
				{
					throw new Exception("Failed to write Base64 string to buffer.");
				}
				List<GameObject> list;
				if ((list = this.Objects) == null)
				{
					list = (this.Objects = new List<GameObject>());
				}
				List<GameObject> list2 = list;
				list2.Clear();
				serializationReader.Start(false);
				serializationReader.ReadGameObjectList(list2, null);
				serializationReader.FinalizeRead();
				foreach (GameObject gameObject in list2)
				{
					if (gameObject.Physics != null)
					{
						gameObject.Physics._InInventory = this.ParentObject;
					}
				}
				if (this.HasObjectDirect(new Predicate<GameObject>(Inventory.IsEmptyObject)))
				{
					MetricsManager.LogError("Restored backup has invalid objects, removing.");
					this.RemoveAll(new Predicate<GameObject>(Inventory.IsEmptyObject), true);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Error reading container backup", ex, "game_exception");
				return false;
			}
			finally
			{
				SerializationReader.ReleaseShared();
			}
			return true;
		}

		// Token: 0x0600A1D1 RID: 41425 RVA: 0x003B0260 File Offset: 0x003AE460
		private static bool IsEmptyObject(GameObject Object)
		{
			string blueprint = Object.Blueprint;
			return blueprint == "Object" || blueprint == "*PooledObject";
		}

		// Token: 0x0600A1D2 RID: 41426 RVA: 0x003B0298 File Offset: 0x003AE498
		public void VerifyContents()
		{
			if (!this.HasObjectDirect(new Predicate<GameObject>(Inventory.IsEmptyObject)))
			{
				return;
			}
			bool flag = this.TryRestoreBackup();
			if (!flag)
			{
				this.RemoveAll(new Predicate<GameObject>(Inventory.IsEmptyObject), true);
			}
			this.FlushWeightCache();
			Zone anyBasisZone = this.GetAnyBasisZone();
			string text = ((anyBasisZone != null) ? anyBasisZone.ZoneID : null) ?? "unknown";
			MetricsManager.LogError(string.Concat(new string[]
			{
				"Invalid inventory objects on ",
				this.ParentObject.DebugName,
				" in ",
				text,
				", ",
				flag ? "restored backup" : "cleared objects",
				"."
			}));
		}

		// Token: 0x0600A1D3 RID: 41427 RVA: 0x003B0350 File Offset: 0x003AE550
		public void AddObject(List<GameObject> Objects)
		{
			foreach (GameObject gameObject in Objects)
			{
				this.AddObject(gameObject, null, false, false, false, null);
			}
			base.FlushTransientCaches();
		}

		// Token: 0x0600A1D4 RID: 41428 RVA: 0x003B03AC File Offset: 0x003AE5AC
		public GameObject AddObjectNoStack(GameObject GO)
		{
			return this.AddObject(GO, null, false, true, true, null);
		}

		// Token: 0x0600A1D5 RID: 41429 RVA: 0x003B03BC File Offset: 0x003AE5BC
		public GameObject AddObject(string Blueprint, bool Silent = false, bool NoStack = false)
		{
			return this.AddObject(GameObject.Create(Blueprint), null, Silent, NoStack, true, null);
		}

		// Token: 0x0600A1D6 RID: 41430 RVA: 0x003B03DC File Offset: 0x003AE5DC
		public GameObject AddObjectToInventory(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, string Context = null, IEvent ParentEvent = null)
		{
			Object.RemoveFromContext(ParentEvent);
			return this.AddObject(Object, null, Silent, NoStack, FlushTransient, ParentEvent);
		}

		// Token: 0x0600A1D7 RID: 41431 RVA: 0x003B03F8 File Offset: 0x003AE5F8
		public GameObject AddObject(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, IEvent ParentEvent = null)
		{
			Physics physics = Object.Physics;
			if (physics == null || !physics.Takeable)
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Attempting to add untakeable object '",
					Object.DebugName,
					"' to inventory of '",
					this.ParentObject.DebugName,
					"'."
				}));
				if (this.ParentObject.IsPlayer())
				{
					AutoAct.Interrupt(Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " can't be picked up", null, null, false);
				}
				return Object;
			}
			if (Object.IsInGraveyard())
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Attempting to add graveyard object '",
					Object.DebugName,
					"' to inventory of '",
					this.ParentObject.DebugName,
					"'."
				}));
				return Object;
			}
			if (Object.IsInvalid())
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Attempting to add invalid object '",
					Object.DebugName,
					"' to inventory of '",
					this.ParentObject.DebugName,
					"'."
				}));
				return Object;
			}
			bool flag = this.Objects.Count == 0;
			this.Objects.Add(Object);
			Cell currentCell = Object.CurrentCell;
			Object.Physics.InInventory = this.ParentObject;
			if (currentCell != null)
			{
				currentCell.RemoveObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				Cell currentCell2 = Object.CurrentCell;
				if (currentCell2 != null && currentCell2.Objects.Contains(Object))
				{
					currentCell2.Objects.Remove(Object);
				}
				Object.Physics.CurrentCell = null;
			}
			this.FlushWeightCache();
			if (FlushTransient)
			{
				base.FlushTransientCaches();
			}
			if (flag)
			{
				this.CheckNonEmptyState();
			}
			AddedToInventoryEvent.Send(Actor ?? this.ParentObject, Object, Silent, NoStack, ParentEvent);
			if (this.ParentObject != null && this.ParentObject.IsPlayer() && Object != null)
			{
				this.ParentObject.FireEvent(Event.New("ObjectAddedToPlayerInventory", "Object", Object));
			}
			EncumbranceChangedEvent.Send(this.ParentObject, Object, Silent);
			return Object;
		}

		// Token: 0x0600A1D8 RID: 41432 RVA: 0x003B0620 File Offset: 0x003AE820
		public void RemoveAll(Predicate<GameObject> Match, bool Obliterate = true)
		{
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = this.Objects[i];
				if (Match(gameObject))
				{
					this.Objects.RemoveAt(i);
					gameObject.Physics.InInventory = null;
					if (Obliterate)
					{
						gameObject.Obliterate(null, true, null);
					}
				}
			}
			this.FlushWeightCache();
			if (this.Objects.Count == 0)
			{
				this.CheckEmptyState();
			}
			EncumbranceChangedEvent.Send(this.ParentObject, null, false);
		}

		// Token: 0x0600A1D9 RID: 41433 RVA: 0x003B06A8 File Offset: 0x003AE8A8
		public bool RemoveObjectFromInventory(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, string Context = null, IEvent ParentEvent = null)
		{
			if (!this.Objects.Contains(Object))
			{
				return false;
			}
			this.Objects.Remove(Object);
			Object.Physics.InInventory = null;
			this.FlushWeightCache();
			if (this.Objects.Count == 0)
			{
				this.CheckEmptyState();
			}
			EncumbranceChangedEvent.Send(this.ParentObject, Object, Silent);
			return true;
		}

		// Token: 0x0600A1DA RID: 41434 RVA: 0x003B0708 File Offset: 0x003AE908
		public void RemoveObject(GameObject Object, IEvent ParentEvent = null)
		{
			this.Objects.Remove(Object);
			Object.Physics.InInventory = null;
			this.FlushWeightCache();
			if (this.Objects.Count == 0)
			{
				this.CheckEmptyState();
			}
			EncumbranceChangedEvent.Send(this.ParentObject, Object, false);
		}

		// Token: 0x0600A1DB RID: 41435 RVA: 0x003B0755 File Offset: 0x003AE955
		public void Clear()
		{
			this.Objects.Clear();
			this.FlushWeightCache();
			this.CheckEmptyState();
			EncumbranceChangedEvent.Send(this.ParentObject, null, false);
		}

		// Token: 0x0600A1DC RID: 41436 RVA: 0x003B077C File Offset: 0x003AE97C
		[Obsolete("version with MapInv argument should always be called")]
		public override IPart DeepCopy(GameObject Parent)
		{
			return base.DeepCopy(Parent);
		}

		// Token: 0x0600A1DD RID: 41437 RVA: 0x003B0788 File Offset: 0x003AE988
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Inventory inventory = (Inventory)base.DeepCopy(Parent, MapInv);
			inventory.Objects = new List<GameObject>(this.Objects.Count);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = ((MapInv != null) ? MapInv(this.Objects[i]) : null) ?? this.Objects[i].DeepCopy(false, false, MapInv);
				if (gameObject != null)
				{
					gameObject.Physics.InInventory = Parent;
					inventory.Objects.Add(gameObject);
				}
				i++;
			}
			return inventory;
		}

		// Token: 0x0600A1DE RID: 41438 RVA: 0x003B081E File Offset: 0x003AEA1E
		public override void FinalizeCopyLate(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopyLate(Source, CopyEffects, CopyID, MapInv);
			if (this.Objects.Count == 0)
			{
				this.CheckEmptyState();
				return;
			}
			this.CheckNonEmptyState();
		}

		// Token: 0x0600A1DF RID: 41439 RVA: 0x003B0845 File Offset: 0x003AEA45
		public int GetWeight()
		{
			if (this.WeightCache != -1)
			{
				return this.WeightCache;
			}
			return this.RecalculateWeight();
		}

		// Token: 0x0600A1E0 RID: 41440 RVA: 0x003B0860 File Offset: 0x003AEA60
		public void FlushWeightCache()
		{
			this.WeightCache = -1;
			this.ParentObject.FlushCarriedWeightCache();
			GameObject inInventory = this.ParentObject.InInventory;
			Inventory inventory = ((inInventory != null) ? inInventory.Inventory : null);
			if (inventory != null && inventory.WeightCache != -1)
			{
				inventory.FlushWeightCache();
			}
		}

		// Token: 0x0600A1E1 RID: 41441 RVA: 0x003B08AC File Offset: 0x003AEAAC
		private int RecalculateWeight()
		{
			this.WeightCache = 0;
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				this.WeightCache += this.Objects[i].Weight;
			}
			return this.WeightCache;
		}

		// Token: 0x0600A1E2 RID: 41442 RVA: 0x003B08FC File Offset: 0x003AEAFC
		public GameObject FindObject(Predicate<GameObject> pFilter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (pFilter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600A1E3 RID: 41443 RVA: 0x003B0944 File Offset: 0x003AEB44
		public GameObject FindObjectByBlueprint(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600A1E4 RID: 41444 RVA: 0x003B0990 File Offset: 0x003AEB90
		public GameObject FindObjectByBlueprint(string Blueprint, Predicate<GameObject> pFilter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint && pFilter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600A1E5 RID: 41445 RVA: 0x003B09F0 File Offset: 0x003AEBF0
		public List<GameObject> GetObjectsDirect()
		{
			this.FlushWeightCache();
			return this.Objects;
		}

		// Token: 0x0600A1E6 RID: 41446 RVA: 0x003B0A00 File Offset: 0x003AEC00
		public List<GameObject> GetObjectsDirect(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjectsDirect();
			}
			this.FlushWeightCache();
			List<GameObject> list = new List<GameObject>(this.Objects.Count);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600A1E7 RID: 41447 RVA: 0x003B0A6D File Offset: 0x003AEC6D
		public void GetObjectsDirect(List<GameObject> store)
		{
			this.FlushWeightCache();
			store.AddRange(this.Objects);
		}

		// Token: 0x0600A1E8 RID: 41448 RVA: 0x003B0A84 File Offset: 0x003AEC84
		public void GetObjectsDirect(List<GameObject> store, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				this.GetObjectsDirect(store);
				return;
			}
			this.FlushWeightCache();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					store.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x0600A1E9 RID: 41449 RVA: 0x003B0AE0 File Offset: 0x003AECE0
		public List<GameObject> GetObjects()
		{
			return this.GetObjectsDirect((GameObject obj) => !obj.HasTag("HiddenInInventory"));
		}

		// Token: 0x0600A1EA RID: 41450 RVA: 0x003B0B08 File Offset: 0x003AED08
		public void GetObjects(List<GameObject> Store)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (!this.Objects[i].HasTag("HiddenInInventory"))
				{
					Store.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x0600A1EB RID: 41451 RVA: 0x003B0B58 File Offset: 0x003AED58
		public void GetObjects(List<GameObject> Store, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				this.GetObjects(Store);
				return;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]) && !this.Objects[i].HasTag("HiddenInInventory"))
				{
					Store.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x0600A1EC RID: 41452 RVA: 0x003B0BC8 File Offset: 0x003AEDC8
		public List<GameObject> GetObjects(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjects();
			}
			List<GameObject> list = new List<GameObject>(this.Objects.Count);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (!this.Objects[i].HasTag("HiddenInInventory") && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600A1ED RID: 41453 RVA: 0x003B0C48 File Offset: 0x003AEE48
		public List<GameObject> GetObjectsViaEventList(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjects();
			}
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (!this.Objects[i].HasTag("HiddenInInventory") && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600A1EE RID: 41454 RVA: 0x003B0CBC File Offset: 0x003AEEBC
		public List<GameObject> GetObjectsReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetObjectsDirect(list, (GameObject obj) => !obj.HasTag("HiddenInInventory"));
			return list;
		}

		// Token: 0x0600A1EF RID: 41455 RVA: 0x003B0CF8 File Offset: 0x003AEEF8
		public List<GameObject> GetObjectsReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjectsReadonly();
			}
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (!this.Objects[i].HasTag("HiddenInInventory") && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600A1F0 RID: 41456 RVA: 0x003B0D6C File Offset: 0x003AEF6C
		public List<GameObject> GetObjectsWithTag(string Name)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Name) && !this.Objects[i].HasTag("HiddenInInventory"))
				{
					num++;
				}
				i++;
			}
			List<GameObject> list = new List<GameObject>(num);
			int j = 0;
			int count2 = this.Objects.Count;
			while (j < count2)
			{
				if (this.Objects[j].HasTag(Name) && !this.Objects[j].HasTag("HiddenInInventory"))
				{
					list.Add(this.Objects[j]);
				}
				j++;
			}
			return list;
		}

		// Token: 0x0600A1F1 RID: 41457 RVA: 0x003B0E2C File Offset: 0x003AF02C
		public GameObject GetFirstObject()
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory"))
					{
						return this.Objects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600A1F2 RID: 41458 RVA: 0x003B0E80 File Offset: 0x003AF080
		public GameObject GetFirstObject(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetFirstObject();
			}
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && Filter(this.Objects[i]))
					{
						return this.Objects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600A1F3 RID: 41459 RVA: 0x003B0EF1 File Offset: 0x003AF0F1
		public GameObject GetFirstObjectDirect()
		{
			List<GameObject> objects = this.Objects;
			if (objects == null)
			{
				return null;
			}
			return objects.First<GameObject>();
		}

		// Token: 0x0600A1F4 RID: 41460 RVA: 0x003B0F04 File Offset: 0x003AF104
		public GameObject GetFirstObjectDirect(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetFirstObjectDirect();
			}
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (Filter(this.Objects[i]))
					{
						return this.Objects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600A1F5 RID: 41461 RVA: 0x003B0F5D File Offset: 0x003AF15D
		public int GetObjectCountDirect()
		{
			List<GameObject> objects = this.Objects;
			if (objects == null)
			{
				return 0;
			}
			return objects.Count;
		}

		// Token: 0x0600A1F6 RID: 41462 RVA: 0x003B0F70 File Offset: 0x003AF170
		public int GetObjectCountDirect(Predicate<GameObject> pFilter)
		{
			int num = 0;
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (pFilter(this.Objects[i]))
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600A1F7 RID: 41463 RVA: 0x003B0FB8 File Offset: 0x003AF1B8
		public int GetObjectStackCount(bool IncludeHidden = true)
		{
			int num = 0;
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (IncludeHidden || !this.Objects[i].HasTag("HiddenInInventory"))
					{
						num += this.Objects[i].Count;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600A1F8 RID: 41464 RVA: 0x003B1018 File Offset: 0x003AF218
		public int GetObjectStackCount(Predicate<GameObject> Filter, bool IncludeHidden = true)
		{
			int num = 0;
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if ((IncludeHidden || !this.Objects[i].HasTag("HiddenInInventory")) && Filter(this.Objects[i]))
					{
						num += this.Objects[i].Count;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600A1F9 RID: 41465 RVA: 0x003B108C File Offset: 0x003AF28C
		public int GetObjectCount()
		{
			int num = 0;
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory"))
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600A1FA RID: 41466 RVA: 0x003B10D8 File Offset: 0x003AF2D8
		public int GetObjectCount(Predicate<GameObject> pFilter)
		{
			int num = 0;
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && pFilter(this.Objects[i]))
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600A1FB RID: 41467 RVA: 0x003B1138 File Offset: 0x003AF338
		public bool HasObject()
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory"))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600A1FC RID: 41468 RVA: 0x003B1180 File Offset: 0x003AF380
		public Cell GetInventoryCell()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return null;
			}
			return parentObject.CurrentCell;
		}

		// Token: 0x0600A1FD RID: 41469 RVA: 0x003B1193 File Offset: 0x003AF393
		public Zone GetInventoryZone()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return null;
			}
			return parentObject.CurrentZone;
		}

		// Token: 0x0600A1FE RID: 41470 RVA: 0x003B11A6 File Offset: 0x003AF3A6
		public bool InventoryContains(GameObject Object)
		{
			return this.Objects.Contains(Object);
		}

		// Token: 0x0600A1FF RID: 41471 RVA: 0x003B11B4 File Offset: 0x003AF3B4
		public bool HasObject(GameObject Object)
		{
			return this.Objects.Contains(Object) && !Object.HasTag("HiddenInInventory");
		}

		// Token: 0x0600A200 RID: 41472 RVA: 0x003B11D4 File Offset: 0x003AF3D4
		public bool HasObject(string Blueprint)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (this.Objects[i].Blueprint == Blueprint && !this.Objects[i].HasTag("HiddenInInventory"))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600A201 RID: 41473 RVA: 0x003B1238 File Offset: 0x003AF438
		public bool HasObject(Predicate<GameObject> pFilter)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && pFilter(this.Objects[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600A202 RID: 41474 RVA: 0x003B1294 File Offset: 0x003AF494
		public bool HasObjectDirect(GameObject obj)
		{
			return this.Objects.Contains(obj);
		}

		// Token: 0x0600A203 RID: 41475 RVA: 0x003B12A4 File Offset: 0x003AF4A4
		public bool HasObjectDirect(string Blueprint)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (this.Objects[i].Blueprint == Blueprint && !this.Objects[i].HasTag("HiddenInInventory"))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600A204 RID: 41476 RVA: 0x003B1308 File Offset: 0x003AF508
		public bool HasObjectDirect(Predicate<GameObject> pFilter)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (pFilter(this.Objects[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600A205 RID: 41477 RVA: 0x003B134C File Offset: 0x003AF54C
		public void ForeachObject(Action<GameObject> aProc)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory"))
					{
						aProc(this.Objects[i]);
					}
					i++;
				}
			}
		}

		// Token: 0x0600A206 RID: 41478 RVA: 0x003B13A4 File Offset: 0x003AF5A4
		public bool ForeachObject(Predicate<GameObject> pProc)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && !pProc(this.Objects[i]))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600A207 RID: 41479 RVA: 0x003B1400 File Offset: 0x003AF600
		public void ForeachObject(Action<GameObject> aProc, Predicate<GameObject> pFilter)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && pFilter(this.Objects[i]))
					{
						aProc(this.Objects[i]);
					}
					i++;
				}
			}
		}

		// Token: 0x0600A208 RID: 41480 RVA: 0x003B146C File Offset: 0x003AF66C
		public bool ForeachObject(Predicate<GameObject> pProc, Predicate<GameObject> pFilter)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && pFilter(this.Objects[i]) && !pProc(this.Objects[i]))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600A209 RID: 41481 RVA: 0x003B14DC File Offset: 0x003AF6DC
		public void SafeForeachObject(Action<GameObject> aProc)
		{
			if (this.Objects.Count == 0)
			{
				return;
			}
			List<GameObject> list = null;
			for (int i = 0; i < ((list == null) ? this.Objects.Count : list.Count); i++)
			{
				GameObject gameObject = ((list == null) ? this.Objects[i] : list[i]);
				if (!gameObject.HasTag("HiddenInInventory"))
				{
					if (list == null)
					{
						list = new List<GameObject>(this.Objects);
					}
					if (this.Objects.Contains(gameObject))
					{
						aProc(gameObject);
					}
				}
			}
		}

		// Token: 0x0600A20A RID: 41482 RVA: 0x003B1564 File Offset: 0x003AF764
		public bool SafeForeachObject(Predicate<GameObject> pProc)
		{
			if (this.Objects.Count == 0)
			{
				return true;
			}
			List<GameObject> list = null;
			for (int i = 0; i < ((list == null) ? this.Objects.Count : list.Count); i++)
			{
				GameObject gameObject = ((list == null) ? this.Objects[i] : list[i]);
				if (!gameObject.HasTag("HiddenInInventory"))
				{
					if (list == null)
					{
						list = new List<GameObject>(this.Objects);
					}
					if (this.Objects.Contains(gameObject) && !pProc(gameObject))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600A20B RID: 41483 RVA: 0x003B15F4 File Offset: 0x003AF7F4
		public void SafeForeachObject(Action<GameObject> aProc, Predicate<GameObject> pFilter)
		{
			if (this.Objects.Count == 0)
			{
				return;
			}
			List<GameObject> list = null;
			for (int i = 0; i < ((list == null) ? this.Objects.Count : list.Count); i++)
			{
				GameObject gameObject = ((list == null) ? this.Objects[i] : list[i]);
				if (!gameObject.HasTag("HiddenInInventory") && pFilter(gameObject))
				{
					if (list == null)
					{
						list = new List<GameObject>(this.Objects);
					}
					if (this.Objects.Contains(gameObject))
					{
						aProc(gameObject);
					}
				}
			}
		}

		// Token: 0x0600A20C RID: 41484 RVA: 0x003B1688 File Offset: 0x003AF888
		public bool SafeForeachObject(Predicate<GameObject> pProc, Predicate<GameObject> pFilter)
		{
			if (this.Objects.Count == 0)
			{
				return true;
			}
			List<GameObject> list = null;
			for (int i = 0; i < ((list == null) ? this.Objects.Count : list.Count); i++)
			{
				GameObject gameObject = ((list == null) ? this.Objects[i] : list[i]);
				if (!gameObject.HasTag("HiddenInInventory") && pFilter(gameObject))
				{
					if (list == null)
					{
						list = new List<GameObject>(this.Objects);
					}
					if (this.Objects.Contains(gameObject) && !pProc(gameObject))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600A20D RID: 41485 RVA: 0x003B1720 File Offset: 0x003AF920
		public void ReverseForeachObject(Action<GameObject> Action)
		{
			if (this.Objects.Count == 0)
			{
				return;
			}
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = this.Objects[i];
				if (!gameObject.HasTag("HiddenInInventory"))
				{
					Action(gameObject);
				}
			}
		}

		// Token: 0x0600A20E RID: 41486 RVA: 0x003B1774 File Offset: 0x003AF974
		public void ForeachObjectWithRegisteredEvent(string EventName, Action<GameObject> aProc)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && this.Objects[i].HasRegisteredEvent(EventName))
					{
						aProc(this.Objects[i]);
					}
					i++;
				}
			}
		}

		// Token: 0x0600A20F RID: 41487 RVA: 0x003B17E0 File Offset: 0x003AF9E0
		public bool ForeachObjectWithRegisteredEvent(string EventName, Predicate<GameObject> pProc)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!this.Objects[i].HasTag("HiddenInInventory") && this.Objects[i].HasRegisteredEvent(EventName) && !pProc(this.Objects[i]))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600A210 RID: 41488 RVA: 0x003B1850 File Offset: 0x003AFA50
		public void ForeachObjectDirect(Action<GameObject> aProc)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					aProc(this.Objects[i]);
					i++;
				}
			}
		}

		// Token: 0x0600A211 RID: 41489 RVA: 0x003B1890 File Offset: 0x003AFA90
		public bool ForeachObjectDirect(Predicate<GameObject> pProc)
		{
			if (this.Objects != null)
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (!pProc(this.Objects[i]))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x0600A212 RID: 41490 RVA: 0x003B18D4 File Offset: 0x003AFAD4
		public static bool IsItemSlotAppropriate(GameObject Actor, GameObject Object, string SlotType)
		{
			if (Object.HasTagOrProperty("CannotEquip") || Object.HasTagOrProperty("NoEquip"))
			{
				return false;
			}
			QueryEquippableListEvent queryEquippableListEvent = QueryEquippableListEvent.FromPool(Actor, Object, SlotType, false, false);
			Object.HandleEvent(queryEquippableListEvent);
			return queryEquippableListEvent.List.Contains(Object);
		}

		// Token: 0x0600A213 RID: 41491 RVA: 0x003B191C File Offset: 0x003AFB1C
		private QueryEquippableListEvent GetEquipmentListEvent(List<GameObject> ObjectList, string SlotType, bool RequireDesirable = false, bool RequirePossible = false)
		{
			QueryEquippableListEvent queryEquippableListEvent = null;
			foreach (GameObject gameObject in ObjectList)
			{
				if (gameObject.WantEvent(QueryEquippableListEvent.ID, MinEvent.CascadeLevel) && !gameObject.HasTagOrProperty("CannotEquip") && !gameObject.HasTagOrProperty("NoEquip"))
				{
					if (queryEquippableListEvent == null)
					{
						queryEquippableListEvent = QueryEquippableListEvent.FromPool(this.ParentObject, gameObject, SlotType, RequireDesirable, RequirePossible);
					}
					queryEquippableListEvent.Item = gameObject;
					if (this.ParentObject.IsPlayer() || !gameObject.HasPropertyOrTag("NoAIEquip"))
					{
						gameObject.HandleEvent(queryEquippableListEvent);
					}
				}
			}
			return queryEquippableListEvent;
		}

		// Token: 0x0600A214 RID: 41492 RVA: 0x003B19D0 File Offset: 0x003AFBD0
		public void GetEquipmentListForSlot(List<GameObject> Return, string SlotType, List<GameObject> ObjectList, bool RequireDesirable = false, bool RequirePossible = false, bool SkipSort = false)
		{
			QueryEquippableListEvent equipmentListEvent = this.GetEquipmentListEvent(ObjectList, SlotType, RequireDesirable, RequirePossible);
			if (equipmentListEvent == null)
			{
				return;
			}
			Return.AddRange(equipmentListEvent.List);
			if (!SkipSort && Return.Count > 1)
			{
				Return.Sort(new Brain.GearSorter(this.ParentObject));
			}
		}

		// Token: 0x0600A215 RID: 41493 RVA: 0x003B1A18 File Offset: 0x003AFC18
		public List<GameObject> GetEquipmentListForSlot(string SlotType, List<GameObject> ObjectList, bool RequireDesirable = false, bool RequirePossible = false, bool SkipSort = false)
		{
			QueryEquippableListEvent equipmentListEvent = this.GetEquipmentListEvent(ObjectList, SlotType, RequireDesirable, RequirePossible);
			if (equipmentListEvent == null)
			{
				return null;
			}
			List<GameObject> list = Event.NewGameObjectList(equipmentListEvent.List);
			if (!SkipSort && list.Count > 1)
			{
				list.Sort(new Brain.GearSorter(this.ParentObject));
			}
			return list;
		}

		// Token: 0x0600A216 RID: 41494 RVA: 0x003B1A61 File Offset: 0x003AFC61
		public void GetEquipmentListForSlot(List<GameObject> Return, string SlotType, bool RequireDesirable = false, bool RequirePossible = false, bool SkipSort = false)
		{
			this.GetEquipmentListForSlot(Return, SlotType, this.GetObjectsReadonly(), RequireDesirable, RequirePossible, SkipSort);
		}

		// Token: 0x0600A217 RID: 41495 RVA: 0x003B1A76 File Offset: 0x003AFC76
		public List<GameObject> GetEquipmentListForSlot(string SlotType, bool RequireDesirable = false, bool RequirePossible = false, bool SkipSort = false)
		{
			return this.GetEquipmentListForSlot(SlotType, this.GetObjectsReadonly(), RequireDesirable, RequirePossible, SkipSort);
		}

		// Token: 0x0600A218 RID: 41496 RVA: 0x003B1A8C File Offset: 0x003AFC8C
		public void CheckEmptyState()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Render : null) != null)
			{
				string text = this.ParentObject.GetPropertyOrTag("EmptyTile", null);
				if (text != null && this.ParentObject.Render != null)
				{
					this.ParentObject.Render.Tile = text.CachedCommaExpansion().GetRandomElement<string>(null);
				}
				text = this.ParentObject.GetPropertyOrTag("EmptyDetailColor", null);
				if (text != null && this.ParentObject.Render != null)
				{
					this.ParentObject.Render.DetailColor = text.CachedCommaExpansion().GetRandomElement<string>(null);
				}
			}
		}

		// Token: 0x0600A219 RID: 41497 RVA: 0x003B1B2C File Offset: 0x003AFD2C
		public void CheckNonEmptyState()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Render : null) != null)
			{
				string text = this.ParentObject.GetPropertyOrTag("FullTile", null);
				if (text != null)
				{
					this.ParentObject.Render.Tile = text.CachedCommaExpansion().GetRandomElement<string>(null);
				}
				text = this.ParentObject.GetPropertyOrTag("FullDetailColor", null);
				if (text != null)
				{
					this.ParentObject.Render.DetailColor = text.CachedCommaExpansion().GetRandomElement<string>(null);
				}
			}
		}

		// Token: 0x0600A21A RID: 41498 RVA: 0x003B1BAF File Offset: 0x003AFDAF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A21B RID: 41499 RVA: 0x003B1BB4 File Offset: 0x003AFDB4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandEquipObject");
			Registrar.Register("CommandForceEquipObject");
			Registrar.Register("CommandForceUnequipObject");
			Registrar.Register("CommandGet");
			Registrar.Register("CommandGetFrom");
			Registrar.Register("CommandRemoveObject");
			Registrar.Register("CommandTakeObject");
			Registrar.Register("CommandUnequipObject");
			Registrar.Register("PerformDrop");
			Registrar.Register("PerformEquip");
			Registrar.Register("PerformTake");
			Registrar.Register("PerformUnequip");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A21C RID: 41500 RVA: 0x003B1C4D File Offset: 0x003AFE4D
		public bool CheckOverburdened()
		{
			if (this.ParentObject.IsOverburdened())
			{
				if (!this.ParentObject.HasEffect<Overburdened>())
				{
					this.ParentObject.ApplyEffect(new Overburdened(), null);
				}
				return true;
			}
			this.ParentObject.RemoveEffect<Overburdened>();
			return false;
		}

		// Token: 0x0600A21D RID: 41501 RVA: 0x003B1C8C File Offset: 0x003AFE8C
		public static List<GameObject> GetInteractableObjects(GameObject Actor, Cell CC, Cell C, bool DoInteractNearby)
		{
			List<GameObject> list;
			if (C != CC && C.IsSolidFor(Actor, false))
			{
				list = C.GetCanInteractInCellWithSolidObjectsFor(Actor);
			}
			else
			{
				list = C.GetObjectsInCell();
			}
			if (list.Count > 1)
			{
				list.Sort((GameObject a, GameObject b) => a.SortVs(b, null, true, true, true, true, null));
			}
			List<GameObject> list2 = Event.NewGameObjectList();
			int num = (Options.DebugInternals ? (-1) : 0);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject = list[i];
				if (gameObject != Actor && (gameObject.Render == null || (gameObject.Render.Visible && gameObject.Render.RenderLayer > num)) && (gameObject.IsTakeable() || (DoInteractNearby && EquipmentAPI.CanBeTwiddled(gameObject, Actor, false))))
				{
					list2.Add(gameObject);
				}
				i++;
			}
			return list2;
		}

		// Token: 0x0600A21E RID: 41502 RVA: 0x003B1D64 File Offset: 0x003AFF64
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandGet" || E.ID == "CommandGetFrom")
			{
				bool doInteractNearby = E.ID == "CommandGetFrom";
				bool flag = !doInteractNearby;
				if (flag && E.HasParameter("getOne"))
				{
					flag = false;
				}
				if (flag && Options.AskForOneItem)
				{
					flag = false;
				}
				bool flag2 = E.HasParameter("SmartUse");
				XRLCore.Core.RenderBaseToBuffer(Popup._ScreenBuffer);
				Popup._TextConsole.DrawBuffer(Popup._ScreenBuffer, null, false);
				GameObject Opener = this.ParentObject;
				Cell CC = Opener.CurrentCell;
				Cell C = E.GetParameter("TargetCell") as Cell;
				if (E.HasParameter("Direction"))
				{
					C = CC.GetCellFromDirection(E.GetStringParameter("Direction", null), true);
				}
				if (C == null)
				{
					if (doInteractNearby && this.ParentObject.IsPlayer())
					{
						string text = XRL.UI.PickDirection.ShowPicker(doInteractNearby ? "Interact" : "Get");
						if (text == null)
						{
							return true;
						}
						C = CC.GetCellFromDirection(text, true);
					}
					else
					{
						C = CC;
					}
					if (C == null)
					{
						return true;
					}
				}
				List<GameObject> interactableObjects = Inventory.GetInteractableObjects(Opener, CC, C, doInteractNearby);
				if (interactableObjects.Count == 1 && flag)
				{
					GameObject gameObject = interactableObjects[0];
					Event @event = Event.New("CommandTakeObject", 0, 0, 0);
					@event.SetParameter("Object", gameObject);
					@event.SetParameter("Context", E.GetStringParameter("Context", null));
					@event.SetSilent(false);
					if (Opener.FireEvent(@event))
					{
						C.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
				}
				else if (interactableObjects.Count > 0)
				{
					bool flag3 = false;
					PickItem.ShowPicker(interactableObjects, ref flag3, null, PickItem.PickItemDialogStyle.GetItemDialog, Opener, null, C, null, false, () => Inventory.GetInteractableObjects(Opener, CC, C, doInteractNearby), false, true, false);
					if (flag3)
					{
						E.RequestInterfaceExit();
					}
				}
				else if (Opener.IsPlayer())
				{
					if (doInteractNearby || flag2)
					{
						Popup.ShowFail("There's nothing you can interact with.", true, true, true);
					}
					else
					{
						Popup.ShowFail("There's nothing to take.", true, true, true);
					}
				}
			}
			else if (E.ID == "PerformEquip")
			{
				GameObject gameObject2 = E.GetGameObjectParameter("Object");
				BodyPart bodyPart = E.GetParameter("BodyPart") as BodyPart;
				bool flag4 = E.IsSilent();
				int intParameter = E.GetIntParameter("AutoEquipTry", 0);
				string text2 = E.GetStringParameter("FailureMessage", null);
				List<GameObject> parameter = E.GetParameter<List<GameObject>>("WasUnequipped");
				bool flag5 = E.HasFlag("DestroyOnUnequipDeclined");
				if (bodyPart == null)
				{
					return false;
				}
				if (gameObject2.Count > 1)
				{
					gameObject2 = gameObject2.RemoveOne();
				}
				GameObject equipped = bodyPart.Equipped;
				if (equipped != null)
				{
					Event event2 = Event.New("CommandUnequipObject", "BodyPart", bodyPart, "Sound", "");
					if (flag4)
					{
						event2.SetSilent(true);
					}
					if (intParameter > 0)
					{
						event2.SetParameter("AutoEquipTry", intParameter);
					}
					if (!text2.IsNullOrEmpty())
					{
						event2.SetParameter("FailureMessage", text2);
					}
					if (flag5)
					{
						event2.SetFlag("DestroyOnUnequipDeclined", true);
					}
					if (!this.ParentObject.FireEvent(event2))
					{
						string stringParameter = event2.GetStringParameter("FailureMessage", null);
						if (!stringParameter.IsNullOrEmpty() && stringParameter != text2)
						{
							text2 = stringParameter;
							E.SetParameter("FailureMessage", text2);
						}
						if (event2.HasFlag("DestroyOnUnequipDeclined"))
						{
							E.SetFlag("DestroyOnUnequipDeclined", true);
						}
						return false;
					}
					if (parameter != null && GameObject.Validate(ref equipped) && !parameter.Contains(equipped))
					{
						parameter.Add(equipped);
					}
				}
				string text3 = null;
				if (!bodyPart.DoEquip(gameObject2, ref text3, flag4, false, true, intParameter, parameter))
				{
					if (!text3.IsNullOrEmpty() && text2.IsNullOrEmpty())
					{
						text2 = text3;
						E.SetParameter("FailureMessage", text2);
					}
					return false;
				}
				if (!flag4)
				{
					base.DidXToY("equip", gameObject2, null, null, null, null, null, null, false, false, false, true, false, null, null, null, false, false, false, false, false, null);
					if (this.ParentObject.IsPlayer())
					{
						string text4;
						if (!E.TryGetStringParameter("Sound", out text4))
						{
							BodyPartType bodyPartType = bodyPart.VariantTypeModel();
							text4 = ((bodyPartType != null) ? bodyPartType.EquipSound : null);
							if (text4.IsNullOrEmpty())
							{
								text4 = gameObject2.GetTagOrStringProperty("EquipSound", null);
							}
							if (text4.IsNullOrEmpty())
							{
								text4 = gameObject2.GetTagOrStringProperty("ReloadSound", null);
							}
						}
						base.PlayWorldSound(text4, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						text4 = gameObject2.GetTagOrStringProperty("EquipLayerSound", null);
						if (!text4.IsNullOrEmpty())
						{
							foreach (ReadOnlySpan<char> readOnlySpan in text4.DelimitedBy(','))
							{
								if (readOnlySpan.Length == text4.Length)
								{
									base.PlayWorldSound(text4, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
									break;
								}
								base.PlayWorldSound(new string(readOnlySpan), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
							}
						}
					}
				}
				EquippedEvent.Send(this.ParentObject, gameObject2, bodyPart);
				EquipperEquippedEvent.Send(this.ParentObject, gameObject2, bodyPart);
			}
			else if (E.ID == "PerformUnequip")
			{
				BodyPart bodyPart2 = E.GetParameter("BodyPart") as BodyPart;
				if (bodyPart2 == null)
				{
					return false;
				}
				if (bodyPart2.Equipped == null)
				{
					return true;
				}
				GameObject equipped2 = bodyPart2.Equipped;
				bodyPart2.Unequip();
				if (equipped2 != null && !E.IsSilent() && this.ParentObject.IsPlayer())
				{
					string text5;
					if (!E.TryGetStringParameter("Sound", out text5))
					{
						BodyPartType bodyPartType2 = bodyPart2.VariantTypeModel();
						text5 = ((bodyPartType2 != null) ? bodyPartType2.UnequipSound : null);
						if (text5.IsNullOrEmpty())
						{
							text5 = equipped2.GetTagOrStringProperty("UnequipSound", null);
						}
					}
					base.PlayWorldSound(text5, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				this.ParentObject.FireEvent(Event.New("EquipperUnequipped", "Object", equipped2));
			}
			else if (E.ID == "PerformTake")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Container");
				bool flag6 = E.HasFlag("NoStack");
				string text6 = null;
				if (gameObjectParameter2 != null && !gameObjectParameter2.IsCreature && gameObjectParameter2.CurrentCell != null && gameObjectParameter2.CurrentCell != this.ParentObject.CurrentCell)
				{
					if (this.ParentObject.IsPlayer())
					{
						text6 = this.ParentObject.DescribeDirectionToward(gameObjectParameter2, false, false);
					}
					else
					{
						text6 = this.ParentObject.DescribeRelativeDirectionToward(gameObjectParameter2, false);
					}
				}
				else if (gameObjectParameter2 == null)
				{
					Cell currentCell = gameObjectParameter.GetCurrentCell();
					Cell currentCell2 = this.ParentObject.GetCurrentCell();
					if (currentCell != null && currentCell2 != null && currentCell != currentCell2)
					{
						text6 = Directions.GetIncomingDirectionDescription(this.ParentObject, currentCell2.GetDirectionFromCell(currentCell, false));
					}
				}
				if (!this.Objects.Contains(gameObjectParameter))
				{
					if (this.ParentObject.IsPlayer() && gameObjectParameter.InInventory != this.ParentObject && gameObjectParameter.Equipped != this.ParentObject && gameObjectParameter.IsOwned())
					{
						if (Popup.ShowYesNoCancel("That is not owned by you. Are you sure you want to take it?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						gameObjectParameter.Physics.BroadcastForHelp(this.ParentObject);
					}
					gameObjectParameter.RemoveFromContext(E);
					this.FlushWeightCache();
					if (!E.IsSilent())
					{
						GameObject gameObjectParameter3 = E.GetGameObjectParameter("PutBy");
						if (gameObjectParameter3 != null)
						{
							if (this.ParentObject.CurrentCell != null)
							{
								if (gameObjectParameter3.IsPlayer())
								{
									text6 = gameObjectParameter3.DescribeDirectionToward(this.ParentObject, false, false);
								}
								else
								{
									text6 = gameObjectParameter3.DescribeRelativeDirectionToward(this.ParentObject, false);
								}
							}
							Container part = this.ParentObject.GetPart<Container>();
							string text7 = ((part != null) ? part.Preposition : null) ?? "in";
							IComponent<GameObject>.WDidXToYWithZ(gameObjectParameter3, "put", gameObjectParameter, text7, this.ParentObject, text6, null, null, null, null, null, false, false, false, false, true, true, false, false, null, null, null, null, false, false, false, false, false, null);
						}
						else if (gameObjectParameter2 != null)
						{
							base.DidXToYWithZ("take", gameObjectParameter, "from", gameObjectParameter2, text6, null, null, null, null, null, false, false, false, false, false, false, false, false, null, null, null, null, false, false, false, false, false, null);
						}
						else if (text6 != null)
						{
							base.DidXToY("take", gameObjectParameter, text6, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
						else
						{
							base.DidXToY("take", gameObjectParameter, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
					}
					if (gameObjectParameter != null)
					{
						string blueprint = gameObjectParameter.Blueprint;
					}
					this.AddObject(gameObjectParameter, null, false, true, true, E);
					string stringParameter2 = E.GetStringParameter("Context", null);
					TakenEvent.Send(gameObjectParameter, this.ParentObject, stringParameter2);
					TookEvent.Send(gameObjectParameter, this.ParentObject, stringParameter2);
					if (!flag6 && gameObjectParameter.IsValid())
					{
						this.CheckStacks();
					}
				}
			}
			else if (E.ID == "PerformDrop")
			{
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Object");
				if (this.Objects.Contains(gameObjectParameter4))
				{
					this.RemoveObject(gameObjectParameter4, null);
					this.FlushWeightCache();
					if (!E.IsSilent())
					{
						base.DidXToY("drop", gameObjectParameter4, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
					DroppedEvent.Send(this.ParentObject, gameObjectParameter4, E.HasFlag("Forced"));
				}
			}
			else if (E.ID == "CommandRemoveObject")
			{
				GameObject gameObjectParameter5 = E.GetGameObjectParameter("Object");
				if (gameObjectParameter5 != null)
				{
					if (this.ParentObject.HasRegisteredEvent("BeginDrop") && !this.ParentObject.FireEvent(Event.New("BeginDrop", "Object", gameObjectParameter5, "ForEquip", E.GetIntParameter("ForEquip", 0))))
					{
						return false;
					}
					if (!gameObjectParameter5.HasRegisteredEvent("BeginBeingDropped") || gameObjectParameter5.FireEvent(Event.New("BeginBeingDropped", "TakingObject", this.ParentObject)))
					{
						Event event3 = Event.New("PerformDrop", 0, 0, 0);
						event3.SetParameter("Object", gameObjectParameter5);
						if (E.HasFlag("Forced"))
						{
							event3.SetFlag("Forced", true);
						}
						if (E.IsSilent())
						{
							event3.SetSilent(true);
						}
						return this.ParentObject.FireEvent(event3);
					}
					return false;
				}
			}
			else if (E.ID == "CommandEquipObject" || E.ID == "CommandForceEquipObject")
			{
				GameObject gameObject3 = E.GetGameObjectParameter("Object");
				Physics physics = gameObject3.Physics;
				if (physics == null || !physics.Takeable)
				{
					MetricsManager.LogError(string.Concat(new string[]
					{
						"Attempting to equip untakeable object '",
						gameObject3.DebugName,
						"' to '",
						this.ParentObject.DebugName,
						"'."
					}));
					return false;
				}
				if (gameObject3.IsInGraveyard())
				{
					MetricsManager.LogError(string.Concat(new string[]
					{
						"Attempting to equip graveyard object '",
						gameObject3.DebugName,
						"' to '",
						this.ParentObject.DebugName,
						"'."
					}));
					return false;
				}
				if (gameObject3.IsInvalid())
				{
					MetricsManager.LogError(string.Concat(new string[]
					{
						"Attempting to equip invalid object '",
						gameObject3.DebugName,
						"' to '",
						this.ParentObject.DebugName,
						"'."
					}));
					return false;
				}
				BodyPart bodyPart3 = E.GetParameter("BodyPart") as BodyPart;
				bool flag7 = E.ID == "CommandForceEquipObject";
				bool flag8 = E.HasFlag("SemiForced");
				bool flag9 = E.IsSilent();
				int intParameter2 = E.GetIntParameter("AutoEquipTry", 0);
				string text8 = E.GetStringParameter("FailureMessage", null);
				List<GameObject> list = (E.GetParameter("WasUnequipped") as List<GameObject>) ?? Event.NewGameObjectList();
				bool flag10 = E.HasFlag("DestroyOnUnequipDeclined");
				if (!flag7 && !flag8)
				{
					if (this.ParentObject.HasEffect<Stuck>())
					{
						if (!flag9 && this.ParentObject.IsPlayer())
						{
							text8 = "You cannot equip items while stuck!";
							if (intParameter2 > 0)
							{
								E.SetParameter("FailureMessage", text8);
							}
							else
							{
								Popup.ShowFail(text8, true, true, true);
							}
						}
						return false;
					}
					if (!this.ParentObject.CanMoveExtremities(null, !flag9, false, true))
					{
						return false;
					}
				}
				Cell currentCell3 = gameObject3.CurrentCell;
				GameObject inInventory = gameObject3.InInventory;
				int num = 0;
				bool flag11 = false;
				try
				{
					if (!flag7 && inInventory != this.ParentObject && gameObject3.Equipped != this.ParentObject)
					{
						gameObject3.SplitFromStack();
						if (!this.ParentObject.ReceiveObject(gameObject3, true, null))
						{
							gameObject3.CheckStack();
							return false;
						}
					}
					GameObject gameObject4 = ((gameObject3 != null) ? gameObject3.InInventory : null);
					if (gameObject4 != null && gameObject4 != this.ParentObject && !gameObject4.FireEvent(Event.New("BeforeContentsTaken", "Taker", this.ParentObject)))
					{
						return false;
					}
					if (this.ParentObject.IsPlayer() && gameObject3.InInventory != this.ParentObject && gameObject3.Equipped != this.ParentObject && (gameObject3.CurrentCell != null || gameObject3.InInventory != null) && !gameObject3.Owner.IsNullOrEmpty())
					{
						if (E.HasFlag("OwnershipViolationDeclined"))
						{
							return false;
						}
						if (!E.HasFlag("OwnershipViolationConfirmed"))
						{
							E.SetFlag("OwnershipViolationChecked", true);
							if (Popup.ShowYesNoCancel("That is not owned by you. Are you sure you want to take it?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
							{
								E.SetFlag("OwnershipViolationDeclined", true);
								return false;
							}
							E.SetFlag("OwnershipViolationConfirmed", true);
							gameObject3.Physics.BroadcastForHelp(this.ParentObject);
						}
					}
					if (gameObject4 != null && gameObject4 != this.ParentObject && !gameObject4.FireEvent(Event.New("AfterContentsTaken", "Taker", this.ParentObject)))
					{
						return false;
					}
					List<BodyPart> @for = QuerySlotListEvent.GetFor(this.ParentObject, gameObject3, ref text8);
					if (!flag7 && !flag8)
					{
						if (@for.Count == 0)
						{
							if (!flag9 && this.ParentObject.IsPlayer())
							{
								if (text8.IsNullOrEmpty())
								{
									text8 = "You cannot equip " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".";
								}
								if (intParameter2 > 0)
								{
									E.SetParameter("FailureMessage", text8);
								}
								else
								{
									Popup.ShowFail(text8, true, true, true);
								}
							}
							return false;
						}
						if (bodyPart3 != null && !@for.Contains(bodyPart3))
						{
							if (!flag9 && this.ParentObject.IsPlayer())
							{
								if (text8.IsNullOrEmpty())
								{
									text8 = string.Concat(new string[]
									{
										"You cannot equip ",
										gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
										" on your ",
										bodyPart3.GetOrdinalName(),
										"."
									});
								}
								if (intParameter2 > 0)
								{
									E.SetParameter("FailureMessage", text8);
								}
								else
								{
									Popup.ShowFail(text8, true, true, true);
								}
							}
							return false;
						}
					}
					if (bodyPart3 == null && this.ParentObject.IsPlayer())
					{
						List<string> list2 = new List<string>(@for.Count);
						List<char> list3 = new List<char>(@for.Count);
						char c = 'a';
						foreach (BodyPart bodyPart4 in @for)
						{
							list2.Add(bodyPart4.ToString());
							list3.Add(c);
							c += '\u0001';
						}
						int num2 = 0;
						if (gameObject3.HasTag("MeleeWeapon"))
						{
							if (@for.Any<BodyPart>((BodyPart p) => p.Primary))
							{
								num2 = @for.IndexOf(@for.First<BodyPart>((BodyPart p) => p.Primary));
							}
						}
						else if (@for.Any<BodyPart>((BodyPart p) => p.Type != "Hand"))
						{
							num2 = @for.IndexOf(@for.First<BodyPart>((BodyPart p) => p.Type != "Hand"));
						}
						int num3 = Popup.PickOption("", null, "", "Sounds/UI/ui_notification", list2.ToArray(), list3.ToArray(), null, null, null, null, null, 0, 60, num2, -1, true, false, false, true, false, null, null);
						if (num3 == -1)
						{
							return false;
						}
						bodyPart3 = @for[num3];
					}
					int num4 = 1000;
					if (bodyPart3 != null && bodyPart3.Type == "Thrown Weapon")
					{
						num4 = 0;
					}
					else if (this.ParentObject.CurrentCell == null)
					{
						num4 = 0;
					}
					num = E.GetIntParameter("EnergyCost", num4);
					if (bodyPart3 == null || gameObject3 == null)
					{
						return false;
					}
					if (gameObject3.Count > 1)
					{
						gameObject3 = gameObject3.RemoveOne();
					}
					if (this.ParentObject.HasRegisteredEvent("BeginEquip"))
					{
						Event event4 = Event.New("BeginEquip", "Object", gameObject3, "BodyPart", bodyPart3);
						if (flag9)
						{
							event4.SetSilent(true);
						}
						if (intParameter2 > 0)
						{
							event4.SetParameter("AutoEquipTry", intParameter2);
						}
						if (!text8.IsNullOrEmpty())
						{
							event4.SetParameter("FailureMessage", text8);
						}
						if (list != null)
						{
							event4.SetParameter("WasUnequipped", list);
						}
						if (flag10)
						{
							event4.SetFlag("DestroyOnUnequipDeclined", true);
						}
						bool flag12 = this.ParentObject.FireEvent(event4);
						string stringParameter3 = event4.GetStringParameter("FailureMessage", null);
						if (!stringParameter3.IsNullOrEmpty() && stringParameter3 != text8)
						{
							text8 = stringParameter3;
							E.SetParameter("FailureMessage", text8);
						}
						if (event4.HasFlag("DestroyOnUnequipDeclined"))
						{
							flag10 = true;
							E.SetFlag("DestroyOnUnequipDeclined", true);
						}
						if (!flag12 && !flag7)
						{
							return false;
						}
					}
					if (gameObject3.HasRegisteredEvent("BeginBeingEquipped"))
					{
						Event event5 = Event.New("BeginBeingEquipped", "EquippingObject", this.ParentObject, "Equipper", this.ParentObject, "BodyPart", bodyPart3);
						if (flag9)
						{
							event5.SetSilent(true);
						}
						if (intParameter2 > 0)
						{
							event5.SetParameter("AutoEquipTry", intParameter2);
						}
						if (!text8.IsNullOrEmpty())
						{
							event5.SetParameter("FailureMessage", text8);
						}
						if (list != null)
						{
							event5.SetParameter("WasUnequipped", list);
						}
						if (flag10)
						{
							event5.SetFlag("DestroyOnUnequipDeclined", flag10);
						}
						bool flag13 = gameObject3.FireEvent(event5);
						string stringParameter4 = event5.GetStringParameter("FailureMessage", null);
						if (!stringParameter4.IsNullOrEmpty() && stringParameter4 != text8)
						{
							text8 = stringParameter4;
							E.SetParameter("FailureMessage", text8);
						}
						if (event5.HasFlag("DestroyOnUnequipDeclined"))
						{
							flag10 = true;
							E.SetFlag("DestroyOnUnequipDeclined", true);
						}
						if (!flag13 && !flag7)
						{
							return false;
						}
					}
					Inventory.eCommandRemoveObject.SetParameter("Object", gameObject3);
					Inventory.eCommandRemoveObject.SetFlag("ForEquip", true);
					Inventory.eCommandRemoveObject.SetSilent(true);
					if (!this.ParentObject.FireEvent(Inventory.eCommandRemoveObject) && !flag7)
					{
						return false;
					}
					if (gameObject3.CurrentCell != null && !gameObject3.CurrentCell.RemoveObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null))
					{
						return false;
					}
					Event event6 = Event.New("PerformEquip", "Object", gameObject3);
					event6.SetParameter("BodyPart", bodyPart3);
					string text9;
					if (E.TryGetStringParameter("Sound", out text9))
					{
						event6.SetParameter("Sound", text9);
					}
					if (flag9)
					{
						event6.SetSilent(true);
					}
					if (intParameter2 > 0)
					{
						event6.SetParameter("AutoEquipTry", intParameter2);
					}
					if (!text8.IsNullOrEmpty())
					{
						event6.SetParameter("FailureMessage", text8);
					}
					if (list != null)
					{
						event6.SetParameter("WasUnequipped", list);
					}
					if (flag10)
					{
						event6.SetFlag("DestroyOnUnequipDeclined", true);
					}
					bool flag14 = this.ParentObject.FireEvent(event6);
					string stringParameter5 = event6.GetStringParameter("FailureMessage", null);
					if (!stringParameter5.IsNullOrEmpty() && stringParameter5 != text8)
					{
						text8 = stringParameter5;
						E.SetParameter("FailureMessage", text8);
					}
					if (event6.HasFlag("DestroyOnUnequipDeclined"))
					{
						flag10 = true;
						E.SetFlag("DestroyOnUnequipDeclined", true);
					}
					if (!flag14 && !flag7)
					{
						Event event7 = Event.New("CommandTakeObject", 0, 0, 0);
						event7.SetParameter("Object", gameObject3);
						event7.SetParameter("Context", E.GetStringParameter("Context", null));
						event7.SetSilent(true);
						if (!this.ParentObject.FireEvent(event7))
						{
							if (this.ParentObject.CurrentCell != null)
							{
								this.ParentObject.CurrentCell.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, this.ParentObject, null, null, null, null);
							}
							else if (this.ParentObject.IsPlayer())
							{
								Popup.ShowFail("Error dropping object, removing to graveyard zone! (Inventory.cs:CommandEquipObject)", true, true, true);
							}
							else
							{
								IComponent<GameObject>.AddPlayerMessage(this.ParentObject.DisplayName + "] Error dropping object, removing to graveyard zone! (Inventory.cs:CommandEquipObject)", null, true);
							}
						}
						if (!flag9 && intParameter2 <= 0 && this.ParentObject.IsPlayer())
						{
							string text10 = null;
							if (!text8.IsNullOrEmpty())
							{
								text10 = text8;
							}
							if (list != null && list.Count > 0)
							{
								string text11 = this.ParentObject.DescribeUnequip(list);
								if (!text11.IsNullOrEmpty())
								{
									if (text10 == null)
									{
										text10 = "";
									}
									if (text10 != null)
									{
										text10 += "\n\n";
									}
									text10 += text11;
								}
							}
							if (!text10.IsNullOrEmpty())
							{
								Popup.ShowFail(text10, true, true, true);
							}
						}
						return false;
					}
					flag11 = true;
				}
				finally
				{
					if (!flag11)
					{
						if (currentCell3 != null)
						{
							if (GameObject.Validate(ref gameObject3) && gameObject3.CurrentCell != currentCell3)
							{
								gameObject3.RemoveFromContext(E);
								currentCell3.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, this.ParentObject, null, null, null, null);
							}
						}
						else if (((inInventory != null) ? inInventory.Inventory : null) != null && GameObject.Validate(ref gameObject3) && gameObject3.InInventory != inInventory)
						{
							gameObject3.RemoveFromContext(E);
							inInventory.Inventory.AddObject(gameObject3, null, false, false, true, null);
						}
					}
				}
				if (flag11 && num > 0)
				{
					this.ParentObject.UseEnergy(num, "Equip Item", null, null, false);
				}
			}
			else if (E.ID == "CommandUnequipObject" || E.ID == "CommandForceUnequipObject")
			{
				bool flag15 = E.ID.Contains("Force") || E.HasFlag("Forced");
				bool flag16 = E.HasFlag("SemiForced");
				bool flag17 = E.HasFlag("NoStack");
				bool flag18 = E.IsSilent();
				int intParameter3 = E.GetIntParameter("AutoEquipTry", 0);
				string text12 = E.GetStringParameter("FailureMessage", null);
				bool flag19 = E.HasFlag("DestroyOnUnequipDeclined");
				BodyPart bodyPart5 = E.GetParameter("BodyPart") as BodyPart;
				if (bodyPart5 == null)
				{
					GameObject gameObject5 = E.GetGameObjectParameter("Target") ?? E.GetGameObjectParameter("Object");
					bodyPart5 = ((gameObject5 != null) ? gameObject5.EquippedOn() : null);
					if (bodyPart5 == null)
					{
						return false;
					}
				}
				GameObject equipped3 = bodyPart5.Equipped;
				if (equipped3 == null)
				{
					return false;
				}
				if (!flag15 && !flag16)
				{
					if (this.ParentObject.HasEffect<Stuck>())
					{
						text12 = "You cannot remove items while stuck!";
						E.SetParameter("FailureMessage", text12);
						if (!flag18 && intParameter3 <= 0 && this.ParentObject.IsPlayer())
						{
							Popup.ShowFail(text12, true, true, true);
						}
						return false;
					}
					if (!this.ParentObject.CheckFrozen(false, true, true, null))
					{
						text12 = "You are frozen solid!";
						E.SetParameter("FailureMessage", text12);
						if (!flag18 && intParameter3 <= 0 && this.ParentObject.IsPlayer())
						{
							Popup.ShowFail(text12, true, true, true);
						}
						return false;
					}
				}
				Event event8 = Event.New("BeginUnequip", "BodyPart", bodyPart5);
				event8.SetFlag("Forced", flag15);
				event8.SetFlag("SemiForced", flag16);
				if (flag17)
				{
					event8.SetFlag("NoStack", true);
				}
				if (flag18)
				{
					event8.SetSilent(true);
				}
				if (intParameter3 > 0)
				{
					event8.SetParameter("AutoEquipTry", intParameter3);
				}
				if (!text12.IsNullOrEmpty())
				{
					event8.SetParameter("FailureMessage", text12);
				}
				if (flag19)
				{
					event8.SetFlag("DestroyOnUnequipDeclined", flag19);
				}
				if (!this.ParentObject.FireEvent(event8))
				{
					string stringParameter6 = event8.GetStringParameter("FailureMessage", null);
					if (!stringParameter6.IsNullOrEmpty() && stringParameter6 != text12)
					{
						text12 = stringParameter6;
						E.SetParameter("FailureMessage", stringParameter6);
					}
					if (event8.HasFlag("DestroyOnUnequipDeclined"))
					{
						flag19 = true;
						E.SetFlag("DestroyOnUnequipDeclined", true);
					}
					if (!flag18 && intParameter3 <= 0 && this.ParentObject.IsPlayer())
					{
						Popup.ShowFail(text12, true, true, true);
					}
					return false;
				}
				bool flag20 = equipped3.BeginBeingUnequipped(ref text12, ref flag19, this.ParentObject, this.ParentObject, bodyPart5, flag18, flag15, flag16, intParameter3);
				E.SetParameter("FailureMessage", text12);
				E.SetFlag("DestroyOnUnequipDeclined", flag19);
				if (!flag20)
				{
					if (!flag18 && intParameter3 <= 0 && this.ParentObject.IsPlayer())
					{
						Popup.ShowFail(text12, true, true, true);
					}
					return false;
				}
				Event event9 = Event.New("PerformUnequip", 0, 0, 0);
				event9.SetParameter("BodyPart", bodyPart5);
				event9.SetFlag("Forced", flag15);
				event9.SetFlag("SemiForced", flag16);
				string text13;
				if (E.TryGetStringParameter("Sound", out text13))
				{
					event9.SetParameter("Sound", text13);
				}
				if (flag17)
				{
					event9.SetFlag("NoStack", true);
				}
				if (flag18)
				{
					event9.SetSilent(true);
				}
				if (intParameter3 > 0)
				{
					event9.SetParameter("AutoEquipTry", intParameter3);
				}
				if (!text12.IsNullOrEmpty())
				{
					event9.SetParameter("FailureMessage", text12);
				}
				if (flag19)
				{
					event9.SetFlag("DestroyOnUnequipDeclined", true);
				}
				if (!this.ParentObject.FireEvent(event9))
				{
					string stringParameter7 = event9.GetStringParameter("FailureMessage", null);
					if (!stringParameter7.IsNullOrEmpty() && stringParameter7 != text12)
					{
						text12 = stringParameter7;
						E.SetParameter("FailureMessage", stringParameter7);
					}
					if (event9.HasFlag("DestroyOnUnequipDeclined"))
					{
						flag19 = true;
						E.SetFlag("DestroyOnUnequipDeclined", true);
					}
					if (!flag18 && intParameter3 <= 0 && this.ParentObject.IsPlayer())
					{
						Popup.ShowFail(text12, true, true, true);
					}
					return false;
				}
				if (GameObject.Validate(ref equipped3))
				{
					if (!flag18 && this.ParentObject.IsCombat)
					{
						base.DidXToY("unequip", equipped3, null, null, null, null, null, null, false, false, false, false, false, null, this.ParentObject, null, false, false, false, false, false, null);
					}
					equipped3.Physics.Equipped = null;
					if (GameObject.Validate(ref equipped3) && !E.HasParameter("NoTake"))
					{
						Inventory.eCommandFreeTakeObject.SetParameter("Object", equipped3);
						Inventory.eCommandFreeTakeObject.SetFlag("NoStack", flag17);
						Inventory.eCommandFreeTakeObject.SetSilent(true);
						if (equipped3 != null && equipped3.HasTag("DestroyWhenUnequipped"))
						{
							equipped3.Destroy(null, false, false, null);
						}
						else
						{
							if (this.ParentObject.FireEvent(Inventory.eCommandFreeTakeObject))
							{
								return true;
							}
							if (this.ParentObject.CurrentCell != null)
							{
								this.ParentObject.CurrentCell.AddObject(equipped3, false, false, false, false, false, true, true, null, null, null, this.ParentObject, null, null, null, null);
							}
							else if (this.ParentObject.IsPlayer())
							{
								Popup.ShowFail("Error dropping object, removing to graveyard zone! (Inventory.cs:CommandEquipObject)", true, true, true);
							}
						}
					}
				}
			}
			else if (E.ID == "CommandTakeObject")
			{
				GameObject gameObjectParameter6 = E.GetGameObjectParameter("Object");
				if (gameObjectParameter6 == null)
				{
					return false;
				}
				int intParameter4 = E.GetIntParameter("EnergyCost", 1000);
				if (gameObjectParameter6.MovingIntoWouldCreateContainmentLoop(this.ParentObject))
				{
					MetricsManager.LogError(this.ParentObject.DebugName + " taking " + gameObjectParameter6.DebugName + " would create containment loop");
					return false;
				}
				if (intParameter4 > 0 && !this.ParentObject.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (gameObjectParameter6.IsInStasis())
				{
					return this.ParentObject.Fail("You cannot budge " + gameObjectParameter6.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				bool flag21 = E.HasFlag("NoStack");
				GameObject inInventory2 = gameObjectParameter6.InInventory;
				if (inInventory2 != null)
				{
					if (!inInventory2.FireEvent(Event.New("BeforeContentsTaken", "Taker", this.ParentObject)))
					{
						return false;
					}
					if (inInventory2.IsOwned() && this.ParentObject.IsPlayer() && inInventory2.HasTagOrProperty("DontWarnOnOpen") && gameObjectParameter6.GetIntProperty("StoredByPlayer", 0) <= 0 && gameObjectParameter6.GetIntProperty("FromStoredByPlayer", 0) <= 0 && Popup.ShowYesNo(string.Concat(new string[]
					{
						"You don't own ",
						inInventory2.t(2147483647, null, null, false, false, false, false, false, true, true, false, inInventory2.indicativeProximal, false, true, false, null, true, null, false),
						". Are you sure you want to take ",
						gameObjectParameter6.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						"?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
					if (!inInventory2.FireEvent(Event.New("AfterContentsTaken", "Taker", this.ParentObject)))
					{
						return false;
					}
					AfterContentsTakenEvent.Send(this.ParentObject, inInventory2, gameObjectParameter6);
				}
				GameObject gameObjectParameter7 = E.GetGameObjectParameter("PutBy");
				string stringParameter8 = E.GetStringParameter("Context", null);
				Event event10 = Event.New("BeginTake", 0, 0, 0);
				event10.SetParameter("Object", gameObjectParameter6);
				event10.SetParameter("PutBy", gameObjectParameter7);
				event10.SetParameter("Container", inInventory2);
				event10.SetParameter("Context", stringParameter8);
				event10.SetSilent(E.IsSilent());
				if (flag21)
				{
					event10.SetFlag("NoStack", true);
				}
				if (!this.ParentObject.FireEvent(event10, E))
				{
					return false;
				}
				Event event11 = Event.New("BeginBeingTaken", 0, 0, 0);
				event11.SetParameter("TakingObject", this.ParentObject);
				event11.SetParameter("PutBy", gameObjectParameter7);
				event11.SetParameter("Container", inInventory2);
				event11.SetParameter("Context", stringParameter8);
				event11.SetSilent(E.IsSilent());
				if (flag21)
				{
					event11.SetFlag("NoStack", true);
				}
				if (!gameObjectParameter6.FireEvent(event11, E))
				{
					return false;
				}
				Event event12 = Event.New("PerformTake", 0, 0, 0);
				event12.SetParameter("Object", gameObjectParameter6);
				event12.SetParameter("PutBy", gameObjectParameter7);
				event12.SetParameter("Container", inInventory2);
				event12.SetParameter("Context", stringParameter8);
				event12.SetSilent(E.IsSilent());
				if (flag21)
				{
					event12.SetFlag("NoStack", true);
				}
				if (!this.ParentObject.FireEvent(event12, E))
				{
					return false;
				}
				if (intParameter4 > 0)
				{
					this.ParentObject.UseEnergy(intParameter4, "Take", stringParameter8, null, false);
				}
			}
			IL_20DC:
			return base.FireEvent(E);
		}

		// Token: 0x0600A21F RID: 41503 RVA: 0x003B3E8C File Offset: 0x003B208C
		public override void TurnTick(long TimeTick, int Amount)
		{
			int i = 0;
			int num = this.Objects.Count;
			while (i < num)
			{
				GameObject gameObject = this.Objects[i];
				gameObject.TurnTick(TimeTick, Amount);
				if (num != this.Objects.Count)
				{
					num = this.Objects.Count;
					if (i < num && this.Objects[i] != gameObject)
					{
						i--;
					}
				}
				i++;
			}
			this.CheckOverburdened();
		}

		// Token: 0x0600A220 RID: 41504 RVA: 0x003B3F00 File Offset: 0x003B2100
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (GameObject gameObject in this.Objects)
			{
				stringBuilder.Append(gameObject.DisplayName);
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600A221 RID: 41505 RVA: 0x003B3F74 File Offset: 0x003B2174
		public override bool WantEvent(int ID, int Cascade)
		{
			if (base.WantEvent(ID, Cascade))
			{
				return true;
			}
			if (ID == AddedToInventoryEvent.ID)
			{
				return true;
			}
			if (ID == SingletonEvent<EndTurnEvent>.ID)
			{
				return true;
			}
			if (ID == GetCarriedWeightEvent.ID && this.Objects.Count > 0)
			{
				return true;
			}
			if (ID == GetExtrinsicWeightEvent.ID && this.Objects.Count > 0)
			{
				return true;
			}
			if (ID == GetExtrinsicValueEvent.ID && this.Objects.Count > 0)
			{
				return true;
			}
			if (ID == AfterObjectCreatedEvent.ID)
			{
				return true;
			}
			if (ID == BeforeDeathRemovalEvent.ID)
			{
				return true;
			}
			if (ID == PooledEvent<StripContentsEvent>.ID)
			{
				return true;
			}
			if (ID == PooledEvent<GetContentsEvent>.ID)
			{
				return true;
			}
			if (ID == PooledEvent<StatChangeEvent>.ID)
			{
				return true;
			}
			if (ID == PooledEvent<CarryingCapacityChangedEvent>.ID)
			{
				return true;
			}
			if (ID == InventoryActionEvent.ID)
			{
				return true;
			}
			if (ID == PooledEvent<HasBlueprintEvent>.ID)
			{
				return true;
			}
			if (ID == ZoneThawedEvent.ID)
			{
				return true;
			}
			if (ID == SingletonEvent<AfterGameLoadedEvent>.ID)
			{
				return true;
			}
			if (ID == SingletonEvent<FlushWeightCacheEvent>.ID && this.WeightCache != -1)
			{
				return true;
			}
			if (!MinEvent.CascadeTo(Cascade, 128))
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null && parentObject.OnWorldMap())
				{
					return false;
				}
			}
			if (MinEvent.CascadeTo(Cascade, 2))
			{
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (this.Objects[i].WantEvent(ID, Cascade))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600A222 RID: 41506 RVA: 0x003B40BC File Offset: 0x003B22BC
		public override bool HandleEvent(MinEvent E)
		{
			if (!base.HandleEvent(E))
			{
				return false;
			}
			if (E.CascadeTo(2))
			{
				int num = -1;
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (this.Objects[i].WantEvent(E.ID, E.GetCascadeLevel()))
					{
						num = i;
						break;
					}
					i++;
				}
				if (num != -1)
				{
					List<GameObject> list = Event.NewGameObjectList();
					list.AddRange(this.Objects);
					int j = num;
					int count2 = list.Count;
					while (j < count2)
					{
						if (!E.Dispatch(list[j]))
						{
							return false;
						}
						j++;
					}
				}
			}
			return true;
		}

		// Token: 0x0600A223 RID: 41507 RVA: 0x003B415C File Offset: 0x003B235C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "CommandDropObject" || E.Command == "CommandDropAllObject")
			{
				bool flag = E.Command == "CommandDropAllObject";
				if (E.Actor == null || E.Item == null)
				{
					return false;
				}
				Stacker stacker;
				if (!E.Forced && !flag && E.Actor.IsPlayer() && E.Item.TryGetPart<Stacker>(out stacker) && stacker != null && stacker.Number > 1)
				{
					int? num = Popup.AskNumber("How many do you want to drop?", "Sounds/UI/ui_notification", "", stacker.Number, 0, stacker.Number);
					if (num != null)
					{
						int? num2 = num;
						int num3 = 0;
						if (!((num2.GetValueOrDefault() == num3) & (num2 != null)))
						{
							int num4 = stacker.Number;
							try
							{
								num4 = num.Value;
							}
							catch
							{
								num4 = stacker.Number;
							}
							if (num4 <= 0)
							{
								goto IL_032E;
							}
							if (num4 >= stacker.Number)
							{
								num4 = stacker.Number;
								goto IL_0128;
							}
							E.Item.SplitStack(num4, this.ParentObject, false);
							goto IL_0128;
						}
					}
					return false;
				}
				IL_0128:
				if (E.Actor.HasRegisteredEvent("BeginDrop") && !E.Actor.FireEvent(Event.New("BeginDrop", "Object", E.Item)) && !E.Forced)
				{
					return false;
				}
				if (E.Item.HasRegisteredEvent("BeginBeingDropped") && !E.Item.FireEvent(Event.New("BeginBeingDropped", "TakingObject", E.Actor)) && !E.Forced)
				{
					return false;
				}
				Event @event = Event.New("PerformDrop", 0, 0, 0);
				@event.SetParameter("Object", E.Item);
				if (E.Forced)
				{
					@event.SetFlag("Forced", true);
				}
				if (E.Silent)
				{
					@event.SetSilent(true);
				}
				if (!E.Actor.FireEvent(@event) && !E.Forced)
				{
					return false;
				}
				IInventory inventory;
				if ((inventory = E.InventoryTarget) == null)
				{
					inventory = E.CellTarget ?? this.ParentObject.GetCurrentCell();
				}
				IInventory inventory2 = inventory;
				if (inventory2 == null)
				{
					return false;
				}
				if (this.ParentObject.IsPlayerControlled())
				{
					if (E.Forced)
					{
						E.Item.RemoveIntProperty("DroppedByPlayer");
						E.Item.RequirePart<SpecialItem>(false).RemovePlayerTake = true;
					}
					else
					{
						E.Item.SetIntProperty("DroppedByPlayer", 1, false);
					}
				}
				inventory2.AddObjectToInventory(E.Item, this.ParentObject, E.Silent, false, true, null, E);
			}
			else if (E.Command == "EmptyForDisassemble")
			{
				List<GameObject> list = Event.NewGameObjectList();
				list.AddRange(this.Objects);
				GameObject gameObject;
				Cell cell;
				this.ParentObject.GetContext(out gameObject, out cell);
				if (gameObject != null)
				{
					gameObject.ReceiveObject(list, false, null);
				}
				if (cell != null)
				{
					foreach (GameObject gameObject2 in list)
					{
						gameObject2.RemoveFromContext(E);
						cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			IL_032E:
			return base.HandleEvent(E);
		}

		// Token: 0x0600A224 RID: 41508 RVA: 0x003B44BC File Offset: 0x003B26BC
		public override bool HandleEvent(EndTurnEvent E)
		{
			int i = 0;
			int num = this.Objects.Count;
			while (i < num)
			{
				GameObject gameObject = this.Objects[i];
				gameObject.HandleEvent(E);
				if (num != this.Objects.Count)
				{
					num = this.Objects.Count;
					if (i < num && this.Objects[i] != gameObject)
					{
						i--;
					}
				}
				i++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A225 RID: 41509 RVA: 0x003B4530 File Offset: 0x003B2730
		public override bool HandleEvent(HasBlueprintEvent E)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == E.Blueprint)
				{
					return false;
				}
				i++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A226 RID: 41510 RVA: 0x003B457C File Offset: 0x003B277C
		public override bool HandleEvent(GetExtrinsicValueEvent E)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				E.Value += this.Objects[i].Value;
				i++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A227 RID: 41511 RVA: 0x003B45C8 File Offset: 0x003B27C8
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (!this.Objects[i].HasTag("HiddenInInventory"))
				{
					E.Weight += this.Objects[i].GetWeight();
				}
				i++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A228 RID: 41512 RVA: 0x003B462A File Offset: 0x003B282A
		public override bool HandleEvent(GetCarriedWeightEvent E)
		{
			E.Weight += (double)this.GetWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A229 RID: 41513 RVA: 0x003B4647 File Offset: 0x003B2847
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			if (this.Objects.Count == 0)
			{
				this.CheckEmptyState();
			}
			else
			{
				this.CheckNonEmptyState();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A22A RID: 41514 RVA: 0x003B466B File Offset: 0x003B286B
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (this.Objects.Count == 0)
			{
				this.CheckEmptyState();
			}
			else
			{
				this.CheckNonEmptyState();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A22B RID: 41515 RVA: 0x003B4690 File Offset: 0x003B2890
		public override bool HandleEvent(StripContentsEvent E)
		{
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in this.Objects)
			{
				if ((!E.KeepNatural || !gameObject.IsNatural()) && (gameObject.Physics == null || gameObject.Physics.IsReal))
				{
					list.Add(gameObject);
				}
			}
			foreach (GameObject gameObject2 in list)
			{
				gameObject2.Obliterate(null, E.Silent, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A22C RID: 41516 RVA: 0x003B4758 File Offset: 0x003B2958
		public override bool HandleEvent(GetContentsEvent E)
		{
			E.Objects.AddRange(this.Objects);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A22D RID: 41517 RVA: 0x003B4774 File Offset: 0x003B2974
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (!this.ClearOnDeath)
			{
				return true;
			}
			if (this.DropOnDeath && this.Objects.Count > 0 && !this.ParentObject.IsTemporary && this.ParentObject.GetIntProperty("SuppressCorpseDrops", 0) <= 0)
			{
				IInventory dropInventory = this.ParentObject.GetDropInventory();
				if (dropInventory != null)
				{
					List<GameObject> list = Event.NewGameObjectList();
					list.AddRange(this.Objects);
					this.Objects.Clear();
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						GameObject gameObject = list[i];
						gameObject.RemoveFromContext(E);
						if (gameObject.IsReal && DropOnDeathEvent.Check(gameObject, dropInventory, false, true, false, false, false, ".", "DropOnDeath"))
						{
							dropInventory.AddObjectToInventory(gameObject, this.ParentObject, false, false, true, null, E);
							DroppedEvent.Send(this.ParentObject, gameObject, false);
						}
						i++;
					}
				}
				else
				{
					this.Objects.Clear();
				}
				this.FlushWeightCache();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A22E RID: 41518 RVA: 0x003B487F File Offset: 0x003B2A7F
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (E.Name == "Strength" && CheckOverburdenedOnStrengthUpdateEvent.Check(this.ParentObject))
			{
				this.ParentObject.FlushCarriedWeightCache();
				this.CheckOverburdened();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A22F RID: 41519 RVA: 0x003B48B9 File Offset: 0x003B2AB9
		public override bool HandleEvent(CarryingCapacityChangedEvent E)
		{
			this.ParentObject.FlushCarriedWeightCache();
			this.CheckOverburdened();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A230 RID: 41520 RVA: 0x003B48D4 File Offset: 0x003B2AD4
		public override bool HandleEvent(FlushWeightCacheEvent E)
		{
			this.FlushWeightCache();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A231 RID: 41521 RVA: 0x003B48E3 File Offset: 0x003B2AE3
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.VerifyContents();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A232 RID: 41522 RVA: 0x003B48F2 File Offset: 0x003B2AF2
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			this.VerifyContents();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A233 RID: 41523 RVA: 0x003B4904 File Offset: 0x003B2B04
		public int GetOccurrences(GameObject obj)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] == obj)
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600A234 RID: 41524 RVA: 0x003B4940 File Offset: 0x003B2B40
		public int CheckStacks()
		{
			int num = 0;
			int num2 = this.Objects.Count - 1;
			int num3 = 0;
			while (num2 >= 0 && num3 < 100)
			{
				if (this.Objects[num2].CheckStack())
				{
					num++;
					num2 = this.Objects.Count;
				}
				num2--;
				num3++;
			}
			return num;
		}

		// Token: 0x04004335 RID: 17205
		public bool DropOnDeath = true;

		// Token: 0x04004336 RID: 17206
		[NonSerialized]
		public List<GameObject> Objects = new List<GameObject>();

		// Token: 0x04004337 RID: 17207
		[NonSerialized]
		private static Event eCommandRemoveObject = new Event("CommandRemoveObject", "Object", null, "ForEquip", 0);

		// Token: 0x04004338 RID: 17208
		[NonSerialized]
		private static Event eCommandFreeTakeObject = new Event("CommandTakeObject", "Object", null, "Context", null, "EnergyCost", 0);

		// Token: 0x04004339 RID: 17209
		[NonSerialized]
		public bool ClearOnDeath = true;

		// Token: 0x0400433A RID: 17210
		private int WeightCache = -1;
	}
}
