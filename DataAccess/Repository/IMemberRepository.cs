using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        public Member Login(string username, string password);
        IEnumerable<Member> GetMembers();
        Member GetMemberByID(int memberID);
        void AddMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(int memberID);
    }
}
