using VAS.Dealer.Models.CC;
using VAS.Dealer.Models.Entities;
using VAS.Dealer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VAS.Dealer.Services
{
    public class PermissionServices : IPermissionServices
    {
        private readonly MP_Context _Context;
        public PermissionServices(MP_Context CrmContext)
        {
            _Context = CrmContext;
        }

        /// <summary>
        /// Lấy danh sách permission theo tên
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public List<MP_Permission> GetListPermission(string Name)
        {
            List<MP_Permission> result = (from s in _Context.Permission where (s.Name.Contains(Name) || string.IsNullOrEmpty(Name)) select s).ToList();
            return result;
        }

        public object GetAllPermission()
        {
            var result = (from a in _Context.Permission select new { a.Id, a.Name }).ToList();
            return result;
        }

        public object UpdateRole(RolePerModel obj, string userLogin)
        {
            MP_Role Role = _Context.Role.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (Role == null) return new { status = "not-found" };
            if (_Context.Role.Any(x => x.Name == obj.Name && x.Id != obj.Id)) return new { status = "err-exit" };
            Role.Name = obj.Name;
            Role.UpdatedDate = DateTime.Now;
            Role.UpdatedBy = userLogin;


            //Lấy danh sách Permission hiện tại trong nhóm quyền. và xóa hết đi
            List<MP_Role_Permission> lstrole = (from p in _Context.RolePermission
                                                where p.IdRole == Role.Id
                                                select p).ToList();
            foreach (MP_Role_Permission p in lstrole)
            {
                _Context.RolePermission.Remove(p);
            }
            //update Permission theo nhóm quyền
            if (!string.IsNullOrEmpty(obj.Permissions))
            {

                string[] s = obj.Permissions.Split(",");
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
            //update user theo nhóm quyền
            if (!string.IsNullOrEmpty(obj.Users))
            {
                string[] s = obj.Users.Split(",");
                for (int j = 0; j < s.Length; j++)
                {
                    if (!string.IsNullOrEmpty(s[j].ToString()))
                    {
                        var itemAcc = _Context.AccountRole.Where(x => x.RoleId == Role.Id && x.AccId == Convert.ToInt32(s[j].ToString())).FirstOrDefault();
                        if (itemAcc != null)
                        {
                            itemAcc.Manager = true;

                        }
                    }
                }
            }

            _Context.SaveChanges();

            return new { status = "ok" };
        }
        public List<GroupPermissionModel> GetGroupPermission()
        {
            return _Context.GroupPermission.Select(s => new GroupPermissionModel()
            {
                GroupName = s.NameGroup,
                Permissions = s.MP_Permission.Select(p => new PermissionModel()
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name
                }).ToList()
            }).ToList();
        }
        public object ListGroupPermissionByRole(int RoleId)
        {
            var lstrolePer = _Context.RolePermission.Where(x => x.IdRole == RoleId)
                .Select(s => s.Permission.Code).ToList();
            var lstGroupPer = _Context.GroupPermission.Select(s => new GroupPermissionRoleModel()
            {
                GroupName = s.NameGroup,
                Permissions = s.MP_Permission.Select(p => new PermissionModel()
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    IsCheck = lstrolePer.Contains(p.Code)
                }).ToList()
            }).ToList();
            return new { GroupPermission = lstGroupPer };
        }
        public object AddRole(RolePerModel obj, string userLogin)
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
            if (obj.Permissions != null)
            {
                string[] s = obj.Permissions.Split(",");
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
    }
}
