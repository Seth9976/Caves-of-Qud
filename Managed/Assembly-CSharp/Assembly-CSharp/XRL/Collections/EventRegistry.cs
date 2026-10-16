using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text;
using Genkit;
using XRL.World;

namespace XRL.Collections
{
	// Token: 0x020005CC RID: 1484
	[Serializable]
	public class EventRegistry : IComposite, IDisposable
	{
		// Token: 0x06003F30 RID: 16176 RVA: 0x0013759C File Offset: 0x0013579C
		public static EventRegistry Get()
		{
			EventRegistry eventRegistry;
			if (!EventRegistry.RegistryPool.TryTake(out eventRegistry))
			{
				eventRegistry = new EventRegistry();
			}
			eventRegistry.Size = 0;
			return eventRegistry;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x001375C5 File Offset: 0x001357C5
		public static void Return(EventRegistry Registry)
		{
			if (Registry.Size == 0)
			{
				Registry.Size = -1;
				EventRegistry.RegistryPool.Add(Registry);
				return;
			}
			if (Registry.Size > 0)
			{
				Registry.Dispose();
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x001375F1 File Offset: 0x001357F1
		public int Capacity
		{
			get
			{
				return this.Size;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x001375F9 File Offset: 0x001357F9
		public int Count
		{
			get
			{
				return this.Amount;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x00137601 File Offset: 0x00135801
		public bool Serialize
		{
			get
			{
				return this.Serialized != 0;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x0013760C File Offset: 0x0013580C
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x0013760F File Offset: 0x0013580F
		public EventRegistry()
		{
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x00137634 File Offset: 0x00135834
		public EventRegistry(int Capacity)
		{
			if (Capacity > 0)
			{
				Capacity = Hash.GetSharedCapacity(Capacity);
				this.Buckets = EventRegistry.BucketPool.Rent(Capacity);
				this.Slots = EventRegistry.SlotPool.Rent(Capacity);
				this.Size = Capacity;
			}
		}

		// Token: 0x17000673 RID: 1651
		public EventRegistry.List this[MinEvent E]
		{
			get
			{
				int num = this.IndexOf(E.ID);
				if (num < 0)
				{
					return null;
				}
				return this.Slots[num].Value;
			}
		}

		// Token: 0x17000674 RID: 1652
		public EventRegistry.List this[int ID]
		{
			get
			{
				int num = this.IndexOf(ID);
				if (num < 0)
				{
					return null;
				}
				return this.Slots[num].Value;
			}
		}

		/// <summary>Register a handler to receive a <see cref="T:XRL.World.MinEvent" />.</summary>
		/// <param name="Handler">An event handler such as <see cref="T:XRL.World.IPart" /> or <see cref="T:XRL.World.Effect" />.</param>
		/// <param name="EventID">The ID of a <see cref="T:XRL.World.MinEvent" />.</param>
		/// <param name="Order">
		/// The handling order of the event. A negative order will execute before standard event cascading,
		/// while a positive order will execute after.
		/// </param>
		/// <param name="Serialize">
		/// Persist event binding through serialization.
		/// If this is false, registration is transient and will have to be re-executed after game load.
		/// </param>
		// Token: 0x06003F3A RID: 16186 RVA: 0x001376FC File Offset: 0x001358FC
		public void Register(IEventHandler Handler, int ID, int Order = 0, bool Serialize = false)
		{
			int num = this.IndexOf(ID);
			if (num >= 0)
			{
				this.Slots[num].Value.Require(Handler, Order, Serialize);
				return;
			}
			EventRegistry.List list = EventRegistry.List.Get(this, ID);
			list.Add(Handler, Order, Serialize);
			this.Insert(ID, list, false, false);
		}

		/// <summary>Unregister a handler from receiving a <see cref="T:XRL.World.MinEvent" />.</summary>
		/// <param name="Handler">An event handler such as <see cref="T:XRL.World.IPart" /> or <see cref="T:XRL.World.Effect" />.</param>
		/// <param name="EventID">The ID of a <see cref="T:XRL.World.MinEvent" />.</param>
		/// <returns><c>true</c> if a matching handler was unregistered; otherwise <c>false</c>.</returns>
		// Token: 0x06003F3B RID: 16187 RVA: 0x00137750 File Offset: 0x00135950
		public bool Unregister(IEventHandler Handler, int ID)
		{
			int num = this.IndexOf(ID);
			if (num == -1)
			{
				return false;
			}
			EventRegistry.List value = this.Slots[num].Value;
			int num2 = value.IndexOf(Handler);
			if (num2 == -1)
			{
				return false;
			}
			if (num2 == 0 && value.Count == 1)
			{
				this.Remove(ID);
			}
			else
			{
				value.RemoveAt(num2);
			}
			return true;
		}

		/// <summary>Unregister a handler from receiving any MinEvent.</summary>
		/// <param name="Handler">An event handler such as <see cref="T:XRL.World.IPart" /> or <see cref="T:XRL.World.Effect" />.</param>
		/// <returns><c>true</c> if a matching handler was unregistered; otherwise <c>false</c>.</returns>
		/// <remarks>This has to linearly iterate all handlers within the collection, specifying ID is preferred.</remarks>
		// Token: 0x06003F3C RID: 16188 RVA: 0x001377AC File Offset: 0x001359AC
		public bool Unregister(IEventHandler Handler)
		{
			bool flag = false;
			for (int i = 0; i < this.Length; i++)
			{
				EventRegistry.List value = this.Slots[i].Value;
				if (value != null)
				{
					int num = value.IndexOf(Handler);
					if (num != -1)
					{
						if (num == 0 && value.Count == 1)
						{
							this.Remove(this.Slots[i].ID);
						}
						else
						{
							value.RemoveAt(num);
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x00137820 File Offset: 0x00135A20
		public void Clear()
		{
			if (this.Length <= 0)
			{
				return;
			}
			int num = 0;
			while (num < this.Length && num < this.Slots.Length)
			{
				EventRegistry.List value = this.Slots[num].Value;
				if (value != null)
				{
					value.Dispose();
				}
				this.Slots[num] = default(EventRegistry.Slot);
				num++;
			}
			for (int i = 0; i < this.Size; i++)
			{
				this.Buckets[i] = 0;
			}
			this.Next = -1;
			this.Length = 0;
			this.Amount = 0;
			this.Serialized = 0;
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x001378B7 File Offset: 0x00135AB7
		public bool ContainsKey(int ID)
		{
			return this.IndexOf(ID) >= 0;
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x001378C8 File Offset: 0x00135AC8
		public bool TryGetValue(int ID, out EventRegistry.List Handlers)
		{
			int num = this.IndexOf(ID);
			if (num >= 0)
			{
				Handlers = this.Slots[num].Value;
				return true;
			}
			Handlers = null;
			return false;
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x001378FC File Offset: 0x00135AFC
		public int GetRegisteredCount(int ID)
		{
			int num = this.IndexOf(ID);
			if (num != -1)
			{
				return this.Slots[num].Value.Count;
			}
			return 0;
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x00137930 File Offset: 0x00135B30
		protected int IndexOf(int ID)
		{
			if (this.Size > 0)
			{
				uint num = (uint)(ID % this.Size);
				for (int i = this.Buckets[(int)num] - 1; i >= 0; i = this.Slots[i].Next)
				{
					if (this.Slots[i].ID == ID)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x0013798C File Offset: 0x00135B8C
		protected bool Insert(int ID, EventRegistry.List Value, bool ReturnOnDuplicate = false, bool ThrowOnDuplicate = false)
		{
			uint num = 0U;
			int i;
			if (this.Size > 0)
			{
				num = (uint)(ID % this.Size);
				i = this.Buckets[(int)num] - 1;
				while (i >= 0)
				{
					if (this.Slots[i].ID == ID)
					{
						if (ThrowOnDuplicate)
						{
							throw new ArgumentException(string.Format("Event by ID '{0}' already registered.", ID));
						}
						if (ReturnOnDuplicate)
						{
							return true;
						}
						this.Slots[i].Value = Value;
						return true;
					}
					else
					{
						i = this.Slots[i].Next;
					}
				}
			}
			if (this.Next >= 0)
			{
				this.Next = this.Slots[i = this.Next].Next;
			}
			else
			{
				if (this.Length == this.Size)
				{
					this.Resize(this.Length * 2);
					num = (uint)(ID % this.Size);
				}
				int length = this.Length;
				this.Length = length + 1;
				i = length;
			}
			this.Slots[i].ID = ID;
			this.Slots[i].Next = this.Buckets[(int)num] - 1;
			this.Slots[i].Value = Value;
			this.Buckets[(int)num] = i + 1;
			this.Amount++;
			return true;
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x00137AD4 File Offset: 0x00135CD4
		protected void Resize(int Capacity)
		{
			Capacity = Hash.GetSharedCapacity(Capacity);
			if (Capacity == this.Size)
			{
				return;
			}
			int[] array = EventRegistry.BucketPool.Rent(Capacity);
			EventRegistry.Slot[] array2 = EventRegistry.SlotPool.Rent(Capacity);
			for (int i = 0; i < this.Length; i++)
			{
				array2[i] = this.Slots[i];
				this.Slots[i] = default(EventRegistry.Slot);
				if (array2[i].ID != 0)
				{
					uint num = (uint)(array2[i].ID % Capacity);
					array2[i].Next = array[(int)num] - 1;
					array[(int)num] = i + 1;
				}
			}
			EventRegistry.BucketPool.Return(this.Buckets, true);
			EventRegistry.SlotPool.Return(this.Slots, false);
			this.Buckets = array;
			this.Slots = array2;
			this.Size = Capacity;
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x00137BAC File Offset: 0x00135DAC
		public bool Remove(int ID)
		{
			if (this.Size == 0)
			{
				return false;
			}
			uint num = (uint)(ID % this.Size);
			int i = this.Buckets[(int)num] - 1;
			int num2 = -1;
			while (i >= 0)
			{
				if (this.Slots[i].ID == ID)
				{
					if (num2 == -1)
					{
						this.Buckets[(int)num] = this.Slots[i].Next + 1;
					}
					else
					{
						this.Slots[num2].Next = this.Slots[i].Next;
					}
					if (this.Slots[i].Value.Serialize)
					{
						this.Serialized--;
					}
					this.Slots[i].Next = this.Next;
					this.Slots[i].ID = 0;
					this.Slots[i].Value.Dispose();
					this.Slots[i].Value = null;
					this.Next = i;
					this.Amount--;
					return true;
				}
				num2 = i;
				i = this.Slots[i].Next;
			}
			return false;
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x00137CE4 File Offset: 0x00135EE4
		public bool Dispatch(MinEvent E)
		{
			int num = this.IndexOf(E.ID);
			return num == -1 || this.Slots[num].Value.Dispatch(E);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x00137D1C File Offset: 0x00135F1C
		public bool DispatchRange(MinEvent E, int Min = -2147483648, int Max = 2147483647)
		{
			int num = this.IndexOf(E.ID);
			return num == -1 || this.Slots[num].Value.DispatchRange(E, Min, Max);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x00137D58 File Offset: 0x00135F58
		public static void Write(SerializationWriter Writer, EventRegistry Registry)
		{
			if (Registry == null || Registry.Serialized == 0)
			{
				Writer.WriteOptimized(0);
				return;
			}
			Writer.WriteOptimized(Registry.Serialized);
			for (int i = 0; i < Registry.Length; i++)
			{
				if (Registry.Slots[i].ID != 0 && Registry.Slots[i].Value.Serialize)
				{
					Registry.Slots[i].Value.Write(Writer);
				}
			}
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00137DD7 File Offset: 0x00135FD7
		public void Write(SerializationWriter Writer)
		{
			EventRegistry.Write(Writer, this);
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00137DE0 File Offset: 0x00135FE0
		public static void Read(SerializationReader Reader, ref EventRegistry Registry)
		{
			int num = Reader.ReadOptimizedInt32();
			if (num <= 0)
			{
				return;
			}
			if (Registry == null)
			{
				Registry = EventRegistry.Get();
			}
			else if (Registry.Size < num)
			{
				Registry.Resize(num);
			}
			for (int i = 0; i < num; i++)
			{
				EventRegistry.List list = EventRegistry.List.Get(Registry, 0);
				list.Read(Reader);
				if (list.Count == 0)
				{
					list.Dispose();
				}
				else
				{
					Registry.Insert(list.EventID, list, false, false);
				}
			}
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x00137E58 File Offset: 0x00136058
		public void Read(SerializationReader Reader)
		{
			EventRegistry eventRegistry = this;
			EventRegistry.Read(Reader, ref eventRegistry);
		}

		/// <inheritdoc cref="M:XRL.Collections.EventRegistry.List.Clean" />
		// Token: 0x06003F4B RID: 16203 RVA: 0x00137E70 File Offset: 0x00136070
		public int Clean()
		{
			int num = 0;
			for (int i = this.Length - 1; i >= 0; i--)
			{
				if (this.Slots[i].ID != 0)
				{
					int num2 = this.Slots[i].Value.Clean();
					if (num2 > 0)
					{
						num += num2;
						if (this.Slots[i].Value.Count == 0)
						{
							this.Remove(this.Slots[i].ID);
						}
					}
				}
			}
			return num;
		}

		/// <summary>Return this registry and its rented arrays to their respective pools.</summary>
		// Token: 0x06003F4C RID: 16204 RVA: 0x00137EF8 File Offset: 0x001360F8
		public virtual void Dispose()
		{
			if (this.Size <= 0)
			{
				return;
			}
			this.Clear();
			this.Size = 0;
			EventRegistry.SlotPool.Return<EventRegistry.Slot>(ref this.Slots, false);
			EventRegistry.BucketPool.Return<int>(ref this.Buckets, false);
			EventRegistry.Return(this);
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x00137F44 File Offset: 0x00136144
		~EventRegistry()
		{
			this.Dispose();
		}

		// Token: 0x04001F32 RID: 7986
		protected static readonly ConcurrentBag<EventRegistry> RegistryPool = new ConcurrentBag<EventRegistry>();

		// Token: 0x04001F33 RID: 7987
		protected static readonly ArrayPool<int> BucketPool = ArrayPool<int>.Shared;

		// Token: 0x04001F34 RID: 7988
		protected static readonly ArrayPool<EventRegistry.Slot> SlotPool = ArrayPool<EventRegistry.Slot>.Shared;

		// Token: 0x04001F35 RID: 7989
		protected int[] Buckets = Array.Empty<int>();

		// Token: 0x04001F36 RID: 7990
		protected EventRegistry.Slot[] Slots = Array.Empty<EventRegistry.Slot>();

		// Token: 0x04001F37 RID: 7991
		protected int Size;

		// Token: 0x04001F38 RID: 7992
		protected int Length;

		// Token: 0x04001F39 RID: 7993
		protected int Amount;

		// Token: 0x04001F3A RID: 7994
		protected int Next = -1;

		// Token: 0x04001F3B RID: 7995
		protected int Serialized;

		// Token: 0x020019DE RID: 6622
		public class List : PooledContainer<EventRegistry.List.Entry>
		{
			// Token: 0x0600E571 RID: 58737 RVA: 0x0052E844 File Offset: 0x0052CA44
			public static EventRegistry.List Get(EventRegistry Registry, int EventID = 0)
			{
				EventRegistry.List list;
				if (!EventRegistry.List.Bag.TryTake(out list))
				{
					list = new EventRegistry.List();
				}
				list.Registry = Registry;
				list.EventID = EventID;
				list.Size = 0;
				return list;
			}

			// Token: 0x0600E572 RID: 58738 RVA: 0x0052E87B File Offset: 0x0052CA7B
			public static void Return(EventRegistry.List List)
			{
				if (List.Registry != null)
				{
					List.Dispose();
					return;
				}
				if (List.Size != -1)
				{
					List.Size = -1;
					EventRegistry.List.Bag.Add(List);
				}
			}

			// Token: 0x17000EAA RID: 3754
			// (get) Token: 0x0600E573 RID: 58739 RVA: 0x0052E8A7 File Offset: 0x0052CAA7
			public bool Serialize
			{
				get
				{
					return this.Serialized != 0;
				}
			}

			// Token: 0x17000EAB RID: 3755
			// (get) Token: 0x0600E574 RID: 58740 RVA: 0x0052E8B2 File Offset: 0x0052CAB2
			public Type EventType
			{
				get
				{
					return MinEvent.ResolveEvent(this.EventID);
				}
			}

			// Token: 0x0600E575 RID: 58741 RVA: 0x0052E8BF File Offset: 0x0052CABF
			public List()
			{
			}

			// Token: 0x0600E576 RID: 58742 RVA: 0x0052E8C7 File Offset: 0x0052CAC7
			public List(EventRegistry Registry, int Capacity = 0, int EventID = 0)
			{
				this.Registry = Registry;
				this.EventID = EventID;
				base.EnsureCapacity(Capacity);
			}

			// Token: 0x0600E577 RID: 58743 RVA: 0x0052E8E4 File Offset: 0x0052CAE4
			public void Require(IEventHandler Handler, int Order = 0, bool Serialize = false)
			{
				int num = this.IndexOf(Handler);
				if (num == -1)
				{
					this.Add(new EventRegistry.List.Entry(Handler, Order, Serialize));
					return;
				}
				if (this.Items[num].Serialize != Serialize)
				{
					this.Items[num].Serialize = Serialize;
					if (Serialize)
					{
						int num2 = this.Serialized;
						this.Serialized = num2 + 1;
						if (num2 == 0)
						{
							this.Registry.Serialized++;
						}
					}
					else
					{
						int num2 = this.Serialized;
						this.Serialized = num2 - 1;
						if (num2 == 1)
						{
							this.Registry.Serialized--;
						}
					}
				}
				if (this.Items[num].Order != Order)
				{
					int num3 = num;
					if (num3 != 0 && this.Items[num3 - 1].Order > Order)
					{
						while (--num3 > 0 && this.Items[num3 - 1].Order > Order)
						{
						}
						this.Shift(num, num3);
					}
					else if (num3 != this.Length - 1 && this.Items[num3 + 1].Order <= Order)
					{
						while (++num3 < this.Length - 1 && this.Items[num3 + 1].Order <= Order)
						{
						}
						this.Shift(num, num3);
					}
					this.Items[num3].Order = Order;
				}
			}

			// Token: 0x0600E578 RID: 58744 RVA: 0x0052EA40 File Offset: 0x0052CC40
			public void Shift(int From, int To)
			{
				EventRegistry.List.Entry entry = this.Items[From];
				if (From > To)
				{
					Array.Copy(this.Items, To, this.Items, To + 1, From - To);
				}
				else
				{
					Array.Copy(this.Items, From + 1, this.Items, From, To - From);
				}
				this.Items[To] = entry;
				this.Version++;
			}

			// Token: 0x0600E579 RID: 58745 RVA: 0x0052EAAB File Offset: 0x0052CCAB
			public void Add(IEventHandler Handler, int Order = 0, bool Serialize = false)
			{
				this.Add(new EventRegistry.List.Entry(Handler, Order, Serialize));
			}

			// Token: 0x0600E57A RID: 58746 RVA: 0x0052EABC File Offset: 0x0052CCBC
			public void Add(EventRegistry.List.Entry Item)
			{
				if (this.Size < 0)
				{
					this.LogDiagnostics(Item, -1, null);
					base.Resize(this.DefaultCapacity);
				}
				else if (this.Length >= this.Size)
				{
					base.Resize(this.Length * 2);
				}
				int num = this.InsertOrder(Item.Order);
				try
				{
					if (num < this.Length)
					{
						Array.Copy(this.Items, num, this.Items, num + 1, this.Length - num);
					}
					if (Item.Serialize)
					{
						int serialized = this.Serialized;
						this.Serialized = serialized + 1;
						if (serialized == 0)
						{
							this.Registry.Serialized++;
						}
					}
					this.Items[num] = Item;
					this.Length++;
					this.Version++;
				}
				catch (Exception ex)
				{
					this.LogDiagnostics(Item, num, ex);
				}
			}

			// Token: 0x0600E57B RID: 58747 RVA: 0x0052EBB0 File Offset: 0x0052CDB0
			private void LogDiagnostics(EventRegistry.List.Entry Item, int Index = -1, Exception Exception = null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Registry.Add");
				stringBuilder.Append("\nIndex: ").Append(Index);
				stringBuilder.Append("\nOrder: ").Append(Item.Order);
				StringBuilder stringBuilder2 = stringBuilder.Append("\nHandler: ");
				IEventHandler handler = Item.Handler;
				stringBuilder2.Append(((handler != null) ? handler.GetType().GetName(false) : null) ?? "null");
				StringBuilder stringBuilder3 = stringBuilder.Append("\nBasis: ");
				IComponent<GameObject> component = Item.Handler as IComponent<GameObject>;
				string text;
				if (component == null)
				{
					text = null;
				}
				else
				{
					GameObject basisGameObject = component.GetBasisGameObject();
					text = ((basisGameObject != null) ? basisGameObject.DebugName : null);
				}
				stringBuilder3.Append(text ?? "none");
				stringBuilder.Append("\nLength: ").Append(this.Length);
				stringBuilder.Append("\nEventType: ").Append(this.EventType);
				stringBuilder.Append("\nSize: ").Append(this.Size);
				stringBuilder.Append("\nArrLen: ").Append(this.Items.Length);
				stringBuilder.Append("\nArray:");
				for (int i = 0; i < this.Items.Length; i++)
				{
					EventRegistry.List.Entry entry = this.Items[i];
					stringBuilder.Append("\n  [").Append(i).Append("] ");
					StringBuilder stringBuilder4 = stringBuilder;
					IEventHandler handler2 = entry.Handler;
					stringBuilder4.Append(((handler2 != null) ? handler2.GetType().GetName(false) : null) ?? "null");
					stringBuilder.Append(", ").Append(entry.Order);
					stringBuilder.Append(", ").Append(entry.Serialize);
				}
				stringBuilder.Append(":\n");
				if (Exception != null)
				{
					MetricsManager.LogException(stringBuilder.ToString(), Exception, "game_exception");
					return;
				}
				MetricsManager.LogError(stringBuilder.ToString());
			}

			// Token: 0x0600E57C RID: 58748 RVA: 0x0052ED98 File Offset: 0x0052CF98
			public bool Remove(IEventHandler Handler)
			{
				int num = this.IndexOf(Handler);
				if (num != -1)
				{
					this.RemoveAt(num);
					return true;
				}
				return false;
			}

			// Token: 0x0600E57D RID: 58749 RVA: 0x0052EDBC File Offset: 0x0052CFBC
			public void RemoveAt(int Index)
			{
				if (Index >= this.Length)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.Items[Index].Serialize)
				{
					int serialized = this.Serialized;
					this.Serialized = serialized - 1;
					if (serialized == 1)
					{
						this.Registry.Serialized--;
					}
				}
				this.Length--;
				if (Index < this.Length)
				{
					Array.Copy(this.Items, Index + 1, this.Items, Index, this.Length - Index);
				}
				this.Items[this.Length] = default(EventRegistry.List.Entry);
				this.Version++;
			}

			// Token: 0x0600E57E RID: 58750 RVA: 0x0052EE6C File Offset: 0x0052D06C
			public int IndexOf(IEventHandler Handler)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (this.Items[i].Handler == Handler)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600E57F RID: 58751 RVA: 0x0052EEA4 File Offset: 0x0052D0A4
			protected int InsertOrder(int Order)
			{
				if (this.Length == 0)
				{
					return 0;
				}
				int num;
				if (Order < 0)
				{
					num = 0;
					while (this.Items[num].Order < Order)
					{
						if (++num >= this.Length)
						{
							break;
						}
					}
				}
				else
				{
					num = this.Length;
					while (this.Items[num - 1].Order > Order && --num > 0)
					{
					}
				}
				return num;
			}

			// Token: 0x0600E580 RID: 58752 RVA: 0x0052EF0C File Offset: 0x0052D10C
			public bool Dispatch(MinEvent E)
			{
				int num = this.Length;
				for (int i = 0; i < num; i++)
				{
					IEventHandler handler = this.Items[i].Handler;
					if (!E.Dispatch(handler))
					{
						return false;
					}
					if (num != this.Length)
					{
						num = this.Length;
						if (i < num && this.Items[i].Handler != handler)
						{
							i--;
						}
					}
				}
				return true;
			}

			// Token: 0x0600E581 RID: 58753 RVA: 0x0052EF78 File Offset: 0x0052D178
			public bool DispatchRange(MinEvent E, int Min = -2147483648, int Max = 2147483647)
			{
				int num = this.Length;
				for (int i = 0; i < num; i++)
				{
					if (this.Items[i].Order >= Min && this.Items[i].Order <= Max)
					{
						IEventHandler handler = this.Items[i].Handler;
						if (!E.Dispatch(handler))
						{
							return false;
						}
						if (num != this.Length)
						{
							num = this.Length;
							if (i < num && this.Items[i].Handler != handler)
							{
								i--;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x0600E582 RID: 58754 RVA: 0x0052F00C File Offset: 0x0052D20C
			public override void Write(SerializationWriter Writer)
			{
				Writer.WriteOptimized(this.Serialized);
				Writer.WriteOptimized(this.EventID);
				for (int i = 0; i < this.Length; i++)
				{
					if (this.Items[i].Serialize)
					{
						IEventBinder binder = this.Items[i].Handler.Binder;
						Writer.WriteTokenized(binder);
						binder.WriteBind(Writer, this.Items[i].Handler, this.EventID);
						Writer.WriteOptimized(this.Items[i].Order);
					}
				}
			}

			// Token: 0x0600E583 RID: 58755 RVA: 0x0052F0A8 File Offset: 0x0052D2A8
			public override void Read(SerializationReader Reader)
			{
				if (this.Registry == null)
				{
					throw new InvalidOperationException("Event handler list does not have a parent registry to track serialized registrations.");
				}
				int num = Reader.ReadOptimizedInt32();
				this.EventID = Reader.ReadOptimizedInt32();
				this.Items = this.Pool.Rent(num);
				this.Size = this.Items.Length;
				for (int i = 0; i < num; i++)
				{
					IEventHandler eventHandler = ((IEventBinder)Reader.ReadTokenized()).ReadBind(Reader, this.EventID);
					int num2 = Reader.ReadOptimizedInt32();
					if (eventHandler == null)
					{
						num--;
						i--;
					}
					else
					{
						this.Items[i].Handler = eventHandler;
						this.Items[i].Order = num2;
						this.Items[i].Serialize = true;
					}
				}
				this.Length = (this.Serialized = num);
				if (num > 0)
				{
					this.Registry.Serialized++;
				}
			}

			// Token: 0x0600E584 RID: 58756 RVA: 0x0052F192 File Offset: 0x0052D392
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public EventRegistry.List.Entry[] GetArray()
			{
				return this.Items;
			}

			/// <summary>Clean up invalid handlers whose game objects have been pooled or otherwise gone out of scope.</summary>
			/// <remarks>A deeper cleansing happens as part of a serialization cycle.</remarks>
			// Token: 0x0600E585 RID: 58757 RVA: 0x0052F19C File Offset: 0x0052D39C
			public int Clean()
			{
				int num = 0;
				for (int i = this.Length - 1; i >= 0; i--)
				{
					if (!this.Items[i].Handler.IsValid)
					{
						this.RemoveAt(i);
						num++;
					}
				}
				return num;
			}

			// Token: 0x0600E586 RID: 58758 RVA: 0x0052F1E2 File Offset: 0x0052D3E2
			public override void Dispose()
			{
				base.Dispose();
				this.Registry = null;
				this.EventID = 0;
				this.Serialized = 0;
				EventRegistry.List.Return(this);
			}

			// Token: 0x0400677D RID: 26493
			protected static readonly ConcurrentBag<EventRegistry.List> Bag = new ConcurrentBag<EventRegistry.List>();

			// Token: 0x0400677E RID: 26494
			protected EventRegistry Registry;

			// Token: 0x0400677F RID: 26495
			public int EventID;

			// Token: 0x04006780 RID: 26496
			protected int Serialized;

			// Token: 0x02001E8D RID: 7821
			public struct Entry
			{
				// Token: 0x0600F7C8 RID: 63432 RVA: 0x0055D143 File Offset: 0x0055B343
				public Entry(IEventHandler Handler, int Order = 0, bool Serialize = false)
				{
					this.Handler = Handler;
					this.Order = Order;
					this.Serialize = Serialize;
				}

				// Token: 0x040076B0 RID: 30384
				public IEventHandler Handler;

				// Token: 0x040076B1 RID: 30385
				public int Order;

				// Token: 0x040076B2 RID: 30386
				public bool Serialize;
			}
		}

		// Token: 0x020019DF RID: 6623
		protected struct Slot
		{
			// Token: 0x04006781 RID: 26497
			public int ID;

			// Token: 0x04006782 RID: 26498
			public int Next;

			// Token: 0x04006783 RID: 26499
			public EventRegistry.List Value;
		}
	}
}
