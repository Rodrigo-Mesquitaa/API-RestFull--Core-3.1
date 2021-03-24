using Api_RestFul.Commands;
using Api_RestFul.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_RestFul.Service.Interfaces
{
    public interface IDummyUserService
    {
        IQueryable<Applicant> Query();
        Task<IEnumerable<Applicant>> All();
        Task<Applicant> Find(string id);
        Task Save(RegisterApplicantCommand command);
        Task Update(Applicant actualApplicant);
        Task<Applicant> Remove(string username);
        Task Approve(string username);
        Task Decline(string username);
        Task<Applicant> Transfer(TransferApplicantCommand command);
    }
}