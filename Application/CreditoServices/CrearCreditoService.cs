using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Domain.Entities;
using Application.EmpleadoServices;
using System.Linq;

namespace Application.CreditoServices
{
    class CrearCreditoService
    {
        readonly IUnitOfWork _unitOfWork;
        ConsultarEmpleadoService consultarEmpleadoService;

        public CrearCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            consultarEmpleadoService = new ConsultarEmpleadoService(_unitOfWork);
        }

        
        public GeneralResponse Ejecutar (CrearCreditoRequest request)
        {
            Empleado empleado = consultarEmpleadoService.ConsultarEmpleado(request.CedulaEmpleado);

            if( empleado == null)
            {
                return new GeneralResponse() { Mensaje = $"El empleado con la cedula {request.CedulaEmpleado} no existe" };
            }

            var errors = new Credito().CanRegistrarCredito(request.ValorCredito, request.PlazoCredito);

            if (errors.Any())
            {
                return new GeneralResponse() { Mensaje = String.Join(",", errors) };
            }

            Credito credito = empleado.SolicitarCredito(request.ValorCredito, request.PlazoCredito);

            _unitOfWork.CreditoRepository.Add(credito);
            _unitOfWork.Commit();

            return new GeneralResponse() { Mensaje = "Credito Registrado Exitosamente" };
        }
    }

    public class CrearCreditoRequest
    {
        public string CedulaEmpleado { get; set; }
        public double ValorCredito { get; set; }
        public int PlazoCredito { get; set; }
    }

}
