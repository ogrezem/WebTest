using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace WebTest {
	class Program {
		static void Main(string[] args) {
			while (true) {
				string input;
				ToStart:
				Console.Write("Введите команду: ");
				string cmd = Console.ReadLine();
				switch (cmd) {
					case "узнать":
						Console.Write("Введите слово в словарной форме: ");
						input = Console.ReadLine();
						string reference = "https://www.online-latin-dictionary.com/latin-english-dictionary.php?parola=" + input;
						try {
							HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(reference);
							HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
							using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8)) {
								string zapr = reader.ReadToEnd();
								Console.WriteLine(GetPeace(zapr, "<div id=\"myth\">", "<a href=\"/latin-english-dictionary.php?"));
							}
						} catch (Exception ex) {
							Console.WriteLine("Скорее всего, было введено неверное слово");
							Console.WriteLine(ex.StackTrace);
							Console.WriteLine(ex.Message);
							continue;
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
