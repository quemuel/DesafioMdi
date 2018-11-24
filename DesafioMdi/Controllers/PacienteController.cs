using System.Linq;
using System.Net;
using System.Web.Mvc;
using DesafioMdi.Models;
using DesafioMdi.Repository;

namespace DesafioMdi.Controllers
{
    public class PacienteController : Controller
    {
        private readonly PacienteRepository _pacienteRepository = new PacienteRepository();

        public ActionResult Listar()
        {
            return View(_pacienteRepository.TodosPacientes());
        }

        public ActionResult Visualizar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = _pacienteRepository.PacientesPorId(id.Value);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "PacienteId,Nome,DataNascimento")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _pacienteRepository.AdicionarPaciente(paciente);
                return RedirectToAction("Listar");
            }

            return View(paciente);
        }

        public ActionResult Atualizar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = _pacienteRepository.PacientesPorId(id.Value);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar([Bind(Include = "PacienteId,Nome,DataNascimento")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _pacienteRepository.AtualizarPaciente(paciente);
                return RedirectToAction("Listar");
            }
            return View(paciente);
        }

        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paciente paciente = _pacienteRepository.PacientesPorId(id.Value);
            if (paciente == null)
            {
                return HttpNotFound();
            }
            return View(paciente);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmed(int id)
        {
            _pacienteRepository.RemoverPaciente(id);
            return RedirectToAction("Listar");
        }
    }
}
