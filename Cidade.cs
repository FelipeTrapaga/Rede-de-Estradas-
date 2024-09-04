using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class Cidade
    {
        public string Nome { get; }

        public Cidade(string nome)
        {
            Nome = nome;
        }

        public override bool Equals(object obj)
        {
            return obj is Cidade cidade && Nome == cidade.Nome;
        }

        public override int GetHashCode()
        {
            return Nome.GetHashCode();
        }
    }
}
