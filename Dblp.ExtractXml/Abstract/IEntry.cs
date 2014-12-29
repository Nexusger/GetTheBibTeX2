using System;

namespace Temp.Abstract
{
    public interface IEntry
    {
        string ProceedingKey { get; set; }
        DateTime ?MDate { get; set; }
        

    }
}