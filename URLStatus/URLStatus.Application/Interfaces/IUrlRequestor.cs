using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLStatus.Domain.Entities;

namespace URLStatus.Application.Interfaces
{
    public interface IUrlRequestor
    {
        Task<ResultData> GetUrlResultData(string url, CancellationToken cancellationToken);
    }
}
