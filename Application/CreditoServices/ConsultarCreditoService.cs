using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Domain.Entities;

namespace Application.CreditoServices
{
    public class ConsultarCreditoService
    {
        readonly IUnitOfWork _unitOfWork;

        public ConsultarCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
