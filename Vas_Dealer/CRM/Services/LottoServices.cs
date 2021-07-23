using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.VAS;
using VAS.Dealer.Services.Interfaces;

namespace VAS.Dealer.Services
{
    public class LottoServices : ILottoServices
    {
        private readonly MP_Context _Context;
        private readonly Random _random = new Random();
        private readonly ILogger<LottoServices> _logger;
        public readonly IUserServices _userServices;
        public LottoServices(MP_Context Context, ILogger<LottoServices> logger, IUserServices userService)
        {
            _Context = Context;
            _logger = logger;
            _userServices = userService;
        }

        public object UpdateLotto(int AccountId)
        {
            int score = 0;
            try
            {
                //check uu tien
                //tao diem random
                var listLotto = GetListLotto();
                var account = _userServices.GetAccountById(AccountId);
                if (account.IsPriviot == true) {
                    int score2 = _random.Next(30,100);
                    var check = checkScore(score2);
                    while (check == true)
                    {
                        score2 = _random.Next(30, 100);
                        check = checkScore(score2);
                    }
                    score = score2;
                }
                else
                {
                    int score2 = _random.Next(1, 29);
                    var check = checkScore(score2);
                    while (check == true)
                    {
                        score2 = _random.Next(1, 29);
                        check = checkScore(score2);
                    }
                    score = score2;
                }
                //get obj lotto by accountId
                var objLotto = _Context.Lotto.Where(l => l.AccountId == AccountId).FirstOrDefault();
                objLotto.Score = score;
                objLotto.IsClosed = true;
                objLotto.UpdatedDate = DateTime.Now;
                objLotto.IsDeleted = false;
                _Context.Update(objLotto);
                _Context.SaveChanges();

                return new { status = "ok" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public List<LottoModel2> GetListLotto()
        {
            var listItem = (from a in _Context.Account
                            from s in _Context.Lotto 
                         where (s.IsDeleted == false && s.AccountId == a.Id)
                         orderby s.Score descending
                         select new LottoModel2() { 
                             Id = s.Id,
                             AccountUserName = a.UserName,
                             AccountFullName = a.FullName,
                             Score = s.Score,
                             IsClosed = s.IsClosed
                         }).ToList(); 
            return listItem;
        } 
        bool checkScore(int score)
        {
            bool check = false;
            var listLotto = GetListLotto(); 
            foreach (var item in listLotto)
            {
                if (score == item.Score)
                {
                    check = true;
                }
            }
            return check; 
        } 
        public bool checkIsClosed(int accountId)
        {
            var check = false;
            var obj = _Context.Lotto.Where(a => a.AccountId == accountId && a.IsClosed == true).ToList();
            if (obj.Count > 0)
            {
                check = true;
            }

            return check;
        }
    }
}
