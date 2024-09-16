using BankApplicationAPI.Interfaces;
using BankApplicationAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BankApplicationAPI.Services
{
    public class LoanTypeService
    {
        private readonly ILoanType _loanType;

        public LoanTypeService(ILoanType loanType)
        {
            _loanType = loanType;
        }

        public async Task<bool> CreateLoanTypeAsync(LoanType loanType)
        {
            try
            {
                return await _loanType.CreateLoanTypeAsync(loanType);
            }
            catch { throw; }
        }
        public async Task<bool> DeleteLoanTypeAsync(LoanType loanType)
        {
            try
            {
                return await _loanType.DeleteLoanTypeAsync(loanType);
            }
            catch { throw; }
        }
        public async Task<IEnumerable<LoanType>> GetLoanTypeAsync(int? LoanTypeId = null, string? LoanTypeName = null)
        {
            try
            {
                return await _loanType.GetLoanTypeAsync(LoanTypeId, LoanTypeName);
            }
            catch { throw; }
        }

        public async Task<LoanType> GetLoanTypeByLoanTypeIdAsync(int LoanTypeId)
        {
            try
            {
                return await _loanType.GetLoanTypeByLoanTypeIdAsync(LoanTypeId);
            }
            catch { throw; }
        }

        public async Task<IEnumerable<LoanType>> GetLoanTypesAsync()
        {
            try
            {
                return await _loanType.GetLoanTypesAsync();
            }
            catch { throw; }
        }

        public async Task<LoanType> UpdateLoanTypeAsync(LoanType loanType)
        {
            try
            {
                return await _loanType.UpdateLoanTypeAsync(loanType);
            }
            catch { throw; }
        }
    }
}
