using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Pool;

namespace Unity.Properties
{
	// Token: 0x0200002B RID: 43
	public readonly struct PropertyPath : IEquatable<PropertyPath>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003881 File Offset: 0x00001A81
		public int Length { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003889 File Offset: 0x00001A89
		public bool IsEmpty
		{
			get
			{
				return this.Length == 0;
			}
		}

		// Token: 0x17000021 RID: 33
		public PropertyPathPart this[int index]
		{
			get
			{
				PropertyPathPart propertyPathPart;
				switch (index)
				{
				case 0:
				{
					bool flag = this.Length < 1;
					if (flag)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part0;
					break;
				}
				case 1:
				{
					bool flag2 = this.Length < 2;
					if (flag2)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part1;
					break;
				}
				case 2:
				{
					bool flag3 = this.Length < 3;
					if (flag3)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part2;
					break;
				}
				case 3:
				{
					bool flag4 = this.Length < 4;
					if (flag4)
					{
						throw new IndexOutOfRangeException();
					}
					propertyPathPart = this.m_Part3;
					break;
				}
				default:
					propertyPathPart = this.m_AdditionalParts[index - 4];
					break;
				}
				return propertyPathPart;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000394C File Offset: 0x00001B4C
		public PropertyPath(string path)
		{
			PropertyPath propertyPath = PropertyPath.ConstructFromPath(path);
			this.m_Part0 = propertyPath.m_Part0;
			this.m_Part1 = propertyPath.m_Part1;
			this.m_Part2 = propertyPath.m_Part2;
			this.m_Part3 = propertyPath.m_Part3;
			this.m_AdditionalParts = propertyPath.m_AdditionalParts;
			this.Length = propertyPath.Length;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000039AC File Offset: 0x00001BAC
		private PropertyPath(in PropertyPathPart part)
		{
			this.m_Part0 = part;
			this.m_Part1 = default(PropertyPathPart);
			this.m_Part2 = default(PropertyPathPart);
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = null;
			this.Length = 1;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000039F8 File Offset: 0x00001BF8
		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1)
		{
			this.m_Part0 = part0;
			this.m_Part1 = part1;
			this.m_Part2 = default(PropertyPathPart);
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = null;
			this.Length = 2;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003A44 File Offset: 0x00001C44
		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1, in PropertyPathPart part2)
		{
			this.m_Part0 = part0;
			this.m_Part1 = part1;
			this.m_Part2 = part2;
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = null;
			this.Length = 3;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A90 File Offset: 0x00001C90
		private PropertyPath(in PropertyPathPart part0, in PropertyPathPart part1, in PropertyPathPart part2, in PropertyPathPart part3)
		{
			this.m_Part0 = part0;
			this.m_Part1 = part1;
			this.m_Part2 = part2;
			this.m_Part3 = part3;
			this.m_AdditionalParts = null;
			this.Length = 4;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003AE0 File Offset: 0x00001CE0
		internal PropertyPath(List<PropertyPathPart> parts)
		{
			this.m_Part0 = default(PropertyPathPart);
			this.m_Part1 = default(PropertyPathPart);
			this.m_Part2 = default(PropertyPathPart);
			this.m_Part3 = default(PropertyPathPart);
			this.m_AdditionalParts = ((parts.Count > 4) ? new PropertyPathPart[parts.Count - 4] : null);
			for (int i = 0; i < parts.Count; i++)
			{
				switch (i)
				{
				case 0:
					this.m_Part0 = parts[i];
					break;
				case 1:
					this.m_Part1 = parts[i];
					break;
				case 2:
					this.m_Part2 = parts[i];
					break;
				case 3:
					this.m_Part3 = parts[i];
					break;
				default:
					this.m_AdditionalParts[i - 4] = parts[i];
					break;
				}
			}
			this.Length = parts.Count;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003BD2 File Offset: 0x00001DD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromPart(in PropertyPathPart part)
		{
			return new PropertyPath(in part);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003BDC File Offset: 0x00001DDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromName(string name)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(name);
			return new PropertyPath(in propertyPathPart);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003BF8 File Offset: 0x00001DF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromIndex(int index)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(index);
			return new PropertyPath(in propertyPathPart);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003C14 File Offset: 0x00001E14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath FromKey(object key)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(key);
			return new PropertyPath(in propertyPathPart);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003C30 File Offset: 0x00001E30
		public static PropertyPath Combine(in PropertyPath path, in PropertyPath pathToAppend)
		{
			bool isEmpty = path.IsEmpty;
			PropertyPath propertyPath;
			if (isEmpty)
			{
				propertyPath = pathToAppend;
			}
			else
			{
				bool isEmpty2 = pathToAppend.IsEmpty;
				if (isEmpty2)
				{
					propertyPath = path;
				}
				else
				{
					int length = path.Length;
					int length2 = pathToAppend.Length;
					int num = length + length2;
					bool flag = num <= 4;
					if (flag)
					{
						int num2 = 0;
						PropertyPathPart part = path.m_Part0;
						PropertyPathPart propertyPathPart = ((length > 1) ? path.m_Part1 : pathToAppend[num2++]);
						PropertyPathPart propertyPathPart2 = ((num > 2) ? ((length > 2) ? path.m_Part2 : pathToAppend[num2++]) : default(PropertyPathPart));
						PropertyPathPart propertyPathPart3 = ((num > 3) ? ((length > 3) ? path.m_Part3 : pathToAppend[num2]) : default(PropertyPathPart));
						switch (num)
						{
						case 2:
							return new PropertyPath(in part, in propertyPathPart);
						case 3:
							return new PropertyPath(in part, in propertyPathPart, in propertyPathPart2);
						case 4:
							return new PropertyPath(in part, in propertyPathPart, in propertyPathPart2, in propertyPathPart3);
						}
					}
					List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
					try
					{
						PropertyPath.GetParts(in path, list);
						PropertyPath.GetParts(in pathToAppend, list);
						propertyPath = new PropertyPath(list);
					}
					finally
					{
						CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
					}
				}
			}
			return propertyPath;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003D94 File Offset: 0x00001F94
		public static PropertyPath Combine(in PropertyPath path, string pathToAppend)
		{
			bool flag = string.IsNullOrEmpty(pathToAppend);
			PropertyPath propertyPath;
			if (flag)
			{
				propertyPath = path;
			}
			else
			{
				PropertyPath propertyPath2 = new PropertyPath(pathToAppend);
				propertyPath = PropertyPath.Combine(in path, in propertyPath2);
			}
			return propertyPath;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003DCC File Offset: 0x00001FCC
		public static PropertyPath AppendPart(in PropertyPath path, in PropertyPathPart part)
		{
			bool isEmpty = path.IsEmpty;
			PropertyPath propertyPath;
			if (isEmpty)
			{
				propertyPath = new PropertyPath(in part);
			}
			else
			{
				switch (path.Length + 1)
				{
				case 2:
					propertyPath = new PropertyPath(in path.m_Part0, in part);
					break;
				case 3:
					propertyPath = new PropertyPath(in path.m_Part0, in path.m_Part1, in part);
					break;
				case 4:
					propertyPath = new PropertyPath(in path.m_Part0, in path.m_Part1, in path.m_Part2, in part);
					break;
				default:
				{
					List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
					try
					{
						PropertyPath.GetParts(in path, list);
						list.Add(part);
						propertyPath = new PropertyPath(list);
					}
					finally
					{
						CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
					}
					break;
				}
				}
			}
			return propertyPath;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003E94 File Offset: 0x00002094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendName(in PropertyPath path, string name)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(name);
			return PropertyPath.AppendPart(in path, in propertyPathPart);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EB0 File Offset: 0x000020B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendIndex(in PropertyPath path, int index)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(index);
			return PropertyPath.AppendPart(in path, in propertyPathPart);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003ECC File Offset: 0x000020CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath AppendKey(in PropertyPath path, object key)
		{
			PropertyPathPart propertyPathPart = new PropertyPathPart(key);
			return PropertyPath.AppendPart(in path, in propertyPathPart);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003EE8 File Offset: 0x000020E8
		public static PropertyPath AppendProperty(in PropertyPath path, IProperty property)
		{
			if (!true)
			{
			}
			IListElementProperty listElementProperty = property as IListElementProperty;
			PropertyPath propertyPath;
			if (listElementProperty == null)
			{
				ISetElementProperty setElementProperty = property as ISetElementProperty;
				if (setElementProperty == null)
				{
					IDictionaryElementProperty dictionaryElementProperty = property as IDictionaryElementProperty;
					if (dictionaryElementProperty == null)
					{
						PropertyPathPart propertyPathPart = new PropertyPathPart(property.Name);
						propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
					}
					else
					{
						PropertyPathPart propertyPathPart = new PropertyPathPart(dictionaryElementProperty.ObjectKey);
						propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
					}
				}
				else
				{
					PropertyPathPart propertyPathPart = new PropertyPathPart(setElementProperty.ObjectKey);
					propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
				}
			}
			else
			{
				PropertyPathPart propertyPathPart = new PropertyPathPart(listElementProperty.Index);
				propertyPath = PropertyPath.AppendPart(in path, in propertyPathPart);
			}
			if (!true)
			{
			}
			return propertyPath;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003F8B File Offset: 0x0000218B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath Pop(in PropertyPath path)
		{
			return PropertyPath.SubPath(in path, 0, path.Length - 1);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F9C File Offset: 0x0000219C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static PropertyPath SubPath(in PropertyPath path, int startIndex)
		{
			return PropertyPath.SubPath(in path, startIndex, path.Length - startIndex);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FB0 File Offset: 0x000021B0
		public static PropertyPath SubPath(in PropertyPath path, int startIndex, int length)
		{
			int length2 = path.Length;
			bool flag = startIndex < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			bool flag2 = startIndex > length2;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			bool flag3 = length < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			bool flag4 = startIndex > length2 - length;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			bool flag5 = length == 0;
			PropertyPath propertyPath;
			if (flag5)
			{
				propertyPath = default(PropertyPath);
			}
			else
			{
				bool flag6 = startIndex == 0 && length == length2;
				if (flag6)
				{
					propertyPath = path;
				}
				else
				{
					switch (length)
					{
					case 1:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						propertyPath = new PropertyPath(in propertyPathPart);
						break;
					}
					case 2:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						PropertyPathPart propertyPathPart2 = path[startIndex + 1];
						propertyPath = new PropertyPath(in propertyPathPart, in propertyPathPart2);
						break;
					}
					case 3:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						PropertyPathPart propertyPathPart2 = path[startIndex + 1];
						PropertyPathPart propertyPathPart3 = path[startIndex + 2];
						propertyPath = new PropertyPath(in propertyPathPart, in propertyPathPart2, in propertyPathPart3);
						break;
					}
					case 4:
					{
						PropertyPathPart propertyPathPart = path[startIndex];
						PropertyPathPart propertyPathPart2 = path[startIndex + 1];
						PropertyPathPart propertyPathPart3 = path[startIndex + 2];
						PropertyPathPart propertyPathPart4 = path[startIndex + 3];
						propertyPath = new PropertyPath(in propertyPathPart, in propertyPathPart2, in propertyPathPart3, in propertyPathPart4);
						break;
					}
					default:
					{
						List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
						try
						{
							for (int i = startIndex; i < startIndex + length; i++)
							{
								list.Add(path[i]);
							}
							propertyPath = new PropertyPath(list);
						}
						finally
						{
							CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
						}
						break;
					}
					}
				}
			}
			return propertyPath;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000416C File Offset: 0x0000236C
		public override string ToString()
		{
			bool flag = this.Length == 0;
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				bool flag2 = this.Length == 1 && this.m_Part0.IsName;
				if (flag2)
				{
					text = this.m_Part0.Name;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder(32);
					bool flag3 = this.Length > 0;
					if (flag3)
					{
						PropertyPath.AppendToBuilder(in this.m_Part0, stringBuilder);
					}
					bool flag4 = this.Length > 1;
					if (flag4)
					{
						PropertyPath.AppendToBuilder(in this.m_Part1, stringBuilder);
					}
					bool flag5 = this.Length > 2;
					if (flag5)
					{
						PropertyPath.AppendToBuilder(in this.m_Part2, stringBuilder);
					}
					bool flag6 = this.Length > 3;
					if (flag6)
					{
						PropertyPath.AppendToBuilder(in this.m_Part3, stringBuilder);
					}
					bool flag7 = this.Length > 4;
					if (flag7)
					{
						foreach (PropertyPathPart propertyPathPart in this.m_AdditionalParts)
						{
							PropertyPath.AppendToBuilder(in propertyPathPart, stringBuilder);
						}
					}
					text = stringBuilder.ToString();
				}
			}
			return text;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004284 File Offset: 0x00002484
		private static void AppendToBuilder(in PropertyPathPart part, StringBuilder builder)
		{
			PropertyPathPartKind kind = part.Kind;
			PropertyPathPartKind propertyPathPartKind = kind;
			if (propertyPathPartKind != PropertyPathPartKind.Name)
			{
				if (propertyPathPartKind - PropertyPathPartKind.Index > 1)
				{
					throw new ArgumentOutOfRangeException();
				}
				builder.Append(part.ToString());
			}
			else
			{
				bool flag = builder.Length > 0;
				if (flag)
				{
					builder.Append('.');
				}
				builder.Append(part.ToString());
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000042F0 File Offset: 0x000024F0
		private static void GetParts(in PropertyPath path, List<PropertyPathPart> parts)
		{
			int length = path.Length;
			for (int i = 0; i < length; i++)
			{
				parts.Add(path[i]);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004324 File Offset: 0x00002524
		private static PropertyPath ConstructFromPath(string path)
		{
			PropertyPath.<>c__DisplayClass36_0 CS$<>8__locals1;
			CS$<>8__locals1.path = path;
			bool flag = string.IsNullOrWhiteSpace(CS$<>8__locals1.path);
			PropertyPath propertyPath;
			if (flag)
			{
				propertyPath = default(PropertyPath);
			}
			else
			{
				CS$<>8__locals1.index = 0;
				CS$<>8__locals1.length = CS$<>8__locals1.path.Length;
				CS$<>8__locals1.state = 0;
				List<PropertyPathPart> list = CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Get();
				try
				{
					list.Clear();
					while (CS$<>8__locals1.index < CS$<>8__locals1.length)
					{
						switch (CS$<>8__locals1.state)
						{
						case 0:
						{
							PropertyPath.<ConstructFromPath>g__TrimStart|36_0(ref CS$<>8__locals1);
							bool flag2 = CS$<>8__locals1.index == CS$<>8__locals1.length;
							if (!flag2)
							{
								bool flag3 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '.';
								if (flag3)
								{
									throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
								}
								bool flag4 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '[';
								if (flag4)
								{
									CS$<>8__locals1.state = 2;
								}
								else
								{
									bool flag5 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '"';
									if (flag5)
									{
										throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
									}
									CS$<>8__locals1.state = 1;
								}
							}
							break;
						}
						case 1:
						{
							int index = CS$<>8__locals1.index;
							while (CS$<>8__locals1.index < CS$<>8__locals1.length)
							{
								bool flag6 = CS$<>8__locals1.path[CS$<>8__locals1.index] == '.' || CS$<>8__locals1.path[CS$<>8__locals1.index] == '[';
								if (flag6)
								{
									break;
								}
								int num = CS$<>8__locals1.index + 1;
								CS$<>8__locals1.index = num;
							}
							bool flag7 = index == CS$<>8__locals1.index;
							if (flag7)
							{
								throw new ArgumentException("Invalid PropertyPath: Name is empty.");
							}
							bool flag8 = CS$<>8__locals1.index == CS$<>8__locals1.length;
							if (flag8)
							{
								list.Add(new PropertyPathPart(CS$<>8__locals1.path.Substring(index)));
								CS$<>8__locals1.state = 0;
							}
							else
							{
								list.Add(new PropertyPathPart(CS$<>8__locals1.path.Substring(index, CS$<>8__locals1.index - index)));
								PropertyPath.<ConstructFromPath>g__ReadNext|36_1(ref CS$<>8__locals1);
							}
							break;
						}
						case 2:
						{
							bool flag9 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '[';
							if (flag9)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							bool flag10 = CS$<>8__locals1.index + 1 < CS$<>8__locals1.length && CS$<>8__locals1.path[CS$<>8__locals1.index + 1] == '"';
							if (flag10)
							{
								CS$<>8__locals1.state = 4;
							}
							else
							{
								CS$<>8__locals1.state = 3;
							}
							break;
						}
						case 3:
						{
							bool flag11 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '[';
							if (flag11)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							int num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							int index2 = CS$<>8__locals1.index;
							while (CS$<>8__locals1.index < CS$<>8__locals1.length)
							{
								char c = CS$<>8__locals1.path[CS$<>8__locals1.index];
								bool flag12 = c == ']';
								if (flag12)
								{
									break;
								}
								num = CS$<>8__locals1.index + 1;
								CS$<>8__locals1.index = num;
							}
							bool flag13 = CS$<>8__locals1.path[CS$<>8__locals1.index] != ']';
							if (flag13)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							string text = CS$<>8__locals1.path.Substring(index2, CS$<>8__locals1.index - index2);
							int num2;
							bool flag14 = !int.TryParse(text, out num2);
							if (flag14)
							{
								throw new ArgumentException("Indices in PropertyPath must be a numeric value.");
							}
							bool flag15 = num2 < 0;
							if (flag15)
							{
								throw new ArgumentException("Invalid PropertyPath: Negative indices are not supported.");
							}
							list.Add(new PropertyPathPart(num2));
							num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							bool flag16 = CS$<>8__locals1.index == CS$<>8__locals1.length;
							if (!flag16)
							{
								PropertyPath.<ConstructFromPath>g__ReadNext|36_1(ref CS$<>8__locals1);
							}
							break;
						}
						case 4:
						{
							bool flag17 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '[';
							if (flag17)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							int num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							bool flag18 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '"';
							if (flag18)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							num = CS$<>8__locals1.index + 1;
							CS$<>8__locals1.index = num;
							int index3 = CS$<>8__locals1.index;
							while (CS$<>8__locals1.index < CS$<>8__locals1.length)
							{
								char c2 = CS$<>8__locals1.path[CS$<>8__locals1.index];
								bool flag19 = c2 == '"';
								if (flag19)
								{
									break;
								}
								num = CS$<>8__locals1.index + 1;
								CS$<>8__locals1.index = num;
							}
							bool flag20 = CS$<>8__locals1.path[CS$<>8__locals1.index] != '"';
							if (flag20)
							{
								throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", CS$<>8__locals1.path[CS$<>8__locals1.index], CS$<>8__locals1.index));
							}
							bool flag21 = CS$<>8__locals1.index + 1 < CS$<>8__locals1.length && CS$<>8__locals1.path[CS$<>8__locals1.index + 1] == ']';
							if (!flag21)
							{
								throw new ArgumentException("Invalid PropertyPath: No matching end quote for key.");
							}
							string text2 = CS$<>8__locals1.path.Substring(index3, CS$<>8__locals1.index - index3);
							list.Add(new PropertyPathPart(text2));
							CS$<>8__locals1.index += 2;
							PropertyPath.<ConstructFromPath>g__ReadNext|36_1(ref CS$<>8__locals1);
							break;
						}
						}
					}
					propertyPath = new PropertyPath(list);
				}
				finally
				{
					CollectionPool<List<PropertyPathPart>, PropertyPathPart>.Release(list);
				}
			}
			return propertyPath;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004A24 File Offset: 0x00002C24
		public static bool operator ==(PropertyPath lhs, PropertyPath rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004A40 File Offset: 0x00002C40
		public static bool operator !=(PropertyPath lhs, PropertyPath rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004A5C File Offset: 0x00002C5C
		public bool Equals(PropertyPath other)
		{
			bool flag = this.Length != other.Length;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.Length; i++)
				{
					bool flag3 = !this[i].Equals(other[i]);
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public override bool Equals(object obj)
		{
			PropertyPath propertyPath;
			bool flag;
			if (obj is PropertyPath)
			{
				propertyPath = (PropertyPath)obj;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(propertyPath);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004AFC File Offset: 0x00002CFC
		public override int GetHashCode()
		{
			int num = 19;
			int length = this.Length;
			bool flag = length == 0;
			int num2;
			if (flag)
			{
				num2 = num;
			}
			else
			{
				bool flag2 = length > 0;
				if (flag2)
				{
					num = num * 31 + this.m_Part0.GetHashCode();
				}
				bool flag3 = length > 1;
				if (flag3)
				{
					num = num * 31 + this.m_Part1.GetHashCode();
				}
				bool flag4 = length > 2;
				if (flag4)
				{
					num = num * 31 + this.m_Part2.GetHashCode();
				}
				bool flag5 = length > 3;
				if (flag5)
				{
					num = num * 31 + this.m_Part3.GetHashCode();
				}
				bool flag6 = length <= 4;
				if (flag6)
				{
					num2 = num;
				}
				else
				{
					foreach (PropertyPathPart propertyPathPart in this.m_AdditionalParts)
					{
						num = num * 31 + propertyPathPart.GetHashCode();
					}
					num2 = num;
				}
			}
			return num2;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004BFC File Offset: 0x00002DFC
		[CompilerGenerated]
		internal static void <ConstructFromPath>g__TrimStart|36_0(ref PropertyPath.<>c__DisplayClass36_0 A_0)
		{
			while (A_0.index < A_0.length && A_0.path[A_0.index] == ' ')
			{
				int num = A_0.index + 1;
				A_0.index = num;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004C48 File Offset: 0x00002E48
		[CompilerGenerated]
		internal static void <ConstructFromPath>g__ReadNext|36_1(ref PropertyPath.<>c__DisplayClass36_0 A_0)
		{
			bool flag = A_0.index == A_0.length;
			if (flag)
			{
				A_0.state = 0;
			}
			else
			{
				char c = A_0.path[A_0.index];
				char c2 = c;
				if (c2 != '.')
				{
					if (c2 != '[')
					{
						throw new ArgumentException(string.Format("{0}: Invalid '{1}' character encountered at index '{2}'.", "PropertyPath", A_0.path[A_0.index], A_0.index));
					}
					A_0.state = 2;
				}
				else
				{
					int num = A_0.index + 1;
					A_0.index = num;
					A_0.state = 0;
				}
			}
		}

		// Token: 0x04000042 RID: 66
		internal const int k_InlineCount = 4;

		// Token: 0x04000043 RID: 67
		private readonly PropertyPathPart m_Part0;

		// Token: 0x04000044 RID: 68
		private readonly PropertyPathPart m_Part1;

		// Token: 0x04000045 RID: 69
		private readonly PropertyPathPart m_Part2;

		// Token: 0x04000046 RID: 70
		private readonly PropertyPathPart m_Part3;

		// Token: 0x04000047 RID: 71
		private readonly PropertyPathPart[] m_AdditionalParts;
	}
}
