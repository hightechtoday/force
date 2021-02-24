﻿using System;

 namespace Force.Ddd.Domain.State
{
    public interface IHasStatus<out T> 
        where T : Enum
    {
        T Status { get; }
    }
}