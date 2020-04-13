using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Abono
    {
        public double ValorAbono { get; set; }
        public DateTime FechaAbono { get; set; }

        public Abono(double abono)
        {
            ValorAbono = abono;
            FechaAbono = DateTime.Now;
        }


        public string ImprimirInformacionAbono()
        {
            return $"Valor del Abono: {ValorAbono}, Fecha de Realizacion del Abono {FechaAbono.ToString("d")}";
        }

        
    }
}
