using System;
using System.Runtime.InteropServices;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000063 RID: 99
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public readonly struct ProfilerCategory
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00003D69 File Offset: 0x00001F69
		public ProfilerCategory(string categoryName)
		{
			this.m_CategoryId = ProfilerUnsafeUtility.CreateCategory(categoryName, ProfilerCategoryColor.Scripts);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003D79 File Offset: 0x00001F79
		public ProfilerCategory(string categoryName, ProfilerCategoryColor color)
		{
			this.m_CategoryId = ProfilerUnsafeUtility.CreateCategory(categoryName, color);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00003D89 File Offset: 0x00001F89
		internal ProfilerCategory(ushort category)
		{
			this.m_CategoryId = category;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003D94 File Offset: 0x00001F94
		public string Name
		{
			get
			{
				ProfilerCategoryDescription categoryDescription = ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId);
				return ProfilerUnsafeUtility.Utf8ToString(categoryDescription.NameUtf8, categoryDescription.NameUtf8Len);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003DC3 File Offset: 0x00001FC3
		public Color32 Color
		{
			get
			{
				return ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId).Color;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public static ProfilerCategory Render
		{
			get
			{
				return new ProfilerCategory(0);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public static ProfilerCategory Scripts
		{
			get
			{
				return new ProfilerCategory(1);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00003E00 File Offset: 0x00002000
		public static ProfilerCategory Gui
		{
			get
			{
				return new ProfilerCategory(4);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003E08 File Offset: 0x00002008
		public static ProfilerCategory Physics
		{
			get
			{
				return new ProfilerCategory(5);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00003E10 File Offset: 0x00002010
		public static ProfilerCategory Physics2D
		{
			get
			{
				return new ProfilerCategory(33);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00003E19 File Offset: 0x00002019
		public static ProfilerCategory Animation
		{
			get
			{
				return new ProfilerCategory(6);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003E21 File Offset: 0x00002021
		public static ProfilerCategory Ai
		{
			get
			{
				return new ProfilerCategory(7);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00003E29 File Offset: 0x00002029
		public static ProfilerCategory Audio
		{
			get
			{
				return new ProfilerCategory(8);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003E31 File Offset: 0x00002031
		public static ProfilerCategory Video
		{
			get
			{
				return new ProfilerCategory(11);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00003E3A File Offset: 0x0000203A
		public static ProfilerCategory Particles
		{
			get
			{
				return new ProfilerCategory(12);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00003E43 File Offset: 0x00002043
		public static ProfilerCategory Lighting
		{
			get
			{
				return new ProfilerCategory(13);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00003E4C File Offset: 0x0000204C
		public static ProfilerCategory Network
		{
			get
			{
				return new ProfilerCategory(14);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003E55 File Offset: 0x00002055
		public static ProfilerCategory Loading
		{
			get
			{
				return new ProfilerCategory(15);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00003E5E File Offset: 0x0000205E
		public static ProfilerCategory Vr
		{
			get
			{
				return new ProfilerCategory(22);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00003E67 File Offset: 0x00002067
		public static ProfilerCategory Input
		{
			get
			{
				return new ProfilerCategory(30);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003E70 File Offset: 0x00002070
		public static ProfilerCategory Memory
		{
			get
			{
				return new ProfilerCategory(23);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00003E79 File Offset: 0x00002079
		public static ProfilerCategory VirtualTexturing
		{
			get
			{
				return new ProfilerCategory(31);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003E82 File Offset: 0x00002082
		public static ProfilerCategory FileIO
		{
			get
			{
				return new ProfilerCategory(25);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00003E8B File Offset: 0x0000208B
		public static ProfilerCategory Internal
		{
			get
			{
				return new ProfilerCategory(24);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00003E94 File Offset: 0x00002094
		internal static ProfilerCategory Any
		{
			get
			{
				return new ProfilerCategory(ushort.MaxValue);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00003EA0 File Offset: 0x000020A0
		internal static ProfilerCategory GPU
		{
			get
			{
				return new ProfilerCategory(32);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00003EAC File Offset: 0x000020AC
		public static implicit operator ushort(ProfilerCategory category)
		{
			return category.m_CategoryId;
		}

		// Token: 0x0400015A RID: 346
		[FieldOffset(0)]
		private readonly ushort m_CategoryId;
	}
}
