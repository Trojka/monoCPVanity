using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using be.trojkasoftware.portableCPVanity;

namespace be.trojkasoftware.monoCPVanity.Data
{
	public class CodeProjectDatabase
	{
		string dbPath;

		public CodeProjectDatabase ()
		{
			dbPath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"items.db3");
			bool exists = File.Exists (dbPath);
			if (!exists) {
				SqliteConnection.CreateFile (dbPath);
			}

			var connection = new SqliteConnection ("Data Source=" + dbPath);
			connection.Open ();

			if (!exists) {
				var commands = new[]{
					"CREATE TABLE [Member] (Key integer, Name ntext, ArticleCnt ntext, BlogCnt ntext, IsMe integer);"
				};
				foreach (var command in commands) {
					using (var c = connection.CreateCommand ()) {
						c.CommandText = command;
						c.ExecuteNonQuery ();
					}
				}
			}
		}

		public bool AddMember(CodeProjectMember member, bool isMe)
		{
			var connection = new SqliteConnection ("Data Source=" + dbPath);
			connection.Open ();

			if (isMe) {
				// set all to not me, the correct one will be set later
				using (var uc = connection.CreateCommand ()) {
					uc.CommandText = "UPDATE [Member] SET " +
						" [IsMe] = 0";
					uc.ExecuteNonQuery ();
				}
			}

			using (var command = connection.CreateCommand ()) {
				command.CommandText = "SELECT [Key] FROM [Member] WHERE [Key]=" + member.Id;
				var r = command.ExecuteReader ();
				if (r.HasRows) {
					using (var uc = connection.CreateCommand ()) {
						uc.CommandText = "UPDATE [Member] SET " +
							" [Name] = '" + member.Name + "'," +
							" [ArticleCnt] = '" + member.ArticleCount + "'," +
							" [BlogCnt] = '" + member.BlogCount + "'," +
							" [IsMe] = " + (isMe ? "1" : "0") + 
							" WHERE [Key]=" + member.Id;
						uc.ExecuteNonQuery ();
					}
				} else {
					using (var ic = connection.CreateCommand ()) {
						ic.CommandText = "INSERT INTO [Member] ([Key], [Name], [ArticleCnt], [BlogCnt], [IsMe])"
							+ " VALUES(" + member.Id + ", '" + member.Name + "', '" + member.ArticleCount + "', '" + member.BlogCount + "', " + (isMe ? "1" : "0") + ")";
						ic.ExecuteNonQuery ();
					}
				}
			}	

			connection.Close ();

			return true;
		}

		public CodeProjectMember GetMember(int memberId)
		{
			var connection = new SqliteConnection ("Data Source=" + dbPath);
			connection.Open ();

			CodeProjectMember member = new CodeProjectMember();
			member.Id = memberId;

			using (var command = connection.CreateCommand ()) {
				command.CommandText = "SELECT [Key], [Name], [ArticleCnt], [BlogCnt], [IsMe] FROM [Member] WHERE [Key]=" + memberId;
				var r = command.ExecuteReader ();
				while (r.Read ()) {
					member.Name = r ["Name"].ToString ();
					member.ArticleCount = r ["ArticleCnt"].ToString ();
					member.BlogCount = r ["BlogCnt"].ToString ();
					member.IsMe = (r ["IsMe"].ToString () == "1")?true:false;
				}
			}		

			connection.Close ();

			return member;
		}

		public List<CodeProjectMember> GetMembers()
		{
			List<CodeProjectMember> memberList = new List<CodeProjectMember> ();

			var connection = new SqliteConnection ("Data Source=" + dbPath);
			connection.Open ();

			using (var command = connection.CreateCommand ()) {
				command.CommandText = "SELECT [Key], [Name], [ArticleCnt], [BlogCnt], [IsMe] FROM [Member]";
				var r = command.ExecuteReader ();
				while (r.Read ()) {
					CodeProjectMember member = new CodeProjectMember ();
					object idAsObject = r ["Key"];
					member.Id = int.Parse(idAsObject.ToString());
					member.Name = r ["Name"].ToString ();
					member.ArticleCount = r ["ArticleCnt"].ToString ();
					member.BlogCount = r ["BlogCnt"].ToString ();
					member.IsMe = (r ["IsMe"].ToString () == "1")?true:false;

					memberList.Add (member);
				}
			}		

			connection.Close ();

			return memberList;
		}

	}
}

