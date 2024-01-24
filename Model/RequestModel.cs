using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcenaria.Model
{
    public class RequestModel
    {
        public required string movel { get; set; }
        public required string material { get; set; }
        public required List<GeometriasModel>geometrias { get; set; }
    }
    public class GeometriasModel
    {
        public  string estrutura { get; set; }
        public  string geometria { get; set; }
        public string? raio_base { get; set; }
        public string? lado { get; set; }
        public string? altura { get; set; }
        public string? largura { get; set; }
        public string? comprimento { get; set; }
    }
}