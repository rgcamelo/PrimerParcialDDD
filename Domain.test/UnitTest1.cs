using NUnit.Framework;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RegistrarCreditoValorMenora5Millones()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            string mensaje = credito.RegistrarCredito(empleado, 4000000, DateTime.Now, 5);
            Assert.AreEqual(mensaje, "El valor del crédito debe estar entre 5 millones y 10 millones");
        }

        [Test]
        public void RegistrarCreditoValorMayor10Millones()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            string mensaje = credito.RegistrarCredito(empleado, 11000000, DateTime.Now, 5);
            Assert.AreEqual(mensaje, "El valor del crédito debe estar entre 5 millones y 10 millones");
        }

        [Test]
        public void RegistrarCreditoPlazoPagoIncorrecto()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();

            string mensaje = credito.RegistrarCredito(empleado, 9000000, DateTime.Now, -2);
            Assert.AreEqual(mensaje, "El plazo de pago incorrecto");
        }

        [Test]
        public void RegistrarCreditoIncorrectoPlazoPagoSuperiorAlLimite()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();

            string mensaje = credito.RegistrarCredito(empleado, 9000000, DateTime.Now, 11);
            Assert.AreEqual(mensaje, "El plazo de pago incorrecto");
        }

        [Test]
        public void RegistrarCreditoCorrecto()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            string mensaje = credito.RegistrarCredito(empleado, 9000000, DateTime.Now, 5);
            Assert.AreEqual(mensaje, "Valor Total a pagar 9225000, Credito Registrado Correctamente");
        }

        [Test]
        public void RegistrarAbonoIncorrecto()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 9000000, DateTime.Now, 5);
            string mensaje = credito.RegistrarAbono(-22000000);
            Assert.AreEqual(mensaje, "Valor del abono incorrecto");
        }

        [Test]
        public void RegistrarAbonoPagoExcedido()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 10);
            string mensaje = credito.RegistrarAbono(11000000);
            Assert.AreEqual("Valor del abono incorrecto", mensaje);
        }

        [Test]
        public void RegistrarAbonoCorrecto()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 10);
            string mensaje = credito.RegistrarAbono(1050000);
            Assert.AreEqual(mensaje, "Valor del abono Correcto  Abono registrado");
        }

        [Test]
        public void RegistrarAbonoCorrectoSobrante()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 10);
            credito.RegistrarAbono(1100000);
           
            Cuota cuota2 = credito.ListaCuota.Find(cuota => cuota.EstadoCuota == false);
            Assert.AreEqual(cuota2.ValorPagado, 1000000);
        }

        [Test]
        public void RealizarAbonoCreditoPagado()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 5);
            credito.RegistrarAbono(10250000);
            string mensaje = credito.RegistrarAbono(2050000);


            Assert.AreEqual("Credito Pagado", mensaje);
        }



        [Test]
        public void ConsultarCuotas()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 2);

            var Cuotas = new List<string>() {
                "Cuota No: 0 - Valor de la Cuota: 5050000 - Valor por Pagar: 5050000 - Estado de Pago: False",
                "Cuota No: 1 - Valor de la Cuota: 5050000 - Valor por Pagar: 5050000 - Estado de Pago: False"};


            CollectionAssert.AreEqual(Cuotas, credito.ConsultarCuotas());
        }

        [Test]
        public void ConsultarCuotasConAbonosRealizados()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 5);
            credito.RegistrarAbono(7000000);
            

            var Cuotas = new List<string>() { 
                "Cuota No: 0 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 1 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 2 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 3 - Valor de la Cuota: 2050000 - Valor por Pagar: 1200000 - Estado de Pago: False",
                "Cuota No: 4 - Valor de la Cuota: 2050000 - Valor por Pagar: 2050000 - Estado de Pago: False"
            };


            CollectionAssert.AreEqual(Cuotas, credito.ConsultarCuotas());
        }


        [Test]
        public void ConsultarCuotasConAbonosRealizadosSegundaPrueba()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 5);
            credito.RegistrarAbono(8200000);


            var Cuotas = new List<string>() {
                "Cuota No: 0 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 1 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 2 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 3 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 4 - Valor de la Cuota: 2050000 - Valor por Pagar: 2050000 - Estado de Pago: False"
            };


            CollectionAssert.AreEqual(Cuotas, credito.ConsultarCuotas());
        }

        [Test]
        public void ConsultarCuotasConAbonosRealizadosTerceeraPrueba()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 5);
            credito.RegistrarAbono(10250000);


            var Cuotas = new List<string>() {
                "Cuota No: 0 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 1 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 2 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 3 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True",
                "Cuota No: 4 - Valor de la Cuota: 2050000 - Valor por Pagar: 0 - Estado de Pago: True"
            };


            CollectionAssert.AreEqual(Cuotas, credito.ConsultarCuotas());
        }

        

        [Test]
        public void ConsultarAbonosRealizados()
        {
            Empleado empleado = new Empleado("1065846150", "Rafael Camelo", "1000000");
            Credito credito = new Credito();
            string Fecha = DateTime.Now.ToString("d");
            credito.RegistrarCredito(empleado, 10000000, DateTime.Now, 5);
            credito.RegistrarAbono(3000000);
            credito.RegistrarAbono(3000000);


            var Abonos = new List<string>() {
                "Valor del Abono: 3000000, Fecha de Realizacion del Abono "+Fecha,
                "Valor del Abono: 3000000, Fecha de Realizacion del Abono "+Fecha,
            };


            CollectionAssert.AreEqual(Abonos, credito.ConsultarAbonos());
        }







    }
}