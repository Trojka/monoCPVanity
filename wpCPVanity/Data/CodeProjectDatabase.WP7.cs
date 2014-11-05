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

namespace be.trojkasoftware.monoCPVanity.Data
{
    public class CodeProjectDatabase
    {
		public bool AddMember(CodeProjectMember member, bool isMe)
		{
            return true;
        }

        public void DeleteMember(int memberId)
        {
        }

		public CodeProjectMember GetMember(int memberId)
		{
            return null;
        }

		public byte[] GetGravatar(int memberId)
		{
            return null;
        }

        public List<CodeProjectMember> GetMembers()
		{
            return null;
        }

    }
}
