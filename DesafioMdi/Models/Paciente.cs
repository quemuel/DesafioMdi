using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DesafioMdi.Models
{
    [XmlRoot(ElementName = "Paciente")]
    public class Paciente
    {
        [DisplayName("ID")]
        public int PacienteId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }

        [DisplayName("Data de Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [Required(ErrorMessage = "Digite a data de nascimento")]
        public DateTime DataNascimento { get; set; }

        public int Idade()
        {
            //SE DATA DE NASCIMENTO EH MAIOR QUE A DATA ATUAL
            if (DateTime.Compare(DateTime.Now, this.DataNascimento) < 0) return 0;

            var idade = DateTime.Now.Year - this.DataNascimento.Year;

            //SUBTRAINDO UMA UNIDADE DA IDADE PARA O CASO DO PACIENTE AINDA NAO COMPLETOU O CICLO.
            if (DateTime.Now.Month < this.DataNascimento.Month || (DateTime.Now.Month == this.DataNascimento.Month && DateTime.Now.Day < this.DataNascimento.Day))
                idade--;

            return idade;
        }
    }
}