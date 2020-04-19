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
        public double SalarioEmpleado { get; set; }
        public List<Credito> Creditos { get; set; }


        public Empleado ( string cedula, string nombre, double salario)
        {
            CedulaEmpleado = cedula;
            NombreEmpleado = nombre;
            SalarioEmpleado = salario;
        }

        public Credito SolicitarCredito(double valorcredito, int plazopago)
        {
            Credito credito = new Credito();
            credito.RegistrarCredito(valorcredito, plazopago);

            Creditos.Add(credito);

            return credito;
        }




    }
}
