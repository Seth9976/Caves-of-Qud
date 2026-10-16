using System;
using System.Reflection;
using Newtonsoft.Json;

namespace XRL.World.ZoneParts
{
	// Token: 0x020009C0 RID: 2496
	[Serializable]
	public class IZonePart : IComponent<Zone>
	{
		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06006B35 RID: 27445 RVA: 0x0022899C File Offset: 0x00226B9C
		public sealed override IEventBinder Binder
		{
			get
			{
				return IZonePart.EventBinder.Instance;
			}
		}

		// Token: 0x06006B36 RID: 27446 RVA: 0x002289A3 File Offset: 0x00226BA3
		public sealed override void RegisterEvent(int EventID, int Order = 0, bool Serialize = false)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006B37 RID: 27447 RVA: 0x002289AA File Offset: 0x00226BAA
		public sealed override void UnregisterEvent(int EventID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06006B38 RID: 27448 RVA: 0x002289B4 File Offset: 0x00226BB4
		[JsonIgnore]
		public string Name
		{
			get
			{
				return this._Name = ModManager.ResolveTypeName(base.GetType());
			}
		}

		// Token: 0x06006B3A RID: 27450 RVA: 0x002289DD File Offset: 0x00226BDD
		public override Zone GetComponentBasis()
		{
			return this.ParentZone;
		}

		// Token: 0x06006B3B RID: 27451 RVA: 0x002289E5 File Offset: 0x00226BE5
		public virtual bool SameAs(IZonePart Part)
		{
			return Part.ParentZone == this.ParentZone && !(Part.GetType().Name != base.GetType().Name);
		}

		// Token: 0x06006B3C RID: 27452 RVA: 0x00228A18 File Offset: 0x00226C18
		public virtual IZonePart DeepCopy(Zone Parent)
		{
			IZonePart zonePart = Activator.CreateInstance(base.GetType()) as IZonePart;
			foreach (FieldInfo fieldInfo in base.GetType().GetFields())
			{
				if ((fieldInfo.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope && !fieldInfo.IsLiteral)
				{
					fieldInfo.SetValue(zonePart, fieldInfo.GetValue(this));
				}
			}
			zonePart.ParentZone = Parent;
			return zonePart;
		}

		// Token: 0x06006B3D RID: 27453 RVA: 0x00228A80 File Offset: 0x00226C80
		public static void Save(IZonePart Part, SerializationWriter Writer)
		{
			SerializationWriter.Block block = Writer.StartBlock();
			Type type = Part.GetType();
			try
			{
				Writer.WriteTokenized(type);
				Part.Write(Part.ParentZone, Writer);
			}
			catch (Exception ex)
			{
				block.Reset();
				Type type2 = type;
				string text = "Skipping failed serialization of zone part '";
				string fullName = type.FullName;
				string text2 = "': ";
				Exception ex2 = ex;
				MetricsManager.LogAssemblyError(type2, text + fullName + text2 + ((ex2 != null) ? ex2.ToString() : null));
			}
			finally
			{
				block.Dispose();
			}
		}

		// Token: 0x06006B3E RID: 27454 RVA: 0x00228B08 File Offset: 0x00226D08
		public static IZonePart Load(Zone Zone, SerializationReader Reader)
		{
			long num;
			int num2;
			Reader.StartBlock(out num, out num2);
			if (num2 == 0)
			{
				return null;
			}
			Type type = null;
			IZonePart zonePart = null;
			try
			{
				type = Reader.ReadTokenizedType();
				zonePart = (IZonePart)Activator.CreateInstance(type);
				zonePart.ParentZone = Zone;
				zonePart.Read(Zone, Reader);
			}
			catch (Exception ex)
			{
				if (zonePart == null || !zonePart.ReadError(ex, Reader, num, num2))
				{
					Reader.SkipBlock(ex, type, num, num2);
				}
			}
			return zonePart;
		}

		// Token: 0x06006B3F RID: 27455 RVA: 0x00228B7C File Offset: 0x00226D7C
		public virtual void Setup()
		{
		}

		// Token: 0x06006B40 RID: 27456 RVA: 0x00228B7E File Offset: 0x00226D7E
		public virtual void Remove()
		{
		}

		// Token: 0x06006B41 RID: 27457 RVA: 0x00228B80 File Offset: 0x00226D80
		public virtual void Initialize()
		{
		}

		// Token: 0x06006B42 RID: 27458 RVA: 0x00228B82 File Offset: 0x00226D82
		public virtual void Attach()
		{
		}

		// Token: 0x06006B43 RID: 27459 RVA: 0x00228B84 File Offset: 0x00226D84
		public virtual void AddedAfterCreation()
		{
		}

		// Token: 0x04003117 RID: 12567
		[NonSerialized]
		public Zone ParentZone;

		// Token: 0x04003118 RID: 12568
		[NonSerialized]
		private string _Name;

		// Token: 0x02001AC7 RID: 6855
		public class EventBinder : IEventBinder
		{
			// Token: 0x0600E90C RID: 59660 RVA: 0x0053A8DE File Offset: 0x00538ADE
			public override void WriteBind(SerializationWriter Writer, IEventHandler Handler, int ID)
			{
				Zone parentZone = ((IZonePart)Handler).ParentZone;
				Writer.WriteOptimized((parentZone != null) ? parentZone.ZoneID : null);
				Writer.WriteTokenized(Handler.GetType());
			}

			// Token: 0x0600E90D RID: 59661 RVA: 0x0053A90C File Offset: 0x00538B0C
			public override IEventHandler ReadBind(SerializationReader Reader, int ID)
			{
				string text = Reader.ReadOptimizedString();
				Type type = Reader.ReadTokenizedType();
				Zone zone;
				if (!text.IsNullOrEmpty() && The.ZoneManager.CachedZones.TryGetValue(text, out zone) && !zone.Parts.IsNullOrEmpty<IZonePart>())
				{
					foreach (IZonePart zonePart in zone.Parts)
					{
						if (zonePart.GetType() == type)
						{
							return zonePart;
						}
					}
				}
				return null;
			}

			// Token: 0x04006A8B RID: 27275
			public static readonly IZonePart.EventBinder Instance = new IZonePart.EventBinder();
		}
	}
}
