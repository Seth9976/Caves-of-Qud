using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200067D RID: 1661
	internal class TextureSlotManager
	{
		// Token: 0x06003064 RID: 12388 RVA: 0x000B75BC File Offset: 0x000B57BC
		static TextureSlotManager()
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				TextureSlotManager.slotIds[i] = Shader.PropertyToID(string.Format("_Texture{0}", i));
			}
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x000B7624 File Offset: 0x000B5824
		public TextureSlotManager()
		{
			this.m_Textures = new TextureId[TextureSlotManager.k_SlotCount];
			this.m_Tickets = new int[TextureSlotManager.k_SlotCount];
			this.m_GpuTextures = new Vector4[TextureSlotManager.k_SlotCount * TextureSlotManager.k_SlotSize];
			this.Reset();
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x000B768C File Offset: 0x000B588C
		public void Reset()
		{
			this.m_CurrentTicket = 0;
			this.m_FirstUsedTicket = 0;
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				this.m_Textures[i] = TextureId.invalid;
				this.m_Tickets[i] = -1;
				this.SetGpuData(i, TextureId.invalid, 1, 1, 0f, 0f);
			}
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x000B76F4 File Offset: 0x000B58F4
		public void StartNewBatch()
		{
			int num = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num;
			this.m_FirstUsedTicket = num;
			this.FreeSlots = TextureSlotManager.k_SlotCount;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x000B7728 File Offset: 0x000B5928
		public int IndexOf(TextureId id)
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Textures[i].index == id.index;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x000B7774 File Offset: 0x000B5974
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MarkUsed(int slotIndex)
		{
			int num = this.m_Tickets[slotIndex];
			bool flag = num < this.m_FirstUsedTicket;
			int num2;
			if (flag)
			{
				num2 = this.FreeSlots - 1;
				this.FreeSlots = num2;
			}
			int[] tickets = this.m_Tickets;
			num2 = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num2;
			tickets[slotIndex] = num2;
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000B77C2 File Offset: 0x000B59C2
		// (set) Token: 0x0600306B RID: 12395 RVA: 0x000B77CA File Offset: 0x000B59CA
		public int FreeSlots { get; private set; } = TextureSlotManager.k_SlotCount;

		// Token: 0x0600306C RID: 12396 RVA: 0x000B77D4 File Offset: 0x000B59D4
		public int FindOldestSlot()
		{
			int num = this.m_Tickets[0];
			int num2 = 0;
			for (int i = 1; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Tickets[i] < num;
				if (flag)
				{
					num = this.m_Tickets[i];
					num2 = i;
				}
			}
			return num2;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x000B7828 File Offset: 0x000B5A28
		public void Bind(TextureId id, float sdfScale, float sharpness, int slot, MaterialPropertyBlock mat, CommandList commandList = null)
		{
			Texture texture = this.textureRegistry.GetTexture(id);
			bool flag = texture == null;
			if (flag)
			{
				texture = Texture2D.whiteTexture;
			}
			this.m_Textures[slot] = id;
			this.MarkUsed(slot);
			this.SetGpuData(slot, id, texture.width, texture.height, sdfScale, sharpness);
			bool flag2 = commandList == null;
			if (flag2)
			{
				mat.SetTexture(TextureSlotManager.slotIds[slot], texture);
				mat.SetVectorArray(TextureSlotManager.textureTableId, this.m_GpuTextures);
			}
			else
			{
				int num = slot * TextureSlotManager.k_SlotSize;
				commandList.SetTexture(TextureSlotManager.slotIds[slot], texture, num, this.m_GpuTextures[num], this.m_GpuTextures[num + 1]);
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000B78EC File Offset: 0x000B5AEC
		public void SetGpuData(int slotIndex, TextureId id, int textureWidth, int textureHeight, float sdfScale, float sharpness)
		{
			int num = slotIndex * TextureSlotManager.k_SlotSize;
			float num2 = 1f / (float)textureWidth;
			float num3 = 1f / (float)textureHeight;
			this.m_GpuTextures[num] = new Vector4(id.ConvertToGpu(), num2, num3, sdfScale);
			this.m_GpuTextures[num + 1] = new Vector4((float)textureWidth, (float)textureHeight, sharpness, 0f);
		}

		// Token: 0x040017BE RID: 6078
		internal static readonly int k_SlotCount = 8;

		// Token: 0x040017BF RID: 6079
		internal static readonly int k_SlotSize = 2;

		// Token: 0x040017C0 RID: 6080
		internal static readonly int[] slotIds = new int[TextureSlotManager.k_SlotCount];

		// Token: 0x040017C1 RID: 6081
		internal static readonly int textureTableId = Shader.PropertyToID("_TextureInfo");

		// Token: 0x040017C2 RID: 6082
		private TextureId[] m_Textures;

		// Token: 0x040017C3 RID: 6083
		private int[] m_Tickets;

		// Token: 0x040017C4 RID: 6084
		private int m_CurrentTicket;

		// Token: 0x040017C5 RID: 6085
		private int m_FirstUsedTicket;

		// Token: 0x040017C6 RID: 6086
		private Vector4[] m_GpuTextures;

		// Token: 0x040017C8 RID: 6088
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}
