using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VAS.Dealer.Models.VAS;

namespace VAS.Dealer.Services.Interfaces
{
    public interface ILottoServices
    {
        public object UpdateLotto(int AccountId);
        public List<LottoModel2> GetListLotto();
        public bool checkIsClosed(int accountId);
    }
}
