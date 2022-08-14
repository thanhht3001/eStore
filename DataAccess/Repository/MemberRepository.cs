using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member Login(string username, string password)
        {
            return MemberDAO.Instance.CheckLogin(username, password);
        }

        public void DeleteMember(int memberID)
        {
            MemberDAO.Instance.Delete(memberID);
        }

        public Member GetMemberByID(int memberID)
        {
            return MemberDAO.Instance.GetMemberByID(memberID);
        }

        public IEnumerable<Member> GetMembers()
        {
            return MemberDAO.Instance.GetMemberList();
        }

        public void UpdateMember(Member member)
        {
            MemberDAO.Instance.Update(member);
        }

        public void AddMember(Member member)
        {
            MemberDAO.Instance.Add(member);
        }
    }
}
