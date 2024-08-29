using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class Cidade
    {
        public string Nome { get; private set; }

        public Cidade(string nome)
        {
            Nome = nome;
        }
    }
}
