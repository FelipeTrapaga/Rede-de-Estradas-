namespace Rede_Estradas
{
    public class Rota
    {
        public Cidade Origem { get; }
        public Cidade Destino { get; }
        public int Distancia { get; set; }
        public string TipoTransporte { get; set; }
        public int TempoViagem { get; set; }

        public Rota(Cidade origem, Cidade destino, int distancia, string tipoTransporte, int tempoViagem)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
            TipoTransporte = tipoTransporte;
            TempoViagem = tempoViagem;
        }
    }
}
