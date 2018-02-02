using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Parser.Html;

namespace WebTest {
	class Program {
		static void Main(string[] args) {
			while (true) {
				ToStart:
				Console.Write("Введите команду: ");
				string cmd = Console.ReadLine();
				switch (cmd) {
					case "узнать":
						Console.Write("Введите слово в словарной форме: ");
						string input = Console.ReadLine();
						string reference = "https://www.online-latin-dictionary.com/latin-english-dictionary.php?parola=" + input;
						var parser = new HtmlParser();
						var document = parser.Parse(GetHtml(reference)); //
						var ddd = from el in document.All
								  where el.LocalName == "span" && el.ClassName == "lemma"
								  select el.TextContent;
						var values = from v in document.All
									 where v.LocalName == "span" && v.ClassName == "english"
									 select v.TextContent;
						try {
							Console.WriteLine(ddd.ElementAt(0));
						} catch (ArgumentOutOfRangeException ex) {
							Console.WriteLine("Неверно введено слово");
							goto ToStart;
						}
						for (int i = 1; i <= values.Count(); i++) {
							Console.WriteLine(i + ". " + values.ElementAt(i-1));
						}
						break;
					case "выйти":
						goto Exit;
					default:
						Console.WriteLine("Вы ввели неверную команду");
						goto ToStart;
				}
			}
			Exit:;
		}

		public static string GetPeace(string text, string first, string second) {
			if (first.Length == 0 || second.Length == 0 || text.Length == 0)
				return null;
			text = text.Replace(text.Substring(0, text.IndexOf(first) - 1), "");
			text = text.Replace(text.Substring(text.IndexOf(second)), "");
			return text;
		}
		public static string GetHtml(string reference) {
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(reference);
			HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
			using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8)) {
				return reader.ReadToEnd();//
			}
		}
	}
}
