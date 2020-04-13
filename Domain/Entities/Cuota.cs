using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuota
    {
        public int NumeroCuota { get; set; }
        public double ValorCuota { get; set; }
        public double ValorPagado { get; set; }
        public bool EstadoCuota { get; set; }

        public Cuota(double valorCuota)
        {
            ValorCuota = valorCuota;
            ValorPagado = valorCuota;
            EstadoCuota = false;
        }

        public double PagarCuota(double valor)
        {
            ValorPagado -= valor;

            if(ValorPagado < 0)
            {
                EstadoCuota = true;
                var Pagar = ValorPagado;
                ValorPagado = 0;
                return Pagar*-1;
                
            }
            return 0;

        }

       public string ImprimirInformacionCuota()
        {
            return $"Cuota No: {NumeroCuota} - Valor de la Cuota: {ValorCuota} - Valor por Pagar: {ValorPagado} - Estado de Pago: {EstadoCuota}";
        }
    }
}
