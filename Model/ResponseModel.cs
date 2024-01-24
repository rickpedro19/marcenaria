using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcenaria.Model
{
    public class ResponseModel
    {
        public  string NomeMovel { get; set; }
        public  List<PartesModel>Partes { get; set; }
        public  string ValorTotal { get; set; }
    }
    public class PartesModel
    {
        public  string Estrutura { get; set; }
        public  string Geometria { get; set; }
        public  string Preco { get; set; }
    }
}