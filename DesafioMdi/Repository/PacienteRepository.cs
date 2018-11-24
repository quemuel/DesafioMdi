using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using DesafioMdi.Models;

namespace DesafioMdi.Repository
{
    public class PacienteRepository
    {
        private readonly XDocument _pacientesData;
        private readonly string _mapPathPacientesData = HttpContext.Current.Server.MapPath("~/App_Data/PacienteData.xml");

        public PacienteRepository()
        {
            _pacientesData = XDocument.Load(_mapPathPacientesData);
        }

        public List<Paciente> TodosPacientes()
        {
            var pacientes = (from node in _pacientesData.Descendants("Paciente")
                select new Paciente {
                    PacienteId = Convert.ToInt32((string) node.Element("PacienteId")?.Value),
                    Nome = Convert.ToString((string) node.Element("Nome")?.Value),
                    DataNascimento = Convert.ToDateTime((string) node.Element("DataNascimento")?.Value)
                }).ToList();

            return pacientes;
        }

        public Paciente PacientesPorId(int id)
        {
            var paciente = (from node in _pacientesData.Descendants("Paciente")
                where Convert.ToInt32(node.Element("PacienteId")?.Value) == id
                select new Paciente
                {
                    PacienteId = Convert.ToInt32(node.Element("PacienteId")?.Value),
                    Nome = Convert.ToString(node.Element("Nome")?.Value),
                    DataNascimento = Convert.ToDateTime(node.Element("DataNascimento")?.Value)
                }).FirstOrDefault();

            return paciente;
        }

        public Paciente AdicionarPaciente(Paciente paciente)
        {
            int ultimoPacienteId = this.TodosPacientes().Max(p => p.PacienteId);

            paciente.PacienteId = ultimoPacienteId + 1;

            _pacientesData.Root.Add(new XElement("Paciente",
                new XElement("PacienteId", paciente.PacienteId),
                new XElement("Nome", paciente.Nome),
                new XElement("DataNascimento", paciente.DataNascimento.Date.ToString())
            ));

            _pacientesData.Save(_mapPathPacientesData);

            return paciente;
        }

        public Paciente AtualizarPaciente(Paciente paciente)
        {
            XElement node = _pacientesData.Root
                    .Elements("Paciente")
                    .FirstOrDefault(p => (int) p.Element("PacienteId") == paciente.PacienteId);

            node.SetElementValue("Nome", paciente.Nome);
            node.SetElementValue("DataNascimento", paciente.DataNascimento.Date.ToString());

            _pacientesData.Save(_mapPathPacientesData);

            return paciente;
        }

        public void RemoverPaciente(int id)
        {
            _pacientesData.Root
                .Elements("Paciente")
                .FirstOrDefault(p => (int)p.Element("PacienteId") == id).Remove();

            _pacientesData.Save(_mapPathPacientesData);
        }
    }
}