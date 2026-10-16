using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000081 RID: 129
	public readonly struct EventDefinition
	{
		// Token: 0x06000518 RID: 1304 RVA: 0x0000C2BE File Offset: 0x0000A4BE
		[NullableContext(1)]
		internal EventDefinition(MetadataReader reader, EventDefinitionHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		private EventDefinitionHandle Handle
		{
			get
			{
				return EventDefinitionHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000C2E1 File Offset: 0x0000A4E1
		public StringHandle Name
		{
			get
			{
				return this._reader.EventTable.GetName(this.Handle);
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000C2F9 File Offset: 0x0000A4F9
		public EventAttributes Attributes
		{
			get
			{
				return this._reader.EventTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000C311 File Offset: 0x0000A511
		public EntityHandle Type
		{
			get
			{
				return this._reader.EventTable.GetEventType(this.Handle);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000C329 File Offset: 0x0000A529
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000C344 File Offset: 0x0000A544
		public EventAccessors GetAccessors()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ImmutableArray<MethodDefinitionHandle>.Builder builder = null;
			ushort num5;
			int num4 = this._reader.MethodSemanticsTable.FindSemanticMethodsForEvent(this.Handle, out num5);
			for (ushort num6 = 0; num6 < num5; num6 += 1)
			{
				int num7 = num4 + (int)num6;
				MethodSemanticsAttributes semantics = this._reader.MethodSemanticsTable.GetSemantics(num7);
				if (semantics <= MethodSemanticsAttributes.Adder)
				{
					if (semantics != MethodSemanticsAttributes.Other)
					{
						if (semantics == MethodSemanticsAttributes.Adder)
						{
							num = this._reader.MethodSemanticsTable.GetMethod(num7).RowId;
						}
					}
					else
					{
						if (builder == null)
						{
							builder = ImmutableArray.CreateBuilder<MethodDefinitionHandle>();
						}
						builder.Add(this._reader.MethodSemanticsTable.GetMethod(num7));
					}
				}
				else if (semantics != MethodSemanticsAttributes.Remover)
				{
					if (semantics == MethodSemanticsAttributes.Raiser)
					{
						num3 = this._reader.MethodSemanticsTable.GetMethod(num7).RowId;
					}
				}
				else
				{
					num2 = this._reader.MethodSemanticsTable.GetMethod(num7).RowId;
				}
			}
			ImmutableArray<MethodDefinitionHandle> immutableArray = ((builder != null) ? builder.ToImmutable() : ImmutableArray<MethodDefinitionHandle>.Empty);
			return new EventAccessors(num, num2, num3, immutableArray);
		}

		// Token: 0x0400034D RID: 845
		private readonly MetadataReader _reader;

		// Token: 0x0400034E RID: 846
		private readonly int _rowId;
	}
}
