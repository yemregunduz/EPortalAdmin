using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPortalAdmin.Core.Domain.Entities
{
    public class UserOperationClaim : BaseEntity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }

        public UserOperationClaim()
        {
        }

        public UserOperationClaim(int userId, int operationClaimId)
        {
            UserId = userId;
            OperationClaimId = operationClaimId;
        }
    }
}
