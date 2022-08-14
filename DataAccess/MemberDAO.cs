using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private FStoreDBContext _context = new FStoreDBContext();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }


        public Member CheckLogin(string email, string password)
        {
            try
            {
                
                Member mem = _context.Members.SingleOrDefault(m => m.Email.Equals(email) && m.Password.Equals(password));
                if (mem != null)
                {
                    return mem;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public IEnumerable<Member> GetMemberList()
        {
            var members = new List<Member>();
            try
            {
                using var context = new FStoreDBContext();
                members = context.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        public Member GetMemberByID(int memberID)
        {
            Member member = null;
            try
            {
                using var context = new FStoreDBContext();
                member = context.Members.SingleOrDefault(m => m.MemberId == memberID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }
        public void Add(Member member)
        {
            if (member == null)
            {
                throw new Exception("Member is underfined");
            }
            try
            {
                if (GetMemberByID(member.MemberId) == null)
                {
                    var context = new FStoreDBContext();
                    context.Members.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member is existed!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member != null)
                {
                    using var context = new FStoreDBContext();
                    context.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Member has not existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int memberID)
        {
            try
            {
                Member member = GetMemberByID(memberID);
                if (member != null)
                {
                    using var context = new FStoreDBContext();
                    context.Members.Remove(member);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
