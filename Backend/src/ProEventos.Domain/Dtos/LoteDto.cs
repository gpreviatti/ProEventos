using System;

namespace ProEventos.Domain.Dtos
{
    public class LoteDto : EntityDto
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public EventoDto EventoDto { get; set; }
    }
}
