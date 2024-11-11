using Sistema.Core.Dominio.DTO.Presenca;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sistema.Core.Aplicacao.Services
{
    public class RelatorioPresencaService
    {
        public static string GerarRelatorioPresenca(IEnumerable<PresencaDTO> presencas)
        {
            var sb = new StringBuilder();
            var stylesheet = @"
            <style>
                .report-container { font-family: Arial, sans-serif; }
                .date-header { 
                    background-color: #f0f0f0; 
                    padding: 10px; 
                    margin: 10px 0; 
                    font-weight: bold;
                }
                .presence-table { 
                    width: 100%; 
                    border-collapse: collapse; 
                    margin-bottom: 20px;
                }
                .presence-table th, .presence-table td { 
                    border: 1px solid #ddd; 
                    padding: 8px; 
                    text-align: left;
                }
                .presence-table th { 
                    background-color: #f8f8f8; 
                }
                .status-presente { color: green; }
                .status-ausente { color: red; }
                @media print {
                    .report-container { margin: 20px; }
                    .date-header { 
                        background-color: #f0f0f0 !important; 
                        -webkit-print-color-adjust: exact; 
                    }
                    .presence-table th { 
                        background-color: #f8f8f8 !important; 
                        -webkit-print-color-adjust: exact;
                    }
                }
            </style>";

            // Início do HTML
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine(stylesheet);
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='report-container'>");
            sb.AppendLine("<h1>Relatório de Presenças</h1>");

            // Agrupar presenças por data do TurmaHorario
            var presencasAgrupadas = presencas
                .OrderBy(p => p.TurmaHorario.Data)
                .GroupBy(p => p.TurmaHorario.Data.Date);

            foreach (var grupo in presencasAgrupadas)
            {
                // Cabeçalho da data
                sb.AppendLine($"<div class='date-header'>");
                sb.AppendLine($"Data: {grupo.Key:dd/MM/yyyy}");
                sb.AppendLine("</div>");

                // Tabela de presenças
                sb.AppendLine("<table class='presence-table'>");
                sb.AppendLine("<thead>");
                sb.AppendLine("<tr>");
                sb.AppendLine("<th>Aluno</th>");
                sb.AppendLine("<th>E-mail</th>");
                sb.AppendLine("<th>Telefone</th>");
                sb.AppendLine("<th>Turma</th>");
                sb.AppendLine("<th>Horário</th>");
                sb.AppendLine("<th>Status</th>");
                sb.AppendLine("</tr>");
                sb.AppendLine("</thead>");
                sb.AppendLine("<tbody>");

                foreach (var presenca in grupo.OrderBy(p => p.Nome))
                {
                    string statusClass = presenca.Presente ? "status-presente" : "status-ausente";
                    string statusText = presenca.Presente ? "Presente" : "Ausente";

                    sb.AppendLine("<tr>");
                    sb.AppendLine($"<td>{HttpUtility.HtmlEncode(presenca.Nome)}</td>");
                    sb.AppendLine($"<td>{HttpUtility.HtmlEncode(presenca.Email)}</td>");
                    sb.AppendLine($"<td>{HttpUtility.HtmlEncode(presenca.Telefone)}</td>");
                    sb.AppendLine($"<td>{HttpUtility.HtmlEncode(presenca.Turma.NomeTurma)}</td>");
                    sb.AppendLine($"<td>{presenca.TurmaHorario.Data:HH:mm}</td>");
                    sb.AppendLine($"<td class='{statusClass}'>{statusText}</td>");
                    sb.AppendLine("</tr>");
                }

                sb.AppendLine("</tbody>");
                sb.AppendLine("</table>");

                // Resumo do dia
                var totalPresentes = grupo.Count(p => p.Presente);
                var totalAusentes = grupo.Count(p => !p.Presente);
                sb.AppendLine("<div class='summary'>");
                sb.AppendLine($"<p>Total de presentes: {totalPresentes}</p>");
                sb.AppendLine($"<p>Total de ausentes: {totalAusentes}</p>");
                sb.AppendLine("</div>");
            }

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        public static string GerarRelatorioMatrizPresenca(IEnumerable<PresencaDTO> presencas)
        {
            var sb = new StringBuilder();
            var stylesheet = @"
            <style>
                .report-container { 
                    font-family: Arial, sans-serif;
                    margin: 20px;
                }
                .presence-table { 
                    width: 100%; 
                    border-collapse: collapse; 
                    margin: 20px 0;
                }
                .presence-table th, .presence-table td { 
                    border: 1px solid #ddd; 
                    padding: 8px; 
                    text-align: center;
                }
                .presence-table th { 
                    background-color: #f8f8f8; 
                }
                .aluno-info {
                    text-align: left !important;
                }
                .presente { 
                    color: green;
                    font-weight: bold;
                }
                .ausente { 
                    color: red;
                }
                @media print {
                    .presence-table th { 
                        background-color: #f8f8f8 !important; 
                        -webkit-print-color-adjust: exact;
                    }
                }
            </style>";

            // Início do HTML
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8'>");
            sb.AppendLine(stylesheet);
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='report-container'>");
            sb.AppendLine("<h1>Relatório de Presenças</h1>");

            // Obter todas as datas únicas ordenadas
            var datas = presencas
                .Select(p => p.TurmaHorario.Data.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            // Agrupar por aluno e turma
            var alunosPorTurma = presencas
                .GroupBy(p => new { p.IdPessoa, p.Nome, p.Turma.NomeTurma })
                .OrderBy(g => g.Key.Nome)
                .ToList();

            // Criar a tabela
            sb.AppendLine("<table class='presence-table'>");

            // Cabeçalho
            sb.AppendLine("<thead><tr>");
            sb.AppendLine("<th>Aluno</th>");
            sb.AppendLine("<th>Turma</th>");

            // Adicionar datas ao cabeçalho
            foreach (var data in datas)
            {
                sb.AppendLine($"<th>{data:dd/MM/yyyy}</th>");
            }
            sb.AppendLine("</tr></thead>");

            // Corpo da tabela
            sb.AppendLine("<tbody>");
            foreach (var grupo in alunosPorTurma)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class='aluno-info'>{HttpUtility.HtmlEncode(grupo.Key.Nome)}</td>");
                sb.AppendLine($"<td class='aluno-info'>{HttpUtility.HtmlEncode(grupo.Key.Nome)}</td>");

                // Para cada data, verificar se existe presença
                foreach (var data in datas)
                {
                    var presenca = grupo
                        .FirstOrDefault(p => p.TurmaHorario.Data.Date == data);

                    if (presenca != null)
                    {
                        string statusClass = presenca.Presente ? "presente" : "ausente";
                        string statusText = presenca.Presente ? "✓" : "✗";
                        sb.AppendLine($"<td class='{statusClass}'>{statusText}</td>");
                    }
                    else
                    {
                        // Caso não haja registro para esta data
                        sb.AppendLine("<td>-</td>");
                    }
                }
                sb.AppendLine("</tr>");
            }
            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            // Legenda
            sb.AppendLine("<div class='legenda'>");
            sb.AppendLine("<p><span class='presente'>✓</span> = Presente");
            sb.AppendLine("<span class='ausente'>✗</span> = Ausente");
            sb.AppendLine("<span>-</span> = Sem registro</p>");
            sb.AppendLine("</div>");

            // Resumo
            sb.AppendLine("<div class='resumo'>");
            sb.AppendLine("<h3>Resumo</h3>");
            foreach (var data in datas)
            {
                var presencasNaData = presencas.Where(p => p.TurmaHorario.Data.Date == data);
                var totalPresentes = presencasNaData.Count(p => p.Presente);
                var totalAusentes = presencasNaData.Count(p => !p.Presente);

                sb.AppendLine($"<p>{data:dd/MM/yyyy}: {totalPresentes} presentes, {totalAusentes} ausentes</p>");
            }
            sb.AppendLine("</div>");

            sb.AppendLine("</div>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
