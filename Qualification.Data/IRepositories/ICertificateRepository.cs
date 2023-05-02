using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Qualification.Domain.Entities.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.IRepositories
{
    public interface ICertificateRepository
    {
        ValueTask<Certificate> DeleteCertificateAsync(Certificate certificate);

        ValueTask<Certificate> InsertCertificateAsync(Certificate certificate);

        IQueryable<Certificate> SelectAllCertificates();

        ValueTask<Certificate> SelectCertificateByIdAsync(long certificateId);

        ValueTask<Certificate> SelectCertificateByIdAsync(long certificateid,
            IReadOnlyList<string> includes);

        ValueTask<Certificate> UpdateCertificateAsync(Certificate certificate);
    }
}
