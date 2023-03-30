using Core.DataAccess;
using DataAccess.Concrete.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Abstract
{
    public interface ITrasnsactionDal : IEntityRepository<Trasnsaction>
    {
    }
}
