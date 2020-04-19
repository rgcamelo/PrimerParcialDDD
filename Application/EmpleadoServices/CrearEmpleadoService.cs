using System;
using System.Collections.Generic;
using System.Text;
using Domain.Contracts;
using Domain.Entities;

namespace Application.EmpleadoServices
{
    public class CrearEmpleadoService
    {
        readonly IUnitOfWork _unitOfWork;
        ConsultarEmpleadoService consultarEmpleadoService;

        public CrearEmpleadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            consultarEmpleadoService = new ConsultarEmpleadoService(_unitOfWork);
        }

        public GeneralResponse Ejecutar( CrearEmpleadoRequest request)
        {
            Empleado empleado = consultarEmpleadoService.ConsultarEmpleado(request.CedulaEmpleado);

            if ( empleado != null)
            {
                return new GeneralResponse() { Mensaje = $"El Numero de cedula: {request.CedulaEmpleado}, Ya se encuentra registrado" };
            }

            Empleado nuevoEmpleado = new Empleado(request.CedulaEmpleado, request.NombreEmpleado, request.SalarioEmpleado);

            _unitOfWork.EmpleadoRepository.Add(nuevoEmpleado);

            return new GeneralResponse() { Mensaje = $"El Empleado {nuevoEmpleado.NombreEmpleado} con cedula {nuevoEmpleado.CedulaEmpleado} fue registrado exitosamente." };

        }
    }

    public class CrearEmpleadoRequest
    {
        public string CedulaEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public double SalarioEmpleado { get; set; }
    }
}
