using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.Contexts;
using Qualification.Domain.Entities.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qualification.Domain.Entities.Quizes;
using Qualification.Data.IRepositories;

namespace Qualification.Data.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly AppDbContext appDbContext;
        public CertificateRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async ValueTask<Certificate> DeleteCertificateAsync(Certificate certificate)
        {
            EntityEntry<Certificate> certificateEntityEntry =
                this.appDbContext.Certificates.Remove(certificate);

            await this.appDbContext.SaveChangesAsync();

            return certificateEntityEntry.Entity;
        }

        public async ValueTask<Certificate> InsertCertificateAsync(Certificate certificate)
        {
            EntityEntry<Certificate> certificateEntityEntry =
                await this.appDbContext.Certificates.AddAsync(certificate);

            await this.appDbContext.SaveChangesAsync();

            return certificateEntityEntry.Entity;
        }

        public IQueryable<Certificate> SelectAllCertificates() => this.appDbContext.Certificates;

        public async ValueTask<Certificate> SelectCertificateByIdAsync(long certificateId) =>
            await this.appDbContext.Certificates.FindAsync(certificateId);

        public async ValueTask<Certificate> SelectCertificateByIdAsync(
            long certificateid,
            IReadOnlyList<string> includes)
        {
            IQueryable<Certificate> certificates = this.appDbContext.Certificates;

            foreach (var include in includes)
                certificates = certificates.Include(include);

            return await certificates
                .FirstOrDefaultAsync(certificate => certificate.Id == certificateid);
        }

        public async ValueTask<Certificate> UpdateCertificateAsync(Certificate certificate)
        {
            EntityEntry<Certificate> certificateEntityEntry =
                 this.appDbContext.Certificates.Update(certificate);

            await this.appDbContext.SaveChangesAsync();

            return certificateEntityEntry.Entity;
        }
    }
}
