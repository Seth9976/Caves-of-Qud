using System;
using System.IO;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000132 RID: 306
	public abstract class PathConstraint : StringConstraint
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x000166FD File Offset: 0x000148FD
		protected PathConstraint(string expected)
			: base(expected)
		{
			this.expected = expected;
			this.caseInsensitive = Path.DirectorySeparatorChar == '\\';
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x0001671C File Offset: 0x0001491C
		public PathConstraint RespectCase
		{
			get
			{
				this.caseInsensitive = false;
				return this;
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00016726 File Offset: 0x00014926
		protected override string GetStringRepresentation()
		{
			return string.Format("<{0} \"{1}\" {2}>", this.DisplayName.ToLower(), this.expected, this.caseInsensitive ? "ignorecase" : "respectcase");
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00016758 File Offset: 0x00014958
		protected string Canonicalize(string path)
		{
			if (Path.DirectorySeparatorChar != Path.AltDirectorySeparatorChar)
			{
				path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			}
			string text = "";
			foreach (char c in path)
			{
				if (c != '\\' && c != '/')
				{
					break;
				}
				text += Path.DirectorySeparatorChar.ToString();
			}
			string[] array = path.Split(PathConstraint.DirectorySeparatorChars, StringSplitOptions.RemoveEmptyEntries);
			int num = 0;
			bool flag = false;
			foreach (string text3 in array)
			{
				if (!(text3 == ".") && (text3 == null || text3.Length != 0))
				{
					if (!(text3 == ".."))
					{
						if (flag)
						{
							array[num] = text3;
						}
						num++;
					}
					else
					{
						flag = true;
						if (num > 0)
						{
							num--;
						}
					}
				}
				else
				{
					flag = true;
				}
			}
			return text + string.Join(Path.DirectorySeparatorChar.ToString(), array, 0, num);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00016860 File Offset: 0x00014A60
		protected bool IsSubPath(string path1, string path2)
		{
			int length = path1.Length;
			int length2 = path2.Length;
			return length < length2 && StringUtil.StringsEqual(path1, path2.Substring(0, length), this.caseInsensitive) && (path2[length - 1] == Path.DirectorySeparatorChar || path2[length] == Path.DirectorySeparatorChar);
		}

		// Token: 0x040001E0 RID: 480
		private const char WindowsDirectorySeparatorChar = '\\';

		// Token: 0x040001E1 RID: 481
		private const char NonWindowsDirectorySeparatorChar = '/';

		// Token: 0x040001E2 RID: 482
		private static readonly char[] DirectorySeparatorChars = new char[] { '\\', '/' };
	}
}
