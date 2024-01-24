using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcenaria.Service
{
    public class CilindroService
    {
        public double Resultado { get; set; }
        public double CalcularCilindro(double  raio, double altura)
        {
            Resultado = 2 * 3.14 * raio * altura + 2 * 3.14 * (raio * 2);
            
            return Resultado;
        }
    }
}