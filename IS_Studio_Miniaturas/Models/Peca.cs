﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS_Studio_Miniaturas.Models
{
    public class Peca
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Estrutura { get; set; }
        public string Componente { get; set; }
        public short Numero { get; set; }
        public string MateriaPrima { get; set; }
        public string Quantidade { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public double Perimetro { get; set; }
        public double Area { get; set; }
        public string Categoria { get; set; }
        public string Infomacoes { get; set; }
        public byte[] Imagem { get; set; }
    }
}