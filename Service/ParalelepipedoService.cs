using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcenaria.Service
{
    public class ParalelepipedoService
    {
        public double Resultado { get; set; }
        public double CalcularParalelepipedo( double largura, double altura, double  comprimento)
        {
            Resultado = comprimento * largura + comprimento * altura + largura * altura;
            
            return Resultado;
        }
    }
}