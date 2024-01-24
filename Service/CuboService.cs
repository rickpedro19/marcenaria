using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcenaria.Service
{
    public class CuboService
    {
        public double Resultado { get; set; }
        public double CalcularCubo(double  lado)
        {
            Resultado = Resultado = 6 * (lado * 2);
            
            return Resultado;
        }
    }
}