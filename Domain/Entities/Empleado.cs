using System;
using System.Collections.Generic;
using System.Text;
using Domain.Base;

namespace Domain.Entities
{
    public class Empleado : Entity<int>
    {
        public string CedulaEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string SalarioEmpleado { get; set; }

        public Empleado ( string cedula, string nombre, string salario)
        {
            CedulaEmpleado = cedula;
            NombreEmpleado = nombre;
            SalarioEmpleado = salario;
        }
    }
}
