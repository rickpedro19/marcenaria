using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marcenaria.Model;
using Microsoft.AspNetCore.Mvc;

namespace Marcenaria.Service
{
    public class GeometriaService
    {
        public double Resultado { get; set; }
        public double Preco { get; set; }
        Esfera e = new Esfera();
        Cubo c = new Cubo();
        CuboService cs = new CuboService();
        Cilindro ci = new Cilindro();
        CilindroService cis = new CilindroService();
        Paralelepipedo p = new Paralelepipedo();
        ParalelepipedoService ps = new ParalelepipedoService();
        public double CalcularPreco(string material, double area)
        {
            if(material == "Pinho")
            {
                Preco = 0.10;
            }
            else if(material == "Carvalho")
            {
                Preco = 0.30;
            }
            else if(material == "Ébano")
            {
                Preco = 5.00;
            }
            else
            {
                Preco = 0;
            }

            Resultado = Preco * area;
            
            return Resultado;
        }
    }
}