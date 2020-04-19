﻿using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ICreditoRepository CreditoRepository { get; }
        ICuotaRepository CuotaRepository { get; }
        IAbonoRepository AbonoRepository { get; }
        IEmpleadoRepository EmpleadoRepository { get; }
        int Commit();
    }
}
