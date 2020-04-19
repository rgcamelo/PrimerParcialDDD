using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Domain.Entities;

namespace Application.EmpleadoServices
{
    public class ConsultarEmpleadoService
    {
        readonly IUnitOfWork _unitOfWork;

        public ConsultarEmpleadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Empleado ConsultarEmpleado(string cedula)
        {
            return _unitOfWork.EmpleadoRepository.
                FindFirstOrDefault(t => t.CedulaEmpleado == cedula);
        }
    }
}
