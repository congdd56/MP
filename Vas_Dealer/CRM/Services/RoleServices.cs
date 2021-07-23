using System;
using System.Collections.Generic;
using System.Linq;
using VAS.Dealer.Models.CRM;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly MP_Context _Context;
        public RoleServices(MP_Context CrmContext)
        {
            _Context = CrmContext;
        }
        /// <summary>
        /// Lấy danh sách quyền theo tên
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public object GetListRole(string Name)
        {
            var q = (from c in _Context.Role.Where(c => (c.Name.Contains(Name) || string.IsNullOrEmpty(Name)) && c.IsDeleted == false)
                     join p in _Context.AccountRole on c.Id equals p.RoleId into ps
                     from p in ps.DefaultIfEmpty()
                     select new
                     {
                         c.Id,
                         c.Name,
                         c.CreatedBy,
                         c.CreatedDate,
                         c.UpdatedBy,
                         c.UpdatedDate,
                         IdRole = p == null ? 0 : 1
                     }).ToList();

            var query = (from o in q
                         group o by new { o.Id, o.Name, o.CreatedBy, o.CreatedDate, o.UpdatedBy, o.UpdatedDate } into g
                         select new
                         {
                             g.Key.Id,
                             g.Key.Name,
                             g.Key.CreatedBy,
                             g.Key.CreatedDate,
                             g.Key.UpdatedBy,
                             g.Key.UpdatedDate,
                             CountAgent = g.Sum(d => (Int32?)d.IdRole)
                         }).ToList();

            return query;
        }
        /// <summary>
        /// Thêm mới quyền
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public object AddRole(RoleModel obj, string userLogin)
        {
            if (_Context.Role.Any(x => x.Name == obj.Name))
                return new { status = "err-exit" };

            MP_Role Role = new MP_Role
            {
                Name = obj.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = userLogin,
                IsDeleted = false
            };
            _Context.Role.Add(Role);
            _Context.SaveChanges();
            if (!string.IsNullOrEmpty(obj.Permission))
            {
                string[] s = obj.Permission.Split(",");
                for (int i = 0; i < s.Length; i++)
                {
                    if (!string.IsNullOrEmpty(s[i].ToString()))
                    {
                        MP_Role_Permission item = new MP_Role_Permission
                        {
                            IdRole = Role.Id,
                            IdPermission = Convert.ToInt32(s[i].ToString())
                        };
                        _Context.RolePermission.Add(item);
                    }
                }
            }
            _Context.SaveChanges();
            return new { status = "ok" };
        }
        /// <summary>
        /// Cập nhật quyền
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public object UpdateRole(RoleModel obj, string userLogin)
        {
            MP_Role Role = _Context.Role.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (Role == null) return new { status = "not-found" };
            if (_Context.Role.Any(x => x.Name == obj.Name && x.Id != obj.Id)) return new { status = "err-exit" };
            Role.Name = obj.Name;
            Role.UpdatedDate = DateTime.Now;
            Role.UpdatedBy = userLogin;

            string[] sManager = { };
            if (!string.IsNullOrEmpty(obj.Manager)) sManager = obj.Manager.Split(",");
            if (Role.AccountRole != null)
            {
                Role.AccountRole.ToList().ForEach(x =>
                {
                    if (sManager.Contains(x.AccId.ToString()))
                        x.Manager = true;
                    else x.Manager = false;
                });
            }

            List<MP_Role_Permission> lstChild = (from p in _Context.RolePermission
                                                 where p.IdRole == Role.Id
                                                 select p).ToList();
            foreach (MP_Role_Permission p in lstChild)
            {
                _Context.RolePermission.Remove(p);
            }

            if (!string.IsNullOrEmpty(obj.Permission))
            {

                string[] s = obj.Permission.Split(",");
                for (int i = 0; i < s.Length; i++)
                {
                    if (!string.IsNullOrEmpty(s[i].ToString()))
                    {
                        MP_Role_Permission item = new MP_Role_Permission
                        {
                            IdRole = Role.Id,
                            IdPermission = Convert.ToInt32(s[i].ToString()),
                        };
                        Role.RolePermission.Add(item);
                    }
                }
            }
            _Context.SaveChanges();
            return new { status = "ok" };
        }

        /// <summary>
        /// Xóa quyền
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        public object DeleteRole(int Id, string userLogin)
        {
            MP_Role Role = _Context.Role.Where(x => x.Id == Id).FirstOrDefault();
            if (Role == null) return new { status = "not-found" };
            Role.UpdatedDate = DateTime.Now;
            Role.UpdatedBy = userLogin;
            Role.IsDeleted = true;
            _Context.SaveChanges();
            return new { status = "ok" };
        }

        public object GetInfoRoleById(int Id)
        {
            var item = _Context.Role.Where(x => x.Id == Id).FirstOrDefault();
            var result = (from a in _Context.Role
                          join b in _Context.RolePermission on a.Id equals b.IdRole
                          where a.IsDeleted == false && a.Id == Id
                          select new RoleInfoModel()
                          {
                              Name = a.Name,
                              IdPer = b.IdPermission
                          }).ToList();
            return new
            {
                value1 = result,
                value2 = item.AccountRole.Where(x => x.Account != null).Select(s => new Select2Model()
                {
                    id = s.Account.Id.ToString(),
                    text = s.Account.UserName,
                    selected = s.Manager
                }).ToList()
            };
        }

        public object GetInfoUserByRoleId(int Id)
        {
            var userByRole = (from a in _Context.Account.Where(a => a.IsDeleted == false)
                              join b in _Context.AccountRole on a.Id equals b.AccId
                              where b.RoleId == Id
                              select new { Name = a.UserName, a.FullName, a.Id, a.Email, b.Manager }).ToList();
            var lstUser = (from a in _Context.Account.Where(a => a.IsDeleted == false)
                           select new { Name = a.UserName, a.FullName, a.Id, a.Email }).ToList();
            return new { lstUser = lstUser, userByRole = userByRole };
        }

        public object GetAllRole()
        {
            var result = (from a in _Context.Role.Where(x => !x.IsDeleted)
                          select new { a.Id, a.Name }).ToList();
            return result;
        }
    }
}
