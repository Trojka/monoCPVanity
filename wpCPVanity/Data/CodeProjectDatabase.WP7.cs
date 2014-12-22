using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using be.trojkasoftware.portableCPVanity;
using be.trojkasoftware.monoCPVanity.Util;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Community.CsharpSqlite.SQLiteClient;
using System.Data.Common;


namespace be.trojkasoftware.monoCPVanity.Data
{
    public class CodeProjectDatabase
    {
        string dbPath;

        public CodeProjectDatabase()
        {
            dbPath = "items.db3";

            IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication();
            bool exists = isf.FileExists(dbPath);

            var connection = new SqliteConnection("data source=" + dbPath);
            connection.Open();
                
            if (!exists)
            {
                var commands = new[]{
					"CREATE TABLE [Member] (Key integer, Name ntext, ArticleCnt integer, BlogCnt integer, Reputation ntext, IsMe integer);"
				};
                foreach (var command in commands)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        c.ExecuteNonQuery();
                    }
                }
            }

        }

		public bool AddMember(CodeProjectMember member)
		{
            var connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [Key] FROM [Member] WHERE [Key]=" + member.Id;
                var r = command.ExecuteReader();
                if (r.HasRows)
                {
                    using (var uc = connection.CreateCommand())
                    {
                        uc.CommandText = "UPDATE [Member] SET " +
                            " [Name] = '" + member.Name + "'," +
                            " [ArticleCnt] = '" + member.ArticleCount + "'," +
                            " [BlogCnt] = '" + member.BlogCount + "'," +
                            " [Reputation] = '" + member.Reputation + "'," +
                            " WHERE [Key]=" + member.Id;
                        uc.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var ic = connection.CreateCommand())
                    {
                        ic.CommandText = "INSERT INTO [Member] ([Key], [Name], [ArticleCnt], [BlogCnt], [Reputation])"
                            + " VALUES(" + member.Id + ", '" + member.Name + "', '" + member.ArticleCount + "', '" + member.BlogCount + "', '" + member.Reputation + "')";
                        ic.ExecuteNonQuery();
                    }
                }
            }

            connection.Close();

            FileStorageService storage = new FileStorageService();
            if (member.Avatar != null)
            {
                storage.WriteBytes(member.Avatar, member.Id.ToString());
            }

            return true;
        }

        public void DeleteMember(int memberId)
        {
            var connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();
            using (var ic = connection.CreateCommand())
            {
                ic.CommandText = "DELETE FROM [Member] " +
                                " WHERE [Key]=" + memberId;
                ic.ExecuteNonQuery();
            }

            connection.Close();

            FileStorageService storage = new FileStorageService();
            storage.DeleteFile(memberId.ToString());
        }

		public CodeProjectMember GetMember(int memberId)
		{
            var connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();

            CodeProjectMember member = new CodeProjectMember();
            member.Id = memberId;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [Key], [Name], [ArticleCnt], [BlogCnt], [Reputation], [IsMe] FROM [Member] WHERE [Key]=" + memberId;
                var r = command.ExecuteReader();
                while (r.Read())
                {
                    FillMemberFromDataReader(member, r);
                }
            }

            connection.Close();

            return member;
        }

		public byte[] GetGravatar(int memberId)
		{
            FileStorageService storage = new FileStorageService();
            if (storage.FileExists(memberId.ToString()))
            {
                return storage.ReadBytes(memberId.ToString());
            }
            else
            {
                return null;
            }
        }

        public List<CodeProjectMember> GetMembers()
		{
            List<CodeProjectMember> memberList = new List<CodeProjectMember>();

            var connection = new SqliteConnection("Data Source=" + dbPath);
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [Key], [Name], [ArticleCnt], [BlogCnt], [Reputation], [IsMe] FROM [Member]";
                var r = command.ExecuteReader();
                while (r.Read())
                {
                    CodeProjectMember member = new CodeProjectMember();
                    FillMemberFromDataReader(member, r);
                    memberList.Add(member);
                }
            }

            connection.Close();

            return memberList;
        }

        private void FillMemberFromDataReader(CodeProjectMember member, DbDataReader r)
        {
            object idAsObject = r["Key"];
            member.Id = int.Parse(idAsObject.ToString());
            member.Name = r["Name"].ToString();
            member.ArticleCount = int.Parse(r["ArticleCnt"].ToString());
            member.BlogCount = int.Parse(r["BlogCnt"].ToString());
            member.Reputation = r["Reputation"].ToString();
        }

    }
}
