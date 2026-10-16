using System;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001A RID: 26
	public class LdapDN
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00007244 File Offset: 0x00005444
		[CLSCompliant(false)]
		public static bool equals(string dn1, string dn2)
		{
			DN dn3 = new DN(dn1);
			DN dn4 = new DN(dn2);
			return dn3.Equals(dn4);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007264 File Offset: 0x00005464
		public static string escapeRDN(string rdn)
		{
			StringBuilder stringBuilder = new StringBuilder(rdn);
			int i = 0;
			while (i < stringBuilder.Length && stringBuilder[i] != '=')
			{
				i++;
			}
			if (i == stringBuilder.Length)
			{
				throw new ArgumentException("Could not parse RDN: Attribute type and name must be separated by an equal symbol, '='");
			}
			i++;
			if (stringBuilder[i] == ' ' || stringBuilder[i] == '#')
			{
				stringBuilder.Insert(i++, '\\');
			}
			while (i < stringBuilder.Length)
			{
				if (stringBuilder[i] == ',' || stringBuilder[i] == '+' || stringBuilder[i] == '"' || stringBuilder[i] == '\\' || stringBuilder[i] == '<' || stringBuilder[i] == '>' || stringBuilder[i] == ';')
				{
					stringBuilder.Insert(i++, '\\');
				}
				i++;
			}
			if (stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				stringBuilder.Insert(stringBuilder.Length - 1, '\\');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007363 File Offset: 0x00005563
		public static string[] explodeDN(string dn, bool noTypes)
		{
			return new DN(dn).explodeDN(noTypes);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007371 File Offset: 0x00005571
		public static string[] explodeRDN(string rdn, bool noTypes)
		{
			return new RDN(rdn).explodeRDN(noTypes);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007380 File Offset: 0x00005580
		public static bool isValid(string dn)
		{
			try
			{
				new DN(dn);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000073B0 File Offset: 0x000055B0
		public static string normalize(string dn)
		{
			return new DN(dn).ToString();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000073C0 File Offset: 0x000055C0
		public static string unescapeRDN(string rdn)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < rdn.Length && rdn[i] != '=')
			{
				i++;
			}
			if (i == rdn.Length)
			{
				throw new ArgumentException("Could not parse rdn: Attribute type and name must be separated by an equal symbol, '='");
			}
			i++;
			if (rdn[i] == '\\' && i + 1 < rdn.Length - 1 && (rdn[i + 1] == ' ' || rdn[i + 1] == '#'))
			{
				i++;
			}
			while (i < rdn.Length)
			{
				if (rdn[i] != '\\' || i == rdn.Length - 1 || (rdn[i + 1] != ',' && rdn[i + 1] != '+' && rdn[i + 1] != '"' && rdn[i + 1] != '\\' && rdn[i + 1] != '<' && rdn[i + 1] != '>' && rdn[i + 1] != ';' && (rdn[i + 1] != ' ' || i + 2 != rdn.Length)))
				{
					stringBuilder.Append(rdn[i]);
				}
				i++;
			}
			return stringBuilder.ToString();
		}
	}
}
