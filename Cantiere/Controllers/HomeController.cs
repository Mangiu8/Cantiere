using Cantiere.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Cantiere.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Dipendenti> dipendenti = new List<Dipendenti>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM DatiDipendenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Dipendenti dipendente = new Dipendenti
                    {
                        IdDipendente = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                        Cognome = reader.GetString(2),
                        CF = reader.GetString(3),
                        Indirizzo = reader.GetString(4),
                        Coniugato = reader.GetBoolean(5),
                        NumeroFigli = reader.GetInt32(6),
                        Mansione = reader.GetString(7),
                        IDPagamento = reader.GetInt32(8)
                    };
                    dipendenti.Add(dipendente);
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return View(dipendenti);
        }

        public ActionResult CreateDipendenti()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDipendenti(Dipendenti dipendente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO DatiDipendenti (Nome, Cognome, CF, Indirizzo, Coniugato, NumeroFigli, Mansione, IDPagamento) " +
                    "VALUES (@Nome, @Cognome, @CF, @Indirizzo, @Coniugato, @NumeroFigli, @Mansione, @IDPagamento)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@CF", dipendente.CF);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.Parameters.AddWithValue("@IDPagamento", dipendente.IDPagamento);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreatePagamenti()
        {
            return View();
        }

        public ActionResult GetDipendente()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDipendente(int id)
        {
            Dipendenti dipendente = null;
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "SELECT * FROM DatiDipendenti WHERE IdDipendente = @IdDipendente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdDipendente", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dipendente = new Dipendenti();
                    dipendente.IdDipendente = reader.GetInt32(0);
                    dipendente.Nome = reader.GetString(1);
                    dipendente.Cognome = reader.GetString(2);
                    dipendente.CF = reader.GetString(3);
                    dipendente.Indirizzo = reader.GetString(4);
                    dipendente.Coniugato = reader.GetBoolean(5);
                    dipendente.NumeroFigli = reader.GetInt32(6);
                    dipendente.Mansione = reader.GetString(7);
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return View(dipendente);
        }

        public ActionResult UpdateDipendente()
        {
            return View();
        }

        [HttpPut]
        public ActionResult UpdateDipendente(Dipendenti dipendente)
        {
            GetDipendente(dipendente.IdDipendente);
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "UPDATE DatiDipendenti SET Nome = @Nome, Cognome = @Cognome, CF = @CF, Indirizzo = @Indirizzo, " +
                    "Coniugato = @Coniugato, NumeroFigli = @NumeroFigli, Mansione = @Mansione WHERE IdDipendente = @IdDipendente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdDipendente", dipendente.IdDipendente);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@CF", dipendente.CF);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = "Errore nell'aggiornamento del dipendente";
            }
            finally
            {
                conn.Close();
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteDipendenti(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "DELETE FROM DatiDipendenti WHERE IdDipendente = @IdDipendente";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdDipendente", id);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreatePagamenti(Pagamenti pagamento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Pagamenti (PeriodoPagamento, Ammontare, Stipendio, Acconto) VALUES (@PeriodoPagamento, @Ammontare, @Stipendio, @Acconto)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PeriodoPagamento", pagamento.PeriodoPagamento);
                cmd.Parameters.AddWithValue("@Ammontare", pagamento.Ammontare);
                cmd.Parameters.AddWithValue("@Stipendio", pagamento.Stipendio);
                cmd.Parameters.AddWithValue("@Acconto", pagamento.Acconto);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Pagamenti");
        }

        public ActionResult Pagamenti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Pagamenti> pagamenti = new List<Pagamenti>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Pagamenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pagamenti pagamento = new Pagamenti
                    {
                        IdPagamento = reader.GetInt32(0),
                        PeriodoPagamento = reader.GetDateTime(1),
                        Ammontare = reader.GetDecimal(2),
                        Stipendio = reader.GetBoolean(3),
                        Acconto = reader.GetBoolean(4)
                    };
                    pagamenti.Add(pagamento);
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return View(pagamenti);
        }

        public ActionResult DeletePagamenti(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "DELETE FROM Pagamenti WHERE IdPagamento = @IdPagamento";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdPagamento", id);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EditPagamenti(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            Pagamenti pagamento = new Pagamenti();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Pagamenti WHERE IdPagamento = @IdPagamento";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdPagamento", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pagamento.IdPagamento = reader.GetInt32(0);
                    pagamento.PeriodoPagamento = reader.GetDateTime(1);
                    pagamento.Ammontare = reader.GetDecimal(2);
                    pagamento.Stipendio = reader.GetBoolean(3);
                    pagamento.Acconto = reader.GetBoolean(4);
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return View(pagamento);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}