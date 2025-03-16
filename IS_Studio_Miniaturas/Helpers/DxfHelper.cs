using IS_Studio_Miniaturas.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Corel.Interop.VGCore;

namespace IS_Studio_Miniaturas.Helpers
{
    public class DxfHelper
    {
        private Corel.Interop.VGCore.Application corelApp;

        private List<Peca> _listaDePecasDXF;

        //Construtor
        public DxfHelper()
        {
            corelApp = new Corel.Interop.VGCore.Application();
            _listaDePecasDXF = new List<Peca>();
        }

        //Metodos

        /// <summary>
        /// Retorna uma lista de peças a partir do filtro USM
        /// </summary>
        /// <returns></returns>
        public List<Peca> GetListaDePecasComFiltroUSM()
        {
            // Inicializa variáveis

            _listaDePecasDXF.Clear();

            int id = 0;
            string modelo = string.Empty;
            string estrutura = string.Empty;
            short numero = 0;
            string componente = string.Empty;
            string materiaPrima = string.Empty;
            string quantidade = string.Empty;
            string data = string.Empty;
            string hora = string.Empty;
            double perimetro = 0;
            double area = 0;
            string categoria = string.Empty;
            //byte [] imagem=;


            CultureInfo culture = new CultureInfo("en-US");
            Document doc = corelApp.ActiveDocument;
            doc.Unit = cdrUnit.cdrMillimeter;
            doc.Unit = cdrUnit.cdrMillimeter;
            doc.ReferencePoint = cdrReferencePoint.cdrCenter;
            Page pag = doc.ActivePage;
            ShapeRange sr = doc.SelectableShapes.All();

            // percorre os objetos da seleção em busca de grupos
            foreach (Shape s in sr)
            {
                if (s.Type == cdrShapeType.cdrGroupShape)
                {
                    ShapeRange gp = s.Shapes.All();

                    // Busca dentro do grupo as informações de texto das pecas
                    int i = 1;
                    foreach (Shape item in gp)
                    {
                        id = item.StaticID;
                        #region FILTRO DXF(USM)
                        string patternModelo = @"^\w+$"; // Uma unica palavra
                        string patternComponente = @"^\w+\s\d+$"; // Uma Palavra Seguida por "espaço" e um numero
                        string patternQuantidade = @"\d\+\d/\d"; // “1+1/1”
                        string patternDataHora = @"(\d{2}/\d{2}/\d{4})\s(\d{2}:\d{2}:\d{2})";
                        string patternEstrutura = @"EST_"; // "-0-"
                        string patternArea = @"mm²"; // "mm²"
                        string patternPerimeter = @"mm(?!2)"; // "mm"
                        string patternCategoria = @"CAT_";
                        //string patternMaterial = @"^\w+\s\w+.*$";  // Uma Palavra Seguida por "espaço" e outra palavra ou mais


                        //Identifica o texto das peças
                        double result;
                        if (item.Type == cdrShapeType.cdrTextShape)
                        {
                            string strTexto = item.Text.Story.Text;

                            if (i == 1) // Assume-se aqui que sempre a primeira ocorrência de texto seja o materiaprima a ser utilizado
                            {
                                materiaPrima = strTexto;
                            }

                            if (Regex.IsMatch(strTexto, patternModelo))
                            {
                                modelo = strTexto;
                            }

                            if (Regex.IsMatch(strTexto, patternComponente))
                            {
                                // Retorna o String sem os dois ultimos caracteres
                                string semDoisUltimos = strTexto.Substring(0, strTexto.Length - 2);
                                componente = semDoisUltimos;

                                // Extrai o numero da string. Pega os dois ultimos caracteres
                                if (strTexto.Length >= 2)
                                {
                                    string numeroString = string.Empty;
                                    string ultimosDoisCaracteres = strTexto.Substring(strTexto.Length - 2);
                                    if (short.TryParse(ultimosDoisCaracteres, out short numeroShort))
                                    {
                                        numero = numeroShort;
                                    }
                                }

                            }

                            if (Regex.IsMatch(strTexto, patternArea))
                            {
                                strTexto = Regex.Replace(strTexto, patternArea, "");
                                if (double.TryParse(strTexto, NumberStyles.Any, culture, out result))
                                {
                                    area = result;
                                }
                            }

                            if (Regex.IsMatch(strTexto, patternPerimeter))
                            {
                                strTexto = Regex.Replace(strTexto, patternPerimeter, "");
                                if (double.TryParse(strTexto, NumberStyles.Any, culture, out result))
                                {
                                    perimetro = result;
                                }
                            }

                            Match match = Regex.Match(strTexto, patternDataHora);
                            if (match.Success)
                            {
                                string date = match.Groups[1].Value;
                                string time = match.Groups[2].Value;
                                data = date;
                                hora = time;
                            }

                            if (Regex.IsMatch(strTexto, patternQuantidade))
                            {
                                quantidade = strTexto;
                            }

                            if (Regex.IsMatch(strTexto, patternEstrutura))
                            {
                                strTexto = Regex.Replace(strTexto, patternEstrutura, "Cabedal");
                                estrutura = strTexto;
                            }
                            // Força a estrutura = Cabedal
                            if (estrutura == "") { estrutura = "Cabedal"; };

                            if (Regex.IsMatch(strTexto, patternCategoria))
                            {
                                strTexto = Regex.Replace(strTexto, patternCategoria, "Categoria");
                                categoria = strTexto;
                            }

                            i++;
                        }
                        #endregion
                    }

                    // Cria um registro dentro da lista de pecas
                    if (componente != "")
                    {
                        _listaDePecasDXF.Add(new Peca
                        {
                            Id = id,
                            Modelo = modelo,
                            Componente = componente,
                            Estrutura = estrutura,
                            Numero = numero,
                            MateriaPrima = materiaPrima,
                            Quantidade = quantidade,
                            Data = data,
                            Hora = hora,
                            Perimetro = perimetro,
                            Area = area,
                            Categoria = categoria,
                            Imagem = null,
                        });
                    };
                }
            }

            return _listaDePecasDXF;
        }
    }
}