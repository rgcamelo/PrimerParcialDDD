﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Base;

namespace Domain.Entities
{
    public class Credito : Entity<int>
    {

        public double ValorCredito { get; set; }
        public double ValorAPagar { get; set; }
        public double SaldoAPagar { get; set; }
        public bool CreditoPagado { get; set; }
        public List<Abono> ListaAbonos { get; set; }
        public List<Cuota> ListaCuota { get; set; }
        public int PlazoDePago { get; set; } 
        public DateTime FechaCredito { get; set; }

        public const double MINIMO_CREDITO = 5000000;
        public const double MAXIMO_CREDITO = 10000000;
        public const double TASADEINTERES = 0.005;

        public Credito()
        {
            ListaAbonos = new List<Abono>();
            ListaCuota = new List<Cuota>();
            CreditoPagado = false;
            FechaCredito = DateTime.Now;
        }

        public IReadOnlyList<string> CanRegistrarCredito(double valorCredito, int plazodepago)
        {
            var errors = new List<string>();

            if((valorCredito >= MINIMO_CREDITO) && (valorCredito <= MAXIMO_CREDITO))
            {
                errors.Add("El Valor del credito debe estar entre 5 millones y 10 millones");
            }

            if(plazodepago > 0 && plazodepago <= 10)
            {
                errors.Add("El plazo de pago incorrecto");
            }

            return errors;
            
        }

        public void RegistrarCredito(double valorCredito, int plazodepago)
        {
            if(CanRegistrarCredito(valorCredito, plazodepago).Any())
            {
                throw new InvalidOperationException();
            }

            PlazoDePago = plazodepago;
            ValorCredito = valorCredito;
            ValorAPagar = ValorCredito * (1 + TASADEINTERES * plazodepago);
            SaldoAPagar = ValorAPagar;
            double valorporCuota = ValorAPagar / plazodepago;
            GenerarCuotas(plazodepago, valorporCuota);

        }

        public void GenerarCuotas(int plazodepago, double valorporcuota )
        {
            for (int i = 0; i < plazodepago; i++)
            {
                Cuota cuota = new Cuota(valorporcuota);
                cuota.NumeroCuota = i;
                ListaCuota.Add(cuota);
            }
        }

        public string RegistrarAbono(double valorAbonado)
        {
            if(CreditoPagado == false)
            {
                Cuota cuota = ListaCuota.Find(cuota => cuota.EstadoCuota == false);
                if (valorAbonado > 0 && valorAbonado <= SaldoAPagar)
                {
                    if (valorAbonado >= cuota.ValorCuota)
                    {
                        double sobrante = cuota.PagarCuota(valorAbonado);

                        PagarAbonoSobrante(sobrante);

                        SaldoAPagar -= valorAbonado;
                        if(SaldoAPagar == 0)
                        {
                            CreditoPagado = true;
                        }
                        Abono abono = new Abono(valorAbonado);
                        ListaAbonos.Add(abono);



                        return "Valor del abono Correcto  Abono registrado";
                    }
                    else
                    {
                        return "Valor del abono incorrecto";
                    }
                }
                else
                {
                    return "Valor del abono incorrecto";
                }
            }
            else
            {
                return "Credito Pagado";
            }
            
        }

        public void PagarAbonoSobrante(double sobrante)
        {
            if (sobrante > 0)
            {
                Cuota cuota2 = ListaCuota.Find(cuota => cuota.EstadoCuota == false);

                if (cuota2 != null)
                {
                    double valor = cuota2.PagarCuota(sobrante);
                    PagarAbonoSobrante(valor);
                }

            }
        }

        public List<string> ConsultarCuotas()
        {
            List<string> Cuotas = new List<string>();

            foreach ( var cuota in ListaCuota)
            {
                Cuotas.Add(cuota.ImprimirInformacionCuota());
            }

            return Cuotas;
        }

        public List<string> ConsultarAbonos()
        {
            List<string> Abonos = new List<string>();

            foreach( var abono in ListaAbonos)
            {
                Abonos.Add(abono.ImprimirInformacionAbono());
            }

            return Abonos;
        }

        public string RegistrarCredito(Empleado empleado, double valorCredito, int plazodepago)
        {
            if ((valorCredito >= MINIMO_CREDITO) && (valorCredito <= MAXIMO_CREDITO))
            {
                if (plazodepago > 0 && plazodepago <= 10)
                {
                    PlazoDePago = plazodepago;
                    ValorCredito = valorCredito;
                    ValorAPagar = valorCredito * (1 + TASADEINTERES * plazodepago);
                    SaldoAPagar = ValorAPagar;

                    double valorporCuota = ValorAPagar / plazodepago;

                    for (int i = 0; i < plazodepago; i++)
                    {
                        Cuota cuota = new Cuota(valorporCuota);
                        cuota.NumeroCuota = i;
                        ListaCuota.Add(cuota);
                    }

                    return $"Valor Total a pagar {ValorAPagar}, Credito Registrado Correctamente";

                }
                else
                {
                    return "El plazo de pago incorrecto";
                }
            }
            else
            {
                return "El valor del crédito debe estar entre 5 millones y 10 millones";
            }

        }

    }

}
