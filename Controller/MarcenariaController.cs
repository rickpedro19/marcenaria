using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Marcenaria.Model;
using Marcenaria.Service;

namespace Marcenaria.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcenariaController : ControllerBase
    {
        private readonly ILogger<MarcenariaController> _logger;

        public MarcenariaController(ILogger<MarcenariaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<ResponseModel> GerarPedido([FromBody] RequestModel request)
        {
            GeometriaService gs = new GeometriaService();
            CilindroService cis = new CilindroService();
            ParalelepipedoService ps = new ParalelepipedoService();

            double areaTotal = 0.0;
            Regex pattern = new Regex(@"(\d+(\.\d+)?)cm");

            List<PartesModel> partes = new List<PartesModel>();

            foreach (var geometria in request.geometrias)
            {
                if (geometria.geometria == "cilindro")
                {
                    var raioBase = geometria.raio_base ?? "0.0";
                    var altura = geometria.altura ?? "0.0";

                    var matchRaioBase = pattern.Match(raioBase);
                    var matchAltura = pattern.Match(altura);

                    if (matchRaioBase.Success && matchAltura.Success)
                    {
                        double cilindroRaioBase, cilindroAltura;

                        if (double.TryParse(matchRaioBase.Groups[1].Value, out cilindroRaioBase) &&
                            double.TryParse(matchAltura.Groups[1].Value, out cilindroAltura))
                        {
                            double preco = gs.CalcularPreco(request.material, cis.CalcularCilindro(cilindroRaioBase, cilindroAltura));

                            // Adiciona a parte com o preço calculado à lista
                            partes.Add(new PartesModel
                            {
                                Estrutura = geometria.estrutura,
                                Geometria = geometria.geometria,
                                Preco = $"R${preco}"
                            });

                            areaTotal += cilindroRaioBase * cilindroAltura;
                            _logger.LogInformation($"Área do cilindro: {areaTotal}");
                        }
                        else
                        {
                            _logger.LogError("Falha na conversão para valores numéricos.");
                        }
                    }
                    else
                    {
                        _logger.LogError("Padrão não corresponde às entradas.");
                    }
                }
                if(geometria.geometria == "paralelepípedo")
                {
                    var comprimento = geometria.comprimento ?? "0.0";
                    var altura = geometria.altura ?? "0.0";
                    var largura = geometria.largura ?? "0.0";

                    var matchComprimento = pattern.Match(comprimento);
                    var matchAltura = pattern.Match(altura);
                    var matchLargura = pattern.Match(largura);

                    if (matchComprimento.Success && matchAltura.Success && matchLargura.Success)
                    {
                        double ParalelepipedoComprimento, ParalelepipedoAltura, ParalelepipedoLargura;

                        if (double.TryParse(matchComprimento.Groups[1].Value, out ParalelepipedoComprimento) &&
                            double.TryParse(matchAltura.Groups[1].Value, out ParalelepipedoAltura)&&
                            double.TryParse(matchLargura.Groups[1].Value, out ParalelepipedoLargura))
                        {
                            double preco = gs.CalcularPreco(request.material, ps.CalcularParalelepipedo( ParalelepipedoLargura, ParalelepipedoAltura, ParalelepipedoComprimento ));

                            // Adiciona a parte com o preço calculado à lista
                            partes.Add(new PartesModel
                            {
                                Estrutura = geometria.estrutura,
                                Geometria = geometria.geometria,
                                Preco = $"R${preco}"
                            });

                            areaTotal += ParalelepipedoLargura * ParalelepipedoAltura * ParalelepipedoComprimento;
                            _logger.LogInformation($"Área do cilindro: {areaTotal}");
                        }
                        else
                        {
                            _logger.LogError("Falha na conversão para valores numéricos.");
                        }
                    }
                    else
                    {
                        _logger.LogError("Padrão não corresponde às entradas.");
                    }
                }
            }

            // Calcula a soma de todos os preços individuais
            double precoTotal = partes.Sum(p => double.Parse(p.Preco.Substring(2))); // Remove "R$"

            var resposta = new ResponseModel
            {
                NomeMovel = request.movel,
                Partes = partes,
                ValorTotal = $"R${precoTotal}"
            };

            return Ok(resposta);
        }
    }
}
